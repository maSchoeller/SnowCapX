using Microsoft.Extensions.Hosting;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureSnowCapHost(this IHostBuilder builder, Action<ISnowCapServerBuilder> configure)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var droneServer = new SnowCapServerBuilder(builder);
            configure?.Invoke(droneServer);
            droneServer.Build();
            return builder;
        }
    }
}
