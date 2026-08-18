
// Type: Intermech.Search.Concretization.ConcretizationCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search.Concretization;

public sealed class ConcretizationCommandsProvider : ICommandsProvider
{
  private IConcretizationClientService _concretizationClientService;

  public ConcretizationCommandsProvider(
    IConcretizationClientService concretizationClientService)
  {
    this._concretizationClientService = concretizationClientService != null ? concretizationClientService : throw new ArgumentNullException(nameof (concretizationClientService));
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    CommandsInfo groupCommands = new CommandsInfo();
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    NodeID nodeID;
    if (this.CheckParamsForAbstractCurrentVersion(items, out nodeID, out navigatorTreeNode))
      groupCommands.Add("Abstraction.CurrentVersion", new CommandInfo(-1, new ClickEventHandler(this.AbstractCurrentVersion)));
    if (this.CheckParamsForAbstractCurrentVersionInComposition(items, viewServices, out nodeID, out navigatorTreeNode))
      groupCommands.Add("Abstraction.CurrentVersionInComposition", new CommandInfo(-1, new ClickEventHandler(this.AbstractCurrentVersionInComposition)));
    if (this.CheckParamsForAbstractEntireComposition(items, viewServices, out navigatorTreeNode))
      groupCommands.Add("Abstraction.EntireComposition", new CommandInfo(-1, new ClickEventHandler(this.AbstractEntireComposition)));
    if (this.CheckParamsForConcretizeCurrentVersion(items, out nodeID, out navigatorTreeNode))
      groupCommands.Add("Concretization.CurrentVersion", new CommandInfo(-1, new ClickEventHandler(this.ConcretizeCurrentVersion)));
    if (this.CheckParamsForConcretizeCurrentVersionInComposition(items, viewServices, out nodeID, out navigatorTreeNode))
      groupCommands.Add("Concretization.CurrentVersionInComposition", new CommandInfo(-1, new ClickEventHandler(this.ConcretizeCurrentVersionInComposition)));
    if (this.CheckParamsForConcretizeSelectedVersion(items, out nodeID, out navigatorTreeNode))
      groupCommands.Add("Concretization.SelectVersion", new CommandInfo(-1, new ClickEventHandler(this.ConcretizeSelectedVersion)));
    if (this.CheckParamsForConcretizeSelectedVersionInComposition(items, viewServices, out nodeID, out navigatorTreeNode))
      groupCommands.Add("Concretization.SelectVersionInComposition", new CommandInfo(-1, new ClickEventHandler(this.ConcretizeSelectedVersionInComposition)));
    if (this.CheckParamsForConcretizeEntireComposition(items, viewServices, out navigatorTreeNode))
      groupCommands.Add("Concretization.EntireComposition", new CommandInfo(-1, new ClickEventHandler(this.ConcretizeEntireComposition)));
    if (this.CheckParamsForCheckVersion(items, out nodeID, out navigatorTreeNode))
      groupCommands.Add("Core.CheckVersion", new CommandInfo(-1, new ClickEventHandler(this.CheckVersion)));
    return groupCommands;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  private bool CheckParamsForAbstractCurrentVersion(
    ISelectedItems selectedItems,
    out NodeID nodeID,
    out NavigatorTreeNode navigatorTreeNode)
  {
    if (SelectedItemsHelper.TryGetSingleObjectNodeIDWithObjectVersionIDObjectTypeIDRelationIDAndRelationTypeID(selectedItems, out nodeID) && this._concretizationClientService.CanAbstract(nodeID) && SelectedItemsHelper.TryGetSingleNavigatorTreeNode(selectedItems, out navigatorTreeNode))
      return true;
    nodeID = (NodeID) null;
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  private void AbstractCurrentVersion(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    NodeID nodeID = (NodeID) null;
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForAbstractCurrentVersion(items, out nodeID, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.AbstractCurrentVersion(nodeID.PrjLinkID);
  }

  private bool CheckParamsForAbstractCurrentVersionInComposition(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out NodeID nodeID,
    out NavigatorTreeNode navigatorTreeNode)
  {
    if (this.CheckParamsForAbstractCurrentVersion(selectedItems, out nodeID, out navigatorTreeNode) && navigatorTreeNode.Tree != null && navigatorTreeNode.Tree.RootNode != null)
      return true;
    nodeID = (NodeID) null;
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  private void AbstractCurrentVersionInComposition(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    NodeID nodeID = (NodeID) null;
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForAbstractCurrentVersionInComposition(items, viewServices, out nodeID, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.AbstractCurrentVersionInComposition(nodeID.PrjLinkID, navigatorTreeNode.Tree);
  }

  private bool CheckParamsForAbstractEntireComposition(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out NavigatorTreeNode navigatorTreeNode)
  {
    return this.CheckParamsForConcretizeEntireComposition(selectedItems, serviceProvider, out navigatorTreeNode);
  }

  private void AbstractEntireComposition(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForAbstractEntireComposition(items, viewServices, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.AbstractEntireComposition(navigatorTreeNode);
  }

  private bool CheckParamsForConcretizeCurrentVersion(
    ISelectedItems selectedItems,
    out NodeID nodeID,
    out NavigatorTreeNode navigatorTreeNode)
  {
    if (SelectedItemsHelper.TryGetSingleObjectNodeIDWithObjectVersionIDObjectTypeIDRelationIDAndRelationTypeID(selectedItems, out nodeID) && this._concretizationClientService.CanConcretize(SelectedItemsHelper.GetProjectTypeID(selectedItems), nodeID) && SelectedItemsHelper.TryGetSingleNavigatorTreeNode(selectedItems, out navigatorTreeNode))
      return true;
    nodeID = (NodeID) null;
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  private void ConcretizeCurrentVersion(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    NodeID nodeID = (NodeID) null;
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForConcretizeCurrentVersion(items, out nodeID, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.ConcretizeCurrentVersion(nodeID.PrjLinkID, nodeID.ObjectID);
  }

  private bool CheckParamsForConcretizeCurrentVersionInComposition(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out NodeID nodeID,
    out NavigatorTreeNode navigatorTreeNode)
  {
    if (this.CheckParamsForConcretizeCurrentVersion(selectedItems, out nodeID, out navigatorTreeNode) && navigatorTreeNode.Tree != null && navigatorTreeNode.Tree.RootNode != null)
      return true;
    nodeID = (NodeID) null;
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  private void ConcretizeCurrentVersionInComposition(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    NodeID nodeID = (NodeID) null;
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForConcretizeCurrentVersionInComposition(items, viewServices, out nodeID, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.ConcretizeCurrentVersionInComposition(nodeID.PrjLinkID, nodeID.ObjectID, navigatorTreeNode.Tree);
  }

  private bool CheckParamsForConcretizeSelectedVersion(
    ISelectedItems selectedItems,
    out NodeID nodeID,
    out NavigatorTreeNode navigatorTreeNode)
  {
    return this.CheckParamsForConcretizeCurrentVersion(selectedItems, out nodeID, out navigatorTreeNode);
  }

  private void ConcretizeSelectedVersion(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    NodeID nodeID = (NodeID) null;
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForConcretizeSelectedVersion(items, out nodeID, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.ConcretizeSelectedVersion(nodeID.PrjLinkID, nodeID.ID);
  }

  private bool CheckParamsForConcretizeSelectedVersionInComposition(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out NodeID nodeID,
    out NavigatorTreeNode navigatorTreeNode)
  {
    return this.CheckParamsForConcretizeCurrentVersionInComposition(selectedItems, serviceProvider, out nodeID, out navigatorTreeNode);
  }

  private void ConcretizeSelectedVersionInComposition(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    NodeID nodeID = (NodeID) null;
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForConcretizeSelectedVersionInComposition(items, viewServices, out nodeID, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.ConcretizeSelectedVersionInComposition(nodeID.PrjLinkID, nodeID.ID, navigatorTreeNode.Tree);
  }

  private bool CheckParamsForConcretizeEntireComposition(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    out NavigatorTreeNode navigatorTreeNode)
  {
    NodeID nodeID = (NodeID) null;
    if (SelectedItemsHelper.TryGetSingleObjectNodeIDWithObjectVersionIDObjectTypeID(selectedItems, out nodeID) && SelectedItemsHelper.TryGetSingleNavigatorTreeNode(selectedItems, out navigatorTreeNode) && (!navigatorTreeNode.Full || navigatorTreeNode.Full && navigatorTreeNode.Children.Count > 0))
      return true;
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  private void ConcretizeEntireComposition(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForConcretizeEntireComposition(items, viewServices, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.ConcretizeEntireComposition(navigatorTreeNode);
  }

  private bool CheckParamsForCheckVersion(
    ISelectedItems selectedItems,
    out NodeID nodeID,
    out NavigatorTreeNode navigatorTreeNode)
  {
    if (SelectedItemsHelper.TryGetSingleObjectNodeIDWithObjectVersionIDObjectTypeIDRelationIDAndRelationTypeID(selectedItems, out nodeID) && (nodeID.State == ObjectFiltrationState.fsCompositeVersion || nodeID.State == ObjectFiltrationState.fsSoftConcretised) && SelectedItemsHelper.TryGetSingleNavigatorTreeNode(selectedItems, out navigatorTreeNode) && navigatorTreeNode.Tree != null && navigatorTreeNode.Tree.RootNode != null)
      return true;
    nodeID = (NodeID) null;
    navigatorTreeNode = (NavigatorTreeNode) null;
    return false;
  }

  private void CheckVersion(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    NodeID nodeID = (NodeID) null;
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (!this.CheckParamsForCheckVersion(items, out nodeID, out navigatorTreeNode))
      throw new ArgumentException();
    this._concretizationClientService.CheckVersion(nodeID, navigatorTreeNode.Tree);
  }
}
