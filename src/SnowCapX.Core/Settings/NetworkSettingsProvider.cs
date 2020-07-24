using SnowCapX.Core.Abstratcs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core.Settings
{
    internal class NetworkSettingsProvider : ISettingsProvider
    {
        public event EventHandler<SettingChangedEventArgs>? SettingChanged;

        public IEnumerable<(object value, string path)> ReadAllSettings()
        {
            throw new NotImplementedException();
        }

        public TValue? ReadSetting<TValue>(string path) 
            where TValue : struct
        {
            throw new NotImplementedException();
        }

        public bool WriteAllSettings(IEnumerable<(object value, string path)> settings)
        {
            throw new NotImplementedException();
        }

        public bool WriteSetting<TValue>(string path, TValue value)
            where TValue : struct
        {
            throw new NotImplementedException();
        }
    }
}
