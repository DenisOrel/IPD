// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBLifecycleStep
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Attributes;
using Intermech.Kernel.LifeCycles;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class DBLifecycleStep : 
  DBSessionable,
  IDBLifecycleStep,
  IDeletable,
  IDBLifecycleLevel,
  IDBGuid,
  IDBSecurity
{
  private int _LCStep;
  private int _ObjectTypeID = -1;
  private IDBLCSchema _Schema;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(9);
  internal long _ObjectAccessConditionID;

  public DBLifecycleStep(UserSession uSession, int aLCStepID, int objectTypeID)
    : base(uSession)
  {
    this.UseAccessCache = false;
    this._LCStep = aLCStepID;
    this._ObjectTypeID = objectTypeID;
    this.paramsTable.Create(this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Rows.Find((object) aLCStepID));
    if (this.paramsTable.RowsCount == 0)
      throw new KernelExceptionID(sc_13210.ssp_appserver_13211(2046509560), (object) aLCStepID);
    this.InitSecurityOptions(7, Convert.ToInt64(objectTypeID) << 32 /*0x20*/ | (long) aLCStepID);
  }

  static DBLifecycleStep()
  {
    DBLifecycleStep.metadataActions.Add(ActionType.GetAccess, false);
    DBLifecycleStep.metadataActions.Add(ActionType.SetAccess, false);
    DBLifecycleStep.metadataActions.Add(ActionType.Edit, true);
    DBLifecycleStep.metadataActions.Add(ActionType.View, true);
    DBLifecycleStep.metadataActions.Add(ActionType.Delete, true);
    DBLifecycleStep.metadataActions.Add(ActionType.Purge, true);
    DBLifecycleStep.metadataActions.Add(ActionType.NextLCStep, true);
    DBLifecycleStep.metadataActions.Add(ActionType.EditAuthenticalFiles, true);
    DBLifecycleStep.metadataActions.Add(ActionType.TakeOwnership, false);
    DBLifecycleStep.metadataActions.Add(ActionType.ChangeBaseVersion, false);
    DBLifecycleStep.metadataActions.Add(ActionType.ChangeAccessLevel, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBLifecycleStep.metadataActions);
  }

  public override long AddEvent(
    long objectID,
    long relationID,
    ActionType eventType,
    EventlogRecordType auditType,
    string note)
  {
    if (this.ObjectTypeID < 0)
      return base.AddEvent(objectID, relationID, eventType, auditType, note);
    if (eventType != ActionType.SetAccess && eventType != ActionType.GetAccess)
      return base.AddEvent(objectID, relationID, eventType, auditType, note);
    if (this.LoggingOn && this.UserSession.LoggingOn)
      this._LastEventID = this.EventHelper.AddEvent(objectID, relationID, 4, (long) this.ObjectTypeID, this.ObjectName, note, eventType, auditType, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
    else
      this._LastEventID = 0L;
    return this._LastEventID;
  }

  public override bool CheckAccess(
    ActionType anAction,
    bool aDefaultAccess,
    CheckAccessFlags flags)
  {
    if (this._ObjectTypeID == -1)
      throw new KernelExceptionID(sc_13210.ssp_appserver_13212(1079666260), (object) this.ObjectName);
    return base.CheckAccess(anAction, aDefaultAccess, flags);
  }

  public override bool IsUserOwner()
  {
    return this.UserSession.DBSecurity.GetGroupsArrayList().IndexOf(this.AccessOwnerID) >= 0;
  }

  public override string ObjectName
  {
    get
    {
      return this._ObjectTypeID > 0 ? string.Format(LocalizationHolder.rm.GetString("LCStepObjectName"), (object) this.LCName, (object) this.Schema.Name, (object) this.UserSession.GetObjectType(this._ObjectTypeID, true).ObjectTypeName) : string.Format(LocalizationHolder.rm.GetString("Kernel_772"), (object) this.LCName, (object) this.Schema.Name);
    }
  }

  private long CheckEditMode(string note)
  {
    if (note == "DEL")
    {
      note = string.Format(LocalizationHolder.rm.GetString("Kernel_773"), (object) this.LCName);
    }
    else
    {
      if (this.IsDeleted)
        throw new KernelExceptionID(sc_13210.ssp_appserver_13213(2007249674));
      note = string.Format(LocalizationHolder.rm.GetString("Kernel_774"), (object) this.LCName, (object) note);
    }
    long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, note);
    (this.Schema as DBLCSchema).CheckAccess(ActionType.EditProperties);
    this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
    return EventID;
  }

  public IDBLCSchema Schema
  {
    get
    {
      if (this._Schema == null)
        this._Schema = this.UserSession.GetLCSchema(this.SchemaID);
      return this._Schema;
    }
  }

  public int LCStep => this._LCStep;

  public string LCName
  {
    get => this.paramsTable[135].ToString();
    set
    {
      if (!(this.LCName != value))
        return;
      long EventID = this.CheckEditMode(value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_775") + value : LocalizationHolder.rm.GetString("Kernel_776"));
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_777"));
        if (this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select($"F_LC_NAME = {SqlHelper.QString(value)} AND F_SCHEMA_ID = {this.SchemaID.ToString()} AND F_DELETED = 0").Length != 0)
          throw new KernelExceptionID(sc_13210.ssp_appserver_13214(327412293), (object) value);
        this.UserSession.DataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13215() + SqlHelper.QString(value) + sc_13210.ssp_appserver_13216() + this.LCStep.ToString());
        this.UserSession.DBCache.ChangeTableValue("F_LC_STEP = " + this.LCStep.ToString(), "IMS_LC_STEPS", "F_LC_NAME", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[135] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13210.ssp_appserver_13217()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string Note
  {
    get
    {
      object obj = this.paramsTable[92];
      return obj == DBNull.Value ? "" : obj.ToString();
    }
    set
    {
      if (!(this.Note != value))
        return;
      long EventID = this.CheckEditMode(value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_779") + value : LocalizationHolder.rm.GetString("Kernel_780"));
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13218() + SqlHelper.QString(value) + sc_13210.ssp_appserver_13219() + this.LCStep.ToString());
        this.UserSession.DBCache.ChangeTableValue(sc_13210.ssp_appserver_13220() + this.LCStep.ToString(), "IMS_LC_STEPS", sc_13210.ssp_appserver_13221(), (object) value, (IUserSession) this.UserSession);
        this.paramsTable[92] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13210.ssp_appserver_13222()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public int SchemaID => Convert.ToInt32(this.paramsTable[25]);

  public int ObjectTypeID => this._ObjectTypeID;

  public LCAccessTypes AccessType
  {
    get => (LCAccessTypes) Convert.ToInt32(this.paramsTable[134]);
    set
    {
      if (this.AccessType == value)
        return;
      long EventID = this.CheckEditMode(LocalizationHolder.rm.GetString("Kernel_782") + EnumTypeHelper.GetCaption((Enum) value));
      try
      {
        IDbManager dataManager = this.UserSession.DataManager;
        string str1 = sc_13210.ssp_appserver_13223();
        string str2 = Convert.ToInt32((object) value).ToString();
        string str3 = sc_13210.ssp_appserver_13224();
        int lcStep = this.LCStep;
        string str4 = lcStep.ToString();
        string commandText = str1 + str2 + str3 + str4;
        dataManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        string str5 = sc_13210.ssp_appserver_13225();
        lcStep = this.LCStep;
        string str6 = lcStep.ToString();
        string filterStr = str5 + str6;
        string fieldName = sc_13210.ssp_appserver_13226();
        __Boxed<int> int32 = (System.ValueType) Convert.ToInt32((object) value);
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_LC_STEPS", fieldName, (object) int32, (IUserSession) userSession);
        this.paramsTable[134] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13210.ssp_appserver_13227()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public LCStepOptions Options
  {
    get => (LCStepOptions) Convert.ToInt32(this.paramsTable[36]);
    set
    {
      if (this.Options == value)
        return;
      long EventID = this.CheckEditMode($"{LocalizationHolder.rm.GetString("LCStepOptionsEdit")} {LCStepOptionsHelper.GetCaptions(value)}");
      try
      {
        if ((value & LCStepOptions.BaseVersion) == LCStepOptions.BaseVersion && (value & LCStepOptions.DisableParallelVersions) == LCStepOptions.None)
          throw new KernelExceptionID(sc_13210.ssp_appserver_13229(1105349924));
        this.UserSession.DataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13230() + Convert.ToInt32((object) value).ToString() + sc_13210.ssp_appserver_13231() + this.LCStep.ToString());
        this.UserSession.DBCache.ChangeTableValue(sc_13210.ssp_appserver_13232() + this.LCStep.ToString(), "IMS_LC_STEPS", sc_13210.ssp_appserver_13233(), (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[36] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        string str = $"{LocalizationHolder.rm.GetString(sc_13210.ssp_appserver_13234())} {ex.Message}";
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public bool IsDeleted => this.Deleted || Convert.ToInt32(this.paramsTable[56]) != 0;

  public bool IsFirstStep
  {
    get => Convert.ToInt32(this.paramsTable[45]) != 0;
    set
    {
      if (!value || this.IsFirstStep == value)
        return;
      long EventID = this.CheckEditMode(LocalizationHolder.rm.GetString("Kernel_784"));
      this.UserSession.StartTransaction();
      try
      {
        string filterStr = $"F_LC_STEP <> {this.LCStep} AND F_SCHEMA_ID = {this.SchemaID}";
        this.UserSession.DataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13239() + sc_13210.ssp_appserver_13240() + this.LCStep.ToString());
        this.UserSession.DataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13241() + filterStr);
        this.UserSession.DBCache.ChangeTableValue(sc_13210.ssp_appserver_13242() + this.LCStep.ToString(), "IMS_LC_STEPS", sc_13210.ssp_appserver_13243(), (object) 1, (IUserSession) this.UserSession);
        this.UserSession.DBCache.ChangeTableValue(filterStr, "IMS_LC_STEPS", sc_13210.ssp_appserver_13244(), (object) 0, (IUserSession) this.UserSession);
        this.paramsTable[45] = (object) 1;
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString(sc_13210.ssp_appserver_13245()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public int Delete(long DeleteMode)
  {
    long EventID = this.CheckEditMode("DEL");
    this.UserSession.StartTransaction();
    try
    {
      if (this.IsFirstStep && (DeleteMode & (long) Consts.PurgeMode) == 0L)
        throw new KernelExceptionID(sc_13210.ssp_appserver_13246(1881408218));
      IDbManager dataManager = this.UserSession.DataManager;
      DataTable dataTable = dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_LC_STEP = {this.LCStep}");
      if (dataTable.Rows.Count > 0)
      {
        long[] objectsID = new long[dataTable.Rows.Count];
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          objectsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
        throw new ObjectsFoundException(string.Format(sc_13210.ssp_appserver_13247(), (object) this.LCName, (object) dataTable.Rows.Count), "Объекты на данном шаге ЖЦ:", objectsID);
      }
      DataTable table = this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES");
      int index1 = this.SchemaID;
      string filterExpression = "F_SCHEMA_ID = " + index1.ToString();
      DataRow[] dataRowArray1 = table.Select(filterExpression);
      int int32 = Convert.ToInt32(dataManager.ExecuteScalar($"SELECT COUNT(*) FROM IMS_LCSTART_DATE WHERE F_LC_STEP = {this.LCStep}"));
      if (int32 == 0)
      {
        dataManager.ExecuteNonQuery("DELETE FROM IMS_LC_LINKS WHERE F_FROM_STEP = :stepID1 OR F_TO_STEP = :stepID2", dataManager.Parameter(sc_13210.ssp_appserver_13248(), (object) this.LCStep), dataManager.Parameter("stepID2", (object) this.LCStep));
        this.UserSession.DBCache.DeleteRecords("IMS_LC_LINKS", string.Format("F_FROM_STEP = {0} OR F_TO_STEP = {0}", (object) this.LCStep), (IUserSession) this.UserSession);
        dataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13249(), dataManager.Parameter("stepID", (object) this.LCStep));
        ICacheDataset dbCache = this.UserSession.DBCache;
        index1 = this.LCStep;
        string condition = "F_LC_STEP = " + index1.ToString();
        UserSession userSession = this.UserSession;
        dbCache.DeleteRecords("IMS_LC_STEPS", condition, (IUserSession) userSession);
      }
      else
      {
        if ((DeleteMode & (long) Consts.DeleteChildren) == (long) Consts.DeleteChildren)
          throw new KernelExceptionID(sc_13210.ssp_appserver_13250(127950147), (object) this.LCName, (object) int32.ToString());
        dataManager.ExecuteNonQuery(string.Format(sc_13210.ssp_appserver_13251(), (object) this.LCStep));
        this.UserSession.DBCache.DeleteRecords("IMS_LC_LINKS", string.Format("F_FROM_STEP = {0} OR F_TO_STEP = {0}", (object) this.LCStep), (IUserSession) this.UserSession);
        IDbManager dbManager = dataManager;
        string str1 = sc_13210.ssp_appserver_13252();
        index1 = this.LCStep;
        string str2 = index1.ToString();
        string commandText = str1 + str2;
        dbManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        string str3 = sc_13210.ssp_appserver_13253();
        index1 = this.LCStep;
        string str4 = index1.ToString();
        string filterStr = str3 + str4;
        // ISSUE: variable of a boxed type
        __Boxed<int> newValue = (System.ValueType) 1;
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_LC_STEPS", "F_DELETED", (object) newValue, (IUserSession) userSession);
        this.paramsTable[56] = (object) 1;
      }
      IContainerService service = ServerServices.GetService(typeof (IContainerService)) as IContainerService;
      service.DeleteContainerForLCStep((object) this.UserSession, this.GUID);
      DataRow[] dataRowArray2 = dataRowArray1;
      for (index1 = 0; index1 < dataRowArray2.Length; ++index1)
      {
        DataRow dataRow = dataRowArray2[index1];
        service.DeleteContainerForLCStepObjectType((object) this.UserSession, this.GUID, new Guid(dataRow["F_GUID"].ToString()));
      }
      this.Deleted = true;
      this.UserSession.Commit();
      index1 = 0;
      return index1;
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
  }

  private DataRow[] GetNextStepRows()
  {
    return this.UserSession.DBCache.GetTable("IMS_LC_LINKS").Select("F_FROM_STEP = " + this.LCStep.ToString());
  }

  public int[] GetNextSteps()
  {
    DataRow[] nextStepRows = this.GetNextStepRows();
    int[] nextSteps = new int[nextStepRows.Length];
    for (int index = 0; index < nextSteps.Length; ++index)
      nextSteps[index] = Convert.ToInt32(nextStepRows[index]["F_TO_STEP"]);
    return nextSteps;
  }

  public int GetNextStep(int levelID)
  {
    int nextStep = -1;
    DataRow[] nextStepRows = this.GetNextStepRows();
    if (nextStepRows.Length != 0)
    {
      foreach (DataRow dataRow in nextStepRows)
      {
        IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(Convert.ToInt32(dataRow["F_TO_STEP"]));
        if (lcStep.LevelID == levelID)
        {
          nextStep = lcStep.LCStepID;
          break;
        }
      }
    }
    return nextStep;
  }

  public int GetDeleteStepID()
  {
    int[] nextSteps = this.GetNextSteps();
    if (nextSteps.Length != 0)
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append("(");
        for (int index = 0; index < nextSteps.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(" OR ");
          stringBuilder.AppendFormat("F_LC_STEP = {0}", (object) nextSteps[index]);
        }
        stringBuilder.AppendFormat(") AND (F_LEVEL_ID = {0})", (object) this.UserSession.IdentHelper.DeletedID);
        DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select(stringBuilder.ToString());
        if (dataRowArray.Length != 0)
          return Convert.ToInt32(dataRowArray[0]["F_LC_STEP"]);
      }
    }
    DataRow[] dataRowArray1 = this.UserSession.DBCache.GetTable("IMS_LC_STEPS").Select($"F_SCHEMA_ID = {this.SchemaID.ToString()} AND F_DELETED = 0 AND F_LEVEL_ID = {this.UserSession.IdentHelper.DeletedID.ToString()}");
    return dataRowArray1.Length != 0 ? Convert.ToInt32(dataRowArray1[0]["F_LC_STEP"]) : -1;
  }

  public ObjectModifyModes ObjectModifyMode
  {
    get => (ObjectModifyModes) Convert.ToInt32(this.paramsTable[49]);
    set
    {
      if (this.ObjectModifyMode == value)
        return;
      long EventID = this.CheckEditMode(LocalizationHolder.rm.GetString("Kernel_786") + EnumTypeHelper.GetCaption((Enum) value));
      try
      {
        if (this.IsFirstStep && (value == ObjectModifyModes.CreateVersion || value == ObjectModifyModes.CantModify))
          throw new KernelExceptionID(sc_13210.ssp_appserver_13254(326692253));
        this.UserSession.DataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13255() + Convert.ToInt32((object) value).ToString() + sc_13210.ssp_appserver_13256() + this.LCStep.ToString());
        this.UserSession.DBCache.ChangeTableValue(sc_13210.ssp_appserver_13257() + this.LCStep.ToString(), "IMS_LC_STEPS", sc_13210.ssp_appserver_13258(), (object) Convert.ToInt32((object) value), (IUserSession) this.UserSession);
        this.paramsTable[49] = (object) Convert.ToInt32((object) value);
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13210.ssp_appserver_13259()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public DBLifecycleStepProperties Properties
  {
    get
    {
      return new DBLifecycleStepProperties(this.LCStep, this.ObjectTypeID, this.LCName, this.Note, this.AccessType, this.LevelID, this.ObjectModifyMode, this.GUID, this.IsFirstStep, this.Options);
    }
    set
    {
      this.UserSession.StartTransaction();
      try
      {
        if (this.LCStep != value.LCStep)
          throw new KernelExceptionID(sc_13210.ssp_appserver_13260(1893586762));
        if (value.FirstStep && (value.ObjectModifyMode == ObjectModifyModes.CreateVersion || value.ObjectModifyMode == ObjectModifyModes.CantModify))
          throw new KernelExceptionID(sc_13210.ssp_appserver_13261(418810288));
        this.LCName = value.LCName;
        this.Note = value.Note;
        this.AccessType = value.AccessType;
        this.LevelID = value.LevelID;
        this.ObjectModifyMode = value.ObjectModifyMode;
        this.GUID = value.StepGuid;
        this.IsFirstStep = value.FirstStep;
        this.Options = value.Options;
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
  }

  public int AutoTransferStepID
  {
    get
    {
      int autoTransferStepId = 0;
      DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_LC_LINKS").Select("F_FROM_STEP = " + (object) this.LCStep);
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        if ((Convert.ToInt32(dataRowArray[index]["F_PARAMS"]) & 1) == 1)
        {
          autoTransferStepId = Convert.ToInt32(dataRowArray[index]["F_TO_STEP"]);
          break;
        }
      }
      return autoTransferStepId;
    }
  }

  public int LevelID
  {
    get => Convert.ToInt32(this.paramsTable[72]);
    set
    {
      if (this.LevelID == value)
        return;
      IDBLifecycleLevelType lifecycleLevel = this.UserSession.GetLifecycleLevel(value);
      long EventID = this.CheckEditMode(LocalizationHolder.rm.GetString("Kernel_788") + lifecycleLevel.LevelName);
      this.UserSession.StartTransaction();
      try
      {
        DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable($"SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_LC_STEP = {this.LCStep}");
        if (dataTable.Rows.Count > 0)
        {
          long[] objectsID = new long[dataTable.Rows.Count];
          for (int index = 0; index < dataTable.Rows.Count; ++index)
            objectsID[index] = Convert.ToInt64(dataTable.Rows[index][0]);
          throw new ObjectsFoundException(string.Format(sc_13210.ssp_appserver_13262(), (object) this.LCName, (object) dataTable.Rows.Count), "Объекты на данном шаге ЖЦ:", objectsID);
        }
        this.UserSession.DataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13264() + value.ToString() + sc_13210.ssp_appserver_13265() + this.LCStep.ToString());
        this.UserSession.DBCache.ChangeTableValue(sc_13210.ssp_appserver_13266() + this.LCStep.ToString(), "IMS_LC_STEPS", sc_13210.ssp_appserver_13267(), (object) value, (IUserSession) this.UserSession);
        this.paramsTable[72] = (object) value;
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        string str = LocalizationHolder.rm.GetString("Kernel_789") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  internal void DeleteAccess()
  {
    if (this.ObjectTypeID <= 0)
      return;
    this.PurgeAccess();
  }

  public string LevelName => this.UserSession.GetLifecycleLevel(this.LevelID).LevelName;

  public string Litera => this.UserSession.GetLifecycleLevel(this.LevelID).Litera;

  public byte[] LevelIcon => this.UserSession.GetLifecycleLevel(this.LevelID).LevelIcon;

  public void SetGUID(Guid guid) => this.GUID = guid;

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  private void SaveNewGuid(string newGuid)
  {
    this.UserSession.DataManager.ExecuteNonQuery(sc_13210.ssp_appserver_13268() + SqlHelper.QString(newGuid) + sc_13210.ssp_appserver_13269() + this.LCStep.ToString());
    this.UserSession.DBCache.ChangeTableValue(sc_13210.ssp_appserver_13270() + this.LCStep.ToString(), "IMS_LC_STEPS", sc_13210.ssp_appserver_13271(), (object) newGuid, (IUserSession) this.UserSession);
    this.paramsTable[76] = (object) newGuid;
  }

  public Guid GUID
  {
    get
    {
      string g = this.paramsTable[76].ToString();
      Guid guid;
      if (g == string.Empty)
      {
        guid = Guid.NewGuid();
        this.SaveNewGuid(guid.ToString());
      }
      else
        guid = new Guid(g);
      return guid;
    }
    set
    {
      if (!(value != this.GUID))
        return;
      long EventID = this.CheckEditMode(LocalizationHolder.rm.GetString("Kernel_790") + value.ToString());
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_13210.ssp_appserver_13272(1666100182));
        this.SaveNewGuid(value.ToString());
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_791") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public IDBSecurity GetAttributeSecurity(int attrID)
  {
    if (this.ObjectTypeID < 0)
      throw new KernelException(sc_13210.ssp_appserver_13273());
    return (IDBSecurity) new DBAttributeLCSecurity(this.UserSession, attrID, this.LCStep, this.ObjectTypeID);
  }

  public override bool EnabledConditionAccess
  {
    get
    {
      return this.ObjectTypeID < 0 || MetaDataHelper.GetAttribute4ObjectType(this._ObjectTypeID, this.UserSession.IdentHelper.AttributeAccessCondition) != null;
    }
  }

  protected override long AccessConditionID => this._ObjectAccessConditionID;
}
