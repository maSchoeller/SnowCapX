using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Settings
{
    public interface ISettingsSynchronizationTrigger
    {
        event EventHandler SychronisationRequested;

        //void RequestSychronisation();
    }
}
