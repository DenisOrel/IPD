// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.FloatConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа Float</summary>
public class FloatConverter : TypeConverter
{
  /// <summary>Исправлять десятичный разделитель ',' и '.'</summary>
  public static bool CorrectDecimalSeparator = true;

  /// <summary>Исправить десятичный разделитель ',' или '.' на системный</summary>
  public static string CorrectDecimal(string value)
  {
    return FloatConverter.CorrectDecimal(value, (CultureInfo) null);
  }

  /// <summary>Исправить десятичный разделитель ',' или '.' на системный</summary>
  public static string CorrectDecimal(string value, CultureInfo culture)
  {
    if (culture == null)
      culture = CultureInfo.CurrentCulture;
    if (value != null && value != "" && FloatConverter.CorrectDecimalSeparator)
    {
      if (culture.NumberFormat.NumberDecimalSeparator != ",")
        value = value.Replace(",", culture.NumberFormat.NumberDecimalSeparator);
      if (culture.NumberFormat.NumberDecimalSeparator != ".")
        value = value.Replace(".", culture.NumberFormat.NumberDecimalSeparator);
    }
    return value;
  }

  /// <summary>Возвращает значение, показывающее, может ли этот конвертер преобразовать
  /// объект данного типа в тип этого конвертера, используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="sourceType">Type, представляющий тип, из которого требуется сделать преобразование</param>
  /// <returns>true, если конвертер может осуществить такое преобразование, false, если нет</returns>
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  /// <summary>Преобразует данный объект в тип этого конвертера,
  /// используя заданную контекстную информацию и информацию о культурной среде</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="culture">Объект CultureInfo, который нужно использовать в качестве текущей культурной среды</param>
  /// <param name="value">Объект Object, который нужно преобразовать</param>
  /// <returns>Объект Object, представляющий преобразованное значение</returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value is string str))
      return base.ConvertFrom(context, culture, value);
    return str.Length == 0 ? (object) null : (object) FloatConverter.ConvertFromString(str, culture);
  }

  /// <summary>Преобразовать строку в Float</summary>
  /// <param name="value">Строка</param>
  /// <param name="culture">Культура</param>
  /// <returns></returns>
  public static float ConvertFromString(string value, CultureInfo culture)
  {
    string s = value != null ? value.Trim() : throw new ArgumentNullException(nameof (value));
    if (s.Length == 0)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_58") + value);
    if (culture == null)
      culture = CultureInfo.CurrentCulture;
    if (FloatConverter.CorrectDecimalSeparator)
    {
      if (culture.NumberFormat.NumberDecimalSeparator != ",")
        s = s.Replace(",", culture.NumberFormat.NumberDecimalSeparator);
      if (culture.NumberFormat.NumberDecimalSeparator != ".")
        s = s.Replace(".", culture.NumberFormat.NumberDecimalSeparator);
    }
    float result = 0.0f;
    if (float.TryParse(s, NumberStyles.Float, (IFormatProvider) culture, out result))
      return result;
    throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_59") + value);
  }
}
