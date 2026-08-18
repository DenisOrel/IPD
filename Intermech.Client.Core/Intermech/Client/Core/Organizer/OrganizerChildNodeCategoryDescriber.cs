
// Type: Intermech.Client.Core.Organizer.OrganizerChildNodeCategoryDescriber
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Класс для регистрации редактора для атрибута "Категории подузла органайзера"
/// </summary>
public class OrganizerChildNodeCategoryDescriber : IAttributePropertyDescriber
{
  /// <summary>Получить преобразователь типов.</summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <returns>Преобразователь типов или null</returns>
  public TypeConverter GetPropDescriptorConverter(int attrID) => (TypeConverter) null;

  /// <summary>Возможность редактирования атрибута.</summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="baseReadonly">Базовое значение</param>
  /// <returns>Новое значение</returns>
  public bool GetPropDescriptorReadonly(int attrID, bool baseReadonly) => baseReadonly;

  /// <summary>Вернуть редактор для указанного атрибута.</summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <returns>Редактор для указанного атрибута</returns>
  public object GetPropDescriptorEditor(int attrID)
  {
    return (object) new OrganizerChildNodeCategoryEditor();
  }

  /// <summary>Получить реальное значение атрибута.</summary>
  /// <param name="iElementInfo">Кому принадлежит атрибут</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="propertyValue">Значение атрибута из грида</param>
  /// <returns>Реальное значение атрибута</returns>
  public object GetAttributeValue(IElementInfo iElementInfo, int attrID, object propertyValue)
  {
    object attributeValue = (object) null;
    if (propertyValue != null && propertyValue is OrganizerChildNodeCategoryProxy)
      attributeValue = (object) (propertyValue as OrganizerChildNodeCategoryProxy).ID;
    return attributeValue;
  }

  /// <summary>
  /// Получить ссылку на тип класса, в котором хранятся значения атрибута.
  /// </summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <param name="baseType">Тип атрибута в базе</param>
  /// <returns>Ссылка на тип класса</returns>
  public Type GetPropDescriptorType(int attrID, FieldTypes baseType)
  {
    return typeof (OrganizerChildNodeCategoryProxy);
  }

  /// <summary>Вернуть класс со значением атрибута.</summary>
  /// <param name="iElementInfo">Описание объекта/связи, которому принадлежит атрибут</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="actualValue">Значение атрибута</param>
  /// <returns>Класс со значением атрибута</returns>
  public object GetPropDescriptorValue(IElementInfo iElementInfo, int attrID, object actualValue)
  {
    object propDescriptorValue;
    if (actualValue != DBNull.Value && actualValue != null)
    {
      int result = -1;
      propDescriptorValue = int.TryParse(actualValue.ToString(), out result) ? (object) new OrganizerChildNodeCategoryProxy(result) : (object) new OrganizerChildNodeCategoryProxy(-1);
    }
    else
      propDescriptorValue = (object) new OrganizerChildNodeCategoryProxy(-1);
    return propDescriptorValue;
  }

  /// <summary>Выполнить сброс значений.</summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="baseReset">Базовое значение</param>
  /// <returns>Новое значение</returns>
  public bool GetPropDescriptorReset(int attrID, bool baseReset) => true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrID"></param>
  /// <param name="baseMask"></param>
  /// <returns></returns>
  public string GetPropDescriptorMask(int attrID, string baseMask) => baseMask;

  /// <summary>Вернуть ссылку на тип преобразователя.</summary>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="attrProcessor">Обработчик атрибута</param>
  /// <returns>Ссылка на тип преобразователя</returns>
  public TypeConverter GetConverter(int attrID, object attrProcessor) => (TypeConverter) null;
}
