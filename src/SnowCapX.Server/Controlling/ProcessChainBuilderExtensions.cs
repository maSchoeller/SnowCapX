using Microsoft.Extensions.DependencyInjection;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Enviroment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Controlling
{
    public static class ProcessChainBuilderExtensions
    {
        public static ProcessChainBuilder Use<TMiddelware>(this ProcessChainBuilder builder, params object[] args)
            where TMiddelware : IProcessPointMiddleware
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.Use(typeof(TMiddelware), args);
            return builder;
        }

        public static ProcessChainBuilder Use(this ProcessChainBuilder builder, Type middlewaretype, params object[] args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (middlewaretype is null)
            {
                throw new ArgumentNullException(nameof(middlewaretype));
            }
            if (!typeof(IProcessPointMiddleware).IsAssignableFrom(middlewaretype))
                throw new ArgumentException($"{middlewaretype.Name} must be implementing { typeof(IProcessPointMiddleware).Name}.");

            builder.Use(next =>
            {
                var middleware = ActivatorUtilities.CreateInstance(builder.ApplicationServices, middlewaretype, args)
                    as IProcessPointMiddleware ?? throw new InvalidOperationException($"Could not instantiate the class {middlewaretype.FullName}, check if all Constructor arguments are registered in the DI container.");
                return context => middleware.InvokeAsync(context, next);
            });
            return builder;
        }

        public static ProcessChainBuilder Invoke(this ProcessChainBuilder builder, Func<VehicleContext, ProcessPoint, Task> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            builder.Use<InlineMiddleware>(middleware);
            return builder;
        }
    }
}
