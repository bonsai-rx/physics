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
    public class CreateHinge : CreateJoint<Hinge>
    {
        public CreateHinge()
        {
            Axis = Vector3.UnitX;
            LimitMotor = new LimitMotorParameters();
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        public Vector3 Axis { get; set; }

        public LimitMotorParameters LimitMotor { get; set; }

        protected override Hinge CreateJointObject(World world)
        {
            return new Hinge(world);
        }

        protected override void ConfigureJointObject(Hinge joint)
        {
            joint.Axis = Axis;
            SetLimitMotor(joint.LimitMotor, LimitMotor);
        }
    }
}
