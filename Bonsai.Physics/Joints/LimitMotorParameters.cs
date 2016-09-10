using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Joints
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LimitMotorParameters
    {
        public LimitMotorParameters()
        {
            FudgeFactor = 1.0;
            LowStop = double.NegativeInfinity;
            HighStop = double.PositiveInfinity;
        }

        [Description("The restitution strength at the stops.")]
        public double Bounce { get; set; }

        [Description("The optional constraint force mixing factor used when not at a stop.")]
        public double? Cfm { get; set; }

        [Description("The scale factor used to prevent excess force being applied at the stops.")]
        public double FudgeFactor { get; set; }

        [Description("The high stop angle or position.")]
        public double HighStop { get; set; }

        [Description("The low stop angle or position.")]
        public double LowStop { get; set; }

        [Description("The maximum force or torque that the motor will use to achieve the desired velocity.")]
        public double MaxForce { get; set; }

        [Description("The optional constraint force mixing factor for the stops.")]
        public double? StopCfm { get; set; }

        [Description("The optional error reduction parameter for the stops.")]
        public double? StopErp { get; set; }

        [Description("The desired motor velocity.")]
        public double Velocity { get; set; }

        public override string ToString()
        {
            return "(LimitMotor)";
        }
    }
}
