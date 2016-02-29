using Ode.Net;
using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    public class CreateSphere : CreateGeom<Sphere>
    {
        public double Radius { get; set; }

        protected override Sphere CreateGeometryObject(Space space)
        {
            return new Sphere(space, Radius);
        }
    }
}
