
// Type: Intermech.Search.BlobInfoConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;


namespace Intermech.Search
{
    public sealed class BlobInfoConverter : TypeConverter
    {
      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (!(value is BlobInfo) || !(destinationType == typeof (string)))
          return base.ConvertTo(context, culture, value, destinationType);
        BlobInfo blobInfo = (BlobInfo) value;
        return (object) blobInfo.FileName ?? (object) blobInfo.BlobID.ToString();
      }

      public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

      public override PropertyDescriptorCollection GetProperties(
        ITypeDescriptorContext context,
        object value,
        Attribute[] attributes)
      {
        return new PropertyDescriptorCollection((System.ComponentModel.PropertyDescriptor[]) ((IEnumerable<PropertyInfo>) typeof (BlobInfo).GetProperties(BindingFlags.Instance | BindingFlags.Public)).Select<PropertyInfo, BlobInfoConverter.PropertyDescriptor>((Func<PropertyInfo, BlobInfoConverter.PropertyDescriptor>) (o => new BlobInfoConverter.PropertyDescriptor(typeof (BlobInfo), o))).ToArray<BlobInfoConverter.PropertyDescriptor>());
      }

      private sealed class PropertyDescriptor : TypeConverter.SimplePropertyDescriptor
      {
        public PropertyDescriptor(Type componentType, PropertyInfo propertyInfo)
          : base(componentType, BlobInfoConverter.PropertyDescriptor.GetDisplayName(propertyInfo), propertyInfo.GetType())
        {
          if (componentType == (Type) null)
            throw new ArgumentNullException(nameof (componentType));
          this.PropertyInfo = !(propertyInfo == (PropertyInfo) null) ? propertyInfo : throw new ArgumentNullException(nameof (propertyInfo));
        }

        public PropertyInfo PropertyInfo { get; private set; }

        public override bool ShouldSerializeValue(object component) => false;

        public override object GetValue(object component)
        {
          return this.PropertyInfo.GetValue(component, new object[0]);
        }

        public override void SetValue(object component, object value)
        {
          throw new NotImplementedException();
        }

        private static string GetDisplayName(PropertyInfo propertyInfo)
        {
          return !(Attribute.GetCustomAttribute((MemberInfo) propertyInfo, typeof (DisplayNameAttribute)) is DisplayNameAttribute customAttribute) ? propertyInfo.Name : customAttribute.DisplayName;
        }
      }
    }
}
