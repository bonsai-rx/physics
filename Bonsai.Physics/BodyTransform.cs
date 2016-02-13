using Ode.Net;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    public class BodyTransform : Combinator<Body, Matrix4>
    {
        public override IObservable<Matrix4> Process(IObservable<Body> source)
        {
            return source.SelectMany(body =>
            {
                return Observable.FromEventPattern<EventHandler, EventArgs>(
                    handler => body.Moved += handler,
                    handler => body.Moved -= handler)
                    .Select(evt =>
                    {
                        Ode.Net.Vector3 position;
                        Ode.Net.Quaternion orientation;
                        body.GetPosition(out position);
                        body.GetQuaternion(out orientation);
                        return Matrix4.CreateFromQuaternion(new OpenTK.Quaternion((float)orientation.X, (float)orientation.Y, (float)orientation.Z, (float)orientation.W)) *
                            Matrix4.CreateTranslation((float)position.X, (float)position.Y, (float)position.Z);
                    });
            });
        }
    }
}
