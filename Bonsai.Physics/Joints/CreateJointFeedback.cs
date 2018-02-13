using Ode.Net;
using Ode.Net.Joints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Joints
{
    [Combinator]
    [Description("Creates a joint feedback structure for the specified joint.")]
    public class CreateJointFeedback
    {
        public IObservable<JointFeedback> Process(IObservable<Joint> source)
        {
            return source.Select(joint =>
            {
                var feedback = new JointFeedback();
                joint.Feedback = feedback;
                return feedback;
            });
        }
    }
}
