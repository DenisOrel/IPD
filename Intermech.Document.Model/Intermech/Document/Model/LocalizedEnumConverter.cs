// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.LocalizedEnumConverter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Локализованный конвертер Enum</summary>
public class LocalizedEnumConverter : StringConverter
{
  /// <summary>Тип Enum</summary>
  protected Type EnumType;
  /// <summary>Список значений</summary>
  protected string[] StringValues;

  /// <summary>Возвращает значение, показывающее, может ли этот конвертер преобразовать данный объект в заданный тип, используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="destinationType">Type, представляющий тип, в который требуется сделать преобразование</param>
  /// <returns>true, если конвертер может осуществить такое преобразование, false, если нет</returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == this.EnumType || base.CanConvertTo(context, destinationType);
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
    if (!(destinationType == typeof (string)) || !(value.GetType() == this.EnumType))
      return base.ConvertTo(context, culture, value, destinationType);
    int int32 = ((IConvertible) value).ToInt32((IFormatProvider) null);
    return ((TypeConverter.StandardValuesCollection) this.GetStandardValues())[int32];
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
    if (!(value is string))
      return base.ConvertFrom(context, culture, value);
    try
    {
      int num = 0;
      TypeConverter.StandardValuesCollection standardValues = (TypeConverter.StandardValuesCollection) this.GetStandardValues();
      for (int index = 0; index < standardValues.Count; ++index)
      {
        if (value == standardValues[index])
        {
          num = index;
          break;
        }
      }
      return (object) num;
    }
    catch
    {
      throw new ArgumentException($"Can not convert '{(string) value}' to type Enum");
    }
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
    return new TypeConverter.StandardValuesCollection((ICollection) this.StringValues);
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
