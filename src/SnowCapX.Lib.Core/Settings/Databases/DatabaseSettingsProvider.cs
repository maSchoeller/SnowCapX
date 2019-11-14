using SnowCapX.Lib.Abstracts.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Settings.Databases
{
    public class DatabaseSettingsProvider : ISettingsProvider
    {
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

        public void RaiseAllSettingsChanged()
        {
            //Todo: Implementing Event, iterate over the Database.
        }

        public void Set(string key, string value)
        {
            if (_context.Values.Find(key) is EFSettingsValue efvalue)
            {
                efvalue.Value = value;
            }
            else
            {
                _context.Values.Add(new EFSettingsValue { Id = key, Value = value });
            }
            _context.SaveChanges();
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
