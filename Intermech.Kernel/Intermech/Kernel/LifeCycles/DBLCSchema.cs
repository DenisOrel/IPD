// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.LifeCycles.DBLCSchema
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Intermech.Kernel.LifeCycles;

internal class DBLCSchema : 
  DBSessionable,
  IDBLCSchema,
  IDBSecurity,
  IDBGuid,
  IDeletable,
  IDBSubjectArea
{
  private int _SchemaID;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(4);

  public DBLCSchema(UserSession uSession, int schemaID)
    : base(uSession)
  {
    this._SchemaID = schemaID;
    this.paramsTable.Create(this.UserSession.DBCache.GetTable("IMS_LC_SCHEMAS").Rows.Find((object) schemaID));
    if (this.paramsTable.RowsCount == 0)
      throw new KernelExceptionID(sc_13136.ssp_appserver_13137(1055829967), (object) schemaID);
    this.InitSecurityOptions(16 /*0x10*/, (long) schemaID);
  }

  static DBLCSchema()
  {
    DBLCSchema.metadataActions.Add(ActionType.GetAccess, false);
    DBLCSchema.metadataActions.Add(ActionType.SetAccess, false);
    DBLCSchema.metadataActions.Add(ActionType.Delete, false);
    DBLCSchema.metadataActions.Add(ActionType.EditProperties, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBLCSchema.metadataActions);
  }

  public override string ObjectName
  {
    get => string.Format(LocalizationHolder.rm.GetString("Kernel_369"), (object) this.Name);
  }

  public int SchemaID => this._SchemaID;

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(16 /*0x10*/, (object) this.SchemaID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_916"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  public string Name
  {
    get => this.paramsTable[80 /*0x50*/].ToString();
    set
    {
      if (!(this.Name != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_370") + value);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_NAME");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_371"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDName"), value.Length, Consts.MaxObjectNameLength);
        this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13138(), this.UserSession.DataManager.Parameter("nam", (object) value), this.UserSession.DataManager.Parameter("shID", (object) this.SchemaID));
        this.UserSession.DBCache.ChangeTableValue(sc_13136.ssp_appserver_13139() + this.SchemaID.ToString(), "IMS_LC_SCHEMAS", "F_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[80 /*0x50*/] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_372");
        if (ex.Message.IndexOf("IMS_LC_SCHEMAS_NAME") >= 0)
        {
          string message = string.Format(LocalizationHolder.rm.GetString("Kernel_373"), (object) value);
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
      if (!(this.Note != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_374") + value : LocalizationHolder.rm.GetString("Kernel_375"));
      SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDNote"), value.Length, Consts.MaxNoteLength);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_NOTE");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13140(), this.UserSession.DataManager.Parameter("note", (object) value), this.UserSession.DataManager.Parameter("shID", (object) this.SchemaID));
        this.UserSession.DBCache.ChangeTableValue(sc_13136.ssp_appserver_13141() + this.SchemaID.ToString(), "IMS_LC_SCHEMAS", "F_NOTE", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[92] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_376") + ex.Message;
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
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_377") + value.ToString());
      this.CheckAccess(ActionType.EditProperties);
      if (!this.UserSession.CanChangeObject(16 /*0x10*/, (object) this.SchemaID))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_916"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_13136.ssp_appserver_13142(925161715));
        this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13143(), this.UserSession.DataManager.Parameter("guid1", (object) value.ToString()), this.UserSession.DataManager.Parameter("shID", (object) this.SchemaID));
        this.UserSession.DBCache.ChangeTableValue(sc_13136.ssp_appserver_13144() + this.SchemaID.ToString(), "IMS_LC_SCHEMAS", sc_13136.ssp_appserver_13145(), (object) value, (IUserSession) this.UserSession);
        this.paramsTable[76] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_378") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public bool IsDefaultSchema
  {
    get => Convert.ToInt32(this.paramsTable[108]) != 0;
    set
    {
      if (this.IsDefaultSchema == value)
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_379"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_DEFAULT");
      this.UserSession.StartTransaction();
      try
      {
        int newValue = 1;
        this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13146(), this.UserSession.DataManager.Parameter("def_val", (object) newValue), this.UserSession.DataManager.Parameter("shID", (object) this.SchemaID));
        this.UserSession.DBCache.ChangeTableValue(sc_13136.ssp_appserver_13147() + this.SchemaID.ToString(), "IMS_LC_SCHEMAS", "F_DEFAULT", (object) newValue, (IUserSession) this.UserSession);
        this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13148(), this.UserSession.DataManager.Parameter("def_val", (object) Convert.ToInt32(0)), this.UserSession.DataManager.Parameter("shID", (object) this.SchemaID));
        this.UserSession.DBCache.ChangeTableValue(sc_13136.ssp_appserver_13149() + this.SchemaID.ToString(), "IMS_LC_SCHEMAS", "F_DEFAULT", (object) 0, (IUserSession) this.UserSession);
        this.paramsTable[108] = (object) newValue;
        this.UserSession.Commit();
        DBLCSchemaCollection.DefaultSchemaID = this.SchemaID;
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString("Kernel_380") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public byte[] DrawData
  {
    get => this.paramsTable[171] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[171];
    set
    {
      if (value != null && ((IEnumerable<byte>) this.DrawData).SequenceEqual<byte>((IEnumerable<byte>) value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_381"));
      this.CheckAccess(ActionType.EditProperties);
      try
      {
        object newValue;
        if (value == null || value.Length == 0)
        {
          this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13150(), this.UserSession.DataManager.Parameter("shID", (object) this.SchemaID));
          newValue = (object) DBNull.Value;
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13151(), this.UserSession.DataManager.Parameter("icon", (object) value), this.UserSession.DataManager.Parameter(sc_13136.ssp_appserver_13152(), (object) this.SchemaID));
          newValue = (object) value;
        }
        this.UserSession.DBCache.ChangeTableValue("F_SCHEMA_ID = " + this.SchemaID.ToString(), "IMS_LC_SCHEMAS", "F_DRAW_DATA", newValue, (IUserSession) this.UserSession);
        this.paramsTable[171] = newValue;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13136.ssp_appserver_13153()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public LCSchemaOptions Options
  {
    get => (LCSchemaOptions) Convert.ToInt32(this.paramsTable[36]);
    set
    {
      if (this.Options == value)
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_383") + LCSchemaOptionsHelper.GetCaptions(value));
      this.CheckAccess(ActionType.EditProperties);
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13154(), this.UserSession.DataManager.Parameter("opt", (object) Convert.ToUInt32((object) value)), this.UserSession.DataManager.Parameter("shID", (object) this.SchemaID));
        this.UserSession.DBCache.ChangeTableValue(sc_13136.ssp_appserver_13155() + this.SchemaID.ToString(), "IMS_LC_SCHEMAS", sc_13136.ssp_appserver_13156(), (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[36] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_384") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public int Delete(long deleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, LocalizationHolder.rm.GetString(sc_13136.ssp_appserver_13157()));
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied);
    this.CheckAccess(ActionType.Delete);
    if (!this.UserSession.CanChangeObject(16 /*0x10*/, (object) this.SchemaID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_921"), (object) this.Name));
    this.UserSession.StartTransaction();
    try
    {
      if (this.IsDefaultSchema)
        throw new KernelException(LocalizationHolder.rm.GetString(sc_13136.ssp_appserver_13158()));
      IDbManager dataManager = this.UserSession.DataManager;
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJ_TYPE_NAME FROM IMS_OBJECT_TYPES WHERE F_SCHEMA_ID = :shID", dataManager.Parameter("shID", (object) this.SchemaID));
      if (dataTable.Rows.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          stringBuilder.Append(dataTable.Rows[index][0].ToString() + ", ");
        stringBuilder.Length -= 2;
        throw new KernelExceptionID(sc_13136.ssp_appserver_13159(144967537), (object) this.Name, (object) stringBuilder.ToString());
      }
      foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select("F_SCHEMA_ID = " + this.SchemaID.ToString()))
        this.UserSession.GetLifecycleStep(Convert.ToInt32(dataRow["F_LC_STEP"])).Delete((long) (Consts.PurgeMode | Consts.DeleteChildren));
      dataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13160(), dataManager.Parameter("shID", (object) this.SchemaID));
      this.UserSession.DBCache.DeleteRecords("IMS_LC_SCHEMAS", "F_SCHEMA_ID = " + this.SchemaID.ToString(), (IUserSession) this.UserSession);
      this.UserSession.Commit();
      this.Deleted = true;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
    return 0;
  }

  public DBLCSchemaProperties SchemaProperties
  {
    get
    {
      return new DBLCSchemaProperties(this.SchemaID, this.Name, this.Note, this.GUID, this.IsDefaultSchema, this.SubjectAreas, this.Options);
    }
    set
    {
      if (value.SchemaID != this.SchemaID)
        throw new KernelException(sc_13136.ssp_appserver_13161());
      this.UserSession.StartTransaction();
      try
      {
        this.Name = value.Name;
        this.Note = value.Note;
        this.GUID = value.GUID;
        this.Options = value.Options;
        this.SubjectAreas = value.AreaID;
        this.IsDefaultSchema = value.IsDefaultSchema;
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  public IDBLifecycleStepCollection GetStepsCollection()
  {
    return (IDBLifecycleStepCollection) new DBLifecycleStepCollection(this.UserSession, (IDBLCSchema) this, 0);
  }

  public string SubjectAreas
  {
    get => this.paramsTable[89].ToString();
    set
    {
      if (!(this.SubjectAreas != value))
        return;
      IDBSubjectAreaCollection subjectAreaCollection = this.UserSession.GetSubjectAreaCollection();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_387") + subjectAreaCollection.GetAreasCaption(value));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_AREA_ID");
      try
      {
        subjectAreaCollection.ValidateAriasString(value);
        this.UserSession.DataManager.ExecuteNonQuery(sc_13136.ssp_appserver_13162(), this.UserSession.DataManager.Parameter("area", (object) value), this.UserSession.DataManager.Parameter("shID", (object) this.SchemaID));
        this.UserSession.DBCache.ChangeTableValue(sc_13136.ssp_appserver_13163() + this.SchemaID.ToString(), "IMS_LC_SCHEMAS", "F_AREA_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[89] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13136.ssp_appserver_13164()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string SubjectAreasCaption
  {
    get => this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
  }
}
