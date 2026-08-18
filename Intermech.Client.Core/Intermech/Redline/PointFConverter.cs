
// Type: Intermech.Redline.PointFConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;


namespace Intermech.Redline;

internal sealed class PointFConverter : TypeConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return !(sourceType != typeof (string)) || !(sourceType != typeof (InstanceDescriptor)) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    switch (value)
    {
      case string str:
        char[] chArray = new char[1]{ ',' };
        string[] strArray = str.Split(chArray);
        return (object) new PointF(float.Parse(strArray[0], (IFormatProvider) NumberFormatInfo.InvariantInfo), float.Parse(strArray[1], (IFormatProvider) NumberFormatInfo.InvariantInfo));
      case InstanceDescriptor instanceDescriptor:
        if (instanceDescriptor.Arguments.Count == 2)
        {
          object[] objArray = new object[2];
          instanceDescriptor.Arguments.CopyTo((Array) objArray, 0);
          return (object) new PointF((float) objArray[0], (float) objArray[1]);
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
    if (value is PointF pointF)
    {
      if (destinationType == typeof (string))
        return (object) $"{pointF.X.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo)}, {pointF.Y.ToString((IFormatProvider) NumberFormatInfo.InvariantInfo)}";
      if (destinationType == typeof (InstanceDescriptor))
      {
        ConstructorInfo constructor = typeof (PointF).GetConstructor(new Type[2]
        {
          typeof (float),
          typeof (float)
        });
        if (constructor != (ConstructorInfo) null)
        {
          object[] arguments = new object[2]
          {
            (object) pointF.X,
            (object) pointF.Y
          };
          return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) arguments, true);
        }
      }
    }
    return base.ConvertTo(context, culture, value, destinationType);
  }

  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    return (object) new PointF((float) propertyValues[(object) "X"], (float) propertyValues[(object) "Y"]);
  }

  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;
}
