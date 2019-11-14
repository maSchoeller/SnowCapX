using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Settings
{
    public interface ISettings
    {
        void Set(string key, string value);
        string? TryGet(string key);

        TBinding GetBinding<TBinding>(string key) where TBinding : class, INotifyPropertyChanged, new();
        TBinding GetSetting<TBinding>(string key) where TBinding : class, new();
    }
}
