using Ode.Net;
using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    public abstract class CreateGeom<TGeom> : Combinator<Space, TGeom> where TGeom : Geom
    {
        public int? CategoryBits { get; set; }

        public int? CollideBits { get; set; }

        public string Material { get; set; }

        protected abstract TGeom CreateGeometryObject(Space space);

        private void ConfigureGeometryObject(TGeom geom)
        {
            geom.CategoryBits = CategoryBits.GetValueOrDefault(-1);
            geom.CollideBits = CollideBits.GetValueOrDefault(-1);
            geom.Tag = new GeomMetadata(Material);
        }

        public override IObservable<TGeom> Process(IObservable<Space> source)
        {
            return source.SelectMany(space =>
            {
                var geom = CreateGeometryObject(space);
                ConfigureGeometryObject(geom);
                return Observable.Return(geom).Concat(Observable.Never(geom)).Finally(geom.Dispose);
            });
        }

        public IObservable<TGeom> Process(IObservable<Space> spaces, IObservable<Body> bodies)
        {
            return spaces.CombineLatest(bodies, (space, body) =>
            {
                var geom = CreateGeometryObject(space);
                ConfigureGeometryObject(geom);
                geom.Body = body;
                return Observable.Return(geom).Concat(Observable.Never(geom)).Finally(geom.Dispose);
            }).Merge();
        }

        public IObservable<TGeom> Process(IObservable<Body> bodies, IObservable<Space> spaces)
        {
            return bodies.CombineLatest(spaces, (body, space) =>
            {
                var geom = CreateGeometryObject(space);
                ConfigureGeometryObject(geom);
                geom.Body = body;
                return Observable.Return(geom).Concat(Observable.Never(geom)).Finally(geom.Dispose);
            }).Merge();
        }
    }
}
