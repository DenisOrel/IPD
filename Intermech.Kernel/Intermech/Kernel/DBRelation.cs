// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelation
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.DelayedNotifications;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public class DBRelation : 
  DBAttributable,
  IDBRelation,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDeletable,
  IDBGuid
{
  private long _RelationID;
  private IDBRelationType _RelationTypeObject;
  private IDBRelationsApplicability _Applicability;
  internal IDBObject _SenderObject;
  internal long _PartObjectID;
  internal IDBObject _ProjObject;
  internal IDBObject _PartObject;
  private string _FiltrationOwnerID = "cad001e2-306c-11d8-b4e9-00304f19f545";
  private IDBSecurity _RTSecurity;
  public bool DontDeleteChildObjectMode;
  private static IDBEditingContextsServerService _editingContextsServerService;
  private long _DeleteEventID;

  public static IDBEditingContextsServerService EditingContextsServerService
  {
    get
    {
      if (DBRelation._editingContextsServerService == null)
        DBRelation._editingContextsServerService = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
      return DBRelation._editingContextsServerService;
    }
  }

  public DBRelation(UserSession uSession, DataTable relationsTable)
    : base(uSession)
  {
    if (relationsTable.Rows.Count == 0)
      throw new KernelException(sc_13537.ssp_appserver_13538());
    this.paramsTable.Create(relationsTable.Rows[0]);
    this._RelationID = Convert.ToInt64(relationsTable.Rows[0]["F_PRJLINK_ID"]);
    this.InitSecurityOptions(5, this._RelationID);
  }

  private IDBSecurity RTSecurity
  {
    get
    {
      if (this._RTSecurity == null)
        this._RTSecurity = !RelationTypeSecurity.DontCacheAccess4Types.TryGetValue(this.RelationType, out bool _) || this.CreatorID <= 0L ? this.RelationTypeObject as IDBSecurity : (IDBSecurity) new RelationTypeSecurity(this.UserSession, this.RelationType, this.CreatorID);
      return this._RTSecurity;
    }
  }

  public override string ObjectName
  {
    get
    {
      string nameInMessages1;
      long num;
      if (this._ProjObject != null)
      {
        nameInMessages1 = this._ProjObject.NameInMessages;
      }
      else
      {
        num = this.ProjID;
        nameInMessages1 = num.ToString();
      }
      string nameInMessages2;
      if (this._PartObject != null)
      {
        nameInMessages2 = this._PartObject.NameInMessages;
      }
      else
      {
        num = this.PartID;
        nameInMessages2 = num.ToString();
      }
      return string.Format(LocalizationHolder.rm.GetString("Kernel_493"), (object) this.UserSession.GetRelationType(this.RelationType).Description, (object) nameInMessages1, (object) nameInMessages2);
    }
  }

  public string FiltrationOwnerID
  {
    get => this._FiltrationOwnerID;
    set
    {
      if (!(value != this._FiltrationOwnerID))
        return;
      this._FiltrationOwnerID = value;
    }
  }

  public IDBObject SenderObject
  {
    get => this._SenderObject;
    set => this._SenderObject = value;
  }

  public IDBObject ProjObject
  {
    get
    {
      if (this._ProjObject == null)
        this._ProjObject = this.UserSession.GetObject(this.ProjID);
      return this._ProjObject;
    }
  }

  public long PartObjectID
  {
    get
    {
      if (this._PartObjectID == 0L && this.RelationTypeObject.HasAttribute(this.UserSession.IdentHelper.CompositionVersionID))
      {
        IDBAttribute attributeById = this.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID);
        if (attributeById != null && attributeById.AsInteger > 0L)
        {
          object obj1 = this.UserSession.DataManager.ExecuteScalar("SELECT F_CHKOUT_BY FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("objID", (object) attributeById.AsInteger));
          if (obj1 != null && obj1 != DBNull.Value)
          {
            if (Convert.ToInt64(obj1) == this.UserSession.UserID)
              this._PartObjectID = -attributeById.AsInteger;
          }
          else
          {
            object obj2 = this.UserSession.DataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("objID", (object) -attributeById.AsInteger));
            if (obj2 != null && obj2 != DBNull.Value)
              this._PartObjectID = -attributeById.AsInteger;
          }
        }
      }
      return this._PartObjectID;
    }
  }

  public IDBObject PartObject
  {
    get
    {
      if (this._PartObject == null && this._PartObjectID != 0L)
        this._PartObject = this.UserSession.GetObject(this._PartObjectID, false);
      return this._PartObject;
    }
  }

  public override IDBAttributeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
        this._Attributes = (IDBAttributeCollection) new DBRelationAttributeCollection(this.UserSession, this.RelationID, this.RelationType, (IDBAttributable) this);
      return this._Attributes;
    }
  }

  public long RelationID => this._RelationID;

  protected virtual void DoRemove(long newProjID, int newRelationTypeID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    if (newRelationTypeID != this.RelationType)
    {
      IDBRelationType relationType = this.UserSession.GetRelationType(newRelationTypeID);
      if (!relationType.AnyAttributes)
      {
        for (int AttrIndex = this.Attributes.Count - 1; AttrIndex >= 0; --AttrIndex)
        {
          if (relationType.Attributes.GetAttributeByID(this.Attributes[AttrIndex].AttributeID, false) == null)
            (this.Attributes[AttrIndex] as DBAttribute).Purge(false);
        }
      }
      foreach (DataRow row in (InternalDataCollectionBase) relationType.Attributes.Select(string.Empty).Rows)
      {
        if (Convert.ToInt32(row["F_REQUIRED"]) == Convert.ToInt32((object) RequiredModes.Auto) || Convert.ToInt32(row["F_REQUIRED"]) == Convert.ToInt32((object) RequiredModes.AutoRequired))
          (this.Attributes as DBAttributeCollection).AddAttribute(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), false, false);
      }
      this.DeleteFromView();
      this.paramsTable[145] = (object) newRelationTypeID;
      this._RelationTypeObject = (IDBRelationType) null;
      this._Attributes = (IDBAttributeCollection) null;
      dataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_PROJ_ID = :val, F_RELATION_TYPE = :rtype WHERE F_PRJLINK_ID = :id", dataManager.Parameter("val", (object) newProjID), dataManager.Parameter("rtype", (object) newRelationTypeID), dataManager.Parameter("id", (object) this.RelationID));
      this.InsertIntoView();
      this.RebuildComputedAttrs();
      for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
        (this.Attributes[AttrIndex] as DBAttribute).InsertIntoView(1);
    }
    else
    {
      dataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_PROJ_ID = :val WHERE F_PRJLINK_ID = :id", dataManager.Parameter("val", (object) newProjID), dataManager.Parameter("id", (object) this.RelationID));
      this.UpdateViewValue("F_PROJ_ID", (object) newProjID);
    }
    this.paramsTable[139] = (object) newProjID;
    object obj = dataManager.ExecuteScalar("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :dublID", dataManager.Parameter("dublID", (object) -this.RelationID));
    if (obj == null || obj == DBNull.Value)
      return;
    this.GenNewRelationID();
  }

  internal void GenNewRelationID()
  {
    this.UserSession.StartTransaction();
    try
    {
      IDbManager dataManager = this.UserSession.DataManager;
      Guid guid = Guid.NewGuid();
      long num = dataManager.DataProvider.NextGeneratorValue("IMS_OBJECTS_GEN", dataManager);
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("newRelID", (object) num);
      IDbDataParameter dbDataParameter2 = dataManager.Parameter("oldRelID", (object) this.RelationID);
      dataManager.ExecuteNonQuery("INSERT INTO IMS_RELATIONS (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR) SELECT :newRelID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, :newRelGuid, F_REL_CREATOR  FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :oldRelID", dbDataParameter1, dbDataParameter2, dataManager.Parameter("newRelGuid", (object) guid));
      dataManager.ExecuteNonQuery("UPDATE IMS_RELATION_ATTRS SET F_PRJLINK_ID = :newRelID WHERE F_PRJLINK_ID = :oldRelID", dbDataParameter1, dbDataParameter2);
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, this.RelationType);
      if (updateTables != null)
      {
        string format = "UPDATE {0} SET F_PRJLINK_ID = :newRelID, F_PRJ_GUID = :newRelGuid WHERE F_PRJLINK_ID = :oldRelID";
        foreach (string str in updateTables)
          dataManager.ExecuteNonQuery(string.Format(format, (object) str), dbDataParameter1, dbDataParameter2, dataManager.Parameter("newRelGuid", (object) guid));
      }
      dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :oldRelID", dbDataParameter2);
      this.UserSession.Commit();
      this.paramsTable[71] = (object) num;
      this._RelationID = num;
      this.paramsTable[33] = (object) guid.ToString();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public long ProjID
  {
    get => Convert.ToInt64(this.paramsTable[139]);
    set
    {
      if (this.ProjID == value)
        return;
      EventlogRecordType auditType = EventlogRecordType.Error;
      Guid guid = this.GUID;
      long projId = this.ProjID;
      IDBObject objectByVersionsRule = this.UserSession.GetObjectByVersionsRule(this.PartID, this.FiltrationOwnerID, true);
      IDBObject projObject = this.ProjObject;
      IDBObject toObject = this.UserSession.GetObject(value);
      if (projObject.AccessLevel > toObject.AccessLevel)
      {
        object obj = this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_ACCESS > :accLevel", this.UserSession.DataManager.Parameter("id1", (object) objectByVersionsRule.ID), this.UserSession.DataManager.Parameter("accLevel", (object) toObject.AccessLevel));
        if (obj != null && obj != DBNull.Value && Convert.ToInt32(obj) > 0)
          throw new KernelExceptionID(432, (object) objectByVersionsRule.NameInMessages, (object) toObject.NameInMessages, (object) this.UserSession.DBCache.GetAccessCaption(toObject.AccessLevel), (object) Convert.ToInt32(obj));
      }
      this.UserSession.StartTransaction();
      try
      {
        this.ValidateEditRelation(true);
        (objectByVersionsRule as DBObject).BeforeRemoveObject(this, value);
        IDBRelationsApplicability relationsApplicability1 = this.UserSession.GetRelationsApplicabilityCollection().GetApplicability(this.RelationType, objectByVersionsRule.ObjectType, toObject.ObjectType) ?? this.UserSession.GetRelationsApplicabilityCollection().GetApplicability(-1, objectByVersionsRule.ObjectType, toObject.ObjectType);
        if (relationsApplicability1 == null)
          throw new KernelExceptionID(sc_13537.ssp_appserver_13539(1589520119), (object) this.UserSession.GetObjectType(toObject.ObjectType).ObjectInstanceName, (object) this.UserSession.GetObjectType(objectByVersionsRule.ObjectType).ObjectInstanceName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(toObject.ObjectID), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(objectByVersionsRule.ObjectID));
        if (relationsApplicability1.ApplicabilityMode == ApplicabilityModes.Disabled)
          throw new KernelExceptionID(sc_13537.ssp_appserver_13540(531537568), (object) this.UserSession.GetObjectType(toObject.ObjectType).ObjectInstanceName, (object) this.UserSession.GetObjectType(objectByVersionsRule.ObjectType).ObjectInstanceName);
        IDBRelationsApplicability relationsApplicability2;
        if (toObject.ObjectType != projObject.ObjectType)
        {
          this.CheckRemoveApplicabilities(projObject, toObject, objectByVersionsRule);
          relationsApplicability2 = this.UserSession.GetRelationsApplicabilityCollection().GetApplicability(this.RelationType, objectByVersionsRule.ObjectType, projObject.ObjectType);
        }
        else
          relationsApplicability2 = relationsApplicability1;
        if ((relationsApplicability1.Options & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.None && this.UserSession.GetRelation(value, this.PartID, relationsApplicability1.RelationType) != null)
          throw new KernelExceptionID(sc_13537.ssp_appserver_13541(394196791), (object) objectByVersionsRule.Caption, (object) objectByVersionsRule.ObjectID, (object) toObject.Caption, (object) toObject.ObjectID, (object) this.UserSession.GetRelationType(relationsApplicability1.RelationType).Description).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(objectByVersionsRule.ObjectID), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(toObject.ObjectID));
        if (relationsApplicability1.MaximumLinks < int.MaxValue)
        {
          if (this.UserSession.GetObjectCollection(toObject.ObjectType).Select(new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(0, RelationalOperators.ConsistFrom, (object) this.PartID, LogicalOperators.NONE, 0, true)
            {
              TypeID = (object) relationsApplicability1.RelationType
            }
          }, new object[1]{ (object) -2 })).Rows.Count > relationsApplicability1.MaximumLinks)
            throw new KernelExceptionID(sc_13537.ssp_appserver_13542(1103449599), (object) this.UserSession.GetObjectType(toObject.ObjectType).ObjectTypeName);
        }
        (this.UserSession.GetRelationCollection(relationsApplicability1.RelationType) as DBRelationCollection).CheckCycleLinks(toObject.ID, this.PartID, DateTime.UtcNow.Date, toObject.ObjectID, objectByVersionsRule.ObjectType);
        (this.EventHelper as EventLogHelper).OnBeforeRemoveRelation((IDBRelation) this, (IUserSession) this.UserSession);
        auditType = EventlogRecordType.Error;
        if (relationsApplicability1.IsContent && this.IsCheckParentReadOnly)
        {
          toObject.CheckRelationsEdit();
          (toObject as DBObject).SetModifyContentDate();
          auditType = EventlogRecordType.AccessDenied;
        }
        if (relationsApplicability2.IsContent && this.IsCheckParentReadOnly)
        {
          projObject.CheckRelationsEdit();
          (projObject as DBObject).SetModifyContentDate();
          auditType = EventlogRecordType.AccessDenied;
        }
        this.UserSession.AddDelayedNotification((DelayedNotification) new RelationDelayedNotification(this.UserSession.RealUserID, ActionType.DeleteLink, this.GetAttributes4Notification(), (AttributeValues[]) null, this.RelationID, this.RelationType, this.ProjID, this.PartID, this.PartObjectID, objectByVersionsRule.Caption));
        long relationId = this.RelationID;
        this.DoRemove(value, relationsApplicability1.RelationType);
        this._ProjObject = toObject;
        this.UserSession.AddDelayedNotification((DelayedNotification) new RelationDelayedNotification(this.UserSession.RealUserID, ActionType.AddLink, (AttributeValues[]) null, this.GetAttributes4Notification(), this.RelationID, this.RelationType, this.ProjID, this.PartID, this.PartObjectID, objectByVersionsRule.Caption));
        (this.EventHelper as EventLogHelper).OnAfterRemoveRelation((IDBRelation) this, (IUserSession) this.UserSession);
        (objectByVersionsRule as DBObject).AddEvent(objectByVersionsRule.ObjectID, this.RelationID, ActionType.Remove, EventlogRecordType.Information);
        this.UserSession.AddToModificationsHistory((CategoryValue) new RelationModificationEvent(5, relationId, ActionType.Delete, this.RelationType, guid, projId));
        this.UserSession.AddToModificationsHistory((CategoryValue) new RelationModificationEvent(5, this.RelationID, ActionType.Create, this.RelationType, this.GUID, this.ProjID));
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        (objectByVersionsRule as DBObject).AddEvent(objectByVersionsRule.ObjectID, this.RelationID, ActionType.Remove, auditType, ex.Message);
        throw;
      }
    }
  }

  public long PartID => Convert.ToInt64(this.paramsTable[138]);

  public int RelationType
  {
    get => Convert.ToInt32(this.paramsTable[145]);
    set => throw new OperationNotApplicableException();
  }

  internal bool ValidateEditRelation(bool throwException)
  {
    bool flag = this.RTSecurity.CheckAccess(ActionType.EditLink, true, throwException);
    return this.ValidateEditObject(throwException) & flag;
  }

  internal bool ValidateEditObject(bool throwException)
  {
    return !this.IsCheckParentReadOnly || (this.ProjObject as DBObject).CheckEditMode(true, true, true, throwException);
  }

  internal bool ValidateEditObject(bool validateCheckOut, bool checkAccess, bool throwExeption = true)
  {
    return !this.IsCheckParentReadOnly || (this.ProjObject as DBObject).CheckEditMode(validateCheckOut, checkAccess, true, throwExeption);
  }

  public virtual bool IsCheckParentReadOnly
  {
    get => this.Applicability != null && this.Applicability.IsContent;
  }

  public IDBRelationsApplicability Applicability
  {
    get
    {
      if (this._Applicability == null)
      {
        IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
        int objectType = -1;
        if (this._PartObjectID == 0L)
        {
          DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_ID = :fID1", this.UserSession.DataManager.Parameter("fID1", (object) this.PartID));
          if (dataTable.Rows.Count <= 0)
            throw new KernelException($"Объект N{this.PartID} не найден.");
          objectType = Convert.ToInt32(dataTable.Rows[0][0]);
        }
        else
        {
          IDBObject partObject = this.PartObject;
          if (partObject != null)
            objectType = partObject.ObjectType;
        }
        IDBObject projObject = this.ProjObject;
        if (objectType == -1 || projObject == null)
          return (IDBRelationsApplicability) null;
        this._Applicability = applicabilityCollection.GetApplicability(this.RelationType, objectType, projObject.ObjectType);
      }
      return this._Applicability;
    }
  }

  public override long CreatorID
  {
    get
    {
      object obj = this.paramsTable[182];
      return obj == DBNull.Value ? 0L : Convert.ToInt64(obj);
    }
  }

  internal void SetCreatorID(long crtID)
  {
    if (this.CreatorID == crtID)
      return;
    if (!this.UserSession.IsAdmin)
    {
      this.AddEvent(this.ProjID, this.RelationID, ActionType.TakeOwnership, EventlogRecordType.AccessDenied);
      throw new KernelExceptionID(sc_13537.ssp_appserver_13543(1388369011));
    }
    this.CheckChangeEnable("F_REL_CREATOR");
    this.UserSession.StartTransaction();
    try
    {
      if (crtID != 0L)
      {
        QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(crtID);
        if (objectInfo.Empty)
          throw new KernelException($"Пользователь N{crtID} не найден.");
        this.AddEvent(this.ProjID, this.RelationID, ActionType.TakeOwnership, EventlogRecordType.AccessGranted, objectInfo.Caption);
      }
      else
        this.AddEvent(this.ProjID, this.RelationID, ActionType.TakeOwnership, EventlogRecordType.AccessGranted, "Поле 'Владелец связи' обнулено");
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_REL_CREATOR = :p0 WHERE F_PRJLINK_ID = :p1", this.UserSession.DataManager.Parameter("p0", (object) crtID), this.UserSession.DataManager.Parameter("p1", (object) this.RelationID));
      this.UpdateViewValue("F_REL_CREATOR", (object) crtID);
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.AddEvent(this.ProjID, this.RelationID, ActionType.TakeOwnership, EventlogRecordType.Error, "Ошибка изменения владельца связи: " + ex.Message);
      throw;
    }
  }

  public DateTime CreateDate
  {
    get
    {
      object obj = this.paramsTable[137];
      return obj == DBNull.Value ? DateTime.MinValue : (Convert.ToDateTime(obj) + this.UserSession.TimeZoneOffset).Date;
    }
    set
    {
      if (!(this.CreateDate != value))
        return;
      this.ValidateEditRelation(true);
      this.CheckChangeEnable("F_CREATE_DATE");
      if (value == DateTime.MinValue)
      {
        this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_CREATE_DATE = NULL WHERE F_PRJLINK_ID = :p0", this.UserSession.DataManager.Parameter("p0", (object) this.RelationID));
        this.UpdateViewValue("F_CREATE_DATE", (object) DBNull.Value);
      }
      else
      {
        value -= this.UserSession.TimeZoneOffset;
        value = value.Date;
        this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_CREATE_DATE = :p0 WHERE F_PRJLINK_ID = :p1", this.UserSession.DataManager.Parameter("p0", (object) value), this.UserSession.DataManager.Parameter("p1", (object) this.RelationID));
        this.UpdateViewValue("F_CREATE_DATE", (object) value);
      }
    }
  }

  [Obsolete]
  public DateTime DeleteDate => DateTime.MaxValue;

  public IDBRelationType RelationTypeObject
  {
    get
    {
      if (this._RelationTypeObject == null)
        this._RelationTypeObject = this.UserSession.GetRelationType(this.RelationType);
      return this._RelationTypeObject;
    }
  }

  protected virtual int DoDelete(long DeleteMode)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    bool flag = false;
    for (int AttrIndex = this.Attributes.Count - 1; AttrIndex >= 0; --AttrIndex)
    {
      if (this.Attributes[AttrIndex].DataType == FieldTypes.ftBlob || this.Attributes[AttrIndex].DataType == FieldTypes.ftFile || this.Attributes[AttrIndex].DataType == FieldTypes.ftMemo || this.Attributes[AttrIndex].DataType == FieldTypes.ftShortBlob)
      {
        if (DeleteMode == (long) Consts.PurgeMode)
          (this.Attributes[AttrIndex] as DBAttribute).Purge(true);
        else
          this.Attributes[AttrIndex].Delete((long) Consts.PurgeMode);
      }
      else
        flag = true;
    }
    IDbDataParameter dbDataParameter = dataManager.Parameter("lnkID", (object) this.RelationID);
    if (flag)
    {
      dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATION_ATTRS WHERE F_PRJLINK_ID = :lnkID", dbDataParameter);
      (this.Attributes as DBAttributeCollection).AttrsListClear();
    }
    this.DeleteFromView();
    dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :lnkID", dbDataParameter);
    dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTR_HISTORY WHERE F_ID = :lnkID", dbDataParameter);
    return 0;
  }

  private void CheckRemoveApplicabilities(
    IDBObject fromObject,
    IDBObject toObject,
    IDBObject partObject)
  {
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    IDBRelationsApplicability applicability1 = applicabilityCollection.GetApplicability(this.RelationType, partObject.ObjectType, fromObject.ObjectType);
    if (applicability1 == null)
      return;
    if (applicability1.ApplicabilityMode == ApplicabilityModes.Required)
    {
      object obj = this.UserSession.DataManager.ExecuteScalar($"SELECT F_PROJ_ID FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE F_PART_ID = :id AND F_PRJLINK_ID <> :prjID AND F_RELATION_TYPE = :rType AND (R.F_CREATE_DATE <= :actDate) AND O.F_OBJECT_ID = R.F_PROJ_ID AND O.F_OBJECT_TYPE IN ({(applicability1 as DBRelationsApplicability).GetInObjectTypes()})", this.UserSession.DataManager.Parameter("actDate", (object) DateTime.UtcNow), this.UserSession.DataManager.Parameter("rType", (object) this.RelationType), this.UserSession.DataManager.Parameter("id", (object) this.PartID), this.UserSession.DataManager.Parameter("prjID", (object) this.RelationID));
      if (obj == null || obj == DBNull.Value)
        throw new KernelExceptionID(sc_13537.ssp_appserver_13544(1513587636), (object) partObject.Caption, (object) partObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(partObject.ObjectID));
    }
    else
    {
      if (applicability1.ApplicabilityMode != ApplicabilityModes.AnyRequired)
        return;
      IDBRelationsApplicability applicability2 = applicabilityCollection.GetApplicability(this.RelationType, partObject.ObjectType, toObject.ObjectType);
      if (applicability2 != null && applicability2.ApplicabilityMode == ApplicabilityModes.AnyRequired)
        return;
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        foreach (DataRow row in (InternalDataCollectionBase) applicabilityCollection.GetApplicabilitiesList(this.RelationType, partObject.ObjectType, -1).Rows)
        {
          string inObjectTypes = (applicabilityCollection.GetApplicability(Convert.ToInt32(row["F_APPLICABILITY_ID"])) as DBRelationsApplicability).GetInObjectTypes();
          if (stringBuilder.Length > 0)
            stringBuilder.Append(",");
          stringBuilder.Append(inObjectTypes);
        }
        DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable($"SELECT R.F_PROJ_ID, O.F_OBJECT_TYPE FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE F_PART_ID = :id AND F_PRJLINK_ID <> :prjID AND F_RELATION_TYPE = :rType AND (R.F_CREATE_DATE <= :actDate) AND O.F_OBJECT_ID = R.F_PROJ_ID AND O.F_OBJECT_TYPE IN ({stringBuilder.ToString()})", this.UserSession.DataManager.Parameter("actDate", (object) DateTime.UtcNow), this.UserSession.DataManager.Parameter("rType", (object) this.RelationType), this.UserSession.DataManager.Parameter("id", (object) this.PartID), this.UserSession.DataManager.Parameter("prjID", (object) this.RelationID));
        bool flag = true;
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          IDBRelationsApplicability applicability3 = applicabilityCollection.GetApplicability(this.RelationType, partObject.ObjectType, Convert.ToInt32(dataTable.Rows[index][1]));
          if (applicability3 != null && applicability3.ApplicabilityMode == ApplicabilityModes.AnyRequired)
          {
            flag = false;
            break;
          }
        }
        if (flag)
          throw new KernelExceptionID(sc_13537.ssp_appserver_13545(376931192), (object) partObject.NameInMessages, (object) partObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(partObject.ObjectID));
      }
    }
  }

  private bool CheckDeleteApplicabilities(
    IDBObject projObject,
    IDBObject partObject,
    bool checkAccess,
    bool checkApplicabilities)
  {
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    IDBRelationsApplicability applicability1 = applicabilityCollection.GetApplicability(this.RelationType, partObject.ObjectType, projObject.ObjectType);
    bool flag1 = false;
    if (applicability1 != null)
    {
      flag1 = applicability1.IsContent;
      if (flag1)
      {
        if (checkAccess && this.IsCheckParentReadOnly)
          projObject.CheckRelationsEdit();
      }
      else if (projObject.CheckoutBy != 0L && projObject.ObjectID > 0L)
        this.UserSession.GetRelation(this.GUID, -this.ProjID, false)?.Delete((long) Consts.PurgeMode);
      if (checkApplicabilities)
      {
        if (applicability1.ApplicabilityMode == ApplicabilityModes.Required)
        {
          object obj = this.UserSession.DataManager.ExecuteScalar($"SELECT F_PROJ_ID FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE F_PART_ID = :id AND F_PRJLINK_ID <> :prjID AND F_RELATION_TYPE = :rType AND (R.F_CREATE_DATE <= :actDate) AND O.F_OBJECT_ID = R.F_PROJ_ID AND O.F_OBJECT_TYPE IN ({(applicability1 as DBRelationsApplicability).GetInObjectTypes()})", this.UserSession.DataManager.Parameter("actDate", (object) DateTime.UtcNow), this.UserSession.DataManager.Parameter("rType", (object) this.RelationType), this.UserSession.DataManager.Parameter("id", (object) this.PartID), this.UserSession.DataManager.Parameter("prjID", (object) this.RelationID));
          if (obj == null || obj == DBNull.Value)
            throw new KernelExceptionID(sc_13537.ssp_appserver_13546(608137098), (object) partObject.Caption, (object) partObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(partObject.ObjectID));
        }
        else if (applicability1.ApplicabilityMode == ApplicabilityModes.AnyRequired)
        {
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
          {
            StringBuilder stringBuilder = objectPoolScope.Object;
            foreach (DataRow row in (InternalDataCollectionBase) applicabilityCollection.GetApplicabilitiesList(this.RelationType, partObject.ObjectType, -1).Rows)
            {
              string inObjectTypes = (applicabilityCollection.GetApplicability(Convert.ToInt32(row["F_APPLICABILITY_ID"])) as DBRelationsApplicability).GetInObjectTypes();
              if (stringBuilder.Length > 0)
                stringBuilder.Append(",");
              stringBuilder.Append(inObjectTypes);
            }
            DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable($"SELECT R.F_PROJ_ID, O.F_OBJECT_TYPE FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE F_PART_ID = :id AND F_PRJLINK_ID <> :prjID AND F_RELATION_TYPE = :rType AND (R.F_CREATE_DATE <= :actDate) AND O.F_OBJECT_ID = R.F_PROJ_ID AND O.F_OBJECT_TYPE IN ({stringBuilder.ToString()})", this.UserSession.DataManager.Parameter("actDate", (object) DateTime.UtcNow), this.UserSession.DataManager.Parameter("rType", (object) this.RelationType), this.UserSession.DataManager.Parameter("id", (object) this.PartID), this.UserSession.DataManager.Parameter("prjID", (object) this.RelationID));
            bool flag2 = true;
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              IDBRelationsApplicability applicability2 = applicabilityCollection.GetApplicability(this.RelationType, partObject.ObjectType, Convert.ToInt32(dataTable.Rows[index][1]));
              if (applicability2 != null && applicability2.ApplicabilityMode == ApplicabilityModes.AnyRequired)
              {
                flag2 = false;
                break;
              }
            }
            if (flag2)
              throw new KernelExceptionID(sc_13537.ssp_appserver_13547(596204823), (object) partObject.NameInMessages, (object) partObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(partObject.ObjectID));
          }
        }
      }
    }
    return flag1;
  }

  internal int DeleteWithoutCheck(long DeleteMode)
  {
    int num = this.DoDelete(DeleteMode);
    if (this._DeleteEventID != 0L)
      return num;
    this.AddEvent(this.ProjID, this.RelationID, ActionType.DeleteLink, EventlogRecordType.Information);
    return num;
  }

  public int Delete(long DeleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, string.Format(LocalizationHolder.rm.GetString("Kernel_494"), (object) this.ObjectName));
    if (!this.UserSession.CanChangeObject(5, (object) this.GUID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_938"), (object) this.ObjectName));
    DBObject projObject = this.ProjObject as DBObject;
    this._DeleteEventID = (DeleteMode & (long) Consts.CheckInMode) != 0L ? 0L : this.AddEvent(this.ProjID, this.RelationID, ActionType.DeleteLink, EventlogRecordType.AccessDenied);
    if ((DeleteMode & (long) Consts.PurgeMode) == 0L)
      this.RTSecurity.CheckAccess(ActionType.DeleteLink);
    bool flag1 = false;
    if (this.RelationType == MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545"))
      flag1 = true;
    this.UserSession.StartTransaction();
    try
    {
      projObject.BeforeDeleteRelation((IDBRelation) this, DeleteMode);
      bool flag2 = false;
      if ((DeleteMode & (long) Consts.PurgeMode) == 0L)
      {
        IDBObject objectByVersionsRule = this.UserSession.GetObjectByVersionsRule(this.PartID, this.FiltrationOwnerID, true);
        flag2 = this.CheckDeleteApplicabilities((IDBObject) projObject, objectByVersionsRule, this.ProjID > 0L, (DeleteMode & (long) Consts.DontCheckApplicabilityModes) == 0L);
        string partCaption = objectByVersionsRule != null ? objectByVersionsRule.Caption : "Объект номер " + this.PartID.ToString();
        this.UserSession.AddDelayedNotification((DelayedNotification) new RelationDelayedNotification(this.UserSession.RealUserID, ActionType.DeleteLink, this.GetAttributes4Notification(), (AttributeValues[]) null, this.RelationID, this.RelationType, this.ProjID, this.PartID, this.PartObjectID, partCaption));
      }
      if (flag1 && (DeleteMode & (long) Consts.CheckInMode) == 0L && (DeleteMode & 2048L /*0x0800*/) == 0L && this.UserSession.GetObject(this.ProjID, false) is IDBEditingContextsObject editingContextsObject)
      {
        bool clearModifiationID = true;
        if (this.PartObjectID != 0L)
          clearModifiationID = !this.UserSession.RemovableObjectsList.Exists(this.PartObjectID);
        editingContextsObject.DeleteObjectFromContext(this.PartID, true, clearModifiationID);
      }
      (this.EventHelper as EventLogHelper).OnBeforeDeleteRelation((IDBRelation) this, DeleteMode, (IUserSession) this.UserSession);
      if (flag2 && this.IsCheckParentReadOnly)
      {
        projObject.CheckRelationsEdit();
        projObject.SetModifyContentDate();
      }
      int num = this.DoDelete(DeleteMode);
      this.UserSession.AddToModificationsHistory((CategoryValue) new RelationModificationEvent(5, this.RelationID, ActionType.Purge, this.RelationType, this.GUID, this.ProjID));
      (this.EventHelper as EventLogHelper).OnAfterDeleteRelation((IDBRelation) this, DeleteMode, (IUserSession) this.UserSession);
      if (this._DeleteEventID > 0L)
        this.CloseEvent(this._DeleteEventID, EventlogRecordType.AccessGranted);
      this.UserSession.Commit();
      return num;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      if (this._DeleteEventID > 0L)
        this.CloseEvent(this._DeleteEventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
  }

  public IDBAttribute GetAttributeByID(int attributeID)
  {
    if (this._Attributes == null)
      return (ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService).GetRelationAttribute((IUserSession) this.UserSession, this.RelationID, attributeID, (IDBAttributable) this);
    return (this._Attributes as DBAttributeCollection).IsAttrListLoaded ? this._Attributes.FindByID(attributeID) : (ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService).GetRelationAttribute((IUserSession) this.UserSession, this.RelationID, attributeID, (IDBAttributable) this);
  }

  public IDBAttribute GetAttributeByGuid(Guid attributeGuid)
  {
    if (this._Attributes != null)
      return this._Attributes.FindByGUID(attributeGuid);
    int attributeID = MetaDataHelper.GetAttributeTypeID(attributeGuid);
    if (attributeID == -10000)
    {
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Select("F_GUID = " + SqlHelper.QString(attributeGuid.ToString()));
      if (dataRowArray.Length == 0)
        return (IDBAttribute) null;
      attributeID = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
    }
    return this.GetAttributeByID(attributeID);
  }

  public IDBAttribute GetAttributeByName(string attributeName)
  {
    if (this._Attributes != null)
      return this._Attributes.FindByName(attributeName);
    int attributeID = MetaDataHelper.GetAttributeByTypeNameID(attributeName);
    if (attributeID == -10000)
    {
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Select("F_NAME = " + SqlHelper.QString(attributeName));
      attributeID = dataRowArray.Length != 0 ? Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]) : throw new AttributeNotFoundException(attributeName, "", this.ObjectID);
    }
    return this.GetAttributeByID(attributeID);
  }

  public IDBAttribute GetAttributeByGuid(Guid attributeGuid, bool throwNotFoundException)
  {
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(attributeGuid);
    return !throwNotFoundException || attributeByGuid != null ? attributeByGuid : throw new AttributeNotFoundException("", attributeGuid.ToString(), this.RelationID);
  }

  public IDBAttribute GetAttributeByName(string attributeName, bool throwNotFoundException)
  {
    IDBAttribute attributeByName = this.GetAttributeByName(attributeName);
    return !throwNotFoundException || attributeByName != null ? attributeByName : throw new AttributeNotFoundException(attributeName, "", this.RelationID);
  }

  public virtual void DoAfterCreate(int assignMode)
  {
    (this.EventHelper as EventLogHelper).OnAfterCreateRelation((IDBRelation) this, (IUserSession) this.UserSession);
    (this.EventHelper as EventLogHelper).OnAfterCreateRelationEx((IDBRelation) this, (IUserSession) this.UserSession, assignMode);
  }

  private void AddObligatoryAttribute(
    List<AttributeValues> attrList,
    ObligatoryObjectAttributes o_attribute,
    object val,
    bool readOnly,
    object description,
    GetAttributeValuesModes modes,
    string guid)
  {
    AttributeValues attributeValues = new AttributeValues(Convert.ToInt32((object) o_attribute), FieldTypes.ftSystem, MultiValueModes.SingleValue, ComputeValueModes.NotComputableValue);
    if ((modes & GetAttributeValuesModes.IncludeName) == GetAttributeValuesModes.IncludeName)
      attributeValues.AttributeName = ObligatoryObjectAttributesHelper.GetCaption(o_attribute);
    attributeValues.ReadOnly = readOnly;
    attributeValues.Values = new object[1]{ val };
    if (val != null && description != null)
      attributeValues.Descriptions = new object[1]
      {
        description
      };
    attributeValues.GroupName = Consts.SystemAttributesGroupName;
    attrList.Add(attributeValues);
  }

  public virtual AttributeValues[] GetInitAttributesValues(int[] attributeIDs)
  {
    AttributeValues[] attributesValues = new AttributeValues[attributeIDs.Length];
    for (int index = 0; index < attributesValues.Length; ++index)
    {
      AttributeValues attributeValues = new AttributeValues(attributeIDs[index]);
      if (this.RelationTypeObject.AnyAttributes || this.RelationTypeObject.GetAttributeType(attributeIDs[index]) != null)
      {
        IDBAttribute dbAttribute = this.Attributes.AddTemporaryAttribute(attributeIDs[index], true);
        attributeValues.ReadOnly = dbAttribute.ReadOnly;
        attributeValues.Values = dbAttribute.Values;
        attributeValues.Descriptions = (object[]) dbAttribute.Descriptions;
        attributeValues.AttributeName = dbAttribute.Name;
        attributeValues.AttributeGuid = (dbAttribute as IDBGuid).GUID;
        attributeValues.AttributeAlias = dbAttribute.AttributeType.Alias;
        attributeValues.AttributeType = dbAttribute.DataType;
        attributeValues.MultipleValued = dbAttribute.AttributeType.MultipleValued;
      }
      else
        attributeValues.ReadOnly = true;
      attributesValues[index] = attributeValues;
    }
    return attributesValues;
  }

  public virtual AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes)
  {
    List<AttributeValues> attrList = new List<AttributeValues>();
    bool readOnly = !this.ValidateEditRelation(false);
    if ((modes & GetAttributeValuesModes.IncludeObligatoryAttributes) == GetAttributeValuesModes.IncludeObligatoryAttributes)
    {
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_PRJLINK_ID, (object) this.RelationID, true, (object) null, modes, "cad00033-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_PROJ_ID, (object) this.ProjID, true, (object) null, modes, "cad00034-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_PART_ID, (object) this.PartID, true, (object) null, modes, "cad00035-306c-11d8-b4e9-00304f19f545");
      object description1 = (object) null;
      object description2 = (object) null;
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
      {
        description1 = (object) MetaDataHelper.GetRelationTypeName(this.RelationType);
        if (this.CreatorID != 0L)
        {
          QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(this.CreatorID);
          if (!objectInfo.Empty)
            description2 = (object) objectInfo.Caption;
        }
      }
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_RELATION_TYPE, (object) this.RelationType, true, description1, modes, "cad00036-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_CREATE_DATE, (object) this.CreateDate, true, (object) null, modes, "cad00037-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_REL_CREATOR, (object) this.CreatorID, true, description2, modes, "cadd96b8-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_PRJ_GUID, (object) this.GUID, readOnly, (object) null, modes, "cad00344-306c-11d8-b4e9-00304f19f545");
    }
    for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
    {
      DBAttribute attribute = this.Attributes[AttrIndex] as DBAttribute;
      if ((modes & GetAttributeValuesModes.CheckVisibility) == GetAttributeValuesModes.CheckVisibility)
      {
        if (!attribute.Visible)
          continue;
      }
      else if ((modes & GetAttributeValuesModes.IncludeOnlyInvisible) == GetAttributeValuesModes.IncludeOnlyInvisible && (attribute.VisibleByFilters || !attribute.CheckAccess(ActionType.List, this.GetDefaultAccess(ActionType.List), false)))
        continue;
      AttributeValues attributeValues = new AttributeValues(attribute.AttributeID, attribute.AttributeType.AttributeType, attribute.AttributeType.MultipleValued, attribute.AttributeType.Computed);
      if ((modes & GetAttributeValuesModes.IncludeAlias) == GetAttributeValuesModes.IncludeAlias)
        attributeValues.AttributeAlias = attribute.AttributeType.Alias;
      if ((modes & GetAttributeValuesModes.IncludeGuid) == GetAttributeValuesModes.IncludeGuid)
        attributeValues.AttributeGuid = attribute.GUID;
      if ((modes & GetAttributeValuesModes.IncludeName) == GetAttributeValuesModes.IncludeName)
        attributeValues.AttributeName = attribute.Name;
      if (attribute is IBlobReader)
      {
        if ((modes & GetAttributeValuesModes.IncludeBlobValues) == GetAttributeValuesModes.None)
          attributeValues.Values = BlobAttributesHelper.GetBlobValues((IDBAttribute) attribute, GetBlobValueModes.BlobValue);
        else if ((modes & GetAttributeValuesModes.IncludeBlobs) != GetAttributeValuesModes.None)
          attributeValues.Values = attribute.Values;
        else
          continue;
        if ((modes & GetAttributeValuesModes.BlobIdentifier) == GetAttributeValuesModes.BlobIdentifier)
          attribute._ValueContentMode = ColumnContents.ID;
      }
      else
        attributeValues.Values = attribute.Values;
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
        attributeValues.Descriptions = (object[]) attribute.Descriptions;
      if ((modes & GetAttributeValuesModes.CheckWriteAccess) == GetAttributeValuesModes.CheckWriteAccess)
        attributeValues.ReadOnly = attribute.ReadOnly;
      if (attribute.AttributeType.AttributeType == FieldTypes.ftGuid)
        attributeValues.ReadOnly = attributeValues.ReadOnly && this.UserSession.DeveloperMode;
      if ((modes & GetAttributeValuesModes.IncludeGroupName) == GetAttributeValuesModes.IncludeGroupName)
        attributeValues.GroupName = this.UserSession.DBCache.GetAttributeGroupName(attribute.AttributeID);
      attrList.Add(attributeValues);
    }
    return attrList.ToArray();
  }

  public Dictionary<string, Exception> SetAttributesValuesEx(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes)
  {
    Dictionary<string, Exception> exceptionsList = new Dictionary<string, Exception>();
    this.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
    return exceptionsList;
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes)
  {
    return this.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, (Dictionary<string, Exception>) null);
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    this.SetAttributesState(Consts.AssignValuesMode, valuesList);
    try
    {
      AttributesValuesEventArgs args = new AttributesValuesEventArgs(valuesList, modes, (IUserSession) this.UserSession);
      (this.EventHelper as EventLogHelper).OnBeforeSetRelationAttributesValues((IDBAttributable) this, args);
      this.UpdateValuesListByArgs(ref valuesList, args);
      if (valuesList.Length > 1)
        this.ViewsUpdaterPrepare();
      this.UserSession.StartTransaction();
      try
      {
        List<int> intList = (List<int>) null;
        if (deleteNotExistingAttributes)
          intList = new List<int>();
        foreach (AttributeValues values in valuesList)
        {
          bool rollbackOff = this.UserSession.RollbackOff;
          if (!values.ThrowSetException)
            this.UserSession.RollbackOff = true;
          try
          {
            IDBAttribute blobAttribute = (IDBAttribute) null;
            int attributeId = values.AttributeID;
            if (attributeId < 0)
            {
              if (attributeId == 0 || attributeId == -10000)
                throw new KernelException($"Атрибут номер {attributeId} не найден.");
              if (values.Values != null)
              {
                if (values.Values.Length != 0)
                {
                  if (attributeId == -26)
                    this.GUID = !(values.Values[0] is Guid) ? new Guid(values.Values[0].ToString()) : (Guid) values.Values[0];
                }
              }
            }
            else
            {
              if (attributeId != 0)
                blobAttribute = this.Attributes.FindByID(values.AttributeID);
              else if (values.AttributeName != null && values.AttributeName != string.Empty)
                blobAttribute = this.Attributes.FindByName(values.AttributeName);
              else if (values.AttributeGuid != Guid.Empty)
                blobAttribute = this.Attributes.FindByGUID(values.AttributeGuid);
              else if (values.AttributeAlias != null && values.AttributeAlias != string.Empty)
                blobAttribute = this.Attributes.FindByAlias(values.AttributeAlias);
              bool flag = values.Values != null && values.Values[0] is DeleteModesEnum;
              if (blobAttribute == null && !flag)
              {
                if (attributeId == 0)
                {
                  object attributeID;
                  if (values.AttributeName != null && values.AttributeName != string.Empty)
                    attributeID = (object) values.AttributeName;
                  else if (values.AttributeGuid != Guid.Empty)
                  {
                    attributeID = (object) values.AttributeGuid;
                  }
                  else
                  {
                    if (values.AttributeAlias == null || !(values.AttributeAlias != string.Empty))
                      throw new KernelExceptionID(sc_13537.ssp_appserver_13548(236208332));
                    attributeID = (object) new AttributeAlias(values.AttributeAlias);
                  }
                  attributeId = (this.EventHelper as EventLogHelper).GetAttributeID(attributeID, true);
                }
                try
                {
                  if (values.Values == null)
                    this.Attributes.AddAttribute(attributeId, false, new object[1]
                    {
                      (object) DBNull.Value
                    });
                  else
                    this.Attributes.AddAttribute(attributeId, false, values.Values);
                }
                catch (Exception ex)
                {
                  if (values.ThrowSetException)
                  {
                    string name = this.UserSession.GetAttributeType(values.AttributeID).Name;
                    throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_495"), (object) name, (object) ex.Message), ex);
                  }
                  if (exceptionsList != null)
                  {
                    if (values.AttributeName != null && values.AttributeName != string.Empty)
                      exceptionsList.Add(values.AttributeName, ex);
                    else
                      exceptionsList.Add(this.UserSession.GetAttributeType(attributeId).Name, ex);
                  }
                }
              }
              if (deleteNotExistingAttributes)
              {
                if (flag)
                  throw new KernelExceptionID(sc_13537.ssp_appserver_13549(438095816));
                intList.Add(attributeId);
              }
              if (blobAttribute != null)
              {
                if (flag)
                {
                  try
                  {
                    blobAttribute.Delete(0L);
                  }
                  catch (Exception ex)
                  {
                    if (values.ThrowSetException)
                      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_496"), (object) blobAttribute.Name, (object) ex.Message), ex);
                    exceptionsList?.Add(blobAttribute.Name, ex);
                  }
                }
                else if (blobAttribute is IBlobWriter)
                {
                  if (values.Values != null)
                    BlobAttributesHelper.SetBlobValues(blobAttribute, values.Values, this.UserSession);
                }
                else if (!values.ReadOnly)
                {
                  try
                  {
                    if (values.Values == null)
                      blobAttribute.Values = new object[1]
                      {
                        (object) DBNull.Value
                      };
                    else
                      blobAttribute.Values = values.Values;
                  }
                  catch (Exception ex)
                  {
                    if (values.ThrowSetException)
                      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_497"), (object) blobAttribute.Name, (object) ex.Message), ex);
                    exceptionsList?.Add(blobAttribute.Name, ex);
                  }
                }
              }
            }
          }
          finally
          {
            this.UserSession.RollbackOff = rollbackOff;
          }
        }
        if (deleteNotExistingAttributes)
        {
          for (int AttrIndex = this.Attributes.Count - 1; AttrIndex > -1; --AttrIndex)
          {
            if ((!dontDeleteBlobs || !(this.Attributes[AttrIndex] is IBlobReader)) && intList.BinarySearch(this.Attributes[AttrIndex].AttributeID) < 0)
              this.Attributes[AttrIndex].Delete(0L);
          }
        }
        this.CommitComputedValues();
        this.ViewsUpdaterCommit();
        this.UserSession.Commit();
      }
      catch
      {
        this.ViewsUpdaterRollback();
        this.UserSession.Rollback();
        throw;
      }
      return returnDelta && this._Attributes != null ? (this.Attributes as DBAttributeCollection).GetDeltaValues(modes, this.GetDefaultAccess(ActionType.Edit)) : (AttributeValues[]) null;
    }
    finally
    {
      this.ClearAttributesState(Consts.AssignValuesMode);
    }
  }

  public AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs)
  {
    return this.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, false, GetAttributeValuesModes.None);
  }

  public AttributeValues[] SetAttributesValues(AttributeValues[] valuesList)
  {
    return this.SetAttributesValues(valuesList, false, true, false, GetAttributeValuesModes.None);
  }

  public bool ReadOnly => !this.ValidateEditRelation(false);

  internal int InsertIntoView()
  {
    string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, this.RelationType);
    if (updateTables == null)
      return 0;
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("relID", (object) this.RelationID);
    foreach (string str in updateTables)
      dataManager.ExecuteNonQuery($"INSERT INTO {str} (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR) SELECT F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :relID", dbDataParameter);
    return updateTables.Length;
  }

  private void DeleteFromView()
  {
    string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, this.RelationType);
    if (updateTables == null)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("relID", (object) this.RelationID);
    foreach (string str in updateTables)
      dataManager.ExecuteNonQuery($"DELETE FROM {str} WHERE F_PRJLINK_ID = :relID", dbDataParameter);
  }

  private void UpdateViewValue(string fldName, object newValue)
  {
    string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, this.RelationType);
    if (updateTables == null)
      return;
    if (this.ViewsUpdaterInited)
    {
      foreach (string viewName in updateTables)
        this.ViewsUpdaterAddValue(viewName, this.RelationID, "F_PRJLINK_ID", newValue, fldName);
    }
    else
    {
      IDbManager dataManager = this.UserSession.DataManager;
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("relID", (object) this.RelationID);
      IDbDataParameter dbDataParameter2 = dataManager.Parameter("newVal", newValue);
      string format = $"UPDATE {{0}} SET {fldName} = :newVal WHERE F_PRJLINK_ID = :relID";
      foreach (string str in updateTables)
        dataManager.ExecuteNonQuery(string.Format(format, (object) str), dbDataParameter2, dbDataParameter1);
    }
  }

  internal void RepairViews()
  {
    if (this.UserSession.DBCache.GetUpdateTables(-1, -1, this.RelationType) == null)
      return;
    this.UserSession.StartTransaction();
    try
    {
      this.DeleteFromView();
      this.InsertIntoView();
      for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
        (this.Attributes[AttrIndex] as DBAttribute).InsertIntoView(1);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public override string[] GetDescriptionsByID(int attributeID, bool throwNotFoundException)
  {
    return this.GetDescriptionsByGuid(new Guid((this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID) ?? throw new KernelExceptionID(sc_13537.ssp_appserver_13550(1639813796), (object) attributeID))["F_GUID"].ToString()), throwNotFoundException);
  }

  public override string[] GetDescriptionsByGuid(Guid guid, bool throwNotFoundException)
  {
    string[] strArray = (string[]) null;
    if (SystemGUIDs.IsSystemGUID(guid))
    {
      switch (guid.ToString())
      {
        case "cad00033-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.RelationID.ToString()
          };
          break;
        case "cad00034-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]{ this.ProjID.ToString() };
          break;
        case "cad00035-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]{ this.PartID.ToString() };
          break;
        case "cad00036-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.RelationTypeObject.Description
          };
          break;
        case "cad00037-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            Convert.ToString(this.CreateDate)
          };
          break;
        case "cad00038-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            Convert.ToString(this.DeleteDate)
          };
          break;
      }
    }
    if (strArray == null)
    {
      IDBAttribute byGuid = this.Attributes.FindByGUID(guid);
      if (byGuid != null)
        strArray = byGuid.Descriptions;
    }
    return !throwNotFoundException || strArray != null ? strArray : throw new AttributeNotFoundException(string.Empty, guid.ToString(), this.RelationID);
  }

  public override object[] GetValuesByName(string attributeName, bool throwNotFoundException)
  {
    int attributeId = this.UserSession.EventLogHelper.GetAttributeID((object) attributeName, throwNotFoundException);
    if (attributeId != -1)
      return this.GetValuesByID(attributeId, throwNotFoundException);
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_13537.ssp_appserver_13551(1742196835), (object) attributeName);
    return (object[]) null;
  }

  public override object[] GetValuesByID(int attributeID, bool throwNotFoundException)
  {
    DataRow dataRow = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID);
    if (dataRow != null)
      return this.GetValuesByGuid(new Guid(dataRow["F_GUID"].ToString()), throwNotFoundException);
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_13537.ssp_appserver_13552(1628261963), (object) attributeID);
    return (object[]) null;
  }

  public override object[] GetValuesByGuid(Guid guid, bool throwNotFoundException)
  {
    object[] objArray = (object[]) null;
    if (SystemGUIDs.IsSystemGUID(guid))
    {
      switch (guid.ToString())
      {
        case "cad00033-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.RelationID
          };
          break;
        case "cad00034-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.ProjID };
          break;
        case "cad00035-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.PartID };
          break;
        case "cad00036-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.RelationType
          };
          break;
        case "cad00037-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.CreateDate
          };
          break;
        case "cad00038-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.DeleteDate
          };
          break;
      }
    }
    if (objArray == null)
    {
      IDBAttribute byGuid = this.Attributes.FindByGUID(guid);
      if (byGuid != null)
        objArray = byGuid.Values;
    }
    return !throwNotFoundException || objArray != null ? objArray : throw new AttributeNotFoundException(string.Empty, guid.ToString(), this.RelationID);
  }

  public int TypeID => this.RelationType;

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public Guid GUID
  {
    get => new Guid(this.paramsTable[33].ToString());
    set
    {
      if (!(this.GUID != value))
        return;
      if (!this.UserSession.CanChangeObject(5, (object) this.GUID))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_937"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        this.ValidateEditRelation(true);
      }
      catch
      {
        this.AddEvent(this.ProjID, this.RelationID, ActionType.EditLink, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_498"), (object) value));
        throw;
      }
      long EventID = this.AddEvent(this.ProjID, this.RelationID, ActionType.EditLink, EventlogRecordType.AccessGranted, LocalizationHolder.rm.GetString("Kernel_499") + value.ToString());
      this.UserSession.StartTransaction();
      try
      {
        IDbManager dataManager = this.UserSession.DataManager;
        IDbDataParameter dbDataParameter = dataManager.Parameter("relID", (object) this.RelationID);
        dataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_PRJ_GUID = :guid WHERE F_PRJLINK_ID = :relID", dataManager.Parameter("guid", (object) value), dbDataParameter);
        this.UpdateViewValue("F_PRJ_GUID", (object) value);
        this.paramsTable[33] = (object) value;
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString("Kernel_500") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public override long HistoryObjectID => this.RelationID;

  public override IDBAttributeType GetAttributeType(int attributeID)
  {
    return (IDBAttributeType) this.RelationTypeObject.Attributes.GetAttributeByID(attributeID, false) ?? this.UserSession.GetAttributeType(attributeID);
  }

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(5, (object) this.GUID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_937"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  public override long ObjectID => this.ProjID;

  internal void ReplacePartObjectInternal(IDBObject part_obj)
  {
    if (this.ProjObject.AccessLevel < part_obj.AccessLevel)
      throw new KernelExceptionID(sc_13537.ssp_appserver_13553(1519447237), (object) part_obj.NameInMessages, (object) this.UserSession.DBCache.GetAccessCaption(part_obj.AccessLevel), (object) this.ProjObject.NameInMessages, (object) this.UserSession.DBCache.GetAccessCaption(this.ProjObject.AccessLevel));
    long partId = this.PartID;
    long id = part_obj.ID;
    this.UserSession.StartTransaction();
    try
    {
      (this.EventHelper as EventLogHelper).OnBeforeReplacePartObject((IDBRelation) this, partId, part_obj, (IUserSession) this.UserSession);
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_PART_ID = :newVal WHERE F_PRJLINK_ID = :relID", this.UserSession.DataManager.Parameter("relID", (object) this.RelationID), this.UserSession.DataManager.Parameter("newVal", (object) id));
      this.UpdateViewValue("F_PART_ID", (object) id);
      this.paramsTable[138] = (object) id;
      if (MetaDataHelper.GetAttribute4RelationType(this.RelationType, this.UserSession.IdentHelper.AttributeVersionInRelation) != null && this.GetAttributeByID(this.UserSession.IdentHelper.AttributeVersionInRelation) is DBAttribute attributeById)
        attributeById.DirectSetValue("F_INTEGER_VALUE", (object) Math.Abs(part_obj.ObjectID));
      (this.EventHelper as EventLogHelper).OnAfterReplacePartObject((IDBRelation) this, partId, part_obj, (IUserSession) this.UserSession);
      this.AddEvent(this.ProjID, this.RelationID, ActionType.EditLink, EventlogRecordType.AccessGranted, string.Format(LocalizationHolder.rm.GetString("ReplacePartObject"), (object) part_obj.NameInMessages));
      this.UserSession.AddToModificationsHistory((CategoryValue) new RelationModificationEvent(5, this.RelationID, ActionType.EditLink, this.RelationType, this.GUID, this.ProjID));
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
    }
  }

  public virtual void ReplacePartObject(long partObjectID)
  {
    IDBObject part_obj = this.UserSession.GetObject(partObjectID);
    IDBObject projObject = this.ProjObject;
    if (part_obj.ID != this.PartID)
    {
      DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :partID AND F_LEVEL_ID <> :delID", this.UserSession.DataManager.Parameter("partID", (object) this.PartID), this.UserSession.DataManager.Parameter("delID", (object) this.UserSession.IdentHelper.DeletedID));
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        int int32 = Convert.ToInt32(dataTable.Rows[index][0]);
        if (!MetaDataHelper.IsObjectTypeChildOf(part_obj.ObjectType, int32))
          throw new KernelExceptionID(sc_13537.ssp_appserver_13554(257662649), (object) this.UserSession.GetObjectType(part_obj.ObjectType).ObjectTypeName, (object) this.UserSession.GetObjectType(int32).ObjectTypeName);
      }
      IMSApplicability applicability = MetaDataHelper.GetApplicability(projObject.ObjectType, part_obj.ObjectType, this.RelationType);
      if ((applicability.Options & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.None && this.UserSession.GetRelation(this.ProjID, part_obj.ID, this.RelationType) != null)
        throw new KernelExceptionID(sc_13537.ssp_appserver_13555(1316924163), (object) part_obj.NameInMessages, (object) projObject.ObjectID, (object) projObject.NameInMessages, (object) projObject.ObjectID, (object) this.UserSession.GetRelationType(this.RelationType).Description).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(projObject.ObjectID), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(projObject.ObjectID));
      try
      {
        this.ValidateEditRelation(true);
      }
      catch
      {
        this.AddEvent(this.ProjID, this.RelationID, ActionType.EditLink, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString(nameof (ReplacePartObject)), (object) part_obj.NameInMessages));
        throw;
      }
      this.ReplacePartObjectInternal(part_obj);
      if (!applicability.IsContent)
        return;
      (projObject as DBObject).SetModifyContentDate();
    }
    else
    {
      if (MetaDataHelper.GetAttribute4RelationType(this.RelationType, this.UserSession.IdentHelper.AttributeVersionInRelation) == null)
        return;
      IDBAttribute attributeById = this.GetAttributeByID(this.UserSession.IdentHelper.AttributeVersionInRelation);
      if (attributeById != null)
        attributeById.AsInteger = Math.Abs(partObjectID);
      else
        this.Attributes.AddAttribute(this.UserSession.IdentHelper.AttributeVersionInRelation, false, new object[1]
        {
          (object) Math.Abs(partObjectID)
        });
    }
  }

  internal AttributeValues[] GetAttributes4Notification()
  {
    return this.UserSession.SendAttrs2DelayedNotificationMode ? this.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes) : (AttributeValues[]) null;
  }
}
