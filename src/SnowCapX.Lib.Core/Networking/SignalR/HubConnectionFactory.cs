using Microsoft.AspNetCore.SignalR.Client;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Networking.SignalR
{
    public class HubConnectionFactory : IHubConnectionFactory
    {
        private readonly string _hostname;
        private readonly IDictionary<string, HubConnection> _hubConnections;

        public HubConnectionFactory(string hostname)
        {
            if (hostname is null)
            {
                throw new ArgumentNullException(nameof(hostname));
            }
            _hostname = hostname;
            _hubConnections = new Dictionary<string, HubConnection>();
        }

        public HubConnection CreateOrGetConnection(string path)
        {
            if (!_hubConnections.ContainsKey(path))
            {
                _hubConnections.Add(path, new HubConnectionBuilder()
                        .WithUrl($"https://{_hostname}/{path}")
                        .WithAutomaticReconnect()
                        .Build());
            }
            return _hubConnections[path];
        }
    }
}
