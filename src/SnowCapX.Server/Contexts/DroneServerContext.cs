using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Contexts
{
    internal class DroneServerContext : IDroneServerContext
    {

        public DroneServerContext(IMovementSource movementSource, IDroneServerEnviroment enviroment, IServiceProvider provider)
        {
            MovementSource = movementSource ?? throw new ArgumentNullException(nameof(movementSource));
            Enviroment = enviroment ?? throw new ArgumentNullException(nameof(enviroment));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }


        public IServiceProvider Provider { get; }

        public IMovementSource MovementSource { get; }

        public IDroneServerEnviroment Enviroment { get; }
    }
}
