using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Networking.SignalR
{
    public interface IMetaReporterClient
    {
        event Action<object, EventArgs> ReceiveLog;
        event Action<object, EventArgs> ReceiveImu;
        event Action<object, EventArgs> ReceiveMotors;


        bool IsConncted { get; }
    }
}
