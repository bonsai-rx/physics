using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bonsai.Physics.Collision
{
    public class CreateHeightfield : CreateGeom<Heightfield>
    {
        [XmlIgnore]
        public HeightfieldData Data { get; set; }

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
