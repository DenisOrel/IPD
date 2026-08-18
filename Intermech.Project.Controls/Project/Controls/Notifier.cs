// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.Notifier
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace Intermech.Project.Controls;

internal class Notifier : INotifier
{
  private int _transCount;
  [CanBeNull]
  private Dictionary<Task.EventKind, Notifier.EventObjectsInfo> _pendingList;

  public void Notify([CanBeNull] object sender, Task.EventKind kind, [NotEmpty] long objectID, [CanBeEmpty] long newObjectID)
  {
    if (this.InTransaction)
    {
      Notifier.EventObjectsInfo eventObjectsInfo;
      if (kind == Task.EventKind.CheckIn && this._pendingList.TryGetValue(Task.EventKind.Created, out eventObjectsInfo))
      {
        int index = eventObjectsInfo.ObjectIDs.IndexOf(-objectID);
        if (index != -1)
        {
          eventObjectsInfo.ObjectIDs[index] = objectID;
          return;
        }
      }
      if (!this._pendingList.TryGetValue(kind, out eventObjectsInfo))
      {
        eventObjectsInfo = new Notifier.EventObjectsInfo();
        this._pendingList.Add(kind, eventObjectsInfo);
      }
      eventObjectsInfo.ObjectIDs.Add(objectID);
      eventObjectsInfo.NewObjectIDs.Add(newObjectID);
    }
    else
      Notifier.Fire(kind, new long[1]{ objectID }, new long[1]
      {
        newObjectID
      });
    if (kind != Task.EventKind.Created || !(sender is Intermech.Project.Project))
      return;
    RecentObjectsNode.MRUObjects.Add(objectID, ObjectAction.Create, DateTime.UtcNow);
  }

  private static void Fire(Task.EventKind kind, [NotNull] long[] objectIDs, [CanBeNull] long[] newObjectIDs)
  {
    NotificationEventArgs e = (NotificationEventArgs) null;
    switch (kind)
    {
      case Task.EventKind.Created:
        e = (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) objectIDs);
        break;
      case Task.EventKind.CheckOut:
        e = (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs);
        break;
      case Task.EventKind.CheckIn:
        e = (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs);
        break;
      case Task.EventKind.CancelChanges:
        e = (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs);
        break;
      case Task.EventKind.Changed:
        e = (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs);
        break;
      case Task.EventKind.RefreshMail:
        e = new NotificationEventArgs("MailRefresh");
        break;
    }
    if (e == null)
      return;
    Intermech.Client.Services.NotificationService.FireEvent((object) null, e);
  }

  private bool InTransaction => !object.Equals((object) this._transCount, (object) 0);

  public void Start()
  {
    if (Interlocked.Increment(ref this._transCount) != 1)
      return;
    this._pendingList = new Dictionary<Task.EventKind, Notifier.EventObjectsInfo>();
  }

  public void Commit()
  {
    if (Interlocked.Decrement(ref this._transCount) != 0)
      return;
    foreach (KeyValuePair<Task.EventKind, Notifier.EventObjectsInfo> pending in this._pendingList)
      Notifier.Fire(pending.Key, pending.Value.ObjectIDs.ToArray(), pending.Value.NewObjectIDs.ToArray());
    this._pendingList = (Dictionary<Task.EventKind, Notifier.EventObjectsInfo>) null;
  }

  public void Rollback()
  {
    if (Interlocked.Decrement(ref this._transCount) != 0)
      return;
    this._pendingList = (Dictionary<Task.EventKind, Notifier.EventObjectsInfo>) null;
  }

  private class EventObjectsInfo
  {
    [NotNull]
    public readonly List<long> ObjectIDs = new List<long>();
    [NotNull]
    public readonly List<long> NewObjectIDs = new List<long>();
  }
}
