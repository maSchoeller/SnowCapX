using SnowCapX.Core.Abstratcs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SnowCapX.Core.Settings
{
    internal class DefaultSettingsManager : ISettingsManager
    {
        public TProxy GetBinding<TProxy>(string path) where TProxy : INotifyPropertyChanged
        {
            throw new NotImplementedException();
        }

        public TObject GetObject<TObject>(string path)
        {
            throw new NotImplementedException();
        }

        public TValue GetValue<TValue>(string path) where TValue : struct
        {
            throw new NotImplementedException();
        }
    }
}
