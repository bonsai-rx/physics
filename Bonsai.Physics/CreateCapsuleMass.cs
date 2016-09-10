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
    [Description("Creates a capsule mass of the specified dimensions and density.")]
    public class CreateCapsuleMass : Combinator<Body, Body>
    {
        [Description("The radius of the cylinder and spherical cap.")]
        public double Radius { get; set; }

        [Description("The length of the cylinder excluding the spherical cap.")]
        public double Length { get; set; }

        [Description("The orientation of the capsule's long axis.")]
        public DirectionAxis Direction { get; set; }

        [Description("The density of the capsule.")]
        public double Density { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body => body.Mass = Mass.CreateCapsule(Density, Direction, Radius, Length));
        }
    }
}
