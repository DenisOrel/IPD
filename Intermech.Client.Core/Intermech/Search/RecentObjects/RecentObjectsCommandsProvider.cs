
// Type: Intermech.Search.RecentObjects.RecentObjectsCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.RecentObjects;

public sealed class RecentObjectsCommandsProvider : ICommandsProvider
{
  private IRecentObjectsClientService _recentObjectsClientService;

  public RecentObjectsCommandsProvider(
    IRecentObjectsClientService recentObjectsClientService)
  {
    this._recentObjectsClientService = recentObjectsClientService != null ? recentObjectsClientService : throw new ArgumentNullException(nameof (recentObjectsClientService));
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    CommandsInfo groupCommands = new CommandsInfo();
    if (this.CheckParamsForChangeRecentObjectsAccessSettings(items))
      groupCommands.Add("ChangeRecentObjectsAccessSettings", new CommandInfo(-1, new ClickEventHandler(this.ChangeRecentObjectsAccessSettings)));
    if (this.CheckParamsForOpenOtherUserRecentObjects(items))
      groupCommands.Add("OpenOtherUserRecentObjects", new CommandInfo(-1, new ClickEventHandler(this.OpenOtherUserRecentObjects)));
    NodeID[] nodeIds = (NodeID[]) null;
    if (this.CheckParamsForRemoveFromRecentObjects(items, out nodeIds))
      groupCommands.Add("RemoveRecentObjects", new CommandInfo(-1, new ClickEventHandler(this.RemoveFromRecentObjects)));
    if (this.CheckParamsForClearRecentObjects(items))
      groupCommands.Add("ClearRecentObjects", new CommandInfo(-1, new ClickEventHandler(this.ClearRecentObjects)));
    return groupCommands;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  private bool CheckParamsForChangeRecentObjectsAccessSettings(ISelectedItems selectedItems)
  {
    return this.IsSingleCurrentUserRecentObjectsNode(selectedItems);
  }

  private bool IsSingleCurrentUserRecentObjectsNode(ISelectedItems selectedItems)
  {
    if (selectedItems.Count != 1)
      return false;
    INodeID itemId = selectedItems.GetItemID(0);
    return itemId != null && this.IsCurrentUserResentObjectsNodeID(itemId);
  }

  private bool IsCurrentUserResentObjectsNodeID(INodeID nodeID)
  {
    return nodeID.CategoryID == Intermech.Navigator.Consts.CategoryRecentObjectsNode && nodeID.TypeID == 0;
  }

  private void ChangeRecentObjectsAccessSettings(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!this.CheckParamsForChangeRecentObjectsAccessSettings(items))
      throw new ArgumentException();
    this._recentObjectsClientService.ChangeRecentObjectsAccessSettings();
  }

  private bool CheckParamsForOpenOtherUserRecentObjects(ISelectedItems selectedItems)
  {
    return this.IsSingleCurrentUserRecentObjectsNode(selectedItems);
  }

  private void OpenOtherUserRecentObjects(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!this.CheckParamsForOpenOtherUserRecentObjects(items))
      throw new ArgumentException();
    this._recentObjectsClientService.OpenOtherUserRecentObjects();
  }

  private bool CheckParamsForRemoveFromRecentObjects(
    ISelectedItems selectedItems,
    out NodeID[] nodeIds)
  {
    NodeIDPath parentPath = selectedItems.GetParentPath(0);
    if (parentPath != null && parentPath.Length > 0 && this.IsCurrentUserResentObjectsNodeID(parentPath.LastID) && SelectedItemsHelper.TryGetObjectNodeIdsWithObjectVersionIDAndObjectTypeID(selectedItems, out nodeIds))
      return true;
    nodeIds = (NodeID[]) null;
    return false;
  }

  private void RemoveFromRecentObjects(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    NodeID[] nodeIds;
    if (!this.CheckParamsForRemoveFromRecentObjects(items, out nodeIds))
      throw new ArgumentException();
    this._recentObjectsClientService.RemoveFromCurrentUserRecentObjects(((IEnumerable<NodeID>) nodeIds).Select<NodeID, long>((Func<NodeID, long>) (o => o.ObjectID)).Distinct<long>().ToArray<long>());
  }

  private bool CheckParamsForClearRecentObjects(ISelectedItems selectedItems)
  {
    return this.IsSingleCurrentUserRecentObjectsNode(selectedItems);
  }

  private void ClearRecentObjects(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!this.CheckParamsForClearRecentObjects(items))
      throw new ArgumentException();
    this._recentObjectsClientService.ClearCurrentUserRecentObjects();
  }
}
