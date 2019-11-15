using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Contexts
{
    public class DroneServerContext : IDroneServerContext
    {

        public DroneServerContext(IMovementSource movementSource, IDroneServerEnviroment enviroment)
        {
            MovementSource = movementSource ?? throw new ArgumentNullException(nameof(movementSource));
            Enviroment = enviroment ?? throw new ArgumentNullException(nameof(enviroment));
        }

        public IMovementSource MovementSource { get; }

        public IDroneServerEnviroment Enviroment { get; }
    }
}
