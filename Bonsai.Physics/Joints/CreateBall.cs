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
    public class CreateBall : CreateJoint<Ball>
    {
        [TypeConverter(typeof(NumericAggregateConverter))]
        public Vector3 Anchor { get; set; }

        public double? Cfm { get; set; }

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
