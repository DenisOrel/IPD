// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Navigator.Commands.BlankSetupCommandProvider
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Document.Client.Commands;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Navigator.Commands;

internal class BlankSetupCommandProvider : ICommandsProvider
{
  CommandsInfo ICommandsProvider.GetGroupCommands(
    ISelectedItems items,
    IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  CommandsInfo ICommandsProvider.GetMergedCommands(
    ISelectedItems items,
    IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0 || viewServices == null || !(viewServices.GetService(typeof (IViewState)) is IViewState service))
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items.Count != 1)
    {
      mergedCommands.Suppress("EditDocument", 4);
      mergedCommands.Suppress("ViewDocument", 4);
      return mergedCommands;
    }
    if (!(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID))
    {
      mergedCommands.Suppress("EditDocument", 4);
      mergedCommands.Suppress("ViewDocument", 4);
    }
    else
    {
      if (service.ViewState.HasFlag((Enum) ViewStateFlags.ReadOnly))
        mergedCommands.Suppress("EditDocument", 4);
      else
        mergedCommands.Add("EditDocument", new CommandInfo(4, new ClickEventHandler(BlankSetupCommandProvider.BlankSetupEditCommand)));
      mergedCommands.Add("ViewDocument", new CommandInfo(4, new ClickEventHandler(BlankSetupCommandProvider.BlankSetupViewCommand)));
    }
    return mergedCommands;
  }

  private static void BlankSetupEditCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    new BlankSetupCommand(BlankSetupCommandMode.Edit).Execute(items, viewServices, additionalInfo);
  }

  private static void BlankSetupViewCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    new BlankSetupCommand(BlankSetupCommandMode.View).Execute(items, viewServices, additionalInfo);
  }

  public static void Register([NotNull] IFactory factory)
  {
    BlankSetupCommandProvider provider = new BlankSetupCommandProvider();
    factory.AddCommandsProvider(1, BlankConsts.ObjectType.BlankSetupId, (ICommandsProvider) provider);
  }
}
