// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AdditionalAttributesConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер класса AdditionalAttributeCollection</summary>
public class AdditionalAttributesConverter : ExpandableObjectConverter
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
    return context.Instance != null && (context.Instance is DocumentTreeNode || context.Instance is AdditionalAttributeCollection);
  }

  /// <summary>Получить атрибуты дочерних ячеек</summary>
  private static bool GetAdditionalAttributesNames(
    DocumentTreeNode node,
    ref StringCollection cur_var)
  {
    if (node is RectangleElement rectangleElement && !rectangleElement.IsSingleCell)
    {
      int index = 0;
      for (int count = rectangleElement.Nodes.Count; index < count; ++index)
      {
        if (!AdditionalAttributesConverter.GetAdditionalAttributesNames(rectangleElement.Nodes[index], ref cur_var))
          return false;
      }
    }
    else
    {
      StringCollection attributeNames = node.GetAttributeNames(true);
      if (cur_var == null)
      {
        cur_var = node.GetAttributeNames(true);
        return true;
      }
      for (int index = cur_var.Count - 1; index >= 0; --index)
      {
        if (!attributeNames.Contains(cur_var[index]))
        {
          cur_var.Remove(cur_var[index]);
          if (cur_var.Count == 0)
            return false;
        }
      }
    }
    return true;
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
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    if (!(context.Instance is DocumentTreeNode node) && context.Instance is AdditionalAttributeCollection instance)
      node = instance.Owner;
    if (node != null)
    {
      StringCollection cur_var = (StringCollection) null;
      if (node.IsVirtualNode)
        AdditionalAttributesConverter.GetAdditionalAttributesNames(node, ref cur_var);
      else
        cur_var = node.GetAttributeNames(true);
      AdditionalAttributeCollection additionalAttributes = node.GetAdditionalAttributes();
      if (additionalAttributes != null)
      {
        IDictionary attributes1 = additionalAttributes.Attributes;
        foreach (string str in cur_var)
        {
          if (!attributes1.Contains((object) str) || attributes1[(object) str] is AddAttrValue addAttrValue && addAttrValue.IsShownInPropertyGrid || attributes1[(object) str] is string)
            propertyDescriptorList.Add((PropertyDescriptor) new AttributeDescriptor(str));
        }
      }
    }
    PropertyDescriptorCollection properties = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
    if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly)
      CustomPropertyDescriptor.SetReadOnlyProperties(properties);
    return properties;
  }
}
