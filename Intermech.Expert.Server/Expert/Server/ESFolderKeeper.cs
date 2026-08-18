// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ESFolderKeeper
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

internal class ESFolderKeeper
{
  private static readonly ESFolderKeeper esfc = new ESFolderKeeper();
  internal ConcurrentDictionary<long, ESFolderInfo> folderDict;
  internal ConcurrentDictionary<long, List<long>> esObjsFolders = new ConcurrentDictionary<long, List<long>>();

  public static ESFolderKeeper Keeper => ESFolderKeeper.esfc;

  internal void LoadAllFormulae(IUserSession ius)
  {
    if (this.folderDict != null)
      this.folderDict.Clear();
    else
      this.folderDict = new ConcurrentDictionary<long, ESFolderInfo>();
    DataTable dataTable1 = ius.GetObjectCollection(ExpertConsts.Consts.objESFolder).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    }));
    if (dataTable1 == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      IDBObject dbObject = ius.GetObject(int64, false);
      ExpertFolder expertFolder = dbObject != null ? dbObject as ExpertFolder : (ExpertFolder) null;
      if (expertFolder != null)
      {
        expertFolder.Load();
        TempFormula tempFormula = expertFolder.GetTempFormula();
        string caption = dbObject.Caption;
        ESFolderInfo esFolderInfo = new ESFolderInfo(Math.Abs(int64), caption, tempFormula);
        this.folderDict.GetOrAdd(int64, esFolderInfo);
      }
    }
    IDBRelationCollection relationCollection = ius.GetRelationCollection(ExpertConsts.Consts.linkSimpleSortId);
    foreach (long key1 in (IEnumerable<long>) this.folderDict.Keys)
    {
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
      });
      DataTable dataTable2 = relationCollection.ConsistFrom(paramSet, key1);
      if (dataTable2 != null && dataTable2.Rows.Count > 0)
      {
        HashSet<long> longSet = new HashSet<long>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          long num = Math.Abs(Convert.ToInt64(row[0]));
          longSet.Add(num);
        }
        foreach (long key2 in longSet)
        {
          if (this.folderDict.ContainsKey(key2))
            this.folderDict[key2].AddParentFolder(key1);
        }
      }
    }
  }

  internal Dictionary<long, ESFolderInfo> GetAllFoldersForESObject(IUserSession ius, long objID)
  {
    List<long> longList;
    if (!this.esObjsFolders.ContainsKey(Math.Abs(objID)))
    {
      IDBRelationCollection relationCollection = ius.GetRelationCollection(ExpertConsts.Consts.linkSimpleSortId);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.Equal, (object) ExpertConsts.Consts.objESFolder, LogicalOperators.NONE, 0, false)
      }, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
      });
      longList = new List<long>();
      DataTable dataTable = relationCollection.EntersInVersion(paramSet, Math.Abs(objID));
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          longList.Add(int64);
        }
      }
      this.esObjsFolders.GetOrAdd(Math.Abs(objID), longList);
    }
    else
      longList = this.esObjsFolders[Math.Abs(objID)];
    if (longList.Count == 0)
      return (Dictionary<long, ESFolderInfo>) null;
    Dictionary<long, ESFolderInfo> folderList = new Dictionary<long, ESFolderInfo>();
    foreach (long folderId in longList)
      this.CollectFoldersList(folderId, folderList);
    return folderList;
  }

  internal void CollectFoldersList(long folderId, Dictionary<long, ESFolderInfo> folderList)
  {
    if (!this.folderDict.ContainsKey(folderId) || folderList.ContainsKey(folderId))
      return;
    ESFolderInfo esFolderInfo = this.folderDict[folderId];
    folderList.Add(folderId, esFolderInfo);
    if (esFolderInfo.ParentFolders == null)
      return;
    foreach (long parentFolder in esFolderInfo.ParentFolders)
      this.CollectFoldersList(parentFolder, folderList);
  }

  internal void RemoveFromFolderCache(long objId)
  {
    long key = Math.Abs(objId);
    List<long> longList = (List<long>) null;
    if (!this.esObjsFolders.ContainsKey(key))
      return;
    this.esObjsFolders.TryRemove(key, out longList);
  }
}
