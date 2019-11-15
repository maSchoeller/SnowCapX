using SnowCapX.Lib.Abstracts.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Utilities
{
    internal class LoopWorker : ILoopWorker
    {
        private readonly Func<CancellationToken, Task> _callback;
        private CancellationTokenSource? _cts;
        private Task? _task;

        public LoopWorker(LoopWorkerOptions options, Func<CancellationToken, Task> callback)
        {
            _callback = callback;
            IsRunning = false;
            Options = options;
        }

        public LoopWorker(LoopWorkerOptions setting, Action<CancellationToken> callback)
            : this(setting, c => { callback(c); return Task.CompletedTask; })
        {
        }

        public LoopWorkerOptions Options { get; }
        public bool IsRunning { get; private set; }

        private async Task Loop()
        {
            var token = _cts!.Token;
            DateTime offset = DateTime.Now;
            while (!token.IsCancellationRequested)
            {
                await _callback.Invoke(token)
                    .ConfigureAwait(false);
                var wait = (Options?.Delay ?? 100) - DateTime.Now.Subtract(offset).Milliseconds;
                if (wait > 0)
                {
                    await Task.Delay(wait)
                        .ConfigureAwait(false);
                }
                offset = DateTime.Now;
            }
        }

        public void StartWorker()
        {
            if (IsRunning) throw new InvalidOperationException($"Loop is already running.");
            _cts = new CancellationTokenSource();
            _task = new Task(() => Loop().Wait(), _cts.Token, TaskCreationOptions.LongRunning);
            _task.Start();
            IsRunning = true;
        }

        public void StopWorker()
        {
            if (!IsRunning) throw new InvalidOperationException($"Loop is not running.");
            _cts!.Cancel();
            IsRunning = false;
        }

        public void Dispose()
        {
            if (IsRunning)
            {
                StopWorker();
            }
        }

    }
}
