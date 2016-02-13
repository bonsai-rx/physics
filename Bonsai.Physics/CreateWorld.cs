using Ode.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class CreateWorld : Source<World>
    {
        public double GravityX { get; set; }

        public double GravityY { get; set; }

        public double GravityZ { get; set; }

        public double StepSize { get; set; }

        public override IObservable<World> Generate()
        {
            return Observable.Defer(() =>
            {
                var world = new World();
                world.Gravity = new Vector3(GravityX, GravityY, GravityZ);
                return Observable.Return(world)
                                 .Concat(Observable.Never(world))
                                 .Finally(world.Dispose);
            });
        }

        public IObservable<World> Generate<TSource>(IObservable<TSource> source)
        {
            return Observable.Defer(() =>
            {
                Ode.Net.Engine.Init();
                var world = new World();
                world.Gravity = new Vector3(GravityX, GravityY, GravityZ);
                return Observable.Return(world).Concat(source.Select(xs =>
                {
                    world.QuickStep(StepSize);
                    return world;
                }).IgnoreElements())
                .Finally(world.Dispose);
            });
        }
    }
}
