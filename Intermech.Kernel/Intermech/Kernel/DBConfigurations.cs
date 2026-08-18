// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBConfigurations
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Data;


namespace Intermech.Kernel;

public class DBConfigurations : DBSessionable, IDBConfigurations
{
  internal static bool CheckObjectsVisibility;
  private DBConfigurationService _CfgService;
  internal ConcurrentDictionary<ConfigParamKey, object> _UserParamsCache;
  private long _User_Cfg_ObjectID;
  private IDBObject _UserCfgObject;
  private long _Common_Cfg_ObjectID;

  public DBConfigurations(UserSession uSession, DBConfigurationService cfgService)
    : base(uSession)
  {
    this._CfgService = cfgService;
    this.InitCacheDictionary(uSession.DataManager.ExecuteDataTable("SELECT * FROM IMS_CONFIGS WHERE F_USER_ID = :usrID", uSession.DataManager.Parameter("usrID", (object) uSession.UserID)));
  }

  public DBConfigurations(
    UserSession uSession,
    ConcurrentDictionary<ConfigParamKey, object> configCache,
    DBConfigurationService cfgService)
    : base(uSession)
  {
    this._CfgService = cfgService;
    this._UserParamsCache = configCache;
  }

  private void InitCacheDictionary(DataTable tbl)
  {
    ConcurrentDictionary<ConfigParamKey, object> concurrentDictionary = new ConcurrentDictionary<ConfigParamKey, object>();
    for (int index = 0; index < tbl.Rows.Count; ++index)
    {
      if (Convert.ToInt64(tbl.Rows[index]["F_USER_ID"]) == this.UserSession.UserID)
        concurrentDictionary.TryAdd(new ConfigParamKey(tbl.Rows[index]["F_MODULE_NAME"].ToString(), tbl.Rows[index]["F_SECTION_ID"].ToString(), tbl.Rows[index]["F_PARAM_NAME"].ToString()), tbl.Rows[index]["F_VALUE"]);
    }
    this._UserParamsCache = concurrentDictionary;
  }

  public string ReadStringNoCache(
    string aModuleName,
    string aSectionID,
    string aParamName,
    bool isGlobalParam)
  {
    long userId = !isGlobalParam ? this.UserSession.UserID : 0L;
    IDbManager dataManager = this.UserSession.DataManager;
    object obj = dataManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :userID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dataManager.Parameter("moduleName", (object) aModuleName), dataManager.Parameter("userID", (object) userId), dataManager.Parameter("sectID", (object) aSectionID), dataManager.Parameter("parName", (object) aParamName));
    return obj != null ? obj.ToString() : string.Empty;
  }

  protected virtual object ReadDBParam(
    string aModuleName,
    string aSectionID,
    string aParamName,
    long aUserID)
  {
    if (aUserID == 0L)
      return this._CfgService.GetValue(aModuleName, aSectionID, aParamName);
    if (aUserID != this.UserSession.UserID)
      throw new KernelException($"Попытка чтения настроек пользователя N{aUserID} из сессии пользователя N{this.UserSession.UserID}.");
    object obj;
    return this._UserParamsCache.TryGetValue(new ConfigParamKey(aModuleName, aSectionID, aParamName), out obj) ? obj : (object) null;
  }

  private object ReadUserDBParam(
    string aModuleName,
    string aSectionID,
    string aParamName,
    DBConfigMode configMode)
  {
    long aUserID = 0;
    if (configMode == DBConfigMode.UserOnly || configMode == DBConfigMode.UserAndGlobal)
      aUserID = this.UserSession.UserID;
    object obj = this.ReadDBParam(aModuleName, aSectionID, aParamName, aUserID);
    if (obj == null)
    {
      if (configMode == DBConfigMode.GlobalAndUser || configMode == DBConfigMode.GlobalOnly)
        obj = this.ReadDBParam(aModuleName, aSectionID, aParamName, 0L);
      if (configMode == DBConfigMode.UserAndGlobal)
        obj = this.ReadDBParam(aModuleName, aSectionID, aParamName, this.UserSession.UserID);
    }
    return obj;
  }

  public bool ParameterPresent(
    string ModuleName,
    string SectionID,
    string ParamName,
    DBConfigMode configMode)
  {
    long aUserID = 0;
    if (configMode == DBConfigMode.UserOnly || configMode == DBConfigMode.UserAndGlobal)
      aUserID = this.UserSession.UserID;
    return this.ReadDBParam(ModuleName, SectionID, ParamName, aUserID) != null;
  }

  public virtual string ReadString(
    string ModuleName,
    string SectionID,
    string ParamName,
    string DefaultValue,
    DBConfigMode configMode)
  {
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? obj.ToString() : DefaultValue;
  }

  public virtual long ReadInteger(
    string ModuleName,
    string SectionID,
    string ParamName,
    long DefaultValue,
    DBConfigMode configMode)
  {
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? Convert.ToInt64(obj) : DefaultValue;
  }

  public virtual double ReadDouble(
    string ModuleName,
    string SectionID,
    string ParamName,
    double DefaultValue,
    DBConfigMode configMode)
  {
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? Convert.ToDouble(obj) : DefaultValue;
  }

  public virtual bool ReadBool(
    string ModuleName,
    string SectionID,
    string ParamName,
    bool DefaultValue,
    DBConfigMode configMode)
  {
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? Convert.ToBoolean(obj) : DefaultValue;
  }

  public virtual DateTime ReadDateTime(
    string ModuleName,
    string SectionID,
    string ParamName,
    DateTime DefaultValue,
    DBConfigMode configMode)
  {
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? DateTime.FromBinary(Convert.ToInt64(obj)) + this.UserSession.TimeZoneOffset : DefaultValue;
  }

  private void ValidateWriteParams(long aUserID)
  {
    if (aUserID != this.UserSession.UserID && !this.UserSession.IsAdmin)
      throw new KernelExceptionID(sc_12264.ssp_appserver_12265(1779822999));
  }

  private void ValidateReadParams(long aUserID)
  {
    if (aUserID != 0L && aUserID != this.UserSession.UserID && !this.UserSession.IsAdmin)
      throw new KernelExceptionID(sc_12264.ssp_appserver_12266(2117777789));
  }

  private int WriteUserDBParam(
    string aModuleName,
    string aSectionID,
    string aParamName,
    object aValue,
    long aUserID)
  {
    this.ValidateWriteParams(aUserID);
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("moduleName", (object) aModuleName);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("userParID", (object) aUserID);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("sectID", (object) aSectionID);
    IDbDataParameter dbDataParameter4 = dataManager.Parameter("parName", (object) aParamName);
    object obj = dataManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :userParID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4);
    bool flag = false;
    if (obj == null)
    {
      dataManager.ExecuteNonQuery("INSERT INTO IMS_CONFIGS (F_MODULE_NAME, F_USER_ID, F_SECTION_ID, F_PARAM_NAME, F_VALUE) VALUES (:moduleName, :userParID, :sectID, :parName, :value)", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dataManager.Parameter("value", (object) aValue.ToString()));
      flag = true;
    }
    else if (!obj.Equals((object) aValue.ToString()))
    {
      dataManager.ExecuteNonQuery("UPDATE IMS_CONFIGS SET F_VALUE = :value WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :userParID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dataManager.Parameter("value", (object) aValue.ToString()));
      flag = true;
    }
    if (aUserID == 0L)
    {
      if (flag)
        this._CfgService.SetValue(aModuleName, aSectionID, aParamName, aValue, dataManager);
    }
    else if (aUserID == this.UserSession.UserID)
    {
      ConfigParamKey key = new ConfigParamKey(aModuleName, aSectionID, aParamName);
      this._UserParamsCache.TryRemove(key, out object _);
      this._UserParamsCache.TryAdd(key, aValue);
    }
    return 0;
  }

  private int WriteUserDBParam(
    string aModuleName,
    string aSectionID,
    string aParamName,
    object aValue)
  {
    return this.WriteUserDBParam(aModuleName, aSectionID, aParamName, aValue, this.UserSession.UserID);
  }

  public virtual int WriteString(
    string ModuleName,
    string SectionID,
    string ParamName,
    string Value)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value);
  }

  public virtual int WriteString(
    string ModuleName,
    string SectionID,
    string ParamName,
    string Value,
    long aUserID)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, aUserID);
  }

  public virtual bool WriteStringNoCache(
    string aModuleName,
    string aSectionID,
    string aParamName,
    string aValue,
    string oldValue,
    long aUserID)
  {
    this.ValidateWriteParams(aUserID);
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter("moduleName", (object) aModuleName);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("userParID", (object) aUserID);
    IDbDataParameter dbDataParameter3 = dataManager.Parameter("sectID", (object) aSectionID);
    IDbDataParameter dbDataParameter4 = dataManager.Parameter("parName", (object) aParamName);
    bool flag;
    if (dataManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :userParID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4) == null)
    {
      dataManager.ExecuteNonQuery("INSERT INTO IMS_CONFIGS (F_MODULE_NAME, F_USER_ID, F_SECTION_ID, F_PARAM_NAME, F_VALUE) VALUES (:moduleName, :userParID, :sectID, :parName, :value)", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dataManager.Parameter("value", (object) aValue));
      flag = true;
    }
    else
      flag = dataManager.ExecuteNonQuery("UPDATE IMS_CONFIGS SET F_VALUE = :value WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :userParID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName AND F_VALUE = :oldValue", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dataManager.Parameter("value", (object) aValue), dataManager.Parameter(nameof (oldValue), (object) oldValue)) != 0;
    return flag;
  }

  public virtual int WriteInteger(
    string ModuleName,
    string SectionID,
    string ParamName,
    long Value)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value);
  }

  public virtual int WriteInteger(
    string ModuleName,
    string SectionID,
    string ParamName,
    long Value,
    long aUserID)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, aUserID);
  }

  public virtual int WriteDouble(
    string ModuleName,
    string SectionID,
    string ParamName,
    double Value)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value);
  }

  public virtual int WriteDouble(
    string ModuleName,
    string SectionID,
    string ParamName,
    double Value,
    long aUserID)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, aUserID);
  }

  public virtual int WriteBool(string ModuleName, string SectionID, string ParamName, bool Value)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value);
  }

  public virtual int WriteBool(
    string ModuleName,
    string SectionID,
    string ParamName,
    bool Value,
    long aUserID)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, aUserID);
  }

  public virtual int WriteDateTime(
    string ModuleName,
    string SectionID,
    string ParamName,
    DateTime Value)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) (Value - this.UserSession.TimeZoneOffset).ToBinary());
  }

  public virtual int WriteDateTime(
    string ModuleName,
    string SectionID,
    string ParamName,
    DateTime Value,
    long aUserID)
  {
    return this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) (Value - this.UserSession.TimeZoneOffset).ToBinary(), aUserID);
  }

  private IDBAttribute GetConfigAttribute(string data_name, long userID)
  {
    if (userID == 0L)
      userID = this.UserSession.IdentHelper.SystemID;
    if (data_name == string.Empty || data_name == null)
      throw new KernelExceptionID(sc_12264.ssp_appserver_12267(1320963799));
    IDBObject dbObject = (IDBObject) null;
    if (userID == 0L)
    {
      if (this._Common_Cfg_ObjectID != 0L)
        dbObject = this.UserSession.GetObject(this._Common_Cfg_ObjectID, false);
    }
    else if (userID == this.UserSession.UserID && this._User_Cfg_ObjectID != 0L)
    {
      if (this._UserCfgObject == null)
      {
        dbObject = this.UserSession.GetObject(this._User_Cfg_ObjectID, false);
        this._UserCfgObject = dbObject;
      }
      else
        dbObject = this._UserCfgObject;
    }
    if (dbObject == null)
    {
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      paramSet.Conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-8, RelationalOperators.Equal, (object) userID, LogicalOperators.NONE, 0, false)
      };
      IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(this.UserSession.IdentHelper.ConfigDataTypeID);
      if (userID != this.UserSession.UserID)
        (objectCollection as DBObjectCollection)._ShowPersonalObjects = true;
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable.Rows.Count == 0)
      {
        dbObject = objectCollection.Create();
        dbObject.CommitCreation(true);
      }
      else
        dbObject = this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
      if (userID == 0L)
        this._Common_Cfg_ObjectID = Math.Abs(dbObject.ObjectID);
      else if (userID == this.UserSession.UserID)
        this._User_Cfg_ObjectID = Math.Abs(dbObject.ObjectID);
    }
    IDBAttribute configAttribute = dbObject.GetAttributeByID(this.UserSession.IdentHelper.ConfigFileAttributeID);
    if (configAttribute == null)
      configAttribute = dbObject.Attributes.AddAttribute(this.UserSession.IdentHelper.ConfigFileAttributeID, false);
    else
      configAttribute.Index = 0;
    while (configAttribute.AsString.ToUpper() != data_name.ToUpper())
    {
      if (configAttribute.Index < configAttribute.ValuesCount - 1)
      {
        ++configAttribute.Index;
      }
      else
      {
        (dbObject as DBObject).ClearAttributesCache();
        configAttribute = dbObject.GetAttributeByID(this.UserSession.IdentHelper.ConfigFileAttributeID);
        for (int index = 0; index < configAttribute.ValuesCount; ++index)
        {
          configAttribute.Index = index;
          if (configAttribute.AsString.ToUpper() == data_name.ToUpper())
            return configAttribute;
        }
        if (!configAttribute.IsNull)
          configAttribute.AddValue((object) null);
        BlobInformation blobInfo = new BlobInformation(0L, 0L, DateTime.UtcNow + this.UserSession.TimeZoneOffset, data_name, ArcMethods.NotPacked, "");
        (configAttribute as IBlobWriter).OpenBlob(blobInfo, false);
        break;
      }
    }
    return configAttribute;
  }

  public IDBAttribute GetConfigAttribute(string data_name)
  {
    return this.GetConfigAttribute(data_name, this.UserSession.UserID);
  }

  public virtual void WriteConfigData(BlobInformation config_info, byte[] config_file, long userID)
  {
    this.ValidateWriteParams(userID);
    this.UserSession.StartTransaction();
    try
    {
      IBlobWriter configAttribute = this.GetConfigAttribute(config_info.FileName, userID) as IBlobWriter;
      if (configAttribute is DBFileAttribute)
        (configAttribute as DBFileAttribute)._ValidateUniqueFileName = false;
      configAttribute.OpenBlob(config_info, false);
      configAttribute.WriteDataBlock(config_file);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public virtual void WriteConfigData(BlobInformation config_info, byte[] config_file)
  {
    this.WriteConfigData(config_info, config_file, this.UserSession.UserID);
  }

  public virtual void LoadConfigData(
    string config_name,
    out BlobInformation config_info,
    out byte[] config_file,
    long userID)
  {
    this.ValidateReadParams(userID);
    IBlobReader configAttribute = this.GetConfigAttribute(config_name, userID) as IBlobReader;
    config_info = configAttribute.OpenBlob(0);
    try
    {
      if (config_info.PackedFileSize == 0L)
        config_file = new byte[0];
      else
        config_file = configAttribute.ReadDataBlock();
    }
    finally
    {
      if (configAttribute.BlobState != BlobAttributeStates.Closed)
        configAttribute.CloseBlob();
    }
  }

  public virtual void LoadConfigData(
    string config_name,
    out BlobInformation config_info,
    out byte[] config_file)
  {
    this.LoadConfigData(config_name, out config_info, out config_file, this.UserSession.UserID);
  }

  public DataTable ReadSection(string ModuleName, string SectionID, long userID)
  {
    this.ValidateReadParams(userID);
    return userID == 0L ? this._CfgService.ReadSection(ModuleName, SectionID) : this.UserSession.DataManager.ExecuteDataTable($"SELECT F_PARAM_NAME, F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = {SqlHelper.QString(ModuleName)} AND F_USER_ID = {userID} AND F_SECTION_ID = {SqlHelper.QString(SectionID)}");
  }

  public void WriteSection(string ModuleName, string SectionID, DataTable table, long userID)
  {
    this.ValidateWriteParams(userID);
    DataTable dataTable = this.ReadSection(ModuleName, SectionID, userID);
    for (int index1 = 0; index1 < table.Rows.Count; ++index1)
    {
      bool flag = true;
      for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
      {
        if (dataTable.Rows[index2][0].ToString().Equals(table.Rows[index1][0].ToString()))
        {
          flag = !dataTable.Rows[index2][1].ToString().Equals(table.Rows[index1][1].ToString());
          dataTable.Rows.RemoveAt(index2);
          break;
        }
      }
      if (flag)
        this.WriteUserDBParam(ModuleName, SectionID, table.Rows[index1][0].ToString(), table.Rows[index1][1], userID);
    }
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (userID == 0L)
        this._CfgService.DeleteValue(ModuleName, SectionID, dataTable.Rows[index][0].ToString());
      this.UserSession.DataManager.ExecuteNonQuery("DELETE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = :userID AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", this.UserSession.DataManager.Parameter("moduleName", (object) ModuleName), this.UserSession.DataManager.Parameter(nameof (userID), (object) userID), this.UserSession.DataManager.Parameter("sectID", (object) SectionID), this.UserSession.DataManager.Parameter("parName", (object) dataTable.Rows[index][0].ToString()));
    }
  }

  public ConcurrentDictionary<ConfigParamKey, object> GetConfigCache() => this._UserParamsCache;
}
