using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Utilities
{
    public class LoopWorkerOptions
    {
        public int Delay { get; }

        public LoopWorkerOptions(int delay)
        {
            Delay = delay;
        }
    }
}
