// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CustomBooleanNullableConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

public class CustomBooleanNullableConverter : CustomBooleanConverter
{
  /// <summary>Конструктор</summary>
  public CustomBooleanNullableConverter()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="trueString">Расшифровка для true</param>
  /// <param name="falseString">Расшифровка для false</param>
  public CustomBooleanNullableConverter(string trueString, string falseString)
    : base(trueString, falseString)
  {
  }

  /// <summary>Возвращает значение, показывающее, является ли исчерпывающим списком возможных
  /// значений коллекция стандартных значений, возвращаемая методом GetStandardValues,
  /// используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <returns>true, если объект TypeConverter.StandardValuesCollection, возвращенный
  /// методом GetStandardValues, является исчерпывающим списком возможных значений,
  /// false, если возможны другие значения</returns>
  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => false;

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
    return value != null && value is string && (string) value == "" ? (object) null : base.ConvertFrom(context, culture, value);
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
    return value == null ? (object) null : base.ConvertTo(context, culture, value, destinationType);
  }
}
