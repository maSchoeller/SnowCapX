using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SnowCapX.Lib.Abstracts.Settings;
using SnowCapX.Lib.Abstracts.Utilities;
using SnowCapX.Lib.Core.Utilities;
using SnowCapX.Lib.Core.Settings;
using SnowCapX.Lib.Core.Settings.Databases;

namespace SnowCapX.Lib.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoopWorkers(this IServiceCollection collection)
        {
            collection.TryAddSingleton<ILoopWorkerFactory, LoopWorkerFactory>();
            return collection;
        }

        public static IServiceCollection AddGlobalExceptionCatching(this IServiceCollection collection)
        {
            collection.TryAddSingleton<GlobalExceptionCatcher>();
            return collection;
        }

        public static IServiceCollection AddSettings(this IServiceCollection collection, bool includeDatabase = false)
        {
            //Todo: Adding networksettings
            if (includeDatabase)
            {
                collection.TryAddEnumerable(ServiceDescriptor.Singleton<ISettingsProvider, DatabaseSettingsProvider>());
            }
            collection.TryAddSingleton<ISettings, SnowCapXSettings>();
            return collection;
        }

        public static IServiceCollection AddPidRegulators(this IServiceCollection collection)
        {
            collection.TryAddSingleton<IPidRegulatorFactory, PidRegulatorFactory>();
            return collection;
        }
    }
}
