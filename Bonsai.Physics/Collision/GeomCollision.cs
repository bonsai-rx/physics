using Ode.Net;
using Ode.Net.Collision;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    public class GeomCollision : Combinator<Geom, EventPattern<Geom, GeomCollisionEventArgs>>
    {
        static readonly ContactGeom[] EmptyContacts = new ContactGeom[0];

        public bool IncludeContacts { get; set; }

        public override IObservable<EventPattern<Geom, GeomCollisionEventArgs>> Process(IObservable<Geom> source)
        {
            return source.SelectMany(geom =>
            {
                var metadata = geom.Tag as GeomMetadata;
                if (metadata == null)
                {
                    throw new InvalidOperationException("Collision geom must have a valid metadata object.");
                }

                return Observable.Create<EventPattern<Geom, GeomCollisionEventArgs>>(observer =>
                {
                    CollisionEventHandler handler = (g1, g2, contacts, length) =>
                    {
                        if (IncludeContacts)
                        {
                            var output = new ContactGeom[length];
                            Array.Copy(contacts, output, length);
                            contacts = output;
                        }
                        else contacts = EmptyContacts;
                        observer.OnNext(new EventPattern<Geom, GeomCollisionEventArgs>(g1, new GeomCollisionEventArgs(g2, contacts)));
                    };

                    metadata.Collision += handler;
                    return Disposable.Create(() => metadata.Collision -= handler);
                });
            });
        }
    }
}
