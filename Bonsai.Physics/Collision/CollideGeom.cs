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
    [Description("Computes contact information between geometries that potentially intersect.")]
    public class CollideGeom : Transform<Geom, ContactGeom[]>
    {
        [Description("The maximum number of contacts generated per collision pair.")]
        public int MaxContacts { get; set; }

        static ContactGeom[] Collide(Geom geom1, Geom geom2, int maxContacts)
        {
            var contacts = new ContactGeom[maxContacts];
            var count = Geom.Collide(geom1, geom2, contacts);
            Array.Resize(ref contacts, count);
            return contacts;
        }

        public override IObservable<ContactGeom[]> Process(IObservable<Geom> source)
        {
            return source.Select(geom => Collide(geom, geom.Space, MaxContacts));
        }

        public IObservable<ContactGeom[]> Process(IObservable<Tuple<Geom, Geom>> source)
        {
            return source.Select(input => Collide(input.Item1, input.Item2, MaxContacts));
        }
    }
}
