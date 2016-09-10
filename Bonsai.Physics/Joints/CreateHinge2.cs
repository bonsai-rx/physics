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
    [Description("Creates a hinge-2 joint.")]
    public class CreateHinge2 : CreateJoint<Hinge2>
    {
        public CreateHinge2()
        {
            Axis1 = Vector3.UnitX;
            LimitMotor1 = new LimitMotorParameters();
            LimitMotor2 = new LimitMotorParameters();
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The axis for the first hinge, in world coordinates.")]
        public Vector3 Axis1 { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The axis for the second hinge, in world coordinates.")]
        public Vector3 Axis2 { get; set; }

        [Description("The limit and motor parameters of the first hinge.")]
        public LimitMotorParameters LimitMotor1 { get; set; }

        [Description("The limit and motor parameters of the second hinge.")]
        public LimitMotorParameters LimitMotor2 { get; set; }

        protected override Hinge2 CreateJointObject(World world)
        {
            return new Hinge2(world);
        }

        protected override void ConfigureJointObject(Hinge2 joint)
        {
            joint.Axis1 = Axis1;
            joint.Axis2 = Axis2;
            SetLimitMotor(joint.LimitMotor1, LimitMotor1);
            SetLimitMotor(joint.LimitMotor2, LimitMotor2);
        }
    }
}
