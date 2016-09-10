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
    [Description("Creates an idealized ray geometry with the specified length.")]
    public class CreateRay : CreateGeom<Ray>
    {
        [Description("The length of the ray.")]
        public double Length { get; set; }

        protected override Ray CreateGeometryObject(Space space)
        {
            return new Ray(space, Length);
        }
    }
}
