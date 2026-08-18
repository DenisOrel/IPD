// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECO_PRMenuProvider
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.ECO.Client;

public class ECO_PRMenuProvider : ICommandsProvider
{
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (ECOPlugin.FindPlugin() == null || items.Count != 1 || ((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("AcceptPR", new CommandInfo(0, new ClickEventHandler(ECO_PRCommands.AcceptCommand)));
    groupCommands.Add("AcceptPRWithContents", new CommandInfo(0, new ClickEventHandler(ECO_PRCommands.AcceptContentsCommand)));
    return groupCommands;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }
}
