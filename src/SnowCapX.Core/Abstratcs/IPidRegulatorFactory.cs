using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core.Abstratcs
{
    public interface IPidRegulatorFactory
    {
    
    }

    public enum PidType
    {
        Standard = 0,
        Ideal = 1,
    }
}
