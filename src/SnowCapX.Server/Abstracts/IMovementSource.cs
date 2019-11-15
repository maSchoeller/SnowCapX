using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface IMovementSource
    {
        public bool SetManual { get; set; }

        public Vector3 Direction { get; set; }
        public double Speed { get; set; }

        public Vector3 Position { get; set; }
        public double Power { get; set; }
    }
}
