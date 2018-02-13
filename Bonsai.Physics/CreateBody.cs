using Ode.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [Description("Creates a new rigid body.")]
    public class CreateBody : Combinator<World, Body>
    {
        public CreateBody()
        {
            Orientation = Quaternion.Identity;
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The position of the rigid body.")]
        public Vector3 Position { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The orientation of the rigid body.")]
        public Quaternion Orientation { get; set; }

        [Description("Indicates whether the rigid body is in a kinematic state.")]
        public bool Kinematic { get; set; }

        public override IObservable<Body> Process(IObservable<World> source)
        {
            return source.Select(world =>
            {
                var body = new Body(world);
                body.Position = Position;
                body.Quaternion = Orientation;
                body.Kinematic = Kinematic;
                return body;
            });
        }
    }
}
