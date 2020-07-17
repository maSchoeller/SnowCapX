using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core.Abstratcs
{
    public interface ILoopWorker
    {
        bool IsRunning { get; }
        bool TryStart();
        bool TryStop();

    }
}
