// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSession
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Data;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.NotifySamples;
using Intermech.Interfaces.Projects;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.DelayedNotifications;
using Intermech.Interfaces.Server.GlobalIndex;
using Intermech.Interfaces.Snapshots;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Attributes;
using Intermech.Kernel.LifeCycles;
using Intermech.Kernel.NotifySamples;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Kernel.Snapshots;
using Intermech.Pools;
using Intermech.Protection;
using Intermech.Text;
using Intermech.Threading;
using Intermech.Workflow;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;


namespace Intermech.Kernel;

public sealed class UserSession : 
  LongLifeObject,
  IReliableServerObject,
  IUserSession,
  IServerSession,
  IUserSessionCacheDataSet,
  IDBTransactions
{
  private static bool _endbleEditOwnSelections;
  private static bool _initedEndbleEditOwnSelections = false;
  private static bool _enabledPdmConfigurator;
  private static bool _initedEnabledPdmConfigurator = false;
  private static bool _enabledSeriesDates;
  private static bool _initedEnabledSeriesDates = false;
  private static bool _enabledVisibilityFiltration;
  private static bool _initedVisibilityFiltration = false;
  private static bool _enabledAutoSoftInstantiation = false;
  private static bool _initedAutoSoftInstantiation = false;
  private static int _maxTaskThreadsCount = 4;
  private static bool _initedMaxTaskThreadsCount = false;
  private const string _moduleName = "IPS.Kernel";
  private const string _sectionName = "UserSession";
  private const string _sequentiallyDesigningItem = "SequentiallyDesigning";
  private AtomicInt64 _UserID = new AtomicInt64(0L);
  private AtomicRef<string> _UserName = new AtomicRef<string>(string.Empty);
  private long _ID;
  private Guid _UserGUID = Guid.Empty;
  private long _clientConnectionID;
  private string _actingUserName = string.Empty;
  private AtomicRef<string> _ComputerName = new AtomicRef<string>(string.Empty);
  private AtomicDateTime _LastCallTime = new AtomicDateTime(DateTime.UtcNow);
  private string _AreaID = string.Empty;
  private string _AreaSQL = string.Empty;
  private string _Languages = string.Empty;
  private string _LanguageSQL = string.Empty;
  private int _MaxRows;
  private IEventLog _EventLog;
  private IEventLog _EventLogArchive;
  private IDBConfigurations _DBConfigurations;
  private IDbManager _dbManager;
  private AtomicRef<DBSecurity> _DBSecurity = new AtomicRef<DBSecurity>((DBSecurity) null);
  private IIDHelper _IDHelper;
  private ICacheDataset _DBCache;
  private IGlobalIndexService _GlobalIndexService;
  private int _SessionID;
  private AtomicInt32 _SessionStatusCode = new AtomicInt32(0);
  private readonly Guid _SessionGUID = Guid.NewGuid();
  private long _LoginEventID;
  private TimeSpan _TimeZoneOffset = TimeSpan.Zero;
  private long _RoleID;
  private List<string> _ChangedCacheTables;
  private ArrayList disposableObjects = new ArrayList();
  private bool _DeveloperMode;
  private bool _ShowDeletedObjects;
  private bool _ShowPersonalObjects;
  private IDBRelationsApplicabilityCollection _RelationsApplicabilityCollection;
  private AtomicInt64 _ActiveStorageID = new AtomicInt64(0L);
  private SqlBuilder _QueryBuilder;
  private IStringNormalizer _StringNormalizer;
  private PswPackage _password;
  private string _loginName;
  private AtomicBoolean _AllowSystemLogin = new AtomicBoolean(false);
  private AtomicBoolean _IsPermanent = new AtomicBoolean(false);
  private bool _IsSystemSession;
  private IServerBriefcase _ServerBriefcase;
  private readonly bool _LoggingOn = true;
  private int _PasswordExpiredDays;
  private static UserSessionCollection _Sessions = new UserSessionCollection();
  private static UserSessionClientConnectionService _ClientConnections = new UserSessionClientConnectionService();
  private AtomicRef<UserSession> _ParentSession = new AtomicRef<UserSession>((UserSession) null);
  private AtomicRef<NotifySamplesProcessor> _NSProcessor = new AtomicRef<NotifySamplesProcessor>((NotifySamplesProcessor) null);
  private IServerCache _ServerCache;
  private UserSessionSharedData _SData;
  private UserSessionCallCounter _CallCounter = new UserSessionCallCounter();
  private AtomicRef<ThreadedAccessWrapper> _ThreadedAccessWrapper;
  private IDBAttributeTypeService _attrType_srv;
  private Dictionary<int, IDBAttributeType> _attrType_Cache;
  private DateTime _attType_CacheDate;
  private HybridDictionary _pluginsData = new HybridDictionary();
  private Dictionary<long, IDBObject> _dbObjectsCache;
  private object _dbObjectsCacheSyncRoot = new object();
  private int _dbObjectsCacheSyncCount;
  private static IDBEditingContextsServerService _editingContextsService;
  private List<CategoryValue> _CreationLog = new List<CategoryValue>();
  private bool _CreationLogMode;
  private bool _SuspendCreationLogMode;
  private readonly List<string> _LogList = new List<string>(300);
  private List<CategoryValue> _ModificationsHistory = new List<CategoryValue>();
  private bool _LogHistory;
  private int _ModificationsHistoryCheckPoint;
  private readonly OperationStateInfo _EmptyOperationInfo = new OperationStateInfo(string.Empty);
  private int _SecurityLevel;
  public IEventLogHelper EventLogHelper;
  private IObligatoryObjectsService _obligatoryObjects;
  private bool _RollbackOff;
  private IDbManagerTransactionState _OldTransactionState;
  private long _actingUserID;
  private static IMServerLoginMode _LoginMode = UserSession.GetLoginModeFromAppConfig();
  private AtomicBoolean _AllowLoginWithoutPassword = new AtomicBoolean(false);
  private List<ActingUserLoginSettings> _LastActingUserLoginSettings;
  private static bool _SubGroupsSecurity = UserSession.GetSubGroupsSecurityFromAppConfig();
  private DelayedUpdaterService _DelayedUpdater;
  private List<DBObject> _CommitCreationObjects = new List<DBObject>();
  private ConcurrentDictionary<CategoryValue, AccessInfo> _AccessCache;
  private readonly SessionStoragesList _StoragesList;
  private string _SessionName = string.Empty;
  private RemotingOperationCancellationHandler _currentOperationCancellationHandler;
  private static int _DoubleLoginTimeout = UserSession.GetDoubleLoginTimeoutFromAppConfig();
  private bool isSessionGuardActive;
  private UserSessionIDGuard sessionIdGuard;
  public const int DBVersion = 710;
  private bool _ShowDeletedObjectsInitialized;
  private bool _ShowPersonalObjectsInitialized;
  private bool _DeveloperModeInitialized;
  private bool _EtalonBaseInitialized;
  private bool _EtalonBase;
  private long _RoleID_ID;
  private static ConcurrentDictionary<long, int> WrongPasswordsDict = new ConcurrentDictionary<long, int>();
  private static volatile int _UserLockedAttributeID = 0;
  private bool _AlreadyInCommit;
  private IKernelCacheSynchronizer _CacheSynchronizer;
  private DateTime _LastGetObjectTime = DateTime.UtcNow;
  private IDBObject _LastGetObject;
  private IDBObjectService _ObjectsService;
  private Dictionary<int, IDBObjectType> _ObjectTypesDict = new Dictionary<int, IDBObjectType>();
  private IDBRelationService _RelationsService;
  private Dictionary<int, IDBRelationType> _RelationTypesDict = new Dictionary<int, IDBRelationType>();
  internal const string MustCloseByKernelName = "MustCloseByKernelName";
  public const int UserStorageEmpty = 0;
  private long _UserStorageID;
  private int _GetRolesListDelay;
  private static DataTable SpecialsPluginsTable = (DataTable) null;
  private CategoryValue _LastModificationValue = new CategoryValue(0, 0L, ActionType.Any);
  private volatile bool enabledEditingContextsCache;
  private PswPackage _newPassword = new PswPackage();
  private static bool _initedDelayedEventlog = false;
  private static bool _IsDelayedEventlog = false;
  private List<EventlogProperties> eventlogList = new List<EventlogProperties>();
  private static bool _initedDelayedAttrHistory = false;
  private static bool _IsDelayedAttrHistory = false;
  private List<AttrHistoryProperties> attrHistoryList;
  private List<IndexQueueProperties> attrIndexQueue = new List<IndexQueueProperties>();
  private List<long> autoSnapshotsList = new List<long>();
  private List<DelayedNotification> _DelayedNotificationsList = new List<DelayedNotification>();
  private RemovableObjects _RemovableObjects;
  private int _ClientAccessLevel = 2147483646;

  public UserSession()
  {
    this._SData = new UserSessionSharedData();
    this._currentOperationCancellationHandler = new RemotingOperationCancellationHandler(new Action(this.LogoutIfSessionIsLost));
    this.EventLogHelper = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this._obligatoryObjects = ServerServices.GetService(typeof (IObligatoryObjectsService)) as IObligatoryObjectsService;
    this._attrType_srv = ServerServices.GetService(typeof (IDBAttributeTypeService)) as IDBAttributeTypeService;
    this._attrType_Cache = new Dictionary<int, IDBAttributeType>();
    this._attType_CacheDate = this.DBCache.ModifyDate;
    this.InitAttrHistory();
    this._StoragesList = new SessionStoragesList(this);
    this._SessionID = UserSession._Sessions.CreateSessionID();
    UserSession._Sessions.AddSession(this);
  }

  internal List<string> LogList
  {
    [DebuggerStepThrough] get => this._LogList;
  }

  private static int GetDoubleLoginTimeoutFromAppConfig()
  {
    int result = 0;
    string s = ConfigurationManager.AppSettings.Get("DoubleLoginTimeout");
    if (s != null && !int.TryParse(s, out result))
      result = 0;
    return result;
  }

  private static IMServerLoginMode GetLoginModeFromAppConfig()
  {
    string str = ConfigurationManager.AppSettings.Get("WindowsLogin");
    switch (str)
    {
      case null:
        return IMServerLoginMode.Normal;
      case "1":
        return IMServerLoginMode.WindowsLogin;
      default:
        if (!(str.ToLower() == "true"))
        {
          if (str.ToLower() == "sid" || str.ToLower() == "domain")
            return IMServerLoginMode.DomainLogin;
          if (str.ToLower() == "domain_only")
            return IMServerLoginMode.DomainOnlyLogin;
          goto case null;
        }
        goto case "1";
    }
  }

  private static bool GetSubGroupsSecurityFromAppConfig()
  {
    switch (ConfigurationManager.AppSettings.Get("SubGroupsSecurity"))
    {
      case "1":
        return true;
      default:
        return false;
    }
  }

  public static IUserSessionCollection Sessions => (IUserSessionCollection) UserSession._Sessions;

  public static IUserSessionClientConnections ClientConnections
  {
    get => (IUserSessionClientConnections) UserSession._ClientConnections;
  }

  public void ActivateSessionGuard()
  {
    if (this.isSessionGuardActive)
      return;
    try
    {
      this.sessionIdGuard = new UserSessionIDGuard(this);
      this.isSessionGuardActive = true;
    }
    catch
    {
      this.sessionIdGuard = (UserSessionIDGuard) null;
      this.isSessionGuardActive = false;
      throw;
    }
  }

  public bool IsSessionGuardActive => this.isSessionGuardActive;

  private void ValidateClientCall()
  {
    if (!this.isSessionGuardActive)
      return;
    this.sessionIdGuard.ValidateCall();
  }

  internal ConcurrentDictionary<CategoryValue, AccessInfo> AccessCache
  {
    get
    {
      if (this.ParentSession != null)
        return this.ParentSession.AccessCache;
      if (this._AccessCache == null)
        this._AccessCache = new ConcurrentDictionary<CategoryValue, AccessInfo>();
      return this._AccessCache;
    }
  }

  internal DelayedUpdaterService DelayedUpdater
  {
    get
    {
      if (this._DelayedUpdater == null)
        this._DelayedUpdater = ServerServices.GetService(typeof (IDelayedUpdaterService)) as DelayedUpdaterService;
      return this._DelayedUpdater;
    }
  }

  public static IMServerLoginMode LoginMode => UserSession._LoginMode;

  public static bool SubGroupsSecurity => UserSession._SubGroupsSecurity;

  public bool IsSystemSession
  {
    [DebuggerStepThrough] get => this._IsSystemSession;
  }

  public bool ShowDeletedObjects
  {
    [DebuggerStepThrough] get
    {
      if (!this._ShowDeletedObjectsInitialized)
      {
        if (!this.IsSystemSession)
          this._ShowDeletedObjects = this.DBSecurity.IsAdminMode && this.Configurations.ReadBool("KERNEL", "SECURITY", "SHOW_DELETED", false, DBConfigMode.UserOnly);
        this._ShowDeletedObjectsInitialized = true;
      }
      return this._ShowDeletedObjects;
    }
    set
    {
      if (value == this._ShowDeletedObjects)
        return;
      if (value && !this.DBSecurity.IsAdminMode)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14239(1197370681));
      this._ShowDeletedObjects = value;
      if (this.IsSystemSession)
        return;
      this.Configurations.WriteBool("KERNEL", "SECURITY", "SHOW_DELETED", value);
    }
  }

  public bool ShowPersonalObjects
  {
    [DebuggerStepThrough] get
    {
      if (!this._ShowPersonalObjectsInitialized)
      {
        if (!this.IsSystemSession)
          this._ShowPersonalObjects = this.DBSecurity.IsAdminMode && this.Configurations.ReadBool("KERNEL", "SECURITY", "SHOW_PERSONAL", false, DBConfigMode.UserOnly);
        this._ShowPersonalObjectsInitialized = true;
      }
      return this._ShowPersonalObjects;
    }
    set
    {
      if (value == this._ShowPersonalObjects)
        return;
      if (value && !this.DBSecurity.IsAdminMode)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14240(1928758524));
      this._ShowPersonalObjects = value;
      if (this.IsSystemSession)
        return;
      this.Configurations.WriteBool("KERNEL", "SECURITY", "SHOW_PERSONAL", value);
    }
  }

  public bool DeveloperMode
  {
    [DebuggerStepThrough] get
    {
      if (this._DeveloperModeInitialized)
        return this._DeveloperMode;
      if (this._IsSystemSession)
      {
        this._DeveloperMode = true;
      }
      else
      {
        object obj = (object) ConfigurationManager.AppSettings.Get(nameof (DeveloperMode));
        if (obj != null)
          this._DeveloperMode = ((string) obj).ToLower() == "true" && this._RoleID == this.IdentHelper.AdminRoleID;
      }
      this._DeveloperModeInitialized = true;
      return this._DeveloperMode;
    }
    set
    {
      if (!this.IsAdmin && !this.IsSystemSession)
        throw new KernelException("Must be system or administrator session!");
      this._DeveloperMode = value;
    }
  }

  public bool EtalonBase
  {
    get
    {
      if (this._EtalonBaseInitialized)
        return this._EtalonBase;
      object obj = (object) ConfigurationManager.AppSettings.Get(nameof (EtalonBase));
      this._EtalonBase = obj != null && ((string) obj).ToLower() == "1";
      this._EtalonBaseInitialized = true;
      return this._EtalonBase;
    }
  }

  public void ValidateSystemDelete(object deletingObject, string deleteCaption)
  {
    if (deletingObject is IDBGuid dbGuid && dbGuid.IsSystemGUID)
      throw new KernelException(deleteCaption);
  }

  public void AddModifiedCacheTable(string tableName)
  {
    if (this._ChangedCacheTables == null)
      this._ChangedCacheTables = new List<string>(1);
    if (!this.DataManager.InTransaction || this._ChangedCacheTables.IndexOf(tableName) >= 0)
      return;
    this._ChangedCacheTables.Add(tableName);
  }

  internal bool LoggingOn
  {
    [DebuggerStepThrough] get => this._LoggingOn;
  }

  void IServerSession.CheckLogin() => this.CheckLogin();

  internal int CallCounter
  {
    get => this._UserID.Value == 0L || this.IsClosingOrDisposed ? 0 : this._CallCounter.Value;
  }

  private void CheckLogin()
  {
    this.CurrentOperationCancellationHandler.CheckCancellationRequested();
    if (this._UserID.Value == 0L || this.IsClosingOrDisposed)
      throw new NotLoggedInException();
    this._LastCallTime.Value = DateTime.UtcNow;
    this._CallCounter.Update();
  }

  public long UserID
  {
    [DebuggerStepThrough] get => this._UserID.Value;
  }

  public long ID
  {
    get
    {
      if (this._ID == 0L)
      {
        object obj = this.DataManager.ExecuteScalar("SELECT F_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :oid", this.DataManager.Parameter("oid", (object) this.UserID));
        if (obj != null && obj != DBNull.Value)
          this._ID = Convert.ToInt64(obj);
      }
      return this._ID;
    }
  }

  [Obsolete("Use the property SessionGUID instead of this.", true)]
  int IUserSession.SessionID => this.SessionID;

  internal int SessionID
  {
    [DebuggerStepThrough] get => this._SessionID;
  }

  public Guid SessionGUID
  {
    [DebuggerStepThrough] get => this._SessionGUID;
  }

  public Guid MasterSessionGUID
  {
    [DebuggerStepThrough] get
    {
      return this.ParentSession == null ? this.SessionGUID : this.ParentSession.SessionGUID;
    }
  }

  public static IUserSession GetSessionByID(Guid sessionGUID, bool throwNotExistsExeption)
  {
    UserSession session = (UserSession) UserSession._Sessions.GetSession(sessionGUID);
    if (session == null & throwNotExistsExeption)
      throw new KernelException($"Сессия с идентификатором {sessionGUID} не найдена");
    if (session != null)
    {
      session.CurrentOperationCancellationHandler.CheckCancellationRequested();
      session.ValidateClientCall();
    }
    return (IUserSession) session;
  }

  public static IUserSession GetSessionByID(Guid sessionGUID)
  {
    return UserSession.GetSessionByID(sessionGUID, true);
  }

  public string ComputerName
  {
    [DebuggerStepThrough] get => this._ComputerName.Value;
  }

  public string UserName => this._UserName.Value;

  public DateTime LastCallTime
  {
    [DebuggerStepThrough] get => this._LastCallTime.Value;
  }

  private void BuildLanguageSQL()
  {
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.Append($"(({this.DataManager.DataProvider.GetEqualEmptyString("F_LANGUAGE_ID")})");
      if (this._Languages == "")
      {
        stringBuilder.AppendFormat(" OR (F_LANGUAGE_ID = '{0}')", (object) this.GetLanguageCollection().DefaultLanguageID);
      }
      else
      {
        for (int index = 0; index < this._Languages.Length; ++index)
          stringBuilder.AppendFormat(" OR (F_LANGUAGE_ID = '{0}')", (object) this._Languages[index]);
      }
      this._LanguageSQL = stringBuilder.Append(")").ToString();
    }
  }

  public string LanguageSQL
  {
    [DebuggerStepThrough] get => this._LanguageSQL;
  }

  private void BuildAreaSQL()
  {
    if (this._AreaID == "")
    {
      this._AreaSQL = "";
    }
    else
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append($"(({this.DataManager.DataProvider.GetEqualEmptyString("F_AREA_ID")})");
        for (int index = 0; index < this._AreaID.Length; ++index)
          stringBuilder.AppendFormat(" OR (F_AREA_ID LIKE '%{0}%')", (object) this._AreaID[index]);
        this._AreaSQL = stringBuilder.Append(")").ToString();
      }
    }
  }

  public string AreaSQL
  {
    [DebuggerStepThrough] get => this._AreaSQL;
  }

  public string AreaID
  {
    [DebuggerStepThrough] get => this._AreaID;
    set
    {
      if (!(this._AreaID != value))
        return;
      this.CheckLogin();
      this.GetSubjectAreaCollection().ValidateAriasID(value);
      this._AreaID = value;
      this.BuildAreaSQL();
    }
  }

  public string LanguageID
  {
    [DebuggerStepThrough] get => this._Languages;
    set
    {
      if (!(this._Languages != value))
        return;
      this.CheckLogin();
      this.GetLanguageCollection().CheckValidLanguageID(value);
      this.Configurations.WriteString("KERNEL", "COMMON", "LANGUAGE", value);
      this._Languages = value;
      this.BuildLanguageSQL();
    }
  }

  public int MaxRows
  {
    [DebuggerStepThrough] get => this._MaxRows;
    set
    {
      this.CheckLogin();
      if (value < 0)
        throw new ArithmeticException(sc_14238.ssp_appserver_14241());
      if (value > 10000)
        throw new KernelExceptionID(403, (object) 10000);
      this.Configurations.WriteInteger("KERNEL", "PERFORMANCE", "MAXROWS", (long) value);
      this._MaxRows = value;
    }
  }

  public Guid UserGUID
  {
    get
    {
      this.CheckLogin();
      if (this._UserGUID == Guid.Empty)
        this._UserGUID = (this.GetObject(this.UserID) as IDBGuid).GUID;
      return this._UserGUID;
    }
  }

  public DBSecurity DBSecurity
  {
    [DebuggerStepperBoundary] get
    {
      DBSecurity dbSecurity = this._DBSecurity.Value;
      if (dbSecurity == null)
      {
        dbSecurity = new DBSecurity(this);
        this._DBSecurity.Value = dbSecurity;
      }
      return dbSecurity;
    }
  }

  internal DBSecurity RaceGetCurrentDBSecurity() => this._DBSecurity.Value;

  public IDBSecurity GetAttributeLCSecurity(int attributeID, int lcStepID, int objectTypeID)
  {
    return ServerConsts.CheckAttributeLCStepSecurity ? (IDBSecurity) new DBAttributeLCSecurity(this, attributeID, lcStepID, objectTypeID) : (IDBSecurity) null;
  }

  public TimeSpan TimeZoneOffset
  {
    [DebuggerStepThrough] get => this._TimeZoneOffset;
  }

  public DateTime UTCTime
  {
    [DebuggerStepThrough] get => DateTime.UtcNow;
  }

  public long ClientConnectionID
  {
    [DebuggerStepThrough] get => this._clientConnectionID;
  }

  internal long RoleID_ID
  {
    [DebuggerStepThrough] get => this._RoleID_ID;
  }

  public long RoleID
  {
    [DebuggerStepThrough] get => this._RoleID;
    set
    {
      if (this._RoleID == value)
        return;
      (ServerServices.GetService(typeof (IRolesCache)) as IRolesCache).ValidateUserRole(this.UserID, value, this.UserName);
      this._RoleID_ID = this.GetObjectInfo(value).ID;
      this._RoleID = value;
      this.DBSecurity.LoadGroupsList();
    }
  }

  internal RemotingOperationCancellationHandler CurrentOperationCancellationHandler
  {
    get => this._currentOperationCancellationHandler;
  }

  public static int UserLockedAttributeID
  {
    get
    {
      if (UserSession._UserLockedAttributeID == 0 && ServerServices.GetService(typeof (IIDHelper)) is IIDHelper service)
        UserSession._UserLockedAttributeID = service.GetAttributeID("cadd99fb-306c-11d8-b4e9-00304f19f545");
      return UserSession._UserLockedAttributeID;
    }
  }

  private static void IncWrongPasswords(long userID, string compName)
  {
    if (ServerConsts.WrongPasswordsLimit <= 0)
      return;
    int num1;
    if (UserSession.WrongPasswordsDict.TryGetValue(userID, out num1))
    {
      int num2;
      if ((num2 = num1 + 1) >= ServerConsts.WrongPasswordsLimit)
      {
        if (!(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service1))
          return;
        IUserSession sessionTemporaryClone = service1.GetSystemSessionTemporaryClone("LockUser");
        try
        {
          IDBObject dbObject = sessionTemporaryClone.GetObject(userID, false);
          if (dbObject == null)
            return;
          IEventLogHelper service2 = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
          DelayedUpdaterService service3 = ServerServices.GetService(typeof (IDelayedUpdaterService)) as DelayedUpdaterService;
          IDBAttribute attributeById = dbObject.GetAttributeByID(UserSession.UserLockedAttributeID);
          if (attributeById == null)
          {
            dbObject.Attributes.AddAttribute(UserSession.UserLockedAttributeID, false, new object[1]
            {
              (object) true
            });
          }
          else
          {
            if (!attributeById.IsNull && attributeById.AsBoolean)
            {
              string Note = $"Заблокированный пользователь '{dbObject.Caption}' пытался зайти в систему.";
              service2?.AddEvent(userID, 0L, 1, userID, dbObject.NameInMessages, Note, ActionType.Login, EventlogRecordType.AccessDenied, sessionTemporaryClone.UserID, compName, sessionTemporaryClone);
              service3?.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, userID, -1, ActionType.Login, new string[4]
              {
                Note,
                dbObject.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545")).AsString,
                string.Empty,
                compName
              }));
              throw new KernelExceptionID(370, (object) dbObject.Caption);
            }
            attributeById.AsBoolean = true;
          }
          UserSession.ResetWrongPasswords(userID);
          service2?.AddEvent(userID, 0L, 1, userID, dbObject.NameInMessages, $"Пользователь {dbObject.Caption} заблокирован после {num2} неудачных попыток входа в систему.", ActionType.Login, EventlogRecordType.Warning, sessionTemporaryClone.UserID, compName, sessionTemporaryClone);
        }
        finally
        {
          sessionTemporaryClone.Logout("LockUser");
        }
      }
      else
        UserSession.WrongPasswordsDict[userID] = num2;
    }
    else
      UserSession.WrongPasswordsDict.TryAdd(userID, 1);
  }

  private static void ResetWrongPasswords(long userID)
  {
    if (ServerConsts.WrongPasswordsLimit <= 0)
      return;
    UserSession.WrongPasswordsDict.TryRemove(userID, out int _);
  }

  public bool CheckDBVersion(string moduleName, int needVersion, bool throwVersionException)
  {
    bool flag = true;
    DataRow dataRow = this.DBCache.GetTable("IMS_DBVERSION").Rows.Find((object) moduleName);
    if (dataRow != null && Convert.ToInt32(dataRow["F_VERSION_ID"]) != needVersion)
    {
      this.DBCache.ReloadTables((IUserSession) this, this.DataManager, "IMS_DBVERSION");
      int int32 = Convert.ToInt32(this.DBCache.GetTable("IMS_DBVERSION").Rows.Find((object) moduleName)["F_VERSION_ID"]);
      if (int32 != needVersion)
      {
        if (throwVersionException)
          throw new KernelExceptionID(253, (object) moduleName, (object) int32, (object) needVersion);
        flag = false;
      }
    }
    return flag;
  }

  public int GetDBVersion(string moduleName)
  {
    object obj = this.DataManager.ExecuteScalar($"SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = '{moduleName}'");
    return obj != null && obj != DBNull.Value ? Convert.ToInt32(obj) : 0;
  }

  public void GetDBVersionEx(string moduleName, ref int version, ref int revision)
  {
    DataTable dataTable = this.DataManager.ExecuteDataTable($"SELECT F_VERSION_ID, F_REVISION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = '{moduleName}'");
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      DataRow row = dataTable.Rows[0];
      version = DBNull.Value.Equals(row[0]) ? 0 : Convert.ToInt32(row[0]);
      revision = DBNull.Value.Equals(row[1]) ? 0 : Convert.ToInt32(row[1]);
    }
    else
    {
      version = 0;
      revision = 0;
    }
  }

  public int SetDBVersion(
    string moduleName,
    int version,
    int revision = 0,
    string addsql = "",
    bool tryInsert = true)
  {
    try
    {
      if (tryInsert)
      {
        this.DataManager.ExecuteNonQuery($"INSERT INTO IMS_DBVERSION (F_MODULE_NAME, F_VERSION_ID, F_REVISION_ID) VALUES('{moduleName}',{version},{revision})");
        return 1;
      }
    }
    catch
    {
      tryInsert = false;
    }
    return this.DataManager.ExecuteNonQuery($"UPDATE IMS_DBVERSION SET F_VERSION_ID={version}, F_REVISION_ID={revision} WHERE F_MODULE_NAME = '{moduleName}'{addsql}");
  }

  private void ClonedLogin(UserSession parentSession, string sessionName)
  {
    if (parentSession._loginName.ToUpper() == "SYSTEM")
    {
      if (!this._AllowSystemLogin.Value)
        throw new InvalidLoginInfoException();
      this._IsSystemSession = true;
    }
    this._SessionName = sessionName;
    this._TimeZoneOffset = parentSession.TimeZoneOffset;
    this._UserName.Value = parentSession.UserName;
    this._SecurityLevel = parentSession._SecurityLevel;
    this._ComputerName.Value = parentSession.ComputerName;
    this._UserID.Value = parentSession.UserID;
    this._actingUserID = parentSession.ActingUserID;
    this._actingUserName = parentSession.ActingUserName;
    this._PasswordExpiredDays = parentSession._PasswordExpiredDays;
    this._RoleID = parentSession.RoleID;
    this.DBSecurity.LoadGroupsList(parentSession.DBSecurity.GetGroupsArrayList(), parentSession.DBSecurity.GetGroupsIDArrayList(), parentSession.DBSecurity._GroupsSQL);
    this.DBSecurity._OwnerGroupsSQL = parentSession.DBSecurity._OwnerGroupsSQL;
    this._MaxRows = parentSession._MaxRows;
    this._Languages = parentSession._Languages;
    this.BuildLanguageSQL();
    this._password = parentSession.Password;
    this._loginName = parentSession.LoginName;
    this._AreaID = parentSession.AreaID;
    this._AreaSQL = parentSession.AreaSQL;
    this._clientConnectionID = parentSession.ClientConnectionID;
    this._SData = parentSession._SData;
    lock (parentSession._pluginsData.SyncRoot)
    {
      foreach (DictionaryEntry dictionaryEntry in parentSession._pluginsData)
      {
        if (!(dictionaryEntry.Value is IUserSessionLocalData))
          this._pluginsData.Add(dictionaryEntry.Key, dictionaryEntry.Value);
      }
    }
    this._ParentSession.Value = parentSession.ParentSession != null ? parentSession.ParentSession : parentSession;
    this._LastCallTime.Value = DateTime.UtcNow;
    this.SessionStatus = UserSessionStatus.Logged;
  }

  public void SetLoginCapabilities(
    bool isPermanent = false,
    bool allowSystemLogin = false,
    bool allowLoginWithoutPassword = false)
  {
    if (this.SessionStatus != UserSessionStatus.NotLogged)
      throw new InvalidOperationException($"Метод {nameof (SetLoginCapabilities)} может быть вызван только до метода {"Login"}.");
    this._IsPermanent.Value = isPermanent;
    this._AllowSystemLogin.Value = allowSystemLogin;
    this._AllowLoginWithoutPassword.Value = allowLoginWithoutPassword;
  }

  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    bool isCloned)
  {
    return this.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, isCloned, -1, "DefaultMainClientSession");
  }

  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    bool isCloned,
    string sessionName)
  {
    return this.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, isCloned, -1, sessionName);
  }

  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    string sessionName)
  {
    return this.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, false, sessionName);
  }

  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    int accessLevel,
    string sessionName)
  {
    return this.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, false, accessLevel, sessionName);
  }

  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    bool isCloned,
    int accessLevel,
    string sessionName)
  {
    if (aLoginName.ToUpper() != "SYSTEM")
    {
      this.CheckDBVersion("KERNEL", 710, true);
      if (accessLevel != 0 && accessLevel != -1 && !this.DBCache.AccessLevelExists(accessLevel))
      {
        string str = string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14245()), (object) accessLevel);
        this.EventLogHelper.AddEvent(0L, 0L, 1, 0L, aLoginName, str, ActionType.Login, EventlogRecordType.AccessDenied, 0L, aComputerName, (IUserSession) null);
        throw new KernelException(str);
      }
    }
    bool flag1 = this._AllowLoginWithoutPassword.Value;
    this._ID = 0L;
    this._TimeZoneOffset = aTimeZoneOffset.Hours <= 12 && aTimeZoneOffset.Hours >= -12 ? aTimeZoneOffset : throw new KernelException(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14246()));
    if (!isCloned && !flag1)
      this._LoginEventID = this.EventLogHelper.AddEvent(0L, 0L, 1, 0L, aLoginName, "", ActionType.Login, EventlogRecordType.AccessDenied, 0L, aComputerName, (IUserSession) null);
    if (aRoleID != 0L && aRoleID == this.IdentHelper.InternalServiceRoleID && !flag1)
      throw new KernelException("Internal role login error.");
    string upper = aLoginName.Trim().ToUpper();
    object obj = (object) null;
    int attributeID = !aPassword.IsValidPassword(Consts.WinloginPswHash) || UserSession.LoginMode != IMServerLoginMode.DomainLogin && UserSession.LoginMode != IMServerLoginMode.DomainOnlyLogin ? this.IdentHelper.LoginNameID : this.IdentHelper.GetAttributeID("cadd93c1-306c-11d8-b4e9-00304f19f545");
    switch (this.DBCache.GetOptimizationMode(attributeID, this.IdentHelper.UsersTypeID, -1))
    {
      case OptimizationModes.Read:
      case OptimizationModes.Seek:
        try
        {
          obj = this.DataManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM IMV_O{this.IdentHelper.UsersTypeID} WHERE F{attributeID} = :loginName AND F_LEVEL_ID NOT IN ({this.IdentHelper.DeletedID}, {this.IdentHelper.AnnulmentLevelID}, {this.IdentHelper.KeepingLevelID}) AND F_OBJECT_ID > 0", this.DataManager.Parameter("loginName", (object) upper));
          break;
        }
        catch (Exception ex)
        {
          this.EventLogHelper.AddToTrace("Login error in optimization table: " + ex.Message, Consts.traceError, string.Empty);
          break;
        }
    }
    if (obj == null || obj == DBNull.Value)
      obj = this.DataManager.ExecuteScalar(string.Format("SELECT IMS_OBJECTS.F_OBJECT_ID FROM IMS_OBJECTS, {0} WHERE IMS_OBJECTS.F_OBJECT_ID > 0 AND IMS_OBJECTS.F_OBJECT_TYPE = {1} AND IMS_OBJECTS.F_LEVEL_ID NOT IN ({2}, {4}, {5}) AND {0}.F_OBJECT_ID = IMS_OBJECTS.F_OBJECT_ID AND {0}.F_ATTRIBUTE_ID = {3} AND {0}.F_STRING_VALUE = :loginName", (object) this.DBCache.GetAttributesTableName(this.IdentHelper.UsersTypeID), (object) this.IdentHelper.UsersTypeID, (object) this.IdentHelper.DeletedID, (object) attributeID, (object) this.IdentHelper.AnnulmentLevelID, (object) this.IdentHelper.KeepingLevelID), this.DataManager.Parameter("loginName", (object) upper));
    if (obj == null || obj == DBNull.Value)
    {
      this.EventLogHelper.CloseEvent(this._LoginEventID, EventlogRecordType.AccessDenied, string.Empty, (IUserSession) null);
      this.DelayedUpdater?.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, 0L, -1, ActionType.Login, new string[1]
      {
        $"Неудачная попытка авторизации в системе под именем '{aLoginName}' с компьютера {aComputerName}."
      }));
      throw new InvalidLoginInfoException();
    }
    long int64_1 = Convert.ToInt64(obj);
    DataTable dataTable = this._dbManager.ExecuteDataTable($"SELECT * FROM {this.DBCache.GetAttributesTableName(this.IdentHelper.UsersTypeID)} WHERE F_OBJECT_ID = :usrID", this._dbManager.Parameter("usrID", (object) int64_1));
    DateTime dateTime = DateTime.UtcNow;
    long int64_2 = Convert.ToInt64((ServerServices.GetService(typeof (IDBConfigurationService)) as IDBConfigurationService).GetValue("KERNEL", "SECURITY", "PSW_LIFETIME", (object) 0L));
    int attributeId = this.IdentHelper.GetAttributeID("cad0005c-306c-11d8-b4e9-00304f19f545");
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      if (int32 == this.IdentHelper.ExternalUserID)
      {
        long result = 0;
        if (row["F_INTEGER_VALUE"] != null && !long.TryParse(row["F_INTEGER_VALUE"].ToString(), out result))
          result = 0L;
        if (result > 0L)
          throw new KernelExceptionID(407);
      }
      else if (int32 == this.IdentHelper.PasswordID)
      {
        if (aLoginName.ToUpper() == "SYSTEM")
        {
          if (!this._AllowSystemLogin.Value)
          {
            this.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, int64_1, -1, ActionType.Login, new string[1]
            {
              $"Неудачная попытка авторизации в системе под именем '{aLoginName}' с компьютера {aComputerName}."
            }));
            throw new InvalidLoginInfoException();
          }
          this._IsSystemSession = true;
        }
        else if (this._actingUserID == 0L && (!aPassword.IsValidPassword(Consts.WinloginPswHash) || UserSession.LoginMode != IMServerLoginMode.WindowsLogin && UserSession.LoginMode != IMServerLoginMode.DomainLogin && UserSession.LoginMode != IMServerLoginMode.DomainOnlyLogin) && !aPassword.IsValidPassword(row["F_STRING_VALUE"].ToString()))
        {
          this.EventLogHelper.CloseEvent(this._LoginEventID, EventlogRecordType.AccessDenied, "Введен неверный пароль", (IUserSession) null);
          this.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, int64_1, -1, ActionType.Login, new string[1]
          {
            $"Неудачная попытка авторизации в системе под именем '{aLoginName}' с компьютера {aComputerName}."
          }));
          UserSession.IncWrongPasswords(int64_1, aComputerName);
          throw new InvalidLoginInfoException();
        }
        if (row["F_DATE_VALUE"] != DBNull.Value)
          dateTime = Convert.ToDateTime(row["F_DATE_VALUE"]);
        else if (int64_2 > 0L && aLoginName.ToUpper() != "SYSTEM" && this.NewPassword.IsEmpty)
          throw new PasswordExpiredException();
      }
      else if (int32 == this.IdentHelper.UserNameID)
        this._UserName.Value = row["F_STRING_VALUE"].ToString();
      else if (int32 == this.IdentHelper.SecurityLevelID)
      {
        int result = 0;
        if (int.TryParse(row["F_INTEGER_VALUE"].ToString(), out result))
        {
          if (accessLevel == -1)
            this._SecurityLevel = result;
          else if (accessLevel <= result)
          {
            this._SecurityLevel = accessLevel;
          }
          else
          {
            string str = string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14247()), (object) accessLevel, (object) this._SecurityLevel);
            this.EventLogHelper.AddEvent(0L, 0L, 1, 0L, aLoginName, str, ActionType.Login, EventlogRecordType.AccessDenied, 0L, aComputerName, (IUserSession) null);
            this.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, int64_1, -1, ActionType.Login, new string[4]
            {
              str,
              aLoginName,
              aPassword.ToString(),
              aComputerName
            }));
            UserSession.IncWrongPasswords(int64_1, aComputerName);
            throw new KernelException(str);
          }
        }
      }
      else if (int32 == this.IdentHelper.LoginNameID)
        this._loginName = row["F_STRING_VALUE"].ToString();
      else if (int32 == attributeId && row["F_INTEGER_VALUE"] != DBNull.Value)
        this._UserStorageID = Convert.ToInt64(row["F_INTEGER_VALUE"]);
    }
    if (this._SecurityLevel > this._ClientAccessLevel)
    {
      string str = $"Попытка зайти в систему с уровнем доступа {this._SecurityLevel}, который выше уровня {this._ClientAccessLevel}, разрешенного для данного клиента.";
      this.EventLogHelper.AddEvent(0L, 0L, 1, 0L, aLoginName, str, ActionType.Login, EventlogRecordType.AccessDenied, 0L, aComputerName, (IUserSession) null);
      this.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, int64_1, -1, ActionType.Login, new string[4]
      {
        str,
        aLoginName,
        aPassword.ToString(),
        aComputerName
      }));
      throw new KernelException(str);
    }
    this._ComputerName.Value = aComputerName;
    this._UserID.Value = int64_1;
    if (int64_2 > 0L && this._actingUserID == 0L && !this.IsSystemSession)
    {
      TimeSpan timeSpan = DateTime.UtcNow - dateTime;
      this._PasswordExpiredDays = Convert.ToInt32(int64_2 - (long) timeSpan.Days);
      if (this._PasswordExpiredDays <= 0 || !this.NewPassword.NotInited)
      {
        bool flag2 = false;
        if (!this.NewPassword.NotInited && this.NewPassword != aPassword)
        {
          IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("UserSession.Login");
          try
          {
            if (!this.Configurations.ReadBool("KERNEL", "SECURITY", "PSW_USER", true, DBConfigMode.GlobalOnly))
              throw new PasswordModifyException();
            sessionTemporaryClone.GetObject(this._UserID.Value).GetAttributeByGuid(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")).Value = (object) this.NewPassword;
            flag2 = true;
          }
          finally
          {
            sessionTemporaryClone.Logout("UserSession.Login");
          }
        }
        if (!flag2)
          throw new PasswordExpiredException();
      }
    }
    try
    {
      if (this._IsSystemSession)
      {
        this._RoleID = this.IdentHelper.AdminRoleID;
        if (DBRoleObject.AdminRoleID == 0L)
          DBRoleObject.AdminRoleID = this._RoleID;
        this.DBSecurity.LoadGroupsList();
      }
      else
      {
        if (aRoleID != 0L)
        {
          this.RoleID = aRoleID;
        }
        else
        {
          long num = this.Configurations.ReadInteger("KERNEL", "COMMON", "ROLE", 0L, DBConfigMode.UserOnly);
          if (num == 0L)
          {
            RoleProperties[] rolesList = this.GetRolesList(int64_1);
            this.RoleID = rolesList.Length != 0 ? rolesList[0].RoleID : throw new KernelException(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14248()), (object) this._UserName.Value));
          }
          else
          {
            this._RoleID = num;
            this.DBSecurity.LoadGroupsList();
          }
        }
        if (UserSession._DoubleLoginTimeout > 0)
        {
          IUserSession sessionByUserId = (IUserSession) (UserSession.Sessions as UserSessionCollection).GetSessionByUserID(int64_1, UserSession._DoubleLoginTimeout, aComputerName);
          if (sessionByUserId != null)
          {
            string str = $"Попытка повторной авторизации в системе пользователя {this.UserName} с компьютера {aComputerName}. Данный пользователь уже зашел в систему с компьютера {sessionByUserId.ComputerName}.";
            this.EventLogHelper.CloseEvent(this._LoginEventID, int64_1, int64_1, this._UserName.Value, str, EventlogRecordType.AccessDenied, (IUserSession) this);
            this.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, this._UserID.Value, -1, ActionType.Login, new string[4]
            {
              str,
              aLoginName,
              aPassword.ToString(),
              aComputerName
            }));
            if (this.GetCustomService(typeof (IRouterService)) is IRouterService customService)
              customService.CreateMessage(this.SessionGUID, this.IdentHelper.SysdbaID, $"Уведомление о попытке повторной авторизации пользователя {this.UserName} в системе", str, this.IdentHelper.SystemID);
            throw new KernelException(str);
          }
        }
      }
      ((IDBSecurityCache) this.DBSecurity).ClearCache();
      if (!flag1)
        this.DBSecurity.CheckAccess(new CategoryValue(14, 0L, ActionType.Login), true, true);
      if (!isCloned && this.EventLogHelper != null && !flag1)
        this.EventLogHelper.CloseEvent(this._LoginEventID, int64_1, int64_1, this._UserName.Value, "", EventlogRecordType.AccessGranted, (IUserSession) this);
      this._MaxRows = Convert.ToInt32(this.Configurations.ReadInteger("KERNEL", "PERFORMANCE", "MAXROWS", 500L, DBConfigMode.UserAndGlobal));
      this._Languages = this.Configurations.ReadString("KERNEL", "COMMON", "LANGUAGE", "", DBConfigMode.UserAndGlobal);
      this.BuildLanguageSQL();
      this._LastCallTime.Value = DateTime.UtcNow;
      this._password = aPassword;
      if (UserSession.LoginMode != IMServerLoginMode.DomainLogin && UserSession.LoginMode != IMServerLoginMode.DomainOnlyLogin)
        this._loginName = aLoginName;
      if (this.IsSystemSession)
        this._SecurityLevel = int.MaxValue;
      IDBObject dbObject1;
      try
      {
        dbObject1 = this.GetObject(this._RoleID);
      }
      catch
      {
        throw new KernelExceptionID(sc_14238.ssp_appserver_14249(1697741113), (object) this._RoleID);
      }
      IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(new Guid("cad001af-306c-11d8-b4e9-00304f19f545"), false);
      this._AreaID = attributeByGuid != null ? attributeByGuid.AsString : string.Empty;
      this.BuildAreaSQL();
      if (this._UserID.Value != this.IdentHelper.SystemID)
      {
        if (this._UserID.Value != this.IdentHelper.SysdbaID)
        {
          IDBObject dbObject2 = this.GetObject(this._UserID.Value);
          IDBAttribute attributeById = dbObject2.GetAttributeByID(UserSession.UserLockedAttributeID);
          if (attributeById != null && attributeById.AsBoolean)
          {
            string Note = $"Заблокированный пользователь '{this.UserName}' пытался зайти в систему.";
            this.EventLogHelper.CloseEvent(this._LoginEventID, int64_1, int64_1, this._UserName.Value, Note, EventlogRecordType.AccessDenied, (IUserSession) this);
            this.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, this._UserID.Value, -1, ActionType.Login, new string[4]
            {
              Note,
              aLoginName,
              aPassword.ToString(),
              aComputerName
            }));
            throw new KernelExceptionID(370, (object) this.UserName);
          }
          if (this.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService)
          {
            if (customService.Info != null)
            {
              if (dbObject2.SiteID != null)
              {
                if (dbObject2.SiteID.Length > 0)
                {
                  if ((int) customService.Info.Code != (int) dbObject2.SiteID[0])
                  {
                    string str = string.Format(sc_14238.ssp_appserver_14250(), (object) this.UserName);
                    this.EventLogHelper.CloseEvent(this._LoginEventID, int64_1, int64_1, this._UserName.Value, str, EventlogRecordType.AccessDenied, (IUserSession) this);
                    this.DelayedUpdater.AddDelayedNotification((DelayedNotification) new AccessDeniedDelayedNotification(0L, ActionType.GetAccess, this._UserID.Value, -1, ActionType.Login, new string[4]
                    {
                      str,
                      aLoginName,
                      aPassword.ToString(),
                      aComputerName
                    }));
                    throw new KernelException(str);
                  }
                }
              }
            }
          }
        }
      }
    }
    catch
    {
      this._UserID.Value = 0L;
      throw;
    }
    UserSession.ResetWrongPasswords(int64_1);
    if (!this._IsSystemSession)
      this._clientConnectionID = UserSession._ClientConnections.CreateConnectionID();
    if (!isCloned)
    {
      (this.EventLogHelper as Intermech.Kernel.EventLogHelper).OnLogin((IUserSession) this);
      if (!this._IsSystemSession)
        (ServerServices.GetService(typeof (IUsersGroupsListCache)) as IUsersGroupsListCache).LoadCache((IUserSession) this);
    }
    this.SessionStatus = UserSessionStatus.Logged;
    this._SessionName = sessionName;
    return int64_1;
  }

  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID)
  {
    return this.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, false);
  }

  public long Login(
    string aLoginName,
    string aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    string sessionName)
  {
    return this.Login(aLoginName, new PswPackage(aPassword, ServerConsts.CryptMethod), aComputerName, aTimeZoneOffset, aRoleID, false, sessionName);
  }

  public long Login(
    string aLoginName,
    PswPackage aPassword,
    string aComputerName,
    TimeSpan aTimeZoneOffset,
    long aRoleID,
    int accessLevel)
  {
    return this.Login(aLoginName, aPassword, aComputerName, aTimeZoneOffset, aRoleID, false, accessLevel, "DefaultMainClientSession");
  }

  public long LoginAsActingUser(ActingUserLoginParameters loginParameters)
  {
    if (loginParameters == null)
      throw new ArgumentNullException(nameof (loginParameters));
    IUserSession sessionTemporaryClone = ServiceUtils.GetService<IDBTimedEvents>((object) ApplicationServices.Container, true).GetSystemSessionTemporaryClone(nameof (LoginAsActingUser));
    try
    {
      return this.LoginAsActingUser(loginParameters, sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (LoginAsActingUser));
    }
  }

  private long LoginAsActingUser(
    ActingUserLoginParameters loginParameters,
    IUserSession systemSession)
  {
    List<ActingUserLoginSettings> userLoginSettings = systemSession.GetActingUserLoginSettings(loginParameters.ActingUser.UserID);
    IDBObject dbObject1 = systemSession.GetObject(loginParameters.UserID);
    IDBObject dbObject2 = systemSession.GetObject(loginParameters.ActingUser.UserID);
    if (loginParameters.SecurityLevel > loginParameters.ActingUser.SecurityLevel)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14251(1463007450), (object) dbObject1.Caption, (object) this.DBCache.GetAccessCaption(Convert.ToInt32(dbObject1.GetAttributeByID(this.IdentHelper.SecurityLevelID).AsInteger)));
    bool flag = false;
    for (int index = 0; index < userLoginSettings.Count; ++index)
    {
      foreach (KeyValuePair<long, string> user in userLoginSettings[index].Users)
      {
        if (user.Key == loginParameters.UserID)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        if (userLoginSettings[index].RoleID != 0L && userLoginSettings[index].RoleID != loginParameters.RoleID)
          flag = false;
        else
          break;
      }
    }
    if (!flag)
    {
      if (loginParameters.RoleID != 0L)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14252(1310968228), (object) dbObject2.Caption, (object) dbObject1.Caption, (object) systemSession.GetObject(loginParameters.RoleID).Caption);
      throw new KernelExceptionID(sc_14238.ssp_appserver_14253(547343927), (object) dbObject2.Caption, (object) dbObject1.Caption);
    }
    this._actingUserID = loginParameters.ActingUser.UserID;
    this._actingUserName = dbObject2.GetAttributeByID(systemSession.IdentHelper.UserNameID).AsString;
    return this.Login(dbObject1.GetAttributeByID(systemSession.IdentHelper.LoginNameID).AsString, new PswPackage(dbObject1.GetAttributeByID(systemSession.IdentHelper.PasswordID).AsString, ServerConsts.CryptMethod), loginParameters.ActingUser.ComputerName, loginParameters.ActingUser.TimeZoneOffset, loginParameters.RoleID, false, loginParameters.SecurityLevel, "DefaultMainClientSession");
  }

  internal long InternalLogin(
    UserSession sourceSession,
    long usrRoleID,
    long usrID,
    string sessionName)
  {
    if (!this._AllowLoginWithoutPassword.Value)
      throw new KernelException(sc_14238.ssp_appserver_14254());
    string asString = sourceSession.GetObject(usrID).GetAttributeByID(sourceSession.IdentHelper.LoginNameID).AsString;
    string computerName = sourceSession.ComputerName;
    TimeSpan timeZoneOffset = sourceSession.TimeZoneOffset;
    this._actingUserID = sourceSession.UserID;
    this._actingUserName = sourceSession.UserName;
    if (usrRoleID == -1L)
    {
      RoleProperties[] rolesList = this.GetRolesList(asString);
      if (rolesList.Length != 0)
      {
        usrRoleID = rolesList[0].RoleID;
        for (int index = 0; index < rolesList.Length; ++index)
        {
          if (rolesList[index].RoleID == this.IdentHelper.AdminRoleID)
          {
            usrRoleID = rolesList[index].RoleID;
            break;
          }
        }
      }
    }
    return this.Login(asString, new PswPackage(), computerName, timeZoneOffset, usrRoleID, false, sessionName);
  }

  public long ActingUserID => this._actingUserID;

  public long RealUserID => this._actingUserID == 0L ? this.UserID : this._actingUserID;

  public string ActingUserName => this._actingUserName;

  public bool IsPermanent
  {
    [DebuggerStepThrough] get => this._IsPermanent.Value;
  }

  public IUserSession Clone(string sessionName) => this.Clone(false, sessionName);

  public IUserSession Clone(bool isPermanent, string sessionName)
  {
    this.CheckLogin();
    if (sessionName == null || sessionName.Trim() == string.Empty)
      throw new UserSessionProtectionException("Попытка клонирования сессии с пустым именем.");
    UserSession userSession = new UserSession();
    userSession.SetLoginCapabilities(isPermanent, this._AllowSystemLogin.Value);
    userSession.ClonedLogin(this, sessionName);
    return (IUserSession) userSession;
  }

  private void CheckDataManagerLogout(bool throwException)
  {
    if (!this.IsClosingOrDisposed)
      return;
    string str = "Попытка вызова сессии, у которой уже был вызван метод Logout()";
    this.EventLogHelper.AddToTrace(str, Consts.traceAlways, "data_manager_errors.log");
    this.EventLogHelper.AddToTrace(Environment.StackTrace, Consts.traceAlways, "data_manager_errors.log");
    if (throwException)
      throw new UserSessionProtectionException(str);
  }

  public IDbManager DataManager
  {
    get
    {
      this.CurrentOperationCancellationHandler.CheckCancellationRequested();
      this.CheckDataManagerLogout(true);
      if (this._dbManager == null)
      {
        this._dbManager = (ServerServices.GetService(typeof (IDbManagerService)) as IDbManagerService).CreateDbManager();
        ((IDbManagerOwnerControl) this._dbManager).SetOwner((object) this);
      }
      return this._dbManager;
    }
  }

  public void StartTransaction()
  {
    int num = this.DataManager.InTransaction ? 1 : 0;
    this.DataManager.BeginTransaction();
    if (num == 0)
    {
      this._StoragesList.StartTransaction();
      this._ModificationsHistoryCheckPoint = this._ModificationsHistory.Count;
    }
    (this.EventLogHelper as Intermech.Kernel.EventLogHelper).OnStartTransaction((IUserSession) this);
  }

  public void StartTransaction(IsolationLevel isoLevel)
  {
    int num = this.DataManager.InTransaction ? 1 : 0;
    this.DataManager.BeginTransaction(isoLevel);
    if (num == 0)
    {
      this._StoragesList.StartTransaction();
      this._ModificationsHistoryCheckPoint = this._ModificationsHistory.Count;
    }
    (this.EventLogHelper as Intermech.Kernel.EventLogHelper).OnStartTransaction((IUserSession) this);
  }

  internal void AddCommitedObject(DBObject obj) => this._CommitCreationObjects.Add(obj);

  private IKernelCacheSynchronizer CacheSynchronizer
  {
    get
    {
      if (this._CacheSynchronizer == null)
        this._CacheSynchronizer = ServerServices.GetService(typeof (IKernelCacheSynchronizer)) as IKernelCacheSynchronizer;
      return this._CacheSynchronizer;
    }
  }

  public void Commit()
  {
    try
    {
      if (!this.DataManager.Commit())
        return;
      this._StoragesList.Commit();
      if (this._ChangedCacheTables != null && this._ChangedCacheTables.Count > 0)
      {
        this.CacheSynchronizer?.AddEvent("0", this.DataManager);
        this._ChangedCacheTables.Clear();
      }
      this._ModificationsHistoryCheckPoint = this._ModificationsHistory.Count;
      if (this.DelayedUpdater != null)
      {
        this.EventlogCommit();
        this.AttrHistoryCommit();
        this.AttrIndexQueueCommit();
        this.DelayedNotificationsCommit();
        this.AutoSnapshotsQueueCommit();
      }
      if (this._AlreadyInCommit)
        return;
      try
      {
        this._AlreadyInCommit = true;
        try
        {
          for (int index = 0; index < this._CommitCreationObjects.Count; ++index)
            this._CommitCreationObjects[index].InternalAfterCommitCreation();
        }
        finally
        {
          this._CommitCreationObjects.Clear();
        }
        (this.EventLogHelper as Intermech.Kernel.EventLogHelper).OnCommit((IUserSession) this);
      }
      finally
      {
        this._AlreadyInCommit = false;
      }
    }
    catch
    {
      this.RollbackAllQueue();
      throw;
    }
  }

  public void Rollback()
  {
    if (this._RollbackOff)
      return;
    this._CommitCreationObjects.Clear();
    try
    {
      this.CheckDataManagerLogout(false);
      if (this._dbManager != null)
        this._dbManager.Rollback();
      this._StoragesList.Rollback();
      if (this._ChangedCacheTables != null)
      {
        this.DBCache.ReloadTables((IUserSession) null, this.DataManager, this._ChangedCacheTables.ToArray());
        this._ChangedCacheTables.Clear();
      }
    }
    finally
    {
      this.RollbackAllQueue();
    }
    (this.EventLogHelper as Intermech.Kernel.EventLogHelper).OnRollback((IUserSession) this);
  }

  private void RollbackAllQueue()
  {
    if (this._ModificationsHistory.Count > this._ModificationsHistoryCheckPoint)
    {
      int count = this._ModificationsHistory.Count - this._ModificationsHistoryCheckPoint;
      this._ModificationsHistory.RemoveRange(this._ModificationsHistory.Count - count, count);
    }
    this.EventlogRollback();
    this.AttrHistoryRollback();
    this.AttrIndexQueueRollback();
    this.AutoSnapshotsQueueRollback();
    this.DelayedNotificationsRollback();
  }

  public bool InTransaction => this._dbManager != null && this._dbManager.InTransaction;

  internal bool RollbackOff
  {
    set
    {
      if (value == this._RollbackOff)
        return;
      if (value)
      {
        this._OldTransactionState = ((IDbManagerTransactions) this.DataManager).CaptureTransactionState();
      }
      else
      {
        if (this._OldTransactionState != null)
        {
          this._OldTransactionState.Restore();
          this._OldTransactionState = (IDbManagerTransactionState) null;
        }
        if (this.DataManager.InTransaction && this.DataManager.TransactionDepth == 0)
        {
          this.DataManager.Rollback();
          throw new KernelException("Ошибка восстановления режима автоматического отката транзакций: в сессии осталась открытая транзакция, хотя глубина вложенности транзакций = 0");
        }
      }
      this._RollbackOff = value;
    }
    get => this._RollbackOff;
  }

  public bool AutoRollback
  {
    get => !this.RollbackOff;
    set => this.RollbackOff = !value;
  }

  public IServerBriefcase GetBriefcase()
  {
    if (this._ServerBriefcase == null)
      this._ServerBriefcase = this.GetCustomService(typeof (IServerBriefcase)) as IServerBriefcase;
    return this._ServerBriefcase;
  }

  public IDBImporter GetImporter(string logFileName)
  {
    return (IDBImporter) new DBImporter(this, logFileName);
  }

  public IDBRelationTypeCollection GetRelationTypeCollection(bool filterRecs)
  {
    this.CheckLogin();
    return (IDBRelationTypeCollection) new DBRelationTypeCollection(this, filterRecs);
  }

  public IDBRelationTypeCollection GetRelationTypeCollection()
  {
    return this.GetRelationTypeCollection(false);
  }

  public IDBAttributeTypeCollection GetAttributeTypeCollection(int groupID, bool filterRecs)
  {
    DBAttributeTypeCollection attributeTypeCollection = new DBAttributeTypeCollection(this, filterRecs);
    attributeTypeCollection.ParentID = (object) groupID;
    return (IDBAttributeTypeCollection) attributeTypeCollection;
  }

  public IDBAttributeTypeCollection GetAttributeTypeCollection(int groupID)
  {
    return this.GetAttributeTypeCollection(groupID, false);
  }

  public IIDHelper IdentHelper
  {
    get
    {
      if (this._IDHelper == null)
        this._IDHelper = ServerServices.GetService(typeof (IIDHelper)) as IIDHelper;
      return this._IDHelper;
    }
  }

  public ICacheDataset DBCache
  {
    get
    {
      if (this._DBCache == null)
        this._DBCache = ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset;
      return this._DBCache;
    }
  }

  internal IGlobalIndexService GlobalIndex
  {
    get
    {
      if (this._GlobalIndexService == null)
        this._GlobalIndexService = ServerServices.GetService(typeof (IGlobalIndexService)) as IGlobalIndexService;
      return this._GlobalIndexService;
    }
  }

  public IEventLog EventLog
  {
    get
    {
      if (this._EventLog == null)
        this._EventLog = (IEventLog) new Intermech.Kernel.EventLog(this, false);
      return this._EventLog;
    }
  }

  public IEventLog EventLogArchive
  {
    get
    {
      if (this._EventLogArchive == null)
        this._EventLogArchive = (IEventLog) new Intermech.Kernel.EventLog(this, true);
      return this._EventLogArchive;
    }
  }

  private IDBObject GetObjectFromSmartCache(long objectID)
  {
    if (ServerConsts.SessionSmartCacheTime == 0 || this._LastGetObject == null)
      return (IDBObject) null;
    if (this._LastGetObject.ObjectID != objectID || !(this._LastGetObject as DBObject).SmartCacheEnabled || !(DateTime.UtcNow - this._LastGetObjectTime < TimeSpan.FromSeconds((double) ServerConsts.SessionSmartCacheTime)))
      return (IDBObject) null;
    this._LastGetObjectTime = DateTime.UtcNow;
    (this._LastGetObject as DBObject).ValidationsTurnOn();
    return this._LastGetObject;
  }

  public void ClearObjectSmartCache()
  {
  }

  private void SetSmartCacheObject(IDBObject obj)
  {
    this._LastGetObject = obj;
    this._LastGetObjectTime = DateTime.UtcNow;
  }

  private IDBObjectService ObjectsService
  {
    get
    {
      if (this._ObjectsService == null)
        this._ObjectsService = ServerServices.GetService(typeof (IDBObjectService)) as IDBObjectService;
      return this._ObjectsService;
    }
  }

  public IDBObject GetObject(long objectID, bool failIfNotFound)
  {
    this.CheckLogin();
    IDBObject version = this.DBObjectsCacheGetVersion(objectID);
    if (version != null)
      return version;
    IDBObject dbObject = this.ObjectsService.GetObject((IUserSession) this, objectID, failIfNotFound, false);
    this.SetSmartCacheObject(dbObject);
    this.DBObjectsCacheAddVersion(dbObject);
    return dbObject;
  }

  public IDBObject GetObjectActualCopy(long objectID, bool failIfNotFound)
  {
    this.CheckLogin();
    IDBObject version = this.DBObjectsCacheGetVersion(objectID);
    if (version != null)
      return version;
    IDBObject dbObject = this.ObjectsService.GetObject((IUserSession) this, objectID, failIfNotFound, true);
    if (dbObject != null)
      this.SetSmartCacheObject(dbObject);
    this.DBObjectsCacheAddVersion(dbObject);
    return dbObject;
  }

  public IDBObject GetObjectActual(long objectID, bool failIfNotFound)
  {
    this.CheckLogin();
    if (this.DBObjectsCacheStarted)
    {
      IDBObject version1 = this.DBObjectsCacheGetVersion(objectID);
      if (version1 != null)
      {
        if (objectID < 0L)
        {
          if (version1.CheckoutBy == this.UserID)
            return version1;
        }
        else if (version1.CheckoutBy != this.UserID)
          return version1;
      }
      IDBObject version2 = this.DBObjectsCacheGetVersion(-objectID);
      if (version2 != null)
      {
        if (version2.ObjectID < 0L)
        {
          if (version2.CheckoutBy == this.UserID)
            return version2;
        }
        else if (version2.CheckoutBy != this.UserID)
          return version2;
      }
    }
    IDBObject objectActual = this.ObjectsService.GetObjectActual((IUserSession) this, objectID, failIfNotFound);
    this.DBObjectsCacheAddVersion(objectActual);
    return objectActual;
  }

  public ObjectFiltrationState GetObjectVersionFiltrationState(long objectID, VersionsRule rule)
  {
    QuickObjectInfo objectInfo = this.GetObjectInfo(objectID);
    if (objectInfo.Empty)
      return ObjectFiltrationState.fsVersionNotFound;
    IDBObjectCollection objectCollection = this.GetObjectCollection(objectInfo.ObjectTypeID);
    try
    {
      return objectCollection.GetObjectVersionFiltrationState(objectID, rule);
    }
    catch
    {
      return ObjectFiltrationState.fsVersionNotFound;
    }
  }

  public IDBObject[] GetObjects(long[] objectIDs, bool failIfNotFound)
  {
    this.CheckLogin();
    return this.ObjectsService.GetObjects((IUserSession) this, objectIDs, failIfNotFound);
  }

  public IDBObject GetObject(long objectID) => this.GetObject(objectID, true);

  public IDBObject GetObject(Guid objectGUID) => this.GetObject(objectGUID, true);

  public IDBObject GetObject(Guid objectGUID, bool throwNotFoundException)
  {
    this.CheckLogin();
    QuickObjectInfo objectInfo = this.DBCache.GetObjectInfo(this.DataManager, objectGUID);
    if (objectInfo.Empty)
    {
      if (throwNotFoundException)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14255(2090589640), (object) objectGUID.ToString());
      return (IDBObject) null;
    }
    IDBObject dbObject = this.GetObject(Convert.ToInt64(objectInfo.ObjectID), throwNotFoundException);
    if (dbObject != null)
    {
      if (dbObject.CheckoutBy == this.UserID && dbObject.ObjectID > 0L)
        dbObject = this.GetObject(-dbObject.ObjectID, throwNotFoundException);
      else if (dbObject.CheckoutBy != this.UserID && dbObject.ObjectID < 0L)
        dbObject = this.GetObject(Math.Abs(dbObject.ObjectID), throwNotFoundException);
    }
    return dbObject;
  }

  public IDBObject GetObjectBaseVersionByID(long id, bool throwNotFoundException)
  {
    this.CheckLogin();
    object obj = this.DataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_BASE_VERSION = 1", this.DataManager.Parameter("id1", (object) id));
    if (obj == null || obj == DBNull.Value)
    {
      if (this.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1", this.DataManager.Parameter("id1", (object) id)).Rows.Count == 0)
      {
        if (throwNotFoundException)
          throw new KernelExceptionID(sc_14238.ssp_appserver_14257(1888441883), (object) id);
        return (IDBObject) null;
      }
      if (throwNotFoundException)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14258(1090888347), (object) id);
      return (IDBObject) null;
    }
    IDBObject objectBaseVersionById = this.GetObject(Convert.ToInt64(obj), throwNotFoundException);
    if (objectBaseVersionById.CheckoutBy == this.UserID && objectBaseVersionById.ObjectID > 0L)
      objectBaseVersionById = this.GetObject(-objectBaseVersionById.ObjectID, throwNotFoundException);
    return objectBaseVersionById;
  }

  public IDBObject GetObjectByVersionsRule(
    long id,
    VersionsRule RuleClass,
    bool throwNotFoundException)
  {
    if (RuleClass == null)
    {
      if (throwNotFoundException)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14259(2085660606), (object) id);
      return (IDBObject) null;
    }
    DataTable dataTable = this.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID, F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_ID = :id", this.DataManager.Parameter(nameof (id), (object) id));
    IDBObject dbObject = (IDBObject) null;
    if (RuleClass == null)
    {
      IDBObject objectByVersionsRule = this.GetObject(Convert.ToInt64(dataTable.Rows[0]["F_OBJECT_ID"]));
      objectByVersionsRule.FiltrationState = ObjectFiltrationState.fsInvalidRule;
      return objectByVersionsRule;
    }
    int objectType = Convert.ToInt32(dataTable.Rows[0]["F_OBJECT_TYPE"]);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (Convert.ToInt32(row["F_OBJECT_TYPE"]) != objectType)
      {
        objectType = -1;
        break;
      }
    }
    IDBObjectCollection objectCollection = this.GetObjectCollection(objectType);
    (objectCollection as DBObjectCollection)._ShowPersonalObjects = true;
    if (objectCollection == null)
      return (IDBObject) null;
    ColumnDescriptor[] attrsColumns4Obj = RuleClass.GetRuleAttrsColumns4Obj(0);
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-3, RelationalOperators.Equal, (object) id, LogicalOperators.AND, 0, false)
    }, attrsColumns4Obj);
    DataTable ObjVersions;
    try
    {
      ObjVersions = objectCollection.Select(paramSet);
    }
    catch
    {
      ObjVersions = (DataTable) null;
    }
    if (ObjVersions == null || ObjVersions.Rows.Count <= 0)
    {
      if (dataTable != null && dataTable.Rows.Count == 1 && Convert.ToInt64(dataTable.Rows[0]["F_OBJECT_ID"]) < 0L)
      {
        IDBObject objectByVersionsRule = this.GetObject(Convert.ToInt64(dataTable.Rows[0]["F_OBJECT_ID"]));
        objectByVersionsRule.FiltrationState = ObjectFiltrationState.fsVersionNotFound;
        return objectByVersionsRule;
      }
      if (dbObject == null & throwNotFoundException)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14260(853625893), (object) id);
      return (IDBObject) null;
    }
    ObjectFiltrationState State;
    long objectID = RuleClass.FiltrateVersions((IUserSession) this, new Tuple<long, RequiredModes>(-1L, RequiredModes.Auto), ref ObjVersions, out State, (IServiceProvider) null);
    if (objectID != 0L)
      dbObject = this.GetObject(objectID);
    if (dbObject != null)
      dbObject.FiltrationState = State;
    if (dbObject == null && dataTable != null && dataTable.Rows.Count == 1)
    {
      IDBObject objectByVersionsRule = this.GetObject(Convert.ToInt64(dataTable.Rows[0]["F_OBJECT_ID"]), throwNotFoundException);
      if (objectByVersionsRule != null)
        objectByVersionsRule.FiltrationState = ObjectFiltrationState.fsVersionNotFound;
      return objectByVersionsRule;
    }
    return !(dbObject == null & throwNotFoundException) ? dbObject : throw new KernelExceptionID(sc_14238.ssp_appserver_14261(989734107), (object) id);
  }

  public IDBObject GetObjectByVersionsRule(
    long id,
    string FiltrationRuleSettings,
    bool throwNotFoundException)
  {
    DataTable dataTable = this.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID, F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_ID = :id", this.DataManager.Parameter(nameof (id), (object) id));
    if (dataTable.Rows.Count == 0)
    {
      if (throwNotFoundException)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14262(1141169585), (object) id);
      return (IDBObject) null;
    }
    IVersionRulesCacheService customService = this.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    FiltrationSettings filtrationSettings = customService.GetFiltrationSettings((object) this, FiltrationRuleSettings);
    bool RuleCompatible = true;
    bool RuleValid = true;
    bool VarsOutOfRange = true;
    VersionsRule filtrationRule = customService.GetFiltrationRule((object) this, (IFiltrationSettings) filtrationSettings, ref RuleCompatible, ref RuleValid, ref VarsOutOfRange);
    if (((filtrationRule == null || !RuleCompatible ? 1 : (!RuleValid ? 1 : 0)) | (VarsOutOfRange ? 1 : 0)) != 0)
    {
      if (throwNotFoundException)
        throw new KernelException($"Не удалось подобрать версию объекта {id}, так как правило подбора не валидное.");
      return (IDBObject) null;
    }
    IDBObject dbObject = (IDBObject) null;
    int objectType = Convert.ToInt32(dataTable.Rows[0]["F_OBJECT_TYPE"]);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (Convert.ToInt32(row["F_OBJECT_TYPE"]) != objectType)
      {
        objectType = -1;
        break;
      }
    }
    IDBObjectCollection objectCollection = this.GetObjectCollection(objectType);
    (objectCollection as DBObjectCollection)._ShowPersonalObjects = true;
    ColumnDescriptor[] attrsColumns4Obj = filtrationRule.GetRuleAttrsColumns4Obj(0);
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-3, RelationalOperators.Equal, (object) id, LogicalOperators.AND, 0, false)
    }, attrsColumns4Obj);
    DataTable ObjVersions;
    try
    {
      ObjVersions = objectCollection.Select(paramSet);
    }
    catch
    {
      ObjVersions = (DataTable) null;
    }
    if (ObjVersions == null || ObjVersions.Rows.Count <= 0)
    {
      if (dataTable != null && dataTable.Rows.Count == 1 && Convert.ToInt64(dataTable.Rows[0]["F_OBJECT_ID"]) < 0L)
      {
        IDBObject objectByVersionsRule = this.GetObject(Convert.ToInt64(dataTable.Rows[0]["F_OBJECT_ID"]));
        objectByVersionsRule.FiltrationState = ObjectFiltrationState.fsVersionNotFound;
        return objectByVersionsRule;
      }
      if (dbObject == null & throwNotFoundException)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14263(614881113), (object) id);
      return (IDBObject) null;
    }
    ObjectFiltrationState State;
    long objectID = filtrationRule.FiltrateVersions((IUserSession) this, new Tuple<long, RequiredModes>(-1L, RequiredModes.Auto), ref ObjVersions, out State, (IServiceProvider) null);
    if (objectID != 0L)
      dbObject = this.GetObject(objectID);
    if (dbObject != null)
      dbObject.FiltrationState = State;
    if (dbObject == null && dataTable != null && dataTable.Rows.Count == 1)
    {
      IDBObject objectByVersionsRule = this.GetObject(Convert.ToInt64(dataTable.Rows[0]["F_OBJECT_ID"]));
      objectByVersionsRule.FiltrationState = ObjectFiltrationState.fsVersionNotFound;
      return objectByVersionsRule;
    }
    return !(dbObject == null & throwNotFoundException) ? dbObject : throw new KernelException($"Не удалось выбрать одну из версий объекта {id}, так как критерии выбора в правиле подбора неоднозначны.");
  }

  public IDBObject GetObjectByVersionsRule(
    Guid guid,
    string FiltrationSettings,
    bool throwNotFoundException)
  {
    object obj = this.DataManager.ExecuteScalar("SELECT F_ID FROM IMS_GUID_RESOLVE WHERE F_GUID = :guid_par", this.DataManager.Parameter("guid_par", (object) guid));
    if (obj != null && obj != DBNull.Value)
      return this.GetObjectByVersionsRule(Convert.ToInt64(obj), FiltrationSettings, throwNotFoundException);
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14265(138005164), (object) guid.ToString());
    return (IDBObject) null;
  }

  public IDBObject GetObjectByID(long id, bool throwNotFoundException)
  {
    object obj = this.DataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id", this.DataManager.Parameter(nameof (id), (object) id));
    if (obj != null && obj != DBNull.Value)
      return this.GetObject(Convert.ToInt64(obj), throwNotFoundException);
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14266(721929688), (object) id.ToString());
    return (IDBObject) null;
  }

  public IDBObject GetObjectByID(Guid guid, bool throwNotFoundException)
  {
    object obj = this.DataManager.ExecuteScalar("SELECT F_ID FROM IMS_GUID_RESOLVE WHERE F_GUID = :guid_par", this.DataManager.Parameter("guid_par", (object) guid));
    if (obj != null && obj != DBNull.Value)
      return this.GetObjectByID(Convert.ToInt64(obj), throwNotFoundException);
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14267(762946909), (object) guid.ToString());
    return (IDBObject) null;
  }

  public IDBLifecycleLevelType GetLifecycleLevel(int aLevelID)
  {
    this.CheckLogin();
    return (IDBLifecycleLevelType) new DBLifecycleLevel(this, aLevelID);
  }

  public IDBLifecycleLevelType GetLifecycleLevel(int aLevelID, bool throwException)
  {
    this.CheckLogin();
    if (this.DBCache.GetTable("IMS_LEVELS").Rows.Find((object) aLevelID) != null)
      return this.GetLifecycleLevel(aLevelID);
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14268(1348589768), (object) aLevelID.ToString());
    return (IDBLifecycleLevelType) null;
  }

  public IDBLifecycleLevelType GetLifecycleLevel(string levelName, bool throwException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_LEVELS").Select("F_LEVEL_NAME = " + SqlHelper.QString(levelName));
    if (dataRowArray.Length != 0)
      return (IDBLifecycleLevelType) new DBLifecycleLevel(this, Convert.ToInt32(dataRowArray[0]["F_LEVEL_ID"]));
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14269(1790999704), (object) levelName);
    return (IDBLifecycleLevelType) null;
  }

  public IDBLifecycleLevelType GetLifecycleLevel(string levelName)
  {
    return this.GetLifecycleLevel(levelName, true);
  }

  public IDBLCSchema GetLCSchema(int schemaID, bool throwException)
  {
    this.CheckLogin();
    if (this.DBCache.GetTable("IMS_LC_SCHEMAS").Rows.Find((object) schemaID) != null)
      return this.GetLCSchema(schemaID);
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14270(973451772), (object) schemaID);
    return (IDBLCSchema) null;
  }

  public IDBLCSchema GetLCSchema(int schemaID)
  {
    this.CheckLogin();
    return (IDBLCSchema) new DBLCSchema(this, schemaID);
  }

  public IDBLCSchema GetLCSchema(Guid schemaGuid)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_LC_SCHEMAS").Select("F_GUID = " + SqlHelper.QString(schemaGuid.ToString()));
    return dataRowArray.Length != 0 ? this.GetLCSchema(Convert.ToInt32(dataRowArray[0]["F_SCHEMA_ID"])) : throw new KernelExceptionID(sc_14238.ssp_appserver_14271(1639698728), (object) schemaGuid.ToString());
  }

  public IDBLCSchema GetLCSchema(string schemaName) => this.GetLCSchema(schemaName, true);

  public IDBLCSchema GetLCSchema(string schemaName, bool throwException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_LC_SCHEMAS").Select("F_NAME = " + SqlHelper.QString(schemaName));
    if (dataRowArray.Length != 0)
      return this.GetLCSchema(Convert.ToInt32(dataRowArray[0]["F_SCHEMA_ID"]));
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14272(2053720867), (object) schemaName.ToString());
    return (IDBLCSchema) null;
  }

  public IDBLCSchema GetLCSchema(Guid schemaGuid, bool throwException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_LC_SCHEMAS").Select("F_GUID = " + SqlHelper.QString(schemaGuid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetLCSchema(Convert.ToInt32(dataRowArray[0]["F_SCHEMA_ID"]));
    if (throwException)
      throw new KernelExceptionID(248, (object) schemaGuid.ToString());
    return (IDBLCSchema) null;
  }

  public IDBLCSchemaCollection GetLCSchemaCollection(bool filterRecs)
  {
    this.CheckLogin();
    return (IDBLCSchemaCollection) new DBLCSchemaCollection(this, filterRecs);
  }

  public IDBLCSchemaCollection GetLCSchemaCollection() => this.GetLCSchemaCollection(false);

  public IDBLifecycleLevelType GetLifecycleLevel(Guid levelGuid)
  {
    return this.GetLifecycleLevel(levelGuid, true);
  }

  public IDBLifecycleLevelType GetLifecycleLevel(Guid levelGuid, bool throwException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_LEVELS").Select("F_GUID = " + SqlHelper.QString(levelGuid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetLifecycleLevel(Convert.ToInt32(dataRowArray[0]["F_LEVEL_ID"]));
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14273(666028243), (object) levelGuid.ToString());
    return (IDBLifecycleLevelType) null;
  }

  public IDBLifecycleLevelCollection GetLifecycleLevelCollection(bool filterRecs)
  {
    this.CheckLogin();
    return (IDBLifecycleLevelCollection) new DBLifecycleLevelCollection(this, filterRecs);
  }

  public IDBLifecycleLevelCollection GetLifecycleLevelCollection()
  {
    return this.GetLifecycleLevelCollection(false);
  }

  public IDBLanguageType GetLanguage(string aLanguageID)
  {
    this.CheckLogin();
    return (IDBLanguageType) new DBLanguageType(this, aLanguageID);
  }

  public IDBLanguageType GetLanguage(string aLanguageID, bool throwNotFoundException)
  {
    this.CheckLogin();
    if (this.DBCache.GetTable("IMS_LANGUAGES").Rows.Find((object) aLanguageID) != null)
      return this.GetLanguage(aLanguageID);
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14274(1535752483), (object) aLanguageID.ToString());
    return (IDBLanguageType) null;
  }

  public IDBLanguageType GetLanguage(Guid guid) => this.GetLanguage(guid, true);

  public IDBLanguageType GetLanguage(Guid guid, bool throwNotFoundException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_LANGUAGES").Select("F_GUID = " + SqlHelper.QString(guid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetLanguage(Convert.ToString(dataRowArray[0]["F_LANGUAGE_ID"]));
    if (throwNotFoundException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14275(1810443612), (object) guid.ToString());
    return (IDBLanguageType) null;
  }

  public IDBAttributesGroup GetAttributesGroup(int aGroupID)
  {
    this.CheckLogin();
    return (IDBAttributesGroup) new DBAttributesGroup(this, aGroupID);
  }

  public IDBAttributesGroup GetAttributesGroup(int aGroupID, bool failIfNotFound)
  {
    this.CheckLogin();
    if (this.DBCache.GetTable("IMS_ATTR_GROUPS").Rows.Find((object) aGroupID) != null)
      return this.GetAttributesGroup(aGroupID);
    if (failIfNotFound)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14276(661933041), (object) aGroupID.ToString());
    return (IDBAttributesGroup) null;
  }

  public IDBAttributesGroup GetAttributesGroup(string groupName)
  {
    return this.GetAttributesGroup(groupName, true);
  }

  public IDBAttributesGroup GetAttributesGroup(string groupName, bool failIfNotFound)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_ATTR_GROUPS").Select("F_GROUP_NAME = " + SqlHelper.QString(groupName));
    if (dataRowArray.Length != 0)
      return this.GetAttributesGroup(Convert.ToInt32(dataRowArray[0]["F_GROUP_ID"]));
    if (failIfNotFound)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14277(223790992), (object) groupName);
    return (IDBAttributesGroup) null;
  }

  public IDBAttributesGroup GetAttributesGroup(Guid guid) => this.GetAttributesGroup(guid, true);

  public IDBAttributesGroup GetAttributesGroup(Guid guid, bool failIfNotFound)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_ATTR_GROUPS").Select("F_GUID = " + SqlHelper.QString(guid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetAttributesGroup(Convert.ToInt32(dataRowArray[0]["F_GROUP_ID"]));
    if (failIfNotFound)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14278(927379529), (object) guid.ToString());
    return (IDBAttributesGroup) null;
  }

  public IDBAttributesGroupCollection GetAttributesGroupCollection(bool filterRecs)
  {
    this.CheckLogin();
    return (IDBAttributesGroupCollection) new DBAttributesGroupCollection(this, filterRecs);
  }

  public IDBAttributesGroupCollection GetAttributesGroupCollection()
  {
    return this.GetAttributesGroupCollection(false);
  }

  public IDBAttributesGroupCollection GetAttributesGroupCollection(
    int parentGroupID,
    bool filterRecs)
  {
    this.CheckLogin();
    DBAttributesGroupCollection attributesGroupCollection = new DBAttributesGroupCollection(this, filterRecs);
    attributesGroupCollection.ParentID = (object) parentGroupID;
    return (IDBAttributesGroupCollection) attributesGroupCollection;
  }

  public IDBAttributesGroupCollection GetAttributesGroupCollection(int parentGroupID)
  {
    return this.GetAttributesGroupCollection(parentGroupID, false);
  }

  private void CheckActualMetadataCache()
  {
    if (!(this._attType_CacheDate != this.DBCache.ModifyDate))
      return;
    this._attrType_Cache.Clear();
    this._ObjectTypesDict.Clear();
    this._RelationTypesDict.Clear();
    this._attType_CacheDate = this.DBCache.ModifyDate;
  }

  private IDBAttributeType CreateAttributeType(int anAttributeType, bool failIfNotFound)
  {
    this.CheckLogin();
    this.CheckActualMetadataCache();
    IDBAttributeType attributeType;
    if (this._attrType_Cache.TryGetValue(anAttributeType, out attributeType))
      return attributeType;
    IDBAttributeType dbAttributeType = this._attrType_srv.GetDBAttributeType((IUserSession) this, anAttributeType, failIfNotFound);
    this._attrType_Cache.Add(anAttributeType, dbAttributeType);
    return dbAttributeType;
  }

  public IDBAttributeType GetAttributeType(int anAttributeType)
  {
    return this.CreateAttributeType(anAttributeType, true);
  }

  public IDBAttributeType GetAttributeType(int anAttributeType, bool failIfNotFound)
  {
    return this.CreateAttributeType(anAttributeType, failIfNotFound);
  }

  public IDBAttributeType GetAttributeType(string anAttributeName)
  {
    return this.GetAttributeType(this.EventLogHelper.GetAttributeID((object) anAttributeName));
  }

  public IDBAttributeType GetAttributeType(string anAttributeName, bool failIfNotFound)
  {
    int attributeId = this.EventLogHelper.GetAttributeID((object) anAttributeName, failIfNotFound);
    return attributeId == -10000 ? (IDBAttributeType) null : this.GetAttributeType(attributeId, failIfNotFound);
  }

  public IDBAttributeType GetAttributeType(Guid anAttributeGuid, bool failIfNotFound)
  {
    int attributeId = this.EventLogHelper.GetAttributeID((object) anAttributeGuid, failIfNotFound);
    return attributeId == -10000 ? (IDBAttributeType) null : this.GetAttributeType(attributeId, failIfNotFound);
  }

  public IDBAttributeType GetAttributeType(Guid anAttributeGuid)
  {
    return this.GetAttributeType(this.EventLogHelper.GetAttributeID((object) anAttributeGuid));
  }

  public IDBConfigurations Configurations
  {
    get
    {
      if (this._DBConfigurations == null)
      {
        IDBConfigurationService service = ServerServices.GetService(typeof (IDBConfigurationService)) as IDBConfigurationService;
        this._DBConfigurations = this.ParentSession == null || this.ParentSession.SessionStatus != UserSessionStatus.Logged || this.ParentSession._DBConfigurations == null ? service.GetDBConfigurations((IUserSession) this) : service.GetDBConfigurations((IUserSession) this, this.ParentSession.Configurations);
      }
      return this._DBConfigurations;
    }
  }

  public void ReloadConfigurations()
  {
    this._DBConfigurations = (ServerServices.GetService(typeof (IDBConfigurationService)) as IDBConfigurationService).GetDBConfigurations((IUserSession) this);
  }

  public IDBLifecycleStep GetLifecycleStep(int aLCStepID, int objectTypeID)
  {
    return this.GetLifecycleStep(aLCStepID, true, objectTypeID);
  }

  public IDBLifecycleStep GetLifecycleStep(int aLCStepID)
  {
    return this.GetLifecycleStep(aLCStepID, true, 0);
  }

  public IDBLifecycleStep GetLifecycleStep(int aLCStepID, bool failIfNotFound, int objectTypeID)
  {
    this.CheckLogin();
    return !failIfNotFound && this.DBCache.GetTable("IMS_LC_STEPS").Rows.Find((object) aLCStepID) == null ? (IDBLifecycleStep) null : (IDBLifecycleStep) new DBLifecycleStep(this, aLCStepID, objectTypeID);
  }

  public IDBLifecycleStep GetLifecycleStep(int aLCStepID, bool failIfNotFound)
  {
    return this.GetLifecycleStep(aLCStepID, failIfNotFound, 0);
  }

  public IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, int objectTypeID)
  {
    return this.GetLifecycleStep(anLCGuid, true, objectTypeID);
  }

  public IDBLifecycleStep GetLifecycleStep(Guid anLCGuid)
  {
    return this.GetLifecycleStep(anLCGuid, true, 0);
  }

  public IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, bool throwException, int objectTypeID)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_LC_STEPS").Select("F_GUID = " + SqlHelper.QString(anLCGuid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetLifecycleStep(Convert.ToInt32(dataRowArray[0]["F_LC_STEP"]), objectTypeID);
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14279(1422775243), (object) anLCGuid.ToString());
    return (IDBLifecycleStep) null;
  }

  public IDBLifecycleStep GetLifecycleStep(Guid anLCGuid, bool throwException)
  {
    return this.GetLifecycleStep(anLCGuid, throwException, 0);
  }

  public IDBLifecycleStepCollection GetLifecycleStepCollection(int anObjectTypeID)
  {
    this.CheckLogin();
    return (IDBLifecycleStepCollection) new DBLifecycleStepCollection(this, this.GetLCSchema(this.GetObjectType(anObjectTypeID).SchemaID), anObjectTypeID);
  }

  public IDBLifecycleStepCollection GetLifecycleStepCollection(int schemaID, int anObjectTypeID)
  {
    this.CheckLogin();
    return (IDBLifecycleStepCollection) new DBLifecycleStepCollection(this, this.GetLCSchema(schemaID), anObjectTypeID);
  }

  public IDBObjectType GetObjectType(int anObjectTypeID)
  {
    this.CheckLogin();
    this.CheckActualMetadataCache();
    IDBObjectType objectType;
    if (!this._ObjectTypesDict.TryGetValue(anObjectTypeID, out objectType))
    {
      objectType = (IDBObjectType) new DBObjectType(this, anObjectTypeID);
      this._ObjectTypesDict.Add(anObjectTypeID, objectType);
    }
    return objectType;
  }

  public IDBObjectType GetObjectType(int anObjectTypeID, bool failIfNotFound)
  {
    this.CheckLogin();
    this.CheckActualMetadataCache();
    IDBObjectType objectType;
    if (!this._ObjectTypesDict.TryGetValue(anObjectTypeID, out objectType))
    {
      if (!failIfNotFound && this.DBCache.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) anObjectTypeID) == null)
        return (IDBObjectType) null;
      objectType = (IDBObjectType) new DBObjectType(this, anObjectTypeID);
      this._ObjectTypesDict.Add(anObjectTypeID, objectType);
    }
    return objectType;
  }

  public IDBObjectType GetObjectType(string anObjectTypeName)
  {
    return this.GetObjectType(anObjectTypeName, true);
  }

  public IDBObjectType GetObjectType(string anObjectTypeName, bool throwException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_OBJ_TYPE_NAME = " + SqlHelper.QString(anObjectTypeName));
    if (dataRowArray.Length != 0)
      return this.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]));
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14280(756337317), (object) anObjectTypeName);
    return (IDBObjectType) null;
  }

  public IDBObjectType GetObjectTypeByObjectName(string anObjectName, bool throwException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_OBJ_NAME = " + SqlHelper.QString(anObjectName));
    if (dataRowArray.Length != 0)
      return this.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]), throwException);
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14281(217894515), (object) anObjectName);
    return (IDBObjectType) null;
  }

  public IDBObjectType GetObjectType(Guid anObjectTypeGuid, bool throwException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_GUID = " + SqlHelper.QString(anObjectTypeGuid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetObjectType(Convert.ToInt32(dataRowArray[0]["F_OBJECT_TYPE"]));
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14282(106536239), (object) anObjectTypeGuid);
    return (IDBObjectType) null;
  }

  public IDBObjectType GetObjectType(Guid anObjectTypeGuid)
  {
    return this.GetObjectType(anObjectTypeGuid, true);
  }

  public List<Tuple<long, int>> GetObjectTypes(ICollection<long> objectIDs)
  {
    return SqlHelper.GetObjectTypes(objectIDs, this.DataManager);
  }

  public IDBObjectTypeCollection GetObjectTypeCollection(int parentTypeID, bool filterRecs)
  {
    this.CheckLogin();
    return (IDBObjectTypeCollection) new DBObjectTypeCollection(this, parentTypeID, filterRecs);
  }

  public IDBObjectTypeCollection GetObjectTypeCollection(int parentTypeID)
  {
    return this.GetObjectTypeCollection(parentTypeID, false);
  }

  private IDBRelationService RelationsService
  {
    get
    {
      if (this._RelationsService == null)
        this._RelationsService = ServerServices.GetService(typeof (IDBRelationService)) as IDBRelationService;
      return this._RelationsService;
    }
  }

  public IDBRelation GetRelation(Guid guid, long prjID)
  {
    this.CheckLogin();
    return this.RelationsService.GetRelation((IUserSession) this, guid, prjID);
  }

  public IDBRelation GetRelation(Guid guid, long prjID, bool failIfNotFound)
  {
    this.CheckLogin();
    return this.RelationsService.GetRelation((IUserSession) this, guid, prjID, failIfNotFound, false);
  }

  public IDBRelation GetRelation(Guid guid, bool failIfNotFound)
  {
    this.CheckLogin();
    return this.RelationsService.GetRelation((IUserSession) this, guid, -1L, failIfNotFound, true);
  }

  public IDBRelation GetRelation(long aRelationID) => this.GetRelation(aRelationID, true);

  public IDBRelation GetRelation(long aRelationID, bool failIfNotFound)
  {
    this.CheckLogin();
    return this.RelationsService.GetRelation((IUserSession) this, aRelationID, failIfNotFound);
  }

  public IDBRelation GetRelation(long projectID, long partID, int relationType, bool versionMode)
  {
    this.CheckLogin();
    long partObjectID = 0;
    if (versionMode)
    {
      partObjectID = partID;
      partID = SqlHelper.GetIDByObjectID(partID, this.DataManager);
    }
    if (this.RelationsService.GetRelation((IUserSession) this, projectID, partID, relationType, partObjectID) is DBRelation relation && partObjectID != 0L)
      relation._PartObjectID = partObjectID;
    return (IDBRelation) relation;
  }

  public IDBRelation GetRelationByPartObjectID(
    long aRelationID,
    long partObjectID,
    bool failIfNotFound)
  {
    this.CheckLogin();
    if (this.RelationsService.GetRelation((IUserSession) this, aRelationID, failIfNotFound) is DBRelation relation)
      relation._PartObjectID = partObjectID;
    return (IDBRelation) relation;
  }

  public IDBRelation GetRelation(long projectID, long partID, int relationType)
  {
    return this.GetRelation(projectID, partID, relationType, false);
  }

  public IDBRelation GetRelation(long projectID, long partID, bool versionMode)
  {
    this.CheckLogin();
    long partObjectID = 0;
    if (versionMode)
    {
      partObjectID = partID;
      partID = SqlHelper.GetIDByObjectID(partID, this.DataManager);
    }
    if (this.RelationsService.GetRelation((IUserSession) this, projectID, partID, -1, partObjectID) is DBRelation relation && partObjectID != 0L)
      relation._PartObjectID = partObjectID;
    return (IDBRelation) relation;
  }

  public IDBRelation[] GetRelations(long[] relationIDs, bool failIfNotFound)
  {
    this.CheckLogin();
    return this.RelationsService.GetRelations((IUserSession) this, relationIDs, failIfNotFound);
  }

  public IDBRelation GetRelation(DataTable tbl, int index)
  {
    this.CheckLogin();
    return this.RelationsService.GetRelation((IUserSession) this, tbl, index);
  }

  public IDBRelation GetRelation(long projectID, long partID)
  {
    return this.GetRelation(projectID, partID, false);
  }

  public IDBRelationType GetRelationType(int aRelationTypeID)
  {
    return this.GetRelationType(aRelationTypeID, true);
  }

  public IDBRelationType GetRelationType(int aRelationTypeID, bool throwException)
  {
    this.CheckLogin();
    this.CheckActualMetadataCache();
    IDBRelationType relationType;
    if (!this._RelationTypesDict.TryGetValue(aRelationTypeID, out relationType))
    {
      if (this.DBCache.GetTable("IMS_RELATION_TYPES").Rows.Find((object) aRelationTypeID) == null)
      {
        if (throwException)
          throw new KernelException(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14283()), (object) aRelationTypeID));
        return (IDBRelationType) null;
      }
      relationType = (IDBRelationType) new DBRelationType(this, aRelationTypeID);
      this._RelationTypesDict.Add(aRelationTypeID, relationType);
    }
    return relationType;
  }

  public IDBRelationType GetRelationType(Guid relationTypeGUID, bool throwException)
  {
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_RELATION_TYPES").Select("F_GUID = " + SqlHelper.QString(relationTypeGUID.ToString()));
    if (dataRowArray.Length == 0)
    {
      if (throwException)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14284(1363201842), (object) relationTypeGUID);
      return (IDBRelationType) null;
    }
    int int32 = Convert.ToInt32(dataRowArray[0]["F_RELATION_TYPE"]);
    this.CheckActualMetadataCache();
    IDBRelationType relationType;
    if (!this._RelationTypesDict.TryGetValue(int32, out relationType))
    {
      relationType = (IDBRelationType) new DBRelationType(this, int32);
      this._RelationTypesDict.Add(int32, relationType);
    }
    return relationType;
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
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_RELATION_TYPES").Select("F_DESCRIPTION = " + SqlHelper.QString(rtypeDescription));
    if (dataRowArray.Length != 0)
      return this.GetRelationType(Convert.ToInt32(dataRowArray[0]["F_RELATION_TYPE"]));
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14285(1121235116), (object) rtypeDescription);
    return (IDBRelationType) null;
  }

  public IDBSubjectAreaType GetSubjectAreaType(char aSubjectAreaTypeID)
  {
    this.CheckLogin();
    return (IDBSubjectAreaType) new DBSubjectAreaType(this, aSubjectAreaTypeID);
  }

  public IDBSubjectAreaType GetSubjectAreaType(char aSubjectAreaTypeID, bool throwException)
  {
    this.CheckLogin();
    if (this.DBCache.GetTable("IMS_SUBJECT_AREAS").Rows.Find((object) aSubjectAreaTypeID) != null)
      return this.GetSubjectAreaType(aSubjectAreaTypeID);
    if (throwException)
      throw new KernelException(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14286()), (object) aSubjectAreaTypeID));
    return (IDBSubjectAreaType) null;
  }

  public IDBSubjectAreaType GetSubjectAreaType(Guid guid) => this.GetSubjectAreaType(guid, true);

  public IDBSubjectAreaType GetSubjectAreaType(Guid guid, bool throwException)
  {
    this.CheckLogin();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_SUBJECT_AREAS").Select("F_GUID = " + SqlHelper.QString(guid.ToString()));
    if (dataRowArray.Length != 0)
      return this.GetSubjectAreaType(Convert.ToChar(dataRowArray[0]["F_AREA_ID"]));
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14287(551699186), (object) guid.ToString());
    return (IDBSubjectAreaType) null;
  }

  public IDBSubjectAreaCollection GetSubjectAreaCollection()
  {
    this.CheckLogin();
    return (IDBSubjectAreaCollection) new DBSubjectAreaCollection(this);
  }

  public IDBLanguageCollection GetLanguageCollection()
  {
    this.CheckLogin();
    return (IDBLanguageCollection) new DBLanguageCollection(this);
  }

  public IDBObjectCollection GetObjectCollection(int objectType)
  {
    this.CheckLogin();
    return (ServerServices.GetService(typeof (IDBObjectCollectionService)) as IDBObjectCollectionService).GetObjectCollection((IUserSession) this, objectType);
  }

  public IDBObjectCollection GetObjectCollection(Guid objectTypeGuid)
  {
    this.CheckLogin();
    return this.GetObjectCollection(this.IdentHelper.GetObjectTypeID(objectTypeGuid.ToString()));
  }

  public IDBRelationsApplicabilityCollection GetRelationsApplicabilityCollection()
  {
    this.CheckLogin();
    if (this._RelationsApplicabilityCollection == null)
      this._RelationsApplicabilityCollection = (IDBRelationsApplicabilityCollection) new DBRelationsApplicabilityCollection(this);
    return this._RelationsApplicabilityCollection;
  }

  public IDBRelationCollection GetRelationCollection(int relationType)
  {
    this.CheckLogin();
    return (ServerServices.GetService(typeof (IDBRelationCollectionService)) as IDBRelationCollectionService).GetRelationCollection((IUserSession) this, relationType);
  }

  public IDBRelationCollection GetRelationCollection(int relationType, string FiltrationOwnerID)
  {
    this.CheckLogin();
    return (ServerServices.GetService(typeof (IDBRelationCollectionService)) as IDBRelationCollectionService).GetRelationCollection((IUserSession) this, relationType, FiltrationOwnerID);
  }

  public IDBRelationCollection GetRelationCollection(int relationType, VersionsRule rule)
  {
    this.CheckLogin();
    return (ServerServices.GetService(typeof (IDBRelationCollectionService)) as IDBRelationCollectionService).GetRelationCollection((IUserSession) this, relationType, rule);
  }

  public IDBSnapshotCollection GetSnapshotCollection()
  {
    return (IDBSnapshotCollection) new DBSnapshotCollection(this);
  }

  public IDBObjectSnapshot GetSnapshot(long snapshotID) => this.GetSnapshot(snapshotID, true);

  public IDBObjectSnapshot GetSnapshot(long snapshotID, bool throwException)
  {
    DataTable tbl = this.DataManager.ExecuteDataTable("SELECT * FROM IMS_SNAPSHOTS WHERE F_SNAPSHOT_ID = :snapID", this.DataManager.Parameter("snapID", (object) snapshotID));
    if (tbl.Rows.Count != 0)
      return (IDBObjectSnapshot) new DBObjectSnapshot(this, snapshotID, tbl);
    if (throwException)
      throw new KernelExceptionID(sc_14238.ssp_appserver_14288(1933871207), (object) snapshotID);
    return (IDBObjectSnapshot) null;
  }

  private void LogoutIfSessionIsLost()
  {
    if (this._dbManager != null && this.InTransaction)
      this.Rollback();
    this.Logout(this._SessionName);
    this.EventLogHelper.AddToTrace($"Пользовательская сессия с SessionGUID={this.SessionGUID:N} была закрыта автоматически, так как ранее она была отключена из-за односторонней ошибки remoting на клиенте.", Consts.traceAlways, "session_management.log");
  }

  public int Logout(string sessionName)
  {
    if (this._SessionName != sessionName && sessionName != "MustCloseByKernelName")
    {
      this.EventLogHelper.AddToTrace($"Попытка вызова Logout() с именем '{sessionName}' для чужой сессии с именем '{this._SessionName}'.", "SessionsError.log");
      this.EventLogHelper.AddToTrace(Environment.StackTrace, "SessionsError.log");
      throw new UserSessionProtectionException($"Попытка вызова Logout() с именем '{sessionName}' для чужой сессии.");
    }
    if (this.IsSystemSession && this.ParentSession == null)
    {
      this.EventLogHelper.AddToTrace(string.Format("Попытка вызова Logout() с именем '{0}' для основной системной сессии.", (object) sessionName, (object) this._SessionName), "SessionsError.log");
      this.EventLogHelper.AddToTrace(Environment.StackTrace, "SessionsError.log");
      throw new UserSessionProtectionException($"Попытка вызова Logout() с именем '{sessionName}' для основной системной сессии.");
    }
    try
    {
      if (this.ParentSession == null && UserSession._Sessions.ExistsLoggedClones(this.SessionGUID))
      {
        this.SessionStatus = UserSessionStatus.Closing;
        this.DisconnectFromRemoting();
        return 0;
      }
      if (this.UserID > 0L && this.ParentSession == null)
      {
        this.EventLogHelper.CloseEvent(this._LoginEventID, EventlogRecordType.AccessGranted, "$NO$", (IUserSession) null);
        (this.EventLogHelper as Intermech.Kernel.EventLogHelper).OnLogout((IUserSession) this);
      }
      if (this._dbManager != null && this._dbManager.InTransaction)
      {
        this.RollbackOff = false;
        this.Rollback();
      }
      for (int index = this.disposableObjects.Count - 1; index >= 0; --index)
      {
        if (this.disposableObjects[index] is IDisposable disposableObject)
          disposableObject.Dispose();
      }
      if (this._dbManager != null)
      {
        ((IDbManagerOwnerControl) this._dbManager).ResetOwner((object) this);
        this._dbManager.Dispose();
      }
      this._UserID.Value = 0L;
      UserSession._Sessions.DeleteSession(this);
      if (this._LogList.Count != 0)
        this._LogList.Clear();
      lock (this._dbObjectsCacheSyncRoot)
      {
        if (this._dbObjectsCache != null)
          this._dbObjectsCache.Clear();
        this._dbObjectsCache = (Dictionary<long, IDBObject>) null;
      }
      this.SessionStatus = UserSessionStatus.Disposed;
      if (this.ParentSession != null && this.ParentSession.SessionStatus == UserSessionStatus.Closing)
        this.ParentSession.Logout("MustCloseByKernelName");
      this._DBConfigurations = (IDBConfigurations) null;
      this._EventLog = (IEventLog) null;
      this._DBSecurity.Value = (DBSecurity) null;
      this._RelationsApplicabilityCollection = (IDBRelationsApplicabilityCollection) null;
      this._QueryBuilder = (SqlBuilder) null;
      this._ServerCache = (IServerCache) null;
      this._dbManager = (IDbManager) null;
    }
    catch (Exception ex)
    {
      if (this.EventLogHelper != null)
      {
        bool flag = false;
        string str = Convert.ToString(ConfigurationManager.AppSettings.Get("LogoutErrorTrace"));
        if (str == "1" || str == "TRUE" || str == "true")
          flag = true;
        if (flag)
          this.EventLogHelper.AddToTrace(string.Format("Ошибка при вызове Logout() для сессии с именем '{0}'.{1}{2}{3}", (object) this._SessionName, (object) Environment.NewLine, (object) ExceptionServices.GetExtendedExceptionText(ex)), "SessionsLogoutError.log");
      }
    }
    this.DisconnectFromRemoting();
    return 0;
  }

  private void DisconnectFromRemoting()
  {
    try
    {
      RemotingServices.Disconnect((MarshalByRefObject) this);
    }
    catch
    {
    }
  }

  internal UserSession ParentSession
  {
    [DebuggerStepThrough] get => this._ParentSession.Value;
  }

  internal UserSessionStatus SessionStatus
  {
    [DebuggerStepThrough] get => (UserSessionStatus) this._SessionStatusCode.Value;
    [DebuggerStepThrough] private set => this._SessionStatusCode.Value = (int) value;
  }

  internal bool IsNotLogged => this.SessionStatus != UserSessionStatus.Logged;

  internal bool IsClosingOrDisposed
  {
    get
    {
      UserSessionStatus sessionStatus = this.SessionStatus;
      return sessionStatus == UserSessionStatus.Closing || sessionStatus == UserSessionStatus.Disposed;
    }
  }

  internal bool RaceSetClosingState()
  {
    int oldValue = this._SessionStatusCode.Value;
    if (oldValue == 2 || !this._SessionStatusCode.TryModify(oldValue, 2))
      return false;
    this.DisconnectFromRemoting();
    return true;
  }

  public void AddDisposableObject(object dispObject) => this.disposableObjects.Add(dispObject);

  public void RemoveDisposableObject(object dispObject)
  {
    this.disposableObjects.Remove(dispObject);
  }

  internal SessionStoragesList StoragesList
  {
    [DebuggerStepThrough] get => this._StoragesList;
  }

  public long UserStorageID
  {
    get => this.ParentSession != null ? this.ParentSession.UserStorageID : this._UserStorageID;
  }

  public long ActiveStorageID
  {
    get
    {
      if (this._ActiveStorageID.Value == 0L)
      {
        IDBAttribute attributeByGuid = this.GetObject(this.UserID).GetAttributeByGuid(new Guid("cad0005c-306c-11d8-b4e9-00304f19f545"), false);
        this._ActiveStorageID.Value = attributeByGuid == null || attributeByGuid.AsInteger <= 0L ? (ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool).GetActiveStorageID((IUserSession) this) : attributeByGuid.AsInteger;
      }
      return this._ActiveStorageID.Value;
    }
  }

  internal void RaceModifyActiveStorageID(long oldValue, long newValue)
  {
    this._ActiveStorageID.TryModify(oldValue, newValue);
  }

  internal SqlBuilder QueryBuilder
  {
    get
    {
      if (this._QueryBuilder == null)
        this._QueryBuilder = new SqlBuilder((IUserSession) this);
      return this._QueryBuilder;
    }
  }

  public IQueryBuilder GetQueryBuilder() => (IQueryBuilder) this.QueryBuilder;

  public IStringNormalizer StringNormalizer
  {
    get
    {
      if (this._StringNormalizer == null)
        this._StringNormalizer = ServerServices.GetService(typeof (IStringNormalizer)) as IStringNormalizer;
      return this._StringNormalizer;
    }
  }

  public void Test() => this.CheckLogin();

  void IReliableServerObject.KnockKnock() => this.Test();

  public RoleProperties[] GetRolesList(long userID)
  {
    if (userID == 0L)
      userID = this.UserID;
    return ServerServices.GetService(typeof (IRolesCache)) is IRolesCache service ? service.GetRolesList(userID) : throw new KernelException("Сервер приложений IPS не закончил инициализацию, поэтому вызов метода GetRolesList отклонен.");
  }

  private long GetUserIDBySID(string sid)
  {
    long userIdBySid = -1;
    int attributeId = this.IdentHelper.GetAttributeID("cadd93c1-306c-11d8-b4e9-00304f19f545");
    switch (this.DBCache.GetOptimizationMode(attributeId, this.IdentHelper.UsersTypeID, -1))
    {
      case OptimizationModes.Read:
      case OptimizationModes.Seek:
        try
        {
          object obj = this.DataManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM IMV_O{this.IdentHelper.UsersTypeID} WHERE F{attributeId} = :userSID AND F_LEVEL_ID <> {this.IdentHelper.DeletedID} AND F_OBJECT_ID > 0", this.DataManager.Parameter("userSID", (object) sid));
          if (obj != null)
          {
            if (obj != DBNull.Value)
            {
              userIdBySid = Convert.ToInt64(obj);
              break;
            }
            break;
          }
          break;
        }
        catch (Exception ex)
        {
          this.EventLogHelper.AddToTrace("GetUserIDBySID error in optimization table: " + ex.Message, Consts.traceError, string.Empty);
          break;
        }
    }
    if (userIdBySid < 0L)
    {
      object obj = this.DataManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM {this.DBCache.GetAttributesTableName(this.IdentHelper.UsersTypeID)} WHERE F_ATTRIBUTE_ID = :attrID AND F_STRING_VALUE = :userSID AND F_OBJECT_ID > 0", this.DataManager.Parameter("attrID", (object) attributeId), this.DataManager.Parameter("userSID", (object) sid));
      if (obj != null && obj != DBNull.Value)
        userIdBySid = Convert.ToInt64(obj);
    }
    return userIdBySid;
  }

  private long GetUserIDByLoginName(string loginName)
  {
    long userIdByLoginName = -1;
    if (UserSession.LoginMode == IMServerLoginMode.DomainLogin || UserSession.LoginMode == IMServerLoginMode.DomainOnlyLogin)
    {
      userIdByLoginName = this.GetUserIDBySID(loginName);
      if (userIdByLoginName != -1L)
        return userIdByLoginName;
    }
    loginName = loginName.Trim().ToUpper();
    switch (this.DBCache.GetOptimizationMode(this.IdentHelper.LoginNameID, this.IdentHelper.UsersTypeID, -1))
    {
      case OptimizationModes.Read:
      case OptimizationModes.Seek:
        try
        {
          object obj = this.DataManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM IMV_O{this.IdentHelper.UsersTypeID} WHERE F{this.IdentHelper.LoginNameID} = :loginName AND F_LEVEL_ID <> {this.IdentHelper.DeletedID} AND F_OBJECT_ID > 0", this.DataManager.Parameter(nameof (loginName), (object) loginName));
          if (obj != null)
          {
            if (obj != DBNull.Value)
            {
              userIdByLoginName = Convert.ToInt64(obj);
              break;
            }
            break;
          }
          break;
        }
        catch (Exception ex)
        {
          this.EventLogHelper.AddToTrace("GetUserIDByLoginName error in optimization table: " + ex.Message, Consts.traceError, string.Empty);
          break;
        }
    }
    if (userIdByLoginName < 0L)
    {
      object obj = this.DataManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM {this.DBCache.GetAttributesTableName(this.IdentHelper.UsersTypeID)} WHERE F_ATTRIBUTE_ID = :attrID AND F_STRING_VALUE = :loginName AND F_OBJECT_ID > 0", this.DataManager.Parameter("attrID", (object) this.IdentHelper.LoginNameID), this.DataManager.Parameter(nameof (loginName), (object) loginName));
      if (obj != null && obj != DBNull.Value)
        userIdByLoginName = Convert.ToInt64(obj);
    }
    if (userIdByLoginName <= 0L)
    {
      Thread.Sleep(this._GetRolesListDelay);
      if (this._GetRolesListDelay < 60000)
        this._GetRolesListDelay += 500;
    }
    return userIdByLoginName;
  }

  public LoginInformation GetLoginInformation(string loginName)
  {
    long userIdByLoginName = this.GetUserIDByLoginName(loginName);
    Dictionary<int, string> securityLevels = this.GetSecurityLevels(userIdByLoginName);
    return new LoginInformation(userIdByLoginName >= 0L ? this.GetRolesList(userIdByLoginName) : new RoleProperties[0], securityLevels);
  }

  public RoleProperties[] GetRolesList(string loginName)
  {
    long userIdByLoginName = this.GetUserIDByLoginName(loginName);
    return userIdByLoginName < 0L ? new RoleProperties[0] : this.GetRolesList(userIdByLoginName);
  }

  public Dictionary<int, string> GetSecurityLevels(long id)
  {
    long num = 0;
    if (id > 0L)
    {
      object obj = this.DataManager.ExecuteScalar("SELECT F_INTEGER_VALUE FROM IMS_OBJECT_ATTRS WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_ID = :objID AND F_INLIST_ID = :inlistID", this.DataManager.Parameter("attrID", (object) this.IdentHelper.SecurityLevelID), this.DataManager.Parameter("objID", (object) id), this.DataManager.Parameter("inlistID", (object) 0));
      if (obj != null && obj != DBNull.Value)
        num = Convert.ToInt64(obj);
    }
    Dictionary<int, string> securityLevels = new Dictionary<int, string>();
    DataRow[] dataRowArray = this.DBCache.GetTable("IMS_POSSIBLE_VALUES").Select($"F_ATTRIBUTE_ID = {this.IdentHelper.SecurityLevelID} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1 AND F_INTEGER_VALUE <= {num}");
    for (int index = 0; index < dataRowArray.Length; ++index)
    {
      if (Convert.ToInt32(dataRowArray[index]["F_INTEGER_VALUE"]) <= this._ClientAccessLevel)
        securityLevels.Add(Convert.ToInt32(dataRowArray[index]["F_INTEGER_VALUE"]), dataRowArray[index]["F_DESCRIPTION"].ToString());
    }
    return securityLevels;
  }

  public Dictionary<int, string> GetSecurityLevels(string loginName)
  {
    return this.GetSecurityLevels(this.GetUserIDByLoginName(loginName));
  }

  public object GetCustomService(Type serviceType)
  {
    if (serviceType == (Type) null)
      throw new ArgumentNullException(nameof (serviceType));
    this.CheckLogin();
    if (serviceType == typeof (IDBTransactions))
      return (object) this;
    return ((ICustomServices) ServerServices.GetService(typeof (ICustomServices)))?.GetService(serviceType);
  }

  public bool IsAdmin
  {
    get
    {
      this.CheckLogin();
      return this.DBSecurity.IsAdminMode;
    }
  }

  public int GetExpirationDays()
  {
    this.CheckLogin();
    return this._PasswordExpiredDays;
  }

  internal static void InitSpecialPlugins(UserSession session)
  {
    try
    {
      DBRecordSetParams paramSet = new DBRecordSetParams();
      paramSet.RecordCount = -1;
      paramSet.Columns = new object[4]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"),
        (object) new Guid("cad00127-306c-11d8-b4e9-00304f19f545"),
        (object) new Guid("cad00126-306c-11d8-b4e9-00304f19f545")
      };
      IDBObjectCollection objectCollection = session.GetObjectCollection(session.IdentHelper.PluginTypeID);
      List<Guid> guidList = new List<Guid>()
      {
        new Guid("cad014ad-306c-11d8-b4e9-00304f19f545"),
        new Guid("cad00720-306c-11d8-b4e9-00304f19f545"),
        new Guid("cad00735-306c-11d8-b4e9-00304f19f545")
      };
      if (session.IsAdmin)
        guidList.Add(new Guid("cadd9a3f-306c-11d8-b4e9-00304f19f545"));
      paramSet.Conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-12, RelationalOperators.In, (object) guidList.ToArray(), LogicalOperators.NONE, 0, true)
      };
      UserSession.SpecialsPluginsTable = objectCollection.Select(paramSet);
    }
    catch
    {
    }
  }

  public DataTable GetClientPlugins()
  {
    this.CheckLogin();
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[5]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"),
      (object) new Guid("cad00127-306c-11d8-b4e9-00304f19f545"),
      (object) new Guid("cad00126-306c-11d8-b4e9-00304f19f545"),
      (object) this.IdentHelper.FileAttributeID
    };
    IDBObjectCollection objectCollection = this.GetObjectCollection(this.IdentHelper.PluginTypeID);
    List<DataTable> tables = new List<DataTable>(2);
    if (UserSession.SpecialsPluginsTable != null)
      tables.Add(UserSession.SpecialsPluginsTable);
    ConditionStructure conditionStructure = new ConditionStructure(0, RelationalOperators.ConsistFrom, (object) this.DBSecurity._GroupsList_ID.ToArray(), LogicalOperators.NONE, 0, true);
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    tables.Add(objectCollection.Select(paramSet));
    return DataTableUtils.Merge((IList<DataTable>) tables);
  }

  public DataSet CacheDataSet => (this.DBCache as CacheDataset)._DBSet;

  public IServerCache ServerCache
  {
    get
    {
      this.CheckLogin();
      if (this._ServerCache == null)
        this._ServerCache = (IServerCache) new Intermech.Kernel.ServerCache(this);
      return this._ServerCache;
    }
  }

  public DataTable[] GetCacheTables(params string[] tableNames)
  {
    return this.ServerCache.GetTables(tableNames);
  }

  public QuickObjectInfo GetObjectInfo(long objectID)
  {
    this.CheckLogin();
    return this.DBCache.GetObjectInfo(this.DataManager, objectID);
  }

  public QuickObjectInfo GetObjectInfo(Guid objectGUID)
  {
    this.CheckLogin();
    return this.DBCache.GetObjectInfo(this.DataManager, objectGUID);
  }

  public MeasureDescriptor[] GetMeasuresList()
  {
    this.CheckLogin();
    return MeasureHelper.Measures;
  }

  public NormalizerSettings GetStringNormalizerSettings()
  {
    this.CheckLogin();
    return this.StringNormalizer.GetSettings();
  }

  public IDBAHistoryCollection GetHistoryCollection(int attributeID)
  {
    return (IDBAHistoryCollection) new DBAHistoryCollection(this, attributeID);
  }

  public IDBHistoryCollection GetHistoryCollection()
  {
    return (IDBHistoryCollection) new DBHistoryCollection(this);
  }

  public int GetObjectLevel(long objectID)
  {
    object obj = this.DataManager.ExecuteScalar("SELECT F_LEVEL_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :id", this.DataManager.Parameter("id", (object) objectID));
    return obj != null && obj != DBNull.Value ? Convert.ToInt32(obj) : -1;
  }

  public bool HasMyWorkCopy(long objectID)
  {
    object obj = this.DataManager.ExecuteScalar("SELECT F_CHKOUT_BY FROM IMS_OBJECTS WHERE F_OBJECT_ID = :obj_id", this.DataManager.Parameter("obj_id", (object) -Math.Abs(objectID)));
    return obj != null && obj != DBNull.Value && Convert.ToInt64(obj) == this.UserID;
  }

  public string[] GetCheckAccessLog(GetAccessModes mode)
  {
    int num;
    switch (mode)
    {
      case GetAccessModes.AllRecords:
        string[] array = this._LogList.ToArray();
        for (int index = 0; index < array.Length; ++index)
          array[index] = DBSessionable.AccessLogKeywords[(object) array[index][0].ToString()].ToString() + array[index].Substring(1);
        return array;
      case GetAccessModes.LastCheck:
        num = 1;
        break;
      case GetAccessModes.ServerMode:
        return this._LogList.ToArray();
      default:
        num = 10;
        break;
    }
    if (this._LogList.Count == 0)
      return new string[0];
    List<string> stringList = new List<string>();
    for (int index = this._LogList.Count - 1; index >= 0 && ((int) this._LogList[index][0] != (int) "-"[0] || --num >= 1); --index)
    {
      string log = this._LogList[index];
      string str = DBSessionable.AccessLogKeywords[(object) log[0].ToString()].ToString() + log.Substring(1);
      stringList.Insert(0, str);
    }
    return stringList.ToArray();
  }

  public bool IsStartedLogHistory
  {
    [DebuggerStepThrough] get => this._LogHistory;
  }

  public void StartLogHistory()
  {
    this._ModificationsHistory.Clear();
    this._LogHistory = true;
    this._LastModificationValue = new CategoryValue(0, 0L, ActionType.Any);
  }

  public void ResumeLogHistory() => this._LogHistory = true;

  public void StopLogHistory() => this._LogHistory = false;

  public List<CategoryValue> GetModificationsHistoryList() => this._ModificationsHistory;

  public CategoryValue[] GetModificationsHistoryArray() => this._ModificationsHistory.ToArray();

  public void AddToModificationsHistory(CategoryValue val)
  {
    if (!this._LogHistory || this._LastModificationValue.Equals((object) val))
      return;
    this._ModificationsHistory.Add(val);
    this._LastModificationValue = val;
  }

  public void AddToModificationsHistory(int categoryType, long categoryID, ActionType at)
  {
    this.AddToModificationsHistory(new CategoryValue(categoryType, categoryID, at));
  }

  [Obsolete("Do not use this method anymore", true)]
  public OperationStateInfo GetOperationInfo() => this._EmptyOperationInfo;

  public void StartCreationLog()
  {
    if (this._CreationLogMode)
      throw new KernelExceptionID(368);
    lock (this._CreationLog)
    {
      this._CreationLog.Clear();
      this._CreationLogMode = true;
      this._SuspendCreationLogMode = false;
    }
  }

  public void CommitCreationLog()
  {
    if (!this._CreationLogMode)
      throw new KernelExceptionID(369);
    lock (this._CreationLog)
    {
      this._CreationLog.Clear();
      this._CreationLogMode = false;
    }
  }

  public void RollBackCreationLog()
  {
    if (!this._CreationLogMode)
      throw new KernelExceptionID(369);
    lock (this._CreationLog)
    {
      this.PurgeByCreationModeLog();
      this._CreationLog.Clear();
      this._CreationLogMode = false;
    }
  }

  public void RollBackCreationLog(long[] purgeList)
  {
    this.StartTransaction();
    try
    {
      for (int index = 0; index < purgeList.Length; ++index)
      {
        if (this.GetObject(purgeList[index], false) is DBObject dbObject1)
          dbObject1.Purge((long) Consts.PurgeMode);
        if (this.GetObject(-purgeList[index], false) is DBObject dbObject2)
          dbObject2.Purge((long) Consts.PurgeMode);
      }
      this.Commit();
    }
    catch
    {
      this.Rollback();
      throw;
    }
  }

  private void PurgeByCreationModeLog()
  {
    this.StartTransaction();
    try
    {
      lock (this._CreationLog)
      {
        for (int index = 0; index < this._CreationLog.Count; ++index)
        {
          if (this._CreationLog[index].CategoryType == 5 && this._CreationLog[index].ActionID == ActionType.Create && this.GetRelation(this._CreationLog[index].CategoryID, false) is DBRelation relation)
            relation.Delete((long) Consts.PurgeMode);
        }
        for (int index = 0; index < this._CreationLog.Count; ++index)
        {
          if (this._CreationLog[index].CategoryType == 1 && this._CreationLog[index].ActionID == ActionType.Create)
          {
            if (this.GetObject(this._CreationLog[index].CategoryID, false) is DBObject dbObject1)
              dbObject1.Purge((long) Consts.PurgeMode);
            if (this.GetObject(-this._CreationLog[index].CategoryID, false) is DBObject dbObject2)
              dbObject2.Purge((long) Consts.PurgeMode);
          }
        }
      }
      this.Commit();
    }
    catch
    {
      this.Rollback();
      throw;
    }
  }

  public void SuspendCreationLog()
  {
    if (!this._CreationLogMode)
      throw new KernelExceptionID(369);
    this._SuspendCreationLogMode = true;
  }

  public void ResumeCreationLog()
  {
    if (!this._CreationLogMode)
      throw new KernelExceptionID(369);
    this._SuspendCreationLogMode = false;
  }

  internal void AddToCreationLog(int categoryType, long categoryID)
  {
    if (!this._CreationLogMode || this._SuspendCreationLogMode)
      return;
    lock (this._CreationLog)
      this._CreationLog.Add(new CategoryValue(categoryType, categoryID, ActionType.Create));
  }

  public bool InCreationLogMode => this._CreationLogMode;

  public CategoryValue[] GetCreationLog()
  {
    lock (this._CreationLog)
      return this._CreationLog.ToArray();
  }

  internal int GetCreationLogLength()
  {
    lock (this._CreationLog)
      return this._CreationLog.Count;
  }

  internal void SetCreationLog(CategoryValue[] logArray)
  {
    lock (this._CreationLog)
    {
      this._CreationLog.Clear();
      this._CreationLog.AddRange((IEnumerable<CategoryValue>) logArray);
    }
  }

  internal void ClearCreationLog()
  {
    lock (this._CreationLog)
      this._CreationLog.Clear();
  }

  public bool EnableEditOwnSelections
  {
    get
    {
      if (!UserSession._initedEndbleEditOwnSelections)
      {
        UserSession._endbleEditOwnSelections = this.Configurations.ReadBool("IPS.Kernel", nameof (UserSession), "EndbleEditOwnSelections", false, DBConfigMode.GlobalOnly);
        UserSession._initedEndbleEditOwnSelections = true;
      }
      return UserSession._endbleEditOwnSelections;
    }
    set
    {
      UserSession._endbleEditOwnSelections = value;
      if (!UserSession._initedEndbleEditOwnSelections)
        UserSession._initedEndbleEditOwnSelections = true;
      this.Configurations.WriteBool("IPS.Kernel", nameof (UserSession), "EndbleEditOwnSelections", UserSession._endbleEditOwnSelections, 0L);
    }
  }

  public bool EnabledPdmConfigurator
  {
    get
    {
      if (!UserSession._initedEnabledPdmConfigurator)
      {
        UserSession._enabledPdmConfigurator = this.Configurations.ReadBool("IPS.Kernel", nameof (UserSession), nameof (EnabledPdmConfigurator), true, DBConfigMode.GlobalOnly);
        UserSession._initedEnabledPdmConfigurator = true;
      }
      return UserSession._enabledPdmConfigurator;
    }
    set
    {
      UserSession._enabledPdmConfigurator = value;
      UserSession._initedEnabledPdmConfigurator = true;
      this.Configurations.WriteBool("IPS.Kernel", nameof (UserSession), nameof (EnabledPdmConfigurator), UserSession._enabledPdmConfigurator, 0L);
    }
  }

  public bool EnabledSeriesDates
  {
    get
    {
      if (!UserSession._initedEnabledSeriesDates)
      {
        UserSession._enabledSeriesDates = this.Configurations.ReadBool("IPS.Kernel", nameof (UserSession), nameof (EnabledSeriesDates), false, DBConfigMode.GlobalOnly);
        UserSession._initedEnabledSeriesDates = true;
      }
      return UserSession._enabledSeriesDates;
    }
    set
    {
      UserSession._enabledSeriesDates = value;
      UserSession._initedEnabledSeriesDates = true;
      this.Configurations.WriteBool("IPS.Kernel", nameof (UserSession), nameof (EnabledSeriesDates), UserSession._enabledSeriesDates, 0L);
    }
  }

  public bool EnabledVisibilityFiltration
  {
    get
    {
      if (!UserSession._initedVisibilityFiltration)
      {
        UserSession._enabledVisibilityFiltration = this.Configurations.ReadBool("IPS.Kernel", nameof (UserSession), nameof (EnabledVisibilityFiltration), true, DBConfigMode.GlobalOnly);
        UserSession._initedVisibilityFiltration = true;
      }
      return UserSession._enabledVisibilityFiltration;
    }
    set
    {
      UserSession._enabledVisibilityFiltration = value;
      UserSession._initedVisibilityFiltration = true;
      this.Configurations.WriteBool("IPS.Kernel", nameof (UserSession), nameof (EnabledVisibilityFiltration), UserSession._enabledVisibilityFiltration, 0L);
    }
  }

  public bool EnabledAutoSoftInstantiation
  {
    get
    {
      if (!UserSession._initedAutoSoftInstantiation)
      {
        UserSession._enabledAutoSoftInstantiation = this.Configurations.ReadBool("IPS.Kernel", nameof (UserSession), nameof (EnabledAutoSoftInstantiation), false, DBConfigMode.GlobalOnly);
        UserSession._initedAutoSoftInstantiation = true;
      }
      return UserSession._enabledAutoSoftInstantiation;
    }
    set
    {
      UserSession._enabledAutoSoftInstantiation = value;
      UserSession._initedAutoSoftInstantiation = true;
      this.Configurations.WriteBool("IPS.Kernel", nameof (UserSession), nameof (EnabledAutoSoftInstantiation), UserSession._enabledAutoSoftInstantiation, 0L);
    }
  }

  public int MaxTaskThreadsCount
  {
    get
    {
      if (!UserSession._initedMaxTaskThreadsCount)
      {
        UserSession._maxTaskThreadsCount = Convert.ToInt32(this.Configurations.ReadInteger("IPS.Kernel", nameof (UserSession), nameof (MaxTaskThreadsCount), 4L, DBConfigMode.GlobalOnly));
        UserSession._initedMaxTaskThreadsCount = true;
      }
      return UserSession._maxTaskThreadsCount;
    }
    set
    {
      if (value < 1)
        value = 1;
      UserSession._maxTaskThreadsCount = value;
      UserSession._initedMaxTaskThreadsCount = true;
      this.Configurations.WriteInteger("IPS.Kernel", nameof (UserSession), nameof (MaxTaskThreadsCount), (long) UserSession._maxTaskThreadsCount, 0L);
    }
  }

  private static IDBEditingContextsServerService EditingContextsService
  {
    get
    {
      if (UserSession._editingContextsService == null)
        UserSession._editingContextsService = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
      return UserSession._editingContextsService;
    }
  }

  public bool IsEditingContextFixed
  {
    get
    {
      CurrentEditingContext currentEditingContext = CurrentEditingContextScope.TryGet();
      return currentEditingContext != null && !currentEditingContext.IsDummy;
    }
  }

  public long EditingContextID
  {
    get
    {
      CurrentEditingContext currentEditingContext = CurrentEditingContextScope.TryGet();
      return currentEditingContext == null || currentEditingContext.IsDummy ? UserSession.EditingContextsService.GetUserContextID(this.MasterSessionGUID) : currentEditingContext.ContextID;
    }
    set
    {
      CurrentEditingContext currentEditingContext = CurrentEditingContextScope.TryGet();
      if (currentEditingContext != null && !currentEditingContext.IsDummy)
        throw new KernelException(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14290()));
      long linkedContextNumber = 0;
      if (value != 0L)
        linkedContextNumber = this.GetObject(value, false) is DBEditingContextsObject editingContextsObject ? editingContextsObject.LinkedContextNumber : 0L;
      UserSession.EditingContextsService.SetUserContextID(this.MasterSessionGUID, value, linkedContextNumber);
    }
  }

  public EditingContextSource EditingContextSource
  {
    get
    {
      if (UserSession.EditingContextsService.HasUserContextSourceInfo(this.UserID, this.RoleID))
        return UserSession.EditingContextsService.GetUserContextSource(this.UserID, this.RoleID);
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cadd9373-306c-11d8-b4e9-00304f19f545");
      if (attributeTypeId == -10000)
        return EditingContextSource.SessionContext;
      IDBAttribute attributeById = this.GetObject(this.RoleID).GetAttributeByID(attributeTypeId);
      UserSession.EditingContextsService.SetUserContextSource(this.UserID, this.RoleID, Convert.ToBoolean(attributeById != null ? attributeById.Value : (object) true) ? EditingContextSource.SessionContext : EditingContextSource.WindowContext);
      return UserSession.EditingContextsService.GetUserContextSource(this.UserID, this.RoleID);
    }
    set
    {
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cadd9373-306c-11d8-b4e9-00304f19f545");
      if (attributeTypeId == -10000)
        return;
      IDBAttribute attributeById = this.GetObject(this.RoleID).GetAttributeByID(attributeTypeId);
      if (attributeById == null)
        return;
      attributeById.Value = (object) (value == EditingContextSource.SessionContext);
      UserSession.EditingContextsService.SetUserContextSource(this.UserID, this.RoleID, value);
    }
  }

  public long EditingContextModificationID
  {
    get
    {
      CurrentEditingContext currentEditingContext = CurrentEditingContextScope.TryGet();
      return currentEditingContext == null || currentEditingContext.IsDummy ? UserSession.EditingContextsService.GetModificationID(this.MasterSessionGUID) : currentEditingContext.ModificationID;
    }
  }

  public EditingContextMode EditingContextMode
  {
    get
    {
      CurrentEditingContext currentEditingContext = CurrentEditingContextScope.TryGet();
      return currentEditingContext == null || currentEditingContext.IsDummy ? UserSession.EditingContextsService.GetUserContextMode(this.MasterSessionGUID) : currentEditingContext.ContextMode;
    }
    set
    {
      CurrentEditingContext currentEditingContext = CurrentEditingContextScope.TryGet();
      if (currentEditingContext != null && !currentEditingContext.IsDummy)
        throw new KernelException(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14291()));
      UserSession.EditingContextsService.SetUserContextMode(this.MasterSessionGUID, value);
    }
  }

  public EditingContextsObjectContainer GetEditingContext(bool withDescriptions)
  {
    return UserSession.EditingContextsService.GetEditingContextsObject((object) this, this.EditingContextID, withDescriptions, true);
  }

  public bool EnabledEditingContextsCache
  {
    get => this.enabledEditingContextsCache;
    set
    {
      if (this.enabledEditingContextsCache == value)
        return;
      UserSession.EditingContextsService.ResetCache();
      this.enabledEditingContextsCache = value;
    }
  }

  public CurrentEditingContext EditingContextGetData(Guid key)
  {
    CurrentEditingContext currentEditingContext = CurrentEditingContextScope.TryGet();
    return currentEditingContext != null && !currentEditingContext.IsDummy ? currentEditingContext : UserSession.EditingContextsService.GetUserContext(key);
  }

  public void EditingContextSetData(Guid key, CurrentEditingContext data)
  {
    CurrentEditingContext currentEditingContext = CurrentEditingContextScope.TryGet();
    if (currentEditingContext != null && !currentEditingContext.IsDummy)
      throw new KernelException(Intermech.Localization.LocalizationHolder.rm.GetString(sc_14238.ssp_appserver_14292()));
    UserSession.EditingContextsService.SetUserContext(key, data);
  }

  public long CurrentProjectID
  {
    [DebuggerStepThrough] get => this._SData.CurrentProjectID;
    set
    {
      if (this._SData.CurrentProjectID == value)
        return;
      if (value != 0L)
      {
        if (!(this.GetObject(value) is IDBProjectObject dbProjectObject))
          throw new KernelExceptionID(sc_14238.ssp_appserver_14293(954544906));
        if (!dbProjectObject.IsProjectParticipant())
          throw new KernelExceptionID(sc_14238.ssp_appserver_14294(1196159388), (object) this.UserName, (object) (dbProjectObject as IDBObject).Caption);
        if ((dbProjectObject as IDBObject).AccessLevel != this.SecurityLevel)
          throw new KernelExceptionID(sc_14238.ssp_appserver_14295(708329212), (object) (dbProjectObject as IDBObject).Caption, (object) this.DBCache.GetAccessCaption(this.SecurityLevel), (object) this.DBCache.GetAccessCaption((dbProjectObject as IDBObject).AccessLevel));
      }
      this._SData.CurrentProjectID = value;
      if (value != 0L)
        return;
      this._SData.ProjectFiltrationMode = ProjectFiltrationModes.None;
    }
  }

  public ProjectFiltrationModes ProjectFiltrationMode
  {
    [DebuggerStepThrough] get => this._SData.ProjectFiltrationMode;
    set
    {
      if ((value == ProjectFiltrationModes.CurrentProject || value == ProjectFiltrationModes.OnlyCurrentProject) && this._SData.CurrentProjectID == 0L)
        throw new KernelExceptionID(sc_14238.ssp_appserver_14296(856499206));
      this._SData.ProjectFiltrationMode = value;
    }
  }

  public int SecurityLevel
  {
    [DebuggerStepThrough] get => this._SecurityLevel;
  }

  public IDBSecurity GetSystemSecurity() => (IDBSecurity) this.DBSecurity;

  [Obsolete("This method is deprecated", true)]
  public void GetCulture(string clientCulture)
  {
    string empty = string.Empty;
    if ((!CultureInfo.CurrentUICulture.IsNeutralCulture ? CultureInfo.CurrentUICulture.Parent.Name : CultureInfo.CurrentUICulture.Name).Equals(clientCulture))
      return;
    this.EventLogHelper.AddEvent(0L, 0L, 14, 0L, Intermech.Localization.LocalizationHolder.rm.GetString("Kernel_798"), Intermech.Localization.LocalizationHolder.rm.GetString("Kernel_900"), ActionType.Login, EventlogRecordType.Error, 0L, EnvironmentConsts.MachineName, (IUserSession) null);
  }

  internal bool CanChangeObjectElement(int categoryID, object id, ObligatoryElementKey elementKey)
  {
    return this._obligatoryObjects == null || !this._obligatoryObjects.IsObligatoryObjectElement(categoryID, id, elementKey);
  }

  internal bool CanChangeObject(int categoryID, object id)
  {
    return this._obligatoryObjects == null || !this._obligatoryObjects.IsObligatoryObject(categoryID, id);
  }

  public IDBLanguageType DefaultLanguage
  {
    get
    {
      this.CheckLogin();
      DataRow[] dataRowArray = this.DBCache.GetTable("IMS_LANGUAGES").Select("F_DEFAULT = 1");
      return dataRowArray.Length == 0 ? (IDBLanguageType) null : this.GetLanguage(Convert.ToString(dataRowArray[0]["F_LANGUAGE_ID"]));
    }
  }

  long IUserSession.GetObjectF_ID(long objectID)
  {
    QuickObjectInfo objectInfo = this.GetObjectInfo(objectID);
    return objectInfo.Empty ? -1L : objectInfo.ID;
  }

  List<long> IUserSession.GetObjectVersions(long F_ID)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-3, RelationalOperators.Equal, (object) F_ID, LogicalOperators.NONE, 0, true)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
    });
    IDBObjectCollection objectCollection = this.GetObjectCollection(-1);
    objectCollection.ShowAllModifications = true;
    objectCollection.LocalTypesMode = true;
    DataTable dataTable = objectCollection.Select(paramSet);
    if (dataTable == null)
      return (List<long>) null;
    List<long> objectVersions = new List<long>();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      object obj = dataTable.Rows[index][0];
      long result;
      if (obj != null && obj != DBNull.Value && long.TryParse(obj.ToString(), out result))
        objectVersions.Add(result);
    }
    return objectVersions;
  }

  List<long> IUserSession.GetObjectVersions(long F_ID, bool includeF_ID)
  {
    List<long> objectVersions = ((IUserSession) this).GetObjectVersions(F_ID);
    if (objectVersions != null & includeF_ID)
      objectVersions.Insert(0, F_ID);
    return objectVersions;
  }

  List<long> IUserSession.GetObjectIDVersions(long objectID)
  {
    return ((IUserSession) this).GetObjectIDVersions(objectID, false);
  }

  List<long> IUserSession.GetObjectIDVersions(long objectID, bool includeF_ID)
  {
    IDBObject dbObject = this.GetObject(objectID, false);
    if (dbObject == null)
      return (List<long>) null;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-3, RelationalOperators.Equal, (object) dbObject.ID, LogicalOperators.NONE, 0, true)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
    });
    IDBObjectCollection objectCollection = this.GetObjectCollection(dbObject.ObjectType);
    objectCollection.ShowAllModifications = true;
    DataTable dataTable = objectCollection.Select(paramSet);
    if (dataTable == null)
      return (List<long>) null;
    List<long> objectIdVersions = new List<long>();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      object obj = dataTable.Rows[index][0];
      long result;
      if (obj != null && obj != DBNull.Value && long.TryParse(obj.ToString(), out result))
        objectIdVersions.Add(result);
    }
    if (includeF_ID)
      objectIdVersions.Insert(0, dbObject.ID);
    return objectIdVersions;
  }

  public DataTable GetAllObjectVersions(
    long id,
    bool isF_ID,
    bool showBlanks,
    bool showDeleted,
    params string[] columns)
  {
    IDbDataParameter dbDataParameter1 = this.DataManager.Parameter(":parID", (object) id);
    IDbDataParameter dbDataParameter2 = this.DataManager.Parameter(":parF_CHKOUT_BY", (object) this.UserID);
    IDbDataParameter dbDataParameter3 = !showBlanks ? this.DataManager.Parameter(":parF_OBJECT_VER_TYPE", (object) -1) : (IDbDataParameter) null;
    IMSLifeCycleLevel lcLevel = MetaDataHelper.GetLCLevel(new Guid("cad0000e-306c-11d8-b4e9-00304f19f545"));
    IDbDataParameter dbDataParameter4 = showDeleted || lcLevel == null ? (IDbDataParameter) null : this.DataManager.Parameter(":parF_LEVEL_ID", (object) lcLevel.LevelID);
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>(4);
    List<string> stringList = new List<string>((IEnumerable<string>) columns);
    StringBuilder cols = new StringBuilder(columns.Length == 0 ? "*" : string.Empty);
    Action<string> action = (Action<string>) (col =>
    {
      if (cols.Length == 0)
      {
        cols.Append(col);
      }
      else
      {
        cols.Append(", ");
        cols.Append(col);
      }
    });
    stringList.ForEach(action);
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.Append(string.Format("SELECT {1} FROM {0}", (object) "IMS_OBJECTS", (object) cols.ToString()));
      stringBuilder.Append(isF_ID ? $" WHERE {"F_ID"} = :parID" : string.Format(" WHERE {0} = (SELECT MIN({0}) FROM {1} WHERE ({2} = :parID))", (object) "F_ID", (object) "IMS_OBJECTS", (object) "F_OBJECT_ID"));
      dbDataParameterList.Add(dbDataParameter1);
      stringBuilder.Append(string.Format(" AND (({0} > 0 AND {1} <> :parF_CHKOUT_BY) OR ({0} < 0 AND {1} = :parF_CHKOUT_BY))", (object) "F_OBJECT_ID", (object) "F_CHKOUT_BY"));
      dbDataParameterList.Add(dbDataParameter2);
      if (!showBlanks)
      {
        stringBuilder.Append($" AND ({"F_OBJECT_VER_TYPE"} <> :parF_OBJECT_VER_TYPE)");
        dbDataParameterList.Add(dbDataParameter3);
      }
      if (!showDeleted)
      {
        stringBuilder.Append($" AND ({"F_LEVEL_ID"} <> :parF_LEVEL_ID)");
        dbDataParameterList.Add(dbDataParameter4);
      }
      return this.DataManager.ExecuteDataTable(stringBuilder.ToString(), dbDataParameterList.ToArray());
    }
  }

  public List<long> GetAllObjectVersionsList(
    long id,
    bool isF_ID,
    bool showBlanks,
    bool showDeleted)
  {
    DataTable allObjectVersions = this.GetAllObjectVersions(id, (isF_ID ? 1 : 0) != 0, (showBlanks ? 1 : 0) != 0, (showDeleted ? 1 : 0) != 0, new string[2]
    {
      "F_OBJECT_ID",
      "F_OBJECT_TYPE"
    });
    List<long> objectVersionsList = new List<long>(allObjectVersions != null ? allObjectVersions.Rows.Count : 0);
    if (allObjectVersions != null)
    {
      for (int index = 0; index < allObjectVersions.Rows.Count; ++index)
      {
        long int64Value = DataSetProcessor.GetInt64Value(allObjectVersions.Rows[index], "F_OBJECT_ID", 0L);
        if (int64Value != 0L && objectVersionsList.IndexOf(int64Value) < 0)
          objectVersionsList.Add(int64Value);
      }
    }
    return objectVersionsList;
  }

  private void DBObjectsCacheSetLock()
  {
  }

  private void DBObjectsCacheReleaseLock()
  {
  }

  private void DBObjectsCacheCheckLock()
  {
  }

  public bool DBObjectsCacheStarted
  {
    get
    {
      lock (this._dbObjectsCacheSyncRoot)
        return this._dbObjectsCacheSyncCount > 0;
    }
  }

  public void DBObjectsCacheStart()
  {
    lock (this._dbObjectsCacheSyncRoot)
    {
      this.DBObjectsCacheSetLock();
      if (this._dbObjectsCacheSyncCount <= 0)
        this._dbObjectsCache = new Dictionary<long, IDBObject>();
      ++this._dbObjectsCacheSyncCount;
    }
  }

  public void DBObjectsCacheStop()
  {
    lock (this._dbObjectsCacheSyncRoot)
    {
      --this._dbObjectsCacheSyncCount;
      if (this._dbObjectsCacheSyncCount > 0)
        return;
      this._dbObjectsCache = (Dictionary<long, IDBObject>) null;
      this.DBObjectsCacheReleaseLock();
    }
  }

  public void DBObjectsCacheClear()
  {
    lock (this._dbObjectsCacheSyncRoot)
    {
      this.DBObjectsCacheCheckLock();
      if (this._dbObjectsCache == null)
        return;
      this._dbObjectsCache.Clear();
    }
  }

  public void DBObjectsCacheRemoveVersion(long fObjectID)
  {
    lock (this._dbObjectsCacheSyncRoot)
    {
      this.DBObjectsCacheCheckLock();
      if (this._dbObjectsCache == null || !this._dbObjectsCache.ContainsKey(fObjectID))
        return;
      this._dbObjectsCache.Remove(fObjectID);
    }
  }

  public void DBObjectsCacheAddVersion(IDBObject dbObject)
  {
    lock (this._dbObjectsCacheSyncRoot)
    {
      this.DBObjectsCacheCheckLock();
      if (this._dbObjectsCache == null || dbObject == null)
        return;
      this._dbObjectsCache[dbObject.ObjectID] = dbObject;
    }
  }

  private IDBObject DBObjectsCacheGetVersion(long fObjectID)
  {
    lock (this._dbObjectsCacheSyncRoot)
    {
      this.DBObjectsCacheCheckLock();
      if (this._dbObjectsCache == null)
        return (IDBObject) null;
      if (this._dbObjectsCache.ContainsKey(fObjectID))
      {
        IDBObject version = this._dbObjectsCache[fObjectID];
        if (!(version as DBObject).Deleted)
          return version;
        this._dbObjectsCache.Remove(fObjectID);
        return (IDBObject) null;
      }
    }
    return (IDBObject) null;
  }

  public int AlgorithmVersion => 2;

  public long MetaDataGeneration
  {
    get
    {
      return !(ServerServices.GetService(typeof (MetaDataHelperUpdateService)) is MetaDataHelperUpdateService service) ? 0L : service._generation;
    }
  }

  public PswPackage NewPassword
  {
    [DebuggerStepThrough] private get => this._newPassword;
    set => this._newPassword = value;
  }

  internal PswPackage Password => this._password;

  internal string LoginName => this._loginName;

  public object GetSessionPluginsData(object key)
  {
    if (key == null)
      return (object) null;
    lock (this._pluginsData.SyncRoot)
      return this._pluginsData[key];
  }

  public void SetSessionPluginsData(object key, object value)
  {
    if (key == null)
      return;
    lock (this._pluginsData.SyncRoot)
      this._pluginsData[key] = value;
  }

  public void RemoveSessionPluginsData(object key)
  {
    if (key == null)
      return;
    lock (this._pluginsData.SyncRoot)
    {
      if (!this._pluginsData.Contains(key))
        return;
      this._pluginsData.Remove(key);
    }
  }

  public UserLoginEvents GetUserLoginEvents()
  {
    UserLoginEvents userLoginEvents = new UserLoginEvents();
    object obj = this.DataManager.ExecuteScalar("SELECT F_BEGIN_DATE FROM IMS_EVENTLOG WHERE F_EVENT_ID = :eventID ", this.DataManager.Parameter("eventID", (object) this._LoginEventID));
    if (obj != null && obj != DBNull.Value)
    {
      userLoginEvents.CurrentLoginDateTime = Convert.ToDateTime(obj);
      userLoginEvents.PrevLoginDateTime = Convert.ToDateTime(obj);
    }
    return userLoginEvents;
  }

  public long InternalDepartmentID
  {
    get
    {
      object sessionPluginsData = this.GetSessionPluginsData((object) "DEPARTMENT_ID");
      return sessionPluginsData == null ? 0L : Convert.ToInt64(sessionPluginsData);
    }
  }

  [Obsolete("Use the method AddToTrace instead of this.", true)]
  public void AddToServerTrace(string text, string traceFileName = null)
  {
    this.AddToTrace(text, Consts.traceAlways, traceFileName);
  }

  public void AddToTrace(string text, int traceLevel, string traceFileName = null)
  {
    this.EventLogHelper.AddToTrace(text, traceLevel, traceFileName, this.ComputerName, this.UserName);
  }

  public List<ActingUserLoginSettings> GetActingUserLoginSettings(long actingUserID)
  {
    if (this._LastActingUserLoginSettings != null)
      return this._LastActingUserLoginSettings;
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (GetActingUserLoginSettings));
    List<ActingUserLoginSettings> userLoginSettings1;
    try
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[5]
      {
        new ColumnDescriptor((object) -2),
        new ColumnDescriptor((object) new Guid("cadd94e6-306c-11d8-b4e9-00304f19f545"), ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0),
        new ColumnDescriptor((object) new Guid("cadd94e4-306c-11d8-b4e9-00304f19f545")),
        new ColumnDescriptor((object) new Guid("cadd94e3-306c-11d8-b4e9-00304f19f545")),
        new ColumnDescriptor((object) new Guid("cadd94e6-306c-11d8-b4e9-00304f19f545"), ColumnContents.String, ColumnNameMapping.Index, SortOrders.NONE, 0)
      };
      DataTable dataTable = sessionTemporaryClone.GetObjectCollection(new Guid("cadd94e2-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cadd91f5-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) actingUserID, LogicalOperators.NONE, 0)
      }, columns));
      userLoginSettings1 = new List<ActingUserLoginSettings>(dataTable.Rows.Count);
      DateTime now = DateTime.Now;
      for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
      {
        DataRow row = dataTable.Rows[index1];
        DateTime dateTime1 = DateTime.MinValue;
        DateTime dateTime2 = DateTime.MaxValue;
        if (row[2] != DBNull.Value)
          dateTime1 = Convert.ToDateTime(row[2]);
        if (row[3] != DBNull.Value)
          dateTime2 = Convert.ToDateTime(row[3]) + TimeSpan.FromDays(1.0);
        if (now >= dateTime1 && now <= dateTime2)
        {
          ActingUserLoginSettings userLoginSettings2 = new ActingUserLoginSettings();
          userLoginSettings2.ActingUserID = actingUserID;
          IDBObject dbObject = sessionTemporaryClone.GetObject(Convert.ToInt64(row[0]));
          if (row[1] != DBNull.Value)
          {
            userLoginSettings2.RoleID = Convert.ToInt64(row[1]);
            userLoginSettings2.RoleName = row[4].ToString();
          }
          Guid attributeGuid = new Guid("cad015c9-306c-11d8-b4e9-00304f19f545");
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(attributeGuid, true);
          object[] values = attributeByGuid.Values;
          string[] descriptions = attributeByGuid.Descriptions;
          userLoginSettings2.Users = new Dictionary<long, string>(values.Length);
          for (int index2 = 0; index2 < values.Length; ++index2)
          {
            if (values[index2] != null && values[index2] != DBNull.Value)
              userLoginSettings2.Users.Add(Convert.ToInt64(values[index2]), descriptions[index2]);
          }
          userLoginSettings1.Add(userLoginSettings2);
        }
      }
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (GetActingUserLoginSettings));
    }
    this._LastActingUserLoginSettings = userLoginSettings1;
    return userLoginSettings1;
  }

  public bool IsDelayedEventlog
  {
    get
    {
      if (!UserSession._initedDelayedEventlog)
      {
        if (!(ServerServices.GetService(typeof (IDelayedUpdaterService)) is DelayedUpdaterService))
          return false;
        UserSession._IsDelayedEventlog = this.Configurations.ReadBool("IPS.Kernel", nameof (UserSession), "DelayedEventlog", false, DBConfigMode.GlobalOnly);
        UserSession._initedDelayedEventlog = true;
      }
      return UserSession._IsDelayedEventlog;
    }
    set
    {
      UserSession._initedDelayedEventlog = true;
      UserSession._IsDelayedEventlog = value;
      this.Configurations.WriteBool("IPS.Kernel", nameof (UserSession), "DelayedEventlog", value, 0L);
    }
  }

  public long AddEvent(EventlogProperties props)
  {
    long num;
    if (this.InTransaction)
    {
      num = this.DelayedUpdater.NextEventID;
      props.EventID = num;
      this.eventlogList.Add(props);
    }
    else
      num = this.DelayedUpdater.AddEvent(props);
    return num;
  }

  public long CloseEvent(EventlogProperties props)
  {
    bool flag = false;
    long num = props.EventID;
    for (int index = this.eventlogList.Count - 1; index >= 0; --index)
    {
      EventlogProperties eventlog = this.eventlogList[index];
      if (eventlog.EventID == props.EventID)
      {
        eventlog.CloseEvent(props);
        flag = true;
        break;
      }
    }
    if (!flag)
    {
      if (this.InTransaction)
        this.eventlogList.Add(props);
      else
        num = this.DelayedUpdater.CloseEvent(props);
    }
    return num;
  }

  private void EventlogCommit()
  {
    this.DelayedUpdater.AddEvents(this.eventlogList);
    this.eventlogList.Clear();
  }

  private void EventlogRollback() => this.eventlogList.Clear();

  public bool IsDelayedAttrHistory
  {
    get
    {
      if (!UserSession._initedDelayedAttrHistory)
      {
        UserSession._IsDelayedAttrHistory = this.Configurations.ReadBool("IPS.Kernel", nameof (UserSession), "DelayedAttrHistory", false, DBConfigMode.GlobalOnly);
        UserSession._initedDelayedAttrHistory = true;
      }
      return UserSession._IsDelayedAttrHistory;
    }
    set
    {
      UserSession._initedDelayedAttrHistory = true;
      UserSession._IsDelayedAttrHistory = value;
      this.Configurations.WriteBool("IPS.Kernel", nameof (UserSession), "DelayedAttrHistory", value, 0L);
    }
  }

  private void InitAttrHistory() => this.attrHistoryList = new List<AttrHistoryProperties>();

  public void AddAttrHistory(AttrHistoryProperties attrProps)
  {
    if (this.InTransaction)
      this.attrHistoryList.Add(attrProps);
    else
      this.DelayedUpdater.AddAttrHistory(attrProps);
  }

  private void AttrHistoryCommit()
  {
    this.DelayedUpdater.AddAttrHistory(this.attrHistoryList);
    this.attrHistoryList.Clear();
  }

  private void AttrHistoryRollback() => this.attrHistoryList.Clear();

  internal void AddAttrToIndexQueue(string newValue, IDBAttribute attr)
  {
    IndexQueueProperties attrProps = new IndexQueueProperties(newValue, attr);
    if (this.InTransaction)
      this.attrIndexQueue.Add(attrProps);
    else
      this.DelayedUpdater.AddAttrToIndexQueue(attrProps);
  }

  internal void AddAttrToIndexQueue(
    long objectID,
    int attrID,
    int inlistID,
    long id,
    string text,
    AttributeOptions options,
    FieldTypes dataType)
  {
    IndexQueueProperties attrProps = new IndexQueueProperties(objectID, attrID, inlistID, id, text, options, dataType);
    if (this.InTransaction)
      this.attrIndexQueue.Add(attrProps);
    else
      this.DelayedUpdater.AddAttrToIndexQueue(attrProps);
  }

  internal void CheckOutToIndexQueue(long objectID)
  {
    IndexQueueProperties attrProps = new IndexQueueProperties(objectID, ActionType.CheckOut);
    if (this.InTransaction)
      this.attrIndexQueue.Add(attrProps);
    else
      this.DelayedUpdater.AddAttrToIndexQueue(attrProps);
  }

  internal void DeleteFromIndexQueue(long objectID)
  {
    for (int index = this.attrIndexQueue.Count - 1; index >= 0; --index)
    {
      if (this.attrIndexQueue[index].ObjectID == objectID)
        this.attrIndexQueue.RemoveAt(index);
    }
  }

  internal void ReplaceSignIndexQueue(long objectID)
  {
    for (int index = 0; index < this.attrIndexQueue.Count; ++index)
    {
      if (this.attrIndexQueue[index].ObjectID == objectID)
        this.attrIndexQueue[index].ObjectID = -objectID;
    }
  }

  internal void CheckInIndexQueue(long objectID)
  {
    IndexQueueProperties attrProps = new IndexQueueProperties(objectID, ActionType.CheckIn);
    if (this.InTransaction)
      this.attrIndexQueue.Add(attrProps);
    else
      this.DelayedUpdater.AddAttrToIndexQueue(attrProps);
  }

  private void AttrIndexQueueCommit()
  {
    if (this.attrIndexQueue.Count <= 0)
      return;
    this.DelayedUpdater.AddAttrToIndexQueue(this.attrIndexQueue);
    this.attrIndexQueue.Clear();
  }

  private void AttrIndexQueueRollback() => this.attrIndexQueue.Clear();

  internal void AddAutoSnaphotToQueue(long objectID)
  {
    if (this.InTransaction)
    {
      if (this.autoSnapshotsList.IndexOf(objectID) >= 0)
        return;
      this.autoSnapshotsList.Add(objectID);
    }
    else
      this.DelayedUpdater.AddToAutoSnapshotsQueue(objectID);
  }

  private void AutoSnapshotsQueueCommit()
  {
    this.DelayedUpdater.AddToAutoSnapshotsQueue(this.autoSnapshotsList);
    this.autoSnapshotsList.Clear();
  }

  private void AutoSnapshotsQueueRollback() => this.autoSnapshotsList.Clear();

  public void AddDelayedNotification(DelayedNotification notify)
  {
    if (this.InTransaction)
      this._DelayedNotificationsList.Add(notify);
    else
      this.DelayedUpdater.AddDelayedNotification(notify);
  }

  private void DelayedNotificationsCommit()
  {
    if (this._DelayedNotificationsList.Count <= 0)
      return;
    this.DelayedUpdater.AddDelayedNotifications(this._DelayedNotificationsList.ToArray());
    this._DelayedNotificationsList.Clear();
  }

  private void DelayedNotificationsRollback() => this._DelayedNotificationsList.Clear();

  public bool SendAttrs2DelayedNotificationMode
  {
    get
    {
      wfConsts.SendAttrs2DelayedNotificationMode = ServerConsts.SendAttrs2DelayedNotificationMode;
      return ServerConsts.SendAttrs2DelayedNotificationMode;
    }
    set
    {
      if (value == ServerConsts.SendAttrs2DelayedNotificationMode)
        return;
      this.Configurations.WriteBool("KERNEL", "COMMON", "COPY_ATTRS2NOTIF", value, 0L);
      wfConsts.SendAttrs2DelayedNotificationMode = value;
      ServerConsts.SendAttrs2DelayedNotificationMode = value;
    }
  }

  public bool AllVersionsAnnulmentMode
  {
    get => ServerConsts.AnnulAllVersions;
    set
    {
      if (value == ServerConsts.AnnulAllVersions)
        return;
      this.Configurations.WriteBool("KERNEL", "COMMON", "ANNUL_ALL_VERSIONS", value, 0L);
      ServerConsts.AnnulAllVersions = value;
    }
  }

  public long GetIDByObjectID(long objectID)
  {
    return SqlHelper.GetIDByObjectID(objectID, this.DataManager);
  }

  internal NotifySamplesProcessor RaceGetCurrentNSProcessor() => this._NSProcessor.Value;

  public INotifySamplesProcessor GetNotifySamplesProcessor()
  {
    NotifySamplesProcessor samplesProcessor = this._NSProcessor.Value;
    if (samplesProcessor == null)
    {
      samplesProcessor = new NotifySamplesProcessor(this);
      this._NSProcessor.Value = samplesProcessor;
    }
    return (INotifySamplesProcessor) samplesProcessor;
  }

  internal void SetNotifySamplesUpdateFlag()
  {
    UserSession._Sessions.SetNotifySamlpesIsModifiedFlag(this.MasterSessionGUID);
  }

  public long[] GetUserGroupsAndRoleID() => this.DBSecurity.GetGroupsList();

  public void CheckClientBackwardConnectivity(IMClientLiveStatus testObject)
  {
    if (testObject == null)
      throw new ArgumentNullException(nameof (testObject));
    try
    {
      testObject.KnockKnock();
    }
    catch (Exception ex)
    {
      throw new KernelException("Сервер приложений IPS не имеет возможности выполнять обратные сетевые обращения к клиенту IPS. Из-за этого работоспособность спонсоров Remoting может быть нарушена.", ex);
    }
  }

  internal RemovableObjects RemovableObjectsList
  {
    get
    {
      if (this._RemovableObjects == null)
        this._RemovableObjects = new RemovableObjects();
      return this._RemovableObjects;
    }
  }

  public void BeginDeleteObjects(IEnumerable<long> objectIDs)
  {
    this.RemovableObjectsList.StartRemoveObjects(objectIDs);
  }

  public void EndDeleteObjects() => this.RemovableObjectsList.Clear();

  public DataTable GetObjectVersionsTree(long id)
  {
    return this.DataManager.ExecuteDataTable("select IMS_VERSIONS_TREE.* from IMS_VERSIONS_TREE, IMS_OBJECTS where IMS_OBJECTS.F_ID = :id1 AND IMS_VERSIONS_TREE.F_OBJECT_ID = IMS_OBJECTS.F_OBJECT_ID", this.DataManager.Parameter("id1", (object) id));
  }

  public UserAndRoleInfo GetUserAndRoleInfo()
  {
    UserAndRoleInfo userAndRoleInfo = new UserAndRoleInfo();
    QuickObjectInfo objectInfo1 = this.GetObjectInfo(new Guid("cad00693-306c-11d8-b4e9-00304f19f545"));
    userAndRoleInfo.RoleDefaultObjectID = !objectInfo1.Empty ? objectInfo1.ObjectID : -1L;
    QuickObjectInfo objectInfo2 = this.GetObjectInfo(this.UserID);
    userAndRoleInfo.UserGuid = objectInfo2.VersionGuid;
    IDBObject dbObject = this.GetObject(this.RoleID);
    userAndRoleInfo.RoleGuid = dbObject.ObjectGUID;
    IDBAttribute attributeById = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545"));
    object obj = attributeById != null ? attributeById.Value : (object) userAndRoleInfo.RoleDefaultObjectID;
    if (obj != null && !obj.Equals((object) DBNull.Value))
    {
      userAndRoleInfo.Rule = new CompositionsAutosortRule();
      userAndRoleInfo.Rule.Load((IUserSession) this, Convert.ToInt64(obj), false);
    }
    userAndRoleInfo.MaxRows = this.MaxRows;
    userAndRoleInfo.ID = this.ID;
    return userAndRoleInfo;
  }

  internal ThreadedAccessWrapper GetThreadedAccessWrapper()
  {
    ThreadedAccessWrapper threadedAccessWrapper = this._ThreadedAccessWrapper.Value;
    if (threadedAccessWrapper == null)
    {
      this._ThreadedAccessWrapper.TryModify((ThreadedAccessWrapper) null, new ThreadedAccessWrapper(this));
      threadedAccessWrapper = this._ThreadedAccessWrapper.Value;
    }
    return threadedAccessWrapper;
  }

  public IDBAttribute GetRelationAttribute(
    long relationID,
    object attributeID,
    bool failIfNotFound)
  {
    IDBAttribute relationAttribute = (IDBAttribute) null;
    IDBRelation relation = this.GetRelation(relationID, failIfNotFound);
    if (relation != null)
    {
      switch (attributeID)
      {
        case int _:
          relationAttribute = relation.GetAttributeByID(Convert.ToInt32(attributeID));
          break;
        case Guid attributeGuid:
          relationAttribute = relation.GetAttributeByGuid(attributeGuid, failIfNotFound);
          break;
        default:
          relationAttribute = relation.GetAttributeByName(attributeID.ToString(), failIfNotFound);
          break;
      }
      if (relationAttribute == null & failIfNotFound)
        throw new AttributeNotFoundException(attributeID.ToString(), string.Empty, relationID);
    }
    return relationAttribute;
  }

  public IDBAttribute GetRelationAttributeByID(long relationID, int attributeID)
  {
    return this.GetRelationAttribute(relationID, (object) attributeID, false);
  }

  public IDBAttribute GetRelationAttributeByGuid(long relationID, Guid attributeGUID)
  {
    return this.GetRelationAttribute(relationID, (object) attributeGUID, false);
  }

  public AttributeValues[] GetRelationAttributesValues(
    long relationID,
    GetAttributeValuesModes modes,
    bool failIfNotFound)
  {
    IDBRelation relation = this.GetRelation(relationID, failIfNotFound);
    return relation == null ? new AttributeValues[0] : relation.GetAttributesValues(modes);
  }

  public DataTable RelationsSelect(int relationTypeID, DBRecordSetParams dbRecordSetParams)
  {
    return this.GetRelationCollection(relationTypeID).Select(dbRecordSetParams);
  }

  public IDBAttribute GetObjectAttribute(
    long objectID,
    object attributeID,
    bool failIfNotFound,
    bool getActualCopy)
  {
    IDBAttribute objectAttribute = (IDBAttribute) null;
    IDBObject dbObject = !getActualCopy ? this.GetObject(objectID, failIfNotFound) : this.GetObjectActual(objectID, failIfNotFound);
    if (dbObject != null)
    {
      switch (attributeID)
      {
        case int _:
          objectAttribute = dbObject.GetAttributeByID(Convert.ToInt32(attributeID));
          break;
        case Guid attributeGuid:
          objectAttribute = dbObject.GetAttributeByGuid(attributeGuid, failIfNotFound);
          break;
        default:
          objectAttribute = dbObject.GetAttributeByName(attributeID.ToString(), failIfNotFound);
          break;
      }
      if (objectAttribute == null & failIfNotFound)
        throw new AttributeNotFoundException(attributeID.ToString(), string.Empty, objectID);
    }
    return objectAttribute;
  }

  public IDBAttribute GetObjectAttributeByID(long objectID, int attributeID)
  {
    return this.GetObjectAttribute(objectID, (object) attributeID, false, false);
  }

  public IDBAttribute GetObjectAttributeByGuid(long objectID, Guid attributeGUID)
  {
    return this.GetObjectAttribute(objectID, (object) attributeGUID, false, false);
  }

  public object[] GetObjectAttributeValuesByGuid(long objectID, Guid attributeGUID)
  {
    return this.GetObjectAttributeByGuid(objectID, attributeGUID)?.Values;
  }

  public object GetObjectAttributeValueByGuid(long objectID, Guid attributeGUID)
  {
    return this.GetObjectAttributeByGuid(objectID, attributeGUID)?.Value;
  }

  public AttributeValues[] GetObjectAttributesValues(
    long objectID,
    GetAttributeValuesModes modes,
    bool failIfNotFound,
    bool getActualCopy)
  {
    IDBObject dbObject = !getActualCopy ? this.GetObject(objectID, failIfNotFound) : this.GetObjectActual(objectID, failIfNotFound);
    return dbObject == null ? new AttributeValues[0] : dbObject.GetAttributesValues(modes);
  }

  public DataTable ObjectsSelect(Guid objectTypeGuid, DBRecordSetParams dbRecordSetParams)
  {
    return this.GetObjectCollection(objectTypeGuid).Select(dbRecordSetParams);
  }

  public DataTable ObjectsSelect(int objectTypeID, DBRecordSetParams dbRecordSetParams)
  {
    return this.GetObjectCollection(objectTypeID).Select(dbRecordSetParams);
  }

  public ObjectSystemProperties GetObjectSystemProperties(
    long objectID,
    bool failIfNotFound,
    bool getActualCopy)
  {
    IDBObject dBObject = !getActualCopy ? this.GetObject(objectID, failIfNotFound) : this.GetObjectActual(objectID, failIfNotFound);
    return dBObject != null ? new ObjectSystemProperties(dBObject) : (ObjectSystemProperties) null;
  }

  public ObjectSystemProperties GetObjectSystemProperties(Guid objectGuid, bool failIfNotFound)
  {
    IDBObject dBObject = this.GetObject(objectGuid, failIfNotFound);
    return dBObject != null ? new ObjectSystemProperties(dBObject) : (ObjectSystemProperties) null;
  }

  public ObjectSystemPropertiesEx GetObjectSystemPropertiesEx(long objectID, bool failIfNotFound)
  {
    IDBObject dBObject = this.GetObject(objectID, failIfNotFound);
    return dBObject != null ? new ObjectSystemPropertiesEx(dBObject) : (ObjectSystemPropertiesEx) null;
  }

  public ObjectSystemPropertiesEx GetObjectSystemPropertiesEx(Guid objectGuid, bool failIfNotFound)
  {
    IDBObject dBObject = this.GetObject(objectGuid, failIfNotFound);
    return dBObject != null ? new ObjectSystemPropertiesEx(dBObject) : (ObjectSystemPropertiesEx) null;
  }

  public IDBAttribute AddObjectAttribute(
    long objectID,
    int attributeID,
    bool failIfNotFound,
    bool failIfExists,
    object[] initValues)
  {
    return this.GetObject(objectID, failIfNotFound)?.Attributes.AddAttribute(attributeID, failIfExists, initValues);
  }

  public IDBAttribute AddRelationAttribute(
    long relationID,
    int attributeID,
    bool failIfNotFound,
    bool failIfExists,
    object[] initValues)
  {
    return this.GetRelation(relationID, failIfNotFound)?.Attributes.AddAttribute(attributeID, failIfExists, initValues);
  }

  public void SetObjectAttributesValues(
    long objectID,
    bool failIfNotFound,
    AttributeValues[] attributeValues)
  {
    this.GetObject(objectID, failIfNotFound)?.SetAttributesValues(attributeValues);
  }

  public void SetRelationAttributesValues(
    long relationID,
    bool failIfNotFound,
    AttributeValues[] attributeValues)
  {
    this.GetRelation(relationID, failIfNotFound)?.SetAttributesValues(attributeValues);
  }

  public long CheckOutCommand(long objectID) => this.GetObject(objectID).CheckOut().ObjectID;

  public long CheckInCommand(long objectID, bool preserveWorkingCopies)
  {
    IDBObject dbObject = this.GetObject(objectID);
    if (preserveWorkingCopies)
      dbObject.SaveToArcCopy();
    else
      dbObject.CheckIn();
    return dbObject.ObjectID;
  }

  private AttributeValues[] GetAttributableAttributes(
    IDBAttributable attributable,
    int[] attributesID,
    GetAttributeValuesModes modes)
  {
    AttributeValues[] attributableAttributes = new AttributeValues[attributesID.Length];
    foreach (AttributeValues attributesValue in attributable.GetAttributesValues(modes))
    {
      for (int index = 0; index < attributesID.Length; ++index)
      {
        if (attributesValue.AttributeID == attributesID[index])
        {
          attributableAttributes[index] = attributesValue;
          break;
        }
      }
    }
    return attributableAttributes;
  }

  public AttributeValues[] GetObjectAttributesValues(
    long objectID,
    int[] attributesID,
    GetAttributeValuesModes modes,
    bool failIfNotFound)
  {
    IDBAttributable attributable = (IDBAttributable) this.GetObject(objectID, failIfNotFound);
    return attributable != null ? this.GetAttributableAttributes(attributable, attributesID, modes) : (AttributeValues[]) null;
  }

  public AttributeValues[] GetRelationAttributesValues(
    long relationID,
    int[] attributesID,
    GetAttributeValuesModes modes,
    bool failIfNotFound)
  {
    IDBAttributable relation = (IDBAttributable) this.GetRelation(relationID, failIfNotFound);
    return relation != null ? this.GetAttributableAttributes(relation, attributesID, modes) : (AttributeValues[]) null;
  }

  public void SetClientAccessLevel(int clientAccessLevel, string machineMame)
  {
    if (!this.DBCache.AccessLevelExists(clientAccessLevel))
      this.EventLogHelper.AddToTrace($"Попытка указать несуществующий уровень доступа {clientAccessLevel} для сессии с устройства {machineMame}.");
    this._ClientAccessLevel = clientAccessLevel;
  }

  private sealed class SessionLockInfo
  {
    public readonly int ThreadID;
    public readonly string StackTrace;
    public string ConflictedStackTrace;

    public SessionLockInfo(int threadId, string stackTrace)
    {
      this.ThreadID = threadId;
      this.StackTrace = stackTrace;
      this.ConflictedStackTrace = (string) null;
    }
  }
}
