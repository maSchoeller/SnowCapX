using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SnowCapX.Server.Abstracts;
using SnowCapX.Lib.Abstracts.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Server.Loops
{
    /// <summary>
    /// contains the control loop and the logic to control the control loop.
    /// </summary>
    internal class ControlLoop : IHostedService
    {
        private readonly ControlPoint _pipe;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<ControlLoop>? _logger;
        private readonly ILoopWorker _loopWorker;

        public ControlLoop(
            ILoopWorkerFactory loopWorkerFactory,
            ControlPoint pipe,
            IMovementHost movementHost,
            IHostApplicationLifetime lifetime,
            ILogger<ControlLoop>? logger)
        {
            if (loopWorkerFactory is null)
            {
                throw new ArgumentNullException(nameof(loopWorkerFactory));
            }
            if (movementHost is null)
            {
                throw new ArgumentNullException(nameof(movementHost));
            }
            _pipe = pipe ?? throw new ArgumentNullException(nameof(pipe));
            _lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            _loopWorker = loopWorkerFactory.Create(nameof(ControlLoop), LoopDelegate);
            _logger = logger;

            void LoopDelegate(CancellationToken token)
            {
                try
                {
                    _pipe().Wait();
                    movementHost.Invoke();
                    _logger?.LogTrace($"Loop run finished.");
                }
                catch (Exception e)
                {
                    //Todo: add logging and catch logic.
                    _logger?.LogError(e, $"Loop run crashed add time: {DateTime.Now}");
                }


            }
        }

        public void Dispose()
        {
            _loopWorker.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger?.LogInformation($"Control Loop is starting.");
            _lifetime.ApplicationStarted.Register(() =>
            {
                _logger?.LogInformation($"Control Loop is running.");
                _loopWorker.StartWorker();
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger?.LogInformation($"Stop loop pipe.");
            if (_loopWorker.IsRunning)
            {
                _loopWorker.StopWorker();
            }
            return Task.CompletedTask;
        }
    }
}
