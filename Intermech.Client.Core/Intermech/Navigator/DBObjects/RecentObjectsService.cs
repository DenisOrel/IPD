
// Type: Intermech.Navigator.DBObjects.RecentObjectsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Search.RecentObjects;
using System;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Сервис, позволяющий управлять коллекцией недавних объектов
/// </summary>
internal sealed class RecentObjectsService : IRecentObjectsService
{
  /// <summary>
  /// Объект для синхронизации доступа к полям экземпляра класса
  /// </summary>
  private object syncRoot = new object();
  /// <summary>
  /// Фоновый поток для обработки заданий от "Недавних объектов"
  /// </summary>
  private Thread thread;
  /// <summary>Дата и время старта задачи обновления</summary>
  private DateTime startTaskTime = DateTime.MinValue;
  /// <summary>
  /// В течение какого периода времени задача по обновлению считается отложенной
  /// </summary>
  private TimeSpan delta = new TimeSpan(0, 0, 0, 1, 500);
  /// <summary>Выполняется ли задание по обновлению</summary>
  private bool inEvent;
  /// <summary>Есть ли задание на обновление</summary>
  private bool hasTask;
  /// <summary>Обработчик событий от глобальной службы уведомлений</summary>
  private NotificationEventHandler globalNotifyHandler;

  /// <summary>Стартовать службу, управляющую недавними объектами</summary>
  public static void StartService()
  {
    if (ServicesManager.GetService(typeof (IRecentObjectsService)) is IRecentObjectsService)
      return;
    RecentObjectsService serviceInstance = new RecentObjectsService();
    ServicesManager.AddService(typeof (IRecentObjectsService), (object) serviceInstance);
    serviceInstance.Start();
  }

  /// <summary>
  /// Добавить/заменить существующий объект
  /// (метод потокобезопасен)
  /// </summary>
  /// <param name="objectID">ID версии объекта</param>
  /// <param name="action">Действие, выполненное над объектом</param>
  /// <param name="date">Дата и время (UTC) выполнения этого действия</param>
  /// <returns>Вновь добавленный или существующий объект</returns>
  public void Add(long objectID, ObjectAction action, DateTime date)
  {
    IRecentObjectsClientService service = (IRecentObjectsClientService) ServicesManager.GetService(typeof (IRecentObjectsClientService));
    RecentObjectsSettings recentObjectsSettings = service.GetCurrentUserRecentObjectsSettings();
    if (recentObjectsSettings != null && !recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) (RecentObjectAction) action))
      return;
    service.AddToCurrentUserRecentObjects(new long[1]
    {
      objectID
    });
  }

  /// <summary>
  /// Добавить/заменить существующие объекты
  /// (метод потокобезопасен)
  /// </summary>
  /// <param name="objectVersionIds">ID версий объектов</param>
  /// <param name="action">Действие, выполненное над объектами</param>
  /// <param name="date">Дата и время (UTC) выполнения этого действия</param>
  public void Add(long[] objectVersionIds, ObjectAction action, DateTime date)
  {
    IRecentObjectsClientService service = (IRecentObjectsClientService) ServicesManager.GetService(typeof (IRecentObjectsClientService));
    RecentObjectsSettings recentObjectsSettings = service.GetCurrentUserRecentObjectsSettings();
    if (recentObjectsSettings != null && !recentObjectsSettings.AllowableRecentObjectActions.HasFlag((Enum) (RecentObjectAction) action))
      return;
    service.AddToCurrentUserRecentObjects(objectVersionIds);
  }

  private RecentObjectAction ConvertToRecentObjectAction(ObjectAction objectAction)
  {
    switch (objectAction)
    {
      case ObjectAction.Create:
        return RecentObjectAction.Create;
      case ObjectAction.CheckOut:
        return RecentObjectAction.CheckOut;
      case ObjectAction.CheckIn:
        return RecentObjectAction.CheckIn;
      case ObjectAction.CancelChanges:
        return RecentObjectAction.CancelChanges;
      case ObjectAction.SaveChanges:
        return RecentObjectAction.SaveChanges;
      case ObjectAction.OpenInNewWindow:
        return RecentObjectAction.OpenInNewWindow;
      case ObjectAction.Open:
        return RecentObjectAction.Open;
      case ObjectAction.Edit:
        return RecentObjectAction.Edit;
      case ObjectAction.View:
        return RecentObjectAction.View;
      case ObjectAction.Print:
        return RecentObjectAction.Print;
      default:
        throw new NotSupportedException();
    }
  }

  /// <summary>
  /// Стартовать работу службы
  /// (метод потокобезопасен)
  /// </summary>
  private void Start()
  {
    this.Stop();
    lock (this.syncRoot)
    {
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
      {
        this.globalNotifyHandler = new NotificationEventHandler(this.GlobalNotificationEventFired);
        service.Subscribe(this.globalNotifyHandler);
      }
      this.thread = new Thread(new ThreadStart(this.ThreadMethod));
      this.thread.IsBackground = true;
      this.thread.Name = "Navigator.RecentObjectsService";
      this.thread.Start();
    }
  }

  /// <summary>
  /// Остановить работу службы
  /// (метод потокобезопасен)
  /// </summary>
  private void Stop()
  {
    lock (this.syncRoot)
    {
      if (this.globalNotifyHandler != null && ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
      {
        service.Unsubscribe(this.globalNotifyHandler);
        this.globalNotifyHandler = (NotificationEventHandler) null;
      }
      this.thread = (Thread) null;
    }
  }

  /// <summary>
  /// Выполняется ли задание по обновлению
  /// (потокобезопасный метод)
  /// </summary>
  private bool InEvent
  {
    get
    {
      lock (this.syncRoot)
        return this.inEvent;
    }
    set
    {
      lock (this.syncRoot)
        this.inEvent = value;
    }
  }

  /// <summary>
  /// Есть ли задание на обновление
  /// (потокобезопасный метод)
  /// </summary>
  private bool HasTask
  {
    get
    {
      lock (this.syncRoot)
        return this.hasTask;
    }
    set
    {
      lock (this.syncRoot)
        this.hasTask = value;
    }
  }

  /// <summary>
  /// Дата и время старта задачи обновления
  /// (потокобезопасный метод)
  /// </summary>
  private DateTime StartTaskTime
  {
    get
    {
      lock (this.syncRoot)
        return this.startTaskTime;
    }
    set
    {
      lock (this.syncRoot)
        this.startTaskTime = value;
    }
  }

  /// <summary>Выполнить обновление недавних объектов</summary>
  private void PerformTask()
  {
    IMainFormUpdate service1 = ServicesManager.GetService(typeof (IMainFormUpdate)) as IMainFormUpdate;
    IRecentObjectsWindow service2 = ServicesManager.GetService(typeof (IRecentObjectsWindow)) as IRecentObjectsWindow;
    if (service1 == null || service1.MainForm == null || service2 == null || service1.MainForm.IsDisposed)
      return;
    service1.MainForm.Invoke((Delegate) new MethodInvoker(service2.Update));
  }

  /// <summary>
  /// Метод фонового потока, выполняющий управление коллекцией недавних объектов и занимающийся рассылкой уведомлений
  /// </summary>
  private void ThreadMethod()
  {
    try
    {
      int num1 = 900;
      int num2 = num1;
      long num3 = 0;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        num3 = sessionKeeper.Session.MetaDataGeneration;
      long num4 = num3;
      while (true)
      {
        try
        {
          lock (this.syncRoot)
          {
            if (this.thread == null)
              break;
          }
          DateTime startTaskTime = this.StartTaskTime;
          if (!this.InEvent && this.HasTask && DateTime.UtcNow - startTaskTime >= this.delta)
          {
            if (startTaskTime > DateTime.MinValue)
            {
              try
              {
                this.InEvent = true;
                this.PerformTask();
              }
              finally
              {
                this.InEvent = false;
                this.StartTaskTime = DateTime.MinValue;
                this.HasTask = false;
              }
            }
          }
          --num2;
          if (num2 <= 0)
          {
            if (!this.InEvent)
            {
              num2 = num1;
              try
              {
                lock (this.syncRoot)
                {
                  if (this.thread == null)
                    break;
                }
                this.InEvent = true;
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                  num4 = sessionKeeper.Session.MetaDataGeneration;
                if (num3 > 0L && num4 > 0L && num4 != num3)
                {
                  lock (this.syncRoot)
                  {
                    if (this.thread == null)
                      break;
                  }
                  using (SessionKeeper sessionKeeper = new SessionKeeper())
                  {
                    IClientCache clientCache = ((IClientSession) sessionKeeper.Session).ClientCache;
                    lock (this.syncRoot)
                    {
                      if (this.thread == null)
                        break;
                    }
                    if (clientCache != null)
                    {
                      clientCache.ReloadCache(sessionKeeper.Session);
                      lock (this.syncRoot)
                      {
                        if (this.thread == null)
                          break;
                      }
                      MetaDataHelper.SyncMetadata(clientCache.CacheDataSet, true);
                      IElementStatusesService customService1 = sessionKeeper.Session.GetCustomService(typeof (IElementStatusesService)) as IElementStatusesService;
                      IPluginStatusesTable customService2 = sessionKeeper.Session.GetCustomService(typeof (IPluginStatusesTable)) as IPluginStatusesTable;
                      Holder.ElementStatusesClientService.SyncWithServerSide(customService1, customService2);
                      Holder.ElementStatusesClientService.LoadUserSettings(sessionKeeper.Session);
                      if (!(ServicesManager.GetService(typeof (StatusesInfoService)) is StatusesInfoService service))
                        ServicesManager.AddService(typeof (StatusesInfoService), (object) new StatusesInfoService());
                      else
                        service.Reload();
                    }
                  }
                }
                num3 = num4;
              }
              finally
              {
                this.InEvent = false;
              }
            }
          }
        }
        catch
        {
        }
        Thread.Sleep(1000);
      }
    }
    catch
    {
    }
  }

  /// <summary>Событие от глобальной службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void GlobalNotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (!(e.EventName == "ApplicationClosed"))
      return;
    lock (this.syncRoot)
      this.thread = (Thread) null;
  }
}
