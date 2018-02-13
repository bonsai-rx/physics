using Ode.Net;
using Ode.Net.Collision;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    [DefaultProperty("CollisionHandlers")]
    [Description("Computes all collisions between geometry objects contained inside the input space.")]
    public class CollideSpace : Sink<Space>
    {
        readonly CollisionHandlerCollection collisionHandlers;

        public CollideSpace()
        {
            collisionHandlers = new CollisionHandlerCollection();
            ExcludeConnected = true;
        }

        [Description("The maximum number of contacts generated per collision pair.")]
        public int MaxContacts { get; set; }

        [Description("Indicates whether to exclude contacts between connected bodies.")]
        public bool ExcludeConnected { get; set; }

        [Description("The set of collision handlers used to generate contact constraints between intersecting bodies.")]
        [Editor("Bonsai.Design.DescriptiveCollectionEditor, Bonsai.Design", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public CollisionHandlerCollection CollisionHandlers
        {
            get { return collisionHandlers; }
        }

        public override IObservable<Space> Process(IObservable<Space> source)
        {
            return Observable.Defer(() =>
            {
                var handlers = collisionHandlers.ToDictionary(h => new CollisionHandlerKey(h.Material1, h.Material2));
                var contacts = new ContactGeom[MaxContacts];
                var collisionGroup = new JointGroup();
                return source.Do(space =>
                {
                    collisionGroup.Clear();
                    space.Collide((g1, g2) =>
                    {
                        var numContacts = Geom.Collide(g1, g2, contacts);
                        if (numContacts == 0) return;

                        var body1 = g1.Body;
                        var body2 = g2.Body;
                        var metadata1 = g1.Tag as GeomMetadata;
                        var metadata2 = g2.Tag as GeomMetadata;
                        if (metadata1 != null) metadata1.OnCollision(g1, g2, contacts, numContacts);
                        if (metadata2 != null) metadata2.OnCollision(g2, g1, contacts, numContacts);

                        CollisionHandler collisionHandler;
                        if (metadata1 != null && metadata2 != null &&
                            handlers.TryGetValue(new CollisionHandlerKey(metadata1.Material, metadata2.Material), out collisionHandler))
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
                }).Finally(collisionGroup.Clear);
            });
        }
    }
}
