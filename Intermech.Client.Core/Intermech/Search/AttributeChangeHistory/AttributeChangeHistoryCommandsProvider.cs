
// Type: Intermech.Search.AttributeChangeHistory.AttributeChangeHistoryCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.AttributeChangeHistory;

public sealed class AttributeChangeHistoryCommandsProvider : ICommandsProvider
{
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    CommandsInfo groupCommands = new CommandsInfo();
    IDBTypedObjectID[] typedObjectIds = (IDBTypedObjectID[]) null;
    if (this.CheckParamsForShowAttributeChangeHistoryForm(items, out typedObjectIds))
      groupCommands.Add("ShowAttributeChangeHistoryForm", new CommandInfo(-1, new ClickEventHandler(this.ShowAttributeChangeHistoryForm)));
    return groupCommands;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  private bool CheckParamsForShowAttributeChangeHistoryForm(
    ISelectedItems selectedItems,
    out IDBTypedObjectID[] typedObjectIds)
  {
    return SelectedItemsHelper.TryGetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIds(selectedItems, out typedObjectIds);
  }

  private void ShowAttributeChangeHistoryForm(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    IDBTypedObjectID[] typedObjectIds = (IDBTypedObjectID[]) null;
    if (!this.CheckParamsForShowAttributeChangeHistoryForm(items, out typedObjectIds))
      throw new ArgumentException();
    ServiceLocator.Get<IAttributeChangeHistoryClientService>().ShowAttributeChangeHistoryForm(((IEnumerable<IDBTypedObjectID>) typedObjectIds).Select<IDBTypedObjectID, long>((Func<IDBTypedObjectID, long>) (o => o.ObjectID)).ToArray<long>());
  }
}
