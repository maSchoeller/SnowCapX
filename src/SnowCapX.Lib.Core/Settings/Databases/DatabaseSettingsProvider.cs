using SnowCapX.Lib.Abstracts.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Settings.Databases
{
    internal class DatabaseSettingsProvider : ISettingsProvider
    {
        public static readonly string ConfigurationPath = "Settings:Sqlite";
        public static readonly string DefaultSqliteFile = "dronesettings.db";

        private readonly SettingsContext _context;

        public DatabaseSettingsProvider(SettingsContext context)
        {

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
            _context.Database.EnsureCreated();
        }

        public event EventHandler<SettingsChangedEventArgs> SettingChanged; //Not in use

        public void InvokeSychronisation()
        {
            foreach (var dBsettings in _context.Values)
            {
                SettingChanged?.Invoke(this, new SettingsChangedEventArgs(dBsettings.Value, dBsettings.Id, true));
            }
        }

        public void Set(string key, string value, bool raiseFromSync = false)
        {
            bool invokeUpdate = true;
            if (_context.Values.Find(key) is EFSettingsValue efvalue)
            {
                if (efvalue.Value != value)
                {
                    efvalue.Value = value;
                }
                else
                {
                    invokeUpdate = false;
                }
            }
            else
            {
                _context.Values.Add(new EFSettingsValue { Id = key, Value = value });
            }
            _context.SaveChanges();
            if (!raiseFromSync && invokeUpdate)
            {
                SettingChanged?.Invoke(this, new SettingsChangedEventArgs(value, key));

            }
        }

        public string? TryGet(string key)
        {
            if (_context.Values.Find(key) is EFSettingsValue efvalue)
            {
                return efvalue.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
