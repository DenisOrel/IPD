// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.SelectDocumentsPageVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.UI;
using Intermech.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TreeListView;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class SelectDocumentsPageVM : WizardPageVM
{
  private readonly ObservableCollection<SelectDocumentsTreeNodeVM> nodes;
  private readonly WizardPageOperationErrorsVM pageErrors;
  private readonly CopyingSession session;
  private ICollection<DBObjectGraphVertex> originalVerticesToCopy;
  private readonly PluggableCommand selectAllNodes;
  private readonly PluggableCommand deselectAllNodes;
  private readonly ContextMenuViewModel<ContextMenuUICommand<SelectDocumentsTreeNodeVM>> contextMenu;
  private DelegateCommand contextMenuOpenedCommand;

  public SelectDocumentsPageVM()
    : base("Выбор документов для копирования")
  {
    this.nodes = new ObservableCollection<SelectDocumentsTreeNodeVM>();
    this.pageErrors = new WizardPageOperationErrorsVM();
    this.pageErrors.PropertyChanged += new PropertyChangedEventHandler(this.OnPageErrorsChanged);
    this.selectAllNodes = new PluggableCommand((Action) (() => this.SelectNodes(true)));
    this.deselectAllNodes = new PluggableCommand((Action) (() => this.SelectNodes(false)));
    this.contextMenu = new ContextMenuViewModel<ContextMenuUICommand<SelectDocumentsTreeNodeVM>>();
    this.contextMenu.Items.Add(new ContextMenuUICommand<SelectDocumentsTreeNodeVM>("Выбрать вложенные", (Action<SelectDocumentsTreeNodeVM>) (node => this.SelectOneLevel(node, true)), new Predicate<SelectDocumentsTreeNodeVM>(this.CanExecuteMenuCommand)));
    this.contextMenu.Items.Add(new ContextMenuUICommand<SelectDocumentsTreeNodeVM>("Выбрать вложенные рекурсивно", (Action<SelectDocumentsTreeNodeVM>) (node => this.SelectAllLevels(node, true)), new Predicate<SelectDocumentsTreeNodeVM>(this.CanExecuteMenuCommand)));
    this.contextMenu.Items.Add(new ContextMenuUICommand<SelectDocumentsTreeNodeVM>("Отменить вложенные", (Action<SelectDocumentsTreeNodeVM>) (node => this.SelectOneLevel(node, false)), new Predicate<SelectDocumentsTreeNodeVM>(this.CanExecuteMenuCommand)));
    this.contextMenuOpenedCommand = new DelegateCommand(new Action<object>(this.OnContextMenuOpened));
  }

  public SelectDocumentsPageVM(CopyingSession session)
    : this()
  {
    this.session = session != null ? session : throw new ArgumentNullException(nameof (session));
    this.pageErrors.SetCopyingSession(session);
    this.nodes.Add(new SelectDocumentsTreeBuilder().CreateTree(this.session, this.session.Graph.RootVertext, true));
  }

  public ObservableCollection<SelectDocumentsTreeNodeVM> Nodes
  {
    [DebuggerStepThrough] get => this.nodes;
  }

  public WizardPageOperationErrorsVM PageErrors
  {
    [DebuggerStepThrough] get => this.pageErrors;
  }

  public PluggableCommand SelectAllNodes
  {
    [DebuggerStepThrough] get => this.selectAllNodes;
  }

  public PluggableCommand DeselectAllNodes
  {
    [DebuggerStepThrough] get => this.deselectAllNodes;
  }

  public ContextMenuViewModel<ContextMenuUICommand<SelectDocumentsTreeNodeVM>> ContextMenu
  {
    [DebuggerStepThrough] get => this.contextMenu;
  }

  public DelegateCommand ContextMenuOpenedCommand
  {
    [DebuggerStepThrough] get => this.contextMenuOpenedCommand;
  }

  private bool ValidateIsCompleted()
  {
    return this.pageErrors.IsEmpty && this.nodes.Count != 0 && this.nodes[0].CopyingSelector.IsSelected;
  }

  private void OnPageErrorsChanged(object sender, PropertyChangedEventArgs e)
  {
    this.IsCompleted = this.ValidateIsCompleted();
  }

  protected override void DoActivate(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage)
  {
    base.DoActivate(navigationType, previousPage);
    if (this.session == null)
      return;
    this.originalVerticesToCopy = this.GetCurrentVerticesToCopy();
    this.IsCompleted = this.ValidateIsCompleted();
  }

  protected override void DoDeactivate(
    WizardPageNavigationType navigationType,
    WizardPageVM nextPage)
  {
    base.DoDeactivate(navigationType, nextPage);
    if (this.session == null || this.originalVerticesToCopy == null)
      return;
    this.session.DeferredEventDispatcher.RaiseAll();
    this.UpdateProcessingHistory();
    this.originalVerticesToCopy = (ICollection<DBObjectGraphVertex>) null;
  }

  private ICollection<DBObjectGraphVertex> GetCurrentVerticesToCopy()
  {
    return this.session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.CopyingSelector.IsSelected));
  }

  private bool HasUserEdits()
  {
    return !this.originalVerticesToCopy.SequenceEqual<DBObjectGraphVertex>((IEnumerable<DBObjectGraphVertex>) this.GetCurrentVerticesToCopy());
  }

  private void UpdateProcessingHistory()
  {
    if (this.session.ProcessingHistory.Contains("SelectDocuments") && !this.HasUserEdits())
      return;
    this.session.ProcessingHistory.Update(new CopyingSessionProcessingStep("SelectDocuments"));
  }

  private void SelectNodes(bool isSelect)
  {
    foreach (SelectDocumentsTreeNodeVM node in (Collection<SelectDocumentsTreeNodeVM>) this.Nodes)
      this.SelectAllLevels(node, isSelect);
  }

  private void SelectOneLevel(SelectDocumentsTreeNodeVM selectedNode, bool isSelect)
  {
    if (!selectedNode.IsVirtual && selectedNode.CopyingSelector.IsUserEditable && selectedNode.CopyingSelector.IsSelected != isSelect)
      selectedNode.CopyingSelector.IsSelected = isSelect;
    if (selectedNode.Nodes.Count <= 0)
      return;
    this.SelectChild(selectedNode.Nodes, isSelect, false);
  }

  private void SelectAllLevels(SelectDocumentsTreeNodeVM selectedNode, bool isSelect)
  {
    if (!selectedNode.IsVirtual && selectedNode.CopyingSelector.IsUserEditable && selectedNode.CopyingSelector.IsSelected != isSelect)
      selectedNode.CopyingSelector.IsSelected = isSelect;
    if (selectedNode.Nodes.Count <= 0)
      return;
    this.SelectChild(selectedNode.Nodes, isSelect, true);
  }

  private void SelectChild(
    ObservableCollection<SelectDocumentsTreeNodeVM> childNodes,
    bool isSelect,
    bool isRecursive)
  {
    foreach (SelectDocumentsTreeNodeVM childNode in (Collection<SelectDocumentsTreeNodeVM>) childNodes)
    {
      if (!childNode.IsVirtual && childNode.CopyingSelector.IsUserEditable && childNode.CopyingSelector.IsSelected != isSelect)
        childNode.CopyingSelector.IsSelected = isSelect;
      if (isRecursive && childNode.Nodes.Count > 0)
        this.SelectChild(childNode.Nodes, isSelect, true);
    }
  }

  private bool CanExecuteMenuCommand(SelectDocumentsTreeNodeVM obj) => obj != null;

  private void OnContextMenuOpened(object sender)
  {
    if (!(sender is RoutedEventArgs routedEventArgs))
      return;
    TreeListViewRow clickedElement = routedEventArgs.OriginalSource is RadContextMenu originalSource ? originalSource.GetClickedElement<TreeListViewRow>() : (TreeListViewRow) null;
    if (clickedElement == null)
      return;
    clickedElement.GridViewDataControl.SelectedItem = clickedElement.Item;
  }
}
