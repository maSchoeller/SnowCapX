using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Regulations
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
            var integral = CalcIntegrale(seconds, error);
            var derivative = CalcDerivate(seconds, error);

            double control = Clamp(proportional + integral + derivative);

            LastError = error;
            LastCalcTime = now;

            return control;
        }

        private double CalcDerivate(double seconds, double error)
        {
            var diff = error - LastError / seconds;
            return Settings.DGain * diff;
        }

        private double CalcIntegrale(double seconds, double error)
        {
            IntegratedError += error * seconds;
            return Settings.IGain * IntegratedError;
        }
    }
}
