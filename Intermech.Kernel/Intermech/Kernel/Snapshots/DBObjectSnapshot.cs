// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Snapshots.DBObjectSnapshot
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Snapshots;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel.Snapshots;

public class DBObjectSnapshot : DBSessionable, IDBObjectSnapshot, IDeletable
{
  private long _SnapshotID;
  private string _SnapshotName;
  private long _ObjectID;
  private long _ID;
  private DateTime _SnapshotModifyDate;
  private long _SnapshotOwnerID;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(2);

  static DBObjectSnapshot()
  {
    DBObjectSnapshot.metadataActions.Add(ActionType.GetAccess, false);
    DBObjectSnapshot.metadataActions.Add(ActionType.SetAccess, false);
  }

  public DBObjectSnapshot(UserSession session, long snapshotID, DataTable tbl)
    : base(session)
  {
    this._SnapshotName = tbl.Rows[0]["F_NAME"].ToString();
    if (this._SnapshotName == string.Empty)
      this._SnapshotName = "N" + snapshotID.ToString();
    this._ObjectID = Convert.ToInt64(tbl.Rows[0]["F_OBJECT_ID"]);
    this._ID = Convert.ToInt64(tbl.Rows[0]["F_ID"]);
    this._SnapshotModifyDate = Convert.ToDateTime(tbl.Rows[0]["F_SNAPSHOT_DATE"]) + this.UserSession.TimeZoneOffset;
    this._SnapshotOwnerID = Convert.ToInt64(tbl.Rows[0]["F_USER_ID"]);
    this._SnapshotID = snapshotID;
    this.InitStaticSecurityOptions(23, snapshotID, DBObjectSnapshot.metadataActions);
  }

  public void CheckEditMode()
  {
    if (this.UserSession.UserID != this.SnapshotOwnerID && !this.UserSession.IsAdmin)
    {
      IDBObject dbObject = this.UserSession.GetObject(this.SnapshotOwnerID, false);
      string str = dbObject == null ? this.SnapshotID.ToString() : dbObject.Caption;
      throw new KernelExceptionID(sc_14217.ssp_appserver_14218(2110612642), (object) this.SnapshotName, (object) str);
    }
  }

  public long SnapshotID => this._SnapshotID;

  public string SnapshotName
  {
    get => this._SnapshotName;
    set
    {
      if (!(value != this._SnapshotName))
        return;
      this.CheckEditMode();
      this.UserSession.DataManager.ExecuteNonQuery(sc_14217.ssp_appserver_14219(), this.UserSession.DataManager.Parameter("snapName", (object) value), this.UserSession.DataManager.Parameter("snapID", (object) this.SnapshotID));
      this._SnapshotName = value;
    }
  }

  public override long ObjectID => this._ObjectID;

  public long ID => this._ID;

  public DateTime SnapshotModifyDate => this._SnapshotModifyDate;

  public long SnapshotOwnerID => this._SnapshotOwnerID;

  public override string ObjectName
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("SnapshotName"), (object) this.SnapshotName);
    }
  }

  public void SaveToSnapshot(List<long> objectIDs, string FiltrationOwnerID)
  {
    try
    {
      this.CheckEditMode();
    }
    catch
    {
      this.AddEvent(this.ObjectID, 0L, ActionType.Edit, EventlogRecordType.AccessDenied);
      throw;
    }
    if (!objectIDs.Contains(this.ObjectID) && !objectIDs.Contains(-this.ObjectID))
      throw new KernelExceptionID(sc_14217.ssp_appserver_14220(813187985), (object) this.SnapshotName, (object) this.ObjectID);
    IDBSnapshotCollection snapshotCollection = this.UserSession.GetSnapshotCollection();
    this.UserSession.StartTransaction();
    try
    {
      this.DeleteSnapshotContent();
      List<long> createdObjects = new List<long>();
      for (int index = 0; index < objectIDs.Count; ++index)
        snapshotCollection.AddObjectToSnapshot(objectIDs[index], this.SnapshotID, string.Empty, FiltrationOwnerID, createdObjects);
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_SNAPSHOTS SET F_SNAPSHOT_DATE = {this.UserSession.DataManager.DataProvider.Now} WHERE F_SNAPSHOT_ID = :snapID", this.UserSession.DataManager.Parameter("snapID", (object) this.SnapshotID));
      IDBObject dbObject = this.UserSession.GetObject(this.ObjectID);
      IDBAttribute attributeById = dbObject.GetAttributeByID(this.UserSession.IdentHelper.ActiveSnapshotID);
      if (attributeById == null)
        dbObject.Attributes.AddAttribute(this.UserSession.IdentHelper.ActiveSnapshotID, false, new object[1]
        {
          (object) this.SnapshotID
        });
      else
        attributeById.AsInteger = this.SnapshotID;
      this.AddEvent(this.ObjectID, 0L, ActionType.Edit, EventlogRecordType.AccessGranted);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public List<long> GetObjectsList()
  {
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJ_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID", this.UserSession.DataManager.Parameter("snapID", (object) this.SnapshotID));
    List<long> objectsList = new List<long>(dataTable.Rows.Count);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      objectsList.Add(Convert.ToInt64(dataTable.Rows[index][0]));
    return objectsList;
  }

  public List<long> GetReadOnlyObjects(long objectID)
  {
    List<long> readOnlyObjects = new List<long>();
    IDBObject dbObject = this.UserSession.GetObject(objectID);
    if (dbObject.ReadOnly)
      readOnlyObjects.Add(objectID);
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable("SELECT F_ID FROM IMS_OBJ_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID AND F_ID <> :f_ID", this.UserSession.DataManager.Parameter("snapID", (object) this.SnapshotID), this.UserSession.DataManager.Parameter("f_ID", (object) dbObject.ID));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IDBObject objectByVersionsRule = this.UserSession.GetObjectByVersionsRule(Convert.ToInt64(dataTable.Rows[index][0]), "cad005aa-306c-11d8-b4e9-00304f19f545", false);
      if (objectByVersionsRule != null && objectByVersionsRule.ReadOnly)
        readOnlyObjects.Add(objectByVersionsRule.ObjectID);
    }
    return readOnlyObjects;
  }

  public void SaveToObject(long objectID) => this.SaveToObject(objectID, true);

  public void SaveToObject(long objectID, bool abortOnError)
  {
    IDBObject dbObject1 = this.UserSession.GetObject(objectID);
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("snapID", (object) this.SnapshotID);
    this.UserSession.StartTransaction();
    try
    {
      DataTable dataTable1 = dataManager.ExecuteDataTable(sc_14217.ssp_appserver_14221(), dbDataParameter, dataManager.Parameter("parID", (object) dbObject1.ID));
      if (dataTable1.Rows.Count == 0)
        throw new KernelExceptionID(376, (object) this.SnapshotID, (object) dbObject1.NameInMessages, (object) dbObject1.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject1.ObjectID));
      if (dataTable1.Rows.Count > 1)
        throw new KernelException($"{sc_14217.ssp_appserver_14222()}{this.SnapshotID.ToString()}, ObjectID = {objectID.ToString()}");
      List<long> restoredObjects = new List<long>();
      List<long> toObjects = new List<long>();
      List<IDBRelation> exactRelations = new List<IDBRelation>();
      (dbObject1 as DBObject).BeforeRestoreSnapshot((IDBObjectSnapshot) this);
      bool flag = this.SnapshotToObject(objectID, dbObject1.ID, Convert.ToInt32(dataTable1.Rows[0][1]), Convert.ToInt64(dataTable1.Rows[0][0]), restoredObjects, toObjects, exactRelations, abortOnError);
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        for (int index = 0; index < restoredObjects.Count; ++index)
          stringBuilder.Append(restoredObjects[index].ToString() + ",");
        if (stringBuilder.Length > 0)
        {
          --stringBuilder.Length;
          DataTable dataTable2 = dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_OBJECT_TYPE, F_ID FROM IMS_OBJ_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID AND F_OBJECT_ID NOT IN ({stringBuilder.ToString()})", dbDataParameter);
          for (int index = 0; index < dataTable2.Rows.Count; ++index)
          {
            IDBObject objectByVersionsRule = this.UserSession.GetObjectByVersionsRule(Convert.ToInt64(dataTable2.Rows[index][2]), "cad005aa-306c-11d8-b4e9-00304f19f545", false);
            this.SnapshotToObject(objectByVersionsRule != null ? objectByVersionsRule.ObjectID : Convert.ToInt64(dataTable2.Rows[index][0]), Convert.ToInt64(dataTable2.Rows[index][2]), Convert.ToInt32(dataTable2.Rows[index][1]), Convert.ToInt64(dataTable2.Rows[index][0]), restoredObjects, toObjects, exactRelations, abortOnError);
          }
        }
      }
      for (int index1 = 0; index1 < exactRelations.Count; ++index1)
      {
        IDBAttribute attributeById = exactRelations[index1].GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID);
        if (attributeById != null)
        {
          long asInteger = attributeById.AsInteger;
          for (int index2 = 0; index2 < restoredObjects.Count; ++index2)
          {
            if (Math.Abs(restoredObjects[index2]) == asInteger)
            {
              attributeById.AsInteger = Math.Abs(toObjects[index2]);
              break;
            }
          }
        }
      }
      this.UserSession.ClearObjectSmartCache();
      IDBObject dbObject2 = this.UserSession.GetObject(objectID);
      (dbObject2 as DBObject).AfterRestoreSnapshot((IDBObjectSnapshot) this);
      if (flag)
      {
        IDBAttribute attributeById = dbObject2.GetAttributeByID(this.UserSession.IdentHelper.ActiveSnapshotID);
        if (attributeById == null)
          dbObject2.Attributes.AddAttribute(this.UserSession.IdentHelper.ActiveSnapshotID, false, new object[1]
          {
            (object) this.SnapshotID
          });
        else
          attributeById.AsInteger = this.SnapshotID;
      }
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  private bool SnapshotToObject(
    long objectID,
    long id,
    int objectTypeID,
    long fromObjectID,
    List<long> restoredObjects,
    List<long> toObjects,
    List<IDBRelation> exactRelations,
    bool abortOnError)
  {
    bool flag1 = restoredObjects.Count == 0;
    restoredObjects.Add(fromObjectID);
    toObjects.Add(objectID);
    long EventID = this.AddEvent(objectID, ActionType.Restore, EventlogRecordType.AccessGranted, string.Format(LocalizationHolder.rm.GetString("RestoreFromSnapshot"), (object) this.SnapshotName));
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("snapID", (object) this.SnapshotID);
    IDbDataParameter objID = dataManager.Parameter("objID", (object) objectID);
    int firstStep1 = this.UserSession.GetLifecycleStepCollection(objectTypeID).GetFirstStep();
    int num1 = firstStep1;
    int levelId = this.UserSession.GetLifecycleStep(firstStep1).LevelID;
    this.UserSession.ClearObjectSmartCache();
    IDBObject dbObject1 = this.UserSession.GetObject(objectID, false);
    DataTable dataTable1 = (DataTable) null;
    string str1 = objectID >= 0L ? "CAPTION" : "F_WORK_CAPTION";
    string str2 = dataManager.ExecuteScalar("SELECT CAPTION FROM IMS_OBJ_SNAPSHOT WHERE F_ID = :id1 AND F_SNAPSHOT_ID = :snapID", dbDataParameter, dataManager.Parameter("id1", (object) id)).ToString();
    bool flag2 = true;
    if (dbObject1 != null)
    {
      if (flag1)
      {
        if (dbObject1.LCStep != firstStep1)
          throw new KernelExceptionID(sc_14217.ssp_appserver_14223(313176648), (object) dbObject1.VersionID, (object) dbObject1.NameInMessages);
        if (abortOnError)
          dbObject1.CheckEdit();
        else
          flag2 = !dbObject1.ReadOnly;
      }
      else
      {
        if ((dbObject1 as IDBLifecycleLevel).LevelID == this.UserSession.IdentHelper.DeletedID)
        {
          IDBLifecycleStepCollection lifecycleStepCollection = this.UserSession.GetLifecycleStepCollection(dbObject1.ObjectType);
          dbObject1.LCStep = lifecycleStepCollection.GetFirstStep();
          if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject1.CheckoutBy == 0L)
          {
            dbObject1 = dbObject1.CheckOut();
            objectID = dbObject1.ObjectID;
            objID = dataManager.Parameter("objID", (object) objectID);
          }
        }
        if (abortOnError)
          dbObject1.CheckEdit();
        else
          flag2 = !dbObject1.ReadOnly;
        if (dbObject1.LCStep != firstStep1)
          throw new KernelExceptionID(sc_14217.ssp_appserver_14224(2039192515), (object) dbObject1.VersionID, (object) dbObject1.NameInMessages);
      }
      if (flag2)
      {
        num1 = dbObject1.LCStep;
        (dbObject1 as IDBSecurity).CheckAccess(ActionType.Edit);
        (dbObject1.Attributes as DBAttributeCollection).Purge();
        dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_LC_STEP = :stepID, F_LEVEL_ID = :levelID WHERE F_OBJECT_ID = :objID", dataManager.Parameter("stepID", (object) firstStep1), dataManager.Parameter("levelID", (object) levelId), objID);
        if (dbObject1.ObjectType != objectTypeID)
          dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_OBJECT_TYPE = :objTypeID WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objTypeID", (object) objectTypeID), objID);
        dataManager.ExecuteNonQuery($"UPDATE IMS_GUID SET {str1} = :captStr WHERE F_OBJECT_ID = :toObjID", dataManager.Parameter("captStr", (object) str2), dataManager.Parameter("toObjID", (object) Math.Abs(objectID)));
        (dbObject1 as DBObject).DeleteFromView(dbObject1.ObjectType);
        dataTable1 = dataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PROJ_ID = :objID", objID);
      }
    }
    else
    {
      DataTable dataTable2 = dataManager.ExecuteDataTable("SELECT F_OBJ_CREATE, F_PROJECT_ID, F_SITE_ID, F_ACCESS, F_CREATOR_ID FROM IMS_OBJ_SNAPSHOT WHERE F_OBJECT_ID = :fromObjID AND F_SNAPSHOT_ID = :snapID", dataManager.Parameter("fromObjID", (object) fromObjectID), dbDataParameter);
      long objectID1 = Convert.ToInt64(dataTable2.Rows[0][1]);
      if (objectID1 != 0L && this.UserSession.GetObjectLevel(objectID1) == -1)
        objectID1 = 0L;
      Guid guid1 = Guid.NewGuid();
      objectID = 0L;
      long num2 = 0;
      int num3 = 0;
      string empty1 = dataTable2.Rows[0][2].ToString();
      string empty2 = string.Empty;
      if (empty1.Length > 2 && dataManager.DataProvider.Name == "Sql")
      {
        empty2 = dataTable2.Rows[0][2].ToString();
        empty1 = string.Empty;
      }
      dataManager.ExecuteSpNonQuery("IMS_ADD_OBJECT", dataManager.Parameter("inID", (object) 0L), dataManager.Parameter("inOBJECT_TYPE", (object) objectTypeID), dataManager.Parameter("inOWNER_ID", (object) this.UserSession.UserID), dataManager.Parameter("inLC_STEP", (object) firstStep1), dataManager.Parameter("inGUID", (object) guid1), dataManager.Parameter("inOBJECT_VER_TYPE", (object) 0), dataManager.Parameter("inCAPTION", (object) str2), dataManager.Parameter("inMODIFY_DATE", (object) null), dataManager.Parameter("inCREATE_DATE", (object) Convert.ToDateTime(dataTable2.Rows[0][0])), dataManager.Parameter("inPROJECT_ID", (object) objectID1), dataManager.Parameter("inMODIFICATION_ID", (object) 0L), dataManager.Parameter("inSITE_ID", (object) empty1), dataManager.Parameter("inCREATOR_ID", dataTable2.Rows[0][4]), dataManager.OutputParameter("outOBJECT_ID", (object) objectID), dataManager.OutputParameter("outID", (object) num2), dataManager.OutputParameter("outVERSION_ID", (object) num3));
      objectID = Convert.ToInt64(dataManager.GetOutputParameterValue("outOBJECT_ID"));
      if (objectID < 0L)
      {
        dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_OBJECT_ID = :objID WHERE F_OBJECT_ID = :m_objID", dataManager.Parameter("objID", (object) -objectID), dataManager.Parameter("m_objID", (object) objectID));
        objectID = -objectID;
      }
      objID = dataManager.Parameter("objID", (object) objectID);
      if (empty2 != string.Empty)
        dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_SITE_ID = :siteID1 WHERE F_OBJECT_ID = :objID", dataManager.Parameter("siteID1", (object) empty2), objID);
      if (Convert.ToInt32(dataTable2.Rows[0][3]) > 0)
        dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_ACCESS = :accID WHERE F_OBJECT_ID = :objID", objID, dataManager.Parameter("accID", (object) Convert.ToInt32(dataTable2.Rows[0][3])));
      dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_ID = :oldID WHERE F_OBJECT_ID = :objID", dataManager.Parameter("oldID", (object) id), objID);
      Guid guid2 = Guid.NewGuid();
      dataManager.ExecuteNonQuery("INSERT INTO IMS_GUID_RESOLVE (F_GUID, F_ID, F_CATEGORY_TYPE) VALUES (:guid, :id1, :typ)", dataManager.Parameter("guid", (object) guid2.ToString()), dataManager.Parameter("id1", (object) id), dataManager.Parameter("typ", (object) 2));
    }
    if (flag2)
    {
      if (num1 != firstStep1)
      {
        long num4 = 0;
        dataManager.ExecuteSpNonQuery("IMS_ADD_LCSTART_DATE", dataManager.Parameter("inOBJECT_ID", (object) Math.Abs(objectID)), dataManager.Parameter("inLC_STEP", (object) firstStep1), dataManager.Parameter("inSTART_DATE", (object) DateTime.UtcNow), dataManager.OutputParameter("outKEY_ID", (object) num4));
      }
      dataManager.ExecuteNonQuery($"INSERT INTO {this.UserSession.DBCache.GetAttributesTableName(objectTypeID)} (F_ATTRIBUTE_ID, F_OBJECT_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) SELECT F_ATTRIBUTE_ID, :objID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE FROM IMS_OBJ_SNAPATTRS WHERE F_SNAPSHOT_ID = :snapID AND F_OBJECT_ID = :fromObjID", dbDataParameter, objID, dataManager.Parameter("fromObjID", (object) fromObjectID));
      this.CopyMemoBlobs(objID, this.UserSession.DBCache.GetAttributesTableName(objectTypeID), "F_OBJECT_ID", id);
      this.UserSession.ClearObjectSmartCache();
      IDBObject dbObject2 = this.UserSession.GetObject(objectID, false);
      (dbObject2 as DBObject).InsertIntoView(false, "0", dbObject2.CheckoutBy);
      for (int AttrIndex = 0; AttrIndex < dbObject2.Attributes.Count; ++AttrIndex)
      {
        DBAttribute attribute = dbObject2.Attributes[AttrIndex] as DBAttribute;
        attribute.InsertIntoView(1);
        if ((attribute.AttributeType.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
        {
          for (int index = 0; index < attribute.ValuesCount; ++index)
          {
            attribute.Index = index;
            if (attribute.IsNull)
              this.UserSession.AddAttrToIndexQueue(string.Empty, (IDBAttribute) attribute);
            else
              this.UserSession.AddAttrToIndexQueue(attribute.AsString, (IDBAttribute) attribute);
          }
          attribute.Index = 0;
        }
        if (attribute.AttributeType.AttributeType == FieldTypes.ftObjectLink)
        {
          string attributesTableName = this.UserSession.DBCache.GetAttributesTableName(dbObject2.ObjectType);
          dataManager.ExecuteNonQuery($"INSERT INTO IMS_OBJECT_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TOOBJECT_ID) SELECT :objID, F_ATTRIBUTE_ID, F_INLIST_ID, F_INTEGER_VALUE FROM {attributesTableName} WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INTEGER_VALUE IS NOT NULL", objID, dataManager.Parameter("attrID", (object) attribute.AttributeID));
        }
        else if (attribute.AttributeType.AttributeType == FieldTypes.ftObjectLinkByID)
        {
          string attributesTableName = this.UserSession.DBCache.GetAttributesTableName(dbObject2.ObjectType);
          dataManager.ExecuteNonQuery($"INSERT INTO IMS_ID_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TO_ID) SELECT :objID, F_ATTRIBUTE_ID, F_INLIST_ID, F_INTEGER_VALUE FROM {attributesTableName} WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INTEGER_VALUE IS NOT NULL", objID, dataManager.Parameter("attrID", (object) attribute.AttributeID));
        }
      }
      if (dataTable1 != null && dataTable1.Rows.Count > 0)
      {
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
        {
          IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(dataTable1.Rows[index][0]), false);
          if (relation != null)
          {
            (relation.Attributes as DBAttributeCollection).Purge();
            relation.Delete((long) (Consts.DontCheckApplicabilityModes | Consts.PurgeMode));
          }
        }
      }
      DataTable dataTable3 = dataManager.ExecuteDataTable("SELECT F_PRJLINK_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR FROM IMS_REL_SNAPSHOT WHERE F_PROJ_ID = :fromObjID AND F_SNAPSHOT_ID = :snapID", dbDataParameter, dataManager.Parameter("fromObjID", (object) fromObjectID));
      for (int index = 0; index < dataTable3.Rows.Count; ++index)
      {
        long int64_1 = Convert.ToInt64(dataTable3.Rows[index][1]);
        IDBObject objectById = this.UserSession.GetObjectByID(int64_1, false);
        if (objectById == null)
        {
          object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJ_SNAPSHOT WHERE F_ID = :id_val AND F_SNAPSHOT_ID = :snapID", dataManager.Parameter("id_val", (object) int64_1), dbDataParameter);
          if (obj == null || obj == DBNull.Value)
            continue;
        }
        else if ((objectById as IDBLifecycleLevel).LevelID == this.UserSession.IdentHelper.DeletedID)
        {
          object obj1 = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id_val AND F_LEVEL_ID <> :deletedID", dataManager.Parameter("id_val", (object) int64_1), dataManager.Parameter("deletedID", (object) this.UserSession.IdentHelper.DeletedID));
          if (obj1 == null || obj1 == DBNull.Value)
          {
            int firstStep2 = this.UserSession.GetLifecycleStepCollection(objectById.ObjectType).GetFirstStep();
            if (objectById.IsBaseVersion)
            {
              objectById.LCStep = firstStep2;
              if (objectById.ObjectModifyMode == ObjectModifyModes.Checkout && objectById.CheckoutBy == 0L)
                objectById.CheckOut();
            }
            else
            {
              object obj2 = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id_val AND F_BASE_VERSION = 1", dataManager.Parameter("id_val", (object) int64_1));
              if (obj2 != null && obj2 != DBNull.Value)
              {
                IDBObject dbObject3 = this.UserSession.GetObject(Convert.ToInt64(obj2));
                dbObject3.LCStep = firstStep2;
                if (dbObject3.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject3.CheckoutBy == 0L)
                  dbObject3.CheckOut();
              }
              else
                continue;
            }
          }
        }
        long num5 = 0;
        dataManager.ExecuteSpNonQuery("IMS_ADD_RELATION", dataManager.Parameter("inPRJLINK_ID", (object) 0L), dataManager.Parameter("inPROJ_ID", (object) objectID), dataManager.Parameter("inPART_ID", dataTable3.Rows[index][1]), dataManager.Parameter("inRELATION_TYPE", dataTable3.Rows[index][2]), dataManager.Parameter("inCREATE_DATE", dataTable3.Rows[index][3]), dataManager.Parameter("inPRJ_GUID", dataTable3.Rows[index][4]), dataManager.Parameter("inREL_CREATOR", dataTable3.Rows[index][5]), dataManager.OutputParameter("outPRJLINK_ID", (object) num5));
        long int64_2 = Convert.ToInt64(dataManager.GetOutputParameterValue("outPRJLINK_ID"));
        dataManager.ExecuteNonQuery("INSERT INTO IMS_RELATION_ATTRS (F_PRJLINK_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) SELECT :newLinkID, F_ATTRIBUTE_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE FROM IMS_REL_SNAPATTRS A WHERE A.F_PRJLINK_ID = :oldLinkID AND A.F_SNAPSHOT_ID = :snapID", dataManager.Parameter("newLinkID", (object) int64_2), dbDataParameter, dataManager.Parameter("oldLinkID", dataTable3.Rows[index][0]));
      }
      DataTable dataTable4 = dataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PROJ_ID = :objID", objID);
      for (int index = 0; index < dataTable4.Rows.Count; ++index)
      {
        objID.Value = dataTable4.Rows[index][0];
        this.CopyMemoBlobs(objID, "IMS_RELATION_ATTRS", "F_PRJLINK_ID", 0L);
        if (this.UserSession.GetRelation(Convert.ToInt64(dataTable4.Rows[index][0]), false) is DBRelation relation)
        {
          relation.InsertIntoView();
          for (int AttrIndex = 0; AttrIndex < relation.Attributes.Count; ++AttrIndex)
            (relation.Attributes[AttrIndex] as DBAttribute).InsertIntoView(1);
          if (relation.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID) != null)
            exactRelations.Add((IDBRelation) relation);
        }
      }
      this.UserSession.DBCache.DeleteObjectInfo(objectID, dbObject2.ObjectGUID);
    }
    this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
    return flag2;
  }

  private void CopyMemoBlobs(
    IDbDataParameter objID,
    string tableName,
    string keyFieldName,
    long id)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable(string.Format("SELECT {0}, F_ATTRIBUTE_ID, F_INLIST_ID, F_INTEGER_VALUE, F_DOUBLE_VALUE FROM {1} WHERE {0} = :objID", (object) keyFieldName, (object) tableName), objID);
    IDbDataParameter dbDataParameter = dataManager.Parameter("currDate", (object) DateTime.UtcNow);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(dataTable.Rows[index][1]));
      long fileID = 0;
      if (dataTable.Rows[index][3] != null && dataTable.Rows[index][3] != DBNull.Value)
        fileID = Convert.ToInt64(dataTable.Rows[index][3]);
      if (attributeType != null && fileID > 0L)
      {
        long num = 0;
        string str = string.Empty;
        switch (attributeType.FieldType)
        {
          case FieldTypes.ftShortBlob:
            if (dataManager.DataProvider.Name != "Sql")
            {
              num = dataManager.DataProvider.NextGeneratorValue("IMS_BLOBS_GEN", dataManager);
              dataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS (F_KEY, F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE) SELECT :newKey, F_VALUE, F_FILESIZE, :currDate, F_ARC_METHOD, F_ZIPSIZE FROM IMS_BLOBS_SNAPSHOT WHERE F_KEY = :oldKey", dataManager.Parameter("newKey", (object) Convert.ToInt32(num)), dbDataParameter, dataManager.Parameter("oldKey", (object) Convert.ToInt32(dataTable.Rows[index][3])));
            }
            else
            {
              using (dataManager.WithOpenConnection())
              {
                dataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS (F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE) SELECT F_VALUE, F_FILESIZE, :currDate, F_ARC_METHOD, F_ZIPSIZE FROM IMS_BLOBS_SNAPSHOT WHERE F_KEY = :oldKey", dbDataParameter, dataManager.Parameter("oldKey", (object) Convert.ToInt32(dataTable.Rows[index][3])));
                num = Convert.ToInt64(dataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
              }
            }
            str = ", F_DATE_VALUE = :currDate";
            break;
          case FieldTypes.ftFile:
          case FieldTypes.ftBlob:
            num = dataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", dataManager);
            IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
            IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable.Rows[index][4]), (IUserSession) this.UserSession);
            try
            {
              FileInfoStruct fileStruct = storage.GetFileStruct(fileID, true);
              fileStruct.FileID = num;
              fileStruct.ObjectLinkID = Convert.ToInt64(objID.Value);
              DateTime dateTime = DateTime.UtcNow;
              dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
              dbDataParameter.Value = (object) dateTime;
              fileStruct.ModifyDate = dateTime;
              storage.CopyToTemporaryFile(fileStruct);
              storage.SetFileStruct(fileStruct);
              str = $", F_DOUBLE_VALUE = {storage.StorageID.ToString()}, F_DATE_VALUE = :currDate";
              if (fileStruct.AttributeID == this.UserSession.IdentHelper.FileAttributeID && id > 0L)
                dataManager.ExecuteNonQuery("INSERT INTO IMS_FILENAMES (F_FILENAME, F_KEY, F_ID) VALUES (:fname, :objID, :id1)", dataManager.Parameter("fname", (object) fileStruct.FileName.Trim().ToUpper()), objID, dataManager.Parameter("id1", (object) id));
              if (fileStruct.FileBody != null)
              {
                fileStruct.FileBody.Close();
                break;
              }
              break;
            }
            finally
            {
              service.ReleaseStorage(storage);
            }
          case FieldTypes.ftMemo:
            if (dataManager.DataProvider.Name != "Sql")
            {
              num = dataManager.DataProvider.NextGeneratorValue("IMS_MEMOS_GEN", dataManager);
              dataManager.ExecuteNonQuery("INSERT INTO IMS_MEMOS (F_KEY, F_VALUE) SELECT :newKey, F_VALUE FROM IMS_MEMOS_SNAPSHOT WHERE F_KEY = :oldKey", dataManager.Parameter("newKey", (object) Convert.ToInt32(num)), dataManager.Parameter("oldKey", (object) Convert.ToInt32(dataTable.Rows[index][3])));
            }
            else
            {
              using (dataManager.WithOpenConnection())
              {
                dataManager.ExecuteNonQuery("INSERT INTO IMS_MEMOS (F_VALUE) SELECT F_VALUE FROM IMS_MEMOS_SNAPSHOT WHERE F_KEY = :oldKey", dataManager.Parameter("oldKey", (object) Convert.ToInt32(dataTable.Rows[index][3])));
                num = Convert.ToInt64(dataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
              }
            }
            str = ", F_DATE_VALUE = :currDate";
            break;
        }
        if (num > 0L)
          dataManager.ExecuteNonQuery($"UPDATE {tableName} SET F_INTEGER_VALUE = :newKey{str} WHERE {keyFieldName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID", dataManager.Parameter("newKey", (object) num), dataManager.Parameter(nameof (objID), (object) Convert.ToInt64(dataTable.Rows[index][0])), dataManager.Parameter("attrID", (object) Convert.ToInt32(dataTable.Rows[index][1])), dataManager.Parameter("inlistID", (object) Convert.ToInt32(dataTable.Rows[index][2])), dbDataParameter);
      }
    }
  }

  public DataTable GetAttributes(long objectID)
  {
    return this.UserSession.DataManager.ExecuteDataTable("SELECT * FROM IMS_OBJ_SNAPATTRS WHERE F_SNAPSHOT_ID = :snapID AND F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("snapID", (object) this.SnapshotID), this.UserSession.DataManager.Parameter("objID", (object) Math.Abs(objectID)));
  }

  public DataTable ConsistFromSnapshotObjects(long projID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    return dataManager.ExecuteDataTable("SELECT  IMS_OBJ_SNAPSHOT.F_OBJECT_ID, IMS_OBJ_SNAPSHOT.F_ID, IMS_OBJ_SNAPSHOT.F_OBJECT_TYPE, IMS_OBJ_SNAPSHOT.CAPTION, IMS_OBJ_SNAPSHOT.F_LC_STEP, IMS_OBJ_SNAPSHOT.F_OWNER_ID, IMS_OBJ_SNAPSHOT.F_VERSION_ID, IMS_OBJ_SNAPSHOT.F_SITE_ID, IMS_OBJ_SNAPSHOT.F_MODIFICATION_ID, IMS_REL_SNAPSHOT.F_PRJLINK_ID, IMS_REL_SNAPSHOT.F_PROJ_ID, IMS_REL_SNAPSHOT.F_RELATION_TYPE, IMS_REL_SNAPSHOT.F_PRJ_GUID FROM IMS_REL_SNAPSHOT, IMS_OBJ_SNAPSHOT WHERE (IMS_REL_SNAPSHOT.F_SNAPSHOT_ID = :snapID) AND (IMS_REL_SNAPSHOT.F_PROJ_ID = :projID) AND (IMS_OBJ_SNAPSHOT.F_ID = IMS_REL_SNAPSHOT.F_PART_ID) AND (IMS_OBJ_SNAPSHOT.F_SNAPSHOT_ID = :snapID)", dataManager.Parameter("snapID", (object) this.SnapshotID), dataManager.Parameter(nameof (projID), (object) Math.Abs(projID)));
  }

  public DataTable ConsistFrom(string orderBy)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    return dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_OBJECT_TYPE, CAPTION, F_VERSION_ID, F_ID, F_LC_STEP, F_LEVEL_ID, F_OWNER_ID, {dataManager.DataProvider.GetUTCSelect("F_OBJ_CREATE", this.UserSession.TimeZoneOffset)} F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID FROM IMS_OBJ_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID", dataManager.Parameter("snapID", (object) this.SnapshotID));
  }

  public int DeleteObject(IDBObject obj)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("snapID", (object) this.SnapshotID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("objID", (object) Math.Abs(obj.ObjectID));
    object obj1 = dataManager.ExecuteScalar("SELECT F_PROJ_ID FROM IMS_REL_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID AND F_PART_ID = :partID", dbDataParameter1, dataManager.Parameter("partID", (object) obj.ID));
    if (obj1 == null || obj1 == DBNull.Value)
    {
      this.UserSession.StartTransaction();
      try
      {
        DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT F_ATTRIBUTE_ID, F_DOUBLE_VALUE, F_INTEGER_VALUE FROM IMS_OBJ_SNAPATTRS WHERE F_SNAPSHOT_ID = :snapID AND F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter2);
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
          this.DeleteBlobs4Object(dataTable1.Rows[index]);
        DataTable dataTable2 = dataManager.ExecuteDataTable("SELECT A.F_ATTRIBUTE_ID, A.F_DOUBLE_VALUE, A.F_INTEGER_VALUE FROM IMS_REL_SNAPSHOT R, IMS_REL_SNAPATTRS A WHERE R.F_SNAPSHOT_ID = :snapID AND R.F_PROJ_ID = :objID AND A.F_PRJLINK_ID = R.F_PRJLINK_ID AND A.F_SNAPSHOT_ID = :snapID", dbDataParameter1, dbDataParameter2);
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
          this.DeleteBlobs4Object(dataTable2.Rows[index]);
        dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJ_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID AND F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter2);
        dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJ_SNAPATTRS WHERE F_SNAPSHOT_ID = :snapID AND F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter2);
        dataManager.ExecuteNonQuery("DELETE FROM IMS_REL_SNAPATTRS WHERE F_SNAPSHOT_ID = :snapID AND F_PRJLINK_ID IN (SELECT F_PRJLINK_ID FROM IMS_REL_SNAPSHOT WHERE IMS_REL_SNAPSHOT.F_SNAPSHOT_ID = :snapID AND IMS_REL_SNAPSHOT.F_PROJ_ID = :objID)", dbDataParameter1, dbDataParameter2);
        dataManager.ExecuteNonQuery("DELETE FROM IMS_REL_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID AND F_PROJ_ID = :objID", dbDataParameter1, dbDataParameter2);
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
    return 0;
  }

  private void DeleteBlobs4Object(DataRow dataRow)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(dataRow[0]));
    if (attributeType == null)
      return;
    if (attributeType.FieldType == FieldTypes.ftBlob || attributeType.FieldType == FieldTypes.ftFile)
    {
      IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
      IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataRow[1]), (IUserSession) this.UserSession);
      try
      {
        storage.DeleteFile(Convert.ToInt64(dataRow[2]));
      }
      finally
      {
        service.ReleaseStorage(storage);
      }
    }
    else if (attributeType.FieldType == FieldTypes.ftMemo)
    {
      this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_MEMOS_SNAPSHOT WHERE F_KEY = :keyID", this.UserSession.DataManager.Parameter("keyID", (object) Convert.ToInt32(dataRow[2])));
    }
    else
    {
      if (attributeType.FieldType != FieldTypes.ftShortBlob)
        return;
      this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_BLOBS_SNAPSHOT WHERE F_KEY = :keyID", this.UserSession.DataManager.Parameter("keyID", (object) Convert.ToInt32(dataRow[2])));
    }
  }

  public int Delete(long DeleteMode)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("snapID", (object) this.SnapshotID);
    this.UserSession.StartTransaction();
    try
    {
      this.DeleteSnapshotContent();
      dataManager.ExecuteNonQuery("DELETE FROM IMS_SNAPSHOTS WHERE F_SNAPSHOT_ID = :snapID", dbDataParameter);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
    return 0;
  }

  private void DeleteSnapshotContent()
  {
    IDbManager dbManager = this.UserSession.InTransaction ? this.UserSession.DataManager : throw new KernelException(sc_14217.ssp_appserver_14225());
    IDbDataParameter dbDataParameter = dbManager.Parameter("snapID", (object) this.SnapshotID);
    dbManager.ExecuteNonQuery("DELETE FROM IMS_MEMOS_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID", dbDataParameter);
    dbManager.ExecuteNonQuery("DELETE FROM IMS_BLOBS_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID", dbDataParameter);
    this.DeleteFromStorage("IMS_OBJ_SNAPATTRS");
    this.DeleteFromStorage("IMS_REL_SNAPATTRS");
    dbManager.ExecuteNonQuery("DELETE FROM IMS_OBJ_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID", dbDataParameter);
    dbManager.ExecuteNonQuery("DELETE FROM IMS_REL_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID", dbDataParameter);
    dbManager.ExecuteNonQuery("DELETE FROM IMS_OBJ_SNAPATTRS WHERE F_SNAPSHOT_ID = :snapID", dbDataParameter);
    dbManager.ExecuteNonQuery("DELETE FROM IMS_REL_SNAPATTRS WHERE F_SNAPSHOT_ID = :snapID", dbDataParameter);
  }

  private void DeleteFromStorage(string p)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable($"SELECT F_ATTRIBUTE_ID, F_DOUBLE_VALUE, F_INTEGER_VALUE FROM {p} WHERE F_SNAPSHOT_ID = :snapID", dataManager.Parameter("snapID", (object) this.SnapshotID));
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(dataTable.Rows[index][0]));
      if (attributeType != null && (attributeType.FieldType == FieldTypes.ftBlob || attributeType.FieldType == FieldTypes.ftFile))
      {
        IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable.Rows[index][1]), (IUserSession) this.UserSession);
        try
        {
          storage.DeleteFile(Convert.ToInt64(dataTable.Rows[index][2]));
        }
        finally
        {
          service.ReleaseStorage(storage);
        }
      }
    }
  }

  public bool ReplaceSnapStorageID(
    string fldName,
    string tblName,
    long fileID,
    long storageID,
    long newStorageID)
  {
    bool flag = false;
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable($"SELECT {fldName}, F_ATTRIBUTE_ID, F_INLIST_ID FROM {tblName} WHERE F_SNAPSHOT_ID = :snapID AND F_INTEGER_VALUE = :fileID AND F_DOUBLE_VALUE = :storID", dataManager.Parameter("snapID", (object) this.SnapshotID), dataManager.Parameter(nameof (fileID), (object) fileID), dataManager.Parameter("storID", (object) storageID));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IDBAttributeType attributeType = this.UserSession.GetAttributeType(Convert.ToInt32(dataTable.Rows[index][1]), false);
      if (attributeType != null && (attributeType.AttributeType == FieldTypes.ftFile || attributeType.AttributeType == FieldTypes.ftBlob))
      {
        dataManager.ExecuteNonQuery($"UPDATE {tblName} SET F_DOUBLE_VALUE = :newStorID WHERE F_SNAPSHOT_ID = :snapID AND {fldName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID", dataManager.Parameter("newStorID", (object) newStorageID), dataManager.Parameter("snapID", (object) this.SnapshotID), dataManager.Parameter("objID", dataTable.Rows[index][0]), dataManager.Parameter("attrID", dataTable.Rows[index][1]), dataManager.Parameter("inlistID", dataTable.Rows[index][2]));
        flag = true;
        break;
      }
    }
    return flag;
  }
}
