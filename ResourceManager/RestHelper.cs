using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

namespace com.sjsu.mobiletaas.resourcemanager
{
    public class RestHelper
    {
        private readonly String BASE_URL = "http://{0}:3345/node/androidcontrol/";
        private String serverUrl;

        public RestHelper(String ipAddress)
        {
            serverUrl = String.Format(BASE_URL, ipAddress);
        }

        public ServerStatus GetStatus()
        {
            RestSharp.RestClient client = new RestSharp.RestClient(serverUrl);
            var request = new RestRequest("status", Method.GET);
            return client.Get<ServerStatus>(request).Data;
        }

        public CreateEmulatorResult CreateEmulator(int version, int memory)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(serverUrl);
            var request = new RestRequest("startemulator/{version}/{memory}", Method.GET);
            request.AddUrlSegment("version", version.ToString());
            request.AddUrlSegment("memory", memory.ToString());
            return client.Get<CreateEmulatorResult>(request).Data;
        }

        public bool StopEmulator(String emulatorId)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(serverUrl);
            var request = new RestRequest("stopemulator/{id}", Method.GET);
            request.AddUrlSegment("id", emulatorId.ToString());
            return client.Get(request).StatusCode == HttpStatusCode.OK;
        }

        public byte[] GetScreenShot(String emulatorId)
        {
            WebClient w = new WebClient();
            try
            {
             return   w.DownloadData(serverUrl + "getscreenshot/" + emulatorId);
            }catch{
                return new byte[]{};
            }
        }

        public bool LaunchApplication(String emulatorId, String intentName)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(serverUrl);
            var request = new RestRequest("launch/{id}", Method.POST);
            request.AddUrlSegment("id", emulatorId.ToString());
            request.AddParameter("appname", intentName);
            return client.Post(request).StatusCode == HttpStatusCode.OK;
        }

        public bool LaunchUrl(String emulatorId, String url)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(serverUrl);
            var request = new RestRequest("launchurl/{id}", Method.POST);
            request.AddUrlSegment("id", emulatorId.ToString());
            request.AddParameter("url", url);
            return client.Post(request).StatusCode == HttpStatusCode.OK;
        }

        public bool InstallApplication(String emulatorId, byte[] data)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(serverUrl);
            var request = new RestRequest("install/{id}", Method.POST);
            request.AddUrlSegment("id", emulatorId.ToString());
            request.AddFile("apkfile", data, "install.apk");
            return client.Post(request).StatusCode == HttpStatusCode.OK;
        }
        
    }
}
