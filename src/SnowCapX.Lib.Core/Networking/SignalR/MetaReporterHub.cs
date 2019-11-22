using Microsoft.AspNetCore.SignalR;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Networking.SignalR
{
    public class MetaReporterHub : Hub<IMetaReporterHub>
    {
        // Only Need for Endpoint routing
        // It use IHubContext<MetaReporterHub,IMetaReporterHub> for invoking methods
    }
}
