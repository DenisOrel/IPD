// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientSession
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using ImSSP;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.NotifySamples;
using Intermech.Interfaces.Projects;
using Intermech.Interfaces.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.Remoting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Remoting;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for ClientSession.</summary>
internal class ClientSession : 
  LongLifeObject,
  IClientSession,
  IUserSession,
  IUserSessionCacheDataSet,
  IServerObjectWrapper
{
  private MarshalByRefObject _sessionMbr;
  private IUserSession _session;
  private readonly ClientSessionContext _clientSessionContext;
  private readonly ClientSessionGuard _guard;
  private IIDHelper _idHelper;
  private Guid? _sessionGuid;
  private Guid? _masterSessionGuid;
  private long? _clientConnectionID;
  private bool? _isSessionGuardActive;
  private IDBTransactions _dbTransactionsProxy;
  private long _UserID;
  private long _actingUserID;
  private string _ActingUserName;
  private string _ComputerName;
  private int _SecurityLevel = -1;
  private string _UserName;
  private string _AreaID;
  private string _LanguageID;
  private int _MaxRows;
  private long _RoleID;
  private bool? _IsAdmin;
  private bool? _IsSystemSession;
  private bool? _ShowPersonalObjects;
  private bool? _EnableEditOwnSelections;
  private bool? _EnabledPdmConfigurator;
  private bool? _EnabledSeriesDates;
  private bool? _EnabledAutoSoftInstantiation;
  private bool? _AllVersionsAnnulmentMode;

  /// <summary>
  /// Метод записывает сообщение в лог-файл сервера приложений.
  /// </summary>
  /// <param name="text">Текст клиентского сообщения</param>
  /// <param name="traceFileName">Имя файла трассировки</param>
  [Obsolete("Use the method AddToTrace instead of this.", true)]
  public void AddToServerTrace(string text, string traceFileName = null)
  {
    this._session.AddToTrace(text, Consts.traceAlways, traceFileName);
  }

  /// <summary>
  /// Метод записывает сообщение в лог-файл сервера приложений.
  /// </summary>
  /// <param name="text">Текст клиентского сообщения</param>
  /// <param name="traceLevel">Уровень трассировки, при котором сообщение будет записано в файл</param>
  /// <param name="traceFileName">Имя файла трассировки</param>
  public void AddToTrace(string text, int traceLevel, string traceFileName = null)
  {
    this._session.AddToTrace(text, traceLevel, traceFileName);
  }

  /// <summary>
  /// Возвращает необернутый объект пользовательской сессии.
  /// </summary>
  public IUserSession Session
  {
    [DebuggerStepThrough] get => this._session;
  }

  /// <summary>
  /// Возвращает необернутый объект пользовательской сессии.
  /// </summary>
  /// <returns>Необернутый объект пользовательской сессии</returns>
  MarshalByRefObject IServerObjectWrapper.GetServerObject() => this._sessionMbr;

  /// <summary>Возвращает remoting-ссылку на текущий объект.</summary>
  /// <param name="requestedType">Тип ссылки</param>
  /// <returns>remoting-ссылка на текущий объект</returns>
  public override ObjRef CreateObjRef(Type requestedType)
  {
    return this._sessionMbr.CreateObjRef(requestedType);
  }

  /// <summary>
  /// Контекст, в котором работает клиентская сессия.
  /// Контекст предназначен для изоляции <see cref="T:Intermech.Interfaces.Client.ClientSession" /> от контейнера сервисов,
  /// он содержит все необходимые сервисы и события.
  /// </summary>
  public ClientSessionContext ClientSessionContext
  {
    [DebuggerStepThrough] get => this._clientSessionContext;
  }

  /// <summary>Клиентский кэш</summary>
  public IClientCache ClientCache
  {
    [DebuggerStepThrough] get => this._clientSessionContext.ClientCache;
  }

  /// <summary>Таблицы кэша метаданных</summary>
  public DataSet CacheDataSet
  {
    [DebuggerStepThrough] get
    {
      return this.ClientCache == null ? (DataSet) null : this.ClientCache.CacheDataSet;
    }
  }

  public ClientSession(IUserSession session, ClientSessionContext clientSessionContext)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (clientSessionContext == null)
      throw new ArgumentNullException(nameof (clientSessionContext));
    this._sessionMbr = (MarshalByRefObject) session;
    this._session = session;
    this._clientSessionContext = clientSessionContext;
    this._guard = new ClientSessionGuard(this);
  }

  /// <summary>
  /// Возвращает объект, который позволяет проверить корректность обращения к сессии и сессионным объектам.
  /// </summary>
  internal ClientSessionGuard Guard
  {
    [DebuggerStepThrough] get => this._guard;
  }

  /// <summary>
  /// Идентификатор подключенного пользователя (только для чтения).
  /// </summary>
  public long UserID
  {
    [DebuggerStepThrough] get
    {
      if (this._UserID == 0L)
        this._UserID = this._session.UserID;
      return this._UserID;
    }
  }

  /// <summary>
  /// Идентификатор пользователя, который на самом деле работает от имени текущего пользователя (исполняет его обязанности)
  /// </summary>
  public long ActingUserID
  {
    [DebuggerStepThrough] get
    {
      if (this._actingUserID == 0L)
        this._actingUserID = this._session.ActingUserID;
      return this._actingUserID;
    }
  }

  /// <summary>
  /// Имя пользователя, который на самом деле работает от имени текущего пользователя (исполняет его обязанности)
  /// </summary>
  public string ActingUserName
  {
    [DebuggerStepThrough] get
    {
      if (this._ActingUserName == null)
        this._ActingUserName = this._session.ActingUserName;
      return this._ActingUserName;
    }
  }

  /// <summary>
  /// Сетевое имя клиентского компьютера (только для чтения).
  /// </summary>
  public string ComputerName
  {
    [DebuggerStepThrough] get
    {
      if (this._ComputerName == null)
        this._ComputerName = this._session.ComputerName;
      return this._ComputerName;
    }
  }

  /// <summary>Уровень допуска текущего пользователя</summary>
  public int SecurityLevel
  {
    [DebuggerStepThrough] get
    {
      if (this._SecurityLevel == -1)
        this._SecurityLevel = this._session.SecurityLevel;
      return this._SecurityLevel;
    }
  }

  /// <summary>Имя залогиненного пользователя (только для чтения).</summary>
  public string UserName
  {
    [DebuggerStepThrough] get
    {
      if (this._UserName == null)
        this._UserName = this._session.UserName;
      return this._UserName;
    }
  }

  /// <summary>
  /// Дата и время последнего обращения к интерфейсу (только для чтения).
  /// </summary>
  public DateTime LastCallTime
  {
    [DebuggerStepThrough] get => this._session.LastCallTime;
  }

  /// <summary>
  /// Идентификатор(ы) предметной области, в которой работает эта сессия. Если пусто,
  /// то доступ к объектам из всех областей.
  /// </summary>
  public string AreaID
  {
    [DebuggerStepThrough] get
    {
      if (this._AreaID == null)
        this._AreaID = this._session.AreaID;
      return this._AreaID;
    }
    [DebuggerStepThrough] set
    {
      this._session.AreaID = value;
      this._AreaID = value;
    }
  }

  /// <summary>
  /// Идентификатор(ы) языков, в которых работает эта сессия. Если пусто,
  /// то доступ к атрибутам на всех языках.
  /// </summary>
  public string LanguageID
  {
    [DebuggerStepThrough] get
    {
      if (this._LanguageID == null)
        this._LanguageID = this._session.LanguageID;
      return this._LanguageID;
    }
    [DebuggerStepThrough] set
    {
      this._session.LanguageID = value;
      this._LanguageID = value;
    }
  }

  /// <summary>
  /// Количество строк с данными в одном пакете (режим пакетного чтения)
  /// </summary>
  public int MaxRows
  {
    [DebuggerStepThrough] get
    {
      if (this._MaxRows == 0)
        this._MaxRows = this._session.MaxRows;
      return this._MaxRows;
    }
    [DebuggerStepThrough] set
    {
      this._session.MaxRows = value;
      this._MaxRows = value;
    }
  }

  /// <summary>
  /// Смещение времени текущей временнОй зоны рабочей
  /// станции пользователя относительно универсального времени по Гринвичу
  /// </summary>
  public TimeSpan TimeZoneOffset
  {
    [DebuggerStepThrough] get => this._session.TimeZoneOffset;
  }

  /// <summary>
  /// Идентификатор роли, с которой пользователь подключился
  /// к системе
  /// </summary>
  public long RoleID
  {
    [DebuggerStepThrough] get
    {
      if (this._RoleID == 0L)
        this._RoleID = this._session.RoleID;
      return this._RoleID;
    }
  }

  /// <summary>Время сервера</summary>
  public DateTime UTCTime
  {
    [DebuggerStepThrough] get => this._session.UTCTime;
  }

  /// <summary>
  /// Уникальный идентификатор клиентского подключения к серверу приложений.
  /// Идентификатор присваивается сервером приложений при создании первой сессии клиента.
  /// Все сессии одного клиента будут иметь один и тот же идентификатор клиентского подключения;
  /// два разных клиента, вошедших под одним и тем же пользователем IPS, будут иметь разные идентификаторы.
  /// </summary>
  public long ClientConnectionID
  {
    [DebuggerStepThrough] get
    {
      if (!this._clientConnectionID.HasValue)
        this._clientConnectionID = new long?(this._session.ClientConnectionID);
      return this._clientConnectionID.Value;
    }
  }

  /// <summary>Вход пользователя в систему</summary>
  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID)
  {
    return this._session.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID);
  }

  /// <summary>Вход пользователя в систему</summary>
  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    int accessLevel)
  {
    return this._session.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, accessLevel);
  }

  /// <summary>Вход пользователя в систему</summary>
  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    string sessionName)
  {
    return this._session.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, sessionName);
  }

  /// <summary>Вход пользователя в систему</summary>
  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    int accessLevel,
    string sessionName)
  {
    return this._session.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, accessLevel, sessionName);
  }

  public long LoginAsActingUser(ActingUserLoginParameters loginParameters)
  {
    return this._session.LoginAsActingUser(loginParameters);
  }

  /// <summary>Выход пользователя из системы</summary>
  public int Logout(string sessionName) => this._session.Logout(sessionName);

  /// <summary>
  /// Создает копию текущей сессии для фоновых операций и выполняет Login
  /// с текущими параметрами.
  /// </summary>
  /// <param name="sessionName">Имя сессии для защиты ее от других сессий</param>
  /// <returns>Копия сессии.</returns>
  public IUserSession Clone(string sessionName)
  {
    return (IUserSession) new ClientSession(this._session.Clone(sessionName), this._clientSessionContext);
  }

  /// <summary>Получить интерфейс для записи в журнал событий</summary>
  public IEventLog EventLog
  {
    [DebuggerStepThrough] get => this._session.EventLog;
  }

  /// <summary>
  /// Получить интерфейс для работы с архивным журналом событий
  /// </summary>
  public IEventLog EventLogArchive
  {
    [DebuggerStepThrough] get => this._session.EventLogArchive;
  }

  /// <summary>Идентификатор сессии в списке сессий.</summary>
  [Obsolete("Use the property SessionGUID instead of this.", true)]
  public int SessionID
  {
    [DebuggerStepThrough] get => this._session.SessionID;
  }

  /// <summary>Глобальный идентификатор сессии в списке сессий.</summary>
  public Guid SessionGUID
  {
    [DebuggerStepThrough] get
    {
      if (!this._sessionGuid.HasValue)
        this._sessionGuid = new Guid?(this._session.SessionGUID);
      return this._sessionGuid.Value;
    }
  }

  /// <summary>
  /// Глобальный идентификатор мастер-сессии в списке сессий
  /// </summary>
  public Guid MasterSessionGUID
  {
    [DebuggerStepThrough] get
    {
      if (!this._masterSessionGuid.HasValue)
        this._masterSessionGuid = new Guid?(this._session.MasterSessionGUID);
      return this._masterSessionGuid.Value;
    }
  }

  /// <summary>
  /// Включение данного режима позволяет включать в списки удаленные объекты. Требует
  /// админских прав.
  /// </summary>
  public bool ShowDeletedObjects
  {
    [DebuggerStepThrough] get => this._session.ShowDeletedObjects;
    [DebuggerStepThrough] set => this._session.ShowDeletedObjects = value;
  }

  /// <summary>
  /// Получить объект базы данных по его идентификатору objectID (F_OBJECT_ID)
  /// </summary>
  public IDBObject GetObject(long objectID) => this._session.GetObject(objectID);

  /// <summary>
  /// Получить объект базы данных по его идентификатору objectID (F_OBJECT_ID)
  /// </summary>
  public IDBObject GetObject(long objectID, bool failIfNotFound)
  {
    return this._session.GetObject(objectID, failIfNotFound);
  }

  public IDBObject[] GetObjects(long[] objectIDs, bool failIfNotFound)
  {
    return this._session.GetObjects(objectIDs, failIfNotFound);
  }

  /// <summary>Получить базовую версию объекта по его идентификатору</summary>
  public IDBObject GetObjectBaseVersionByID(long id, bool failIfNotFound)
  {
    return this._session.GetObjectBaseVersionByID(id, failIfNotFound);
  }

  /// <summary>
  /// Возвращает обработчик рабочей копии объекта для данного пользователя (если таковая в базе имеется).
  /// Если рабочей копии у объекта нет (или объект взят на изменение другим пользователем), то метод
  /// возвращает обработчик архивной копии объекта.
  /// </summary>
  public IDBObject GetObjectActualCopy(long objectID, bool failIfNotFound)
  {
    return this._session.GetObjectActualCopy(objectID, failIfNotFound);
  }

  public IDBObject GetObjectActual(long objectID, bool failIfNotFound)
  {
    return this._session.GetObjectActual(objectID, failIfNotFound);
  }

  /// <summary>
  /// Получить объект базы данных по его идентификатору GUID-у
  /// </summary>
  public IDBObject GetObject(Guid objectGUID) => this._session.GetObject(objectGUID);

  /// <summary>
  /// Получить объект базы данных по его идентификатору GUID-у
  /// </summary>
  public IDBObject GetObject(Guid objectGUID, bool throwNotFoundException)
  {
    return this._session.GetObject(objectGUID, throwNotFoundException);
  }

  /// <summary>Получить уровень продвижения номер aLevelID</summary>
  public IDBLifecycleLevelType GetLifecycleLevel(int aLevelID)
  {
    return (IDBLifecycleLevelType) new СLifecycleLevel(this, aLevelID);
  }

  public IDBLifecycleLevelType GetLifecycleLevel(int aLevelID, bool throwException)
  {
    if (this.ClientCache.GetTable("IMS_LEVELS").Rows.Find((object) aLevelID) == null)
    {
      this.ClientCache.ReloadCacheCategory(8, this.Session);
      if (this.ClientCache.GetTable("IMS_LEVELS").Rows.Find((object) aLevelID) == null)
      {
        if (throwException)
          throw new KernelExceptionID(215, (object) aLevelID.ToString());
        return (IDBLifecycleLevelType) null;
      }
    }
    return this.GetLifecycleLevel(aLevelID);
  }

  /// <summary>Получить уровень продвижения с именем levelName</summary>
  public IDBLifecycleLevelType GetLifecycleLevel(string levelName)
  {
    return this.GetLifecycleLevel(levelName, true);
  }

  /// <summary>Получить уровень продвижения с именем levelName</summary>
  public IDBLifecycleLevelType GetLifecycleLevel(string levelName, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_LEVELS").Select("F_LEVEL_NAME = " + DataSetProcessor.QString(levelName));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(8, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_LEVELS").Select("F_LEVEL_NAME = " + DataSetProcessor.QString(levelName));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(173, (object) levelName);
        return (IDBLifecycleLevelType) null;
      }
    }
    return this.GetLifecycleLevel(Convert.ToInt32(dataRowArray[0]["F_LEVEL_ID"]));
  }

  public IDBLCSchema GetLCSchema(int schemaID) => (IDBLCSchema) new CLCSchema(this, schemaID);

  public IDBLCSchema GetLCSchema(int schemaID, bool throwException)
  {
    if (this.ClientCache.GetTable("IMS_LC_SCHEMAS").Rows.Find((object) schemaID) == null)
    {
      this.ClientCache.ReloadCacheCategory(16 /*0x10*/, this.Session);
      if (this.ClientCache.GetTable("IMS_LC_SCHEMAS").Rows.Find((object) schemaID) == null)
      {
        if (throwException)
          throw new KernelExceptionID(247, (object) schemaID);
        return (IDBLCSchema) null;
      }
    }
    return this.GetLCSchema(schemaID);
  }

  public IDBLCSchema GetLCSchema(Guid schemaGuid) => this.GetLCSchema(schemaGuid, true);

  public IDBLCSchema GetLCSchema(Guid schemaGuid, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_LC_SCHEMAS").Select("F_GUID = " + DataSetProcessor.QString(schemaGuid.ToString()));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(16 /*0x10*/, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_LC_SCHEMAS").Select("F_GUID = " + DataSetProcessor.QString(schemaGuid.ToString()));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(248, (object) schemaGuid.ToString());
        return (IDBLCSchema) null;
      }
    }
    return this.GetLCSchema(Convert.ToInt32(dataRowArray[0]["F_SCHEMA_ID"]));
  }

  public IDBLCSchema GetLCSchema(string schemaName) => this.GetLCSchema(schemaName, true);

  public IDBLCSchema GetLCSchema(string schemaName, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_LC_SCHEMAS").Select("F_GUID = " + DataSetProcessor.QString(schemaName));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(16 /*0x10*/, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_LC_SCHEMAS").Select("F_GUID = " + DataSetProcessor.QString(schemaName));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(254, (object) schemaName);
        return (IDBLCSchema) null;
      }
    }
    return this.GetLCSchema(Convert.ToInt32(dataRowArray[0]["F_SCHEMA_ID"]));
  }

  public IDBLCSchemaCollection GetLCSchemaCollection(bool filterRecs)
  {
    return this._session.GetLCSchemaCollection(filterRecs);
  }

  public IDBLCSchemaCollection GetLCSchemaCollection() => this.GetLCSchemaCollection(false);

  /// <summary>
  /// Получить уровень продвижения с глобальным ид. levelGuid
  /// </summary>
  public IDBLifecycleLevelType GetLifecycleLevel(Guid levelGuid)
  {
    return this.GetLifecycleLevel(levelGuid, true);
  }

  /// <summary>
  /// Получить уровень продвижения с глобальным ид. levelGuid
  /// </summary>
  public IDBLifecycleLevelType GetLifecycleLevel(Guid levelGuid, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_LEVELS").Select("F_GUID = " + DataSetProcessor.QString(levelGuid.ToString()));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(8, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_LEVELS").Select("F_GUID = " + DataSetProcessor.QString(levelGuid.ToString()));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(207, (object) levelGuid.ToString());
        return (IDBLifecycleLevelType) null;
      }
    }
    return this.GetLifecycleLevel(Convert.ToInt32(dataRowArray[0]["F_LEVEL_ID"]));
  }

  /// <summary>Получить список всех уровеней продвижения</summary>
  public IDBLifecycleLevelCollection GetLifecycleLevelCollection()
  {
    return this.GetLifecycleLevelCollection(false);
  }

  /// <summary>Получить список всех уровеней продвижения</summary>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  public IDBLifecycleLevelCollection GetLifecycleLevelCollection(bool filterRecs)
  {
    return (IDBLifecycleLevelCollection) new CLifecycleLevelCollection(this, filterRecs);
  }

  /// <summary>
  /// Получить языковой вариант (aLanguageID - буква-идентификатор)
  /// </summary>
  public IDBLanguageType GetLanguage(string aLanguageID)
  {
    return (IDBLanguageType) new CLanguageType(this, aLanguageID);
  }

  public IDBLanguageType GetLanguage(string aLanguageID, bool throwNotFoundException)
  {
    if (this.ClientCache.GetTable("IMS_LANGUAGES").Rows.Find((object) aLanguageID) == null)
    {
      this.ClientCache.ReloadCacheCategory(9, this._session);
      if (this.ClientCache.GetTable("IMS_LANGUAGES").Rows.Find((object) aLanguageID) == null)
      {
        if (throwNotFoundException)
          throw new KernelExceptionID(216, (object) aLanguageID.ToString());
        return (IDBLanguageType) null;
      }
    }
    return this.GetLanguage(aLanguageID);
  }

  /// <summary>Получить языковой вариант по guid-у</summary>
  public IDBLanguageType GetLanguage(Guid guid) => this.GetLanguage(guid, true);

  /// <summary>Получить языковой вариант по guid-у</summary>
  public IDBLanguageType GetLanguage(Guid guid, bool throwNotFoundException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_LANGUAGES").Select("F_GUID = " + DataSetProcessor.QString(guid.ToString()));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(9, this._session);
      dataRowArray = this.ClientCache.GetTable("IMS_LANGUAGES").Select("F_GUID = " + DataSetProcessor.QString(guid.ToString()));
      if (dataRowArray.Length == 0)
      {
        if (throwNotFoundException)
          throw new KernelExceptionID(184, (object) guid.ToString());
        return (IDBLanguageType) null;
      }
    }
    return this.GetLanguage(Convert.ToString(dataRowArray[0]["F_LANGUAGE_ID"]));
  }

  /// <summary>Получить группу атрибутов номер aGroupID</summary>
  public IDBAttributesGroup GetAttributesGroup(int aGroupID)
  {
    return (IDBAttributesGroup) new CAttributesGroup(this, aGroupID);
  }

  /// <summary>Получить группу атрибутов по guid-у</summary>
  public IDBAttributesGroup GetAttributesGroup(Guid guid) => this.GetAttributesGroup(guid, true);

  public IDBAttributesGroup GetAttributesGroup(int aGroupID, bool failIfNotFound)
  {
    if (this.ClientCache.GetTable("IMS_ATTR_GROUPS").Rows.Find((object) aGroupID) == null)
    {
      this.ClientCache.ReloadCacheCategory(12, this.Session);
      if (this.ClientCache.GetTable("IMS_ATTR_GROUPS").Rows.Find((object) aGroupID) == null)
      {
        if (failIfNotFound)
          throw new KernelExceptionID(217, (object) aGroupID.ToString());
        return (IDBAttributesGroup) null;
      }
    }
    return this.GetAttributesGroup(aGroupID);
  }

  public IDBAttributesGroup GetAttributesGroup(string groupName)
  {
    return this.GetAttributesGroup(groupName, true);
  }

  public IDBAttributesGroup GetAttributesGroup(string groupName, bool failIfNotFound)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_ATTR_GROUPS").Select("F_GROUP_NAME = " + DataSetProcessor.QString(groupName));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(12, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_ATTR_GROUPS").Select("F_GROUP_NAME = " + DataSetProcessor.QString(groupName));
      if (dataRowArray.Length == 0)
      {
        if (failIfNotFound)
          throw new KernelExceptionID(275, (object) groupName);
        return (IDBAttributesGroup) null;
      }
    }
    return this.GetAttributesGroup(Convert.ToInt32(dataRowArray[0]["F_GROUP_ID"]));
  }

  /// <summary>Получить группу атрибутов по guid-у</summary>
  public IDBAttributesGroup GetAttributesGroup(Guid guid, bool failIfNotFound)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_ATTR_GROUPS").Select("F_GUID = " + DataSetProcessor.QString(guid.ToString()));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(12, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_ATTR_GROUPS").Select("F_GUID = " + DataSetProcessor.QString(guid.ToString()));
      if (dataRowArray.Length == 0)
      {
        if (failIfNotFound)
          throw new KernelExceptionID(185, (object) guid.ToString());
        return (IDBAttributesGroup) null;
      }
    }
    return this.GetAttributesGroup(Convert.ToInt32(dataRowArray[0]["F_GROUP_ID"]));
  }

  /// <summary>Получить полный список групп атрибутов</summary>
  public IDBAttributesGroupCollection GetAttributesGroupCollection()
  {
    return this.GetAttributesGroupCollection(false);
  }

  /// <summary>Получить полный список групп атрибутов</summary>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  public IDBAttributesGroupCollection GetAttributesGroupCollection(bool filterRecs)
  {
    return (IDBAttributesGroupCollection) new CAttributesGroupCollection(this, filterRecs);
  }

  /// <summary>
  /// Получить список групп атрибутов, входящих в состав группы parentGroupID. Если parentGroupID == 0, то возвращается верний уровень иерархии групп атрибутов.
  /// Если parentGroupID меньше 0, то возвращается полный список групп.
  /// </summary>
  public IDBAttributesGroupCollection GetAttributesGroupCollection(int parentGroupID)
  {
    return this.GetAttributesGroupCollection(parentGroupID, false);
  }

  /// <summary>
  /// Получить список групп атрибутов, входящих в состав группы parentGroupID. Если parentGroupID == 0, то возвращается верний уровень иерархии групп атрибутов.
  /// Если parentGroupID меньше 0, то возвращается полный список всех групп атрибутов.
  /// </summary>
  /// <param name="parentGroupID">Ид. родительской группы атрибутов. Если parentGroupID == 0, то возвращается верний уровень иерархии групп атрибутов. Если parentGroupID меньше 0, то возвращается полный список всех групп атрибутов.</param>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  public IDBAttributesGroupCollection GetAttributesGroupCollection(
    int parentGroupID,
    bool filterRecs)
  {
    CAttributesGroupCollection attributesGroupCollection = new CAttributesGroupCollection(this, filterRecs);
    attributesGroupCollection.ParentID = (object) parentGroupID;
    return (IDBAttributesGroupCollection) attributesGroupCollection;
  }

  /// <summary>
  /// Возвращает числовой ид. атрибута по его имени, Guidу или числовому ид. attributeID
  /// </summary>
  internal int GetAttributeID(object attributeID, bool failIfNotFound)
  {
    switch (attributeID)
    {
      case null:
        attributeID = (object) 0;
        break;
      case string _:
        DataRow[] dataRowArray1 = this.ClientCache.GetTable("IMS_ATTRIBUTES").Select("F_NAME = " + DataSetProcessor.QString(attributeID.ToString()));
        if (dataRowArray1.Length != 0)
        {
          attributeID = dataRowArray1[0]["F_ATTRIBUTE_ID"];
          break;
        }
        if (failIfNotFound)
          throw new KernelExceptionID(84, attributeID);
        return 0;
      case Guid _:
        DataRow[] dataRowArray2 = this.ClientCache.GetTable("IMS_ATTRIBUTES").Select("F_GUID = " + DataSetProcessor.QString(attributeID.ToString()));
        if (dataRowArray2.Length != 0)
        {
          attributeID = dataRowArray2[0]["F_ATTRIBUTE_ID"];
          break;
        }
        if (failIfNotFound)
          throw new KernelExceptionID(85, attributeID);
        return 0;
      case int _:
        DataRow dataRow = this.ClientCache.GetTable("IMS_ATTRIBUTES").Rows.Find(attributeID);
        if (dataRow != null)
        {
          attributeID = dataRow["F_ATTRIBUTE_ID"];
          break;
        }
        if (failIfNotFound)
          throw new KernelExceptionID(181, attributeID);
        return 0;
    }
    return Convert.ToInt32(attributeID);
  }

  private IDBAttributeType GetClientAttributeType(object anAttribute, bool failIfNotFound)
  {
    int attributeId;
    if (anAttribute is string)
    {
      attributeId = this.GetAttributeID(anAttribute, false);
      if (attributeId == 0 & failIfNotFound)
      {
        this.ClientCache.ReloadCacheCategory(3, this._session);
        attributeId = this.GetAttributeID(anAttribute, failIfNotFound);
      }
    }
    else
      attributeId = this.GetAttributeID(anAttribute, failIfNotFound);
    return attributeId != 0 ? (IDBAttributeType) CAttributeTypeCreator.CreateCAttributeType(this, attributeId) : (IDBAttributeType) null;
  }

  /// <summary>Получить атрибут-тип номер anAttributeType</summary>
  public IDBAttributeType GetAttributeType(int anAttributeType)
  {
    return this.GetClientAttributeType((object) anAttributeType, true);
  }

  public IDBAttributeType GetAttributeType(int anAttributeType, bool failIfNotFound)
  {
    return this.GetClientAttributeType((object) anAttributeType, failIfNotFound);
  }

  /// <summary>Получить атрибут-тип с именем anAttributeName</summary>
  public IDBAttributeType GetAttributeType(string anAttributeName)
  {
    return this.GetClientAttributeType((object) anAttributeName, true);
  }

  public IDBAttributeType GetAttributeType(string anAttributeName, bool failIfNotFound)
  {
    return this.GetClientAttributeType((object) anAttributeName, failIfNotFound);
  }

  /// <summary>Получить атрибут-тип с гуидом anAttributeGuid</summary>
  public IDBAttributeType GetAttributeType(Guid anAttributeGuid)
  {
    return this.GetClientAttributeType((object) anAttributeGuid, true);
  }

  public IDBAttributeType GetAttributeType(Guid anAttributeGuid, bool failIfNotFound)
  {
    return this.GetClientAttributeType((object) anAttributeGuid, failIfNotFound);
  }

  /// <summary>Получить объект для работы с конфигурациями</summary>
  public IDBConfigurations Configurations
  {
    get
    {
      return (IDBConfigurations) this._clientSessionContext.DBConfigurationsSpeedupService ?? this._session.Configurations;
    }
  }

  /// <summary>Получить шаг жизненного цикла номер aLCStepID</summary>
  public IDBLifecycleStep GetLifecycleStep(int aLCStepID)
  {
    return (IDBLifecycleStep) new CLifecycleStep(this, aLCStepID, 0);
  }

  public IDBLifecycleStep GetLifecycleStep(int aLCStepID, int objectTypeID)
  {
    return (IDBLifecycleStep) new CLifecycleStep(this, aLCStepID, objectTypeID);
  }

  public IDBLifecycleStep GetLifecycleStep(int aLCStepID, bool failIfNotFound, int objectTypeID)
  {
    return !failIfNotFound && this.ClientCache.GetTable("IMS_LC_STEPS").Rows.Find((object) aLCStepID) == null ? (IDBLifecycleStep) null : (IDBLifecycleStep) new CLifecycleStep(this, aLCStepID, objectTypeID);
  }

  public IDBLifecycleStep GetLifecycleStep(int aLCStepID, bool failIfNotFound)
  {
    return this.GetLifecycleStep(aLCStepID, failIfNotFound, 0);
  }

  /// <summary>
  /// Получить шаг жизненного цикла с глобальным идентификатором anLCGuid
  /// </summary>
  public IDBLifecycleStep GetLifecycleStep(Guid anLCGuid) => this.GetLifecycleStep(anLCGuid, true);

  /// <summary>
  /// Получить шаг жизненного цикла с глобальным идентификатором anLCGuid
  /// </summary>
  public IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, int objectTypeID)
  {
    return this.GetLifecycleStep(anLCGuid, true, objectTypeID);
  }

  /// <summary>
  /// Получить шаг жизненного цикла с глобальным идентификатором anLCGuid
  /// </summary>
  public IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, bool throwException)
  {
    return this.GetLifecycleStep(anLCGuid, throwException, 0);
  }

  public IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, bool throwException, int objectTypeID)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_LC_STEPS").Select("F_GUID = " + DataSetProcessor.QString(anLCGuid.ToString()));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(7, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_LC_STEPS").Select("F_GUID = " + DataSetProcessor.QString(anLCGuid.ToString()));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(208 /*0xD0*/, (object) anLCGuid.ToString());
        return (IDBLifecycleStep) null;
      }
    }
    return this.GetLifecycleStep(Convert.ToInt32(dataRowArray[0]["F_LC_STEP"]), objectTypeID);
  }

  /// <summary>
  /// Получить коллекцию шагов жизненного цикла для объектов типа anObjectTypeID.
  /// Результатом может быть схема родительского типа в случае, если у объекта нет своей.
  /// </summary>
  public IDBLifecycleStepCollection GetLifecycleStepCollection(int anObjectTypeID)
  {
    return (IDBLifecycleStepCollection) new CLifecycleStepCollection(this, this.GetLCSchema(this.GetObjectType(anObjectTypeID).SchemaID), anObjectTypeID);
  }

  public IDBLifecycleStepCollection GetLifecycleStepCollection(int schemaID, int anObjectTypeID)
  {
    return (IDBLifecycleStepCollection) new CLifecycleStepCollection(this, this.GetLCSchema(schemaID), anObjectTypeID);
  }

  /// <summary>Получить тип объектов номер anObjectType</summary>
  public IDBObjectType GetObjectType(int anObjectType)
  {
    return (IDBObjectType) new СObjectType(this, anObjectType);
  }

  /// <summary>
  /// Получить тип объектов номер anObjectType. Если failIfNotFound == false, то возвращать null при отсутствии такого типа.
  /// </summary>
  public IDBObjectType GetObjectType(int anObjectType, bool failIfNotFound)
  {
    return !failIfNotFound && this.ClientCache.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) anObjectType) == null ? (IDBObjectType) null : (IDBObjectType) new СObjectType(this, anObjectType);
  }

  public IDBObjectType GetObjectType(string anObjectTypeName)
  {
    return this.GetObjectType(anObjectTypeName, true);
  }

  public IDBObjectType GetObjectType(string anObjectTypeName, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_OBJECT_TYPES").Select("F_OBJ_TYPE_NAME = " + DataSetProcessor.QString(anObjectTypeName));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(4, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_OBJECT_TYPES").Select("F_OBJ_TYPE_NAME = " + DataSetProcessor.QString(anObjectTypeName));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(97, (object) anObjectTypeName);
        return (IDBObjectType) null;
      }
    }
    return this.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]));
  }

  /// <summary>
  /// Получить тип объекта по имени объекта (например, "Деталь")
  /// </summary>
  public IDBObjectType GetObjectTypeByObjectName(string anObjectName, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_OBJECT_TYPES").Select("F_OBJ_NAME = " + DataSetProcessor.QString(anObjectName));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(4, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_OBJECT_TYPES").Select("F_OBJ_NAME = " + DataSetProcessor.QString(anObjectName));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(sc_10462.ssp_appserver_10463(292995212), (object) anObjectName);
        return (IDBObjectType) null;
      }
    }
    return this.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]), throwException);
  }

  public IDBObjectType GetObjectType(Guid anObjectTypeGuid)
  {
    return this.GetObjectType(anObjectTypeGuid, true);
  }

  public IDBObjectType GetObjectType(Guid anObjectTypeGuid, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_OBJECT_TYPES").Select("F_GUID = " + DataSetProcessor.QString(anObjectTypeGuid.ToString()));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(4, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_OBJECT_TYPES").Select("F_GUID = " + DataSetProcessor.QString(anObjectTypeGuid.ToString()));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(99, (object) anObjectTypeGuid);
        return (IDBObjectType) null;
      }
    }
    return this.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]));
  }

  /// <summary>
  /// Получает список типов объектов, идентификаторы версий которых указаны в массиве objectIDs.
  /// В результате присутствуют только неудалённые объекты. Результат отсортирован по возрастанию идентификаторов версий объектов.
  /// </summary>
  /// <param name="objectIDs">Идентификаторы версий объектов.</param>
  /// <returns>Список элементов Tuple, в котором хранятся идентификаторы версий и типа объектов.</returns>
  public List<Tuple<long, int>> GetObjectTypes(ICollection<long> objectIDs)
  {
    return this._session.GetObjectTypes(objectIDs);
  }

  /// <summary>
  /// Получить связь между объектами по ее идентификатору aRelationID
  /// </summary>
  public IDBRelation GetRelation(long aRelationID) => this._session.GetRelation(aRelationID);

  /// <summary>
  /// Получить связь между объектами по ее идентификатору aRelationID
  /// </summary>
  public IDBRelation GetRelation(long aRelationID, bool failIfNotFound)
  {
    return this._session.GetRelation(aRelationID, failIfNotFound);
  }

  /// <summary>
  /// Получить связь между объектами по ее глобальному идентификатору guid
  /// </summary>
  public IDBRelation GetRelation(Guid guid, long prjID) => this._session.GetRelation(guid, prjID);

  public IDBRelation GetRelation(Guid guid, long prjID, bool failIfNotFound)
  {
    return this._session.GetRelation(guid, prjID, failIfNotFound);
  }

  public IDBRelation GetRelation(Guid guid, bool failIfNotFound)
  {
    return this._session.GetRelation(guid, failIfNotFound);
  }

  /// <summary>
  /// Получить связь между, обозначающую вхождение объекта partID (если versionMode==true, то partID это ObjectID, иначе ID Объекта)
  /// в объект projectID связью типа
  /// relationType
  /// </summary>
  public IDBRelation GetRelation(long projectID, long partID, int relationType, bool versionMode)
  {
    return this._session.GetRelation(projectID, partID, relationType, versionMode);
  }

  /// <summary>
  /// Получить связь между, обозначающую вхождение объекта partID (если versionMode==true, то partID это ObjectID, иначе ID Объекта)
  /// в объект projectID связью любого типа
  /// </summary>
  public IDBRelation GetRelation(long projectID, long partID, bool versionMode)
  {
    return this._session.GetRelation(projectID, partID, versionMode);
  }

  /// <summary>
  /// Получить связь между, обозначающую вхождение объекта partID (IDBObject.ID) в объект projectID (IDBObject.ObjectID) связью типа
  /// relationType
  /// </summary>
  public IDBRelation GetRelation(long projectID, long partID, int relationType)
  {
    return this._session.GetRelation(projectID, partID, relationType);
  }

  /// <summary>
  /// Получить связь между, обозначающую вхождение объекта partID (IDBObject.ID) в объект projectID (IDBObject.ObjectID) связью любого типа
  /// </summary>
  public IDBRelation GetRelation(long projectID, long partID)
  {
    return this._session.GetRelation(projectID, partID);
  }

  public IDBRelation GetRelationByPartObjectID(
    long aRelationID,
    long partObjectID,
    bool failIfNotFound)
  {
    return this._session.GetRelationByPartObjectID(aRelationID, partObjectID, failIfNotFound);
  }

  /// <summary>Получить тип связей номер aRelationTypeID</summary>
  public IDBRelationType GetRelationType(int aRelationTypeID)
  {
    return this.GetRelationType(aRelationTypeID, true);
  }

  public IDBRelationType GetRelationType(int aRelationTypeID, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_RELATION_TYPES").Select("F_RELATION_TYPE = " + aRelationTypeID.ToString());
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCache(this._session);
      dataRowArray = this.ClientCache.GetTable("IMS_RELATION_TYPES").Select("F_RELATION_TYPE = " + aRelationTypeID.ToString());
    }
    if (dataRowArray.Length != 0)
      return (IDBRelationType) new CRelationType(this, aRelationTypeID);
    if (throwException)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_39"), (object) aRelationTypeID));
    return (IDBRelationType) null;
  }

  public IDBRelationType GetRelationType(Guid relationTypeGUID, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_RELATION_TYPES").Select("F_GUID = " + DataSetProcessor.QString(relationTypeGUID.ToString()));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(6, this._session);
      dataRowArray = this.ClientCache.GetTable("IMS_RELATION_TYPES").Select("F_GUID = " + DataSetProcessor.QString(relationTypeGUID.ToString()));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(122, (object) relationTypeGUID);
        return (IDBRelationType) null;
      }
    }
    return (IDBRelationType) new CRelationType(this, Convert.ToInt32(dataRowArray[0]["F_RELATION_TYPE"]));
  }

  public IDBRelationType GetRelationType(Guid relationTypeGUID)
  {
    return this.GetRelationType(relationTypeGUID, true);
  }

  public IDBRelationType GetRelationType(string rtypeDescription)
  {
    return this.GetRelationType(rtypeDescription, true);
  }

  public IDBRelationType GetRelationType(string rtypeDescription, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_RELATION_TYPES").Select("F_DESCRIPTION = " + DataSetProcessor.QString(rtypeDescription));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(6, this._session);
      dataRowArray = this.ClientCache.GetTable("IMS_RELATION_TYPES").Select("F_DESCRIPTION = " + DataSetProcessor.QString(rtypeDescription));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(174, (object) rtypeDescription);
        return (IDBRelationType) null;
      }
    }
    return this.GetRelationType(Convert.ToInt32(dataRowArray[0]["F_RELATION_TYPE"]));
  }

  /// <summary>
  /// Получить предметную область с идентификатором aSubjectAreaTypeID
  /// </summary>
  public IDBSubjectAreaType GetSubjectAreaType(char aSubjectAreaTypeID)
  {
    return (IDBSubjectAreaType) new CSubjectAreaType(this, aSubjectAreaTypeID);
  }

  public IDBSubjectAreaType GetSubjectAreaType(char aSubjectAreaTypeID, bool throwException)
  {
    if (this.ClientCache.GetTable("IMS_SUBJECT_AREAS").Rows.Find((object) aSubjectAreaTypeID) != null)
      return this.GetSubjectAreaType(aSubjectAreaTypeID);
    if (throwException)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_40"), (object) aSubjectAreaTypeID));
    return (IDBSubjectAreaType) null;
  }

  /// <summary>Получить предметную область с guid</summary>
  public IDBSubjectAreaType GetSubjectAreaType(Guid guid) => this.GetSubjectAreaType(guid, true);

  /// <summary>Получить предметную область с guid</summary>
  public IDBSubjectAreaType GetSubjectAreaType(Guid guid, bool throwException)
  {
    DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_SUBJECT_AREAS").Select("F_GUID = " + DataSetProcessor.QString(guid.ToString()));
    if (dataRowArray.Length == 0)
    {
      this.ClientCache.ReloadCacheCategory(11, this.Session);
      dataRowArray = this.ClientCache.GetTable("IMS_SUBJECT_AREAS").Select("F_GUID = " + DataSetProcessor.QString(guid.ToString()));
      if (dataRowArray.Length == 0)
      {
        if (throwException)
          throw new KernelExceptionID(183, (object) guid.ToString());
        return (IDBSubjectAreaType) null;
      }
    }
    return this.GetSubjectAreaType(Convert.ToChar(dataRowArray[0]["F_AREA_ID"]));
  }

  /// <summary>Получить список предметных областей</summary>
  public IDBSubjectAreaCollection GetSubjectAreaCollection()
  {
    return (IDBSubjectAreaCollection) new CSubjectAreaCollection(this);
  }

  /// <summary>Получить список языковых вариантов</summary>
  /// <returns></returns>
  public IDBLanguageCollection GetLanguageCollection()
  {
    return (IDBLanguageCollection) new CLanguageCollection(this);
  }

  /// <summary>
  /// Получить коллекцию объектов типа objectType. Если objectType=-1, то
  /// получается коллекция всех объектов. Под коллекцией объектов в данном
  /// случае понимаем объект, управляющий списком объектов. Никаких данных
  /// с СУБД эта операция не получае.
  /// </summary>
  /// <param name="objectType">ID типа объекта</param>
  /// <returns></returns>
  public IDBObjectCollection GetObjectCollection(int objectType)
  {
    return (IDBObjectCollection) new CDBObjectCollectionProxy(this, this._session.GetObjectCollection(objectType));
  }

  public IDBObjectCollection GetObjectCollection(Guid objectTypeGuid)
  {
    return (IDBObjectCollection) new CDBObjectCollectionProxy(this, this._session.GetObjectCollection(objectTypeGuid));
  }

  /// <summary>
  /// Получить список атрибутов в группе groupID. Если groupID = -1, то получается
  /// список всех атрибутов, зарегистрированных в системе.
  /// </summary>
  /// <param name="groupID">id группы атрибутов</param>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  public IDBAttributeTypeCollection GetAttributeTypeCollection(int groupID, bool filterRecs)
  {
    CAttributeTypeCollection attributeTypeCollection = new CAttributeTypeCollection(this, filterRecs);
    attributeTypeCollection.ParentID = (object) groupID;
    return (IDBAttributeTypeCollection) attributeTypeCollection;
  }

  /// <summary>
  /// Получить список атрибутов в группе groupID. Если groupID = -1, то получается
  /// список всех атрибутов, зарегистрированных в системе.
  /// </summary>
  public IDBAttributeTypeCollection GetAttributeTypeCollection(int groupID)
  {
    return this.GetAttributeTypeCollection(groupID, false);
  }

  /// <summary>
  /// Возвращает коллекцию типов объектов, входящих в состав типа parentTypeID.
  /// </summary>
  /// <param name="parentTypeID">Идентификатор родительского типа объектов. Если == -1,
  /// то возвращает корневые типы объектво. Если == -2, то возвращает ВСЕ типы объектов</param>
  public IDBObjectTypeCollection GetObjectTypeCollection(int parentTypeID)
  {
    return this.GetObjectTypeCollection(parentTypeID, false);
  }

  /// <summary>
  /// Возвращает коллекцию типов объектов, входящих в состав типа parentTypeID.
  /// </summary>
  /// <param name="parentTypeID">Идентификатор родительского типа объектов. Если == -1,
  /// то возвращает корневые типы объектво. Если == -2, то возвращает ВСЕ типы объектов</param>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  public IDBObjectTypeCollection GetObjectTypeCollection(int parentTypeID, bool filterRecs)
  {
    return (IDBObjectTypeCollection) new CObjectTypeCollection(this, parentTypeID, filterRecs);
  }

  /// <summary>Возвращает полный список типов связей.</summary>
  public IDBRelationTypeCollection GetRelationTypeCollection()
  {
    return this.GetRelationTypeCollection(false);
  }

  /// <summary>Возвращает список типов связей.</summary>
  /// <param name="filterRecs">Если == true, то список фильтруется по предметным областям и правам доступа.</param>
  public IDBRelationTypeCollection GetRelationTypeCollection(bool filterRecs)
  {
    return (IDBRelationTypeCollection) new CRelationTypeCollection(this, filterRecs);
  }

  /// <summary>
  /// Возвращает интерфейс на объект, управляющий входимостями типов объектов друг в друга
  /// </summary>
  public IDBRelationsApplicabilityCollection GetRelationsApplicabilityCollection()
  {
    return (IDBRelationsApplicabilityCollection) new CDBRelationsApplicabilityCollection(this);
  }

  /// <summary>
  /// Возвращает объект-получатель списка связей типа relationType
  /// (если relationType меньше 0, то связей всех типов).
  /// </summary>
  public IDBRelationCollection GetRelationCollection(int relationType)
  {
    return this._session.GetRelationCollection(relationType);
  }

  /// <summary>
  /// Возвращает объект-получатель списка связей типа relationType (если relationType меньше 0, то связей всех типов),
  /// при этом будет использоваться фильтрация состава на основе указанных настроек фильтрации.
  /// </summary>
  public IDBRelationCollection GetRelationCollection(int relationType, string FiltrationOwnerID)
  {
    return this._session.GetRelationCollection(relationType, FiltrationOwnerID);
  }

  /// <summary>
  /// Возвращает объект-получатель списка связей типа relationType (если relationType меньше 0, то связей всех типов),
  /// при этом будет использоваться фильтрация состава на основе указанных настроек фильтрации.
  /// </summary>
  public IDBRelationCollection GetRelationCollection(int relationType, VersionsRule rule)
  {
    return this._session.GetRelationCollection(relationType, rule);
  }

  public IDBSnapshotCollection GetSnapshotCollection() => this._session.GetSnapshotCollection();

  public IDBObjectSnapshot GetSnapshot(long snapshotID, bool throwException)
  {
    return this._session.GetSnapshot(snapshotID, throwException);
  }

  public IDBObjectSnapshot GetSnapshot(long snapshotID) => this.GetSnapshot(snapshotID, true);

  /// <summary>Возвращает интерфейс серверной части портфеля</summary>
  public IServerBriefcase GetBriefcase() => this._session.GetBriefcase();

  /// <summary>Возвращает интерфейс на импортер</summary>
  /// <param name="logFileName">Имя лог-файла</param>
  /// <returns></returns>
  public IDBImporter GetImporter(string logFileName) => this._session.GetImporter(logFileName);

  /// <summary>Интерфейс получателя идентификаторов</summary>
  public IIDHelper IdentHelper
  {
    [DebuggerStepThrough] get
    {
      if (this._idHelper == null)
        this._idHelper = (IIDHelper) new CIDHelper(this);
      return this._idHelper;
    }
  }

  /// <summary>
  /// Возвращает список ролей, которыми обладает пользователь номер userID. Если userID = -1,
  /// то возвращает список всех ролей, зарегистрированных в системе. Если userID = 0,
  /// то возвращает список ролей, которые имеются у пользователя данной сессии.
  /// </summary>
  public RoleProperties[] GetRolesList(long userID) => this._session.GetRolesList(userID);

  /// <summary>
  /// Возвращает список ролей, которыми обладает пользователь с логином loginName.
  /// Если такого юзера в системе нет, то возвращает полный список ролей.
  /// </summary>
  public RoleProperties[] GetRolesList(string loginName) => this._session.GetRolesList(loginName);

  /// <summary>
  /// Метод возвращает словарь с уровнями доступа, которые могут быть у юзера с логином loginName (либо все уровни, если такого логина в системе нет).
  /// Если пользователю уровень не назначен, то возвращает одну запись с минимальным уровнем доступа.
  /// </summary>
  /// <param name="loginName">Имя входа пользователя.</param>
  /// <returns>Возвращает структуру ид.уровня=наименование уровня доступа</returns>
  public Dictionary<int, string> GetSecurityLevels(string loginName)
  {
    return this._session.GetSecurityLevels(loginName);
  }

  public Dictionary<int, string> GetSecurityLevels(long id) => this._session.GetSecurityLevels(id);

  /// <summary>
  /// Получает интерфейс, зарегистрированный не сервере службой ICustomServices
  /// </summary>
  public object GetCustomService(Type serviceType)
  {
    if (serviceType == (Type) null)
      throw new ArgumentNullException(nameof (serviceType));
    if (serviceType == typeof (IDBTransactions))
    {
      if (this._dbTransactionsProxy == null)
        this._dbTransactionsProxy = (IDBTransactions) new CDBTransactionsProxy(this, (IDBTransactions) this._session.GetCustomService(serviceType));
      return (object) this._dbTransactionsProxy;
    }
    ICustomServicesSpeedupService servicesSpeedupService = this._clientSessionContext.CustomServicesSpeedupService;
    return servicesSpeedupService == null ? this._session.GetCustomService(serviceType) : servicesSpeedupService.GetCustomService(serviceType);
  }

  /// <summary>
  /// Проверяет работоспособность подключения к сессии сервера приложений.
  /// </summary>
  /// <exception cref="T:System.Exception">Подключение к сессии сервера приложений нарушено</exception>
  public void Test() => this._session.Test();

  /// <summary>Возвращает true, если это администратор</summary>
  public bool IsAdmin
  {
    [DebuggerStepThrough] get
    {
      if (!this._IsAdmin.HasValue)
        this._IsAdmin = new bool?(this._session.IsAdmin);
      return this._IsAdmin.Value;
    }
  }

  /// <summary>Если true, то это системная сессия</summary>
  public bool IsSystemSession
  {
    [DebuggerStepThrough] get
    {
      if (!this._IsSystemSession.HasValue)
        this._IsSystemSession = new bool?(this._session.IsSystemSession);
      return this._IsSystemSession.Value;
    }
  }

  /// <summary>Показывать персональные объекты других пользователей</summary>
  public bool ShowPersonalObjects
  {
    [DebuggerStepThrough] get
    {
      if (!this._ShowPersonalObjects.HasValue)
        this._ShowPersonalObjects = new bool?(this._session.ShowPersonalObjects);
      return this._ShowPersonalObjects.Value;
    }
    [DebuggerStepThrough] set
    {
      this._session.ShowPersonalObjects = value;
      this._ShowPersonalObjects = new bool?(value);
    }
  }

  /// <summary>Если true, то режим разработчика</summary>
  public bool DeveloperMode
  {
    [DebuggerStepThrough] get => this._session.DeveloperMode;
  }

  /// <summary>Является ли база эталонной</summary>
  public bool EtalonBase
  {
    [DebuggerStepThrough] get => this._session.EtalonBase;
  }

  /// <summary>
  /// Возвращает количество дней, оставшихся до истечения срока действия пароля. Если 0, то
  /// пароль постоянный.
  /// </summary>
  public int GetExpirationDays() => this._session.GetExpirationDays();

  /// <summary>
  /// Возвращает список плагинов, которых нужно грузить на клиента
  /// </summary>
  public DataTable GetClientPlugins() => this._session.GetClientPlugins();

  /// <summary>Интерфейс, позволяющий получить инфу о кэше сервера</summary>
  public IServerCache ServerCache
  {
    [DebuggerStepThrough] get => this._session.ServerCache;
  }

  /// <summary>Считать из клиентского кэша таблицы</summary>
  /// <param name="tableNames">Имена таблицы</param>
  /// <returns>Таблицы или null</returns>
  public DataTable[] GetCacheTables(params string[] tableNames)
  {
    DataTable[] cacheTables = new DataTable[tableNames.Length];
    if (tableNames.Length == 0)
      return (DataTable[]) null;
    for (int index = 0; index < tableNames.Length; ++index)
      cacheTables[index] = this.ClientCache.GetTable(tableNames[index]);
    return cacheTables;
  }

  /// <summary>
  /// Возвращает краткую информацию об объекте по идентификатору его версии
  /// </summary>
  public QuickObjectInfo GetObjectInfo(long objectID) => this._session.GetObjectInfo(objectID);

  /// <summary>
  /// Возвращает краткую информацию об объекте по глобальному идентификатору его версии
  /// </summary>
  public QuickObjectInfo GetObjectInfo(Guid objectGUID) => this._session.GetObjectInfo(objectGUID);

  /// <summary>
  /// Возвращает версию объекта, соответствующую текущим правилам подбора версий.
  /// </summary>
  /// <param name="id">Идентификатор объекта (IDBObject.ID)</param>
  /// <param name="rule">Правила фильтрации</param>
  /// <param name="throwNotFoundException">Если throwNotFoundException == false, то при отсутствии такого объекта возвращает null</param>
  /// <returns>Ссылка на интерфейс указанного объекта или null</returns>
  public IDBObject GetObjectByVersionsRule(long id, VersionsRule rule, bool throwNotFoundException)
  {
    return this._session.GetObjectByVersionsRule(id, rule, throwNotFoundException);
  }

  /// <summary>
  /// Возвращает версию объекта, соответствующую текущим правилам подбора версий.
  /// </summary>
  /// <param name="id">Идентификатор объекта (IDBObject.ID)</param>
  /// <param name="FiltrationSettings">Идентификатор настроек фильтрации состава</param>
  /// <param name="throwNotFoundException">Если throwNotFoundException == false, то при отсутствии такого объекта возвращает null</param>
  /// <returns>Ссылка на интерфейс указанного объекта или null</returns>
  public IDBObject GetObjectByVersionsRule(
    long id,
    string FiltrationSettings,
    bool throwNotFoundException)
  {
    return this._session.GetObjectByVersionsRule(id, FiltrationSettings, throwNotFoundException);
  }

  /// <summary>
  /// Возвращает версию объекта, соответствующую текущим правилам подбора версий.
  /// guid - GUID объекта (не версии !!!!)
  /// Если throwNotFoundException == false, то при отсутствии такого объекта возвращает null
  /// </summary>
  public IDBObject GetObjectByVersionsRule(
    Guid guid,
    string FiltrationSettings,
    bool throwNotFoundException)
  {
    return this._session.GetObjectByVersionsRule(guid, FiltrationSettings, throwNotFoundException);
  }

  /// <summary>
  /// Получить статус версии объекта согласно указанному правилу подбора версий
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="rule">Правило подбора версий</param>
  /// <returns>Статус версии объекта согласно указанному правилу подбора версий</returns>
  public ObjectFiltrationState GetObjectVersionFiltrationState(long objectID, VersionsRule rule)
  {
    return this._session.GetObjectVersionFiltrationState(objectID, rule);
  }

  /// <summary>Получить идентификатор объекта (F_ID)</summary>
  /// <param name="objectID">Идентификатор версии объекта (F_OBJECT_ID)</param>
  /// <returns>Идентификатор объекта (F_ID)</returns>
  public long GetObjectF_ID(long objectID) => this._session.GetObjectF_ID(objectID);

  public IDBObject GetObjectByID(Guid guid, bool throwNotFoundException)
  {
    return this._session.GetObjectByID(guid, throwNotFoundException);
  }

  public IDBObject GetObjectByID(long id, bool throwNotFoundException)
  {
    return this._session.GetObjectByID(id, throwNotFoundException);
  }

  /// <summary>
  /// Возвращает массив описателей единиц измерения, зарегистрированных в БД
  /// </summary>
  public MeasureDescriptor[] GetMeasuresList() => this._session.GetMeasuresList();

  /// <summary>
  /// Возвращает обработчик истории значений атрибута attributeID
  /// </summary>
  public IDBAHistoryCollection GetHistoryCollection(int attributeID)
  {
    return this._session.GetHistoryCollection(attributeID);
  }

  /// <summary>Возвращает обработчик истории значений атрибутов</summary>
  public IDBHistoryCollection GetHistoryCollection() => this._session.GetHistoryCollection();

  /// <summary>
  /// Возвращает уровень продвижения объекта номер objectID. Если объекта в базе нет, то возвращает -1.
  /// </summary>
  public int GetObjectLevel(long objectID) => this._session.GetObjectLevel(objectID);

  public bool HasMyWorkCopy(long objectID) => this._session.HasMyWorkCopy(objectID);

  /// <summary>Возвращает текущие настройки нормализатора строк</summary>
  public NormalizerSettings GetStringNormalizerSettings()
  {
    return this._session.GetStringNormalizerSettings();
  }

  public bool CheckDBVersion(string moduleName, int needVersion, bool throwVersionException)
  {
    return this._session.CheckDBVersion(moduleName, needVersion, throwVersionException);
  }

  /// <summary>
  /// Ф-ция возвращает текстовый отчет о проверках прав доступа, выполняемых в текущей сессии
  /// </summary>
  /// <param name="mode">Если mode == GetAccessModes.AllRecords, то возвращает отчет о всех проверках,
  /// выполненных за время жизни этой сессии. Если mode == GetAccessModes.LastCheck, то возвращает отчет
  /// о последней проверке.</param>
  public string[] GetCheckAccessLog(GetAccessModes mode) => this._session.GetCheckAccessLog(mode);

  /// <summary>
  /// Проверить, включен ли режим запоминания списка изменений, сделанных в БД сервером
  /// </summary>
  public bool IsStartedLogHistory
  {
    [DebuggerStepThrough] get => this._session.IsStartedLogHistory;
  }

  /// <summary>
  /// Включает режим запоминания списка изменений, сделанных в БД сервером
  /// </summary>
  public void StartLogHistory() => this._session.StartLogHistory();

  /// <summary>
  /// Отключает режим запоминания списка изменений, сделанных в БД сервером
  /// </summary>
  public void StopLogHistory() => this._session.StopLogHistory();

  /// <summary>
  /// Возвращает список изменений, сделанных в БД сервером с момента вызова ф-ции StartLogHistory
  /// </summary>
  public List<CategoryValue> GetModificationsHistoryList()
  {
    return this._session.GetModificationsHistoryList();
  }

  /// <summary>
  /// Возвращает массив изменений, сделанных в БД сервером с момента вызова ф-ции StartLogHistory
  /// </summary>
  public CategoryValue[] GetModificationsHistoryArray()
  {
    return this._session.GetModificationsHistoryArray();
  }

  /// <summary>
  /// Возвращает информацию о состоянии текущей операции, выполняемой на сервере
  /// </summary>
  [Obsolete("Do not use this method anymore", true)]
  public OperationStateInfo GetOperationInfo() => this._session.GetOperationInfo();

  /// <summary>
  /// Зафиксирован ли контекст редактирования в контексте вызова потока, в котором работает сессия
  /// </summary>
  public bool IsEditingContextFixed
  {
    [DebuggerStepThrough] get => this._session.IsEditingContextFixed;
  }

  /// <summary>
  /// Идентификатор текущего контекста редактирования. Если в контексте вызова сессии есть
  /// информация о контексте редактирования, будет возвращена именно она, т.к. имеет наивысший приоритет
  /// </summary>
  public virtual long EditingContextID
  {
    [DebuggerStepThrough] get => this._session.EditingContextID;
    [DebuggerStepThrough] set => this._session.EditingContextID = value;
  }

  /// <summary>
  /// Источник информации о текущем контексте редактирования (глобальный, оконный)
  /// </summary>
  public EditingContextSource EditingContextSource
  {
    [DebuggerStepThrough] get => this._session.EditingContextSource;
    [DebuggerStepThrough] set => this._session.EditingContextSource = value;
  }

  /// <summary>
  /// Номер группы изменений текущего контекста редактирования. Если в контексте вызова сессии есть
  /// информация о контексте редактирования, будет возвращена именно она, т.к. имеет наивысший приоритет
  /// </summary>
  public long EditingContextModificationID
  {
    [DebuggerStepThrough] get => this._session.EditingContextModificationID;
  }

  /// <summary>
  /// Режим работы текущего контекста редактирования. Если в контексте вызова сессии есть
  /// информация о контексте редактирования, будет возвращена именно она, т.к. имеет наивысший приоритет
  /// </summary>
  public virtual EditingContextMode EditingContextMode
  {
    [DebuggerStepThrough] get => this._session.EditingContextMode;
    [DebuggerStepThrough] set => this._session.EditingContextMode = value;
  }

  /// <summary>
  /// Содержимое текущего контекста редактирования.
  /// Свойство кэшируется!
  /// </summary>
  /// <param name="withDescriptions">true - загружать описания каждой версии и контекстов, иначе только содержимое контекста</param>
  public virtual EditingContextsObjectContainer GetEditingContext(bool withDescriptions)
  {
    return this._session.GetEditingContext(withDescriptions);
  }

  /// <summary>
  /// Разрешено ли использовать кэш контекстов редактирования. Рекомендуется
  /// включать кэширование перед длительными операциями, которые работают с объектами, меняя их состояние,
  /// выпуском версий, т.п. Изменение данного флага попутно очищает старый кэш контекстов редактирования
  /// </summary>
  public virtual bool EnabledEditingContextsCache
  {
    [DebuggerStepThrough] get => this._session.EnabledEditingContextsCache;
    [DebuggerStepThrough] set => this._session.EnabledEditingContextsCache = value;
  }

  /// <summary>
  /// Получить информацию о текущем контексте редактирования (включая режим его работы, номер группы изменений),
  /// привязанную к Guid мастер-сессии или другим уникальным Guid-ключам. Если в контексте вызова сессии есть
  /// информация о контексте редактирования, будет возвращена именно она, т.к. имеет наивысший приоритет
  /// </summary>
  /// <param name="key">Guid мастер-сессии или другой уникальный Guid-ключ</param>
  /// <returns>Информация о текущем контексте редактирования, режиме его работы, номеру группы изменений или null, если информации нет</returns>
  public CurrentEditingContext EditingContextGetData(Guid key)
  {
    return this._session.EditingContextGetData(key);
  }

  /// <summary>
  /// Установить или очистить информацию о текущем контексте редактирования, режиме его работы, номеру группы изменений
  /// </summary>
  /// <param name="key">Guid мастер-сессии или другой уникальный Guid-ключ</param>
  /// <param name="data">Информация о текущем контексте редактирования, режиме его работы, номеру группы изменений.
  /// Если указать значение null, информация будет удалена из коллекции у сессии</param>
  public void EditingContextSetData(Guid key, CurrentEditingContext data)
  {
    this._session.EditingContextSetData(key, data);
  }

  public long CurrentProjectID
  {
    [DebuggerStepThrough] get => this._session.CurrentProjectID;
    [DebuggerStepThrough] set => this._session.CurrentProjectID = value;
  }

  /// <summary>Фильтровать объекты, принадлежащие другим проектам</summary>
  public ProjectFiltrationModes ProjectFiltrationMode
  {
    [DebuggerStepThrough] get => this._session.ProjectFiltrationMode;
    [DebuggerStepThrough] set => this._session.ProjectFiltrationMode = value;
  }

  /// <summary>
  /// Получить список всех версий для указанного объекта (F_ID)
  /// </summary>
  /// <param name="ID">Идентификатор объекта (F_ID)</param>
  /// <returns>Cписок всех версий для указанного объекта или null</returns>
  public List<long> GetObjectVersions(long ID) => this._session.GetObjectVersions(ID);

  /// <summary>
  /// Получить список всех версий для указанного объекта (F_ID)
  /// </summary>
  /// <param name="ID">Идентификатор объекта (F_ID)</param>
  /// <param name="includeF_ID">Если указать true, то нулевым элементом в результирующий
  /// список будет добавлено значение идентификатора объекта (F_ID)</param>
  /// <returns>Cписок всех версий для указанного объекта или null</returns>
  public List<long> GetObjectVersions(long ID, bool includeF_ID)
  {
    return this._session.GetObjectVersions(ID, includeF_ID);
  }

  /// <summary>
  /// Получить список всех версий для указанной версии объекта (F_OBJECT_ID)
  /// </summary>
  /// <param name="objectID">Идентификатор любой из версий объекта (F_OBJECT_ID)</param>
  /// <returns>Cписок всех версий для указанной версии объекта или null</returns>
  public List<long> GetObjectIDVersions(long objectID)
  {
    return this._session.GetObjectIDVersions(objectID);
  }

  /// <summary>
  /// Получить список всех версий для указанной версии объекта (F_OBJECT_ID)
  /// </summary>
  /// <param name="objectID">Идентификатор любой из версий объекта (F_OBJECT_ID)</param>
  /// <param name="includeF_ID">Если указать true, то нулевым элементом в результирующий
  /// список будет добавлено значение идентификатора объекта (F_ID)</param>
  /// <returns>Cписок всех версий для указанной версии объекта или null</returns>
  public List<long> GetObjectIDVersions(long objectID, bool includeF_ID)
  {
    return this._session.GetObjectIDVersions(objectID, includeF_ID);
  }

  /// <summary>
  /// Получить версии объекта (фрагмент таблицы IMS_OBJECTS), без фильтрации по контекстам редактирования и т.п.
  /// </summary>
  /// <param name="id">Идентификатор объекта (F_ID) либо идентификатор версии объекта (в зависимости от флажка isF_ID)</param>
  /// <param name="isF_ID">false - параметр id содержит идентификатор любой версии объекта (F_OBJECT_ID),
  /// true - параметр id содержит идентификатор объекта (F_ID)</param>
  /// <param name="showBlanks">true - показывать также заготовки версий</param>
  /// <param name="showDeleted">true - показывать также удалённые версии</param>
  /// <param name="columns">Список запрашиваемых колонок. Если значение пустое, будут возвращены все колонки</param>
  /// <returns>Найденные версии объектов (фрагмент таблицы IMS_OBJECTS) либо null</returns>
  public DataTable GetAllObjectVersions(
    long id,
    bool isF_ID,
    bool showBlanks,
    bool showDeleted,
    params string[] columns)
  {
    return this._session.GetAllObjectVersions(id, isF_ID, showBlanks, showDeleted, columns);
  }

  /// <summary>
  /// Получить список версий объекта, без фильтрации по контекстам редактирования и т.п.
  /// </summary>
  /// <param name="id">Идентификатор объекта (F_ID) либо идентификатор версии объекта (в зависимости от флажка isF_ID)</param>
  /// <param name="isF_ID">false - параметр id содержит идентификатор любой версии объекта (F_OBJECT_ID),
  /// true - параметр id содержит идентификатор объекта (F_ID)</param>
  /// <param name="showBlanks">true - показывать также заготовки версий</param>
  /// <param name="showDeleted">true - показывать также удалённые версии</param>
  /// <returns>Список версий объекта или пустой список</returns>
  public List<long> GetAllObjectVersionsList(
    long id,
    bool isF_ID,
    bool showBlanks,
    bool showDeleted)
  {
    return this._session.GetAllObjectVersionsList(id, isF_ID, showBlanks, showDeleted);
  }

  /// <summary>
  /// Метод возвращает интерфейс для работы с уведомляющими выборками данного пользователя
  /// </summary>
  /// <returns>Обработчик уведомляющих выборок пользователя</returns>
  public INotifySamplesProcessor GetNotifySamplesProcessor()
  {
    return this._session.GetNotifySamplesProcessor();
  }

  /// <summary>
  /// Возвращает интерфейс для проверки прав доступа к системе
  /// </summary>
  public IDBSecurity GetSystemSecurity() => this._session.GetSystemSecurity();

  /// <summary>Устанавливаем культуру и возвращаем её</summary>
  [Obsolete("This method is deprecated", true)]
  public void GetCulture(string clientCulture) => this._session.GetCulture(clientCulture);

  /// <summary>
  /// Начато ли кэширование обработчиков объектов (IDBObject)
  /// </summary>
  public bool DBObjectsCacheStarted
  {
    [DebuggerStepThrough] get => this._session.DBObjectsCacheStarted;
  }

  /// <summary>Начать кэширование обработчиков объектов (IDBObject)</summary>
  public void DBObjectsCacheStart() => this._session.DBObjectsCacheStart();

  /// <summary>
  /// Завершить кэширование обработчиков объектов (IDBObject)
  /// </summary>
  public void DBObjectsCacheStop() => this._session.DBObjectsCacheStop();

  /// <summary>Очистить кэш обработчиков объектов (IDBObject)</summary>
  public void DBObjectsCacheClear() => this._session.DBObjectsCacheClear();

  /// <summary>
  /// Удалить из кэша обработчиков объектов (IDBObject) объект с указанным идентификатором версии
  /// </summary>
  /// <param name="fObjectID">Идентификатор версии объекта, обработчик которой надо удалить из кэша</param>
  public void DBObjectsCacheRemoveVersion(long fObjectID)
  {
    this._session.DBObjectsCacheRemoveVersion(fObjectID);
  }

  public IDBLanguageType DefaultLanguage
  {
    [DebuggerStepThrough] get
    {
      DataRow[] dataRowArray = this.ClientCache.GetTable("IMS_LANGUAGES").Select("F_DEFAULT = 1");
      return dataRowArray.Length == 0 ? (IDBLanguageType) null : this.GetLanguage(Convert.ToString(dataRowArray[0]["F_LANGUAGE_ID"]));
    }
  }

  /// <summary>
  /// Свойство (только для записи), позволяющее выполнять замену пароля у пользователя,
  /// выполняющего подключение к системе. Если изменение пароля пользователю запрещено,
  /// будет сгенерировано исключение
  /// </summary>
  public PswPackage NewPassword
  {
    [DebuggerStepThrough] set => this._session.NewPassword = value;
  }

  /// <summary>
  /// Ф-ция возвращает информацию из журнала событий о двух последних логинах текущего пользователя с компьютера, имя которого записано в данной сессии.
  /// </summary>
  public UserLoginEvents GetUserLoginEvents() => this._session.GetUserLoginEvents();

  /// <summary>
  /// Метод возвращает список объектов с информацией о пользователях, обязанности которых в данный момент может исполнять пользователь actingUserID.
  /// </summary>
  /// <param name="actingUserID">Ид. юзера, для которого нужно получить инфу о возможном исполнении обязанностей.</param>
  /// <returns>Если ничьи обязанности исполнять не может, то список пустой.</returns>
  public List<ActingUserLoginSettings> GetActingUserLoginSettings(long actingUserID)
  {
    return this._session.GetActingUserLoginSettings(actingUserID);
  }

  public bool EnableEditOwnSelections
  {
    [DebuggerStepThrough] get
    {
      if (!this._EnableEditOwnSelections.HasValue)
        this._EnableEditOwnSelections = new bool?(this._session.EnableEditOwnSelections);
      return this._EnableEditOwnSelections.Value;
    }
    [DebuggerStepThrough] set
    {
      this._session.EnableEditOwnSelections = value;
      this._EnableEditOwnSelections = new bool?(value);
    }
  }

  /// <summary>
  /// Режим, при включении которого разрешается работа конфигуратора составов
  /// </summary>
  public bool EnabledPdmConfigurator
  {
    [DebuggerStepThrough] get
    {
      if (!this._EnabledPdmConfigurator.HasValue)
        this._EnabledPdmConfigurator = new bool?(this._session.EnabledPdmConfigurator);
      return this._EnabledPdmConfigurator.Value;
    }
    [DebuggerStepThrough] set
    {
      this._session.EnabledPdmConfigurator = value;
      this._EnabledPdmConfigurator = new bool?(value);
    }
  }

  /// <summary>
  /// Режим, при включении которого разрешается подбор версий по сериям/датам
  /// </summary>
  public bool EnabledSeriesDates
  {
    [DebuggerStepThrough] get
    {
      if (!this._EnabledSeriesDates.HasValue)
        this._EnabledSeriesDates = new bool?(this._session.EnabledSeriesDates);
      return this._EnabledSeriesDates.Value;
    }
    [DebuggerStepThrough] set
    {
      this._session.EnabledSeriesDates = value;
      this._EnabledSeriesDates = new bool?(value);
    }
  }

  /// <summary>
  /// Режим Автоматическая мягкая конкретизация создаваемых связей
  /// </summary>
  public bool EnabledAutoSoftInstantiation
  {
    [DebuggerStepThrough] get
    {
      if (!this._EnabledAutoSoftInstantiation.HasValue)
        this._EnabledAutoSoftInstantiation = new bool?(this._session.EnabledAutoSoftInstantiation);
      return this._EnabledAutoSoftInstantiation.Value;
    }
    [DebuggerStepThrough] set
    {
      this._session.EnabledAutoSoftInstantiation = value;
      this._EnabledAutoSoftInstantiation = new bool?(value);
    }
  }

  /// <summary>
  /// Режим аннулирования всех версий объекта при аннулировании одной версии
  /// </summary>
  public bool AllVersionsAnnulmentMode
  {
    [DebuggerStepThrough] get
    {
      if (!this._AllVersionsAnnulmentMode.HasValue)
        this._AllVersionsAnnulmentMode = new bool?(this._session.AllVersionsAnnulmentMode);
      return this._AllVersionsAnnulmentMode.Value;
    }
    [DebuggerStepThrough] set
    {
      this._session.AllVersionsAnnulmentMode = value;
      this._AllVersionsAnnulmentMode = new bool?(value);
    }
  }

  /// <summary>
  /// Разрешена ли передача значений атрибутов в службу автоматических уведомлений
  /// </summary>
  public bool SendAttrs2DelayedNotificationMode
  {
    [DebuggerStepThrough] get => this._session.SendAttrs2DelayedNotificationMode;
    [DebuggerStepThrough] set => this._session.SendAttrs2DelayedNotificationMode = value;
  }

  /// <summary>Режим Отложенная запись истории значений атрибутов</summary>
  public bool IsDelayedAttrHistory
  {
    [DebuggerStepThrough] get => this._session.IsDelayedAttrHistory;
    [DebuggerStepThrough] set => this._session.IsDelayedAttrHistory = value;
  }

  /// <summary>Режим Отложенная запись событий в журнал аудита</summary>
  public bool IsDelayedEventlog
  {
    [DebuggerStepThrough] get => this._session.IsDelayedEventlog;
    [DebuggerStepThrough] set => this._session.IsDelayedEventlog = value;
  }

  /// <summary>
  /// Режим, при включении которого разрешается фильтрация списков и составов объектов по атрибуту "Видимость"
  /// </summary>
  public bool EnabledVisibilityFiltration
  {
    [DebuggerStepThrough] get => this._session.EnabledVisibilityFiltration;
    [DebuggerStepThrough] set => this._session.EnabledVisibilityFiltration = value;
  }

  /// <summary>
  /// Максимальное количество одновременно работающих фоновых потоков,
  /// которое может использоваться распараллеливаемыми заданиями
  /// </summary>
  public int MaxTaskThreadsCount
  {
    [DebuggerStepThrough] get => this._session.MaxTaskThreadsCount;
    [DebuggerStepThrough] set => this._session.MaxTaskThreadsCount = value;
  }

  /// <summary>версия алгоритма подписания объектов</summary>
  public int AlgorithmVersion
  {
    [DebuggerStepThrough] get => this._session.AlgorithmVersion;
  }

  /// <summary>Поколение метаданных для текущего сервера приложений</summary>
  public long MetaDataGeneration
  {
    [DebuggerStepThrough] get => this._session.MetaDataGeneration;
  }

  /// <summary>
  /// Возвращает ид. объекта IDBObject.ID по ид. его версии IDBObject.ObjectID.
  /// Если такой версии объекта нет, то генерит исключение ObjectNotFoundException
  /// </summary>
  public long GetIDByObjectID(long objectID) => this._session.GetIDByObjectID(objectID);

  public object GetSessionPluginsData(object key) => this._session.GetSessionPluginsData(key);

  /// <summary>
  /// Записывает в сессию информацию модуля расширения. Следует учитывать, что записанная этим методом информация копируется при клонировании сессии.
  /// Чтобы избежать копирования, записываемый объект должен реализовывать интерфейс ISessionInstanceData.
  /// </summary>
  /// <param name="key">Ключ</param>
  /// <param name="value">Значение</param>
  public void SetSessionPluginsData(object key, object value)
  {
    this._session.SetSessionPluginsData(key, value);
  }

  public void RemoveSessionPluginsData(object key) => this._session.RemoveSessionPluginsData(key);

  /// <summary>
  /// Включает для сессии и всех ее объектов защиту от использования вне SessionKeeper. После выхода сессии за пределы SessionKeeper любые обращения к ней или
  /// ее объектам будут приводить к возникновению исключения. Выключить режим защиты нельзя. По умолчанию, режим защиты выключен.
  /// </summary>
  public void ActivateSessionGuard()
  {
    this._session.ActivateSessionGuard();
    this._isSessionGuardActive = new bool?(true);
  }

  /// <summary>
  /// Возвращает true, если для сессии и всех ее объектов активирована защита от использования вне SessionKeeper.
  /// </summary>
  public bool IsSessionGuardActive
  {
    [DebuggerStepThrough] get
    {
      if (!this._isSessionGuardActive.HasValue)
        this._isSessionGuardActive = new bool?(this._session.IsSessionGuardActive);
      return this._isSessionGuardActive.Value;
    }
  }

  public void ClearObjectSmartCache()
  {
  }

  /// <summary>
  /// Массив идентификаторов групп, в которые входит данный пользователь, а также его текущей роли и самого пользователя
  /// </summary>
  /// <returns>массив ObjectID</returns>
  public long[] GetUserGroupsAndRoleID() => this._session.GetUserGroupsAndRoleID();

  public IDBSecurity GetAttributeLCSecurity(int attributeID, int lcStepID, int objectTypeID)
  {
    return this._session.GetAttributeLCSecurity(attributeID, lcStepID, objectTypeID);
  }

  /// <summary>
  /// Проверяет возможность выполнения обратных вызовов от сервера приложений к клиенту.
  /// Метод используется для контроля работоспособности спонсоров Remoting.
  /// </summary>
  /// <param name="testObject">Клиентский объект, используемый для проверки</param>
  /// <exception cref="T:Intermech.KernelException">Обратные вызовы невозможны</exception>
  public void CheckClientBackwardConnectivity(IMClientLiveStatus testObject)
  {
    this._session.CheckClientBackwardConnectivity(testObject);
  }

  public DataTable GetObjectVersionsTree(long id) => this._session.GetObjectVersionsTree(id);

  public UserAndRoleInfo GetUserAndRoleInfo() => this._session.GetUserAndRoleInfo();

  /// <summary>
  /// Возвращает информацию о ролях и уровнях доступа юзера по его логину
  /// </summary>
  /// <param name="loginName">логин юзера</param>
  /// <returns></returns>
  public LoginInformation GetLoginInformation(string loginName)
  {
    return this._session.GetLoginInformation(loginName);
  }

  /// <summary>Возвращает обработчик атрибута для объекта</summary>
  /// <param name="objectID">Ид. версии объекта</param>
  /// <param name="attributeID">Ид. атрибута (локальный ид., глобальный ид. или наименование)</param>
  /// <param name="failIfNotFound">Сгенерить эксепшен если чего-то не нашлось</param>
  /// <param name="getActualCopy">Получить указанный объект или его актуальную копию</param>
  /// <returns>Обработчик атрибута</returns>
  public IDBAttribute GetObjectAttribute(
    long objectID,
    object attributeID,
    bool failIfNotFound,
    bool getActualCopy)
  {
    return this._session.GetObjectAttribute(objectID, attributeID, failIfNotFound, getActualCopy);
  }

  /// <summary>Возвращает обработчик атрибута для объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeID">Ид. атрибута</param>
  /// <returns>Обработчик атрибута или null, если что-то пошло не так</returns>
  public IDBAttribute GetObjectAttributeByID(long objectID, int attributeID)
  {
    return this.GetObjectAttribute(objectID, (object) attributeID, false, false);
  }

  /// <summary>Возвращает обработчик атрибута для объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeGUID">Глобальный ид. атрибута</param>
  /// <returns>Обработчик атрибута или null, если что-то пошло не так</returns>
  public IDBAttribute GetObjectAttributeByGuid(long objectID, Guid attributeGUID)
  {
    return this.GetObjectAttribute(objectID, (object) attributeGUID, false, false);
  }

  /// <summary>Возвращает массив значений атрибутов объекта</summary>
  /// <param name="objectID">Ид. версии объекта</param>
  /// <param name="modes">Флаги управления</param>
  /// <param name="failIfNotFound">Сгенерить эксепшен если чего-то не нашлось</param>
  /// <param name="getActualCopy">Получить указанный объект или его актуальную копию</param>
  /// <returns>Массив значений (пустой, если объекта нет)</returns>
  public AttributeValues[] GetObjectAttributesValues(
    long objectID,
    GetAttributeValuesModes modes,
    bool failIfNotFound,
    bool getActualCopy)
  {
    return this._session.GetObjectAttributesValues(objectID, modes, failIfNotFound, getActualCopy);
  }

  /// <summary>Получить таблицу со списком объектов</summary>
  /// <param name="objectTypeGuid">Гуид типа объектов</param>
  /// <param name="dbRecordSetParams">Параметры запроса</param>
  /// <returns>Таблица с объектами</returns>
  public DataTable ObjectsSelect(Guid objectTypeGuid, DBRecordSetParams dbRecordSetParams)
  {
    return this._session.ObjectsSelect(objectTypeGuid, dbRecordSetParams);
  }

  /// <summary>Получить таблицу со списком объектов</summary>
  /// <param name="objectTypeID">ид типа объектов</param>
  /// <param name="dbRecordSetParams">Параметры запроса</param>
  /// <returns>Таблица с объектами</returns>
  public DataTable ObjectsSelect(int objectTypeID, DBRecordSetParams dbRecordSetParams)
  {
    return this._session.ObjectsSelect(objectTypeID, dbRecordSetParams);
  }

  /// <summary>Возвращает обработчик атрибута для связи</summary>
  /// <param name="relationID">Ид. связи</param>
  /// <param name="attributeID">Ид. атрибута (локальный ид., глобальный ид. или наименование)</param>
  /// <param name="failIfNotFound">Сгенерить эксепшен если чего-то не нашлось</param>
  /// <returns>Обработчик атрибута</returns>
  public IDBAttribute GetRelationAttribute(
    long relationID,
    object attributeID,
    bool failIfNotFound)
  {
    return this._session.GetRelationAttribute(relationID, attributeID, failIfNotFound);
  }

  /// <summary>Возвращает обработчик атрибута для связи</summary>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="attributeID">Ид. атрибута</param>
  /// <returns>Обработчик атрибута или null, если что-то пошло не так</returns>
  public IDBAttribute GetRelationAttributeByID(long relationID, int attributeID)
  {
    return this.GetRelationAttribute(relationID, (object) attributeID, false);
  }

  /// <summary>Возвращает обработчик атрибута для связи</summary>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="attributeGUID">Глобальный ид. атрибута</param>
  /// <returns>Обработчик атрибута или null, если что-то пошло не так</returns>
  public IDBAttribute GetRelationAttributeByGuid(long relationID, Guid attributeGUID)
  {
    return this.GetRelationAttribute(relationID, (object) attributeGUID, false);
  }

  /// <summary>Возвращает массив значений атрибутов связи</summary>
  /// <param name="relationID">Ид. связи</param>
  /// <param name="modes">Флаги управления</param>
  /// <param name="failIfNotFound">Сгенерить эксепшен если чего-то не нашлось</param>
  /// <returns>Массив значений (пустой, если связи нет)</returns>
  public AttributeValues[] GetRelationAttributesValues(
    long relationID,
    GetAttributeValuesModes modes,
    bool failIfNotFound)
  {
    return this._session.GetRelationAttributesValues(relationID, modes, failIfNotFound);
  }

  /// <summary>Получить таблицу со списком связей</summary>
  /// <param name="relationTypeID">Ид. типа связей</param>
  /// <param name="dbRecordSetParams">Параметры запроса</param>
  /// <returns>Таблица связей</returns>
  public DataTable RelationsSelect(int relationTypeID, DBRecordSetParams dbRecordSetParams)
  {
    return this._session.RelationsSelect(relationTypeID, dbRecordSetParams);
  }

  /// <summary>Получить системные свойства объекта</summary>
  /// <param name="objectID">Ид. версии объекта</param>
  /// <param name="failIfNotFound">Сбрасывать эксепшен если объект не найден</param>
  /// <param name="getActualCopy">Получать ли акутальную копию объекта</param>
  /// <returns></returns>
  public ObjectSystemProperties GetObjectSystemProperties(
    long objectID,
    bool failIfNotFound,
    bool getActualCopy)
  {
    return this._session.GetObjectSystemProperties(objectID, failIfNotFound, getActualCopy);
  }

  /// <summary>Получить системные свойства объекта</summary>
  /// <param name="objectGuid">Гуид версии объекта</param>
  /// <param name="failIfNotFound">Сбрасывать эксепшен если объект не найден</param>
  /// <returns></returns>
  public ObjectSystemProperties GetObjectSystemProperties(Guid objectGuid, bool failIfNotFound)
  {
    return this._session.GetObjectSystemProperties(objectGuid, failIfNotFound);
  }

  public ObjectSystemPropertiesEx GetObjectSystemPropertiesEx(long objectID, bool failIfNotFound)
  {
    return this._session.GetObjectSystemPropertiesEx(objectID, failIfNotFound);
  }

  public ObjectSystemPropertiesEx GetObjectSystemPropertiesEx(Guid objectGuid, bool failIfNotFound)
  {
    return this._session.GetObjectSystemPropertiesEx(objectGuid, failIfNotFound);
  }

  /// <summary>Возвращает значения атрибута для объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeGUID">Глобальный ид. атрибута</param>
  /// <returns>Массив значений атрибута или null, если что-то не нашлось</returns>
  public object[] GetObjectAttributeValuesByGuid(long objectID, Guid attributeGUID)
  {
    return this._session.GetObjectAttributeValuesByGuid(objectID, attributeGUID);
  }

  /// <summary>Возвращает первое значение атрибута для объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="attributeGUID">Глобальный ид. атрибута</param>
  /// <returns>Первое значение атрибута или null, если что-то не нашлось</returns>
  public object GetObjectAttributeValueByGuid(long objectID, Guid attributeGUID)
  {
    return this._session.GetObjectAttributeValueByGuid(objectID, attributeGUID);
  }

  /// <summary>
  /// Добавляет атрибут номер attributeID к объекту objectID и инициализирует его значениями initValues.
  /// </summary>
  /// <param name="objectID">Ид. версии объекта</param>
  /// <param name="attributeID">Ид. атрибута</param>
  /// <param name="failIfNotFound">Генерировать ли исключение если объекта нет</param>
  /// <param name="failIfExists">Если failIfExists==true и атрибут уже существует, то генерируется исключение. Иначе присваивает атрибуту значения nitValues</param>
  /// <param name="initValues">Значения, которыми нужно проинициализировать атрибут</param>
  /// <returns>Возвращает обработчик добавленного атрибута либо null</returns>
  public IDBAttribute AddObjectAttribute(
    long objectID,
    int attributeID,
    bool failIfNotFound,
    bool failIfExists,
    object[] initValues)
  {
    return this._session.AddObjectAttribute(objectID, attributeID, failIfNotFound, failIfExists, initValues);
  }

  /// <summary>
  /// Добавляет атрибут номер attributeID к связи relationID и инициализирует его значениями initValues.
  /// </summary>
  /// <param name="relationID">Ид. связи</param>
  /// <param name="attributeID">Ид. атрибута</param>
  /// <param name="failIfNotFound">Генерировать ли исключение если связи нет</param>
  /// <param name="failIfExists">Если failIfExists==true и атрибут уже существует, то генерируется исключение. Иначе присваивает атрибуту значения nitValues</param>
  /// <param name="initValues">Значения, которыми нужно проинициализировать атрибут</param>
  /// <returns>Возвращает обработчик добавленного атрибута либо null</returns>
  public IDBAttribute AddRelationAttribute(
    long relationID,
    int attributeID,
    bool failIfNotFound,
    bool failIfExists,
    object[] initValues)
  {
    return this._session.AddRelationAttribute(relationID, attributeID, failIfNotFound, failIfExists, initValues);
  }

  /// <summary>
  /// Присваивает объекту objectID атрибуты attributeValues
  /// </summary>
  /// <param name="objectID">Ид. версии объекта</param>
  /// <param name="failIfNotFound">Генерировать ли исключение если объекта нет</param>
  /// <param name="attributeValues">Набор атрибутов и их значений, которые нужно присвоить объекту. Другие атрибуты у объекта не удаляются.</param>
  public void SetObjectAttributesValues(
    long objectID,
    bool failIfNotFound,
    AttributeValues[] attributeValues)
  {
    this._session.SetObjectAttributesValues(objectID, failIfNotFound, attributeValues);
  }

  /// <summary>
  /// Присваивает связи relationID атрибуты attributeValues
  /// </summary>
  /// <param name="relationID">Ид. связи</param>
  /// <param name="failIfNotFound">Генерировать ли исключение если связи нет</param>
  /// <param name="attributeValues">Набор атрибутов и их значений, которые нужно присвоить связи. Другие атрибуты у связи не удаляются.</param>
  public void SetRelationAttributesValues(
    long relationID,
    bool failIfNotFound,
    AttributeValues[] attributeValues)
  {
    this._session.SetRelationAttributesValues(relationID, failIfNotFound, attributeValues);
  }

  public IDBRelation[] GetRelations(long[] relationIDs, bool failIfNotFound)
  {
    return this._session.GetRelations(relationIDs, failIfNotFound);
  }

  public long CheckOutCommand(long objectID) => this._session.CheckOutCommand(objectID);

  public long CheckInCommand(long objectID, bool preserveWorkingCopies)
  {
    return this._session.CheckInCommand(objectID, preserveWorkingCopies);
  }

  public AttributeValues[] GetObjectAttributesValues(
    long objectID,
    int[] attributesID,
    GetAttributeValuesModes modes,
    bool failIfNotFound)
  {
    return this._session.GetObjectAttributesValues(objectID, attributesID, modes, failIfNotFound);
  }

  public AttributeValues[] GetRelationAttributesValues(
    long relationID,
    int[] attributesID,
    GetAttributeValuesModes modes,
    bool failIfNotFound)
  {
    return this._session.GetRelationAttributesValues(relationID, attributesID, modes, failIfNotFound);
  }

  public void SetClientAccessLevel(int clientAccessLevel, string machineName)
  {
    this._session.SetClientAccessLevel(clientAccessLevel, machineName);
  }

  /// <summary>
  /// Указывает системе на начало процесса удаления указанных объектов
  /// </summary>
  /// <param name="objectIDs">Список ObjectID удаляемых объектов</param>
  public void BeginDeleteObjects(IEnumerable<long> objectIDs)
  {
    this._session.BeginDeleteObjects(objectIDs);
  }

  /// <summary>
  /// Указывает системе на завершение процесса удаления объектов
  /// </summary>
  public void EndDeleteObjects() => this._session.EndDeleteObjects();
}
