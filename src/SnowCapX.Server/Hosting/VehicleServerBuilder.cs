using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Controlling;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Hosting
{
    internal class VehicleServerBuilder : IVehicleServerBuilder
    {
        private readonly IHostBuilder _builder;
        private Action<ProcessChainBuilder>? _configure;
        private Action<IServiceCollection>? _confServices;
        private Type? _startupType;

        public bool UseSameStartup { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public VehicleServerBuilder(IHostBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public void ConfigureProcessChain(Action<ProcessChainBuilder> callback)
            => _configure = callback;

        public void ConfigureServices(Action<IServiceCollection> callback)
            => _confServices += callback;

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
                    ConfigureProcessChain(b => StartupResolver.InvokeConfigureProcessChain(startup, b, context));
                }                
                _confServices?.Invoke(collection);
                collection.TryAddSingleton(p =>
                {
                    var builder = new ProcessChainBuilder(p);
                    _configure?.Invoke(builder);
                    return builder.Build();
                });
            });
        }

    }
}
