using System;

namespace SnowCapX.Lib.Abstracts.Settings
{
    public class SettingsChangedEventArgs : EventArgs
    {
        public SettingsChangedEventArgs(string value, string key, bool raiseFromSynchronization = false)
        {
            Value = value;
            Key = key;
            RaiseFromSynchronization = raiseFromSynchronization;
        }
        public string Key { get; }
        public bool RaiseFromSynchronization { get; }
        public string Value { get; }

    }
}
