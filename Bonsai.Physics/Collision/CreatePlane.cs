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
    public class CreatePlane : CreateGeom<Plane>
    {
        public double A { get; set; }

        public double B { get; set; }

        public double C { get; set; }

        public double D { get; set; }

        protected override Plane CreateGeometryObject(Space space)
        {
            return new Plane(space, A, B, C, D);
        }
    }
}
