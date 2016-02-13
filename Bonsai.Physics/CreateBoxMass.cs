using Ode.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CreateBoxMass : Combinator<Body, Body>
    {
        public double LengthX { get; set; }

        public double LengthY { get; set; }

        public double LengthZ { get; set; }

        public double TotalMass { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body => body.Mass = Mass.CreateBoxTotal(TotalMass, LengthX, LengthY, LengthZ));
        }
    }
}
