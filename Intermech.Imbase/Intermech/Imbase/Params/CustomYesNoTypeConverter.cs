// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.CustomYesNoTypeConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase.Params;

internal class CustomYesNoTypeConverter : BooleanConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (value is string strA)
    {
      if (string.Compare(strA, Intermech.Consts.YesValue, true) == 0)
        return (object) true;
      if (string.Compare(strA, Intermech.Consts.NoValue, true) == 0)
        return (object) false;
    }
    return base.ConvertFrom(context, culture, value);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(destinationType == typeof (string)))
      return base.ConvertTo(context, culture, value, destinationType);
    return !Convert.ToBoolean(value) ? (object) Intermech.Consts.NoValue : (object) Intermech.Consts.YesValue;
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    return !(context.PropertyDescriptor is CustomPropertyDescriptor propertyDescriptor) ? (PropertyDescriptorCollection) null : propertyDescriptor.ChildProperties;
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    return !(context.PropertyDescriptor is CustomPropertyDescriptor propertyDescriptor) ? base.GetPropertiesSupported(context) : propertyDescriptor.PropertiesSupported;
  }
}
