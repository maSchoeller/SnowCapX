using SnowCapX.Core.Abstratcs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core.Settings
{
    public class NetworkSettingsAdapter
    {

        internal event EventHandler<SettingChangedEventArgs>? SettingChanged;

        internal void WriteSetting<TValue>(string path, TValue value)
        {
            Callback?.Invoke(path, value);
        }


        public bool ReceiveSetting<TValue>(string path, TValue value)
        {
            return true;
        }

        public Action<string, object> Callback{ get; set; }



    }
}
