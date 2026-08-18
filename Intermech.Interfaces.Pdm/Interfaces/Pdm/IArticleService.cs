// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IArticleService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// 
/// </summary>
public interface IArticleService
{
  /// <summary>
  /// Функция ищет конструкторский документ по его идентификационным параметрам и возвращает ид. версии найденного документа.
  /// </summary>
  /// <param name="designation">Обозначение</param>
  /// <param name="name">Наименование</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Ид. найденного конструкторского документа. Consts.UnknownObjectId если документ не найден.</returns>
  long FindDocumentID(
    string designation,
    string name,
    string filtrationRuleSettings,
    object session);

  /// <summary>
  /// Функция ищет конструкторский документ по его идентификационным параметрам и возвращает ид. версии найденного документа.
  /// </summary>
  /// <param name="designation">Обозначение</param>
  /// <param name="name">Наименование</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Интерфейс объекта. null если изделие не найдено.</returns>
  IDBObject FindDocumentObject(
    string designation,
    string name,
    string filtrationRuleSettings,
    object session);

  /// <summary>
  /// Функция ищет изделие по его идентификационным параметрам и возвращает ид. версии найденного изделия.
  /// Поиск идет сперва в изделиях, и если такое изделие не найдено - в материалах.
  /// </summary>
  /// <param name="designation">Обозначение изделия</param>
  /// <param name="okpCode">Код ОКП</param>
  /// <param name="name">Наименование изделия</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Ид. найденного изделия. Consts.UnknownObjectId если изделие не найдено.</returns>
  long FindArticleID(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session);

  /// <summary>
  /// Функция ищет изделие по его идентификационным параметрам и возвращает ид. версии найденного изделия.
  /// </summary>
  /// <param name="designation">Обозначение изделия</param>
  /// <param name="okpCode">Код ОКП</param>
  /// <param name="name">Наименование изделия</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <param name="firstInMaterials">Производить поиск сперва в материалах, а после в изделиях</param>
  /// <returns>Ид. найденного изделия. Consts.UnknownObjectId если изделие не найдено.</returns>
  long FindArticleID(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session,
    bool firstInMaterials);

  /// <summary>
  /// Функция ищет изделие по его идентификационным параметрам и возвращает интерфейс обработчика данного найденного изделия.
  /// Поиск идет сперва в изделиях, и если такое изделие не найдено - в материалах.
  /// </summary>
  /// <param name="designation">Обозначение изделия</param>
  /// <param name="okpCode">Код ОКП</param>
  /// <param name="name">Наименование изделия</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Интерфейс объекта. null если изделие не найдено.</returns>
  IDBObject FindArticleObject(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session);

  /// <summary>
  /// Функция ищет изделие по его идентификационным параметрам и возвращает интерфейс обработчика данного найденного изделия.
  /// </summary>
  /// <param name="designation">Обозначение изделия</param>
  /// <param name="okpCode">Код ОКП</param>
  /// <param name="name">Наименование изделия</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <param name="firstInMaterials">Производить поиск сперва в материалах, а после в изделиях</param>
  /// <returns>Интерфейс объекта. null если изделие не найдено.</returns>
  IDBObject FindArticleObject(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session,
    bool firstInMaterials);

  /// <summary>
  /// Получает идентификаторы всех исполнений, выпускаемых по документу с идентификатором версии documentID
  /// </summary>
  /// <param name="documentID">Идентификатор документа (ObjectID)</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Массив идентификаторов исполнений,  Если их нет, то возвращает массив нулевой длины.</returns>
  long[] FindArticles(long documentID, string filtrationRuleSettings, object session);

  /// <summary>
  /// Получает идентификаторы всех исполнений, выпускаемых по документу с идентификатором версии documentID.
  /// Не фильтрует результат по настройкам фильтрации пользователя.
  /// </summary>
  /// <param name="documentID">Идентификатор документа (ObjectID)</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Массив идентификаторов исполнений,  Если их нет, то возвращает массив нулевой длины.</returns>
  long[] FindArticlesWithoutFiltration(long documentID, string versionsRule, object session);

  /// <summary>
  /// Получает идентификаторы всех исполнений группового изделия
  /// </summary>
  /// <param name="articleID">Идентификатор одного из исполнений группового изделия</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Массив идентификаторов исполнений,  Если их нет, то возвращает массив нулевой длины.</returns>
  long[] FindArticlesByGroupID(long articleID, object session);

  /// <summary>
  /// Получает идентификаторы всех исполнений группового изделия
  /// Не фильтрует результат по настройкам фильтрации пользователя.
  /// </summary>
  /// <param name="articleID">Идентификатор одного из исполнений группового изделия</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Массив идентификаторов исполнений,  Если их нет, то возвращает массив нулевой длины.</returns>
  long[] FindArticlesByGroupIDWithoutFiltration(long articleID, object session);

  /// <summary>
  /// Находит главный конструкторский документ по идентификатору изделия.
  /// </summary>
  /// <param name="articleID">Ид. версии изделия</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Возвращает ид. версии документа (ObjectID). Если документ взят на изменение данным пользователем,
  /// то возвращает ид. рабочей копии документа. Если документа нет, то возвращает Consts.UnknownObjectId. Если изделие безчертёжное,
  /// то возвращает 0.</returns>
  long FindMainDocumentID(long articleID, string filtrationRuleSettings, object session);

  /// <summary>
  /// Находит все конструкторские документы по идентификатору изделия.
  /// </summary>
  /// <param name="articleID">Ид. версии изделия</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Возвращает ид. версий найденных документов (ObjectID). Если их нет, то возвращает массив нулевой длины.</returns>
  long[] FindMainDocuments(long articleID, string filtrationRuleSettings, object session);

  /// <summary>
  /// Находит главный конструкторский документ по идентификатору изделия.
  /// </summary>
  /// <param name="articleID">Ид. версии изделия</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Возвращает интерфейс объекта-обработчика найденного документа. Если документа нет, то возвращает null.
  /// </returns>
  IDBObject FindMainDocument(long articleID, string filtrationRuleSettings, object session);

  /// <summary>
  /// Находит главные конструкторские документы по идентификаторам изделия.
  /// </summary>
  /// <param name="articleIDs">Массив с идентификаторами версий изделий</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Возвращает массив идентификаторов версий документов (ObjectID). Количество элементов
  /// результирующего массива совпадает с количеством запрашиваемых изделий. Если документ взят на изменение данным пользователем,
  /// то возвращает ид. рабочей копии документа. Если документа нет, то возвращает Consts.UnknownObjectId. Если изделие безчертёжное,
  /// то возвращает 0. </returns>
  long[] FindMainDocuments(long[] articleIDs, string filtrationRuleSettings, object session);

  /// <summary>
  /// Находит все главные конструкторские документы-чертежи по идентификаторам изделия.
  /// </summary>
  /// <param name="articleIDs">Массив с идентификаторами версий изделий</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Возвращает массив идентификаторов версий документов-чертежей (ObjectID).  Если документ взят на изменение данным пользователем,
  /// то возвращает ид. рабочей копии документа. Если документа нет, то возвращает Consts.UnknownObjectId. Если изделие безчертёжное,
  /// то возвращает 0. </returns>
  long[] FindMainDocumentIDsForAllDrawings(
    long[] articleIDs,
    string filtrationRuleSettings,
    object session);

  /// <summary>
  /// Для главного конструкторского документа documentID ищет базовое исполнение изделия и возвращает соотв. ссылку на объект.
  /// Базовым считается исполнение с одинаковым идентификационным атрибутом (такое же Обозначение, Код ОКП, Наименование).
  /// Если базовое исполнение не найдено, то возвращает null.
  /// </summary>
  /// <param name="documentID">Идентификатор документа (ObjectID)</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  IDBObject FindBaseArticle(long documentID, string filtrationRuleSettings, object session);

  /// <summary>
  /// Для главного конструкторского документа documentID ищет базовое исполнение изделия и возвращает соотв. ссылку на объект.
  /// Базовым считается исполнение с одинаковым идентификационным атрибутом, значение которого передаем в value).
  /// Если базовое исполнение не найдено, то возвращает null.
  /// </summary>
  /// <param name="documentID">Идентификатор документа (ObjectID)</param>
  /// <param name="value">значение атрибута "Обозначение" по которому вести поиск</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns></returns>
  IDBObject FindBaseArticleForValue(
    long documentID,
    string value,
    string filtrationRuleSettings,
    object session);

  /// <summary>Найти список исполнений</summary>
  /// <param name="articleID">ID версии изделия</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Вернет коллекцию ID версий исполнений</returns>
  List<long> GetListInstances(long articleID, object session);

  /// <summary>Найти список исполнений</summary>
  /// <param name="groupID">Значение атрибута "Идентификатор группового изделия"</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns>Вернет коллекцию ID версий исполнений</returns>
  List<long> GetListInstances(object groupID, object session);

  /// <summary>
  /// Получает идентификаторы всех исполнений, выпускаемых по документу с идентификатором версии documentID
  /// Нулевым элементом в возвращаемой коллекции стоит базовое исполнение.
  /// </summary>
  /// <param name="documentID">Идентификатор документа (ObjectID)</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns> Возвращает коллекцию исполнений. Если их нет, то возвращает массив нулевой длины.</returns>
  List<QuickObjectInfo> FindListInstances(
    long documentID,
    string filtrationRuleSettings,
    object session);

  /// <summary>Ищет материал среди объектов типа "Материал базовый"</summary>
  /// <param name="designation">Обозначение материала</param>
  /// <param name="okpCode">Код ОКП</param>
  /// <param name="name">Наименование материала</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns></returns>
  IDBObject FindMaterial(
    string designation,
    string okpCode,
    string name,
    string filtrationRuleSettings,
    object session);

  /// <summary>
  /// Ищет материал среди объектов указанного в параметрах типа materialType
  /// </summary>
  /// <param name="designation">Обозначение материала</param>
  /// <param name="okpCode">Код ОКП</param>
  /// <param name="name">Наименование материала</param>
  /// <param name="materialType">тип, среди объектов которого искать</param>
  /// <param name="filtrationRuleSettings">Правило фильтрации версий</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns></returns>
  IDBObject FindMaterial(
    string designation,
    string okpCode,
    string name,
    int materialType,
    string filtrationRuleSettings,
    object session);

  long GetMaterialID(string name, string filtrationRuleSettings, object session);

  long GetMaterialID(
    string name,
    string filtrationRuleSettings,
    object session,
    bool trueMaterialsOnly);

  /// <summary>Функция по идентификатору материала формирует строку*</summary>
  /// <param name="materialID">ID материала</param>
  /// <param name="session">Пользовательская сессия. В реализации службы выборок на
  /// стороне сервера предполагается, что данный параметр типа Guid. Для клиентской службы
  /// реализована обработка параметра как IUserSession. Т.е. при обращении к методам службы
  /// на сервере нужно передавать Guid пользовательской сессии, а при вызове методов службы
  /// на клиенте надо передать ссылку на интерфейс IUserSession</param>
  /// <returns></returns>
  string GetMaterialName(long materialID, object session);
}
