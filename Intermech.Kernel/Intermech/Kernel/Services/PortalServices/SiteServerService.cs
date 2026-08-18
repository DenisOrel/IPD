// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.SiteServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class SiteServerService : LongLifeObject, ISiteServerService
{
  private IUserSession _mainReplicSession;
  private int _objTypeTasks = -1;

  public ConnectionSettings Settings
  {
    get
    {
      string url = ConfigurationManager.AppSettings.Get("PortalUrl") ?? string.Empty;
      ConnectionSettings settings = new ConnectionSettings(url);
      if (url != string.Empty)
      {
        settings.Url = url;
        string str1 = ConfigurationManager.AppSettings.Get("PortalName");
        settings.Name = str1 ?? string.Empty;
        string str2 = ConfigurationManager.AppSettings.Get("SiteGuid");
        settings.SiteGuid = str2 == null || !GuidHelper.IsGuid(str2) ? Guid.Empty : new Guid(str2);
        string str3 = ConfigurationManager.AppSettings.Get("SiteCode");
        if (str3 != null && str3 != string.Empty && str3.Length == 1)
          settings.SiteCode = str3[0];
        string str4 = ConfigurationManager.AppSettings.Get("PortalReplicLogin");
        settings.UserLogin = str4 ?? string.Empty;
        string cipherText = ConfigurationManager.AppSettings.Get("PortalReplicPassword");
        settings.Password = !string.IsNullOrEmpty(cipherText) ? Cryptor.Decrypt(cipherText, "cad00016-306c-11d8-b4e9-00304f19f545") : string.Empty;
        string str5 = ConfigurationManager.AppSettings.Get("ProxyAddress");
        settings.ProxyAddress = str5 ?? string.Empty;
        string s = ConfigurationManager.AppSettings.Get("ProxyPort");
        if (!string.IsNullOrEmpty(s))
          int.TryParse(s, out settings.ProxyPort);
        string str6 = ConfigurationManager.AppSettings.Get("PortalValidateVersion");
        if (!string.IsNullOrEmpty(str6))
          bool.TryParse(str6, out settings.ValidateVersion);
        string str7 = ConfigurationManager.AppSettings.Get("PortalAsyncSupported");
        if (!string.IsNullOrEmpty(str7))
          bool.TryParse(str7, out settings.AsyncSupported);
        settings.IsValid = this.IsValid(settings, true);
      }
      return settings;
    }
    set
    {
      if (!this.IsValid(value, true))
        throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1094"));
      ConfigurationManager.AppSettings.Set("PortalUrl", value.Url);
      ConfigurationManager.AppSettings.Set("PortalName", value.Name);
      ConfigurationManager.AppSettings.Set("SiteCode", value.SiteCode.ToString());
      ConfigurationManager.AppSettings.Set("SiteGuid", value.SiteGuid.ToString());
      ConfigurationManager.AppSettings.Set("PortalReplicLogin", value.UserLogin);
      ConfigurationManager.AppSettings.Set("ProxyAddress", value.ProxyAddress);
      ConfigurationManager.AppSettings.Set("ProxyPort", Convert.ToString(value.ProxyPort));
      ConfigurationManager.AppSettings.Set("PortalReplicPassword", value.Password != string.Empty ? Cryptor.Encrypt(value.Password, "cad00016-306c-11d8-b4e9-00304f19f545") : string.Empty);
    }
  }

  private bool IsValid(ConnectionSettings settings, bool withCode)
  {
    Regex regex = new Regex("^(ht|f)tp(s?)\\:\\/\\/[0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*(:(0-9)*)*(\\/?)([a-zA-Z0-9\\-\\.\\?\\,\\'\\/\\\\\\+&amp;%\\$#_]*)?$");
    bool flag = settings.Url != string.Empty && (settings.IsOffline && Directory.Exists(settings.Url) || !settings.IsOffline && regex.IsMatch(settings.Url)) && settings.SiteGuid != Guid.Empty && settings.UserLogin != string.Empty && settings.Password != string.Empty;
    if (flag & withCode)
      flag = (int) settings.SiteCode != (int) Consts.NoSymbol;
    return flag;
  }

  private IDBObject GetUser(IUserSession session, string userName, string login, string password)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"));
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(new Guid("cad00018-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) login.ToUpper(), LogicalOperators.AND, 0)
    }, new object[1]{ (object) -2 });
    DataTable dataTable = objectCollection.Select(paramSet);
    if (dataTable.Rows.Count > 0)
      return session.GetObject(Convert.ToInt64(dataTable.Rows[0][0]));
    IDBObject user = objectCollection.Create();
    user.Attributes.AddAttribute(session.IdentHelper.UserNameID, false, new object[1]
    {
      userName == string.Empty || userName == null ? (object) login : (object) userName
    });
    user.Attributes.AddAttribute(session.IdentHelper.LoginNameID, false, new object[1]
    {
      (object) login.ToUpper()
    });
    user.Attributes.AddAttribute(session.IdentHelper.PasswordID, false, new object[1]
    {
      (object) password
    });
    user.CommitCreation(true);
    return user;
  }

  private void CheckRelation(IUserSession session, IDBObject user, IDBObject role)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.SimpleRelationTypeID);
    if (relationCollection.Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -20
    }), role.ObjectID, user.ID, DateTime.Now).Rows.Count != 0)
      return;
    relationCollection.Create(role.ObjectID, user.ObjectID, DateTime.Now);
  }

  public void InitServices(object session, ConnectionSettings settings)
  {
    IUserSession userSession = session is Guid sessionGUID ? UserSession.GetSessionByID(sessionGUID) : (IUserSession) session;
    AttributesHandlerService serviceInstance1 = new AttributesHandlerService(userSession);
    ServerServices.AddService(typeof (IExportAttributesHandlerService), (object) serviceInstance1);
    ServerServices.AddService(typeof (IImportAttributesHandlerService), (object) serviceInstance1);
    ServerServices.AddService(typeof (IImportUnitHandlerService), (object) new ImportUnitHandlerService());
    ICustomServices service1 = ServerServices.GetService(typeof (ICustomServices)) as ICustomServices;
    if (service1.GetService(typeof (IPortalConnector)) != null || !this.IsValid(settings, true))
      return;
    IEventLogHelper service2 = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    service2.AfterCreateObjectEvent += new AfterCreateObjectHandler(this.EventHelper_AfterCreateObjectEvent);
    service2.AfterNextLCStepEvent += new NextLCStepHandler(this.EventHelper_AfterNextLCStepEvent);
    (ServerServices.GetService(typeof (IDBObjectService)) as ICreatorContainer).AddCreator((object) new Guid("cad0149e-306c-11d8-b4e9-00304f19f545"), (object) new DBTaskCreator());
    IDBObject role1 = userSession.GetObject(settings.SiteGuid, false);
    if (role1 == null)
    {
      DBSiteObject.autoCreate = true;
      try
      {
        role1 = userSession.GetObjectCollection(PortalConsts.objtypeSites).Create(settings.SiteGuid);
        role1.GetAttributeByGuid(PortalConsts.attributeSiteCode).AsString = Convert.ToString(settings.SiteCode);
        role1.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AsString = LocalizationHolder.rm.GetString("Kernel_1095");
        role1.CommitCreation(true);
      }
      finally
      {
        DBSiteObject.autoCreate = false;
      }
    }
    IDBObject user1 = this.GetUser(userSession, ConfigurationManager.AppSettings.Get("PortalReplicUserName"), settings.UserLogin, settings.Password);
    IDBObject role2 = userSession.GetObject(userSession.IdentHelper.AdminRoleID, true);
    this.CheckRelation(userSession, user1, userSession.GetObject(PortalConsts.objectReplicatorRole, true));
    this.CheckRelation(userSession, user1, role2);
    this.CheckRelation(userSession, user1, role1);
    string userName = ConfigurationManager.AppSettings.Get("PortalAdminUserName");
    string login = ConfigurationManager.AppSettings.Get("PortalAdminLogin");
    string cipherText1 = ConfigurationManager.AppSettings.Get("PortalAdminPassword");
    string password = cipherText1 == null || !(cipherText1 != string.Empty) ? string.Empty : Cryptor.Decrypt(cipherText1, "cad00016-306c-11d8-b4e9-00304f19f545");
    if (login != null && login != string.Empty)
    {
      IDBObject user2 = this.GetUser(userSession, userName, login, password);
      this.CheckRelation(userSession, user2, userSession.GetObject(PortalConsts.objectPortalAdminRole, true));
      this.CheckRelation(userSession, user2, role1);
    }
    string replicUserName = ConfigurationManager.AppSettings.Get("PortalReplicLogin");
    string cipherText2 = ConfigurationManager.AppSettings.Get("PortalReplicPassword");
    string replicPassword = cipherText2 == null || !(cipherText2 != string.Empty) ? string.Empty : Cryptor.Decrypt(cipherText2, "cad00016-306c-11d8-b4e9-00304f19f545");
    this._mainReplicSession = this.GetReplicatorSession(replicUserName, replicPassword);
    ISitesCacheService customService = (ISitesCacheService) userSession.GetCustomService(typeof (ISitesCacheService));
    ((SitesCacheService) customService).Info = new SiteInfo(role1.ObjectID, settings.SiteGuid, settings.SiteCode, role1.Caption, SystemTypes.IPS);
    service1.AddService(typeof (IPortalConnector), (object) new PortalConnectorService(this.GetPermanentSessionClone(this._mainReplicSession, "PortalConnectorService"), settings, customService.Info));
    ImportRulesService serviceInstance2 = new ImportRulesService(this.GetPermanentSessionClone(userSession, "ImportRulesService"));
    ServerServices.AddService(typeof (IImportRulesService), (object) serviceInstance2);
    service1.AddService(typeof (IImportRulesService), (object) serviceInstance2);
    PublishRulesService serviceInstance3 = new PublishRulesService(this.GetPermanentSessionClone(userSession, "PublishRulesService"));
    serviceInstance3.RegisterForbiddenAttribute(MetaDataHelper.GetObjectTypeGuid(userSession.IdentHelper.UsersTypeID), userSession.IdentHelper.PasswordID);
    ServerServices.AddService(typeof (IPublishRulesService), (object) serviceInstance3);
    service1.AddService(typeof (IPublishRulesService), (object) serviceInstance3);
    PublishTypesConfiguration serviceInstance4 = new PublishTypesConfiguration(this.GetPermanentSessionClone(userSession, "PublishTypesConfiguration") as UserSession);
    service1.AddService(typeof (IPublishTypesConfiguration), (object) serviceInstance4);
    serviceInstance4.Reload();
    PortalTasksQueue portalTasksQueue = new PortalTasksQueue();
    portalTasksQueue.Init();
    service1.AddService(typeof (IPortalTasksQueue), (object) portalTasksQueue);
    portalTasksQueue.ImportTaskCompletedEvent += new ImportTaskCompletedEventHandler(ImportEventHandlers.OnImportCompleted);
    portalTasksQueue.ImportTaskErrorEvent += new ImportTaskErrorEventHandler(ImportEventHandlers.OnImportError);
    ServerServices.AddService(typeof (IPortalTasksQueue), (object) portalTasksQueue);
    ServerServices.AddService(typeof (IPortalEventsService), (object) portalTasksQueue);
    ServerServices.AddService(typeof (ICustomPublisherService), (object) portalTasksQueue);
    ServerServices.AddService(typeof (IPublishTypesConfiguration), (object) serviceInstance4);
    PublishCompositionService serviceInstance5 = new PublishCompositionService();
    service1.AddService(typeof (IPublishCompositionService), (object) serviceInstance5);
    ServerServices.AddService(typeof (IPublishCompositionService), (object) serviceInstance5);
    LinkedObjectsService serviceInstance6 = new LinkedObjectsService();
    serviceInstance6.RegisterHandler((ILinkedObjectsHandler) new InseparableObjectTypesHandler());
    ServerServices.AddService(typeof (ILinkedObjectsService), (object) serviceInstance6);
    IDBTimedEvents service3 = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    Dictionary<TaskPriority, Guid> dictionary1 = new Dictionary<TaskPriority, Guid>(3)
    {
      {
        TaskPriority.Hight,
        new Guid("E1AEC65C-9075-4191-960E-7A775A2A2FCC")
      },
      {
        TaskPriority.Low,
        new Guid("63B8FB19-CD34-48E8-819C-2C45C038F234")
      },
      {
        TaskPriority.Normal,
        new Guid("10A7F8ED-345A-4420-80BC-53A1A381E3FD")
      }
    };
    Dictionary<TaskPriority, Guid> dictionary2 = new Dictionary<TaskPriority, Guid>(3)
    {
      {
        TaskPriority.Hight,
        new Guid("4655A57C-4DEC-442A-BF11-046BB1651FFF")
      },
      {
        TaskPriority.Low,
        new Guid("44CC402C-71B4-4161-9875-3542DE0CAA4F")
      },
      {
        TaskPriority.Normal,
        new Guid("8A5FF8AA-ABD4-4AAB-9D20-535C3ABFED4D")
      }
    };
    foreach (TaskPriority key in Enum.GetValues(typeof (TaskPriority)))
    {
      service3.RegisterService((object) new ScheduledPublishTasks(this.GetPermanentSessionClone(this._mainReplicSession, "ScheduledPublishTasks") as UserSession, "ScheduledPublishTasks1", portalTasksQueue, new TaskPriority?(key), $"Запуск задач публикации на портал с приоритетом {EnumDescConverter.GetEnumDescription((Enum) key)}", dictionary1[key]));
      service3.RegisterService((object) new ScheduledImportTasks(this.GetPermanentSessionClone(this._mainReplicSession, "ScheduledImportTasks") as UserSession, "ScheduledImportTasks1", portalTasksQueue, new TaskPriority?(key), $"Запуск задач импорта из портала с приоритетом {EnumDescConverter.GetEnumDescription((Enum) key)}", dictionary2[key]));
    }
    service3.RegisterService((object) new ScheduledPublishTasks(this.GetPermanentSessionClone(this._mainReplicSession, "ScheduledPublishTasks") as UserSession, "ScheduledPublishTasks2", portalTasksQueue, new TaskPriority?(), "Запуск всех задач публикации на портал", new Guid("CD8FEB8E-7422-44E0-822A-EBDC6C9321EC")));
    service3.RegisterService((object) new ScheduledImportTasks(this.GetPermanentSessionClone(this._mainReplicSession, "ScheduledImportTasks") as UserSession, "ScheduledImportTasks2", portalTasksQueue, new TaskPriority?(), "Запуск всех задач импорта из портала", new Guid("B68D3B29-44D2-429A-98A1-55FEA45F2162")));
    this._objTypeTasks = MetaDataHelper.GetObjectTypeID(PortalConsts.objtypeUpdateTasks);
    (ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool).GetStorageIDEvent += new GetStorageIDHandler(this.GetStorageIDEvent);
    if (ApplicationServices.Container.GetService(typeof (IAppServers)) is IAppServers service4)
      userSession.Configurations.WriteString("KERNEL", "PortalProps", "PortalServerName", service4.ServerName, 0L);
    this.Initialized = true;
  }

  private void GetStorageIDEvent(GetStorageIDEventArgs args)
  {
    if (args.ParentObject == null || !args.ParentObject.ObjectType.Equals(this._objTypeTasks))
      return;
    IPublishRulesService service = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true);
    if (service.BlobStorageID == 0L)
      return;
    args.StorageID = service.BlobStorageID;
  }

  private void EventHelper_AfterNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (nextstep.LevelID == session.IdentHelper.DeletedID)
      return;
    ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    if (customService.Info == null || !SiteIDHelper.IsOwner(customService.Info.Code, sender.SiteID))
      return;
    this.SetPublicationNecessary(sender);
  }

  private void EventHelper_AfterCreateObjectEvent(
    IDBObject newObject,
    IDBObject prototype,
    IUserSession session)
  {
    this.SetPublicationNecessary(newObject);
  }

  private void SetPublicationNecessary(IDBObject obj)
  {
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(PortalConsts.attributePublicationNecessary);
    if (attributeByGuid == null)
      return;
    attributeByGuid.AsInteger = 1L;
  }

  private IUserSession GetPermanentSessionClone(IUserSession masterSession, string sessionName)
  {
    return PortalServicesSessionHelper.GetCloneSession(masterSession, $"{sessionName}_{Guid.NewGuid()}", "SiteServerService.GetPermanentSessionClone", true);
  }

  private IUserSession GetReplicatorSession(string replicUserName, string replicPassword)
  {
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IUserSession userSession = (IUserSession) null;
    long objectId;
    TimeSpan timeZoneOffset;
    try
    {
      userSession = service.GetSystemSessionTemporaryClone(nameof (GetReplicatorSession));
      objectId = userSession.GetObject(PortalConsts.objectReplicatorRole, true).ObjectID;
      timeZoneOffset = userSession.TimeZoneOffset;
    }
    finally
    {
      userSession.Logout(nameof (GetReplicatorSession));
    }
    UserSession replicatorSession = new UserSession();
    replicatorSession.SetLoginCapabilities(true);
    replicatorSession.Login(replicUserName, new PswPackage(replicPassword, ServerConsts.CryptMethod), EnvironmentConsts.MachineName, timeZoneOffset, objectId, false, "ReplicatorSession");
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"Create session = {replicatorSession.SessionGUID} (ReplicatorSession) from \"SiteServerService.GetReplicatorSession\".");
    return (IUserSession) replicatorSession;
  }

  public bool Initialized { get; private set; }

  public long AddUser(
    object session,
    string userName,
    string login,
    string password,
    Guid userGuid,
    char siteCode)
  {
    return new SiteUserCreator<string>().Create(session, userName, login, password, userGuid, siteCode);
  }

  public long AddUser(
    object session,
    string userName,
    string login,
    PswPackage password,
    Guid userGuid,
    char siteCode)
  {
    return new SiteUserCreator<PswPackage>().Create(session, userName, login, password, userGuid, siteCode);
  }

  public void ChangeUserPassword(object session, string login, string password)
  {
    new SiteUserCreator<string>().ChangeUserPassword(session, login, password);
  }

  public void ChangeUserPassword(object session, string login, PswPackage password)
  {
    new SiteUserCreator<PswPackage>().ChangeUserPassword(session, login, password);
  }
}
