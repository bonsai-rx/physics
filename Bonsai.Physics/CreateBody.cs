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
    public class CreateBody : Combinator<World, Body>
    {
        public CreateBody()
        {
            Orientation = Quaternion.Identity;
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        public Vector3 Position { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        public Quaternion Orientation { get; set; }

        public override IObservable<Body> Process(IObservable<World> source)
        {
            return source.SelectMany(world =>
            {
                var body = new Body(world);
                body.Position = Position;
                body.Quaternion = Orientation;
                return Observable.Return(body).Concat(Observable.Never(body)).Finally(body.Dispose);
            });
        }
    }
}
