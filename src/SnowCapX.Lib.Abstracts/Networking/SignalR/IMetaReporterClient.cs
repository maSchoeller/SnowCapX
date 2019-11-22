using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Networking.SignalR
{
    public interface IMetaReporterClient : ISignalRClient
    {
        event Action<object, EventArgs> ReceiveLog;
        event Action<object, Quaternion> ReceiveImu;
        event Action<object, byte[]> ReceiveMotors;       
    }
}
