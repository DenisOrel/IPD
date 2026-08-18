// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FilesProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FilesProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("Cut", new CommandInfo(0, new ClickEventHandler(FileStorageInfoCommands.CutCommand)));
    return groupCommands;
  }
}
