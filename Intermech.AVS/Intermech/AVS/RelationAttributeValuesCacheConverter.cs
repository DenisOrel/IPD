// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RelationAttributeValuesCacheConverter
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.AVS;

/// <summary>Конвертер типа RelationAttributeValuesCache</summary>
public class RelationAttributeValuesCacheConverter : TypeConverter
{
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
    return destinationType == typeof (string) && value is RelationAttributeValuesCache ? (object) Convert.ToString(value) : base.ConvertTo(context, culture, value, destinationType);
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
    return TypeDescriptor.GetProperties(typeof (RelationAttributeValuesCache), attributes);
  }

  /// <summary>Поддерживает ли класс получение свойств GetProperties()</summary>
  /// <param name="context">Контекст дескриптора</param>
  /// <returns>true, если класс получение свойств GetProperties()</returns>
  public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
}
