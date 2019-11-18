using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SnowCapX.Server.Hosting;
using SnowCapX.Server.Loops;

namespace SnowCapX.Example1.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureDroneHostDefaults(droneBuilder =>
                {
                    droneBuilder.ConfigureLoop(loopBuilder =>
                    {
                        loopBuilder.Invoke(async (context, next) =>
                        {
                            context.Provider.GetService<ILogger<Program>>().LogInformation("Testlogging");
                            await next();
                        });
                    });
                });
    }
}
