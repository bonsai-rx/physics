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
    public class CreateHinge
    {
        public IObservable<Hinge> Process(IObservable<Body> body1, IObservable<Body> body2)
        {
            return body1.CombineLatest(body2, (b1, b2) =>
            {
                var hinge = new Hinge(b1.World);
                hinge.Attach(b1, b2);
                return Observable.Return(hinge).Concat(Observable.Never(hinge)).Finally(hinge.Dispose);
            }).Merge();
        }
    }
}
