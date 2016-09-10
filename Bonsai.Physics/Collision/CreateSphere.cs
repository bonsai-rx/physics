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
    [Description("Creates a sphere geometry of the specified dimensions.")]
    public class CreateSphere : CreateGeom<Sphere>
    {
        [Description("The radius of the sphere.")]
        public double Radius { get; set; }

        protected override Sphere CreateGeometryObject(Space space)
        {
            return new Sphere(space, Radius);
        }
    }
}
