using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core.Regulations
{

    internal class IdealPidRegulator : PidRegulatorBase
    {
        public IdealPidRegulator(PidRegulatorSetting setting, string key) : base(setting, key)
        {
        }

        public override double Calculate(double input, double target)
        {
            var now = DateTime.Now;

            var dt = now - LastCalcTime;
            var seconds = ((double)dt.Ticks / 10_000 / 1000);

            var error = target - input;

            var proportional = Settings.PGain * error;
            IntegratedError += error * seconds;
            var integral = Settings.IGain * IntegratedError;

            //Todo: maybe wrong => (error - LastError)
            //      need to proof
            var derivative = Settings.DGain * (error - LastError / seconds);
            double control = Clamp(proportional + integral + derivative);

            LastError = error;
            LastCalcTime = now;

            return control;
        }
    }
}
