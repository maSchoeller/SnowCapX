using System;

namespace SnowCapX.Lib.Abstracts.Settings
{
    public class SettingsChangedEventArgs : EventArgs
    {
        public SettingsChangedEventArgs(string value, string key)
        {
            Value = value;
            Key = key;
        }
        public string Key { get; }
        public string Value { get; }

    }
}
