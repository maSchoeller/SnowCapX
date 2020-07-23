using Microsoft.Extensions.DependencyInjection;
using SnowCapX.Server.Controlling;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface ISnowCapServerBuilder
    {
        public bool UseSameStartup { get; set; }

        void ConfigureProcessChain(Action<ProcessChainBuilder> callback);
        void ConfigureServices(Action<IServiceCollection> callback);
        void UseStartup<TStartup>();

        void Build();
    }
}
