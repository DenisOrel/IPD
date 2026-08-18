// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.RowColumnParamsConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер для типа RowColumnParams</summary>
public class RowColumnParamsConverter : LocalizedExpandableObjectConverter
{
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
    PropertyDescriptorCollection properties1 = base.GetProperties(context, value, attributes);
    PropertyDescriptor[] properties2 = new PropertyDescriptor[properties1.Count];
    RowColParams rowColParams = value as RowColParams;
    for (int index = 0; index < properties1.Count; ++index)
    {
      if (rowColParams != null && properties1[index].Name == "BorderLine1")
      {
        CustomPropertyDescriptor propertyDescriptor;
        properties2[index] = (PropertyDescriptor) (propertyDescriptor = new CustomPropertyDescriptor(properties1[index]));
        if (rowColParams.IsColumn)
        {
          propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_139")));
          propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_140")));
        }
        else
        {
          propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_141")));
          propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_142")));
        }
      }
      else if (rowColParams != null && properties1[index].Name == "BorderLine2")
      {
        CustomPropertyDescriptor propertyDescriptor;
        properties2[index] = (PropertyDescriptor) (propertyDescriptor = new CustomPropertyDescriptor(properties1[index]));
        if (rowColParams.IsColumn)
        {
          propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_143")));
          propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_144")));
        }
        else
        {
          propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_145")));
          propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_146")));
        }
      }
      else
        properties2[index] = properties1[index];
    }
    PropertyDescriptorCollection properties3 = new PropertyDescriptorCollection(properties2);
    if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly)
      CustomPropertyDescriptor.SetReadOnlyProperties(properties3);
    return properties3;
  }
}
