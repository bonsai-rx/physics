using Ode.Net;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Joints
{
    [Description("Creates a hinge joint.")]
    public class CreateHinge : CreateJoint<Hinge>
    {
        public CreateHinge()
        {
            Axis = Vector3.UnitX;
            LimitMotor = new LimitMotorParameters();
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The hinge rotation axis, in world coordinates.")]
        public Vector3 Axis { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The optional joint anchor point, in world coordinates.")]
        public Vector3? Anchor { get; set; }

        [Description("The limit and motor parameters of the hinge.")]
        public LimitMotorParameters LimitMotor { get; set; }

        protected override Hinge CreateJointObject(World world)
        {
            return new Hinge(world);
        }

        protected override void ConfigureJointObject(Hinge joint)
        {
            joint.Axis = Axis;
            var anchor = Anchor;
            if (anchor.HasValue) joint.Anchor = anchor.Value;
            SetLimitMotor(joint.LimitMotor, LimitMotor);
        }
    }
}
