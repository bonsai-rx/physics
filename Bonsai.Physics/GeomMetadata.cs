using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    class GeomMetadata
    {
        public GeomMetadata(string material)
        {
            Material = material;
        }

        public string Material { get; private set; }

        public event CollisionEventHandler Collision;

        internal void OnCollision(Geom g1, Geom g2, ContactGeom[] contacts)
        {
            var handler = Collision;
            if (handler != null)
            {
                handler(g1, g2, contacts);
            }
        }
    }

    delegate void CollisionEventHandler(Geom g1, Geom g2, ContactGeom[] contacts);
}
