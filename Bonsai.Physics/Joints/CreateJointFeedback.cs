using Ode.Net;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Joints
{
    [Combinator]
    public class CreateJointFeedback
    {
        public IObservable<JointFeedback> Process(IObservable<Joint> source)
        {
            return source.SelectMany(joint =>
            {
                var feedback = new JointFeedback();
                joint.Feedback = feedback;
                return Observable.Return(feedback).Concat(Observable.Never(feedback)).Finally(feedback.Dispose);
            });
        }
    }
}
