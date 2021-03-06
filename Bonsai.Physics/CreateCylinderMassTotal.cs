﻿using Ode.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [Description("Creates a cylinder mass of the specified dimensions and total mass.")]
    public class CreateCylinderMassTotal : Combinator<Body, Body>
    {
        [Description("The radius of the cylinder.")]
        public double Radius { get; set; }

        [Description("The length of the cylinder.")]
        public double Length { get; set; }

        [Description("The orientation of the cylinder's long axis.")]
        public DirectionAxis Direction { get; set; }

        [Description("The total mass of the cylinder.")]
        public double TotalMass { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body => body.Mass = Mass.CreateCylinderTotal(TotalMass, Direction, Radius, Length));
        }
    }
}
