using Microsoft.AspNetCore.SignalR;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Networking.SignalR
{

    //Note: It's only a reporter, there is no need for get requests.
    public class MetaReporterHub : Hub<IMetaReporterHubModel>
    {
        public MetaReporterHub()
        {

        }
    }
}
