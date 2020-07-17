using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Movements
{
    public class DefaultMovementTransformer : IMovementTransformer
    {
        public (Vector3 position, float power) Convert(Vector3 direction, float speed)
        {
            return (Vector3.Zero, 0.5f);
        }
    }
}
