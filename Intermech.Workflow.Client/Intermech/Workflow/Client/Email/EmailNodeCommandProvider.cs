// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.EmailNodeCommandProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Threading;

#nullable disable
namespace Intermech.Workflow.Client.Email;

internal class EmailNodeCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("DownloadMessages", new CommandInfo(0, new ClickEventHandler(this.DownloadMessages)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public void DownloadMessages(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IEmailNode itemData = items.GetItemData(0, typeof (IEmailNode)) as IEmailNode;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DownloadTask task = new DownloadTask(itemData.AccauntEmail, (EmailNode) itemData);
      ((IBackgroundTaskView) ApplicationServices.Container.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) task);
      new Thread(new ParameterizedThreadStart(task.Download))
      {
        Name = $"EmailDownload_{itemData.AccauntEmail}",
        IsBackground = true
      }.Start((object) sessionKeeper.Session);
    }
  }
}
