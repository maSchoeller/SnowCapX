using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Abstracts
{
    /// <summary>
    /// Provides the possibility to encapsulate a <see cref="ControlPoint"/> in a class.
    /// </summary>
    public interface ILoopControlMiddleware
    {
        /// <summary>
        /// The control point that is to be executed.
        /// </summary>
        Task InvokeAsync();

    }
}
