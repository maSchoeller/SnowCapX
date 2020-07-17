using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Loops
{
    internal class ControlLoopBuilder : IControlLoopBuilder
    {

        private readonly IList<Func<ControlPoint, ControlPoint>> middlewareList;

        public ControlLoopBuilder(IServiceProvider provider)
        {
            ApplicationServices = provider;
            middlewareList = new List<Func<ControlPoint, ControlPoint>>();
        }

        public IServiceProvider ApplicationServices { get; }

        public void Use(Func<ControlPoint, ControlPoint> middleware) =>
            middlewareList.Add(middleware);

        public IControlLoop Build()
        {
            static Task Last() => Task.CompletedTask;

            ControlPoint app = Last;
            foreach (var mid in middlewareList.Reverse())
                app = mid(app);

            return ActivatorUtilities.
                CreateInstance<ControlLoop>(ApplicationServices, app);
        }


    }
}
