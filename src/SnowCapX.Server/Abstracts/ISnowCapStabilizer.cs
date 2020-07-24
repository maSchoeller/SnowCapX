﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface ISnowCapStabilizer
    {
        void Invoke(MovementTarget target);
    }
}