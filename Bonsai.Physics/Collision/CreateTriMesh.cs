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
    public class CreateTriMesh : CreateGeom<TriMesh>
    {
        [XmlIgnore]
        public TriMeshData Data { get; set; }

        protected override TriMesh CreateGeometryObject(Space space)
        {
            var data = Data;
            if (data == null)
            {
                throw new InvalidOperationException("Valid triangle mesh data must be provided at creation time.");
            }

            return new TriMesh(space, data);
        }
    }
}
