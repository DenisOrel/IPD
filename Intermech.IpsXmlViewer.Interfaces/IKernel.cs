// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IKernel
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Микроядро, позволяющее читать информацию из индекса</summary>
public interface IKernel
{
  /// <summary>Получить уникальное внутреннее целочисленное значение</summary>
  long GetUniqueID { get; }

  /// <summary>Контейнер сервисов</summary>
  IServiceProvider Services { get; }

  /// <summary>База данных</summary>
  IIndexer Indexer { get; }

  /// <summary>Метаданные</summary>
  IImMetaData MetaData { get; }

  /// <summary>
  /// Получить значение поля у записи со значением указанного ключа
  /// </summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="keyFieldName">Имя ключевого поля для поиска</param>
  /// <param name="keyFieldValue">Значение ключевого поля для поиска</param>
  /// <param name="fieldName">Имя запрашиваемого поля</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Значение или null</returns>
  object GetFieldValue(
    string tableName,
    string keyFieldName,
    object keyFieldValue,
    string fieldName,
    bool throwIfNotFound);

  /// <summary>
  /// Установить значение поля у записи со значением указанного ключа
  /// </summary>
  /// <param name="tableName">Имя таблицы</param>
  /// <param name="keyFieldName">Имя ключевого поля для поиска</param>
  /// <param name="keyFieldValue">Значение ключевого поля для поиска</param>
  /// <param name="fieldName">Имя устанавливаемого поля</param>
  /// <param name="fieldValue">Значение</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Значение или null</returns>
  bool SetFieldValue(
    string tableName,
    string keyFieldName,
    object keyFieldValue,
    string fieldName,
    object fieldValue,
    bool throwIfNotFound);

  /// <summary>Получить список типов атрибутов</summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов атрибутов или null</returns>
  List<IImAttributeType> GetAttributeTypes(bool throwIfError);

  /// <summary>Получить список типов объектов</summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов объектов или null</returns>
  List<IImObjectType> GetObjectTypes(bool throwIfError);

  /// <summary>Получить список типов связей</summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов связей или null</returns>
  List<IImRelationType> GetRelationTypes(bool throwIfError);

  /// <summary>Загрузить атрибуты в указанный объект/связь</summary>
  /// <param name="item">Объект/связь</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Описание объекта или null</returns>
  void ReadItemAttributes(IImDataElement item, bool throwIfNotFound);

  /// <summary>
  /// Получить идентификатор версии объекта из индекса на основании уникального глобального идентификатора его версии
  /// </summary>
  /// <param name="F_OBJECTGUID">Уникальный глобальный идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>ID версии объекта или null</returns>
  long GetObjectID(Guid F_OBJECTGUID, bool throwIfNotFound);

  /// <summary>
  /// Получить Guid версии объекта из индекса на основании идентификатора его версии
  /// </summary>
  /// <param name="F_OBJECT_ID">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Guid версии объекта или null</returns>
  Guid GetObjectGuid(long F_OBJECT_ID, bool throwIfNotFound);

  /// <summary>
  /// Получить описание объекта из индекса на основании идентификатора его версии
  /// </summary>
  /// <param name="F_OBJECT_ID">Идентификатор версии объекта</param>
  /// <param name="onlyObligatory">true - читаются только обязательные атрибуты (минус один SQL-запрос)</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Описание объекта или null</returns>
  IImObject GetObject(long F_OBJECT_ID, bool onlyObligatory, bool throwIfNotFound);

  /// <summary>
  /// Получить описание объекта из индекса на основании уникального глобального идентификатора его версии
  /// </summary>
  /// <param name="onlyObligatory">true - читаются только обязательные атрибуты (минус один SQL-запрос)</param>
  /// <param name="F_OBJECTGUID">Уникальный глобальный идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если объект не был найден</param>
  /// <returns>Описание объекта или null</returns>
  IImObject GetObject(Guid F_OBJECTGUID, bool onlyObligatory, bool throwIfNotFound);

  /// <summary>
  /// Получить список объектов (только с обязательными атрибутами по умолчанию) указанного типа
  /// </summary>
  /// <param name="F_OBJECT_TYPE">Идентификатор типа объекта (-1 - все объекты)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) указанного типа или null</returns>
  List<IImObject> GetObjects(int F_OBJECT_TYPE, bool throwIfError, bool readAttributes = false);

  /// <summary>
  /// Получить список объектов (только с обязательными атрибутами по умолчанию) для заданного условия
  /// </summary>
  /// <param name="sqlCondition">Условие на объекты  ("" - все объекты)</param>
  /// <param name="sqlOrder">Сортировка объектов</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) указанного типа или null</returns>
  IEnumerable<IImObject> GetObjects(
    string sqlCondition,
    string sqlOrder,
    bool throwIfError,
    bool readAttributes = false);

  /// <summary>
  /// Получить идентификатор связи из индекса на основании её уникального глобального идентификатора
  /// </summary>
  /// <param name="F_PRJ_GUID">Уникальный глобальный идентификатор связи</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если связь не была найдена</param>
  /// <returns>Gud версии объекта или null</returns>
  long GetRelationID(Guid F_PRJ_GUID, bool throwIfNotFound);

  /// <summary>
  /// Получить Guid связи из индекса на основании её идентификатора
  /// </summary>
  /// <param name="F_PRJLINK_ID">Идентификатор связи</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если связь не была найдена</param>
  /// <returns>Guid связи или Guid.Empty</returns>
  Guid GetRelationGuid(long F_PRJLINK_ID, bool throwIfNotFound);

  /// <summary>
  /// Получить все связи указанного типа (или все связи из индекса)
  /// </summary>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns></returns>
  IEnumerable<IImRelation> GetRelations(
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false);

  /// <summary>
  /// Получить все связи (только с обязательными атрибутами по умолчанию) для заданного условия
  /// </summary>
  /// <param name="sqlCondition">Условие на объекты  ("" - все объекты)</param>
  /// <param name="sqlOrder">Сортировка связей</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) указанного типа или null</returns>
  IEnumerable<IImRelation> GetRelations(
    string sqlCondition,
    string sqlOrder,
    bool throwIfError,
    bool readAttributes = false);

  /// <summary>
  /// Получить описание связи из индекса на основании её идентификатора
  /// </summary>
  /// <param name="F_PRJLINK_ID">Идентификатор связи</param>
  /// <param name="onlyObligatory">true - читаются только обязательные атрибуты (минус один SQL-запрос)</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если связь не была найдена</param>
  /// <returns>Описание связи или null</returns>
  IImRelation GetRelation(long F_PRJLINK_ID, bool onlyObligatory, bool throwIfNotFound);

  /// <summary>
  /// Получить описание связи из индекса на основании её глобального уникального идентификатора
  /// </summary>
  /// <param name="F_PRJ_GUID">Уникальный глобальный идентификатор связи</param>
  /// <param name="onlyObligatory">true - читаются только обязательные атрибуты (минус один SQL-запрос)</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если связь не была найдена</param>
  /// <returns>Описание связи или null</returns>
  IImRelation GetRelation(Guid F_PRJ_GUID, bool onlyObligatory, bool throwIfNotFound);

  /// <summary>
  /// Получить список объектов (только с обязательными атрибутами по умолчанию) верхнего уровня в составах
  /// </summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  List<IImObject> GetRootObjects(bool throwIfError, bool readAttributes = false);

  /// <summary>
  /// Получить состав (только с обязательными атрибутами связей и объектов) указанного родительского объекта
  /// </summary>
  /// <param name="F_PART_ID">Уникальный глобальный идентификатор версии родительского объекта</param>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  List<IImRelation> GetComposition(
    Guid F_PROJ_ID,
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false);

  /// <summary>
  /// Получить состав (только с обязательными атрибутами связей и объектов) указанного родительского объекта
  /// </summary>
  /// <param name="F_PROJ_OBJ">Идентификатор версии родительского объекта</param>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  List<IImRelation> GetComposition(
    long F_PROJ_OBJ,
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false);

  /// <summary>
  /// Получить список типов связей из состава указанного родительского объекта
  /// </summary>
  /// <param name="F_PROJ_OBJ">Локальный идентификатор версии родительского объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов связей из состава указанного родительского объекта</returns>
  List<IImRelationType> GetCompositionRelTypes(long F_PROJ_OBJ, bool throwIfError);

  /// <summary>
  /// Проверить наличие состава у указанного объекта (по указанному типусвязи либо по любым)
  /// </summary>
  /// <param name="F_PROJ_OBJ">Локальный идентификатор версии родительского объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="F_RELATION_TYPE">Проверяемый тип связи или Consts.UnknownIDx32, если требуется проверить любые типы связей</param>
  /// <returns>true - в составе есть как минимум одна связь указанного (или любого) типа</returns>
  bool HasComposition(long F_PROJ_OBJ, bool throwIfError, int F_RELATION_TYPE = 0);

  /// <summary>
  /// Получить применяемость (только с обязательными атрибутами связей и объектов) указанного дочернего объекта
  /// </summary>
  /// <param name="F_PART_ID">Уникальный глобальный идентификатор версии родительского объекта</param>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  List<IImRelation> GetApplicability(
    Guid F_PART_ID,
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false);

  /// <summary>
  /// Получить применяемость (только с обязательными атрибутами связей и объектов) указанного дочернего объекта
  /// </summary>
  /// <param name="F_PART_OBJ">Идентификатор версии родительского объекта</param>
  /// <param name="F_RELATION_TYPE">Тип связи (-1 - все типы связей)</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) верхнего уровня в составах или null</returns>
  List<IImRelation> GetApplicability(
    long F_PART_OBJ,
    int F_RELATION_TYPE,
    bool throwIfError,
    bool readAttributes = false);

  /// <summary>
  /// Получить список типов связей из состава указанного родительского объекта
  /// </summary>
  /// <param name="F_PART_OBJ">Локальный идентификатор версии дочернего объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Список типов связей из состава указанного родительского объекта</returns>
  List<IImRelationType> GetGetApplicabilityRelTypes(long F_PART_OBJ, bool throwIfError);

  /// <summary>
  /// Проверить наличие применяемости у указанного объекта (по указанному типусвязи либо по любым)
  /// </summary>
  /// <param name="F_PART_OBJ">Локальный идентификатор версии дочернего объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="F_RELATION_TYPE">Проверяемый тип связи или Consts.UnknownIDx32, если требуется проверить любые типы связей</param>
  /// <returns>true - в составе есть как минимум одна связь указанного (или любого) типа</returns>
  bool HasApplicability(long F_PART_OBJ, bool throwIfError, int F_RELATION_TYPE = 0);

  /// <summary>Создать в индексе запись объекта и его атрибутов</summary>
  /// <param name="obj">Описание создаваемой версии объекта</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Описание созданной версии объекта</returns>
  IImObject CreateObject(IImObject obj, bool throwIfError);

  /// <summary>Удалить объект с указанным идентификатором из индекса</summary>
  /// <param name="F_OBJECT_ID">Идентификатор удаляемой версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - объект был удалён</returns>
  bool DeleteObject(long F_OBJECT_ID, bool throwIfNotFound);

  /// <summary>Удалить объект с указанным идентификатором из индекса</summary>
  /// <param name="F_OBJECTGUID">Уникальный глобальный идентификатор удаляемой версии объекта</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - объект был удалён</returns>
  bool DeleteObject(Guid F_OBJECTGUID, bool throwIfNotFound);

  /// <summary>Создать в индексе запись связи и её атрибутов</summary>
  /// <param name="rel">Описание создаваемой связи</param>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <returns>Описание созданной связи</returns>
  IImRelation CreateRelation(IImRelation rel, bool throwIfError);

  /// <summary>Удалить связь с указанным идентификатором из индекса</summary>
  /// <param name="F_PRJLINK_ID">Идентификатор удаляемой связи</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - связь была удалена</returns>
  bool DeleteRelation(long F_PRJLINK_ID, bool throwIfNotFound);

  /// <summary>
  /// Удалить связь с указанным уникальным глобальным идентификатором из индекса
  /// </summary>
  /// <param name="F_PRJ_GUID">Уникальный глобальный идентификатор удаляемой связи</param>
  /// <param name="throwIfNotFound">true - генерировать исключение, если были ошибки</param>
  /// <returns>true - связь была удалена</returns>
  bool DeleteRelation(Guid F_PRJ_GUID, bool throwIfNotFound);

  /// <summary>Загрузить метаданные из индекса</summary>
  void LoadMetaData();

  /// <summary>
  /// Получить список объектов, на которые есть ссылки (только с обязательными атрибутами объектов)
  /// </summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) или null</returns>
  List<IImObject> GetReferencedObjects(bool throwIfError, bool readAttributes = false);

  /// <summary>
  /// Получить список объектов типа "Пользователи", на которые есть ссылки у других объектов
  /// в полях F_OWNER_ID и F_CHKOUT_BY (только с обязательными атрибутами объектов)
  /// </summary>
  /// <param name="throwIfError">true - генерировать исключение, если были ошибки</param>
  /// <param name="readAttributes">Загружать в объекты все атрибуты (выполняется много дополнительных запросов)</param>
  /// <returns>Список объектов (только с обязательными атрибутами) или null</returns>
  List<IImObject> GetUserReferences(bool throwIfError, bool readAttributes = false);

  /// <summary>Связать объект базы данных индекса с объектом IPS</summary>
  /// <param name="xmlObjectID">Идентификатор версии объекта в базе данных индекса</param>
  /// <param name="ipsObjectID">Идентификатор версии объекта в базе данных IPS</param>
  /// <param name="ipsObjType">Идентификатор типа объекта в базе данных IPS</param>
  void LinkWithIPSObject(long xmlObjectID, long ipsObjectID, int ipsObjType);
}
