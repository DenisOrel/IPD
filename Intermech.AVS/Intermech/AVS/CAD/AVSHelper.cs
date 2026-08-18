// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.CAD.AVSHelper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;

#nullable disable
namespace Intermech.AVS.CAD;

/// <summary>
/// Содержит методы для типизированного чтения значений атрибутов из записей спецификации. Используется интеграторами с CAD-системами.
/// </summary>
public static class AVSHelper
{
  /// <summary>Читает и возвращает копию значения атрибута.</summary>
  /// <typeparam name="T">Тип значения атрибута</typeparam>
  /// <param name="attributesCache">Кэш значений атрибутов из записи спецификации</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="defaultValue">Значение по умолчанию, которое используется при отсутствии указанного атрибута</param>
  /// <returns>Прочитанное значение атрибута</returns>
  public static T CloneFieldValue<T>(
    AttributeValuesCache attributesCache,
    int attributeId,
    T defaultValue)
    where T : class, ICloneable
  {
    T fieldValue = AVSHelper.GetFieldValue<T>(attributesCache, attributeId, defaultValue);
    return (object) fieldValue == null ? fieldValue : (T) fieldValue.Clone();
  }

  /// <summary>Читает и возвращает значение атрибута.</summary>
  /// <typeparam name="T">Тип значения атрибута</typeparam>
  /// <param name="attributesCache">Кэш значений атрибутов из записи спецификации</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="defaultValue">Значение по умолчанию, которое используется при отсутствии указанного атрибута</param>
  /// <returns>Прочитанное значение атрибута</returns>
  public static T GetFieldValue<T>(
    AttributeValuesCache attributesCache,
    int attributeId,
    T defaultValue)
  {
    object attrValue = attributesCache != null ? attributesCache.GetValue(attributeId, false) : throw new ArgumentNullException(nameof (attributesCache));
    if (attrValue == null || attrValue == DBNull.Value)
      return defaultValue;
    if (attrValue is T fieldValue)
      return fieldValue;
    if (attrValue is AVSObjectInfo)
      return (T) AVSHelper.GetObjectValue((AVSObjectInfo) attrValue, typeof (T));
    if (typeof (T) == typeof (Guid))
      return (T) AVSHelper.GetGuidValue(attrValue);
    return !(attrValue is IConvertible) ? (T) attrValue : (T) Convert.ChangeType(attrValue, typeof (T));
  }

  private static object GetObjectValue(AVSObjectInfo attrValue, Type returnType)
  {
    if (returnType == typeof (string))
      return (object) attrValue.Text;
    if (returnType == typeof (long))
      return (object) attrValue.Id;
    throw new InvalidCastException($"Не удалось преобразовать значение '{attrValue}' типа '{attrValue.GetType()}' к типу '{returnType}'.");
  }

  private static object GetGuidValue(object attrValue)
  {
    switch (attrValue)
    {
      case string _:
        return (object) new Guid((string) attrValue);
      case byte[] _:
        return (object) new Guid((byte[]) attrValue);
      default:
        throw new InvalidCastException($"Не удалось преобразовать значение '{attrValue}' типа '{attrValue.GetType()}' к типу '{typeof (Guid)}'.");
    }
  }
}
