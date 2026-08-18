// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IConditionDataProvider
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Провайдер данных для условия выборки с атрибутом.</summary>
public interface IConditionDataProvider
{
  /// <summary>Функция возвращает тип атрибута по его идентификатору</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns></returns>
  FieldTypes GetFieldType(object attributeID);

  /// <summary>
  /// Получить список допустимых атрибутов для условия выборки
  /// </summary>
  /// <param name="sourceType">Принадлежность атрибута</param>
  /// <returns>Идентификатор выбранного атрибута или Intermech.Consts.UnknownAttributeId</returns>
  List<ConditionAttributeInfo> GetListAttributes(
    AttributeSourceTypes sourceType,
    int[] objectTypeIDs);

  /// <summary>Возможен ли выбор любого атрибута для текущей выборки</summary>
  /// <param name="sourceType"></param>
  /// <returns></returns>
  bool AnyAttributes(AttributeSourceTypes sourceType, int[] objectTypeIDs);

  /// <summary>Получить имя атрибута по его идентификатору</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns></returns>
  string GetAttributeName(object attributeID);

  MultiValueModes GetAttributeMultiValueMode(object attributeID);

  /// <summary>
  /// Получить допустимые значения атрибута по его идентификатору
  /// </summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns></returns>
  Dictionary<object, string> GetPossibleValues(object attributeID);

  /// <summary>Получить глобальный идентификатор атрибута</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns></returns>
  Guid GetAttributeGuid(object attributeID);

  /// <summary>Получить идентификатор атрибута</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <returns></returns>
  int GetAttributeID(object attributeID);

  /// <summary>Получить строковое представление значения</summary>
  /// <param name="selParType"></param>
  /// <param name="objValue"></param>
  /// <param name="possibleValues"></param>
  /// <param name="tag">доп параметры</param>
  /// <returns></returns>
  string ConvertToString(
    object attributeID,
    RelationalOperators relationalOperator,
    SelectionParameterTypes selParType,
    object objValue,
    Dictionary<object, string> possibleValues,
    object tag);

  /// <summary>Выбрать тип объектов посредством диалога</summary>
  /// <param name="objectType"></param>
  /// <param name="selectionType"></param>
  /// <returns></returns>
  bool ChoiseObjectType(ref object objectType, SelectionType selectionType);

  /// <summary>Выбрать тип связей</summary>
  /// <param name="relationType"></param>
  /// <returns></returns>
  bool ChoiseRelationType(ref object relationType);

  /// <summary>Список разрешенных типов данных для провайдера</summary>
  List<SelectionParameterTypes> EnabledParameterTypes { get; }

  /// <summary>Получить заголовок объекта</summary>
  /// <param name="value"></param>
  /// <returns></returns>
  string GetObjectCaption(object value);

  /// <summary>Идентификатор типа пользователей</summary>
  int UserTypeID { get; }

  /// <summary>
  /// Список идентификаторов типов объектов, в которых сгруппированы объекты-пользователи,
  /// например Группы пользователей, Подразделения..
  /// </summary>
  int[] UserGroupTypeIDs { get; }

  /// <summary>Получить название типа объектов</summary>
  /// <param name="objectType"></param>
  /// <returns></returns>
  string GetObjectTypeCaption(object value);

  /// <summary>Получить название типа связей</summary>
  /// <param name="value"></param>
  /// <returns></returns>
  string GetRelationTypeCaption(object value);

  /// <summary>Сгенерировать описание условия</summary>
  /// <param name="conditionStructure">Структура, описывающая условие поиска объектов в базе</param>
  /// <returns></returns>
  string GenerateConditionCaption(
    ConditionStructure conditionStructure,
    string value1,
    string value2);

  /// <summary>Получить атрибуты для типов объектов</summary>
  /// <param name="objTypes"></param>
  /// <returns></returns>
  List<ConditionAttributeInfo> GetAttributesForObjectTypes(int[] objTypes);

  /// <summary>Получить обязательные атрибуты</summary>
  /// <param name="sourceType"></param>
  /// <returns></returns>
  List<ConditionAttributeInfo> GetObligatoryAttributes(AttributeSourceTypes sourceType);

  /// <summary>Отобразить диалог для выбора</summary>
  /// <param name="value"></param>
  /// <param name="type"></param>
  /// <param name="addInfo">Доп инфо, для ссылок тип объекта</param>
  /// <param name="attrID"> Идентификатор атрибута, для редактирования значения которого открывается диалог</param>
  /// <param name="selection4Types">Типы объектов, для которых создана выборка</param>
  /// <returns></returns>
  bool SelectDialog(
    ref object value,
    SelectionParameterTypes type,
    object addInfo,
    int attrID,
    int[] selection4Types);

  /// <summary>
  /// Получить идентификатор типа объектов для атрибута-ссылки
  /// </summary>
  /// <param name="attributeID"></param>
  /// <returns></returns>
  int GetObjectType4ObjectLink(int attributeID);

  int GetObjectTypeID(Guid objectTypeGuid);

  bool IsUserObjectID(long objectID);

  string GetUserCaption(object userID);

  string GetSubjectAreaCaption(object value);

  string GetLifecycleStepCaption(object value);

  string GetLifecycleLevelCaption(object value);

  void GetDateAttributeFormat(
    int attributeID,
    int[] objectTypeIDs,
    out DateTimePickerFormat format,
    out string formatString);

  RelationalOperators[] GetEnableRelationalOperators(FieldTypes fieldType, int attributeID);
}
