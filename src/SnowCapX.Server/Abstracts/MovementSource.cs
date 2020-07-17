using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public class MovementSource
    {
        public Vector3 Direction { get; set; }
        public float Speed { get; set; }


        public bool SetManual { get; set; }
        public Vector3 Position { get; set; }
        public float Power { get; set; }
    }
}
