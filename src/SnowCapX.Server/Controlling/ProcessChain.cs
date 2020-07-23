using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SnowCapX.Core.Abstratcs;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Enviroment;
using SnowCapX.Server.Movements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Server.Controlling
{
    internal class ProcessChain
    {
        private readonly ProcessPoint _pipe;
        private readonly ProcessContext _context;
        private readonly ILogger<ProcessChain>? _logger;
        private readonly MovementHost _movementHost;

        public ProcessChain(
          ProcessPoint pipe,
          MovementHost movementHost,
          ProcessContext vehicleContext,
          ILogger<ProcessChain>? logger = null)
        {
            _movementHost = movementHost ?? throw new ArgumentNullException(nameof(movementHost));
            _context = vehicleContext ?? throw new ArgumentNullException(nameof(vehicleContext));
            _pipe = pipe ?? throw new ArgumentNullException(nameof(pipe));
            _logger = logger;
        }


        public bool IsRunning { get; private set; }

        public bool TryStart()
        {
            Timer t = new Timer(LoopDelegate,null,Timeout.Infinite,Timeout.Infinite);
            return false;
        }

        public bool TryStop()
        {
            throw new NotImplementedException();
        }

        private void LoopDelegate(object _)
        {
            try
            {
                _pipe(_context);
                _movementHost.Invoke();
                _logger?.LogTrace($"ProcessChain run finished.");
            }
            catch (Exception e)
            {
                _logger?.LogError(e, $"ProcessChain run crashed add time: {DateTime.Now}");
            }
        }

    }
}
