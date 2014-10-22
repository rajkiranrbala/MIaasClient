using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Specialized;

namespace com.sjsu.mobiletaas.resourcemanager
{
    public class ResourceManager
    {
        public static readonly String PRIMARY_INSTANCE = "";
        public static readonly String SECONDARY_INSTANCE = "";
        private static OrderedDictionary mapServerToServerInfo = new OrderedDictionary();

        private Dictionary<String, String> mapEmulatorsToServers = new Dictionary<string, string>();
        private Dictionary<int, List<EmulatorInformation>> mapUsersToEmulators = new Dictionary<int, List<EmulatorInformation>>();
        private Queue<TaskInfo> emulatorCreationTasks = new Queue<TaskInfo>();

        private Thread threadEmulatorTasks = null;
        private Thread threadInstanceTasks = null;

        private static ResourceManager resourceManager = null;

        public static ResourceManager GetResourceManager()
        {
            if (resourceManager == null)
            {
                resourceManager = new ResourceManager();
            }
            return resourceManager;
        }

        private ResourceManager()
        {
            RestHelper helper = new RestHelper(PRIMARY_INSTANCE);
            ServerStatus s = helper.GetStatus();
            ServerInformation i = new ServerInformation("", PRIMARY_INSTANCE);
            i.updateStatus(s);
            i.IsPrimary = true;
            mapServerToServerInfo.Add(PRIMARY_INSTANCE, i);

            helper = new RestHelper(SECONDARY_INSTANCE);
            s = helper.GetStatus();
            i = new ServerInformation("", SECONDARY_INSTANCE);
            i.updateStatus(s);
            i.IsPrimary = true;
            mapServerToServerInfo.Add(SECONDARY_INSTANCE, i);
            //    threadEmulatorTasks = new Thread(new ThreadStart(ProcessTasks));
            threadInstanceTasks = new Thread(new ThreadStart(ServerTasks));
            threadInstanceTasks.Start();
        }

        public TaskInfo StartEmulator(int userid, int version, int memory)
        {
            TaskInfo t = new TaskInfo(userid);
            t.Action = "CreateEmulator";
            t.Description = String.Format("Create emulator with version:{0} and memory:{1}", version, memory);
            t.Status = "Pending";
            t.Version = version;
            t.Memory = memory;
            emulatorCreationTasks.Enqueue(t);
            if (threadEmulatorTasks == null)
            {
                threadEmulatorTasks = new Thread(new ThreadStart(ProcessTasks));
                threadEmulatorTasks.Start();
            }
            return t;
        }

        public void StopEmulator(int userid, String emulatorid)
        {
            if (!mapUsersToEmulators.ContainsKey(userid))
            {
                throw new Exception("No Such Emulator!");
            }
            List<EmulatorInformation> ems = mapUsersToEmulators[userid];
            EmulatorInformation e = ems.Where(p => p.name == emulatorid).FirstOrDefault();
            if (e == null)
            {
                throw new Exception("No Such Emulator!");
            }
            else
            {
                String server = mapEmulatorsToServers[e.name];
                RestHelper helper = new RestHelper(server);
                helper.StopEmulator(e.name);
                mapEmulatorsToServers.Remove(e.name);
                mapUsersToEmulators[userid].Remove(e);
                using (MobileTaasEntities entities = new MobileTaasEntities())
                {
                    var transaction = entities.Transactions.Where(p => p.EmulatorId == e.name).FirstOrDefault();
                    transaction.EndTime = DateTime.Now;
                    entities.SaveChanges();
                }
                (mapServerToServerInfo[server] as ServerInformation).updateStatus(helper.GetStatus());
            }
        }

        private RestHelper getHelper(int userid, String emulatorid)
        {
            if (!mapUsersToEmulators.ContainsKey(userid))
            {
                throw new Exception("No Such Emulator!");
            }
            else
            {
                List<EmulatorInformation> ems = mapUsersToEmulators[userid];
                if (ems.Exists(p => p.name == emulatorid))
                {
                    return new RestHelper(mapEmulatorsToServers[emulatorid]);
                }
                else
                {
                    throw new Exception("No Such Emulator!");
                }
            }
        }

        private bool ServerCreationInProgress = false;

        private void ProcessTasks()
        {
            while (emulatorCreationTasks.Count != 0)
            {
                TaskInfo info = emulatorCreationTasks.Peek();
                info.Status = "Processing";
                if (info.Action == "CreateEmulator")
                {
                    ServerInformation s = mapServerToServerInfo.Values.Cast<ServerInformation>().Where(p => p.hasCapacity()).FirstOrDefault();
                    while (s == null)
                    {
                        try
                        {
                            if (!ServerCreationInProgress)
                            {
                                threadInstanceTasks.Interrupt();
                            }
                            Thread.Sleep(1000 * 5);
                        }
                        catch (Exception)
                        {

                        }
                        s = mapServerToServerInfo.Values.Cast<ServerInformation>().Where(p => p.hasCapacity()).FirstOrDefault();
                    }
                    RestHelper helper = new RestHelper(s.IpAddress);
                    var result = helper.CreateEmulator(info.Version, info.Memory);
                    if (result.success)
                    {
                        mapEmulatorsToServers.Add(result.emulatorInformation.name, s.IpAddress);
                        List<EmulatorInformation> emulators = new List<EmulatorInformation>();
                        if (mapUsersToEmulators.ContainsKey(info.UserID))
                        {
                            emulators = mapUsersToEmulators[info.UserID];
                        }
                        else
                        {
                            mapUsersToEmulators.Add(info.UserID, emulators);
                        }
                        emulators.Add(result.emulatorInformation);
                        using (MobileTaasEntities entities = new MobileTaasEntities())
                        {
                            Transaction t = new Transaction()
                            {
                                Billed = false,
                                EmulatorId = result.emulatorInformation.name,
                                StartTime = DateTime.Now,
                                TaskId = info.ID,
                                Userid = info.UserID,
                                Version = info.Version,
                                Memory = info.Memory,
                                Rate = CostHelper.getCost(info.Version, info.Memory)
                            };
                            entities.Transactions.Add(t);
                            entities.SaveChanges();
                        }
                        s.updateStatus(helper.GetStatus());
                    }
                    else
                    {
                        s.updateStatus(helper.GetStatus());
                    }
                }
                emulatorCreationTasks.Dequeue();
            }
            threadEmulatorTasks = null;
        }

        private void ServerTasks()
        {
            while (true)
            {
                int idleServers = mapServerToServerInfo.Values.Cast<ServerInformation>().Where(p => p.isIdle()).Count();
                if (idleServers == 0)
                {
                    lock (this)
                    {
                        ServerCreationInProgress = true;
                    }
                    ServerInformation f = AmazonHelper.StartInstance();
                    mapServerToServerInfo.Add(f.IpAddress, f);
                    lock (this)
                    {
                        ServerCreationInProgress = false;
                    }
                }
                else if (idleServers > 1)
                {
                    var servers = mapServerToServerInfo.Values.Cast<ServerInformation>().Where(p => p.isIdle()).Where(p => p.IsPrimary == false).ToArray();
                    foreach (var server in servers)
                    {
                        mapServerToServerInfo.Remove(server.IpAddress);
                        AmazonHelper.TerminateInstance(server.InstanceId);
                    }
                }

                try
                {
                    threadEmulatorTasks.Interrupt();
                }
                catch (Exception)
                {

                }

                try
                {
                    System.Threading.Thread.Sleep(1000 * 60);
                }
                catch (Exception)
                {

                }
            }
        }

        public ServerInformation[] GetServerInformation()
        {
            return mapServerToServerInfo.Values.Cast<ServerInformation>().ToArray();
        }

        public Transaction[] GetPendingPayments(int userid)
        {
            using (MobileTaasEntities entities = new MobileTaasEntities())
            {
                return entities.Transactions.Where(p => p.Userid == userid).Where(p => p.EndTime.HasValue == true).Where(p => p.Billed == false).ToArray();
            }
        }

        public EmulatorInformation[] GetActiveEmulators(int userid)
        {
            if (mapUsersToEmulators.ContainsKey(userid))
            {
                return mapUsersToEmulators[userid].ToArray();
            }
            return new EmulatorInformation[] { };
        }

        public int AuthenticateUser(String username, String password)
        {
            using (MobileTaasEntities entities = new MobileTaasEntities())
            {
                var user = entities.Users.Where(p => p.UserName.Equals(username)).Where(p => p.Password.Equals(password)).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("Invalid username or password");
                }
                return user.UserId;
            }
        }

        public void InstallApplication(int userid, String emulatorid, byte[] data)
        {
            getHelper(userid, emulatorid).InstallApplication(emulatorid, data);
        }

        public void LaunchUrl(int userid, String emulatorid, String url)
        {
            getHelper(userid, emulatorid).LaunchUrl(emulatorid, url);
        }

        public void LaunchApp(int userid, String emulatorid, String app)
        {
            getHelper(userid, emulatorid).LaunchApplication(emulatorid, app);
        }

        public byte[] GetScreenShot(int userid, String emulatorid)
        {
            return getHelper(userid, emulatorid).GetScreenShot(emulatorid);
        }

        public TaskInfo[] GetTasks(int userid)
        {
            return emulatorCreationTasks.Where(p => p.UserID == userid).OrderBy(p => p.RequestDate).ToArray();
        }

        public void Pay(int userid)
        {
            using (MobileTaasEntities entities = new MobileTaasEntities())
            {
                var ts = entities.Transactions.Where(p => p.Userid == userid).Where(p => p.Billed == false).Where(p => p.EndTime.HasValue).Select(p => p);
                foreach (var t in ts)
                {
                    t.Billed = true;
                }
                entities.SaveChanges();
            }
        }
    }
}
