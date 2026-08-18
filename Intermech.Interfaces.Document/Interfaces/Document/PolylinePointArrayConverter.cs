// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PolylinePointArrayConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер для массива точек полилинии</summary>
[Serializable]
public class PolylinePointArrayConverter : TypeConverter
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
    return context.Instance != null && (context.Instance is PolylineData || context.Instance is PointF[]);
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
    if (!(context.Instance is PolylineData instance) || !(value is PointF[] pointFArray))
      return base.GetProperties(context, value, attributes);
    PropertyDescriptor[] properties1 = new PropertyDescriptor[pointFArray.Length];
    for (int pointIndex = 0; pointIndex < pointFArray.Length; ++pointIndex)
      properties1[pointIndex] = (PropertyDescriptor) new PolylinePointDescriptor(instance, pointIndex);
    PropertyDescriptorCollection properties2 = new PropertyDescriptorCollection(properties1);
    if (context.PropertyDescriptor != null && context.PropertyDescriptor.IsReadOnly)
      CustomPropertyDescriptor.SetReadOnlyProperties(properties2);
    return properties2;
  }
}
