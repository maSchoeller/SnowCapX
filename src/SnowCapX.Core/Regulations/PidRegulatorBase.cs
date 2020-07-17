using SnowCapX.Core.Abstratcs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core.Regulations
{
    public abstract class PidRegulatorBase : IPidRegulator
    {
        protected double LastError { get; set; } = 0;
        protected double IntegratedError { get; set; } = 0;
        protected DateTime LastCalcTime { get; set; } = DateTime.Now;

        protected PidRegulatorBase(PidRegulatorSetting setting, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key can't null or Empty", nameof(key));
            }

            Settings = setting ?? throw new ArgumentNullException(nameof(setting));
            Key = key;
        }

        public string Key { get; }
        public PidRegulatorSetting Settings { get; }

        public double MinValue => Settings.MinValue;
        public double MaxValue => Settings.MaxValue;

        public abstract double Calculate(double input, double target);

        protected virtual double Clamp(double input)
        {
            if (Settings.MaxValue < input) return Settings.MaxValue;
            if (Settings.MinValue > input) return Settings.MinValue;
            return input;
        }

        public void ResetIntegrator() => IntegratedError = 0;
    }
}
