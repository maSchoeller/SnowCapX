using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface IMovementResolver
    {
        (Vector3 Position, double RotorPower) Convert(Vector3 direction, double speed);
    }
}
