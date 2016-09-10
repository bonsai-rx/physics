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
    [Description("Creates an infinite plane geometry from the specified plane equation.")]
    public class CreatePlane : CreateGeom<Plane>
    {
        [Description("The first parameter of the plane equation.")]
        public double A { get; set; }

        [Description("The second parameter of the plane equation.")]
        public double B { get; set; }

        [Description("The third parameter of the plane equation.")]
        public double C { get; set; }

        [Description("The fourth parameter of the plane equation.")]
        public double D { get; set; }

        protected override Plane CreateGeometryObject(Space space)
        {
            return new Plane(space, A, B, C, D);
        }
    }
}
