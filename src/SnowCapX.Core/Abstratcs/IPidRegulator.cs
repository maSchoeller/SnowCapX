using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core.Abstratcs
{
    public interface IPidRegulator
    {
        string Key { get; }
        double Calculate(double input, double target);
        double MinValue { get; }
        double MaxValue { get; }

        void ResetIntegrator();
    }
}
