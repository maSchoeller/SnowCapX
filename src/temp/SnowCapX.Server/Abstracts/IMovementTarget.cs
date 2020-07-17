using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface IMovementTarget
    {
        public (Vector3 Position,double Power) Target { get; }
    }
}
