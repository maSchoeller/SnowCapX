using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Abstracts
{
    public interface IDroneServerContext
    {
        IMovementSource MovementSource { get; }

        IDroneServerEnviroment Enviroment { get; }
    }
}
