
// Type: Intermech.Search.EditingContexts.EditingContextsCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.EditingContexts;

public sealed class EditingContextsCommandsProvider : ICommandsProvider
{
  private LazyService<ICurrentUserAndRole> _currentUserAndRole = new LazyService<ICurrentUserAndRole>();
  private LazyService<IEditingContextClientService> _editingContextClientService = new LazyService<IEditingContextClientService>();

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
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (this.CheckParamsForActivateEditingContext(items, viewServices, out typedObjectID))
      groupCommands.Add("EditingContextActivate", new CommandInfo(0, new ClickEventHandler(this.ActivateEditingContext)));
    IDBTypedObjectID[] typedObjectIds = (IDBTypedObjectID[]) null;
    if (this.CheckParamsForAddToEditingContext(items, viewServices, out typedObjectIds))
      groupCommands.Add("EditingContextAdd", new CommandInfo(0, new ClickEventHandler(this.AddToEditingContext)));
    if (this.CheckParamsForAddWithCompositionToEditingContext(items, viewServices, out typedObjectIds))
      groupCommands.Add("EditingContextAddComposition", new CommandInfo(0, new ClickEventHandler(this.AddWithCompositionToEditingContext)));
    if (this.CheckParamsForReplaceVersionInEditingContext(items, viewServices, out typedObjectID))
      groupCommands.Add("EditingContextReplaceVersion", new CommandInfo(0, new ClickEventHandler(this.ReplaceVersionInEditingContext)));
    return groupCommands;
  }

  private void ActivateEditingContext(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (!this.CheckParamsForActivateEditingContext(items, viewServices, out typedObjectID))
      throw new ArgumentException();
    this._editingContextClientService.Value.ActivateEditingContext(typedObjectID.ObjectID);
  }

  private void AddToEditingContext(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    IDBTypedObjectID[] typedObjectIds = (IDBTypedObjectID[]) null;
    if (!this.CheckParamsForAddToEditingContext(items, viewServices, out typedObjectIds))
      throw new ArgumentException();
    this._editingContextClientService.Value.AddObjectsToCurrentEditingContext(((IEnumerable<IDBTypedObjectID>) typedObjectIds).Select<IDBTypedObjectID, long>((Func<IDBTypedObjectID, long>) (o => o.ObjectID)).ToArray<long>());
  }

  private void AddWithCompositionToEditingContext(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    IDBTypedObjectID[] typedObjectIds = (IDBTypedObjectID[]) null;
    if (!this.CheckParamsForAddWithCompositionToEditingContext(items, viewServices, out typedObjectIds))
      throw new ArgumentException();
    this._editingContextClientService.Value.AddObjectsWithCompositionToCurrentEditingContext(((IEnumerable<IDBTypedObjectID>) typedObjectIds).Select<IDBTypedObjectID, long>((Func<IDBTypedObjectID, long>) (o => o.ObjectID)).ToArray<long>());
  }

  private void ReplaceVersionInEditingContext(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    IDBTypedObjectID typedObjectID = (IDBTypedObjectID) null;
    if (!this.CheckParamsForReplaceVersionInEditingContext(items, viewServices, out typedObjectID))
      throw new ArgumentException();
    this._editingContextClientService.Value.ReplaceVersionInCurrentEditingContext(typedObjectID.ObjectID);
  }

  private bool CheckParamsForActivateEditingContext(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out IDBTypedObjectID typedObjectID)
  {
    IViewState service = serviceProvider.GetService(typeof (IViewState)) as IViewState;
    SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(selectedItems, out typedObjectID);
    return typedObjectID != null && EditingContextsHelper.IsEditingContextObjectTypeID(typedObjectID.ObjectType) && service != null && !service.ViewState.HasFlag((Enum) ViewStateFlags.InDialog) && !service.ViewState.HasFlag((Enum) ViewStateFlags.InParametersCard);
  }

  private bool CheckParamsForAddToEditingContext(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out IDBTypedObjectID[] typedObjectIds)
  {
    IViewState service = serviceProvider.GetService(typeof (IViewState)) as IViewState;
    SelectedItemsHelper.TryGetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIds(selectedItems, out typedObjectIds);
    if (ObjectHelper.IsUnknownObjectVersionID(this._currentUserAndRole.Value.CachedEditingContextID) || selectedItems.Count <= 0 || typedObjectIds == null || EditingContextsHelper.IsAnyEditingContextObjectTypeID(((IEnumerable<IDBTypedObjectID>) typedObjectIds).Select<IDBTypedObjectID, int>((Func<IDBTypedObjectID, int>) (o => o.ObjectType))) || service == null || service.ViewState.HasFlag((Enum) ViewStateFlags.InDialog) || service.ViewState.HasFlag((Enum) ViewStateFlags.InParametersCard))
      return false;
    return service.ViewState.HasFlag((Enum) ViewStateFlags.NodeInTree) || service.ViewState.HasFlag((Enum) ViewStateFlags.NodeInViews);
  }

  private bool CheckParamsForAddWithCompositionToEditingContext(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out IDBTypedObjectID[] typedObjectIds)
  {
    IViewState service = serviceProvider.GetService(typeof (IViewState)) as IViewState;
    SelectedItemsHelper.TryGetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIds(selectedItems, out typedObjectIds);
    return !ObjectHelper.IsUnknownObjectVersionID(this._currentUserAndRole.Value.CachedEditingContextID) && selectedItems.Count > 0 && typedObjectIds != null && !EditingContextsHelper.IsAnyEditingContextObjectTypeID(((IEnumerable<IDBTypedObjectID>) typedObjectIds).Select<IDBTypedObjectID, int>((Func<IDBTypedObjectID, int>) (o => o.ObjectType))) && service != null && !service.ViewState.HasFlag((Enum) ViewStateFlags.InDialog) && !service.ViewState.HasFlag((Enum) ViewStateFlags.InParametersCard) && !service.ViewState.HasFlag((Enum) ViewStateFlags.NodeInViews) && service.ViewState.HasFlag((Enum) ViewStateFlags.NodeInTree);
  }

  private bool CheckParamsForReplaceVersionInEditingContext(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out IDBTypedObjectID typedObjectID)
  {
    IViewState service = serviceProvider.GetService(typeof (IViewState)) as IViewState;
    SelectedItemsHelper.TryGetSingleTypedObjectIDWithObjectVersionIDAndObjectTypeID(selectedItems, out typedObjectID);
    if (ObjectHelper.IsUnknownObjectVersionID(this._currentUserAndRole.Value.CachedEditingContextID) || typedObjectID == null || ObjectHelper.IsUnknownObjectID(typedObjectID.ID) || !ObjectTypeHelper.IsVersionedObjectTypeID(typedObjectID.ObjectType) || EditingContextsHelper.IsEditingContextObjectTypeID(typedObjectID.ObjectType) || service == null || service.ViewState.HasFlag((Enum) ViewStateFlags.InDialog) || service.ViewState.HasFlag((Enum) ViewStateFlags.InParametersCard))
      return false;
    return service.ViewState.HasFlag((Enum) ViewStateFlags.NodeInTree) || service.ViewState.HasFlag((Enum) ViewStateFlags.NodeInViews);
  }
}
