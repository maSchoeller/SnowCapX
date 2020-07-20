using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Enviroment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Controlling
{
    public abstract class MiddlewareBase : IProcessPointMiddleware
    {
        protected MiddlewareBase()
        {
        }

        public async Task InvokeAsync(SnowCapContext context, ProcessPoint next)
        {
            if (await CanInvokeAsync())
                await InnerInvokeAsync(context, next);
            else
                await next(context);
        }

        protected virtual Task<bool> CanInvokeAsync()
            => Task.FromResult(true);

        protected abstract Task InnerInvokeAsync(SnowCapContext context, ProcessPoint next);
    }
}
