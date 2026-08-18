// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseServer
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Интерфейс серверной части IMBASE</summary>
public interface IImbaseServer
{
  /// <summary>
  /// Создает объект заданного типа из указанного в baseId объекта
  /// </summary>
  /// <param name="sessionGuid">Guid сессии </param>
  /// <param name="catalogId">Идентификатор Каталога</param>
  /// <param name="baseId">Идентификатор базового объекта ( папка, запись каталога, таблица или ссылка на таблицу Imbase</param>
  /// <param name="recordId">Идентификатор записи таблицы если базовый объект - таблица или ссылка на таблицу IMBASE</param>
  /// <param name="commitCreation">Вносить объект в базу после создания</param>
  /// <param name="needType">Тип создаваемого объекта или -1 для выбора системой</param>
  /// <returns>Идентификатор созданного объекта. Если отрицательный, то объект создан, но не сохранен.</returns>
  long CreateObject(
    Guid sessionGuid,
    long catalogId,
    long baseId,
    long recordId,
    bool commitCreation,
    int needType);

  /// <summary>
  /// Заполнение атрибутов объекта атрибутами из указанного в baseId объекта.
  /// </summary>
  /// <param name="sessionGuid">Guid сессии </param>
  /// <param name="destObjID">Ид. объекта</param>
  /// <param name="baseID">Идентификатор базового объекта ( папка, запись каталога ссылка или таблица Imbase</param>
  /// <param name="recordId">Идентификатор записи таблицы, если базовый объект - ссылка или таблица IMBASE</param>
  /// <param name="createNew"></param>
  /// <returns>Сообщение об ошибке при синхронизации параметров</returns>
  string FillObjectAttributes(
    Guid sessionGuid,
    long destObjID,
    long baseID,
    long recordId,
    bool createNew);

  /// <summary>
  /// Заполнение атрибутов объекта атрибутами из указанного в baseId объекта.
  /// </summary>
  /// <param name="sessionGuid">Guid сессии </param>
  /// <param name="destObjId">Ид. объекта</param>
  /// <param name="linkId">Идентификатор базового объекта ( папка, запись каталога или запись таблицы Imbase</param>
  /// <param name="tableId">Идентификатор таблицы, если базовый объект - запись таблицы IMBASE</param>
  /// <param name="recordId">Идентификатор ссылки на таблицу, если базовый объект - запись таблицы IMBASE</param>
  /// <param name="createNew"></param>
  /// <returns>Сообщение об ошибке при синхронизации параметров</returns>
  string FillObjectAttributes(
    Guid sessionGuid,
    long destObjId,
    long linkId,
    long tableId,
    long recordId,
    bool createNew);

  /// <summary>
  /// Fill object link atttibutes from imbase by master attribute.
  /// </summary>
  /// <param name="sessionGuid">User session's guid</param>
  /// <param name="destObjID">Destination object id</param>
  /// <param name="masterAttributeID">Master attribute id</param>
  /// <param name="imbaseObjID">Imbase object ID</param>
  void FillObjectLinkAttributes(
    Guid sessionGuid,
    long destObjID,
    int masterAttributeID,
    long imbaseObjID);

  /// <summary>
  /// Получает с сервера иерархию папок IMBASE, выбранных по указанным условиям.
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="catalogs">Идентификатор Каталога или массив идентификаторов или null</param>
  /// <param name="conditions">Условия для первоначального выбора папок</param>
  /// <param name="objectTypes">Типы объектов для первоначального выбора</param>
  /// <returns>DataTable с иерархией</returns>
  DataTable GetTreePart(
    Guid sessionGuid,
    long[] catalogs,
    object conditions,
    ImbaseObjectTypes objectTypes);

  /// <summary>
  /// Возвращает список идентификаторов атрибутов, назначенных типу
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="rowsTypeGuid">Guid идентификатора типа объекта</param>
  /// <param name="rowsProps">Структуры со свойствами атрибутов</param>
  /// <param name="rowsProps4">Структуры со свойствами атрибутов для типа</param>
  /// <param name="rowsTypeID">Идентификатор типа объекта</param>
  /// <returns></returns>
  int[] GetAttributesForType(
    Guid sessionGuid,
    Guid rowsTypeGuid,
    out AttributeTypeProperties[] rowsProps,
    out Attribute4ObjectTypeProperties[] rowsProps4,
    out int rowsTypeID);

  /// <summary>
  /// Возвращает список подпапок для указанного объекта ( Каталог или папка)
  /// </summary>
  /// <param name="guid">GUID сессии</param>
  /// <param name="parentId">Идентификатор родительского объекта</param>
  /// <param name="addTypes">Список дополнительных типов для включения в состав</param>
  /// <returns>DataTable с составом папки</returns>
  DataTable GetSubfolders(Guid sessionGuid, long parentId, int[] addTypes);

  /// <summary>
  /// Возвращает список подпапок для указанных объектов ( Каталог или папка)
  /// </summary>
  /// <param name="guid">GUID сессии</param>
  /// <param name="parentIds">Идентификаторы родительских объектов</param>
  /// <param name="addTypes">Список дополнительных типов для включения в состав</param>
  /// <returns>DataTable с составом папки</returns>
  DataTable GetSubfolders(Guid sessionGuid, long[] parentIds, int[] addTypes);

  /// <summary>
  /// Возвращает полный список подпапок  для указанного объекта ( Каталог или папка)
  /// </summary>
  /// <param name="guid">GUID сессии</param>
  /// <param name="parentId">Идентификатор родительского объекта</param>
  /// <param name="addTypes">Список дополнительных типов для включения в состав</param>
  /// <returns>DataTable с составом папки</returns>
  DataTable GetAllSubfolders(Guid sessionGuid, long parentId, int[] addTypes);

  /// <summary>
  /// Возвращает список папок и Каталогов, у которых установлен атрибут "Тип создаваемого объекта" в значение
  /// указанное в параметре needType, а также все подпапки у которых данный атрибут не установлен
  /// </summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="needType">Тип создаваемого объекта (int, строка или Guid)</param>
  /// <param name="catalogs">Список идентификаторов Каталогов, включаемых в результат</param>
  /// <param name="buildTree">Указывает, строить ли дерево вверх по иерархии от выбранных папок</param>
  /// <returns>Таблица с результатом</returns>
  DataTable GetFoldersForCreateType(
    Guid sessionGuid,
    object needType,
    long[] catalogs,
    bool buildTree);

  /// <summary>
  /// Возвращает список папок и Каталогов, у которых установлен атрибут "Тип создаваемого объекта" в значение
  /// указанное в параметре needType
  /// </summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="needType">Тип создаваемого объекта (int, строка или Guid)</param>
  /// <param name="catalogs">Список идентификаторов Каталогов, включаемых в результат</param>
  /// <param name="buildTree">Указывает, строить ли дерево вверх по иерархии от выбранных папок</param>
  /// <param name="needSubFolders">Указывает, загружать ли подпапки</param>
  /// <returns>Таблица с результатом</returns>
  DataTable GetFoldersForCreateType(
    Guid sessionGuid,
    object needType,
    long[] catalogs,
    bool buildTree,
    bool needSubFolders);

  /// <summary>
  /// Строит таблицу иерархии от указанных объектов вверх до каталога
  /// </summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="objectList">Список идентификаторов папок и/или Каталогов</param>
  /// <param name="catalogs">Список идентификаторов Каталогов, включаемых в результат</param>
  /// <returns></returns>
  DataTable GetFoldersForObjects(Guid sessionGuid, long[] objectList, long[] catalogs);

  /// <summary>
  /// Возвращает идентификатор объекта по старому ключу IMBASE или создает (если надо)
  /// объект в базе данных.
  /// </summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="oldImbaseKey">Cтарый ключ IMBASE</param>
  /// <param name="objectType">Тип создаваемого объекта</param>
  /// <param name="createIfNotFound">Создавать ли объект, если не найдет</param>
  /// <param name="status">Расширенная информация, если в результате поиска объект по старому ключу не найден</param>
  /// <returns>Идентификатор созданного объекта или NoObject в случае ошибки</returns>
  long GetObjectIdByOldImbaseKey(
    Guid sessionGuid,
    string oldImbaseKey,
    int objectType,
    bool createIfNotFound,
    out ScanOldKeyStatus status);

  /// <summary>
  /// Анализирует и возвращает идентификатор ссылки на ярлык таблицы
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="objectDef">Описание Каталога</param>
  /// <param name="catalogDef">Описание таблицы/таблиц</param>
  /// <param name="ids">список идентификаторов ссылок на таблицы (если несколько)</param>
  /// <param name="tree">Данные для посторения иерархии при нескольких ссылках</param>
  /// <returns>Идентияикатор первой ссылки на таблицу</returns>
  long ResolveObjectDef(
    Guid sessionGuid,
    string objectDef,
    string catalogDef,
    out List<long> ids,
    out DataTable tree);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sessionGuid"></param>
  /// <param name="tableID"></param>
  /// <param name="catalogDef"></param>
  /// <param name="ids"></param>
  /// <param name="tree"></param>
  /// <returns></returns>
  long GetFoldersForTable(
    Guid sessionGuid,
    long tableID,
    string catalogDef,
    out List<long> ids,
    out DataTable tree);

  ICatalogInfo GetCatalogInfo(Guid sessionGuid, string catalogDef);

  /// <summary>
  /// Возвращает объект, который был использован в качестве прототипа из базы данных
  /// IMBASE для создания этого объекта.
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="objectDef">Идентификатор объекта</param>
  /// <returns>Объект прототип</returns>
  IDBObject GetPrototypeObject(Guid sessionGuid, object objectDef);

  /// <summary>
  /// Возвращает информацию об объекте, который который был использован в качестве прототипа из базы данных
  /// IMBASE для создания этого объекта.
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="objectGuid">Идентификатор объекта</param>
  /// <param name="linkId">Идентификатор ссылки на таблицу</param>
  /// <param name="recordId">Идентификатор записи в таблице</param>
  /// <returns>true в случае успеха</returns>
  bool GetPrototypeDetails(Guid sessionGuid, Guid objectGuid, ref long linkId, ref long recordId);

  /// <summary>
  /// Возвращает информацию об объекте, который который был использован в качестве прототипа из базы данных
  /// IMBASE для создания этого объекта.
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="objectGuid">Идентификатор ВЕРСИИ объекта</param>
  /// <param name="linkId">Идентификатор ссылки на таблицу</param>
  /// <param name="recordId">Идентификатор записи в таблице</param>
  /// <returns>true в случае успеха</returns>
  bool GetPrototypeDetailsByVersion(
    Guid sessionGuid,
    Guid objectGuid,
    ref long linkId,
    ref long recordId);

  /// <summary>
  /// Возвращает объект IMBASE ( запись в таблице) в котнексте ссылки на таблицу
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="recordId">Идентификатор записи</param>
  /// <param name="linkId">Идентификатор ссылки</param>
  /// <returns>Объект базы данных</returns>
  IDBObject GetContextedObject(Guid sessionGuid, long recordId, long linkId);

  /// <summary>
  /// Возвращает список Каталогов, из которых возможен выбор
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <returns>список Каталогов</returns>
  long[] GetCatalogsList(Guid sessionGuid);

  /// <summary>
  /// Загружает из базы таблицу с записями для указанного ярлыка или таблицы.
  /// Вычисляет все поля и применяет к таблице указанный фильтр.
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="objectId">Идентификатор ссылки на таблицу или самой таблицы</param>
  /// <param name="filter">фильтр</param>
  /// <param name="DecimalSeparator">Десятичный разделитель для дробных чисел ( клиента )</param>
  /// <param name="recordsTable">Таблица с записями</param>
  /// <param name="columnsAttributes">Массив атрибутов колонок</param>
  /// <param name="keyInfo">Расширенная информация</param>
  void LoadRecords(
    Guid sessionGuid,
    long objectId,
    string filter,
    string DecimalSeparator,
    out DataTable recordsTable,
    out AttributeTypeProperties[] columnsAttributes,
    out ImbaseKeyInfo keyInfo);

  /// <summary>
  /// Ищет в базе информацию о ранее созданных объектах, режиме создания и типе
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="linkId">Идентификатор ссылки на таблицу или самой таблицы</param>
  /// <param name="recordId">Идентификатор записи</param>
  /// <param name="createNew">Создавать ли новый объект</param>
  /// <param name="type">тип создаваемого объекта</param>
  /// <param name="existingObjects">список идентификаторов созданных объектов</param>
  /// <returns></returns>
  void GetObjectCreateInfo(
    Guid sessionGuid,
    long linkId,
    long recordId,
    ref bool createNew,
    ref int type,
    ref long[] existingObjects);

  /// <summary>
  /// Возвращает тип создаваемого объекта для ярлыка таблицы
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="linkId">Идентификатор ссылки на таблицу или самой таблицы</param>
  /// <returns></returns>
  int GetObjectType(Guid sessionGuid, long linkId);

  /// <summary>Создает массив именованных ссылок на записи таблиц</summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="keyValues">набор ссылок на записи таблиц</param>
  /// <returns></returns>
  Dictionary<string, string> NameRecordReferences(Guid sessionGuid, List<string> keyValues);

  /// <summary>
  /// Создает массив именованных ссылок на записи таблиц и применяемость
  /// </summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="keyValues">набор ссылок на записи таблиц</param>
  /// <returns></returns>
  Dictionary<string, Tuple<string, bool>> NameRecordReferencesWithApplicability(
    Guid sessionGuid,
    List<string> keyValues);

  /// <summary>Создает массив именованных ссылок объекты IPS</summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="keyValues">набор GUID объектов</param>
  /// <returns></returns>
  Dictionary<string, string> NameObjectReferences(Guid sessionGuid, List<string> keyValues);

  /// <summary>
  /// Получает таблицу со ссылками на указанную таблицу IMBASE
  /// </summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="tableId">Идентификатор таблицы</param>
  /// <param name="queryParams">Параметры запроса</param>
  /// <returns>Таблица со ссылками</returns>
  DataTable GetTableRefs(Guid sessionGuid, long tableId, DBRecordSetParams queryParams);

  /// <summary>
  /// Преобразует старый ключ IMBASE в новый временный ключ, если возможно
  /// </summary>
  /// <param name="sessionGuid">GUID сессии</param>
  /// <param name="oldImbaseKey">старый ключ IMBASE</param>
  /// <param name="status">Расширенная информация, если в результате поиска объект по старому ключу не найден</param>
  /// <returns>Новый временный ключ или пустая строка</returns>
  string ConvertOldImbaseKey(Guid sessionGuid, string oldImbaseKey, out ScanOldKeyStatus status);

  /// <summary>
  /// Получение данных таблиц IMBASE, на которые не ссылается ни одна ссылка на таблицу.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <returns>Таблица с данными</returns>
  DataTable GetUnlinkedTables(Guid sessionGuid);

  /// <summary>
  /// Преобразует наименование папки и всех нижележащих в верхний/нижний регистр
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="folderId">Идентификатор начальной папки</param>
  /// <param name="upperCase">верхний/нижний режим</param>
  void CapitalizeFolders(Guid sessionGuid, long folderId, bool upperCase);

  /// <summary>
  /// Возвращает все идентификаторы таблиц, в которых используется указанный атрибут
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="attributeId">Идентияикатор атрибута</param>
  /// <returns>Список идентификаторов таблиц</returns>
  List<long> GetTablesWithAtt(Guid sessionGuid, int attributeId);

  /// <summary>Обновляет индексы в ссылочных таблицах IMBASE</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  void UpdateSystemIndexes(Guid sessionGuid);

  /// <summary>
  /// Возвращает таблицу с списком объектов и записей таблицы, созданных из ярлыка
  /// </summary>
  /// <param name="session">Идентификатор сессии</param>
  /// <param name="objectId">Идентификатор ярлыка или таблицы</param>
  /// <returns>Таблицу с созданными объектами</returns>
  DataTable GetCreatedObjects(Guid sessionGuid, long objectId);

  /// <summary>Поиск значения по индексам</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="fieldName">Имя атрибута</param>
  /// <param name="fieldValue">значение для поиска</param>
  /// <param name="imbaseKey">временный ключ IMBASE или пустая строка, если ничего не найдено</param>
  /// <returns>0 - успех, !=0 - ошибка</returns>
  int FindItemByValue(Guid sessionGuid, string fieldName, string fieldValue, ref string imbaseKey);

  /// <summary>
  /// Получение объекта безопасности для записи таблицы Imabse
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableId">Идентификатор таблицы</param>
  /// <param name="recordId">Идентификатор записи</param>
  /// <returns></returns>
  IDBSecurity GetSecurityForRecord(Guid sessionGuid, long tableId, long recordId);

  /// <summary>
  /// Получение объекта безопасности для атрибута в таблицы Imabse
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableId">Идентификатор таблицы</param>
  /// <param name="recordId">Идентификатор атрибута</param>
  /// <returns></returns>
  IDBSecurity GetSecurityForAtt(Guid sessionGuid, long tableId, int attId);

  /// <summary>Получение безопасности для индекса</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="catalogId"></param>
  /// <param name="attId"></param>
  /// <returns></returns>
  IDBSecurity GetSecurityForIndex(Guid sessionGuid, long catalogId, int attId);

  /// <summary>Удаления безопасности для индекса</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="catalogId"></param>
  /// <param name="attId"></param>
  void PurgeSecurityForIndex(Guid sessionGuid, long catalogId, int attId);

  /// <summary>
  /// Записывает в журнал событий информацию об изменении структуры и данных таблицы
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableId">Идентификатор таблицы</param>
  /// <param name="newDataSet">Датасет с новыми данными</param>
  void LogDataChanges(Guid sessionGuid, long tableId, DataSet newDataSet);

  /// <summary>
  /// Получает список Каталогов, которые могут быть использованы при создании объекта заданного типв
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="needType">Требуемый тип</param>
  /// <param name="derivedTypes">Искать порожденные от базового типы</param>
  /// <returns>Список идентификаторов Каталогов</returns>
  long[] GetCatalogsForCreateType(Guid sessionGuid, object needType, bool derivedTypes);

  void ForceImportImbaseTable(Guid sessionGuid, long tableObjectId, long linkObjectId);

  /// <summary>
  /// Вычисляет старый ключ IMBASE вида I6CCCCCCRRRRRRTTTTTT
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="linkObjectId">Идентификатор ярлыка таблицы</param>
  /// <param name="recordId">Номер записи в таблице</param>
  /// <returns>Ключ IMBASE</returns>
  string CalcOldImbaseKey(Guid sessionGuid, long linkObjectId, long recordId);

  /// <summary>Получение базовых версий объекта по гуидам версии.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="objectGuids">массив GUIDов объектов</param>
  /// <param name="baseData"> массив данных о базовых версиях ( новый гуид и заголовок)</param>
  /// <returns></returns>
  int GetBaseVersionGuids(Guid sessionGuid, string[] objectGuids, out object[] baseData);

  /// <summary>Получение папок Избранное для каталога</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogIds"> id-ы каталогов</param>
  /// <param name="needContent"> подгружать ли содержимое папок Избранное</param>
  /// <returns></returns>
  DataTable GetFavoriteFoldersForCatalogs(Guid sessionGuid, long[] catalogIds, bool needContent);

  /// <summary>Получить список атрибутов для типа объкета</summary>
  /// <param name="session"></param>
  /// <param name="objTypeID"></param>
  /// <returns></returns>
  List<IMSAttribute4ObjectType> GetAttributesForObjectType(IUserSession session, int objTypeID);
}
