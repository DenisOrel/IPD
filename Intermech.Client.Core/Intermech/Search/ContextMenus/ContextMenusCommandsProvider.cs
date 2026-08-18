
// Type: Intermech.Search.ContextMenus.ContextMenusCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenusCommandsProvider : ICommandsProvider
{
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    NodeID objectNodeID = (NodeID) null;
    if (this.CheckParamsForEdit(items, out objectNodeID))
      mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(this.Edit)));
    return mergedCommands;
  }

  private bool CheckParamsForEdit(ISelectedItems selectedItems, out NodeID objectNodeID)
  {
    if (SelectedItemsHelper.TryGetSingleObjectNodeIDWithObjectVersionIDObjectTypeID(selectedItems, out objectNodeID) && objectNodeID.ObjectTypeID == ContextMenuConstants.ContextMenuObjectTypeID)
      return true;
    objectNodeID = (NodeID) null;
    return false;
  }

  private void Edit(ISelectedItems items, IServiceProvider viewServices, object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    NodeID objectNodeID = (NodeID) null;
    if (!this.CheckParamsForEdit(items, out objectNodeID))
      throw new ArgumentException();
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, objectNodeID.ObjectID, false, typeof (ContextMenuEditorView).Name);
  }
}
