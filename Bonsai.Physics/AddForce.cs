using Ode.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class AddForce : Sink<Body>
    {
        public Vector3 Force { get; set; }

        public Vector3? Position { get; set; }

        public bool RelativeForce { get; set; }

        public bool RelativePosition { get; set; }

        public override IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body =>
            {
                var position = Position;
                if (RelativeForce)
                {
                    if (!position.HasValue) body.AddForce(Force);
                    else if (RelativePosition) body.AddForceAtRelativePosition(Force, position.Value);
                    else body.AddForceAtPosition(Force, position.Value);
                }
                else
                {
                    if (!position.HasValue) body.AddRelativeForce(Force);
                    else if (RelativePosition) body.AddRelativeForceAtRelativePosition(Force, position.Value);
                    else body.AddRelativeForceAtPosition(Force, position.Value);
                }
            });
        }
    }
}
