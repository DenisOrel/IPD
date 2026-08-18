// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IObjectCreatorService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс IObjectCreatorService службы создания объектов позволяет вызвать мастер создания объектов.
/// </summary>
public interface IObjectCreatorService
{
  /// <summary>Создание нового объекта</summary>
  /// <returns>Идентификатор созданного объекта</returns>
  long CreateObjectDialog();

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeID">Идентификатор ипа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(int aObjectTypeID);

  /// <summary>
  /// Создание нового объекта заданного типа.
  /// Функция в обход стандартного механизма получения значения флажка.
  /// Написал по требованию Гинзбурга 21.05.2010
  /// </summary>
  /// <param name="aObjectTypeID">Идентификатор типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="OpenEditor">Открывать редактор после создания</param>
  /// <returns></returns>
  long CreateObjectByTypeDialog(int aObjectTypeID, out OpenEditorMode OpenEditor);

  /// <summary>
  /// Создание нового объекта заданного типа.
  /// Функция в обход стандартного механизма получения значения флажка.
  /// Написал по требованию Гинзбурга 21.05.2010
  /// </summary>
  /// <param name="aObjectTypeID">Идентификатор типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="OpenEditor">Открывать редактор после создания</param>
  /// <param name="creatorParams">Доп. параметры для управления созданием объекта</param>
  /// <returns></returns>
  long CreateObjectByTypeDialog(
    int aObjectTypeID,
    out OpenEditorMode OpenEditor,
    IObjectCreatorParams creatorParams);

  /// <summary>Создание нового объекта заданного типа.</summary>
  /// <param name="aObjectTypeID">Идентификатор типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинают действовать созданные связи</param>
  /// <param name="isVersion">Признак - создавать версию, или объект</param>
  /// <param name="openEditor"></param>
  /// <param name="creatorParams">Доп. параметры для креатора</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(
    int aObjectTypeID,
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate,
    bool isVersion,
    ref OpenEditorMode openEditor,
    IObjectCreatorParams creatorParams);

  /// <summary>
  /// Создание нового объекта заданного типа с возможностью форсировать или заблокировать открытие редактора после создания
  /// </summary>
  /// <param name="aObjectTypeID">Идентификатор типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="openEditor">Если true, редактор будет всегда открываться после создания, если false - никогда</param>
  /// <returns></returns>
  long CreateObjectByTypeDialog(int aObjectTypeID, bool openEditor);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectType">Тип объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(IDBObjectType aObjectType);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeGuid">GUID типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(Guid aObjectTypeGuid);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeName">Наименование типа объекта, по которому будет создан новый экземпляр объекта.
  /// Если aObjectTypeName не задано, то будет предложено выбрать тип объекта в диалоге</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(string aObjectTypeName);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeIDs">Массив идентификаторов типов объектов, из которых
  /// будет разрешено выбрать тип создаваемого экземпляра объекта.</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(int[] aObjectTypeIDs);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeIDs">Массив идентификаторов типов объектов, из которых
  /// будет разрешено выбрать тип создаваемого экземпляра объекта.</param>
  /// <param name="objectTypeID">Тип созданного объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(int[] aObjectTypeIDs, out int objectTypeID);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeIDs">Массив идентификаторов типов объектов, из которых
  /// будет разрешено выбрать тип создаваемого экземпляра объекта.</param>
  /// <param name="selectedID">Тип объектов, выделенный по-умолчанию, или Consts.UnknownObjectTypeId если это неважно</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(int[] aObjectTypeIDs, int selectedID);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypes">Массив типов объектов, из которых
  /// будет разрешено выбрать тип создаваемого экземпляра объекта.</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(IDBObjectType[] aObjectTypes);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeGuids">Массив GUID типов объектов, из которых
  /// будет разрешено выбрать тип создаваемого экземпляра объекта.</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(Guid[] aObjectTypeGuids);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeIDs">Массив ID типов объектов, из которых будет разрешено выбрать тип создаваемого экземпляра объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="selectedTypeID">Тип объектов, выделенный по-умолчанию, или Consts.UnknownObjectTypeId если это неважно</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(
    int[] aObjectTypeIDs,
    ObjectRelationLink[] aObjRelations,
    int selectedTypeID);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeNames">Массив наименований типов объектов, из которых
  /// будет разрешено выбрать тип создаваемого экземпляра объекта.</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(string[] aObjectTypeNames);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeID">Идентификатор ипа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(int aObjectTypeID, ObjectRelationLink[] aObjRelations);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectType">Тип объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(IDBObjectType aObjectType, ObjectRelationLink[] aObjRelations);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeGuid">GUID типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(Guid aObjectTypeGuid, ObjectRelationLink[] aObjRelations);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeName">Наименование типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(string aObjectTypeName, ObjectRelationLink[] aObjRelations);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeID">Идентификатор ипа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(
    int aObjectTypeID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectType">Тип объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(
    IDBObjectType aObjectType,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeGuid">GUID типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(
    Guid aObjectTypeGuid,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeName">Наименование типа объекта, по которому будет создан новый экземпляр объекта.</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(
    string aObjectTypeName,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeIDRelationTypeIDs">Hashtable в которой идентификатору типа объекта
  /// поставлен в соответствие идентификатор типа связей, которые нужно будет создать с
  /// объектами, чьи идентификаторы переданы в aRelatedObjectIDs</param>
  /// <param name="aRelatedObjectIDs">массив идентификаторов в состав которых
  /// нужно включить создаваемый объект</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(Hashtable aObjectTypeIDRelationTypeIDs, long[] aRelatedObjectIDs);

  /// <summary>Создание нового объекта заданного типа</summary>
  /// <param name="aObjectTypeIDRelationTypeIDs">Hashtable в которой идентификатору типа объекта
  /// поставлен в соответствие идентификатор типа связей, которые нужно будет создать с
  /// объектами, чьи идентификаторы переданы в aRelatedObjectIDs</param>
  /// <param name="aRelatedObjectIDs">массив идентификаторов в состав которых
  /// нужно включить создаваемый объект</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTypeDialog(
    Hashtable aObjectTypeIDRelationTypeIDs,
    long[] aRelatedObjectIDs,
    DateTime aStartDate);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aTemmplateObject">Прототип объекта по которому будет создан новый экземпляр</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(IDBObject aTemmplateObject);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(long aTemplateObjectID);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aObjectGuid">GUID объекта, который является прототипом для создаваемого экземпляра</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(Guid aObjectGuid);

  /// <summary>Создать версию другого типа</summary>
  /// <param name="aTemplateObjectID"></param>
  /// <param name="aTemplateObjectType"></param>
  /// <returns></returns>
  long CreateVersionAnotherType(long aTemplateObjectID, int aTemplateObjectType);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aTemmplateObject">Прототип объекта по которому будет создан новый экземпляр</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(IDBObject aTemmplateObject, ObjectRelationLink[] aObjRelations);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(long aTemplateObjectID, ObjectRelationLink[] aObjRelations);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aObjectGuid">GUID объекта, который является прототипом для создаваемого экземпляра</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(Guid aObjectGuid, ObjectRelationLink[] aObjRelations);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aTemmplateObject">Прототип объекта по которому будет создан новый экземпляр</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(
    IDBObject aTemmplateObject,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="aObjectGuid">GUID объекта, который является прототипом для создаваемого экземпляра</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectByTemplateDialog(
    Guid aObjectGuid,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aTemmplateObject">Прототип объекта по которому будет создан новый экземпляр</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(IDBObject aTemmplateObject);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(long aTemplateObjectID);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aObjectGuid">GUID объекта, который является прототипом для создаваемого экземпляра</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(Guid aObjectGuid);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aTemmplateObject">Прототип объекта по которому будет создан новый экземпляр</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(
    IDBObject aTemmplateObject,
    ObjectRelationLink[] aObjRelations);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aObjectGuid">GUID объекта, который является прототипом для создаваемого экземпляра</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(Guid aObjectGuid, ObjectRelationLink[] aObjRelations);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aTemmplateObject">Прототип объекта по которому будет создан новый экземпляр</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(
    IDBObject aTemmplateObject,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aTemplateObjectID">Идентификатор, который задает объект-прототип для создаваемого экземпляра</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(
    long aTemplateObjectID,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Создание новой версии объекта по прототипу</summary>
  /// <param name="aObjectGuid">GUID объекта, который является прототипом для создаваемого экземпляра</param>
  /// <param name="aObjRelations">Массив структур, содержащих описание связей, которые нужно создать для нового объекта</param>
  /// <param name="aStartDate">Дата с которой начинаеют действовать созданные связи</param>
  /// <returns>Идентификатор созданного объекта. Возвращается -1 если объект не создан</returns>
  long CreateObjectVersionByTemplateDialog(
    Guid aObjectGuid,
    ObjectRelationLink[] aObjRelations,
    DateTime aStartDate);

  /// <summary>Получение описаний созданных объектов (версий)</summary>
  /// <returns></returns>
  IList<ObjectCreatedInfo> GetObjectCreatedInfo();

  /// <summary>
  /// Регистрация мастера создания объектов определенного типа
  /// для переопределения имеющегося по умолчанию диалога
  /// </summary>
  /// <param name="aObjectTypeID">идентификатор типа объектов для которого производится
  /// замена стандартного мастера создания объектов</param>
  /// <param name="aCustomServiceType">сылка на тип объекта реализующего интерфейс
  /// перекрытого диалога создания объекта определенного типа</param>
  void RegisterCreatorCustomService(int aObjectTypeID, Type aCustomServiceType);

  /// <summary>
  /// Разрегистрация мастера создания объектов определенного типа
  /// </summary>
  /// <param name="aObjectTypeID">идентификатор типа объектов для которого производится
  /// замена стандартного мастера создания объектов</param>
  /// <param name="aCustomServiceType">сылка на тип объекта реализующего интерфейс
  /// который надо удалить из списка перекрывающих диалогов</param>
  void UnregisterCreatorCustomService(int aObjectTypeID, Type aCustomServiceType);

  /// <summary>
  /// Событие, возникающее перед созданием заготовки нового объекта
  /// (позволяет выполнить подстановку идентификатора прототипа объекта)
  /// </summary>
  event BeforeDraftCreateEventHandler BeforeDraftCreateEvent;

  /// <summary>
  /// Событие, возникающее при создании заготовки нового объекта
  /// </summary>
  event AfterDraftCreatedEventHandler AfterDraftCreatedEvent;

  /// <summary>
  /// Событие, возникающее при успешном завершении создания нового объекта
  /// </summary>
  event AfterObjectCreatedEventHandler AfterObjectCreatedEvent;

  /// <summary>
  /// Событие, возникающее при ОТМЕНЕ создания нового объекта, если заготовка объекта была создана
  /// </summary>
  event ObjectCreatorCanceledEventHandler ObjectCreatorCanceledEvent;

  /// <summary>
  /// Событие для открытия успешно созданного объекта, если включен переключатель "Открыть редактор после создания"
  /// Вызывается после AfterObjectCreatedEvent, чтобы гарантировать, что новый объект откроется только после других обработчиков
  /// </summary>
  event AfterObjectCreatedEventHandler OpenObjectAfterCreationEvent;

  /// <summary>
  /// Событие, возникающее при успешном включении в какой-либо состав создаваемого объекта
  /// </summary>
  event AfterEntersInCreatedEventHandler AfterEntersInCreatedEvent;

  /// <summary>
  /// Событие возникает после копирования переименованных файлов из объекта-прототипа в заготовку
  /// </summary>
  event FilesRenamedEventHandler FilesRenamedEvent;

  /// <summary>
  /// Cобытиt, возникающего перед CommitCreation для заготовки
  /// </summary>
  event BeforeCommitCreationEventHandler BeforeCommitCreationEvent;

  /// <summary>
  /// Событие возникает при выборе пользовательского мастера создания объектов определенного типа.
  /// </summary>
  event EventHandler<ObjectCreatorCustomServiceEventArgs> SelectCustomServiceEvent;

  /// <summary>Генерация события об отмене создания версии</summary>
  /// <param name="zagId">ИД заготовки отменяемой версии</param>
  /// <param name="isVersion">Является ли заготовка версией</param>
  /// <param name="objTypeID">ИД типа объекта</param>
  void FireOnObjectCreatorCanceledEvent(long zagId, bool isVersion = true, int objTypeID = -1);
}
