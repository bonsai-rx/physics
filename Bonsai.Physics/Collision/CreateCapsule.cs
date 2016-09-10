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
    public class CreateCapsule : CreateGeom<Capsule>
    {
        public double Radius { get; set; }

        public double Length { get; set; }

        protected override Capsule CreateGeometryObject(Space space)
        {
            return new Capsule(space, Radius, Length);
        }
    }
}
