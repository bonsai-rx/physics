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
    public class CreateRay : CreateGeom<Ray>
    {
        public double Length { get; set; }

        protected override Ray CreateGeometryObject(Space space)
        {
            return new Ray(space, Length);
        }
    }
}
