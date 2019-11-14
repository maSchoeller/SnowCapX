using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Utilities
{
    public interface ILoopWorker : IDisposable
    {

        bool IsRunning { get; }
        LoopWorkerOptions Options { get; }

        void StartWorker();
        void StopWorker();

    }
}
