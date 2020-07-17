using Microsoft.Extensions.DependencyInjection;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Enviroment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Controlling
{
    public  class ProcessChainBuilder
    {
        private readonly IList<Func<ProcessPoint, ProcessPoint>> middlewareList;

        internal ProcessChainBuilder(IServiceProvider provider)
        {
            ApplicationServices = provider ?? throw new ArgumentNullException(nameof(provider));
            middlewareList = new List<Func<ProcessPoint, ProcessPoint>>();
        }

        public IServiceProvider ApplicationServices { get; }

        internal ProcessChain Build()
        {
            static Task Last(VehicleContext _) => Task.CompletedTask;
            ProcessPoint app = Last;
            foreach (var mid in middlewareList.Reverse())
                app = mid(app);

            return ActivatorUtilities.
                CreateInstance<ProcessChain>(ApplicationServices, app);
        }

        public void Use(Func<ProcessPoint, ProcessPoint> middleware) =>
            middlewareList.Add(middleware);
    }
}
