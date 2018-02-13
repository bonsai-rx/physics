using Ode.Net;
using Ode.Net.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    public abstract class CreateGeom<TGeom> : Combinator<Space, TGeom> where TGeom : Geom
    {
        protected CreateGeom()
        {
            Enabled = true;
        }

        [Description("Indicates whether the geometry object is active to intersect other objects.")]
        public bool Enabled { get; set; }

        [Description("The category bitfield of the geometry object.")]
        public int? CategoryBits { get; set; }

        [Description("The collision bitfield for the geometry object indicating with which categories the object should intersect.")]
        public int? CollideBits { get; set; }

        [Description("The name of the collision material used to generate contacts for the geometry object.")]
        public string Material { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The position of the geometry object relative to its body (or absolute if the geometry is static).")]
        public Vector3? Position { get; set; }

        [TypeConverter(typeof(NumericAggregateConverter))]
        [Description("The orientation of the geometry object relative to its body (or absolute if the geometry is static).")]
        public Quaternion? Orientation { get; set; }

        protected abstract TGeom CreateGeometryObject(Space space);

        private void ConfigureGeometryObject(TGeom geom)
        {
            geom.Enabled = Enabled;
            geom.CategoryBits = CategoryBits.GetValueOrDefault(-1);
            geom.CollideBits = CollideBits.GetValueOrDefault(-1);
            geom.Tag = new GeomMetadata(Material);

            var position = Position;
            var orientation = Orientation;
            if (position.HasValue || orientation.HasValue)
            {
                if (geom.Body != null)
                {
                    geom.OffsetPosition = position.GetValueOrDefault();
                    geom.OffsetQuaternion = orientation.HasValue ? orientation.Value : Quaternion.Identity;
                }
                else
                {
                    geom.Position = position.GetValueOrDefault();
                    geom.Quaternion = orientation.HasValue ? orientation.Value : Quaternion.Identity;
                }
            }
        }

        public override IObservable<TGeom> Process(IObservable<Space> source)
        {
            return source.Select(space =>
            {
                var geom = CreateGeometryObject(space);
                ConfigureGeometryObject(geom);
                return geom;
            });
        }

        public IObservable<TGeom> Process(IObservable<Space> spaces, IObservable<Body> bodies)
        {
            return spaces.CombineLatest(bodies, (space, body) =>
            {
                var geom = CreateGeometryObject(space);
                geom.Body = body;
                ConfigureGeometryObject(geom);
                return geom;
            });
        }

        public IObservable<TGeom> Process(IObservable<Body> bodies, IObservable<Space> spaces)
        {
            return bodies.CombineLatest(spaces, (body, space) =>
            {
                var geom = CreateGeometryObject(space);
                geom.Body = body;
                ConfigureGeometryObject(geom);
                return geom;
            });
        }
    }
}
