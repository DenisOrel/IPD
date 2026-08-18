// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RelationListConverter
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.AVS;

/// <summary>Конвертер для списка связей. Только для отладки</summary>
[Serializable]
public class RelationListConverter : ExpandableObjectConverter
{
  /// <summary>Возвращает значение, показывающее, может ли этот конвертер преобразовать данный объект в заданный тип, используя заданную контекстную информацию</summary>
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
    return context.Instance != null && (context.Instance is AVSRow || context.Instance is List<RelationAttributeValuesCache>);
  }

  /// <summary>Возвращает коллекцию свойств для типа массива, заданного параметром, используя
  /// заданную контекстную информацию и атрибуты</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="value">Объект Object, задающий тип массива, для которого нужно получить свойства</param>
  /// <param name="attributes">Массив типа Attribute, используемый как фильтр</param>
  /// <returns>PropertyDescriptorCollection со свойствами, доступными для этого типа данных, или пустая ссылка, если свойства не доступны</returns>
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    if (!(value is List<RelationAttributeValuesCache> attributeValuesCacheList))
      return base.GetProperties(context, value, attributes);
    PropertyDescriptor[] properties = new PropertyDescriptor[attributeValuesCacheList.Count];
    for (int index = 0; index < attributeValuesCacheList.Count; ++index)
      properties[index] = (PropertyDescriptor) new RelationAttributeValuesCacheDescriptor(attributeValuesCacheList[index], $"[{index}]");
    return new PropertyDescriptorCollection(properties);
  }
}
