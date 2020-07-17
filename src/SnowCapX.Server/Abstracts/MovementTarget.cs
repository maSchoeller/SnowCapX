using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public class MovementTarget
    {
        public (Vector3 Position, float Power) Target { get; internal set; }
    }
}
