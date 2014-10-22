using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.sjsu.mobiletaas.resourcemanager
{
    public class ServerInformation : ServerStatus
    {
        public ServerInformation(String instanceId, String ipAddress)
        {
            this.InstanceId = instanceId;
            this.IpAddress = ipAddress;
            this.IsPrimary = false;
        }

        public Boolean IsPrimary { set; get; }

        public String InstanceId { private set; get; }

        public String IpAddress { private set; get; }

       
    }
}
