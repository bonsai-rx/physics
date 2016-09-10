using Ode.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CreateCapsuleMassTotal : Combinator<Body, Body>
    {
        public double Radius { get; set; }

        public double Length { get; set; }

        public DirectionAxis Direction { get; set; }

        public double TotalMass { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body => body.Mass = Mass.CreateCapsuleTotal(TotalMass, Direction, Radius, Length));
        }
    }
}
