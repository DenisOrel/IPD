// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.SyncWithCompositionForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Infralution.Controls;
using Intermech.Bars;
using Intermech.Client;
using Intermech.Client.Core;
using Intermech.Client.Core.Forms;
using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.Project.Controls.AdvStructure;
using Intermech.UI;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class SyncWithCompositionForm : 
  SyncWithCompositionFormBase,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  /// <summary>Дескриптор импортированного в проект объекта</summary>
  [CanBeNull]
  protected ImportedObject _ImportedObject;
  [NotNull]
  private Intermech.Project.Project _project;
  private IContainer components;

  /// <summary>Тип контрола выбора объектов из структуры, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного формы, в этом случае контрол будет создан указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public new static System.Type OverrideSelectObjectsInCompositionControlType
  {
    [DebuggerStepThrough] get
    {
      return ImportObjectsFormAdvBase.OverrideSelectObjectsInCompositionControlType;
    }
    [DebuggerStepThrough] set
    {
      ImportObjectsFormAdvBase.OverrideSelectObjectsInCompositionControlType = !(value != (System.Type) null) || !(value != typeof (SelectObjectsForSyncWithCompositionControl)) || value.IsSubclassOf(typeof (SelectObjectsForSyncWithCompositionControl)) ? value : throw new Exception($"Tree class must be {typeof (SelectObjectsForSyncWithCompositionControl).FullName} or it`s child class");
    }
  }

  /// <summary>Конструктор Design-time</summary>
  protected SyncWithCompositionForm()
  {
    Intermech.Diagnostics.Check.ObjectState(this.InDesignMode, "Only in design mode");
    this._project = new Intermech.Project.Project();
    this.InitializeComponent();
  }

  /// <summary>Constructor</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="contextName">Уникальное имя операции. Служит для идентификации контейнера настроек для сохранения/чтения настроек формы</param>
  /// <param name="importedObject">Дескриптор импортированного в проект объекта</param>
  public SyncWithCompositionForm(
    [NotNull] System.IServiceProvider ownerServices,
    [NotNull] string contextName,
    [NotNull] ImportedObject importedObject)
    : base(ownerServices, contextName)
  {
    this.InitializeComponent();
    this._project = this.Services.GetService<Intermech.Project.Project>(true, "Project not found in context services");
    this._ImportedObject = importedObject;
    this.ServiceContainer.AddService<ImportedObject>(this._ImportedObject);
    this._Settings = this.CreateEmptySettings();
    this.TreeViewControl.OnInitTreeServices += new NavigatorTreeViewWithObjectTypeFiltration.OnInitTreeServicesDelegate(this.TreeViewControl_OnInitTreeServices);
    this.TreeView.OnCreateObjectDescriptor = new ObjectsCompositionNavigatorTree.OnCreateObjectDescriptorDelegate(this.TreeViewControl_CreateObjectDescriptor);
    this.TreeViewControl.CreateSettingsForm = new SelectObjectCompositionNavTreeView.CreateSettingsFormDelegate(this.TreeViewControl_CreateSettingsForm);
    this.TreeViewControl.CreateDefaultSettings = new SelectObjectCompositionNavTreeView.CreateDefaultSettingsDelegate(SyncWithCompositionForm.TreeViewControl_CreateDefaultSettings);
  }

  /// <summary>Инициализация сервисов дерева</summary>
  private void TreeViewControl_OnInitTreeServices([NotNull] AdvancedServiceContainer treeServices)
  {
    if (this._ImportedObject == null || this._ImportedObject.ObjectIterationID == 0L)
      return;
    ISnapshot serviceInstance = Repository.Snapshots.Create(this._ImportedObject.ObjectIterationID, failIfNotFound: false);
    if (serviceInstance != null)
      treeServices.AddService(typeof (ISnapshot), (object) serviceInstance);
    else
      this._ImportedObject.MarkObjectIterationAsDeleted();
  }

  /// <summary>Конструктор дескриптора объекта</summary>
  [NotNull]
  private Intermech.Navigator.DBObjects.Descriptor TreeViewControl_CreateObjectDescriptor(
    [NotEmpty] long objectVersionID)
  {
    return (Intermech.Navigator.DBObjects.Descriptor) CompareWithSnapshotObjectDescriptor.Create((System.IServiceProvider) this.TreeViewControl.ServicesTree, objectVersionID);
  }

  protected override ImportObjectSettings _CreateEmptySettings()
  {
    return this._ImportedObject.ImportSettings;
  }

  protected override ImportObjectSettings CreateEmptySettings()
  {
    return this._ImportedObject == null ? (ImportObjectSettings) null : base.CreateEmptySettings();
  }

  protected override bool SerializeImportSettings() => false;

  /// <summary>Shows the dialog</summary>
  public new DialogResult ShowDialog()
  {
    return this.ShowDialog((IReadOnlyCollection<long>) new long[1]
    {
      this.RootObjectVersionID
    });
  }

  private void SyncWithCompositionForm_Shown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateUI_fromSettings();
  }

  /// <summary>Идентификатор версии объекта, чей состав синхронизируется</summary>
  public long RootObjectVersionID
  {
    get
    {
      ImportedObject importedObject = this._ImportedObject;
      return importedObject == null ? 0L : importedObject.ObjectVersionID;
    }
  }

  /// <summary>Дескриптор импортированного в проект объекта</summary>
  [NotNull]
  public ImportedObject ImportedObject
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._ImportedObject.CheckInitializedIn<ImportedObject>((object) this);
    }
  }

  /// <summary>Настройки импорта</summary>
  [NotNull]
  public ImportObjectSettings ImportSettings
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ImportedObject.ImportSettings;
    }
  }

  /// <summary>Идентификатор итерации объекта, сохранённой при импорте, либо в момент последней синхронизации состава</summary>
  public long IterationID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ImportedObject.ObjectIterationID;
    }
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this.DialogResult != DialogResult.OK)
      return;
    int num = e.Cancel ? 1 : 0;
  }

  /// <summary>Внешняя функция для создания диалога настроек дерева структуры объекта для синхронизации</summary>
  [NotNull]
  private SelectObjectCompositionsSettingsForm TreeViewControl_CreateSettingsForm(
    [CanBeNull] Form parentForm,
    [CanBeNull] string contextName,
    [NotNull] SelectObjectCompositionSettings settings)
  {
    return (SelectObjectCompositionsSettingsForm) new SyncObjectStructureSettingsDialog(parentForm, this.Services, contextName, settings);
  }

  /// <summary>Создание настроек дерева структуры объекта для синхронизации по-умолчанию</summary>
  [NotNull]
  private static SelectObjectCompositionSettings TreeViewControl_CreateDefaultSettings()
  {
    return (SelectObjectCompositionSettings) new SyncObjectCompositionSettings();
  }

  protected override void InitTreeView(
    IReadOnlyCollection<long> selectedObjectVersions,
    List<int> objectTypeIDsThatCanBeImportToProject)
  {
    this._treeViewControl.Init(this.Services, selectedObjectVersions, (IReadOnlyCollection<int>) objectTypeIDsThatCanBeImportToProject);
  }

  [CanBeNull]
  protected override IEnumerable<string> GetWarnings() => (IEnumerable<string>) null;

  /// <summary>Синхронизация отметок с проектом</summary>
  public void SyncWithProject()
  {
    if (this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID != 0L)
    {
      this._Prototype = new Intermech.Project.Project();
      this._Prototype.AssignProperties((Task) this.Project);
      this._Prototype.Load(this._SelectedSpecialSettingsForObjType.PrototypeObjectVersionID, new bool?(false));
    }
    this._SubProjectObjectImportInfoStack = (Stack<ImportObjectsFormAdv.SubProjectObjectImportInfo>) null;
    this.CreatedTasks = new List<(Task, long, int, long)>();
    this.CurrentRootImportedObjectGuid = this.ImportedObject.ObjectVersionGuid;
    this.ProjectView.GridView.CancelEdit();
    Entity.GlobalBeginUpdate();
    this.Project.SetState(TaskState.Loading);
    using (this._ImportSessionKeeper = new SessionKeeper())
    {
      try
      {
        List<Task> list = this.Project.TasksImportedFromObject[this.CurrentRootImportedObjectGuid].ToList<Task>().Where<Task>((Func<Task, bool>) (task => !this.ObjectIsChecked(task.ImportedObjectVersionID))).ToList<Task>();
        if (list.Any<Task>())
          this.Project.RemoveTasks((IEnumerable<Task>) list);
        this.SyncChecked(this.TreeView.RootNode, true);
        if (this.CreatedTasks == null || this.CreatedTasks.Count <= 0 || this._SelectedSpecialSettingsForObjType.FinalScriptID == 0L)
          return;
        MiscFunx.ExecScript(this._ImportSessionKeeper.Session, this._SelectedSpecialSettingsForObjType.FinalScriptID, (object) this.Project, (object) this.CreatedTasks);
      }
      finally
      {
        this.Project.UnsetState(TaskState.Loading);
        Entity.GlobalEndUpdate();
        this.Project.DebugClearCache();
      }
    }
  }

  private void SyncChecked([NotNull] NavigatorTreeNode node, bool addChildren, int level = 0)
  {
    if (node.CheckState == CheckState.Unchecked && (this.TreeView.GetObjectNodeLevel(node) != 0 || this._Settings.ImportRootObjects) || !this.CheckMaxLevel(node))
      return;
    if (this._Settings.ImportRootObjects || this.TreeView.GetObjectNodeLevel(node) > 0)
    {
      long objectId = (node.NodeID as NodeID).ObjectID;
      if (this._AddedObjectIDs.Contains(objectId))
        return;
      int typeId = (node.NodeID as NodeID).TypeID;
      if (this._DisallowedTypes.Contains(typeId))
        return;
      this._SelectedSpecialSettingsForObjType = this._Settings.SettingsForObjType[typeId];
      this._AddedObjectIDs.Add(objectId);
      if (this.SyncObject(node, ref level) == null)
        return;
      if (!this._Settings.LinearImport)
        ++level;
    }
    if (!addChildren || node.Children == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
      this.SyncChecked(child, true, level);
  }

  [CanBeNull]
  private Task SyncObject([NotNull] NavigatorTreeNode node, ref int level, bool creatingSummary = false)
  {
    bool flag = this._Prototype != null && ((!node.HasChildren || !node.Full ? 1 : (node.Children.All<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (childNode => !childNode.ShowCheckState || childNode.CheckState == CheckState.Unchecked)) ? 1 : 0)) | (creatingSummary ? 1 : 0)) != 0;
    if (this._SubProjectNodes.Contains(node))
      flag = false;
    if (!flag)
      return this._syncObject(node, ref level, this._SelectedSpecialSettingsForObjType.InitTaskParams, creatingSummary);
    try
    {
      Task task1 = (Task) null;
      foreach (Task task2 in (System.Collections.ObjectModel.Collection<Task>) this._Prototype.Tasks)
      {
        if (!task2.IsProjectSummaryTask)
        {
          int level1 = level + task2.IndentLevel;
          Task task3 = this.Project.GetTaskImportedByObject(this.CurrentRootImportedObjectGuid, (node.NodeID as NodeID).ObjectID) ?? this._syncObject(node, ref level1, task2, creatingSummary);
          if (this._SelectedSpecialSettingsForObjType.InitTaskParams != null && this._SelectedSpecialSettingsForObjType.InitTaskParams.Start != DateTime.MinValue && task3.Start != DateTime.MinValue)
            task3.Start = this._SelectedSpecialSettingsForObjType.InitTaskParams.Start + (task2.Start - this._Prototype.Start);
          this._LastTask = (Task) null;
          this._LastNode = (NavigatorTreeNode) null;
          if (task1 == null)
            task1 = task3;
        }
      }
      if (!creatingSummary && this._Settings.CopySummaries)
      {
        this._LastTask = task1;
        this._LastNode = node;
        this._LastLevel = level;
      }
      foreach (Task task4 in (System.Collections.ObjectModel.Collection<Task>) this._Prototype.Tasks)
      {
        if (task4.Dependencies.Count > 0)
        {
          Task addedByPrototype1 = this.FindAddedByPrototype(task4);
          if (addedByPrototype1 != null)
          {
            foreach (Dependency dependency in (System.Collections.ObjectModel.Collection<Dependency>) task4.Dependencies)
            {
              Task addedByPrototype2 = this.FindAddedByPrototype(dependency.DependentOfTask);
              if (addedByPrototype2 != null)
                new Dependency(addedByPrototype2, dependency.DependencyType).Task = addedByPrototype1;
            }
          }
        }
      }
      return task1;
    }
    finally
    {
      this._AddedTasks.Clear();
    }
  }

  [CanBeNull]
  protected Task _syncObject(
    [NotNull] NavigatorTreeNode node,
    ref int level,
    [CanBeNull] Task proto,
    bool creatingSummary = false)
  {
    NodeID nodeId = node.NodeID as NodeID;
    if (!creatingSummary && this._LastNode != null && level > this._LastLevel)
      this.SyncObject(this._LastNode, ref level, true);
    string name = nodeId?.Caption ?? string.Empty;
    this._CurrentObject = (IDBObject) null;
    if (proto != null)
    {
      if (nodeId != null)
        this._CurrentObject = this._ImportSessionKeeper.Session.GetObject(nodeId.ObjectID, false);
      name = StringFuncs.ReplaceMacros(proto.Name, new StringFuncs.GetMacroValueDelegate(((ImportObjectsFormAdv) this).GetMacroValue));
    }
    Task task = this._syncObject(node, name, proto, ref level, this._SubProjectNodes.Contains(node));
    task.Tag = (object) proto;
    if (proto != null)
      this._AddedTasks[task.Tag] = task;
    if (nodeId != null)
    {
      PrjAttachment prjAttachment = new PrjAttachment();
      prjAttachment.ObjectID = nodeId.ObjectID;
      prjAttachment.ID = nodeId.ID;
      prjAttachment.TypeID = nodeId.ObjectTypeID;
      prjAttachment.Kind = PrjAttachKind.SrcData;
      if (!task.Attachments.Contains((Attachment) prjAttachment))
        task.Attachments.Add((Attachment) prjAttachment);
    }
    if (this._SelectedSpecialSettingsForObjType.InitTaskScriptID != 0L)
    {
      if (this._CurrentObject == null && nodeId != null)
        this._CurrentObject = this._ImportSessionKeeper.Session.GetObject(nodeId.ObjectID, false);
      object[] objArray = new object[2]
      {
        (object) task,
        (object) this._CurrentObject
      };
      MiscFunx.ExecScript(this._ImportSessionKeeper.Session, this._SelectedSpecialSettingsForObjType.InitTaskScriptID, objArray);
      if (task != objArray[0] && objArray[0] == null)
      {
        this.Project.Tasks.Remove(task);
        --this._TaskIndex;
        return (Task) null;
      }
    }
    if (this._Settings.CopySummaries)
    {
      this._LastTask = task;
      this._LastNode = node;
      this._LastLevel = level;
    }
    return task;
  }

  [NotNull]
  private Task _syncObject(
    [NotNull] NavigatorTreeNode node,
    [CanBeNull] string name,
    [CanBeNull] Task proto,
    ref int level,
    bool isProject = false)
  {
    NodeID nodeId = (NodeID) node.NodeID;
    this.Project.Tasks.ResetBindings();
    Task task = this.Project.GetTaskImportedByObject(this.CurrentRootImportedObjectGuid, nodeId.ObjectID);
    if (task != null)
    {
      if (isProject)
      {
        Intermech.Project.Project project = task as Intermech.Project.Project;
      }
      this._TaskIndex = task.Index + 1;
      level = task.IndentLevel;
    }
    else
    {
      if (this._SubProjectObjectImportInfoStack != null && level <= this.CurrentProjectIdentLevel && this._SubProjectObjectImportInfoStack.Count > 0)
      {
        ImportObjectsFormAdv.SubProjectObjectImportInfo objectImportInfo = this._SubProjectObjectImportInfoStack.Pop();
        this.CurrentRootImportedObjectGuid = objectImportInfo.RootImportedObjectGuid;
        this.CurrentProjectIdentLevel = objectImportInfo.ProjectIdentLevel;
      }
      if (isProject)
      {
        Intermech.Project.Project project = new Intermech.Project.Project(name ?? string.Empty);
        long objectIteration = this._Settings.CreateIteration ? this.CreateObjectIteration(node) : 0L;
        ImportObjectSettings importSettings = (ImportObjectSettings) this._Settings.Clone();
        importSettings.LimitMaxLevelsCount = Math.Max(1, this._Settings.LimitMaxLevelsCount - this.TreeView.GetObjectNodeLevel(node));
        importSettings.ImportRootObjects = false;
        this._SubProjectObjectImportInfoStack = this._SubProjectObjectImportInfoStack ?? new Stack<ImportObjectsFormAdv.SubProjectObjectImportInfo>();
        this._SubProjectObjectImportInfoStack.Push(new ImportObjectsFormAdv.SubProjectObjectImportInfo(this.CurrentRootImportedObjectGuid, this.CurrentProjectIdentLevel));
        this.CurrentRootImportedObjectGuid = this._ImportSessionKeeper.Session.GetObjectInfo(nodeId.ObjectID).VersionGuid;
        this.CurrentProjectIdentLevel = level;
        project.AddImportedObjectInfo(nodeId.ObjectID, objectIteration, importSettings);
        task = (Task) project;
      }
      else
      {
        ImportObjectsFormAdv.ClonedTask clonedTask = new ImportObjectsFormAdv.ClonedTask(name ?? string.Empty);
        if (proto != null)
          clonedTask.LoadFrom(proto);
        task = (Task) clonedTask;
      }
      this.Project.Tasks.Insert(this._TaskIndex, task);
      ++this._TaskIndex;
      if (name != null)
        task.Name = name;
      task.IndentLevel = level;
      if (proto != null && proto.IndentLevel == -1 && proto.Assignments.Count > 0)
        task.Assignments.AddRange(proto.Assignments.Select<Assignment, Assignment>((Func<Assignment, Assignment>) (assignment => new Assignment(assignment.Resource, assignment.Units, assignment.MaxUnits))));
      task.LinkWithImportedObject(this.CurrentRootImportedObjectGuid, nodeId.ObjectID, nodeId.RelGuid);
    }
    this.CreatedTasks.Add((task, nodeId.ObjectID, nodeId.ObjectTypeID, nodeId.PrjLinkID));
    return task;
  }

  /// <summary>Проверить, загружен ли объект в дерево, а если загружен, то установлена ли у него отметка</summary>
  private bool ObjectIsChecked(long objectVersionID)
  {
    return this.TreeView.NodesEnumeration().Any<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node => Math.Abs(node.NodeID is NodeID nodeId ? nodeId.ObjectID : 0L) == objectVersionID && node.CheckState != 0));
  }

  /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SyncWithCompositionForm));
    this.TreeViewControl.PanelSelectButtons.SuspendLayout();
    this._treeViewControl.SuspendLayout();
    this._panelTreeCaption.SuspendLayout();
    this._panelRight.SuspendLayout();
    this._groupBoxSettings.SuspendLayout();
    this._editMaxLevels.BeginInit();
    this._panelRightDown.SuspendLayout();
    this._panel1.SuspendLayout();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.TreeViewControl.TreeView.BeginInit();
    this.SuspendLayout();
    this._treeViewControl.AllowChangeObjects = true;
    this.TreeViewControl.BtnClearSorting.AutoToggle = AutoToggleType.Single;
    this.TreeViewControl.BtnClearSorting.CommandName = "btCancelSort";
    this.TreeViewControl.BtnClearSorting.ImageIndex = 9;
    this.TreeViewControl.BtnClearSorting.ToolTipText = "Режим ручной сортировки";
    this._treeViewControl.BtnSelectObjects.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    this._treeViewControl.BtnSelectObjects.Location = new Point(173, 6);
    this.TreeViewControl.BtnSetupSorting.CommandName = "btSetupSorting";
    this.TreeViewControl.BtnSetupSorting.ImageIndex = 10;
    this.TreeViewControl.BtnSetupSorting.ToolTipText = "Выполнить настройку ручной сортировки";
    this.TreeViewControl.ImagesToolbar.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("SyncWithCompositionForm.TreeViewControl.ImagesToolbar.ImageStream");
    this.TreeViewControl.ImagesToolbar.TransparentColor = Color.Transparent;
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(0, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(1, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(2, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(3, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(4, "ручная_сортировка.png");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(5, "настройка_ручной_сортировки.png");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(6, "SettingsIcons");
    this.TreeViewControl.LabelSpace.BeginGroup = true;
    this.TreeViewControl.LabelSpace.CommandName = "labelSpace";
    this.TreeViewControl.LabelSpace.Enabled = false;
    this.TreeViewControl.LabelSpace.Stretch = true;
    this.TreeViewControl.LabelSpace.Text = " ";
    this.TreeViewControl.LabelSpace.ToolTipText = " ";
    this.TreeViewControl.PanelSelectButtons.Location = new Point(0, 474);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnUncheckAll, 0);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnCheckAll, 0);
    this.TreeViewControl.TreeToolbar.FlipLastItem = true;
    this.TreeViewControl.TreeToolbar.FullMenus = true;
    this.TreeViewControl.TreeToolbar.Guid = new Guid("3fb71a02-4b93-44ea-84a6-db6e9ca5869f");
    this.TreeViewControl.TreeToolbar.Hidden = false;
    this.TreeViewControl.TreeToolbar.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.TreeViewControl.BtnClearSorting,
      (ToolbarItemBase) this.TreeViewControl.BtnSetupSorting,
      (ToolbarItemBase) this.TreeViewControl.LabelSpace
    });
    this.TreeViewControl.TreeToolbar.Location = new Point(0, 0);
    this.TreeViewControl.TreeToolbar.Name = "_tbTreePanel";
    this.TreeViewControl.TreeToolbar.Size = new Size(562, 24);
    this.TreeViewControl.TreeToolbar.TabIndex = 8;
    this.TreeViewControl.TreeToolbar.Text = "";
    this._checkBoxAsProject.Visible = false;
    this._editIterationName.Visible = false;
    this._checkBoxImportRoot.Enabled = false;
    this._labelIterationName.Visible = false;
    this._checkBoxCopySummaries.Enabled = false;
    this._checkBoxCreateIteration.Visible = false;
    this._checkBoxLinear.Enabled = false;
    this._panelRightDown.Visible = false;
    this._bevelObjTypes.Style = BevelStyle.Lowered;
    this._checkBoxAsSubTask.Enabled = false;
    this.bevel1.Style = BevelStyle.Lowered;
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.ClientSize = new Size(915, 637);
    this.Name = nameof (SyncWithCompositionForm);
    this.Text = "Синхронизация с составом объекта";
    this.TreeViewControl.TreeView.BackgroundImageMode = ImageDrawMode.Tile;
    this.TreeViewControl.TreeView.BorderStyle = BorderStyle.Fixed3D;
    this.TreeViewControl.TreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.TreeViewControl.TreeView.RootDbObjectVersionIDs = (IReadOnlyList<long>) componentResourceManager.GetObject("_treeViewControl.TreeView.RootDbObjectVersionIDs");
    this.TreeViewControl.TreeView.RowEvenStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowOddStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowSelectedStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowStyle.BorderColor = SystemColors.Control;
    this.TreeViewControl.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.TreeViewControl.TreeView.RowStyle.BorderWidth = 1;
    this.TreeViewControl.TreeView.RowStyle.WordWrap = false;
    this.TreeViewControl.TreeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.Shown += new EventHandler(this.SyncWithCompositionForm_Shown);
    this.TreeViewControl.PanelSelectButtons.ResumeLayout(false);
    this.TreeViewControl.PanelSelectButtons.PerformLayout();
    this._treeViewControl.ResumeLayout(false);
    this._panelTreeCaption.ResumeLayout(false);
    this._panelTreeCaption.PerformLayout();
    this._panelRight.ResumeLayout(false);
    this._groupBoxSettings.ResumeLayout(false);
    this._groupBoxSettings.PerformLayout();
    this._editMaxLevels.EndInit();
    this._panelRightDown.ResumeLayout(false);
    this._panel1.ResumeLayout(false);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.TreeViewControl.TreeView.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
