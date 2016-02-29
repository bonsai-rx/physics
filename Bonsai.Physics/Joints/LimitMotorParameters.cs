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

        public double Bounce { get; set; }

        public double? Cfm { get; set; }

        public double FudgeFactor { get; set; }

        public double HighStop { get; set; }

        public double LowStop { get; set; }

        public double MaxForce { get; set; }

        public double? StopCfm { get; set; }

        public double? StopErp { get; set; }

        public double Velocity { get; set; }

        public override string ToString()
        {
            return "(LimitMotor)";
        }
    }
}
