using Ode.Net;
using Ode.Net.Collision;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    [Description("Creates a new hash collision space for geometry objects.")]
    public class CreateHashSpace : Source<Space>
    {
        [Description("The minimum cell size level for the hash space.")]
        public int MinLevel { get; set; }

        [Description("The maximum cell size level for the hash space")]
        public int MaxLevel { get; set; }

        public override IObservable<Space> Generate()
        {
            return Generate(Observable.Return(Unit.Default));
        }

        public IObservable<Space> Generate<TSource>(IObservable<TSource> source)
        {
            return source.Select(xs =>
            {
                Engine.Init();
                Engine.AllocateDataForThread(AllocateDataFlags.CollisionData);
                var space = new HashSpace();
                space.Cleanup = false;
                space.MinLevel = MinLevel;
                space.MaxLevel = MaxLevel;
                return space;
            });
        }
    }
}
