
// Type: Intermech.Search.GroupAttributesChanging.GroupAttributesChangingCommandsProvider
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


namespace Intermech.Search.GroupAttributesChanging;

public sealed class GroupAttributesChangingCommandsProvider : ICommandsProvider
{
  private IGroupAttributesChangingClientService _groupAttributesChangingClientService;

  public GroupAttributesChangingCommandsProvider(
    IGroupAttributesChangingClientService groupAttributesChangingClientService)
  {
    this._groupAttributesChangingClientService = groupAttributesChangingClientService != null ? groupAttributesChangingClientService : throw new ArgumentNullException(nameof (groupAttributesChangingClientService));
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    CommandsInfo groupCommands = new CommandsInfo();
    if (this.CheckSelectedItemsForGroupAttributesChanging(items, out NodeID[] _))
      groupCommands.Add("GroupAttributesChanging", new CommandInfo(0, new ClickEventHandler(this.GroupAttributesChanging)));
    return groupCommands;
  }

  private void GroupAttributesChanging(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    NodeID[] objectNodeIds;
    if (!this.CheckSelectedItemsForGroupAttributesChanging(items, out objectNodeIds))
      throw new ArgumentException();
    this._groupAttributesChangingClientService.ChangeAttributes(((IEnumerable<NodeID>) objectNodeIds).Select<NodeID, long>((Func<NodeID, long>) (o => o.ObjectID)).Distinct<long>().ToArray<long>());
  }

  private bool CheckSelectedItemsForGroupAttributesChanging(
    ISelectedItems selectedItems,
    out NodeID[] objectNodeIds)
  {
    if (SelectedItemsHelper.TryGetObjectNodeIdsWithObjectVersionIDAndObjectTypeID(selectedItems, out objectNodeIds))
      return true;
    objectNodeIds = (NodeID[]) null;
    return false;
  }
}
