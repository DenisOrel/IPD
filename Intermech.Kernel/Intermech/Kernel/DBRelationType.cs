// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class DBRelationType : 
  DBAttributableType,
  IDBRelationType,
  IDBAttributableType,
  IDBGuid,
  IDBSubjectArea,
  IDeletable,
  IDBSecurity
{
  private int _RelationType;
  private IDBAttribute4TypeCollection _Attributes;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(8);
  private string _LastEventNote;

  static DBRelationType()
  {
    DBRelationType.metadataActions.Add(ActionType.GetAccess, false);
    DBRelationType.metadataActions.Add(ActionType.SetAccess, false);
    DBRelationType.metadataActions.Add(ActionType.EditProperties, false);
    DBRelationType.metadataActions.Add(ActionType.Delete, false);
    DBRelationType.metadataActions.Add(ActionType.List, true);
    DBRelationType.metadataActions.Add(ActionType.EditLink, true);
    DBRelationType.metadataActions.Add(ActionType.DeleteLink, true);
    DBRelationType.metadataActions.Add(ActionType.AddLink, true);
  }

  public DBRelationType(UserSession uSession, int aRelationTypeID)
    : base(uSession)
  {
    this._RelationType = aRelationTypeID;
    this.paramsTable.Create(uSession.DBCache.GetTable("IMS_RELATION_TYPES").Rows.Find((object) aRelationTypeID));
    if (this.paramsTable.RowsCount == 0)
    {
      DataTable dataTable = uSession.DataManager.ExecuteDataTable(sc_13619.ssp_appserver_13620() + aRelationTypeID.ToString());
      if (dataTable.Rows.Count == 0)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13621()), (object) aRelationTypeID));
      uSession.DBCache.ReloadTables((IUserSession) uSession, uSession.DataManager, "IMS_RELATION_TYPES", "IMS_ATTR4RELATION_TYPES", "IMS_FORMULA_ATTRS", "IMS_TYPES_APPLICABILITY");
      this.paramsTable.Create(dataTable.Rows[0]);
    }
    this.InitSecurityOptions(6, (long) aRelationTypeID);
    this.SetMDExtensionsType(-1, -1, this._RelationType);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBRelationType.metadataActions);
  }

  public override string ObjectName
  {
    get => string.Format(LocalizationHolder.rm.GetString("Kernel_887"), (object) this.Description);
  }

  public int RelationType => this._RelationType;

  public string Description
  {
    get => this.paramsTable[47].ToString();
    set
    {
      if (!(this.Description != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_530") + value : LocalizationHolder.rm.GetString("Kernel_531");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_DESCRIPTION");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_532"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDName"), value.Length, Consts.MaxObjectNameLength);
        this.UserSession.DataManager.ExecuteNonQuery(sc_13619.ssp_appserver_13622() + SqlHelper.QString(value) + sc_13619.ssp_appserver_13623() + this.RelationType.ToString());
        this.UserSession.DBCache.ChangeTableValue("F_RELATION_TYPE = " + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_DESCRIPTION", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[47] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_533");
        if (ex.Message.IndexOf("IMS_RELATION_TYPES_DESCRIPTION") >= 0)
        {
          string message = string.Format(LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13624()), (object) value);
          this.CloseEvent(EventID, EventlogRecordType.Error, str + message);
          throw new AlreadyExistsException(message);
        }
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw;
      }
    }
  }

  public string TypeName
  {
    get => this.paramsTable[144 /*0x90*/].ToString();
    set
    {
      if (!(this.TypeName != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_535") + value : LocalizationHolder.rm.GetString("Kernel_536");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_TYPE_NAME");
      SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13625()));
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13626()), value.Length, Consts.MaxObjectNameLength);
      this.UserSession.StartTransaction();
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_RELATION_TYPES SET F_TYPE_NAME = {SqlHelper.QString(value)}{sc_13619.ssp_appserver_13627()}{this.RelationType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue("F_RELATION_TYPE = " + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_TYPE_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[144 /*0x90*/] = (object) value;
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13628()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        this.UserSession.Rollback();
        throw new KernelException(str, ex);
      }
    }
  }

  public string ReverseName
  {
    get => this.paramsTable[143].ToString();
    set
    {
      if (!(this.ReverseName != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_538") + value : LocalizationHolder.rm.GetString("Kernel_539");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_REVERSE_NAME");
      SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13629()));
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13630()), value.Length, Consts.MaxObjectNameLength);
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"UPDATE IMS_RELATION_TYPES SET F_REVERSE_NAME = {SqlHelper.QString(value)}{sc_13619.ssp_appserver_13631()}{this.RelationType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue("F_RELATION_TYPE = " + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_REVERSE_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[143] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13632()) + ex.Message;
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
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_541") + value : LocalizationHolder.rm.GetString("Kernel_542");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDNote"), value.Length, Consts.MaxNoteLength);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_NOTE");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_13619.ssp_appserver_13633() + SqlHelper.QString(value) + sc_13619.ssp_appserver_13634() + this.RelationType.ToString());
        this.UserSession.DBCache.ChangeTableValue("F_RELATION_TYPE = " + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_NOTE", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[92] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13635()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public bool CheckoutFile
  {
    get => Convert.ToBoolean(this.paramsTable[142]);
    set
    {
      if (this.CheckoutFile == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_544") + value.ToString();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_CHKOUTFILE");
      try
      {
        int newValue = 0;
        if (value)
          newValue = 1;
        this.UserSession.DataManager.ExecuteNonQuery(sc_13619.ssp_appserver_13636() + newValue.ToString() + sc_13619.ssp_appserver_13637() + this.RelationType.ToString());
        this.UserSession.DBCache.ChangeTableValue("F_RELATION_TYPE = " + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_CHKOUTFILE", (object) newValue, (IUserSession) this.UserSession);
        this.paramsTable[142] = (object) newValue;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13638()) + ex.Message;
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
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_546");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_ICON");
      try
      {
        object newValue;
        if (value == null || value.Length == 0)
        {
          this.UserSession.DataManager.ExecuteNonQuery(sc_13619.ssp_appserver_13639() + this.RelationType.ToString());
          newValue = (object) DBNull.Value;
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(sc_13619.ssp_appserver_13640() + this.RelationType.ToString(), this.UserSession.DataManager.Parameter("icon", (object) value));
          newValue = (object) value;
        }
        this.UserSession.DBCache.ChangeTableValue("F_RELATION_TYPE = " + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_ICON", newValue, (IUserSession) this.UserSession);
        this.paramsTable[129] = newValue;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_547") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public bool SaveHistory => false;

  public RelationTypeOptions Options
  {
    get => (RelationTypeOptions) Convert.ToInt32(this.paramsTable[36]);
    set
    {
      if (this.Options == value)
        return;
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_445") + RelationTypeOptionsHelper.GetCaptions(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      foreach (RelationTypeOptions optionsFlag in (RelationTypeOptions[]) Enum.GetValues(typeof (RelationTypeOptions)))
      {
        if ((value & optionsFlag) != (this.Options & optionsFlag) && !this.UserSession.CanChangeObjectElement(6, (object) this.RelationType, ObligatoryElementKeys.GetKeyForObjectOptionsFlag((int) optionsFlag)))
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_929"), (object) EnumDescConverter.GetEnumDescription((Enum) optionsFlag)));
      }
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13619.ssp_appserver_13641()}{Convert.ToInt32((object) value).ToString()} WHERE F_RELATION_TYPE = {this.RelationType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13619.ssp_appserver_13642() + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_OPTIONS", (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[36] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        string str = $"{LocalizationHolder.rm.GetString("RelationTypeOptionsError")} {ex.Message}";
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
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_550") + value.ToString();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      if (!this.UserSession.CanChangeObject(6, (object) this.RelationType))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_929"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_13619.ssp_appserver_13643(1104173967));
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13619.ssp_appserver_13644()}{SqlHelper.QString(value.ToString())} WHERE F_RELATION_TYPE = {this.RelationType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13619.ssp_appserver_13645() + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_GUID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[76] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13646()) + ex.Message;
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
      this._LastEventNote = LocalizationHolder.rm.GetString("Kernel_552") + subjectAreaCollection.GetAreasCaption(value);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_AREA_ID");
      try
      {
        subjectAreaCollection.ValidateAriasString(value);
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13619.ssp_appserver_13647()}{SqlHelper.QString(value)} WHERE F_RELATION_TYPE = {this.RelationType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13619.ssp_appserver_13648() + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_AREA_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[89] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13649()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string SubjectAreasCaption
  {
    get => this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
  }

  public override bool AnyAttributes
  {
    get => Convert.ToBoolean(this.paramsTable[60]);
    set
    {
      if (this.AnyAttributes == value)
        return;
      string str1 = LocalizationHolder.rm.GetString("Kernel_554");
      if (!value)
        str1 = LocalizationHolder.rm.GetString("Kernel_555");
      this._LastEventNote = string.Format(LocalizationHolder.rm.GetString("Kernel_556"), (object) str1);
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_ANY_ATTRIBUTES");
      try
      {
        int newValue = 1;
        if (!value)
        {
          newValue = 0;
          DataTable dataTable1 = this.Attributes.Select("");
          string str2;
          if (dataTable1.Rows.Count > 0)
          {
            StringBuilder stringBuilder = new StringBuilder("F_ATTRIBUTE_ID <> " + dataTable1.Rows[0]["F_ATTRIBUTE_ID"].ToString());
            for (int index = 1; index < dataTable1.Rows.Count; ++index)
              stringBuilder.AppendFormat(" AND F_ATTRIBUTE_ID <> {0}", dataTable1.Rows[index]["F_ATTRIBUTE_ID"]);
            str2 = stringBuilder.ToString();
          }
          else
            str2 = "F_ATTRIBUTE_ID > 0";
          DataTable dataTable2 = this.UserSession.DataManager.ExecuteDataTable($"SELECT R.F_PRJLINK_ID FROM IMS_RELATIONS R, IMS_RELATION_ATTRS A WHERE R.F_RELATION_TYPE = {this.RelationType} AND A.F_PRJLINK_ID = R.F_PRJLINK_ID AND {str2}");
          if (dataTable2.Rows.Count > 0)
          {
            long[] relationsID = new long[dataTable2.Rows.Count];
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
              relationsID[index] = Convert.ToInt64(dataTable2.Rows[index][0]);
            throw new RelationsFoundException($"Нельзя выключить настройку 'Любой атрибут' у типа связей '{this.Description}', т.к. в базе данных есть {dataTable2.Rows.Count} связь(ей) с атрибутами, не назначенными данному типу связей.", $"Связи типа '{this.Description}':", relationsID);
          }
        }
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13619.ssp_appserver_13650()}{newValue.ToString()} WHERE F_RELATION_TYPE = {this.RelationType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13619.ssp_appserver_13651() + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_ANY_ATTRIBUTES", (object) newValue, (IUserSession) this.UserSession);
        this.paramsTable[60] = (object) newValue;
      }
      catch (Exception ex)
      {
        string str3 = LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13652()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str3);
        throw new KernelException(str3, ex);
      }
    }
  }

  public string ShortName
  {
    get => this.paramsTable[79].ToString();
    set
    {
      if (!(this.ShortName != value))
        return;
      this._LastEventNote = value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_558") + value : LocalizationHolder.rm.GetString("Kernel_559");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, this._LastEventNote);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_SHORT_NAME");
      try
      {
        if (value != string.Empty && this.UserSession.DBCache.GetTable("IMS_RELATION_TYPES").Select("F_SHORT_NAME = " + SqlHelper.QString(value)).Length != 0)
          throw new KernelExceptionID(sc_13619.ssp_appserver_13653(1209134461), (object) value);
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDShortName"), value.Length, Consts.MaxShortNameLength);
        this.UserSession.DataManager.ExecuteNonQuery($"{sc_13619.ssp_appserver_13654()}{SqlHelper.QString(value)} WHERE F_RELATION_TYPE = {this.RelationType.ToString()}");
        this.UserSession.DBCache.ChangeTableValue(sc_13619.ssp_appserver_13655() + this.RelationType.ToString(), "IMS_RELATION_TYPES", "F_SHORT_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[79] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13656()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public virtual int Delete(long DeleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, string.Format(LocalizationHolder.rm.GetString("Kernel_888"), (object) this.Description));
    IDbManager dataManager = this.UserSession.DataManager;
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied);
    this.CheckAccess(ActionType.Delete);
    if (!this.UserSession.CanChangeObject(6, (object) this.RelationType))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_930"), (object) this.Description));
    this.UserSession.StartTransaction();
    try
    {
      DataTable table = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES");
      int relationType = this.RelationType;
      string filterExpression = "F_DEFAULT_RELATION = " + relationType.ToString();
      DataRow[] dataRowArray = table.Select(filterExpression);
      if (dataRowArray.Length != 0)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13657()), (object) this.Description, dataRowArray[0]["F_OBJ_TYPE_NAME"]));
      IDbManager dbManager1 = dataManager;
      string str1 = sc_13619.ssp_appserver_13658();
      relationType = this.RelationType;
      string str2 = relationType.ToString();
      string commandText1 = str1 + str2;
      DataTable dataTable = dbManager1.ExecuteDataTable(commandText1);
      if (dataTable.Rows.Count > 0)
      {
        long[] relationsID = new long[dataTable.Rows.Count];
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          relationsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
        throw new RelationsFoundException(string.Format(sc_13619.ssp_appserver_13659(), (object) this.Description, (object) dataTable.Rows.Count), $"Связи типа '{this.Description}':", relationsID);
      }
      IDbManager dbManager2 = dataManager;
      string str3 = sc_13619.ssp_appserver_13660();
      relationType = this.RelationType;
      string str4 = relationType.ToString();
      string commandText2 = str3 + str4;
      dbManager2.ExecuteNonQuery(commandText2);
      IDbManager dbManager3 = dataManager;
      relationType = this.RelationType;
      string commandText3 = "DELETE FROM IMS_POSSIBLE_VALUES WHERE F_RELATION_TYPE = " + relationType.ToString();
      dbManager3.ExecuteNonQuery(commandText3);
      IDbManager dbManager4 = dataManager;
      relationType = this.RelationType;
      string commandText4 = "DELETE FROM IMS_TYPES_APPLICABILITY WHERE F_RELATION_TYPE = " + relationType.ToString();
      dbManager4.ExecuteNonQuery(commandText4);
      IDbManager dbManager5 = dataManager;
      relationType = this.RelationType;
      string commandText5 = "DELETE FROM IMS_FORMULA_ATTRS WHERE F_RELATION_TYPE = " + relationType.ToString();
      dbManager5.ExecuteNonQuery(commandText5);
      IDbManager dbManager6 = dataManager;
      string str5 = sc_13619.ssp_appserver_13661();
      relationType = this.RelationType;
      string str6 = relationType.ToString();
      string commandText6 = str5 + str6;
      dbManager6.ExecuteNonQuery(commandText6);
      ICacheDataset dbCache1 = this.UserSession.DBCache;
      string str7 = sc_13619.ssp_appserver_13662();
      relationType = this.RelationType;
      string str8 = relationType.ToString();
      string condition1 = str7 + str8;
      UserSession userSession1 = this.UserSession;
      dbCache1.DeleteRecords("IMS_RELATION_TYPES", condition1, (IUserSession) userSession1);
      ICacheDataset dbCache2 = this.UserSession.DBCache;
      relationType = this.RelationType;
      string condition2 = "F_RELATION_TYPE = " + relationType.ToString();
      UserSession userSession2 = this.UserSession;
      dbCache2.DeleteRecords("IMS_TYPES_APPLICABILITY", condition2, (IUserSession) userSession2);
      ICacheDataset dbCache3 = this.UserSession.DBCache;
      string str9 = sc_13619.ssp_appserver_13663();
      relationType = this.RelationType;
      string str10 = relationType.ToString();
      string condition3 = str9 + str10;
      UserSession userSession3 = this.UserSession;
      dbCache3.DeleteRecords("IMS_ATTR4RELATION_TYPES", condition3, (IUserSession) userSession3);
      ICacheDataset dbCache4 = this.UserSession.DBCache;
      relationType = this.RelationType;
      string condition4 = "F_RELATION_TYPE = " + relationType.ToString();
      UserSession userSession4 = this.UserSession;
      dbCache4.DeleteRecords("IMS_FORMULA_ATTRS", condition4, (IUserSession) userSession4);
      if (ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is IFormDesignerService service)
        service.RemoveTypeFromCache(this._RelationType, AttributableElements.Relation);
      this.UserSession.Commit();
      this.Deleted = true;
    }
    catch (Exception ex)
    {
      string str = ex.Message.IndexOf("FK_RELATIONS_TYPES") <= -1 ? string.Format(LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13665()), (object) this.Description, (object) ex.Message) : string.Format(LocalizationHolder.rm.GetString(sc_13619.ssp_appserver_13664()), (object) this.Description);
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, str);
      if (!(ex is AccessDeniedException))
        throw new KernelException(str, ex);
      throw;
    }
    return 0;
  }

  public RelationTypeProperties PropertiesStructure
  {
    get
    {
      return new RelationTypeProperties(this.RelationType, this.TypeName, this.ReverseName, this.Note, this.CheckoutFile, this.SaveHistory, this.Description, this.GUID, this.SubjectAreas, this.AnyAttributes, this.ShortName, this.Options);
    }
    set
    {
      if (value.RelationType != this.RelationType)
        throw new KernelException(sc_13619.ssp_appserver_13666());
      this.UserSession.StartTransaction();
      try
      {
        this.TypeName = value.TypeName;
        this.ReverseName = value.ReverseName;
        this.Note = value.Note;
        this.CheckoutFile = value.CheckoutFile;
        this.Description = value.Description;
        this.SubjectAreas = value.AreaID;
        this.AnyAttributes = value.AnyAttributes;
        this.ShortName = value.ShortName;
        this.GUID = value.RelationTypeGuid;
        this.Options = value.Options;
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

  public override IDBAttribute4TypeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
        this._Attributes = (IDBAttribute4TypeCollection) new DBAttribute4RelationTypeCollection(this.UserSession, this.RelationType, false);
      return this._Attributes;
    }
  }

  public IDBAttribute4TypeCollection VisibleAttributes
  {
    get
    {
      return (IDBAttribute4TypeCollection) new DBAttribute4RelationTypeCollection(this.UserSession, this.RelationType, true);
    }
  }

  public bool HasAttribute(int attributeID)
  {
    if (attributeID < 0)
      return ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeID) == AttributeSourceTypes.Relation;
    return this.AnyAttributes || this.Attributes.GetAttributeByID(attributeID, false) != null;
  }

  internal string ViewName => SqlHelper.viewForRelationTypePrefix + this.RelationType.ToString();

  internal void InsertIntoView()
  {
    this.UserSession.DataManager.ExecuteNonQuery($"INSERT INTO {this.ViewName} (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR) SELECT F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR FROM IMS_RELATIONS WHERE F_RELATION_TYPE = {this.RelationType}");
  }

  public void RebuildView()
  {
    this.CheckAccess(ActionType.EditProperties);
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable attributes = this.Attributes.Select("");
    this.UserSession.StartTransaction();
    try
    {
      List<string> indexes = new List<string>();
      if (this.UserSession.QueryBuilder.RebuildTypedView(this.ViewName, attributes, AttributeSourceTypes.Relation, dataManager, false, true, false, indexes))
      {
        DataTable dataTable = dataManager.ExecuteDataTable($"SELECT * FROM {this.ViewName} WHERE F_PRJLINK_ID = -1");
        dataManager.SetAdminCommandTimeout();
        this.InsertIntoView();
        foreach (DataRow row in (InternalDataCollectionBase) attributes.Rows)
        {
          if (Convert.ToInt32(row["F_INVIEW"]) != 0)
          {
            IDBAttributeType attributeType = this.UserSession.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
            string str = "F" + attributeType.AttributeID.ToString();
            IDbManager dbManager = dataManager;
            string commandText;
            if (!(dataManager.DataProvider.Name != "Linter"))
              commandText = string.Format("UPDATE {0} JOIN IMS_RELATION_ATTRS SET {1} = {2}  WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID = {0}.F_PRJLINK_ID AND IMS_RELATION_ATTRS.F_ATTRIBUTE_ID = {3} AND IMS_RELATION_ATTRS.F_INLIST_ID = 0 AND IMS_RELATION_ATTRS.{4} IS NOT NULL", (object) this.ViewName, (object) str, (object) SqlHelper.MakeCASTString("IMS_RELATION_ATTRS", attributeType.TextFieldName, attributeType, dataManager.DataProvider), (object) attributeType.AttributeID, (object) attributeType.TextFieldName);
            else
              commandText = string.Format("UPDATE {0} SET {1} = (SELECT {2} FROM IMS_RELATION_ATTRS WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID = {0}.F_PRJLINK_ID AND IMS_RELATION_ATTRS.F_ATTRIBUTE_ID = {3} AND IMS_RELATION_ATTRS.F_INLIST_ID = 0 AND IMS_RELATION_ATTRS.{2} IS NOT NULL)", (object) this.ViewName, (object) str, (object) attributeType.TextFieldName, (object) attributeType.AttributeID);
            dbManager.ExecuteNonQuery(commandText);
            if (dataTable.Columns.IndexOf(str + "ID") > -1)
              dataManager.ExecuteNonQuery(dataManager.DataProvider.Name != "Linter" ? string.Format("UPDATE {0} SET {1}ID = (SELECT F_INTEGER_VALUE FROM IMS_RELATION_ATTRS WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID = {0}.F_PRJLINK_ID AND IMS_RELATION_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_RELATION_ATTRS.F_INLIST_ID = 0 AND IMS_RELATION_ATTRS.F_INTEGER_VALUE IS NOT NULL)", (object) this.ViewName, (object) str, (object) attributeType.AttributeID) : string.Format("UPDATE {0} JOIN IMS_RELATION_ATTRS SET {1}ID = IMS_RELATION_ATTRS.F_INTEGER_VALUE  WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID = {0}.F_PRJLINK_ID AND IMS_RELATION_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_RELATION_ATTRS.F_INLIST_ID = 0 AND IMS_RELATION_ATTRS.F_INTEGER_VALUE IS NOT NULL", (object) this.ViewName, (object) str, (object) attributeType.AttributeID));
            if (dataTable.Columns.IndexOf(str + "ID2") > -1)
              dataManager.ExecuteNonQuery(dataManager.DataProvider.Name != "Linter" ? string.Format("UPDATE {0} SET {1}ID2 = (SELECT F_DOUBLE_VALUE FROM IMS_RELATION_ATTRS WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID = {0}.F_PRJLINK_ID AND IMS_RELATION_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_RELATION_ATTRS.F_INLIST_ID = 0 AND IMS_RELATION_ATTRS.F_DOUBLE_VALUE IS NOT NULL)", (object) this.ViewName, (object) str, (object) attributeType.AttributeID) : string.Format("UPDATE {0} JOIN IMS_RELATION_ATTRS SET {1}ID2 = IMS_RELATION_ATTRS.F_DOUBLE_VALUE  WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID = {0}.F_PRJLINK_ID AND IMS_RELATION_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_RELATION_ATTRS.F_INLIST_ID = 0 AND IMS_RELATION_ATTRS.F_DOUBLE_VALUE IS NOT NULL", (object) this.ViewName, (object) str, (object) attributeType.AttributeID));
            if (dataTable.Columns.IndexOf(str + "ID3") > -1)
              dataManager.ExecuteNonQuery(dataManager.DataProvider.Name != "Linter" ? string.Format("UPDATE {0} SET {1}ID3 = (SELECT F_DATE_VALUE FROM IMS_RELATION_ATTRS WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID = {0}.F_PRJLINK_ID AND IMS_RELATION_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_RELATION_ATTRS.F_INLIST_ID = 0 AND IMS_RELATION_ATTRS.F_DATE_VALUE IS NOT NULL)", (object) this.ViewName, (object) str, (object) attributeType.AttributeID) : string.Format("UPDATE {0} JOIN IMS_RELATION_ATTRS SET {1}ID3 = IMS_RELATION_ATTRS.F_DATE_VALUE  WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID = {0}.F_PRJLINK_ID AND IMS_RELATION_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_RELATION_ATTRS.F_INLIST_ID = 0 AND IMS_RELATION_ATTRS.F_DATE_VALUE IS NOT NULL", (object) this.ViewName, (object) str, (object) attributeType.AttributeID));
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

  public void SetGUID(Guid guid) => throw new OperationNotApplicableException();

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(6, (object) this.RelationType, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_929"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  internal bool CanQuickRelationsCopy()
  {
    bool flag = !this.AnyAttributes;
    if (flag)
    {
      List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(this.RelationType);
      for (int index = relationTypeList.Count - 1; index >= 0; --index)
      {
        if (relationTypeList[index].FieldType == FieldTypes.ftBlob || relationTypeList[index].FieldType == FieldTypes.ftFile || relationTypeList[index].FieldType == FieldTypes.ftMemo || relationTypeList[index].FieldType == FieldTypes.ftShortBlob)
        {
          flag = false;
          break;
        }
      }
    }
    return flag;
  }

  public bool MustCheckAccessLevel
  {
    get
    {
      return !ServerConsts.EnableSecret2Public && !this.GUID.Equals(SystemGUIDs.relationTypeAttachments);
    }
  }
}
