using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Networking.SignalR
{
    internal class HubConnectionFactory : IHubConnectionFactory
    {
        public static readonly string HttpsProtocolText = "https";
        public static readonly string HttpProtocolText = "http";

        private readonly SignalROptions _options;
        private readonly ConcurrentDictionary<string, HubConnection> _connections;

        public HubConnectionFactory(IOptionsMonitor<SignalROptions> options)
        {
            _options = options?.CurrentValue ?? throw new ArgumentNullException(nameof(options));
            _connections = new ConcurrentDictionary<string, HubConnection>();
        }

        public HubConnection GetConnection(string path)
        {
            return _connections.GetOrAdd(path, p =>
            {
                string protocol = _options.IsHttps ? HttpsProtocolText : HttpProtocolText;
                int port = _options.Port ?? (_options.IsHttps ? 443 : 80);
                return new HubConnectionBuilder()
                    .WithUrl($"{protocol}://{_options.Host}:{port}/{p}")
                    .WithAutomaticReconnect()
                    .Build();
            });
        }
    }
}
