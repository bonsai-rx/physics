using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Physics.Collision
{
    class SurfaceParametersConverter : TypeConverter
    {
        FieldInfo[] fieldCache;
        Dictionary<string, Attribute[]> attributeCache;

        Dictionary<string, Attribute[]> GetFieldAttributes()
        {
            return attributeCache ?? (attributeCache = new Dictionary<string, Attribute[]>
            {
                { "Bounce", new[] { new DescriptionAttribute("The restitution parameter for the surface, between 0 and 1.") } },
                { "BounceVelocity", new[] { new DescriptionAttribute("Specifies the minimum incoming velocity necessary for bounce.") } },
                { "Mode", new[] { new DescriptionAttribute("Specifies the active contact modes for the surface.") } },
                { "Motion1", new[] { new DescriptionAttribute("Specifies the surface velocity in friction direction 1.") } },
                { "Motion2", new[] { new DescriptionAttribute("Specifies the surface velocity in friction direction 2.") } },
                { "MotionN", new[] { new DescriptionAttribute("Specifies the surface velocity in the direction of the contact normal.") } },
                { "Mu", new[] { new DescriptionAttribute("Specifies the Coulomb friction coefficient of the surface, between 0 and infinity (∞).") } },
                { "Mu2", new[] { new DescriptionAttribute("Specifies the optional Coulomb friction coefficient for friction direction 2.") } },
                { "Rho", new[] { new DescriptionAttribute("Specifies the rolling friction coefficient around direction 1.") } },
                { "Rho2", new[] { new DescriptionAttribute("Specifies the rolling friction coefficient around direction 2.") } },
                { "RhoN", new[] { new DescriptionAttribute("Specifies the rolling friction coefficient around the normal direction.") } },
                { "Slip1", new[] { new DescriptionAttribute("Specifies the coefficient of force-dependent-slip (FDS) for friction direction 1.") } },
                { "Slip2", new[] { new DescriptionAttribute("Specifies the coefficient of force-dependent-slip (FDS) for friction direction 2.") } },
                { "SoftCfm", new[] { new DescriptionAttribute("Specifies the contact constraint force mixing parameter.") } },
                { "SoftErp", new[] { new DescriptionAttribute("Specifies the contact error reduction parameter.") } }
            });
        }

        FieldInfo[] GetFields(ITypeDescriptorContext context)
        {
            return fieldCache ?? (fieldCache = context.PropertyDescriptor.PropertyType.GetFields(BindingFlags.Instance | BindingFlags.Public));
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            var fields = GetFields(context);
            var instance = Activator.CreateInstance(context.PropertyDescriptor.PropertyType);
            foreach (var field in fields)
            {
                var value = Convert.ChangeType(propertyValues[field.Name], field.FieldType);
                field.SetValue(instance, value);
            }

            return instance;
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value != null)
            {
                var valueType = value.GetType();
                var fields = GetFields(context);
                var fieldAttributes = GetFieldAttributes();
                var fieldNames = fields.Select(field => field.Name).ToArray();
                var fieldProperties = fields.Select(field => new FieldPropertyDescriptor(valueType, field, context, fieldAttributes[field.Name])).ToArray();
                return new PropertyDescriptorCollection(fieldProperties).Sort(fieldNames);
            }
            return base.GetProperties(context, value, attributes);
        }

        class FieldPropertyDescriptor : SimplePropertyDescriptor
        {
            FieldInfo field;
            ITypeDescriptorContext structContext;

            public FieldPropertyDescriptor(Type componentType, FieldInfo fieldInfo, ITypeDescriptorContext context, Attribute[] attributes)
                : base(componentType, fieldInfo.Name, fieldInfo.FieldType, attributes)
            {
                field = fieldInfo;
                structContext = context;
            }

            public override object GetValue(object component)
            {
                return field.GetValue(component);
            }

            public override void SetValue(object component, object value)
            {
                field.SetValue(component, value);
                structContext.PropertyDescriptor.SetValue(structContext.Instance, component);
            }
        }
    }
}
