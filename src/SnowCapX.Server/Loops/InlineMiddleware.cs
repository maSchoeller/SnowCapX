using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Loops
{
    internal class InlineMiddleware : ILoopControlMiddleware
    {
        private readonly Func<IDroneServerContext, ControlPoint, Task> _func;
        private readonly ControlPoint _next;
        private readonly IDroneServerContext _context;

        public InlineMiddleware(
            ControlPoint next,
            Func<IDroneServerContext, ControlPoint, Task> func,
            IDroneServerContext context)
        {
            _func = func;
            _next = next;
            _context = context;
        }


        public Task InvokeAsync()
        {
            return _func(_context, _next);
        }
    }
}
