
// Type: Intermech.PropertyEditors.AttrProcessor.Int64Converter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors.AttrProcessor;

internal class Int64Converter(int attributeId, AttributeProcessor attributeProcessor) : 
  CommonTypeConverter(attributeId, attributeProcessor)
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (short) || sourceType == typeof (int) || sourceType == typeof (bool) || sourceType == typeof (Decimal) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    switch (value)
    {
      case short _:
      case int _:
        return (object) Convert.ToInt64(value);
      case Decimal _:
        return (object) Decimal.ToInt64(Convert.ToDecimal(value));
      case bool _:
        return (object) (Convert.ToBoolean(value) ? 1 : 0);
      default:
        return base.ConvertFrom(context, culture, value);
    }
  }
}
