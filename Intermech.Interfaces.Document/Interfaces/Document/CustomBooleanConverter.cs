// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CustomBooleanConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер для булевского типа. При конвертировании в строку выдает Да/Нет.
/// Имеет список стандартных значений.</summary>
public class CustomBooleanConverter : BooleanConverter
{
  /// <summary>Расшифровка значения true</summary>
  public string TrueString = LocalizationHolder.rm.GetString("Interfaces.Document_4");
  /// <summary>Расшифровка значения false</summary>
  public string FalseString = LocalizationHolder.rm.GetString("Interfaces.Document_5");

  /// <summary>Конструктор</summary>
  public CustomBooleanConverter()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="trueString">Расшифровка для true</param>
  /// <param name="falseString">Расшифровка для false</param>
  public CustomBooleanConverter(string trueString, string falseString)
  {
    this.TrueString = trueString;
    this.FalseString = falseString;
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
    if (!(destinationType == typeof (string)) || !(value is bool flag))
      return base.ConvertTo(context, culture, value, destinationType);
    return !flag ? (object) this.FalseString : (object) this.TrueString;
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
    if (value == null || !(value is string))
      return base.ConvertFrom(context, culture, value);
    string str = (string) value;
    if (str.ToLower() == this.TrueString.ToLower())
      return (object) true;
    if (str.ToLower() == this.FalseString.ToLower() || str.Trim() == string.Empty)
      return (object) false;
    throw new ArgumentException($"Can not convert '{(string) value}' to type Boolean");
  }

  /// <summary>Получает значение, показывающее, поддерживает ли этот объект стандартный
  /// набор значений, которые можно выбрать из списка, используя заданную
  /// контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <returns>true, если, чтобы найти стандартный набор значений, поддерживаемых данным объектом, следует
  /// вызвать метод GetStandardValues, false, если нет</returns>
  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

  /// <summary>Возвращает коллекцию стандартных значений для того типа данных,
  /// которым предназначен этот конвертер типа, если предоставлена контекстная
  /// информация о формате</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
  /// о формате, которая может быть использована для извлечения дополнительных сведений о среде,
  ///  из которой вызывается этот конвертер. Этот параметр или свойства этого параметра
  ///  могут иметь значение пустая ссылка</param>
  /// <returns>TypeConverter.StandardValuesCollection, содержащий стандартный
  /// набор допустимых значений, или пустая ссылка, если этот тип данных не поддерживает
  /// стандартный набор значений</returns>
  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    return new TypeConverter.StandardValuesCollection((ICollection) new bool[2]
    {
      false,
      true
    });
  }

  /// <summary>Возвращает значение, показывающее, является ли исчерпывающим списком возможных
  /// значений коллекция стандартных значений, возвращаемая методом GetStandardValues,
  /// используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <returns>true, если объект TypeConverter.StandardValuesCollection, возвращенный
  /// методом GetStandardValues, является исчерпывающим списком возможных значений,
  /// false, если возможны другие значения</returns>
  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;
}
