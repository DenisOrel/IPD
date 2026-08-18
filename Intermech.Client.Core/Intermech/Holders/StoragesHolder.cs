
// Type: Intermech.Holders.StoragesHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Holders;

public class StoragesHolder : DataHolder
{
  public long GetIDbyName(string name)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.DataTable.Rows)
    {
      if (Convert.ToString(row[1]) == name)
        return Convert.ToInt64(row[0]);
    }
    return 0;
  }

  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (this.dataTable == null | reload || (DateTime.Now - this.lastReload).Seconds > ClientConsts.CacheLifeTime)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.CAPTION
        });
        this.dataTable = sessionKeeper.Session.ObjectsSelect(new Guid("cad00014-306c-11d8-b4e9-00304f19f545"), dbRecordSetParams);
        this.lastReload = DateTime.Now;
      }
    }
    return this.dataTable;
  }
}
