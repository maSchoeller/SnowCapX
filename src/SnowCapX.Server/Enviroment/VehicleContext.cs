using Microsoft.Extensions.Configuration;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Enviroment
{
    public class VehicleContext
    {
        public VehicleContext()
        {
            //Todo: add real services
            Properties = new Dictionary<object, object>();
        }
        
        public IServiceProvider Services { get; } = null!;
        public IDictionary<object, object> Properties { get; }
        public MovementSource Movement { get; } = null!;
        public IConfiguration Configuration { get; } = null!;

    }
}
