
// Type: Intermech.Search.ObjectGroups.ObjectGroupCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupCommandsProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    CommandsInfo groupCommands = new CommandsInfo();
    CommandsTable commandsTable = (CommandsTable) null;
    if (this.CheckSelectedItemsForPaste(items, viewServices, out commandsTable))
      groupCommands.Add("Paste", new CommandInfo(-1, new ClickEventHandler(this.Paste)));
    return groupCommands;
  }

  private bool CheckSelectedItemsForPaste(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out CommandsTable commandsTable)
  {
    commandsTable = (CommandsTable) null;
    if (!(selectedItems.GetItemID(0) is ObjectGroupNodeID) || !(selectedItems.GetItemData(0, typeof (NavigatorTreeNode)) is NavigatorTreeNode itemData) || itemData.Parent == null)
      return false;
    commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable((ISelectedItems) new NavigatorTreeViewSelectedItems(itemData.Tree, new NavigatorTreeNode[1]
    {
      itemData.Parent
    }), serviceProvider);
    return ((IEnumerable<string>) commandsTable.CommandNames).Contains<string>("Paste");
  }

  private void Paste(ISelectedItems items, IServiceProvider viewServices, object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    CommandsTable commandsTable = (CommandsTable) null;
    if (!this.CheckSelectedItemsForPaste(items, viewServices, out commandsTable))
      throw new ArgumentException();
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(nameof (Paste), commandsTable, viewServices);
  }
}
