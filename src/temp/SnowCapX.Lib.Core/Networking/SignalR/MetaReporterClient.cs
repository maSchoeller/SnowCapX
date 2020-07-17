using Microsoft.AspNetCore.SignalR.Client;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Networking.SignalR
{
    public class MetaReporterClient : IMetaReporterClient
    {
        public event Action<object, EventArgs> ReceiveLog;
        public event Action<object, Quaternion> ReceiveImu;
        public event Action<object, byte[]> ReceiveMotors;

        public MetaReporterClient(IHubConnectionFactory factory)
        {
            BaseConnection = factory?.GetConnection(IMetaReporterHub.Path)
                ?? throw new ArgumentNullException(nameof(factory));

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

        private void Register()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(async () =>
            {
                var stream = BaseConnection.StreamAsync<EventArgs>(IMetaReporterHub.ReportLogMethodename);
                await foreach (var item in stream)
                {
                    ReceiveLog?.Invoke(this, item);
                }
            });
            Task.Run(async () =>
            {
                var stream = BaseConnection.StreamAsync<Quaternion>(IMetaReporterHub.ReportImuMethodename);
                await foreach (var item in stream)
                {
                    ReceiveImu?.Invoke(this, item);
                }
            });
            Task.Run(async () =>
            {
                var stream = BaseConnection.StreamAsync<byte[]>(IMetaReporterHub.ReportMotorsMethodename);
                await foreach (var item in stream)
                {
                    ReceiveMotors?.Invoke(this, item);
                }
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}
