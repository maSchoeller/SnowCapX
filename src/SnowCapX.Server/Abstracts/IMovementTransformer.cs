using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface IMovementTransformer
    {
        (Vector3 position, float power) Convert(Vector3 direction, float speed);
    }
}
