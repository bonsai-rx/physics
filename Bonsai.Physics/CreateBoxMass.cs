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
    [Description("Creates a box mass of the specified dimensions and density.")]
    public class CreateBoxMass : Combinator<Body, Body>
    {
        [Description("The length of the box along the X axis.")]
        public double LengthX { get; set; }

        [Description("The length of the box along the Y axis.")]
        public double LengthY { get; set; }

        [Description("The length of the box along the Z axis.")]
        public double LengthZ { get; set; }

        [Description("The density of the box.")]
        public double Density { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body => body.Mass = Mass.CreateBox(Density, LengthX, LengthY, LengthZ));
        }
    }
}
