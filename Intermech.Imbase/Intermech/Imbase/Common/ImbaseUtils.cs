// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Common.ImbaseUtils
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Common;

public class ImbaseUtils
{
  public static List<long> GetCatalogIDForObjType(int[] objTypeIDs, IUserSession session)
  {
    List<long> catalogIdForObjType = new List<long>();
    if (objTypeIDs == null || objTypeIDs.Length == 0)
      return catalogIdForObjType;
    List<int> intList = new List<int>(objTypeIDs.Length);
    foreach (int objTypeId in objTypeIDs)
    {
      if (objTypeId != -1 && !intList.Contains(objTypeId))
        intList.Add(objTypeId);
    }
    if (intList.Count == 0 || !(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
      return catalogIdForObjType;
    DataTable toTable = (DataTable) null;
    foreach (int needType in intList)
    {
      DataTable foldersForCreateType = customService.GetFoldersForCreateType(session.SessionGUID, (object) needType, (long[]) null, false, false);
      if (foldersForCreateType != null)
      {
        if (toTable != null && toTable.Rows.Count > 0)
          DataSetProcessor.AddTable(toTable, foldersForCreateType, false);
        else
          toTable = foldersForCreateType;
      }
    }
    if (toTable == null)
      return catalogIdForObjType;
    toTable.AcceptChanges();
    List<string> stringList = new List<string>();
    bool flag = false;
    if (toTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) toTable.Rows)
      {
        string str1 = row["F_PATH"].ToString();
        if (!(str1 == string.Empty))
        {
          string str2 = str1.Substring(0, 2);
          if (str2 == str1)
          {
            long result = 0;
            if (row["F_OBJECT_ID"] != DBNull.Value && row["F_OBJECT_ID"] != null)
            {
              long.TryParse(row["F_OBJECT_ID"].ToString(), out result);
              if (result != 0L)
                catalogIdForObjType.Add(result);
              else
                continue;
            }
          }
          else
            flag = true;
          if (!stringList.Contains(str2))
            stringList.Add(str2);
        }
      }
    }
    if (!flag || stringList.Count == 0)
      return catalogIdForObjType;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    if (objectCollection == null)
      return catalogIdForObjType;
    catalogIdForObjType.Clear();
    DBRecordSetParams paramSet = new DBRecordSetParams(new List<ConditionStructure>()
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) stringList.ToArray(), LogicalOperators.NONE, 0, true)
    }.ToArray(), new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    }.ToArray());
    DataTable dataTable = objectCollection.Select(paramSet);
    if (dataTable != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row != null && row[0] != null && row[0] != DBNull.Value)
        {
          long result = 0;
          long.TryParse(row[0].ToString(), out result);
          if (result != 0L)
            catalogIdForObjType.Add(result);
        }
      }
    }
    return catalogIdForObjType;
  }
}
