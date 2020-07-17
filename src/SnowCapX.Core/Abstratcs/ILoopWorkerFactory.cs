using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Core.Abstratcs
{
    public interface ILoopWorkerFactory
    {
        public ILoopWorker Create(string key, Action callback);
        public ILoopWorker Create(string key, Func<Task> callback);
    }
}
