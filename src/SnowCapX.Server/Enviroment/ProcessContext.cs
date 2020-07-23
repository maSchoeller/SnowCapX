﻿using Microsoft.Extensions.Configuration;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Enviroment
{
    public class ProcessContext
    {
        public ProcessContext(IServiceProvider provider,
            IConfiguration configuration,
            MovementSource movement)
        {
            Properties = new Dictionary<object, object>();
            Services = provider;
            Configuration = configuration;
            Movement = movement;
        }

        public virtual IServiceProvider Services { get; } = null!;
        public virtual IDictionary<object, object> Properties { get; }
        public virtual MovementSource Movement { get; } = null!;
        public virtual IConfiguration Configuration { get; } = null!;

    }
}
