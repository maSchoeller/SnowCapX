using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface IDroneServerBuilder
    {
        IDroneServerEnviroment Enviroment { get; }

        void ConfigureLoop(Action<IControlLoopBuilder> action);

        void ConfigureServices(Action<IServiceCollection> action);

        void UseStartup<TStartup>();

        void Build();
    }
}
