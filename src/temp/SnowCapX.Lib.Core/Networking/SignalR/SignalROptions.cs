using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Networking.SignalR
{
    public class SignalROptions
    {
        public string Host { get; set; } = "localhost";

        public int? Port { get; set; } 

        public bool IsHttps { get; set; } = true;
    }
}
