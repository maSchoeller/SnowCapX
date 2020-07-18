using SnowCapX.Lib.Abstracts.Settings;
using System;

namespace SnowCapX.Server.Networking
{
    internal class GrpcSettingsSychronisationTrigger : ISettingsSynchronizationTrigger
    {
        public event EventHandler SychronisationRequested;

        internal void InvokeRequest()
        {
            SychronisationRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}