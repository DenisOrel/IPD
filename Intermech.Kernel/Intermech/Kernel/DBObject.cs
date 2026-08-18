// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Projects;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.DelayedNotifications;
using Intermech.Interfaces.Services;
using Intermech.Interfaces.Snapshots;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Helpers;
using Intermech.Kernel.Projects;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace Intermech.Kernel;

public class DBObject : 
  DBAttributable,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBGuid,
  IDeletable,
  IDBLifecycleLevel,
  IDBSecurityCollection,
  IDBSecurity,
  IObjectTemplater
{
  private long _ObjectID;
  protected string _Caption = string.Empty;
  private DataTable _GuidTable;
  private bool _IsCreationMode;
  internal bool _MustCheckValidatingRule = true;
  private IDBLifecycleStep _LCStepObject;
  private ObjectFiltrationState _FiltrationState;
  protected IDBSecurity _ProjectSecurity;
  internal bool ValidationRulesOn = true;
  internal bool CheckAnnulment = true;
  private IDBObjectType _ObjectTypeClass;
  protected internal DBSessionable _NextLCStep;
  private object _ObjectGUID;
  internal bool _DenyChangeLCStep;
  private static IDBEditingContextsServerService _editingContextsServerService;
  private const int columnF_PRJLINK_ID = 0;
  private const int columnF_PROJ_ID = 1;
  private const int columnF_RELATION_TYPE = 3;
  private const int columnF_OBJECT_TYPE = 6;
  protected const int columnDownLinksF_PRJLINK_ID = 0;
  protected const int columnDownLinksF_RELATION_TYPE = 3;
  protected const int columnDownLinksF_OBJECT_ID = 6;
  protected const int columnDownLinksF_OBJECT_TYPE = 7;
  private bool relationsDeleted;
  private bool _isCheckOutMode;
  private long _accessConditionID = -1;
  internal long _ParentVersionID;
  private int _NewAccessLevel = -1;

  public static IDBEditingContextsServerService EditingContextsServerService
  {
    get
    {
      if (DBObject._editingContextsServerService == null)
        DBObject._editingContextsServerService = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
      return DBObject._editingContextsServerService;
    }
  }

  public DBObject(UserSession uSession, DataTable objectsTable)
    : base(uSession)
  {
    if (objectsTable.Rows.Count == 0)
      throw new KernelException(sc_13302.ssp_appserver_13303());
    this.paramsTable.Create(objectsTable.Rows[0]);
    this._ObjectID = Convert.ToInt64(objectsTable.Rows[0]["F_OBJECT_ID"]);
    this.InitSecurityOptions(1, this._ObjectID);
  }

  public DBObject(UserSession uSession)
    : base(uSession)
  {
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.Edit, this.GetDefaultAccess(ActionType.Edit));
    this.AccessActions.Add(ActionType.View, this.GetDefaultAccess(ActionType.View));
    this.AccessActions.Add(ActionType.Delete, this.GetDefaultAccess(ActionType.Delete));
    this.AccessActions.Add(ActionType.Purge, this.GetDefaultAccess(ActionType.Purge));
    this.AccessActions.Add(ActionType.NextLCStep, this.GetDefaultAccess(ActionType.NextLCStep));
    this.AccessActions.Add(ActionType.TakeOwnership, this.GetDefaultAccess(ActionType.TakeOwnership));
    this.AccessActions.Add(ActionType.ChangeBaseVersion, this.GetDefaultAccess(ActionType.ChangeBaseVersion));
    this.AccessActions.Add(ActionType.ChangeAccessLevel, this.GetDefaultAccess(ActionType.ChangeAccessLevel));
  }

  internal DataTable guidTable
  {
    get
    {
      if (this._GuidTable == null)
      {
        long num = !this.IsCreationMode ? Math.Abs(this._ObjectID) : this._ObjectID;
        this._GuidTable = this.UserSession.DataManager.ExecuteDataTable("SELECT * FROM IMS_GUID WHERE F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("objID", (object) num));
        if (this._GuidTable.Rows.Count == 0)
          this._GuidTable = this.UserSession.DataManager.ExecuteDataTable("SELECT * FROM IMS_GUID WHERE F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("objID", (object) -num));
      }
      return this._GuidTable;
    }
  }

  public IDBObjectType ObjectTypeClass
  {
    get
    {
      if (this._ObjectTypeClass == null)
        this._ObjectTypeClass = this.UserSession.GetObjectType(this.ObjectType);
      return this._ObjectTypeClass;
    }
  }

  public override long AddEvent(
    long objectID,
    long relationID,
    ActionType eventType,
    EventlogRecordType auditType,
    string note)
  {
    return this.ObjectType == this.UserSession.IdentHelper.ConfigDataTypeID || (this.EventHelper as EventLogHelper).NotLoggedTypes.ContainsKey(this.ObjectType) || (this.EventHelper as EventLogHelper).NotLoggedObjects.ContainsKey(this.ObjectID) ? 0L : base.AddEvent(objectID, relationID, eventType, auditType, note);
  }

  public virtual string Caption
  {
    get
    {
      if (this._Caption == string.Empty)
      {
        try
        {
          this._Caption = this.ObjectID >= 0L ? this.guidTable.Rows[0]["CAPTION"].ToString() : this.guidTable.Rows[0]["F_WORK_CAPTION"].ToString();
        }
        catch
        {
          this._Caption = $"{this.ObjectTypeClass.ObjectInstanceName} N{this.ObjectID.ToString()}";
        }
      }
      return this._Caption;
    }
    set
    {
      if (!(value != this.Caption))
        return;
      try
      {
        this.CheckEditMode(true, true, false);
      }
      catch
      {
        this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_397") + value);
        throw;
      }
      this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessGranted, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_398") + value : LocalizationHolder.rm.GetString("Kernel_399"));
      try
      {
        if (this.ObjectTypeClass.CaptionAttribute > 0)
        {
          IDBAttributeType attributeType = this.UserSession.GetAttributeType(this.ObjectTypeClass.CaptionAttribute);
          if (attributeType.AttributeType != FieldTypes.ftString && attributeType.AttributeType != FieldTypes.ftMemo)
            throw new KernelExceptionID(sc_13302.ssp_appserver_13304(478253294), (object) this.NameInMessages, (object) EnumDescConverter.GetEnumDescription((Enum) attributeType.AttributeType));
          IDBAttribute dbAttribute = this.GetAttributeByID(this.ObjectTypeClass.CaptionAttribute);
          if (dbAttribute == null)
          {
            dbAttribute = this.Attributes.AddAttribute(this.ObjectTypeClass.CaptionAttribute, false, new object[1]
            {
              (object) value
            });
          }
          else
          {
            dbAttribute.Index = 0;
            dbAttribute.Value = (object) value;
          }
          (this.Attributes as DBAttributeCollection).AddDeltaValue(dbAttribute.AttributeID);
        }
        else
        {
          SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_400"));
          this.SetCaption(value);
        }
      }
      catch (Exception ex)
      {
        this.CloseEvent(this._LastEventID, EventlogRecordType.Error, ex.Message);
        throw;
      }
    }
  }

  protected virtual void AfterSetCaption()
  {
  }

  internal void SetCaption(string newCaption)
  {
    if (!(this.Caption != newCaption))
      return;
    this.UserSession.StartTransaction();
    try
    {
      IDbManager dataManager = this.UserSession.DataManager;
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("captPar", (object) newCaption);
      string columnName;
      IDbDataParameter dbDataParameter2;
      if (this.ObjectID > 0L)
      {
        columnName = "CAPTION";
        dbDataParameter2 = dataManager.Parameter("objID", (object) this.ObjectID);
      }
      else
      {
        columnName = "F_WORK_CAPTION";
        dbDataParameter2 = !this.IsCreationMode ? dataManager.Parameter("objID", (object) -this.ObjectID) : dataManager.Parameter("objID", (object) this.ObjectID);
      }
      dataManager.ExecuteNonQuery($"UPDATE IMS_GUID SET {columnName} = :captPar WHERE F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter2);
      if (this._GuidTable != null && this._GuidTable.Rows.Count != 0)
      {
        this._GuidTable.Rows[0][columnName] = (object) newCaption;
        this._GuidTable.AcceptChanges();
      }
      if (!this.IsCreationMode)
        this.UpdateViewValue("CAPTION", (object) newCaption);
      if (this.CanUpdateCaptionDate && this.ObjectTypeClass.CaptionAttribute == 0)
        this.SetModifyContentDate();
      if (this.ObjectID > 0L && !this.IsCreationMode)
      {
        dbDataParameter2.Value = (object) this.ObjectID;
        int num1 = -1;
        int num2 = -1;
        bool flag = false;
        IMSAttributeType imsAttributeType = (IMSAttributeType) null;
        IMSAttribute4ObjectType attribute4ObjectType = (IMSAttribute4ObjectType) null;
        string[] strArray = (string[]) null;
        IDbDataParameter dbDataParameter3 = dataManager.Parameter("attrID", (object) 0);
        IDbDataParameter dbDataParameter4 = dataManager.Parameter("lstID", (object) 0);
        string str1 = !this.IsBaseVersion ? string.Empty : " UNION ALL SELECT A1.F_OBJECT_ID, A1.F_ATTRIBUTE_ID, A1.F_INLIST_ID, O1.F_OBJECT_TYPE, O1.F_ID FROM IMS_ID_LINKS A1, IMS_OBJECTS O1 WHERE (A1.F_TO_ID = :ID11) AND (O1.F_OBJECT_ID = A1.F_OBJECT_ID)";
        foreach (DataRowView dataRowView in new DataView(dataManager.ExecuteDataTable("SELECT A.F_OBJECT_ID, A.F_ATTRIBUTE_ID, A.F_INLIST_ID, O.F_OBJECT_TYPE, O.F_ID FROM IMS_OBJECT_LINKS A, IMS_OBJECTS O WHERE (A.F_TOOBJECT_ID = :objID) AND (O.F_OBJECT_ID = A.F_OBJECT_ID)" + str1, dbDataParameter2, dataManager.Parameter("ID11", (object) this.ID)))
        {
          Sort = "F_OBJECT_TYPE  ASC, F_ATTRIBUTE_ID ASC"
        })
        {
          dbDataParameter2.Value = dataRowView[0];
          dbDataParameter3.Value = dataRowView[1];
          dbDataParameter4.Value = dataRowView[2];
          dataManager.ExecuteNonQuery($"UPDATE {this.UserSession.DBCache.GetAttributesTableName(Convert.ToInt32(dataRowView[3]))} SET F_STRING_VALUE = :captPar WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :lstID", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4);
          if (Convert.ToInt32(dataRowView[3]) != num1 || Convert.ToInt32(dataRowView[1]) != num2)
          {
            num1 = Convert.ToInt32(dataRowView[3]);
            num2 = Convert.ToInt32(dataRowView[1]);
            attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(num1, num2);
            imsAttributeType = attribute4ObjectType == null ? MetaDataHelper.GetAttributeType(num2) : (IMSAttributeType) null;
            strArray = this.UserSession.DBCache.GetUpdateTables(num2, num1, -1);
            flag = MetaDataHelper.GetObjectType(num1).CaptionAttribute == num2;
          }
          if (attribute4ObjectType != null)
          {
            if ((attribute4ObjectType.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
              this.UserSession.AddAttrToIndexQueue(Convert.ToInt64(dataRowView[0]), Convert.ToInt32(dataRowView[1]), Convert.ToInt32(dataRowView[2]), Convert.ToInt64(dataRowView[4]), newCaption, attribute4ObjectType.Options, attribute4ObjectType.FieldType);
          }
          else if ((imsAttributeType.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
            this.UserSession.AddAttrToIndexQueue(Convert.ToInt64(dataRowView[0]), Convert.ToInt32(dataRowView[1]), Convert.ToInt32(dataRowView[2]), Convert.ToInt64(dataRowView[4]), newCaption, imsAttributeType.Options, imsAttributeType.FieldType);
          if (flag)
          {
            string str2;
            IDbDataParameter dbDataParameter5;
            if (Convert.ToInt64(dataRowView[0]) > 0L)
            {
              str2 = "CAPTION";
              dbDataParameter5 = dbDataParameter2;
            }
            else
            {
              str2 = "F_WORK_CAPTION";
              dbDataParameter5 = dataManager.Parameter("objID", (object) -Convert.ToInt64(dataRowView[0]));
            }
            dataManager.ExecuteNonQuery($"UPDATE IMS_GUID SET {str2} = :captPar WHERE F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter5);
          }
          if (strArray != null)
          {
            foreach (string str3 in strArray)
            {
              string str4 = !flag ? string.Empty : ", CAPTION = :captPar";
              dataManager.ExecuteNonQuery($"UPDATE {str3} SET F{dataRowView[1].ToString()} = :captPar{str4} WHERE F_OBJECT_ID = :objID", dbDataParameter1, dbDataParameter2);
            }
          }
        }
        dbDataParameter2.Value = (object) this.ObjectID;
        int relationTypeID = -1;
        foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable($"SELECT A.F_PRJLINK_ID, A.F_ATTRIBUTE_ID, A.F_INLIST_ID, R.F_RELATION_TYPE FROM IMS_RELATION_ATTRS A, IMS_RELATIONS R WHERE (A.F_INTEGER_VALUE = :objID) AND (A.F_ATTRIBUTE_ID IN (SELECT ATRS.F_ATTRIBUTE_ID FROM IMS_ATTRIBUTES ATRS WHERE ATRS.F_ATTRIBUTE_TYPE = {8})) AND (R.F_PRJLINK_ID = A.F_PRJLINK_ID)", dbDataParameter2).Rows)
        {
          dbDataParameter2.Value = row[0];
          dbDataParameter3.Value = row[1];
          dbDataParameter4.Value = row[2];
          dataManager.ExecuteNonQuery("UPDATE IMS_RELATION_ATTRS SET F_STRING_VALUE = :captPar WHERE F_PRJLINK_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :lstID", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4);
          if (Convert.ToInt32(row[3]) != relationTypeID)
          {
            relationTypeID = Convert.ToInt32(row[3]);
            strArray = this.UserSession.DBCache.GetUpdateTables(Convert.ToInt32(row[1]), -1, relationTypeID);
          }
          if (strArray != null)
          {
            foreach (string str5 in strArray)
              dataManager.ExecuteNonQuery($"UPDATE {str5} SET F{row[1].ToString()} = :captPar WHERE F_PRJLINK_ID = :objID", dbDataParameter1, dbDataParameter2);
          }
        }
      }
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
    this._Caption = newCaption;
    this.UserSession.DBCache.UpdateObjectInfo(new QuickObjectInfo(this.ObjectID, newCaption, this.ObjectType, this.ObjectGUID, this.ID));
    if (this._Attributes != null)
      (this.Attributes as DBAttributeCollection).AddDeltaValue(-50);
    this.AfterSetCaption();
  }

  protected virtual bool CanUpdateCaptionDate => false;

  public override string ObjectName => $"{this.ObjectTypeName} '{this.Caption}'";

  public override string ObjectNameEx
  {
    get
    {
      return $"{this.ObjectTypeName} '{this.Caption}' (ид. версии {this.ObjectID}, владелец {this.UserSession.GetObjectInfo(this.OwnerID).Caption})";
    }
  }

  public virtual string NameInMessages
  {
    get
    {
      string nameInMessages = this.Caption;
      if (nameInMessages == string.Empty)
        nameInMessages = string.Format(LocalizationHolder.rm.GetString("Kernel_401"), (object) this.ObjectTypeName, (object) this.ObjectID);
      else if (this.ObjectTypeName != nameInMessages)
        nameInMessages = $"{this.ObjectTypeName} '{nameInMessages}'";
      return nameInMessages;
    }
  }

  public string ObjectTypeName
  {
    get
    {
      DataRow dataRow = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) this.ObjectType);
      return dataRow != null ? dataRow["F_OBJ_NAME"].ToString() : $"Object type {this.ObjectType} not found";
    }
  }

  public virtual void DoAfterCreate()
  {
    this._IsCreationMode = true;
    (this.EventHelper as EventLogHelper).OnCreateObject((IDBObject) this, (IUserSession) this.UserSession);
  }

  protected virtual void DoCommitCreation()
  {
  }

  protected virtual EditingContextMode GetEditingContextMode() => this.Session.EditingContextMode;

  protected virtual void DoCheckEditingContext()
  {
    int num = MetaDataHelper.IsObjectTypeEditingContext(this.ObjectType) ? 0 : (MetaDataHelper.MustAppendVersionToEditingContext((IUserSession) this.UserSession, this.ObjectType, new Func<EditingContextMode>(this.GetEditingContextMode)) ? 1 : 0);
    IDBEditingContextsObject editingContextsObject = num != 0 ? this.UserSession.GetObject(this.UserSession.EditingContextID, false) as IDBEditingContextsObject : (IDBEditingContextsObject) null;
    if (num == 0 || editingContextsObject == null || editingContextsObject.ExistsVersionID(this.ObjectID, false))
      return;
    editingContextsObject.AddVersionID(this.ID, this.ObjectID, true);
  }

  protected virtual void DoBeforeCommitCreation()
  {
  }

  private void ExecuteLCSterScriptOnCreate()
  {
    ((LCStepScriptService) ApplicationServices.Container.GetService(typeof (ILCScriptService)))?.ExecuteScript((IDBObject) this, this.LCStepObject, this.Session);
  }

  private void ValidateCheckinRules()
  {
    if (!this.ValidationRulesOn)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    this._Attributes = (IDBAttributeCollection) null;
    for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
    {
      DBAttribute attribute = this.Attributes[AttrIndex] as DBAttribute;
      if ((attribute.AttributeType.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls && !attribute.TemporaryAttribute)
      {
        for (int index = 0; index < attribute.ValuesCount; ++index)
        {
          attribute.Index = index;
          attribute.CheckNotNullValue(attribute.Value);
        }
      }
      if (attribute.AttributeType.UniqueMode != UniqueValueModes.NotUnique && attribute.DataType != FieldTypes.ftMemo)
        attribute.CheckUniqueValue(attribute.Values, true);
      if (attribute.AttributeType.ValidationRule.Length > 0)
        attribute.ValidateRule(attribute.AttributeID, attribute.Value);
    }
    DataTable applicabilitiesList = this.UserSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, this.ObjectType, -1);
    ListDictionary listDictionary = (ListDictionary) null;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("pID", (object) this.ID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("relType", (object) 0);
    foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
    {
      ApplicabilityModes int32_1 = (ApplicabilityModes) Convert.ToInt32(row["F_MIN_LINKS"]);
      if (int32_1 == ApplicabilityModes.Required)
      {
        dbDataParameter2.Value = (object) Convert.ToInt32(row["F_RELATION_TYPE"]);
        DBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(row["F_INOBJECT_TYPE"])) as DBObjectType;
        object obj = dataManager.ExecuteScalar($"SELECT O.F_OBJECT_ID FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE R.F_PART_ID = :pID AND R.F_RELATION_TYPE = :relType AND O.F_OBJECT_ID = R.F_PROJ_ID AND O.F_OBJECT_VER_TYPE <> -1 AND O.F_OBJECT_TYPE IN ({objectType.GetChildrenListSQL()})", dbDataParameter1, dbDataParameter2);
        if (obj == null || obj == DBNull.Value)
        {
          ArrayList objsTreeList = new ArrayList();
          objectType.FillChildrenList(objsTreeList);
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
          {
            StringBuilder stringBuilder = objectPoolScope.Object;
            stringBuilder.Append(this.UserSession.GetObjectType((int) objsTreeList[0]).ObjectTypeName);
            for (int index = 1; index < objsTreeList.Count; ++index)
              stringBuilder.Append(", " + this.UserSession.GetObjectType((int) objsTreeList[index]).ObjectTypeName);
            throw new KernelExceptionID(sc_13302.ssp_appserver_13305(465132665), (object) this.ObjectTypeClass.ObjectInstanceName, (object) stringBuilder.ToString());
          }
        }
      }
      if (int32_1 == ApplicabilityModes.AnyRequired)
      {
        if (listDictionary == null)
          listDictionary = new ListDictionary();
        int int32_2 = Convert.ToInt32(row["F_RELATION_TYPE"]);
        DBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(row["F_INOBJECT_TYPE"])) as DBObjectType;
        if (!(listDictionary[(object) int32_2] is ArrayList objsTreeList1))
        {
          ArrayList objsTreeList = new ArrayList();
          objectType.FillChildrenList(objsTreeList);
          listDictionary[(object) int32_2] = (object) objsTreeList;
        }
        else
          objectType.AddChildrenForType(objectType.ObjectType, objsTreeList1);
      }
    }
    if (listDictionary == null)
      return;
    ICollection keys = listDictionary.Keys;
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    foreach (int num in (IEnumerable) keys)
    {
      ArrayList arrayList = (ArrayList) listDictionary[(object) num];
      for (int index = arrayList.Count - 1; index >= 0; --index)
      {
        IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(num, this.ObjectType, Convert.ToInt32(arrayList[index]));
        if (applicability == null || applicability.ApplicabilityMode != ApplicabilityModes.AnyRequired)
          arrayList.RemoveAt(index);
      }
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append(arrayList[0].ToString());
        for (int index = 1; index < arrayList.Count; ++index)
          stringBuilder.Append("," + arrayList[index].ToString());
        dbDataParameter2.Value = (object) num;
        object obj = dataManager.ExecuteScalar($"SELECT O.F_OBJECT_ID FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE R.F_PART_ID = :pID AND R.F_RELATION_TYPE = :relType AND O.F_OBJECT_ID = R.F_PROJ_ID AND O.F_OBJECT_VER_TYPE <> -1 AND O.F_OBJECT_TYPE IN ({stringBuilder.ToString()})", dbDataParameter1, dbDataParameter2);
        if (obj != null)
        {
          if (obj != DBNull.Value)
            continue;
        }
        stringBuilder.Length = 0;
        stringBuilder.Append(this.UserSession.GetObjectType((int) arrayList[0]).ObjectTypeName);
        for (int index = 1; index < arrayList.Count; ++index)
          stringBuilder.Append(", " + this.UserSession.GetObjectType((int) arrayList[index]).ObjectTypeName);
        throw new KernelExceptionID(sc_13302.ssp_appserver_13306(246947072), (object) this.ObjectTypeClass.ObjectInstanceName, (object) stringBuilder.ToString());
      }
    }
  }

  public virtual void CommitCreation(bool deleteOnException, bool autoCheckout)
  {
    if (!this.IsCreationMode)
      throw new ObjectAlreadyCommited(this.ObjectID);
    ActionType eventType = ActionType.Create;
    this.UserSession.StartTransaction();
    try
    {
      if (this.ProjectID > 0L && Math.Abs(this.ObjectID) != this.ProjectID)
      {
        IDBObject dbObject = this.UserSession.GetObject(this.ProjectID, false);
        if (dbObject == null)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13307(366744972), (object) this.ProjectID);
        if (!(dbObject is DBProjectObject))
          throw new KernelExceptionID(sc_13302.ssp_appserver_13308(1934972414), (object) this.ProjectID);
        (dbObject as DBProjectObject).ProjectSecurity.CheckAccess(ActionType.Create);
      }
      if (this.VersionID > 0)
        this.ValidateStepRules(this.ModificationID);
      IDbManager dataManager = this.UserSession.DataManager;
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) this._ObjectID);
      IDbDataParameter dbDataParameter2 = dataManager.Parameter("mobjID", (object) -this._ObjectID);
      if (this.ModificationID == 0L && !this.IsBaseVersion)
      {
        DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID, F_LC_STEP, F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_BASE_VERSION = 1 AND F_MODIFICATION_ID <> 0 AND F_OBJECT_ID > 0", dataManager.Parameter("id1", (object) this.ID));
        if (dataTable.Rows.Count > 0 && (this.UserSession.GetLifecycleStep(Convert.ToInt32(dataTable.Rows[0][1]), Convert.ToInt32(dataTable.Rows[0][2])).Options & LCStepOptions.BaseVersion) == LCStepOptions.None)
        {
          this.paramsTable[177] = (object) 1;
          dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_BASE_VERSION = 1 WHERE F_OBJECT_ID = :objID", dbDataParameter1);
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            if (this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false) is DBObject dbObject)
              dbObject.SetBaseVersion(0L);
          }
        }
      }
      this.DoCheckEditingContext();
      this.DoBeforeCommitCreation();
      this.ExecuteLCSterScriptOnCreate();
      this._IsCreationMode = false;
      this.paramsTable[151] = (object) 0;
      this.ValidateCheckinRules();
      this._IsCreationMode = true;
      int num = this.InsertIntoView(true, "0", 0L);
      if (this.ObjectTypeClass.IsLocalType)
        num = 2;
      bool flag = false;
      List<DBLinkAttribute> dbLinkAttributeList = new List<DBLinkAttribute>();
      for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
      {
        DBAttribute attribute = this.Attributes[AttrIndex] as DBAttribute;
        attribute.Index = 0;
        if (num > 1 || this.UserSession.DBCache.GetOptimizationMode(attribute.AttributeID) != OptimizationModes.Write)
          attribute.InsertIntoView(-1);
        if (attribute is DBStorageAttribute)
          (attribute as DBStorageAttribute).ChangeObjectLinkID(-this._ObjectID);
        if ((attribute.AttributeType.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
          flag = true;
        if (attribute is DBLinkAttribute)
          dbLinkAttributeList.Add(attribute as DBLinkAttribute);
      }
      dataManager.ExecuteNonQuery("INSERT INTO IMS_OBJECTS (F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_ACCESS, F_MODIFY_DATE, F_LEVEL_ID, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_CREATOR_ID) SELECT -F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, 0, 0, F_OBJECT_TYPE, F_OWNER_ID, F_ACCESS, F_MODIFY_DATE, F_LEVEL_ID, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_CREATOR_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dbDataParameter1);
      dataManager.ExecuteNonQuery($"UPDATE {this.AttributesTableName} SET F_OBJECT_ID = :mobjID WHERE F_OBJECT_ID = :objID", dbDataParameter2, dbDataParameter1);
      if (MetaDataHelper.GetAttribute4ObjectType(this.ObjectType, this.UserSession.IdentHelper.FileAttributeID) != null || this.ObjectTypeClass.AnyAttributes)
        dataManager.ExecuteNonQuery("UPDATE IMS_FILENAMES SET F_KEY = :mobjID WHERE F_KEY = :objID", dbDataParameter2, dbDataParameter1);
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT DISTINCT F_RELATION_TYPE FROM IMS_RELATIONS WHERE F_PROJ_ID = :objID", dbDataParameter1).Rows)
      {
        string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, Convert.ToInt32(row[0]));
        if (updateTables != null)
        {
          foreach (string str in updateTables)
            dataManager.ExecuteNonQuery($"UPDATE {str} SET F_PROJ_ID = :mobjID WHERE F_PROJ_ID = :objID", dbDataParameter2, dbDataParameter1);
        }
      }
      dataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_PROJ_ID = :mobjID WHERE F_PROJ_ID = :objID", dbDataParameter2, dbDataParameter1);
      dataManager.ExecuteNonQuery($"UPDATE IMS_LCSTART_DATE SET F_OBJECT_ID = :mobjID, F_START_DATE = {dataManager.DataProvider.Now} WHERE F_OBJECT_ID = :objID", dbDataParameter2, dbDataParameter1);
      dataManager.ExecuteNonQuery("UPDATE IMS_GUID SET F_OBJECT_ID = :mobjID, CAPTION = F_WORK_CAPTION WHERE F_OBJECT_ID = :objID", dbDataParameter2, dbDataParameter1);
      foreach (DBLinkAttribute dbLinkAttribute in dbLinkAttributeList)
      {
        for (int index = 0; index < dbLinkAttribute.ValuesCount; ++index)
        {
          dbLinkAttribute.Index = index;
          if (!dbLinkAttribute.IsNull && dbLinkAttribute.AsInteger > 0L)
          {
            IDBObject dbObject = dbLinkAttribute.GetObject(false);
            if (dbObject == null)
            {
              dbObject = this.UserSession.GetObject(-dbLinkAttribute.AsInteger, false);
              if (dbObject == null)
                throw new KernelException(string.Format(sc_13302.ssp_appserver_13309(), (object) dbLinkAttribute.Name, (object) dbLinkAttribute.AsInteger));
            }
            if (dbObject.Caption != dbLinkAttribute.AsString)
              dbLinkAttribute.DirectSetValue("F_STRING_VALUE", (object) dbObject.Caption);
            dbLinkAttribute.InsertIntoObjectLink(dbLinkAttribute.AsInteger, true, -1);
          }
        }
      }
      dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dbDataParameter1);
      this._ObjectID = -this._ObjectID;
      this._Attributes = (IDBAttributeCollection) null;
      this.paramsTable[152] = (object) 0;
      this._IsCreationMode = false;
      if (flag)
      {
        for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
        {
          DBAttribute attribute = this.Attributes[AttrIndex] as DBAttribute;
          if ((attribute.AttributeType.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
          {
            for (int index = 0; index < attribute.ValuesCount; ++index)
            {
              attribute.Index = index;
              this.UserSession.AddAttrToIndexQueue(attribute.AsString, (IDBAttribute) attribute);
            }
            attribute.Index = 0;
          }
        }
      }
      (this.EventHelper as EventLogHelper).OnCommitCreationObject((IDBObject) this, (IUserSession) this.UserSession);
      this.UserSession.AddDelayedNotification((DelayedNotification) new ObjectDelayedNotification(this.UserSession.RealUserID, ActionType.Create, (AttributeValues[]) null, this.GetAttributes4Notification((DBAttribute) null), Math.Abs(this.ObjectID), this.ObjectType, this.ID, this.Caption, this.LevelID, this.VersionID));
      this.DoCommitCreation();
      if (autoCheckout && this.ObjectModifyMode == ObjectModifyModes.Checkout)
      {
        this._IsCreationMode = true;
        IDBObject dbObject = this.CheckOut();
        this._Attributes = (IDBAttributeCollection) null;
        this._Caption = string.Empty;
        this._GuidTable = (DataTable) null;
        this._IsCreationMode = false;
        this.paramsTable[152] = (object) this.UserSession.UserID;
        this._ObjectID = dbObject.ObjectID;
        this.Deleted = false;
      }
      else
        this.UserSession.DBCache.DeleteObjectInfo(-this._ObjectID, this.VersionGUID);
      this.UserSession.DBObjectsCacheRemoveVersion(this._ObjectID);
      this.UserSession.DBObjectsCacheRemoveVersion(-this._ObjectID);
      this.UserSession.AddCommitedObject(this);
      this.UserSession.Commit();
      this.UserSession.DBObjectsCacheAddVersion((IDBObject) this);
      this.AddEvent(Math.Abs(this.ObjectID), eventType, EventlogRecordType.AccessGranted);
    }
    catch (Exception ex1)
    {
      this.UserSession.Rollback();
      if (this._Caption == string.Empty)
        this._Caption = LocalizationHolder.rm.GetString("Kernel_402") + this.ObjectID.ToString();
      if (ex1 is AccessDeniedException)
        this.AddEvent(Math.Abs(this.ObjectID), eventType, EventlogRecordType.AccessDenied, ex1.Message);
      else
        this.AddEvent(Math.Abs(this.ObjectID), eventType, EventlogRecordType.Error, ex1.Message);
      if (deleteOnException)
      {
        try
        {
          this.Delete(0L);
        }
        catch (Exception ex2)
        {
          this.AddEvent(this.ObjectID, ActionType.Delete, EventlogRecordType.Warning, LocalizationHolder.rm.GetString("Kernel_403") + ex2.Message);
        }
      }
      this._ObjectID = -this._ObjectID;
      throw;
    }
  }

  internal void InternalAfterCommitCreation()
  {
    this.DoAfterCommitCreation();
    (this.EventHelper as EventLogHelper).OnAfterCommitCreationObject((IDBObject) this, (IUserSession) this.UserSession);
  }

  protected virtual void DoAfterCommitCreation()
  {
  }

  public void CommitCreation(bool deleteOnException)
  {
    this.CommitCreation(deleteOnException, false);
  }

  public bool IsCreationMode
  {
    get
    {
      if (this._IsCreationMode)
        return true;
      if (this.ObjectID > -1L)
        return false;
      this._IsCreationMode = this.ObjectVerType == -1;
      return this._IsCreationMode;
    }
  }

  public override long ObjectID => this._ObjectID;

  public long ID => Convert.ToInt64(this.paramsTable[121]);

  public int VersionID => Convert.ToInt32(this.paramsTable[120]);

  public IDBLifecycleStep LCStepObject
  {
    get
    {
      if (this._LCStepObject == null)
      {
        this._LCStepObject = this.UserSession.GetLifecycleStep(this.LCStep, this.ObjectType);
        (this._LCStepObject as DBSessionable)._AccessOwnerID = this.AccessOwnerID;
      }
      return this._LCStepObject;
    }
  }

  public DataTable GetLCHistory(bool allVersions)
  {
    DataTable lcHistory;
    if (allVersions)
      lcHistory = this.UserSession.DataManager.ExecuteDataTable($"SELECT TL1.F_LC_STEP, {this.UserSession.DataManager.DataProvider.GetUTCSelect("F_START_DATE", this.UserSession.TimeZoneOffset)} F_START_DATE, TO1.F_VERSION_ID FROM IMS_OBJECTS TO1, IMS_LCSTART_DATE TL1 WHERE TO1.F_ID = :objID AND TO1.F_OBJECT_ID > 0 AND TL1.F_OBJECT_ID = TO1.F_OBJECT_ID ORDER BY F_START_DATE", this.UserSession.DataManager.Parameter("objID", (object) this.ID));
    else
      lcHistory = this.UserSession.DataManager.ExecuteDataTable($"SELECT F_LC_STEP, {this.UserSession.DataManager.DataProvider.GetUTCSelect("F_START_DATE", this.UserSession.TimeZoneOffset)} F_START_DATE FROM IMS_LCSTART_DATE WHERE F_OBJECT_ID = :objID ORDER BY F_START_DATE", this.UserSession.DataManager.Parameter("objID", (object) Math.Abs(this.ObjectID)));
    return lcHistory;
  }

  public DataTable GetLCHistory() => this.GetLCHistory(false);

  public bool DenyChangeLCStep => this._DenyChangeLCStep;

  protected void DoDeleteObj()
  {
    if (this.IsSystemGUID && !this.UserSession.IsSystemSession)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13302.ssp_appserver_13310()), (object) this.ObjectName));
    this.CheckAccess(ActionType.Delete, this.GetDefaultAccess(ActionType.Delete));
    if (this.CheckoutBy == this.UserSession.UserID)
      this.UserSession.GetObject(-this.ObjectID, false)?.Delete(0L);
    this.DoDelete();
    IDbManager dataManager = this.UserSession.DataManager;
    DataRow[] dataRowArray = dataManager.ExecuteDataTable("SELECT AO.F_OBJECT_ID, AO.F_ATTRIBUTE_ID, AO.F_INLIST_ID, O.F_OBJECT_TYPE FROM IMS_OBJECT_LINKS AO, IMS_OBJECTS O WHERE AO.F_TOOBJECT_ID = :toObjID AND O.F_OBJECT_ID = AO.F_OBJECT_ID UNION ALL SELECT AO1.F_OBJECT_ID, AO1.F_ATTRIBUTE_ID, AO1.F_INLIST_ID, O1.F_OBJECT_TYPE FROM IMS_ID_LINKS AO1, IMS_OBJECTS O1 WHERE AO1.F_TO_ID = :toID AND O1.F_OBJECT_ID = AO1.F_OBJECT_ID", dataManager.Parameter("toObjID", (object) this.ObjectID), dataManager.Parameter("toID", (object) this.ID)).Select(string.Empty, "F_OBJECT_ID DESC");
    List<int> intList1 = new List<int>(dataRowArray.Length);
    List<int> intList2 = new List<int>(dataRowArray.Length);
    for (int index = 0; index < dataRowArray.Length; ++index)
    {
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(Convert.ToInt32(dataRowArray[index][3]), Convert.ToInt32(dataRowArray[index][1]));
      if (attribute4ObjectType != null && attribute4ObjectType.ValidationRule == "Value")
      {
        if (!this.UserSession.RemovableObjectsList.Exists(Convert.ToInt64(dataRowArray[index][0])))
          intList1.Add(index);
      }
      else
        intList2.Add(index);
    }
    if (intList1.Count > 0)
    {
      long[] objectsID = new long[intList1.Count];
      for (int index = 0; index < intList1.Count; ++index)
        objectsID[index] = Convert.ToInt64(dataRowArray[intList1[index]][0]);
      throw new ObjectsFoundException(string.Format(sc_13302.ssp_appserver_13311(), (object) this.NameInMessages), string.Empty, objectsID);
    }
    if (intList2.Count > 0)
    {
      for (int index = 0; index < intList2.Count; ++index)
      {
        IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(dataRowArray[intList2[index]][0]), false);
        if (dbObject != null)
        {
          DBAttribute attributeById = dbObject.GetAttributeByID(Convert.ToInt32(dataRowArray[intList2[index]][1])) as DBAttribute;
          attributeById.ValidatingOn = false;
          if (attributeById.ValuesCount > 1)
          {
            attributeById.Index = Convert.ToInt32(dataRowArray[intList2[index]][2]);
            attributeById.DeleteValue();
          }
          else
            attributeById.Clear();
        }
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT AO.F_PRJLINK_ID, AO.F_ATTRIBUTE_ID, AO.F_INLIST_ID FROM IMS_RELATION_ATTRS AO, IMS_ATTRIBUTES A WHERE AO.F_INTEGER_VALUE = :objID AND A.F_ATTRIBUTE_ID = AO.F_ATTRIBUTE_ID AND A.F_ATTRIBUTE_TYPE = :attrType ORDER BY AO.F_INLIST_ID DESC", dataManager.Parameter("objID", (object) this.ObjectID), dataManager.Parameter("attrType", (object) 8)).Rows)
    {
      IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row[0]));
      DBAttribute attributeById = relation.GetAttributeByID(Convert.ToInt32(row[1])) as DBAttribute;
      if (attributeById.AttributeType.ValidationRule == "Value")
        throw new KernelExceptionID(sc_13302.ssp_appserver_13312(889762499), (object) this.ObjectName, (object) this.ObjectID, (object) attributeById.Name, (object) (relation as DBRelation).ObjectName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      attributeById.ValidatingOn = false;
      if (attributeById.ValuesCount > 1)
      {
        attributeById.Index = Convert.ToInt32(row[2]);
        attributeById.DeleteValue();
      }
      else
        attributeById.Clear();
    }
    this.DoDeleteObj_DeleteUpLinks();
    this.DoDeleteObj_DeleteDownLinks();
    this.relationsDeleted = true;
    this.DeleteFromContext(true);
  }

  protected virtual void DoDelete()
  {
  }

  protected virtual void DoDeleteObj_DeleteUpLinks()
  {
    IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(-1, "cad001e0-306c-11d8-b4e9-00304f19f545");
    relationCollection.LocalTypesMode = true;
    (relationCollection as DBRelationCollection).GlobalSelectMode = true;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[7]
    {
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
      (object) ObligatoryObjectAttributes.F_PROJ_ID,
      (object) ObligatoryObjectAttributes.F_PART_ID,
      (object) ObligatoryObjectAttributes.F_RELATION_TYPE,
      (object) ObligatoryObjectAttributes.F_CREATE_DATE,
      (object) ObligatoryObjectAttributes.F_PRJ_GUID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    });
    paramSet.ColumnNames = new ColumnNameMapping[7]
    {
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName,
      ColumnNameMapping.FieldName
    };
    IDbManager dataManager = this.UserSession.DataManager;
    object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID <> :objID AND F_ID = :id1 AND F_LEVEL_ID <> :level1 AND F_OBJECT_VER_TYPE <> :blankID", dataManager.Parameter("objID", (object) this.ObjectID), dataManager.Parameter("id1", (object) this.ID), dataManager.Parameter("level1", (object) this.UserSession.IdentHelper.DeletedID), dataManager.Parameter("blankID", (object) -1));
    bool lastVersion = obj == null || obj == DBNull.Value;
    bool flag = false;
    if (!lastVersion)
    {
      this.ValidateDeleteBaseVersion();
      DataTable applicabilitiesList = this.UserSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, this.ObjectType, -1);
      DataRow[] dataRowArray = applicabilitiesList.Select(string.Empty, "F_RELATION_TYPE");
      int aRelationTypeID = -1;
      int columnIndex1 = applicabilitiesList.Columns.IndexOf("F_RELATION_TYPE");
      int columnIndex2 = applicabilitiesList.Columns.IndexOf("F_MIN_LINKS");
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        if (aRelationTypeID != Convert.ToInt32(dataRowArray[index][columnIndex1]) && Convert.ToInt32(dataRowArray[index][columnIndex2]) != -1)
        {
          aRelationTypeID = Convert.ToInt32(dataRowArray[index][columnIndex1]);
          if (this.UserSession.GetRelationType(aRelationTypeID).Attributes.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID, false) != null)
          {
            flag = true;
            break;
          }
        }
      }
    }
    if (!(lastVersion | flag))
      return;
    this.DoDeleteObj_DeleteUpLinks(relationCollection.EntersInVersion(paramSet, this.ObjectID, this.ID), lastVersion);
  }

  protected virtual void DoDeleteObj_DeleteUpLinks(DataTable table, bool lastVersion)
  {
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      DataRow row = table.Rows[index];
      int int32 = Convert.ToInt32(row[3]);
      if (this.UserSession.GetRelation(table, index) is DBRelation relation)
      {
        if (!lastVersion)
        {
          IDBAttribute attributeById = relation.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID);
          if (attributeById == null || attributeById.AsInteger != this.ObjectID)
            continue;
        }
        IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(int32, this.ObjectType, Convert.ToInt32(row[6]));
        bool flag = false;
        if (applicability == null)
        {
          try
          {
            this.EventHelper.AddEvent(this.ObjectID, 0L, 14, 1001L, LocalizationHolder.rm.GetString("Kernel_884"), string.Format(LocalizationHolder.rm.GetString("Kernel_404"), (object) this.ObjectTypeClass.ObjectInstanceName, (object) this.UserSession.GetObjectType(Convert.ToInt32(row[6])).ObjectInstanceName, (object) this.UserSession.GetRelationType(Convert.ToInt32(row[3])).Description), ActionType.Delete, EventlogRecordType.Error, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
          }
          catch
          {
          }
        }
        else
        {
          flag = applicability.IsContent;
          if ((applicability.RelationConstraintMode == RelationConstraintModes.ChildConstrained || applicability.RelationConstraintMode == RelationConstraintModes.ParentChildConstrained) && !this.UserSession.RemovableObjectsList.Exists(Convert.ToInt64(row["F_PROJ_ID"])))
            throw new KernelExceptionID(108, (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObject(Convert.ToInt64(row[1])).NameInMessages, (object) row[1].ToString()).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(Convert.ToInt64(row[1].ToString())));
        }
        if (lastVersion & flag && this.ObjectID != Convert.ToInt64(row[1]) && this.UserSession.GetObject(Convert.ToInt64(row[1]), false) is DBObject dbObject)
          dbObject.CheckEditMode(dbObject.CheckoutBy != this.UserSession.UserID, true, true);
        relation._SenderObject = (IDBObject) this;
        relation.Delete((long) Consts.PurgeMode);
        if (applicability != null && applicability.RelationConstraintMode == RelationConstraintModes.ParentDelete)
        {
          IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(Convert.ToInt32(row[3]));
          relationCollection.LocalTypesMode = true;
          (relationCollection as DBRelationCollection).GlobalSelectMode = true;
          if (relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
          {
            (object) ObligatoryObjectAttributes.F_PRJLINK_ID
          }), Convert.ToInt64(row[1])).Rows.Count == 0)
            this.UserSession.GetObject(Convert.ToInt64(row[1]), false)?.Delete((long) Consts.RelationConstraintMode);
        }
      }
    }
  }

  protected virtual void DoDeleteObj_DeleteDownLinks()
  {
    IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(-1, "cad00601-306c-11d8-b4e9-00304f19f545");
    relationCollection.LocalTypesMode = true;
    (relationCollection as DBRelationCollection).GlobalSelectMode = true;
    this.DoDeleteObj_DeleteDownLinks(relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[8]
    {
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
      (object) ObligatoryObjectAttributes.F_PROJ_ID,
      (object) ObligatoryObjectAttributes.F_PART_ID,
      (object) ObligatoryObjectAttributes.F_RELATION_TYPE,
      (object) ObligatoryObjectAttributes.F_CREATE_DATE,
      (object) ObligatoryObjectAttributes.F_PRJ_GUID,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    })
    {
      ColumnNames = new ColumnNameMapping[8]
      {
        ColumnNameMapping.FieldName,
        ColumnNameMapping.FieldName,
        ColumnNameMapping.FieldName,
        ColumnNameMapping.FieldName,
        ColumnNameMapping.FieldName,
        ColumnNameMapping.FieldName,
        ColumnNameMapping.FieldName,
        ColumnNameMapping.FieldName
      }
    }, this.ObjectID));
  }

  protected virtual void DoDeleteObj_DeleteDownLinks(DataTable table)
  {
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      DataRow row = table.Rows[index];
      IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(Convert.ToInt32(row[3]), Convert.ToInt32(row[7]), this.ObjectType);
      if (applicability != null)
      {
        if ((applicability.RelationConstraintMode == RelationConstraintModes.ParentChildConstrained || applicability.RelationConstraintMode == RelationConstraintModes.ParentConstrained) && !this.UserSession.RemovableObjectsList.Exists(Convert.ToInt64(row["F_OBJECT_ID"])))
          throw new KernelExceptionID(sc_13302.ssp_appserver_13314(299045745), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      }
      else
      {
        try
        {
          this.EventHelper.AddEvent(this.ObjectID, 0L, 14, 1001L, LocalizationHolder.rm.GetString("Kernel_885"), string.Format(LocalizationHolder.rm.GetString("Kernel_405"), (object) this.UserSession.GetObjectType(Convert.ToInt32(row[7])).ObjectInstanceName, (object) this.ObjectTypeClass.ObjectInstanceName, (object) this.UserSession.GetRelationType(Convert.ToInt32(row[3])).Description), ActionType.Delete, EventlogRecordType.Error, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
        }
        catch
        {
        }
      }
      if (this.UserSession.GetRelation(table, index) is DBRelation relation)
      {
        long DeleteVersionID = 0;
        if (applicability != null && (applicability.RelationConstraintMode == RelationConstraintModes.ChildDelete || applicability.RelationConstraintMode == RelationConstraintModes.ChildForcedDelete) && this.UserSession.GetRelationType(applicability.RelationType).GetAttributeType(this.UserSession.IdentHelper.AttributeVersionInRelation) != null)
        {
          IDBAttribute attributeById = relation.GetAttributeByID(this.UserSession.IdentHelper.AttributeVersionInRelation);
          if (attributeById != null && !attributeById.IsNull)
            DeleteVersionID = attributeById.AsInteger;
        }
        relation._SenderObject = (IDBObject) this;
        relation.Delete((long) Consts.PurgeMode);
        if (applicability != null && (applicability.RelationConstraintMode == RelationConstraintModes.ChildDelete || applicability.RelationConstraintMode == RelationConstraintModes.ChildForcedDelete) && this.UserSession.GetObject(Convert.ToInt64(row[6]), false) != null)
          this.DoDeleteObj_DeleteDownLink_DeleteObj((IDBRelation) relation, Convert.ToInt32(row[7]), DeleteVersionID, applicability.RelationConstraintMode == RelationConstraintModes.ChildForcedDelete);
      }
    }
  }

  protected virtual void DoDeleteObj_DeleteDownLink_DeleteObj(
    IDBRelation ChildRelation,
    int ChildObjTypeID,
    long DeleteVersionID,
    bool forcedDelete,
    string ExtraSqlCond = "")
  {
    bool flag = true;
    IDbManager dataManager = this.UserSession.DataManager;
    if (DeleteVersionID != 0L)
    {
      if (!forcedDelete)
      {
        if (this.ObjectID < 0L)
        {
          object obj = dataManager.ExecuteScalar("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PART_ID = :partID AND (F_PROJ_ID = :projID OR EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = F_PROJ_ID AND F_ID <> F_PART_ID)) " + ExtraSqlCond, dataManager.Parameter("partID", (object) ChildRelation.PartID), dataManager.Parameter("projID", (object) -this.ObjectID));
          flag = obj == null || obj == DBNull.Value;
        }
        else
        {
          object obj = dataManager.ExecuteScalar("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PART_ID = :partID AND EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = F_PROJ_ID AND F_ID <> F_PART_ID) " + ExtraSqlCond, dataManager.Parameter("partID", (object) ChildRelation.PartID));
          flag = obj == null || obj == DBNull.Value;
        }
      }
      if (!flag)
        return;
      this.UserSession.GetObject(DeleteVersionID, false)?.Delete((long) Consts.RelationConstraintMode);
    }
    else
    {
      if ((this.ObjectID < 0L || this.ObjectTypeClass.Versionable == ObjectVersionModes.MultiVersion) && !forcedDelete)
      {
        object obj = dataManager.ExecuteScalar("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PART_ID = :partID AND F_PROJ_ID <> :projID " + ExtraSqlCond, dataManager.Parameter("partID", (object) ChildRelation.PartID), dataManager.Parameter("projID", (object) this.ObjectID));
        flag = obj == null || obj == DBNull.Value;
      }
      if (!flag)
        return;
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :parID", dataManager.Parameter("parID", (object) ChildRelation.PartID));
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false)?.Delete((long) Consts.RelationConstraintMode);
    }
  }

  public void ValidateSetNextLCStep(int nextstepID)
  {
    this.ValidateSetNextLCStep(this.UserSession.GetLifecycleStep(nextstepID, true), new List<long>());
  }

  public void ValidateSetNextLCStep(Guid nextstepGUID)
  {
    this.ValidateSetNextLCStep(this.UserSession.GetLifecycleStep(nextstepGUID, true), new List<long>());
  }

  protected virtual void ValidateSetNextLCStep(IDBLifecycleStep nextstep, List<long> validateList)
  {
    if (nextstep.LCStep == this.LCStepObject.LCStep)
      return;
    (this.EventHelper as EventLogHelper).OnBeforeNextLCStep((IDBObject) this, nextstep, (IUserSession) this.UserSession);
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) this.ObjectID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("lcStep", (object) nextstep.LCStep);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("id1", (object) this.ID);
    if (this.CheckoutBy != 0L)
    {
      if (this.ObjectID < 0L)
        throw new KernelExceptionID(sc_13302.ssp_appserver_13315(78448626), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      if (nextstep.LevelID != this.UserSession.IdentHelper.DeletedID)
        throw new KernelExceptionID(sc_13302.ssp_appserver_13316(200562547), (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObjectInfo(this.CheckoutBy).Caption).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    }
    int[] nextSteps = this.LCStepObject.GetNextSteps();
    bool flag = false;
    foreach (int num in nextSteps)
    {
      if (num == nextstep.LCStep)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13317(1579756648), (object) this.NameInMessages, (object) this.ObjectID, (object) nextstep.LCName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    (nextstep as DBLifecycleStep).CheckAccess(ActionType.NextLCStep);
    this.CheckChangeEnable("F_LC_STEP");
    if (this.LCStepObject.LevelID == this.UserSession.IdentHelper.DeletedID)
      this.ValidateCheckinRules();
    if (nextstep.LevelID == this.UserSession.IdentHelper.DeletedID && this.ObjectTypeClass.LifetimeReserve <= 0)
      return;
    if (nextstep.AutoTransferStepID == 0)
    {
      if (this.ModificationID == 0L && (nextstep.Options & LCStepOptions.DisableParallelVersions) == LCStepOptions.DisableParallelVersions)
      {
        object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_LC_STEP = :lcStep AND F_OBJECT_ID <> :objID AND F_OBJECT_ID <> :objID_m AND F_LEVEL_ID <> :delLevel AND F_OBJECT_VER_TYPE <> :blankID AND F_MODIFICATION_ID = 0", dbDataParameter3, dbDataParameter2, dbDataParameter1, dataManager.Parameter("objID_m", (object) -this.ObjectID), dataManager.Parameter("delLevel", (object) this.UserSession.IdentHelper.DeletedID), dataManager.Parameter("blankID", (object) -1));
        if (obj != null && obj != DBNull.Value)
          throw new KernelExceptionID(325, (object) nextstep.LCName, (object) this.NameInMessages, (object) this.UserSession.GetLCSchema(nextstep.SchemaID).Name);
      }
      if ((nextstep.Options & LCStepOptions.DisableContextParallelVersions) == LCStepOptions.DisableContextParallelVersions)
      {
        object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_LC_STEP = :lcStep AND F_OBJECT_ID <> :objID AND F_OBJECT_ID <> :objID_m AND F_LEVEL_ID <> :delLevel AND F_OBJECT_VER_TYPE <> :blankID AND F_MODIFICATION_ID <> 0", dbDataParameter3, dbDataParameter2, dbDataParameter1, dataManager.Parameter("objID_m", (object) -this.ObjectID), dataManager.Parameter("delLevel", (object) this.UserSession.IdentHelper.DeletedID), dataManager.Parameter("blankID", (object) -1));
        if (obj != null && obj != DBNull.Value)
          throw new KernelExceptionID(325, (object) nextstep.LCName, (object) this.NameInMessages, (object) this.UserSession.GetLCSchema(nextstep.SchemaID).Name);
      }
    }
    this.DoSyncLC_RelatedObjects(nextstep, validateList);
  }

  public bool CanSetNextLCStep(int nextstepID, out string errorMessage)
  {
    try
    {
      UserSessionPluginsData<List<long>> lcListFromSession = DBObject.GetChangeLCListFromSession(this.UserSession);
      bool flag = false;
      if (lcListFromSession.Value == null)
      {
        lcListFromSession.Value = new List<long>();
        flag = true;
      }
      lcListFromSession.Value.Add(this.ObjectID);
      try
      {
        this.ValidateSetNextLCStep(nextstepID);
      }
      finally
      {
        if (flag)
          lcListFromSession.Value = (List<long>) null;
      }
      errorMessage = string.Empty;
      return true;
    }
    catch (Exception ex)
    {
      errorMessage = ex.Message;
      return false;
    }
  }

  protected virtual void DoNextLCStep(IDBLifecycleStep nextstep)
  {
    this.ValidateSetNextLCStep(nextstep, new List<long>());
    if (nextstep.LevelID == this.UserSession.IdentHelper.DeletedID)
    {
      if (this.ObjectID < 0L)
      {
        this.Deleted = true;
        return;
      }
      this.DoDeleteObj();
    }
    else if (this.LCStepObject.LevelID == this.UserSession.IdentHelper.DeletedID && !this.IsBaseVersion)
    {
      if (Convert.ToInt32(this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_BASE_VERSION = 1 AND F_LEVEL_ID <> :levelID", this.UserSession.DataManager.Parameter("id1", (object) this.ID), this.UserSession.DataManager.Parameter("levelID", (object) this.UserSession.IdentHelper.DeletedID))) == 0)
        this.SetBaseVersion();
    }
    if (nextstep.LevelID != this.UserSession.IdentHelper.DeletedID || this.ObjectTypeClass.LifetimeReserve > 0)
      this.SetLCStep(nextstep);
    if (nextstep.LevelID != this.UserSession.IdentHelper.DeletedID)
      return;
    this.paramsTable[136] = (object) nextstep.LCStep;
    this.paramsTable[72] = (object) nextstep.LevelID;
    if (this.ObjectTypeClass.LifetimeReserve == 0)
      return;
    this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, this.ObjectID, ActionType.Delete, this.ObjectType));
  }

  internal void SetLCStep(IDBLifecycleStep nextstep)
  {
    if (this.LCStep == nextstep.LCStep)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) this.ObjectID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("lcStep", (object) nextstep.LCStep);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("id1", (object) this.ID);
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_LC_LINKS").Select("F_FROM_STEP = " + (object) nextstep.LCStep);
    for (int index1 = 0; index1 < dataRowArray.Length; ++index1)
    {
      if ((Convert.ToInt32(dataRowArray[index1]["F_PARAMS"]) & 1) == 1)
      {
        DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_LC_STEP = :lcStep AND F_OBJECT_ID <> :objID", dbDataParameter3, dbDataParameter2, dbDataParameter1);
        for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
        {
          DBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index2][0])) as DBObject;
          if (!this.UserSession.DBCache.IsSpecification(dbObject.ObjectType))
            dbObject._DenyChangeLCStep = true;
          dbObject.LCStep = Convert.ToInt32(dataRowArray[index1]["F_TO_STEP"]);
          int attributeId = this.UserSession.IdentHelper.GetAttributeID("cadd9597-306c-11d8-b4e9-00304f19f545");
          if (this.ObjectTypeClass.HasAttribute(attributeId))
          {
            try
            {
              this.Attributes.AddAttribute(attributeId, false, new object[1]
              {
                (object) dbObject.ObjectID
              });
            }
            catch (Exception ex)
            {
              this.UserSession.EventLogHelper.AddToTrace($"Ошибка сохранения идентификатора предыдущей версии объекта в объект '{this.NameInMessages}': {ex.Message}", Consts.traceAlways, string.Empty);
            }
          }
        }
        break;
      }
    }
    this.DoSyncLC_RelatedObjects(nextstep, (List<long>) null);
    if (nextstep.LevelID == this.UserSession.IdentHelper.AnnulmentLevelID && this.CheckAnnulment)
    {
      UserSessionPluginsData<List<long>> lcListFromSession = DBObject.GetChangeLCListFromSession(this.UserSession);
      DataTable dataTable1 = dataManager.ExecuteDataTable(SqlHelper.GetEntersInSQL(this.ID, "F_OBJECT_ID, F_RELATION_TYPE", $"F_LEVEL_ID <> {this.UserSession.IdentHelper.AnnulmentLevelID} AND F_LEVEL_ID <> {this.UserSession.IdentHelper.KeepingLevelID} AND F_OBJECT_VER_TYPE <> {-1}", dataManager), dataManager.Parameter("partID", (object) this.ID));
      if (dataTable1.Rows.Count > 0)
      {
        IDBRelationType dbRelationType = (IDBRelationType) null;
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
        {
          int int32 = Convert.ToInt32(dataTable1.Rows[index][1]);
          if (dbRelationType == null || dbRelationType.RelationType != int32)
            dbRelationType = this.UserSession.GetRelationType(int32);
          if ((dbRelationType.Options & RelationTypeOptions.EnableCheckAnnulment) == RelationTypeOptions.EnableCheckAnnulment && lcListFromSession.Value.IndexOf(Convert.ToInt64(dataTable1.Rows[index][0])) < 0)
            throw new KernelExceptionID(340, (object) this.NameInMessages, (object) this.ObjectID, (object) dataTable1.Rows.Count).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
        }
      }
      if (ServerConsts.AnnulAllVersions)
      {
        DataTable dataTable2 = dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_OBJECT_ID <> :objID AND F_LEVEL_ID <> {this.UserSession.IdentHelper.DeletedID} AND F_LEVEL_ID <> {this.UserSession.IdentHelper.AnnulmentLevelID} AND F_OBJECT_VER_TYPE <> {-1}", dbDataParameter3, dbDataParameter1);
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          if (this.UserSession.GetObject(Convert.ToInt64(dataTable2.Rows[index][0]), false) is DBObject dbObject && lcListFromSession.Value.IndexOf(dbObject.ObjectID) < 0)
          {
            dbObject.CheckAnnulment = false;
            dbObject.LCStep = nextstep.LCStep;
          }
        }
      }
      else
      {
        this.CheckAnnulment = false;
        this.LCStep = nextstep.LCStep;
      }
    }
    if (this.UserSession.DBCache.IsDocument(this.ObjectType) && ((ICustomServices) ServerServices.GetService(typeof (ICustomServices))).GetService(typeof (IRedliningService)) is IRedliningService service && service.DeleteFiles && nextstep.LevelID == service.LevelID && this.LevelID != service.LevelID)
    {
      if (this.GetAttributeByID(service.RedliningAttributeID) is DBAttribute attributeById1)
        attributeById1.Purge(false);
      if (this.GetAttributeByID(this.UserSession.IdentHelper.FileAttributeID) is DBAttribute attributeById2 && attributeById2.ValuesCount > 1)
      {
        attributeById2.Index = 0;
        attributeById2.AsString.Trim().ToUpper();
        for (int index = attributeById2.ValuesCount - 1; index > 0; --index)
        {
          attributeById2.Index = index;
          if ((attributeById2 as IBlobReader).OpenBlob(-1).FileType == FileTypes.ftRedlining)
          {
            try
            {
              this._MustCheckValidatingRule = false;
              attributeById2.DeleteValue();
            }
            finally
            {
              this._MustCheckValidatingRule = true;
            }
          }
        }
      }
    }
    this.DoSetLCStep(nextstep, true);
  }

  private void DoSyncLC_RelatedObjects(IDBLifecycleStep nextstep, List<long> validateList)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    UserSessionPluginsData<List<long>> lcListFromSession = DBObject.GetChangeLCListFromSession(this.UserSession);
    if (lcListFromSession.Value == null || this._DenyChangeLCStep || nextstep.LevelID == this.UserSession.IdentHelper.DeletedID)
      return;
    validateList?.Add(this.ObjectID);
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, this.ObjectType, -1);
    ConditionStructure[] conditions = (ConditionStructure[]) null;
    if (applicabilitiesList.Rows.Count > 0)
    {
      if (this.ModificationID == 0L)
        conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-4, RelationalOperators.Equal, (object) this.LCStep, LogicalOperators.AND, 0, true)
        };
      else
        conditions = new ConditionStructure[2]
        {
          new ConditionStructure(-4, RelationalOperators.Equal, (object) this.LCStep, LogicalOperators.OR, 1, true),
          new ConditionStructure(-15, RelationalOperators.Equal, (object) this.ModificationID, LogicalOperators.AND, -1, true)
        };
    }
    for (int index1 = 0; index1 < applicabilitiesList.Rows.Count; ++index1)
    {
      if (Convert.ToInt32(applicabilitiesList.Rows[index1]["F_MIN_LINKS"]) >= 0 && (Convert.ToInt32(applicabilitiesList.Rows[index1]["F_OPTIONS"]) & 4) == 4)
      {
        DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(Convert.ToInt32(applicabilitiesList.Rows[index1]["F_RELATION_TYPE"]), "cad005ac-306c-11d8-b4e9-00304f19f5455") as DBRelationCollection;
        relationCollection._ShowPersonalObjects = true;
        relationCollection.ChildObjectTypes = (IList<int>) MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(Convert.ToInt32(applicabilitiesList.Rows[index1]["F_INOBJECT_TYPE"]));
        DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[2]
        {
          (object) -21,
          (object) -7
        });
        DataTable dataTable = relationCollection.EntersInVersion(paramSet, this.ObjectID, this.ID);
        for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
        {
          IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(relationCollection.RelationTypeID, this.ObjectType, Convert.ToInt32(dataTable.Rows[index2][1]));
          if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled && (applicability.Options & ApplicabilityOptions.ChangeLCStep) == ApplicabilityOptions.ChangeLCStep)
          {
            long int64 = Convert.ToInt64(dataTable.Rows[index2][0]);
            if (int64 != this.ObjectID)
            {
              DBObject dbObject = this.UserSession.GetObject(int64, true) as DBObject;
              if (dbObject.ID != this.ID && dbObject != null)
              {
                if (validateList != null)
                {
                  if (validateList.IndexOf(dbObject.ObjectID) < 0)
                    dbObject.ValidateSetNextLCStep(nextstep, validateList);
                }
                else if (lcListFromSession.Value.IndexOf(dbObject.ObjectID) < 0)
                  dbObject.LCStep = nextstep.LCStep;
              }
            }
          }
        }
      }
    }
    if (!this.UserSession.DBCache.IsSyncParentObjectType(this.ObjectType))
      return;
    DataTable dataTable1 = dataManager.ExecuteDataTable("select distinct R.F_RELATION_TYPE, O.F_OBJECT_TYPE FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE R.F_PROJ_ID = :objID AND O.F_ID = R.F_PART_ID", dataManager.Parameter("objID", (object) this.ObjectID));
    for (int index3 = 0; index3 < dataTable1.Rows.Count; ++index3)
    {
      IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(Convert.ToInt32(dataTable1.Rows[index3][0]), Convert.ToInt32(dataTable1.Rows[index3][1]), this.ObjectType);
      if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled && (applicability.Options & ApplicabilityOptions.ChangeLCStep) == ApplicabilityOptions.ChangeLCStep)
      {
        DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(Convert.ToInt32(dataTable1.Rows[index3][0]), "cad005ac-306c-11d8-b4e9-00304f19f5455") as DBRelationCollection;
        relationCollection._ShowPersonalObjects = true;
        relationCollection.ObjectTypeID = Convert.ToInt32(dataTable1.Rows[index3][1]);
        DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[2]
        {
          (object) -2,
          (object) -7
        });
        DataTable dataTable2 = relationCollection.ConsistFrom(paramSet, this.ObjectID);
        for (int index4 = 0; index4 < dataTable2.Rows.Count; ++index4)
        {
          if (Convert.ToInt32(dataTable1.Rows[index3][1]) == Convert.ToInt32(dataTable2.Rows[index4][1]))
          {
            long int64 = Convert.ToInt64(dataTable2.Rows[index4][0]);
            if (int64 != this.ObjectID)
            {
              DBObject dbObject = this.UserSession.GetObject(int64, true) as DBObject;
              if (dbObject.ID != this.ID && dbObject != null)
              {
                if (validateList != null)
                {
                  if (validateList.IndexOf(dbObject.ObjectID) < 0)
                    dbObject.ValidateSetNextLCStep(nextstep, validateList);
                }
                else if (lcListFromSession.Value.IndexOf(dbObject.ObjectID) < 0)
                  dbObject.LCStep = nextstep.LCStep;
              }
            }
          }
        }
      }
    }
  }

  internal void DoSetLCStep(IDBLifecycleStep nextstep, bool doSoftInstant)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) this.ObjectID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("lcStep", (object) nextstep.LCStep);
    this.FireObligatoryAttributeWrite(ObligatoryObjectAttributes.F_LC_STEP, 136, (object) nextstep.LCStep);
    dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_LC_STEP = :lcStep WHERE F_OBJECT_ID = :objID", dbDataParameter2, dbDataParameter1);
    this.UpdateViewValue("F_LC_STEP", (object) nextstep.LCStep);
    long num = 0;
    if (this.ObjectID > 0L)
      dataManager.ExecuteSpNonQuery("IMS_ADD_LCSTART_DATE", dataManager.Parameter("inOBJECT_ID", (object) this.ObjectID), dataManager.Parameter("inLC_STEP", (object) nextstep.LCStep), dataManager.Parameter("inSTART_DATE", (object) DateTime.UtcNow), dataManager.OutputParameter("outKEY_ID", (object) num));
    dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_LEVEL_ID = :levID WHERE F_OBJECT_ID = :objID", dataManager.Parameter("levID", (object) nextstep.LevelID), dbDataParameter1);
    this.UpdateViewValue("F_LEVEL_ID", (object) nextstep.LevelID);
    if (doSoftInstant)
    {
      if ((nextstep.Options & LCStepOptions.BaseVersion) == LCStepOptions.BaseVersion)
      {
        this.SetBaseVersion();
        Dictionary<int, string> instantiationApp = this.GetSoftInstantiationApp();
        string str = "F" + this.UserSession.IdentHelper.CompositionVersionID.ToString();
        foreach (KeyValuePair<int, string> keyValuePair in instantiationApp)
        {
          IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(keyValuePair.Key, this.UserSession.IdentHelper.CompositionVersionID);
          DataTable dataTable = dataManager.ExecuteDataTable(string.Format("SELECT R.F_PRJLINK_ID, R.{0} FROM IMV_R{1} R, IMS_OBJECTS O WHERE (R.F_PROJ_ID = :objID) AND (R.{0} > 0) AND (O.F_OBJECT_ID = R.{0}) AND (O.F_OBJECT_TYPE IN ({2}))", (object) str, (object) keyValuePair.Key, (object) keyValuePair.Value), dbDataParameter1);
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            if (this.UserSession.GetRelation(Convert.ToInt64(dataTable.Rows[index][0]), false) is DBRelation relation)
            {
              IDBAttribute attributeByGuid = relation.GetAttributeByGuid(Intermech.Search.Data.Filters.Constants.RevisionInstantiationModeAttributeTypeGuid);
              if (attributeByGuid == null || attributeByGuid.AsInteger != 1L)
              {
                relation._PartObjectID = Convert.ToInt64(dataTable.Rows[index][1]);
                relation.Attributes.AddAttribute(this.UserSession.IdentHelper.CompositionVersionBackup, false, new object[1]
                {
                  (object) relation._PartObjectID
                });
                if (relation.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID) is DBAttribute attributeById)
                {
                  if (attribute4RelationType.Required == RequiredModes.AutoRequired)
                    attributeById.DirectSetValue("F_INTEGER_VALUE", (object) 0);
                  else
                    attributeById.Purge(false);
                }
              }
            }
          }
        }
      }
      else if ((nextstep.Options & LCStepOptions.RestoreSoftInstantiation) == LCStepOptions.RestoreSoftInstantiation)
      {
        Dictionary<int, string> instantiationApp = this.GetSoftInstantiationApp();
        string str = "F" + this.UserSession.IdentHelper.CompositionVersionBackup.ToString();
        foreach (KeyValuePair<int, string> keyValuePair in instantiationApp)
        {
          IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(keyValuePair.Key, this.UserSession.IdentHelper.CompositionVersionBackup);
          if (attribute4RelationType.OptimizationMode != OptimizationModes.Seek)
            throw new KernelExceptionID(sc_13302.ssp_appserver_13319(168656724), (object) this.UserSession.GetAttributeType(this.UserSession.IdentHelper.CompositionVersionBackup).Name, (object) this.UserSession.GetRelationType(keyValuePair.Key).Description);
          DataTable dataTable = dataManager.ExecuteDataTable(string.Format("SELECT R.F_PRJLINK_ID, R.{0} FROM IMV_R{1} R, IMS_OBJECTS O WHERE (R.F_PROJ_ID = :objID) AND (R.{0} > 0) AND (O.F_OBJECT_ID = R.{0}) AND (O.F_OBJECT_TYPE IN ({2}))", (object) str, (object) keyValuePair.Key, (object) keyValuePair.Value), dbDataParameter1);
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            if (this.UserSession.GetRelation(Convert.ToInt64(dataTable.Rows[index][0]), false) is DBRelation relation)
            {
              relation._PartObjectID = Convert.ToInt64(dataTable.Rows[index][1]);
              (relation.Attributes as DBAttributeCollection).AddAttribute(this.UserSession.IdentHelper.CompositionVersionID, false, false, new object[1]
              {
                (object) relation._PartObjectID
              });
              if (relation.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionBackup) is DBAttribute attributeById)
              {
                if (attribute4RelationType.Required == RequiredModes.AutoRequired)
                  attributeById.DirectSetValue("F_INTEGER_VALUE", (object) 0);
                else
                  attributeById.Purge(false);
              }
            }
          }
        }
      }
    }
    foreach (IMSAttribute4ObjectType attribute4ObjectType in (ServerServices.GetService(typeof (IMetaDataHelper)) as IMetaDataHelper).GetAttribute4ObjectTypeList(this.ObjectType))
    {
      if (attribute4ObjectType.LevelID == nextstep.LevelID && (attribute4ObjectType.Required == RequiredModes.Auto || attribute4ObjectType.Required == RequiredModes.AutoRequired))
        this.Attributes.AddAttribute(attribute4ObjectType.AttributeID, false);
    }
    this.paramsTable[136] = (object) nextstep.LCStep;
    this.paramsTable[72] = (object) nextstep.LevelID;
    this._LCStepObject = (IDBLifecycleStep) null;
    this.RecalcAttributes(-4);
    this.RecalcAttributes(-9);
    this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, this.ObjectID, ActionType.NextLCStep, this.ObjectType));
    this.ClearObjectAccessCache();
  }

  public int LCStep
  {
    get => Convert.ToInt32(this.paramsTable[136]);
    set
    {
      if (this.LCStep == value)
        return;
      this.CheckDeleted(nameof (LCStep));
      IDBLifecycleStep lifecycleStep = this.UserSession.GetLifecycleStep(value, this.ObjectType);
      this.PrepareNextLCStep(lifecycleStep as DBSessionable);
      long EventID = this.AddEvent(this.ObjectID, ActionType.NextLCStep, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_407"), (object) lifecycleStep.LCName));
      if (this.CheckoutBy != 0L)
      {
        if (this.ObjectID < 0L)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13320(1320775269), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
        int deleteStepId = this.LCStepObject.GetDeleteStepID();
        if (deleteStepId >= 0 && value != deleteStepId)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13321(1743483278), (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObjectInfo(this.CheckoutBy).Caption).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      }
      this.CheckAccess(ActionType.NextLCStep, this.GetDefaultAccess(ActionType.NextLCStep));
      this.UserSession.StartTransaction();
      try
      {
        UserSessionPluginsData<List<long>> lcListFromSession = DBObject.GetChangeLCListFromSession(this.UserSession);
        bool flag = false;
        if (lcListFromSession.Value == null)
        {
          lcListFromSession.Value = new List<long>();
          flag = true;
        }
        lcListFromSession.Value.Add(this.ObjectID);
        try
        {
          int lcStep = this.LCStep;
          int levelId = this.LevelID;
          AttributeValues[] attributes4Notification = this.GetAttributes4Notification((DBAttribute) null);
          this.DoNextLCStep(lifecycleStep);
          this.UserSession.AddDelayedNotification((DelayedNotification) new SetLCStepDelayedNotification(this.UserSession.RealUserID, attributes4Notification, (AttributeValues[]) null, Math.Abs(this.ObjectID), this.ObjectType, this.ID, this.Caption, levelId, lifecycleStep.LevelID, this.VersionID, lcStep, lifecycleStep.LCStep));
          if (lifecycleStep.LevelID == this.UserSession.IdentHelper.DeletedID)
            this.UserSession.AddDelayedNotification((DelayedNotification) new ObjectDelayedNotification(this.UserSession.RealUserID, ActionType.Delete, attributes4Notification, (AttributeValues[]) null, Math.Abs(this.ObjectID), this.ObjectType, this.ID, this.Caption, lifecycleStep.LevelID, this.VersionID));
          this._LCStepObject = (IDBLifecycleStep) null;
          (this.EventHelper as EventLogHelper).OnAfterNextLCStep((IDBObject) this, lifecycleStep, (IUserSession) this.UserSession);
          if (lifecycleStep.LevelID == this.UserSession.IdentHelper.DeletedID && this.ObjectTypeClass.LifetimeReserve == 0)
            this.Purge(0L);
          this.AfterSetLCStep();
          this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
          this.UserSession.Commit();
        }
        finally
        {
          if (flag)
            lcListFromSession.Value = (List<long>) null;
        }
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
        throw;
      }
    }
  }

  protected virtual void AfterSetLCStep()
  {
  }

  public virtual void PrepareNextLCStep(DBSessionable nextLCStep) => this._NextLCStep = nextLCStep;

  private static UserSessionPluginsData<List<long>> GetChangeLCListFromSession(
    UserSession UserSession)
  {
    return new UserSessionPluginsData<List<long>>((IUserSession) UserSession, "ChangeLC_List");
  }

  public void ClearObjectAccessCache()
  {
    ((IDBSecurityCache) this.UserSession.DBSecurity).ClearCategoryCache(20, Math.Abs(this.ObjectID), this.AccessActions);
  }

  private Dictionary<int, string> GetSoftInstantiationApp()
  {
    DataTable applicabilitiesList = this.UserSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, this.ObjectType);
    Dictionary<int, string> instantiationApp = new Dictionary<int, string>();
    for (int index = 0; index < applicabilitiesList.Rows.Count; ++index)
    {
      if (Convert.ToInt32(applicabilitiesList.Rows[index]["F_MIN_LINKS"]) >= 0 && (Convert.ToInt32(applicabilitiesList.Rows[index]["F_OPTIONS"]) & 64 /*0x40*/) == 64 /*0x40*/)
      {
        int int32 = Convert.ToInt32(applicabilitiesList.Rows[index]["F_RELATION_TYPE"]);
        string str;
        if (instantiationApp.TryGetValue(int32, out str))
          instantiationApp[int32] = $"{str},{applicabilitiesList.Rows[index]["F_OBJECT_TYPE"].ToString()}";
        else
          instantiationApp.Add(int32, applicabilitiesList.Rows[index]["F_OBJECT_TYPE"].ToString());
      }
    }
    return instantiationApp;
  }

  private void CheckDeleted(string funcName)
  {
    if (this.Deleted)
      throw new KernelExceptionID(414, (object) funcName);
  }

  private void ValidateObjectTypeLinksDown(int objTypeID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT R.F_RELATION_TYPE, O.F_OBJECT_TYPE FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE (R.F_PROJ_ID = :objID) AND (O.F_ID = R.F_PART_ID) GROUP BY R.F_RELATION_TYPE, O.F_OBJECT_TYPE", dataManager.Parameter("objID", (object) this.ObjectID));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (!MetaDataHelper.HasApplicability(objTypeID, Convert.ToInt32(dataTable.Rows[index][1]), Convert.ToInt32(dataTable.Rows[index][0])))
        throw new KernelExceptionID(sc_13302.ssp_appserver_13322(109746737), (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObjectType(objTypeID).ObjectInstanceName, (object) this.UserSession.GetObjectType(Convert.ToInt32(dataTable.Rows[index][1])).ObjectTypeName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    }
  }

  private void ValidateObjectTypeLinksUp(int objTypeID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT R.F_RELATION_TYPE, O.F_OBJECT_TYPE FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE (R.F_PART_ID = :partID) AND (O.F_OBJECT_ID = R.F_PROJ_ID) GROUP BY R.F_RELATION_TYPE, O.F_OBJECT_TYPE", dataManager.Parameter("partID", (object) this.ID));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (!MetaDataHelper.HasApplicability(Convert.ToInt32(dataTable.Rows[index][1]), objTypeID, Convert.ToInt32(dataTable.Rows[index][0])))
        throw new KernelExceptionID(sc_13302.ssp_appserver_13323(1434041631), (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObjectType(objTypeID).ObjectInstanceName, (object) this.UserSession.GetObjectType(Convert.ToInt32(dataTable.Rows[index][1])).ObjectTypeName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    }
  }

  public virtual int ObjectType
  {
    get => Convert.ToInt32(this.paramsTable[86]);
    set
    {
      if (this.ObjectType == value)
        return;
      IDBObjectType objectType1 = this.UserSession.GetObjectType(value);
      long EventID = this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_408"), (object) this.ObjectTypeClass.ObjectInstanceName, (object) objectType1.ObjectInstanceName));
      this.CheckEditMode(false, true, false);
      this.CheckChangeEnable("F_OBJECT_TYPE");
      if (!this.IsCreationMode && this.CheckoutBy != 0L)
        throw new KernelExceptionID(sc_13302.ssp_appserver_13324(126760928), (object) this.NameInMessages);
      int objectType2 = this.ObjectType;
      IDbManager dataManager = this.UserSession.DataManager;
      this.UserSession.StartTransaction();
      try
      {
        if (objectType1.Versionable == ObjectVersionModes.Abstract)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13325(1262367431), (object) objectType1.ObjectTypeName);
        if (objectType1.Versionable == ObjectVersionModes.SingleVersion && this.VersionID > 0)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13326(2134521788), (object) objectType1.ObjectTypeName);
        this.ValidateObjectTypeLinksDown(value);
        this.ValidateObjectTypeLinksUp(value);
        (this.EventHelper as EventLogHelper).OnBeforeChangeObjectType((IDBObject) this, value, (IUserSession) this.UserSession);
        int firstStep = this.UserSession.GetLifecycleStepCollection(value).GetFirstStep();
        int levelId = this.UserSession.GetLifecycleStep(firstStep).LevelID;
        DataTable dataTable = objectType1.Attributes.Select("");
        if (!objectType1.AnyAttributes)
        {
          for (int AttrIndex = this.Attributes.Count - 1; AttrIndex >= 0; --AttrIndex)
          {
            bool flag = false;
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              if (Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"]) == this.Attributes[AttrIndex].AttributeID)
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              (this.Attributes[AttrIndex] as DBAttribute).Purge(false);
          }
        }
        if (objectType1.IsLocalType || this.ObjectTypeClass.IsLocalType)
        {
          string attributesTableName1 = this.UserSession.DBCache.GetAttributesTableName(value);
          string attributesTableName2 = this.UserSession.DBCache.GetAttributesTableName(this.ObjectType);
          dataManager.ExecuteNonQuery(string.Format("INSERT INTO {0} (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_STRING_VALUE, F_INTEGER_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) SELECT F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_STRING_VALUE, F_INTEGER_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE FROM {1} WHERE {1}.F_OBJECT_ID = :objID", (object) attributesTableName1, (object) attributesTableName2), dataManager.Parameter("objID", (object) this.ObjectID));
          dataManager.ExecuteNonQuery($"DELETE FROM {attributesTableName2} WHERE F_OBJECT_ID = :objID", dataManager.Parameter("objID", (object) this.ObjectID));
        }
        if (!this.IsCreationMode)
          this.DeleteFromView(this.ObjectType);
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_OBJECTS SET F_OBJECT_TYPE = {value}, F_LEVEL_ID = {levelId}, F_LC_STEP = {firstStep} WHERE F_OBJECT_ID = {this.ObjectID}");
        this.paramsTable[86] = (object) value;
        this.paramsTable[72] = (object) levelId;
        this.paramsTable[136] = (object) firstStep;
        if (!this.IsCreationMode)
          this.InsertIntoView(false, "F_OBJECT_VER_TYPE", this.CheckoutBy);
        this._Attributes = (IDBAttributeCollection) null;
        this._ObjectTypeClass = (IDBObjectType) null;
        this._LCStepObject = (IDBLifecycleStep) null;
        (this.Attributes as DBAttributeCollection).ObjectType = value;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if ((Convert.ToInt32(row["F_REQUIRED"]) == Convert.ToInt32((object) RequiredModes.Auto) || Convert.ToInt32(row["F_REQUIRED"]) == Convert.ToInt32((object) RequiredModes.AutoRequired)) && (Convert.ToInt32(row["F_LEVEL_ID"]) == 0 || Convert.ToInt32(row["F_LEVEL_ID"]) == this.LevelID))
            (this.Attributes as DBAttributeCollection).AddAttribute(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), false, false);
        }
        if (this.ObjectID > 0L)
          this.ValidateCheckinRules();
        this.RebuildComputedAttrs();
        if (objectType1.CaptionAttribute > 0)
        {
          IDBAttribute attributeById = this.GetAttributeByID(objectType1.CaptionAttribute);
          if (attributeById != null && attributeById.AsString != this.Caption)
            this.SetCaption(attributeById.AsString);
        }
        for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
          (this.Attributes[AttrIndex] as DBAttribute).InsertIntoView(1);
        (this.EventHelper as EventLogHelper).OnAfterChangeObjectType((IDBObject) this, objectType2, (IUserSession) this.UserSession);
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
        this.UserSession.Commit();
        this.UserSession.DBCache.UpdateObjectInfo(new QuickObjectInfo(this.ObjectID, this.Caption, value, this.ObjectGUID, this.ID));
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, string.Format(LocalizationHolder.rm.GetString("Kernel_409"), (object) this.ObjectTypeClass.ObjectInstanceName, (object) objectType1.ObjectInstanceName, (object) ex.Message));
        this.Deleted = true;
        throw;
      }
    }
  }

  private void RecalcAttributes(int attributeID)
  {
    string str = !this.ObjectTypeClass.AnyAttributes ? " = " + this.ObjectType.ToString() : $" IN (-1, {this.ObjectType.ToString()})";
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS").Select($"F_ATTRIBUTE_ID = {attributeID} AND F_OBJECT_TYPE {str} AND F_RELATION_TYPE = -1 AND F_MODE_ID = {Consts.Attribute4Formula}"))
    {
      if (this.GetAttributeByID(Convert.ToInt32(dataRow["F_FORMULA_ID"])) is DBAttribute attributeById)
        attributeById.Compute(false);
    }
  }

  public override long CreatorID => Convert.ToInt64(this.paramsTable[181]);

  public virtual long OwnerID
  {
    get => Convert.ToInt64(this.paramsTable[150]);
    set
    {
      if (this.OwnerID == value)
        return;
      try
      {
        this.CheckAccess(ActionType.TakeOwnership, this.GetDefaultAccess(ActionType.TakeOwnership));
      }
      catch
      {
        this.AddEvent(this.ObjectID, ActionType.TakeOwnership, EventlogRecordType.AccessDenied);
        throw;
      }
      this.CheckChangeEnable("F_OWNER_ID");
      string caption = value.ToString();
      this.UserSession.StartTransaction();
      try
      {
        IDBObject dbObject = this.UserSession.GetObject(value);
        caption = dbObject.Caption;
        if (this.UserSession.DBCache.IsInhertitedFrom(dbObject.ObjectType, this.UserSession.IdentHelper.UsersTypeID) || this.UserSession.DBCache.IsInhertitedFrom(dbObject.ObjectType, this.UserSession.IdentHelper.GroupsTypeID))
        {
          this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_OWNER_ID = :ownID WHERE F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("ownID", (object) value), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID));
          this.UpdateViewValue("F_OWNER_ID", (object) value);
          this.paramsTable[150] = (object) value;
          this.AddEvent(this.ObjectID, ActionType.TakeOwnership, EventlogRecordType.AccessGranted);
          this.UserSession.Commit();
        }
        else
          throw new KernelExceptionID(sc_13302.ssp_appserver_13327(1553525248), (object) dbObject.NameInMessages, (object) dbObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.AddEvent(this.ObjectID, ActionType.TakeOwnership, EventlogRecordType.Error, $"Ошибка изменения владельца объекта на {caption}: {ex.Message}");
        throw;
      }
    }
  }

  internal void SetCreatorID(long newCreatorID)
  {
    if (this.CreatorID == newCreatorID)
      return;
    try
    {
      this.CheckAccess(ActionType.TakeOwnership, this.GetDefaultAccess(ActionType.TakeOwnership));
    }
    catch
    {
      this.AddEvent(this.ObjectID, ActionType.TakeOwnership, EventlogRecordType.AccessDenied);
      throw;
    }
    this.CheckChangeEnable("F_CREATOR_ID");
    string caption = newCreatorID.ToString();
    this.UserSession.StartTransaction();
    try
    {
      IDBObject dbObject = this.UserSession.GetObject(newCreatorID);
      caption = dbObject.Caption;
      if (this.UserSession.DBCache.IsInhertitedFrom(dbObject.ObjectType, this.UserSession.IdentHelper.UsersTypeID))
      {
        this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_CREATOR_ID = :crtID WHERE F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("crtID", (object) newCreatorID), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID));
        this.UpdateViewValue("F_CREATOR_ID", (object) newCreatorID);
        this.paramsTable[181] = (object) newCreatorID;
        this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessGranted, "Изменение атрибута 'Создатель объекта' на " + caption);
        this.UserSession.Commit();
      }
      else
        throw new KernelExceptionID(sc_13302.ssp_appserver_13328(1439762349), (object) dbObject.NameInMessages, (object) dbObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.Error, $"Ошибка изменения атрибута 'Создатель объекта' на {caption}: {ex.Message}");
      throw;
    }
  }

  public DateTime ModifyDate
  {
    get
    {
      return (this.GetAttributeByID(this.UserSession.IdentHelper.ModifyContentDateID) ?? throw new KernelException($"У объекта '{this.Caption}' (идентификатор версии '{this.ObjectID}') отсутствует атрибут '{this.UserSession.GetAttributeType(this.UserSession.IdentHelper.ModifyContentDateID).Name}'.").WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID))).AsDateTime;
    }
  }

  public int AccessLevel
  {
    get => Convert.ToInt32(this.paramsTable[179]);
    set => this.SetAccessLevel(value, (List<long>) null);
  }

  internal void DoSetAccessLevel(int value)
  {
    this.UserSession.StartTransaction();
    try
    {
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_ACCESS = :accessID WHERE F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("accessID", (object) value), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID));
      this.UpdateViewValue("F_ACCESS", (object) value);
      this.paramsTable[179] = (object) value;
      this.AddEvent(this.ObjectID, ActionType.ChangeAccessLevel, EventlogRecordType.AccessGranted);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  private void ValidateAccessLevel(int value, List<long> excludeList)
  {
    if (value == this.AccessLevel)
      return;
    try
    {
      if (!this.UserSession.DBCache.AccessLevelExists(value))
        throw new KernelExceptionID(428, (object) this.NameInMessages, (object) this.ObjectID, (object) value).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      if (value > 0 && (this.ObjectTypeClass.Options & ObjectTypeOptions.MandateAccess) == ObjectTypeOptions.None)
        throw new KernelExceptionID(429, (object) this.NameInMessages, (object) this.UserSession.DBCache.GetAccessCaption(value), (object) this.ObjectTypeClass.ObjectTypeName, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      if (value > this.UserSession.SecurityLevel && !this.UserSession.IsSystemSession)
        throw new KernelExceptionID(sc_13302.ssp_appserver_13331(229017009), (object) this.UserSession.DBCache.GetAccessCaption(this.UserSession.SecurityLevel), (object) this.UserSession.DBCache.GetAccessCaption(value));
      this.CheckAccess(ActionType.ChangeAccessLevel, this.GetDefaultAccess(ActionType.SetAccess), true);
      if (ServerConsts.EnableSecret2Public)
        return;
      if (value > this.AccessLevel)
        this.CheckAccessLevelUP(value, excludeList);
      else
        this.CheckAccessLevelDOWN(value, excludeList);
    }
    catch (Exception ex)
    {
      this.AddEvent(this.ObjectID, ActionType.ChangeAccessLevel, !(ex is AccessDeniedException) ? EventlogRecordType.Error : EventlogRecordType.AccessDenied, ex.Message);
      throw;
    }
  }

  internal virtual void SetAccessLevel(int value, List<long> excludeList)
  {
    if (value == this.AccessLevel)
      return;
    this.ValidateAccessLevel(value, excludeList);
    try
    {
      this.DoSetAccessLevel(value);
    }
    catch (Exception ex)
    {
      this.AddEvent(this.ObjectID, ActionType.ChangeAccessLevel, !(ex is AccessDeniedException) ? EventlogRecordType.Error : EventlogRecordType.AccessDenied, ex.Message);
      throw;
    }
  }

  internal void CheckAccessLevelDOWN(int value, List<long> excludeList)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable tbl = dataManager.ExecuteDataTable("SELECT O.F_OBJECT_ID FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE (F_PROJ_ID = :projID) AND (O.F_ID = R.F_PART_ID) AND (O.F_ACCESS > :alID)", dataManager.Parameter("projID", (object) this.ObjectID), dataManager.Parameter("alID", (object) value));
    for (int index = 0; index < tbl.Rows.Count; ++index)
    {
      if (excludeList == null || excludeList.IndexOf(Convert.ToInt64(tbl.Rows[index][0])) < 0)
        throw new KernelExceptionID(430, (object) this.NameInMessages, (object) this.UserSession.DBCache.GetAccessCaption(value), (object) this.GetObjectsNamesInError(tbl), (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    }
  }

  private string GetObjectsNamesInError(DataTable tbl)
  {
    int capacity = tbl.Rows.Count;
    bool flag = false;
    if (capacity > 10)
    {
      capacity = 10;
      flag = true;
    }
    List<long> longList = new List<long>(capacity);
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      for (int index = 0; index < capacity; ++index)
      {
        long int64 = Convert.ToInt64(tbl.Rows[index][0]);
        if (!longList.Contains(Math.Abs(int64)))
        {
          IDBObject dbObject = this.UserSession.GetObject(int64, false);
          if (dbObject != null)
          {
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("{0} [{1}]", (object) dbObject.NameInMessages, (object) dbObject.VersionID);
          }
          longList.Add(Math.Abs(int64));
        }
      }
      if (flag)
      {
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("...");
      }
      return stringBuilder.ToString();
    }
  }

  internal void CheckAccessLevelUP(int value, List<long> excludeList)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable tbl = dataManager.ExecuteDataTable("SELECT R.F_PROJ_ID FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE (F_PART_ID = :partID) AND (O.F_OBJECT_ID = R.F_PROJ_ID) AND (O.F_ACCESS < :alID)", dataManager.Parameter("partID", (object) this.ID), dataManager.Parameter("alID", (object) value));
    for (int index = 0; index < tbl.Rows.Count; ++index)
    {
      if (excludeList == null || excludeList.IndexOf(Convert.ToInt64(tbl.Rows[index][0])) < 0)
        throw new KernelExceptionID(431, (object) this.NameInMessages, (object) this.UserSession.DBCache.GetAccessCaption(value), (object) this.GetObjectsNamesInError(tbl), (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    }
  }

  private int GetAutoAccessLevelUpID()
  {
    if (!ServerConsts.AutomaticAccessLevelUp || (this.ObjectTypeClass.Options & ObjectTypeOptions.MandateAccess) != ObjectTypeOptions.MandateAccess || this.AccessLevel >= this.UserSession.SecurityLevel || this.UserSession.IsSystemSession)
      return 0;
    this.ValidateAccessLevel(this.UserSession.SecurityLevel, new List<long>());
    return this.UserSession.SecurityLevel;
  }

  public virtual int ObjectVerType
  {
    get => Convert.ToInt32(this.paramsTable[151]);
    set
    {
    }
  }

  internal void SetObjectVerType(ObjectRecordKind verTypeID)
  {
    if (verTypeID == (ObjectRecordKind) this.ObjectVerType)
      return;
    if (verTypeID == ObjectRecordKind.Import && this.ObjectID < 0L)
      throw new KernelExceptionID(360, (object) this.NameInMessages, (object) this.ObjectID);
    this.UserSession.StartTransaction();
    try
    {
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_OBJECT_VER_TYPE = :vertypeID WHERE F_OBJECT_ID = :objID OR F_OBJECT_ID = :m_objID", this.UserSession.DataManager.Parameter("vertypeID", (object) (int) verTypeID), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("m_objID", (object) -this.ObjectID));
      this.UpdateViewValue("F_OBJECT_VER_TYPE", (object) (int) verTypeID);
      this.paramsTable[151] = (object) (int) verTypeID;
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  protected Guid VersionGUID
  {
    get
    {
      this.CheckDeleted("VersionGUID.get");
      if (this.guidTable.Rows.Count > 0)
        return new Guid(this.guidTable.Rows[0]["F_GUID"].ToString());
      this.ObjectGuidNotFound();
      return Guid.Empty;
    }
    set
    {
      if (!(this.VersionGUID != value))
        return;
      try
      {
        this.CheckEditMode(true, true, false);
      }
      catch
      {
        this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_410") + value.ToString());
        throw;
      }
      long EventID = this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessGranted, LocalizationHolder.rm.GetString("Kernel_411") + value.ToString());
      this.UserSession.StartTransaction();
      try
      {
        IDbManager dataManager = this.UserSession.DataManager;
        IDbDataParameter dbDataParameter = dataManager.Parameter("objID", (object) this.ObjectID);
        if (this.ObjectID < 0L && !this.IsCreationMode)
          dbDataParameter.Value = (object) Math.Abs(this.ObjectID);
        dataManager.ExecuteNonQuery("UPDATE IMS_GUID SET F_GUID = :guid WHERE F_OBJECT_ID = :objID", dataManager.Parameter("guid", (object) value), dbDataParameter);
        this.UpdateViewValue("F_GUID", (object) value);
        this.UserSession.Commit();
        this.guidTable.Rows[0]["F_GUID"] = (object) value;
        this.guidTable.AcceptChanges();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString("Kernel_412") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  private void ObjectGuidNotFound()
  {
    if (this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID IN (:objID, :neg_objID)", this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("neg_objID", (object) -this.ObjectID)).Rows.Count == 0)
      throw new ObjectNotFoundException(this.ObjectID);
    throw new KernelException($"Для объекта {this.ObjectTypeClass.ObjectInstanceName} {(this._Caption == null || !(this._Caption.Trim() != string.Empty) ? string.Empty : this._Caption)} (Ид. версии {this.ObjectID}, ид. владельца {this.OwnerID}) в базе данный не найден глобальный идентификатор версии.").WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
  }

  Guid IDBGuid.GUID => this.VersionGUID;

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.VersionGUID);

  public string SubjectAreas => (this.ObjectTypeClass as IDBSubjectArea).SubjectAreas;

  private void DeleteFromContext(bool clearModifiationID)
  {
    bool flag = false;
    long objectId = this.ObjectID;
    int objectType = this.ObjectType;
    if (DBObject.EditingContextsServerService != null && (this.ObjectID > 0L || this.IsCreationMode))
      flag = true;
    if (!flag || DBObject.EditingContextsServerService == null)
      return;
    DBObject.EditingContextsServerService.DeleteFromContext((object) this.UserSession, this.UserSession.EditingContextID, objectId, true, clearModifiationID);
    DBObject.EditingContextsServerService.DeleteFromIMS_VERSIONS_CONTEXT((object) this.UserSession, objectId, true);
  }

  protected virtual void DoPurge(long DeleteMode)
  {
    if (this.ObjectTypeClass.Versionable != ObjectVersionModes.MultiVersion)
      return;
    this.DeleteFromContext(false);
  }

  protected virtual void DoPurge_DeleteUpLinks(long DeleteMode)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) this._ObjectID);
    object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id AND F_OBJECT_ID <> :objID AND F_OBJECT_VER_TYPE <> :blankID", dataManager.Parameter("id", (object) this.ID), dbDataParameter1, dataManager.Parameter("blankID", (object) -1));
    if (obj != null && obj != DBNull.Value)
      return;
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("id", (object) this.ID);
    dataManager.ExecuteNonQuery("DELETE FROM IMS_GUID_RESOLVE WHERE F_ID = :id AND F_CATEGORY_TYPE = " + 2.ToString(), dbDataParameter2);
    dataManager.ExecuteNonQuery("DELETE FROM IMS_SELECTIONS WHERE F_ID = :id", dbDataParameter2);
    dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTR_HISTORY WHERE F_ID = :id", dbDataParameter2);
    if (this.relationsDeleted)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PART_ID = :id", dbDataParameter2).Rows)
    {
      IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row[0]), false);
      if (relation != null)
      {
        (relation.Attributes as DBAttributeCollection).Purge();
        relation.Delete((long) (Consts.DontCheckApplicabilityModes | Consts.PurgeMode));
      }
    }
  }

  protected virtual void DoPurge_DeleteDownLinks(long DeleteMode)
  {
    if (this.relationsDeleted)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("objID", (object) this._ObjectID);
    string commandText = !(dataManager.DataProvider.Name == "Linter") ? $"SELECT R.F_PRJLINK_ID, (SELECT {dataManager.DataProvider.GetTopString(1)}O.F_OBJECT_TYPE FROM IMS_OBJECTS O WHERE (O.F_ID = R.F_PART_ID){dataManager.DataProvider.FetchRowsSQL(1)}) F_OBJECT_TYPE FROM IMS_RELATIONS R WHERE R.F_PROJ_ID = :objID " : "SELECT R.F_PRJLINK_ID, (SELECT MIN(O.F_OBJECT_TYPE) FROM IMS_OBJECTS O WHERE (O.F_ID = R.F_PART_ID)) F_OBJECT_TYPE FROM IMS_RELATIONS R WHERE R.F_PROJ_ID = :objID ";
    DataTable dataTable = dataManager.ExecuteDataTable(commandText, dbDataParameter);
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row[0]), false);
      if (relation != null)
      {
        (relation as DBRelation).SenderObject = (IDBObject) this;
        (relation as DBRelation)._ProjObject = (IDBObject) this;
        if (relation.ProjID == this._ObjectID)
        {
          IDBRelationsApplicability applicability = row[1] == null || row[1] == DBNull.Value ? (IDBRelationsApplicability) null : applicabilityCollection.GetApplicability(relation.RelationType, Convert.ToInt32(row[1]), this.ObjectType);
          if (applicability != null)
          {
            switch (applicability.RelationConstraintMode)
            {
              case RelationConstraintModes.ParentConstrained:
              case RelationConstraintModes.ParentChildConstrained:
                if (!this.IsCreationMode && (DeleteMode & 2048L /*0x0800*/) == 0L)
                  throw new KernelExceptionID(sc_13302.ssp_appserver_13334(428854862), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
                break;
              case RelationConstraintModes.ChildDelete:
              case RelationConstraintModes.ChildForcedDelete:
                if (!this.IsCreationMode)
                {
                  this.DoPurge_DeleteDownLinks_DeleteObj(DeleteMode, relation, Convert.ToInt32(row[1]), applicability.RelationConstraintMode == RelationConstraintModes.ChildForcedDelete);
                  break;
                }
                break;
            }
          }
        }
        (relation.Attributes as DBAttributeCollection).Purge();
        relation.Delete((long) ((int) DeleteMode | Consts.DontCheckApplicabilityModes | Consts.PurgeMode));
      }
    }
  }

  protected virtual void DoPurge_DeleteDownLinks_DeleteObj(
    long DeleteMode,
    IDBRelation ChildRelation,
    int ChildObjType,
    bool forcedDelete,
    string ExtraSqlCond = "")
  {
    bool flag = true;
    IDbManager dataManager = this.UserSession.DataManager;
    if ((this.ObjectID < 0L || this.ObjectTypeClass.Versionable == ObjectVersionModes.MultiVersion) && !forcedDelete)
    {
      object obj = dataManager.ExecuteScalar("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PART_ID = :partID AND F_PROJ_ID <> :projID " + ExtraSqlCond, dataManager.Parameter("partID", (object) ChildRelation.PartID), dataManager.Parameter("projID", (object) this.ObjectID));
      flag = obj == null || obj == DBNull.Value;
    }
    if (!flag)
      return;
    if (MetaDataHelper.GetAttribute4RelationType(ChildRelation.RelationType, this.UserSession.IdentHelper.CompositionVersionID) != null)
    {
      IDBAttribute attributeById = ChildRelation.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID);
      if (attributeById != null && !attributeById.IsNull && attributeById.AsInteger > 0L)
      {
        DeleteChildObject(attributeById.AsInteger);
        return;
      }
    }
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :parID", dataManager.Parameter("parID", (object) ChildRelation.PartID));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      DeleteChildObject(Convert.ToInt64(dataTable.Rows[index][0]));

    void DeleteChildObject(long objectID)
    {
      IDBObject dbObject = this.UserSession.GetObject(objectID, false);
      if (dbObject == null)
        return;
      if (dbObject.ObjectID > 0L && dbObject.CheckoutBy != 0L && (DeleteMode & 16L /*0x10*/) == 16L /*0x10*/)
      {
        this.UserSession.GetObject(-dbObject.ObjectID, false)?.CancelChanges(true);
        dbObject = this.UserSession.GetObject(dbObject.ObjectID, false);
      }
      if ((DeleteMode & (long) Consts.PurgeMode) == (long) Consts.PurgeMode)
        (dbObject as DBObject).Purge(DeleteMode);
      else
        dbObject.Delete((long) (uint) Consts.RelationConstraintMode | DeleteMode);
    }
  }

  internal int Purge(long DeleteMode)
  {
    EventlogRecordType auditType = EventlogRecordType.AccessDenied;
    this.UserSession.StartTransaction();
    try
    {
      if (this.ObjectID >= 0L)
        this.CheckAccess(ActionType.Purge, this.GetDefaultAccess(ActionType.Purge));
      auditType = EventlogRecordType.Error;
      if (this._Caption == string.Empty)
        this._Caption = string.Format(LocalizationHolder.rm.GetString("Kernel_401"), (object) this.ObjectTypeName, (object) this.ObjectID);
      this.DoPurge(DeleteMode);
      (this.EventHelper as EventLogHelper).OnBeforePurgeObjectEvent((IDBObject) this);
      (this.EventHelper as EventLogHelper).OnBeforePurgeObjectExtendedEvent((IDBObject) this, new ObjectDeleteEventArgs(DeleteMode, (IUserSession) this.UserSession));
      IDbManager dataManager = this.UserSession.DataManager;
      IDbDataParameter dbDataParameter = dataManager.Parameter("objID", (object) this._ObjectID);
      if (this.HasIndexedAttributes())
      {
        dataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_OBJECT_ID = :objID", dbDataParameter);
        dataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_RESULT WHERE F_OBJECT_ID = :objID", dbDataParameter);
        this.UserSession.DeleteFromIndexQueue(this._ObjectID);
      }
      (this.Attributes as DBAttributeCollection).Purge();
      this.DoPurge_DeleteUpLinks(DeleteMode);
      this.DoPurge_DeleteDownLinks(DeleteMode);
      this.DeleteFromView(this.ObjectType);
      dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECT_LINKS WHERE F_OBJECT_ID = :objID", dbDataParameter);
      dataManager.ExecuteNonQuery("DELETE FROM IMS_ID_LINKS WHERE F_OBJECT_ID = :objID", dbDataParameter);
      if (MetaDataHelper.GetAttribute4ObjectType(this.ObjectType, this.UserSession.IdentHelper.FileAttributeID) != null)
        dataManager.ExecuteNonQuery("DELETE FROM IMS_FILENAMES WHERE F_KEY = :objID", dbDataParameter);
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PROJ_ID = :objID", dbDataParameter);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        this.UserSession.GetRelation(Convert.ToInt64(dataTable.Rows[index][0]), false)?.Delete((long) Consts.PurgeMode);
      dataManager.ExecuteSpNonQuery("IMS_DELETE_OBJECT", dataManager.Parameter("inOBJECT_ID", (object) this._ObjectID));
      this.UserSession.ClearObjectSmartCache();
      long objectId = this._ObjectID;
      Guid versionGuid = this._GuidTable == null || this._GuidTable.Rows.Count <= 0 ? Guid.Empty : this.VersionGUID;
      if (this._ObjectID < 0L)
      {
        dbDataParameter.Value = (object) -this._ObjectID;
        dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_CHKOUT_BY = 0 WHERE F_OBJECT_ID = :objID", dbDataParameter);
        this._ObjectID = -this._ObjectID;
        object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dbDataParameter);
        if (obj != null && obj != DBNull.Value)
          this.paramsTable[86] = obj;
        this.UpdateViewValue("F_CHKOUT_BY", (object) 0);
      }
      else
        this.AddEvent(this.ObjectID, ActionType.Purge, EventlogRecordType.AccessGranted);
      this.UserSession.Commit();
      this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, objectId, ActionType.Purge, this.ObjectType));
      this.UserSession.DBCache.DeleteObjectInfo(objectId, versionGuid);
      this.Deleted = true;
      this.RaiseAfterPurgeObjectEvent(objectId);
      return 1;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      if (this.ObjectID > 0L)
        this.AddEvent(this.ObjectID, ActionType.Purge, auditType, ex.Message);
      throw;
    }
  }

  private void RaiseAfterPurgeObjectEvent(long realObjectId)
  {
    long objectId = this._ObjectID;
    try
    {
      this._ObjectID = realObjectId;
      (this.EventHelper as EventLogHelper).OnAfterPurgeObjectEvent((IDBObject) this);
    }
    finally
    {
      this._ObjectID = objectId;
    }
  }

  public override bool Deleted
  {
    get => base.Deleted;
    protected set => this._Deleted = value;
  }

  public virtual int Delete(long DeleteMode)
  {
    this.CheckDeleted(nameof (Delete));
    bool flag = false;
    long objectId = this.ObjectID;
    int objectType = this.ObjectType;
    if (DBObject.EditingContextsServerService != null && (this.ObjectID > 0L || this.IsCreationMode))
      flag = true;
    if (this.IsCreationMode)
    {
      if (flag)
      {
        this.UserSession.DBObjectsCacheRemoveVersion(objectId);
        if (DBObject.EditingContextsServerService != null && MetaDataHelper.IsObjectTypeEditingContext(objectType))
        {
          DBObject.EditingContextsServerService.RemoveUsersContext(objectId);
          DBObject.EditingContextsServerService.ResetCache();
        }
      }
      return this.Purge(DeleteMode);
    }
    if (!this.UserSession.CanChangeObject(2, (object) this.ObjectID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_932"), (object) this.NameInMessages, (object) this.ObjectID)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    try
    {
      if (this.ObjectID < 0L)
      {
        if (this.CheckoutBy != 0L && this.CheckoutBy != this.UserSession.UserID && (!this.UserSession.DBSecurity.IsAdminMode || (DeleteMode & 16L /*0x10*/) == 0L))
          throw new KernelExceptionID(133, (object) this.Caption, (object) this.ObjectID, (object) this.UserSession.GetObject(this.CheckoutBy).Caption).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
        long EventID = this.AddEvent(this.ObjectID, ActionType.Cancel, EventlogRecordType.AccessDenied);
        this.UserSession.StartTransaction();
        try
        {
          this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
          (this.EventHelper as EventLogHelper).OnBeforeUndoCheckout((IDBObject) this, (IUserSession) this.UserSession);
          int num = this.Purge(DeleteMode);
          (this.EventHelper as EventLogHelper).OnAfterUndoCheckout((IDBObject) this, (IUserSession) this.UserSession);
          (this.EventHelper as EventLogHelper).OnAfterUndoCheckoutEx((IDBObject) this, new ObjectDeleteEventArgs(DeleteMode, (IUserSession) this.UserSession));
          this.UserSession.Commit();
          return num;
        }
        catch (Exception ex)
        {
          this.UserSession.Rollback();
          this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
          throw;
        }
      }
      else
      {
        if (this.LevelID == this.UserSession.IdentHelper.DeletedID)
          return this.Purge(DeleteMode);
        int deleteStepId = this.LCStepObject.GetDeleteStepID();
        this.LCStep = deleteStepId >= 0 ? deleteStepId : throw new KernelExceptionID(sc_13302.ssp_appserver_13336(1474525148), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
        this.Deleted = true;
        return 0;
      }
    }
    catch
    {
      throw;
    }
    finally
    {
      this.UserSession.DBObjectsCacheRemoveVersion(objectId);
      if (DBObject.EditingContextsServerService != null && MetaDataHelper.IsObjectTypeEditingContext(objectType))
      {
        DBObject.EditingContextsServerService.RemoveUsersContext(objectId);
        DBObject.EditingContextsServerService.ResetCache();
      }
    }
  }

  public virtual int LevelID
  {
    get => Convert.ToInt32(this.paramsTable[72]);
    set => throw new OperationNotApplicableException();
  }

  public string LevelName => MetaDataHelper.GetLCLevelName(this.LevelID);

  public string Litera => this.UserSession.GetLifecycleLevel(this.LevelID).Litera;

  public byte[] LevelIcon => this.UserSession.GetLifecycleLevel(this.LevelID).LevelIcon;

  public override IDBAttributeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
        this._Attributes = (IDBAttributeCollection) new DBObjectAttributeCollection(this.UserSession, this.ObjectID, this.ObjectType, (IDBAttributable) this);
      return this._Attributes;
    }
  }

  public void ClearAttributesCache() => this._Attributes = (IDBAttributeCollection) null;

  public virtual IDBAttribute GetAttributeByID(int attributeID)
  {
    if (this._Attributes == null)
      return (ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService).GetObjectAttribute((IUserSession) this.UserSession, this.ObjectID, attributeID, (IDBAttributable) this);
    return (this._Attributes as DBAttributeCollection).IsAttrListLoaded ? this._Attributes.FindByID(attributeID) : (ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService).GetObjectAttribute((IUserSession) this.UserSession, this.ObjectID, attributeID, (IDBAttributable) this);
  }

  public IDBAttribute GetAttributeByGuid(Guid attributeGuid)
  {
    if (this._Attributes != null && (this._Attributes as DBAttributeCollection).IsAttrListLoaded)
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
    if (this._Attributes != null && (this._Attributes as DBAttributeCollection).IsAttrListLoaded)
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
    return !throwNotFoundException || attributeByGuid != null ? attributeByGuid : throw new AttributeNotFoundException("", attributeGuid.ToString(), this._ObjectID);
  }

  public IDBAttribute GetAttributeByName(string attributeName, bool throwNotFoundException)
  {
    IDBAttribute attributeByName = (IDBAttribute) null;
    try
    {
      attributeByName = this.GetAttributeByName(attributeName);
      if (attributeByName == null & throwNotFoundException)
        throw new AttributeNotFoundException(attributeName, "", this._ObjectID);
    }
    catch (Exception ex)
    {
      if (ex is AttributeNotFoundException)
      {
        if (throwNotFoundException)
          throw;
      }
      else
        throw;
    }
    return attributeByName;
  }

  public long CheckoutBy => Convert.ToInt64(this.paramsTable[152]);

  internal void SetCheckoutBy(long userID)
  {
    if (this.CheckoutBy == userID)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_CHKOUT_BY = :chk WHERE F_OBJECT_ID = :id", dataManager.Parameter("chk", (object) userID), dataManager.Parameter("id", (object) this.ObjectID));
    this.UpdateViewValue("F_CHKOUT_BY", (object) userID);
    this.paramsTable[152] = (object) userID;
  }

  internal bool HasIndexedAttributes()
  {
    bool flag = false;
    for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
    {
      if ((this.Attributes[AttrIndex].AttributeType.Options & AttributeOptions.AddToGlobalIndex) == AttributeOptions.AddToGlobalIndex)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public virtual IDBObject DoCheckout()
  {
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter idPar = dataManager.Parameter("objID", (object) this.ObjectID);
    dataManager.ExecuteNonQuery("INSERT INTO IMS_OBJECTS (F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_ACCESS, F_MODIFY_DATE, F_LEVEL_ID, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_CREATOR_ID) SELECT -F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, :uID1, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_ACCESS, F_MODIFY_DATE, F_LEVEL_ID, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_CREATOR_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dataManager.Parameter("uID1", (object) this.UserSession.UserID), idPar);
    this.InsertIntoView(true, "F_OBJECT_VER_TYPE", this.UserSession.UserID);
    bool flag;
    try
    {
      flag = (this.Attributes as DBAttributeCollection).QuickAddAttributes(-this.ObjectID, true, false, false);
    }
    catch
    {
      flag = false;
    }
    IDBObject dbObject = this.UserSession.GetObject(-this.ObjectID);
    (dbObject as DBObject)._MustCheckValidatingRule = false;
    if (!flag)
      (dbObject.Attributes as DBAttributeCollection).Assign(this.Attributes, Consts.CheckOutMode);
    if (this.HasIndexedAttributes())
      this.UserSession.CheckOutToIndexQueue(this.ObjectID);
    dataManager.ExecuteNonQuery($"UPDATE IMS_GUID SET F_WORK_CAPTION = CAPTION, F_CHECKOUT_DATE = {dataManager.DataProvider.Now} WHERE F_OBJECT_ID = :objID", idPar);
    this.CopyRelations(idPar, "objID");
    this.SetCheckoutBy(this.UserSession.UserID);
    (dbObject as DBObject).CopySearchWorkFiles();
    this.UserSession.AddDelayedNotification((DelayedNotification) new ObjectDelayedNotification(this.UserSession.RealUserID, ActionType.CheckOut, (AttributeValues[]) null, this.GetAttributes4Notification((DBAttribute) null), Math.Abs(this.ObjectID), this.ObjectType, this.ID, this.Caption, this.LevelID, this.VersionID));
    this.Deleted = true;
    this.UserSession.ClearObjectSmartCache();
    (dbObject as DBObject)._MustCheckValidatingRule = true;
    return dbObject;
  }

  internal void CopySearchWorkFiles()
  {
    if (!ServerConsts.UseSearchWorkcopyFiles || !this.UserSession.DBCache.IsDocument(this.ObjectType))
      return;
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cadd98bc-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || (attributeByGuid as IDBAttributeEx).IsNull)
      return;
    IDBAttribute attributeById = this.GetAttributeByID(this.UserSession.IdentHelper.FileAttributeID);
    if (attributeById == null)
      return;
    attributeById.Assign(attributeByGuid);
    attributeByGuid.Delete((long) Consts.PurgeMode);
  }

  private void CopyRelations(IDbDataParameter idPar, string idParameterName)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable1 = dataManager.ExecuteDataTable($"SELECT DISTINCT F_RELATION_TYPE FROM IMS_RELATIONS WHERE F_PROJ_ID = :{idParameterName}", idPar);
    for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
    {
      DBRelationType relationType = this.UserSession.GetRelationType(Convert.ToInt32(dataTable1.Rows[index1][0]), true) as DBRelationType;
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, relationType.RelationType);
      if (relationType != null)
      {
        if (relationType.CanQuickRelationsCopy())
        {
          try
          {
            dataManager.ExecuteNonQuery($"INSERT INTO IMS_RELATIONS (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR) SELECT -F_PRJLINK_ID, -F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR FROM IMS_RELATIONS WHERE (F_PROJ_ID = :{idParameterName}) AND (F_RELATION_TYPE = :rType)", idPar, dataManager.Parameter("rType", (object) relationType.RelationType));
          }
          catch (Exception ex)
          {
            if (ex.Message.IndexOf("PRIMARY") >= 0)
            {
              DataTable dataTable2 = dataManager.ExecuteDataTable($"SELECT -F_PRJLINK_ID, F_RELATION_TYPE FROM IMS_RELATIONS R1 WHERE (R1.F_PROJ_ID = :{idParameterName}) AND EXISTS(SELECT R2.F_PRJLINK_ID FROM IMS_RELATIONS R2 WHERE R2.F_PRJLINK_ID = -R1.F_PRJLINK_ID)", idPar);
              for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
              {
                IDbDataParameter dbDataParameter = dataManager.Parameter("linkID", (object) Convert.ToInt64(dataTable2.Rows[index2][0]));
                dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATION_ATTRS WHERE F_PRJLINK_ID = :linkID", dbDataParameter);
                dataManager.ExecuteNonQuery($"DELETE FROM IMV_R{dataTable2.Rows[index2][1].ToString()} WHERE F_PRJLINK_ID = :linkID", dbDataParameter);
                dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :linkID", dbDataParameter);
              }
              dataManager.ExecuteNonQuery($"INSERT INTO IMS_RELATIONS (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR) SELECT -F_PRJLINK_ID, -F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR FROM IMS_RELATIONS WHERE (F_PROJ_ID = :{idParameterName}) AND (F_RELATION_TYPE = :rType)", idPar, dataManager.Parameter("rType", (object) relationType.RelationType));
            }
            else
              throw;
          }
          dataManager.ExecuteNonQuery($"INSERT INTO IMS_RELATION_ATTRS (F_PRJLINK_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) SELECT -A.F_PRJLINK_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE FROM IMS_RELATIONS R, IMS_RELATION_ATTRS A WHERE (R.F_PROJ_ID = :{idParameterName}) AND (R.F_RELATION_TYPE = :rType) AND (A.F_PRJLINK_ID = R.F_PRJLINK_ID)", idPar, dataManager.Parameter("rType", (object) relationType.RelationType));
          if (updateTables != null && updateTables.Length != 0)
          {
            using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
            {
              StringBuilder stringBuilder = objectPoolScope.Object;
              DataTable dataTable3 = relationType.Attributes.Select(string.Empty);
              for (int index3 = 0; index3 < dataTable3.Rows.Count; ++index3)
              {
                if (Convert.ToInt32(dataTable3.Rows[index3]["F_INVIEW"]) != 0)
                {
                  foreach (string fieldName in (this.UserSession.GetAttributeType(Convert.ToInt32(dataTable3.Rows[index3]["F_ATTRIBUTE_ID"])) as DBAttributeType).FieldNames)
                    stringBuilder.Append("," + fieldName);
                }
              }
              try
              {
                dataManager.ExecuteNonQuery(string.Format("INSERT INTO {0} (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR{1}) SELECT -F_PRJLINK_ID, -F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR{1} FROM {0} WHERE (F_PROJ_ID = :{2})", (object) updateTables[0], (object) stringBuilder.ToString(), (object) idParameterName), idPar);
                continue;
              }
              catch (Exception ex)
              {
                if (ex.Message.IndexOf("PRIMARY") >= 0)
                {
                  dataManager.ExecuteNonQuery($"DELETE FROM {updateTables[0]} WHERE (F_PROJ_ID = :neg_objID)", dataManager.Parameter("neg_objID", (object) -Convert.ToInt64(idPar.Value)));
                  dataManager.ExecuteNonQuery(string.Format("INSERT INTO {0} (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR{1}) SELECT -F_PRJLINK_ID, -F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR{1} FROM {0} WHERE (F_PROJ_ID = :{2})", (object) updateTables[0], (object) stringBuilder.ToString(), (object) idParameterName), idPar);
                  continue;
                }
                throw;
              }
            }
          }
          else
            continue;
        }
      }
      DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(relationType.RelationType) as DBRelationCollection;
      relationCollection._AssignMode = Consts.CheckOutMode;
      string str = updateTables == null || updateTables.Length == 0 ? "IMS_RELATIONS" : updateTables[0];
      DataTable dataTable4 = dataManager.ExecuteDataTable($"SELECT F_PRJLINK_ID FROM {str} WHERE (F_PROJ_ID = :{idParameterName}) AND (F_RELATION_TYPE = :rType)", idPar, dataManager.Parameter("rType", (object) relationType.RelationType));
      for (int index4 = 0; index4 < dataTable4.Rows.Count; ++index4)
      {
        IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(dataTable4.Rows[index4][0]), false);
        if (relation != null)
        {
          relationCollection._CheckCreateRules = false;
          relationCollection.Create(new NewRelationProperties(relation, -Convert.ToInt64(idPar.Value), relation.GUID));
        }
      }
    }
  }

  public ObjectModifyModes ObjectModifyMode => this.LCStepObject.ObjectModifyMode;

  public ObjectFiltrationState FiltrationState
  {
    get => this._FiltrationState;
    set => this._FiltrationState = value;
  }

  public IDBObject CheckOut() => this.CheckOut(true);

  public IDBObject CheckOut(bool throwModifyModeException)
  {
    this.CheckDeleted(nameof (CheckOut));
    if (this.CheckoutBy == this.UserSession.UserID)
      return this.ObjectID > 0L ? this.UserSession.GetObject(-this.ObjectID) : (IDBObject) this;
    if (this.ObjectID == 0L || this.ObjectID == 1L)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13337(353649379), (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    if (!this.IsCreationMode && this.SiteID.Length > 0 && this.ReadonlyPublishedObject(false))
      throw new KernelExceptionID(sc_13302.ssp_appserver_13338(701212606), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    long EventID;
    try
    {
      this._isCheckOutMode = true;
      if (!this._IsCreationMode)
        this.CheckAccess(ActionType.Edit, this.GetDefaultAccess(ActionType.Edit));
      else
        this._IsCreationMode = false;
      EventID = this.AddEvent(this.ObjectID, ActionType.CheckOut, EventlogRecordType.AccessGranted);
    }
    catch
    {
      EventID = this.AddEvent(this.ObjectID, ActionType.CheckOut, EventlogRecordType.AccessDenied);
      throw;
    }
    finally
    {
      this._isCheckOutMode = false;
    }
    ObjectModifyModes objectModifyMode = this.ObjectModifyMode;
    switch (objectModifyMode)
    {
      case ObjectModifyModes.InBase:
        if (throwModifyModeException)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13340(1360061389), (object) this.NameInMessages, (object) this.ObjectID, (object) this.LCStepObject.LCName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
        return (IDBObject) this;
      case ObjectModifyModes.CantModify:
        throw new KernelExceptionID(sc_13302.ssp_appserver_13339(1288947907), (object) this.NameInMessages, (object) this.ObjectID, (object) this.LCStepObject.LCName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      default:
        long objectId = this.ObjectID;
        this.DoBeforeCheckout();
        this.UserSession.StartTransaction();
        try
        {
          bool flag1 = MetaDataHelper.IsObjectTypeChildOf(this.ObjectType, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
          bool flag2 = this.AppendVersionToContext() && !MetaDataHelper.IsObjectTypeEditingContext(this.ObjectType);
          IDBObject dbObject;
          if (objectModifyMode != ObjectModifyModes.CreateVersion)
          {
            if (this.CheckoutBy != 0L)
              throw new KernelExceptionID(63 /*0x3F*/, (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObject(this.CheckoutBy).NameInMessages).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
            int autoAccessLevelUpId = this.GetAutoAccessLevelUpID();
            (this.EventHelper as EventLogHelper).OnBeforeCheckout((IDBObject) this, (IUserSession) this.UserSession);
            dbObject = this.DoCheckout();
            if (autoAccessLevelUpId > 0)
              (dbObject as DBObject).DoSetAccessLevel(autoAccessLevelUpId);
            (this.EventHelper as EventLogHelper).OnAfterCheckout((IDBObject) this, (IUserSession) this.UserSession);
          }
          else
          {
            if (throwModifyModeException)
              throw new KernelExceptionID(sc_13302.ssp_appserver_13342(1152403053), (object) this.NameInMessages, (object) this.ObjectID, (object) this.LCStepObject.LCName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
            dbObject = this.UserSession.GetObjectCollection(this.ObjectType).CreateVersion(this.ObjectID);
            dbObject.CommitCreation(false, true);
          }
          this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
          this.UserSession.Commit();
          this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, objectId, ActionType.CheckOut, this.ObjectType));
          this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, dbObject.ObjectID, ActionType.CheckOut, this.ObjectType));
          if (DBObject.EditingContextsServerService != null)
          {
            if (flag1)
            {
              EditingContextsObjectContainer editingContextsObject = DBObject.EditingContextsServerService.GetEditingContextsObject((object) this.UserSession, Math.Abs(this.ObjectID), false, this.UserSession.EnabledEditingContextsCache);
              if (editingContextsObject != null)
              {
                editingContextsObject.ContextID = dbObject.ObjectID;
                DBObject.EditingContextsServerService.SetEditingContextsObject((object) this.UserSession, editingContextsObject, true, true);
              }
            }
            if (DBObject.EditingContextsServerService.GetUserContextID(this.UserSession.MasterSessionGUID) == objectId)
              DBObject.EditingContextsServerService.SetUserContextID(this.UserSession.MasterSessionGUID, dbObject.ObjectID, (dbObject as DBEditingContextsObject).LinkedContextNumber);
          }
          if (flag2 && DBObject.EditingContextsServerService != null && !DBObject.EditingContextsServerService.ExistsInContext((object) this.UserSession, this.UserSession.EditingContextID, dbObject.ObjectID))
            DBObject.EditingContextsServerService.AddToContext((object) this.UserSession, this.UserSession.EditingContextID, this.UserSession.EditingContextModificationID, dbObject.ID, dbObject.ObjectID, Math.Abs(this.ModificationID) != Math.Abs(this.UserSession.EditingContextModificationID), true);
          return dbObject;
        }
        catch (Exception ex)
        {
          this.UserSession.Rollback();
          this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
          throw;
        }
    }
  }

  protected virtual void DoBeforeCheckout()
  {
  }

  public int CancelChanges(bool isAdminMode)
  {
    long num1 = 0;
    if (isAdminMode)
      num1 = 16L /*0x10*/;
    long DeleteMode = num1 | 2048L /*0x0800*/;
    if (this.UserSession.IsSystemSession)
    {
      if (this.CheckoutBy == 0L)
        return 0;
    }
    else if (this.CheckoutBy != this.UserSession.UserID && !isAdminMode)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13343(1339389709), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    long objectId = this.ObjectID;
    bool flag = MetaDataHelper.IsObjectTypeChildOf(this.ObjectType, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
    if (this.ObjectID < 0L)
    {
      this.UserSession.DBObjectsCacheRemoveVersion(this.ObjectID);
      this.UserSession.StartTransaction();
      try
      {
        if (DBObject.EditingContextsServerService != null && DBObject.EditingContextsServerService.GetUserContextID(this.UserSession.MasterSessionGUID) == Math.Abs(this.ObjectID))
          DBObject.EditingContextsServerService.SetUserContextID(this.UserSession.MasterSessionGUID, -this.ObjectID, (this as DBEditingContextsObject).LinkedContextNumber);
        ObjectDelayedNotification notify = new ObjectDelayedNotification(this.UserSession.RealUserID, ActionType.Cancel, this.GetAttributes4Notification((DBAttribute) null), (AttributeValues[]) null, Math.Abs(this.ObjectID), this.ObjectType, this.ID, this.Caption, this.LevelID, this.VersionID);
        int num2 = this.Delete(DeleteMode);
        this.UserSession.AddDelayedNotification((DelayedNotification) notify);
        if (DBObject.EditingContextsServerService != null && flag)
          DBObject.EditingContextsServerService.SetEditingContextsObject((object) this.UserSession, DBObject.EditingContextsServerService.GetEditingContextsObject((object) this.UserSession, Math.Abs(this.ObjectID), false, this.UserSession.EnabledEditingContextsCache), true, true);
        this.UserSession.Commit();
        return num2;
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
    else
    {
      if (!(this.UserSession.GetObject(-this.ObjectID, false) is DBObject dbObject))
        throw new KernelExceptionID(sc_13302.ssp_appserver_13344(1105425368), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      this.UserSession.StartTransaction();
      try
      {
        ObjectDelayedNotification notify = new ObjectDelayedNotification(this.UserSession.RealUserID, ActionType.Cancel, dbObject.GetAttributes4Notification((DBAttribute) null), (AttributeValues[]) null, this.ObjectID, dbObject.ObjectType, this.ID, dbObject.Caption, dbObject.LevelID, this.VersionID);
        int num3 = dbObject.Delete(DeleteMode);
        this.UserSession.AddDelayedNotification((DelayedNotification) notify);
        this.paramsTable[152] = (object) 0;
        this.UserSession.DBObjectsCacheRemoveVersion(-this.ObjectID);
        if (DBObject.EditingContextsServerService != null && DBObject.EditingContextsServerService.GetUserContextID(this.UserSession.MasterSessionGUID) == Math.Abs(this.ObjectID))
          DBObject.EditingContextsServerService.SetUserContextID(this.UserSession.MasterSessionGUID, this.ObjectID, (this as DBEditingContextsObject).LinkedContextNumber);
        this.UserSession.Commit();
        return num3;
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  public int CancelChanges() => this.CancelChanges(false);

  protected virtual void DoCheckIn()
  {
  }

  public int CheckIn()
  {
    this.CheckDeleted(nameof (CheckIn));
    if (this.IsCreationMode)
    {
      this.CommitCreation(false);
      return 0;
    }
    if (this.CheckoutBy == 0L)
      return 0;
    if (this.ObjectID > 0L)
    {
      int num = this.UserSession.GetObject(-this.ObjectID).CheckIn();
      this.paramsTable[152] = (object) 0;
      this.UserSession.DBObjectsCacheRemoveVersion(-this.ObjectID);
      if (DBObject.EditingContextsServerService == null || DBObject.EditingContextsServerService.GetUserContextID(this.UserSession.MasterSessionGUID) != Math.Abs(this.ObjectID))
        return num;
      DBObject.EditingContextsServerService.SetUserContextID(this.UserSession.MasterSessionGUID, this.ObjectID, (this as DBEditingContextsObject).LinkedContextNumber);
      return num;
    }
    long EventID = this.AddEvent(this.ObjectID, ActionType.CheckIn, EventlogRecordType.AccessGranted);
    this.UserSession.StartTransaction();
    try
    {
      if (this.CheckoutBy != this.UserSession.UserID)
        throw new KernelExceptionID(sc_13302.ssp_appserver_13345(1106517564), (object) this.UserSession.UserName);
      int count = this.Attributes.Count;
      (this.EventHelper as EventLogHelper).OnBeforeCheckin((IDBObject) this, (IUserSession) this.UserSession);
      this.ValidateCheckinRules();
      AttributeValues[] oldValues = (AttributeValues[]) null;
      AttributeValues[] attributes4Notification = this.GetAttributes4Notification((DBAttribute) null);
      this.DoCheckIn();
      IDbManager dataManager = this.UserSession.DataManager;
      IDBObject dbObject = this.UserSession.GetObject(-this.ObjectID);
      if (attributes4Notification != null)
        oldValues = (dbObject as DBObject).GetAttributes4Notification((DBAttribute) null);
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) -this.ObjectID);
      IDbDataParameter dbDataParameter2 = dataManager.Parameter("wobjID", (object) this.ObjectID);
      if (this.HasIndexedAttributes())
        this.UserSession.CheckInIndexQueue(this.ObjectID);
      (dbObject.Attributes as DBAttributeCollection).Purge();
      this.DeleteArcRelations();
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT DISTINCT F_RELATION_TYPE FROM IMS_RELATIONS WHERE F_PROJ_ID = :wobjID", dbDataParameter2);
      if (this.ObjectType != dbObject.ObjectType)
        (dbObject as DBObject).DeleteFromView(dbObject.ObjectType);
      foreach (string updateTable in this.UserSession.DBCache.GetUpdateTables(-1, this.ObjectType, -1))
      {
        if (this.ObjectType == dbObject.ObjectType)
          dataManager.ExecuteNonQuery($"DELETE FROM {updateTable} WHERE F_OBJECT_ID = :objID", dbDataParameter1);
        dataManager.ExecuteNonQuery($"UPDATE {updateTable} SET F_OBJECT_ID = :objID, F_CHKOUT_BY = 0 WHERE F_OBJECT_ID = :wobjID", dbDataParameter1, dbDataParameter2);
      }
      dataManager.ExecuteNonQuery($"UPDATE {this.AttributesTableName} SET F_OBJECT_ID = :objID WHERE F_OBJECT_ID = :wobjID", dbDataParameter1, dbDataParameter2);
      if (MetaDataHelper.GetAttribute4ObjectType(this.ObjectType, this.UserSession.IdentHelper.FileAttributeID) != null || this.ObjectTypeClass.AnyAttributes)
        dataManager.ExecuteNonQuery("UPDATE IMS_FILENAMES SET F_KEY = :objID WHERE F_KEY = :wobjID", dbDataParameter1, dbDataParameter2);
      dataManager.ExecuteNonQuery("UPDATE IMS_OBJECT_LINKS SET F_OBJECT_ID = :objID WHERE F_OBJECT_ID = :wobjID", dbDataParameter1, dbDataParameter2);
      dataManager.ExecuteNonQuery("UPDATE IMS_ID_LINKS SET F_OBJECT_ID = :objID WHERE F_OBJECT_ID = :wobjID", dbDataParameter1, dbDataParameter2);
      (dbObject as DBObject).SetCaption(this.Caption);
      try
      {
        dataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_PROJ_ID = :objID WHERE F_PROJ_ID = :wobjID", dbDataParameter1, dbDataParameter2);
      }
      catch (Exception ex)
      {
        if (ex.Message.IndexOf("IMS_RELATIONS_PRJ_GUID") > -1)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT R1.F_PRJLINK_ID FROM IMS_RELATIONS R1 WHERE (F_PROJ_ID = :objID) AND EXISTS(SELECT R2.F_PRJLINK_ID FROM IMS_RELATIONS R2 WHERE (R2.F_PROJ_ID = -R1.F_PROJ_ID) AND (R2.F_PRJ_GUID = R1.F_PRJ_GUID))", dbDataParameter1).Rows)
          {
            IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row[0]));
            (relation.Attributes as DBAttributeCollection).Purge();
            relation.Delete((long) (Consts.DontCheckApplicabilityModes | Consts.PurgeMode | Consts.CheckInMode));
          }
          dataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_PROJ_ID = :objID WHERE F_PROJ_ID = :wobjID", dbDataParameter1, dbDataParameter2);
        }
        else
          throw;
      }
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, Convert.ToInt32(dataTable.Rows[index][0]));
        if (updateTables != null)
        {
          foreach (string str in updateTables)
            dataManager.ExecuteNonQuery($"UPDATE {str} SET F_PROJ_ID = :objID WHERE F_PROJ_ID = :wobjID", dbDataParameter1, dbDataParameter2);
        }
      }
      dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_LC_STEP = :lcStep, F_CHKOUT_BY = 0, F_OBJECT_VER_TYPE = :verType, F_OBJECT_TYPE = :objType, F_OWNER_ID = :ownID, F_ACCESS = :accessID, F_LEVEL_ID = :levID, F_PROJECT_ID = :prjID, F_MODIFICATION_ID = :modifyID, F_BASE_VERSION = :baseVer, F_SITE_ID = :siteID WHERE F_OBJECT_ID = :objID", dataManager.Parameter("lcStep", (object) this.LCStep), dataManager.Parameter("verType", (object) this.ObjectVerType), dataManager.Parameter("objType", (object) this.ObjectType), dataManager.Parameter("ownID", (object) this.OwnerID), dataManager.Parameter("levID", (object) this.LevelID), dataManager.Parameter("accessID", (object) this.AccessLevel), dataManager.Parameter("prjID", (object) this.ProjectID), dataManager.Parameter("modifyID", (object) this.ModificationID), dataManager.Parameter("baseVer", (object) (this.IsBaseVersion ? 1 : 0)), dataManager.Parameter("siteID", (object) this.SiteID), dbDataParameter1);
      for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
      {
        if (this.Attributes[AttrIndex] is DBStorageAttribute)
          (this.Attributes[AttrIndex] as DBStorageAttribute).ChangeObjectLinkID(Math.Abs(this._ObjectID));
      }
      dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECTS WHERE F_OBJECT_ID = :wobjID", dbDataParameter2);
      this.CheckinRelatedObjects(true);
      this.UserSession.ClearObjectSmartCache();
      (this.EventHelper as EventLogHelper).OnAfterCheckin(this.UserSession.GetObject(-this.ObjectID), (IUserSession) this.UserSession);
      this.DoAfterCheckIn();
      this.UserSession.Commit();
      this.DoAfterCheckInCommited();
      this.UserSession.AddDelayedNotification((DelayedNotification) new ObjectDelayedNotification(this.UserSession.RealUserID, ActionType.CheckIn, oldValues, attributes4Notification, Math.Abs(this.ObjectID), this.ObjectType, this.ID, this.Caption, this.LevelID, this.VersionID));
      long objectId = this._ObjectID;
      Guid versionGuid = this._GuidTable == null || this._GuidTable.Rows.Count <= 0 ? Guid.Empty : this.VersionGUID;
      this._ObjectID = Math.Abs(this._ObjectID);
      this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, objectId, ActionType.CheckIn, this.ObjectType));
      this.UserSession.AddToModificationsHistory((CategoryValue) new ModificationEvent(1, this._ObjectID, ActionType.CheckIn, this.ObjectType));
      this.UserSession.DBObjectsCacheRemoveVersion(objectId);
      this.UserSession.DBObjectsCacheRemoveVersion(-objectId);
      this.UserSession.DBCache.DeleteObjectInfo(objectId, versionGuid);
      this.paramsTable[152] = (object) 0L;
      this.paramsTable[70] = (object) this._ObjectID;
      this.Deleted = true;
      this.UserSession.ClearObjectSmartCache();
      return 0;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
  }

  protected virtual void DoAfterCheckInCommited()
  {
  }

  private void CheckinRelatedObjects(bool delWorkCopy)
  {
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, this.ObjectType, -1);
    ConditionStructure[] conditions = (ConditionStructure[]) null;
    if (applicabilitiesList.Rows.Count > 0)
    {
      if (this.ModificationID == 0L)
        conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-6, RelationalOperators.Equal, (object) this.UserSession.UserID, LogicalOperators.AND, 0, true)
        };
      else
        conditions = new ConditionStructure[2]
        {
          new ConditionStructure(-6, RelationalOperators.Equal, (object) this.UserSession.UserID, LogicalOperators.AND, 1, true),
          new ConditionStructure(-15, RelationalOperators.Equal, (object) this.ModificationID, LogicalOperators.AND, -1, true)
        };
    }
    for (int index1 = 0; index1 < applicabilitiesList.Rows.Count; ++index1)
    {
      if (Convert.ToInt32(applicabilitiesList.Rows[index1]["F_MIN_LINKS"]) >= 0 && (Convert.ToInt32(applicabilitiesList.Rows[index1]["F_OPTIONS"]) & 32 /*0x20*/) == 32 /*0x20*/)
      {
        DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(Convert.ToInt32(applicabilitiesList.Rows[index1]["F_RELATION_TYPE"]), "cad005ac-306c-11d8-b4e9-00304f19f5455") as DBRelationCollection;
        relationCollection._ShowPersonalObjects = true;
        relationCollection.ChildObjectTypes = (IList<int>) MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(Convert.ToInt32(applicabilitiesList.Rows[index1]["F_INOBJECT_TYPE"]));
        DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[2]
        {
          (object) -21,
          (object) -7
        });
        DataTable dataTable = relationCollection.EntersInVersion(paramSet, -this.ObjectID, this.ID);
        for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
        {
          IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(relationCollection.RelationTypeID, this.ObjectType, Convert.ToInt32(dataTable.Rows[index2][1]));
          if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled && (applicability.Options & ApplicabilityOptions.SyncCheckin) == ApplicabilityOptions.SyncCheckin)
          {
            IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index2][0]), false);
            if (dbObject != null && dbObject.CheckoutBy == this.UserSession.UserID)
            {
              if (delWorkCopy)
                dbObject.CheckIn();
              else
                dbObject.SaveToArcCopy();
            }
          }
        }
      }
    }
    if (!this.UserSession.DBCache.IsSyncCheckInParentObjectType(this.ObjectType))
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable1 = dataManager.ExecuteDataTable("select distinct R.F_RELATION_TYPE, O.F_OBJECT_TYPE FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE R.F_PROJ_ID = :objID AND O.F_ID = R.F_PART_ID AND O.F_CHKOUT_BY = :usrID", dataManager.Parameter("objID", (object) Math.Abs(this.ObjectID)), dataManager.Parameter("usrID", (object) this.UserSession.UserID));
    for (int index3 = 0; index3 < dataTable1.Rows.Count; ++index3)
    {
      IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(Convert.ToInt32(dataTable1.Rows[index3][0]), Convert.ToInt32(dataTable1.Rows[index3][1]), this.ObjectType);
      if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled && (applicability.Options & ApplicabilityOptions.SyncCheckin) == ApplicabilityOptions.SyncCheckin)
      {
        DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(Convert.ToInt32(dataTable1.Rows[index3][0]), "cad005ac-306c-11d8-b4e9-00304f19f5455") as DBRelationCollection;
        relationCollection._ShowPersonalObjects = true;
        relationCollection.ObjectTypeID = Convert.ToInt32(dataTable1.Rows[index3][1]);
        DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[2]
        {
          (object) -2,
          (object) -7
        });
        DataTable dataTable2 = relationCollection.ConsistFrom(paramSet, Math.Abs(this.ObjectID));
        for (int index4 = 0; index4 < dataTable2.Rows.Count; ++index4)
        {
          if (Convert.ToInt32(dataTable1.Rows[index3][1]) == Convert.ToInt32(dataTable2.Rows[index4][1]) && this.UserSession.GetObject(Convert.ToInt64(dataTable2.Rows[index4][0]), false) is DBObject dbObject && dbObject.CheckoutBy == this.UserSession.UserID)
          {
            if (delWorkCopy)
              dbObject.CheckIn();
            else
              dbObject.SaveToArcCopy();
          }
        }
      }
    }
  }

  protected virtual void DoAfterCheckIn()
  {
  }

  private void DeleteArcRelations()
  {
    IDbManager dataManager = this.UserSession.DataManager;
    this.UserSession.GetObject(-this.ObjectID);
    IDbDataParameter dbDataParameter = dataManager.Parameter("objID", (object) -this.ObjectID);
    DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT F_RELATION_TYPE FROM IMS_RELATIONS WHERE (F_PROJ_ID = :objID)", dbDataParameter);
    if (dataTable1.Rows.Count <= 0)
      return;
    List<int> intList = new List<int>(1);
    for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
    {
      int int32 = Convert.ToInt32(dataTable1.Rows[index1][0]);
      if (intList.BinarySearch(int32) < 0)
      {
        intList.Add(int32);
        DBRelationType relationType = this.UserSession.GetRelationType(Convert.ToInt32(dataTable1.Rows[index1][0]), true) as DBRelationType;
        string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, relationType.RelationType);
        string str = updateTables == null || updateTables.Length == 0 ? "IMS_RELATIONS" : updateTables[0];
        if (relationType != null && relationType.CanQuickRelationsCopy())
        {
          dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATION_ATTRS WHERE F_PRJLINK_ID IN (SELECT R.F_PRJLINK_ID FROM IMS_RELATIONS R WHERE (R.F_PROJ_ID = :objID) AND (R.F_RELATION_TYPE = :rType))", dbDataParameter, dataManager.Parameter("rType", (object) relationType.RelationType));
          if (str != "IMS_RELATIONS")
            dataManager.ExecuteDataTable($"DELETE FROM {str} WHERE (F_PROJ_ID = :objID) AND (F_RELATION_TYPE = :rType)", dbDataParameter, dataManager.Parameter("rType", (object) relationType.RelationType));
          dataManager.ExecuteDataTable("DELETE FROM IMS_RELATIONS WHERE (F_PROJ_ID = :objID) AND (F_RELATION_TYPE = :rType)", dbDataParameter, dataManager.Parameter("rType", (object) relationType.RelationType));
        }
        else
        {
          DataTable dataTable2 = dataManager.ExecuteDataTable($"SELECT F_PRJLINK_ID FROM {str} WHERE (F_PROJ_ID = :objID) AND (F_RELATION_TYPE = :rType)", dbDataParameter, dataManager.Parameter("rType", (object) relationType.RelationType));
          for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
          {
            IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(dataTable2.Rows[index2][0]));
            (relation.Attributes as DBAttributeCollection).Purge();
            relation.Delete((long) (Consts.DontCheckApplicabilityModes | Consts.PurgeMode | Consts.CheckInMode));
          }
        }
      }
    }
  }

  private UserSessionPluginsData<List<long>> GetSaveToArcCopyListFromSession(UserSession UserSession)
  {
    return new UserSessionPluginsData<List<long>>((IUserSession) UserSession, "SaveToArcCopy_List");
  }

  public virtual void SaveToArcCopy()
  {
    if (this.IsCreationMode)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13346(925239697), (object) nameof (SaveToArcCopy));
    if (this.ObjectID > 0L)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13347(1722548377), (object) nameof (SaveToArcCopy));
    if (this.CheckoutBy != this.UserSession.UserID)
      throw new KernelExceptionID(134, (object) this.UserSession.UserName, (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObject(this.CheckoutBy).Caption).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    UserSessionPluginsData<List<long>> copyListFromSession = this.GetSaveToArcCopyListFromSession(this.UserSession);
    if (copyListFromSession.Value != null && copyListFromSession.Value.IndexOf(this.ObjectID) >= 0)
      return;
    this.UserSession.StartTransaction();
    try
    {
      IDbDataParameter idPar = this.UserSession.DataManager.Parameter("wobjID", (object) this.ObjectID);
      IDBObject dbObject = this.UserSession.GetObject(-this.ObjectID);
      if (this.ObjectType != dbObject.ObjectType)
      {
        int num = this.UserSession.IsStartedLogHistory ? 1 : 0;
        if (num != 0)
          this.UserSession.StopLogHistory();
        dbObject.CheckIn();
        this.UserSession.GetObject(-this.ObjectID).CheckOut();
        if (num != 0)
          this.UserSession.ResumeLogHistory();
      }
      else
      {
        bool flag = false;
        if (copyListFromSession.Value == null)
        {
          copyListFromSession.Value = new List<long>();
          flag = true;
        }
        copyListFromSession.Value.Add(this.ObjectID);
        try
        {
          (this.EventHelper as EventLogHelper).OnBeforeSaveToArcCopy(this, this.UserSession);
          (dbObject.Attributes as DBAttributeCollection).ValidatingOn = false;
          (dbObject as DBObject)._MustCheckValidatingRule = false;
          (dbObject.Attributes as DBAttributeCollection).Assign(this.Attributes, Consts.DeleteInstances | Consts.CheckInMode);
          this.DeleteArcRelations();
          this.CopyRelations(idPar, "wobjID");
          this.CheckinRelatedObjects(false);
          (this.EventHelper as EventLogHelper).OnAfterSaveToArcCopy(this, this.UserSession);
        }
        finally
        {
          if (flag)
            copyListFromSession.Value = (List<long>) null;
        }
      }
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  protected virtual void DoSaveChanges(bool flag)
  {
  }

  public void SaveChanges()
  {
    long EventID = this.AddEvent(this.ObjectID, ActionType.Save, EventlogRecordType.AccessGranted);
    this.UserSession.StartTransaction();
    try
    {
      if (this.ObjectID > 0L)
      {
        if (this.ObjectModifyMode != ObjectModifyModes.InBase)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13349(323024243), (object) this.NameInMessages);
      }
      else
      {
        if (this.IsCreationMode)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13350(304519800), (object) nameof (SaveChanges));
        if (this.CheckoutBy != this.UserSession.UserID)
          throw new KernelExceptionID(134, (object) this.UserSession.UserName, (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObject(this.CheckoutBy).Caption).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
      }
      (this.EventHelper as EventLogHelper).OnBeforeSaveChanges((IDBObject) this, (IUserSession) this.UserSession);
      this.DoSaveChanges(false);
      (this.EventHelper as EventLogHelper).OnAfterSaveChanges((IDBObject) this, (IUserSession) this.UserSession);
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
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
    attributeValues.GroupName = Consts.SystemAttributesGroupName;
    if (val != null && description != null)
      attributeValues.Descriptions = new object[1]
      {
        description
      };
    if ((modes & GetAttributeValuesModes.IncludeGuid) == GetAttributeValuesModes.IncludeGuid)
      attributeValues.AttributeGuid = new Guid(guid);
    attrList.Add(attributeValues);
  }

  public virtual AttributeValues[] GetInitAttributesValues(int[] attributeIDs)
  {
    AttributeValues[] attributesValues = new AttributeValues[attributeIDs.Length];
    for (int index = 0; index < attributesValues.Length; ++index)
    {
      AttributeValues attributeValues = new AttributeValues(attributeIDs[index]);
      if (this.ObjectTypeClass.AnyAttributes || this.ObjectTypeClass.GetAttributeType(attributeIDs[index]) != null)
      {
        if (this.Attributes.FindByID(attributeIDs[index]) == null)
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
      }
      else
        attributeValues.ReadOnly = true;
      attributesValues[index] = attributeValues;
    }
    return attributesValues;
  }

  internal bool isReadOnlyCaption()
  {
    bool flag;
    if (this.ObjectTypeClass.CaptionAttribute > 0)
    {
      IDBAttribute byId = this.Attributes.FindByID(this.ObjectTypeClass.CaptionAttribute);
      flag = byId == null || byId.ReadOnly;
    }
    else
      flag = this.ReadOnly;
    return flag;
  }

  internal bool isReadOnlyAccessLevel()
  {
    return (this.ObjectTypeClass.Options & ObjectTypeOptions.MandateAccess) != ObjectTypeOptions.MandateAccess || !this.CheckAccess(ActionType.ChangeAccessLevel, this.GetDefaultAccess(ActionType.ChangeAccessLevel), false);
  }

  public virtual AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes)
  {
    this.CheckDeleted(nameof (GetAttributesValues));
    List<AttributeValues> attrList = new List<AttributeValues>();
    bool flag = false;
    if ((modes & GetAttributeValuesModes.CheckWriteAccess) == GetAttributeValuesModes.CheckWriteAccess)
      flag = this.ReadOnly;
    if ((modes & GetAttributeValuesModes.IncludeObligatoryAttributes) == GetAttributeValuesModes.IncludeObligatoryAttributes)
    {
      object description1 = (object) null;
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_OBJECT_ID, (object) this.ObjectID, true, description1, modes, "cad00029-306c-11d8-b4e9-00304f19f545");
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
        description1 = this.CheckoutBy != 0L ? (object) this.UserSession.GetObjectInfo(this.CheckoutBy).Caption : (object) LocalizationHolder.rm.GetString("Kernel_413");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_CHKOUT_BY, (object) this.CheckoutBy, true, description1, modes, "cad0002d-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_ID, (object) this.ID, true, (object) null, modes, "cad0002a-306c-11d8-b4e9-00304f19f545");
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
        description1 = (object) MetaDataHelper.GetLCStepName(this.LCStep);
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_LC_STEP, (object) this.LCStep, this.CheckoutBy != 0L, description1, modes, "cad0002b-306c-11d8-b4e9-00304f19f545");
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
        description1 = (object) MetaDataHelper.GetLCLevelName(this.LevelID);
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_LEVEL_ID, (object) this.LevelID, true, description1, modes, "cad00030-306c-11d8-b4e9-00304f19f545");
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
        description1 = (object) this.ObjectTypeClass.ObjectInstanceName;
      bool readOnly1 = false;
      if ((modes & GetAttributeValuesModes.CheckWriteAccess) == GetAttributeValuesModes.CheckWriteAccess)
        readOnly1 = this.CheckoutBy != 0L || this.ObjectModifyMode == ObjectModifyModes.CantModify || this.ObjectModifyMode == ObjectModifyModes.CreateVersion || !this.CheckAccess(ActionType.Edit, this.GetDefaultAccess(ActionType.Edit), false);
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_OBJECT_TYPE, (object) this.ObjectType, readOnly1, description1, modes, "cad0002e-306c-11d8-b4e9-00304f19f545");
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
        description1 = (object) this.UserSession.GetObjectInfo(this.OwnerID).Caption;
      bool readOnly2 = false;
      if ((modes & GetAttributeValuesModes.CheckWriteAccess) == GetAttributeValuesModes.CheckWriteAccess)
        readOnly2 = !this.CheckAccess(ActionType.TakeOwnership, this.GetDefaultAccess(ActionType.TakeOwnership), false);
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_OWNER_ID, (object) this.OwnerID, readOnly2, description1, modes, "cad0002f-306c-11d8-b4e9-00304f19f545");
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
        description1 = this.ProjectID <= 0L ? (object) string.Empty : (object) this.UserSession.GetObjectInfo(this.ProjectID).Caption;
      if ((modes & GetAttributeValuesModes.CheckWriteAccess) == GetAttributeValuesModes.CheckWriteAccess)
        readOnly2 = this.ReadOnlyProjectID();
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_PROJECT_ID, (object) this.ProjectID, readOnly2, description1, modes, "cad00811-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_MODIFICATION_ID, (object) this.ModificationID, true, (object) string.Empty, modes, "cad014d2-306c-11d8-b4e9-00304f19f545");
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
      {
        description1 = (object) string.Empty;
        if (this.SiteID != string.Empty)
          description1 = (object) SiteIDHelper.GetCaption((ISitesCacheService) this.UserSession.GetCustomService(typeof (ISitesCacheService)), this.SiteID);
      }
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_SITE_ID, (object) this.SiteID, true, description1, modes, "cad01501-306c-11d8-b4e9-00304f19f545");
      object description2 = (object) null;
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
      {
        description1 = !this.IsBaseVersion ? (object) string.Empty : (object) LocalizationHolder.rm.GetString("BaseVersionDescription");
        if (this.CreatorID != 0L)
        {
          QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(this.CreatorID);
          if (!objectInfo.Empty)
            description2 = (object) objectInfo.Caption;
        }
      }
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_BASE_VERSION, (object) this.IsBaseVersion, true, description1, modes, "cad014d3-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_VERSION_ID, (object) this.VersionID, true, (object) null, modes, "cad0002c-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_GUID, (object) this.ObjectGUID, flag || !this.UserSession.DeveloperMode, (object) null, modes, "cad00130-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_OBJ_GUID, (object) ((IDBObject) this).GUID, flag || !this.UserSession.DeveloperMode, (object) null, modes, "cad00800-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_OBJ_CREATE, (object) this.CreateDate, true, (object) null, modes, "cad0013c-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_CREATOR_ID, (object) this.CreatorID, true, description2, modes, "cadd96b7-306c-11d8-b4e9-00304f19f545");
      if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
        description1 = (object) this.UserSession.DBCache.GetAccessCaption(this.AccessLevel);
      bool readOnly3 = (modes & GetAttributeValuesModes.CheckWriteAccess) != GetAttributeValuesModes.CheckWriteAccess || this.isReadOnlyAccessLevel();
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_ACCESS, (object) this.AccessLevel, readOnly3, description1, modes, "cadd959f-306c-11d8-b4e9-00304f19f545");
    }
    if ((modes & GetAttributeValuesModes.IncludeObligatoryAttributes) == GetAttributeValuesModes.IncludeObligatoryAttributes || (modes & GetAttributeValuesModes.IncludeCaption) == GetAttributeValuesModes.IncludeCaption)
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.CAPTION, (object) this.Caption, this.isReadOnlyCaption(), (object) null, modes, "cad00047-306c-11d8-b4e9-00304f19f545");
    if ((modes & GetAttributeValuesModes.IncludeVirtualAttributes) == GetAttributeValuesModes.IncludeVirtualAttributes)
    {
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_VERSIONS_COUNT, (object) this.VersionsCount, true, (object) null, modes, "cadd98e9-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_RELATIONS_COUNT, (object) this.RelationsCount, true, (object) null, modes, "cadd98ee-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_REFERENCE_COUNT, (object) this.ReferencesCount, true, (object) null, modes, "cadd98ed-306c-11d8-b4e9-00304f19f545");
      this.AddObligatoryAttribute(attrList, ObligatoryObjectAttributes.F_LCSTEP_DATE, (object) this.LCStepDate, true, (object) null, modes, "cadd9972-306c-11d8-b4e9-00304f19f545");
    }
    for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
    {
      DBAttribute attribute = this.Attributes[AttrIndex] as DBAttribute;
      if ((modes & GetAttributeValuesModes.CheckReadAccess) == GetAttributeValuesModes.CheckReadAccess)
      {
        if (!attribute.VisibleByAccess)
          continue;
      }
      else if ((modes & GetAttributeValuesModes.CheckVisibility) == GetAttributeValuesModes.CheckVisibility)
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
        if ((modes & GetAttributeValuesModes.IncludeBlobValues) == GetAttributeValuesModes.IncludeBlobValues)
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

  public bool CheckoutByOther(bool throwException)
  {
    bool flag = this.CheckoutBy != 0L && this.CheckoutBy != this.UserSession.UserID;
    return !(throwException & flag) ? flag : throw new KernelExceptionID(sc_13302.ssp_appserver_13352(209118290), (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObjectInfo(this.CheckoutBy).Caption).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
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
    this.CheckDeleted(nameof (SetAttributesValues));
    this.SetAttributesState(Consts.AssignValuesMode, valuesList);
    try
    {
      AttributesValuesEventArgs args = new AttributesValuesEventArgs(valuesList, modes, (IUserSession) this.UserSession);
      (this.EventHelper as EventLogHelper).OnBeforeSetObjectAttributesValues((IDBAttributable) this, args);
      this.UpdateValuesListByArgs(ref valuesList, args);
      if (valuesList.Length > 1)
        this.ViewsUpdaterPrepare();
      this.UserSession.StartTransaction();
      try
      {
        List<int> intList = (List<int>) null;
        if (deleteNotExistingAttributes)
          intList = new List<int>();
        AttributeValues[] oldValues = (AttributeValues[]) null;
        if (this.ObjectID > 0L)
          oldValues = this.GetAttributes4Notification((DBAttribute) null);
        for (int index = 0; index < valuesList.Length; ++index)
        {
          AttributeValues values = valuesList[index];
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
              if (!values.ReadOnly)
              {
                if (values.Values != null)
                {
                  switch (attributeId)
                  {
                    case -80:
                      this.AccessLevel = Convert.ToInt32(values.Values[0]);
                      continue;
                    case -50:
                      this.Caption = Convert.ToString(values.Values[0]);
                      continue;
                    case -18:
                      ((IDBObject) this).GUID = new Guid(values.Values[0].ToString());
                      continue;
                    case -14:
                      this.ProjectID = Convert.ToInt64(values.Values[0]);
                      continue;
                    case -12:
                      this.ObjectGUID = new Guid(values.Values[0].ToString());
                      continue;
                    case -8:
                      this.OwnerID = Convert.ToInt64(values.Values[0]);
                      continue;
                    case -7:
                      this.ObjectType = Convert.ToInt32(values.Values[0]);
                      continue;
                    case -4:
                      this.LCStep = Convert.ToInt32(values.Values[0]);
                      continue;
                    default:
                      continue;
                  }
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
                      throw new KernelExceptionID(sc_13302.ssp_appserver_13353(1387649278));
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
                    throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_415"), (object) this.UserSession.GetAttributeType(attributeId).Name, (object) ex.Message), ex);
                  exceptionsList?.Add(this.UserSession.GetAttributeType(attributeId).Name, ex);
                }
              }
              if (deleteNotExistingAttributes)
              {
                if (flag)
                  throw new KernelExceptionID(sc_13302.ssp_appserver_13354(1310820138));
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
                      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_416"), (object) this.UserSession.GetAttributeType(attributeId).Name, (object) ex.Message), ex);
                    exceptionsList?.Add(this.UserSession.GetAttributeType(attributeId).Name, ex);
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
                      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_417"), (object) this.UserSession.GetAttributeType(attributeId).Name, (object) ex.Message), ex);
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
        if (this.ObjectID > 0L)
          this.UserSession.AddDelayedNotification((DelayedNotification) new SetAttributesValuesDelayedNotification(this.UserSession.RealUserID, ActionType.Write, oldValues, this.GetAttributes4Notification((DBAttribute) null), this.ObjectID, this.ObjectType, valuesList));
        (this.EventHelper as EventLogHelper).OnAfterSetObjectAttributesValues((IDBAttributable) this, new AttributesValuesEventArgs(valuesList, modes, (IUserSession) this.UserSession));
        this.UserSession.Commit();
      }
      catch (Exception ex)
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

  public override long AccessOwnerID => this.OwnerID;

  public override bool IsUserOwner()
  {
    return this.UserSession.DBSecurity.GetGroupsArrayList().IndexOf(this.AccessOwnerID) >= 0;
  }

  internal bool CheckEditMode(
    bool validateCheckOut,
    bool checkAccess,
    bool isRelationCheck,
    bool throwExeption = true)
  {
    if (this.CheckoutBy != this.UserSession.UserID || this.ObjectID > 0L)
    {
      if (this.CheckoutBy != 0L && this.CheckoutBy != this.UserSession.UserID && !this.UserSession.IsSystemSession)
      {
        if (throwExeption)
          throw new KernelException(string.Format(sc_13302.ssp_appserver_13355(), (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObjectInfo(this.CheckoutBy).Caption)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
        return false;
      }
      if (this.ObjectID > 0L & validateCheckOut)
      {
        switch (this.ObjectModifyMode)
        {
          case ObjectModifyModes.Checkout:
            if (throwExeption)
              throw new KernelExceptionID(sc_13302.ssp_appserver_13357(1946975457), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
            return false;
          case ObjectModifyModes.CreateVersion:
            if (throwExeption)
              throw new KernelExceptionID(sc_13302.ssp_appserver_13358(1820063090), (object) this.NameInMessages, (object) this.ObjectID, (object) this.LCStepObject.LCName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
            return false;
          case ObjectModifyModes.CantModify:
            if (throwExeption)
              throw new KernelExceptionID(sc_13302.ssp_appserver_13356(2025390893), (object) this.NameInMessages, (object) this.ObjectID, (object) this.LCStepObject.LCName).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
            return false;
        }
      }
      if (checkAccess)
      {
        if (this.IsCreationMode || this.SiteID.Length <= 0 || !this.ReadonlyPublishedObject(isRelationCheck))
          return this.CheckAccess(ActionType.Edit, this.GetDefaultAccess(ActionType.Edit), throwExeption);
        if (throwExeption)
          throw new KernelExceptionID(sc_13302.ssp_appserver_13359(209578518), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
        return false;
      }
    }
    return true;
  }

  public void CheckEdit() => this.CheckEditMode(true, true, false);

  public void CheckRelationsEdit() => this.CheckEditMode(true, true, true);

  private bool ReturnAccessTypeDeny(bool throwException, bool defaultAccess, ActionType anAction)
  {
    if (this.UseAccessCache)
    {
      AccessInfo accessResult = new AccessInfo(false, true, defaultAccess, this._GrantAlways, this.UserSession.LogList, this.GetCheckAccessHash());
      ((IDBSecurityCache) this.UserSession.DBSecurity).AddToCache(new CategoryValue(20, Math.Abs(this.ObjectID), anAction), accessResult);
    }
    if (throwException)
      throw new AccessDeniedException((IUserSession) this.UserSession);
    return false;
  }

  public override long GetCategoryID4ActionName(long _categoryID)
  {
    return _categoryID == Math.Abs(this.ObjectID) ? (long) this.ObjectType : base.GetCategoryID4ActionName(_categoryID);
  }

  protected virtual void DoBeforeEdit()
  {
  }

  public void Edit()
  {
    this.DoBeforeEdit();
    if (this.SiteID.Length > 0 && this.ReadonlyPublishedObject(false))
      throw new KernelExceptionID(sc_13302.ssp_appserver_13360(2005917661), (object) this.NameInMessages, (object) this.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    try
    {
      this.CheckAccess(ActionType.Edit);
      this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessGranted);
    }
    catch (AccessDeniedException ex)
    {
      this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessDenied);
      throw;
    }
  }

  internal string GetObjectExtendedAccessSQL(string s)
  {
    long num;
    if (this.CreatorID == this.UserSession.UserID)
    {
      if (s == string.Empty)
      {
        num = this.UserSession.IdentHelper.ObjectCreatorGroupID;
        s = num.ToString();
      }
      else
        s = $"{s},{this.UserSession.IdentHelper.ObjectCreatorGroupID.ToString()}";
    }
    if (this.ProjectID > 0L)
    {
      IDbManager dataManager = this.UserSession.DataManager;
      object obj = dataManager.ExecuteScalar("SELECT F_OPTIONS FROM IMS_PROJECT_TEAM WHERE F_PROJECT_ID = :prjID AND F_USER_ID = :usrID", dataManager.Parameter("prjID", (object) this.ProjectID), dataManager.Parameter("usrID", (object) this.UserSession.UserID));
      if (obj != null && obj != DBNull.Value)
      {
        string str;
        if ((Convert.ToInt32(obj) & 1) == 1)
        {
          str = $"{DBProjectConsts.GetManagersGroupID(this.UserSession)},{DBProjectConsts.GetMembersGroupID(this.UserSession)}";
        }
        else
        {
          num = DBProjectConsts.GetMembersGroupID(this.UserSession);
          str = num.ToString();
        }
        s = !(s == string.Empty) ? $"{s},{str}" : str;
      }
    }
    return s;
  }

  protected override string GetExtendedAccessSQL()
  {
    return this.GetObjectExtendedAccessSQL(base.GetExtendedAccessSQL());
  }

  protected bool CheckAccessResult(ActionType anAction, bool result)
  {
    if (anAction == ActionType.View)
    {
      long lastEventId = this._LastEventID;
      this.AddEvent(this.ObjectID, anAction, result ? EventlogRecordType.AccessGranted : EventlogRecordType.AccessDenied);
      this._LastEventID = lastEventId;
    }
    return result;
  }

  protected virtual bool CheckAccessInCache(
    int aCategoryType,
    ActionType anAction,
    CheckAccessFlags flags,
    out bool accessResult)
  {
    accessResult = false;
    if (this.UseAccessCache && anAction != ActionType.NextLCStep)
    {
      AccessInfo accessInfo = ((IDBSecurityCache) this.UserSession.DBSecurity).CheckAccessInCache(new CategoryValue(aCategoryType, Math.Abs(this.ObjectID), anAction));
      if (accessInfo != null && accessInfo.CheckAccessHashCode == this.GetCheckAccessHash())
      {
        this._LastDeny = accessInfo.DenyMode;
        this._LastDefault = accessInfo.DefaultAccess;
        for (int index = 0; index < accessInfo.CheckLogString.Count; ++index)
          this.UserSession.LogList.Add(accessInfo.CheckLogString[index]);
        if ((flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException && !accessInfo.Result)
          throw new AccessDeniedException((IUserSession) this.UserSession);
        accessResult = this.CheckAccessResult(anAction, accessInfo.Result);
        return true;
      }
    }
    return false;
  }

  protected override int GetCheckAccessHash()
  {
    return this.LCStep.GetHashCode() ^ this.ProjectID.GetHashCode();
  }

  protected override long AccessConditionID
  {
    get
    {
      if (this._accessConditionID == -1L)
      {
        if (MetaDataHelper.GetAttribute4ObjectType(this.ObjectType, this.UserSession.IdentHelper.AttributeAccessCondition) != null)
        {
          IDBAttribute attributeById = this.GetAttributeByID(this.UserSession.IdentHelper.AttributeAccessCondition);
          if (attributeById != null)
            this._accessConditionID = attributeById.AsInteger;
        }
        else
          this._accessConditionID = 0L;
      }
      return this._accessConditionID;
    }
  }

  public override bool EnabledConditionAccess
  {
    get
    {
      return this.ObjectTypeClass.Attributes.GetAttributeByID(this.UserSession.IdentHelper.AttributeAccessCondition) != null;
    }
  }

  public override bool CheckAccess(
    ActionType anAction,
    bool aDefaultAccess,
    CheckAccessFlags flags)
  {
    int accessLevel = this._NewAccessLevel < 0 ? this.AccessLevel : this._NewAccessLevel;
    if ((accessLevel > 0 || (this.ObjectTypeClass.Options & ObjectTypeOptions.MandateAccess) == ObjectTypeOptions.MandateAccess) && !this.UserSession.IsSystemSession)
    {
      bool result;
      switch (this.GetActionCategory(anAction))
      {
        case ActionCategory.Read:
          result = this.UserSession.SecurityLevel >= accessLevel;
          break;
        case ActionCategory.Write:
          result = anAction == ActionType.Delete || anAction == ActionType.Purge || anAction == ActionType.NextLCStep ? this.UserSession.SecurityLevel >= accessLevel : (!ServerConsts.AutomaticAccessLevelUp || !this._isCheckOutMode ? this.UserSession.SecurityLevel == accessLevel : this.UserSession.SecurityLevel >= accessLevel);
          break;
        case ActionCategory.Admin:
          result = this.UserSession.SecurityLevel >= accessLevel;
          break;
        default:
          result = false;
          break;
      }
      if (this.EnableCheckAccessLog)
      {
        this.UserSession.LogList.Add("-");
        this.UserSession.LogList.Add($"{"1"} '{this.UserSession.EventLogHelper.GetActionName(this._CategoryType, this.GetCategoryID4ActionName(this._CategoryID), anAction)}' для объекта '{this.NameInMessages}' с уровнем доступа '{this.UserSession.DBCache.GetAccessCaption(accessLevel)}':");
        this.UserSession.LogList.Add($"{(!result ? (object) "4" : (object) "2")} для сессии с уровнем доступа '{this.UserSession.DBCache.GetAccessCaption(this.UserSession.SecurityLevel)}'");
      }
      if (!result)
      {
        if ((flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException)
        {
          this.UserSession.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(this.UserSession.RealUserID, ActionType.GetAccess, Math.Abs(this.ObjectID), this.ObjectType, anAction, this.UserSession.GetCheckAccessLog(GetAccessModes.LastCheck)));
          throw new AccessDeniedException((IUserSession) this.UserSession);
        }
        return this.CheckAccessResult(anAction, result);
      }
    }
    LCAccessTypes accessType = this.LCStepObject.AccessType;
    if (accessType == LCAccessTypes.NoCheck)
      return this.CheckAccessResult(anAction, true);
    try
    {
      bool accessResult1;
      if (this.CheckAccessInCache(20, anAction, flags, out accessResult1))
        return accessResult1;
      DBSessionable dbSessionable;
      if (anAction == ActionType.NextLCStep)
      {
        dbSessionable = this._NextLCStep;
        if (this._NextLCStep == null)
          throw new KernelException("_NextLCStep is null");
      }
      else
        dbSessionable = this.LCStepObject as DBSessionable;
      bool flag1 = false;
      CheckAccessFlags flags1 = CheckAccessFlags.None;
      if (accessType == LCAccessTypes.CheckAll)
      {
        flag1 = base.CheckAccess(anAction, aDefaultAccess, CheckAccessFlags.None);
        flags1 = CheckAccessFlags.BatchCheck;
        if (this._GrantAlways)
        {
          if (this.UseAccessCache)
          {
            AccessInfo accessResult2 = new AccessInfo(true, false, aDefaultAccess, true, this.UserSession.LogList, this.GetCheckAccessHash());
            ((IDBSecurityCache) this.UserSession.DBSecurity).AddToCache(new CategoryValue(20, Math.Abs(this.ObjectID), anAction), accessResult2);
          }
          return this.CheckAccessResult(anAction, true);
        }
        if (this.IsAccessTypeDeny)
          return this.CheckAccessResult(anAction, this.ReturnAccessTypeDeny((flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException, this.IsLastDefault, anAction));
      }
      bool result;
      bool defaultAccess;
      if (dbSessionable.ActionTypeExists(anAction))
      {
        dbSessionable._CheckAccessSQL = this.GetExtendedAccessSQL();
        dbSessionable._ExtendedUserID = this.GetExtendedUserID();
        (dbSessionable as DBLifecycleStep)._ObjectAccessConditionID = this.AccessConditionID;
        result = dbSessionable.CheckAccess(anAction, aDefaultAccess, flags1);
        if (dbSessionable.IsAccessTypeDeny)
          return this.CheckAccessResult(anAction, this.ReturnAccessTypeDeny((flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException, dbSessionable.IsLastDefault, anAction));
        defaultAccess = dbSessionable._LastDefault;
      }
      else
      {
        defaultAccess = true;
        result = this.ActionTypeExists(anAction) && this.AccessActions[anAction];
      }
      if (this.ProjectSecurity != null && this.ObjectType != this.UserSession.IdentHelper.ProjectsTypeID && anAction != ActionType.DocRegistry)
      {
        bool flag2 = this.ProjectSecurity.CheckAccess(anAction, aDefaultAccess, CheckAccessFlags.BatchCheck);
        bool lastDefault = (this.ProjectSecurity as DBSessionable)._LastDefault;
        if (defaultAccess)
          result = !lastDefault ? flag2 : flag2 | result;
        else if (!lastDefault)
          result |= flag2;
        defaultAccess = lastDefault & defaultAccess;
        if (this.ProjectSecurity.IsAccessTypeDeny)
          return this.CheckAccessResult(anAction, this.ReturnAccessTypeDeny((flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException, this.ProjectSecurity.IsLastDefault, anAction));
      }
      if (accessType == LCAccessTypes.CheckAll)
      {
        bool flag3 = flag1;
        bool flag4 = this._LastDefault;
        if (flag4 && (this.ObjectTypeClass.Options & ObjectTypeOptions.CheckParentAccess) == ObjectTypeOptions.CheckParentAccess && this.ObjectTypeClass.DefaultRelation > 0)
        {
          IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(this.ObjectTypeClass.DefaultRelation);
          IMSRelationType relationType = MetaDataHelper.GetRelationType(this.ObjectTypeClass.DefaultRelation);
          if ((relationType.Options & RelationTypeOptions.EnableCycleRelations) == RelationTypeOptions.EnableCycleRelations)
            throw new KernelExceptionID(410, (object) relationType.Description, (object) this.ObjectTypeClass.ObjectTypeName);
          relationCollection.LocalTypesMode = true;
          (relationCollection as DBRelationCollection).GlobalSelectMode = true;
          DataTable dataTable = relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
          {
            (object) -2
          }), this.ID);
          if (dataTable.Rows.Count > 0 && this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[0][0]), false) is IDBSecurity dbSecurity)
          {
            if (anAction == ActionType.NextLCStep)
              (dbSecurity as DBObject)._NextLCStep = this._NextLCStep;
            bool flag5 = dbSecurity.CheckAccess(anAction, aDefaultAccess, flags);
            if (!dbSecurity.IsLastDefault)
            {
              flag3 = flag5;
              flag4 = false;
              this._LastDefault = false;
              this._LastDeny = dbSecurity.IsAccessTypeDeny;
            }
          }
        }
        if (defaultAccess)
          result = !flag4 ? flag3 : flag3 | result;
        else if (!flag4)
          result |= flag3;
        defaultAccess = flag4 & defaultAccess;
        if (this.IsAccessTypeDeny)
          result = false;
      }
      GetObjectSecurityEventArgs args = new GetObjectSecurityEventArgs((List<IDBSecurity>) null);
      (this.EventHelper as EventLogHelper).OnGetObjectSecurity((IDBObject) this, args, (IUserSession) this.UserSession);
      List<IDBSecurity> securityList = args.SecurityList;
      if (securityList != null)
      {
        for (int index = 0; index < securityList.Count; ++index)
        {
          bool flag6 = securityList[index].CheckAccess(anAction, aDefaultAccess, CheckAccessFlags.BatchCheck);
          bool isLastDefault = securityList[index].IsLastDefault;
          if (!isLastDefault)
          {
            this._LastDefault = false;
            if (securityList[index].IsAccessTypeDeny)
              this._LastDeny = true;
          }
          if (defaultAccess)
            result = !isLastDefault ? flag6 : flag6 | result;
          else if (!isLastDefault)
            result |= flag6;
          defaultAccess = isLastDefault & defaultAccess;
          if (securityList[index].IsAccessTypeDeny)
            return this.CheckAccessResult(anAction, this.ReturnAccessTypeDeny((flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException, securityList[index].IsLastDefault, anAction));
        }
      }
      if (this.UseAccessCache)
      {
        AccessInfo accessResult3 = new AccessInfo(result, this.IsAccessTypeDeny, defaultAccess, this._GrantAlways, this.UserSession.LogList, this.GetCheckAccessHash());
        ((IDBSecurityCache) this.UserSession.DBSecurity).AddToCache(new CategoryValue(20, Math.Abs(this.ObjectID), anAction), accessResult3);
      }
      return result || (flags & CheckAccessFlags.ThrowACException) != CheckAccessFlags.ThrowACException ? this.CheckAccessResult(anAction, result) : throw new AccessDeniedException((IUserSession) this.UserSession);
    }
    catch (AccessDeniedException ex)
    {
      this.UserSession.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(this.UserSession.RealUserID, ActionType.GetAccess, Math.Abs(this.ObjectID), this.ObjectType, anAction, this.UserSession.GetCheckAccessLog(GetAccessModes.LastCheck)));
      this.CheckAccessResult(anAction, false);
      throw;
    }
  }

  internal void UpdateViewValue(string fldName, object newValue)
  {
    if (this.IsCreationMode)
      return;
    string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, this.ObjectType, -1);
    if (updateTables == null)
      return;
    if (this.ViewsUpdaterInited)
    {
      foreach (string viewName in updateTables)
        this.ViewsUpdaterAddValue(viewName, this.ObjectID, "F_OBJECT_ID", newValue, fldName);
    }
    else
    {
      IDbManager dataManager = this.UserSession.DataManager;
      IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) this.ObjectID);
      IDbDataParameter dbDataParameter2 = dataManager.Parameter("newVal", newValue);
      string format = $"UPDATE {{0}} SET {fldName} = :newVal WHERE F_OBJECT_ID = :objID";
      foreach (string str in updateTables)
        dataManager.ExecuteNonQuery(string.Format(format, (object) str), dbDataParameter2, dbDataParameter1);
    }
  }

  internal void DeleteFromView(int objTypeID)
  {
    if (this.IsCreationMode)
      return;
    string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, objTypeID, -1);
    if (updateTables == null)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("objID", (object) this.ObjectID);
    foreach (string str in updateTables)
      dataManager.ExecuteNonQuery($"DELETE FROM {str} WHERE F_OBJECT_ID = :objID", dbDataParameter);
  }

  internal int InsertIntoView(bool createMode, string verType, long checkOutBy)
  {
    string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, this.ObjectType, -1);
    if (updateTables == null)
      throw new Exception(sc_13302.ssp_appserver_13361() + this.ObjectType.ToString());
    IDbManager dataManager = this.UserSession.DataManager;
    string str1 = !createMode ? string.Empty : "-";
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID", (object) this.ObjectID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("caption", (object) this.Caption);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("start_date", this.paramsTable[42]);
    IDbDataParameter dbDataParameter4 = dataManager.Parameter("guid", (object) this.ObjectGUID.ToString());
    IDbDataParameter dbDataParameter5 = dataManager.Parameter("checkOut", (object) checkOutBy);
    foreach (string str2 in updateTables)
      dataManager.ExecuteNonQuery($"INSERT INTO {str2} (F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, F_GUID, CAPTION, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID) SELECT {str1}F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, :checkOut, {verType}, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, :guid, :caption, :start_date, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", dbDataParameter5, dbDataParameter4, dbDataParameter2, dbDataParameter3, dbDataParameter1);
    return updateTables.Length;
  }

  internal void RepairViews()
  {
    if (this.IsCreationMode || this.UserSession.DBCache.GetUpdateTables(-1, this.ObjectType, -1) == null)
      return;
    this.UserSession.StartTransaction();
    try
    {
      this.DeleteFromView(this.ObjectType);
      this.InsertIntoView(false, "F_OBJECT_VER_TYPE", this.CheckoutBy);
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

  public DateTime CreateDate
  {
    get => Convert.ToDateTime(this.paramsTable[42]) + this.UserSession.TimeZoneOffset;
  }

  public bool ReadOnly
  {
    get
    {
      if (this.CheckoutBy != this.UserSession.UserID)
      {
        if (this.CheckoutBy > 0L)
          return !this.UserSession.IsSystemSession;
        if (this.SiteID.Length > 0 && this.ReadonlyPublishedObject(false))
          return true;
        ObjectModifyModes objectModifyMode = this.ObjectModifyMode;
        if (!this.CheckAccess(ActionType.Edit, this.GetDefaultAccess(ActionType.Edit), false))
          return true;
        return this.ObjectID > 0L && objectModifyMode != 0;
      }
      if ((this.ObjectTypeClass.Options & ObjectTypeOptions.MandateAccess) != ObjectTypeOptions.MandateAccess || this.UserSession.IsSystemSession)
        return this.ObjectID > 0L;
      return this.ObjectID > 0L || this.AccessLevel != this.UserSession.SecurityLevel;
    }
  }

  public virtual bool ReadonlyPublishedObject(bool isRelationCheck)
  {
    ISitesCacheService customService = (ISitesCacheService) this.UserSession.GetCustomService(typeof (ISitesCacheService));
    return !isRelationCheck ? SiteIDHelper.IsForeign(customService, this.SiteID) : SiteIDHelper.IsCompositionForeign(customService, this.SiteID);
  }

  public virtual string GetHashFile(
    int versionID,
    X509Certificate2 certificate,
    bool setContent,
    IHashContent hashContent)
  {
    if (HashProcs.SimpleVersion(versionID) < 2)
      return this.GetAttributesHash((IDBAttributable) this, versionID, (HashAlgorithm) null) + this.GetRelationHash(versionID, (HashAlgorithm) null);
    if (HashProcs.SimpleVersion(versionID) >= 2 && HashProcs.SimpleVersion(versionID) < 4)
    {
      using (HashAlgorithm ha = (HashAlgorithm) new SHA1Managed())
      {
        this.GetAttributesHash((IDBAttributable) this, versionID, ha);
        this.GetRelationHash(versionID, ha);
        byte[] bytes = Encoding.Unicode.GetBytes("final");
        ha.TransformFinalBlock(bytes, 0, bytes.Length);
        return Encoding.Unicode.GetString(ha.Hash);
      }
    }
    if (HashProcs.SimpleVersion(versionID) < 4)
      return string.Empty;
    if (setContent)
      hashContent.Clear(HashConsts.Compatible);
    using (HashAlgorithm ha = (HashAlgorithm) new SHA1Managed())
    {
      this.GetAttributesHash((IDBAttributable) this, versionID, ha);
      this.GetRelationHash(versionID, ha);
      byte[] bytes = Encoding.Unicode.GetBytes("final");
      ha.TransformFinalBlock(bytes, 0, bytes.Length);
      return Encoding.Unicode.GetString(ha.Hash);
    }
  }

  protected IDBAttributable GetContentAttributes()
  {
    return (IDBAttributable) this.UserSession.GetObject(this.ObjectID);
  }

  protected string GetAttributesHash(
    IDBAttributable attrObject,
    int hashVersionID,
    HashAlgorithm ha)
  {
    string attributesHash = string.Empty;
    for (int AttrIndex = 0; AttrIndex < attrObject.Attributes.Count; ++AttrIndex)
    {
      IDBAttribute attribute = attrObject.Attributes[AttrIndex];
      if (attribute.AttributeType.IsContent)
      {
        if (attribute.AttributeType.AttributeType != FieldTypes.ftBlob && attribute.AttributeType.AttributeType != FieldTypes.ftFile && attribute.AttributeType.AttributeType != FieldTypes.ftShortBlob)
        {
          for (int index = 0; index < attribute.ValuesCount; ++index)
          {
            attribute.Index = index;
            DateTime asDateTime;
            int hashCode;
            if (!attribute.Value.Equals((object) DBNull.Value))
            {
              if (attribute.DataType == FieldTypes.ftDateTime && HashProcs.SimpleVersion(hashVersionID) > 0)
              {
                if (HashProcs.SimpleVersion(hashVersionID) == 1)
                {
                  asDateTime = attribute.AsDateTime;
                  DateTime universalTime = asDateTime.ToUniversalTime();
                  string str1 = attributesHash;
                  hashCode = universalTime.GetHashCode();
                  string str2 = hashCode.ToString();
                  attributesHash = str1 + str2;
                }
                else if (HashProcs.SimpleVersion(hashVersionID) >= 2)
                {
                  DateTime dateTime;
                  if (HashProcs.SimpleVersion(hashVersionID) == 2)
                  {
                    asDateTime = attribute.AsDateTime;
                    dateTime = asDateTime.ToUniversalTime();
                  }
                  else
                    dateTime = attribute.AsDateTime - attribute.Session.TimeZoneOffset;
                  this.AddBlock(ha, (object) attribute.AttributeType.PropertiesStructure.AttributeGuid);
                  this.AddBlock(ha, (object) dateTime);
                }
              }
              else
              {
                object obj = attribute.Value;
                if (HashProcs.SimpleVersion(hashVersionID) < 2)
                {
                  string str3 = attributesHash;
                  hashCode = obj.GetHashCode();
                  string str4 = hashCode.ToString();
                  attributesHash = str3 + str4;
                }
                else if (HashProcs.SimpleVersion(hashVersionID) >= 2)
                {
                  this.AddBlock(ha, (object) attribute.AttributeType.PropertiesStructure.AttributeGuid);
                  this.AddBlock(ha, obj);
                }
              }
            }
          }
        }
        else if (HashProcs.SimpleVersion(hashVersionID) < 2)
          attributesHash += this.GetBlobHash(attribute, hashVersionID, (HashAlgorithm) null);
        else if (HashProcs.SimpleVersion(hashVersionID) >= 2)
          this.GetBlobHash(attribute, hashVersionID, ha);
      }
    }
    return attributesHash;
  }

  protected string GetBlobHash(IDBAttribute attr, int hashVersionID, HashAlgorithm ha)
  {
    string empty = string.Empty;
    for (int index = 0; index < attr.ValuesCount; ++index)
    {
      attr.Index = index;
      IBlobReader blobReader = attr as IBlobReader;
      BlobInformation blobInformation = blobReader.OpenBlob(-1);
      DateTime dateTime1;
      int hashCode;
      if (HashProcs.SimpleVersion(hashVersionID) == 0)
      {
        dateTime1 = blobInformation.ModifyDate;
        hashCode = dateTime1.GetHashCode();
        empty = hashCode.ToString();
      }
      else if (HashProcs.SimpleVersion(hashVersionID) == 1)
      {
        dateTime1 = blobInformation.ModifyDate;
        dateTime1 = dateTime1.ToUniversalTime();
        hashCode = dateTime1.GetHashCode();
        empty = hashCode.ToString();
      }
      else if (HashProcs.SimpleVersion(hashVersionID) >= 2)
      {
        DateTime dateTime2;
        if (HashProcs.SimpleVersion(hashVersionID) == 2)
        {
          dateTime1 = blobInformation.ModifyDate;
          dateTime2 = dateTime1.ToUniversalTime();
        }
        else
          dateTime2 = blobInformation.ModifyDate - attr.Session.TimeZoneOffset;
        this.AddBlock(ha, (object) dateTime2);
        this.AddBlock(ha, (object) attr.AttributeType.PropertiesStructure.AttributeGuid);
      }
      if (!string.IsNullOrEmpty(blobInformation.FileName))
      {
        string fileName = blobInformation.FileName;
        if (HashProcs.SimpleVersion(hashVersionID) < 2)
          empty += (string) (object) fileName.GetHashCode();
        else if (HashProcs.SimpleVersion(hashVersionID) >= 2)
          this.AddBlock(ha, (object) fileName);
      }
      blobReader.CloseBlob();
    }
    return empty;
  }

  protected string GetRelationHash(int hashVersionID, HashAlgorithm ha)
  {
    string relationHash = string.Empty;
    DataTable applicabilitiesList = this.UserSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, this.ObjectType);
    if (applicabilitiesList != null && applicabilitiesList.Rows.Count > 0)
    {
      foreach (DataRow row1 in (InternalDataCollectionBase) applicabilitiesList.Rows)
      {
        if (Convert.ToBoolean(row1["F_CONTENT"]))
        {
          IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(this.UserSession.GetRelationType(Convert.ToInt32(row1["F_RELATION_TYPE"])).RelationType);
          relationCollection.LocalTypesMode = true;
          DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[4]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID,
            (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
            (object) ObligatoryObjectAttributes.F_OBJ_GUID,
            (object) ObligatoryObjectAttributes.F_PRJ_GUID
          }, new object[1]
          {
            (object) ObligatoryObjectAttributes.F_PRJLINK_ID
          }, new SortOrders[1]{ SortOrders.ASC });
          DataTable dataTable = relationCollection.ConsistFrom(paramSet, this.ObjectID);
          if (dataTable != null && dataTable.Rows.Count > 0)
          {
            foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
            {
              string str1 = Convert.ToString(row2[0]);
              string str2 = Convert.ToString(row2[1]);
              string str3 = Convert.ToString(row2[2]);
              string g = Convert.ToString(row2[3]);
              IDBRelation relation = this.UserSession.GetRelation(new Guid(g), -1L, false);
              if (HashProcs.SimpleVersion(hashVersionID) == 0)
                relationHash = str2 + str1;
              else if (HashProcs.SimpleVersion(hashVersionID) == 1)
              {
                relationHash = g + str3;
                if (relation != null)
                  relationHash += this.GetAttributesHash((IDBAttributable) relation, hashVersionID, (HashAlgorithm) null);
              }
              else if (HashProcs.SimpleVersion(hashVersionID) >= 2)
              {
                this.AddBlock(ha, (object) g);
                this.AddBlock(ha, (object) str3);
                if (relation != null)
                  this.GetAttributesHash((IDBAttributable) relation, hashVersionID, ha);
              }
            }
          }
        }
      }
    }
    return relationHash;
  }

  private void AddBlock(HashAlgorithm ha, object value)
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, value);
      byte[] array = serializationStream.ToArray();
      ha.TransformBlock(array, 0, array.Length, array, 0);
    }
  }

  public virtual int GetHashVersion()
  {
    int hashVersion = 4;
    if (HashConsts.Compatible)
      hashVersion |= 1073741824 /*0x40000000*/;
    return hashVersion;
  }

  public override string[] GetDescriptionsByID(int attributeID, bool throwNotFoundException)
  {
    return this.GetDescriptionsByGuid(new Guid((this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID) ?? throw new KernelExceptionID(sc_13302.ssp_appserver_13362(1041399857), (object) attributeID))["F_GUID"].ToString()), throwNotFoundException);
  }

  public override string[] GetDescriptionsByGuid(Guid guid, bool throwNotFoundException)
  {
    string[] strArray = (string[]) null;
    if (SystemGUIDs.IsSystemGUID(guid))
    {
      switch (guid.ToString())
      {
        case "cad00029-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.ObjectID.ToString()
          };
          break;
        case "cad0002a-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]{ this.ID.ToString() };
          break;
        case "cad0002b-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.LCStepObject.LCName
          };
          break;
        case "cad0002c-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.VersionID.ToString()
          };
          break;
        case "cad0002d-306c-11d8-b4e9-00304f19f545":
          if (this.CheckoutBy == 0L)
          {
            strArray = new string[1]{ string.Empty };
            break;
          }
          QuickObjectInfo objectInfo1 = this.UserSession.GetObjectInfo(this.CheckoutBy);
          if (objectInfo1.Empty)
          {
            strArray = new string[1]{ string.Empty };
            break;
          }
          strArray = new string[1]{ objectInfo1.Caption };
          break;
        case "cad0002e-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.UserSession.GetObjectType(this.ObjectType, throwNotFoundException).ObjectInstanceName
          };
          break;
        case "cad0002f-306c-11d8-b4e9-00304f19f545":
          QuickObjectInfo objectInfo2 = this.UserSession.GetObjectInfo(this.OwnerID);
          if (objectInfo2.Empty)
          {
            strArray = new string[1]{ string.Empty };
            break;
          }
          strArray = new string[1]{ objectInfo2.Caption };
          break;
        case "cad00030-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            MetaDataHelper.GetLCLevelName(this.LevelID)
          };
          break;
        case "cad00031-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            Convert.ToString(this.ModifyDate)
          };
          break;
        case "cad00047-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]{ this.Caption };
          break;
        case "cad0012f-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas)
          };
          break;
        case "cad00130-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.ObjectGUID.ToString()
          };
          break;
        case "cad0013c-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            Convert.ToString(this.CreateDate)
          };
          break;
        case "cad00800-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            ((IDBObject) this).GUID.ToString()
          };
          break;
        case "cad00811-306c-11d8-b4e9-00304f19f545":
          if (this.ProjectID > 0L)
          {
            QuickObjectInfo objectInfo3 = this.UserSession.GetObjectInfo(this.ProjectID);
            if (objectInfo3.Empty)
            {
              strArray = new string[1]{ string.Empty };
              break;
            }
            strArray = new string[1]{ objectInfo3.Caption };
            break;
          }
          strArray = new string[1]{ string.Empty };
          break;
        case "cad014d2-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.ModificationID.ToString()
          };
          break;
        case "cad01501-306c-11d8-b4e9-00304f19f545":
          string sitesCodes = this.SiteID.Trim();
          if (sitesCodes != string.Empty)
            sitesCodes = SiteIDHelper.GetCaption((ISitesCacheService) this.UserSession.GetCustomService(typeof (ISitesCacheService)), sitesCodes);
          strArray = new string[1]{ sitesCodes };
          break;
        case "cadd959f-306c-11d8-b4e9-00304f19f545":
          strArray = new string[1]
          {
            this.UserSession.DBCache.GetAccessCaption(this.AccessLevel)
          };
          break;
        case "cadd96b7-306c-11d8-b4e9-00304f19f545":
          QuickObjectInfo objectInfo4 = this.UserSession.GetObjectInfo(this.CreatorID);
          if (objectInfo4.Empty)
          {
            strArray = new string[1]{ string.Empty };
            break;
          }
          strArray = new string[1]{ objectInfo4.Caption };
          break;
      }
    }
    if (strArray == null)
    {
      IDBAttribute byGuid = this.Attributes.FindByGUID(guid);
      if (byGuid != null)
        strArray = byGuid.Descriptions;
    }
    return !throwNotFoundException || strArray != null ? strArray : throw new AttributeNotFoundException(string.Empty, guid.ToString(), this.ObjectID);
  }

  public override object[] GetValuesByName(string attributeName, bool throwNotFoundException)
  {
    int attributeId = this.UserSession.EventLogHelper.GetAttributeID((object) attributeName, throwNotFoundException);
    if (attributeId != -1)
      return this.GetValuesByID(attributeId, throwNotFoundException);
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13363(1087493380), (object) attributeName);
    return (object[]) null;
  }

  public override object[] GetValuesByID(int attributeID, bool throwNotFoundException)
  {
    DataRow dataRow = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID);
    if (dataRow != null)
      return this.GetValuesByGuid(new Guid(dataRow["F_GUID"].ToString()), throwNotFoundException);
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13364(295163261), (object) attributeID);
    return (object[]) null;
  }

  public override object[] GetValuesByGuid(Guid guid, bool throwNotFoundException)
  {
    this.CheckDeleted(nameof (GetValuesByGuid));
    object[] objArray = (object[]) null;
    if (SystemGUIDs.IsSystemGUID(guid))
    {
      switch (guid.ToString())
      {
        case "cad00029-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.ObjectID };
          break;
        case "cad0002a-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.ID };
          break;
        case "cad0002b-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.LCStep };
          break;
        case "cad0002c-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.VersionID
          };
          break;
        case "cad0002d-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.CheckoutBy
          };
          break;
        case "cad0002e-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.ObjectType
          };
          break;
        case "cad0002f-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.OwnerID };
          break;
        case "cad00030-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.LevelID };
          break;
        case "cad00031-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.ModifyDate
          };
          break;
        case "cad00047-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.Caption };
          break;
        case "cad0012f-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.SubjectAreas
          };
          break;
        case "cad00130-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.ObjectGUID
          };
          break;
        case "cad0013c-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.CreateDate
          };
          break;
        case "cad00800-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) ((IDBObject) this).GUID
          };
          break;
        case "cad00811-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.ProjectID
          };
          break;
        case "cad014d2-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.ModificationID
          };
          break;
        case "cad014d3-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.IsBaseVersion
          };
          break;
        case "cad01501-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]{ (object) this.SiteID };
          break;
        case "cadd959f-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.AccessLevel
          };
          break;
        case "cadd96b7-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.CreatorID
          };
          break;
        case "cadd9717-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.ParentVersionID
          };
          break;
        case "cadd98e9-306c-11d8-b4e9-00304f19f545":
          objArray = new object[1]
          {
            (object) this.VersionsCount
          };
          break;
      }
    }
    if (objArray == null)
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(guid, throwNotFoundException);
      if (attributeByGuid != null)
        objArray = attributeByGuid.Values;
    }
    return !throwNotFoundException || objArray != null ? objArray : throw new AttributeNotFoundException(string.Empty, guid.ToString(), this.ObjectID);
  }

  public Guid ObjectGUID
  {
    get => this.VersionGUID;
    set
    {
      if (!(this.VersionGUID != value))
        return;
      if (!this.UserSession.CanChangeObject(2, (object) this.ObjectID))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_931"), (object) DataSetProcessor.GetCaption("F_GUID")));
      this.VersionGUID = value;
    }
  }

  Guid IDBObject.GUID
  {
    get
    {
      if (this._ObjectGUID == null)
      {
        IDbManager dataManager = this.UserSession.DataManager;
        object obj = dataManager.ExecuteScalar("SELECT F_GUID FROM IMS_GUID_RESOLVE WHERE F_ID = :id AND F_CATEGORY_TYPE = :typ", dataManager.Parameter("id", (object) this.ID), dataManager.Parameter("typ", (object) 2));
        if (obj == null || obj == DBNull.Value)
        {
          obj = (object) Guid.NewGuid();
          dataManager.ExecuteNonQuery("INSERT INTO IMS_GUID_RESOLVE (F_GUID, F_ID, F_CATEGORY_TYPE) VALUES (:guid, :id, :typ)", dataManager.Parameter("guid", (object) obj.ToString()), dataManager.Parameter("id", (object) this.ID), dataManager.Parameter("typ", (object) 2));
        }
        else if (!(obj is Guid))
          obj = (object) new Guid(obj.ToString());
        this._ObjectGUID = obj;
      }
      return (Guid) this._ObjectGUID;
    }
    set
    {
      IDbManager dataManager = this.UserSession.DataManager;
      object obj = dataManager.ExecuteScalar("SELECT F_GUID FROM IMS_GUID_RESOLVE WHERE F_ID = :id AND F_CATEGORY_TYPE = :typ", dataManager.Parameter("id", (object) this.ID), dataManager.Parameter("typ", (object) 2));
      bool flag = obj == null || obj == DBNull.Value;
      if (!flag && !(obj is Guid))
        obj = (object) new Guid(obj.ToString());
      if (!flag && !((Guid) obj != value))
        return;
      try
      {
        this.CheckEditMode(true, true, false);
      }
      catch
      {
        this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_418") + value.ToString());
        throw;
      }
      string str1 = !flag ? value.ToString() : "NULL";
      long EventID = this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessGranted, LocalizationHolder.rm.GetString("Kernel_419") + str1);
      try
      {
        dataManager.Parameter("id", (object) this.ID);
        if (flag)
          dataManager.ExecuteNonQuery("INSERT INTO IMS_GUID_RESOLVE (F_GUID, F_ID, F_CATEGORY_TYPE) VALUES (:guid, :id, :typ)", dataManager.Parameter("guid", (object) value.ToString()), dataManager.Parameter("id", (object) this.ID), dataManager.Parameter("typ", (object) 2));
        else
          dataManager.ExecuteNonQuery("UPDATE IMS_GUID_RESOLVE SET F_GUID = :guid WHERE F_ID = :id AND F_CATEGORY_TYPE = :typ", dataManager.Parameter("guid", (object) value.ToString()), dataManager.Parameter("id", (object) this.ID), dataManager.Parameter("typ", (object) 2));
        this._ObjectGUID = (object) value;
      }
      catch (Exception ex)
      {
        string str2 = LocalizationHolder.rm.GetString("Kernel_420") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str2);
        throw new KernelException(str2, ex);
      }
    }
  }

  public int TypeID => this.ObjectType;

  public void SetModifyContentDate()
  {
    if (!(this.GetAttributeByID(this.UserSession.IdentHelper.ModifyContentDateID) is DBDateAttribute attributeById))
      return;
    attributeById.WriteContentDate();
  }

  protected virtual void SetPublicationFlag()
  {
    this.DoSetPublicationFlag(PublicationNecessary.Object);
  }

  internal void DoSetPublicationFlag(PublicationNecessary publicationNecessary)
  {
    if (MetaDataHelper.GetAttribute4ObjectType(this.ObjectType, this.UserSession.IdentHelper.AttributePublicationNecessary) == null)
      return;
    IDBAttribute attributeById = this.GetAttributeByID(this.UserSession.IdentHelper.AttributePublicationNecessary);
    if (attributeById == null || (int) attributeById.AsInteger == 3)
      return;
    if ((int) attributeById.AsInteger != 0)
    {
      switch (publicationNecessary)
      {
        case PublicationNecessary.Object:
          break;
        case PublicationNecessary.FCAttributes:
          if ((int) attributeById.AsInteger == 1)
            return;
          break;
        default:
          return;
      }
    }
    attributeById.AsInteger = (long) publicationNecessary;
  }

  public virtual bool isParentType(Guid guid)
  {
    IDBObjectType objectType = this.UserSession.GetObjectType(guid, true);
    if (objectType.ObjectType == this.ObjectType)
      return true;
    for (int objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(this.ObjectType); objectTypeParentId > -1; objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objectTypeParentId))
    {
      if (objectTypeParentId == objectType.ObjectType)
        return true;
    }
    return false;
  }

  public long ParentVersionID
  {
    get
    {
      if (this._ParentVersionID != 0L)
        return this._ParentVersionID;
      if (this.ObjectTypeClass.Versionable != ObjectVersionModes.MultiVersion)
      {
        this._ParentVersionID = -1L;
      }
      else
      {
        object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_PARENT_ID FROM IMS_VERSIONS_TREE WHERE F_OBJECT_ID = :id", this.UserSession.DataManager.Parameter("id", (object) Math.Abs(this.ObjectID)));
        this._ParentVersionID = obj == null || obj == DBNull.Value ? -1L : Convert.ToInt64(obj);
      }
      return this._ParentVersionID;
    }
  }

  public override IDBSecurity[] GetRelatedSecurity()
  {
    GetObjectSecurityEventArgs args = new GetObjectSecurityEventArgs((List<IDBSecurity>) null);
    args.SetAccessMode = true;
    (this.EventHelper as EventLogHelper).OnGetObjectSecurity((IDBObject) this, args, (IUserSession) this.UserSession);
    List<IDBSecurity> dbSecurityList;
    if (args.SecurityList != null)
    {
      dbSecurityList = args.SecurityList;
      for (int index = dbSecurityList.Count - 1; index >= 0; --index)
      {
        if (!dbSecurityList[index].CheckAccess(ActionType.GetAccess, this.UserSession.IsAdmin, false))
          dbSecurityList.RemoveAt(index);
      }
    }
    else
      dbSecurityList = new List<IDBSecurity>();
    if (this.ProjectID > 0L && (this.UserSession.GetObject(this.ProjectID) as IDBSecurity).CheckAccess(ActionType.GetAccess, this.UserSession.IsAdmin, false))
      dbSecurityList.Add(this.ProjectSecurity);
    if ((this.ObjectTypeClass as DBSessionable).CheckAccess(ActionType.GetAccess, this.UserSession.IsAdmin, false))
      dbSecurityList.Add(this.LCStepObject as IDBSecurity);
    return dbSecurityList.ToArray();
  }

  public override long HistoryObjectID => this.ID;

  public override bool MustCheckValidatingRule
  {
    get => !this.IsCreationMode && this._MustCheckValidatingRule;
  }

  public override IDBAttributeType GetAttributeType(int attributeID)
  {
    return (IDBAttributeType) this.ObjectTypeClass.Attributes.GetAttributeByID(attributeID, false) ?? this.UserSession.GetAttributeType(attributeID);
  }

  public virtual long ProjectID
  {
    get => Convert.ToInt64(this.paramsTable[24]);
    set
    {
      if (this.ProjectID == value)
        return;
      try
      {
        if (value > 0L)
        {
          if (this.UserSession.GetObject(value) is DBProjectObject project)
          {
            if ((this.ObjectTypeClass.Options & ObjectTypeOptions.MandateAccess) == ObjectTypeOptions.MandateAccess)
            {
              int int32 = Convert.ToInt32(project.GetAttributeByID(this.UserSession.IdentHelper.SecurityLevelID).AsInteger);
              if (int32 != this.AccessLevel)
                this._NewAccessLevel = int32;
            }
            if (!this.UserSession.IsSystemSession && !project.IsProjectParticipant())
              throw new KernelExceptionID(sc_13302.ssp_appserver_13365(1074955712), (object) project.Caption);
            new ProjectDBSecurity(this.UserSession, project, this, false).CheckAccess(ActionType.Create, true);
          }
          else
            throw new KernelExceptionID(sc_13302.ssp_appserver_13366(545738123), (object) this.UserSession.GetObject(value).NameInMessages, (object) value).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(value));
        }
        if (!this.IsCreationMode)
        {
          this.CheckoutByOther(true);
          if (value < 0L)
            value = -value;
          try
          {
            this.CheckAccess(ActionType.Edit, this.GetDefaultAccess(ActionType.Edit));
          }
          catch
          {
            this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessDenied);
            throw;
          }
          this.AddEvent(this.ObjectID, ActionType.Edit, EventlogRecordType.AccessGranted);
          this.CheckChangeEnable("F_PROJECT_ID");
          if (this.ProjectID > 0L && this.UserSession.GetObject(this.ProjectID) is DBProjectObject dbProjectObject)
          {
            if (!dbProjectObject.IsProjectParticipant())
              throw new KernelExceptionID(sc_13302.ssp_appserver_13367(1000575704), (object) dbProjectObject.Caption);
            this.ProjectSecurity.CheckAccess(ActionType.Remove, true);
            if (value == 0L)
              this.UserSession.GetRelation(dbProjectObject.ObjectID, this.ID)?.Delete(this.ValidationRulesOn ? 0L : (long) Consts.PurgeMode);
          }
        }
        this.UserSession.StartTransaction();
        try
        {
          if (this._NewAccessLevel >= 0)
            this.SetAccessLevel(this._NewAccessLevel, (List<long>) null);
          this.SetProjectID(value);
          this.UserSession.Commit();
        }
        catch
        {
          this.UserSession.Rollback();
          throw;
        }
      }
      finally
      {
        this._NewAccessLevel = -1;
      }
    }
  }

  private void FireObligatoryAttributeWrite(
    ObligatoryObjectAttributes attrID,
    int columnID,
    object value)
  {
    ObligatoryAttributeValueEventArgs args = new ObligatoryAttributeValueEventArgs(this.paramsTable[columnID], value, (IUserSession) this.UserSession);
    (this.EventHelper as EventLogHelper).OnObligatoryAttributeWrite((IDBObject) this, attrID, args);
  }

  internal void SetProjectID(long value)
  {
    this.FireObligatoryAttributeWrite(ObligatoryObjectAttributes.F_PROJECT_ID, 24, (object) value);
    if (ServerConsts.CopyProjectVisibility && value > 0L)
    {
      IDBObject projObject = this.UserSession.GetObject(value);
      if (projObject.GetAttributeByID(ObjectsVisibilityHelper.AttrVisibilityId) != null && MetaDataHelper.GetAttribute4ObjectType(this.ObjectType, ObjectsVisibilityHelper.AttrVisibilityId) != null)
      {
        ICacheDataset dbCache = this.UserSession.DBCache;
        if (dbCache.IsArticle(this.ObjectType) || dbCache.IsDocument(this.ObjectType) || dbCache.IsProduct(this.ObjectType))
        {
          IDBObject arcObject = (IDBObject) null;
          if (ServerConsts.CopyArcVisibility)
          {
            IDBAttribute attributeByGuid = this.GetAttributeByGuid(SystemGUIDs.attributeArchive);
            if (attributeByGuid != null && !attributeByGuid.IsNull && attributeByGuid.AsInteger > 0L)
              arcObject = this.UserSession.GetObject(attributeByGuid.AsInteger, false);
          }
          ObjectsVisibilityHelper.SetArcProjVisibility((IDBObject) this, arcObject, projObject);
        }
      }
    }
    this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_PROJECT_ID = :prjID WHERE F_OBJECT_ID = :objID OR F_OBJECT_ID = :m_objID", this.UserSession.DataManager.Parameter("prjID", (object) value), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("m_objID", (object) -this.ObjectID));
    this.UpdateViewValue("F_PROJECT_ID", (object) value);
    this.paramsTable[24] = (object) value;
    if (this.CheckoutBy != 0L && !this.IsCreationMode && this.UserSession.GetObject(-this.ObjectID) is DBObject dbObject)
      dbObject.UpdateViewValue("F_PROJECT_ID", (object) value);
    this._ProjectSecurity = (IDBSecurity) null;
    this.RecalcAttributes(-14);
  }

  protected internal virtual IDBSecurity ProjectSecurity
  {
    get
    {
      if (this.ProjectID > 0L && this._ProjectSecurity == null && this.UserSession.GetObject(this.ProjectID) is DBProjectObject project)
        this._ProjectSecurity = (IDBSecurity) new ProjectDBSecurity(this.UserSession, project, this, false);
      return this._ProjectSecurity;
    }
  }

  protected internal virtual bool ReadOnlyProjectID() => this.CheckoutByOther(false);

  protected override IDBSecurity GetSecurityByID(long categoryID)
  {
    return this.UserSession.GetObject(categoryID) as IDBSecurity;
  }

  public override string SecurityCollectionName
  {
    get => LocalizationHolder.rm.GetString(nameof (SecurityCollectionName));
  }

  public override bool IsCompatibleElements(long[] categoryID)
  {
    this.CheckCategoryArray(categoryID);
    bool flag = true;
    Type type1 = this.UserSession.GetObject(categoryID[0]).GetType();
    for (int index = 1; index < categoryID.Length; ++index)
    {
      Type type2 = this.UserSession.GetObject(categoryID[index]).GetType();
      if (type1 != type2)
        return false;
    }
    return flag;
  }

  internal void BeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
    this.DoBeforeDeleteRelation(relation, deleteMode);
  }

  protected virtual void DoBeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
  }

  public virtual void DoBeforeCreateRelation(
    DBRelationCollection dBRelationCollection,
    long partID,
    long partObjectID,
    long prjlinkID,
    IDBRelation prototype)
  {
  }

  public virtual void DoAfterCreateRelation(IDBRelation newrelation)
  {
  }

  public void Print()
  {
    this.AddEvent(this.ObjectID, ActionType.Print, EventlogRecordType.Information);
    (this.EventHelper as EventLogHelper).OnBeforeObjectPrint((IDBObject) this, (IUserSession) this.UserSession);
  }

  public void SaveToDisk()
  {
    (this.EventHelper as EventLogHelper).OnBeforeObjectSaveToDiskEvent((IDBObject) this, (IUserSession) this.UserSession);
  }

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(2, (object) this.ObjectID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_931"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  public DataTable GetEventsList(DBRecordSetParams paramSet, bool translateValues)
  {
    return this.GetEventsList(paramSet, translateValues, false);
  }

  public DataTable GetEventsList(
    DBRecordSetParams paramSet,
    bool translateValues,
    bool archiveMode)
  {
    this.CheckAccess(ActionType.GetAccess);
    paramSet.Conditions = ConditionStructure.Join(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.Equal, (object) Math.Abs(this.ObjectID), LogicalOperators.NONE, 0, true)
    }, paramSet.Conditions ?? new ConditionStructure[0]);
    return new EventLog(this.UserSession, archiveMode)
    {
      _NeedSelectCheckAccess = false
    }.Select(paramSet, translateValues);
  }

  public virtual bool AppendVersionToContext()
  {
    return this.ModificationID == 0L && MetaDataHelper.MustAppendVersionToEditingContext((IUserSession) this.UserSession, this.ObjectType, new Func<EditingContextMode>(this.GetEditingContextMode));
  }

  public long ModificationID
  {
    get => Convert.ToInt64(this.paramsTable[175]);
    internal set => this.SetModificationID(value);
  }

  internal void SetModificationID(long modificationID)
  {
    this.SetModificationID(modificationID, true);
  }

  internal void SetModificationID(long modificationID, bool checkRules)
  {
    if (modificationID == this.ModificationID)
      return;
    modificationID = Math.Abs(modificationID);
    if (checkRules)
      this.ValidateStepRules(modificationID);
    ActionType eventType;
    string note;
    if (modificationID == 0L)
    {
      eventType = ActionType.DeleteLink;
      note = "Версия объекта исключена из группы изменений номер " + (object) this.ModificationID;
    }
    else
    {
      eventType = ActionType.AddLink;
      note = "Версия объекта включена в группу изменений номер " + (object) modificationID;
    }
    this.UserSession.StartTransaction();
    try
    {
      if ((this.AttributesState & Consts.AssignValuesMode) == 0)
        this.UserSession.AddDelayedNotification((DelayedNotification) new AttributeValueWriteDelayedNotification(this.UserSession.RealUserID, ActionType.Write, this.GetAttributes4Notification((DBAttribute) null), (AttributeValues[]) null, Math.Abs(this.ObjectID), this.ObjectType, (object) modificationID, -15, 0));
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_MODIFICATION_ID = :modID WHERE F_OBJECT_ID = :objID OR F_OBJECT_ID = :m_objID", this.UserSession.DataManager.Parameter("modID", (object) modificationID), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("m_objID", (object) -this.ObjectID));
      this.UpdateViewValue("F_MODIFICATION_ID", (object) modificationID);
      this.paramsTable[175] = (object) modificationID;
      if (this.CheckoutBy != 0L && !this.IsCreationMode && this.UserSession.GetObject(-this.ObjectID) is DBObject dbObject)
        dbObject.UpdateViewValue("F_MODIFICATION_ID", (object) modificationID);
      this.AddEvent(this.ObjectID, eventType, EventlogRecordType.AccessGranted, note);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  private void ValidateStepRules(long modifyID)
  {
    bool flag1 = (this.LCStepObject.Options & LCStepOptions.DisableParallelVersions) == LCStepOptions.DisableParallelVersions;
    bool flag2 = (this.LCStepObject.Options & LCStepOptions.DisableContextParallelVersions) == LCStepOptions.DisableContextParallelVersions;
    if (flag2 & flag1)
    {
      this.ValidateStepRulesInternal(string.Empty);
    }
    else
    {
      if (modifyID == 0L && flag1)
        this.ValidateStepRulesInternal(" AND F_MODIFICATION_ID = 0");
      if (!flag2)
        return;
      this.ValidateStepRulesInternal(" AND F_MODIFICATION_ID <> 0");
    }
  }

  private void ValidateStepRulesInternal(string modificationSQL)
  {
    object obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_LC_STEP = :lcStep AND F_OBJECT_VER_TYPE <> -1 AND F_OBJECT_ID <> :objID AND F_OBJECT_ID <> :objID1" + modificationSQL, this.UserSession.DataManager.Parameter("id1", (object) this.ID), this.UserSession.DataManager.Parameter("lcStep", (object) this.LCStep), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("objID1", (object) -this.ObjectID));
    if (obj == null || obj == DBNull.Value)
      return;
    IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(obj));
    if (modificationSQL.IndexOf('=') < 0)
      throw new KernelException(string.Format("Ошибка создания версии объекта '{0}' или исключения версии из контекста: в базе уже существует версия данного объекта (ид. версии объекта {2}, владелец {3}) на первом шаге схемы '{1}', а схема не допускает наличия более 1 контекстной версии объекта на этом шаге.", (object) dbObject.NameInMessages, (object) this.UserSession.GetLCSchema(this.LCStepObject.SchemaID).Name, (object) dbObject.ObjectID, (object) this.UserSession.GetObjectInfo(dbObject.OwnerID).Caption));
    throw new KernelExceptionID(sc_13302.ssp_appserver_13368(2058490103), (object) dbObject.NameInMessages, (object) this.UserSession.GetLCSchema(this.LCStepObject.SchemaID).Name, (object) $"(ид. версии объекта {dbObject.ObjectID}, владелец {this.UserSession.GetObjectInfo(dbObject.OwnerID).Caption})").WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
  }

  protected string AttributesTableName
  {
    get
    {
      return this.ObjectTypeClass.IsLocalType ? (this.ObjectTypeClass as DBObjectType).AttributesTableName : "IMS_OBJECT_ATTRS";
    }
  }

  public bool IsBaseVersion => Convert.ToInt64(this.paramsTable[177]) != 0L;

  public void MakeBaseVersion()
  {
    if (this.IsBaseVersion)
      return;
    try
    {
      this.CheckAccess(ActionType.ChangeBaseVersion, this.GetDefaultAccess(ActionType.ChangeBaseVersion));
    }
    catch
    {
      this.AddEvent(this.ObjectID, ActionType.ChangeBaseVersion, EventlogRecordType.AccessDenied);
      throw;
    }
    this.AddEvent(this.ObjectID, ActionType.ChangeBaseVersion, EventlogRecordType.AccessGranted);
    this.CheckChangeEnable("F_BASE_VERSION");
    this.SetBaseVersion();
  }

  internal void SetBaseVersion()
  {
    if (this.IsBaseVersion)
      return;
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_BASE_VERSION <> 0", this.UserSession.DataManager.Parameter("id1", (object) this.ID));
    this.UserSession.StartTransaction();
    try
    {
      this.DoBeforeMakeBaseVersion(0);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if (this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false) is DBObject dbObject)
          dbObject.SetBaseVersion(0L);
      }
      this.SetBaseVersion(1L);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  protected virtual void DoBeforeMakeBaseVersion(int flags)
  {
  }

  internal void SetSystemField(string fieldName, int columnNumber, object newValue)
  {
    this.UserSession.StartTransaction();
    try
    {
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_OBJECTS SET {fieldName} = :newValue WHERE F_OBJECT_ID = :objID OR F_OBJECT_ID = :m_objID", this.UserSession.DataManager.Parameter(nameof (newValue), newValue), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("m_objID", (object) -this.ObjectID));
      this.UpdateViewValue(fieldName, newValue);
      this.paramsTable[columnNumber] = newValue;
      if (this.CheckoutBy != 0L && !this.IsCreationMode && this.UserSession.GetObject(-this.ObjectID) is DBObject dbObject)
        dbObject.UpdateViewValue(fieldName, newValue);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public void SetVersionID(int verID)
  {
    IDbManager dbManager = this.UserSession.IsSystemSession ? this.UserSession.DataManager : throw new KernelExceptionID(sc_13302.ssp_appserver_13369(1141062189));
    DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id99 AND F_VERSION_ID = :verID", dbManager.Parameter("id99", (object) this.ID), dbManager.Parameter(nameof (verID), (object) verID));
    if (dataTable.Rows.Count > 0)
      throw new KernelException($"Нельзя изменить номер версии объекта '{this.NameInMessages}', т.к. в базе уже есть версия {dataTable.Rows[0][0]} c номером {verID}");
    int versionId = this.VersionID;
    this.SetSystemField("F_VERSION_ID", 120, (object) verID);
    this.AddEvent(this.ObjectID, ActionType.Write, EventlogRecordType.AccessGranted, $"Изменение номера версии объекта с {versionId} на {verID}");
  }

  internal void SetBaseVersion(long baseVer)
  {
    this.UserSession.StartTransaction();
    try
    {
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_BASE_VERSION = :verID WHERE F_OBJECT_ID = :objID OR F_OBJECT_ID = :m_objID", this.UserSession.DataManager.Parameter("verID", (object) baseVer), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("m_objID", (object) -this.ObjectID));
      this.UpdateViewValue("F_BASE_VERSION", (object) baseVer);
      this.paramsTable[177] = (object) baseVer;
      if (this.CheckoutBy != 0L && !this.IsCreationMode && this.UserSession.GetObject(-this.ObjectID) is DBObject dbObject)
        dbObject.UpdateViewValue("F_BASE_VERSION", (object) baseVer);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  internal void SetCreateDate(DateTime createDate)
  {
    this.UserSession.StartTransaction();
    try
    {
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_OBJ_CREATE = :createDate WHERE F_OBJECT_ID = :objID OR F_OBJECT_ID = :m_objID", this.UserSession.DataManager.Parameter(nameof (createDate), (object) createDate), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("m_objID", (object) -this.ObjectID));
      this.UpdateViewValue("F_OBJ_CREATE", (object) createDate);
      this.paramsTable[42] = (object) createDate;
      if (this.CheckoutBy != 0L && !this.IsCreationMode && this.UserSession.GetObject(-this.ObjectID) is DBObject dbObject)
        dbObject.UpdateViewValue("F_OBJ_CREATE", (object) createDate);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  private void ValidateDeleteBaseVersion()
  {
    if (this.ObjectID > 0L && this.IsBaseVersion)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13370(1578038745), (object) this.NameInMessages, (object) this.ObjectID);
  }

  public string SiteID => this.paramsTable[178].ToString();

  public virtual void SetSiteID(string siteID)
  {
    if (!(siteID != this.SiteID))
      return;
    int num = siteID != string.Empty ? 1 : 0;
    this.UserSession.StartTransaction();
    try
    {
      this.UserSession.DataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_SITE_ID = :siteID WHERE F_OBJECT_ID = :objID OR F_OBJECT_ID = :m_objID", this.UserSession.DataManager.Parameter(nameof (siteID), (object) siteID), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID), this.UserSession.DataManager.Parameter("m_objID", (object) -this.ObjectID));
      this.UpdateViewValue("F_SITE_ID", (object) siteID);
      this.paramsTable[178] = (object) siteID;
      if (this.CheckoutBy != 0L && !this.IsCreationMode && this.UserSession.GetObject(-this.ObjectID) is DBObject dbObject)
        dbObject.UpdateViewValue("F_SITE_ID", (object) siteID);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  internal void BeforeRestoreSnapshot(IDBObjectSnapshot sender)
  {
    (this.EventHelper as EventLogHelper).OnBeforeRestoreSnapshot(sender, (IDBObject) this);
    this.DoBeforeRestoreSnapshot(sender);
  }

  internal void AfterRestoreSnapshot(IDBObjectSnapshot sender)
  {
    (this.EventHelper as EventLogHelper).OnAfterRestoreSnapshot(sender, (IDBObject) this);
    this.DoAfterRestoreSnapshot(sender);
  }

  protected virtual void DoBeforeRestoreSnapshot(IDBObjectSnapshot sender)
  {
  }

  protected virtual void DoAfterRestoreSnapshot(IDBObjectSnapshot sender)
  {
  }

  internal void AfterCreateSnapshot(
    IDBSnapshotCollection sender,
    long snapshotID,
    string snapshotName,
    string FiltrationOwnerID,
    List<long> lst)
  {
    this.DoAfterCreateSnapshot(sender, snapshotID, snapshotName, FiltrationOwnerID, lst);
  }

  protected virtual void DoAfterCreateSnapshot(
    IDBSnapshotCollection sender,
    long snapshotID,
    string snapshotName,
    string FiltrationOwnerID,
    List<long> lst)
  {
  }

  protected virtual void DoBeforeRemoveObject(DBRelation dBRelation, long newProjID)
  {
  }

  internal void BeforeRemoveObject(DBRelation dBRelation, long newProjID)
  {
    this.DoBeforeRemoveObject(dBRelation, newProjID);
  }

  public DateTime GetCheckOutDate()
  {
    object obj = this.guidTable.Rows[0]["F_CHECKOUT_DATE"];
    return obj == DBNull.Value || obj == null ? DateTime.MinValue : Convert.ToDateTime(obj) + this.UserSession.TimeZoneOffset;
  }

  public virtual void SetRelationsAttributes(RelationAttributeValues[] relValues)
  {
    for (int index = 0; index < relValues.Length; ++index)
    {
      DBRelation relation = this.UserSession.GetRelation(relValues[index].RelationID, true) as DBRelation;
      relation._PartObjectID = relValues[index].PartObjectID;
      relation._ProjObject = (IDBObject) this;
      relation.SetAttributesValues(relValues[index].Values);
    }
  }

  public DataTable GetObjectLinks(int attributeID)
  {
    string str = attributeID <= 0 ? string.Empty : " AND F_ATTRIBUTE_ID = " + attributeID.ToString();
    return this.UserSession.DataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID FROM IMS_OBJECT_LINKS O1 WHERE F_TOOBJECT_ID = :to_objID{str} UNION ALL SELECT F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID FROM IMS_ID_LINKS WHERE F_TO_ID = :to_ID{str}", this.UserSession.DataManager.Parameter("to_objID", (object) Math.Abs(this.ObjectID)), this.UserSession.DataManager.Parameter("to_ID", (object) this.ID));
  }

  public void RemoveFromStep()
  {
    if (this.CheckoutBy != 0L)
      throw new KernelExceptionID(426, (object) this.NameInMessages, (object) this.ObjectID, (object) this.UserSession.GetObjectInfo(this.CheckoutBy).Caption).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(this.ObjectID));
    int autoTransferStepId = this.LCStepObject.AutoTransferStepID;
    if (autoTransferStepId <= 0)
      throw new KernelExceptionID(sc_13302.ssp_appserver_13372(1098140654), (object) this.NameInMessages, (object) this.UserSession.GetLifecycleStep(autoTransferStepId).LCName);
    int lcStep = this.LCStep;
    this.LCStep = autoTransferStepId;
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cadd9597-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null)
      return;
    IDBObject prevObjectId = this.GetPrevObjectID(attributeByGuid.AsInteger, attributeByGuid.AsInteger);
    if (prevObjectId == null || prevObjectId.ID != this.ID)
      return;
    prevObjectId.LCStep = lcStep;
  }

  private IDBObject GetPrevObjectID(long objID, long firstObjID)
  {
    if (objID <= 0L)
      return (IDBObject) null;
    IDBObject prevObjectId = this.UserSession.GetObject(objID, false);
    if (prevObjectId != null)
    {
      IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545"));
      relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad00349-306c-11d8-b4e9-00304f19f545");
      DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      }), prevObjectId.ObjectID, prevObjectId.ID);
      if (dataTable.Rows.Count > 0)
      {
        IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[0][0]), false);
        if (dbObject != null)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeID((object) "cadd9562-306c-11d8-b4e9-00304f19f545"));
          if (attributeById != null && !attributeById.IsNull && attributeById.AsDateTime < DateTime.Now.Date)
          {
            IDBAttribute attributeByGuid = prevObjectId.GetAttributeByGuid(new Guid("cadd9597-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid != null && !attributeByGuid.IsNull)
              return attributeByGuid.AsInteger != firstObjID ? this.GetPrevObjectID(attributeByGuid.AsInteger, firstObjID) : (IDBObject) null;
          }
        }
      }
    }
    return prevObjectId;
  }

  public int ConnectToObject(long toObjectID)
  {
    if (!this.IsCreationMode)
      throw new KernelException(sc_13302.ssp_appserver_13373());
    IDbManager dataManager = this.UserSession.DataManager;
    IDBObject dbObject = this.UserSession.GetObject(toObjectID);
    if (this.UserSession.GetObjectType(dbObject.ObjectType).Versionable != ObjectVersionModes.MultiVersion)
      throw new KernelException(string.Format(sc_13302.ssp_appserver_13374(), (object) dbObject.NameInMessages));
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("newID", (object) dbObject.ID);
    int num = Convert.ToInt32(dataManager.ExecuteScalar("SELECT MAX(F_VERSION_ID) FROM IMS_OBJECTS WHERE F_ID = :newID", dbDataParameter1)) + 1;
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("oldID", (object) this.ID);
    dataManager.ExecuteNonQuery("UPDATE IMS_OBJECTS SET F_ID = :newID, F_VERSION_ID = :verID, F_BASE_VERSION = 0 WHERE F_OBJECT_ID = :objID", dbDataParameter1, dataManager.Parameter("verID", (object) num), dataManager.Parameter("objID", (object) this.ObjectID));
    dataManager.ExecuteNonQuery("INSERT INTO IMS_VERSIONS_TREE (F_PARENT_ID, F_OBJECT_ID) VALUES (:parentID, :objID)", dataManager.Parameter("parentID", (object) toObjectID), dataManager.Parameter("objID", (object) Math.Abs(this.ObjectID)));
    dataManager.ExecuteNonQuery("UPDATE IMS_ATTR_HISTORY SET F_ID = :newID WHERE F_ID = :oldID", dbDataParameter1, dbDataParameter2);
    dataManager.ExecuteNonQuery("UPDATE IMS_SELECTIONS SET F_ID = :newID WHERE F_ID = :oldID", dbDataParameter1, dbDataParameter2);
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT DISTINCT F_RELATION_TYPE FROM IMS_RELATIONS WHERE F_PART_ID = :oldID", dbDataParameter2);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      foreach (object updateTable in this.UserSession.DBCache.GetUpdateTables(-1, -1, Convert.ToInt32(dataTable.Rows[index][0])))
        dataManager.ExecuteNonQuery($"UPDATE {updateTable} SET F_PART_ID = :newID WHERE F_PART_ID = :oldID", dbDataParameter1, dbDataParameter2);
    }
    dataManager.ExecuteNonQuery("UPDATE IMS_RELATIONS SET F_PART_ID = :newID WHERE F_PART_ID = :oldID", dbDataParameter1, dbDataParameter2);
    this.paramsTable[177] = (object) 0;
    this.paramsTable[121] = (object) dbObject.ID;
    this.paramsTable[120] = (object) num;
    this.UserSession.DBCache.DeleteObjectInfo(this.ObjectID, this.VersionGUID);
    return num;
  }

  internal AttributeValues[] GetAttributes4Notification(DBAttribute attribute)
  {
    if (!this.UserSession.SendAttrs2DelayedNotificationMode)
      return (AttributeValues[]) null;
    int num = attribute == null ? 0 : (this._Attributes == null ? 1 : 0);
    AttributeValues[] attributesValues = this.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes);
    if (num != 0)
      (this._Attributes as DBAttributeCollection).ReplaceAttributeClass(attribute);
    return attributesValues;
  }

  internal void InitNewObligatoryAttributes(List<IMSAttribute4ObjectType> attrsList)
  {
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      for (int index = 0; index < attrsList.Count; ++index)
      {
        if (attrsList[index].Required != RequiredModes.Manual && (attrsList[index].LevelID == 0 || attrsList[index].LevelID == this.LevelID) && attrsList[index].DefaultValue == string.Empty && attrsList[index].Computed == ComputeValueModes.NotComputableValue && !AttributesTypeHelper.IsComplexAttributeType(attrsList[index].FieldType))
          stringBuilder.Append(attrsList[index].AttributeID.ToString() + ",");
      }
      if (stringBuilder.Length <= 0)
        return;
      --stringBuilder.Length;
      IDbManager dataManager = this.UserSession.DataManager;
      dataManager.ExecuteNonQuery($"INSERT INTO {this.UserSession.DBCache.GetAttributesTableName(this.ObjectType)} (F_ATTRIBUTE_ID, F_OBJECT_ID, F_INLIST_ID) SELECT F_ATTRIBUTE_ID, :objID, 0 FROM IMS_ATTRIBUTES WHERE F_ATTRIBUTE_ID IN ({stringBuilder.ToString()})", dataManager.Parameter("objID", (object) this.ObjectID));
    }
  }

  public virtual bool SmartCacheEnabled
  {
    get
    {
      bool smartCacheEnabled = !this.Deleted;
      if (smartCacheEnabled && this._Attributes != null && (this._Attributes as DBObjectAttributeCollection).IsAttrListLoaded)
      {
        for (int AttrIndex = 0; AttrIndex < this._Attributes.Count; ++AttrIndex)
        {
          if (this._Attributes[AttrIndex].TemporaryAttribute)
          {
            smartCacheEnabled = false;
            break;
          }
        }
      }
      return smartCacheEnabled;
    }
  }

  internal void ValidationsTurnOn()
  {
    this._MustCheckValidatingRule = true;
    this.ValidationRulesOn = true;
  }

  public int VersionsCount
  {
    get
    {
      return Convert.ToInt32(this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_LEVEL_ID <> :levelID AND F_OBJECT_ID > 0", this.UserSession.DataManager.Parameter("id1", (object) this.ID), this.UserSession.DataManager.Parameter("levelID", (object) this.UserSession.IdentHelper.DeletedID)));
    }
  }

  public int ReferencesCount
  {
    get
    {
      object obj1 = this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECT_LINKS WHERE F_TOOBJECT_ID = :objID", this.UserSession.DataManager.Parameter("objID", (object) Math.Abs(this.ObjectID)));
      if (!this.IsBaseVersion)
        return Convert.ToInt32(obj1);
      object obj2 = this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_ID_LINKS WHERE F_TO_ID = :ID11", this.UserSession.DataManager.Parameter("ID11", (object) this.ID));
      return Convert.ToInt32(obj1) + Convert.ToInt32(obj2);
    }
  }

  public int RelationsCount
  {
    get
    {
      return Convert.ToInt32(this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(DISTINCT F_PROJ_ID) FROM IMS_RELATIONS WHERE F_PART_ID = :id1", this.UserSession.DataManager.Parameter("id1", (object) this.ID)));
    }
  }

  public DateTime LCStepDate
  {
    get
    {
      object obj = this.UserSession.DataManager.ExecuteScalar("SELECT MAX(F_START_DATE) FROM IMS_LCSTART_DATE LC_HISTORY WHERE LC_HISTORY.F_OBJECT_ID = :objID", this.UserSession.DataManager.Parameter("objID", (object) Math.Abs(this.ObjectID)));
      return obj == DBNull.Value || obj == null ? DateTime.MinValue : Convert.ToDateTime(obj) + this.UserSession.TimeZoneOffset;
    }
  }

  public virtual Dictionary<int, List<CreatedProjectData>> AddTemplateObjects(long templateID)
  {
    return new ObjectTemplater(this, this.UserSession).AddTemplateObjects(templateID);
  }

  public virtual Dictionary<int, List<CreatedProjectData>> AddTemplateObjects(
    ArrayList _IDs,
    long templateID)
  {
    return new ObjectTemplater(this, this.UserSession).AddTemplateObjects(_IDs, templateID);
  }
}
