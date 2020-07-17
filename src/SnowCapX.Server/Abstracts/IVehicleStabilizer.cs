using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface IVehicleStabilizer
    {
        bool IsRunning { get; }
        bool TryStart();
        bool TryStop();
    }
}
