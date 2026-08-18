// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DefaultCharFormatConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа CharFormat для настроек по умолчанию</summary>
public class DefaultCharFormatConverter : LocalizedExpandableObjectConverter
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

  /// <summary>Возвращает значение, показывающее, требуется ли при изменении значения
  /// этого объекта вызывать CreateInstance, чтобы создать новое значение, используя
  /// заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
  /// о формате</param>
  /// <returns>true, если при изменении значения этого объекта требуется вызывать CreateInstance,
  /// чтобы создать новое значение, false, если нет</returns>
  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

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
    if (propertyValues.Count != 4)
      LogManager.AddLine("ImDoc.DefaultCharFormatConverter. The number of properties has been changed!");
    return (object) instance;
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    if (properties != null)
    {
      PropertyDescriptor PropDesc = properties.Find("FontFamily", false);
      if (PropDesc != null && DocumentTreeNode.OverridePropertyAttributes[(object) "FontFamily"] is PropertyAttributeWrapper propertyAttribute)
      {
        if (!(PropDesc is CustomPropertyDescriptor propertyDescriptor))
          propertyDescriptor = new CustomPropertyDescriptor(PropDesc);
        for (int index = 0; index < propertyAttribute.AttributesForTypes.Count; ++index)
          propertyDescriptor.AddAttribute(((PropertyAttributeForType) propertyAttribute.AttributesForTypes[index]).Attribute);
        int index1 = properties.IndexOf(PropDesc);
        if (index1 != -1)
        {
          properties.RemoveAt(index1);
          properties.Insert(index1, (PropertyDescriptor) propertyDescriptor);
        }
        else
          properties.Add((PropertyDescriptor) propertyDescriptor);
      }
      PropertyDescriptor propertyDescriptor1;
      if ((propertyDescriptor1 = properties.Find("TextColorForUser", false)) != null)
        properties.Remove(propertyDescriptor1);
      PropertyDescriptor propertyDescriptor2;
      if ((propertyDescriptor2 = properties.Find("TextBkColorForUser", false)) != null)
        properties.Remove(propertyDescriptor2);
      PropertyDescriptor propertyDescriptor3;
      if ((propertyDescriptor3 = properties.Find("Underline", false)) != null)
        properties.Remove(propertyDescriptor3);
      PropertyDescriptor propertyDescriptor4;
      if ((propertyDescriptor4 = properties.Find("UnderlineColor", false)) != null)
        properties.Remove(propertyDescriptor4);
      PropertyDescriptor propertyDescriptor5;
      if ((propertyDescriptor5 = properties.Find("Strike", false)) != null)
        properties.Remove(propertyDescriptor5);
      if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly)
        CustomPropertyDescriptor.SetReadOnlyProperties(properties);
    }
    return properties;
  }
}
