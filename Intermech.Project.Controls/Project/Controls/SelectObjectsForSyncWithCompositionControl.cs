// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.SelectObjectsForSyncWithCompositionControl
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Bars;
using Intermech.Client.Core.Forms;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Snapshots;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Контрол выбора объектов для синхронизации задач проекта с составом объекта</summary>
public class SelectObjectsForSyncWithCompositionControl : 
  SelectObjectsForSyncWithCompositionControlBase,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  ITreeListColumns,
  ICommandTarget,
  ITreeNodesFactory,
  IDBObjectsSource
{
  /// <summary>Дескриптор импортированного в проект объекта</summary>
  [NotNull]
  protected ImportedObject _ImportedObject;
  /// <summary>Глобальный идентификатор версии корневого объекта, импортированного в проект</summary>
  [NotEmpty]
  protected Guid _RootImportedObjectVersionGuid;
  /// <summary>Проект, с которым производится синхронизация</summary>
  [NotNull]
  protected Intermech.Project.Project _Project;
  private bool _isInitialized;
  private const string LoadCheckedCompositionName = "SelectObjectsForSyncWithCompositionControl: Checked composition is loading";
  [NotNull]
  [ItemNotNull]
  private readonly List<NavigatorTreeNode> _loadCheckedCompositionTreeNodes = new List<NavigatorTreeNode>();

  /// <summary>Тип контрола дерева, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public new static System.Type OverrideTreeViewClass
  {
    [DebuggerStepThrough] get => SelectObjectCompositionNavTreeView.OverrideTreeViewClass;
    [DebuggerStepThrough] set
    {
      SelectObjectCompositionNavTreeView.OverrideTreeViewClass = !(value != (System.Type) null) || !(value != typeof (SelectObjectsForSyncWithCompositionNavTreeView)) || value.IsSubclassOf(typeof (SelectObjectsForSyncWithCompositionNavTreeView)) ? value : throw new Exception($"Tree class must be {typeof (SelectObjectsForSyncWithCompositionNavTreeView).FullName} or it`s child class");
    }
  }

  public SelectObjectsForSyncWithCompositionControl()
  {
    this.InitializeComponent();
    this.TreeView.JobsUpdateCanceled += new EventHandler(this.TreeView_JobsUpdateCanceled);
  }

  /// <summary>Вызывается после инициализации фрейма</summary>
  protected override void BuildTree([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._ImportedObject = this.Services.GetService<ImportedObject>(true, "ImportedObject not found in context services");
    this._RootImportedObjectVersionGuid = this._ImportedObject.ObjectVersionGuid;
    this._Project = this.Services.GetService<Intermech.Project.Project>(true, "Project not found in context services");
    base.BuildTree(sender, e);
    this.SyncCheckedWithProject();
    this.LoadCheckedComposition();
    this._isInitialized = true;
  }

  /// <summary>Синхронизация отметок загруженных нод с актуальным составом проекта</summary>
  private void SyncCheckedWithProject()
  {
    foreach (NavigatorTreeNode navigatorTreeNode in (IEnumerable<NavigatorTreeNode>) this.RootObjectNavigatorTreeNodes)
      this.SyncNodeCheckStatusWithProject(navigatorTreeNode);
  }

  /// <summary>Рекурсивная синхронизация отметки у переданной ноды и входящих в неё с актуальным составом проекта</summary>
  private void SyncNodeCheckStatusWithProject([NotNull] NavigatorTreeNode node, bool processOnlyChilds = false)
  {
    if (!processOnlyChilds && node.NodeID is NodeID nodeId && this._Project.IsObjectWasImportedAsTask(this._RootImportedObjectVersionGuid, nodeId.ObjectID))
    {
      node.SetCheckState(node.HasChildren ? CheckState.Indeterminate : CheckState.Checked, false, false, false);
      if (node.HasChildren && !node.Full && this._isInitialized)
        this.StartLoadCheckedComposition((ICollection<NavigatorTreeNode>) new NavigatorTreeNode[1]
        {
          node
        });
    }
    if (node.CheckState == CheckState.Unchecked || !node.HasChildren || !node.Full || node.Children == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
      this.SyncNodeCheckStatusWithProject(child);
    if (node.CheckState != CheckState.Indeterminate || !node.Children.All<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (childNode => childNode.CheckState == CheckState.Checked)))
      return;
    node.SetCheckState(CheckState.Checked, true, false, false);
  }

  /// <summary>Вызывается после загрузки всех дочерних нод, проставляет отметки у них, корректирует отметку у данной если требуется</summary>
  protected override void ProcessChecks_AfterChildsLoaded([NotNull] NavigatorTreeNode node)
  {
    if (this._ImportedObject != null && node.CheckState == CheckState.Indeterminate)
    {
      if (!node.HasChildren)
        node.SetCheckState(CheckState.Checked, true, false, false);
      else
        this.SyncNodeCheckStatusWithProject(node, true);
    }
    lock (this._loadCheckedCompositionTreeNodes)
    {
      if (this._loadCheckedCompositionTreeNodes.Any<NavigatorTreeNode>())
      {
        int index = this._loadCheckedCompositionTreeNodes.IndexOf(node);
        if (index >= 0)
        {
          this._loadCheckedCompositionTreeNodes.RemoveAt(index);
          if (this._ObjectStructureIsLoadingForm != null)
            ++this._ObjectStructureIsLoadingForm.ObjectsLoaded;
          this._loadCheckedCompositionTreeNodes.RemoveRange<NavigatorTreeNode>((IEnumerable<NavigatorTreeNode>) this._loadCheckedCompositionTreeNodes.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (queriedNode => queriedNode.Full || !queriedNode.HasChildren)).ToList<NavigatorTreeNode>());
          if (this._ObjectStructureIsLoadingForm != null)
          {
            if (this._loadCheckedCompositionTreeNodes.Count == 0)
              this._ObjectStructureIsLoadingForm.Close();
          }
        }
      }
    }
    Application.DoEvents();
  }

  /// <summary>Рекурсивная загрузка состава всех отмеченных нод в дереве</summary>
  public override void LoadCheckedComposition()
  {
    if (this.TreeView.RootNode == null)
      return;
    this._LoadCheckedComposition(this.TreeView.RootNode);
  }

  private void StartLoadCheckedComposition([NotNull] ICollection<NavigatorTreeNode> nodes)
  {
    lock (this._loadCheckedCompositionTreeNodes)
      this._loadCheckedCompositionTreeNodes.SafeAddRange<NavigatorTreeNode>(nodes.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node => node.HasChildren && !node.Full)));
    if (this._ObjectStructureIsLoadingForm == null)
    {
      lock (this._loadCheckedCompositionTreeNodes)
      {
        if (this._loadCheckedCompositionTreeNodes.Count > 0)
        {
          this.LockSave("SelectObjectsForSyncWithCompositionControl: Checked composition is loading");
          this._ObjectStructureIsLoadingForm = ObjectStructureIsLoadingForm.Init(this.FindForm(), new Action(this.CancelAutoLoad));
        }
      }
    }
    foreach (NavigatorTreeNode node in (IEnumerable<NavigatorTreeNode>) nodes)
      this._treeView.QueuePlusJob(node);
    this.TreeView.ReduceJobQueue();
    Application.DoEvents();
  }

  /// <summary>Выполнение команды "Загрузить состав отмеченных объектов"
  /// Работает рекурсивно</summary>
  public void _LoadCheckedComposition([NotNull] NavigatorTreeNode rootNode)
  {
    List<NavigatorTreeNode> list = rootNode.EnumerationWithChilds().Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node => node.NodeID.IsObjectCategory() && node.CheckState != CheckState.Unchecked && node.HasChildren && !node.Full)).ToList<NavigatorTreeNode>();
    if (list.Any<NavigatorTreeNode>())
      this.StartLoadCheckedComposition((ICollection<NavigatorTreeNode>) list);
    if (this._ObjectStructureIsLoadingForm == null)
      return;
    lock (this._loadCheckedCompositionTreeNodes)
    {
      if (this._loadCheckedCompositionTreeNodes.Count != 0)
        return;
      this._ObjectStructureIsLoadingForm.Close();
    }
  }

  private void CancelAutoLoad()
  {
    this._ObjectStructureIsLoadingForm = (ObjectStructureIsLoadingForm) null;
    this.TreeView.FireFinishLoadTreeComposition();
    if (this._loadCheckedCompositionTreeNodes.Count > 0)
    {
      lock (this._loadCheckedCompositionTreeNodes)
      {
        this._loadCheckedCompositionTreeNodes.Clear();
        this._treeView.CancelUpdateJobs((object) null, false);
      }
    }
    this.AfterObjectStructureIsLoadingForm();
    this.UnlockSave("SelectObjectsForSyncWithCompositionControl: Checked composition is loading");
  }

  /// <summary>Кнопка "Отметить все новые объекты"</summary>
  protected override void _btnCheckAll_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CheckAllNew();
    this.TreeView.Focus();
  }

  /// <summary>Кнопка "Снять все отметки у удалённых объектов"</summary>
  protected override void _btnUnCheckAll_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UncheckAllDeleted();
    this.TreeView.Focus();
  }

  /// <summary>Отметить все новые объекты</summary>
  private void CheckAllNew()
  {
    foreach (NavigatorTreeNode enumerationWithChild in this.TreeView.RootNode.EnumerationWithChilds((Func<NavigatorTreeNode, bool>) (node => node.NodeID is CompareWithSnapshotObjectNodeID nodeId1 && nodeId1.CompareResult == CompositionCompareResult.New && node.CheckState == CheckState.Unchecked), (Func<NavigatorTreeNode, bool>) (node => (!(node.NodeID is CompareWithSnapshotObjectNodeID nodeId2) || nodeId2.CompareResult != CompositionCompareResult.Deleted) && node.CheckState != 0)))
      enumerationWithChild.CheckState = CheckState.Checked;
  }

  /// <summary>Снять все отметки у удалённых объектов</summary>
  private void UncheckAllDeleted()
  {
    foreach (NavigatorTreeNode enumerationWithChild in this.TreeView.RootNode.EnumerationWithChilds((Func<NavigatorTreeNode, bool>) (node => node.NodeID is CompareWithSnapshotObjectNodeID nodeId1 && nodeId1.CompareResult == CompositionCompareResult.Deleted && node.CheckState != 0), (Func<NavigatorTreeNode, bool>) (node => (!(node.NodeID is CompareWithSnapshotObjectNodeID nodeId2) || nodeId2.CompareResult == CompositionCompareResult.NotChanged) && node.CheckState != 0)))
      enumerationWithChild.CheckState = CheckState.Unchecked;
  }

  /// <summary>Наполнение выпадающего меню кнопки "Отметить все новые объекты"</summary>
  protected override void FillCheckAllDropDownMenu(ContextMenuBarItem checkAllDropDownMenuItem)
  {
    this.AddCommandToMenu(checkAllDropDownMenuItem, "Отметить все новые объекты в составе", "CheckAllNew", true);
    base.FillCheckAllDropDownMenu(checkAllDropDownMenuItem);
  }

  /// <summary>Наполнение выпадающего меню кнопки "Снять отметки у удалённых объектов"</summary>
  protected override void FillUncheckAllDropDownMenu(ContextMenuBarItem uncheckAllDropDownMenuItem)
  {
    this.AddCommandToMenu(uncheckAllDropDownMenuItem, "Снять отметки у удалённых объектов", "UncheckAllDeleted", false);
    base.FillUncheckAllDropDownMenu(uncheckAllDropDownMenuItem);
  }

  /// <summary>Обработка команды выпадающего меню кнопки "Выбрать все"</summary>
  /// <returns>true если команда обработана, иначе false</returns>
  protected override bool ProcessCommand(string commandName)
  {
    switch (commandName)
    {
      case "CheckAllNew":
        this.CheckAllNew();
        return true;
      case "UncheckAllDeleted":
        this.UncheckAllDeleted();
        return true;
      default:
        return base.ProcessCommand(commandName);
    }
  }

  /// <summary>Обработчик события очистки очереди запланированных работ</summary>
  private void TreeView_JobsUpdateCanceled([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._ObjectStructureIsLoadingForm == null)
      return;
    this._ObjectStructureIsLoadingForm.Close();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.Name = nameof (SelectObjectsForSyncWithCompositionControl);
  }
}
