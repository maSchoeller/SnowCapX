using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SnowCapX.Lib.Abstracts.Settings;
using SnowCapX.Lib.Abstracts.Utilities;
using SnowCapX.Lib.Core.Utilities;
using SnowCapX.Lib.Core.Settings;
using SnowCapX.Lib.Core.Settings.Databases;
using SnowCapX.Lib.Abstracts.Regulations;
using SnowCapX.Lib.Core.Regulations;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using SnowCapX.Lib.Core.Networking.SignalR;

namespace SnowCapX.Lib.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoopWorkers(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.TryAddSingleton<ILoopWorkerFactory, LoopWorkerFactory>();
            return collection;
        }

        public static IServiceCollection AddGlobalExceptionCatching(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.TryAddSingleton<GlobalExceptionCatcher>();
            return collection;
        }

        public static IServiceCollection AddSettings(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.TryAddSingleton<ISettings, SnowCapXSettings>();
            return collection;
        }

        public static IServiceCollection AddSettingsWithDB(this IServiceCollection collection, IConfiguration configuration)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            collection.TryAddEnumerable(ServiceDescriptor.Singleton<ISettingsProvider, DatabaseSettingsProvider>());
            var conPath = configuration.GetSection(DatabaseSettingsProvider.ConfigurationPath).Value 
                ?? $"Data Source={DatabaseSettingsProvider.DefaultSqliteFile}";

            collection.AddDbContext<SettingsContext>(
                optionsAction: builder => builder.UseSqlite(conPath), 
                contextLifetime: ServiceLifetime.Singleton, 
                optionsLifetime: ServiceLifetime.Transient);


            collection.AddSettings();
            return collection;
        }

        public static IServiceCollection AddPidRegulators(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.TryAddSingleton<IPidRegulatorFactory, PidRegulatorFactory>();
            return collection;
        }


        public static IServiceCollection AddSignalRHubConnectionFactory(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.TryAddSingleton<IHubConnectionFactory, HubConnectionFactory>();
            return collection;
        }

    }
}
