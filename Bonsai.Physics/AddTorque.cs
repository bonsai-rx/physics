using Ode.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class AddTorque : Sink<Body>
    {
        public Vector3 Torque { get; set; }

        public bool Relative { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body =>
            {
                if (Relative) body.AddRelativeTorque(Torque);
                else body.AddTorque(Torque);
            });
        }
    }
}
