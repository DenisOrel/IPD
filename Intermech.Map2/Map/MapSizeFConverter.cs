// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapSizeFConverter
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;


namespace Intermech.Map
{
    public sealed class MapSizeFConverter : TypeConverter
    {
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
        return !(sourceType != typeof (string)) || !(sourceType != typeof (InstanceDescriptor)) || base.CanConvertFrom(context, sourceType);
      }

      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
        return !(destinationType != typeof (string)) || !(destinationType != typeof (InstanceDescriptor)) || base.CanConvertTo(context, destinationType);
      }

      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        switch (value)
        {
          case string _:
            char[] chArray = new char[1]{ 'x' };
            string[] strArray = ((string) value).Split(chArray);
            return (object) new SizeF(float.Parse(strArray[0], (IFormatProvider) NumberFormatInfo.InvariantInfo), float.Parse(strArray[1], (IFormatProvider) NumberFormatInfo.InvariantInfo));
          case InstanceDescriptor _:
            InstanceDescriptor instanceDescriptor = (InstanceDescriptor) value;
            if (instanceDescriptor.Arguments.Count == 2)
            {
              object[] objArray = new object[2];
              instanceDescriptor.Arguments.CopyTo((Array) objArray, 0);
              return (object) new SizeF((float) objArray[0], (float) objArray[1]);
            }
            break;
        }
        return base.ConvertFrom(context, culture, value);
      }

      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (value is SizeF sizeF)
        {
          if (destinationType == typeof (string))
            return (object) $"{sizeF.Width.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo)}x{sizeF.Height.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo)}";
          if (destinationType == typeof (InstanceDescriptor))
          {
            ConstructorInfo constructor = typeof (SizeF).GetConstructor(new Type[2]
            {
              typeof (float),
              typeof (float)
            });
            if (constructor != (ConstructorInfo) null)
            {
              object[] arguments = new object[2]
              {
                (object) sizeF.Width,
                (object) sizeF.Height
              };
              return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) arguments, true);
            }
          }
        }
        return base.ConvertTo(context, culture, value, destinationType);
      }

      public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
      {
        return (object) new SizeF((float) propertyValues[(object) "Width"], (float) propertyValues[(object) "Height"]);
      }

      public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;
    }
}
