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
            services.AddSingleton<ISnowCapStabilizerHost, DefaultStabilizer>();
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

    public class DefaultStabilizer : ISnowCapStabilizerHost
    {
        public bool IsRunning => true;

        public bool TryStart()
        {
            return true;
        }

        public bool TryStop()
        {
            return true;
        }
    }
}
