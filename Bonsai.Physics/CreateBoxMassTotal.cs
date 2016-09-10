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
    [Description("Creates a box mass of the specified dimensions and total mass.")]
    public class CreateBoxMassTotal : Combinator<Body, Body>
    {
        [Description("The length of the box along the X axis.")]
        public double LengthX { get; set; }

        [Description("The length of the box along the X axis.")]
        public double LengthY { get; set; }

        [Description("The length of the box along the X axis.")]
        public double LengthZ { get; set; }

        [Description("The total mass of the box.")]
        public double TotalMass { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body => body.Mass = Mass.CreateBoxTotal(TotalMass, LengthX, LengthY, LengthZ));
        }
    }
}
