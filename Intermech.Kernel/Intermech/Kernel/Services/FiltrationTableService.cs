// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.FiltrationTableService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services;

public class FiltrationTableService : IFiltrationTableService
{
  public void AddValue(IDbManager db, long objectID, long filterID, string strValue)
  {
    db.ExecuteNonQuery("INSERT INTO IMS_ATTRFILTER_VALUE (F_OBJECT_ID, F_FILTER_ID, F_STRING_VALUE) VALUES (:objID, :filtID, :strValue)", db.Parameter("objID", (object) Math.Abs(objectID)), db.Parameter("filtID", (object) filterID), db.Parameter(nameof (strValue), (object) strValue));
  }

  public void DeleteValue(IDbManager db, long objectID, long filterID)
  {
    db.ExecuteNonQuery("DELETE FROM IMS_ATTRFILTER_VALUE WHERE F_OBJECT_ID = :objID AND F_FILTER_ID= :filtID", db.Parameter("objID", (object) Math.Abs(objectID)), db.Parameter("filtID", (object) filterID));
  }

  public void UpdateValue(IDbManager db, long objectID, long filterID, string strValue)
  {
    db.ExecuteNonQuery("UPDATE IMS_ATTRFILTER_VALUE SET F_STRING_VALUE = :strValue WHERE F_OBJECT_ID = :objID AND F_FILTER_ID = :filtID", db.Parameter("objID", (object) Math.Abs(objectID)), db.Parameter("filtID", (object) filterID), db.Parameter(nameof (strValue), (object) strValue));
  }

  public void AddOrUpdateValue(IDbManager db, long objectID, long filterID, string strValue)
  {
    DataTable dataTable = db.ExecuteDataTable("SELECT F_STRING_VALUE FROM IMS_ATTRFILTER_VALUE WHERE F_OBJECT_ID = :objID AND F_FILTER_ID = :filtID", db.Parameter("objID", (object) Math.Abs(objectID)), db.Parameter("filtID", (object) filterID));
    if (dataTable.Rows.Count == 0)
    {
      this.AddValue(db, objectID, filterID, strValue);
    }
    else
    {
      if (!(dataTable.Rows[0][0].ToString() != strValue))
        return;
      this.UpdateValue(db, objectID, filterID, strValue);
    }
  }

  public string GetValue(IDbManager db, long objectID, long filterID)
  {
    object obj = db.ExecuteScalar("SELECT F_STRING_VALUE FROM IMS_ATTRFILTER_VALUE WHERE F_OBJECT_ID = :objID AND F_FILTER_ID = :filtID", db.Parameter("objID", (object) Math.Abs(objectID)), db.Parameter("filtID", (object) filterID));
    return obj == null || obj == DBNull.Value ? string.Empty : obj.ToString();
  }

  public long[] GetFilterIDs(IDbManager db, long objectID)
  {
    DataTable dataTable = db.ExecuteDataTable("SELECT F_FILTER_ID FROM IMS_ATTRFILTER_VALUE WHERE F_OBJECT_ID = :objID", db.Parameter("objID", (object) Math.Abs(objectID)));
    List<long> longList = new List<long>(dataTable.Rows.Count);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      longList.Add(Convert.ToInt64(dataTable.Rows[index][0]));
    return longList.ToArray();
  }
}
