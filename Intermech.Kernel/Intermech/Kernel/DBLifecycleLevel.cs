// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBLifecycleLevel
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

public class DBLifecycleLevel : 
  DBSessionable,
  IDBLifecycleLevelType,
  IDeletable,
  IDBSubjectArea,
  IDBGuid,
  IDBSecurity
{
  private int _LevelID;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(4);

  public DBLifecycleLevel(UserSession uSession, int aLevelID)
    : base(uSession)
  {
    this._LevelID = aLevelID;
    this.paramsTable.Create(this.UserSession.DBCache.GetTable("IMS_LEVELS").Rows.Find((object) aLevelID));
    if (this.paramsTable.RowsCount == 0)
      throw new KernelExceptionID(sc_13171.ssp_appserver_13172(1036482881), (object) aLevelID);
    this.InitSecurityOptions(8, (long) aLevelID);
  }

  static DBLifecycleLevel()
  {
    DBLifecycleLevel.metadataActions.Add(ActionType.GetAccess, false);
    DBLifecycleLevel.metadataActions.Add(ActionType.SetAccess, false);
    DBLifecycleLevel.metadataActions.Add(ActionType.Delete, false);
    DBLifecycleLevel.metadataActions.Add(ActionType.EditProperties, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBLifecycleLevel.metadataActions);
  }

  public override string ObjectName
  {
    get => string.Format(LocalizationHolder.rm.GetString("Kernel_739"), (object) this.LevelName);
  }

  public int LevelID
  {
    get => this._LevelID;
    set => throw new OperationNotApplicableException();
  }

  public string LevelName
  {
    get => this.paramsTable[131].ToString();
    set
    {
      if (!(this.LevelName != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_740") + value : LocalizationHolder.rm.GetString("Kernel_741"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_LEVEL_NAME");
      try
      {
        SqlHelper.ValidateEmptyValue(value, LocalizationHolder.rm.GetString("Kernel_742"));
        SqlHelper.ValidateFieldLength(LocalizationHolder.rm.GetString("MDName"), value.Length, Consts.MaxObjectNameLength);
        IDbManager dataManager = this.UserSession.DataManager;
        string[] strArray = new string[6]
        {
          sc_13171.ssp_appserver_13173(),
          "F_LEVEL_NAME = ",
          SqlHelper.QString(value),
          sc_13171.ssp_appserver_13174(),
          "F_LEVEL_ID = ",
          null
        };
        int levelId = this.LevelID;
        strArray[5] = levelId.ToString();
        string commandText = string.Concat(strArray);
        dataManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        string str1 = sc_13171.ssp_appserver_13175();
        levelId = this.LevelID;
        string str2 = levelId.ToString();
        string filterStr = $"F_LEVEL_ID{str1}{str2}";
        string newValue = value;
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_LEVELS", "F_LEVEL_NAME", (object) newValue, (IUserSession) userSession);
        this.paramsTable[131] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13176());
        if (ex.Message.IndexOf("IMS_LEVELS_LEVEL_NAME") >= 0)
        {
          string message = string.Format(LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13177()), (object) value);
          this.CloseEvent(EventID, EventlogRecordType.Error, str + message);
          throw new AlreadyExistsException(message);
        }
        this.CloseEvent(EventID, EventlogRecordType.Error, str + ex.Message);
        throw;
      }
    }
  }

  private void CheckChangeDefault()
  {
    DataTable dataTable = this.UserSession.GetLifecycleLevelCollection().Select(string.Empty);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      int int32 = Convert.ToInt32(dataTable.Rows[index]["F_LEVEL_ID"]);
      if (int32 != this.LevelID && !this.UserSession.CanChangeObjectElement(8, (object) int32, ObligatoryElementKeys.GetKeyForObjectProperty("F_DEFAULT")))
      {
        IDBLifecycleLevelType lifecycleLevel = this.UserSession.GetLifecycleLevel(int32);
        if (lifecycleLevel.IsDefaultLevel)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1142"), (object) lifecycleLevel.LevelName));
      }
    }
  }

  private void CheckChangeEnable(string propertyID)
  {
    if (!this.UserSession.CanChangeObjectElement(8, (object) this.LevelID, ObligatoryElementKeys.GetKeyForObjectProperty(propertyID)))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_915"), (object) DataSetProcessor.GetCaption(propertyID)));
  }

  public string Litera
  {
    get => this.paramsTable[130].ToString();
    set
    {
      if (!(this.Litera != value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, value != string.Empty ? LocalizationHolder.rm.GetString("Kernel_745") + value : LocalizationHolder.rm.GetString("Kernel_746"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_LITERA");
      try
      {
        IDbManager dataManager = this.UserSession.DataManager;
        string[] strArray = new string[6]
        {
          sc_13171.ssp_appserver_13178(),
          "F_LITERA = ",
          SqlHelper.QString(value),
          sc_13171.ssp_appserver_13179(),
          "F_LEVEL_ID = ",
          null
        };
        int levelId = this.LevelID;
        strArray[5] = levelId.ToString();
        string commandText = string.Concat(strArray);
        dataManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        string str1 = sc_13171.ssp_appserver_13180();
        levelId = this.LevelID;
        string str2 = levelId.ToString();
        string filterStr = $"F_LEVEL_ID{str1}{str2}";
        string newValue = value;
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_LEVELS", "F_LITERA", (object) newValue, (IUserSession) userSession);
        this.paramsTable[130] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13181()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public byte[] LevelIcon
  {
    get => this.paramsTable[129] == DBNull.Value ? new byte[0] : (byte[]) this.paramsTable[129];
    set
    {
      if (SqlHelper.IsEqual(this.LevelIcon, value))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_748"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_ICON");
      try
      {
        object newValue;
        if (value == null || value.Length == 0)
        {
          this.UserSession.DataManager.ExecuteNonQuery($"{sc_13171.ssp_appserver_13182()}F_ICON = NULL WHERE F_LEVEL_ID = {this.LevelID.ToString()}");
          newValue = (object) DBNull.Value;
        }
        else
        {
          this.UserSession.DataManager.ExecuteNonQuery($"{sc_13171.ssp_appserver_13183()}F_ICON = :icon WHERE F_LEVEL_ID = {this.LevelID.ToString()}", this.UserSession.DataManager.Parameter("icon", (object) value));
          newValue = (object) value;
        }
        this.UserSession.DBCache.ChangeTableValue("F_LEVEL_ID = " + this.LevelID.ToString(), "IMS_LEVELS", "F_ICON", newValue, (IUserSession) this.UserSession);
        this.paramsTable[129] = newValue;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_749") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str);
      }
    }
  }

  public bool IsDefaultLevel
  {
    get => Convert.ToBoolean(this.paramsTable[108]);
    set
    {
      if (this.IsDefaultLevel == value)
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_750"));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_DEFAULT");
      this.UserSession.StartTransaction();
      try
      {
        string str1 = ") AND (F_AREA_ID = " + SqlHelper.QString(this.SubjectAreas);
        string str2 = !(this.SubjectAreas == "") ? str1 + ")" : str1 + " OR F_AREA_ID IS NULL)";
        if (value)
        {
          if (this.UserSession.IdentHelper.DeletedID == this.LevelID)
            throw new KernelException(LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13184()));
          this.CheckChangeDefault();
          this.UserSession.DataManager.ExecuteNonQuery($"{sc_13171.ssp_appserver_13185()}F_DEFAULT = 1 WHERE (F_LEVEL_ID = {this.LevelID.ToString()}{str2}");
          this.UserSession.DataManager.ExecuteNonQuery($"{sc_13171.ssp_appserver_13186()}F_DEFAULT = 0 WHERE (F_LEVEL_ID <> {this.LevelID.ToString()}{str2}");
        }
        else
        {
          if (this.SubjectAreas == "")
          {
            DataTable table = this.UserSession.DBCache.GetTable("IMS_LEVELS");
            bool flag = true;
            foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
            {
              if (Convert.ToInt32(row["F_LEVEL_ID"]) != this.LevelID && Convert.ToString(row["F_AREA_ID"]) == "")
              {
                flag = false;
                this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_13171.ssp_appserver_13187() + "F_DEFAULT = 1 WHERE F_LEVEL_ID = {0}", row["F_LEVEL_ID"]));
                break;
              }
            }
            if (flag)
              throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13188()), (object) this.LevelName));
          }
          this.UserSession.DataManager.ExecuteNonQuery($"{sc_13171.ssp_appserver_13189()}F_DEFAULT = 0 WHERE F_LEVEL_ID = {this.LevelID.ToString()}");
        }
        this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, this.UserSession.DataManager, "IMS_LEVELS");
        this.paramsTable[108] = (object) Convert.ToInt32(value);
        this.UserSession.Commit();
      }
      catch (Exception ex)
      {
        this.UserSession.Rollback();
        this.CloseEvent(EventID, EventlogRecordType.Error, LocalizationHolder.rm.GetString("Kernel_753") + ex.Message);
        throw;
      }
    }
  }

  private void CheckLevelTable(string tableName, string categoryName)
  {
    long int64 = Convert.ToInt64(this.UserSession.DataManager.ExecuteScalar(string.Format("SELECT COUNT(*) FROM {0} WHERE F_LEVEL_ID = " + this.LevelID.ToString(), (object) tableName)));
    if (int64 > 0L)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13190()) + categoryName, (object) this.LevelName, (object) int64));
  }

  public long StorageID
  {
    get => Convert.ToInt64(this.paramsTable[180]);
    set
    {
      if (this.StorageID == value)
        return;
      string note;
      if (value != 0L)
      {
        IDBObject dbObject = this.UserSession.GetObject(value);
        if (dbObject.ObjectType != this.UserSession.IdentHelper.StorageTypeID)
          throw new KernelExceptionID(436, (object) dbObject.NameInMessages, (object) dbObject.ObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(dbObject.ObjectID));
        note = $"{LocalizationHolder.rm.GetString("SetLevelStorage")} {dbObject.Caption}";
      }
      else
        note = LocalizationHolder.rm.GetString("ClearLevelStorage");
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, note);
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_STORAGE_ID");
      try
      {
        this.UserSession.DataManager.ExecuteNonQuery(string.Format(sc_13171.ssp_appserver_13192(), (object) value, (object) this.LevelID));
        this.UserSession.DBCache.ChangeTableValue("F_LEVEL_ID = " + this.LevelID.ToString(), "IMS_LEVELS", "F_STORAGE_ID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[180] = (object) value;
        (ServerServices.GetService(typeof (IBlobStoragesPool)) as BlobStoragesPool).InitLevels((IUserSession) this.UserSession);
      }
      catch (Exception ex)
      {
        this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
        throw;
      }
    }
  }

  public int Delete(long DeleteMode)
  {
    this.UserSession.ValidateSystemDelete((object) this, LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13193()));
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied);
    this.CheckAccess(ActionType.Delete);
    if (!this.UserSession.CanChangeObject(8, (object) this.LevelID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_920"), (object) this.LevelName));
    try
    {
      if (this.IsDefaultLevel)
        throw new KernelException(LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13194()));
      IDbManager dataManager = this.UserSession.DataManager;
      this.CheckLevelTable("IMS_ATTRIBUTES", LocalizationHolder.rm.GetString("Kernel_757"));
      this.CheckLevelTable("IMS_ATTR4OBJ_TYPES", LocalizationHolder.rm.GetString("Kernel_758"));
      this.CheckLevelTable("IMS_OBJECTS", LocalizationHolder.rm.GetString("Kernel_759"));
      this.CheckLevelTable("IMS_LC_STEPS", LocalizationHolder.rm.GetString("Kernel_760"));
      string commandText = $"{sc_13171.ssp_appserver_13195()}F_LEVEL_ID = {this.LevelID.ToString()}";
      dataManager.ExecuteNonQuery(commandText);
      this.UserSession.DBCache.DeleteRecords("IMS_LEVELS", "F_LEVEL_ID = " + this.LevelID.ToString(), (IUserSession) this.UserSession);
      (ServerServices.GetService(typeof (IContainerService)) as IContainerService).DeleteContainerForLCLevel((object) this.UserSession, this.GUID);
      (ServerServices.GetService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).UpdateLifecycleRule((object) this.UserSession, this.LevelID);
      this.Deleted = true;
    }
    catch (Exception ex)
    {
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
    return 0;
  }

  public string SubjectAreas
  {
    get => this.paramsTable[89].ToString();
    set
    {
      if (!(this.SubjectAreas != value))
        return;
      IDBSubjectAreaCollection subjectAreaCollection = this.UserSession.GetSubjectAreaCollection();
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_761") + subjectAreaCollection.GetAreasCaption(value));
      this.CheckAccess(ActionType.EditProperties);
      this.CheckChangeEnable("F_AREA_ID");
      try
      {
        subjectAreaCollection.ValidateAriasString(value);
        IDbManager dataManager = this.UserSession.DataManager;
        string[] strArray = new string[6]
        {
          sc_13171.ssp_appserver_13196(),
          "F_AREA_ID = ",
          SqlHelper.QString(value),
          sc_13171.ssp_appserver_13197(),
          "F_LEVEL_ID = ",
          null
        };
        int levelId = this.LevelID;
        strArray[5] = levelId.ToString();
        string commandText = string.Concat(strArray);
        dataManager.ExecuteNonQuery(commandText);
        ICacheDataset dbCache = this.UserSession.DBCache;
        string str1 = sc_13171.ssp_appserver_13198();
        levelId = this.LevelID;
        string str2 = levelId.ToString();
        string filterStr = $"F_LEVEL_ID{str1}{str2}";
        string newValue = value;
        UserSession userSession = this.UserSession;
        dbCache.ChangeTableValue(filterStr, "IMS_LEVELS", "F_AREA_ID", (object) newValue, (IUserSession) userSession);
        this.paramsTable[89] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString(sc_13171.ssp_appserver_13199()) + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }

  public string SubjectAreasCaption
  {
    get => this.UserSession.GetSubjectAreaCollection().GetAreasCaption(this.SubjectAreas);
  }

  public DBLifecycleStepProperties DefaultPropertiesForLCStep()
  {
    LCAccessTypes accessType = LCAccessTypes.CheckAll;
    object obj1 = this.UserSession.DataManager.ExecuteScalar(string.Format(sc_13171.ssp_appserver_13200(), (object) this.LevelID));
    if (obj1 != null && obj1 != DBNull.Value)
      accessType = (LCAccessTypes) Convert.ToInt32(obj1);
    ObjectModifyModes objectModifyMode = ObjectModifyModes.InBase;
    object obj2 = this.UserSession.DataManager.ExecuteScalar(string.Format(sc_13171.ssp_appserver_13201(), (object) this.LevelID));
    if (obj2 != null && obj2 != DBNull.Value)
      objectModifyMode = (ObjectModifyModes) Convert.ToInt32(obj2);
    return new DBLifecycleStepProperties(0, 0, this.LevelName, "", accessType, this.LevelID, objectModifyMode, Guid.NewGuid(), this.IsDefaultLevel, LCStepOptions.None);
  }

  public bool IsSystemGUID => SystemGUIDs.IsSystemGUID(this.GUID);

  public Guid GUID
  {
    get => new Guid(this.paramsTable[76].ToString());
    set
    {
      if (!(value != this.GUID))
        return;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_763") + value.ToString());
      this.CheckAccess(ActionType.EditProperties);
      if (!this.UserSession.CanChangeObject(8, (object) this.LevelID))
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_915"), (object) DataSetProcessor.GetCaption("F_GUID")));
      try
      {
        if (!this.UserSession.DeveloperMode)
          throw new KernelExceptionID(sc_13171.ssp_appserver_13202(816399738));
        this.UserSession.DataManager.ExecuteNonQuery(sc_13171.ssp_appserver_13203() + SqlHelper.QString(value.ToString()) + sc_13171.ssp_appserver_13204() + this.LevelID.ToString());
        this.UserSession.DBCache.ChangeTableValue("F_LEVEL_ID = " + this.LevelID.ToString(), "IMS_LEVELS", "F_GUID", (object) value, (IUserSession) this.UserSession);
        this.paramsTable[76] = (object) value;
      }
      catch (Exception ex)
      {
        string str = LocalizationHolder.rm.GetString("Kernel_764") + ex.Message;
        this.CloseEvent(EventID, EventlogRecordType.Error, str);
        throw new KernelException(str, ex);
      }
    }
  }
}
