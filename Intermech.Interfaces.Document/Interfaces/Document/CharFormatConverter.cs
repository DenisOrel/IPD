// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CharFormatConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа CharFormat</summary>
public class CharFormatConverter : LocalizedExpandableObjectConverter
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
    if (!(destinationType == typeof (string)) || !(value is CharFormat))
      return base.ConvertTo(context, culture, value, destinationType);
    CharFormat charFormat = value as CharFormat;
    string str1 = "";
    if (charFormat.FontFamily != null)
      str1 = $"{str1}{charFormat.FontFamily.ToString()},";
    float? fontSize = charFormat.FontSize;
    if (fontSize.HasValue)
    {
      string str2 = str1;
      fontSize = charFormat.FontSize;
      string str3 = fontSize.ToString();
      str1 = str2 + str3;
    }
    return (object) str1;
  }

  /// <summary>Создает экземпляр типа, с которым связан этот TypeConverter,
  /// используя заданную контекстную информацию и переданный набор значений свойств
  /// для этого объекта</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную
  /// информацию о формате</param>
  /// <param name="propertyValues">IDictionary новых значений свойства</param>
  /// <returns>Object, представляющий данный IDictionary или пустая ссылка,
  /// если объект не может быть создан</returns>
  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    CharFormat instance = !(context.PropertyDescriptor.GetValue(context.Instance) is CharFormat charFormat) ? new CharFormat() : charFormat.Clone();
    instance.FontFamily = (string) propertyValues[(object) "FontFamily"];
    instance.BoldItalic = (BoldItalicStyle?) propertyValues[(object) "BoldItalic"];
    float? propertyValue1 = (float?) propertyValues[(object) "FontSize"];
    float? propertyValue2 = (float?) propertyValues[(object) "FontSizeMm"];
    float? fontSize = instance.FontSize;
    float? nullable1 = propertyValue1;
    if (!((double) fontSize.GetValueOrDefault() == (double) nullable1.GetValueOrDefault() & fontSize.HasValue == nullable1.HasValue))
    {
      instance.FontSize = propertyValue1;
    }
    else
    {
      float? fontSizeMm = instance.FontSizeMm;
      float? nullable2 = propertyValue2;
      if (!((double) fontSizeMm.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & fontSizeMm.HasValue == nullable2.HasValue))
        instance.FontSizeMm = propertyValue2;
    }
    instance.TextColorForUser = (Color?) propertyValues[(object) "TextColorForUser"];
    instance.TextBkColorForUser = (Color?) propertyValues[(object) "TextBkColorForUser"];
    instance.Underline = (UnderlineStyle?) propertyValues[(object) "Underline"];
    instance.UnderlineColor = (Color?) propertyValues[(object) "UnderlineColor"];
    instance.Strike = (StrikeoutLineStyle?) propertyValues[(object) "Strike"];
    if (propertyValues.Count != 9)
      LogManager.AddLine("CharFormatConverter. The number of properties has been changed!");
    return (object) instance;
  }

  /// <summary>Возвращает значение, показывающее, требуется ли при изменении значения
  /// этого объекта вызывать CreateInstance, чтобы создать новое значение, используя
  /// заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
  /// о формате</param>
  /// <returns>true, если при изменении значения этого объекта требуется вызывать CreateInstance,
  /// чтобы создать новое значение, false, если нет</returns>
  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    PropertyDescriptor PropDesc1 = properties.Find("UnderlineColor", false);
    if (PropDesc1 != null && DocumentTreeNode.OverridePropertyAttributes[(object) "UnderlineColor"] is PropertyAttributeWrapper propertyAttribute1)
    {
      if (!(PropDesc1 is CustomPropertyDescriptor propertyDescriptor))
        propertyDescriptor = new CustomPropertyDescriptor(PropDesc1);
      for (int index = 0; index < propertyAttribute1.AttributesForTypes.Count; ++index)
        propertyDescriptor.AddAttribute(((PropertyAttributeForType) propertyAttribute1.AttributesForTypes[index]).Attribute);
      int index1 = properties.IndexOf(PropDesc1);
      if (index1 != -1)
      {
        properties.RemoveAt(index1);
        properties.Insert(index1, (PropertyDescriptor) propertyDescriptor);
      }
      else
        properties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor PropDesc2 = properties.Find("TextColorForUser", false);
    if (PropDesc2 != null && DocumentTreeNode.OverridePropertyAttributes[(object) "TextColorForUser"] is PropertyAttributeWrapper propertyAttribute2)
    {
      if (!(PropDesc2 is CustomPropertyDescriptor propertyDescriptor))
        propertyDescriptor = new CustomPropertyDescriptor(PropDesc2);
      for (int index = 0; index < propertyAttribute2.AttributesForTypes.Count; ++index)
        propertyDescriptor.AddAttribute(((PropertyAttributeForType) propertyAttribute2.AttributesForTypes[index]).Attribute);
      int index2 = properties.IndexOf(PropDesc2);
      if (index2 != -1)
      {
        properties.RemoveAt(index2);
        properties.Insert(index2, (PropertyDescriptor) propertyDescriptor);
      }
      else
        properties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor PropDesc3 = properties.Find("TextBkColorForUser", false);
    if (PropDesc3 != null && DocumentTreeNode.OverridePropertyAttributes[(object) "TextBkColorForUser"] is PropertyAttributeWrapper propertyAttribute3)
    {
      if (!(PropDesc3 is CustomPropertyDescriptor propertyDescriptor))
        propertyDescriptor = new CustomPropertyDescriptor(PropDesc3);
      for (int index = 0; index < propertyAttribute3.AttributesForTypes.Count; ++index)
        propertyDescriptor.AddAttribute(((PropertyAttributeForType) propertyAttribute3.AttributesForTypes[index]).Attribute);
      int index3 = properties.IndexOf(PropDesc3);
      if (index3 != -1)
      {
        properties.RemoveAt(index3);
        properties.Insert(index3, (PropertyDescriptor) propertyDescriptor);
      }
      else
        properties.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor PropDesc4 = properties.Find("FontFamily", false);
    if (PropDesc4 != null && DocumentTreeNode.OverridePropertyAttributes[(object) "FontFamily"] is PropertyAttributeWrapper propertyAttribute4)
    {
      if (!(PropDesc4 is CustomPropertyDescriptor propertyDescriptor))
        propertyDescriptor = new CustomPropertyDescriptor(PropDesc4);
      for (int index = 0; index < propertyAttribute4.AttributesForTypes.Count; ++index)
        propertyDescriptor.AddAttribute(((PropertyAttributeForType) propertyAttribute4.AttributesForTypes[index]).Attribute);
      int index4 = properties.IndexOf(PropDesc4);
      if (index4 != -1)
      {
        properties.RemoveAt(index4);
        properties.Insert(index4, (PropertyDescriptor) propertyDescriptor);
      }
      else
        properties.Add((PropertyDescriptor) propertyDescriptor);
    }
    if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly)
      CustomPropertyDescriptor.SetReadOnlyProperties(properties);
    return properties;
  }
}
