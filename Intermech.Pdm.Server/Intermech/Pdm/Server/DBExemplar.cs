// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.DBExemplar
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

internal class DBExemplar(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    this.OnCreateNewVersion();
    this.AddTechRelationAfterCreate();
  }

  private void OnCreateNewVersion()
  {
    if (this.ParentVersionID == -1L || this.VersionID == 0)
      return;
    IDBRelationType relationType = this.Session.GetRelationType(PDMHelper.relationTypeInstances, false);
    if (relationType == null)
      return;
    IDBLifecycleStep lifecycleStep1 = this.Session.GetLifecycleStep(new Guid("cad0080c-306c-11d8-b4e9-00304f19f545"), false);
    if (lifecycleStep1 == null)
      return;
    IDBLifecycleStep lifecycleStep2 = this.Session.GetLifecycleStep(new Guid("cad0080e-306c-11d8-b4e9-00304f19f545"), false);
    if (lifecycleStep2 == null)
      return;
    IDBRelationCollection relationCollection = this.Session.GetRelationCollection(relationType.RelationType);
    DBRecordSetParams paramSet1 = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) -2, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -20, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -4, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -5, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable dataTable1 = relationCollection.EntersInVersion(paramSet1, this.ParentVersionID);
    if (dataTable1.Rows.Count <= 0)
      return;
    DataRow dataRow1 = dataTable1.Rows[0];
    if (dataTable1.Rows.Count > 1)
    {
      DataRow[] dataRowArray1 = dataTable1.Select($"[{-4}]={lifecycleStep1.LCStep}");
      if (dataRowArray1 != null && dataRowArray1.Length != 0)
      {
        int num = int.MinValue;
        foreach (DataRow dataRow2 in dataRowArray1)
        {
          int int32 = Convert.ToInt32(dataRow2[3]);
          if (int32 > num)
          {
            dataRow1 = dataRow2;
            num = int32;
          }
        }
      }
      else
      {
        DataRow[] dataRowArray2 = dataTable1.Select($"[{-4}]={lifecycleStep2.LCStep}");
        if (dataRowArray2 == null || dataRowArray2.Length == 0)
        {
          IDBObject dbObject = this.Session.GetObject(Convert.ToInt64(dataRow1[0]));
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Pdm.Server_14"), (object) dbObject.NameInMessages, (object) lifecycleStep1.LCName, (object) lifecycleStep2.LCName));
        }
        dataRow1 = dataRowArray2[0];
      }
    }
    int int32_1 = Convert.ToInt32(dataRow1[2]);
    if (int32_1 == lifecycleStep1.LCStep)
    {
      IDBRelation relation = this.Session.GetRelation(Convert.ToInt64(dataRow1[0]), this.ID);
      if (relation != null)
      {
        IDBAttribute attributeByGuid = relation.GetAttributeByGuid(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid == null)
          return;
        attributeByGuid.AsInteger = this.ObjectID;
      }
      else
        relationCollection.Create(Convert.ToInt64(dataRow1[0]), this.ObjectID);
    }
    else if (int32_1 == lifecycleStep2.LCStep)
    {
      IDBObject dbObject1 = this.Session.GetObject(Convert.ToInt64(dataRow1[0]), false);
      if (dbObject1 == null)
        return;
      IDBObject version = this.Session.GetObjectCollection(dbObject1.ObjectType).CreateVersion(dbObject1.ObjectID);
      version.CommitCreation(false);
      IDBObject dbObject2 = version.CheckOut(false);
      DBRecordSetParams paramSet2 = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-22, RelationalOperators.Equal, (object) this.ID, LogicalOperators.AND, 0, false)
      }, new object[1]{ (object) -20 });
      DataTable dataTable2 = relationCollection.ConsistFrom(paramSet2, dbObject2.ObjectID);
      if (dataTable2.Rows.Count != 1)
        return;
      IDBAttribute attributeByGuid = this.Session.GetRelation(Convert.ToInt64(dataTable2.Rows[0][0])).GetAttributeByGuid(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid == null)
        return;
      attributeByGuid.AsInteger = Math.Abs(this.ObjectID);
    }
    else
    {
      IDBObject dbObject = this.Session.GetObject(Convert.ToInt64(dataRow1[0]));
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Pdm.Server_15"), (object) dbObject.NameInMessages, (object) lifecycleStep1.LCName, (object) lifecycleStep2.LCName));
    }
  }

  private void AddTechRelationAfterCreate()
  {
    if (this.ParentVersionID != -1L)
      return;
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad00622-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || attributeByGuid.AsInteger == 0L)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Pdm.Server_47"), (object) this.NameInMessages));
    IDBObject dbObject = this.Session.GetObject(attributeByGuid.AsInteger, false);
    if (dbObject == null)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Pdm.Server_48"), (object) attributeByGuid.AsInteger, (object) this.NameInMessages));
    IDBRelationCollection relationCollection = this.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"));
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545"), LogicalOperators.AND, 0, false)
    }, new object[2]{ (object) -20, (object) -22 });
    DataTable dataTable = relationCollection.ConsistFrom(paramSet, dbObject.ObjectID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64_1 = Convert.ToInt64(dataTable.Rows[index][0]);
      long int64_2 = Convert.ToInt64(dataTable.Rows[index][1]);
      relationCollection.Create(new NewRelationProperties(int64_1, this.ObjectID, int64_2));
    }
  }
}
