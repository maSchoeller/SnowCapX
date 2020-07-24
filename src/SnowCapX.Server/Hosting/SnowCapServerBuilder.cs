using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Controlling;
using SnowCapX.Server.Enviroment;
using SnowCapX.Server.Movements;
using System;

namespace SnowCapX.Server.Hosting
{
    internal class SnowCapServerBuilder : ISnowCapServerBuilder
    {
        private readonly IHostBuilder _builder;
        private Action<ProcessChainBuilder>? _configure;
        private Action<IServiceCollection>? _confServices;
        private Type? _startupType;

        public bool UseSameStartup { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SnowCapServerBuilder(IHostBuilder builder)
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
                    ConfigureServices(c => AddCoreServices(c));
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

        private void AddCoreServices(IServiceCollection services)
        {
            services.TryAddSingleton<ProcessContext>();
            services.TryAddSingleton<MovementHost>();
            services.TryAddSingleton<MovementSource>();
            services.TryAddSingleton<MovementTarget>();
            services.TryAddSingleton<IMovementTransformer, DefaultMovementTransformer>();
            services.TryAddSingleton<ISnowCapStabilizerHost, DefaultStabilizerHost>();
            services.AddHostedService<ServerStartupHost>();
        }

    }
}
