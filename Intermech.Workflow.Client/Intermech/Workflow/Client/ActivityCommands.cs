// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.ActivityCommands
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

public class ActivityCommands : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("ViewDocument", new CommandInfo(0, new ClickEventHandler(this.ViewCommand)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void ViewCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    for (int index = 0; index < items.Count; ++index)
      wfFunx.ShowActivityProperties((items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID);
  }
}
