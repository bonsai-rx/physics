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
                var fieldNames = fields.Select(field => field.Name).ToArray();
                var fieldProperties = fields.Select(field => new FieldPropertyDescriptor(valueType, field, context)).ToArray();
                return new PropertyDescriptorCollection(fieldProperties).Sort(fieldNames);
            }
            return base.GetProperties(context, value, attributes);
        }

        class FieldPropertyDescriptor : SimplePropertyDescriptor
        {
            FieldInfo field;
            ITypeDescriptorContext structContext;

            public FieldPropertyDescriptor(Type componentType, FieldInfo fieldInfo, ITypeDescriptorContext context)
                : base(componentType, fieldInfo.Name, fieldInfo.FieldType)
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
