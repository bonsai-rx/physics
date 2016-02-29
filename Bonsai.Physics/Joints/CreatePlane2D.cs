using Ode.Net;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Joints
{
    [Combinator]
    public class CreatePlane2D
    {
        public IObservable<Plane2D> Process(IObservable<Body> source)
        {
            return source.SelectMany(body =>
            {
                var plane = new Plane2D(body.World);
                plane.Attach(body);
                return Observable.Return(plane).Concat(Observable.Never(plane)).Finally(plane.Dispose);
            });
        }
    }
}
