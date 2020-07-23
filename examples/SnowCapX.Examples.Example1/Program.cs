using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using SnowCapX.Server;
using SnowCapX.Server.Controlling;
using SnowCapX.Server.Abstracts;
using Microsoft.Extensions.Logging;

namespace SnowCapX.Examples.Example1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                      .ConfigureSnowCapHost(b =>
                      {
                          b.UseStartup<Startup>();
                      })
                      .RunConsoleAsync();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISnowCapStabilizer, DefaultStabilizer>();
        }
        public void ConfigureProcessChain(ProcessChainBuilder builder)
        {
            builder.Invoke(async (c, n) =>
            {
                c.Services.GetService<ILogger<Program>>().LogWarning("Test");
                await n(c);
            });
        }
    }

    public class DefaultStabilizer : ISnowCapStabilizer
    {
        private readonly ILogger<DefaultStabilizer>? _logger;

        public DefaultStabilizer(ILogger<DefaultStabilizer>? logger = null)
        {
            _logger = logger;
        }

        public void Invoke(MovementTarget target)
        {
            _logger?.LogInformation("Stabilizer");
        }
    }
}
