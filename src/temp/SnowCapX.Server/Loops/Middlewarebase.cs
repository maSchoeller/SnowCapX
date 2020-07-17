
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Loops
{
    public abstract class MiddlewareBase : IControlLoopMiddleware
    {
        protected readonly ControlPoint _next;

        public MiddlewareBase(ControlPoint next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync()
        {
            if (await CanInvokeAsync())
                await InnerInvokeAsync(_next);
            else
                await _next();
        }

        protected virtual Task<bool> CanInvokeAsync()
            => Task.FromResult(true);

        protected abstract Task InnerInvokeAsync(ControlPoint next);
    }
}
