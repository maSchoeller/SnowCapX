using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Abstracts.Regulations
{
    public interface IRegulator
    {
        string Key { get; }
        double Calculate(double input, double target);
        double MinValue { get; }
        double MaxValue { get; }
    }
}
