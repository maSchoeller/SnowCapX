using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SnowCapX.Lib.Core;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Contexts;
using SnowCapX.Server.Loops;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Hosting
{
    public class GenericDroneServerHostBuilder : IDroneServerBuilder
    {
        private readonly IHostBuilder _builder;
        private Action<IControlLoopBuilder>? _configure;
        private Action<IServiceCollection>? _confServices;
        private Type? _startupType;

        public IDroneServerEnviroment Enviroment { get; }

        public GenericDroneServerHostBuilder(IHostBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            Enviroment = new DroneServerEnviroment();
        }

        public void ConfigureLoop(Action<IControlLoopBuilder> action)
            => _configure += action;

        public void ConfigureServices(Action<IServiceCollection> action)
            => _confServices += action;

        public void UseStartup<TStartup>()
            => _startupType = typeof(TStartup);

        public void Build()
        {
            _builder.ConfigureServices((context, collection) =>
            {
                if (!(_startupType is null))
                {
                    var startup = StartupResolver.CreateStartup(_startupType, context);
                    if (startup is null)
                    {
                        throw new InvalidOperationException($"Startup({_startupType.FullName}) has no matching constructor. " +
                            $"Possible constructors are: parameter less, argument:{typeof(IHostEnvironment).Name}, argument:{typeof(IConfiguration).Name} or a combination from booth.");
                    }

                    ConfigureServices(c => StartupResolver.InvokeConfigureServices(startup, c, context));
                    ConfigureLoop(b => StartupResolver.InvokeConfigureControlLoop(startup, b, context));
                }
                ConfigureServices(c => AddDefaultServices(c, Enviroment, context.Configuration));
                _confServices?.Invoke(collection);
                collection.AddHostedService(p =>
                {
                    var loopBuilder = new ControlLoopBuilder(p);
                    _configure?.Invoke(loopBuilder);
                    return loopBuilder.Build();
                });
            });
        }

        private static void AddDefaultServices(
            IServiceCollection collection, 
            IDroneServerEnviroment enviroment, 
            IConfiguration configuration)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (enviroment is null)
            {
                throw new ArgumentNullException(nameof(enviroment));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            collection.AddSettingsWithDB(configuration);
            collection.AddDroneContexts(enviroment);
            collection.AddLoopWorkers();
            collection.AddMovement();
            collection.AddPidRegulators();
            collection.AddGlobalExceptionCatching();
            collection.AddStabilization();
        }

    }
}
