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
    [Description("Advances the dynamics simulation by the specified time delta.")]
    public class WorldStep : Sink<World>
    {
        [Description("The number of seconds the simulation should advance in each step.")]
        public double StepSize { get; set; }

        public override IObservable<World> Process(IObservable<World> source)
        {
            return source.Do(world => world.QuickStep(StepSize));
        }
    }
}
