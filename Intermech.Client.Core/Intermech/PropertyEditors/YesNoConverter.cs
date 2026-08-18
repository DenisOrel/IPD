
// Type: Intermech.PropertyEditors.YesNoConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

public sealed class YesNoConverter : BooleanConverter
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
    if (value is string)
    {
      string strA = (string) value;
      if (string.Compare(strA, Consts.YesValue, true) == 0)
        return (object) true;
      if (string.Compare(strA, Consts.NoValue, true) == 0)
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
    return !Convert.ToBoolean(value) ? (object) Consts.NoValue : (object) Consts.YesValue;
  }
}
