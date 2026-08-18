// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseIndexingService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
public interface IImbaseIndexingService
{
  /// <summary>Процент завершенности задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="nState">Состояние задачи</param>
  /// <param name="text">Наименование выполняемых действий</param>
  /// <returns>Процент завершенности</returns>
  int GetCompleted(Guid taskGuid, out int nState, out string text);

  /// <summary>Получение результата выполнения задачи.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <returns>Список ошибок</returns>
  /// <remarks>Удаляет задачу из списка задач</remarks>
  List<Exception> GetResult(Guid taskGuid);

  /// <summary>Удалить задачу после завершения.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <remarks>В некоторых случаях нужно, чтобы задача автоматически удалялась после завершения.
  /// Например если она выполняется не в фоновом потоке, или когда клиент закрыт, а задача продолжает выполняться на сервере.</remarks>
  void RemoveAfterComplete(Guid taskGuid);

  /// <summary>Остановка задачи индексирования.</summary>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  void StopTask(Guid taskGuid);

  /// <summary>Добавление индексов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="catalogId">Идентификатор каталога</param>
  /// <param name="attrs">Список пар "Идентификатор атрибута - уникальность"</param>
  /// <remarks>Вызывается при создании индексов на закладке "Индексы" каталога.
  /// Использовать в фоновом потоке, т.к. задача занимает много времени.</remarks>
  void Add(Guid sessionGuid, Guid taskGuid, long catalogId, Dictionary<int, bool> attrs);

  /// <summary>Удаление индексов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="catalogID">Идентификатор каталога</param>
  /// <param name="attrIDs">Список идентификаторов атрибутов</param>
  /// <remarks>Вызывается при удалении индексов на закладке "Индексы" каталога.
  /// Использовать в фоновом потоке, т.к. задача занимает много времени.</remarks>
  void Remove(Guid sessionGuid, Guid taskGuid, long catalogID, List<int> attrIDs);

  /// <summary>Обновление индексов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="catalogID">Идентификатор каталога</param>
  /// <remarks>Вызывается при обновлении индексов на закладке "Индексы" каталога.
  /// Использовать в фоновом потоке, т.к. задача занимает много времени.</remarks>
  void Update(Guid sessionGuid, Guid taskGuid, long catalogID);

  /// <summary>Обновить уникальность индексов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="catalogId">Идентификатор каталога</param>
  /// <param name="attrs">Список пар "Идентификатор атрибута - уникальность"</param>
  /// <remarks>Вызывается при изменении значения уникальности индексов на закладке "Индексы" каталога</remarks>
  void UpdateFlags(Guid sessionGuid, Guid taskGuid, long catalogId, Dictionary<int, bool> attrs);

  /// <summary>Получить опции атрибутов</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="catalogId"></param>
  /// <param name="attrs"></param>
  /// <returns></returns>
  Dictionary<int, IndexesFlags> GetAttributesFlags(
    Guid sessionGuid,
    long catalogId,
    IEnumerable<int> attrs);

  /// <summary>
  /// Проверка на уникальность данных, после регистрации объекта в IMBASE.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="dtAttrs">Таблица с настройками атрибутов</param>
  /// <param name="dtData">Таблица с данными</param>
  /// <param name="rowNums">Номера добавленных строк</param>
  /// <remarks>Если данные будут не уникальными, то генерируется исключение</remarks>
  void CheckUniqueBeforeRegistryInImbase(
    Guid sessionGuid,
    long tableID,
    DataTable dtAttrs,
    DataTable dtData,
    List<long> rowNums);

  /// <summary>Обновение данных после регистрации в IMBASE.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="dtAttrs">Таблица с настройками атрибутов</param>
  /// <param name="dtData">Таблица с данными</param>
  /// <param name="rowNums">Номера добавленных строк</param>
  void UpdateAfterRegisteredInImbase(
    Guid sessionGuid,
    long tableID,
    DataTable dtAttrs,
    DataTable dtData,
    List<long> rowNums);

  /// <summary>
  /// Проверить уникальность данных для случая, когда меняется таблица, на которую ссылается ярлык.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу</param>
  /// <param name="tableID">Идентификатор новой таблицы</param>
  void CheckUniqueBeforeTableRefAttrChange(Guid sessionGuid, long tableRefID, long tableID);

  /// <summary>
  /// Обновить данные индексов, после того как ярлык стал ссылаться на другую таблицу.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу IMBASE</param>
  void UpdateAfterTableRefAttrChanged(Guid sessionGuid, long tableRefID);

  /// <summary>
  /// Проверить уникальность данных для случая, когда меняется значение произвольного атрибута у объекта типа "Ссылка на таблицу IMBASE".
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="value">Новое значение атрибута</param>
  void CheckUniqueBeforeAttrInTableRefChange(
    Guid sessionGuid,
    long tableRefID,
    long tableID,
    int attrID,
    object value);

  /// <summary>
  /// Проверить уникальность данных для случая, когда меняется значение произвольного атрибута у объекта типа "Таблица IMBASE".
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="value">Новое значение атрибута</param>
  void CheckUniqueBeforeAttrInTableChange(
    Guid sessionGuid,
    long tableID,
    int attrID,
    object value);

  /// <summary>
  /// Проверить уникальность данных для случая, когда удаляется произвольный атрибут у объекта типа "Ссылка на таблицу IMBASE".
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  void CheckUniqueBeforeAttrInTableRefDelete(
    Guid sessionGuid,
    long tableRefID,
    long tableID,
    int attrID);

  /// <summary>
  /// Проверить уникальность данных для случая, когда удаляется произвольный атрибут у объекта типа "Таблица IMBASE".
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  void CheckUniqueBeforeAttrInTableDelete(Guid sessionGuid, long tableID, int attrID);

  /// <summary>
  /// Обновить данные после изменения значения произвольного атрибута у объекта типа "Ссылка на таблицу IMBASE".
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="value">Новое значение атрибута</param>
  void UpdateAfterAttrInTableRefChanged(
    Guid sessionGuid,
    long tableRefID,
    long tableID,
    int attrID,
    object value);

  void UpdateAfterAttrInTableChanged(Guid sessionGuid, long tableID, int attrID);

  /// <summary>Проверка возможности удаления объекта.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="objTypeID">Тип удаляемого объекта</param>
  /// <returns>Возможность удаления атрибута</returns>
  bool CheckBeforeObjectDelete(Guid sessionGuid, long objID, int objTypeID);

  /// <summary>Обновить данные после удаления объекта IMBASE.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="objTypeID">Тип удаляемого объекта</param>
  void UpdateAfterObjectDelete(Guid sessionGuid, long objID, int objTypeID);

  /// <summary>Проверка возможности удаления атрибута.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <returns>Возможность удаления атрибута</returns>
  /// <remarks>Атрибут нельзя удалять, если он находится в списке атрибутов выполняемой задачи.
  /// Вызвать перед удалением атрибута из  системы. Если атрибут удалять нельзя, то нужно дождаться завершения задачи и повторить попытку</remarks>
  bool CheckBeforeAttributeDelete(Guid sessionGuid, int attrID);

  /// <summary>
  /// Удаление данных и индексов, после удаления атрибута из системы.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="attrID">Идентификатор удаленного атрибута</param>
  void UpdateAfterAttributeDelete(Guid sessionGuid, int attrID);

  /// <summary>
  /// Проверить на уникальность значения атрибутов объекта типа 'Ссылка на таблицу IMBASE', которые являются уникальными индексами каталога.
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="catalogID">Идентификатор каталога</param>
  /// <param name="values">Список пар 'идентификатор атрибута - значение'</param>
  /// <returns>Список идентификаторов атрибутов, которые имеют неуникальные значения</returns>
  List<int> CheckUniqueBeforeTableRefCreate(
    Guid sessionGuid,
    long catalogID,
    Dictionary<int, object> values);

  /// <summary>
  /// Проверить на уникальность значения атрибутов объекта типа 'Ссылка на таблицу IMBASE' и значения в таблице, которые являются уникальными индексами каталога.
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="catalogID">Идентификатор каталога</param>
  /// <param name="values">Список пар 'идентификатор атрибута - значение'</param>
  /// <param name="dtData">Таблица уникальных индексов с их значениями</param>
  /// <param name="notUniqueColumns">Список колонок таблицы, в которых есть неуникальные данные</param>
  /// <returns>Список идентификаторов атрибутов, которые имеют неуникальные значения</returns>
  List<int> CheckUniqueBeforeTableRefCreate(
    Guid sessionGuid,
    long catalogID,
    Dictionary<int, object> values,
    DataTable dtData,
    out List<int> notUniqueColumns);

  /// <summary>
  /// Обновить данные индексов для ссылки на таблицу IMBASE.
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу</param>
  /// <param name="isNewObj">Создается новый объект или обновляется существующий</param>
  void UpdateAfterTableRefCreated(Guid sessionGuid, Guid taskGuid, long tableRefID, bool isNewObj);

  /// <summary>
  /// Проверка уникальности данных копируемых/перемещаемых объектов IMBASE.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogID">Идентификатор каталога</param>
  /// <param name="objIDs">Список идентификаторов копируемых/перемещаемых объектов IMBASE</param>
  /// <param name="isCopy">Операция копирования</param>
  /// <returns>Список идентификаторов ярлыков, которые ссылаются на одинаковые таблицы</returns>
  List<long> CheckUniqueBeforeCopyMove(
    Guid sessionGuid,
    long catalogID,
    List<long> objIDs,
    bool isCopy);

  /// <summary>
  /// Обновить данные после копирования/перемещения объектов IMBASE.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="oldCatalogID">Идентификатор старого каталога (для перемещения)</param>
  /// <param name="newCatalogID">Идентификатор нового каталога</param>
  /// <param name="objIDs">Список идентификаторов скопированных/перемещенных объектов</param>
  void UpdateAfterCopiedMoved(
    Guid sessionGuid,
    Guid taskGuid,
    long oldCatalogID,
    long newCatalogID,
    List<long> objIDs);

  /// <summary>
  /// Проверить уникальность данных при редактировании таблицы/ссылки на таблицу.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="dtAttrs">Таблица с настройками атрибутов</param>
  /// <param name="dtData">Таблица с данными</param>
  /// <param name="uIndexes">Список неуникальных индексов</param>
  /// <param name="keys">Список номеров строк с неуникальными значениями</param>
  /// <returns>Таблица с информацией о неуникальных данных в других ссылках</returns>
  DataTable CheckUniqueBeforeTableDataChange(
    Guid sessionGuid,
    long tableID,
    DataTable dtAttrs,
    DataTable dtData,
    out List<int> uIndexes,
    out List<long> keys);

  /// <summary>
  /// Обновить данные для всех ссылок, которые ссылаются на указанную таблицу, после изменения данных в таблице.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="taskGuid">Глобальный идентификатор задачи</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="deletedRowNums">Список удаленных строк</param>
  /// <param name="deletedIndexes">Список удаленных индексов (удаленные столбцы из таблицы, которые являются индексами)</param>
  void UpdateAfterTableDataChanged(
    Guid sessionGuid,
    Guid taskGuid,
    long tableID,
    List<long> deletedRowNums,
    List<int> deletedIndexes);

  /// <summary>
  /// Обновление данных индексов, после реструктуризации таблицы.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogID">Идентификатор каталога, которому принадлежат объекты типа "Ссылка на таблицу IMBASE"</param>
  /// <param name="tableRefIDs">Список идентификаторов объектов типа "Ссылка на таблицу IMBASE", ссылающиеся на рассматриваемую таблицу</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  /// <param name="dtAttrs">Таблица настроек атрибутов</param>
  /// <param name="dtData">Таблица данных</param>
  /// <param name="attrIDs">Список идентификаторов атрибутов, значения которых нужно обновить</param>
  void UpdateAfterRestructured(
    Guid sessionGuid,
    long catalogID,
    List<long> tableRefIDs,
    long tableID,
    DataTable dtAttrs,
    DataTable dtData,
    List<int> attrIDs);

  /// <summary>
  /// Проверка уникальности данных перед завершением редактирования ссылки на таблицу IMBASE.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу</param>
  /// <param name="uIndexes">Список неуникальных индексов</param>
  /// <param name="keys">Список номеров строк с неуникальными значениями</param>
  /// <returns>Таблица с информацией о неуникальных данных в других ссылках</returns>
  DataTable CheckUniqueBeforeTableRefCheckIn(
    Guid sessionGuid,
    long tableRefID,
    out List<int> uIndexes,
    out List<long> keys);

  /// <summary>
  /// Обновить данные после завершения редактирования объекта типа "Ссылка на таблицу IMBASE".
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу IMBASE</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  void UpdateAfterTableRefCheckIn(Guid sessionGuid, long tableRefID, long tableID);

  /// <summary>
  /// Проверить уникальность данных перед завершением редактирования объекта типа "Таблица IMBASE".
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableID">Идентификатор таблицы IMBASE</param>
  /// <param name="uIndexes">Список неуникальных индексов</param>
  /// <param name="keys">Список номеров строк с неуникальными значениями</param>
  /// <returns>Таблица с информацией о неуникальных данных в других ссылках</returns>
  DataTable CheckUniqueBeforeTableCheckIn(
    Guid sessionGuid,
    long tableID,
    out List<int> uIndexes,
    out List<long> keys);

  /// <summary>
  /// Обновить данные после завершения редактирования объекта типа "Таблица IMBASE".
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  void UpdateAfterTableCheckIn(Guid sessionGuid, long tableID);

  /// <summary>Получить список индексов.</summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="sourceID">Идентификатор каталога. Если идентификатор каталога &gt; -1, то получаем индексы соответствующего каталога</param>
  /// <param name="colsNames">Список наименований колонок</param>
  /// <returns>Список индексов</returns>
  DataTable GetIndexes(Guid sessionGuid, long sourceID, string[] colsNames);

  /// <summary>Получить список индексов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogIDs">Список идентификаторов каталогов. Если null, то для всех каталогов</param>
  /// <param name="colsNames">Список наименований колонок
  /// Если catalogsIDs == null и colsNames содержит только IndexesField.F_ATTRIBUTE_ID, то выбираются только уникальные значения идентификаторов атрибутов</param>
  /// <returns>Таблица с данными индексов</returns>
  DataTable GetIndexes(Guid sessionGuid, List<long> catalogIDs, string[] colsNames = null);

  /// <summary>Получить список уникальных индексов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogIDs">Список идентификаторов каталогов</param>
  /// <param name="colsNames">Список наименований колонок</param>
  /// <returns>Таблица с данными уникальных индексов</returns>
  DataTable GetUniqueIndexes(Guid sessionGuid, List<long> catalogIDs, string[] colsNames = null);

  /// <summary>Поиск по значению индекса.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="attrID">Идентификатор индекса</param>
  /// <param name="request">Строка запроса</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу</param>
  /// <param name="recID">Номер записи</param>
  /// <returns>Результат поиска</returns>
  bool FindByIndex(
    Guid sessionGuid,
    int attrID,
    string request,
    out long tableRefID,
    out long recID);

  /// <summary>Поиск по значению индекса.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogID">Идентификатор каталога</param>
  /// <param name="attrID">Идентификатор индекса</param>
  /// <param name="request">Строка запроса</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу</param>
  /// <param name="recID">Номер записи</param>
  /// <returns>Результат поиска</returns>
  bool FindByIndex(
    Guid sessionGuid,
    long catalogID,
    int attrID,
    string request,
    out long tableRefID,
    out long recID);

  /// <summary>Найти данные.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="sourceID">Идентификатор каталога. Если идентификатор каталога &gt; -1, то ищем в пределах соответствующего каталога</param>
  /// <param name="attrID">Идентификатор индекса</param>
  /// <param name="colsNames">Список наименований колонок</param>
  /// <param name="request">Строка запроса</param>
  /// <param name="sa">Точность совпадения</param>
  /// <returns>Полученные данные</returns>
  [Obsolete("Использовать функцию DataTable Search(Guid sessionGuid, List<Int64> catalogIDs, Int32 attrID, string[] colsNames, string request, SearchesAccuracy sa)")]
  DataTable Search(
    Guid sessionGuid,
    long sourceID,
    int attrID,
    string[] colsNames,
    string request,
    SearchesAccuracy sa);

  /// <summary>Найти данные.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogIDs">Список идентификаторов каталогов. Если список заполнен, то ищем в пределах указанных каталогов</param>
  /// <param name="attrID">Идентификатор индекса</param>
  /// <param name="colsNames">Список наименований колонок</param>
  /// <param name="request">Строка запроса</param>
  /// <param name="sa">Точность совпадения</param>
  /// <returns>Полученные данные</returns>
  DataTable Search(
    Guid sessionGuid,
    List<long> catalogIDs,
    int attrID,
    string[] colsNames,
    string request,
    SearchesAccuracy sa);

  /// <summary>Найти данные.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogID">Идентификаторов каталога</param>
  /// <param name="attrID">Идентификатор индекса</param>
  /// <param name="tableID">Идентификатор таблицы, от которой был начат поиск</param>
  /// <param name="colsNames">Список наименований колонок</param>
  /// <param name="request">Строка запроса</param>
  /// <param name="sa">Точность совпадения</param>
  /// <returns>Полученные данные</returns>
  DataTable Search(
    Guid sessionGuid,
    long catalogID,
    int attrID,
    long tableID,
    string[] colsNames,
    string request,
    SearchesAccuracy sa);

  /// <summary>
  /// Получение неуникальных значений указанного индекса в пределах указанного каталога.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogID">Идентификатор каталога</param>
  /// <param name="attrID">Иденификатор атрибута</param>
  /// <returns>Таблица неуникальных значений с идентификаторами ссылок на таблицы IMBASE</returns>
  DataTable GetNotUniqueValues(Guid sessionGuid, long catalogID, int attrID);

  /// <summary>Быстрый поиск для технологических объектов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
  /// <param name="catalogIDs">Список идентификаторов каталогов</param>
  /// <param name="request">Строка запроса</param>
  /// <param name="dtFilter">Данные фильтра</param>
  /// <param name="recordCount">Количество строк, которые нужно найти (не более указанного количества)</param>
  /// <returns>Результат поиска</returns>
  DataTable QuickSearch(
    Guid sessionGuid,
    List<long> catalogIDs,
    string request,
    DataTable dtFilter,
    int recordCount);

  /// <summary>
  /// Обновление базы.
  /// Переход на новые индексы.
  /// </summary>
  void UpdateBase();
}
