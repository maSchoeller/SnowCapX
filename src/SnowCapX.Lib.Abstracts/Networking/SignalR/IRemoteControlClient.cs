using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Networking.SignalR
{
    public interface IRemoteControlClient : ISignalRClient
    {
        Task UpdateDirection(Vector3 dircetion);
    }
}
