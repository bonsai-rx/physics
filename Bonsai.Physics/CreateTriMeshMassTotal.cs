﻿using Ode.Net;
using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [Description("Creates a mass for the given triangle mesh with the specified total mass.")]
    public class CreateTriMeshMassTotal : Combinator<TriMesh, TriMesh>
    {
        [Description("The total mass of the triangle mesh.")]
        public double TotalMass { get; set; }

        public override IObservable<TriMesh> Process(IObservable<TriMesh> source)
        {
            return source.Do(triMesh =>
            {
                var body = triMesh.Body;
                if (body == null)
                {
                    throw new InvalidOperationException("The triangle mesh must be associated with a rigid body.");
                }

                body.Mass = Mass.CreateTriMeshTotal(TotalMass, triMesh);
            });
        }
    }
}
