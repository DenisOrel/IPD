// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BorderLineConverterTE
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Interfaces.Document;

public class BorderLineConverterTE : LocalizedExpandableObjectConverter
{
  /// <summary>Имеют ли элементы шаблон</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private bool HasTemplate(ITypeDescriptorContext context)
  {
    if (context == null || context.Instance == null)
      return false;
    if (context.Instance is RectangleElement)
    {
      RectangleElement instance = context.Instance as RectangleElement;
      if (instance.IsVirtualNode)
      {
        List<DocumentTreeNode> singleCells = instance.GetSingleCells();
        for (int index = 0; index < singleCells.Count; ++index)
        {
          if (singleCells[index].TemplateId != null)
            return true;
        }
      }
      else if (instance.TemplateId != null)
        return true;
    }
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
    if (!(destinationType == typeof (string)) || !(value is BorderLineTE))
      return base.ConvertTo(context, culture, value, destinationType);
    BorderLineTE borderLineTe = value as BorderLineTE;
    string str1 = "";
    if (borderLineTe.StyleTE.HasValue)
    {
      TypeConverter converter = TypeDescriptor.GetConverter(borderLineTe.StyleTE.GetType());
      str1 = $"{str1}{converter.ConvertToString((object) borderLineTe.StyleTE)},";
    }
    float? widthTe = borderLineTe.WidthTE;
    int num1 = widthTe.HasValue ? 1 : 0;
    widthTe = borderLineTe.WidthTE;
    float num2 = 0.0f;
    int num3 = !((double) widthTe.GetValueOrDefault() == (double) num2 & widthTe.HasValue) ? 1 : 0;
    if ((num1 & num3) != 0)
    {
      string str2 = str1;
      widthTe = borderLineTe.WidthTE;
      string str3 = widthTe.ToString();
      str1 = str2 + str3;
    }
    if (str1.Length != 0 && str1[str1.Length - 1] == ',')
      str1 = str1.Remove(str1.Length - 1);
    return (object) str1;
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
    BorderLineTE borderLineTe = new BorderLineTE();
    Match match1 = new Regex("(?i)(Color\\s*=\\s*\"*)(\\w+)").Match(input);
    if (match1.Groups.Count < 2)
      throw new ArgumentException("Text Parse Failed Format: " + value);
    borderLineTe.ColorTE = new Color?(Color.FromName(match1.Groups[2].Value));
    Match match2 = new Regex("(?i)(Style\\s*=\\s*\"*)(\\w+)").Match(input);
    if (match2.Groups.Count < 2)
      throw new ArgumentException("Text Parse Failed Format: " + value);
    borderLineTe.StyleTE = new BorderStyles?((BorderStyles) Enum.Parse(typeof (BorderStyles), match2.Groups[2].Value));
    Match match3 = new Regex("(?i)(Width\\s*=\\s*\"*)([\\-\\+\\w\\.\\,]+)").Match(input);
    if (match3.Groups.Count < 2)
      throw new ArgumentException("Text Parse Failed Format: " + value);
    borderLineTe.WidthTE = new float?(float.Parse(match3.Groups[2].Value, (IFormatProvider) culture));
    return (object) borderLineTe;
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
    return (object) new BorderLineTE((Color?) propertyValues[(object) "ColorTE"], (BorderStyles?) propertyValues[(object) "StyleTE"], (float?) propertyValues[(object) "WidthTE"], (float?) propertyValues[(object) "SerifWidthTE"]);
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

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    if (properties != null)
    {
      PropertyDescriptor PropDesc1 = properties.Find("StyleTE", false);
      if (PropDesc1 != null && DocumentTreeNode.OverridePropertyAttributes[(object) "StyleTE"] is PropertyAttributeWrapper propertyAttribute1)
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
      PropertyDescriptor PropDesc2 = properties.Find("ColorTE", false);
      if (PropDesc2 != null && DocumentTreeNode.OverridePropertyAttributes[(object) "ColorTE"] is PropertyAttributeWrapper propertyAttribute2)
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
      if (value is BorderLineTE borderLineTe)
      {
        BorderStyles? styleTe = borderLineTe.StyleTE;
        BorderStyles borderStyles = BorderStyles.Serif;
        if (!(styleTe.GetValueOrDefault() == borderStyles & styleTe.HasValue))
        {
          PropertyDescriptor propertyDescriptor = properties.Find("SerifWidthTE", false);
          if (propertyDescriptor != null)
            properties.RemoveAt(properties.IndexOf(propertyDescriptor));
        }
      }
    }
    if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly || this.HasTemplate(context))
      CustomPropertyDescriptor.SetReadOnlyProperties(properties);
    return properties;
  }
}
