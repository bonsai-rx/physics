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
    [Description("Creates a capsule geometry of the specified dimensions.")]
    public class CreateCapsule : CreateGeom<Capsule>
    {
        [Description("The radius of the cylinder and spherical cap.")]
        public double Radius { get; set; }

        [Description("The length of the cylinder excluding the spherical cap.")]
        public double Length { get; set; }

        protected override Capsule CreateGeometryObject(Space space)
        {
            return new Capsule(space, Radius, Length);
        }
    }
}
