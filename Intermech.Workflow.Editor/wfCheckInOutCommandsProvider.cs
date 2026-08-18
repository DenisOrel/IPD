// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Editor.wfCheckInOutCommandsProvider
// Assembly: Intermech.Workflow.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 48E18BC1-AABA-4AA1-97DA-4BBD788BE326
// Assembly location: D:\IPS\Client\Intermech.Workflow.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Editor.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Editor;

internal class wfCheckInOutCommandsProvider : CheckInOutCommandsProvider
{
  public override void Postprocess(CommandsInfo commandsInfo)
  {
    if (this.AllowCheckIn)
      commandsInfo.Add("CheckIn", new CommandInfo(0, new ClickEventHandler(this.CheckInCommand)));
    if (this.AllowSave)
      commandsInfo.Add("SaveChanges", new CommandInfo(4, new ClickEventHandler(this.SaveChangesCommand)));
    if (!this.AllowCancel)
      return;
    commandsInfo.Add("CancelChanges", new CommandInfo(0, new ClickEventHandler(this.CancelChangesCommand)));
  }

  private bool CheckInScheme(long objID)
  {
    if ((Holder.Editors.FindEditor(objID, true) is wfEditorForm editor ? editor.Parent : (Control) null) is DockControl)
    {
      DockControl parent = (DockControl) editor.Parent;
      editor.AutoSaveOnClose = DialogResult.Yes;
      parent.Close();
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      session.ClearObjectSmartCache();
      IDBObject dbObject = session.GetObject(objID, false);
      if (dbObject != null)
      {
        if (dbObject.CheckoutBy == session.UserID)
        {
          dbObject.CheckIn();
          return true;
        }
      }
    }
    return false;
  }

  private bool SaveChangesScheme(long objID)
  {
    if (Holder.Editors.FindEditor(objID, true) is wfEditorForm editor)
    {
      editor.Save();
      return true;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      session.ClearObjectSmartCache();
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

  private bool CancelSchemeChanges(long objID)
  {
    if ((Holder.Editors.FindEditor(objID, true) is wfEditorForm editor ? editor.Parent : (Control) null) is DockControl)
    {
      DockControl parent = (DockControl) editor.Parent;
      editor.AutoSaveOnClose = DialogResult.No;
      parent.Close();
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      session.ClearObjectSmartCache();
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
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count <= 0)
      return;
    string empty = string.Empty;
    string s;
    if (items.Count == sc_22030.ssp_workflow_22031(439485388))
    {
      IDBObjectID itemData = items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID;
      s = string.Format(LocalizationHolder.rm.GetString("Workflow.Editor_CancelChanges"), (object) itemData.Caption);
    }
    else
      s = empty + string.Format(LocalizationHolder.rm.GetString("Workflow.Editor_CancelChanges2"), (object) items.Count);
    if (MessageFuncs.Ask(s) != DialogResult.Yes)
      return;
    List<long> objectIDs = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      if (this.CancelSchemeChanges(itemData.ObjectID))
        objectIDs.Add(itemData.ObjectID);
    }
    if (objectIDs.Count <= 0)
      return;
    DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs);
    BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
  }

  public void CheckInCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items.Count <= 0)
      return;
    string empty = string.Empty;
    string text;
    if (items.Count == 1)
    {
      IDBObjectID itemData = items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID;
      text = string.Format(LocalizationHolder.rm.GetString("Workflow.Editor_CheckIn"), (object) itemData.Caption);
    }
    else
      text = string.Format(LocalizationHolder.rm.GetString(sc_22030.ssp_workflow_22032()), (object) items.Count);
    if (MessageBox.Show(text, LocalizationHolder.rm.GetString("Workflow.Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    List<long> objectIDs = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      if (this.CheckInScheme(itemData.ObjectID))
        objectIDs.Add(itemData.ObjectID);
    }
    if (objectIDs.Count <= 0)
      return;
    DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs);
    BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
    this.FireRecentChanged();
  }

  public void SaveChangesCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> objectIDs = new List<long>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      if (this.SaveChangesScheme(itemData.ObjectID))
        objectIDs.Add(itemData.ObjectID);
    }
    if (objectIDs.Count <= 0)
      return;
    DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs);
    BaseHolder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
    this.FireRecentChanged();
  }

  private void FireRecentChanged()
  {
    BaseHolder.NotificationService.FireEvent((object) null, new NotificationEventArgs("RecentObjectsChanged"));
  }
}
