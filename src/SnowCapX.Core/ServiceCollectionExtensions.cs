using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SnowCapX.Core.Abstratcs;
using SnowCapX.Core.Regulations;
using SnowCapX.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoopWorkers(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            //collection.TryAddSingleton<ILoopWorkerFactory, >();
            return collection;
        }

        public static IServiceCollection AddGlobalExceptionCatching(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddHostedService<GlobalExceptionCatcher>();
            return collection;
        }

        //public static IServiceCollection AddSettings(this IServiceCollection collection)
        //{
        //    if (collection is null)
        //    {
        //        throw new ArgumentNullException(nameof(collection));
        //    }

        //    collection.TryAddSingleton<ISettings, SnowCapXSettings>();
        //    return collection;
        //}

        public static IServiceCollection AddPidRegulators(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.TryAddSingleton<IPidRegulatorFactory, PidRegulatorFactory>();
            return collection;
        }

        public static IServiceCollection AddSnowCapCore(this IServiceCollection collection)
        {
            collection.AddPidRegulators();
            collection.AddGlobalExceptionCatching();
            collection.AddLoopWorkers();
            return collection;
        }
    }
}
