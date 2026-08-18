// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.IIMHIndexingService
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>
/// 
/// </summary>
public interface IIMHIndexingService
{
  /// <summary>Процент завершенности задачи.</summary>
  int Completed { get; }

  /// <summary>Наличие объекта среди уже индексируемых объектов.</summary>
  /// <returns>Результат проверки</returns>
  bool IsBusy { get; }

  /// <summary>
  /// 
  /// </summary>
  string Msg { get; }

  /// <summary>Добавить индексы.</summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="sourceID">Идентификатор объекта, от которого (вниз по иерархии) будет происходить индексирование</param>
  /// <param name="attrs">Список глобальных идентификаторов индексируемых атрибутов</param>
  void Add(Guid sessionGuid, long sourceID, Dictionary<string, List<Guid>> attrs);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sessionGuid"></param>
  /// <param name="sourceID"></param>
  void IndexingMaterial(Guid sessionGuid, long sourceID);

  /// <summary>Пометить как НЕ индексируемый в данный момент.</summary>
  void MarkAsFree();

  /// <summary>
  /// Удалить данные, относящиеся к указанному индексу и сам индекс.
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="sourceID">Идентификатор объекта, от которого (вниз по иерархии) происхоло индексирование</param>
  /// <param name="attrs">Список глобальных идентификаторов атрибутов</param>
  void RemoveIndexes(Guid sessionGuid, long sourceID, Dictionary<string, List<Guid>> attrs);

  /// <summary>
  /// Удалить данные.
  /// Данные могут относиться к каталогу, ссылке на таблицу, таблице.
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="sourceID">Идентификатор объекта</param>
  /// <param name="isTable">Таблица/ссылка на таблицу</param>
  void RemoveObject(Guid sessionGuid, long objID, bool isTable);

  /// <summary>Найти данные.</summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="sourceID">Идентификатор объекта в иерархии которого осуществляется поиск.
  /// Если идентификатор объекта &gt; -1, то ищем в иерархии соответствующего объекта</param>
  /// <param name="attrGuid">Глобальный идентификатор атрибута</param>
  /// <param name="colsNames">Список наименований колонок</param>
  /// <param name="Request">Строка запроса</param>
  /// <param name="sa">Точность совпадения</param>
  /// <returns>Полученные данные</returns>
  DataTable Search(
    Guid sessionGuid,
    long sourceID,
    Guid attrGuid,
    string[] colsNames,
    string request,
    SearchesAccuracy sa);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sessionGuid"></param>
  /// <param name="sourceID"></param>
  /// <param name="className"></param>
  /// <param name="conditions"></param>
  /// <returns></returns>
  List<long> SearchAssortmentData(
    Guid sessionGuid,
    long sourceID,
    string className,
    List<ConditionClass> conditions);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sessionGuid"></param>
  /// <param name="sourceID"></param>
  /// <param name="addedAttrs"></param>
  /// <param name="deletedAttrs"></param>
  void UpdateIndexes(
    Guid sessionGuid,
    long sourceID,
    Dictionary<string, List<Guid>> addedAttrs,
    Dictionary<string, List<Guid>> deletedAttrs);

  /// <summary>Обновить данные для таблицы.</summary>
  /// <param name="sessionGuid">Идентификатор сессии пользователя</param>
  /// <param name="tableID">Идентификатор таблицы</param>
  bool UpdateDataByTableID(Guid sessionGuid, long tableID);
}
