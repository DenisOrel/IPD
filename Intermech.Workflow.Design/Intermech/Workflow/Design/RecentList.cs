// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.RecentList
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Design;

public class RecentList(string name) : BaseRecentList(name, 5)
{
  public void LoadCaptions()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.LoadCaptions(sessionKeeper.Session);
  }

  public int Load()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.Load(sessionKeeper.Session);
  }

  private INotificationService TryGetNotificationService()
  {
    return (INotificationService) ApplicationServices.Container.GetService(typeof (INotificationService));
  }

  /// <summary>
  /// Заставляет перечитать список в окне редактора шаблонов
  /// </summary>
  protected void NotifyChanged()
  {
    INotificationService notificationService = this.TryGetNotificationService();
    if (notificationService == null)
      return;
    NotificationEventArgs e = new NotificationEventArgs(this.Name + "Changed");
    notificationService.FireEvent((object) null, e);
  }

  public void AddRecent(long objectID)
  {
    if (objectID == 0L)
      return;
    int index = this._list.IndexOf(objectID);
    if (index != -1)
      this._list.RemoveAt(index);
    if (this._list.Count > this._maxcount - 1)
      this._list.RemoveRange(this._maxcount - 1, this._list.Count - this._maxcount + 1);
    this._list.Insert(0, objectID);
    this.Save();
    this.LoadCaptions();
    this.NotifyChanged();
  }

  public void RemoveRecent(IList<long> objectIDs)
  {
    bool flag = false;
    foreach (long objectId in (IEnumerable<long>) objectIDs)
    {
      int index = this._list.IndexOf(objectId);
      if (index != -1)
      {
        this._list.RemoveAt(index);
        this._captions.RemoveAt(index);
        flag = true;
      }
    }
    if (!flag)
      return;
    this.Save();
    this.NotifyChanged();
  }
}
