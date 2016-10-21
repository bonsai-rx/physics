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
    [Description("Adds torque at the center of mass of a rigid body.")]
    public class AddTorque : Sink<Body>
    {
        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The vector specifying the torque to be applied.")]
        public Vector3 Torque { get; set; }

        [Description("Indicates whether the torque vector is provided in relative or absolute coordinates.")]
        public bool Relative { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body =>
            {
                if (Relative) body.AddRelativeTorque(Torque);
                else body.AddTorque(Torque);
            });
        }
    }
}
