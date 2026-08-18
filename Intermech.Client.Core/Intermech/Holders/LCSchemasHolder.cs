
// Type: Intermech.Holders.LCSchemasHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using System;
using System.Data;


namespace Intermech.Holders;

public class LCSchemasHolder : DataHolder
{
  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (this.dataTable == null | reload)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.dataTable = (sessionKeeper.Session.GetLCSchemaCollection(CoreConsts.FilterRecords) as IDBCollection).Select("");
        this.lastReload = DateTime.Now;
      }
    }
    return this.dataTable;
  }

  public int GetIDbyName(string name)
  {
    DataRow[] dataRowArray = this.DataTable.Select($"F_NAME='{name.Replace("'", "''")}'");
    if (dataRowArray.Length != 0)
      return Convert.ToInt32(dataRowArray[0]["F_SCHEMA_ID"]);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBLCSchema dblcSchema = (IDBLCSchema) null;
      try
      {
        dblcSchema = sessionKeeper.Session.GetLCSchema(name);
      }
      catch
      {
      }
      if (dblcSchema != null)
        return dblcSchema.SchemaID;
    }
    return 0;
  }

  public string GetNamebyID(int id)
  {
    DataRow[] dataRowArray = this.DataTable.Select("F_SCHEMA_ID=" + id.ToString());
    if (dataRowArray.Length != 0)
      return (string) dataRowArray[0]["F_NAME"];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBLCSchema dblcSchema = (IDBLCSchema) null;
      try
      {
        dblcSchema = sessionKeeper.Session.GetLCSchema(id);
      }
      catch
      {
      }
      if (dblcSchema != null)
        return dblcSchema.Name;
      return string.Empty;
    }
  }

  public override void ClearInfo(params object[] args) => base.ClearInfo(args);
}
