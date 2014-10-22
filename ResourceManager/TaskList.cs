using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.sjsu.mobiletaas.resourcemanager
{
    public class TaskInfo
    {
        public String ID { private set; get; }

        public DateTime RequestDate { private set; get; }

        public String Status { set; get; }

        public int UserID { private set; get; }

        public TaskInfo(int userid)
        {
            UserID = userid;
            ID = Guid.NewGuid().ToString();
            RequestDate = DateTime.Now;
        }

        public string Description { get; set; }

        public string Action { get; set; }

        public int Version { get; set; }

        public int Memory { get; set; }

        public String EmulatorID {get; set;}
    }
}
