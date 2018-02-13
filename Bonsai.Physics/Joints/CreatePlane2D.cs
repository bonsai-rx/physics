using Ode.Net;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Joints
{
    [Combinator]
    [Description("Creates a planar constraint on the specified body preventing movement along the Z axis.")]
    public class CreatePlane2D
    {
        public IObservable<Plane2D> Process(IObservable<Body> source)
        {
            return source.Select(body =>
            {
                var plane = new Plane2D(body.World);
                plane.Attach(body);
                return plane;
            });
        }
    }
}
