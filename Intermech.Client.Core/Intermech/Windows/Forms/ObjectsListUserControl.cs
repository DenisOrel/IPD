
// Type: Intermech.Windows.Forms.ObjectsListUserControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Common;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.ContextMenu.Extensions;
using Intermech.Navigator.Controls;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Windows.Forms;

/// <summary>Фрейм для отображения списка объектов с возможностью прямого или отложенного редактирования</summary>
public class ObjectsListUserControl : 
  IpsBaseUserControl,
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
  ILocalCommandsProvider,
  ICommandsFilter
{
  public const string AddCommandName = "AddObject";
  public const string DeleteCommandName = "DeleteObject";
  public const string MarkAllCommandName = "MarkAll";
  public const string UnmarkAllCommandName = "UnmarkAll";
  public const string ResetCommandName = "ResetObjectsList";
  /// <summary>Список идентификаторов типов объектов, которые могут быть добавлены в список</summary>
  [CanBeNull]
  private List<int> _objectTypes;
  /// <summary>Хэш идентификаторов типов объектов, которые могут быть добавлены в список (включая подтипы)</summary>
  [CanBeNull]
  private HashSet<int> _possibleObjectTypesRecursive;
  /// <summary>Список идентификаторов версий объектов в списке</summary>
  [CanBeNull]
  private List<long> _objectVerIDs;
  /// <summary>Хэш идентификаторов версий объектов, удаление которых заблокировано</summary>
  [CanBeNull]
  private HashSet<long> _protectedObjectVerIDs;
  /// <summary>Список идентификаторов версий объектов, добавленных в список</summary>
  [NotNull]
  private readonly List<long> _addedObjectVerIDs = new List<long>();
  /// <summary>Список идентификаторов версий объектов, удалённых из списка</summary>
  [NotNull]
  private readonly List<long> _removedObjectVerIDs = new List<long>();
  /// <summary>Дескриптор отображения списка объектов в гриде</summary>
  [CanBeNull]
  private ObjectsSelectionDescriptor _objectsListDescriptor;
  /// <summary>Провайдер команд для контекстного меню навигатора</summary>
  [NotNull]
  [ItemNotNull]
  private readonly Lazy<ICommandsProvider> _lazyCommandsProvider;
  /// <summary>Диспетчер событий</summary>
  [NotNull]
  private IIODispatcher _ioDispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>Заготовка элемента меню для команды "Добавить"</summary>
  [CanBeNull]
  private MenuTemplateNode _addOfficeSupervisorMenuTemplateNode;
  /// <summary>Заготовка элемента меню для команды "Удалить"</summary>
  [CanBeNull]
  private MenuTemplateNode _deleteOfficeSupervisorMenuTemplateNode;
  /// <summary>Заготовка элемента меню для команды "Отметить все"</summary>
  [CanBeNull]
  private MenuTemplateNode _markAllOfficeSupervisorMenuTemplateNode;
  /// <summary>Заготовка элемента меню для команды "Снять все отметки"</summary>
  [CanBeNull]
  private MenuTemplateNode _unmarkAllOfficeSupervisorMenuTemplateNode;
  /// <summary>Заготовка элемента меню для команды "Отменить правки"</summary>
  [CanBeNull]
  private MenuTemplateNode _resetOfficeSupervisorMenuTemplateNode;
  [NotNull]
  private static readonly HashSet<string> _menuEnabledCommands = new HashSet<string>((IEnumerable<string>) new string[7]
  {
    "AddObject",
    "DeleteObject",
    "ResetObjectsList",
    "MarkAll",
    "UnmarkAll",
    "ResetColumns",
    "SetupColumns"
  });
  /// <summary>Координаты, по которым было открыто контекстное меню</summary>
  private Point _contextMenuPoint;
  private int _lockListChangedCounter;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel _panelTreeCaption;
  protected Label _labelDescription;
  protected Panel _panelButtons;
  protected Button _btnReset;
  protected Button _btnAdd;
  protected Button _btnDelete;
  protected ObjectsViewBase _objectsView;

  [NotNull]
  [DebuggerHidden]
  protected Panel PanelTreeCaption
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelTreeCaption.CheckInitializedIn<Panel>("InitializeComponent");
    }
  }

  [NotNull]
  [DebuggerHidden]
  protected Label LabelDescription
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelDescription.CheckInitializedIn<Label>("InitializeComponent");
    }
  }

  [NotNull]
  [DebuggerHidden]
  protected Panel PanelButtons
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelButtons.CheckInitializedIn<Panel>("InitializeComponent");
    }
  }

  [NotNull]
  [DebuggerHidden]
  protected Button BtnReset
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnReset.CheckInitializedIn<Button>("InitializeComponent");
    }
  }

  [NotNull]
  [DebuggerHidden]
  protected Button BtnAdd
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnAdd.CheckInitializedIn<Button>("InitializeComponent");
    }
  }

  [NotNull]
  [DebuggerHidden]
  protected Button BtnDelete
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnDelete.CheckInitializedIn<Button>("InitializeComponent");
    }
  }

  [NotNull]
  [DebuggerHidden]
  protected ObjectsViewBase ObjectsView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._objectsView.CheckInitializedIn<ObjectsViewBase>("InitializeComponent");
    }
  }

  public ObjectsListUserControl()
  {
    this.InitializeComponent();
    this._lazyCommandsProvider = new Lazy<ICommandsProvider>(new Func<ICommandsProvider>(this.CommandsProviderFactory), true);
  }

  /// <summary>Признак того, что контрол инициализирован</summary>
  public bool WasInit { get; private set; }

  /// <summary>Инициализация фрейма</summary>
  /// <param name="ownerServices">сервисы контекста</param>
  /// <param name="objectTypes">Перечисление идентификаторов типов объектов, которые могут быть добавлены в список</param>
  /// <param name="objectTypesCaption">В том случае, если в _objectTypes несколько типов объектов, - заголовок ноды в диалоге добавления
  /// объектов, объединяющая эти типы объектов</param>
  /// <param name="objectVerIDs">Перечисление идентификаторов версий объектов в списке</param>
  /// <param name="protectedObjectVerIDs">Перечисление идентификаторов версий объектов, удаление которых заблокировано</param>
  public virtual void Init(
    [CanBeNull] System.IServiceProvider ownerServices,
    [NotNull, ItemNotEmpty] IReadOnlyCollection<int> objectTypes,
    [CanBeNull] string objectTypesCaption,
    [NotNull, ItemNotEmpty] IReadOnlyCollection<long> objectVerIDs,
    [CanBeNull] IReadOnlyCollection<long> protectedObjectVerIDs = null)
  {
    this.LockListChanged();
    try
    {
      protectedObjectVerIDs = (IReadOnlyCollection<long>) ((object) protectedObjectVerIDs ?? (object) Array.Empty<long>());
      this._objectTypes = objectTypes.Distinct<int>().ToList<int>(objectTypes.Count);
      this.ObjectType = this._objectTypes.Count == 1 ? this._objectTypes[0] : MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<int>) this._objectTypes);
      this._possibleObjectTypesRecursive = new HashSet<int>(this._objectTypes.SelectMany<int, int>((Func<int, IEnumerable<int>>) (objectTypeId => MetaDataHelper.GetObjectTypeChildrenID(objectTypeId).Append<int>(objectTypeId).Distinct<int>())).Distinct<int>());
      if (string.IsNullOrWhiteSpace(objectTypesCaption))
        objectTypesCaption = this.ObjectType != -1 ? MetaDataHelper.GetObjectTypeName(this.ObjectType) : Resources.GetString("Client.Core_1113");
      this.ObjectTypesCaption = objectTypesCaption;
      if (protectedObjectVerIDs.Count > 0 && protectedObjectVerIDs.Any<long>((Func<long, bool>) (objVerId => !objectVerIDs.Contains<long>(objVerId))))
        objectVerIDs = (IReadOnlyCollection<long>) new List<long>((IEnumerable<long>) this.FilterObjectsCanBeAdded((IReadOnlyCollection<long>) protectedObjectVerIDs.Concat<long>((IEnumerable<long>) objectVerIDs).Distinct<long>().ToArray<long>(protectedObjectVerIDs.Count + objectVerIDs.Count)));
      this._objectVerIDs = objectVerIDs.Abs().Distinct<long>().ToList<long>(objectVerIDs.Count);
      this._protectedObjectVerIDs = new HashSet<long>(protectedObjectVerIDs.Abs().Distinct<long>());
      if (ownerServices != null)
        this.Services = ownerServices;
      this.AddService<IIODispatcher>(this._ioDispatcher);
      this.ObjectsView.OnGetMenuServiceContainer = new ChildrenView.GetMenuServiceContainerDelegate(this.ObjectsView_OnGetMenuServiceContainer);
      this._ioDispatcher.RegisterDestination((IIODestination) this);
      this.ServiceContainer.StackLocalContextCommandsFilter((ICommandsFilter) this);
      this._objectsListDescriptor = new ObjectsSelectionDescriptor(this.ObjectType, string.Empty, objectVerIDs);
      this.PanelTreeCaption.Visible = this.Description != null;
      this.ObjectsView.ShowCustomContextMenu += new EventHandler<ContextMenuEventArgs>(this.ObjectsView_ShowCustomContextMenu);
      ViewStateFlags flags = ViewStateFlags.InDialog | ViewStateFlags.ReadOnly | ViewStateFlags.NoPluginsViews | ViewStateFlags.InSelectionWindow | ViewStateFlags.DisableGlobalCommandProviders;
      IViewState service = this.ObjectsView.Services.GetService<IViewState>(false);
      if (service != null)
        flags &= service.ViewState;
      this.ObjectsView.Services.AddService<IViewState>((IViewState) new ViewStateService(flags));
      this.ObjectsView.Services.AddService<NavigatorViewOptions>(new NavigatorViewOptions(NavigatorViewContext.TreeViews));
    }
    finally
    {
      this.UnlockListChanged();
      this.FireListChangedInternal(ObjectsListUserControl.ChangeType.Init);
    }
    this.WasInit = true;
  }

  /// <summary>Инициализация фрейма</summary>
  /// <param name="ownerServices">сервисы контекста</param>
  /// <param name="objectTypes">Перечисление идентификаторов типов объектов, которые могут быть добавлены в список</param>
  /// <param name="objectVerIDs">Перечисление идентификаторов версий объектов в списке</param>
  /// <param name="protectedObjectVerIDs">Перечисление идентификаторов версий объектов, удаление которых заблокировано</param>
  public void Init(
    [CanBeNull] System.IServiceProvider ownerServices,
    [NotNull, ItemNotEmpty] IReadOnlyCollection<int> objectTypes,
    [NotNull, ItemNotEmpty] IReadOnlyCollection<long> objectVerIDs,
    [CanBeNull] IReadOnlyCollection<long> protectedObjectVerIDs = null)
  {
    this.Init(ownerServices, objectTypes, (string) null, objectVerIDs, protectedObjectVerIDs);
  }

  /// <summary>Инициализация фрейма</summary>
  /// <param name="ownerServices">сервисы контекста</param>
  /// <param name="objectType">Идентификатор типа объектов, который может быть добавлен в список</param>
  /// <param name="objectTypesCaption">В том случае, если в _objectTypes несколько типов объектов, - заголовок ноды в диалоге добавления
  /// объектов, объединяющая эти типы объектов</param>
  /// <param name="objectVerIDs">Перечисление идентификаторов версий объектов в списке</param>
  /// <param name="protectedObjectVerIDs">Перечисление идентификаторов версий объектов, удаление которых заблокировано</param>
  public void Init(
    [CanBeNull] System.IServiceProvider ownerServices,
    [NotEmpty] int objectType,
    [NotNull] IReadOnlyCollection<long> objectVerIDs,
    [CanBeNull] IReadOnlyCollection<long> protectedObjectVerIDs = null)
  {
    this.Init(ownerServices, (IReadOnlyCollection<int>) new int[1]
    {
      objectType
    }, (string) null, objectVerIDs, protectedObjectVerIDs);
  }

  /// <summary>Вызов события AfterShown - после первого отображения контрола (первого WM_PAINT)</summary>
  protected override void FireFirstPaint()
  {
    if (!this.InDesignMode)
    {
      Intermech.Diagnostics.Check.ObjectState(this.WasInit, "Method Init was not called!");
      this.LockUpdateCommands(true);
      try
      {
        this.ObjectsView.Initialize((IDescriptor) this._objectsListDescriptor, this.Services);
        this.ObjectsView.Activate((IView) null);
        this.ObjectsView.OpenEmbeddedViews(150);
        this.ObjectsView.ToggleSplitterState();
      }
      finally
      {
        this.UnlockUpdateCommands();
      }
    }
    base.FireFirstPaint();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._ioDispatcher = (IIODispatcher) null;
      this.ServiceContainer.DisposeLocalContextCommandsTemplates((ILocalCommandsProvider) this);
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Имя контектса по-умолчанию</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override string DefaultContextName
  {
    [DebuggerHidden] get => "Objects list";
  }

  /// <summary>Список идентификаторов типов объектов, которые могут быть добавлены в список</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<int> ObjectTypes
  {
    get => (IReadOnlyCollection<int>) (this._objectTypes ?? throw new InvalidOperationException());
  }

  /// <summary>Тип объектов, объединяющий ObjectTypes</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ObjectType { get; private set; }

  /// <summary>В том случае, если в _objectTypes несколько типов объектов, - заголовок ноды в диалоге добавления объектов, объединяющая эти типы объектов</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string ObjectTypesCaption { get; private set; }

  /// <summary>Список идентификаторов версий объектов в списке</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<long> ObjectVerIDs
  {
    [DebuggerHidden] get
    {
      return (IReadOnlyCollection<long>) (this._objectVerIDs ?? throw new InvalidOperationException());
    }
    set
    {
      Intermech.Diagnostics.Check.ObjectState(this.WasInit, "Контрол должен быть инициализирован");
      this.LockListChanged();
      try
      {
        if (this._objectVerIDs != null)
        {
          this._objectVerIDs.Clear();
          if (value != null)
            this._objectVerIDs.AddRange((IEnumerable<long>) this.FilterObjectsCanBeAdded(value));
          if (this._objectVerIDs != null)
          {
            List<long> objectVerIds = this._objectVerIDs;
            objectVerIds.SafeAddRange<long>((IEnumerable<long>) (this._protectedObjectVerIDs ?? throw new InvalidOperationException()));
          }
        }
        this.UpdateObjectsList();
        this.MarkListAsActual();
      }
      finally
      {
        this.UnlockListChanged();
        this.FireListChangedInternal(ObjectsListUserControl.ChangeType.Init);
      }
    }
  }

  /// <summary>Хэш идентификаторов версий объектов, удаление которых заблокировано</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<long> ProtectedObjectVerIDs
  {
    [DebuggerHidden] get
    {
      return (IReadOnlyCollection<long>) (this._protectedObjectVerIDs ?? throw new InvalidOperationException());
    }
  }

  /// <summary>Список идентификаторов версий объектов, добавленных в список</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<long> AddedObjectVerIDs
  {
    [DebuggerHidden] get => (IReadOnlyCollection<long>) this._addedObjectVerIDs;
  }

  /// <summary>Список идентификаторов версий объектов, удалённых из списка</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<long> RemovedObjectVerIDs
  {
    [DebuggerHidden] get => (IReadOnlyCollection<long>) this._removedObjectVerIDs;
  }

  /// <summary>Признак того, что список изменён</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Changed
  {
    [DebuggerHidden] get
    {
      return this._addedObjectVerIDs.Any<long>() || this._removedObjectVerIDs.Any<long>();
    }
  }

  /// <summary>Интерфейс идентификатора ноды сфокусированной ноды</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INodeID FocusedNodeId => this.ObjectsView.FocusedNodeID;

  /// <summary>Идентификатор сфокусированной версии объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectVersionId
  {
    get
    {
      INodeID focusedNodeId = this.FocusedNodeId;
      return focusedNodeId == null ? new long?() : new long?(focusedNodeId.GetObjVerID(false));
    }
  }

  /// <summary>Идентификатор типа сфокусированного объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? FocusedObjectTypeId
  {
    get
    {
      INodeID focusedNodeId = this.FocusedNodeId;
      return focusedNodeId == null ? new int?() : new int?(focusedNodeId.GetObjTypeID(false));
    }
  }

  /// <summary>Идентификаторы выбранных версий объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<long> SelectedObjectVersionIDs
  {
    [DebuggerHidden] get
    {
      return (IReadOnlyCollection<long>) ObjectsListUserControl.SelectedItemsAsNodeIdList(this.ObjectsView.SelectedItems).Select<INodeID, long>((Func<INodeID, long>) (iNodeId => Math.Abs(iNodeId.GetObjVerID()))).ToList<long>(this.ObjectsView.SelectedItems.Count);
    }
  }

  /// <summary>Уникальные идентификаторы типов выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<int> SelectedObjectTypeIDs
  {
    [DebuggerHidden] get
    {
      return (IReadOnlyCollection<int>) ObjectsListUserControl.SelectedItemsAsNodeIdList(this.ObjectsView.SelectedItems).Select<INodeID, int>((Func<INodeID, int>) (iNodeId => iNodeId.GetObjTypeID())).Distinct<int>().ToList<int>(this.ObjectsView.SelectedItems.Count);
    }
  }

  /// <summary>Преобразует интерфейс выбранных элементов в список INodeID</summary>
  [NotNull]
  [ItemNotNull]
  public static IReadOnlyCollection<INodeID> SelectedItemsAsNodeIdList([NotNull] ISelectedItems selectedItems)
  {
    List<INodeID> nodeIdList = new List<INodeID>(selectedItems.Count);
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      INodeID itemId = selectedItems.GetItemID(index);
      nodeIdList.Add(itemId);
    }
    return (IReadOnlyCollection<INodeID>) nodeIdList;
  }

  /// <summary>Описание списка объектов</summary>
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_314")]
  public string Description
  {
    get
    {
      return !string.IsNullOrEmpty(this.LabelDescription.Text) ? this.LabelDescription.Text : (string) null;
    }
    set
    {
      string description = this.Description;
      if (!(value != description))
        return;
      this.LabelDescription.Text = value ?? string.Empty;
      this.PanelTreeCaption.Visible = this.Description != null;
    }
  }

  /// <summary>Заголовок окна выбора объектов для добавления в список</summary>
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_315")]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_316")]
  public string CustomAddDialogCaption { get; set; }

  /// <summary>Описание окна выбора объектов для добавления в список</summary>
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_317")]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_316")]
  public string CustomAddDialogDescription { get; set; }

  /// <summary>Заголовок окна подтверждения удаления выбранных объектов</summary>
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomDescription("DeleteConfirmationCaption")]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_316")]
  public string CustomDeleteConfirmationCaption { get; set; }

  /// <summary>Вопрос подтверждения удаления одного выбранного объекта</summary>
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomDescription("DeleteConfirmationQuestionOneObject")]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_316")]
  public string CustomDeleteConfirmationQuestionOneObject { get; set; }

  /// <summary>Вопрос подтверждения удаления нескольких выбранных объектов (параметром {0} идёт кол-во)</summary>
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomDescription("DeleteConfirmationQuestionSomeObjects")]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_316")]
  public string CustomDeleteConfirmationQuestionSomeObjects { get; set; }

  /// <summary>Заголовок окна подтверждения сброса правок</summary>
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomDescription("ResetConfirmationCaption")]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_316")]
  public string CustomResetConfirmationCaption { get; set; }

  /// <summary>Вопрос подтверждения сброса правок</summary>
  [CanBeNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomDescription("ResetConfirmationQuestion")]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_316")]
  public string CustomResetConfirmationQuestion { get; set; }

  /// <summary>Провайдер команд для контекстного меню навигатора</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private ICommandsProvider NavigatorCommandsProvider
  {
    [DebuggerHidden] get => this._lazyCommandsProvider.Value;
  }

  /// <summary>внешнюю функцию заполнения провайдера сервисов контекстного меню грида</summary>
  [CanBeNull]
  private IServiceContainer ObjectsView_OnGetMenuServiceContainer(
    [CanBeNull] object sender,
    [CanBeNull] IServiceContainer originalMenuServiceContainer)
  {
    Intermech.Diagnostics.Check.Argument(sender == this.ObjectsView, "sender != this.ObjectsView");
    if (originalMenuServiceContainer is System.ComponentModel.Design.ServiceContainer localContext)
    {
      localContext.StackLocalContextCommandsProvider((ILocalCommandsProvider) this);
      localContext.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.ReadOnly | ViewStateFlags.InSelectionWindow | ViewStateFlags.DisableGlobalCommandProviders));
    }
    return originalMenuServiceContainer;
  }

  /// <summary>Получение провайдера команд для контекстного меню навигатора</summary>
  [NotNull]
  private ICommandsProvider CommandsProviderFactory()
  {
    return ((IIOSource) this.ObjectsView).Services.GetService<ICommandsProvider>("ICommandsProvider not found in menu services");
  }

  /// <summary>Фильтрация списка объектов, фильтрацию проходят лишь те, которые могут быть добавлены в список</summary>
  [NotNull]
  protected virtual IReadOnlyCollection<long> FilterObjectsCanBeAdded(
    [NotNull, ItemNotEmpty] IReadOnlyCollection<long> objectVerIDs,
    [CanBeNull] IUserSession session = null)
  {
    if (objectVerIDs.Count == 0)
      return (IReadOnlyCollection<long>) Array.Empty<long>();
    using (ISessionKeeper sessionKeeper = UserSessionKeeper.Get(session))
    {
      List<long> collection = new List<long>(objectVerIDs.Count);
      foreach (long objectVerId in (IEnumerable<long>) objectVerIDs)
      {
        if (this.ObjectCanBeAdded(objectVerId, sessionKeeper.Session))
          collection.SafeAdd<long>(Math.Abs(objectVerId));
      }
      return (IReadOnlyCollection<long>) collection;
    }
  }

  /// <summary>Проверка, что объект может быть добавлен в список</summary>
  protected virtual bool ObjectCanBeAdded([CanBeEmpty] long objectVerId, [CanBeNull] IUserSession session = null)
  {
    if (Intermech.Check.ObjectIdIsEmpty(objectVerId))
      return false;
    QuickObjectInfo quickObjectInfo = session.SessionGuarantee<QuickObjectInfo>((Intermech.Interfaces.UserSessionExtensions.NotNullSessionFunc<QuickObjectInfo>) (sk => sk.GetObjectInfo(Math.Abs(objectVerId))));
    return this._objectVerIDs != null && this._possibleObjectTypesRecursive != null && !quickObjectInfo.Empty && this._possibleObjectTypesRecursive.Contains(quickObjectInfo.ObjectTypeID) && !this._objectVerIDs.Contains(objectVerId);
  }

  /// <summary>Обновить статус доступности команд</summary>
  /// <returns>true если обновление прошло успешно, если обновление команд заблокировано, то false</returns>
  protected override bool UpdateCommands()
  {
    if (!base.UpdateCommands())
      return false;
    this.BtnAdd.Enabled = !this.IsReadOnly;
    this.BtnDelete.Enabled = !this.IsReadOnly && (this.InDesignMode || this.SelectedObjectVersionIDs.Any<long>((Func<long, bool>) (objVer =>
    {
      HashSet<long> protectedObjectVerIds = this._protectedObjectVerIDs;
      // ISSUE: explicit non-virtual call
      return protectedObjectVerIds != null && !__nonvirtual (protectedObjectVerIds.Contains(Math.Abs(objVer)));
    })));
    this.BtnReset.Enabled = !this.IsReadOnly && (this.InDesignMode || this.Changed);
    this.PanelButtons.Visible = !this.ForceIsReadOnly;
    return true;
  }

  /// <summary>Список поддерживаемых обработчиком событий</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyUp;
    set
    {
    }
  }

  /// <summary>Выполнить обработку события</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если обработка выполнена успешно, false, если событие не обработано</returns>
  public bool ProcessEvent([CanBeNull] IIOEvent Event)
  {
    if (Event == null || Event.EventType != IOEventType.evKeyUp)
      return false;
    KeyEventArgs eventData = (KeyEventArgs) Event.EventData;
    switch (eventData.KeyCode)
    {
      case Keys.Delete:
        this.DeleteSelectedObjects();
        return true;
      case Keys.Add:
      case Keys.Oemplus:
        this.MarkAll();
        return true;
      case Keys.Subtract:
      case Keys.OemMinus:
        this.UnmarkAll();
        return true;
      default:
        if (eventData.Modifiers == Keys.Control)
        {
          switch (eventData.KeyCode)
          {
            case Keys.A:
              this.AskUserToSelectObjectsAndAddThemToList();
              return true;
            case Keys.D:
              this.DeleteSelectedObjects();
              return true;
            case Keys.R:
            case Keys.F5:
              this.ResetChanges();
              return true;
          }
        }
        return false;
    }
  }

  /// <summary>Инициализировать заготовки локальных команд меню, задать им заголовок, иконку, сочетания горячих клавиш и т.п.</summary>
  /// <param name="contextMenuTemplate"></param>
  public void InitCommandTemplates([NotNull] MenuTemplate contextMenuTemplate)
  {
    if (this._addOfficeSupervisorMenuTemplateNode == null)
      contextMenuTemplate.Nodes.Add(LazyInitializer.EnsureInitialized<MenuTemplateNode>(ref this._addOfficeSupervisorMenuTemplateNode, (Func<MenuTemplateNode>) (() => new MenuTemplateNode("AddObject", this.BtnAdd.Text, this.NamedImageList.ImageIndex("imgAdd"), 0, 0, Keys.A | Keys.Control))));
    if (this._deleteOfficeSupervisorMenuTemplateNode == null)
      contextMenuTemplate.Nodes.Add(LazyInitializer.EnsureInitialized<MenuTemplateNode>(ref this._deleteOfficeSupervisorMenuTemplateNode, (Func<MenuTemplateNode>) (() => new MenuTemplateNode("DeleteObject", this.BtnDelete.Text, this.NamedImageList.ImageIndex("imgDelete"), 0, 1, Keys.D | Keys.Control))));
    if (this._markAllOfficeSupervisorMenuTemplateNode == null)
      contextMenuTemplate.Nodes.Add(LazyInitializer.EnsureInitialized<MenuTemplateNode>(ref this._markAllOfficeSupervisorMenuTemplateNode, (Func<MenuTemplateNode>) (() => new MenuTemplateNode("MarkAll", Resources.GetString("MarkAll"), -1, 1, 0, Keys.None))));
    if (this._unmarkAllOfficeSupervisorMenuTemplateNode == null)
      contextMenuTemplate.Nodes.Add(LazyInitializer.EnsureInitialized<MenuTemplateNode>(ref this._unmarkAllOfficeSupervisorMenuTemplateNode, (Func<MenuTemplateNode>) (() => new MenuTemplateNode("UnmarkAll", Resources.GetString("UnmarkAll"), -1, 1, 1, Keys.None))));
    if (this._resetOfficeSupervisorMenuTemplateNode != null)
      return;
    contextMenuTemplate.Nodes.Add(LazyInitializer.EnsureInitialized<MenuTemplateNode>(ref this._resetOfficeSupervisorMenuTemplateNode, (Func<MenuTemplateNode>) (() => new MenuTemplateNode("ResetObjectsList", this.BtnReset.Text, this.NamedImageList.ImageIndex("imgUndo"), 2, 0, Keys.R | Keys.Control))));
  }

  /// <summary>Подчистить за собой заготовки локальных команд меню, задать им заголовок, иконку, сочетания горячих клавиш и т.п. //! Должен
  /// вызываться на выходе из контекста, например в Dispose реализующего интерфейс формы/контрола/etc</summary>
  /// <param name="contextMenuTemplate"></param>
  public void DisposeCommandTemplates([NotNull] MenuTemplate contextMenuTemplate)
  {
    if (this._addOfficeSupervisorMenuTemplateNode != null)
    {
      this.HotKeysManager.UnregisterCommand("AddObject");
      contextMenuTemplate.Nodes.Remove(this._addOfficeSupervisorMenuTemplateNode);
      this._addOfficeSupervisorMenuTemplateNode = (MenuTemplateNode) null;
    }
    if (this._deleteOfficeSupervisorMenuTemplateNode != null)
    {
      this.HotKeysManager.UnregisterCommand("DeleteObject");
      contextMenuTemplate.Nodes.Remove(this._deleteOfficeSupervisorMenuTemplateNode);
      this._deleteOfficeSupervisorMenuTemplateNode = (MenuTemplateNode) null;
    }
    if (this._markAllOfficeSupervisorMenuTemplateNode != null)
    {
      this.HotKeysManager.UnregisterCommand("MarkAll");
      contextMenuTemplate.Nodes.Remove(this._markAllOfficeSupervisorMenuTemplateNode);
      this._markAllOfficeSupervisorMenuTemplateNode = (MenuTemplateNode) null;
    }
    if (this._unmarkAllOfficeSupervisorMenuTemplateNode != null)
    {
      this.HotKeysManager.UnregisterCommand("UnmarkAll");
      contextMenuTemplate.Nodes.Remove(this._unmarkAllOfficeSupervisorMenuTemplateNode);
      this._unmarkAllOfficeSupervisorMenuTemplateNode = (MenuTemplateNode) null;
    }
    if (this._resetOfficeSupervisorMenuTemplateNode == null)
      return;
    this.HotKeysManager.UnregisterCommand("ResetObjectsList");
    contextMenuTemplate.Nodes.Remove(this._resetOfficeSupervisorMenuTemplateNode);
    this._resetOfficeSupervisorMenuTemplateNode = (MenuTemplateNode) null;
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
  public CommandsInfo GetMergedCommands([CanBeNull] ISelectedItems items, [CanBeNull] System.IServiceProvider viewServices)
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
  [NotNull]
  public CommandsInfo GetGroupCommands([CanBeNull] ISelectedItems items, [CanBeNull] System.IServiceProvider viewServices)
  {
    return !this.IsReadOnly ? new CommandsInfo().Add("AddObject", new CommandInfo(0, new ClickEventHandler(this.AddSelectedItemsHandler))).Add("DeleteObject", new CommandInfo(0, new ClickEventHandler(this.DeleteSelectedItemsHandler))).Add("MarkAll", new CommandInfo(0, new ClickEventHandler(this.MarkAllHandler))).Add("UnmarkAll", new CommandInfo(0, new ClickEventHandler(this.UnmarkAllHandler))).Add("ResetObjectsList", new CommandInfo(0, new ClickEventHandler(this.ResetSelectedItemsHandler))) : new CommandsInfo();
  }

  /// <summary>Команда UI "Добавить"</summary>
  private void AddSelectedItemsHandler(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    this.AskUserToSelectObjectsAndAddThemToList();
  }

  /// <summary>Команда UI "Удалить"</summary>
  private void DeleteSelectedItemsHandler(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    this.DeleteSelectedObjects();
  }

  /// <summary>Команда UI "Отметить все"</summary>
  private void MarkAllHandler(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    this.MarkAll();
  }

  /// <summary>Команда UI "Сбросить все отметки"</summary>
  private void UnmarkAllHandler(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    this.UnmarkAll();
  }

  /// <summary>Команда UI "Отменить изменения"</summary>
  private void ResetSelectedItemsHandler(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    this.ResetChanges();
  }

  /// <summary>Осуществляет фильтрацию видимости команд в данном контексте. Для скрытия команды надо установить параметр commandIsVisible в
  /// false. При этом надо учитывать, что фильтры в контексте могут быть вложенными, соотв. более "глубокий" фильтр (например
  /// контрол вложенный в данный контрол) может отфильтровать команду ранее. Если команда всё же нужна - можно установить
  /// видимость принудительно в true.</summary>
  /// <param name="items">Список выделенных сущностей</param>
  /// <param name="commandWithVisibleStatuses">Перечисление команд и их статусов</param>
  public void FilterCommands(
    [NotNull] ISelectedItems items,
    [NotNull, ItemNotNull] IEnumerable<CommandAndVisibleStatus> commandWithVisibleStatuses)
  {
    foreach (CommandAndVisibleStatus withVisibleStatuse in commandWithVisibleStatuses)
      withVisibleStatuse.IsVisible = ObjectsListUserControl._menuEnabledCommands.Contains(withVisibleStatuse.Name) && withVisibleStatuse.IsVisible && this.IsMenuItemVisible(items, withVisibleStatuse.Name);
  }

  /// <summary>Проверка видимости команды в выпадающем меню списка объектов</summary>
  private bool IsMenuItemVisible([NotNull] ISelectedItems items, [NotNull] string commandName)
  {
    switch (commandName)
    {
      case "DeleteObject":
        return this.BtnDelete.Enabled && this.ObjectsView.GetNodeAtCursor(this._contextMenuPoint) != null;
      case "ResetObjectsList":
        return this.BtnReset.Enabled;
      case "MarkAll":
        return this._objectVerIDs != null && this._objectVerIDs.Count > 1 && items.Count < this._objectVerIDs.Count;
      case "UnmarkAll":
        return this._objectVerIDs != null && this._objectVerIDs.Count > 1 && items.Count > 1;
      default:
        return true;
    }
  }

  /// <summary>Кнопка "Добавить"</summary>
  private void BtnAdd_Click([NotNull] object sender, [NotNull] EventArgs e)
  {
    this.AskUserToSelectObjectsAndAddThemToList();
  }

  /// <summary>Кнопка "Удалить"</summary>
  private void BtnDelete_Click([NotNull] object sender, [NotNull] EventArgs e)
  {
    this.DeleteSelectedObjects();
  }

  /// <summary>Кнопка "Сбросить"</summary>
  private void BtnReset_Click([NotNull] object sender, [NotNull] EventArgs e)
  {
    this.ResetChanges();
  }

  /// <summary>Выбранные элементы в гриде были изменены</summary>
  private void ObjectsView_SelectedItemsChanged([NotNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateCommands();
  }

  /// <summary>Обработчик события, возникающего когда грид может показать пользовательское контекстное меню</summary>
  private void ObjectsView_ShowCustomContextMenu([NotNull] object sender, [NotNull] ContextMenuEventArgs e)
  {
    this._contextMenuPoint = e.Location;
  }

  /// <summary>Команда "Добавить"</summary>
  public virtual void AskUserToSelectObjectsAndAddThemToList()
  {
    this.UpdateCommands();
    if (!this.BtnAdd.Enabled)
      return;
    IReadOnlyList<IDBObjectID> source = SelectDialog.Objects((IReadOnlyCollection<int>) this._objectTypes, !string.IsNullOrWhiteSpace(this.CustomAddDialogCaption) ? this.CustomAddDialogCaption : Resources.GetString("Client.Core_1680"), !string.IsNullOrWhiteSpace(this.CustomAddDialogDescription) ? this.CustomAddDialogDescription : (string) null, this.ObjectTypesCaption, operationName: this.GetFullContextName() + "/AddCommandName", disableGlobalContextMenuCommands: true);
    if (source == null)
      return;
    this.AddObjectsInternal((IReadOnlyCollection<long>) source.Select<IDBObjectID, long>((Func<IDBObjectID, long>) (dbObjectId => Math.Abs(dbObjectId.Value))).Distinct<long>().ToList<long>(source.Count));
  }

  /// <summary>API для добавления в список перечисления идентификаторов версий объектов, помечает их как добавленные (будут
  /// присутствовать в списке AddedObjectVerIDs)
  /// При этом производятся проверки что объект фактически существует в БД, на его тип (должны быть в ObjectTypes или
  /// дочерних), а так что объекты присутствуют уже в списке</summary>
  /// <param name="objectVerIDs">Перечисление версии идентификаторов объектов, которые должны быть добавлены</param>
  /// <param name="session">(Optional) Сессия</param>
  /// <returns>Массив идентификаторов фактически добавленных, прошедших все проверки, объектов</returns>
  [NotNull]
  public virtual IReadOnlyCollection<long> AddObjects(
    [NotNull] IReadOnlyCollection<long> objectVerIDs,
    [CanBeNull] IUserSession session = null)
  {
    if (objectVerIDs.Count == 0)
      return (IReadOnlyCollection<long>) Array.Empty<long>();
    IReadOnlyList<long> list = (IReadOnlyList<long>) this._addedObjectVerIDs.ToList<long>(this._addedObjectVerIDs.Count);
    this.AddObjectsInternal((IReadOnlyCollection<long>) this.FilterObjectsCanBeAdded(objectVerIDs, session).ToArray<long>());
    return (IReadOnlyCollection<long>) this._addedObjectVerIDs.Except<long>((IEnumerable<long>) list).ToList<long>(this._addedObjectVerIDs.Count);
  }

  /// <summary>API для добавления в список идентификатора версии объекта, помечает его как добавленный (будет присутствовать в списке AddedObjectVerIDs)
  /// При этом производятся проверки что объект фактически существует в БД, на тип объектов (должен быть в ObjectTypes или дочерних), а так что объект не присутствует уже в списке</summary>
  /// <param name="objectVerId">Идентификаторов версии объекта, который должен быть добавлен</param>
  /// <returns>True если был добавлен, иначе (напр. не прошёл проверки) false</returns>
  public virtual bool AddObject(long objectVerId, [CanBeNull] IUserSession session = null)
  {
    if (!session.SessionGuarantee<bool>((Intermech.Interfaces.UserSessionExtensions.NotNullSessionFunc<bool>) (userSession => this.ObjectCanBeAdded(objectVerId, userSession))))
      return false;
    this.AddObjectsInternal((IReadOnlyCollection<long>) new List<long>(1)
    {
      objectVerId
    });
    return true;
  }

  /// <summary>Собственно работа по добавлению в объектов в список</summary>
  private void AddObjectsInternal([NotNull] IReadOnlyCollection<long> objVerIDsToAddCollection)
  {
    List<long> longList = new List<long>((IEnumerable<long>) objVerIDsToAddCollection);
    longList.AddRange((IEnumerable<long>) objVerIDsToAddCollection);
    this.LockSave("AddObject");
    try
    {
      bool changed = this.Changed;
      IReadOnlyCollection<long> list = (IReadOnlyCollection<long>) this._removedObjectVerIDs.Intersect<long>((IEnumerable<long>) longList).ToList<long>(Math.Min(longList.Count, this._removedObjectVerIDs.Count));
      if (list.Count > 0)
        this._removedObjectVerIDs.RemoveRange<long>((IEnumerable<long>) list);
      List<long> collection = longList;
      collection.RemoveRange<long>((IEnumerable<long>) (this._objectVerIDs ?? throw new InvalidOperationException()));
      if (longList.Count <= 0)
        return;
      this._objectVerIDs.AddRange((IEnumerable<long>) longList);
      this._addedObjectVerIDs.AddRange(list.IsNullOrEmpty<long>() ? (IEnumerable<long>) longList : longList.Except<long>((IEnumerable<long>) list));
      this.UpdateObjectsList();
      this.FireListChangedInternal(!this.Changed ? ObjectsListUserControl.ChangeType.BecomeActual : (changed ? ObjectsListUserControl.ChangeType.CommonChange : ObjectsListUserControl.ChangeType.FirstChange));
    }
    finally
    {
      this.UnlockSave("AddObject");
    }
  }

  /// <summary>Команда "Удалить"</summary>
  /// <param name="askUserUserConfirmation">Запрашивать ли у пользователя подтверждение</param>
  public virtual void DeleteSelectedObjects(bool askUserUserConfirmation = true)
  {
    this.UpdateCommands();
    if (!this.BtnDelete.Enabled)
      return;
    IReadOnlyCollection<long> objectVersionIds = this.SelectedObjectVersionIDs;
    IReadOnlyCollection<long> list = (IReadOnlyCollection<long>) objectVersionIds.Where<long>((Func<long, bool>) (objVer =>
    {
      HashSet<long> protectedObjectVerIds = this._protectedObjectVerIDs;
      // ISSUE: explicit non-virtual call
      return protectedObjectVerIds != null && !__nonvirtual (protectedObjectVerIds.Contains(Math.Abs(objVer)));
    })).ToList<long>(objectVersionIds.Count);
    if (list.Count <= 0 || askUserUserConfirmation && MessageBox.Show((IWin32Window) this, list.Count == 1 ? (!string.IsNullOrWhiteSpace(this.CustomDeleteConfirmationQuestionOneObject) ? this.CustomDeleteConfirmationQuestionOneObject : Resources.GetString("DeleteConfirmationQuestionOneObjectDefault")) : string.Format(!string.IsNullOrWhiteSpace(this.CustomDeleteConfirmationQuestionSomeObjects) ? this.CustomDeleteConfirmationQuestionSomeObjects : Resources.GetString("DeleteConfirmationQuestionSomeObjectsDefault"), (object) list.Count), !string.IsNullOrWhiteSpace(this.CustomDeleteConfirmationCaption) ? this.CustomDeleteConfirmationCaption : Resources.GetString("DeleteConfirmationCaptionDefault"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
      return;
    this.DeleteObjectsInternal(list);
  }

  /// <summary>API для удаления из списка перечисления идентификаторов версий объектов, помечает их как удалённые (будут присутствовать в
  /// списке RemovedObjectVerIDs)</summary>
  /// <param name="objectVerIDs">Перечисление идентификаторов версий объектов, которые должны быть удалены</param>
  /// <returns>Массив идентификаторов фактически удалённых объектов</returns>
  [NotNull]
  public virtual IReadOnlyCollection<long> DeleteObjects([NotNull] IReadOnlyCollection<long> objectVerIDs)
  {
    if (objectVerIDs.Count == 0)
      return (IReadOnlyCollection<long>) Array.Empty<long>();
    IReadOnlyCollection<long> list = (IReadOnlyCollection<long>) this._removedObjectVerIDs.ToList<long>(this._removedObjectVerIDs.Count);
    this.DeleteObjectsInternal(objectVerIDs);
    return (IReadOnlyCollection<long>) this._removedObjectVerIDs.Except<long>((IEnumerable<long>) list).ToList<long>(this._removedObjectVerIDs.Count);
  }

  /// <summary>API для удаления из списка объекта, помечает его как удалённый (будет присутствовать в
  /// списке RemovedObjectVerIDs)</summary>
  /// <param name="objectVerId">Идентификатор версии объектов, которые должны быть удалены</param>
  /// <returns>true если объект был удалён, иначе (UnknownObjectId, защищён от удаления, отсутствует в списке) false</returns>
  public virtual bool DeleteObject(long objectVerId)
  {
    if (objectVerId == 0L)
      return false;
    objectVerId = Math.Abs(objectVerId);
    if (this._objectVerIDs != null && this._protectedObjectVerIDs != null && (this._protectedObjectVerIDs.Contains(objectVerId) || !this._objectVerIDs.Contains(objectVerId)))
      return false;
    this.DeleteObjectsInternal((IReadOnlyCollection<long>) new List<long>(1)
    {
      objectVerId
    });
    return true;
  }

  /// <summary>Собственно работа по добавлению в объектов в список</summary>
  private void DeleteObjectsInternal([NotNull] IReadOnlyCollection<long> objVerIDsToDelete)
  {
    this.LockSave("DeleteObject");
    try
    {
      bool changed = this.Changed;
      IReadOnlyCollection<long> list = (IReadOnlyCollection<long>) this._addedObjectVerIDs.Intersect<long>((IEnumerable<long>) objVerIDsToDelete).ToList<long>(Math.Min(objVerIDsToDelete.Count, this._addedObjectVerIDs.Count));
      if (list.Count > 0)
        this._addedObjectVerIDs.RemoveRange<long>((IEnumerable<long>) list);
      (this._objectVerIDs ?? throw new InvalidOperationException()).RemoveRange<long>((IEnumerable<long>) objVerIDsToDelete);
      this._removedObjectVerIDs.AddRange(list.IsNullOrEmpty<long>() ? (IEnumerable<long>) objVerIDsToDelete : objVerIDsToDelete.Except<long>((IEnumerable<long>) list));
      this.UpdateObjectsList();
      if (objVerIDsToDelete.Count <= 0)
        return;
      this.FireListChangedInternal(!this.Changed ? ObjectsListUserControl.ChangeType.BecomeActual : (changed ? ObjectsListUserControl.ChangeType.CommonChange : ObjectsListUserControl.ChangeType.FirstChange));
    }
    finally
    {
      this.UnlockSave("DeleteObject");
    }
  }

  /// <summary>Команда "Выделить всё"</summary>
  public virtual void MarkAll() => this.ExecuteNavigatorMenuItem("MarkGroupAll");

  /// <summary>Команда "Снять все отметки"</summary>
  public virtual void UnmarkAll() => this.ExecuteNavigatorMenuItem("UnMarkGroupAll");

  /// <summary>Вызывать команду контекстного меню навигатора</summary>
  /// <param name="commandName">Наименование команды</param>
  public void ExecuteNavigatorMenuItem([NotNull] string commandName)
  {
    Intermech.Diagnostics.Check.ArgumentNotNullOrWhitespace(commandName, nameof (commandName));
    ISelectedItems selectedItems = this.ObjectsView.SelectedItems;
    Intermech.Diagnostics.Check.NotNull<ISelectedItems>(selectedItems, "selectedItems is null");
    System.IServiceProvider services = ((IIOSource) this.ObjectsView).Services;
    Intermech.Diagnostics.Check.NotNull<System.IServiceProvider>(services, "viewServices is null");
    CommandsInfo groupCommands = this.NavigatorCommandsProvider.GetGroupCommands(selectedItems, services);
    Intermech.Diagnostics.Check.NotNull<CommandsInfo>(groupCommands, "commandsInfo is null");
    CommandInfo info = groupCommands.GetInfo(commandName);
    if (info == null || info.ClickHandler == null)
      return;
    info.ClickHandler(selectedItems, services, (object) null);
  }

  /// <summary>Команда "Отменить изменения" отменяющая все сделанные правки с момента инициализации контрола</summary>
  /// <param name="askUserConfirmation">Запрашивать ли у пользователя подтверждение</param>
  public virtual void ResetChanges(bool askUserConfirmation = true)
  {
    if (askUserConfirmation && MessageBox.Show((IWin32Window) this, !string.IsNullOrWhiteSpace(this.CustomResetConfirmationQuestion) ? this.CustomResetConfirmationQuestion : Resources.GetString("ResetConfirmationQuestionDefault"), !string.IsNullOrWhiteSpace(this.CustomResetConfirmationCaption) ? this.CustomResetConfirmationCaption : Resources.GetString("ResetConfirmationCaptionDefault"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
      return;
    this.ResetChangesInternal();
  }

  /// <summary>Кишочки команды "Отменить изменения"</summary>
  protected virtual void ResetChangesInternal()
  {
    if (!this.Changed)
      return;
    this.LockSave("ResetObjectsList");
    try
    {
      if (this._objectVerIDs != null)
      {
        this._objectVerIDs.AddRange((IEnumerable<long>) this._removedObjectVerIDs);
        this._objectVerIDs.RemoveRange<long>((IEnumerable<long>) this._addedObjectVerIDs);
      }
      this._addedObjectVerIDs.Clear();
      this._removedObjectVerIDs.Clear();
      this.UpdateObjectsList();
      this.FireListChangedInternal(ObjectsListUserControl.ChangeType.Reset);
    }
    finally
    {
      this.UnlockSave("ResetObjectsList");
    }
  }

  /// <summary>Обновление визуального представления списка объектов</summary>
  private void UpdateObjectsList()
  {
    this.LockUpdateCommands(true);
    try
    {
      if (this._objectsListDescriptor != null && this._objectVerIDs != null)
        this._objectsListDescriptor.Update((IReadOnlyCollection<long>) this._objectVerIDs, true);
      this.ObjectsView.ReloadItems();
    }
    finally
    {
      this.UnlockUpdateCommands();
    }
  }

  /// <summary>Сделать список актуальным, очистить списки AddedObjectVerIDs и RemovedObjectVerIDs
  /// Должен вызываться после сохранения списка, например в БД</summary>
  public void MarkListAsActual()
  {
    if (this._addedObjectVerIDs.Count <= 0 && this._removedObjectVerIDs.Count <= 0)
      return;
    this._addedObjectVerIDs.Clear();
    this._removedObjectVerIDs.Clear();
    this.UpdateCommands();
    this.FireListChangedInternal(ObjectsListUserControl.ChangeType.BecomeActual);
  }

  /// <summary>Событие изменения списка</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [System.ComponentModel.Description("When list is changed")]
  public event ObjectsListUserControl.ListChangedDelegate ListChanged;

  /// <summary>Вызов события ListChanged при изменении списка</summary>
  /// <param name="changeType">Тип изменения</param>
  private void FireListChangedInternal(ObjectsListUserControl.ChangeType changeType)
  {
    if (this._lockListChangedCounter != 0)
      return;
    this.FireListChanged(changeType);
  }

  /// <summary>Вызов события ListChanged при изменении списка</summary>
  /// <param name="changeType">Тип изменения</param>
  protected virtual void FireListChanged(ObjectsListUserControl.ChangeType changeType)
  {
    if (this._lockListChangedCounter != 0 || this.ListChanged == null)
      return;
    this.ListChanged(changeType);
  }

  /// <summary>Заблокировать вызов события ListChanged</summary>
  public void LockListChanged() => ++this._lockListChangedCounter;

  /// <summary>Разблокировать вызов события ListChanged</summary>
  public void UnlockListChanged() => --this._lockListChangedCounter;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._panelTreeCaption = new Panel();
    this._labelDescription = new Label();
    this._panelButtons = new Panel();
    this._btnDelete = new Button();
    this._btnAdd = new Button();
    this._btnReset = new Button();
    this._objectsView = new ObjectsViewBase();
    this._panelTreeCaption.SuspendLayout();
    this._panelButtons.SuspendLayout();
    this.SuspendLayout();
    this._panelTreeCaption.AutoSize = true;
    this._panelTreeCaption.Controls.Add((Control) this._labelDescription);
    this._panelTreeCaption.Dock = DockStyle.Top;
    this._panelTreeCaption.Location = new Point(0, 0);
    this._panelTreeCaption.Name = "_panelTreeCaption";
    this._panelTreeCaption.Size = new Size(661, 21);
    this._panelTreeCaption.TabIndex = 7;
    this._labelDescription.AutoSize = true;
    this._labelDescription.Location = new Point(12, 8);
    this._labelDescription.Name = "_labelDescription";
    this._labelDescription.Size = new Size(116, 13);
    this._labelDescription.TabIndex = 1;
    this._labelDescription.Text = "Выбранные объекты:";
    this._labelDescription.TextAlign = ContentAlignment.BottomLeft;
    this._panelButtons.Controls.Add((Control) this._btnDelete);
    this._panelButtons.Controls.Add((Control) this._btnAdd);
    this._panelButtons.Controls.Add((Control) this._btnReset);
    this._panelButtons.Dock = DockStyle.Right;
    this._panelButtons.Location = new Point(577, 21);
    this._panelButtons.Name = "_panelButtons";
    this._panelButtons.Size = new Size(84, 559);
    this._panelButtons.TabIndex = 1;
    this._btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._btnDelete.Location = new Point(0, 37);
    this._btnDelete.Name = "_btnDelete";
    this._btnDelete.Size = new Size(75, 23);
    this._btnDelete.TabIndex = 1;
    this._btnDelete.Text = "Удалить";
    this._btnDelete.UseVisualStyleBackColor = true;
    this._btnDelete.Click += new EventHandler(this.BtnDelete_Click);
    this._btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._btnAdd.Location = new Point(0, 8);
    this._btnAdd.Name = "_btnAdd";
    this._btnAdd.Size = new Size(75, 23);
    this._btnAdd.TabIndex = 0;
    this._btnAdd.Text = "Добавить";
    this._btnAdd.UseVisualStyleBackColor = true;
    this._btnAdd.Click += new EventHandler(this.BtnAdd_Click);
    this._btnReset.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._btnReset.Location = new Point(0, 75);
    this._btnReset.Name = "_btnReset";
    this._btnReset.Size = new Size(75, 23);
    this._btnReset.TabIndex = 2;
    this._btnReset.Text = "Сброс";
    this._btnReset.UseVisualStyleBackColor = true;
    this._btnReset.Click += new EventHandler(this.BtnReset_Click);
    this._objectsView.AllowCustomGroupValues = true;
    this._objectsView.AllowEditing = true;
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
    this._objectsView.EditingMode = false;
    this._objectsView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._objectsView.Font = new Font("Tahoma", 8.25f);
    this._objectsView.Location = new Point(0, 21);
    this._objectsView.Name = "_objectsView";
    this._objectsView.Padding = new Padding(7);
    this._objectsView.Size = new Size(577, 559);
    this._objectsView.TabIndex = 0;
    this._objectsView.ViewContentType = ContentType.Folders | ContentType.NonFolders;
    this._objectsView.SelectedItemsChanged += new EventHandler(this.ObjectsView_SelectedItemsChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._objectsView);
    this.Controls.Add((Control) this._panelButtons);
    this.Controls.Add((Control) this._panelTreeCaption);
    this.Name = nameof (ObjectsListUserControl);
    this.Size = new Size(661, 580);
    this._panelTreeCaption.ResumeLayout(false);
    this._panelTreeCaption.PerformLayout();
    this._panelButtons.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Тип изменения</summary>
  public enum ChangeType
  {
    /// <summary>Список был инициализирован</summary>
    Init,
    /// <summary>Первое изменение списка. Последующе до обнуления списков AddedObjectVerIDs и RemovedObjectVerIDs
    /// помечаются как CommonChange</summary>
    FirstChange,
    /// <summary>Простое изменение списка. Если изменение вызвало обнуление списков AddedObjectVerIDs и RemovedObjectVerIDs, то
    /// вместо этого приходит BecomeActual</summary>
    CommonChange,
    /// <summary>Правки были сброшены</summary>
    Reset,
    /// <summary>Списки AddedObjectVerIDs и RemovedObjectVerIDs были обнулены, список стал актуальным</summary>
    BecomeActual,
  }

  /// <summary>Делегат события изменения списка</summary>
  /// <param name="changeType">Тип изменения</param>
  public delegate void ListChangedDelegate(ObjectsListUserControl.ChangeType changeType);
}
