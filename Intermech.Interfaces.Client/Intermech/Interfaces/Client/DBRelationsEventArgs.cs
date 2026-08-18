// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBRelationsEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Список идентификаторов связей, с которыми произошло некоторое событие
/// </summary>
[Serializable]
public class DBRelationsEventArgs : NotificationEventArgs, IDataMergingSupport
{
  /// <summary>Код выполненной команды "Навигатора"</summary>
  private NavigatorRelationCommand _relationCommand;
  /// <summary>Список идентификаторов связей</summary>
  private IList<long> _relationIDs;
  /// <summary>Список идентификаторов связей</summary>
  private List<int> _knownRelationTypes = new List<int>();
  /// <summary>
  /// Информация по связям
  /// [(Int64)Идентификатор связи] =&gt; [(RelInfo)Краткое описание связи]
  /// </summary>
  private Dictionary<long, RelInfo> _relationInfo = new Dictionary<long, RelInfo>();
  /// <summary>
  /// Список присутствующих идентификаторов родительских объектов
  /// </summary>
  private List<long> _projIDs = new List<long>();

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationID">Идентификатор связи</param>
  public DBRelationsEventArgs(string eventName, long relationID)
    : this(eventName, relationID, -1)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="relationType"></param>
  public DBRelationsEventArgs(string eventName, long relationID, int relationType)
    : this(eventName, (IList<long>) new long[1]
    {
      relationID
    }, (IList<long>) new long[1], (IList<int>) new int[1]
    {
      -1
    }, (IList<int>) new int[1]{ relationType }, NavigatorRelationCommand.Unknown)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="relType">Идентификатор типа связи</param>
  public DBRelationsEventArgs(string eventName, long relationID, long projID, int relType)
    : this(eventName, (IList<long>) new long[1]
    {
      relationID
    }, (IList<long>) new long[1]{ projID }, (IList<int>) new int[1]
    {
      -1
    }, (IList<int>) new int[1]{ relType }, NavigatorRelationCommand.Unknown)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="projTypeID">Идентификатор типа родительского объекта</param>
  /// <param name="relType">Идентификатор типа связи</param>
  public DBRelationsEventArgs(
    string eventName,
    long relationID,
    long projID,
    int projTypeID,
    int relType)
    : this(eventName, (IList<long>) new long[1]
    {
      relationID
    }, (IList<long>) new long[1]{ projID }, (IList<int>) new int[1]
    {
      projTypeID
    }, (IList<int>) new int[1]{ relType }, NavigatorRelationCommand.Unknown)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="projTypeID">Идентификатор типа родительского объекта</param>
  /// <param name="relType">Идентификатор типа связи</param>
  /// <param name="relCommand">Код выполненной команды "Навигатора"</param>
  public DBRelationsEventArgs(
    string eventName,
    long relationID,
    long projID,
    int projTypeID,
    int relType,
    NavigatorRelationCommand relCommand)
    : this(eventName, (IList<long>) new long[1]
    {
      relationID
    }, (IList<long>) new long[1]{ projID }, (IList<int>) new int[1]
    {
      projTypeID
    }, (IList<int>) new int[1]{ relType }, relCommand)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationIDs">Список идентификаторов связей</param>
  public DBRelationsEventArgs(string eventName, IList<long> relationIDs)
    : this(eventName, relationIDs, (IList<long>) null, (IList<int>) null, (IList<int>) null, NavigatorRelationCommand.Unknown)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationIDs">Список идентификаторов связей</param>
  /// <param name="projIDs">Список идентификаторов версий родительских объектов или null</param>
  /// <param name="projTypeIDs">Список идентификаторов типов родительских объектов или null</param>
  /// <param name="relTypeIDs">Список идентификаторов типов связей или null</param>
  public DBRelationsEventArgs(
    string eventName,
    IList<long> relationIDs,
    IList<long> projIDs,
    IList<int> projTypeIDs,
    IList<int> relTypeIDs)
    : this(eventName, relationIDs, projIDs, projTypeIDs, relTypeIDs, NavigatorRelationCommand.Unknown)
  {
  }

  /// <summary>
  /// Подготовить список идентификаторов связей, с которыми произошло некоторое событие
  /// </summary>
  /// <param name="eventName">Наименование события</param>
  /// <param name="relationIDs">Список идентификаторов связей</param>
  /// <param name="projIDs">Список идентификаторов версий родительских объектов или null</param>
  /// <param name="projTypeIDs">Список идентификаторов типов родительских объектов или null</param>
  /// <param name="relTypeIDs">Список идентификаторов типов связей или null</param>
  /// <param name="relationCommand">Код выполненной команды "Навигатора"</param>
  public DBRelationsEventArgs(
    string eventName,
    IList<long> relationIDs,
    IList<long> projIDs,
    IList<int> projTypeIDs,
    IList<int> relTypeIDs,
    NavigatorRelationCommand relationCommand)
    : base(eventName)
  {
    this._relationIDs = relationIDs != null ? (IList<long>) new List<long>((IEnumerable<long>) relationIDs) : (IList<long>) new List<long>();
    this._relationCommand = relationCommand;
    if (projIDs != null && this._relationIDs.Count != projIDs.Count)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces.Client_151"));
    if (projIDs != null && projTypeIDs != null && projTypeIDs.Count != projIDs.Count)
      throw new ArgumentException(nameof (projTypeIDs));
    if (relTypeIDs != null && this._relationIDs.Count != relTypeIDs.Count)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Interfaces.Client_152"));
    for (int index = 0; index < this._relationIDs.Count; ++index)
    {
      long relationId = this._relationIDs[index];
      long projId = projIDs != null ? projIDs[index] : 0L;
      int projTypeID = projTypeIDs != null ? projTypeIDs[index] : -1;
      int relType = relTypeIDs != null ? relTypeIDs[index] : -1;
      if (relType != -1 && !this._knownRelationTypes.Contains(relType))
        this._knownRelationTypes.Add(relType);
      this._relationInfo[relationId] = new RelInfo(projId, projTypeID, relType);
      if (!Consts.IsUndefinedObjectId(projId) && this._projIDs.IndexOf(projId) < 0)
        this._projIDs.Add(projId);
    }
    if (!(eventName == "RelationsCreated"))
      return;
    if (this._projIDs == null || this._projIDs.Count != this._relationIDs.Count)
      this._projIDs = projIDs == null || projIDs.Count < this._relationIDs.Count ? new List<long>(this._relationIDs.Select<long, long>((Func<long, long>) (o => 0L))) : projIDs.Take<long>(this._relationIDs.Count).ToList<long>();
    if (this._knownRelationTypes == null || this._knownRelationTypes.Count != this._relationIDs.Count)
      this._knownRelationTypes = relTypeIDs == null || relTypeIDs.Count < this._relationIDs.Count ? new List<int>(this._relationIDs.Select<long, int>((Func<long, int>) (o => -1))) : relTypeIDs.Take<int>(this._relationIDs.Count).ToList<int>();
    for (int index = 0; index < this._relationIDs.Count; ++index)
    {
      if (ObjectHelper.IsUnknownObjectVersionID(this._projIDs[index]) || RelationTypeHelper.IsUnknownRelationTypeID(this._knownRelationTypes[index]))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelation relation = sessionKeeper.Session.GetRelation(this._relationIDs[index], false);
          if (relation != null)
          {
            this._projIDs[index] = relation.ProjID;
            this._knownRelationTypes[index] = relation.RelationType;
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(relation.ProjID);
            if (!objectInfo.Empty)
              this._relationInfo[(long) index] = new RelInfo(relation.ProjID, objectInfo.ObjectTypeID, relation.RelationType);
          }
        }
      }
    }
  }

  /// <summary>
  /// Возвращает список идентификаторов связей, с которыми произошло событие
  /// </summary>
  public IList<long> RelationIDs
  {
    [DebuggerStepThrough] get => this._relationIDs;
  }

  /// <summary>
  /// Список присутствующих идентификаторов родительских объектов
  /// </summary>
  public List<long> ProjIDs
  {
    [DebuggerStepThrough] get => this._projIDs;
  }

  /// <summary>Уникальный список присутствующих типов связей</summary>
  public List<int> KnownRelationTypes
  {
    [DebuggerStepThrough] get => this._knownRelationTypes;
  }

  /// <summary>Код выполненной команды "Навигатора"</summary>
  public NavigatorRelationCommand RelationCommand
  {
    [DebuggerStepThrough] get => this._relationCommand;
    set => this._relationCommand = value;
  }

  /// <summary>
  /// Должно быть заполнено для оптимизации вставки связей при включенной группировке по типам.
  /// </summary>
  public Dictionary<long, int> PartTypeDictionaryByRelationID { get; set; }

  /// <summary>
  /// Получить идентификатор версии родительского объекта для указанной связи
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <returns>Идентификатор версии родительского объекта для указанной связи</returns>
  public long GetProjID(long prjLinkID)
  {
    return this._relationInfo.ContainsKey(prjLinkID) ? this._relationInfo[prjLinkID].ProjID : 0L;
  }

  /// <summary>
  /// Получить идентификатор типа родительского объекта для указанной связи
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <returns>Идентификатор типа родительского объекта для указанной связи</returns>
  public int GetProjTypeID4Link(long prjLinkID)
  {
    return this._relationInfo.ContainsKey(prjLinkID) ? this._relationInfo[prjLinkID].ProjTypeID : -1;
  }

  /// <summary>
  /// Получить идентификатор типа указанного родительского объекта
  /// </summary>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <returns>Идентификатор типа родительского объекта для указанной связи</returns>
  public int GetProjTypeID(long projID)
  {
    foreach (KeyValuePair<long, RelInfo> keyValuePair in this._relationInfo)
    {
      if (keyValuePair.Value.ProjID == projID && keyValuePair.Value.ProjTypeID != -1)
        return keyValuePair.Value.ProjTypeID;
    }
    return -1;
  }

  /// <summary>Получить идентификатор типа указанной связи</summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <returns>Идентификатор типа указанной связи</returns>
  public int GetRelationType(long prjLinkID)
  {
    return this._relationInfo.ContainsKey(prjLinkID) ? this._relationInfo[prjLinkID].RelType : -1;
  }

  /// <summary>
  /// Проверить, есть ли в аргументах события хотя бы одна связь,
  /// у которой - указанные родительский объект и тип связи
  /// </summary>
  /// <param name="projID">Родительский тип объекта</param>
  /// <param name="relTypeID">Тип связи</param>
  /// <returns>true - в коллекции есть как минимум одна связь с указанными параметрами</returns>
  public bool Exists(long projID, int relTypeID)
  {
    return this._relationInfo.ContainsValue(new RelInfo(projID, -1, relTypeID));
  }

  /// <summary>
  /// Проверить, есть ли хотя бы один пустой элемент в словарике
  /// </summary>
  /// <returns>true - как минимум для одной связи не задан родительский объект или тип связи</returns>
  public bool HasEmptyItems()
  {
    foreach (KeyValuePair<long, RelInfo> keyValuePair in this._relationInfo)
    {
      if (keyValuePair.Value.ProjID == 0L || keyValuePair.Value.RelType == -1)
        return true;
    }
    return false;
  }

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public virtual bool MergeWith(object obj)
  {
    if (!(obj is DBRelationsEventArgs relationsEventArgs))
      return false;
    for (int index = 0; index < relationsEventArgs._relationIDs.Count; ++index)
    {
      long relationId = relationsEventArgs._relationIDs[index];
      if (this._relationIDs.IndexOf(relationId) < 0)
      {
        this._relationIDs.Add(relationId);
        long projId = relationsEventArgs.GetProjID(relationId);
        if (!Consts.IsUndefinedObjectId(projId) && this._projIDs.IndexOf(projId) < 0)
          this._projIDs.Add(projId);
        int projTypeId = relationsEventArgs.GetProjTypeID(relationId);
        int relationType = relationsEventArgs.GetRelationType(relationId);
        this._relationInfo[relationId] = new RelInfo(projId, projTypeId, relationType);
      }
    }
    return true;
  }

  /// <summary>Количество заданий в аргументах</summary>
  public override int ItemsCount
  {
    get
    {
      int num = 0;
      if (this._relationIDs != null)
        num += this._relationIDs.Count;
      return num <= 0 ? base.ItemsCount : num;
    }
  }

  /// <summary>
  /// Проверить, поддерживается ли указанный режим оптимизации аргументами события и,
  /// в случае необходимости, вернуть максимальный уровень поддерживаемой оптимизации
  /// </summary>
  /// <param name="mode">Запрашиваемый режим оптимизации</param>
  /// <returns>Допустимый режим оптимизации</returns>
  public override NotificationServiceMode GetSupportedOptimization(NotificationServiceMode mode)
  {
    return mode;
  }
}
