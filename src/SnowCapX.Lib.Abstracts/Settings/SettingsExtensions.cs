using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Settings
{
    public static class SettingsExtensions
    {
        public static TValue? TryGet<TValue>(this ISettings setting, string key)
            where TValue : struct
        {
            if (setting is null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            var ret = setting.TryGet(key, typeof(TValue));
            return ret is null ? default : (TValue)ret;
        }

        public static object? TryGet(this ISettings setting, string key, Type valueType)
        {
            if (setting is null)
            {
                throw new ArgumentNullException(nameof(setting));
            }
            if (valueType is null)
            {
                throw new ArgumentNullException(nameof(valueType));
            }

            var value = setting.TryGet(key);
            return value is null ? null : TryConvert(value, valueType);
        }

        public static T? TryConvert<T>(string value) where T : struct
        { 
            var ret = TryConvert(value, typeof(T));
            return ret is null ? default : (T)ret;
        }

        public static object? TryConvert(string value, Type valueType)
        {
            if (!(value is null))
            {
                if (valueType is null)
                {
                    throw new ArgumentNullException(nameof(valueType));
                }
                var data = Convert.ChangeType(value, valueType);
                if (!(data is null))
                {
                    return data;
                }
            }
            return null;
        }
    }
}
