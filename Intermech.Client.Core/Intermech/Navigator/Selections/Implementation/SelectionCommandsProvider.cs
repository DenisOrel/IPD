
// Type: Intermech.Navigator.Selections.Implementation.SelectionCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Реализует провайдер команд контекстного меню для выборок и классификаторов.
/// </summary>
internal class SelectionCommandsProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.InDialog) != ViewStateFlags.None)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Suppress("ViewDocument", 0);
    mergedCommands.Suppress("PrintDocument", 0);
    mergedCommands.Suppress("SaveChanges", 0);
    mergedCommands.Suppress("CancelChanges", 0);
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
    {
      if (SelectionCommands.IsClassifier((items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectType))
        mergedCommands.Suppress("Cut", 0);
      mergedCommands.Suppress("OpenDocument", 0);
      mergedCommands.Suppress("OpenWith", 0);
      mergedCommands.Suppress("ViewWithOptions", 0);
      mergedCommands.Suppress("EditDocument", 0);
      mergedCommands.Add("Delete", new CommandInfo(0, new ClickEventHandler(SelectionCommands.DeleteCommand)));
    }
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if (items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (SelectionCommands.IsSelection(itemData.ObjectType) && ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).IsTemporaryValuesPresent(itemData.ObjectID))
      groupCommands.Add("RestoreSelectionValues", new CommandInfo(0, new ClickEventHandler(SelectionCommands.RestoreSelectionValues)));
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
    {
      if (((IClipboard) ServicesManager.GetService(typeof (IClipboard))).GetDataObject() is IDBObjectTypedIDCollection dataObject && dataObject.Count > 0)
        groupCommands.Add("Paste", new CommandInfo(0, new ClickEventHandler(SelectionCommands.PasteCommand)));
      if ((viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None)
        groupCommands.Suppress("OpenInNewWindow", 0);
    }
    else
      groupCommands.Add("CreateInclude2", new CommandInfo(0, new ClickEventHandler(ObjectCommands.CreateIncludeCommand)));
    return groupCommands;
  }
}
