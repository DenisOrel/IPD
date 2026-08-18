// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.DopIzvObject
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

public class DopIzvObject(UserSession uSession, DataTable objectsTable) : VerIzvObject(uSession, objectsTable)
{
  protected override void DoDelete()
  {
    if (this.LCStep == ECOServer.ecos.lcActualizeId)
    {
      long linkedEco = this.GetLinkedECO();
      if (linkedEco != 0L && linkedEco != 0L)
      {
        IDBObject dbObject = this.UserSession.GetObject(linkedEco, false);
        if (dbObject != null)
        {
          string str = " ";
          dbObject.GetAttributeByID(ECOServer.idAttrDopIzv).AsInteger = 0L;
          dbObject.GetAttributeByID(ECOServer.idAttrDopDesign).AsString = str;
        }
      }
    }
    base.DoDelete();
  }

  private long GetLinkedECO()
  {
    DataTable dataTable = this.UserSession.GetRelationCollection(ECOServer.relTypeDI).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    }), this.ObjectID);
    return dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
  }

  protected override void DoNextLCStep(IDBLifecycleStep nextstep)
  {
    if (nextstep.LCStep == ECOServer.ecos.lcActualizeId)
    {
      long linkedEco = this.GetLinkedECO();
      if (linkedEco != 0L && linkedEco != 0L)
      {
        IDBObject dbObject = this.UserSession.GetObject(linkedEco, false);
        if (dbObject != null)
        {
          string asString = this.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")).AsString;
          dbObject.GetAttributeByID(ECOServer.idAttrDopIzv).AsInteger = this.ObjectID;
          dbObject.GetAttributeByID(ECOServer.idAttrDopDesign).AsString = asString;
        }
      }
      DateTime result1 = new DateTime(3000, 12, 12);
      DateTime result2 = new DateTime(3000, 12, 12);
      IDBAttribute attributeById1 = this.GetAttributeByID(ECOServer.ecos.attrChangeDateId);
      if (attributeById1 != null && attributeById1.Value != null && attributeById1.Value != DBNull.Value)
        DateTime.TryParse(Convert.ToString(attributeById1.Value), out result1);
      IDBAttribute attributeById2 = this.GetAttributeByID(ECOServer.ecos.attrChangeDateEndId);
      if (attributeById2 != null && attributeById2.Value != null && attributeById2.Value != DBNull.Value)
        DateTime.TryParse(Convert.ToString(attributeById2.Value), out result2);
      if (result1.Year < 3000 || result2.Year < 3000)
        this.SetTermsForECOVersions(linkedEco, result1, result2);
    }
    base.DoNextLCStep(nextstep);
  }

  protected void SetTermsForECOVersions(long revId, DateTime changeDate, DateTime expireDate)
  {
    DataTable dataTable = this.Session.GetRelationCollection(ECOServer.ecos.idLinkRevision).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[6]
    {
      (object) -26,
      (object) -22,
      (object) -2,
      (object) -21,
      (object) ECOServer.ecos.attrIncludeGoalId,
      (object) ECOServer.ecos.idAttrVerId
    }), revId);
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      switch (Convert.ToInt32(row[4]))
      {
        case 0:
        case 3:
        case 4:
          long objId = 0;
          if (row[5] != null && row[5] != DBNull.Value)
            objId = Convert.ToInt64(row[5]);
          else if (row[2] != null && row[2] != DBNull.Value)
            objId = Convert.ToInt64(row[2]);
          if (objId != 0L)
          {
            if (changeDate.Year < 3000)
              ECOServer.ecos.SetStartDate(objId, changeDate);
            if (expireDate.Year < 3000)
            {
              ECOServer.ecos.SetEndDate(objId, expireDate);
              continue;
            }
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }
}
