
// Type: Intermech.PropertyEditors.IntZeroEmptyStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

/// <summary>
/// Инт32-конвентор, отображающий пустую строку вместо ноля.
/// Нужен там, где 0 - это неработающая настройка, а не числовое значение.
/// </summary>
public class IntZeroEmptyStringConverter : Int32Converter
{
  /// <summary>
  /// Converts the given object to the type of this converter, using the specified context and culture information.
  /// </summary>
  /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
  /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
  /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
  /// <returns>
  /// An <see cref="T:System.Object" /> that represents the converted value.
  /// </returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value is string && value as string == string.Empty ? (object) 0 : base.ConvertFrom(context, culture, value);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return (int) value == 0 ? (object) string.Empty : base.ConvertTo(context, culture, value, destinationType);
  }
}
