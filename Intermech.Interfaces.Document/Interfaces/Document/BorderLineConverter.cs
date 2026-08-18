// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BorderLineConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа BorderLine</summary>
public class BorderLineConverter : LocalizedExpandableObjectConverter
{
  /// <summary>Возвращает значение, показывающее, может ли этот конвертер преобразовать
  /// объект данного типа в тип этого конвертера, используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="sourceType">Type, представляющий тип, из которого требуется сделать преобразование</param>
  /// <returns>true, если конвертер может осуществить такое преобразование, false, если нет</returns>
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return !(sourceType == typeof (string)) && base.CanConvertFrom(context, sourceType);
  }

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
    if (!(destinationType == typeof (string)) || !(value is BorderLine))
      return base.ConvertTo(context, culture, value, destinationType);
    BorderLine borderLine = value as BorderLine;
    TypeConverter converter = TypeDescriptor.GetConverter(borderLine.Style.GetType());
    return borderLine.Style == BorderStyles.None || (double) borderLine.Width == 0.0 ? (object) converter.ConvertToString((object) borderLine.Style) : (object) $"{converter.ConvertToString((object) borderLine.Style)},{borderLine.Width.ToString()}";
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
    if (!(value is string str))
      return base.ConvertFrom(context, culture, value);
    string input = str.Trim();
    if (input.Length == 0)
      return (object) null;
    if (culture == null)
      culture = CultureInfo.CurrentCulture;
    BorderLine borderLine = new BorderLine();
    Match match1 = new Regex("(?i)(Color\\s*=\\s*\"*)(\\w+)").Match(input);
    if (match1.Groups.Count < 2)
      throw new ArgumentException("Text Parse Failed Format: " + value);
    borderLine.Color = Color.FromName(match1.Groups[2].Value);
    Match match2 = new Regex("(?i)(Style\\s*=\\s*\"*)(\\w+)").Match(input);
    if (match2.Groups.Count < 2)
      throw new ArgumentException("Text Parse Failed Format: " + value);
    borderLine.Style = (BorderStyles) Enum.Parse(typeof (BorderStyles), match2.Groups[2].Value);
    Match match3 = new Regex("(?i)(Width\\s*=\\s*\"*)([\\-\\+\\w\\.\\,]+)").Match(input);
    if (match3.Groups.Count < 2)
      throw new ArgumentException("Text Parse Failed Format: " + value);
    borderLine.Width = float.Parse(match3.Groups[2].Value, (IFormatProvider) culture);
    return (object) borderLine;
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
    return propertyValues[(object) "Width"] == null ? (object) new BorderLine((Color) propertyValues[(object) "Color"], (BorderStyles) propertyValues[(object) "Style"], 0.0f) : (object) new BorderLine((Color) propertyValues[(object) "Color"], (BorderStyles) propertyValues[(object) "Style"], (float) propertyValues[(object) "Width"], (float) ((double) (float?) propertyValues[(object) "SerifWidth"] ?? 0.0));
  }

  /// <summary>Возвращает значение, показывающее, требуется ли при изменении значения
  /// этого объекта вызывать CreateInstance, чтобы создать новое значение, используя
  /// заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
  /// о формате</param>
  /// <returns>true, если при изменении значения этого объекта требуется вызывать CreateInstance,
  /// чтобы создать новое значение, false, если нет</returns>
  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
  {
    return !this.HasTemplate(context);
  }

  /// <summary>Имеют ли элементы шаблон</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private bool HasTemplate(ITypeDescriptorContext context)
  {
    if (context == null || context.Instance == null)
      return false;
    if (context.Instance is RectangleElement && (context.Instance as RectangleElement).TemplateId != null)
      return true;
    if (context.Instance is object[])
    {
      object[] instance = (object[]) context.Instance;
      for (int index = 0; index < instance.Length; ++index)
      {
        if (instance[index] is RectangleElement && (instance[index] as RectangleElement).TemplateId != null)
          return true;
      }
    }
    return false;
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    if (properties != null)
    {
      PropertyDescriptor PropDesc = properties.Find("Style", false);
      if (PropDesc != null && DocumentTreeNode.OverridePropertyAttributes[(object) "Style"] is PropertyAttributeWrapper propertyAttribute)
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
      if (value is BorderLine borderLine && borderLine.Style != BorderStyles.Serif)
      {
        PropertyDescriptor propertyDescriptor = properties.Find("SerifWidth", false);
        if (propertyDescriptor != null)
          properties.RemoveAt(properties.IndexOf(propertyDescriptor));
      }
    }
    if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly || this.HasTemplate(context))
      CustomPropertyDescriptor.SetReadOnlyProperties(properties);
    return properties;
  }
}
