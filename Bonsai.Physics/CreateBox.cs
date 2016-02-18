using Ode.Net;
using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CreateBox : CreateGeom<Box>
    {
        public double LengthX { get; set; }

        public double LengthY { get; set; }

        public double LengthZ { get; set; }

        protected override Box CreateGeometryObject(Space space)
        {
            return new Box(space, LengthX, LengthY, LengthZ);
        }
    }
}
