// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AllObjectsCommandProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow.Design;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

public class AllObjectsCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  protected void LaunchProcessCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    wfFunx.CreateProcess(0L, (ISimpleSelectedItems) items);
  }
}
