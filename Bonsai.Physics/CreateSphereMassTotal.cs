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
    [Description("Creates a sphere mass of the specified dimensions and total mass.")]
    public class CreateSphereMassTotal : Combinator<Body, Body>
    {
        [Description("The radius of the sphere.")]
        public double Radius { get; set; }

        [Description("The total mass of the sphere.")]
        public double TotalMass { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body => body.Mass = Mass.CreateSphereTotal(TotalMass, Radius));
        }
    }
}
