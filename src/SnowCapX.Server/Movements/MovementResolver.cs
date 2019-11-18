using Microsoft.Extensions.Logging;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Movements
{
    internal class MovementResolver : IMovementResolver
    {
        private readonly ILogger<MovementResolver>? _logger;

        public MovementResolver(ILogger<MovementResolver>? logger)
        {
            _logger = logger;
        }

        public (Vector3 Position, double RotorPower) Convert(Vector3 direction, double speed)
        {
            _logger?.LogInformation("From library");
            return (Vector3.Zero, 0);
        }
    }
}
