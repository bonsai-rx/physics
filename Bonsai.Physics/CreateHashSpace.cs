using Ode.Net;
using Ode.Net.Collision;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CreateHashSpace : Source<HashSpace>
    {
        public int MaxContacts { get; set; }

        public override IObservable<HashSpace> Generate()
        {
            return Observable.Defer(() =>
            {
                var space = new HashSpace();
                space.Cleanup = false;
                return Observable.Return(space)
                                 .Concat(Observable.Never(space))
                                 .Finally(space.Dispose);
            });
        }

        public IObservable<Space> Generate<TSource>(IObservable<TSource> source)
        {
            return Observable.Defer(() =>
            {
                var contacts = new ContactGeom[MaxContacts];
                var collisionGroup = new JointGroup();
                var space = new HashSpace();
                space.Cleanup = false;
                return Observable.Return(space).Concat(source.Select(xs =>
                {
                    collisionGroup.Clear();
                    space.Collide((g1, g2) =>
                    {
                        var body1 = g1.Body;
                        var body2 = g2.Body;
                        var numContacts = Geom.Collide(g1, g2, contacts);
                        if (body1 != null || body2 != null)
                        {
                            var world = body1 != null ? body1.World : body2.World;
                            for (int i = 0; i < numContacts; i++)
                            {
                                var contactInfo = new ContactInfo();
                                contactInfo.Geometry = contacts[i];
                                var contact = new Contact(world, contactInfo, collisionGroup);
                                contact.Attach(body1, body2);
                            }
                        }
                    });
                    return space;
                }).IgnoreElements())
                .Finally(() =>
                {
                    collisionGroup.Clear();
                    space.Dispose();
                });
            });
        }
    }
}
