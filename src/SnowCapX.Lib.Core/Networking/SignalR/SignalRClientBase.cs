using Microsoft.AspNetCore.SignalR.Client;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Networking.SignalR
{
    internal abstract class SignalRClientBase : ISignalRClient
    {
        public SignalRClientBase(HubConnection baseConnection)
        {
            BaseConnection = baseConnection 
                ?? throw new ArgumentNullException(nameof(baseConnection));
            BaseConnection.Reconnected += s =>
            {
                IsConncted = true;
                Register();
                return Task.CompletedTask;
            };
            BaseConnection.Closed += s =>
            {
                IsConncted = false;
                return Task.CompletedTask;
            };
        }

        public HubConnection BaseConnection { get; }

        public bool IsConncted { get; private set; }

        public async Task ConnectAsync()
        {
            if (IsConncted)
            {
                return;
            }
            await BaseConnection.StartAsync()
                .ConfigureAwait(false);
            IsConncted = true;
            Register();
        }


        protected abstract void Register();
    }
}
