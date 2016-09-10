using Ode.Net;
using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    [Description("Creates a box geometry of the specified dimensions.")]
    public class CreateBox : CreateGeom<Box>
    {
        [Description("The length of the box along the X axis.")]
        public double LengthX { get; set; }

        [Description("The length of the box along the Y axis.")]
        public double LengthY { get; set; }

        [Description("The length of the box along the Z axis.")]
        public double LengthZ { get; set; }

        protected override Box CreateGeometryObject(Space space)
        {
            return new Box(space, LengthX, LengthY, LengthZ);
        }
    }
}
