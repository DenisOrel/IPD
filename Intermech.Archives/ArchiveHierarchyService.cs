// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchiveHierarchyService
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Сервис, позволяющий получить информацию о вышестоящем архиве
/// </summary>
public class ArchiveHierarchyService
{
  /// <summary>
  /// Словарь содержащий информацию о родительском архиве. long ChildId, long ParentInfo
  /// </summary>
  public Dictionary<long, DBSimpleObject> _archivesParentsCache = new Dictionary<long, DBSimpleObject>();

  /// <summary>Конструктор.</summary>
  public ArchiveHierarchyService(SessionKeeper sk) => this.FillCache(sk);

  /// <summary>Заполняем кэш</summary>
  /// <param name="sk"></param>
  private void FillCache(SessionKeeper sk)
  {
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(ConstsHolder.ArcTypeID).ToArray(), LogicalOperators.NONE, 0, true)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -21, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0)
    });
    DataTable dataTable1 = sk.Session.RelationsSelect(sk.Session.IdentHelper.SimpleRelationTypeID, dbRecordSetParams);
    if (dataTable1 == null || dataTable1.Rows.Count == 0)
      return;
    Dictionary<long, long> dictionary = new Dictionary<long, long>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
    {
      if (row[0] != null && !(row[0] is DBNull) && row[1] != null && !(row[1] is DBNull) && !dictionary.TryGetValue(Convert.ToInt64(row[0]), out long _))
        dictionary.Add(Convert.ToInt64(row[0]), Convert.ToInt64(row[1]));
    }
    dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) dictionary.Values.Distinct<long>().ToArray<long>(), LogicalOperators.NONE, 0, true)
    }, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    });
    DataTable dataTable2 = sk.Session.ObjectsSelect(ConstsHolder.ArcTypeID, dbRecordSetParams);
    List<DBSimpleObject> source = new List<DBSimpleObject>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
    {
      if (row[0] != null && !(row[0] is DBNull))
      {
        long int64 = Convert.ToInt64(row[0]);
        int int32 = Convert.ToInt32(row[1]);
        string caption = Convert.ToString(row[2]);
        source.Add(new DBSimpleObject(int64, int32, caption));
      }
    }
    foreach (KeyValuePair<long, long> keyValuePair in dictionary)
    {
      KeyValuePair<long, long> pair = keyValuePair;
      DBSimpleObject dbSimpleObject = source.First<DBSimpleObject>((System.Func<DBSimpleObject, bool>) (x => x.ObjectID == pair.Value));
      if (!this._archivesParentsCache.TryGetValue(pair.Key, out DBSimpleObject _))
        this._archivesParentsCache.Add(pair.Key, dbSimpleObject);
    }
  }

  /// <summary>
  /// Возвращаем вышестоящий архив для указанного архива
  /// Либо класс с пустыми значениями, если такого архива нет в списке (а значит, его родитель - это абстрактный тип Все архивы)
  /// </summary>
  /// <param name="archiveId"></param>
  /// <returns>Возвращаем вышестоящий архив для указанного архива.
  /// Либо класс с пустыми значениями, если такого архива нет в списке (а значит, его родитель - это абстрактный тип Все архивы) </returns>
  public DBSimpleObject GetArchiveParentFromCache(long archiveId)
  {
    DBSimpleObject dbSimpleObject;
    return this._archivesParentsCache.TryGetValue(archiveId, out dbSimpleObject) ? dbSimpleObject : new DBSimpleObject(0L, -1, string.Empty);
  }

  /// <summary>Добавляем архив в кэш</summary>
  /// <param name="archiveId">ИД добавляемого архива</param>
  public void AddArchiveToCashe(long archiveId, long parentArchiveId, int parentTypeId)
  {
    this._archivesParentsCache.Remove(archiveId);
    if (parentArchiveId == 0L)
      return;
    this._archivesParentsCache.Add(archiveId, new DBSimpleObject(parentArchiveId, parentTypeId));
  }
}
