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
    public class CreateSphere : Combinator<Space, Sphere>
    {
        public double Radius { get; set; }

        public string Material { get; set; }

        public override IObservable<Sphere> Process(IObservable<Space> source)
        {
            return source.SelectMany(space =>
            {
                var sphere = new Sphere(space, Radius);
                sphere.Tag = new GeomMetadata(Material);
                return Observable.Return(sphere).Concat(Observable.Never(sphere)).Finally(sphere.Dispose);
            });
        }

        public IObservable<Sphere> Process(IObservable<Space> spaces, IObservable<Body> bodies)
        {
            return spaces.CombineLatest(bodies, (space, body) =>
            {
                var sphere = new Sphere(space, Radius);
                sphere.Body = body;
                sphere.Tag = new GeomMetadata(Material);
                return Observable.Return(sphere).Concat(Observable.Never(sphere)).Finally(sphere.Dispose);
            }).Merge();
        }

        public IObservable<Sphere> Process(IObservable<Body> bodies, IObservable<Space> spaces)
        {
            return bodies.CombineLatest(spaces, (body, space) =>
            {
                var sphere = new Sphere(space, Radius);
                sphere.Body = body;
                sphere.Tag = new GeomMetadata(Material);
                return Observable.Return(sphere).Concat(Observable.Never(sphere)).Finally(sphere.Dispose);
            }).Merge();
        }
    }
}
