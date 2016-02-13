using Ode.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CreateBody : Combinator<World, Body>
    {
        public double PositionX { get; set; }

        public double PositionY { get; set; }

        public double PositionZ { get; set; }

        public override IObservable<Body> Process(IObservable<World> source)
        {
            return source.SelectMany(world =>
            {
                var body = new Body(world);
                body.Position = new Vector3(PositionX, PositionY, PositionZ);
                return Observable.Return(body).Concat(Observable.Never(body)).Finally(body.Dispose);
            });
        }
    }
}
