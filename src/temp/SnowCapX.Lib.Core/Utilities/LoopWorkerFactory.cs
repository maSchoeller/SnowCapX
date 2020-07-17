using Microsoft.Extensions.Configuration;
using SnowCapX.Lib.Abstracts.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Utilities
{
    internal class LoopWorkerFactory : ILoopWorkerFactory
    {
        public static readonly string ConfigurationSectionName = "LoopWorker";
        private readonly IConfiguration _configuration;
        public LoopWorkerFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ILoopWorker Create(string key, Action<CancellationToken> callback)
        {
            var opt = GetOptions(key);
            return new LoopWorker(opt, callback);
        }

        public ILoopWorker Create(string key, Func<CancellationToken, Task> callback)
        {
            var opt = GetOptions(key);
            return new LoopWorker(opt, callback);
        }

        private LoopWorkerOptions GetOptions(string key)
            => _configuration.GetSection($"{ConfigurationSectionName}:{key}").Get<LoopWorkerOptions>();

    }
}
