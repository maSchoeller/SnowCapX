using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Enviroment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Controlling
{
    internal class InlineMiddleware : IProcessPointMiddleware
    {
        private readonly Func<SnowCapContext, ProcessPoint, Task> _callback;

        public InlineMiddleware(
            Func<SnowCapContext, ProcessPoint, Task> callabck)
        {
            _callback = callabck;
        }

        public Task InvokeAsync(SnowCapContext context, ProcessPoint next)
        {
            return _callback(context, next);
        }
    }
}
