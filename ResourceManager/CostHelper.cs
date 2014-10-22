using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.sjsu.mobiletaas.resourcemanager
{
    internal class CostHelper
    {
        public static Decimal getCost(int verison, int memory)
        {
            if (memory <= 512)
            {
                return 0.010m;
            }
            else if (memory <= 1024)
            {
                return 0.030m;
            }
            else
            {
                return 0.050m;
            }
        }
    }
}
