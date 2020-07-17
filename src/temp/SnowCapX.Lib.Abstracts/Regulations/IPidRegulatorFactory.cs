using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Regulations
{
    public interface IPidRegulatorFactory
    {
        public IPidRegulator Create(string key, PidType type = PidType.Standard);
    }
}
