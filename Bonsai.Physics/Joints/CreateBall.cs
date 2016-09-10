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
    [Description("Creates a ball joint.")]
    public class CreateBall : CreateJoint<Ball>
    {
        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The anchor position of the ball joint, in world coordinates.")]
        public Vector3 Anchor { get; set; }

        [Description("The optional constraint force mixing factor for the ball joint.")]
        public double? Cfm { get; set; }

        [Description("The optional error reduction parameter for the ball joint.")]
        public double? Erp { get; set; }

        protected override Ball CreateJointObject(World world)
        {
            return new Ball(world);
        }

        protected override void ConfigureJointObject(Ball joint)
        {
            var cfm = Cfm;
            var erp = Erp;
            joint.Anchor = Anchor;
            if (cfm.HasValue) joint.Cfm = cfm.Value;
            if (erp.HasValue) joint.Erp = erp.Value;
        }
    }
}
