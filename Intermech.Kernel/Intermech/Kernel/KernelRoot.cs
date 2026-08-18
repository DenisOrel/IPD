// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.KernelRoot
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Calendars;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Cache;
using Intermech.Kernel.NotifySamples;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using Intermech.Office.Interfaces;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;


namespace Intermech.Kernel;

public class KernelRoot
{
  private static bool _Inited;

  public static void InitAdminList(IUserSession sys_session)
  {
    List<long> rolesID = new List<long>();
    try
    {
      try
      {
        IDBObjectCollection objectCollection = sys_session.GetObjectCollection(sys_session.IdentHelper.RolesTypeID);
        IDBAttributeType attributeType = sys_session.GetAttributeType(SystemGUIDs.attributeIsAdminRole, false);
        if (attributeType == null)
          return;
        DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(attributeType.AttributeID, RelationalOperators.Equal, (object) 1, LogicalOperators.NONE, 0, true)
        }, new object[1]{ (object) -2 }));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
          if (int64 != sys_session.IdentHelper.AdminRoleID)
            rolesID.Add(int64);
        }
      }
      finally
      {
        rolesID.Add(sys_session.IdentHelper.AdminRoleID);
        DBRoleObject.SetRolesList(rolesID);
      }
    }
    catch
    {
    }
  }

  public static void Init()
  {
    if (KernelRoot._Inited)
      return;
    if (ConfigurationManager.AppSettings.Get("SecurityFilter") == "0")
      ServerConsts.MandateAccess = false;
    if (ConfigurationManager.AppSettings.Get("DelayedPurge") == "0")
      BlobStoragesPool.DelayedPurge = false;
    if (ConfigurationManager.AppSettings.Get("BackupEvenlogRecords") == "1")
      ServerConsts.BackupEventlogRecords = true;
    if (ConfigurationManager.AppSettings.Get("BackupEventlogRecords") == "1")
      ServerConsts.BackupEventlogRecords = true;
    if (ConfigurationManager.AppSettings.Get("OldUniqueAttributesCheck") == "1")
      ServerConsts.OldUniqueAttributesCheck = true;
    if (ConfigurationManager.AppSettings.Get("UseSearchWorkcopyFiles") == "1")
      ServerConsts.UseSearchWorkcopyFiles = true;
    if (ConfigurationManager.AppSettings.Get("CreateObjectLogging") == "1")
      ServerConsts.CreateObjectLogging = true;
    if (ConfigurationManager.AppSettings.Get("CreateRelationLogging") == "1")
      ServerConsts.CreateRelationLogging = true;
    if (ConfigurationManager.AppSettings.Get("EnableSyncCheckin") == "1")
      ServerConsts.EnableSyncCheckin = true;
    string s1 = ConfigurationManager.AppSettings.Get("SessionSmartCacheTime");
    int result1;
    if (s1 != null && s1 != string.Empty && int.TryParse(s1, out result1) && result1 >= 0)
      ServerConsts.SessionSmartCacheTime = result1;
    string s2 = ConfigurationManager.AppSettings.Get("PeakMemoryUsageNotify");
    int result2;
    if (s2 != null && s2 != string.Empty && int.TryParse(s2, out result2) && result2 >= 0)
      ServerConsts.PeakMemoryUsageNotify = result2;
    DBUserObject.Init();
    DBMeasureObject.Init();
    IDbManagerService service1 = ServerServices.GetService(typeof (IDbManagerService)) as IDbManagerService;
    using (IDbManager dbManager = service1.CreateDbManager())
    {
      object obj1 = dbManager.ExecuteScalar($"SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = {SqlHelper.QString("KERNEL")} AND F_USER_ID = 0 AND F_SECTION_ID = {SqlHelper.QString("SECURITY")} AND F_PARAM_NAME = {SqlHelper.QString("ACC_CACHE")}");
      if (obj1 != null)
      {
        if (obj1 != DBNull.Value)
        {
          try
          {
            Intermech.Consts.CacheClearPeriod = TimeSpan.FromMinutes((double) Convert.ToInt32(obj1));
          }
          catch (Exception ex)
          {
            (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(LocalizationHolder.rm.GetString("Kernel_851") + ex.Message, Intermech.Consts.traceAlways, string.Empty);
          }
        }
      }
      object obj2 = dbManager.ExecuteScalar($"SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = {SqlHelper.QString("KERNEL")} AND F_USER_ID = 0 AND F_SECTION_ID = {SqlHelper.QString("SECURITY")} AND F_PARAM_NAME = {SqlHelper.QString("CHECK_LISTS")}");
      if (obj2 != null)
      {
        if (obj2 != DBNull.Value)
        {
          try
          {
            DBConfigurations.CheckObjectsVisibility = Convert.ToInt32(obj2) != 0;
          }
          catch (Exception ex)
          {
            (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(LocalizationHolder.rm.GetString("Kernel_852") + ex.Message, Intermech.Consts.traceAlways, string.Empty);
          }
        }
      }
      object obj3 = dbManager.ExecuteScalar($"SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = {SqlHelper.QString("KERNEL")} AND F_USER_ID = 0 AND F_SECTION_ID = {SqlHelper.QString("SECURITY")} AND F_PARAM_NAME = {SqlHelper.QString("CHECK_ATTR_LCACCESS")}");
      try
      {
        if (obj3 != null)
        {
          if (obj3 != DBNull.Value)
          {
            if (Convert.ToInt32(obj3) > 0)
              ServerConsts.CheckAttributeLCStepSecurity = true;
          }
        }
      }
      catch (Exception ex)
      {
        (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace("Ошибка инициализации режима CheckAttributeLCStepSecurity: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
    }
    ICacheDataset service2 = ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset;
    IDBTimedEvents service3 = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IVersionRulesCacheService service4 = ServerServices.GetService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    IUserSession sessionTemporaryClone = service3.GetSystemSessionTemporaryClone("Kernet.Init");
    try
    {
      UserSession.InitSpecialPlugins(sessionTemporaryClone as UserSession);
      RelationTypeSecurity.InitDontCacheAccess4Types(sessionTemporaryClone as UserSession);
      NotifySamplesConst.Init(sessionTemporaryClone.IdentHelper);
      service2.LoadFilePrototypes(sessionTemporaryClone, -1);
      KernelRoot.LoadConsts(sessionTemporaryClone);
      if (service4 != null)
      {
        service4.Load((object) sessionTemporaryClone);
        service4.LoadRolesSettings((object) sessionTemporaryClone);
      }
      MetaDataHelper.SyncMetadata((sessionTemporaryClone as IUserSessionCacheDataSet).CacheDataSet);
      ICreatorContainer service5 = ServerServices.GetService(typeof (IDBObjectService)) as ICreatorContainer;
      IDBObjectCreator creatorInstance1 = (IDBObjectCreator) new DBKernelObjectCreator();
      List<Guid> specialGroupingGuids = MetaDataHelper.GetSpecialGroupingGuids();
      for (int index = 0; index < specialGroupingGuids.Count; ++index)
        service5.AddCreator((object) specialGroupingGuids[index], (object) creatorInstance1);
      service5.AddCreator((object) new Guid("cad0146b-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance1);
      (ServerServices.GetService(typeof (IDBRelationCollectionService)) as ICreatorContainer).AddCreator((object) new Guid("cad0036b-306c-11d8-b4e9-00304f19f545"), (object) new DBEcoRelationCollectionCreator());
      EcoImportService.RegisterService();
      IDBObjectCreator creatorInstance2 = (IDBObjectCreator) new DBTableReportCreator();
      service5.AddCreator((object) new Guid("cad00289-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance2);
      service5.AddCreator((object) new Guid("cad0028a-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance2);
      service5.AddCreator((object) new Guid("cadd9237-306c-11d8-b4e9-00304f19f545"), (object) new DBCityCreator());
      ClearServerCache timedService = new ClearServerCache();
      service3.RegisterService((object) timedService);
      service5.AddCreator((object) new Guid("cadd94cd-306c-11d8-b4e9-00304f19f545"), (object) new DBScheduledCreator());
      AppServers appServers = new AppServers(service1.CreateDbManager());
      ServerServices.AddService(typeof (IAppServers), (object) appServers);
      ServersSynchTask serviceInstance = new ServersSynchTask(service3.GetSystemSessionPermanentClone("ServersSynchTask") as UserSession, (IAppServers) appServers);
      ServerServices.AddService(typeof (IServerSynchronizersManager), (object) serviceInstance);
      KernelCacheSynchronizer cacheSynchronizer = new KernelCacheSynchronizer();
      serviceInstance.RegisterSynchronizer((IServerSynchronizer) cacheSynchronizer);
      ServerServices.AddService(typeof (IKernelCacheSynchronizer), (object) cacheSynchronizer);
      ServerServices.AddService(typeof (IRolesCache), (object) new RolesCache(sessionTemporaryClone));
      try
      {
        IAttachedSelectionsServerService service6 = ServerServices.GetService(typeof (IAttachedSelectionsServerService)) as IAttachedSelectionsServerService;
        service6.RegisterCategory(sessionTemporaryClone, sessionTemporaryClone.GetObjectType(new Guid("cad00289-306c-11d8-b4e9-00304f19f545")).ObjectType);
        service6.RegisterCategory(sessionTemporaryClone, sessionTemporaryClone.GetObjectType(new Guid("cad0028a-306c-11d8-b4e9-00304f19f545")).ObjectType);
      }
      catch (Exception ex)
      {
        (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(LocalizationHolder.rm.GetString("Kernel_1144") + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
      ServerServices.AddService(typeof (ICalendarsService), (object) new CalendarsService(sessionTemporaryClone));
      try
      {
        Intermech.Project.Library.Init((IServiceProvider) ServerServices.ServiceContainer, sessionTemporaryClone);
        OfficeConsts.Init(sessionTemporaryClone);
      }
      catch (Exception ex)
      {
        (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace("Ошибка инициализации календарей, проектов или канцелярии: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
      HashConsts.Init(sessionTemporaryClone);
      SignConsts.Init(sessionTemporaryClone);
      ServerServices.AddService(typeof (IUserSessionsCleaner), (object) new UserSessionsCleaner((IEventLogHelper) ServerServices.GetService(typeof (IEventLogHelper))));
    }
    finally
    {
      sessionTemporaryClone?.Logout("Kernet.Init");
    }
    KernelRoot._Inited = true;
  }

  private static void LoadConsts(IUserSession sys_session)
  {
    AdminUtilsService.OptimizerStatisticsON = sys_session.Configurations.ReadBool("KERNEL", "PERFORMANCE", "OPTIM_STAT", AdminUtilsService.OptimizerStatisticsON, DBConfigMode.GlobalOnly);
    ServerConsts.AutomaticAccessLevelUp = sys_session.Configurations.ReadBool("KERNEL", "SECURITY", "ACC_AUTO_UP", false, DBConfigMode.GlobalOnly);
    ServerConsts.EnableSecret2Public = sys_session.Configurations.ReadBool("KERNEL", "SECURITY", "SECRET2PUBLIC", false, DBConfigMode.GlobalOnly);
    ServerConsts.CopyAuthenticalFiles = sys_session.Configurations.ReadBool("KERNEL", "COMMON", "COPY_AUTHENTICAL_FILES", false, DBConfigMode.GlobalOnly);
    ServerConsts.SendAttrs2DelayedNotificationMode = sys_session.Configurations.ReadBool("KERNEL", "COMMON", "COPY_ATTRS2NOTIF", false, DBConfigMode.GlobalOnly);
    ServerConsts.AnnulAllVersions = sys_session.Configurations.ReadBool("KERNEL", "COMMON", "ANNUL_ALL_VERSIONS", true, DBConfigMode.GlobalOnly);
    ServerConsts.WrongPasswordsLimit = Convert.ToInt32(sys_session.Configurations.ReadInteger("KERNEL", "SECURITY", "WRONG_PSW_COUNT", 0L, DBConfigMode.GlobalOnly));
    ServerConsts.CryptMethod = Convert.ToChar(sys_session.Configurations.ReadString("KERNEL", "SECURITY", "CRYPTO_METHOD", Convert.ToString(CryptHelper.SHA1Crypt), DBConfigMode.GlobalOnly));
    ServerConsts.IndexTablespaceName = sys_session.Configurations.ReadString("KERNEL", "COMMON", "INDEX_TABLESPACE", string.Empty, DBConfigMode.GlobalOnly);
    ServerConsts.CopyProjectVisibility = sys_session.Configurations.ReadBool("KERNEL", "SECURITY", "COPY_PROJ_VISIBLE", false, DBConfigMode.GlobalOnly);
    ServerConsts.CopyArcVisibility = sys_session.Configurations.ReadBool("ARCHIVES", "SECURITY", "COPY_ARC_VISIBLE", false, DBConfigMode.GlobalOnly);
    ServerConsts.SetProjectOnCreateRelation = sys_session.Configurations.ReadBool("KERNEL", "PROJECT", "SET_PROJ2CHILD", true, DBConfigMode.GlobalOnly);
  }
}
