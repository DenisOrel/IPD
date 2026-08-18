// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetaDataHelperUpdateService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Threading;


namespace Intermech.Kernel.Services;

internal class MetaDataHelperUpdateService
{
  private object _syncRoot = new object();
  private Thread _thread;
  private MetaDataHelperServiceUpdateTask _task;
  private DateTime _startTaskTime = DateTime.MinValue;
  private static TimeSpan _delta = new TimeSpan(0, 0, 1);
  private bool _inEvent;
  private IUserSession _session;
  internal long _generation;

  public MetaDataHelperUpdateService() => this.Start();

  public static MetaDataHelperUpdateService RegisterService()
  {
    if (!(ServerServices.GetService(typeof (MetaDataHelperUpdateService)) is MetaDataHelperUpdateService serviceInstance))
    {
      serviceInstance = new MetaDataHelperUpdateService();
      ServerServices.AddService(typeof (MetaDataHelperUpdateService), (object) serviceInstance);
    }
    return serviceInstance;
  }

  public static void AddTask(MetaDataHelperServiceUpdateTask task)
  {
    MetaDataHelperUpdateService.RegisterService().InternalAddTask(task);
  }

  private void InternalAddTask(MetaDataHelperServiceUpdateTask task)
  {
    if (task == MetaDataHelperServiceUpdateTask.None)
      return;
    while (this.InEvent)
      Thread.Sleep(100);
    lock (this._syncRoot)
    {
      this._task |= task;
      this._startTaskTime = DateTime.UtcNow;
    }
  }

  private void Start()
  {
    this._thread = new Thread(new ThreadStart(this.MainThreadMethod));
    this.StartThread(this._thread, "MetaDataHelper service thread");
  }

  private void Stop() => this._thread = (Thread) null;

  public long MetaDataGeneration
  {
    get
    {
      lock (this._syncRoot)
      {
        if (this._session == null)
          return 0;
        string s = this._session.Configurations.ReadStringNoCache(MetaDataHelper.MetaDataGenerationModule, MetaDataHelper.MetaDataGenerationSection, MetaDataHelper.MetaDataGenerationKey, true);
        long result;
        return string.IsNullOrEmpty(s) ? -1L : (long.TryParse(s, out result) ? result : 0L);
      }
    }
    internal set
    {
      lock (this._syncRoot)
      {
        if (this._session == null)
          return;
        this._session.Configurations.WriteString(MetaDataHelper.MetaDataGenerationModule, MetaDataHelper.MetaDataGenerationSection, MetaDataHelper.MetaDataGenerationKey, Convert.ToString(value), 0L);
      }
    }
  }

  internal bool InEvent
  {
    get
    {
      lock (this._syncRoot)
        return this._inEvent;
    }
    set
    {
      lock (this._syncRoot)
        this._inEvent = value;
    }
  }

  internal MetaDataHelperServiceUpdateTask Task
  {
    get
    {
      lock (this._syncRoot)
        return this._task;
    }
    set
    {
      lock (this._syncRoot)
        this._task = value;
    }
  }

  internal DateTime StartTaskTime
  {
    get
    {
      lock (this._syncRoot)
        return this._startTaskTime;
    }
    set
    {
      lock (this._syncRoot)
        this._startTaskTime = value;
    }
  }

  internal void Touch()
  {
    lock (this._syncRoot)
    {
      long metaDataGeneration = this.MetaDataGeneration;
      if (metaDataGeneration < 0L)
        return;
      this._generation = metaDataGeneration;
    }
  }

  private void StartThread(Thread thread, string name)
  {
    thread.Name = name;
    thread.IsBackground = true;
    thread.Start();
  }

  private void MainThreadMethod()
  {
    IDBTimedEvents dbTimedEvents = (IDBTimedEvents) null;
    while (dbTimedEvents == null)
    {
      dbTimedEvents = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
      Thread.Sleep(1000);
    }
    this.Touch();
    int num1 = 600;
    int num2 = num1;
    bool flag = false;
    try
    {
      while (this._thread != null)
      {
        try
        {
          MetaDataHelperServiceUpdateTask task = this.Task;
          if (this._session == null)
          {
            this._session = dbTimedEvents.GetSystemSessionPermanentClone("MetaDataHelperService");
            flag = true;
          }
          if (this._session != null)
          {
            --num2;
            if (num2 <= 0 || (task & MetaDataHelperServiceUpdateTask.MetaDataGeneration) == MetaDataHelperServiceUpdateTask.MetaDataGeneration)
            {
              num2 = num1;
              long metaDataGeneration = this.MetaDataGeneration;
              lock (this._syncRoot)
              {
                if (this._generation != metaDataGeneration && this._generation > 0L && metaDataGeneration >= 0L)
                {
                  this._task = (task |= MetaDataHelperServiceUpdateTask.MetaDataCacheGeneration);
                  this._startTaskTime = DateTime.UtcNow;
                }
                if (metaDataGeneration >= 0L)
                  this._generation = metaDataGeneration;
              }
            }
          }
          DateTime startTaskTime = this.StartTaskTime;
          if (task != MetaDataHelperServiceUpdateTask.None)
          {
            if (DateTime.UtcNow - startTaskTime >= MetaDataHelperUpdateService._delta)
            {
              if (startTaskTime > DateTime.MinValue)
              {
                try
                {
                  this.InEvent = true;
                  if (this._session != null)
                  {
                    if ((task & MetaDataHelperServiceUpdateTask.MetaDataIncGeneration) == MetaDataHelperServiceUpdateTask.MetaDataIncGeneration)
                    {
                      ++this.MetaDataGeneration;
                      this.Touch();
                      this.Task = task & ~MetaDataHelperServiceUpdateTask.MetaDataIncGeneration;
                    }
                    if ((task & MetaDataHelperServiceUpdateTask.MetaDataCacheGeneration) == MetaDataHelperServiceUpdateTask.MetaDataCacheGeneration)
                    {
                      (ServerServices.GetService(typeof (IAdminUtilsService)) as IAdminUtilsService).ReloadCache(this._session.SessionGUID);
                      this.Touch();
                      task |= MetaDataHelperServiceUpdateTask.Full;
                    }
                  }
                  this.UpdateMetaDataHelper(this._session, task);
                }
                finally
                {
                  this.InEvent = false;
                  this.StartTaskTime = DateTime.MinValue;
                  this.Task = MetaDataHelperServiceUpdateTask.None;
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          if (ServerServices.GetService(typeof (IOutputView)) is IOutputView service)
          {
            service.WriteString("MetaDataHelper", $"Exception source:\n{ex.Source}");
            service.WriteString("MetaDataHelper", $"Exception message:\n{ex.Message}");
            service.WriteString("MetaDataHelper", $"Exception stack:\n{ex.StackTrace}");
            if (ex.InnerException != null)
            {
              service.WriteString("MetaDataHelper", $"Inner exception source:\n{ex.InnerException.Source}");
              service.WriteString("MetaDataHelper", $"Inner exception message:\n{ex.InnerException.Message}");
              service.WriteString("MetaDataHelper", $"Inner exception stack:\n{ex.InnerException.StackTrace}");
            }
          }
        }
        Thread.Sleep(1000);
      }
    }
    finally
    {
      if (flag && this._session != null)
      {
        this._session.Logout("MetaDataHelperService");
        this._session = (IUserSession) null;
      }
    }
  }

  private void UpdateMetaDataHelper(IUserSession session, MetaDataHelperServiceUpdateTask task)
  {
    bool forced = MetaDataHelper.Forced;
    try
    {
      if (!(ServerServices.GetService(typeof (ICacheDataset)) is CacheDataset service1))
        return;
      MetaDataHelper.Forced = true;
      if ((task & MetaDataHelperServiceUpdateTask.Full) == MetaDataHelperServiceUpdateTask.Full)
        MetaDataHelper.SyncObjectTypesMetadata(service1._DBSet);
      if ((task & MetaDataHelperServiceUpdateTask.ObjectTypesHierarchy) == MetaDataHelperServiceUpdateTask.ObjectTypesHierarchy)
        MetaDataHelper.SyncObjectTypesHierarchy(service1._DBSet);
      if ((task & MetaDataHelperServiceUpdateTask.RelationTypes) == MetaDataHelperServiceUpdateTask.RelationTypes)
        MetaDataHelper.SyncRelationTypesMetadata(service1._DBSet);
      if ((task & MetaDataHelperServiceUpdateTask.AttrTypes) == MetaDataHelperServiceUpdateTask.AttrTypes)
        MetaDataHelper.SyncAttrTypesMetadata(service1._DBSet);
      if ((task & MetaDataHelperServiceUpdateTask.SpecialRelationTypes) == MetaDataHelperServiceUpdateTask.SpecialRelationTypes)
        MetaDataHelper.SyncSpecialRelationTypes(service1._DBSet);
      if ((task & MetaDataHelperServiceUpdateTask.SpecialObjectTypes) == MetaDataHelperServiceUpdateTask.SpecialObjectTypes)
        MetaDataHelper.SyncSpecialObjectTypes(service1._DBSet);
      if ((task & MetaDataHelperServiceUpdateTask.LCSteps) == MetaDataHelperServiceUpdateTask.LCSteps)
      {
        MetaDataHelper.SyncLCStepsMetadata(service1._DBSet);
        if (session != null)
        {
          IPluginStatusesTable service2 = ServerServices.GetService(typeof (IPluginStatusesTable)) as IPluginStatusesTable;
          VersionSelectionStatuses.ReloadLevelsStatuses(session, service2);
        }
      }
      MetaDataHelper.SyncGlobals(service1._DBSet);
    }
    finally
    {
      MetaDataHelper.Forced = forced;
      MetaDataHelper.Touch();
    }
  }
}
