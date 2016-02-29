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
    public abstract class CreateJoint<TJoint> : Combinator<World, TJoint> where TJoint : Joint
    {
        protected abstract TJoint CreateJointObject(World world);

        protected static void SetLimitMotor(JointLimitMotor limitMotor, LimitMotorParameters parameters)
        {
            if (limitMotor == null)
            {
                throw new ArgumentNullException("limitMotor");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            limitMotor.Bounce = parameters.Bounce;
            if (parameters.Cfm.HasValue) limitMotor.Cfm = parameters.Cfm.Value;
            limitMotor.FudgeFactor = parameters.FudgeFactor;
            limitMotor.HighStop = parameters.HighStop;
            limitMotor.LowStop = parameters.LowStop;
            limitMotor.MaxForce = parameters.MaxForce;
            if (parameters.StopCfm.HasValue) limitMotor.StopCfm = parameters.StopCfm.Value;
            if (parameters.StopErp.HasValue) limitMotor.StopErp = parameters.StopErp.Value;
            limitMotor.Velocity = parameters.Velocity;
        }

        protected virtual void ConfigureJointObject(TJoint joint)
        {
        }

        public override IObservable<TJoint> Process(IObservable<World> source)
        {
            return source.SelectMany(world =>
            {
                var joint = CreateJointObject(world);
                ConfigureJointObject(joint);
                return Observable.Return(joint).Concat(Observable.Never(joint)).Finally(joint.Dispose);
            });
        }

        public IObservable<TJoint> Process(IObservable<Body> source)
        {
            return source.SelectMany(body =>
            {
                var joint = CreateJointObject(body.World);
                joint.Attach(body, null);
                ConfigureJointObject(joint);
                return Observable.Return(joint).Concat(Observable.Never(joint)).Finally(joint.Dispose);
            });
        }

        public IObservable<TJoint> Process(IObservable<Body> body1, IObservable<Body> body2)
        {
            return body1.CombineLatest(body2, (b1, b2) =>
            {
                var joint = CreateJointObject(b1.World);
                joint.Attach(b1, b2);
                ConfigureJointObject(joint);
                return Observable.Return(joint).Concat(Observable.Never(joint)).Finally(joint.Dispose);
            }).Merge();
        }
    }
}
