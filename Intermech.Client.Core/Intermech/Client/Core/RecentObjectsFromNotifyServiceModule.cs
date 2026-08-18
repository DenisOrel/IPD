
// Type: Intermech.Client.Core.RecentObjectsFromNotifyServiceModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Client.Core;

/// <summary>
/// Модуль Ninject отвечающий за подписку на сервис нотификаций и занесение в недавние объекты
/// </summary>
internal sealed class RecentObjectsFromNotifyServiceModule : InitializerModule
{
  private INotificationService notificationService;

  public RecentObjectsFromNotifyServiceModule(INotificationService notificationService)
  {
    this.notificationService = notificationService ?? throw new ArgumentNullException(nameof (notificationService));
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.notificationService.Subscribe(new NotificationEventHandler(this.AddObjectsToRecent));
  }

  protected override void DoShutdown()
  {
    this.notificationService.Unsubscribe(new NotificationEventHandler(this.AddObjectsToRecent));
    base.DoShutdown();
  }

  private void AddObjectsToRecent(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs.Count <= 0)
      return;
    switch (e.EventName)
    {
      case "ObjectsCreated":
        this.UpdateRecentObjects(objectsEventArgs.ObjectIDs, ObjectAction.Create);
        break;
      case "ObjectsChanged":
        this.UpdateRecentObjects(objectsEventArgs.ObjectIDs, ObjectAction.SaveChanges);
        break;
      case "ObjectsCheckedOut":
        if (e is DBObjectsCheckOutEventArgs checkOutEventArgs1)
        {
          this.UpdateRecentObjects(checkOutEventArgs1.NewObjectIDs, ObjectAction.CheckOut);
          break;
        }
        this.UpdateRecentObjects((IList<long>) objectsEventArgs.ObjectIDs.Select<long, long>((Func<long, long>) (x => -x)).ToList<long>(), ObjectAction.CheckOut);
        break;
      case "ObjectsCheckedIn":
        this.UpdateRecentObjects((IList<long>) objectsEventArgs.ObjectIDs.Select<long, long>((Func<long, long>) (x => -x)).ToList<long>(), ObjectAction.CheckIn);
        break;
      case "ObjectsChangesCancelled":
        if (e is DBObjectsCheckOutEventArgs checkOutEventArgs2)
        {
          this.UpdateRecentObjects(checkOutEventArgs2.NewObjectIDs, ObjectAction.CancelChanges);
          break;
        }
        this.UpdateRecentObjects((IList<long>) objectsEventArgs.ObjectIDs.Select<long, long>((Func<long, long>) (x => -x)).ToList<long>(), ObjectAction.CancelChanges);
        break;
    }
  }

  private void UpdateRecentObjects(IList<long> objectIDs, ObjectAction objectAction)
  {
    IRecentObjectsService mruObjects = RecentObjectsNode.MRUObjects;
    DateTime utcNow = DateTime.UtcNow;
    long[] array = objectIDs.ToArray<long>();
    int action = (int) objectAction;
    DateTime date = utcNow;
    mruObjects.Add(array, (ObjectAction) action, date);
  }
}
