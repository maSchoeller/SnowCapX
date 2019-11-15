using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Contexts;
using SnowCapX.Server.Movements;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStabilization(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddHostedService<Stabilizer>();
            return collection;
        }

        public static IServiceCollection AddMovement(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.TryAddSingleton<MovementHost>();

            //Todo: Handle if one of these three Services is registers remove the other two services or handle an alternative way?
            collection.TryAddSingleton<IMovementTarget>(s => s.GetRequiredService<MovementHost>());
            collection.TryAddSingleton<IMovementSource>(s => s.GetRequiredService<MovementHost>());
            collection.TryAddSingleton<IMovementHost>(s => s.GetRequiredService<MovementHost>());
            collection.TryAddSingleton<IMovementResolver, MovementResolver>();
            return collection;
        }

        public static IServiceCollection AddDroneContexts(this IServiceCollection collection, IDroneServerEnviroment enviroment)
        {
            collection.TryAddSingleton<IDroneServerContext, DroneServerContext>();
            collection.TryAddSingleton(enviroment);
            return collection;
        }
    }
}
