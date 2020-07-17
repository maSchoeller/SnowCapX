using SnowCapX.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SnowCapX.Core.Regulations
{
    public class PidRegulatorSetting : NotifyPropertyChangedBase   
    {
        public PidRegulatorSetting()
        {
            MinValue = -1;
            MaxValue = 1;
            PGain = 1;
            DGain = 0;
            IGain = 0;
        }

        private double minValue;
        public double MinValue { get => minValue; set => SetProperty(ref minValue, value); }

        private double maxValue;
        public double MaxValue { get => maxValue; set => SetProperty(ref maxValue, value); }


        private double pGain;
        public double PGain { get => pGain; set => SetProperty(ref pGain, value); }

        private double dGain;
        public double DGain { get => dGain; set => SetProperty(ref dGain, value); }

        private double iGain;
        public double IGain { get => iGain; set => SetProperty(ref iGain, value); }
    }
}
