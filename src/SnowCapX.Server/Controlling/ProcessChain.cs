using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SnowCapX.Core.Abstratcs;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Enviroment;
using SnowCapX.Server.Movements;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Server.Controlling
{
    internal class ProcessChain : ILoopHost
    {
        private readonly ProcessChainOptions _options;
        private readonly ProcessPoint _pipe;
        private readonly ProcessContext _context;
        private readonly ILogger<ProcessChain>? _logger;
        private readonly MovementHost _movementHost;
        private CancellationTokenSource _tokenSource;
        private Thread? _processChainThread;

        public ProcessChain(IOptions<ProcessChainOptions> options,
          ProcessPoint pipe,
          MovementHost movementHost,
          SnowCapContext snowCapContext,
          ILogger<ProcessChain>? logger = null)
            : this(options.Value, pipe, movementHost, snowCapContext, logger)
        { }

        public ProcessChain(ProcessChainOptions options,
          ProcessPoint pipe,
          MovementHost movementHost,
          ProcessContext vehicleContext,
          ILogger<ProcessChain>? logger = null)
        {
            _movementHost = movementHost ?? throw new ArgumentNullException(nameof(movementHost));
            _context = vehicleContext ?? throw new ArgumentNullException(nameof(vehicleContext));
            _pipe = pipe ?? throw new ArgumentNullException(nameof(pipe));
            _options = options;
            _logger = logger;
            _tokenSource = new CancellationTokenSource();
        }


        public bool IsRunning { get; private set; }

        public bool TryStart()
        {
            _tokenSource = new CancellationTokenSource();
            _processChainThread = new Thread(LoopDelegate)
            {
                IsBackground = true,
                Priority = ThreadPriority.Highest,
            };
            _processChainThread.Start();
            IsRunning = true;
            return true;
        }

        public bool TryStop()
        {
            _tokenSource.Cancel();
            IsRunning = false;
            return true;
        }

        private void LoopDelegate()
        {
            var token = _tokenSource.Token;
            DateTime offset = DateTime.Now;
            while (!token.IsCancellationRequested)
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
                var wait = _options.Duration - DateTime.Now.Subtract(offset).Milliseconds;
                if (wait > 0) Thread.Sleep(wait);
                offset = DateTime.Now;
            }
        }

    }
}
