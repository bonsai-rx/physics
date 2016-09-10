using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [Description("Creates a representation of a joint axis at a specified anchor.")]
    public class CreateAxisAnchor : Transform<Matrix4, AxisAnchor>
    {
        Vector3 initialAxis;
        Vector3 initialAnchor;

        public CreateAxisAnchor()
        {
            Axis = Ode.Net.Vector3.UnitX;
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The axis of the joint.")]
        public Ode.Net.Vector3 Axis
        {
            get { return new Ode.Net.Vector3(initialAxis.X, initialAxis.Y, initialAxis.Z); }
            set { initialAxis = new Vector3((float)value.X, (float)value.Y, (float)value.Z); }
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The anchor of the joint.")]
        public Ode.Net.Vector3 Anchor
        {
            get { return new Ode.Net.Vector3(initialAnchor.X, initialAnchor.Y, initialAnchor.Z); }
            set { initialAnchor = new Vector3((float)value.X, (float)value.Y, (float)value.Z); }
        }

        public override IObservable<AxisAnchor> Process(IObservable<OpenTK.Matrix4> source)
        {
            return source.Select(input =>
            {
                Vector3 axis, anchor;
                Vector3.TransformNormal(ref initialAxis, ref input, out axis);
                Vector3.TransformPosition(ref initialAnchor, ref input, out anchor);
                return new AxisAnchor(axis.X, axis.Y, axis.Z, anchor.X, anchor.Y, anchor.Z);
            });
        }
    }
}
