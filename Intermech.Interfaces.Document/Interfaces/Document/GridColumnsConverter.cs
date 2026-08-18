// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.GridColumnsConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер класса для массива столбцов</summary>
public class GridColumnsConverter : LocalizedExpandableObjectConverter
{
  /// <summary>Возвращает значение, показывающее, может ли этот конвертер преобразовать данный объект
  /// в заданный тип, используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="destinationType">Type, представляющий тип, в который требуется сделать преобразование</param>
  /// <returns>true, если конвертер может осуществить такое преобразование, false, если нет</returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return !(destinationType == typeof (string)) && base.CanConvertTo(context, destinationType);
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
    return destinationType == typeof (string) ? (object) "" : base.ConvertTo(context, culture, value, destinationType);
  }

  /// <summary>Поддерживает ли класс получение свойств GetProperties()</summary>
  /// <param name="context">Контекст дескриптора</param>
  /// <returns>true, если класс получение свойств GetProperties()</returns>
  public override bool GetPropertiesSupported(ITypeDescriptorContext context)
  {
    return context.Instance != null && (context.Instance is TableData || context.Instance is List<RowColParams>);
  }

  /// <summary>Получить дескрипторы свойств</summary>
  /// <param name="context">Контекст</param>
  /// <param name="value">Объект</param>
  /// <param name="attributes">Атрибуты свойств</param>
  /// <returns>Коллекция дескрипторов свойств объекта</returns>
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    TableData instance = context.Instance as TableData;
    List<RowColParams> rowColParamsList = instance != null ? instance.GridColumnsParams : context.Instance as List<RowColParams>;
    PropertyDescriptor[] properties1;
    if (rowColParamsList != null)
    {
      properties1 = new PropertyDescriptor[rowColParamsList.Count];
      string displayName = (string) null;
      for (int index = 0; index < rowColParamsList.Count; ++index)
      {
        if (rowColParamsList[index] != null)
          displayName = $"[{index.ToString((IFormatProvider) CultureInfo.InvariantCulture)}] {rowColParamsList[index].ColRowName}";
        if (displayName == null || displayName == "")
          displayName = $"[{index.ToString((IFormatProvider) CultureInfo.InvariantCulture)}]";
        properties1[index] = (PropertyDescriptor) new GridColumnDescriptor(displayName, index, new Attribute[0]);
      }
    }
    else
      properties1 = new PropertyDescriptor[0];
    PropertyDescriptorCollection properties2 = new PropertyDescriptorCollection(properties1);
    if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly)
      CustomPropertyDescriptor.SetReadOnlyProperties(properties2);
    return properties2;
  }
}
