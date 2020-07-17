using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Abstracts
{
    /// <summary>
    /// Contains the logic for a control point in the control loop, these are used to tell the stabilizer how to stabilize it.
    /// </summary>
    public delegate Task ControlPoint();
}
