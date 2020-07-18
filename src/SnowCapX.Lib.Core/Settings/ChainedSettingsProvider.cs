using SnowCapX.Lib.Abstracts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Settings
{
    internal class ChainedSettingsProvider : ISettingsProvider
    {
        private readonly IEnumerable<ISettingsProvider> _providers;
        public event EventHandler<SettingsChangedEventArgs> SettingChanged;

        public ChainedSettingsProvider(IEnumerable<ISettingsProvider> providers)
        {
            if (providers is null)
            {
                throw new ArgumentNullException(nameof(providers));
            }
            _providers = providers;
            foreach (var provider in _providers)
            {
                provider.SettingChanged += (s, e) =>
                {
                    SettingChanged?.Invoke(this, e);
                };
            }
        }


        public void Set(string key, string value, bool raiseFromSync = false)
        {
            foreach (var provider in _providers)
            {
                provider.Set(key, value, raiseFromSync);
            }
        }

        public string? TryGet(string key)
        {
            foreach (var provider in _providers.Reverse())
            {
                var res = provider.TryGet(key);
                if (!(res is null))
                {
                    return res;
                }
            }
            return null;
        }

        public void InvokeSychronisation()
        {
            foreach (var provider in _providers)
            {
                provider.InvokeSychronisation();
            }
        }
    }
}
