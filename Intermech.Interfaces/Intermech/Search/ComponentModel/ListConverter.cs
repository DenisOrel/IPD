
// Type: Intermech.Search.ComponentModel.ListConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Search.ComponentModel
{
    public sealed class ListConverter : CollectionConverter
    {
      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (!(value is IList) || !(destinationType == typeof (string)))
          return base.ConvertTo(context, culture, value, destinationType);
        return (object) string.Join(", ", (object) (IList) value);
      }

      public override PropertyDescriptorCollection GetProperties(
        ITypeDescriptorContext context,
        object value,
        Attribute[] attributes)
      {
        List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
        int index = 0;
        for (int count = ((ICollection) value).Count; index < count; ++index)
        {
          ListConverter.ListPropertyDescriptor propertyDescriptor = new ListConverter.ListPropertyDescriptor(value.GetType(), ((IList) value)[index].GetType(), index);
          propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
        }
        return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
      }

      public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

      protected sealed class ListPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
      {
        public ListPropertyDescriptor(Type listType, Type elementType, int index)
          : base(listType, $"[{(object) index}]", elementType)
        {
          this.Index = index >= 0 ? index : throw new ArgumentException();
        }

        public int Index { get; private set; }

        public override object GetValue(object component)
        {
          if (component == null)
            throw new ArgumentNullException(nameof (component));
          return component is IList ? ((IList) component)[this.Index] : throw new ArgumentException();
        }

        public override void SetValue(object component, object value)
        {
          if (component == null)
            throw new ArgumentNullException(nameof (component));
          if (!(component is IList))
            throw new ArgumentException();
          ((IList) component)[this.Index] = value;
        }
      }
    }
}
