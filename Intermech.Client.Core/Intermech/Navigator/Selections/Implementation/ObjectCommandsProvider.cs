
// Type: Intermech.Navigator.Selections.Implementation.ObjectCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Класс для реализации команды исключения объектов из ручной выборки (классификатора)
/// </summary>
internal class ObjectCommandsProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service1 ? (long) service1.ViewState : 0L) & 2L) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    NodeIDPath parentPath = items.GetParentPath(0);
    if (parentPath.Length != 0 && !items.IsCollage && SelectionCommands.IsSelectionOrClassifier(parentPath.LastID.TypeID))
    {
      IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      if (!SelectionCommands.IsSelectionOrClassifier(itemData.ObjectType))
      {
        bool flag1 = true;
        bool flag2 = true;
        if (SelectionCommands.IsSelection(parentPath.LastID.TypeID))
          flag1 = parentPath.LastID is SelectionNodeID lastId && lastId.HandSelection;
        if (flag1 && ServicesManager.GetService(typeof (ISelectionsService)) is ISelectionsService service2)
          flag2 = !service2.GetShowInternalFolders();
        if (flag1 && (SelectionCommands.IsSelection(parentPath.LastID.TypeID) || flag2 && SelectionCommands.IsClassifier(parentPath.LastID.TypeID)))
          mergedCommands.Add("ExcludeFromSelection", new CommandInfo(16 /*0x10*/, new ClickEventHandler(SelectionCommands.ExcludeCommand)));
      }
      if (SelectionCommands.IsClassifierFolder(itemData.ObjectType))
        mergedCommands.Suppress("Exclude", 18);
    }
    mergedCommands.Add("IncludeToSelection", new CommandInfo(16 /*0x10*/, new ClickEventHandler(SelectionCommands.IncludeCommand)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }
}
