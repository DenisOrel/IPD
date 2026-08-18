
// Type: Intermech.Navigator.Controls.NavigatorTreeViewCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

public sealed class NavigatorTreeViewCommandsProvider : ICommandsProvider
{
  private NavigatorTreeView _navigatorTreeView;
  private LazyService<INotificationService> _notificationService = new LazyService<INotificationService>();

  public NavigatorTreeViewCommandsProvider(NavigatorTreeView navigatorTreeView)
  {
    this._navigatorTreeView = navigatorTreeView != null ? navigatorTreeView : throw new ArgumentNullException(nameof (navigatorTreeView));
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    if ((items.Count == 1 || this._navigatorTreeView.CheckedItems.Count > 0) && this._navigatorTreeView.FocusedNode != null)
    {
      bool flag = this._navigatorTreeView.FocusedNode.HasChildren || this._navigatorTreeView.FocusedNode.Children.Count > 0;
      groupCommands.Add("Refresh", new CommandInfo(0, new ClickEventHandler(this.TreeRefreshNodeCommand)));
      groupCommands.Add("SeekInTree", new CommandInfo(0, new ClickEventHandler(this.SeekInTreeCommand)));
      if (this._navigatorTreeView.FocusedNode.Expanded & flag)
        groupCommands.Add("CollapseNode", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.TreeCollapseNodeCommand)));
      else if (!this._navigatorTreeView.FocusedNode.Expanded & flag)
        groupCommands.Add("ExpandNode", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.TreeExpandNodeCommand)));
      if (flag)
        groupCommands.Add("ExpandNodeRecursive", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.TreeRecursiveExpandNodeCommand)));
    }
    if (this._navigatorTreeView.SupportedColumns != null && this._navigatorTreeView.SupportedColumns.Count > 1)
      groupCommands.Add("SetupColumns", new CommandInfo(64 /*0x40*/, new ClickEventHandler(this.TreeSetupColumnsCommand)));
    ISelectedItems selectedItems = this._navigatorTreeView.SelectedItems;
    if (selectedItems != null && selectedItems.Count > 0 && ManualSortingEditForm.FindFirstSortingObjectItem(items) == 0)
      groupCommands.Add("ManualSortingSetup", new CommandInfo(0, new ClickEventHandler(this.TreeManualSortingSetupCommand)));
    return groupCommands;
  }

  private void TreeManualSortingSetupCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ISelectedItems selectedItems = this._navigatorTreeView.SelectedItems;
    if (selectedItems == null || selectedItems.Count == 0)
      return;
    long[] ChRels = (long[]) null;
    if (ManualSortingEditForm.Execute(string.Empty, selectedItems, this._navigatorTreeView.Services, out ChRels) != DialogResult.OK || ChRels.Length == 0)
      return;
    this._notificationService.Value.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("SortedRelationsChanged", (IList<long>) ChRels));
    this._notificationService.Value.FireEvent((object) this, new NotificationEventArgs("FiltrationChanged"));
  }

  private void TreeSetupColumnsCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    NodeColumnCollection columnCollection = this._navigatorTreeView.ReflectTreeColumsChanges();
    if (AppearanceTuningForm.Execute(this._navigatorTreeView.GetChildHandler(this._navigatorTreeView.FocusedNode), ContentType.Folders, this._navigatorTreeView.SupportedColumns, columnCollection) != DialogResult.OK)
      return;
    this._navigatorTreeView.SetColumns(columnCollection);
    INavigatorColumnsService service = ServicesManager.GetService(typeof (INavigatorColumnsService)) as INavigatorColumnsService;
    NavigatorColumns navigatorColumns = service.CreateNavigatorColumns(this._navigatorTreeView.RootNodeID != null ? this._navigatorTreeView.RootNodeID.CategoryID : 0, this._navigatorTreeView.RootNodeID != null ? this._navigatorTreeView.RootNodeID.TypeID : 0, "TreeView");
    navigatorColumns.Columns = columnCollection.Clone() as NodeColumnCollection;
    service.CreateNavigatorColumns(navigatorColumns);
    service.SaveToUserConfig();
  }

  private void TreeRecursiveExpandNodeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    bool flag = this._navigatorTreeView.FocusedNode.HasChildren || this._navigatorTreeView.FocusedNode.Children.Count > 0;
    if (this._navigatorTreeView.FocusedNode == null || !flag || !(ServicesManager.GetService(typeof (INavigatorTreeViewClientService)) is INavigatorTreeViewClientService service))
      return;
    service.ExpandAll(this._navigatorTreeView.FocusedNode);
  }

  private void TreeExpandNodeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (this._navigatorTreeView.FocusedNode == null)
      return;
    this._navigatorTreeView.FocusedNode.Expanded = true;
  }

  private void TreeCollapseNodeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (this._navigatorTreeView.FocusedNode == null)
      return;
    this._navigatorTreeView.FocusedNode.Expanded = false;
  }

  private void SeekInTreeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (this._navigatorTreeView.FocusedNode == null)
      return;
    TreeViewSearchForm.ShowFor(this._navigatorTreeView);
  }

  private void TreeRefreshNodeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this._navigatorTreeView.TreeRefreshNodeCommand(items, viewServices, additionalInfo);
  }
}
