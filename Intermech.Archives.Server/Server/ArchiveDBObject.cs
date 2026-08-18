// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ArchiveDBObject
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Archives.Server.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Text;

#nullable disable
namespace Intermech.Archives.Server;

internal class ArchiveDBObject(IUserSession uSession, DataTable objectsTable) : 
  DBObject(uSession as UserSession, objectsTable),
  IArchiveDBObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  private IDBSecurity _AccessChecker;
  internal IDBObject _ArchivedObject;

  public IDBObject ArchivedObject
  {
    get => this._ArchivedObject;
    set => this._ArchivedObject = value;
  }

  public IDBSecurity AccessChecker
  {
    get
    {
      return this._AccessChecker ?? (this._AccessChecker = (IDBSecurity) new ArchiveSecurity(this.UserSession, this, false));
    }
  }

  public override string SecurityCollectionName
  {
    get => ArchivesServerHolder.rm.GetString("ArchivesCollectionName");
  }

  public override IDBSecurityCollection GetRelatedSecurityCollection(long[] categoryID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    StringBuilder stringBuilder = new StringBuilder("SELECT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_OBJECT_ID IN (");
    for (int index = 0; index < categoryID.Length; ++index)
    {
      if (index >= dataManager.DataProvider.MaximumINOperands)
        return (IDBSecurityCollection) null;
      stringBuilder.Append(categoryID[index].ToString() + ",");
    }
    --stringBuilder.Length;
    stringBuilder.Append(")");
    DataTable dataTable = dataManager.ExecuteDataTable(stringBuilder.ToString());
    int objectTypeId = this.UserSession.IdentHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (!MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(dataTable.Rows[index][0]), objectTypeId))
        return (IDBSecurityCollection) null;
    }
    return this.AccessChecker as IDBSecurityCollection;
  }

  public override IDBSecurity[] GetRelatedSecurity()
  {
    if (this._AccessChecker == null)
      this._AccessChecker = (IDBSecurity) new ArchiveSecurity(this.UserSession, this, true);
    return new IDBSecurity[2]
    {
      this.LCStepObject as IDBSecurity,
      this.AccessChecker
    };
  }

  protected override void DoDelete()
  {
    if (this.Session.GetObjectCollection(ConstsHolder.DocTypeGuid).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.ArcAttrGuid, RelationalOperators.Equal, (object) this.ObjectID, LogicalOperators.NONE, 0)
      {
        Content = ColumnContents.ID
      }
    })
    {
      RecordCount = 1
    }).Rows.Count > 0)
      throw new ArgumentException(string.Format(ArchivesServerHolder.rm.GetString("Archives.Server_2"), (object) this.Caption));
    base.DoDelete();
  }

  private void CopyAccess(IDBObject toArc)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    if (Convert.ToInt32(dataManager.ExecuteScalar($"SELECT COUNT(*) FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE IN ({17}, {1}) AND F_CATEGORY_ID = {Math.Abs(toArc.ObjectID)}")) != 0)
      return;
    DataTable accessList1 = dataManager.ExecuteDataTable($"SELECT * FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = {1} AND F_CATEGORY_ID = {Math.Abs(this.ObjectID)}");
    if (accessList1.Rows.Count > 0)
    {
      for (int index = 0; index < accessList1.Rows.Count; ++index)
      {
        accessList1.Rows[index]["F_KEY"] = (object) 0;
        accessList1.Rows[index]["F_CATEGORY_ID"] = (object) Math.Abs(toArc.ObjectID);
      }
      accessList1.AcceptChanges();
      (toArc as IDBSecurity).SetAccess(accessList1);
    }
    DataTable accessList2 = dataManager.ExecuteDataTable($"SELECT * FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = {17} AND F_CATEGORY_ID = {Math.Abs(this.ObjectID)}");
    if (accessList2.Rows.Count <= 0)
      return;
    for (int index = 0; index < accessList2.Rows.Count; ++index)
    {
      accessList2.Rows[index]["F_KEY"] = (object) 0;
      accessList2.Rows[index]["F_CATEGORY_ID"] = (object) Math.Abs(toArc.ObjectID);
    }
    accessList2.AcceptChanges();
    (toArc as ArchiveDBObject).AccessChecker.SetAccess(accessList2);
  }

  public override void DoAfterCreateRelation(IDBRelation newrelation)
  {
    base.DoAfterCreateRelation(newrelation);
    IDBObject toArc = newrelation.PartObjectID == 0L ? this.UserSession.GetObjectByID(newrelation.PartID, true) : this.UserSession.GetObject(newrelation.PartObjectID);
    if (!(toArc is ArchiveDBObject) || toArc.IsCreationMode)
      return;
    this.CopyAccess(toArc);
  }

  protected override void DoBeforeRemoveObject(DBRelation dBRelation, long newProjID)
  {
    base.DoBeforeRemoveObject(dBRelation, newProjID);
    if (!(this.UserSession.GetObject(newProjID, false) is ArchiveDBObject archiveDbObject))
      return;
    archiveDbObject.CopyAccess((IDBObject) this);
  }

  protected override void DoAfterCommitCreation()
  {
    base.DoAfterCommitCreation();
    IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(this.UserSession.IdentHelper.SimpleRelationTypeID);
    relationCollection.ObjectTypeID = this.UserSession.IdentHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545");
    DataTable dataTable = relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    }), this.ID);
    if (dataTable.Rows.Count <= 0)
      return;
    try
    {
      if (!(this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[0][0]), false) is ArchiveDBObject archiveDbObject))
        return;
      archiveDbObject.CopyAccess((IDBObject) this);
    }
    catch
    {
    }
  }

  protected override void DoPurge(long DeleteMode)
  {
    base.DoPurge(DeleteMode);
    this.PurgeAccess(17, this.ObjectID);
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    if (ArchivesServerStartup.StorageIDService != null && attribute.AttributeID == ArchivesServerStartup.StorageIDService.StorageAttrID)
    {
      if (!this.UserSession.IsAdmin)
        throw new KernelExceptionID(126);
      if (!attribute.IsNull)
      {
        ArchivesServerStartup.StorageIDService.SetStorageID(this.ObjectID, attribute.AsInteger);
        DBObjectCollection objectCollection = this.UserSession.GetObjectCollection(ConstsHolder.DocTypeID) as DBObjectCollection;
        objectCollection.GlobalSelectMode = true;
        DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(ConstsHolder.ArchiveAttrID, RelationalOperators.Equal, (object) this.ObjectID, LogicalOperators.NONE, 0, false)
        }, new object[1]{ (object) -2 }));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false);
          if (dbObject != null)
            ArchivesServerStartup.RemoveBlobs(dbObject, this.ObjectID, (IUserSession) this.UserSession);
        }
      }
      else
        ArchivesServerStartup.StorageIDService.ClearStorageID(this.ObjectID);
    }
    if (attribute.AttributeID == ObjectsVisibilityHelper.AttrVisibilityId && (ServerServices.GetService(typeof (IArchiveService)) as IArchiveService).CopyArcVisibility)
    {
      DBObjectCollection objectCollection = this.UserSession.GetObjectCollection(ConstsHolder.DocTypeID) as DBObjectCollection;
      objectCollection.GlobalSelectMode = true;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(ConstsHolder.ArchiveAttrID, RelationalOperators.Equal, (object) this.ObjectID, LogicalOperators.NONE, 0, false)
      }, new object[1]{ (object) -2 });
      ObjectsVisibilityHelper.SetArcVisibility((IUserSession) this.UserSession, objectCollection.Select(paramSet), attribute.AsString, ServerConsts.CopyProjectVisibility);
      IDBObjectType objectType1 = this.UserSession.GetObjectType(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
      if (objectType1.Attributes.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId) != null && objectType1.Attributes.GetAttributeByID(ConstsHolder.ArchiveAttrID) != null)
      {
        objectCollection.ObjectTypeID = objectType1.ObjectType;
        ObjectsVisibilityHelper.SetArcVisibility((IUserSession) this.UserSession, objectCollection.Select(paramSet), attribute.AsString, ServerConsts.CopyProjectVisibility);
      }
      int objectTypeId = MetaDataHelper.GetObjectTypeID(SystemGUIDs.objtypeProductionObjectsGuid);
      if (objectTypeId > -1)
      {
        foreach (int anObjectTypeID in MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(objectTypeId))
        {
          IDBObjectType objectType2 = this.UserSession.GetObjectType(anObjectTypeID);
          if (objectType2.Attributes.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId) != null && objectType2.Attributes.GetAttributeByID(ConstsHolder.ArchiveAttrID) != null)
          {
            objectCollection.ObjectTypeID = objectType2.ObjectType;
            ObjectsVisibilityHelper.SetArcVisibility((IUserSession) this.UserSession, objectCollection.Select(paramSet), attribute.AsString, ServerConsts.CopyProjectVisibility);
          }
        }
      }
    }
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }
}
