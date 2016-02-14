﻿using Ode.Net;
using Ode.Net.Collision;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [DefaultProperty("CollisionHandlers")]
    public class CreateHashSpace : Source<HashSpace>
    {
        readonly CollisionHandlerCollection collisionHandlers;

        public CreateHashSpace()
        {
            collisionHandlers = new CollisionHandlerCollection();
            ExcludeConnected = true;
        }

        public int MaxContacts { get; set; }

        public bool ExcludeConnected { get; set; }

        public CollisionHandlerCollection CollisionHandlers
        {
            get { return collisionHandlers; }
        }

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
                        var metadata1 = g1.Tag as GeomMetadata;
                        var metadata2 = g2.Tag as GeomMetadata;
                        var numContacts = Geom.Collide(g1, g2, contacts);
                        if (metadata1 != null) metadata1.OnCollision(g1, g2, contacts, numContacts);
                        if (metadata2 != null) metadata2.OnCollision(g2, g1, contacts, numContacts);
                        var collisionHandler = (metadata1 != null && metadata2 != null)
                            ? collisionHandlers[new CollisionHandlerKey(metadata1.Material, metadata2.Material)]
                            : null;
                        if (collisionHandler != null)
                        {
                            if (body1 != null || body2 != null)
                            {
                                if (ExcludeConnected && body1 != null && body2 != null &&
                                    Body.AreConnectedExcluding(body1, body2, JointType.Contact))
                                    return;

                                var collisionSurface = collisionHandler.CollisionSurface;
                                var world = body1 != null ? body1.World : body2.World;
                                for (int i = 0; i < numContacts; i++)
                                {
                                    var contactInfo = new ContactInfo();
                                    contactInfo.Geometry = contacts[i];
                                    contactInfo.Surface = collisionSurface;
                                    var contact = new Contact(world, contactInfo, collisionGroup);
                                    contact.Attach(body1, body2);
                                }
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
