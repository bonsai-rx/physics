using Ode.Net;
using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CreateBox : Combinator<Space, Box>
    {
        public double LengthX { get; set; }

        public double LengthY { get; set; }

        public double LengthZ { get; set; }

        public string Material { get; set; }

        public override IObservable<Box> Process(IObservable<Space> source)
        {
            return source.SelectMany(space =>
            {
                var box = new Box(space, LengthX, LengthY, LengthZ);
                box.Tag = new GeomMetadata(Material);
                return Observable.Return(box).Concat(Observable.Never(box)).Finally(box.Dispose);
            });
        }

        public IObservable<Box> Process(IObservable<Space> spaces, IObservable<Body> bodies)
        {
            return spaces.CombineLatest(bodies, (space, body) =>
            {
                var box = new Box(space, LengthX, LengthY, LengthZ);
                box.Body = body;
                return Observable.Return(box).Concat(Observable.Never(box)).Finally(box.Dispose);
            }).Merge();
        }

        public IObservable<Box> Process(IObservable<Body> bodies, IObservable<Space> spaces)
        {
            return bodies.CombineLatest(spaces, (body, space) =>
            {
                var box = new Box(space, LengthX, LengthY, LengthZ);
                box.Body = body;
                return Observable.Return(box).Concat(Observable.Never(box)).Finally(box.Dispose);
            }).Merge();
        }
    }
}
