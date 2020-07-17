using Microsoft.Extensions.Logging;
using SnowCapX.Core.Abstratcs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Core.Regulations
{
    internal class PidRegulatorFactory : IPidRegulatorFactory
    {
        private readonly ILogger<PidRegulatorFactory>? _logger;

        public PidRegulatorFactory(ILogger<PidRegulatorFactory>? logger = null)
        {
            _logger = logger;
        }

        public IPidRegulator Create(string key, PidType type = PidType.Standard)
        {
            _logger?.LogDebug($"Create new PID Regulator with key:{key}, from type: {type}");
            //Todo: Solve Settings dependency
            //PidRegulatorSetting opt = _setting.GetBinding<PidRegulatorSetting>(key);
            return type switch
            {
                //PidType.Standard => new StandardPidRegulator(opt, key),
                //PidType.Ideal => new IdealPidRegulator(opt, key),
                _ => throw new NotSupportedException($"PID Type {type.ToString()} is unsupported."),
            };
        }
    }
}
