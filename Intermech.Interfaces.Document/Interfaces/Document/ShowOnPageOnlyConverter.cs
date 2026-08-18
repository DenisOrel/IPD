// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ShowOnPageOnlyConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа ShowOnPageOnlyPropertyWrapper</summary>
public class ShowOnPageOnlyConverter : LocalizedExpandableObjectConverter
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
    if (!(destinationType == typeof (string)) || !(value is ShowOnPageOnlyPropertyWrapper onlyPropertyWrapper))
      return base.ConvertTo(context, culture, value, destinationType);
    if (onlyPropertyWrapper.ShowOnAllPages)
      return (object) EnumCustomConverter.GetEnumDescription((Enum) onlyPropertyWrapper.OwnerNode.ShowOnPageOnly);
    List<string> values = new List<string>();
    if (onlyPropertyWrapper.FirstDataPage)
      values.Add(EnumCustomConverter.GetEnumDescription((Enum) ShowOnPageOnly.FirstDataPage));
    if (onlyPropertyWrapper.NextDataPage)
      values.Add(EnumCustomConverter.GetEnumDescription((Enum) ShowOnPageOnly.NextDataPage));
    if (onlyPropertyWrapper.LastDataPage)
      values.Add(EnumCustomConverter.GetEnumDescription((Enum) ShowOnPageOnly.LastDataPage));
    return values.Count == 0 ? (object) EnumCustomConverter.GetEnumDescription((Enum) ShowOnPageOnly.None) : (object) string.Join(", ", (IEnumerable<string>) values);
  }

  /// <summary>Возвращает значение, показывающее, может ли этот конвертер преобразовать
  /// объект данного типа в тип этого конвертера, используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="sourceType">Type, представляющий тип, из которого требуется сделать преобразование</param>
  /// <returns>true, если конвертер может осуществить такое преобразование, false, если нет</returns>
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return !(sourceType == typeof (string)) && base.CanConvertFrom(context, sourceType);
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    bool flag = !(value is ShowOnPageOnlyPropertyWrapper onlyPropertyWrapper) || onlyPropertyWrapper.IsReadOnly;
    foreach (PropertyDescriptor propertyDescriptor1 in properties)
    {
      if (propertyDescriptor1 is CustomPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.SetIsReadOnly(flag);
    }
    return properties.Sort(ShowOnPageOnlyPropertyWrapper.FieldsOrder);
  }
}
