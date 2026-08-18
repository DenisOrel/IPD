
// Type: Intermech.Holders.AttributeGroupsHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Holders;

public class AttributeGroupsHolder : DataHolder
{
  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (this.dataTable == null | reload)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.dataTable = sessionKeeper.Session.GetAttributesGroupCollection(-1, CoreConsts.FilterRecords).Select("");
        this.lastReload = DateTime.Now;
      }
    }
    return this.dataTable;
  }

  public string GetNamebyID(int id)
  {
    DataRow[] dataRowArray = this.DataTable.Select("F_GROUP_ID=" + id.ToString());
    if (dataRowArray.Length != 0)
      return (string) dataRowArray[0]["F_GROUP_NAME"];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributesGroup dbAttributesGroup = (IDBAttributesGroup) null;
      try
      {
        dbAttributesGroup = sessionKeeper.Session.GetAttributesGroup(id);
      }
      catch
      {
      }
      if (dbAttributesGroup != null)
        return dbAttributesGroup.GroupName;
      return string.Empty;
    }
  }
}
