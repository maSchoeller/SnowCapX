using Microsoft.Extensions.Configuration;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Enviroment
{
    public class SnowCapContext
    {
        public SnowCapContext(IServiceProvider provider, 
            IConfiguration configuration,
            MovementSource movement)
        {
            Properties = new Dictionary<object, object>();
            Services = provider;
            Configuration = configuration;
            Movement = movement;
        }
        
        public IServiceProvider Services { get; }
        public IDictionary<object, object> Properties { get; }
        public MovementSource Movement { get; }
        public IConfiguration Configuration { get; }
    }
}
