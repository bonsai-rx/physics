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
    [Description("Adds force to a rigid body applied at some specified position.")]
    public class AddForce : Sink<Body>
    {
        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The vector specifying the force to be applied.")]
        public Vector3 Force { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The optional position at which the force will be applied. If no position is specified, the force is applied at the center of mass.")]
        public Vector3? Position { get; set; }

        [Description("Indicates whether the force vector is provided in relative or absolute coordinates.")]
        public bool RelativeForce { get; set; }

        [Description("Indicates whether the position vector is provided in relative or absolute coordinates.")]
        public bool RelativePosition { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body =>
            {
                var position = Position;
                if (RelativeForce)
                {
                    if (!position.HasValue) body.AddRelativeForce(Force);
                    else if (RelativePosition) body.AddRelativeForceAtRelativePosition(Force, position.Value);
                    else body.AddRelativeForceAtPosition(Force, position.Value);
                }
                else
                {
                    if (!position.HasValue) body.AddForce(Force);
                    else if (RelativePosition) body.AddForceAtRelativePosition(Force, position.Value);
                    else body.AddForceAtPosition(Force, position.Value);
                }
            });
        }
    }
}
