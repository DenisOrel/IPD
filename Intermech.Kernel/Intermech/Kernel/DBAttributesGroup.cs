// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributesGroup
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class DBAttributesGroup : 
  DBSessionable,
  IDBAttributesGroup,
  IDBLanguage,
  IDBSubjectArea,
  IDeletable,
  IDBSecurity,
  IDBGuid
{
  private int _GroupID;
  private IDBAttributeTypeCollection _Attributes;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(7);

  public DBAttributesGroup(UserSession uSession, int aGroupID)
    : base(uSession)
  {
    this._GroupID = aGroupID;
    this.paramsTable.Create(this.UserSession.DBCache.GetTable("IMS_ATTR_GROUPS").Rows.Find((object) aGroupID));
    if (this.paramsTable.RowsCount == 0)
      throw new KernelExceptionID(sc_12395.ssp_appserver_12396(1807185414), (object) aGroupID);
    this.InitSecurityOptions(12, (long) aGroupID);
  }

  private void CheckSystemGroup()
  {
    if (this._GroupID == -1)
      throw new KernelException(LocalizationHolder.rm.GetString(sc_12395.ssp_appserver_12397()));
  }

  static DBAttributesGroup()
  {
    DBAttributesGroup.metadataActions.Add(ActionType.GetAccess, false);
    DBAttributesGroup.metadataActions.Add(ActionType.SetAccess, false);
    DBAttributesGroup.metadataActions.Add(ActionType.EditProperties, false);
    DBAttributesGroup.metadataActions.Add(ActionType.AddLink, false);
    DBAttributesGroup.metadataActions.Add(ActionType.DeleteLink, false);
    DBAttributesGroup.metadataActions.Add(ActionType.Delete, false);
    DBAttributesGroup.metadataActions.Add(ActionType.List, true);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAttributesGroup.metadataActions);
  }

  public override string ObjectName
  {
    get => string.Format(LocalizationHolder.rm.GetString("Kernel_10"), (object) this.GroupName);
  }

  public int GroupID => this._GroupID;

  public string GroupName
  {
    get => this.paramsTable[57].ToString();
    set
    {
      this.CheckSystemGroup();
      if (!(this.GroupName != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_11") + value : LocalizationHolder.rm.GetString("Kernel_12"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_GROUP_NAME");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_13"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("Kernel_13"), value.Length, Consts.MaxObjectNameLength);
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_12395.ssp_appserver_12398(), (object) "F_GROUP_NAME"), this.UserSession.DataManager.Parameter("grpName", (object) value), this.UserSession.DataManager.Parameter("grpID", (object) this.GroupID));
        this.UserSession.DBCache.ChangeTableValue("F_GROUP_ID = " + this.GroupID.ToString(), "IMS_ATTR_GROUPS", "F_GROUP_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[57] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_14");
        if (ex.Message.IndexOf("IMS_ATTR_GROUPS_GROUP_NAME") >= 0)
        {
          string message = string.Format(LocalizationHolder.rm.GetString("Kernel_15"), (object) value);
          this.CloseEvent(EventID, EventlogRecordType.Error, str + message);
          throw new AlreadyExistsException(message);
        }
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw;
      }
    }
  }

  public string Note
  {
    get => this.paramsTable[92].ToString();
    set
    {
      this.CheckSystemGroup();
      if (!(this.Note != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_16") + value : LocalizationHolder.rm.GetString("Kernel_17"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_NOTE");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_12395.ssp_appserver_12399()}F_NOTE = {SqlHelper.QString(value)} WHERE F_GROUP_ID = {this.GroupID.ToString()}");
        this.UserSession.DBCache.ChangeTableValue("F_GROUP_ID = " + this.GroupID.ToString(), "IMS_ATTR_GROUPS", "F_NOTE", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[92] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_18") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public int IncludeAttribute(int attributeID)
  {
    return this.IncludeAttribute(new int[1]{ attributeID });
  }

  internal void FastIncludeAttribute(int attributeID)
  {
    this.UserSession.DataManager.ExecuteNonQuery($"INSERT INTO IMS_ATTR_IN_GROUPS (F_GROUP_ID, F_ATTRIBUTE_ID) VALUES ({this.GroupID.ToString()}, {attributeID.ToString()})");
  }

  public int IncludeAttribute(int[] attributeIDs)
  {
    this.CheckSystemGroup();
    IDbManager dataManager = this.UserSession.DataManager;
    foreach (int attributeId in attributeIDs)
    {
      IDBAttributeType attributeType = this.UserSession.GetAttributeType(attributeId);
      long EventID = this.AddEvent(0L, ActionType.AddLink, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_19") + attributeType.Name);
      this.CheckAccess(ActionType.AddLink);
      try
      {
        dataManager.ExecuteNonQuery($"INSERT INTO IMS_ATTR_IN_GROUPS (F_GROUP_ID, F_ATTRIBUTE_ID) VALUES ({this.GroupID.ToString()}, {attributeId.ToString()})");
        DataTable dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_ATTR_IN_GROUPS WHERE F_GROUP_ID = :grpID AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("grpID", (object) this.GroupID), dataManager.Parameter("attrID", (object) attributeId));
        if (dataTable.Rows.Count <= 0)
          throw new KernelException("Не найдена только что добавленная запись в таблице IMS_ATTR_IN_GROUPS");
        this.UserSession.DBCache.AddRow("IMS_ATTR_IN_GROUPS", dataTable.Rows[0], (IUserSession) this.UserSession);
        (this.EventHelper as EventLogHelper).OnAfterIncludeAttributeToGroup((IDBAttributesGroup) this, attributeId);
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        bool flag = false;
        if (message.ToUpper().IndexOf("PRIMARY") > -1 || message.ToLower().IndexOf("unique constraint") > -1 || message.ToUpper().IndexOf("ORA-00001") > -1)
        {
          message = LocalizationHolder.rm.GetString("Kernel_20");
          flag = true;
        }
        string str = string.Format(LocalizationHolder.rm.GetString("Kernel_21"), (object) attributeType.Name, (object) message);
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        if (flag)
          throw new KernelExceptionID(sc_12395.ssp_appserver_12400(282468483), (object) attributeType.Name, (object) this.GroupName);
        throw new KernelException(str, ex);
      }
    }
    (this.UserSession.DBCache as CacheDataset).FillAttrGroupNames(dataManager.ExecuteDataTable("SELECT * FROM IMS_ATTR_IN_GROUPS"));
    return 0;
  }

  public int ParentID
  {
    get => Convert.ToInt32(this.paramsTable[128 /*0x80*/]);
    set
    {
      if (this.ParentID == value)
        return;
      this.CheckChangeEnable("F_PARENT_ID");
      IDBAttributesGroup dbAttributesGroup = (IDBAttributesGroup) null;
      long EventID = 0;
      try
      {
        if (value > 0)
        {
          dbAttributesGroup = this.UserSession.GetAttributesGroup(value);
          EventID = (dbAttributesGroup as DBSessionable).AddEvent(0L, ActionType.AddLink, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_10"), (object) this.GroupName));
          (dbAttributesGroup as IDBSecurity).CheckAccess(ActionType.AddLink);
        }
        else
        {
          dbAttributesGroup = this.UserSession.GetAttributesGroup(this.ParentID, false);
          if (dbAttributesGroup != null)
          {
            EventID = (dbAttributesGroup as DBSessionable).AddEvent(0L, ActionType.DeleteLink, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_10"), (object) this.GroupName));
            (dbAttributesGroup as IDBSecurity).CheckAccess(ActionType.DeleteLink);
          }
        }
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_12395.ssp_appserver_12401()}F_PARENT_ID = {value.ToString()} WHERE F_GROUP_ID = {this.GroupID.ToString()}");
        this.UserSession.DBCache.ChangeTableValue("F_GROUP_ID = " + this.GroupID.ToString(), "IMS_ATTR_GROUPS", "F_PARENT_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[128 /*0x80*/] = (object) value;
        if (EventID <= 0L)
          return;
        (dbAttributesGroup as DBSessionable).CloseEvent(EventID, EventlogRecordType.Information);
      }
      catch (Exception ex)
      {
        if (EventID > 0L)
          (dbAttributesGroup as DBSessionable).CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
        throw;
      }
    }
  }

  public int ExcludeAttribute(params int[] attributeIDs)
  {
    this.CheckSystemGroup();
    foreach (int attributeId in attributeIDs)
    {
      IDBAttributeType attributeType = this.UserSession.GetAttributeType(attributeId);
      long EventID = this.AddEvent(0L, ActionType.DeleteLink, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_22") + attributeType.Name);
      this.CheckAccess(ActionType.DeleteLink);
      if (!this.UserSession.CanChangeObjectElement(3, (object) attributeId, ObligatoryElementKeys.GetKeyForObjectProperty("F_GROUP_ID")))
        throw new KernelException($"Нельзя исключать атрибут {MetaDataHelper.GetAttributeTypeName(attributeId)} из группы {this.GroupName}");
      try
      {
        string condition = $"F_GROUP_ID = {this.GroupID.ToString()} AND F_ATTRIBUTE_ID = {attributeId.ToString()}";
        this.UserSession.DataManager.ExecuteNonQuery(sc_12395.ssp_appserver_12402() + condition);
        this.UserSession.DBCache.DeleteRecords("IMS_ATTR_IN_GROUPS", condition, (IUserSession) this.UserSession);
        (this.UserSession.DBCache as CacheDataset).FillAttrGroupNames(this.UserSession.DBCache.GetTable("IMS_ATTR_IN_GROUPS"));
        (this.EventHelper as EventLogHelper).OnAfterExcludeAttributeFromGroup((IDBAttributesGroup) this, attributeId);
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, string.Format(LocalizationHolder.rm.GetString("Kernel_23"), (object) attributeType.Name, (object) ex.Message));
        throw;
      }
    }
    return 0;
  }

  public string LanguageID
  {
    get => this.paramsTable[69].ToString().Trim();
    set
    {
      this.CheckSystemGroup();
      if (!(this.LanguageID != value))
        return;
      IDBLanguageType language = this.UserSession.GetLanguage(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_24") + language.LanguageName);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_LANGUAGE_ID");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_12395.ssp_appserver_12403()}F_LANGUAGE_ID = {SqlHelper.QString(value)} WHERE F_GROUP_ID = {this.GroupID.ToString()}");
        this.UserSession.DBCache.ChangeTableValue("F_GROUP_ID = " + this.GroupID.ToString(), "IMS_ATTR_GROUPS", "F_LANGUAGE_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[69] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_25");
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw new KernelException(str + ex.Message, ex);
      }
    }
  }

  public string LanguageName
  {
    get
    {
      return this.LanguageID == string.Empty ? string.Empty : this.UserSession.GetLanguage(this.LanguageID).LanguageName;
    }
  }

  public bool IsDefaultLanguage
  {
    get
    {
      return this.LanguageID == string.Empty || this.UserSession.GetLanguage(this.LanguageID).IsDefaultLanguage;
    }
  }

  public string SubjectAreas
  {
    get => this.paramsTable[89].ToString();
    set
    {
      this.CheckSystemGroup();
      if (!(this.SubjectAreas != value))
        return;
      IDBSubjectAreaCollection subjectAreaCollection = this.UserSession.GetSubjectAreaCollection();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_26") + subjectAreaCollection.GetAreasCaption(value));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_AREA_ID");
      try
      {
        subjectAreaCollection.ValidateAriasString(value);
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_12395.ssp_appserver_12404()}F_AREA_ID = {SqlHelper.QString(value)}{sc_12395.ssp_appserver_12405()}{this.GroupID.ToString()}");
        this.UserSession.DBCache.ChangeTableValue("F_GROUP_ID = " + this.GroupID.ToString(), "IMS_ATTR_GROUPS", "F_AREA_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[89] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_27");
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw new KernelException(str + ex.Message, ex);
      }
    }
  }

  public string SubjectAreasCaption
  {
    get => this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
  }

  public int Delete(long DeleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, LocalizationHolder.rm.GetString("Kernel_28"));
    this.CheckSystemGroup();
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied);
    this.CheckAccess(ActionType.Delete);
    try
    {
      DataTable dataTable = this.UserSession.CanChangeObject(12, (object) this.GroupID) ? this.UserSession.DBCache.GetTable("IMS_ATTR_GROUPS") : throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_918"), (object) this.GroupName));
      int groupId = this.GroupID;
      string filterExpression = "F_PARENT_ID = " + groupId.ToString();
      if (dataTable.Select(filterExpression).Length != 0)
        throw new KernelExceptionID(393, (object) this.GroupName);
      IDbManager dataManager = this.UserSession.DataManager;
      groupId = this.GroupID;
      string commandText = "DELETE FROM IMS_ATTR_GROUPS WHERE F_GROUP_ID = " + groupId.ToString();
      dataManager.ExecuteNonQuery(commandText);
      ICacheDataset dbCache1 = this.UserSession.DBCache;
      groupId = this.GroupID;
      string condition1 = "F_GROUP_ID = " + groupId.ToString();
      UserSession userSession1 = this.UserSession;
      dbCache1.DeleteRecords("IMS_ATTR_GROUPS", condition1, (IUserSession) userSession1);
      ICacheDataset dbCache2 = this.UserSession.DBCache;
      groupId = this.GroupID;
      string condition2 = "F_GROUP_ID = " + groupId.ToString();
      UserSession userSession2 = this.UserSession;
      dbCache2.DeleteRecords("IMS_ATTR_IN_GROUPS", condition2, (IUserSession) userSession2);
      this.Deleted = true;
    }
    catch (Exception ex)
    {
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
    return 0;
  }

  public IDBAttributeTypeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
        this._Attributes = this.UserSession.GetAttributeTypeCollection(this.GroupID);
      return this._Attributes;
    }
  }

  public void SetGUID(Guid guid) => this.GUID = guid;

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(12, (object) this.GroupID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_913"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  public bool HasAttribute(int attrID)
  {
    return this.UserSession.DBCache.GetTable("IMS_ATTR_IN_GROUPS").Select($"F_GROUP_ID = {this.GroupID} AND F_ATTRIBUTE_ID = {attrID}").Length != 0;
  }

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public Guid GUID
  {
    get => new Guid(this.paramsTable[76].ToString());
    set
    {
      if (!(value != this.GUID))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_29") + value.ToString());
      this.CheckAccess(ActionType.EditProperties);
      if (!this.UserSession.CanChangeObject(12, (object) this.GroupID))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_913"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_12395.ssp_appserver_12406(1527971257));
        this.UserSession.DataManager.ExecuteNonQuery(sc_12395.ssp_appserver_12407() + SqlHelper.QString(value.ToString()) + sc_12395.ssp_appserver_12408() + this.GroupID.ToString());
        this.UserSession.DBCache.ChangeTableValue("F_GROUP_ID = " + this.GroupID.ToString(), "IMS_ATTR_GROUPS", "F_GUID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[76] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_30") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }
}
