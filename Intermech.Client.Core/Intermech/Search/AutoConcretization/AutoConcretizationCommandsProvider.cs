
// Type: Intermech.Search.AutoConcretization.AutoConcretizationCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search.AutoConcretization;

public sealed class AutoConcretizationCommandsProvider : ICommandsProvider
{
  private IAutoConcretizationClientService _autoConcretizationClientService;

  public AutoConcretizationCommandsProvider(
    IAutoConcretizationClientService autoConcretizationClientService)
  {
    this._autoConcretizationClientService = autoConcretizationClientService != null ? autoConcretizationClientService : throw new ArgumentNullException(nameof (autoConcretizationClientService));
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    CommandsInfo groupCommands = new CommandsInfo();
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (this.CheckParamsForEnableAutoConcretization(items, out navigatorTreeNode))
      groupCommands.Add("EnableAutoConcretization", new CommandInfo(-1, new ClickEventHandler(this.EnableAutoConcretization)));
    if (this.CheckParamsForDisableAutoConcretization(items, out navigatorTreeNode))
      groupCommands.Add("DisableAutoConcretization", new CommandInfo(-1, new ClickEventHandler(this.DisableAutoConcretization)));
    return groupCommands;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  private bool CheckParamsForEnableAutoConcretization(
    ISelectedItems selectedItems,
    out NavigatorTreeNode navigatorTreeNode)
  {
    if (SelectedItemsHelper.TryGetSingleNavigatorTreeNode(selectedItems, out navigatorTreeNode) && this._autoConcretizationClientService.CanEnableAutoConcretization(navigatorTreeNode))
      return true;
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  private void EnableAutoConcretization(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForEnableAutoConcretization(items, out navigatorTreeNode))
      throw new ArgumentException();
    this._autoConcretizationClientService.EnableAutoConcretization(navigatorTreeNode);
  }

  private bool CheckParamsForDisableAutoConcretization(
    ISelectedItems selectedItems,
    out NavigatorTreeNode navigatorTreeNode)
  {
    if (SelectedItemsHelper.TryGetSingleNavigatorTreeNode(selectedItems, out navigatorTreeNode) && this._autoConcretizationClientService.CanDisableAutoConcretization(navigatorTreeNode))
      return true;
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  private void DisableAutoConcretization(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForDisableAutoConcretization(items, out navigatorTreeNode))
      throw new ArgumentException();
    this._autoConcretizationClientService.DisableAutoConcretization(navigatorTreeNode);
  }
}
