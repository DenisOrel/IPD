// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.SelectImportedObjectForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Client.Core;
using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.ContextMenu.Extensions;
using Intermech.Navigator.Controls;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class SelectImportedObjectForm : 
  IpsBaseDialog,
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
  ICanBeReadOnly2,
  IIODestination,
  ICommandsProvider,
  ILocalCommandsProvider
{
  public const string DeleteCommandName = "DeleteImportedObject";
  /// <summary>Проект</summary>
  [NotNull]
  private readonly Intermech.Project.Project _project;
  /// <summary>Диспетчер событий</summary>
  [NotNull]
  private readonly IIODispatcher _ioDispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>Список идентификаторов объектов, информация о импорте которых должна быть удалена из проекта</summary>
  [NotNull]
  private readonly List<long> _deleteImportedObjectIDs = new List<long>();
  /// <summary>Список идентификаторов объектов, задачи, импортированные из которых должны быть удалены из проекта</summary>
  [NotNull]
  private readonly List<long> _deleteTasksImportedObjectIDs = new List<long>();
  /// <summary>Дескриптор отображения списка объектов в гриде</summary>
  [CanBeNull]
  private ObjectsSelectionDescriptor _projectImportedObjectsDescriptor;
  /// <summary>Заготовка элемента меню для команды "Удалить объект из импортированных в проект"</summary>
  [CanBeNull]
  private MenuTemplateNode _deleteObjectMenuTemplateNode;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _panelButtons;
  private Bevel _bevelButtons;
  private Button _buttonDelete;
  private ObjectsViewBase _objectsView;
  protected Panel _panelTreeCaption;
  protected Label _labelImportedObjectsCaption;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Panel PanelButtons
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelButtons.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Bevel BevelButtons
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._bevelButtons.CheckInitializedIn<Bevel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Button ButtonDelete
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonDelete.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal ObjectsViewBase ObjectsView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._objectsView.CheckInitializedIn<ObjectsViewBase>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Panel PanelTreeCaption
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelTreeCaption.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Label LabelImportedObjectsCaption
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelImportedObjectsCaption.CheckInitializedIn<Label>((object) this);
    }
  }

  public SelectImportedObjectForm()
  {
    Intermech.Diagnostics.Check.ObjectState(this.InDesignMode, "Only in design mode");
    this._project = new Intermech.Project.Project();
    this.InitializeComponent();
  }

  public SelectImportedObjectForm(
    [NotNull] System.IServiceProvider ownerServices,
    [CanBeNull, NotEmpty] string contextName,
    bool multiSelect = false)
    : base(ownerServices, contextName)
  {
    this.MultiSelect = multiSelect;
    this._project = ownerServices.GetService<Intermech.Project.Project>();
    this.InitializeComponent();
    if (this.Services.GetService<INotificationService>(false) == null)
      this.ServiceContainer.AddService(typeof (INotificationService), (object) new NotificationService());
    this.ServiceContainer.AddService(typeof (SelectionOptionsHolder), (object) new SelectionOptionsHolder((SelectionOptions) ((multiSelect ? 0L : 16777216L /*0x01000000*/) | 64L /*0x40*/ | 16L /*0x10*/ | 32L /*0x20*/)));
    this.ServiceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.ReadOnly | ViewStateFlags.InSelectionWindow));
    this.ServiceContainer.AddService(typeof (IIODispatcher), (object) this._ioDispatcher);
    this.ObjectsView.OnGetMenuServiceContainer = new ChildrenView.GetMenuServiceContainerDelegate(this.ObjectsView_OnGetMenuServiceContainer);
    this._ioDispatcher.RegisterDestination((IIODestination) this);
  }

  /// <summary>внешнюю функцию заполнения провайдера сервисов контекстного меню грида</summary>
  [NotNull]
  private IServiceContainer ObjectsView_OnGetMenuServiceContainer(
    [CanBeNull] object sender,
    [NotNull] IServiceContainer originalMenuServiceContainer)
  {
    if (originalMenuServiceContainer is System.ComponentModel.Design.ServiceContainer localContext)
    {
      localContext.StackLocalContextCommandsProvider((ILocalCommandsProvider) this);
      localContext.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.ReadOnly | ViewStateFlags.InSelectionWindow | ViewStateFlags.DisableGlobalCommandProviders));
    }
    return originalMenuServiceContainer;
  }

  /// <summary>При показе окна</summary>
  protected override void OnShown([NotNull] EventArgs e)
  {
    if (this.InDesignMode)
    {
      base.OnShown(e);
    }
    else
    {
      this._projectImportedObjectsDescriptor = new ObjectsSelectionDescriptor(Localization.GetString("ImportedInProjectObjects"), (IReadOnlyCollection<long>) this._project.ImportedObjects.Select<ImportedObject, long>((Func<ImportedObject, long>) (importedObjectDescriptor => importedObjectDescriptor.ObjectVersionID)).ToList<long>());
      this.ObjectsView.Initialize((IDescriptor) this._projectImportedObjectsDescriptor, this.Services);
      this.ObjectsView.Activate((IView) null);
      this.ObjectsView.OpenEmbeddedViews(150);
      this.ObjectsView.ToggleSplitterState();
      base.OnShown(e);
      this.UpdateCommands();
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.ObjectsView.Deactivate((IView) null);
      this.ServiceContainer.DisposeLocalContextCommandsTemplates((ILocalCommandsProvider) this);
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Список идентификаторов объектов, информация о импорте которых должна быть удалена из проекта</summary>
  [NotNull]
  public IReadOnlyCollection<long> DeleteImportedObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IReadOnlyCollection<long>) this._deleteImportedObjectIDs;
    }
  }

  /// <summary>Список идентификаторов объектов, задачи, импортированные из которых должны быть удалены из проекта</summary>
  [NotNull]
  public IReadOnlyCollection<long> DeleteTasksImportedObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IReadOnlyCollection<long>) this._deleteTasksImportedObjectIDs;
    }
  }

  /// <summary>Сохранять ли изменения</summary>
  public bool SaveChanges { get; private set; } = true;

  /// <summary>Интерфейс идентификатора ноды сфокусированной ноды</summary>
  [CanBeNull]
  public INodeID FocusedNodeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ObjectsView.FocusedNodeID;
    }
  }

  /// <summary>Идентификатор сфокусированной версии объекта</summary>
  [CanBeEmpty]
  public long FocusedObjectVersionID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      INodeID focusedNodeId = this.FocusedNodeID;
      return focusedNodeId == null ? 0L : focusedNodeId.GetObjVerID(false);
    }
  }

  /// <summary>Идентификатор типа сфокусированного объекта</summary>
  [CanBeEmpty]
  public int FocusedObjectTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      INodeID focusedNodeId = this.FocusedNodeID;
      return focusedNodeId == null ? -1 : focusedNodeId.GetObjTypeID(false);
    }
  }

  /// <summary>Идентификаторы выбранных версий объектов</summary>
  [NotNull]
  [ItemNotEmpty]
  public IReadOnlyList<long> SelectedObjectVersionIDs
  {
    [DebuggerStepThrough] get
    {
      if (this.MultiSelect)
        return (IReadOnlyList<long>) this.ObjectsView.SelectedItems.AsNodeIdList().Select<INodeID, long>((Func<INodeID, long>) (iNodeID => iNodeID.GetObjVerID())).ToList<long>(this.ObjectsView.SelectedItems.Count);
      return (IReadOnlyList<long>) new List<long>(1)
      {
        this.FocusedObjectVersionID
      };
    }
  }

  /// <summary>Уникальные идентификаторы типов выбранных объектов</summary>
  [NotNull]
  [ItemNotEmpty]
  public IReadOnlyList<int> SelectedObjectTypeIDs
  {
    [DebuggerStepThrough] get
    {
      if (this.MultiSelect)
        return (IReadOnlyList<int>) this.ObjectsView.SelectedItems.AsNodeIdList().Select<INodeID, int>((Func<INodeID, int>) (iNodeID => iNodeID.GetObjTypeID())).Distinct<int>().ToList<int>(this.ObjectsView.SelectedItems.Count);
      return (IReadOnlyList<int>) new List<int>(1)
      {
        this.FocusedObjectTypeID
      };
    }
  }

  /// <summary>Описание операции сверху таблицы-списка импортированных ранее в проект объектов</summary>
  [NotNull]
  [CanBeEmpty]
  public string OperationHint
  {
    [DebuggerStepThrough] get => this.LabelImportedObjectsCaption.Text;
    set
    {
      if (!(this.LabelImportedObjectsCaption.Text != value))
        return;
      this.LabelImportedObjectsCaption.Text = value;
    }
  }

  /// <summary>Возможность множественного выбора</summary>
  public bool MultiSelect { get; }

  /// <summary>Список поддерживаемых обработчиком событий</summary>
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  /// <summary>Выполнить обработку события</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если обработка выполнена успешно, false, если событие не обработано</returns>
  public bool ProcessEvent([CanBeNull] IIOEvent Event)
  {
    if (Event == null)
      return false;
    switch (Event.EventType)
    {
      case IOEventType.evKeyUp:
        KeyEventArgs eventData = Event.EventData as KeyEventArgs;
        if (eventData.Modifiers == Keys.None && eventData.KeyCode == Keys.Delete)
        {
          this.DeleteSelectedObjects();
          return true;
        }
        break;
      case IOEventType.evMouseDoubleClick:
        if (this.ObjectsView.GetNodeAtCursor((Event.EventData as MouseEventArgs).Location) != null)
        {
          this.DialogResult = DialogResult.OK;
          this.Close();
          this.DialogResult = DialogResult.OK;
          return true;
        }
        break;
    }
    return false;
  }

  /// <summary>Обновить статус доступности команд</summary>
  /// <returns>true если обновление прошло успешно, если обновление команд заблокировано, то false</returns>
  protected override bool UpdateCommands()
  {
    if (!base.UpdateCommands())
      return false;
    this.ButtonDelete.Enabled = this.ObjectsView.SelectedNodeIDs.Any<INodeID>();
    return true;
  }

  /// <summary>Удалить выбранные объекты из списка импортированных в проект, предложить пользователю удалить импортированные задачи</summary>
  protected void DeleteSelectedObjects()
  {
    switch (MessageFuncs.Ask(Localization.GetString("RemoveLinkWithSelectedImportedObjectAndDeleteTasks"), MessageBoxButtons.YesNoCancel))
    {
      case DialogResult.Cancel:
        return;
      case DialogResult.Yes:
        this._deleteTasksImportedObjectIDs.AddRange(this.ObjectsView.SelectedNodeIDs.Select<INodeID, long>((Func<INodeID, long>) (nodeID => Math.Abs(nodeID.GetObjVerID()))));
        break;
    }
    this._deleteImportedObjectIDs.AddRange(this.ObjectsView.SelectedNodeIDs.Select<INodeID, long>((Func<INodeID, long>) (nodeID => Math.Abs(nodeID.GetObjVerID()))));
    this._projectImportedObjectsDescriptor.Update((IReadOnlyCollection<long>) this._project.ImportedObjects.Where<ImportedObject>((Func<ImportedObject, bool>) (importedObjectDescriptor => !this._deleteImportedObjectIDs.Contains(importedObjectDescriptor.ObjectVersionID))).Select<ImportedObject, long>((Func<ImportedObject, long>) (importedObjectDescriptor => importedObjectDescriptor.ObjectVersionID)).ToList<long>(), true);
    this.ObjectsView.ReloadItems();
    this.UpdateCommands();
  }

  /// <summary>Кнопка "Удалить"</summary>
  private void _buttonDelete_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.DeleteSelectedObjects();
  }

  /// <summary>Двойной клик по списку импортированных в проект объектов</summary>
  private void _objectsView_DoubleClick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.ObjectsView.GetNodeAtCursor(this.ObjectsView.PointToClient(Cursor.Position)) == null)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Event handler. Called by SelectImportedObjectForm for form closing events</summary>
  private void SelectImportedObjectForm_FormClosing([CanBeNull] object sender, [NotNull] FormClosingEventArgs e)
  {
    if (this.DialogResult == DialogResult.OK || !this._deleteImportedObjectIDs.Any<long>())
      return;
    switch (MessageFuncs.Ask(Localization.GetString("LoseChangesDeletedImportedObjects"), MessageBoxButtons.YesNoCancel))
    {
      case DialogResult.Cancel:
        e.Cancel = true;
        break;
      case DialogResult.Yes:
        this.SaveChanges = true;
        break;
      case DialogResult.No:
        this.SaveChanges = false;
        break;
    }
  }

  /// <summary>Инициализировать заготовки локальных команд меню, задать им заголовок, иконку, сочетания горячих клавиш и т.п.</summary>
  /// <param name="contextMenuTemplate"></param>
  public void InitCommandTemplates([NotNull] MenuTemplate contextMenuTemplate)
  {
    if (this._deleteObjectMenuTemplateNode != null)
      return;
    this._deleteObjectMenuTemplateNode = new MenuTemplateNode("DeleteImportedObject", Localization.GetString("RemoveFromList"), Intermech.Client.Services.NamedList.ImageIndex("imgDelete"), 0, 0, Keys.Delete | Keys.Control);
    contextMenuTemplate.Nodes.Add(this._deleteObjectMenuTemplateNode);
  }

  /// <summary>Подчистить за собой заготовки локальных команд меню, задать им заголовок, иконку, сочетания горячих клавиш и т.п.
  /// //! Должен вызываться на выходе из контекста, например в Dispose реализующего интерфейс формы/контрола/etc</summary>
  /// <param name="contextMenuTemplate"></param>
  public void DisposeCommandTemplates([NotNull] MenuTemplate contextMenuTemplate)
  {
    if (this._deleteObjectMenuTemplateNode == null)
      return;
    this.HotKeysManager.UnregisterCommand("DeleteImportedObject");
    contextMenuTemplate.Nodes.Remove(this._deleteObjectMenuTemplateNode);
    this._deleteObjectMenuTemplateNode = (MenuTemplateNode) null;
  }

  /// <summary>Метод вызывается для получения допустимых и подавляемых команд контекстного меню для выделенных элементов навигации одной
  /// категории и типа. Например, если в «Навигаторе» выделены элементы навигации нескольких разных категорий и типов, то
  /// данная команда будет вызываться для каждой из подгрупп этих элементов, сгруппированных по их категориям и типам. Наиболее
  /// применяемый метод данного интерфейса. Позволяет перекрывать команды контекстного меню для элементов навигации определённых
  /// категорий, типов, задавая более высокий приоритет описаниям этих команд. ВНИМАНИЕ! Основное требование к данному методу –
  /// нельзя выполнять обращения к базе данных  для того, чтобы проверить, можно ли отображать команду меню или нет!</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  /// <returns>The merged commands</returns>
  [NotNull]
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  /// <summary>Метод вызывается для получения допустимых и подавляемых команд контекстного меню для всей группы выделенных элементов
  /// навигации. Особенности данного метода: 1. Если команда зарегистрирована на все категории, то метод вызывается один раз и
  /// получает в качестве параметра items все выделенные в «Навигаторе» элементы навигации;
  /// 2. Если команда зарегистрирована на конкретную категорию, то метод будет вызван один раз для всех выделенных элементов
  /// навигации только в том случае, если все они принадлежат одной категории; для всех выделенных элементов навигации только в
  /// том случае, если все они принадлежат указанной категории;
  /// 3. Если команда зарегистрирована на конкретные категорию и тип, то метод будет вызван один раз для всех выделенных
  /// элементов навигации только в том случае, если все они принадлежат указанной категории и типу.</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  /// <returns>The group commands</returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return !this.IsReadOnly ? new CommandsInfo().Add("DeleteImportedObject", new CommandInfo(0, new ClickEventHandler(this.DeleteSelectedItemsHandler))) : new CommandsInfo();
  }

  /// <summary>Команда удаления выбранных элементов</summary>
  private void DeleteSelectedItemsHandler(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    this.DeleteSelectedObjects();
  }

  /// <summary>Вызывается после изменения статуса IsReadOnly, рассылает событие ReadOnlyWasChanged</summary>
  protected override void FireReadOnlyWasChanged()
  {
    base.FireReadOnlyWasChanged();
    this.PanelButtons.Visible = !this.IsReadOnly;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._panelButtons = new Panel();
    this._bevelButtons = new Bevel();
    this._buttonDelete = new Button();
    this._objectsView = new ObjectsViewBase();
    this._panelTreeCaption = new Panel();
    this._labelImportedObjectsCaption = new Label();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this._panelButtons.SuspendLayout();
    this._panelTreeCaption.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 382);
    this._pnlDialogButtons.Size = new Size(607, 36);
    this._pnlDialogButtons.TabIndex = 2;
    this._bevelDialogButtons.Location = new Point(0, 380);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(607, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(434, 0);
    this._panelButtons.Controls.Add((Control) this._bevelButtons);
    this._panelButtons.Controls.Add((Control) this._buttonDelete);
    this._panelButtons.Dock = DockStyle.Bottom;
    this._panelButtons.Location = new Point(0, 343);
    this._panelButtons.Name = "_panelButtons";
    this._panelButtons.Size = new Size(607, 37);
    this._panelButtons.TabIndex = 1;
    this._bevelButtons.Dock = DockStyle.Top;
    this._bevelButtons.Location = new Point(0, 0);
    this._bevelButtons.Name = "_bevelButtons";
    this._bevelButtons.Size = new Size(607, 2);
    this._bevelButtons.TabIndex = 1;
    this._bevelButtons.Text = "bevel1";
    this._buttonDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._buttonDelete.Location = new Point(12, 7);
    this._buttonDelete.Name = "_buttonDelete";
    this._buttonDelete.Size = new Size(125, 23);
    this._buttonDelete.TabIndex = 0;
    this._buttonDelete.Text = "Исключить из списка";
    this._buttonDelete.UseVisualStyleBackColor = true;
    this._buttonDelete.Click += new EventHandler(this._buttonDelete_Click);
    this._objectsView.AllowCustomGroupValues = true;
    this._objectsView.Control = (object) this._objectsView;
    this._objectsView.DisableColumnsGrouping = true;
    this._objectsView.DisableFiltration = true;
    this._objectsView.DisableGroupBox = true;
    this._objectsView.DisableKeyDownEvents = false;
    this._objectsView.DisableManualSortingSetup = true;
    this._objectsView.DisableMultiValuesAttrButton = true;
    this._objectsView.DisablePacketsReading = true;
    this._objectsView.DisableParentSelectedItems = true;
    this._objectsView.DisableStatusBar = true;
    this._objectsView.DisableToolBar = true;
    this._objectsView.Dock = DockStyle.Fill;
    this._objectsView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._objectsView.Font = new Font("Tahoma", 8.25f);
    this._objectsView.Location = new Point(0, 28);
    this._objectsView.Name = "_objectsView";
    this._objectsView.Size = new Size(607, 315);
    this._objectsView.TabIndex = 0;
    this._objectsView.DoubleClick += new EventHandler(this._objectsView_DoubleClick);
    this._panelTreeCaption.Controls.Add((Control) this._labelImportedObjectsCaption);
    this._panelTreeCaption.Dock = DockStyle.Top;
    this._panelTreeCaption.Location = new Point(0, 0);
    this._panelTreeCaption.Name = "_panelTreeCaption";
    this._panelTreeCaption.Size = new Size(607, 28);
    this._panelTreeCaption.TabIndex = 6;
    this._labelImportedObjectsCaption.AutoSize = true;
    this._labelImportedObjectsCaption.Location = new Point(3, 8);
    this._labelImportedObjectsCaption.Name = "_labelImportedObjectsCaption";
    this._labelImportedObjectsCaption.Size = new Size(585, 13);
    this._labelImportedObjectsCaption.TabIndex = 1;
    this._labelImportedObjectsCaption.Text = "Выберите ранее импортированный в проект объект, с составов которого необходимо синхронизировать задачи:";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(607, 418);
    this.Controls.Add((Control) this._objectsView);
    this.Controls.Add((Control) this._panelButtons);
    this.Controls.Add((Control) this._panelTreeCaption);
    this.FormBorderStyle = FormBorderStyle.Sizable;
    this.MinimumSize = new Size(623, 324);
    this.Name = nameof (SelectImportedObjectForm);
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Text = "Импортированные в проект объекты";
    this.FormClosing += new FormClosingEventHandler(this.SelectImportedObjectForm_FormClosing);
    this.Controls.SetChildIndex((Control) this._panelTreeCaption, 0);
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._panelButtons, 0);
    this.Controls.SetChildIndex((Control) this._objectsView, 0);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this._panelButtons.ResumeLayout(false);
    this._panelTreeCaption.ResumeLayout(false);
    this._panelTreeCaption.PerformLayout();
    this.ResumeLayout(false);
  }
}
