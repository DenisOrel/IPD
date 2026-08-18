
// Type: Intermech.Navigator.CurrentUserAndRole
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Projects;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.Search.Navigator;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Класс, помогающий определять идентификаторы текущего пользователя и роли
/// </summary>
public sealed class CurrentUserAndRole : ICurrentUserAndRole
{
  /// <summary>Объект для синхронизации</summary>
  private object _syncRoot = new object();
  /// <summary>Guid панели инструментов "Контекст редактирования"</summary>
  private Guid _contextToolbarGuid = new Guid("7e41d6d7-f8e4-4809-b69a-09b9706dffef");
  /// <summary>Идентификатор текущего пользователя</summary>
  private long _userID = -1;
  /// <summary>Идентификатор объекта-пользователя (IDBObject.ID)</summary>
  private long _ID = -1;
  /// <summary>Идентификатор текущей роли</summary>
  private long _roleID = -1;
  /// <summary>
  /// Обладает ли текущий пользователь правами администратора
  /// </summary>
  private bool _isAdmin;
  /// <summary>Имя текущего пользователя</summary>
  private string _userName = string.Empty;
  /// <summary>Guid текущего пользователя</summary>
  private Guid _userGuid = Guid.Empty;
  /// <summary>Guid текущей роли</summary>
  private Guid _roleGuid = Guid.Empty;
  /// <summary>Текущее правило по сортировке и отображению составов</summary>
  private CompositionsAutosortRule _rule = new CompositionsAutosortRule();
  /// <summary>Идентификатор настроек роли по умолчанию</summary>
  private long _roleDefaultObjectID = -1;
  /// <summary>Идентификатор типа атрибута "Конфигурации ролей"</summary>
  private int _roleConfigAttr = -1;
  /// <summary>
  /// Идентификатор типа атрибута "Глобальный контекст редактирования"
  /// </summary>
  private int _roleGlobalContextAttr = -1;
  /// <summary>Идентификатор атрибута "Настройки видов Навигатора"</summary>
  private int _navViewsAttr = -1;
  /// <summary>Надо ли блокировать настройку видимости закладок</summary>
  private bool _blockedViews;
  /// <summary>Надо ли блокировать настройку контекстных меню</summary>
  private bool _blockedMenus;
  /// <summary>
  /// Надо ли блокировать настройку отображения узлов с составами
  /// </summary>
  private bool _blockedCompositions;
  /// <summary>
  /// Надо ли блокировать скрытие панелей инструментов для составов
  /// </summary>
  private bool _blockedToolbars;
  /// <summary>Была ли зачитана информация из данного атрибута</summary>
  private bool _blockedToolbarsReaded;
  /// <summary>
  /// Идентификатор текущего выбранного пользователем проекта. Это значение используется для инициализации
  /// каждого создаваемого SessionKeeper
  /// </summary>
  private long activeProjectId;
  /// <summary>
  /// Идентификатор текущего выбранного режима фильтрования объектов из разных проектов. Также используется
  /// для инициализации SessionKeeper
  /// </summary>
  private ProjectFiltrationModes activeFiltrationMode;
  /// <summary>Режим работы без диалога с пользователем</summary>
  private bool _silentMode;
  /// <summary>Настройки видов "Навигатора" для текущей роли</summary>
  private Dictionary<NavigatorColumnsKey, NavigatorColumns> _roleNavStreams;
  /// <summary>Флаг того, что текущий IPS Client работает с порталом</summary>
  private bool _portalClient;
  /// <summary>Выполняется ли обработка события</summary>
  private bool _inEvents;
  /// <summary>
  /// Количество блокировок изменения текущего контекста редактирования
  /// </summary>
  private long _lockEditingContextID;
  /// <summary>
  /// Кэшированная информация о текущем контексте редактирования
  /// </summary>
  private CurrentEditingContext _cachedEditingContext = CurrentEditingContext.Empty;
  /// <summary>
  /// Идентификатор типа объекта текущего контекста редактирования
  /// </summary>
  private int _cachedEditingContextTypeID = -1;
  /// <summary>Режим работы контекста редактирования</summary>
  private EditingContextSource _cachedEditingContextSource = EditingContextSource.SessionContext;
  /// <summary>Служба уведомлений</summary>
  private INotificationService _service;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  private NotificationEventHandler _notifyHandler;
  /// <summary>
  /// Был ли задан вопрос при первом выборе контекста редактирования
  /// </summary>
  private bool _autoEditModeQuestionFired;
  /// <summary>Сервис фильтрации составов</summary>
  private IFiltrationService _filtrationService;
  private TimeSpan EnabledPdmConfiguratorSynchronizationInterval = new TimeSpan(12, 0, 0);
  private ColumnPack _defaultColumnPack;
  private bool _isDeafultColumnPackLoaded;
  private bool _enabledPdmConfigurator;
  private DateTime _enabledPdmConfiguratorLastSynchronizationTime;

  /// <summary>Количество записей в пакете</summary>
  public int MaxRows { get; private set; }

  /// <summary>Режим разработчика</summary>
  public bool DeveloperMode { get; private set; }

  /// <summary>Создает объект.</summary>
  public CurrentUserAndRole()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.activeProjectId = sessionKeeper.Session.CurrentProjectID;
      this.activeFiltrationMode = sessionKeeper.Session.ProjectFiltrationMode;
      this._navViewsAttr = MetaDataHelper.GetAttributeTypeID("cad01487-306c-11d8-b4e9-00304f19f545");
    }
    int editingContextSource = (int) this.EditingContextSource;
    long editingContextId = this.EditingContextID;
    this._service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
    this._service.Subscribe(this._notifyHandler);
    if (!(ServicesManager.GetService(typeof (IUserSessionPool)) is IUserSessionPool service))
      return;
    service.MainSessionCreated += new EventHandler<UserSessionCreatedEventArgs>(CurrentUserAndRole.RefreshEditingContextAfterReconnect);
  }

  /// <summary>Перечитывает размер пакета</summary>
  public void ReloadMaxRows()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.MaxRows = sessionKeeper.Session.MaxRows;
  }

  /// <summary>Сервис фильтрации составов</summary>
  private IFiltrationService FiltrationService
  {
    get
    {
      if (this._filtrationService != null)
        return this._filtrationService;
      this._filtrationService = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
      return this._filtrationService;
    }
  }

  /// <summary>Флаг того, что текущий IPS Client работает с порталом</summary>
  public bool PortalClient
  {
    get => this._portalClient;
    set => this._portalClient = value;
  }

  /// <summary>Идентификатор текущего пользователя</summary>
  public long UserID
  {
    get
    {
      if (this._userID == -1L)
      {
        this._roleConfigAttr = this._roleConfigAttr < 0 ? MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545") : this._roleConfigAttr;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this._userID = sessionKeeper.Session.UserID;
          this._roleID = sessionKeeper.Session.RoleID;
          this._isAdmin = sessionKeeper.Session.IsAdmin;
          this._userName = sessionKeeper.Session.UserName;
          this.DeveloperMode = sessionKeeper.Session.DeveloperMode;
          UserAndRoleInfo userAndRoleInfo = sessionKeeper.Session.GetUserAndRoleInfo();
          this._roleDefaultObjectID = userAndRoleInfo.RoleDefaultObjectID;
          lock (this._syncRoot)
          {
            this.MaxRows = userAndRoleInfo.MaxRows;
            this._ID = userAndRoleInfo.ID;
            this._userGuid = userAndRoleInfo.UserGuid;
            this._roleGuid = userAndRoleInfo.RoleGuid;
            if (userAndRoleInfo.Rule != null)
            {
              this._rule = userAndRoleInfo.Rule.Clone() as CompositionsAutosortRule;
            }
            else
            {
              this._rule.Clear();
              this._rule.ObjectID = -1L;
            }
          }
        }
      }
      return this._userID;
    }
  }

  /// <summary>Имя текущего пользователя</summary>
  public string UserName
  {
    get
    {
      if (string.IsNullOrEmpty(this._userName))
      {
        long userId = this.UserID;
      }
      return this._userName;
    }
  }

  /// <summary>Guid текущего пользователя</summary>
  public Guid UserGuid
  {
    get
    {
      if (this._userGuid == Guid.Empty)
      {
        long userId = this.UserID;
      }
      return this._userGuid;
    }
  }

  /// <summary>ID текущего пользователя</summary>
  public long ID
  {
    get
    {
      if (this._ID == -1L)
      {
        long userId = this.UserID;
      }
      return this._ID;
    }
  }

  /// <summary>Идентификатор текущей роли</summary>
  public long RoleID
  {
    get
    {
      if (this._roleID == -1L)
      {
        long userId = this.UserID;
      }
      return this._roleID;
    }
  }

  /// <summary>Guid текущей роли</summary>
  public Guid RoleGuid
  {
    get
    {
      if (this._roleGuid == Guid.Empty)
      {
        long userId = this.UserID;
      }
      return this._roleGuid;
    }
  }

  /// <summary>
  /// Обладает ли текущий пользователь правами администратора
  /// </summary>
  public bool IsAdmin
  {
    get
    {
      if (this._roleID == -1L)
      {
        long userId = this.UserID;
      }
      return this._isAdmin;
    }
  }

  /// <summary>
  /// Уникальный идентификатор клиентского подключения к серверу приложений.
  /// Идентификатор присваивается сервером приложений при создании первой сессии клиента.
  /// Все сессии одного клиента будут иметь один и тот же идентификатор клиентского подключения;
  /// два разных клиента, вошедших под одним и тем же пользователем IPS, будут иметь разные идентификаторы.
  /// </summary>
  public long ClientConnectionID
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.ClientConnectionID;
    }
  }

  /// <summary>
  /// Свойство, позволяющее читать/сохранять информацию о контексте редактирования без рассылки уведомлений
  /// в Навигатор. Обращений к серверу приложений нет, вся информация записывается во внутренние кэши, чтение
  ///  также выполняется из кэша
  /// </summary>
  private CurrentEditingContext InternalCurrentContext
  {
    get
    {
      if (this.CachedEditingContextSource == EditingContextSource.SessionContext)
        return this._cachedEditingContext;
      return this.FiltrationService == null || this.FiltrationService.Filtration == null ? CurrentEditingContext.Empty : this.FiltrationService.Filtration.EditingContext;
    }
    set
    {
      if (this.CachedEditingContextSource == EditingContextSource.WindowContext && this.FiltrationService != null && this.FiltrationService.Filtration != null)
        this.FiltrationService.Filtration.EditingContext = value;
      else
        this._cachedEditingContext = value;
    }
  }

  /// <summary>
  /// Проверить права доступа к контексту, если текущий режим доступа - автообновление контекста
  /// </summary>
  private void CheckEditingContextAccessRights()
  {
    if (this._inEvents)
      return;
    try
    {
      this._inEvents = true;
      bool flag = false;
      if (this.InternalCurrentContext.ContextMode != EditingContextMode.AutoUpdate)
        return;
      long contextId = this.InternalCurrentContext.ContextID;
      if (contextId == 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetObjectActualCopy(contextId, false) is IDBEditingContextsObject objectActualCopy))
          objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(-contextId, false) as IDBEditingContextsObject;
        if (objectActualCopy == null || !this.IsToolbarVisible(this._contextToolbarGuid))
        {
          this._inEvents = false;
          this.CachedContextMode = EditingContextMode.Default;
          this.EditingContextID = 0L;
          return;
        }
        lock (this._syncRoot)
          this._cachedEditingContextTypeID = objectActualCopy.ObjectType;
        switch (objectActualCopy.ObjectModifyMode)
        {
          case ObjectModifyModes.Checkout:
            if (objectActualCopy.CheckoutBy == 0L)
            {
              DialogResult dialogResult = DialogResult.No;
              if (!this.SilentMode)
                dialogResult = IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1477"), LocalizationHolder.rm.GetString("Client.Core_1478") + LocalizationHolder.rm.GetString("Client.Core_1479") + LocalizationHolder.rm.GetString("Client.Core_1480") + LocalizationHolder.rm.GetString("Client.Core_1481"), new IMMessageBoxButton[2]
                {
                  new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1482"), DialogResult.Yes),
                  new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1483"), DialogResult.No)
                }, IMMessageBoxImage.Question);
              if (dialogResult != DialogResult.Yes)
              {
                this.EditingContextMode = EditingContextMode.Default;
                return;
              }
              long objectId = objectActualCopy.ObjectID;
              long num1 = objectActualCopy.CheckOut() is IDBEditingContextsObject editingContextsObject ? editingContextsObject.ObjectID : objectId;
              if (objectId != num1)
              {
                List<long> objectIDs = new List<long>(1);
                objectIDs.Add(objectId);
                List<long> newObjectIDs = new List<long>(1);
                newObjectIDs.Add(num1);
                this._inEvents = false;
                (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
                return;
              }
              this.EditingContextMode = EditingContextMode.Default;
              if (this.SilentMode)
                return;
              int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1484"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              return;
            }
            if (objectActualCopy.CheckoutBy == this.UserID)
              return;
            this.EditingContextMode = EditingContextMode.Default;
            if (this.SilentMode)
              return;
            IUserNamesCache userNamesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
            int num3 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1485"), (object) userNamesCache.GetUserName(objectActualCopy.CheckoutBy)), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
          case ObjectModifyModes.CreateVersion:
            this.EditingContextMode = EditingContextMode.Default;
            if (this.SilentMode)
              return;
            int num4 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1476"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
          case ObjectModifyModes.CantModify:
            this.EditingContextMode = EditingContextMode.Default;
            if (this.SilentMode)
              return;
            int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1474") + LocalizationHolder.rm.GetString("Client.Core_1475"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
          default:
            flag = !(objectActualCopy as IDBSecurity).CheckAccess(ActionType.Edit, true, false);
            if (flag)
            {
              this.EditingContextMode = EditingContextMode.Default;
              break;
            }
            break;
        }
      }
      if (flag)
      {
        if (this.SilentMode)
          return;
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1486") + LocalizationHolder.rm.GetString("Client.Core_1487"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        if (!this.IsECOEditingContext)
          return;
        using (new SessionKeeper())
          this.EditingContextMode = EditingContextMode.AutoUpdate;
      }
    }
    finally
    {
      this._inEvents = false;
    }
  }

  /// <summary>
  /// Можно ли выбрать режим автоматического пополнения для указанного контекста редактирования
  /// </summary>
  /// <param name="contextID">Идентификатор версии объекта с контекстом</param>
  /// <returns>true - можно включить режим автоматического пополнения указанного контекста редактирования</returns>
  public CanSetContextModeCode CanSetContextAutoUpdateMode(long contextID)
  {
    if (contextID == 0L)
      return CanSetContextModeCode.UnknownContext;
    if (!this.IsToolbarVisible(this._contextToolbarGuid))
      return CanSetContextModeCode.ContextToolbarDisabled;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObjectActualCopy(contextID, false) is IDBEditingContextsObject objectActualCopy))
        objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(-contextID, false) as IDBEditingContextsObject;
      if (objectActualCopy == null)
        return CanSetContextModeCode.UnknownContext;
      switch (objectActualCopy.ObjectModifyMode)
      {
        case ObjectModifyModes.Checkout:
          return objectActualCopy.CheckoutBy == 0L || objectActualCopy.CheckoutBy == this.UserID ? CanSetContextModeCode.CanSetAutoUpdate : CanSetContextModeCode.CheckedOutByOtherUser;
        case ObjectModifyModes.CreateVersion:
          return CanSetContextModeCode.ModifyByCreateVersion;
        case ObjectModifyModes.CantModify:
          return CanSetContextModeCode.CantModifyObject;
        default:
          return !(objectActualCopy as IDBSecurity).CheckAccess(ActionType.Edit, true, false) ? CanSetContextModeCode.ReadOnlyByAccessRights : CanSetContextModeCode.CanSetAutoUpdate;
      }
    }
  }

  /// <summary>
  /// Можно ли оставить режим автоматического пополнения для указанного контекста редактирования
  /// </summary>
  /// <param name="contextID">Идентификатор версии объекта с контекстом</param>
  /// <returns>true - можно оставить режим автоматического пополнения указанного контекста редактирования</returns>
  public bool CanLeaveContextAutoUpdateMode(long contextID)
  {
    if (contextID == 0L || !this.IsToolbarVisible(this._contextToolbarGuid))
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObjectActualCopy(contextID, false) is IDBEditingContextsObject objectActualCopy))
        objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(-contextID, false) as IDBEditingContextsObject;
      if (objectActualCopy == null)
        return false;
      switch (objectActualCopy.ObjectModifyMode)
      {
        case ObjectModifyModes.Checkout:
          return objectActualCopy.CheckoutBy == this.UserID;
        case ObjectModifyModes.CreateVersion:
          return false;
        case ObjectModifyModes.CantModify:
          return false;
        default:
          return (objectActualCopy as IDBSecurity).CheckAccess(ActionType.Edit, true, false);
      }
    }
  }

  private static void RefreshEditingContextAfterReconnect(
    object sender,
    UserSessionCreatedEventArgs e)
  {
    ((ICurrentUserAndRole) ServicesManager.GetService(typeof (ICurrentUserAndRole))).RefreshEditingContext();
  }

  /// <summary>
  /// Метод позволяет обновить контекст редактирования в случае переподключения клиентской программы к серверу приложений и прочих ситуациях
  /// </summary>
  public void RefreshEditingContext() => this.EditingContextID = this.CachedEditingContextID;

  /// <summary>
  /// Источник информации о текущем контексте редактирования (глобальный, оконный)
  /// </summary>
  public EditingContextSource EditingContextSource
  {
    get
    {
      this._roleGlobalContextAttr = this._roleGlobalContextAttr < 0 ? MetaDataHelper.GetAttributeTypeID("cadd9373-306c-11d8-b4e9-00304f19f545") : this._roleGlobalContextAttr;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this.RoleID, this._roleGlobalContextAttr);
        this.CachedEditingContextSource = Convert.ToBoolean(objectAttributeById != null ? objectAttributeById.Value : (object) false) ? EditingContextSource.SessionContext : EditingContextSource.WindowContext;
        return this.CachedEditingContextSource;
      }
    }
    set
    {
      this._roleGlobalContextAttr = this._roleGlobalContextAttr < 0 ? MetaDataHelper.GetAttributeTypeID("cadd9373-306c-11d8-b4e9-00304f19f545") : this._roleGlobalContextAttr;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._roleID, this._roleGlobalContextAttr);
        if (objectAttributeById == null)
          return;
        objectAttributeById.Value = (object) (value == EditingContextSource.SessionContext);
        this.CachedEditingContextSource = value;
      }
    }
  }

  /// <summary>
  /// Идентификатор текущего контекста редактирования (глобально для всех сессий пользователя, значение читается из сессии)
  /// </summary>
  public long EditingContextID
  {
    get
    {
      if (this.CachedEditingContextSource == EditingContextSource.SessionContext)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this.InternalCurrentContext = sessionKeeper.Session.EditingContextGetData(sessionKeeper.Session.MasterSessionGUID);
          this._cachedEditingContextTypeID = sessionKeeper.Session.GetObjectInfo(this.CachedEditingContextID).ObjectTypeID;
        }
      }
      return this.InternalCurrentContext.ContextID;
    }
    set
    {
      if (this.LockEditingContextID || this.InternalCurrentContext.ContextID == value)
        return;
      bool flag1 = this.CachedContextMode == EditingContextMode.AutoUpdate && this.CanLeaveContextAutoUpdateMode(value);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.EditingContextID = value;
        if (this.CachedContextMode == EditingContextMode.AutoUpdate && !flag1)
          sessionKeeper.Session.EditingContextMode = EditingContextMode.Default;
        this.InternalCurrentContext = sessionKeeper.Session.EditingContextGetData(sessionKeeper.Session.MasterSessionGUID);
        this._cachedEditingContextTypeID = sessionKeeper.Session.GetObjectInfo(this.CachedEditingContextID).ObjectTypeID;
      }
      bool flag2 = false;
      if (((this._autoEditModeQuestionFired ? 0 : (!this._silentMode ? 1 : 0)) & (flag1 ? 1 : 0)) != 0 && this.InternalCurrentContext.ContextID != 0L && this.InternalCurrentContext.ContextMode != EditingContextMode.AutoUpdate)
      {
        if (this.CanSetContextAutoUpdateMode(this.InternalCurrentContext.ContextID) == CanSetContextModeCode.CanSetAutoUpdate)
        {
          string str = string.Empty;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this.InternalCurrentContext.ContextID);
            this._cachedEditingContextTypeID = objectInfo.ObjectTypeID;
            str = objectInfo.Caption;
          }
          if (this.IsECOEditingContext || IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1467"), LocalizationHolder.rm.GetString("Client.Core_1488") + string.Format("[{1}] \"{0}\" ?", (object) str, (object) this.InternalCurrentContext.ContextID), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
          {
            this.CachedContextMode = EditingContextMode.AutoUpdate;
            this.ReplaceEditingContext(new CurrentEditingContext(this.CachedEditingContextID, this.CachedEditingContextModificationID, this.CachedContextMode));
          }
        }
        this._autoEditModeQuestionFired = true;
      }
      if (!flag2)
        this.FiltrationService.FiltrationApplyUpdates(true);
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, new NotificationEventArgs("EditingContextChanged"));
    }
  }

  /// <summary>
  /// Проверить, является ли текущий контекст редактирования извещением об изменении
  /// </summary>
  public bool IsECOEditingContext
  {
    get
    {
      lock (this._syncRoot)
        return MetaDataHelper.IsObjectTypeChildOf(this._cachedEditingContextTypeID, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
    }
  }

  /// <summary>
  /// Метод позволяет передать информацию о текущем контексте редактирования на сервер приложений.
  /// В кэш ничего не записывается, из кэша ничего не читается
  /// </summary>
  public void ReplaceEditingContext(CurrentEditingContext editingContext)
  {
    if (editingContext == null)
      throw new ArgumentNullException(nameof (editingContext));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.EditingContextSetData(sessionKeeper.Session.MasterSessionGUID, editingContext);
  }

  /// <summary>
  /// Заблокировано ли изменение текущего контекста редактирования
  /// </summary>
  public bool LockEditingContextID
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this._lockEditingContextID > 0L;
    }
    set
    {
      lock (this._syncRoot)
      {
        if (value)
          ++this._lockEditingContextID;
        else
          --this._lockEditingContextID;
        if (this._lockEditingContextID < 0L)
          this._lockEditingContextID = 0L;
        this.EnableToolbar(this._contextToolbarGuid, this._lockEditingContextID == 0L);
      }
    }
  }

  /// <summary>Управление состоянием панели инструментов</summary>
  /// <param name="tbGuid">Guid панели</param>
  /// <param name="enabled">Требуемое состояние панели</param>
  private void EnableToolbar(Guid tbGuid, bool enabled)
  {
    if (!(ServicesManager.GetService(typeof (BarManager)) is BarManager service) || !(tbGuid != Guid.Empty))
      return;
    Intermech.Bars.ToolBar toolbar = service.FindToolbar(tbGuid);
    if (toolbar == null || toolbar.Enabled == enabled)
      return;
    toolbar.Enabled = enabled;
  }

  /// <summary>
  /// Видна ли панель инструментов "Контекст редактирования"
  /// </summary>
  public bool IsContextToolbarVisible
  {
    [DebuggerStepThrough] get => this.IsToolbarVisible(this._contextToolbarGuid);
  }

  /// <summary>Проверить видимость панели инструментов</summary>
  /// <param name="tbGuid">Guid панели</param>
  /// <returns>true - панель видима</returns>
  private bool IsToolbarVisible(Guid tbGuid)
  {
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service && tbGuid != Guid.Empty)
    {
      Intermech.Bars.ToolBar toolbar = service.FindToolbar(tbGuid);
      if (toolbar != null)
        return toolbar.Visible;
    }
    return false;
  }

  /// <summary>
  /// Режим работы контекста редактирования - глобальный, оконный (кэшированное значение)
  /// </summary>
  public EditingContextSource CachedEditingContextSource
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this._cachedEditingContextSource;
    }
    set
    {
      lock (this._syncRoot)
        this._cachedEditingContextSource = value;
    }
  }

  /// <summary>
  /// Идентификатор текущего контекста редактирования (кэшированное значение)
  /// </summary>
  public long CachedEditingContextID
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this.InternalCurrentContext.ContextID;
    }
    set
    {
      if (this.LockEditingContextID || this.InternalCurrentContext.ContextID == value)
        return;
      lock (this._syncRoot)
      {
        if (this.CachedEditingContextSource == EditingContextSource.SessionContext)
        {
          this._cachedEditingContext = this._cachedEditingContext.WithContextID(value);
          this.InternalCurrentContext = this._cachedEditingContext;
        }
        else
        {
          this.InternalCurrentContext = new CurrentEditingContext(value, this.InternalCurrentContext.ModificationID, this.InternalCurrentContext.ContextMode);
          this.FiltrationService.FiltrationApplyUpdates(true);
        }
      }
    }
  }

  /// <summary>
  /// Номер группы изменений текущего контекста редактирования (кэшированное значение)
  /// </summary>
  public long CachedEditingContextModificationID
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this.InternalCurrentContext.ModificationID;
    }
    private set
    {
      if (this.InternalCurrentContext.ModificationID == value)
        return;
      lock (this._syncRoot)
      {
        if (this.CachedEditingContextSource == EditingContextSource.SessionContext)
        {
          this._cachedEditingContext = this._cachedEditingContext.WithModificationID(value);
          this.InternalCurrentContext = this._cachedEditingContext;
        }
        else
        {
          this.InternalCurrentContext = new CurrentEditingContext(this.InternalCurrentContext.ContextID, value, this.InternalCurrentContext.ContextMode);
          this.FiltrationService.FiltrationApplyUpdates(true);
        }
      }
    }
  }

  /// <summary>
  /// Режим автопополнения контекста редактирования (кэшированное значение)
  /// </summary>
  public EditingContextMode CachedContextMode
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this.InternalCurrentContext.ContextMode;
    }
    set
    {
      lock (this._syncRoot)
      {
        if (!this.IsContextToolbarVisible)
          value = EditingContextMode.Default;
        if (this.InternalCurrentContext.ContextMode == value)
          return;
        if (this.CachedEditingContextSource == EditingContextSource.SessionContext)
        {
          this._cachedEditingContext = this._cachedEditingContext.WithContextMode(value);
          this.InternalCurrentContext = this._cachedEditingContext;
        }
        else
        {
          this.InternalCurrentContext = new CurrentEditingContext(this.InternalCurrentContext.ContextID, this.InternalCurrentContext.ModificationID, value);
          this.FiltrationService.FiltrationApplyUpdates(true);
        }
      }
    }
  }

  /// <summary>Режим работы текущего контекста редактирования</summary>
  public EditingContextMode EditingContextMode
  {
    get
    {
      if (this.CachedEditingContextSource == EditingContextSource.SessionContext)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this.InternalCurrentContext = sessionKeeper.Session.EditingContextGetData(sessionKeeper.Session.MasterSessionGUID);
          this._cachedEditingContextTypeID = sessionKeeper.Session.GetObjectInfo(this.CachedEditingContextID).ObjectTypeID;
        }
      }
      return this.InternalCurrentContext.ContextMode;
    }
    set
    {
      CanSetContextModeCode setContextModeCode = value == EditingContextMode.AutoUpdate ? this.CanSetContextAutoUpdateMode(this.CachedEditingContextID) : CanSetContextModeCode.None;
      if (value == EditingContextMode.AutoUpdate && setContextModeCode != CanSetContextModeCode.CanSetAutoUpdate)
      {
        if (this.SilentMode)
          return;
        switch (setContextModeCode)
        {
          case CanSetContextModeCode.CheckedOutByOtherUser:
            long userObjectID = 0;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(Math.Abs(this.CachedEditingContextID), false);
              if (objectActualCopy == null)
                break;
              userObjectID = objectActualCopy.CheckoutBy;
            }
            if (userObjectID == 0L || userObjectID == this.UserID)
              break;
            IUserNamesCache userNamesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
            int num1 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1493"), (object) userNamesCache.GetUserName(userObjectID)), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            break;
          case CanSetContextModeCode.ReadOnlyByAccessRights:
            int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1491"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            break;
          case CanSetContextModeCode.ModifyByCreateVersion:
            int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1490"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            break;
          case CanSetContextModeCode.CantModifyObject:
            int num4 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1489"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            break;
          case CanSetContextModeCode.UnknownContext:
            int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1473") + LocalizationHolder.rm.GetString("Client.Core_1492"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            break;
        }
      }
      else
      {
        if (!this.IsContextToolbarVisible)
          value = EditingContextMode.Default;
        if (this.InternalCurrentContext.ContextMode == value)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          sessionKeeper.Session.EditingContextMode = value;
        this.CachedContextMode = value;
        this.CheckEditingContextAccessRights();
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, new NotificationEventArgs("EditingContextChanged"));
      }
    }
  }

  /// <summary>
  /// Загрузить информацию из атрибутов, связанных с блокировками некоторых действий пользователя
  /// </summary>
  private void InternalLoadBlockingAttrs()
  {
    if (this._blockedToolbarsReaded)
      return;
    this._blockedCompositions = false;
    this._blockedMenus = false;
    this._blockedViews = false;
    this._blockedToolbars = false;
    this._blockedToolbarsReaded = true;
    if (this.IsAdmin)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.RoleID, false);
      if (dbObject == null)
        return;
      IDBAttribute byId1 = dbObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cadd93ab-306c-11d8-b4e9-00304f19f545"));
      object obj1 = byId1 != null ? byId1.Value : (object) this._blockedViews;
      if (obj1 != null && !obj1.Equals((object) DBNull.Value))
        this._blockedViews = Convert.ToBoolean(obj1.ToString());
      IDBAttribute byId2 = dbObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cadd93a9-306c-11d8-b4e9-00304f19f545"));
      object obj2 = byId2 != null ? byId2.Value : (object) this._blockedMenus;
      if (obj2 != null && !obj2.Equals((object) DBNull.Value))
        this._blockedMenus = Convert.ToBoolean(obj2.ToString());
      IDBAttribute byId3 = dbObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cadd93aa-306c-11d8-b4e9-00304f19f545"));
      object obj3 = byId3 != null ? byId3.Value : (object) this._blockedCompositions;
      if (obj3 != null && !obj3.Equals((object) DBNull.Value))
        this._blockedCompositions = Convert.ToBoolean(obj3.ToString());
      IDBAttribute byId4 = dbObject.Attributes.FindByID(MetaDataHelper.GetAttributeTypeID("cad014b5-306c-11d8-b4e9-00304f19f545"));
      object obj4 = byId4 != null ? byId4.Value : (object) this._blockedToolbars;
      if (obj4 == null || obj4.Equals((object) DBNull.Value))
        return;
      this._blockedToolbars = Convert.ToBoolean(obj4.ToString());
    }
  }

  /// <summary>Надо ли блокировать настройку видимости закладок</summary>
  public bool BlockedViews
  {
    get
    {
      this.InternalLoadBlockingAttrs();
      return this._blockedViews;
    }
  }

  /// <summary>Надо ли блокировать настройку контекстных меню</summary>
  public bool BlockedMenus
  {
    get
    {
      this.InternalLoadBlockingAttrs();
      return this._blockedMenus;
    }
  }

  /// <summary>
  /// Надо ли блокировать настройку отображения узлов с составами
  /// </summary>
  public bool BlockedCompositions
  {
    get
    {
      this.InternalLoadBlockingAttrs();
      return this._blockedCompositions;
    }
  }

  /// <summary>Заблокированы ли панели инструментов составов</summary>
  public bool BlockedToolbars
  {
    get
    {
      this.InternalLoadBlockingAttrs();
      return this._blockedToolbars;
    }
  }

  /// <summary>
  /// Идентификатор текущего проекта
  /// (значение кэшировано)
  /// </summary>
  public long CachedProjectID
  {
    get
    {
      lock (this._syncRoot)
        return this.activeProjectId;
    }
  }

  /// <summary>Идентификатор текущего проекта</summary>
  public long ProjectID
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        lock (this._syncRoot)
        {
          this.activeProjectId = sessionKeeper.Session.CurrentProjectID;
          return this.activeProjectId;
        }
      }
    }
  }

  /// <summary>
  /// Способ фильтрации списков объектов в зависимости от их принадлежности к проектам
  /// (значение кэшировано)
  /// </summary>
  public ProjectFiltrationModes CachedProjectFiltrationMode
  {
    get
    {
      lock (this._syncRoot)
        return this.activeFiltrationMode;
    }
  }

  /// <summary>
  /// Способ фильтрации списков объектов в зависимости от их принадлежности к проектам
  /// </summary>
  public ProjectFiltrationModes ProjectFiltrationMode
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        lock (this._syncRoot)
        {
          this.activeFiltrationMode = sessionKeeper.Session.ProjectFiltrationMode;
          return this.activeFiltrationMode;
        }
      }
    }
  }

  /// <summary>Текущее правило по сортировке и отображению составов</summary>
  internal CompositionsAutosortRule InternalRule => this._rule;

  /// <summary>Текущее правило по сортировке и отображению составов</summary>
  public CompositionsAutosortRule Rule
  {
    get
    {
      lock (this._syncRoot)
      {
        if (this._rule.ObjectID < 0L)
        {
          long userId = this.UserID;
        }
        return this._rule;
      }
    }
    set
    {
      lock (this._syncRoot)
      {
        CompositionsAutosortRule compositionsAutosortRule = new CompositionsAutosortRule();
        compositionsAutosortRule.Assign((object) value);
        if (value == null || compositionsAutosortRule.ObjectID < 0L)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            compositionsAutosortRule.Load(sessionKeeper.Session, this._roleDefaultObjectID, false);
        }
        this._rule = compositionsAutosortRule;
      }
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, new NotificationEventArgs("FiltrationChanged"));
    }
  }

  /// <summary>
  /// Применяются ли события в текущем правиле по сортировке и отображению составов
  /// </summary>
  public bool UseRuleEvents
  {
    get
    {
      lock (this._syncRoot)
        return this._rule.UseEvents;
    }
    set
    {
      lock (this._syncRoot)
        this._rule.UseEvents = value;
    }
  }

  /// <summary>Настройки видов Навигатора для текущей роли</summary>
  public Dictionary<NavigatorColumnsKey, NavigatorColumns> RoleNavStreams
  {
    get
    {
      lock (this._syncRoot)
      {
        if (this._roleNavStreams == null)
        {
          INavigatorColumnsService service = ServicesManager.GetService(typeof (INavigatorColumnsService)) as INavigatorColumnsService;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._roleID, MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545"));
            object obj = objectAttributeById != null ? objectAttributeById.Value : (object) this._roleDefaultObjectID;
            if (obj != null)
            {
              if (!obj.Equals((object) DBNull.Value))
                this._roleNavStreams = service.LoadFromObject(Convert.ToInt64(obj), this._navViewsAttr);
            }
          }
        }
      }
      return this._roleNavStreams;
    }
    set => this._roleNavStreams = value;
  }

  /// <summary>
  /// Установить текущее значение проекта и режим отображения объектов в проекте
  /// </summary>
  /// <param name="projectID">Идентификатор текущего проекта</param>
  /// <param name="projectFiltrationMode">Способ фильтрации списков объектов в зависимости от их принадлежности к проектам</param>
  public void SetCurrentProject(
    long projectID,
    ProjectFiltrationModes projectFiltrationMode,
    bool silentMode = false)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObjectInfo(projectID).Empty)
          projectID = 0L;
        lock (this._syncRoot)
        {
          try
          {
            sessionKeeper.Session.CurrentProjectID = projectID;
            this.activeProjectId = projectID;
            ProjectFiltrationModes projectFiltrationModes = projectID != 0L ? projectFiltrationMode : (projectFiltrationMode == ProjectFiltrationModes.UserProjects ? ProjectFiltrationModes.UserProjects : ProjectFiltrationModes.None);
            sessionKeeper.Session.ProjectFiltrationMode = projectFiltrationModes;
            this.activeFiltrationMode = projectFiltrationModes;
          }
          catch
          {
            if (sessionKeeper.Session.CurrentProjectID == 0L)
            {
              this.activeProjectId = 0L;
              sessionKeeper.Session.ProjectFiltrationMode = ProjectFiltrationModes.None;
              this.activeFiltrationMode = ProjectFiltrationModes.None;
            }
            if (silentMode)
              return;
            throw;
          }
        }
      }
    }
    finally
    {
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, new NotificationEventArgs("ProjectChanged"));
    }
  }

  /// <summary>Режим работы без диалога с пользователем</summary>
  public bool SilentMode
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this._silentMode;
    }
    set
    {
      lock (this._syncRoot)
        this._silentMode = value;
    }
  }

  public ColumnPack DefaultColumnPack
  {
    get
    {
      this.LoadDefaultColumnPack();
      return this._defaultColumnPack;
    }
  }

  public bool EnabledPdmConfigurator
  {
    get
    {
      if (DateTime.Now - this._enabledPdmConfiguratorLastSynchronizationTime >= this.EnabledPdmConfiguratorSynchronizationInterval)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this._enabledPdmConfigurator = sessionKeeper.Session.EnabledPdmConfigurator;
        this._enabledPdmConfiguratorLastSynchronizationTime = DateTime.Now;
      }
      return this._enabledPdmConfigurator;
    }
    set
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.EnabledPdmConfigurator = value;
      this._enabledPdmConfigurator = value;
      this._enabledPdmConfiguratorLastSynchronizationTime = DateTime.Now;
    }
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "ObjectsChanged" && e is DBObjectsEventArgs objectsEventArgs1 && objectsEventArgs1.ObjectIDs != null)
    {
      long roleId = this.RoleID;
      if (objectsEventArgs1.ObjectIDs.Contains(roleId) || objectsEventArgs1.ObjectIDs.Contains(-roleId))
      {
        this._blockedToolbarsReaded = false;
        if (ServicesManager.GetService(typeof (IMainFormUpdate)) is IMainFormUpdate service)
          service.CheckToolbarsBlocking();
        int editingContextSource = (int) this.EditingContextSource;
      }
    }
    if (this.CachedEditingContextID != 0L && e is DBObjectsEventArgs && (e.EventName == "ObjectsChangesCancelled" || e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsCheckedOut"))
    {
      DBObjectsEventArgs objectsEventArgs2 = (DBObjectsEventArgs) e;
      if (objectsEventArgs2.ObjectIDs != null)
      {
        if (e.EventName == "ObjectsChangesCancelled" || e.EventName == "ObjectsCheckedIn")
        {
          if (objectsEventArgs2.ObjectIDs.Contains(-Math.Abs(this.CachedEditingContextID)))
            this.EditingContextID = Math.Abs(this.CachedEditingContextID);
        }
        else if (objectsEventArgs2.ObjectIDs.Contains(Math.Abs(this.CachedEditingContextID)))
          this.EditingContextID = -Math.Abs(this.CachedEditingContextID);
      }
    }
    if (e.EventName == "ObjectsCheckedIn" && e is DBObjectsEventArgs)
    {
      DBObjectsEventArgs objectsEventArgs3 = (DBObjectsEventArgs) e;
      if (objectsEventArgs3.ObjectIDs != null && (objectsEventArgs3.ObjectIDs.Contains(-Math.Abs(this.CachedEditingContextID)) || objectsEventArgs3.ObjectIDs.Contains(Math.Abs(this.CachedEditingContextID))))
        this.EditingContextMode = EditingContextMode.Default;
    }
    if (!(e.EventName == "ObjectsRemoved") && !(e.EventName == "ObjectsCheckedIn") && !(e.EventName == "ObjectsChangesCancelled") || !(e is DBObjectsEventArgs objectsEventArgs4) || objectsEventArgs4.ObjectIDs == null || !objectsEventArgs4.ObjectIDs.Contains(this.CachedEditingContextID) && !objectsEventArgs4.ObjectIDs.Contains(-this.CachedEditingContextID))
      return;
    switch (e.EventName)
    {
      case "ObjectsRemoved":
        this.ReplaceEditingContext(CurrentEditingContext.Empty);
        this.CachedEditingContextID = 0L;
        this.CachedEditingContextModificationID = 0L;
        this.CachedContextMode = EditingContextMode.Default;
        break;
      case "ObjectsChanged":
      case "ObjectsCheckedIn":
      case "ObjectsChangesCancelled":
        if (this.CanLeaveContextAutoUpdateMode(this.CachedEditingContextID))
        {
          this.EditingContextMode = this.CachedContextMode;
          break;
        }
        this.EditingContextMode = EditingContextMode.Default;
        break;
    }
  }

  private void LoadDefaultColumnPack()
  {
    lock (this._syncRoot)
    {
      if (this._isDeafultColumnPackLoaded)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IRoleConfigurationManager configurationManager = ServiceLocator.Get<IRoleConfigurationManager>();
        long id4RoleVersionId = this.GetRoleConfigurationVersionID4RoleVersionID(session);
        this._defaultColumnPack = ObjectHelper.IsUnknownObjectVersionID(id4RoleVersionId) ? new ColumnPack() : configurationManager.LoadNavigatorDefaultColumnPack(id4RoleVersionId);
      }
      this._isDeafultColumnPackLoaded = true;
    }
  }

  private long GetRoleConfigurationVersionID4RoleVersionID(IUserSession userSession)
  {
    return userSession.GetObjectAttributeByGuid(userSession.RoleID, new Guid("cad00692-306c-11d8-b4e9-00304f19f545")).AsInteger;
  }
}
