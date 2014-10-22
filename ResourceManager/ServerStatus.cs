using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.sjsu.mobiletaas.resourcemanager
{
    public class ServerStatus
    {
        public int emulatorCount { get; set; }
        public List<EmulatorInformation> emulatorInformation { get; set; }
        public int freeMemory { get; set; }
        public int maxSupported { get; set; }
        public int totalMemory { get; set; }

        public bool hasCapacity()
        {
            return !(maxSupported == emulatorCount);
        }

        public bool isIdle()
        {
            return emulatorCount == 0;
        }

        public void updateStatus(ServerStatus s)
        {
            try
            {
                this.totalMemory = s.totalMemory;
                this.maxSupported = s.maxSupported;
                this.freeMemory = s.freeMemory;
                this.emulatorCount = s.emulatorCount;
                this.emulatorInformation = s.emulatorInformation;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
