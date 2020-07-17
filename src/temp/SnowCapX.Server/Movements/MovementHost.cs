using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Movements
{
    internal class MovementHost : IMovementHost, IMovementSource, IMovementTarget
    {
        private readonly IMovementResolver _resolver;

        public MovementHost(IMovementResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public void Invoke()
        {
            if (SetManual)
            {
                Target = (Position, Power);
            }
            else
            {
                Target = _resolver.Convert(Direction, Speed);
            }
        }

        public (Vector3 Position, double Power) Target { get; private set; }

        public bool SetManual { get; set; }
        public Vector3 Direction { get; set; }
        public double Speed { get; set; }
        public Vector3 Position { get; set; }
        public double Power { get; set; }
    }
}
