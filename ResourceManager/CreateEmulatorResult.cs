using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.sjsu.mobiletaas.resourcemanager
{
    public class CreateEmulatorResult
    {
        public EmulatorInformation emulatorInformation { get; set; }
        public string errorMessage { get; set; }
        public bool success { get; set; }
    }
}
