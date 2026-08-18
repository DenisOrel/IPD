// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesDocumentsDescriber
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Класс для регистрации редактора атрибутов с типом выбираемого объекта Документы или их потомки
/// </summary>
internal class ArchivesDocumentsDescriber : IAttributePropertyDescriber
{
  /// <summary>
  /// Получить ссылку на тип класса, в котором хранятся значения атрибута.
  /// </summary>
  /// <param name="attributeId">Идентификатор типа атрибута</param>
  /// <param name="baseType">Тип атрибута в базе</param>
  /// <returns>Ссылка на тип класса</returns>
  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (ObjectPropertyClass);
  }

  /// <summary>Вернуть редактор для указанного атрибута.</summary>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <returns>Редактор для указанного атрибута</returns>
  public object GetPropDescriptorEditor(int attributeId)
  {
    return (object) new DocumentTypedAttributeEditor(attributeId);
  }

  /// <summary>Получить преобразователь типов.</summary>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <returns>Преобразователь типов или null</returns>
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  /// <summary>Возможность редактирования атрибута.</summary>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="baseReadonly">Базовое значение</param>
  /// <returns>Новое значение</returns>
  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  /// <summary>Выполнить сброс значений.</summary>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="baseReset">Базовое значение</param>
  /// <returns>Новое значение</returns>
  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="baseMask"></param>
  /// <returns></returns>
  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  /// <summary>Вернуть класс со значением атрибута.</summary>
  /// <param name="elementInfo">Описание объекта/связи, которому принадлежит атрибут</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="actualValue">Значение атрибута</param>
  /// <returns>Класс со значением атрибута</returns>
  public object GetPropDescriptorValue(
    IElementInfo elementInfo,
    int attributeId,
    object actualValue)
  {
    switch (actualValue)
    {
      case long _:
      case int _:
        return (object) new ObjectPropertyClass(Convert.ToInt64(actualValue));
      default:
        return (object) null;
    }
  }

  /// <summary>Получить реальное значение атрибута.</summary>
  /// <param name="elementInfo">Кому принадлежит атрибут</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="propertyValue">Значение атрибута из грида</param>
  /// <returns>Реальное значение атрибута</returns>
  public object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue)
  {
    return propertyValue is ObjectPropertyClass ? (object) (propertyValue as ObjectPropertyClass).ObjectID : (object) null;
  }

  /// <summary>Вернуть ссылку на тип преобразователя.</summary>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="attributeProcessor">Обработчик атрибута</param>
  /// <returns>Ссылка на тип преобразователя</returns>
  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
