using Ode.Net;
using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics
{
    [Combinator]
    [WorkflowElementCategory(ElementCategory.Sink)]
    [Description("Updates the frame of reference of a rigid body or geometry object.")]
    public class SetTransform
    {
        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The optional new position of the rigid body or geometry object.")]
        public Vector3? Position { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The optional new orientation of the rigid body or geometry object.")]
        public Quaternion? Orientation { get; set; }

        public IObservable<Body> Process(IObservable<Body> source)
        {
            return source.Do(body =>
            {
                var position = Position;
                var orientation = Orientation;
                if (position.HasValue) body.Position = position.Value;
                if (orientation.HasValue) body.Quaternion = orientation.Value;
            });
        }

        public IObservable<TGeom> Process<TGeom>(IObservable<TGeom> source) where TGeom : Geom
        {
            return source.Do(geom =>
            {
                var position = Position;
                var orientation = Orientation;
                if (position.HasValue) geom.Position = position.Value;
                if (orientation.HasValue) geom.Quaternion = orientation.Value;
            });
        }
    }
}
