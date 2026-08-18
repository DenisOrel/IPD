
// Type: Intermech.PropertyEditors.AttrProcessor.DoubleConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// 
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="attributeId"></param>
/// <param name="attributeProcessor"></param>
internal class DoubleConverter(int attributeId, AttributeProcessor attributeProcessor) : 
  CommonTypeConverter(attributeId, attributeProcessor)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (value is string)
    {
      NumberFormatInfo provider = new NumberFormatInfo();
      provider.NumberDecimalSeparator = culture.NumberFormat.NumberDecimalSeparator;
      double result;
      if (double.TryParse((string) value, NumberStyles.Any, (IFormatProvider) provider, out result))
        return (object) result;
      provider.NumberDecimalSeparator = !culture.NumberFormat.NumberDecimalSeparator.Equals(".") ? "." : ",";
      return double.TryParse((string) value, NumberStyles.Any, (IFormatProvider) provider, out result) ? (object) result : base.ConvertFrom(context, culture, value);
    }
    if (value != null && value != DBNull.Value)
    {
      double result = 0.0;
      if (double.TryParse(value.ToString(), out result))
        return (object) result;
    }
    return base.ConvertFrom(context, culture, value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="sourceType"></param>
  /// <returns></returns>
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (long) || sourceType == typeof (int) || sourceType == typeof (Decimal) || base.CanConvertFrom(context, sourceType);
  }
}
