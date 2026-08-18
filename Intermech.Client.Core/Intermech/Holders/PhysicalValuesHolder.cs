
// Type: Intermech.Holders.PhysicalValuesHolder
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

public class PhysicalValuesHolder : DataHolder
{
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
        this.dataTable = sessionKeeper.Session.ObjectsSelect(sessionKeeper.Session.IdentHelper.PhysicValueTypeID, dbRecordSetParams);
        this.lastReload = DateTime.Now;
      }
    }
    return this.dataTable;
  }
}
