using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface IControlLoopBuilder
    {

        IServiceProvider ApplicationServices { get; }

        void Use(Func<ControlPoint, ControlPoint> middleware);

        IHostedService Build();
    }
}
