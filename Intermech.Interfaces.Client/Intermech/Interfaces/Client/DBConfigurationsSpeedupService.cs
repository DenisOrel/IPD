// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DBConfigurationsSpeedupService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Клиентский прокси-класс сервиса для чтения и записи настроек.
/// Реализация является thread safe.
/// </summary>
internal sealed class DBConfigurationsSpeedupService : 
  ClientSessionSpeedupServiceBase,
  IDBConfigurations,
  IDBConfigurationsSpeedupService
{
  private long _CurrentUserID;
  private bool _IsAdmin;
  private ConcurrentDictionary<ConfigParamKeyEx, object> _ParamsCache;

  /// <summary>Создает объект</summary>
  /// <param name="clientCache">Сервис клиентского кэша метаданных</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="clientCache" /> содержит null</exception>
  public DBConfigurationsSpeedupService(IClientCache clientCache)
    : base(clientCache)
  {
    this._CurrentUserID = 0L;
    this._IsAdmin = false;
    this._ParamsCache = new ConcurrentDictionary<ConfigParamKeyEx, object>();
  }

  /// <summary>
  /// Очищает сервис после очистки клиентского кэша метаданных.
  /// Реализация является thread safe.
  /// </summary>
  protected override void DoClear()
  {
    base.DoClear();
    this._CurrentUserID = 0L;
    this._IsAdmin = false;
    this._ParamsCache.Clear();
  }

  /// <summary>
  /// Инициализирует сервис после заполнения клиентского кэша метаданных.
  /// Реализация является thread safe.
  /// </summary>
  /// <param name="userSession">Пользовательская сессия</param>
  protected override void DoInitialize(IUserSession userSession)
  {
    base.DoInitialize(userSession);
    this._CurrentUserID = userSession.UserID;
    this._IsAdmin = userSession.IsAdmin;
    this._ParamsCache.Clear();
    DataTable configurations = userSession.ServerCache.GetConfigurations();
    for (int index = 0; index < configurations.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(configurations.Rows[index]["F_USER_ID"]);
      this._ParamsCache.TryAdd(new ConfigParamKeyEx(configurations.Rows[index]["F_MODULE_NAME"].ToString(), configurations.Rows[index]["F_SECTION_ID"].ToString(), configurations.Rows[index]["F_PARAM_NAME"].ToString(), int64 == 0L), configurations.Rows[index]["F_VALUE"]);
    }
  }

  private object ReadDBParam(
    string aModuleName,
    string aSectionID,
    string aParamName,
    bool isCommon)
  {
    object obj;
    return this._ParamsCache.TryGetValue(new ConfigParamKeyEx(aModuleName, aSectionID, aParamName, isCommon), out obj) ? obj : (object) null;
  }

  private object ReadUserDBParam(
    string aModuleName,
    string aSectionID,
    string aParamName,
    DBConfigMode configMode)
  {
    bool isCommon = configMode != DBConfigMode.UserOnly && configMode != DBConfigMode.UserAndGlobal;
    object obj = this.ReadDBParam(aModuleName, aSectionID, aParamName, isCommon);
    if (obj == null)
    {
      switch (configMode)
      {
        case DBConfigMode.GlobalOnly:
        case DBConfigMode.GlobalAndUser:
          obj = this.ReadDBParam(aModuleName, aSectionID, aParamName, true);
          break;
        case DBConfigMode.UserAndGlobal:
          obj = this.ReadDBParam(aModuleName, aSectionID, aParamName, false);
          break;
      }
    }
    return obj;
  }

  public IDBAttribute GetConfigAttribute(string data_name)
  {
    this.CheckInitialized();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session as ClientSession).Session.Configurations.GetConfigAttribute(data_name);
  }

  public void LoadConfigData(
    string config_name,
    out BlobInformation config_info,
    out byte[] config_file)
  {
    this.CheckInitialized();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session as ClientSession).Session.Configurations.LoadConfigData(config_name, out config_info, out config_file);
  }

  public void LoadConfigData(
    string config_name,
    out BlobInformation config_info,
    out byte[] config_file,
    long userID)
  {
    this.CheckInitialized();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session as ClientSession).Session.Configurations.LoadConfigData(config_name, out config_info, out config_file, userID);
  }

  public bool ParameterPresent(
    string ModuleName,
    string SectionID,
    string ParamName,
    DBConfigMode configMode)
  {
    this.CheckInitialized();
    bool isCommon = configMode == DBConfigMode.GlobalOnly || configMode == DBConfigMode.GlobalAndUser;
    return this.ReadDBParam(ModuleName, SectionID, ParamName, isCommon) != null;
  }

  /// <summary>Прочитать логический параметр</summary>
  public bool ReadBool(
    string ModuleName,
    string SectionID,
    string ParamName,
    bool DefaultValue,
    DBConfigMode configMode)
  {
    this.CheckInitialized();
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? Convert.ToBoolean(obj) : DefaultValue;
  }

  /// <summary>Прочитать дату</summary>
  public DateTime ReadDateTime(
    string ModuleName,
    string SectionID,
    string ParamName,
    DateTime DefaultValue,
    DBConfigMode configMode)
  {
    this.CheckInitialized();
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    if (obj == null)
      return DefaultValue;
    DateTime now = DateTime.Now;
    TimeSpan timeSpan = now - now.ToUniversalTime();
    return DateTime.FromBinary(Convert.ToInt64(obj)) + timeSpan;
  }

  /// <summary>Прочитать целочисленный параметр</summary>
  public long ReadInteger(
    string ModuleName,
    string SectionID,
    string ParamName,
    long DefaultValue,
    DBConfigMode configMode)
  {
    this.CheckInitialized();
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? Convert.ToInt64(obj) : DefaultValue;
  }

  /// <summary>Прочитать вещественный параметр</summary>
  public double ReadDouble(
    string ModuleName,
    string SectionID,
    string ParamName,
    double DefaultValue,
    DBConfigMode configMode)
  {
    this.CheckInitialized();
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? Convert.ToDouble(obj) : DefaultValue;
  }

  /// <summary>Прочитать строковый параметр</summary>
  /// <param name="ModuleName">Имя модуля, который сохраняет конфигурации</param>
  /// <param name="SectionID">Имя секции</param>
  /// <param name="ParamName">Имя параметра</param>
  /// <param name="DefaultValue">Значение параметра по умолчанию.</param>
  /// <param name="configMode"></param>
  public string ReadString(
    string ModuleName,
    string SectionID,
    string ParamName,
    string DefaultValue,
    DBConfigMode configMode)
  {
    this.CheckInitialized();
    object obj = this.ReadUserDBParam(ModuleName, SectionID, ParamName, configMode);
    return obj != null ? obj.ToString() : DefaultValue;
  }

  private void ValidateReadParams(long aUserID)
  {
    if (aUserID != 0L && aUserID != this._CurrentUserID && !this._IsAdmin)
      throw new KernelExceptionID(117);
  }

  /// <summary>
  /// Прочитать содержимое секции SectionID для текущего пользователя. F_PARAM_NAME - имя параметра,
  /// F_VALUE - значение параметра (строка).
  /// </summary>
  /// <param name="ModuleName">Имя модуля</param>
  /// <param name="SectionID">Имя секции</param>
  /// <param name="userID">Идентификатор пользователя</param>
  public DataTable ReadSection(string ModuleName, string SectionID, long userID)
  {
    this.CheckInitialized();
    this.ValidateReadParams(userID);
    DataTable dataTable = new DataTable("IMS_CONFIGS");
    dataTable.Columns.Add("F_PARAM_NAME", typeof (string));
    dataTable.Columns.Add("F_VALUE", typeof (string));
    bool flag = userID == 0L;
    foreach (KeyValuePair<ConfigParamKeyEx, object> keyValuePair in this._ParamsCache)
    {
      if (keyValuePair.Key.ModuleName == ModuleName && keyValuePair.Key.SectionName == SectionID && keyValuePair.Key.IsCommonParam == flag)
        dataTable.Rows.Add((object) keyValuePair.Key.ParamName, keyValuePair.Value);
    }
    return dataTable;
  }

  public string ReadStringNoCache(
    string aModuleName,
    string aSectionID,
    string aParamName,
    bool isGlobalParam)
  {
    this.CheckInitialized();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session as ClientSession).Session.Configurations.ReadStringNoCache(aModuleName, aSectionID, aParamName, isGlobalParam);
  }

  private void ValidateWriteParams(long aUserID)
  {
    if (aUserID != this._CurrentUserID && !this._IsAdmin)
      throw new KernelExceptionID(115);
  }

  private void WriteUserDBParam(
    string aModuleName,
    string aSectionID,
    string aParamName,
    object aValue,
    long aUserID)
  {
    this.ValidateWriteParams(aUserID);
    if (aUserID != 0L && aUserID != this._CurrentUserID)
      return;
    ConfigParamKeyEx key = new ConfigParamKeyEx(aModuleName, aSectionID, aParamName, aUserID == 0L);
    this._ParamsCache.TryRemove(key, out object _);
    this._ParamsCache.TryAdd(key, aValue);
  }

  private void WriteUserDBParam(
    string aModuleName,
    string aSectionID,
    string aParamName,
    object aValue)
  {
    this.WriteUserDBParam(aModuleName, aSectionID, aParamName, aValue, this._CurrentUserID);
  }

  public int WriteBool(string ModuleName, string SectionID, string ParamName, bool Value)
  {
    this.CheckInitialized();
    return this.WriteBool(ModuleName, SectionID, ParamName, Value, this._CurrentUserID);
  }

  public int WriteBool(
    string ModuleName,
    string SectionID,
    string ParamName,
    bool Value,
    long userID)
  {
    this.CheckInitialized();
    this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, userID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session as ClientSession).Session.Configurations.WriteBool(ModuleName, SectionID, ParamName, Value, userID);
  }

  public void WriteConfigData(BlobInformation config_info, byte[] config_file)
  {
    this.CheckInitialized();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session as ClientSession).Session.Configurations.WriteConfigData(config_info, config_file);
  }

  public void WriteConfigData(BlobInformation config_info, byte[] config_file, long userID)
  {
    this.CheckInitialized();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session as ClientSession).Session.Configurations.WriteConfigData(config_info, config_file, userID);
  }

  public int WriteDateTime(string ModuleName, string SectionID, string ParamName, DateTime Value)
  {
    this.CheckInitialized();
    return this.WriteDateTime(ModuleName, SectionID, ParamName, Value, this._CurrentUserID);
  }

  public int WriteDateTime(
    string ModuleName,
    string SectionID,
    string ParamName,
    DateTime Value,
    long userID)
  {
    this.CheckInitialized();
    this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, userID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session as ClientSession).Session.Configurations.WriteDateTime(ModuleName, SectionID, ParamName, Value, userID);
  }

  public int WriteDouble(string ModuleName, string SectionID, string ParamName, double Value)
  {
    this.CheckInitialized();
    return this.WriteDouble(ModuleName, SectionID, ParamName, Value, this._CurrentUserID);
  }

  public int WriteDouble(
    string ModuleName,
    string SectionID,
    string ParamName,
    double Value,
    long userID)
  {
    this.CheckInitialized();
    this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, userID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session as ClientSession).Session.Configurations.WriteDouble(ModuleName, SectionID, ParamName, Value, userID);
  }

  public int WriteInteger(string ModuleName, string SectionID, string ParamName, long Value)
  {
    this.CheckInitialized();
    return this.WriteInteger(ModuleName, SectionID, ParamName, Value, this._CurrentUserID);
  }

  public int WriteInteger(
    string ModuleName,
    string SectionID,
    string ParamName,
    long Value,
    long userID)
  {
    this.CheckInitialized();
    this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, userID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session as ClientSession).Session.Configurations.WriteInteger(ModuleName, SectionID, ParamName, Value, userID);
  }

  public void WriteSection(string ModuleName, string SectionID, DataTable table, long userID)
  {
    this.CheckInitialized();
    this.ValidateWriteParams(userID);
    this.DeleteSectionFromCache(ModuleName, SectionID, userID);
    for (int index = 0; index < table.Rows.Count; ++index)
      this.WriteUserDBParam(ModuleName, SectionID, table.Rows[index][0].ToString(), table.Rows[index][1], userID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session as ClientSession).Session.Configurations.WriteSection(ModuleName, SectionID, table, userID);
  }

  /// <summary>Удаляет из кэша значений секцию</summary>
  /// <param name="moduleName">Модуль</param>
  /// <param name="sectionID">Секция</param>
  /// <param name="userID">ид. юзера, чье это настройки</param>
  private void DeleteSectionFromCache(string moduleName, string sectionID, long userID)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.ReadSection(moduleName, sectionID, userID).Rows)
      this._ParamsCache.TryRemove(new ConfigParamKeyEx(moduleName, sectionID, row[0].ToString(), userID == 0L), out object _);
  }

  public int WriteString(string ModuleName, string SectionID, string ParamName, string Value)
  {
    this.CheckInitialized();
    return this.WriteString(ModuleName, SectionID, ParamName, Value, this._CurrentUserID);
  }

  public int WriteString(
    string ModuleName,
    string SectionID,
    string ParamName,
    string Value,
    long userID)
  {
    this.CheckInitialized();
    this.WriteUserDBParam(ModuleName, SectionID, ParamName, (object) Value, userID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session as ClientSession).Session.Configurations.WriteString(ModuleName, SectionID, ParamName, Value, userID);
  }

  public bool WriteStringNoCache(
    string ModuleName,
    string SectionID,
    string ParamName,
    string Value,
    string oldValue,
    long userID)
  {
    this.CheckInitialized();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session as ClientSession).Session.Configurations.WriteStringNoCache(ModuleName, SectionID, ParamName, Value, oldValue, userID);
  }
}
