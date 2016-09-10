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
    [Description("Creates a cylinder geometry of the specified dimensions.")]
    public class CreateCylinder : CreateGeom<Cylinder>
    {
        [Description("The radius of the cylinder.")]
        public double Radius { get; set; }

        [Description("The length of the cylinder.")]
        public double Length { get; set; }

        protected override Cylinder CreateGeometryObject(Space space)
        {
            return new Cylinder(space, Radius, Length);
        }
    }
}
