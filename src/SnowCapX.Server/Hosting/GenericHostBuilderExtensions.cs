using Microsoft.Extensions.Hosting;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Hosting
{
    public static class GenericHostBuilderExtensions
    {
        public static IHostBuilder UseDroneServerDefaults(this IHostBuilder builder, Action<IDroneServerBuilder> configure)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var droneServer = new GenericDroneServerHostBuilder(builder);
            configure?.Invoke(droneServer);
            droneServer.Build();
            return builder;
        }
    }
}
