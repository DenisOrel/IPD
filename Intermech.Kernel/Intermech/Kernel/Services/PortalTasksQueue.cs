// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalTasksQueue
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.PortalServices;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Threading;


namespace Intermech.Kernel.Services;

internal sealed class PortalTasksQueue : 
  LongLifeObject,
  IPortalTasksQueue,
  IPortalEventsService,
  ICustomPublisherService
{
  internal BackupStorage Storage;
  private UserSession _session;
  internal Dictionary<long, IDBObject> StartedTasksObjects;
  private readonly string _rootFolder;

  public PortalTasksQueue()
  {
    this.Storage = new BackupStorage();
    this.StartedTasksObjects = new Dictionary<long, IDBObject>();
    string path = ConfigurationManager.AppSettings.Get("PortalFileStorage");
    if (path != null && path != string.Empty)
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(path);
      if (!directoryInfo.Exists)
        Directory.CreateDirectory(directoryInfo.FullName);
      this._rootFolder = directoryInfo.FullName;
    }
    else
      this._rootFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), PortalConsts.StorageFolder), "PublishTasksData");
  }

  public bool Init()
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    long num = 0;
    TimeSpan aTimeZoneOffset = TimeSpan.Zero;
    string aLoginName;
    string password;
    try
    {
      aLoginName = ConfigurationManager.AppSettings.Get("PortalReplicLogin");
      password = Cryptor.Decrypt(ConfigurationManager.AppSettings.Get("PortalReplicPassword"), "cad00016-306c-11d8-b4e9-00304f19f545");
    }
    catch
    {
      TasksHelper.AddMessageToLog(LocalizationHolder.rm.GetString("Kernel_1061"));
      return false;
    }
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service.GetSystemSessionTemporaryClone("PortalTasksQueue.Init");
      num = userSession.GetObject(PortalConsts.objectReplicatorRole, true).ObjectID;
      aTimeZoneOffset = userSession.TimeZoneOffset;
    }
    finally
    {
      userSession.Logout("PortalTasksQueue.Init");
    }
    this._session = new UserSession();
    this._session.SetLoginCapabilities(true);
    this._session.Login(aLoginName, new PswPackage(password, ServerConsts.CryptMethod), EnvironmentConsts.MachineName, aTimeZoneOffset, num != 0L ? num : this._session.IdentHelper.AdminRoleID, true);
    if (MeasureHelper.Measures == null)
      MeasureHelper.Init(this._session.GetMeasuresList());
    IPortalConnector customService = (IPortalConnector) this._session.GetCustomService(typeof (IPortalConnector));
    Guid connectGuid = Guid.Empty;
    try
    {
      connectGuid = customService.Login(this._session.SessionGUID);
    }
    catch
    {
    }
    if (connectGuid != Guid.Empty)
    {
      try
      {
        this.ReloadSitesInfo((IUserSession) this._session, connectGuid, customService);
      }
      finally
      {
        if (connectGuid != Guid.Empty && customService != null)
          customService.Logout(connectGuid);
      }
    }
    return true;
  }

  public void StartUpdate(Guid sessionGuid, string updateGuid, object tag)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    IPortalConnector customService = (IPortalConnector) sessionById.GetCustomService(typeof (IPortalConnector));
    Guid connectGuid = customService.Login(sessionGuid);
    if (!(tag is long num))
      num = 0L;
    long taskID = num;
    try
    {
      taskID = this.FormingImportTaskFromUpdate(sessionById, customService, sessionById.GetObjectCollection(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545")), connectGuid, updateGuid, taskID);
    }
    finally
    {
      if (connectGuid != Guid.Empty && customService != null)
        customService.Logout(connectGuid);
    }
    if (taskID == 0L)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1076"), (object) updateGuid));
    this.StartTask(taskID);
  }

  public void OwnComplete(
    Guid sessionGuid,
    long[] objectIDs,
    Guid[] objectGuids,
    string ownerSites,
    bool withComposition,
    bool autoUpdate)
  {
    if (!OwnCompleteHelper.ExecuteCommand(UserSession.GetSessionByID(sessionGuid) as UserSession, objectIDs, objectGuids, ownerSites, withComposition, autoUpdate))
      throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1087") + TasksHelper.LogFile);
  }

  public long PublishObjects(
    Guid sessionGuid,
    string taskName,
    TaskPriority priority,
    PublishComposition composition,
    ExtendedPublishOptions options)
  {
    return this.PublishObjects(sessionGuid, taskName, priority, composition, options, (Packet4Publish) null, false);
  }

  public long PublishObjects(
    Guid sessionGuid,
    string taskName,
    TaskPriority priority,
    PublishComposition composition,
    ExtendedPublishOptions options,
    Packet4Publish packet,
    bool createReceipt)
  {
    CheckPublishCompositionEventHandler compositionEvent = this.CheckPublishCompositionEvent;
    if (compositionEvent != null)
      compositionEvent((object) this, new CheckPublishCompositionEventArgs(UserSession.GetSessionByID(sessionGuid), composition, options));
    return this.CustomPublish(sessionGuid, (IPublisher) new CustomObjectsPublisher(composition, options, packet, createReceipt), taskName, priority);
  }

  public bool StartTask(long taskID)
  {
    new Thread(new ParameterizedThreadStart(this.StartTaskMethod))
    {
      IsBackground = true,
      Name = $"Portal_Any_Task_Thread_{taskID}"
    }.Start((object) taskID);
    return true;
  }

  public long CustomPublish(
    Guid sessionGuid,
    IPublisher publisher,
    string taskName,
    TaskPriority priority)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    publisher.CheckBeforePublication(sessionById);
    try
    {
      (sessionById as UserSession).StartTransaction();
      IDBObject dbObject = sessionById.GetObjectCollection(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545")).Create();
      this.AddPublishInformation(dbObject, publisher);
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(PortalConsts.attributeTaskFiles);
      using (TaskBackupStorage taskBackupStorage = new TaskBackupStorage(this._rootFolder, dbObject.ObjectGUID))
      {
        ITransferedObject[] transObjs = publisher.Pack(sessionById, taskBackupStorage.Writer);
        taskBackupStorage.SaveToFile(attributeByGuid);
        if (transObjs == null || transObjs.Length == 0)
          throw new Exception(LocalizationHolder.rm.GetString("Kernel_1090"));
        ITask exportTask = publisher.GetExportTask(sessionById, sessionById.UserID, string.IsNullOrEmpty(taskName) ? $"Export_task_{Guid.NewGuid()}" : taskName, (sessionById as UserSession).UserGUID, priority, transObjs, attributeByGuid);
        exportTask.TaskID = Math.Abs(dbObject.ObjectID);
        this.Storage.UpdateTask(sessionById, dbObject, exportTask);
        dbObject.CommitCreation(true);
        (sessionById as UserSession).Commit();
        return dbObject.ObjectID;
      }
    }
    catch (Exception ex)
    {
      (sessionById as UserSession).Rollback();
      throw;
    }
  }

  private void AddPublishInformation(IDBObject taskObject, IPublisher publisher)
  {
    string publicationInfo = publisher.PublicationInfo;
    if (string.IsNullOrEmpty(publicationInfo))
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(PortalConsts.attributePublishInformation);
    IMemoWriter memoWriter = (taskObject.GetAttributeByID(attributeTypeId) ?? taskObject.Attributes.AddAttribute(attributeTypeId, false)) as IMemoWriter;
    memoWriter.OpenMemo(publicationInfo.Length);
    memoWriter.WriteDataBlock(publicationInfo.ToCharArray());
  }

  public void FireObjectsPublished(object sender, ObjectsPublishedEventArgs e)
  {
    if (this.ObjectsPublishedEvent == null)
      return;
    this.ObjectsPublishedEvent(sender, e);
  }

  public void FireRelationImported(object sender, RelationImportedEventArgs e)
  {
    if (this.RelationImportedEvent == null)
      return;
    this.RelationImportedEvent(sender, e);
  }

  public void FireObjectImported(object sender, ObjectImportedEventArgs e)
  {
    if (this.ObjectImportedEvent == null)
      return;
    this.ObjectImportedEvent(sender, e);
  }

  public void FireImportTaskCompleted(object sender, ImportTaskCompletedEventArgs e)
  {
    if (this.ImportTaskCompletedEvent == null)
      return;
    this.ImportTaskCompletedEvent(sender, e);
  }

  public void FireStartResolveBaseVersionConflict(
    object sender,
    StartResolveBaseVersionConflictEventArgs e)
  {
    if (this.StartResolveBaseVersionConflictEvent == null)
      return;
    this.StartResolveBaseVersionConflictEvent(sender, e);
  }

  public void FireImportTaskError(object sender, ImportTaskErrorEventArgs e)
  {
    if (this.ImportTaskErrorEvent == null)
      return;
    this.ImportTaskErrorEvent(sender, e);
  }

  internal void FireBeforeObjectRefreshEvent(object sender, BeforeObjectRefreshEventArgs e)
  {
    if (this.BeforeObjectRefreshEvent == null)
      return;
    this.BeforeObjectRefreshEvent(sender, e);
  }

  public bool OnGetTaskByTypeEvent(IDBObject taskObject, TaskType taskType, out ITask task)
  {
    GetTaskByTypeEventArgs e = new GetTaskByTypeEventArgs(taskType, taskObject);
    GetTaskByTypeEventHandler getTaskByTypeEvent = this.GetTaskByTypeEvent;
    if (getTaskByTypeEvent != null)
      getTaskByTypeEvent((object) this, e);
    task = e.Task;
    return e.Handled;
  }

  public bool OnObjectAutoPublishEvent(
    IUserSession session,
    long objectID,
    int objectType,
    out IPublisher publisher,
    out string taskName,
    out TaskPriority taskPriority)
  {
    ObjectAutoPublishEventArgs e = new ObjectAutoPublishEventArgs(session, objectID, objectType);
    ObjectAutoPublishEventHandler autoPublishEvent = this.ObjectAutoPublishEvent;
    if (autoPublishEvent != null)
      autoPublishEvent((object) this, e);
    publisher = e.Publisher;
    taskName = e.TaskName;
    taskPriority = e.Priority;
    return e.Handled;
  }

  public void OnReadImportedObjectAttributes(ReadImportedObjectAttributesEventArgs e)
  {
    ReadImportedObjectAttributesEventHandler objectAttributesEvent = this.ReadImportedObjectAttributesEvent;
    if (objectAttributesEvent == null)
      return;
    objectAttributesEvent((object) this, e);
  }

  private bool ReloadSitesInfo(IUserSession session, Guid connectGuid, IPortalConnector connector)
  {
    try
    {
      SiteInfo[] sitesInfo = connector.GetSitesInfo(connectGuid);
      if (sitesInfo != null)
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection(PortalConsts.objtypeSites);
        ColumnDescriptor[] columns = new ColumnDescriptor[3]
        {
          new ColumnDescriptor((object) -12),
          new ColumnDescriptor((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545")),
          new ColumnDescriptor((object) PortalConsts.attributeSystem)
        };
        (session as UserSession).StartTransaction();
        try
        {
          for (int index = 0; index < sitesInfo.Length; ++index)
          {
            DataTable dataTable = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(PortalConsts.attributeSiteCode, RelationalOperators.Equal, (object) Convert.ToString(sitesInfo[index].Code), LogicalOperators.AND, 0)
            }, columns));
            if (dataTable.Rows.Count > 0)
            {
              DataRow row = dataTable.Rows[0];
              Guid guid = new Guid(Convert.ToString(row[0]));
              if (!sitesInfo[index].GUID.Equals(guid))
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1074"), (object) sitesInfo[index].Code));
              string str = Convert.ToString(row[1]);
              SystemTypes int32 = (SystemTypes) Convert.ToInt32(row[2]);
              string caption = sitesInfo[index].Caption;
              if (!str.Equals(caption) || !int32.Equals((object) sitesInfo[index].SystemType))
              {
                IDBObject dbObject = session.GetObject(guid);
                dbObject.GetAttributeByID(session.IdentHelper.NameID).AsString = sitesInfo[index].Caption;
                dbObject.GetAttributeByGuid(PortalConsts.attributeSystem).AsInteger = (long) sitesInfo[index].SystemType;
              }
            }
            else
            {
              DBSiteObject.autoCreate = true;
              try
              {
                IDBObject dbObject = objectCollection.Create(sitesInfo[index].GUID);
                dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeSiteCode), false, new object[1]
                {
                  (object) sitesInfo[index].Code
                });
                dbObject.Attributes.AddAttribute(session.IdentHelper.NameID, false, new object[1]
                {
                  (object) sitesInfo[index].Caption
                });
                dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeSystem), false, new object[1]
                {
                  (object) (int) sitesInfo[index].SystemType
                });
                dbObject.CommitCreation(true);
              }
              finally
              {
                DBSiteObject.autoCreate = false;
              }
            }
          }
          (session as UserSession).Commit();
          return true;
        }
        catch
        {
          (session as UserSession).Rollback();
          throw;
        }
      }
    }
    catch (Exception ex)
    {
      TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1075"), (object) Intermech.Kernel.Services.PortalServices.Helper.FormingLogError(ex)));
    }
    finally
    {
      ((ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService))).Reload((object) session);
    }
    return false;
  }

  private long FormingImportTaskFromUpdate(
    IUserSession session,
    IPortalConnector connector,
    IDBObjectCollection objColl,
    Guid connectGuid,
    string updateGuid,
    long taskID)
  {
    TransferedObject[] updateUnit = connector.GetUpdateUnit(connectGuid, updateGuid);
    if (updateUnit == null || updateUnit.Length == 0)
    {
      connector.EndUpdateUnit(connectGuid, updateGuid);
      return 0;
    }
    connector.StartUpdateUnit(connectGuid, updateGuid);
    (session as UserSession).StartTransaction();
    try
    {
      ImportUpdatesTask importUpdatesTask = new ImportUpdatesTask(session.UserID, (session as UserSession).UserGUID, string.Format(LocalizationHolder.rm.GetString("Kernel_1077"), (object) updateGuid), TaskPriority.Normal, updateGuid, updateUnit, new ObjectImportedEventHandler(this.FireObjectImported), new RelationImportedEventHandler(this.FireRelationImported), new ImportTaskCompletedEventHandler(this.FireImportTaskCompleted));
      bool flag = false;
      if (taskID == 0L)
      {
        ConditionStructure conditionStructure1 = new ConditionStructure(PortalConsts.attributeUpdateGuid, RelationalOperators.Equal, (object) updateGuid, LogicalOperators.AND, 0);
        ConditionStructure conditionStructure2 = new ConditionStructure(PortalConsts.attributeTaskType, RelationalOperators.Equal, (object) 2, LogicalOperators.AND, 0);
        DataTable dataTable = objColl.Select(new DBRecordSetParams(new ConditionStructure[2]
        {
          conditionStructure1,
          conditionStructure2
        }, new object[2]{ (object) -2, (object) -8 }));
        if (dataTable.Rows.Count == 1)
          taskID = Convert.ToInt64(dataTable.Rows[0][0]);
      }
      IDBObject dbTask;
      if (taskID != 0L)
      {
        importUpdatesTask.TaskID = taskID;
        dbTask = session.GetObject(importUpdatesTask.TaskID);
        importUpdatesTask.UserID = dbTask.OwnerID;
        ImportTask importTask = new ImportTask(importUpdatesTask.TaskID);
        (dbTask as DBTask).LoadTaskData((ITask) importTask);
        importUpdatesTask.Priority = importTask.Priority;
        flag = true;
      }
      else
      {
        dbTask = objColl.Create();
        importUpdatesTask.TaskID = Math.Abs(dbTask.ObjectID);
      }
      this.Storage.UpdateTask(session, dbTask, (ITask) importUpdatesTask);
      if (!flag)
        dbTask.CommitCreation(true);
      (session as UserSession).Commit();
      return dbTask.ObjectID;
    }
    catch
    {
      (session as UserSession).Rollback();
      throw;
    }
  }

  private void GetUpdates(IUserSession session, Guid connectGuid, IPortalConnector connector)
  {
    try
    {
      string[] updates = connector.GetUpdates(connectGuid, session.SessionGUID);
      IDBObjectCollection objectCollection = session.GetObjectCollection(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545"));
      if (updates == null || updates.Length == 0)
        return;
      for (int index = 0; index < updates.Length; ++index)
        this.FormingImportTaskFromUpdate(session, connector, objectCollection, connectGuid, updates[index], 0L);
    }
    catch (Exception ex)
    {
      TasksHelper.AddMessageToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1078"), (object) Intermech.Kernel.Services.PortalServices.Helper.FormingLogError(ex)));
    }
  }

  private void StartTaskMethod(object args)
  {
    CustomPortalScheduledTasks.StartCustomTask((IUserSession) this._session, this, (long) args);
  }

  public event ImportTaskCompletedEventHandler ImportTaskCompletedEvent;

  public event GetTaskByTypeEventHandler GetTaskByTypeEvent;

  public event ObjectImportedEventHandler ObjectImportedEvent;

  public event ObjectAutoPublishEventHandler ObjectAutoPublishEvent;

  public event ObjectsPublishedEventHandler ObjectsPublishedEvent;

  public event StartResolveBaseVersionConflictEventHandler StartResolveBaseVersionConflictEvent;

  public event CheckPublishCompositionEventHandler CheckPublishCompositionEvent;

  public event ImportTaskErrorEventHandler ImportTaskErrorEvent;

  public event BeforeObjectRefreshEventHandler BeforeObjectRefreshEvent;

  public event ReadImportedObjectAttributesEventHandler ReadImportedObjectAttributesEvent;

  public event RelationImportedEventHandler RelationImportedEvent;
}
