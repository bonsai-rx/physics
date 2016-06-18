using Ode.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class AxisAnchor
    {
        public AxisAnchor(Vector3 axis, Vector3 anchor)
        {
            Axis = axis;
            Anchor = anchor;
        }

        public AxisAnchor(
            float axisX, float axisY, float axisZ,
            float anchorX, float anchorY, float anchorZ)
            : this(new Vector3(axisX, axisY, axisZ),
                   new Vector3(anchorX, anchorY, anchorZ))
        {
        }

        public Vector3 Axis { get; private set; }

        public Vector3 Anchor { get; private set; }
    }
}
