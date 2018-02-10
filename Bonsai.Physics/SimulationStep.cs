using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [Description("Produces a sequence of fixed simulation microsteps matching the elapsed real time.")]
    public class SimulationStep : Combinator<double, double>
    {
        [Description("The number of seconds the simulation should advance in each microstep.")]
        public double StepSize { get; set; }

        [Description("The optional upper bound for elapsed time, used to allow running non-real time simulations in slower systems.")]
        public double? MaxElapsedTime { get; set; }

        public override IObservable<double> Process(IObservable<double> source)
        {
            return Observable.Create<double>(observer =>
            {
                var remainingTime = 0.0;
                return source.Do(elapsedTime =>
                {
                    elapsedTime += remainingTime;
                    if (elapsedTime > MaxElapsedTime)
                    {
                        elapsedTime = MaxElapsedTime.GetValueOrDefault();
                    }

                    var stepSize = StepSize;
                    if (stepSize <= 0)
                    {
                        throw new InvalidOperationException("The size of each simulation microstep must be a positive value.");
                    }

                    var stepCount = (int)Math.Floor(elapsedTime / stepSize);
                    for (int i = 0; i < stepCount; i++)
                    {
                        observer.OnNext(stepSize);
                    }

                    remainingTime = elapsedTime - stepCount * stepSize;
                })
                .IgnoreElements()
                .SubscribeSafe(observer);
            });
        }
    }
}
