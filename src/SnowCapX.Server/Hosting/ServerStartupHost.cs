using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Controlling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Server.Hosting
{
    internal class ServerStartupHost : IHostedService
    {
        private readonly ProcessChain _processChain;
        private readonly IVehicleStabilizer _stabilizer;
        private readonly ILogger<ServerStartupHost>? _logger;

        public ServerStartupHost(ProcessChain processChain,
            IVehicleStabilizer stabilizer,
            ILogger<ServerStartupHost>? logger = null)
        {
            _processChain = processChain;
            _stabilizer = stabilizer;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                const int maxTries = 4;
                BootstrapProcessChain(maxTries);
                BootstrapStabilizer(maxTries);
            });
        }

        private void BootstrapStabilizer(int maxTries)
        {
            if (_stabilizer.IsRunning)
            {
                _logger?.LogWarning("Stabilizer is already Running, now startup was invoked.");
            }
            else
            {
                int tries = 1;
                do
                {
                    _logger.LogInformation("Start {0}. time to start the Stabilizer", tries);
                    if (_stabilizer.TryStart())
                    {
                        _logger?.LogWarning("{0}.try to start the Stabilizer failed.", tries);
                    }
                    tries++;
                } while (!_stabilizer.IsRunning && tries < maxTries);
                if (_stabilizer.IsRunning)
                {
                    _logger.LogInformation("Starting successful the Stabilizer after {0}. tries", tries);
                }
                else
                {
                    _logger?.LogCritical("Can't start the Stabilizer after {0}. tries.", tries);
                }
            }
        }
        private void BootstrapProcessChain(int maxTries)
        {
            if (_processChain.IsRunning)
            {
                _logger?.LogWarning("ProcessChain is already Running, now startup was invoked.");
            }
            else
            {
                int tries = 1;
                do
                {
                    _logger.LogInformation("Start {0}. time to start the ProcessChain", tries);
                    if (_processChain.TryStart())
                    {
                        _logger?.LogWarning("{0}.try to start the ProcessChain failed.", tries);
                    }
                    tries++;
                } while (!_processChain.IsRunning && tries < maxTries);
                if (_processChain.IsRunning)
                {
                    _logger.LogInformation("Starting successful the ProcessChain after {0}. tries", tries);
                }
                else
                {
                    _logger?.LogCritical("Can't start the ProcessChain after {0}. tries.", tries);
                }
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            //TODO: handle shutdown same as startup
            _processChain.TryStop();
            _stabilizer.TryStop();
            return Task.CompletedTask;
        }
    }
}
