using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Settings
{
    public interface ISettingsProvider
    {
        event EventHandler<SettingsChangedEventArgs> SettingChanged;

        void Set(string key, string value);
        string? TryGet(string key);
        void RaiseAllSettingsChanged();
    }
}
