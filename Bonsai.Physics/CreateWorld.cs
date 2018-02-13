using Ode.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [Description("Creates a new physics simulation world for rigid bodies and joints.")]
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

        [Description("The global constraint force mixing value.")]
        public double Cfm { get; set; }

        [Description("The global error reduction parameter, controlling how much error correction is performed in each step.")]
        public double Erp { get; set; }

        public override IObservable<World> Generate()
        {
            return Generate(Observable.Return(Unit.Default));
        }

        public IObservable<World> Generate<TSource>(IObservable<TSource> source)
        {
            return source.Select(xs =>
            {
                Engine.Init();
                Engine.AllocateDataForThread(AllocateDataFlags.BasicData);
                var world = new World();
                world.Cfm = Cfm;
                world.Erp = Erp;
                world.Gravity = Gravity;
                return world;
            });
        }
    }
}
