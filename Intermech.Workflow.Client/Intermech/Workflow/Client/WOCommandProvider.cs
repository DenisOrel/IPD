// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.WOCommandProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

public class WOCommandProvider : ICommandsProvider
{
  private bool _showWOcommands;

  public WOCommandProvider(bool showWOcommands) => this._showWOcommands = showWOcommands;

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (MailItemsView.NodeCategoryID(items, viewServices) == Intermech.Navigator.Consts.CategoryMailInbox)
    {
      mergedCommands.Add("SendToNext", new CommandInfo(0));
      mergedCommands.Add("SendToBack", new CommandInfo(0));
      if (this._showWOcommands)
      {
        mergedCommands.Add("AcceptWO", new CommandInfo(0, new ClickEventHandler(this.AcceptWOCommand)));
        mergedCommands.Add("RejectWO", new CommandInfo(0, new ClickEventHandler(this.RejectWOCommand)));
      }
      else
      {
        mergedCommands.Add("AcceptWO", new CommandInfo(0));
        mergedCommands.Add("RejectWO", new CommandInfo(0));
      }
    }
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  protected void AcceptReject(ISelectedItems items, bool DoAccept)
  {
    if (items.Count <= sc_21678.ssp_workflow_21679(883532204))
      return;
    HashSet<long> activityIDs = new HashSet<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        IDBAttribute byId = (sessionKeeper.Session.GetObject(itemData.ObjectID, false) ?? throw new NotificationException($"{string.Format(LocalizationHolder.rm.GetString("WOCompleted"), (object) itemData.Caption)} {LocalizationHolder.rm.GetString("MailRefreshNeeded")}")).Attributes.FindByID(wfConsts.AttrActivityID);
        IDBObject dbObject = sessionKeeper.Session.GetObject(byId.AsInteger);
        if (dbObject is IUserActivity userActivity)
        {
          if (DoAccept)
          {
            activityIDs.Add(dbObject.ObjectID);
            userActivity.AcceptWorkOffer();
          }
          else
            userActivity.RejectWorkOffer();
        }
      }
    }
    NotificationEventArgs e = (NotificationEventArgs) new MailRefreshWithReloadWorkOfferEventArgs("MailRefresh", activityIDs);
    BaseHolder.NotificationService.FireEvent((object) null, e);
  }

  protected void AcceptWOCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.AcceptReject(items, true);
  }

  protected void RejectWOCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.AcceptReject(items, false);
  }
}
