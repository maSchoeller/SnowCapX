using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Utilities
{
    public interface ILoopWorkerFactory
    {
        public ILoopWorker Create(string key, Action<CancellationToken> callback);

        public ILoopWorker Create(string key, Func<CancellationToken, Task> callback);
    }
}
