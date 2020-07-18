using SnowCapX.Lib.Abstracts.Settings;
using System;

namespace SnowCapX.Server.Networking
{
    public class GrpcSettingsProvider : ISettingsProvider
    {
        public event EventHandler<SettingsChangedEventArgs> SettingChanged;
        public event EventHandler<EventArgs> SynchronizationRequested;

        public void InvokeSychronisation()
        {
            throw new NotImplementedException();
        }

        public void Set(string key, string value, bool raiseFromSynchronization = false)
        {
            throw new NotImplementedException();
        }

        public string? TryGet(string key)
        {
            throw new NotImplementedException();
        }
    }
}