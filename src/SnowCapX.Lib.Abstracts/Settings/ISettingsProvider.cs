using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Settings
{
    public interface ISettingsProvider
    {
        event EventHandler<SettingsChangedEventArgs> SettingChanged;

        void Set(string key, string value, bool raiseFromSynchronization = false);
        string? TryGet(string key);

        void InvokeSychronisation();
    }
}
