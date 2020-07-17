using Microsoft.Extensions.DependencyInjection;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Loops
{
    public static class ControlLoopBuilderExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMiddelware"></typeparam>
        /// <param name="builder"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IControlLoopBuilder Use<TMiddelware>(this IControlLoopBuilder builder, params object[] args) where TMiddelware : IControlLoopMiddleware
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Use(typeof(TMiddelware), args);
            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="middlewaretype"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IControlLoopBuilder Use(this IControlLoopBuilder builder, Type middlewaretype, params object[] args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (middlewaretype is null)
            {
                throw new ArgumentNullException(nameof(middlewaretype));
            }

            builder.Use(point =>
            {
                if (!typeof(IControlLoopMiddleware).IsAssignableFrom(middlewaretype))
                    throw new ArgumentException($"{middlewaretype.Name} must be implementing { typeof(IControlLoopMiddleware).Name}.");
                var conArgs = new object[args.Length + 1];
                conArgs[0] = point;
                Array.Copy(args, 0, conArgs, 1, args.Length);
                var middleware = ActivatorUtilities.CreateInstance(builder.ApplicationServices, middlewaretype, conArgs) as IControlLoopMiddleware
                    ?? throw new Exception(); //Todo: Change Exception to a more meaningful exception.
                return middleware.InvokeAsync;
            });
            return builder;
        }

        public static IControlLoopBuilder Invoke(this IControlLoopBuilder builder, Func<IDroneServerContext, ControlPoint, Task> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            builder.Use<InlineMiddleware>(builder.ApplicationServices.GetRequiredService<IDroneServerContext>(), middleware);
            return builder;
        }
    }
}
