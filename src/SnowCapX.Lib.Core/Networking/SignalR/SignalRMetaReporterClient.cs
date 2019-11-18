using Microsoft.AspNetCore.SignalR.Client;
using SnowCapX.Lib.Abstracts.Networking.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Networking.SignalR
{
    public class SignalRMetaReporterClient : IMetaReporterClient
    {

        public event Action<object, EventArgs> ReceiveLog;
        public event Action<object, EventArgs> ReceiveImu;
        public event Action<object, EventArgs> ReceiveMotors;


        public SignalRMetaReporterClient()
        { 

            
        }


        public bool IsConncted { get; set; }
    }
}
