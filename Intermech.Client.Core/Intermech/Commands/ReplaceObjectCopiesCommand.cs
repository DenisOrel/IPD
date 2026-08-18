
// Type: Intermech.Commands.ReplaceObjectCopiesCommand
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Search.RecentObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Commands;

public class ReplaceObjectCopiesCommand : ExtendedSelectedItemsCommand
{
  private string subCommandName;
  private string singleObjectWarning;
  private string multipleObjectsWarning;

  public ReplaceObjectCopiesCommand(
    string name,
    string subCommandName,
    string singleObjectWarning,
    string multipleObjectsWarning)
    : base(name)
  {
    this.subCommandName = subCommandName;
    this.singleObjectWarning = singleObjectWarning;
    this.multipleObjectsWarning = multipleObjectsWarning;
  }

  protected override void DoExecute()
  {
    if (!this.GetCommonOptions().HasFlag((Enum) ObjectCommandsOptions.NoConfirmation) && MessageBox.Show(this.GetWarningMessage(), LocalizationHolder.rm.GetString("Client.Core_281"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      return;
    this.ProcessObjects();
  }

  protected virtual void ProcessObjects(System.IServiceProvider subCommandContextServices = null)
  {
    if (subCommandContextServices == null)
      subCommandContextServices = this.ContextServices;
    HistoryProcessor historyProcessor = new HistoryProcessor();
    GetObjectCopiesTask objectLists = new GetObjectCopiesTask(1, this.GetActionType(this.subCommandName));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> longList = new List<long>(this.Items.Count);
      this.DoBeforeProceedItems(sessionKeeper.Session);
      try
      {
        for (int index = 0; index < this.Items.Count; ++index)
        {
          IDBTypedObjectID itemData = this.Items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          if (!objectLists.PreviousObjectVersions.Contains(itemData.ObjectID))
          {
            this.DoBeforeProceedItem(index);
            historyProcessor.Start(sessionKeeper.Session);
            try
            {
              ObjectCopyCommand copyCommandByName = ObjectCommandFactory.CreateObjectCopyCommandByName(this.subCommandName, true);
              longList.Add(itemData.ObjectID);
              copyCommandByName.ObjectId = itemData.ObjectID;
              copyCommandByName.UpdateUI = false;
              copyCommandByName.CommonOptions = this.CommonOptions;
              copyCommandByName.ContextServices = subCommandContextServices;
              copyCommandByName.Execute();
            }
            finally
            {
              objectLists.ProcessModifications(historyProcessor.Stop(sessionKeeper.Session));
              this.DoAfterProceedItem(index);
            }
          }
        }
      }
      finally
      {
        if (longList.Count > 0)
          ((IRecentObjectsClientService) ServicesManager.GetService(typeof (IRecentObjectsClientService))).AddToCurrentUserRecentObjects(longList.ToArray());
        ((INotificationService) ServicesManager.GetService(typeof (INotificationService))).FireEvent((object) null, this.GetObjectNotifyArgs(this.subCommandName, objectLists));
        this.DoAfterProceedItems(sessionKeeper.Session);
      }
    }
  }

  protected virtual string GetWarningMessage()
  {
    return this.Items.Count > 1 ? string.Format(this.multipleObjectsWarning, (object) this.Items.Count) : string.Format(this.singleObjectWarning, (object) ((IDBObjectID) this.Items.GetItemData(0, typeof (IDBTypedObjectID))).Caption);
  }

  private ActionType GetActionType(string commandName)
  {
    switch (commandName)
    {
      case "Checkout":
        return ActionType.CheckOut;
      case "Checkin":
        return ActionType.CheckIn;
      default:
        throw new NotSupportedException();
    }
  }

  private ObjectAction GetObjectAction(string commandName)
  {
    switch (commandName)
    {
      case "Checkout":
        return ObjectAction.CheckOut;
      case "Checkin":
        return ObjectAction.CheckIn;
      default:
        throw new NotSupportedException();
    }
  }

  private NotificationEventArgs GetObjectNotifyArgs(
    string commandName,
    GetObjectCopiesTask objectLists)
  {
    switch (commandName)
    {
      case "Checkout":
        return (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectLists.PreviousObjectVersions, (IList<long>) objectLists.CurrentObjectVersions);
      case "Checkin":
        return (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objectLists.PreviousObjectVersions);
      default:
        throw new NotSupportedException();
    }
  }
}
