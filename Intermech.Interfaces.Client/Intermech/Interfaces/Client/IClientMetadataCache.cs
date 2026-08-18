// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IClientMetadataCache
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.LifeCycles;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для получения классов с информацией о метаданных на клиенте
/// </summary>
public interface IClientMetadataCache
{
  /// <summary>Возвращает интерфейс с информацией о типе объектов</summary>
  /// <param name="objectTypeID">Ид. типа объектов</param>
  /// <returns></returns>
  IDBObjectTypeInfo GetObjectType(int objectTypeID);

  /// <summary>Возвращает интерфейс с информацией об атрибуте</summary>
  /// <param name="attributeTypeID">Ид. атрибута</param>
  /// <returns></returns>
  IDBAttributeTypeInfo GetAttributeType(int attributeTypeID);

  /// <summary>Возвращает интерфейс с информацией о типе связей</summary>
  /// <param name="relationTypeID">Ид. типа связей</param>
  /// <returns></returns>
  IDBRelationTypeInfo GetRelationType(int relationTypeID);

  /// <summary>
  /// Возвращает интерфейс с информацией об уровне продвижения
  /// </summary>
  /// <param name="levelID">Ид. уровня продвижения</param>
  /// <returns></returns>
  IDBLifecycleLevelInfo GetLifecycleLevel(int levelID);

  /// <summary>
  /// Возвращает интерфейс с информацией о шаге жизненного цикла
  /// </summary>
  /// <param name="stepID">Ид. шага</param>
  /// <returns></returns>
  IDBLifecycleStepInfo GetLCStep(int stepID);

  /// <summary>Возвращает интерфейс с информацией о типе объектов</summary>
  /// <param name="objectTypeID">Ид. типа объектов</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBObjectTypeInfo GetObjectType(int objectTypeID, bool notFoundException);

  /// <summary>Возвращает интерфейс с информацией об атрибуте</summary>
  /// <param name="attributeTypeID">Ид. атрибута</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBAttributeTypeInfo GetAttributeType(int attributeTypeID, bool notFoundException);

  /// <summary>Возвращает интерфейс с информацией об атрибуте</summary>
  /// <param name="attributeGUID">Глобальный ид. атрибута</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBAttributeTypeInfo GetAttributeType(Guid attributeGUID, bool notFoundException);

  /// <summary>Возвращает интерфейс с информацией об атрибуте</summary>
  /// <param name="attributeName">Наименование атрибута</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBAttributeTypeInfo GetAttributeType(string attributeName, bool notFoundException);

  /// <summary>Возвращает интерфейс с информацией о типе связей</summary>
  /// <param name="relationTypeID">Ид. типа связей</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBRelationTypeInfo GetRelationType(int relationTypeID, bool notFoundException);

  /// <summary>Возвращает интерфейс с информацией о типе связей</summary>
  /// <param name="relationGUID">Глобальный ид. типа связей</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBRelationTypeInfo GetRelationType(Guid relationGUID, bool notFoundException);

  /// <summary>
  /// Возвращает интерфейс с информацией об уровне продвижения
  /// </summary>
  /// <param name="levelID">Ид. уровня продвижения</param>
  /// <param name="notFoundException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBLifecycleLevelInfo GetLifecycleLevel(int levelID, bool notFoundException);

  /// <summary>
  /// Получить нефильтрованную коллекцию уровней продвижения
  /// </summary>
  /// <returns></returns>
  IDBLifecycleLevelInfoCollection GetLifecycleLevelCollection();

  /// <summary>Возвращает коллекцию групп атрибутов</summary>
  /// <param name="groupID">Ид. родительской группы атрибутов (-1 если получаем первый уровень)</param>
  /// <param name="filterRecs">Фильтровать ли результат по видимости</param>
  /// <returns></returns>
  IDBAttributesGroupInfoCollection GetAttributesGroupCollection(int groupID, bool filterRecs);

  /// <summary>
  /// Получить список атрибутов в группе groupID. Если groupID = -1, то получается
  /// список всех атрибутов, зарегистрированных в системе.
  /// </summary>
  /// <param name="groupID">Идентификатор группы атрибутов.</param>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  IDBAttributeTypeInfoCollection GetAttributeTypeCollection(int groupID, bool filterRecs);

  /// <summary>Возвращает интерфейс с информацией о типе объектов</summary>
  /// <param name="anObjectTypeGuid">Гуид типа объектов</param>
  /// <param name="throwException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBObjectTypeInfo GetObjectType(Guid anObjectTypeGuid, bool throwException);

  /// <summary>Возвращает интерфейс с информацией о типе объектов</summary>
  /// <param name="anObjectTypeName">Наименование типа объектов</param>
  /// <param name="throwException">Выдавать ли эксепшен если такой тип не найден</param>
  /// <returns></returns>
  IDBObjectTypeInfo GetObjectType(string anObjectTypeName, bool throwException);

  /// <summary>
  /// Возвращает коллекцию типов объектов, входящих в состав типа parentTypeID.
  /// </summary>
  /// <param name="parentTypeID">Идентификатор родительского типа объектов. Если == -1,
  /// то возвращает корневые типы объектво. Если == -2, то возвращает ВСЕ типы объектов</param>
  IDBObjectTypeInfoCollection GetObjectTypeCollection(int parentTypeID);

  /// <summary>
  /// Возвращает коллекцию типов объектов, входящих в состав типа parentTypeID.
  /// </summary>
  /// <param name="parentTypeID">Идентификатор родительского типа объектов. Если == -1,
  /// то возвращает корневые типы объектво. Если == -2, то возвращает ВСЕ типы объектов</param>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  IDBObjectTypeInfoCollection GetObjectTypeCollection(int parentTypeID, bool filterRecs);

  /// <summary>Возвращает полный список типов связей.</summary>
  IDBRelationTypeInfoCollection GetRelationTypeCollection();

  /// <summary>Возвращает список типов связей.</summary>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  IDBRelationTypeInfoCollection GetRelationTypeCollection(bool filterRecs);

  /// <summary>Получить группу атрибутов номер aGroupID</summary>
  IDBAttributesGroupInfo GetAttributesGroup(int aGroupID);

  /// <summary>Возвращает схему ЖЦ</summary>
  /// <param name="schemaID">Ид. схемы</param>
  /// <returns>Класс с инфой о схеме ЖЦ</returns>
  IDBLCSchemaInfo GetLCSchema(int schemaID);

  /// <summary>Возвращает схему ЖЦ</summary>
  /// <param name="schemaID">Ид. схемы</param>
  /// <param name="throwException">Генерить ли исключение если такой схемы нет</param>
  /// <returns>Класс с инфой о схеме ЖЦ</returns>
  IDBLCSchemaInfo GetLCSchema(int schemaID, bool throwException);

  /// <summary>Возвращает схему ЖЦ</summary>
  /// <param name="schemaGuid">Гуид схемы</param>
  /// <returns>Класс с инфой о схеме ЖЦ</returns>
  IDBLCSchemaInfo GetLCSchema(Guid schemaGuid);

  /// <summary>Возвращает схему ЖЦ</summary>
  /// <param name="schemaGuid">Гуид схемы</param>
  /// <param name="throwException">Генерить ли исключение если такой схемы нет</param>
  /// <returns>Класс с инфой о схеме ЖЦ</returns>
  IDBLCSchemaInfo GetLCSchema(Guid schemaGuid, bool throwException);

  /// <summary>Ид. атрибута Файл</summary>
  int FileAttributeID { get; }

  /// <summary>Группы пользователей</summary>
  int GroupsTypeID { get; }

  /// <summary>Пользователи</summary>
  int UsersTypeID { get; }

  /// <summary>Роли</summary>
  int RolesTypeID { get; }

  /// <summary>Загружаемые модули</summary>
  int PluginTypeID { get; }
}
