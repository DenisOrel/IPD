
// Type: Intermech.PropertyEditors.UnlimitedConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

public class UnlimitedConverter : TypeConverter
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
    if (!(value is string))
      return base.ConvertFrom(context, culture, value);
    return value.ToString() == CoreConsts.UnlimitedCaption ? (object) int.MaxValue : (object) Convert.ToInt32(value);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (int) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value is string && (string) value == CoreConsts.UnlimitedCaption && destinationType == typeof (int))
      return (object) int.MaxValue;
    if (value is int.MaxValue && destinationType == typeof (string))
      return (object) CoreConsts.UnlimitedCaption;
    return destinationType == typeof (int) ? (object) Convert.ToInt32(value) : base.ConvertTo(context, culture, value, destinationType);
  }
}
