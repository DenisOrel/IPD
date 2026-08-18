// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.SizeFConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа SizeF</summary>
public class SizeFConverter : TypeConverter
{
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
    if (!(value is string str1))
      return base.ConvertFrom(context, culture, value);
    string str2 = str1.Trim();
    if (str2.Length == 0)
      return (object) null;
    if (culture == null)
      culture = CultureInfo.CurrentCulture;
    string listSeparator = culture.TextInfo.ListSeparator;
    string[] strArray = str2.Split(';');
    if (strArray.Length != 2)
      throw new ArgumentException("Text Parse Failed Format: " + value);
    if (FloatConverter.CorrectDecimalSeparator)
    {
      for (int index = 0; index < 2; ++index)
      {
        if (strArray[index] != null)
        {
          if (culture.NumberFormat.NumberDecimalSeparator != ",")
            strArray[index] = strArray[index].Replace(",", culture.NumberFormat.NumberDecimalSeparator);
          if (culture.NumberFormat.NumberDecimalSeparator != ".")
            strArray[index] = strArray[index].Replace(".", culture.NumberFormat.NumberDecimalSeparator);
        }
      }
    }
    return (object) new SizeF(float.Parse(strArray[0], (IFormatProvider) culture), float.Parse(strArray[1], (IFormatProvider) culture));
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
    if (!(destinationType == typeof (string)) || !(value is SizeF sizeF))
      return base.ConvertTo(context, culture, value, destinationType);
    string[] strArray = new string[2];
    TypeConverter converter = TypeDescriptor.GetConverter(typeof (float));
    strArray[0] = converter.ConvertToString(context, culture, (object) sizeF.Width);
    strArray[1] = converter.ConvertToString(context, culture, (object) sizeF.Height);
    return (object) string.Join("; ", strArray);
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
    return (object) new SizeF((float) propertyValues[(object) "Width"], (float) propertyValues[(object) "Height"]);
  }

  /// <summary>Возвращает значение, показывающее, требуется ли при изменении значения
  /// этого объекта вызывать CreateInstance, чтобы создать новое значение, используя
  /// заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
  /// о формате</param>
  /// <returns>true, если при изменении значения этого объекта требуется вызывать CreateInstance,
  /// чтобы создать новое значение, false, если нет</returns>
  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

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
    PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(typeof (SizeF), attributes);
    PropertyDescriptorCollection properties2 = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    CustomPropertyDescriptor propertyDescriptor1 = new CustomPropertyDescriptor(properties1["Width"]);
    propertyDescriptor1.AddAttribute((Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_147")));
    propertyDescriptor1.AddAttribute((Attribute) new TypeConverterAttribute(typeof (FloatConverter)));
    properties2.Add((PropertyDescriptor) propertyDescriptor1);
    CustomPropertyDescriptor propertyDescriptor2 = new CustomPropertyDescriptor(properties1["Height"]);
    propertyDescriptor2.AddAttribute((Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("Interfaces.Document_148")));
    propertyDescriptor2.AddAttribute((Attribute) new TypeConverterAttribute(typeof (FloatConverter)));
    properties2.Add((PropertyDescriptor) propertyDescriptor2);
    if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly)
      CustomPropertyDescriptor.SetReadOnlyProperties(properties2);
    return properties2.Sort(new string[2]
    {
      "Width",
      "Height"
    });
  }

  /// <summary>Поддерживает ли класс получение свойств GetProperties()</summary>
  /// <param name="context">Контекст дескриптора</param>
  /// <returns>true, если класс получение свойств GetProperties()</returns>
  public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
}
