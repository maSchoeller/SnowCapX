using System;

namespace SnowCapX.Core.Abstratcs
{
    public class SettingChangedEventArgs : EventArgs
    {
        public SettingChangedEventArgs(object value, string path)
        {
            Value = value;
            Path = path;
        }

        public object Value { get; }
        public string Path { get; }
    }
}
