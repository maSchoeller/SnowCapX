using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Regulations
{
    internal class StandardPidRegulator : PidRegulatorBase
    {
        public StandardPidRegulator(PidRegulatorSetting options, string key)
            : base(options, key)
        { 
        }

        public override double Calculate(double input, double target)
        {
            var now = DateTime.Now;
            var dt = now - LastCalcTime;
            var seconds = ((double)dt.Ticks / 10_000 / 1000);
            var error = target - input;

            IntegratedError += error * seconds;
            var integral = (1 / (Settings.IGain * 60)) * IntegratedError;

            var diff = error - LastError / seconds;
            var derivative = (Settings.DGain * 60) * diff;

            double control = Settings.PGain * (error + integral + derivative);

            control = Clamp(control);

            LastError = error;
            LastCalcTime = now;
            return control;
        }
    }
}
