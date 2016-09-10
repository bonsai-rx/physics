using Ode.Net;
using Ode.Net.Collision;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [Combinator]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class GetTransform
    {
        public IObservable<Matrix4> Process(IObservable<Body> source)
        {
            return source.Select(body =>
            {
                Ode.Net.Vector3 position;
                Ode.Net.Quaternion orientation;
                body.GetPosition(out position);
                body.GetQuaternion(out orientation);
                return Matrix4.CreateFromQuaternion(new OpenTK.Quaternion((float)orientation.X, (float)orientation.Y, (float)orientation.Z, (float)orientation.W)) *
                    Matrix4.CreateTranslation((float)position.X, (float)position.Y, (float)position.Z);
            });
        }

        public IObservable<Matrix4> Process(IObservable<Geom> source)
        {
            return source.Select(geom =>
            {
                Ode.Net.Vector3 position;
                Ode.Net.Quaternion orientation;
                geom.GetPosition(out position);
                geom.GetQuaternion(out orientation);
                return Matrix4.CreateFromQuaternion(new OpenTK.Quaternion((float)orientation.X, (float)orientation.Y, (float)orientation.Z, (float)orientation.W)) *
                    Matrix4.CreateTranslation((float)position.X, (float)position.Y, (float)position.Z);
            });
        }
    }
}
