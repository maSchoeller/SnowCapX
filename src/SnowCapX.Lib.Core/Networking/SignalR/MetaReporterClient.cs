using Microsoft.AspNetCore.SignalR.Client;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Networking.SignalR
{
    public class MetaReporterClient : IMetaReporterClient
    {
        public event Action<object, EventArgs> ReceiveLog;
        public event Action<object, EventArgs> ReceiveImu;
        public event Action<object, EventArgs> ReceiveMotors;

        public MetaReporterClient(IHubConnectionFactory factory)
        {
            BaseConnection = factory?.GetConnection(IMetaReporterHub.Path) 
                ?? throw new ArgumentNullException(nameof(factory));
            BaseConnection.Reconnected += s =>
            {
                IsConncted = true;
                return Task.CompletedTask;
            };
            BaseConnection.Closed += s =>
            {
                IsConncted = false;
                return Task.CompletedTask;
            };
            BaseConnection.On<EventArgs>(IMetaReporterHub.ReportLogMethodename, e => ReceiveLog?.Invoke(this, e));
            BaseConnection.On<EventArgs>(IMetaReporterHub.ReportImuMethodename, e => ReceiveImu?.Invoke(this, e));
            BaseConnection.On<EventArgs>(IMetaReporterHub.ReportMotorsMethodename, e => ReceiveMotors?.Invoke(this, e));
        }

        public HubConnection BaseConnection { get; }

        public bool IsConncted { get; private set; }

        public async Task ConnectAsync()
        {
            await BaseConnection.StartAsync()
                .ConfigureAwait(false);
            IsConncted = true;
        }
    }
}
