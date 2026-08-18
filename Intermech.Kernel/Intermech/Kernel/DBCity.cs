// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBCity
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBCity(UserSession uSession, DataTable objectsTable) : DBObject(uSession, objectsTable)
{
  private Guid _attributeCountryName = new Guid("cadd9264-306c-11d8-b4e9-00304f19f545");
  private Guid _attributeRegionName = new Guid("cadd9244-306c-11d8-b4e9-00304f19f545");

  protected override void DoBeforeCommitCreation()
  {
    IDBAttribute attributeByGuid1 = this.GetAttributeByGuid(this._attributeCountryName);
    if (attributeByGuid1 == null || attributeByGuid1.IsNull || attributeByGuid1.AsString == string.Empty)
      return;
    IDBAttribute attributeByGuid2 = this.GetAttributeByGuid(this._attributeRegionName);
    if (attributeByGuid2 == null || attributeByGuid2.IsNull || attributeByGuid2.AsString == string.Empty)
      return;
    (this.Session as UserSession).StartTransaction();
    try
    {
      IDBObjectCollection objectCollection = this.Session.GetObjectCollection(new Guid("cadd9239-306c-11d8-b4e9-00304f19f545"));
      DataTable dataTable1 = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(this._attributeCountryName, RelationalOperators.Equal, (object) attributeByGuid1.AsString, LogicalOperators.AND, 0)
      }, new object[1]{ (object) -2 }));
      bool flag = false;
      long projectID;
      if (dataTable1.Rows.Count == 0)
      {
        IDBObject dbObject = objectCollection.Create();
        dbObject.Attributes.AddAttribute(attributeByGuid1.AttributeID, false, new object[1]
        {
          (object) attributeByGuid1.AsString
        });
        dbObject.CommitCreation(true);
        projectID = dbObject.ObjectID;
        flag = true;
      }
      else
        projectID = Convert.ToInt64(dataTable1.Rows[0][0]);
      long num = 0;
      IDBRelationCollection relationCollection = this.Session.GetRelationCollection(this.Session.IdentHelper.SimpleRelationTypeID);
      if (!flag)
      {
        relationCollection.ChildObjectTypes = (IList<int>) new List<int>((IEnumerable<int>) new int[1]
        {
          MetaDataHelper.GetObjectTypeID("cadd9238-306c-11d8-b4e9-00304f19f545")
        });
        DataTable dataTable2 = relationCollection.ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(attributeByGuid2.AttributeID, RelationalOperators.Equal, (object) attributeByGuid2.AsString, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
        }, new object[1]{ (object) -2 }), projectID);
        if (dataTable2.Rows.Count > 0)
          num = Convert.ToInt64(dataTable2.Rows[0][0]);
      }
      if (num == 0L)
      {
        IDBObject dbObject = this.Session.GetObjectCollection(new Guid("cadd9238-306c-11d8-b4e9-00304f19f545")).Create();
        dbObject.Attributes.AddAttribute(attributeByGuid2.AttributeID, false, new object[1]
        {
          (object) attributeByGuid2.AsString
        });
        dbObject.CommitCreation(true);
        num = dbObject.ObjectID;
        relationCollection.Create(projectID, num);
      }
      relationCollection.Create(num, this.ObjectID);
      attributeByGuid1.Delete(0L);
      attributeByGuid2.Delete(0L);
      (this.Session as UserSession).Commit();
    }
    catch
    {
      (this.Session as UserSession).Rollback();
      throw;
    }
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }
}
