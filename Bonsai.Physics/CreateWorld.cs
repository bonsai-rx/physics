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
    [Description("Creates and optionally updates a physics simulation world for rigid bodies and joints.")]
    public class CreateWorld : Source<World>
    {
        public CreateWorld()
        {
            Cfm = 0.0000000001;
            Erp = 0.2;
        }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The world's global gravity vector.")]
        public Vector3 Gravity { get; set; }

        [Description("The number of seconds the simulation should advance in each step.")]
        public double StepSize { get; set; }

        [Description("The global constraint force mixing value.")]
        public double Cfm { get; set; }

        [Description("The global error reduction parameter, controlling how much error correction is performed in each step.")]
        public double Erp { get; set; }

        public override IObservable<World> Generate()
        {
            return Observable.Using(OdeManager.ReserveEngine, engine => Observable.Defer(() =>
            {
                var world = new World();
                world.Cfm = Cfm;
                world.Erp = Erp;
                world.Gravity = Gravity;
                return Observable.Return(world)
                                 .Concat(Observable.Never(world))
                                 .Finally(world.Dispose);
            }));
        }

        public IObservable<World> Generate<TSource>(IObservable<TSource> source)
        {
            return Observable.Using(OdeManager.ReserveEngine, engine => Observable.Defer(() =>
            {
                Ode.Net.Engine.Init();
                var world = new World();
                world.Cfm = Cfm;
                world.Erp = Erp;
                world.Gravity = Gravity;
                return Observable.Return(world).Concat(source.Select(xs =>
                {
                    world.QuickStep(StepSize);
                    return world;
                }).IgnoreElements())
                .Finally(world.Dispose);
            }));
        }
    }
}
