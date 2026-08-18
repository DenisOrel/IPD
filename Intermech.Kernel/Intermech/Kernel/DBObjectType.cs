// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class DBObjectType : 
  DBAttributableType,
  IDBObjectType,
  IDBAttributableType,
  IDBGuid,
  IDBSubjectArea,
  IDeletable,
  IDBSecurity
{
  private int _ObjectType;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(10);
  private IDBAttribute4TypeCollection _Attributes;
  private string _LastEventNote;
  private int _StoredFieldID;
  private object _OldValue;

  static DBObjectType()
  {
    DBObjectType.metadataActions.Add(ActionType.GetAccess, false);
    DBObjectType.metadataActions.Add(ActionType.SetAccess, false);
    DBObjectType.metadataActions.Add(ActionType.EditProperties, false);
    DBObjectType.metadataActions.Add(ActionType.Delete, false);
    DBObjectType.metadataActions.Add(ActionType.AddLink, false);
    DBObjectType.metadataActions.Add(ActionType.DeleteLink, false);
    DBObjectType.metadataActions.Add(ActionType.EditLink, false);
    DBObjectType.metadataActions.Add(ActionType.List, true);
    DBObjectType.metadataActions.Add(ActionType.View, true);
    DBObjectType.metadataActions.Add(ActionType.CreateChildItem, true);
  }

  public DBObjectType(UserSession uSession, int anObjectTypeID)
    : base(uSession)
  {
    this._ObjectType = anObjectTypeID;
    this.paramsTable.Create(uSession.DBCache.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) anObjectTypeID));
    if (this.paramsTable.RowsCount == 0)
    {
      DataTable dataTable = uSession.DataManager.ExecuteDataTable(sc_13393.ssp_appserver_13394() + anObjectTypeID.ToString());
      if (dataTable.Rows.Count == 0)
        throw new KernelExceptionID(sc_13393.ssp_appserver_13395(422866272), (object) anObjectTypeID);
      uSession.DBCache.ReloadTables((IUserSession) uSession, uSession.DataManager, "IMS_OBJECT_TYPES", "IMS_ATTR4OBJ_TYPES", "IMS_FORMULA_ATTRS", "IMS_TYPES_APPLICABILITY");
      this.paramsTable.Create(dataTable.Rows[0]);
    }
    this.InitSecurityOptions(4, (long) anObjectTypeID);
    this.SetMDExtensionsType(-1, this._ObjectType, -1);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBObjectType.metadataActions);
  }

  public override string ObjectName
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Kernel_425"), (object) this.ObjectTypeName);
    }
  }

  public int ObjectType => this._ObjectType;

  public string ObjectTypeName
  {
    get => this.paramsTable[125].ToString();
    set
    {
      if (!(this.ObjectTypeName != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_426") + value : LocalizationHolder.rm.GetString("Kernel_427");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_OBJ_TYPE_NAME");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_428"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDName"), value.Length, Consts.MaxObjectNameLength);
        this.UserSession.DataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13396() + SqlHelper.QString(value) + sc_13393.ssp_appserver_13397() + this.ObjectType.ToString());
        this.UserSession.DBCache.ChangeTableValue("F_OBJECT_TYPE = " + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_OBJ_TYPE_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[125] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_429");
        if (ex.Message.IndexOf("IMS_OBJECT_T_OBJ_TYPE_NAME") >= 0)
        {
          string message = string.Format(LocalizationHolder.rm.GetString("Kernel_430"), (object) value);
          this.CloseEvent(EventID, EventlogRecordType.Error, str + message);
          throw new AlreadyExistsException(message);
        }
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw;
      }
    }
  }

  public string ObjectTypeShortName
  {
    get => this.paramsTable[79].ToString();
    set
    {
      if (!(this.ObjectTypeShortName != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_431") + value : LocalizationHolder.rm.GetString("Kernel_432");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_SHORT_NAME");
      try
      {
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDShortName"), value.Length, Consts.MaxShortNameLength);
        if (value != string.Empty && this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_SHORT_NAME = " + SqlHelper.QString(value)).Length != 0)
          throw new KernelExceptionID(sc_13393.ssp_appserver_13398(1295293281), (object) value);
        this.UserSession.DataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13399() + SqlHelper.QString(value) + sc_13393.ssp_appserver_13400() + this.ObjectType.ToString());
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13401() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_SHORT_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[79] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_433") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string ObjectInstanceName
  {
    get => this.paramsTable[62].ToString();
    set
    {
      if (!(this.ObjectInstanceName != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_434") + value : LocalizationHolder.rm.GetString("Kernel_435");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_OBJ_NAME");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_436"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDObjectName"), value.Length, Consts.MaxObjectNameLength);
        this.UserSession.DataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13402() + SqlHelper.QString(value) + sc_13393.ssp_appserver_13403() + this.ObjectType.ToString());
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13404() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_OBJ_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[62] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_437") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public byte[] Icon
  {
    get => this.paramsTable[129] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[129];
    set
    {
      if (SqlHelper.IsEqual(this.Icon, value))
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_438");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_ICON");
      try
      {
        object newValue;
        if (value == null || value.Length == 0)
        {
          this.UserSession.DataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13405() + this.ObjectType.ToString());
          newValue = (object) DBNull.Value;
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13406() + this.ObjectType.ToString(), this.UserSession.DataManager.Parameter("icon", (object) value));
          newValue = (object) value;
        }
        this.UserSession.DBCache.ChangeTableValue("F_OBJECT_TYPE = " + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_ICON", newValue, (IUserSession) this.UserSession);
        this.paramsTable[129] = newValue;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_439") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public ObjectVersionModes Versionable
  {
    get => (ObjectVersionModes) Convert.ToInt32(this.paramsTable[123]);
    set
    {
      if (this.Versionable == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_440") + ObjectVersionModesHelper.GetCaption(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_VERSIONABLE");
      try
      {
        string commandText = "";
        string str = "";
        switch (value)
        {
          case ObjectVersionModes.Abstract:
            commandText = sc_13393.ssp_appserver_13407() + this.ObjectType.ToString();
            str = LocalizationHolder.rm.GetString("Kernel_441");
            break;
          case ObjectVersionModes.SingleVersion:
            commandText = $"{sc_13393.ssp_appserver_13408()}{this.ObjectType.ToString()} AND F_VERSION_ID > 0";
            str = LocalizationHolder.rm.GetString("Kernel_442");
            break;
        }
        if (commandText != "")
        {
          DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable(commandText);
          if (dataTable.Rows.Count > 0)
          {
            long[] objectsID = new long[dataTable.Rows.Count];
            for (int index = 0; index < dataTable.Rows.Count; ++index)
              objectsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
            throw new ObjectsFoundException(string.Format(sc_13393.ssp_appserver_13409(), (object) ObjectVersionModesHelper.GetCaption(value), (object) dataTable.Rows.Count, (object) str), $"Объекты/версии объектов, мешающие изменить версионность для типа объектов '{this.ObjectTypeName}':", objectsID);
          }
        }
        this.UserSession.DataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13411() + Convert.ToInt32((object) value).ToString() + sc_13393.ssp_appserver_13412() + this.ObjectType.ToString());
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13413() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_VERSIONABLE", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[123] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_444") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public ObjectTypeOptions Options
  {
    get => (ObjectTypeOptions) Convert.ToInt32(this.paramsTable[36]);
    set
    {
      if (this.Options == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_445") + ObjectTypeOptionsHelper.GetCaptions(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      foreach (ObjectTypeOptions optionsFlag in (ObjectTypeOptions[]) Enum.GetValues(typeof (ObjectTypeOptions)))
      {
        if ((value & optionsFlag) != (this.Options & optionsFlag) && !this.UserSession.CanChangeObjectElement(4, (object) this.ObjectType, ObligatoryElementKeys.GetKeyForObjectOptionsFlag((int) optionsFlag)))
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_924"), (object) EnumDescConverter.GetEnumDescription((Enum) optionsFlag)));
      }
      bool flag1 = false;
      bool flag2 = this.IsLocalType;
      if (this.IsLocalType)
      {
        if ((value & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.None)
        {
          this.LocalToGlobal();
          flag1 = true;
          flag2 = false;
        }
      }
      else if ((value & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType)
      {
        this.GlobalToLocal();
        flag1 = true;
      }
      if ((value & ObjectTypeOptions.CheckParentAccess) == ObjectTypeOptions.CheckParentAccess && this.DefaultRelation > 0)
      {
        IMSRelationType relationType = MetaDataHelper.GetRelationType(this.DefaultRelation);
        if ((relationType.Options & RelationTypeOptions.EnableCycleRelations) == RelationTypeOptions.EnableCycleRelations)
          throw new KernelExceptionID(411, (object) relationType.Description);
      }
      if ((value & ObjectTypeOptions.CreateSnapshots) == ObjectTypeOptions.CreateSnapshots)
      {
        if ((this.Options & ObjectTypeOptions.CreateSnapshots) == ObjectTypeOptions.None)
        {
          IDBAttributeType attributeType = this.UserSession.GetAttributeType(new Guid("cadd94ce-306c-11d8-b4e9-00304f19f545"));
          if (this.Attributes.GetAttributeByID(attributeType.AttributeID) == null)
            (this.Attributes as IDBAttribute4ObjectTypeCollection).Create(new Attribute4ObjectTypeProperties(attributeType.AttributeID, this.ObjectType, InheritModes.Private, RequiredModes.Manual, string.Empty, attributeType.Computed, attributeType.Formula, UniqueValueModes.NotUnique, attributeType.LevelID, attributeType.DefaultValue, OptimizationModes.Write, attributeType.IsContent, attributeType.Options, attributeType.Mask, 0, 0));
        }
      }
      else if ((value & ObjectTypeOptions.AutoCreateSnapshots) == ObjectTypeOptions.AutoCreateSnapshots && (this.Options & ObjectTypeOptions.CreateSnapshots) == ObjectTypeOptions.CreateSnapshots)
        throw new KernelException(sc_13393.ssp_appserver_13414());
      if ((value & ObjectTypeOptions.AutoCreateSnapshots) == ObjectTypeOptions.AutoCreateSnapshots && (this.Options & ObjectTypeOptions.AutoCreateSnapshots) == ObjectTypeOptions.None)
      {
        if ((value & ObjectTypeOptions.CreateSnapshots) == ObjectTypeOptions.None)
          throw new KernelExceptionID(sc_13393.ssp_appserver_13415(1434219688), (object) this.ObjectTypeName);
        if (this.Attributes.GetAttributeByID(this.UserSession.IdentHelper.ModifyContentDateID) == null)
          throw new KernelException(string.Format(sc_13393.ssp_appserver_13416(), (object) this.ObjectTypeName));
      }
      if (flag2)
      {
        if ((value & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex)
        {
          if ((this.Options & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.None)
            this.UserSession.DataManager.DataProvider.CreateAttrValuesIndex(this.UserSession.DBCache.GetAttributesTableName(this.ObjectType), this.UserSession.DataManager);
        }
        else if ((this.Options & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex)
          this.UserSession.DataManager.DataProvider.DropAttrValuesIndex(this.UserSession.DBCache.GetAttributesTableName(this.ObjectType), this.UserSession.DataManager);
      }
      if ((value & ObjectTypeOptions.AutoContextEnabled) == ObjectTypeOptions.AutoContextEnabled && this.Versionable == ObjectVersionModes.SingleVersion)
        throw new KernelException(sc_13393.ssp_appserver_13417());
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13393.ssp_appserver_13418()}{Convert.ToInt32((object) value).ToString()} WHERE F_OBJECT_TYPE = {this.ObjectType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13419() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_OPTIONS", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[36] = (object) Convert.ToInt32((object) value);
        if (!flag1)
          return;
        (this.UserSession.DBCache as CacheDataset).FillAttributeID4ObjectHash(this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES"), this.UserSession.DataManager);
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_446") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string Note
  {
    get => this.paramsTable[92].ToString();
    set
    {
      if (!(this.Note != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_447") + value : LocalizationHolder.rm.GetString("Kernel_448");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDNote"), value.Length, Consts.MaxNoteLength);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_NOTE");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13393.ssp_appserver_13420()}{SqlHelper.QString(value)} WHERE F_OBJECT_TYPE = {this.ObjectType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13421() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_NOTE", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[92] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_449") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public int DefaultRelation
  {
    get => Convert.ToInt32(this.paramsTable[122]);
    set
    {
      if (this.DefaultRelation == value)
        return;
      IDBRelationType relationType = this.UserSession.GetRelationType(value);
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_886") + relationType.Description;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_DEFAULT_RELATION");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13393.ssp_appserver_13422()}{value.ToString()} WHERE F_OBJECT_TYPE = {this.ObjectType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13423() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_DEFAULT_RELATION", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[122] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_450") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public Guid GUID
  {
    get => new Guid(this.paramsTable[76].ToString());
    set
    {
      if (!(value != this.GUID))
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_451") + value.ToString();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      if (!this.UserSession.CanChangeObject(4, (object) this.ObjectType))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_924"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_13393.ssp_appserver_13424(1658053163));
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13393.ssp_appserver_13425()}{SqlHelper.QString(value.ToString())} WHERE F_OBJECT_TYPE = {this.ObjectType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13426() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_GUID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[76] = (object) value;
        (this.UserSession.DBCache as CacheDataset).ObjecTypeGUIDs[(object) this.ObjectType] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_452") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public string SubjectAreas
  {
    get => this.paramsTable[89].ToString();
    set
    {
      if (!(this.SubjectAreas != value))
        return;
      IDBSubjectAreaCollection subjectAreaCollection = this.UserSession.GetSubjectAreaCollection();
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_453") + subjectAreaCollection.GetAreasCaption(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_AREA_ID");
      try
      {
        subjectAreaCollection.ValidateAriasString(value);
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13393.ssp_appserver_13427()}{SqlHelper.QString(value)} WHERE F_OBJECT_TYPE = {this.ObjectType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13428() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_AREA_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[89] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_454") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string SubjectAreasCaption
  {
    get => this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
  }

  public virtual int Delete(long DeleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, string.Format(LocalizationHolder.rm.GetString("Kernel_455"), (object) this.ObjectName));
    IDbManager dataManager = this.UserSession.DataManager;
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied);
    this.CheckAccess(ActionType.Delete);
    if (this.ParentTypeID > 0)
      (this.UserSession.GetObjectType(this.ParentTypeID) as IDBSecurity).CheckAccess(ActionType.DeleteLink);
    if (!this.UserSession.CanChangeObject(4, (object) this.ObjectType))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_925"), (object) this.ObjectTypeName));
    DataRow[] dataRowArray1 = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Select($"F_SIZE_TYPE = {this.ObjectType} AND F_ATTRIBUTE_TYPE = {8}");
    if (dataRowArray1.Length != 0)
    {
      StringBuilder stringBuilder = new StringBuilder($"\"{dataRowArray1[0]["F_NAME"].ToString()}\"");
      for (int index = 1; index < dataRowArray1.Length; ++index)
        stringBuilder.Append($", \"{dataRowArray1[index]["F_NAME"].ToString()}\"");
      throw new KernelExceptionID(sc_13393.ssp_appserver_13429(1523065505), (object) this.ObjectTypeName, (object) stringBuilder.ToString());
    }
    this.UserSession.StartTransaction();
    try
    {
      (this.EventHelper as EventLogHelper).OnBeforeDeleteObjectType((IDBObjectType) this, (IUserSession) this.UserSession);
      DataRow[] dataRowArray2 = this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select("F_PARENT_ID = " + this.ObjectType.ToString());
      if ((DeleteMode & (long) Consts.DeleteChildren) == 0L)
      {
        if (dataRowArray2.Length != 0)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13393.ssp_appserver_13430()), (object) this.ObjectTypeName));
      }
      else
      {
        foreach (DataRow dataRow in dataRowArray2)
          this.UserSession.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"])).Delete(DeleteMode);
      }
      dataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13431() + this.ObjectType.ToString());
      (this.UserSession.GetLifecycleStepCollection(this.ObjectType) as DBLifecycleStepCollection).RemoveObjectTypeData();
      DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otID AND (F_OBJECT_VER_TYPE = :ovType OR F_LEVEL_ID = :delLevel)", dataManager.Parameter("otID", (object) this.ObjectType), dataManager.Parameter("ovType", (object) -1), dataManager.Parameter("delLevel", (object) this.UserSession.IdentHelper.DeletedID));
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
        this.UserSession.GetObject(Convert.ToInt64(dataTable1.Rows[index][0]), false)?.Delete((long) Consts.PurgeMode);
      DataTable dataTable2 = dataManager.ExecuteDataTable("SELECT DISTINCT F_SNAPSHOT_ID FROM IMS_OBJ_SNAPSHOT WHERE F_OBJECT_TYPE = :otID", dataManager.Parameter("otID", (object) this.ObjectType));
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
        (this.UserSession.GetSnapshot(Convert.ToInt64(dataTable2.Rows[index][0])) as IDeletable).Delete((long) Consts.PurgeMode);
      dataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13432() + this.ObjectType.ToString());
      dataManager.ExecuteNonQuery("DELETE FROM IMS_POSSIBLE_VALUES WHERE F_OBJECT_TYPE = " + this.ObjectType.ToString());
      string condition1 = string.Format("F_OBJECT_TYPE = {0} OR F_INOBJECT_TYPE = {0}", (object) this.ObjectType);
      dataManager.ExecuteNonQuery("DELETE FROM IMS_TYPES_APPLICABILITY WHERE " + condition1);
      IDbManager dbManager1 = dataManager;
      int objectType = this.ObjectType;
      string commandText1 = "DELETE FROM IMS_FORMULA_ATTRS WHERE F_OBJECT_TYPE = " + objectType.ToString();
      dbManager1.ExecuteNonQuery(commandText1);
      IDbManager dbManager2 = dataManager;
      string str1 = sc_13393.ssp_appserver_13433();
      objectType = this.ObjectType;
      string str2 = objectType.ToString();
      string commandText2 = str1 + str2;
      dbManager2.ExecuteNonQuery(commandText2);
      ICacheDataset dbCache1 = this.UserSession.DBCache;
      objectType = this.ObjectType;
      string condition2 = "F_OBJECT_TYPE = " + objectType.ToString();
      UserSession userSession1 = this.UserSession;
      dbCache1.DeleteRecords("IMS_OBJECT_TYPES", condition2, (IUserSession) userSession1);
      ICacheDataset dbCache2 = this.UserSession.DBCache;
      objectType = this.ObjectType;
      string condition3 = "F_OBJECT_TYPE = " + objectType.ToString();
      UserSession userSession2 = this.UserSession;
      dbCache2.DeleteRecords("IMS_ATTR4OBJ_TYPES", condition3, (IUserSession) userSession2);
      ICacheDataset dbCache3 = this.UserSession.DBCache;
      objectType = this.ObjectType;
      string condition4 = "F_OBJECT_TYPE = " + objectType.ToString();
      UserSession userSession3 = this.UserSession;
      dbCache3.DeleteRecords("IMS_FORMULA_ATTRS", condition4, (IUserSession) userSession3);
      ICacheDataset dbCache4 = this.UserSession.DBCache;
      objectType = this.ObjectType;
      string condition5 = "F_OBJECT_TYPE = " + objectType.ToString();
      UserSession userSession4 = this.UserSession;
      dbCache4.DeleteRecords("IMS_POSSIBLE_VALUES", condition5, (IUserSession) userSession4);
      ICacheDataset dbCache5 = this.UserSession.DBCache;
      objectType = this.ObjectType;
      string condition6 = $"F_OBJECT_TYPE = {objectType.ToString()}";
      UserSession userSession5 = this.UserSession;
      dbCache5.DeleteRecords("IMS_OBJTYPES_TREE", condition6, (IUserSession) userSession5);
      this.UserSession.DBCache.DeleteRecords("IMS_TYPES_APPLICABILITY", condition1, (IUserSession) this.UserSession);
      if (ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is IFormDesignerService service)
        service.RemoveTypeFromCache(this._ObjectType, AttributableElements.Object);
      (ServerServices.GetService(typeof (IContainerService)) as IContainerService).DeleteContainerForObjectType((object) this.UserSession, this.GUID);
      this.UserSession.Commit();
      try
      {
        dataManager.ExecuteNonQuery("DROP TABLE " + this.ViewName);
      }
      catch
      {
      }
      try
      {
        dataManager.ExecuteNonQuery("DROP TABLE " + this.AttributesTableName);
      }
      catch
      {
      }
      (this.UserSession.DBCache as CacheDataset).ObjecTypeGUIDs.Remove((object) this.ObjectType);
      this.Deleted = true;
      (this.EventHelper as EventLogHelper).OnAfterDeleteObjectType((IDBObjectType) this, (IUserSession) this.UserSession);
    }
    catch (Exception ex)
    {
      string str = ex.Message.IndexOf("FK_OBJECTS_OBJTYPES") <= -1 ? string.Format(LocalizationHolder.rm.GetString("Kernel_458"), (object) this.ObjectTypeName, (object) ex.Message) : string.Format(LocalizationHolder.rm.GetString("Kernel_457"), (object) this.ObjectTypeName);
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, str);
      if (!(ex is AccessDeniedException))
        throw new KernelException(str, ex);
      throw;
    }
    return 0;
  }

  public void GetObjectsInfo(out int objectsCount, out int snapshotsCount)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    objectsCount = Convert.ToInt32(dataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otID AND F_OBJECT_VER_TYPE <> :ovType AND F_LEVEL_ID <> :delLevel", dataManager.Parameter("otID", (object) this.ObjectType), dataManager.Parameter("ovType", (object) -1), dataManager.Parameter("delLevel", (object) this.UserSession.IdentHelper.DeletedID)));
    snapshotsCount = Convert.ToInt32(dataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJ_SNAPSHOT WHERE F_OBJECT_TYPE = :otID", dataManager.Parameter("otID", (object) this.ObjectType)));
  }

  public InheritModes PublicLC
  {
    get => (InheritModes) Convert.ToInt32(this.paramsTable[53]);
    set
    {
      if (this.PublicLC == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_459") + EnumTypeHelper.GetCaption((Enum) value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_PUBLIC_LC");
      this.UserSession.StartTransaction();
      try
      {
        if (value == InheritModes.Inherited)
        {
          if (this.ParentTypeID < 0)
            throw new KernelExceptionID(sc_13393.ssp_appserver_13434(1853877070), (object) this.ObjectTypeName);
          if (Convert.ToInt64(this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = " + this.ObjectType.ToString())) > 0L)
            throw new KernelExceptionID(sc_13393.ssp_appserver_13435(1994940545));
        }
        else
        {
          if (value != InheritModes.Private)
            throw new KernelExceptionID(sc_13393.ssp_appserver_13436(1500147380));
          this.UserSession.GetLifecycleStepCollection(this.ParentTypeID).CopyTo(this.ObjectType);
        }
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_13393.ssp_appserver_13437(), (object) Convert.ToInt32((object) value), (object) this.ObjectType));
        this.UserSession.DBCache.ChangeTableValue("F_OBJECT_TYPE = " + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_PUBLIC_LC", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[53] = (object) Convert.ToInt32((object) value);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = string.Format(LocalizationHolder.rm.GetString(sc_13393.ssp_appserver_13438()), (object) ex.Message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public int IncludeObjectType(params int[] objectTypes)
  {
    try
    {
      IDbManager dataManager = this.UserSession.DataManager;
      foreach (int objectType1 in objectTypes)
      {
        IDBObjectType dbObjectType = objectType1 != 0 ? this.UserSession.GetObjectType(objectType1) : throw new KernelExceptionID(sc_13393.ssp_appserver_13439(1645184129));
        long EventID = this.AddEvent(0L, ActionType.AddLink, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_461") + dbObjectType.ObjectTypeName);
        this.CheckAccess(ActionType.AddLink);
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
        this.UserSession.StartTransaction();
        try
        {
          DataRow[] dataRowArray1 = this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select($"F_OBJECT_TYPE = {objectType1.ToString()} AND F_PARENT_ID <> {this._ObjectType.ToString()}");
          if (dataRowArray1.Length != 0)
          {
            IDBObjectType objectType2 = this.UserSession.GetObjectType(Convert.ToInt32(dataRowArray1[0]["F_PARENT_ID"]));
            IDBObjectType objectType3 = this.UserSession.GetObjectType(Convert.ToInt32(dataRowArray1[0]["F_OBJECT_TYPE"]));
            throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13393.ssp_appserver_13440()), (object) objectType3.ObjectTypeName, (object) objectType2.ObjectTypeName));
          }
          dataManager.ExecuteNonQuery($"INSERT INTO IMS_OBJTYPES_TREE (F_PARENT_ID, F_OBJECT_TYPE) VALUES ({this._ObjectType.ToString()}, {objectType1.ToString()})");
          DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT * FROM IMS_OBJTYPES_TREE WHERE F_PARENT_ID = :parentID AND F_OBJECT_TYPE = :objTypeID", dataManager.Parameter("parentID", (object) this._ObjectType), dataManager.Parameter("objTypeID", (object) objectType1));
          if (dataTable1.Rows.Count <= 0)
            throw new KernelException(sc_13393.ssp_appserver_13441());
          this.UserSession.DBCache.AddRow("IMS_OBJTYPES_TREE", dataTable1.Rows[0], (IUserSession) this.UserSession);
          (this.UserSession.DBCache as CacheDataset).ObjecTypeParents[objectType1] = this._ObjectType;
          DataTable dataTable2 = this.Attributes.Select("");
          DataRow[] dataRowArray2 = this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS").Select("F_OBJECT_TYPE = " + this.ObjectType.ToString());
          int[] numArray1 = new int[dataRowArray2.Length];
          int[] numArray2 = new int[dataRowArray2.Length];
          for (int index = 0; index < dataRowArray2.Length; ++index)
          {
            numArray1[index] = Convert.ToInt32(dataRowArray2[index]["F_FORMULA_ID"]);
            numArray2[index] = Convert.ToInt32(dataRowArray2[index]["F_ATTRIBUTE_ID"]);
          }
          bool isEmpty = Convert.ToInt32(this.UserSession.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = " + objectType1.ToString())) == 0;
          List<int> intList = new List<int>();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          {
            InheritModes int32_1 = (InheritModes) Convert.ToInt32(row["F_PUBLIC"]);
            ComputeValueModes int32_2 = (ComputeValueModes) Convert.ToInt32(row["F_COMPUTED"]);
            if (int32_1 == InheritModes.Inherited || int32_1 == InheritModes.Public)
            {
              if (int32_2 == ComputeValueModes.NotComputableValue)
              {
                DBAttributeType4Object attributeById = this.Attributes.GetAttributeByID(Convert.ToInt32(row["F_ATTRIBUTE_ID"])) as DBAttributeType4Object;
                try
                {
                  attributeById._FreezeUpdateAttributesViewHash = true;
                  attributeById.AddInheritAttribute(objectType1, isEmpty);
                }
                finally
                {
                  attributeById._FreezeUpdateAttributesViewHash = false;
                }
              }
              else
                intList.Add(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
            }
          }
          while (intList.Count > 0)
          {
            for (int index1 = intList.Count - 1; index1 >= 0; --index1)
            {
              bool flag = true;
              for (int index2 = 0; index2 < dataRowArray2.Length; ++index2)
              {
                if (numArray1[index2] == intList[index1])
                {
                  for (int index3 = 0; index3 < intList.Count; ++index3)
                  {
                    if (intList[index3] == numArray2[index2])
                    {
                      flag = false;
                      break;
                    }
                  }
                }
                if (!flag)
                  break;
              }
              if (flag)
              {
                DBAttributeType4Object attributeById = this.Attributes.GetAttributeByID(intList[index1]) as DBAttributeType4Object;
                try
                {
                  attributeById._FreezeUpdateAttributesViewHash = true;
                }
                finally
                {
                  attributeById._FreezeUpdateAttributesViewHash = false;
                }
                attributeById.AddInheritAttribute(objectType1, isEmpty);
                intList.RemoveAt(index1);
              }
            }
          }
          this.UserSession.Commit();
        }
        catch (Exception ex)
        {
          string str = ex.Message.ToUpper().IndexOf("PRIMARY") <= -1 ? string.Format(LocalizationHolder.rm.GetString(sc_13393.ssp_appserver_13443()), (object) dbObjectType.ObjectTypeName, (object) this.ObjectTypeName, (object) ex.Message) : string.Format(LocalizationHolder.rm.GetString(sc_13393.ssp_appserver_13442()), (object) dbObjectType.ObjectTypeName, (object) this.ObjectTypeName);
          this.UserSession.Rollback();
          this.CloseEvent(EventID, EventlogRecordType.Error, str);
          throw new KernelException(str, ex);
        }
      }
    }
    finally
    {
      (this.UserSession.DBCache as CacheDataset).FillAttributeID4ObjectHash(this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES"), this.UserSession.DataManager);
    }
    return 0;
  }

  public override IDBAttribute4TypeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
        this._Attributes = (IDBAttribute4TypeCollection) new DBAttribute4ObjectTypeCollection(this.UserSession, this.ObjectType, false);
      return this._Attributes;
    }
  }

  public IDBAttribute4TypeCollection VisibleAttributes
  {
    get
    {
      return (IDBAttribute4TypeCollection) new DBAttribute4ObjectTypeCollection(this.UserSession, this.ObjectType, true);
    }
  }

  public int CaptionAttribute
  {
    get => Convert.ToInt32(this.paramsTable[61]);
    set
    {
      if (this.CaptionAttribute == value)
        return;
      string name = LocalizationHolder.rm.GetString("Kernel_465");
      if (value > 0)
        name = this.UserSession.GetAttributeType(value).Name;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_466") + name;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_CAPTION_ATTRIBUTE");
      this.UserSession.StartTransaction();
      try
      {
        if (value > 0)
        {
          IDBAttributeType attributeById = (IDBAttributeType) this.Attributes.GetAttributeByID(value, false);
          if (attributeById == null)
            throw new KernelExceptionID(sc_13393.ssp_appserver_13444(73270215), (object) name, (object) this.ObjectTypeName);
          if (attributeById.AttributeType != FieldTypes.ftString && attributeById.AttributeType != FieldTypes.ftObjectLink && attributeById.AttributeType != FieldTypes.ftObjectLinkByID)
            throw new KernelExceptionID(sc_13393.ssp_appserver_13445(258766704), (object) attributeById.Name, (object) AttributesTypeHelper.GetCaption(attributeById.AttributeType));
          if (attributeById.Computed == ComputeValueModes.JITValue)
            throw new KernelExceptionID(sc_13393.ssp_appserver_13446(278986073));
          if (attributeById.Computed == ComputeValueModes.IndexValue)
            throw new KernelExceptionID(sc_13393.ssp_appserver_13447(2123596346));
        }
        else if (value < 0)
          throw new KernelExceptionID(sc_13393.ssp_appserver_13448(1392509098));
        IDbManager dataManager = this.UserSession.DataManager;
        string str1 = sc_13393.ssp_appserver_13449();
        string str2 = value.ToString();
        int objectType = this.ObjectType;
        string str3 = objectType.ToString();
        string commandText = $"{str1}{str2} WHERE F_OBJECT_TYPE = {str3}";
        dataManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        objectType = this.ObjectType;
        string filterStr = "F_OBJECT_TYPE = " + objectType.ToString();
        __Boxed<int> newValue = (System.ValueType) value;
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_OBJECT_TYPES", "F_CAPTION_ATTRIBUTE", (object) newValue, (IUserSession) userSession);
        this.paramsTable[61] = (object) value;
        this.UserSession.DBCache.EnterReadLocker();
        try
        {
          (this.UserSession.DBCache as CacheDataset).FillCaptionAttributes(this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES"));
        }
        finally
        {
          this.UserSession.DBCache.ExitReadLocker();
        }
        if (value > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = " + this.ObjectType.ToString()).Rows)
          {
            DBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(row[0])) as DBObject;
            IDBAttribute attributeById = dbObject.GetAttributeByID(value);
            dbObject.SetCaption(attributeById == null ? string.Empty : attributeById.AsString);
          }
        }
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.UserSession.DBCache.EnterReadLocker();
        try
        {
          ((CacheDataset) this.UserSession.DBCache).FillCaptionAttributes(this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES"));
        }
        finally
        {
          this.UserSession.DBCache.ExitReadLocker();
        }
        string str = LocalizationHolder.rm.GetString(sc_13393.ssp_appserver_13450()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  public override bool AnyAttributes
  {
    get => Convert.ToBoolean(this.paramsTable[60]);
    set
    {
      if (this.AnyAttributes == value)
        return;
      string str1 = LocalizationHolder.rm.GetString("Kernel_468");
      if (!value)
        str1 = LocalizationHolder.rm.GetString("Kernel_469");
      this._LastEventNote = string.Format(LocalizationHolder.rm.GetString("Kernel_470"), (object) str1);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_ANY_ATTRIBUTES");
      try
      {
        int newValue = 1;
        if (!value)
        {
          newValue = 0;
          DataTable dataTable1 = this.CaptionAttribute <= 0 || this.Attributes.GetAttributeByID(this.CaptionAttribute, false) != null ? this.Attributes.Select("") : throw new KernelExceptionID(385, (object) this.UserSession.GetAttributeType(this.CaptionAttribute).Name);
          string str2;
          if (dataTable1.Rows.Count > 0)
          {
            StringBuilder stringBuilder = new StringBuilder("F_ATTRIBUTE_ID NOT IN (" + dataTable1.Rows[0]["F_ATTRIBUTE_ID"].ToString());
            for (int index = 1; index < dataTable1.Rows.Count; ++index)
              stringBuilder.Append("," + dataTable1.Rows[index]["F_ATTRIBUTE_ID"].ToString());
            str2 = stringBuilder.ToString() + ")";
          }
          else
            str2 = "F_ATTRIBUTE_ID > 0";
          DataTable dataTable2 = this.UserSession.DataManager.ExecuteDataTable(string.Format("SELECT DISTINCT A.F_OBJECT_ID FROM IMS_OBJECTS O, {2} A WHERE O.F_OBJECT_TYPE = {0} AND A.F_OBJECT_ID = O.F_OBJECT_ID AND {1}", (object) this.ObjectType, (object) str2, (object) this.UserSession.DBCache.GetAttributesTableName(this.ObjectType)));
          if (dataTable2.Rows.Count > 0)
          {
            long[] objectsID = new long[dataTable2.Rows.Count];
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
              objectsID[index] = Convert.ToInt64(dataTable2.Rows[index][0]);
            throw new ObjectsFoundException(string.Format(sc_13393.ssp_appserver_13451(), (object) dataTable2.Rows.Count, (object) this.ObjectTypeName), "Объекты, у которых присутствуют недопустимые атрибуты:", objectsID);
          }
        }
        this.UserSession.DataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13452() + newValue.ToString() + sc_13393.ssp_appserver_13453() + this.ObjectType.ToString());
        this.UserSession.DBCache.ChangeTableValue("F_OBJECT_TYPE = " + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_ANY_ATTRIBUTES", (object) newValue, (IUserSession) this.UserSession);
        this.paramsTable[60] = (object) newValue;
      }
      catch (Exception ex)
      {
        string str3 = LocalizationHolder.rm.GetString(sc_13393.ssp_appserver_13454()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str3);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str3, ex);
        throw;
      }
    }
  }

  public int ParentTypeID
  {
    get
    {
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select("F_OBJECT_TYPE = " + this.ObjectType.ToString());
      return dataRowArray.Length == 0 ? -1 : Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"]);
    }
    set
    {
      if (this.ParentTypeID == value)
        return;
      IDbManager dataManager = this.UserSession.DataManager;
      DBObjectType dbObjectType = (DBObjectType) null;
      DataTable dataTable1 = (DataTable) null;
      if (value > -1)
      {
        this.CheckCycleParent(value);
        dbObjectType = this.UserSession.GetObjectType(value) as DBObjectType;
        dataTable1 = this.UserSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, dbObjectType.ObjectType, -1);
      }
      if (value < 0 || dbObjectType.Attributes.Count > 0L || this.ParentTypeID > -1 || dataTable1.Rows.Count > 0)
      {
        ArrayList objsTreeList = new ArrayList();
        this.FillChildrenList(objsTreeList);
        StringBuilder stringBuilder = new StringBuilder("(");
        foreach (int num in objsTreeList)
          stringBuilder.Append(num.ToString() + ",");
        stringBuilder[stringBuilder.Length - 1] = ')';
        if (Convert.ToInt64(dataManager.ExecuteScalar(string.Format(sc_13393.ssp_appserver_13455(), (object) this.UserSession.IdentHelper.DeletedID, (object) stringBuilder.ToString()))) > 0L)
          this.ValidateChangeObjectsParent(value);
      }
      this.CheckChangeEnable("F_PARENT_ID");
      this.UserSession.StartTransaction();
      try
      {
        int parentTypeId = this.ParentTypeID;
        if (this.ParentTypeID > -1)
        {
          foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Select($"F_OBJECT_TYPE = {this.ParentTypeID} AND F_PUBLIC = {Convert.ToInt32((object) InheritModes.Inherited)}"))
            (this.Attributes.GetAttributeByID(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), false) as DBAttributeType4Object).DeleteInheritAttribute(0L, false);
        }
        string condition = $"F_PARENT_ID = {this.ParentTypeID.ToString()} AND F_OBJECT_TYPE = {this.ObjectType.ToString()}";
        dataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13457() + condition);
        this.UserSession.DBCache.DeleteRecords("IMS_OBJTYPES_TREE", condition, (IUserSession) this.UserSession);
        if (value > -1)
          dbObjectType.IncludeObjectType(new int[1]
          {
            this.ObjectType
          });
        DataTable dataTable2 = this.Attributes.Select("").Copy();
        for (int index = dataTable2.Rows.Count - 1; index >= 0; --index)
        {
          if (Convert.ToInt32(dataTable2.Rows[index]["F_PUBLIC"]) == 2)
          {
            bool flag = true;
            if (value > -1 && this.UserSession.GetObjectType(value).Attributes.GetAttributeByID(Convert.ToInt32(dataTable2.Rows[index]["F_ATTRIBUTE_ID"]), false) is IDBAttributeType4Object attributeById)
              flag = attributeById.InheritMode == InheritModes.Private;
            if (flag)
              this.DeleteInheritedAttribute(this, Convert.ToInt32(dataTable2.Rows[index]["F_ATTRIBUTE_ID"]));
          }
        }
        this.RemoveObjectsData(parentTypeId, value);
        this.UserSession.Commit();
        if (parentTypeId <= -1)
          return;
        this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_OBJTYPES_TREE", "IMS_ATTR4OBJ_TYPES");
      }
      catch
      {
        this.UserSession.Rollback();
        this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_ATTR4OBJ_TYPES", "IMS_FORMULA_ATTRS");
        throw;
      }
    }
  }

  private void ValidateChangeObjectsParent(int newTypeID)
  {
    if (this.ParentTypeID < 0)
      return;
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    DataTable applicabilitiesList1 = applicabilityCollection.GetApplicabilitiesList(-1, this.ObjectType, -1);
    for (int index = 0; index < applicabilitiesList1.Rows.Count; ++index)
    {
      if (Convert.ToInt32(applicabilitiesList1.Rows[index]["F_PUBLIC"]) == 2 && Convert.ToInt32(applicabilitiesList1.Rows[index]["F_MAX_LINKS"]) > 0)
      {
        IDBRelationsApplicability relationsApplicability = (IDBRelationsApplicability) null;
        if (newTypeID >= 0)
          relationsApplicability = applicabilityCollection.GetApplicability(Convert.ToInt32(applicabilitiesList1.Rows[index]["F_RELATION_TYPE"]), Convert.ToInt32(newTypeID), Convert.ToInt32(applicabilitiesList1.Rows[index]["F_INOBJECT_TYPE"]));
        if (relationsApplicability == null)
          throw new KernelException(string.Format(sc_13393.ssp_appserver_13458(), (object) this.ObjectTypeName, (object) this.UserSession.GetRelationType(Convert.ToInt32(applicabilitiesList1.Rows[index]["F_RELATION_TYPE"])).Description, (object) this.UserSession.GetObjectType(Convert.ToInt32(applicabilitiesList1.Rows[index]["F_INOBJECT_TYPE"])).ObjectTypeName, (object) this.UserSession.GetObjectType(this.ParentTypeID).ObjectTypeName));
      }
    }
    DataTable applicabilitiesList2 = applicabilityCollection.GetApplicabilitiesList(-1, -1, this.ObjectType);
    for (int index = 0; index < applicabilitiesList2.Rows.Count; ++index)
    {
      if (Convert.ToInt32(applicabilitiesList2.Rows[index]["F_PUBLIC"]) == 2 && Convert.ToInt32(applicabilitiesList2.Rows[index]["F_MAX_LINKS"]) > 0)
      {
        IDBRelationsApplicability relationsApplicability = (IDBRelationsApplicability) null;
        if (newTypeID >= 0)
          relationsApplicability = applicabilityCollection.GetApplicability(Convert.ToInt32(applicabilitiesList2.Rows[index]["F_RELATION_TYPE"]), Convert.ToInt32(applicabilitiesList2.Rows[index]["F_OBJECT_TYPE"]), Convert.ToInt32(newTypeID));
        if (relationsApplicability == null)
          throw new KernelException(string.Format(sc_13393.ssp_appserver_13459(), (object) this.ObjectTypeName, (object) this.UserSession.GetRelationType(Convert.ToInt32(applicabilitiesList2.Rows[index]["F_RELATION_TYPE"])).Description, (object) this.UserSession.GetObjectType(Convert.ToInt32(applicabilitiesList2.Rows[index]["F_OBJECT_TYPE"])).ObjectTypeName, (object) this.UserSession.GetObjectType(this.ParentTypeID).ObjectTypeName));
      }
    }
    IDBObjectType dbObjectType = (IDBObjectType) null;
    if (newTypeID >= 0)
      dbObjectType = this.UserSession.GetObjectType(newTypeID);
    DataTable dataTable = this.Attributes.Select(string.Empty);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (Convert.ToInt32(dataTable.Rows[index]["F_PUBLIC"]) == 2)
      {
        IDBAttributeType4Object attributeType4Object = (IDBAttributeType4Object) null;
        if (dbObjectType != null)
          attributeType4Object = dbObjectType.Attributes.GetAttributeByID(Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"]), false) as IDBAttributeType4Object;
        if (dbObjectType == null || attributeType4Object == null || attributeType4Object.InheritMode == InheritModes.Private)
          throw new KernelException(string.Format(sc_13393.ssp_appserver_13460(), (object) this.ObjectTypeName, (object) this.UserSession.GetAttributeType(Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"])).Name, (object) this.UserSession.GetObjectType(this.ParentTypeID).ObjectTypeName));
      }
    }
  }

  private void RemoveObjectsData(int oldTypeID, int newTypeID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    object obj = dataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :oType", dataManager.Parameter("oType", (object) this.ObjectType));
    if (obj != null && Convert.ToInt32(obj) == 0)
      return;
    ArrayList objsTreeList = new ArrayList();
    this.FillChildrenList(objsTreeList);
    if (oldTypeID >= 0)
    {
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, oldTypeID, -1);
      if (updateTables != null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < objsTreeList.Count; ++index)
        {
          IDBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(objsTreeList[index]));
          if (!objectType.IsLocalType)
            stringBuilder.Append(objectType.ObjectType.ToString() + ",");
        }
        if (stringBuilder.Length > 0)
        {
          --stringBuilder.Length;
          for (int index = 0; index < updateTables.Length; ++index)
          {
            if (updateTables[index] != "IMS_OBJECTS_VIEW")
              dataManager.ExecuteNonQuery($"DELETE FROM {updateTables[index]} WHERE F_OBJECT_TYPE IN ({stringBuilder.ToString()})");
          }
        }
      }
    }
    if (newTypeID < 0)
      return;
    int parentTypeId;
    for (IDBObjectType objectType1 = this.UserSession.GetObjectType(newTypeID); objectType1 != null; objectType1 = parentTypeId < 0 ? (IDBObjectType) null : this.UserSession.GetObjectType(parentTypeId))
    {
      for (int index = 0; index < objsTreeList.Count; ++index)
      {
        IDBObjectType objectType2 = this.UserSession.GetObjectType(Convert.ToInt32(objsTreeList[index]));
        if (!objectType2.IsLocalType)
          (objectType1 as DBObjectType).RebuildView(objectType2.ObjectType, false, true);
      }
      parentTypeId = objectType1.ParentTypeID;
    }
  }

  private void CheckCycleParent(int newParentID)
  {
    IDBObjectType dbObjectType = this.ObjectType != newParentID ? this.UserSession.GetObjectType(newParentID) : throw new KernelExceptionID(378, (object) this.ObjectTypeName);
    if (dbObjectType.ParentTypeID < 0)
      return;
    this.CheckCycleParent(dbObjectType.ParentTypeID);
  }

  private void DeleteInheritedAttribute(DBObjectType dBObjectType, int attributeID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    dataManager.ExecuteNonQuery("DELETE FROM IMS_FORMULA_ATTRS WHERE F_OBJECT_TYPE = :otID AND F_FORMULA_ID = :atID", dataManager.Parameter("otID", (object) dBObjectType.ObjectType), dataManager.Parameter("atID", (object) attributeID));
    this.UserSession.DBCache.DeleteRecords("IMS_FORMULA_ATTRS", string.Format(sc_13393.ssp_appserver_13461(), (object) dBObjectType.ObjectType, (object) attributeID), (IUserSession) this.UserSession);
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_FORMULA_ATTRS").Select($"F_OBJECT_TYPE = {dBObjectType.ObjectType} AND F_ATTRIBUTE_ID = {attributeID}"))
    {
      if (dBObjectType.Attributes.GetAttributeByID(Convert.ToInt32(dataRow["F_FORMULA_ID"])) is IDBAttributeType4Object attributeById && attributeById.InheritMode != InheritModes.Inherited)
        throw new KernelExceptionID(265, (object) dBObjectType.ObjectTypeName, (object) this.UserSession.GetAttributeType(attributeID).Name, (object) attributeById.Name);
    }
    dataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13462(), dataManager.Parameter("otID", (object) dBObjectType.ObjectType), dataManager.Parameter("atID", (object) attributeID));
    this.UserSession.DBCache.DeleteRecords("IMS_ATTR4OBJ_TYPES", $"F_OBJECT_TYPE = {dBObjectType.ObjectType} AND F_ATTRIBUTE_ID = {attributeID}", (IUserSession) this.UserSession);
    ArrayList objsTreeList = new ArrayList();
    dBObjectType.FillChildrenList(objsTreeList);
    foreach (int anObjectTypeID in objsTreeList)
    {
      if (dBObjectType.ObjectType != anObjectTypeID)
      {
        DBObjectType objectType = this.UserSession.GetObjectType(anObjectTypeID) as DBObjectType;
        if (objectType.Attributes.GetAttributeByID(attributeID) is IDBAttributeType4Object attributeById && attributeById.InheritMode == InheritModes.Inherited)
          this.DeleteInheritedAttribute(objectType, attributeID);
      }
    }
  }

  public void FillParentsArray(ArrayList objsTreeList)
  {
    objsTreeList.Add((object) this.ObjectType);
    for (int parentTypeId = this.ParentTypeID; parentTypeId > -1; parentTypeId = this.UserSession.GetObjectType(parentTypeId).ParentTypeID)
      objsTreeList.Add((object) parentTypeId);
  }

  public void AddChildrenForType(int objTypeID, ArrayList objsTreeList)
  {
    objsTreeList.Add((object) objTypeID);
    foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select("F_PARENT_ID = " + objTypeID.ToString()))
      this.AddChildrenForType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), objsTreeList);
  }

  public void FillChildrenList(ArrayList objsTreeList)
  {
    objsTreeList.Clear();
    this.AddChildrenForType(this.ObjectType, objsTreeList);
  }

  internal string GetChildrenListSQL()
  {
    ArrayList objsTreeList = new ArrayList();
    this.FillChildrenList(objsTreeList);
    StringBuilder stringBuilder = new StringBuilder(objsTreeList[0].ToString());
    for (int index = 0; index < objsTreeList.Count; ++index)
      stringBuilder.Append("," + objsTreeList[index].ToString());
    return stringBuilder.ToString();
  }

  public bool HasAttribute(int attributeID)
  {
    if (attributeID < 0)
      return ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeID) == AttributeSourceTypes.Object;
    return this.AnyAttributes || this.Attributes.GetAttributeByID(attributeID, false) != null;
  }

  private void SaveNewValue(int fldID, object newValue)
  {
    if (this.paramsTable[fldID] == newValue)
      return;
    this._StoredFieldID = fldID;
    this._OldValue = this.paramsTable[fldID];
    this.paramsTable[fldID] = newValue;
  }

  private void RestoreOldValue()
  {
    if (this._OldValue == null)
      return;
    this.paramsTable[this._StoredFieldID] = this._OldValue;
    this._OldValue = (object) null;
  }

  public ObjectTypeProperties PropertiesStructure
  {
    get
    {
      return new ObjectTypeProperties(this.ObjectType, this.ObjectTypeName, this.ObjectInstanceName, this.Note, this.Versionable, this.DefaultRelation, this.SubjectAreas, this.GUID, this.CaptionAttribute, this.AnyAttributes, this.PublicLC, this.ObjectTypeShortName, this.LifetimeReserve, this.Options, this.SchemaID);
    }
    set
    {
      if (value.ObjectType != this.ObjectType)
        throw new KernelException(sc_13393.ssp_appserver_13463());
      this.UserSession.StartTransaction();
      try
      {
        this.ObjectTypeName = value.ObjectTypeName;
        this.ObjectInstanceName = value.ObjectInstanceName;
        this.ObjectTypeShortName = value.ObjectTypeShortName;
        this.Note = value.Note;
        this.Versionable = value.Versionable;
        this.DefaultRelation = value.DefaultRelation;
        this.SubjectAreas = value.AreaID;
        this.SaveNewValue(61, (object) value.CaptionAttribute);
        try
        {
          this.AnyAttributes = value.AnyAttributes;
        }
        finally
        {
          this.RestoreOldValue();
        }
        this.CaptionAttribute = value.CaptionAttribute;
        this.PublicLC = value.PublicLCSchema;
        this.GUID = value.ObjectTypeGuid;
        this.LifetimeReserve = value.LifetimeReserve;
        this.Options = value.Options;
        if (this.SchemaID == value.SchemaID)
          value.ChangeObjectsSchema = false;
        this.SchemaID = value.SchemaID;
        if (value.ChangeObjectsSchema)
          this.ChangeObjectsSchema(true);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        EventlogRecordType auditType = !(ex is AccessDeniedException) ? EventlogRecordType.Error : EventlogRecordType.AccessDenied;
        if (this._LastEventNote != null)
          this.AddEvent(0L, ActionType.EditProperties, auditType, $"{this._LastEventNote}{Environment.NewLine} Ошибка: {ex.Message}");
        throw;
      }
    }
  }

  public Hashtable GetPossibleChildren()
  {
    Hashtable possibleChildren = new Hashtable();
    ArrayList objsTreeList = new ArrayList();
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, -1, this.ObjectType);
    DataTable table = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES");
    foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
    {
      if (Convert.ToInt32(row["F_MIN_LINKS"]) != -1)
      {
        objsTreeList.Clear();
        this.AddChildrenForType(Convert.ToInt32(row["F_OBJECT_TYPE"]), objsTreeList);
        int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
        foreach (int num in objsTreeList)
        {
          IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(int32, num, this.ObjectType);
          if (applicability != null && applicability.ApplicabilityMode == ApplicabilityModes.Disabled)
          {
            possibleChildren.Remove((object) num);
          }
          else
          {
            DataRow dataRow = table.Rows.Find((object) num);
            ApplicabilityOptions applicabilityOptions = dataRow != null ? (ApplicabilityOptions) DataSetProcessor.GetInt32Value(row, "F_OPTIONS", 0) : ApplicabilityOptions.None;
            if (dataRow != null && Convert.ToInt32(dataRow["F_VERSIONABLE"]) != 0 && ((applicabilityOptions & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation || !possibleChildren.ContainsKey((object) num)))
              possibleChildren[(object) num] = (object) int32;
          }
        }
      }
    }
    return possibleChildren;
  }

  public Hashtable GetAllChildren()
  {
    Hashtable allChildren = new Hashtable();
    ArrayList objsTreeList = new ArrayList();
    IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
    DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, -1, this.ObjectType);
    DataTable table = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES");
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
    {
      if (Convert.ToInt32(row["F_MIN_LINKS"]) != -1)
      {
        objsTreeList.Clear();
        this.AddChildrenForType(Convert.ToInt32(row["F_OBJECT_TYPE"]), objsTreeList);
        int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
        foreach (int num in objsTreeList)
        {
          IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(int32, num, this.ObjectType);
          if (applicability != null && applicability.ApplicabilityMode == ApplicabilityModes.Disabled)
          {
            allChildren.Remove((object) num);
            if (dictionary.ContainsKey(num))
              dictionary.Remove(num);
          }
          else
          {
            bool flag = dictionary.ContainsKey(num) && allChildren.ContainsKey((object) num);
            if (table.Rows.Find((object) num) != null && !flag)
            {
              ApplicabilityOptions options = applicability.Options;
              if (allChildren.ContainsKey((object) num))
              {
                if ((options & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation)
                {
                  dictionary[num] = int32;
                  allChildren[(object) num] = (object) int32;
                }
              }
              else
                allChildren[(object) num] = (object) int32;
            }
          }
        }
      }
    }
    return allChildren;
  }

  public string ViewName => SqlHelper.viewForObjectTypePrefix + this.ObjectType.ToString();

  internal void InsertIntoView(int forObjectTypeID)
  {
    ArrayList objsTreeList = new ArrayList();
    this.FillChildrenList(objsTreeList);
    StringBuilder stringBuilder = new StringBuilder($"{"F_OBJECT_VER_TYPE"} <> {-1.ToString()} AND F_OBJECT_TYPE IN (");
    if (forObjectTypeID == -1)
    {
      if (this.IsLocalType)
      {
        stringBuilder.Append(this.ObjectType.ToString() + ",");
      }
      else
      {
        foreach (int anObjectTypeID in objsTreeList)
        {
          if (anObjectTypeID == this.ObjectType || !this.UserSession.GetObjectType(anObjectTypeID).IsLocalType)
            stringBuilder.AppendFormat(anObjectTypeID.ToString() + ",");
        }
      }
    }
    else
      stringBuilder.Append(forObjectTypeID.ToString() + ",");
    stringBuilder[stringBuilder.Length - 1] = ')';
    try
    {
      this.UserSession.DataManager.SetAdminCommandTimeout();
      this.UserSession.DataManager.ExecuteNonQuery(string.Format($"INSERT INTO {{0}} (F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, F_GUID, CAPTION, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID){sc_13393.ssp_appserver_13464()}{sc_13393.ssp_appserver_13465()}", (object) this.ViewName, (object) stringBuilder.ToString()));
      this.UserSession.DataManager.ExecuteNonQuery(string.Format($"{sc_13393.ssp_appserver_13466()}{sc_13393.ssp_appserver_13467()}(SELECT F_WORK_CAPTION FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = -IMS_OBJECTS.F_OBJECT_ID), F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID < 0 AND F_OBJECT_VER_TYPE > -1 AND {{1}}", (object) this.ViewName, (object) stringBuilder.ToString()));
    }
    finally
    {
      this.UserSession.DataManager.SetNormalCommandTimeout();
    }
  }

  public void RebuildView() => this.RebuildView(-1, this.IsLocalType, true);

  internal void RebuildViewWithoutData() => this.RebuildView(-1, this.IsLocalType, true, false);

  public void RebuildView(int forObjectTypeID, bool isLocalType, bool needDrop)
  {
    this.RebuildView(forObjectTypeID, isLocalType, needDrop, true);
  }

  public void RebuildView(int forObjectTypeID, bool isLocalType, bool needDrop, bool needFillData)
  {
    this.CheckAccess(ActionType.EditProperties);
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable attributes = this.Attributes.Select("");
    this.UserSession.StartTransaction();
    try
    {
      List<string> indexes = new List<string>();
      bool flag;
      if (forObjectTypeID < 0)
      {
        flag = this.UserSession.QueryBuilder.RebuildTypedView(this.ViewName, attributes, AttributeSourceTypes.Object, dataManager, isLocalType, needDrop, false, indexes);
      }
      else
      {
        try
        {
          dataManager.DataProvider.CheckTableExists(this.ViewName, "F_OBJECT_ID", dataManager);
          flag = true;
        }
        catch
        {
          flag = false;
        }
      }
      if (flag & needFillData)
      {
        DataTable dataTable = dataManager.ExecuteDataTable($"SELECT * FROM {this.ViewName} WHERE F_OBJECT_ID = -1");
        dataManager.SetAdminCommandTimeout();
        this.InsertIntoView(forObjectTypeID);
        foreach (DataRow row in (InternalDataCollectionBase) attributes.Rows)
        {
          if (Convert.ToInt32(row["F_INVIEW"]) != 0)
          {
            IDBAttributeType attributeType = this.UserSession.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
            string attributesTableName = this.UserSession.DBCache.GetAttributesTableName(this.ObjectType);
            string str1 = "F" + attributeType.AttributeID.ToString();
            string str2;
            if (!(dataManager.DataProvider.Name != "Linter"))
              str2 = string.Format("UPDATE {0} JOIN {1} SET {2} = {3}  WHERE {1}.F_OBJECT_ID = {0}.F_OBJECT_ID AND {1}.F_ATTRIBUTE_ID = {4} AND {1}.F_INLIST_ID = 0 AND {5} IS NOT NULL", (object) this.ViewName, (object) attributesTableName, (object) str1, (object) SqlHelper.MakeCASTString(attributesTableName, attributeType.TextFieldName, attributeType, dataManager.DataProvider), (object) attributeType.AttributeID, (object) attributeType.TextFieldName);
            else
              str2 = string.Format("UPDATE {0} SET {1} = (SELECT {2} FROM {4} WHERE {4}.F_OBJECT_ID = {0}.F_OBJECT_ID AND {4}.F_ATTRIBUTE_ID = {3} AND {4}.F_INLIST_ID = 0 AND {2} IS NOT NULL)", (object) this.ViewName, (object) str1, (object) attributeType.TextFieldName, (object) attributeType.AttributeID, (object) attributesTableName);
            string commandText1 = str2;
            dataManager.ExecuteNonQuery(commandText1);
            if (dataTable.Columns.IndexOf(str1 + "ID") > -1)
            {
              string str3;
              if (!(dataManager.DataProvider.Name != "Linter"))
                str3 = string.Format("UPDATE {0} JOIN {1} SET {2}ID = {3}  WHERE {1}.F_OBJECT_ID = {0}.F_OBJECT_ID AND {1}.F_ATTRIBUTE_ID = {4} AND {1}.F_INLIST_ID = 0 AND {3} IS NOT NULL", (object) this.ViewName, (object) attributesTableName, (object) str1, (object) this.MakeFieldString(attributesTableName, "F_INTEGER_VALUE"), (object) attributeType.AttributeID);
              else
                str3 = string.Format("UPDATE {0} SET {1}ID = (SELECT F_INTEGER_VALUE FROM {3} WHERE {3}.F_OBJECT_ID = {0}.F_OBJECT_ID AND {3}.F_ATTRIBUTE_ID = {2} AND {3}.F_INLIST_ID = 0 AND F_INTEGER_VALUE IS NOT NULL)", (object) this.ViewName, (object) str1, (object) attributeType.AttributeID, (object) attributesTableName);
              string commandText2 = str3;
              dataManager.ExecuteNonQuery(commandText2);
            }
            if (dataTable.Columns.IndexOf(str1 + "ID2") > -1)
            {
              string str4;
              if (!(dataManager.DataProvider.Name != "Linter"))
                str4 = string.Format("UPDATE {0} JOIN {1} SET {2}ID2 = {3}  WHERE {1}.F_OBJECT_ID = {0}.F_OBJECT_ID AND {1}.F_ATTRIBUTE_ID = {4} AND {1}.F_INLIST_ID = 0 AND {3} IS NOT NULL", (object) this.ViewName, (object) attributesTableName, (object) str1, (object) this.MakeFieldString(attributesTableName, "F_DOUBLE_VALUE"), (object) attributeType.AttributeID);
              else
                str4 = string.Format("UPDATE {0} SET {1}ID2 = (SELECT F_DOUBLE_VALUE FROM {3} WHERE {3}.F_OBJECT_ID = {0}.F_OBJECT_ID AND {3}.F_ATTRIBUTE_ID = {2} AND {3}.F_INLIST_ID = 0 AND F_DOUBLE_VALUE IS NOT NULL)", (object) this.ViewName, (object) str1, (object) attributeType.AttributeID, (object) attributesTableName);
              string commandText3 = str4;
              dataManager.ExecuteNonQuery(commandText3);
            }
            if (dataTable.Columns.IndexOf(str1 + "ID3") > -1)
            {
              string str5;
              if (!(dataManager.DataProvider.Name != "Linter"))
                str5 = string.Format("UPDATE {0} JOIN {1} SET {2}ID3 = {3}  WHERE {1}.F_OBJECT_ID = {0}.F_OBJECT_ID AND {1}.F_ATTRIBUTE_ID = {4} AND {1}.F_INLIST_ID = 0 AND {3} IS NOT NULL", (object) this.ViewName, (object) attributesTableName, (object) str1, (object) this.MakeFieldString(attributesTableName, "F_DATE_VALUE"), (object) attributeType.AttributeID);
              else
                str5 = string.Format("UPDATE {0} SET {1}ID3 = (SELECT F_DATE_VALUE FROM {3} WHERE {3}.F_OBJECT_ID = {0}.F_OBJECT_ID AND {3}.F_ATTRIBUTE_ID = {2} AND {3}.F_INLIST_ID = 0 AND F_DATE_VALUE IS NOT NULL)", (object) this.ViewName, (object) str1, (object) attributeType.AttributeID, (object) attributesTableName);
              string commandText4 = str5;
              dataManager.ExecuteNonQuery(commandText4);
            }
          }
        }
        foreach (string commandText in indexes)
          dataManager.ExecuteNonQuery(commandText);
      }
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
    finally
    {
      dataManager.SetNormalCommandTimeout();
    }
  }

  private string MakeFieldString(string sourceTableName, string sourceFieldName)
  {
    return !(sourceTableName != string.Empty) ? sourceFieldName : $"{sourceTableName}.{sourceFieldName}";
  }

  public int LifetimeReserve
  {
    get => Convert.ToInt32(this.paramsTable[40]);
    set
    {
      if (this.LifetimeReserve == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_472") + value.ToString();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_DEL_TIME");
      this.UserSession.StartTransaction();
      try
      {
        if (value != int.MaxValue && (value < 0 || value > 36500))
          throw new KernelExceptionID(sc_13393.ssp_appserver_13468(794872427), (object) value, (object) 0, (object) 36500);
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13393.ssp_appserver_13469()}{value.ToString()} WHERE F_OBJECT_TYPE = {this.ObjectType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue("F_OBJECT_TYPE = " + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_DEL_TIME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[40] = (object) value;
        if (value == 0)
        {
          DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable(string.Format(sc_13393.ssp_appserver_13470(), (object) this.ObjectType, (object) this.UserSession.IdentHelper.DeletedID));
          for (int index = 0; index < dataTable.Rows.Count; ++index)
            this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false)?.Delete(0L);
        }
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString("Kernel_473") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (!(ex is AccessDeniedException))
          throw new KernelException(str, ex);
        throw;
      }
    }
  }

  public bool IsChildType(int objectTypeID)
  {
    if (objectTypeID == this.ObjectType)
      return true;
    bool flag = false;
    ArrayList objsTreeList = new ArrayList();
    this.FillChildrenList(objsTreeList);
    for (int index = 0; index < objsTreeList.Count; ++index)
    {
      if ((int) objsTreeList[index] == this.ObjectType)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public void SetGUID(Guid guid) => throw new OperationNotApplicableException();

  public int SchemaID
  {
    get => Convert.ToInt32(this.paramsTable[25]);
    set
    {
      if (this.SchemaID == value)
        return;
      IDBLCSchema lcSchema = this.UserSession.GetLCSchema(value);
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_474") + lcSchema.Name;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      if (lcSchema.GetStepsCollection().GetFirstStep() <= 0)
        throw new KernelExceptionID(382);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_SCHEMA_ID");
      try
      {
        string str = (lcSchema as IDBSubjectArea).SubjectAreas.Trim();
        if (this.SubjectAreas.Trim() != string.Empty && str != string.Empty)
        {
          bool flag = false;
          foreach (char subjectArea in this.SubjectAreas)
          {
            if (str.IndexOf(subjectArea) > -1)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            throw new KernelExceptionID(sc_13393.ssp_appserver_13471(1263456639));
        }
        this.UserSession.DataManager.ExecuteNonQuery(sc_13393.ssp_appserver_13472(), this.UserSession.DataManager.Parameter("shID", (object) value), this.UserSession.DataManager.Parameter("typeID", (object) this.ObjectType));
        this.UserSession.DBCache.ChangeTableValue(sc_13393.ssp_appserver_13473() + this.ObjectType.ToString(), "IMS_OBJECT_TYPES", "F_SCHEMA_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[25] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_475") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  private int ChangeObjectsSchema(bool throwException)
  {
    int num1 = 0;
    int num2 = -1;
    DataTable table = this.UserSession.GetLifecycleStepCollection(this.ObjectType).GetSchema().Tables["IMS_LC_STEPS"];
    IDBLifecycleStep[] dbLifecycleStepArray = new IDBLifecycleStep[table.Rows.Count];
    StringBuilder stringBuilder = new StringBuilder("F_LC_STEP NOT IN (");
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      dbLifecycleStepArray[index] = this.UserSession.GetLifecycleStep(Convert.ToInt32(table.Rows[index]["F_LC_STEP"]));
      stringBuilder.Append(dbLifecycleStepArray[index].LCStep.ToString() + ",");
      if (dbLifecycleStepArray[index].IsFirstStep)
        num2 = index;
    }
    if (num2 == -1)
      throw new KernelExceptionID(382);
    stringBuilder[stringBuilder.Length - 1] = ')';
    DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_LC_STEP FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = {this.ObjectType} AND {stringBuilder.ToString()} ORDER BY F_LC_STEP");
    if (dataTable.Rows.Count > 0)
    {
      int num3 = int.MinValue;
      int index1 = -1;
      for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
      {
        int int32 = Convert.ToInt32(dataTable.Rows[index2][1]);
        int aLevelID = 0;
        if (int32 != num3)
        {
          num3 = int32;
          index1 = -1;
          IDBLifecycleStep lifecycleStep = this.UserSession.GetLifecycleStep(int32);
          aLevelID = lifecycleStep.LevelID;
          for (int index3 = 0; index3 < dbLifecycleStepArray.Length; ++index3)
          {
            if (dbLifecycleStepArray[index3].LevelID == aLevelID)
            {
              if (index1 == -1)
              {
                index1 = index3;
              }
              else
              {
                index1 = -2;
                break;
              }
            }
          }
          if (index1 < 0 && lifecycleStep.IsFirstStep)
            index1 = num2;
        }
        if (index1 >= 0)
        {
          if (this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index2][0]), false) is DBObject dbObject1)
            dbObject1.DoSetLCStep(dbLifecycleStepArray[index1], false);
        }
        else
        {
          if (throwException)
          {
            IDBObject dbObject2 = this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index2][0]));
            if (index1 == -1)
              throw new KernelExceptionID(256 /*0x0100*/, (object) dbObject2.Caption, (object) dbObject2.ObjectID, (object) this.UserSession.GetLifecycleLevel(aLevelID).LevelName, (object) this.UserSession.GetLCSchema(this.SchemaID).Name).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject2.ObjectID));
            throw new KernelExceptionID(sc_13393.ssp_appserver_13475(34048206), (object) dbObject2.Caption, (object) dbObject2.ObjectID, (object) this.UserSession.GetLifecycleLevel(aLevelID).LevelName, (object) this.UserSession.GetLCSchema(this.SchemaID).Name).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject2.ObjectID));
          }
          ++num1;
        }
      }
    }
    return num1;
  }

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(4, (object) this.ObjectType, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_924"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  public bool IsLocalType
  {
    get => (this.Options & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType;
  }

  public string AttributesTableName => "IMV_A" + this.ObjectType.ToString();

  internal void GlobalToLocal()
  {
    IDbManager dataManager = this.UserSession.DataManager;
    try
    {
      dataManager.DataProvider.DropTableIfExists(dataManager, this.AttributesTableName);
      dataManager.DataProvider.CreateObjectsTypeAttrView(this.AttributesTableName, dataManager);
    }
    catch
    {
      dataManager.DataProvider.CreateObjectsTypeAttrView(this.AttributesTableName, dataManager);
    }
    this.RebuildView(-1, true, true);
    this.UserSession.StartTransaction();
    try
    {
      dataManager.SetAdminCommandTimeout();
      dataManager.ExecuteNonQuery(string.Format(sc_13393.ssp_appserver_13476(), (object) this.AttributesTableName, (object) this.ObjectType));
      dataManager.DataProvider.CreateObjectsTypeAttrIndexes(this.AttributesTableName, dataManager, (this.Options & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex);
      dataManager.ExecuteNonQuery(string.Format(sc_13393.ssp_appserver_13477(), (object) this.ObjectType));
      foreach (object parentOptimizerView in this.GetParentOptimizerViews())
        dataManager.ExecuteNonQuery($"DELETE FROM {parentOptimizerView} WHERE F_OBJECT_ID IN (SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_TYPE = {this.ObjectType})");
      dataManager.ExecuteNonQuery($"DELETE FROM IMS_OBJECTS_VIEW WHERE F_OBJECT_ID IN (SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_TYPE = {this.ObjectType})");
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
    finally
    {
      dataManager.SetNormalCommandTimeout();
    }
  }

  private string[] GetParentOptimizerViews()
  {
    List<string> views = new List<string>();
    if (this.ParentTypeID > -1)
      this.AddParentViews(views, this.ParentTypeID);
    string[] parentOptimizerViews = new string[views.Count];
    for (int index = 0; index < views.Count; ++index)
      parentOptimizerViews[index] = views[index];
    return parentOptimizerViews;
  }

  private void AddParentViews(List<string> views, int p)
  {
    DBObjectType objectType = this.UserSession.GetObjectType(p) as DBObjectType;
    bool flag = true;
    try
    {
      this.UserSession.DataManager.DataProvider.CheckTableExists(objectType.ViewName, "F_OBJECT_ID", this.UserSession.DataManager);
    }
    catch
    {
      flag = false;
    }
    if (flag)
      views.Add(objectType.ViewName);
    if (objectType.ParentTypeID <= -1)
      return;
    this.AddParentViews(views, objectType.ParentTypeID);
  }

  private void LocalToGlobal()
  {
    IDbManager dataManager = this.UserSession.DataManager;
    this.UserSession.StartTransaction();
    try
    {
      dataManager.SetAdminCommandTimeout();
      dataManager.ExecuteNonQuery(string.Format(sc_13393.ssp_appserver_13478(), (object) this.AttributesTableName));
      dataManager.ExecuteNonQuery(string.Format(sc_13393.ssp_appserver_13479(), (object) this.AttributesTableName));
      (this.UserSession.GetCustomService(typeof (IAdminUtilsService)) as AdminUtilsService).RebuildObjectsView(this.UserSession.SessionGUID, this.ObjectType);
      ArrayList objsTreeList = new ArrayList();
      this.FillParentsArray(objsTreeList);
      for (int index = 0; index < objsTreeList.Count; ++index)
      {
        if (Convert.ToInt32(objsTreeList[index]) != this.ObjectType)
        {
          DBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(objsTreeList[index])) as DBObjectType;
          objectType.RebuildView(this.ObjectType, objectType.IsLocalType, true);
        }
      }
      dataManager.SetAdminCommandTimeout();
      dataManager.ExecuteNonQuery($"DELETE FROM {this.AttributesTableName}");
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
    finally
    {
      dataManager.SetNormalCommandTimeout();
    }
  }

  public bool CanStoreAttributeByFiledType(FieldTypes[] fldTypes)
  {
    bool anyAttributes = this.AnyAttributes;
    if (!anyAttributes)
    {
      List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(this.ObjectType);
      for (int index1 = attribute4ObjectTypeList.Count - 1; index1 >= 0; --index1)
      {
        for (int index2 = 0; index2 < fldTypes.Length; ++index2)
        {
          if (attribute4ObjectTypeList[index1].FieldType == fldTypes[index2])
            return true;
        }
      }
    }
    return anyAttributes;
  }
}
