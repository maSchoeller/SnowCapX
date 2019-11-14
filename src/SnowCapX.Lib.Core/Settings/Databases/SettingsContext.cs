using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Settings.Databases
{
    public class SettingsContext : DbContext
    {
        public SettingsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EFSettingsValue> Values { get; set; }
    }
}
