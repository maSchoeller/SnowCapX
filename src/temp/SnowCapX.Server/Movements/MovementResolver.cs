using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Movements
{
    public class MovementResolver : IMovementResolver
    {
        public (Vector3 Position, double RotorPower) Convert(Vector3 direction, double speed)
        {
            return (Vector3.Zero, 0);
        }
    }
}
