using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    public class CollisionHandler
    {
        [Description("The name of the first collision material.")]
        public string Material1 { get; set; }

        [Description("The name of the second collision material.")]
        public string Material2 { get; set; }

        [TypeConverter(typeof(SurfaceParametersConverter))]
        [Description("The surface parameters used to generate contact constraints between intersecting bodies of the specified materials.")]
        public SurfaceParameters CollisionSurface { get; set; }

        public override string ToString()
        {
            return string.Format("{{{0}}} x {{{1}}}", Material1, Material2);
        }
    }
}
