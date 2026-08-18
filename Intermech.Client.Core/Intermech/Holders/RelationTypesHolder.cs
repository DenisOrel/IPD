
// Type: Intermech.Holders.RelationTypesHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Data;


namespace Intermech.Holders;

public class RelationTypesHolder : DataHolder
{
  public override DataTable LoadData(bool reload, params object[] args)
  {
    if (this.dataTable == null | reload)
    {
      this.dataTable = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetRelationTypeCollection(CoreConsts.FilterRecords).Select("");
      this.lastReload = DateTime.Now;
    }
    return this.dataTable;
  }

  public int GetIDbyName(string name)
  {
    DataRow[] dataRowArray = this.DataTable.Select($"F_DESCRIPTION='{name.Replace("'", "''")}'");
    if (dataRowArray.Length != 0)
      return Convert.ToInt32(dataRowArray[0]["F_RELATION_TYPE"]);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationType dbRelationType = (IDBRelationType) null;
      try
      {
        dbRelationType = sessionKeeper.Session.GetRelationType(name);
      }
      catch
      {
      }
      if (dbRelationType != null)
        return dbRelationType.RelationType;
      return 0;
    }
  }

  public string GetNamebyID(int id)
  {
    id.ToString();
    DataRow[] dataRowArray = this.DataTable.Select("F_RELATION_TYPE=" + id.ToString());
    if (dataRowArray.Length != 0)
      return (string) dataRowArray[0]["F_DESCRIPTION"];
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBRelationTypeInfo relationTypeInfo = (IDBRelationTypeInfo) null;
    try
    {
      relationTypeInfo = service.GetRelationType(id);
    }
    catch
    {
    }
    if (relationTypeInfo != null)
      return relationTypeInfo.Description;
    return string.Empty;
  }

  public override void ClearInfo(params object[] args) => base.ClearInfo(args);
}
