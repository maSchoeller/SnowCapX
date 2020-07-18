using Microsoft.Extensions.Logging;
using SnowCapX.Lib.Abstracts.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SnowCapX.Lib.Core.Settings
{
    public class SnowCapXSettings : ISettings
    {
        public static readonly char SeperatorToken = ':';

        private readonly ISettingsProvider _provider;
        private readonly ILogger<SnowCapXSettings>? _logger;
        private readonly Dictionary<string, object> _bindings;
        public SnowCapXSettings(
            IEnumerable<ISettingsProvider> provider,
            IEnumerable<ISettingsSynchronizationTrigger> triggers,
            ILogger<SnowCapXSettings>? logger)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            if (triggers is null)
            {
                throw new ArgumentNullException(nameof(triggers));
            }

            _provider = new ChainedSettingsProvider(provider);
            _logger = logger;
            _bindings = new Dictionary<string, object>();
            _provider.SettingChanged += Provider_SettingChanged;
            foreach (var trigger in triggers)
            {
                trigger.SychronisationRequested += (s, e) =>
                {
                    _provider.InvokeSychronisation();
                };
            }
            void Provider_SettingChanged(object sender, SettingsChangedEventArgs e)
            {
                var splited = e.Key.Split(SeperatorToken);
                if (_bindings.TryGetValue(splited[0], out object binding))
                {
                    var propInfo = binding.GetType().GetProperty(splited[1]);
                    if (!(propInfo is null))
                    {
                        var converted = SettingsExtensions.TryConvert(e.Value, propInfo.PropertyType);
                        if (!(converted is null))
                        {
                            propInfo.SetValue(binding, converted);
                        }
                    }
                }
                _logger.LogInformation($"Setting changed from provider, Path: {e.Key}; Value:{e.Value}");
            }
        }


        public TBinding GetBinding<TBinding>(string key) where TBinding : class, INotifyPropertyChanged, new()
        {
            if (_bindings.TryGetValue(key, out object value) && value is TBinding dictbinding)
            {
                return dictbinding;
            }
            else if (_bindings.ContainsKey(key))
            {
                throw new ArgumentException($"Key is in use, but type {nameof(TBinding)}: {typeof(TBinding).Name} contains not with the registered type: {value?.GetType().Name}");
            }

            var binding = GetSetting<TBinding>(key);
            binding.PropertyChanged += Binding_PropertyChanged;
            void Binding_PropertyChanged(object sender, PropertyChangedEventArgs args)
            {
                var propInfo = sender.GetType().GetProperty(args.PropertyName);
                var value = propInfo.GetValue(sender).ToString();
                _provider.Set($"{key}{SeperatorToken}{args.PropertyName}", value);
                _logger.LogInformation($"Setting changed from binding, Path: {key}{SeperatorToken}{args.PropertyName}; Value:{value}");
            }
            _bindings.Add(key, binding);
            return binding;
        }

        public TBinding GetSetting<TBinding>(string key) where TBinding : class, new()
        {
            var binding = new TBinding();
            var bindingtype = typeof(TBinding);
            foreach (var propInfo in bindingtype.GetProperties())
            {
                var propvalue = this.TryGet($"{key}{SeperatorToken}{propInfo.Name}", propInfo.PropertyType);
                if (!(propvalue is null))
                {
                    propInfo.SetValue(binding, propvalue);
                }
            }
            return binding;
        }

        public void Set(string key, string value)
        {
            _provider.Set(key, value);
        }

        public string? TryGet(string key)
        {
            return _provider.TryGet(key);
        }
    }
}
