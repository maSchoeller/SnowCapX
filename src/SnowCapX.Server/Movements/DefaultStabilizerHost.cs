using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SnowCapX.Server.Movements
{
    public class DefaultStabilizerHost : ISnowCapStabilizerHost
    {
        private readonly StabilizerOptions _options;
        private readonly ISnowCapStabilizer _stabilizer;
        private readonly MovementTarget _target;
        private readonly ILogger<DefaultStabilizerHost>? _logger;
        private CancellationTokenSource _tokenSource;

        public DefaultStabilizerHost(IOptions<StabilizerOptions> options,
            ISnowCapStabilizer stabilizer,
            MovementTarget target,
            ILogger<DefaultStabilizerHost>? logger = null)
            : this(options.Value, stabilizer, target, logger)
        { }

        public DefaultStabilizerHost(StabilizerOptions options,
            ISnowCapStabilizer stabilizer,
            MovementTarget target,
            ILogger<DefaultStabilizerHost>? logger = null)
        {
            _tokenSource = new CancellationTokenSource();
            _options = options;
            _stabilizer = stabilizer;
            _target = target;
            _logger = logger;
        }
        public bool IsRunning { get; private set; }

        public bool TryStart()
        {
            _tokenSource = new CancellationTokenSource();
            var thread = new Thread(LoopDelegate)
            {
                IsBackground = true,
                Priority = ThreadPriority.Highest,
            };
            thread.Start();
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
                    _stabilizer.Invoke(_target);
                    _logger?.LogTrace($"Stabilizer run finished.");
                }
                catch (Exception e)
                {
                    _logger?.LogError(e, $"Stabilizer run crashed add time: {DateTime.Now}");
                }
                var wait = _options.Duration - DateTime.Now.Subtract(offset).Milliseconds;
                if (wait > 0) Thread.Sleep(wait);
                offset = DateTime.Now;
            }
        }
    }
}
