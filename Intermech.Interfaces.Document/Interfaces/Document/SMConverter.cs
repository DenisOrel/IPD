// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.SMConverter
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

/// <summary>Конвертер типа Float для полей с размерностью в сантиметрах.</summary>
public class SMConverter : FloatConverter
{
  /// <summary>Возвращает значение, показывающее, может ли этот конвертер преобразовать данный объект в заданный тип, используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="destinationType">Type, представляющий тип, в который требуется сделать преобразование</param>
  /// <returns>true, если конвертер может осуществить такое преобразование, false, если нет</returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }

  /// <summary>Преобразует данное значение в заданный тип, используя заданные
  /// контекстную информацию и информацию о культурной среде</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="culture">Объект CultureInfo. Если передается значение пустая ссылка,
  /// то предполагается использование информации о культурной среде</param>
  /// <param name="value">Объект Object, который нужно преобразовать</param>
  /// <param name="destinationType">Type, в который требуется преобразовать параметр value</param>
  /// <returns>Объект Object, представляющий преобразованное значение</returns>
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (destinationType == (Type) null)
      throw new ArgumentNullException(nameof (destinationType));
    return destinationType == typeof (string) && value is float num ? (object) (num.ToString() + LocalizationHolder.rm.GetString("Interfaces.Document_30")) : base.ConvertTo(context, culture, value, destinationType);
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
    string text = value as string;
    double number;
    string textAfterNumber;
    NumberParserAdvanced.ParseNumber(text, true, out number, out string _, out textAfterNumber);
    string str = textAfterNumber.Trim();
    if (text == null)
      return base.ConvertFrom(context, culture, value);
    double num;
    switch (str)
    {
      case "":
        num = 1.0;
        break;
      case "cm":
        num = 1.0;
        break;
      case "m":
        num = 100.0;
        break;
      case "mm":
        num = 0.1;
        break;
      case "pt":
        num = 0.035277777777777776;
        break;
      case "м":
        num = 100.0;
        break;
      case "мм":
        num = 0.1;
        break;
      case "пт":
        num = 0.035277777777777776;
        break;
      case "см":
        num = 1.0;
        break;
      default:
        throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Document_35") + value);
    }
    return (object) (float) Math.Round(number * num, 2);
  }
}
