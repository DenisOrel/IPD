// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.ImbaseTableMixPumpService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Sync.DataBase;
using Intermech.Imbase.Server.Sync.Helper;
using Intermech.Imbase.Server.Sync.Records;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Sync;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync;

internal class ImbaseTableMixPumpService : 
  BaseSyncTaskService,
  IImbaseTableMixPumpService,
  IServiceForBackgroundTask
{
  protected override void BeforeTaskExecute(IUserSession session, IDataBase sourceDB)
  {
    base.BeforeTaskExecute(session, sourceDB);
    VisibleAttHelper.Init(session);
  }

  protected override List<EventRecord> GetEventRecs()
  {
    List<EventRecord> eventRecs = new List<EventRecord>();
    DataTable tableMixDt = this.GetTableMixDt(this.SourceDb);
    if (tableMixDt == null || tableMixDt.Rows.Count <= 0)
      return eventRecs;
    Dictionary<int, Tuple<string, string, string, int, string>> catalogsRecInfo = this.GetCatalogsRecInfo(this.SourceDb);
    foreach (DataRow row in (InternalDataCollectionBase) tableMixDt.Rows)
    {
      int int32_1 = Convert.ToInt32(row["F_KEY"]);
      string str = Convert.ToString(row["F_TABLE"]);
      foreach (KeyValuePair<int, Tuple<string, string, string, int, string>> keyValuePair in catalogsRecInfo)
      {
        DataTable dataTable = this.SourceDb.ExecuteDataTable($"SELECT * FROM {keyValuePair.Value.Item5} LEFT JOIN {keyValuePair.Value.Item1} ON {keyValuePair.Value.Item5}.F_LEVEL = {keyValuePair.Value.Item1}.F_LEVEL WHERE {keyValuePair.Value.Item3} = :F_TABLE", this.SourceDb.CreateParameter("F_TABLE", (object) str));
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
          int int32_2 = Convert.ToInt32(dataTable.Rows[0]["F_LEVEL"]);
          int int32_3 = Convert.ToInt32(dataTable.Rows[0]["F_KEY"]);
          eventRecs.Add(new EventRecord()
          {
            Code = 120,
            Catalog = keyValuePair.Key,
            Folder = int32_2,
            Table = int32_1,
            ObjKey = int32_3,
            Text = str
          });
          eventRecs.Add(new EventRecord()
          {
            Code = 200,
            Catalog = 0,
            Folder = 0,
            Table = int32_1,
            ObjKey = 0
          });
        }
      }
    }
    return eventRecs;
  }

  private Dictionary<int, Tuple<string, string, string, int, string>> GetCatalogsRecInfo(
    IDataBase sourceDb)
  {
    Dictionary<int, Tuple<string, string, string, int, string>> catalogsRecInfo = new Dictionary<int, Tuple<string, string, string, int, string>>();
    string sql1 = "select t.F_KEY, t.F_TABLE, t.F_DESCR, f.F_FIELD from IM_TABLES t left join IM_FIELDS f on f.F_TABLE_ID = t.F_KEY and upper(f.F_LONGNAME) = :BASEPAR where t.F_TYPE = 'CATALOG' and t.F_KEY in (select F_TABLE_ID from IM_FIELDS where upper(IM_FIELDS.F_LONGNAME) = :BASEPAR)";
    DataTable dataTable1 = sourceDb.ExecuteDataTable(sql1, this.SourceDb.CreateParameter("BASEPAR", (object) "БАЗА"));
    if (dataTable1 != null && dataTable1.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
      {
        int int32_1 = Convert.ToInt32(row["F_KEY"]);
        string str1 = Convert.ToString(row["F_TABLE"]);
        string str2 = Convert.ToString(row["F_DESCR"]);
        string str3 = Convert.ToString(row["F_FIELD"]);
        string str4 = str1 + "_REC";
        string sql2 = "select * from IM_TABLES where F_TABLE = :F_TABLE and F_TYPE = 'CTLREC'";
        DataTable dataTable2 = sourceDb.ExecuteDataTable(sql2, sourceDb.CreateParameter("F_TABLE", (object) str4));
        if (dataTable2 != null && dataTable2.Rows.Count > 0)
        {
          int int32_2 = Convert.ToInt32(dataTable2.Rows[0]["F_KEY"]);
          string str5 = Convert.ToString(dataTable2.Rows[0]["F_TABLE"]);
          if (!catalogsRecInfo.ContainsKey(int32_1))
            catalogsRecInfo.Add(int32_1, new Tuple<string, string, string, int, string>(str1, str2, str3, int32_2, str5));
        }
      }
    }
    return catalogsRecInfo;
  }

  private DataTable GetTableMixDt(IDataBase sourceDb)
  {
    string sql = "SELECT * FROM IM_TABLES A WHERE F_OPENMODE = 2";
    return sourceDb.ExecuteDataTable(sql);
  }
}
