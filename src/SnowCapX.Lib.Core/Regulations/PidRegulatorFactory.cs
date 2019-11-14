using Microsoft.Extensions.Logging;
using SnowCapX.Lib.Abstracts.Regulations;
using SnowCapX.Lib.Abstracts.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Lib.Core.Regulations
{
    public class PidRegulatorFactory : IPidRegulatorFactory
    {
        private readonly ISettings _setting;
        private readonly ILogger<PidRegulatorFactory>? _logger;

        public PidRegulatorFactory(ISettings setting, ILogger<PidRegulatorFactory>? logger = null)
        {
            _setting = setting;
            _logger = logger;
        }

        public IPidRegulator Create(string key, PidType type = PidType.Standard)
        {
            _logger?.LogDebug($"Create new PID Regulator with key:{key}, from type: {type}");
            PidRegulatorSetting opt = _setting.GetBinding<PidRegulatorSetting>(key);
            return type switch
            {
                PidType.Standard => new StandardPidRegulator(opt, key),
                PidType.Ideal => new IdealPidRegulator(opt, key),
                _ => throw new NotSupportedException($"PID Type {type.ToString()} is unsupported."),
            };
        }
    }
}
