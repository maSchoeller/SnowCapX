using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Core.Utilities
{
    internal class GlobalExceptionCatcher : IHostedService
    {
        public GlobalExceptionCatcher(ILogger<GlobalExceptionCatcher>? logger = null)
        {
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                logger?.LogError(e.Exception, $"Exception from unobserved Task.");
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                logger?.LogError(ex, $"Global uncatched Exception.");
            };
        }

        public Task StartAsync(CancellationToken cancellationToken) 
            => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken) 
            => Task.CompletedTask;
    }
}
