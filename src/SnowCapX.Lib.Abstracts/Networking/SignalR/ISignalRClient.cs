using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Networking.SignalR
{
    public interface ISignalRClient
    {
        HubConnection BaseConnection { get; }
        bool IsConncted { get; }
        Task ConnectAsync();
    }
}
