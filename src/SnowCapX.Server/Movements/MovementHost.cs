using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Movements
{
    public class MovementHost
    {
        private readonly MovementSource _source;
        private readonly MovementTarget _target;
        private readonly IMovementTransformer _transformer;

        public MovementHost(MovementSource source,
            MovementTarget target,
            IMovementTransformer transformer)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
        }

        public void Invoke()
        {
            if (_source.SetManual)
            {
                _target.Target = (_source.Position, _source.Power);
            }
            else
            {
                _target.Target = _transformer.Convert(_source.Direction, _source.Speed);
            }
        }
    }
}
