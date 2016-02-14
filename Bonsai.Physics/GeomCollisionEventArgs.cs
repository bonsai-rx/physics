using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class GeomCollisionEventArgs : EventArgs
    {
        public GeomCollisionEventArgs(Geom geom, ContactGeom[] contacts)
        {
            Geom = geom;
            Contacts = contacts;
        }

        public Geom Geom { get; private set; }

        public ContactGeom[] Contacts { get; private set; }
    }

    public delegate void GeomCollisionEventHandler(Geom sender, GeomCollisionEventArgs e);
}
