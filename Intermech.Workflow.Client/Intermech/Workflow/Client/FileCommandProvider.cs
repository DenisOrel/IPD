// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.FileCommandProvider
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

internal class FileCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("ReplaceFile", new CommandInfo(0, new ClickEventHandler(this.ReplaceFileCommand)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void ReplaceFileCommand(
    ISelectedItems items,
    IServiceProvider services,
    object additionalInfo)
  {
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      wfFunx.AddFileToObject(itemData.ObjectType, itemData.ObjectID);
    }
  }
}
