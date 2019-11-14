using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Regulations
{
    public interface IPidRegulator : IRegulator
    {
        void ResetIntegrator();
    }
}
