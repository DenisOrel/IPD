// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProjectCheckInOutCommandsProvider
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

internal class ProjectCheckInOutCommandsProvider : CheckInOutCommandsProvider
{
  public override void Postprocess(CommandsInfo commandsInfo)
  {
    if (this.AllowCheckIn)
      commandsInfo.Add("CheckIn", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.CheckInCommand)));
    if (this.AllowSave)
      commandsInfo.Add("SaveChanges", new Intermech.Navigator.ContextMenu.CommandInfo(4, new ClickEventHandler(this.SaveChangesCommand)));
    if (!this.AllowCancel)
      return;
    commandsInfo.Add("CancelChanges", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.CancelChangesCommand)));
  }

  private static bool SaveChangesScheme(long objID)
  {
    if (Editors.FindEditor(objID, true) is ProjectEditorForm editor)
    {
      editor.Save();
      return true;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(objID, false);
      if (dbObject != null)
      {
        if (dbObject.CheckoutBy == session.UserID)
        {
          dbObject.SaveChanges();
          return true;
        }
      }
    }
    return false;
  }

  private static bool CancelSchemeChanges(long objID)
  {
    if (IMProject.CloseEditor(objID))
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject dbObject = session.GetObject(objID, false);
      if (dbObject != null)
      {
        if (dbObject.CheckoutBy == session.UserID)
        {
          dbObject.CancelChanges();
          return true;
        }
      }
    }
    return false;
  }

  public void CancelChangesCommand(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    if (items.Count <= 0)
      return;
    string empty = string.Empty;
    if (MessageFuncs.Ask(items.Count != 1 ? empty + $"Отменить изменения проектов ImProject ({items.Count})?" : $"Отменить изменения проекта ImProject \"{items.GetItemData<IDBObjectID>(0).Caption}\"?") != DialogResult.Yes)
      return;
    List<long> objectIDs = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData<IDBTypedObjectID>(index);
      if (ProjectCheckInOutCommandsProvider.CancelSchemeChanges(itemData.ObjectID))
        objectIDs.Add(itemData.ObjectID);
    }
    if (objectIDs.Count <= 0)
      return;
    Intermech.Client.Services.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs));
  }

  public void CheckInCommand(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    if (items.Count <= 0 || MessageFuncs.Ask(items.Count != 1 ? $"Завершить изменение проектов ImProject ({items.Count})?" : $"Завершить изменение проекта ImProject \"{items.GetItemData<IDBObjectID>(0).Caption}\"?") != DialogResult.Yes)
      return;
    List<long> objectIDs = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData<IDBTypedObjectID>(index);
      if (IMProject.CheckInProject(itemData.ObjectID))
        objectIDs.Add(itemData.ObjectID);
    }
    if (objectIDs.Count <= 0)
      return;
    Intermech.Client.Services.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs));
    ProjectCheckInOutCommandsProvider.FireRecentChanged();
  }

  public void SaveChangesCommand(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    List<long> objectIDs = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData<IDBTypedObjectID>(index);
      if (ProjectCheckInOutCommandsProvider.SaveChangesScheme(itemData.ObjectID))
        objectIDs.Add(itemData.ObjectID);
    }
    if (objectIDs.Count <= 0)
      return;
    Intermech.Client.Services.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs));
    ProjectCheckInOutCommandsProvider.FireRecentChanged();
  }

  private static void FireRecentChanged()
  {
    Intermech.Client.Services.NotificationService.FireEvent((object) null, new NotificationEventArgs("RecentObjectsChanged"));
  }
}
