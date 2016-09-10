using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bonsai.Physics.Collision
{
    [Description("Creates a heightfield with the specified data.")]
    public class CreateHeightfield : CreateGeom<Heightfield>
    {
        [XmlIgnore]
        [Description("The heightfield data object.")]
        public HeightfieldData Data { get; set; }

        [Description("Indicates whether the heightfield can be transformed in the world. Otherwise, the y-axis will be the global height axis.")]
        public bool Placeable { get; set; }

        protected override Heightfield CreateGeometryObject(Space space)
        {
            var data = Data;
            if (data == null)
            {
                throw new InvalidOperationException("Valid heightfield data must be provided at creation time.");
            }

            return new Heightfield(space, data, Placeable);
        }
    }
}
