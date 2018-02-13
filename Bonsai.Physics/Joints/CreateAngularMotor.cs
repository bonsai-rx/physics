using Ode.Net;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Joints
{
    [Description("Creates an angular motor joint.")]
    public class CreateAngularMotor : CreateJoint<AngularMotor>
    {
        public CreateAngularMotor()
        {
            LimitMotor1 = new LimitMotorParameters();
            LimitMotor2 = new LimitMotorParameters();
            LimitMotor3 = new LimitMotorParameters();
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The first optional axis of the angular motor, in world coordinates.")]
        public Vector3? Axis1 { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The first optional axis of the angular motor, in world coordinates.")]
        public Vector3? Axis2 { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The first optional axis of the angular motor, in world coordinates.")]
        public Vector3? Axis3 { get; set; }

        [Description("The limit and motor parameters of the first angular motor axis.")]
        public LimitMotorParameters LimitMotor1 { get; set; }

        [Description("The limit and motor parameters of the second angular motor axis.")]
        public LimitMotorParameters LimitMotor2 { get; set; }

        [Description("The limit and motor parameters of the third angular motor axis.")]
        public LimitMotorParameters LimitMotor3 { get; set; }

        protected override AngularMotor CreateJointObject(World world)
        {
            return new AngularMotor(world);
        }

        protected override void ConfigureJointObject(AngularMotor joint)
        {
            var axis1 = Axis1;
            var axis2 = Axis2;
            var axis3 = Axis3;
            if (axis1.HasValue)
            {
                joint.Axis1 = axis1.Value;
                SetLimitMotor(joint.LimitMotor1, LimitMotor1);
                if (axis2.HasValue)
                {
                    joint.Axis2 = axis2.Value;
                    SetLimitMotor(joint.LimitMotor2, LimitMotor2);
                    if (axis3.HasValue)
                    {
                        joint.Axis3 = axis3.Value;
                        SetLimitMotor(joint.LimitMotor3, LimitMotor3);
                        joint.NumAxes = 3;
                    }
                    else joint.NumAxes = 2;
                }
                else if (axis3.HasValue)
                {
                    throw new InvalidOperationException("Active axes of an angular motor must be specified consecutively.");
                }
                else joint.NumAxes = 1;
            }
            else if (axis2.HasValue || axis3.HasValue)
            {
                throw new InvalidOperationException("Active axes of an angular motor must be specified consecutively.");
            }
            else joint.NumAxes = 0;
        }
    }
}
