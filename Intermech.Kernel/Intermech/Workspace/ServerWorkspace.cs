// Decompiled with JetBrains decompiler
// Type: Intermech.Workspace.ServerWorkspace
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Objects;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Workspace;

internal class ServerWorkspace(UserSession uSession, DataTable objectsTable) : 
  DBObject(uSession, objectsTable),
  IServerWorkspace
{
  private IDBRelationCollection _SamplesCollection;
  internal bool _CanDelete;
  internal bool _CanCreate;

  public long FindInWorkspace(long objectID) => -1;

  private IDBRelationCollection SamplesCollection
  {
    get
    {
      if (this._SamplesCollection == null)
      {
        this._SamplesCollection = this.UserSession.GetRelationCollection(this.UserSession.IdentHelper.GetRelationTypeID("cad0005e-306c-11d8-b4e9-00304f19f545"));
        this._SamplesCollection.ObjectTypeID = this.UserSession.IdentHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545");
      }
      return this._SamplesCollection;
    }
  }

  private void WriteSampleConditions(
    long sampleID,
    ConditionStructure[] conditions,
    SampleFunctions sampleFunction)
  {
    if (sampleID == 0L)
    {
      IDBObject dbObject = this.UserSession.GetObjectCollection(this.UserSession.IdentHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545")).Create();
      dbObject.Attributes.AddAttribute(this.UserSession.IdentHelper.GetAttributeID("cad00345-306c-11d8-b4e9-00304f19f545"), false, new object[1]
      {
        (object) (int) sampleFunction
      });
      dbObject.GetAttributeByID(this.UserSession.IdentHelper.NameID).AsString = EnumTypeHelper.GetCaption((Enum) sampleFunction);
      dbObject.OwnerID = this.OwnerID;
      dbObject.CommitCreation(true);
      sampleID = dbObject.ObjectID;
      this.SamplesCollection.Create(this.ObjectID, sampleID);
    }
    (ServerServices.GetService(typeof (ISelectionsService)) as ISelectionsService).SetConditionStructures((object) this.UserSession.SessionGUID, sampleID, conditions);
  }

  private ConditionStructure[] GetSampleConditions(SampleFunctions sample)
  {
    ConditionStructure[] sampleConditions = (ConditionStructure[]) null;
    if (sample == SampleFunctions.InBoxDocs)
    {
      ConditionStructure[] conditionStructureArray = new ConditionStructure[1]
      {
        new ConditionStructure(SystemGUIDs.attributeRecipient, RelationalOperators.Equal, (object) Consts.CurrentUserFunction, LogicalOperators.NONE, 0)
      };
      ConditionStructure conditionStructure1 = new ConditionStructure(0, RelationalOperators.EntersInType, (object) MetaDataHelper.GetObjectTypeID("cad002b5-306c-11d8-b4e9-00304f19f545"), LogicalOperators.OR, 1, true);
      ConditionStructure conditionStructure2 = new ConditionStructure(0, RelationalOperators.EntersInType, (object) MetaDataHelper.GetObjectTypeID("cad002b4-306c-11d8-b4e9-00304f19f545"), LogicalOperators.OR, 0, true);
      ConditionStructure conditionStructure3 = new ConditionStructure(0, RelationalOperators.EntersInType, (object) MetaDataHelper.GetObjectTypeID("cad002bd-306c-11d8-b4e9-00304f19f545"), LogicalOperators.AND, -1, true);
      conditionStructure1.NestedConditions = conditionStructureArray;
      conditionStructure1.TypeID = (object) MetaDataHelper.GetRelationTypeID(SystemGUIDs.relationTypeAttachments);
      conditionStructure2.NestedConditions = conditionStructureArray;
      conditionStructure2.TypeID = (object) MetaDataHelper.GetRelationTypeID(SystemGUIDs.relationTypeAttachments);
      conditionStructure3.NestedConditions = conditionStructureArray;
      conditionStructure3.TypeID = (object) MetaDataHelper.GetRelationTypeID(SystemGUIDs.relationTypeAttachments);
      sampleConditions = new ConditionStructure[4]
      {
        new ConditionStructure(0, RelationalOperators.ObjectTypeFilter, (object) MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545"), (object) null, LogicalOperators.AND, 0, true),
        conditionStructure1,
        conditionStructure2,
        conditionStructure3
      };
    }
    return sampleConditions;
  }

  public bool CreateSamples()
  {
    if (this.ObjectType != this.UserSession.IdentHelper.WorkspaceTypeID)
      return false;
    bool samples = false;
    DataTable dataTable = this.SamplesCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) this.UserSession.IdentHelper.GetAttributeID("cad00345-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    }), this.ObjectID);
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long num = 0;
      if (row[0] != DBNull.Value && row[0] != null)
        num = Convert.ToInt64(row[0]);
      switch (num)
      {
        case 1:
          flag1 = true;
          this.WriteSampleConditions(Convert.ToInt64(row[1]), new ConditionStructure[1]
          {
            new ConditionStructure(-6, RelationalOperators.Equal, (object) this.OwnerID, (object) null, LogicalOperators.NONE, 0, true)
          }, SampleFunctions.CheckedOut);
          continue;
        case 2:
          flag2 = true;
          this.WriteSampleConditions(Convert.ToInt64(row[1]), new ConditionStructure[2]
          {
            new ConditionStructure(-8, RelationalOperators.Equal, (object) this.OwnerID, (object) null, LogicalOperators.AND, 0, true),
            new ConditionStructure(-9, RelationalOperators.Equal, (object) this.UserSession.IdentHelper.DeletedID, (object) null, LogicalOperators.NONE, 0, true)
          }, SampleFunctions.MyTrash);
          continue;
        case 3:
          flag3 = true;
          this.WriteSampleConditions(Convert.ToInt64(row[1]), this.GetSampleConditions(SampleFunctions.InBoxDocs), SampleFunctions.InBoxDocs);
          continue;
        default:
          continue;
      }
    }
    if (!flag1)
      this.WriteSampleConditions(0L, new ConditionStructure[1]
      {
        new ConditionStructure(-6, RelationalOperators.Equal, (object) this.OwnerID, (object) null, LogicalOperators.NONE, 0, true)
      }, SampleFunctions.CheckedOut);
    if (!flag2)
      this.WriteSampleConditions(0L, new ConditionStructure[2]
      {
        new ConditionStructure(-8, RelationalOperators.Equal, (object) this.OwnerID, (object) null, LogicalOperators.AND, 0, true),
        new ConditionStructure(-9, RelationalOperators.Equal, (object) this.UserSession.IdentHelper.DeletedID, (object) null, LogicalOperators.NONE, 0, true)
      }, SampleFunctions.MyTrash);
    if (!flag3)
      this.WriteSampleConditions(0L, this.GetSampleConditions(SampleFunctions.InBoxDocs), SampleFunctions.InBoxDocs);
    return samples;
  }

  protected override void DoCommitCreation()
  {
    if (!this._CanCreate && this.ObjectType == this.UserSession.IdentHelper.WorkspaceTypeID)
    {
      object obj = this.UserSession.DataManager.ExecuteScalar(sc_13512.ssp_appserver_13513(), this.UserSession.DataManager.Parameter("ownerID", (object) this.OwnerID), this.UserSession.DataManager.Parameter("objTypeID", (object) this.UserSession.IdentHelper.WorkspaceTypeID), this.UserSession.DataManager.Parameter("delID", (object) this.UserSession.IdentHelper.DeletedID));
      if (obj != null && obj != DBNull.Value)
        throw new KernelExceptionID(367, (object) this.UserSession.GetObjectInfo(this.OwnerID).Caption);
    }
    base.DoCommitCreation();
    this.CreateSamples();
  }

  protected override void DoDelete()
  {
    if (!this._CanDelete && this.ObjectType == MetaDataHelper.GetObjectTypeID("cad0004a-306c-11d8-b4e9-00304f19f545"))
    {
      IDBObject dbObject = this.UserSession.GetObject(this.OwnerID, false);
      if (dbObject != null && dbObject.LCStep != this.UserSession.IdentHelper.DeletedID)
        throw new KernelExceptionID(sc_13512.ssp_appserver_13514(756798139), (object) dbObject.Caption);
    }
    base.DoDelete();
  }
}
