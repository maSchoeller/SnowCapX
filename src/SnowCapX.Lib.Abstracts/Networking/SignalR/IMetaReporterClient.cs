using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Networking.SignalR
{
    public interface IMetaReporterClient
    {
        event Action<object, EventArgs> ReceiveLog;
        event Action<object, EventArgs> ReceiveImu;
        event Action<object, EventArgs> ReceiveMotors;

        HubConnection BaseConnection { get; }

        bool IsConncted { get; }

        Task ConnectAsync();
    }
}
