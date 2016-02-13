using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CollisionHandler
    {
        public string Material1 { get; set; }

        public string Material2 { get; set; }

        [TypeConverter(typeof(SurfaceParametersConverter))]
        public SurfaceParameters CollisionSurface { get; set; }
    }
}
