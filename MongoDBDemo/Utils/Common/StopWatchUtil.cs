using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo.Utils.Common
{
    public class StopWatchUtil
    {

        public static double CalMilSeconds(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (action != null)
                action();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            return ts.TotalMilliseconds;
        }
    }
}
