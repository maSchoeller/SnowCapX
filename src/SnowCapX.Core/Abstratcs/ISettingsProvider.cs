using System;
using System.Collections.Generic;

namespace SnowCapX.Core.Abstratcs
{
    public interface ISettingsProvider
    {
        event EventHandler<SettingChangedEventArgs> SettingChanged;

        bool WriteSetting<TValue>(string path, TValue value) 
            where TValue : struct;
        bool WriteAllSettings(IEnumerable<(object value, string path)> settings);

        TValue? ReadSetting<TValue>(string path)
            where TValue : struct;
        IEnumerable<(object value, string path)> ReadAllSettings();
    }
}
