
// Type: Intermech.Holders.LevelsHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Holders;

public class LevelsHolder : DataHolder
{
  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (this.dataTable == null | reload)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.dataTable = sessionKeeper.Session.GetLifecycleLevelCollection(CoreConsts.FilterRecords).Select("");
        this.lastReload = DateTime.Now;
      }
    }
    return this.dataTable;
  }

  public int GetIDbyName(string name)
  {
    DataRow[] dataRowArray = this.DataTable.Select($"F_LEVEL_NAME='{name.Replace("'", "''")}'");
    if (dataRowArray.Length != 0)
      return Convert.ToInt32(dataRowArray[0]["F_LEVEL_ID"]);
    if (name == string.Empty)
      return -1;
    if (name != CoreConsts.AnyLevel)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBLifecycleLevel dbLifecycleLevel = (IDBLifecycleLevel) null;
        try
        {
          dbLifecycleLevel = sessionKeeper.Session.GetLifecycleLevel(name) as IDBLifecycleLevel;
        }
        catch
        {
        }
        if (dbLifecycleLevel != null)
          return dbLifecycleLevel.LevelID;
      }
    }
    return 0;
  }

  public string GetNamebyID(int id)
  {
    DataRow[] dataRowArray = this.DataTable.Select("F_LEVEL_ID=" + id.ToString());
    if (dataRowArray.Length != 0)
      return (string) dataRowArray[0]["F_LEVEL_NAME"];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBLifecycleLevel dbLifecycleLevel = (IDBLifecycleLevel) null;
      try
      {
        dbLifecycleLevel = sessionKeeper.Session.GetLifecycleLevel(id) as IDBLifecycleLevel;
      }
      catch
      {
      }
      if (dbLifecycleLevel != null)
        return dbLifecycleLevel.LevelName;
      return string.Empty;
    }
  }

  public override void ClearInfo(params object[] args) => base.ClearInfo(args);
}
