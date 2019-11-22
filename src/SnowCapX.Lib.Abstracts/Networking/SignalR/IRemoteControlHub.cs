using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Networking.SignalR
{
    public interface IRemoteControlHub
    {
        Task UploadFlightDirectionStream(IAsyncEnumerable<Vector3> update);
    }
}
