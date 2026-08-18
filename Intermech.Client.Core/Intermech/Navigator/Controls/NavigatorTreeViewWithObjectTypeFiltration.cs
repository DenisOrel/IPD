
// Type: Intermech.Navigator.Controls.NavigatorTreeViewWithObjectTypeFiltration
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search;
using Intermech.Search.CompositionByObjectTypesFilters;
using Intermech.Search.Navigator.Windows;
using Intermech.Search.Utilities;
using Intermech.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>User Control деревом навигации и тубларом фильтрации по типам объектов
/// Само дерево навигации в дизайнере формы будет видно только в property grid в свойствах данного UserControl-а,
/// визуально будет не видно, т.к. создаётся руками. Сделано это так, дабы решить 2 казалось бы
/// взаимоисключающие задачи:
/// 1) Конструктор форм не должен валит ошибки при открытии данного UserControl-а и форм, в которые он будет вставлен
/// 2) У разработчика должна быть возможность указать класс NavTreeView, который должен создаваться
/// Взаимно они себя исключают по причине того, что попытка поменять логику создания контролов с "прибито гвоздями"
/// на "вызывается виртуальный метод или делегат" приводит к ошибкам при открытии дизайнера форм
/// 
/// Для определения класса дерева, которое должно создаваться надо назначить тип класса дерева-потомка NavigatorTreeView
/// перед вызовом конструктора данного контрола</summary>
public class NavigatorTreeViewWithObjectTypeFiltration : 
  IpsBaseUserControl,
  ITreeListColumns,
  ICommandTarget,
  IContextAware,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  /// <summary>Тип контрола дерева, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  [CanBeNull]
  public static System.Type OverrideTreeViewClass;
  /// <summary>Собственно дерево навигаторе. В DesignTime видно только в Properties всего UserControl, на форме </summary>
  [CanBeNull]
  protected NavigatorTreeView _treeView;
  /// <summary>Список именованных значков</summary>
  [CanBeNull]
  private INamedImageList _namedImageList;
  /// <summary>Контейнер сервисов для дерева</summary>
  [CanBeNull]
  protected AdvancedServiceContainer _servicesTree;
  /// <summary>Колонка, по которой была выполнена сортировка дерева, до включения режима ручной сортировки</summary>
  [CanBeNull]
  private NodeColumn _lastSortedColumn;
  /// <summary>Ссылка на активный фильтр составов по родительским и дочерним типам связей</summary>
  [NotNull]
  private readonly ICompositionByObjectTypesFiltration _otSupport = (ICompositionByObjectTypesFiltration) new CompositionByObjectTypesFiltration();
  /// <summary>Кэш ссылки на колонку заголовка</summary>
  [CanBeNull]
  private NodeColumn _captionColumn;
  private bool _captionColumnPropLoaded;
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [NotNull]
  public Action FullWindowRefresh;
  private long _selectedFilterVersionID;
  [NotNull]
  private readonly NavigatorTreeViewWithObjectTypeFiltration.CompositionByObjectTypesFilterProvider _compositionByObjectTypesFilterProvider;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ButtonItem _btnClearSorting;
  private ButtonItem _btnSetupSorting;
  private LabelItem _labelSpace;
  private Intermech.Bars.ToolBar _tbTreePanel;
  private ImageList _imagesToolbar;
  private ButtonItem _refreshButtonItem;
  private DropDownMenuItem _filterDropDownMenuItem;
  private ButtonItem _changeFiltersButtonItem;
  private MenuButtonItem _disableFiltrationMenuButtonItem;
  private ImageList _mainImageList;
  private ButtonItem _editingModeButtonItem;

  /// <summary>UI: Дерево состава объекта</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public NavigatorTreeView TreeView
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._treeView.CheckInitializedIn<NavigatorTreeView>((object) this);
    }
  }

  /// <summary>UI: Тулбар дерева состава объекта</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Intermech.Bars.ToolBar TreeToolbar
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tbTreePanel.CheckInitializedIn<Intermech.Bars.ToolBar>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public ButtonItem BtnClearSorting
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnClearSorting.CheckInitializedIn<ButtonItem>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public ButtonItem BtnSetupSorting
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnSetupSorting.CheckInitializedIn<ButtonItem>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public LabelItem LabelSpace
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelSpace.CheckInitializedIn<LabelItem>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public ImageList ImagesToolbar
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._imagesToolbar.CheckInitializedIn<ImageList>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal ButtonItem RefreshButtonItem
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._refreshButtonItem.CheckInitializedIn<ButtonItem>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal DropDownMenuItem FilterDropDownMenuItem
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._filterDropDownMenuItem.CheckInitializedIn<DropDownMenuItem>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal ButtonItem ChangeFiltersButtonItem
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._changeFiltersButtonItem.CheckInitializedIn<ButtonItem>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal MenuButtonItem DisableFiltrationMenuButtonItem
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._disableFiltrationMenuButtonItem.CheckInitializedIn<MenuButtonItem>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal ImageList MainImageList
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._mainImageList.CheckInitializedIn<ImageList>((object) this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  protected internal ButtonItem EditingModeButtonItem
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editingModeButtonItem.CheckInitializedIn<ButtonItem>((object) this);
    }
  }

  /// <summary>Default constructor</summary>
  public NavigatorTreeViewWithObjectTypeFiltration()
  {
    this.FullWindowRefresh = new Action(this.FullWindowRefreshInternal);
    this.SuspendLayout();
    this.InitializeComponent();
    this.CreateTreeView();
    this.ResumeLayout(true);
    if ((this.DesignMode ? 1 : (LicenseManager.UsageMode == LicenseUsageMode.Designtime ? 1 : 0)) == 0)
    {
      if (this._treeView != null)
      {
        this._treeView.EnableRowCaching = true;
        this._treeView.Columns.ListChanged += new ListChangedEventHandler(this.Columns_ListChanged);
      }
      this.Load += new EventHandler(this.NavigatorTreeViewWithObjectTypeFiltration_Load);
      this.InitVisibleColumns();
    }
    this._compositionByObjectTypesFilterProvider = new NavigatorTreeViewWithObjectTypeFiltration.CompositionByObjectTypesFilterProvider(this);
  }

  private void NavigatorTreeViewWithObjectTypeFiltration_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.TreeView.OnGetSupportedColumnsEventHandlerAssigned)
      return;
    this.InitDefaultSupportedColumns();
  }

  /// <summary>Инициализация доступных для выбора в диалоге "Настройка отображения"</summary>
  protected virtual void InitDefaultSupportedColumns()
  {
    this.TreeView.SupportedColumns = Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending);
  }

  /// <summary>Инициализация колонок отображаемых пользователю</summary>
  protected virtual void InitVisibleColumns()
  {
    this.TreeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Descending));
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected virtual System.Type DefaultNavigatorTreeViewClass
  {
    [DebuggerStepThrough] get => typeof (NavigatorTreeView);
  }

  protected virtual void CreateTreeView()
  {
    System.Type type = NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass;
    if ((object) type == null)
      type = this.DefaultNavigatorTreeViewClass;
    this._treeView = (NavigatorTreeView) Activator.CreateInstance(type);
    NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass = (System.Type) null;
    this._treeView.BeginInit();
    this._treeView.AllowDrop = true;
    this._treeView.AllowMultiSelect = false;
    this._treeView.AllowUserPinnedColumns = false;
    this._treeView.Dock = DockStyle.Fill;
    this._treeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._treeView.LineStyle = LineStyle.Dot;
    this._treeView.Location = new Point(1, 3);
    this._treeView.Name = "_treeView";
    this._treeView.RowEvenStyle.WordWrap = false;
    this._treeView.RowOddStyle.WordWrap = false;
    this._treeView.RowSelectedStyle.WordWrap = false;
    this._treeView.RowStyle.BorderColor = SystemColors.Control;
    this._treeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._treeView.RowStyle.BorderWidth = 1;
    this._treeView.RowStyle.WordWrap = false;
    this._treeView.SelectBeforeEdit = true;
    this._treeView.ShowRootRow = false;
    this._treeView.Size = new Size(797, 484);
    this._treeView.SuppressErrorMessages = true;
    this._treeView.TabIndex = 0;
    this._treeView.BeforeColumnsSorting += new EventHandler(this._treeView_BeforeColumnsSorting);
    this._treeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this._treeView_AfterFocusNode);
    this._treeView.BuildTree += new EventHandler(this._treeView_BuildTree);
    this._treeView.PlusJobCompleted += new PlusJobCompletedEventHandler(this.NavigatorTreeViewWithObjectTypeFiltration_TreeView_PlusJobCompleted);
    this._treeView.AfterPopulateNode += new EventHandler<NodeEventArgs>(this.NavigatorTreeViewWithObjectTypeFiltration_TreeView_AfterPopulateNode);
    this.Controls.Add((Control) this._treeView);
    this.TreeToolbar.Dock = DockStyle.Top;
    this._treeView.BringToFront();
    this._treeView.EndInit();
  }

  /// <summary>Вызывается после создания контейнера сервисов дерева, позволяет его дополнить (не наследуется во вьюшки внизу дерева)</summary>
  public event NavigatorTreeViewWithObjectTypeFiltration.OnInitTreeServicesDelegate OnInitTreeServices;

  /// <summary>Инициализировать сервисы.</summary>
  public virtual void InitializeServices([CanBeNull] System.IServiceProvider ownerServices)
  {
    this._namedImageList = ApplicationServices.Container.GetService<INamedImageList>(false);
    this.TreeToolbar.ImageList = this._namedImageList?.ImageList;
    this.AddService<NavigatorTreeView>(this.TreeView);
    this.AddService<ITreeListColumns>((ITreeListColumns) this);
    this.AddService<INavWindowSettings>((INavWindowSettings) new NavWindowSettings());
    this.AddService<IDisableDelayedUpdates>((IDisableDelayedUpdates) new DisableDelayedUpdates(false));
    this._servicesTree = new AdvancedServiceContainer(ownerServices);
    this._servicesTree.AddService<IViewState>((IViewState) new ViewStateService(ViewStateFlags.NodeInTree));
    this._servicesTree.AddService<ICompositionByObjectTypesFilterProvider>((ICompositionByObjectTypesFilterProvider) this._compositionByObjectTypesFilterProvider);
    NavigatorTreeViewWithObjectTypeFiltration.OnInitTreeServicesDelegate initTreeServices = this.OnInitTreeServices;
    if (initTreeServices != null)
      initTreeServices(this._servicesTree);
    this._otSupport.ActiveFilterGuid = Guid.Empty;
    BarManager service = ApplicationServices.Container.GetService<BarManager>(false);
    if (service != null)
    {
      service.RendererChanged += new EventHandler(this.NavWinToolbarRendererChanged);
      this.NavWinToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.TreeView.Services = (System.IServiceProvider) this._servicesTree;
    this.TreeView.BringToFront();
  }

  /// <summary>Вызов события AfterShown - после первого отображения контрола (первого WM_PAINT)</summary>
  protected override void FireFirstPaint()
  {
    if (this._servicesTree == null)
      this.InitializeServices((System.IServiceProvider) null);
    base.FireFirstPaint();
  }

  /// <summary>Деинициализировать сервисы</summary>
  protected virtual void DisposeServices()
  {
    if (this._servicesTree != null)
    {
      this._servicesTree.Dispose();
      this._servicesTree = (AdvancedServiceContainer) null;
    }
    this.RemoveService<ITreeListColumns>();
    this.RemoveService<IDisableDelayedUpdates>();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      BarManager service = ApplicationServices.Container.GetService<BarManager>(false);
      this.Load -= new EventHandler(this.NavigatorTreeViewWithObjectTypeFiltration_Load);
      if (this._treeView != null)
        this._treeView.Columns.ListChanged -= new ListChangedEventHandler(this.Columns_ListChanged);
      this._captionColumn = (NodeColumn) null;
      if (service != null)
      {
        this.TreeToolbar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        service.RendererChanged -= new EventHandler(this.NavWinToolbarRendererChanged);
      }
      if (!this.DesignMode)
        this.DisposeServices();
      if (this._servicesTree != null)
      {
        if (this._treeView?.Services != null)
          this._treeView.Services = (System.IServiceProvider) null;
        this._servicesTree.Dispose();
        this._servicesTree = (AdvancedServiceContainer) null;
      }
      this.RemoveService<NavigatorTreeView>();
      this.RemoveService<ITreeListColumns>();
      if (this.components != null)
      {
        this.components.Dispose();
        this.components = (IContainer) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary>Класс дерева (должен быть потомком NavigatorTreeView)</summary>
  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.Type NavigatorTreeViewType
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.GetType();
    }
  }

  /// <summary>Состояние ручной сортировки</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool ManualSorting { get; set; }

  /// <summary>Gets or sets options for controlling the operation</summary>
  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public WindowSettingsBase Settings
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetSettings();
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.SetSettings(value);
  }

  /// <summary>Колонка заголовка (!!! может быть null)</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeColumn CaptionColumn
  {
    [DebuggerStepThrough] get
    {
      if (!this._captionColumnPropLoaded)
      {
        this._captionColumn = this.TreeView.TreeColumns.FirstOrDefault<NodeColumn>((Func<NodeColumn, bool>) (column =>
        {
          if (column.ID == null)
            return false;
          if (column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_CAPTION"))
            return true;
          return column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION);
        }));
        this._captionColumnPropLoaded = true;
      }
      return this._captionColumn;
    }
  }

  /// <summary>Колонка дерева содержащая заголовок (!!! может быть null)</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigatorTreeColumn CaptionTreeColumn
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CaptionColumn == null ? (NavigatorTreeColumn) null : this.TreeView.GetColumn(this.CaptionColumn);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public event EventHandler<NavigatorTreeNodeEventArgs> ChildsLoaded;

  /// <summary>Контейнер сервисов для дерева</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AdvancedServiceContainer ServicesTree
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._servicesTree;
    }
  }

  /// <summary>Сфокусированная в данный момент нода дерева</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public NavigatorTreeNode FocusedTreeNode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.GetFocusedTreeNode();
    }
  }

  /// <summary>Интерфейс идентификатора сфокусированной в дереве ноды</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public INodeID FocusedNodeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.GetFocusedNodeID();
    }
  }

  /// <summary>Идентификатор категории сфокусированной в данной момент в дереве сущности</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? FocusedCategoryID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.GetFocusedCategoryID();
    }
  }

  /// <summary>Идентификатор типа сфокусированной в данной момент в дереве сущности</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? FocusedTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.GetFocusedTypeID();
    }
  }

  /// <summary>Перечисление выбранных нод дерева навигатора без какой-либо фильтрации</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  [ItemNotNull]
  public IReadOnlyList<NavigatorTreeNode> SelectedNodes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.GetSelectedNodes();
    }
  }

  /// <summary>Перечисление интерфейсов идентификаторов выбранных нод без какой-либо фильтрации</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  [ItemNotNull]
  public IReadOnlyList<INodeID> SelectedNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.GetSelectedNodeIDs();
    }
  }

  /// <summary>Перечисление идентификаторов категорий выбранных сущностей без какой-либо фильтрации</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  public IEnumerable<int> SelectedCategoryIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<int>) this.TreeView.GetSelectedCategoryIDs();
    }
  }

  /// <summary>Перечисление выбранных нод дерева навигатора
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  [ItemNotNull]
  public IReadOnlyList<NavigatorTreeNode> SelectedClosestToRootNodes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.GetSelectedNodesClosestToRoot();
    }
  }

  /// <summary>Перечисление интерфейсов идентификаторов выбранных сущностей
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  [ItemNotNull]
  public IEnumerable<INodeID> SelectedClosestToRootNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<INodeID>) this.TreeView.GetSelectedClosestToRootNodeIDs();
    }
  }

  /// <summary>Перечисление идентификаторов категорий выбранных сущностей
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [NotNull]
  public IEnumerable<int> SelectedClosestToRootCategoryIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IEnumerable<int>) this.TreeView.GetSelectedClosestToRootCategoryIDs();
    }
  }

  /// <summary>Event handler. Called by _treeView for after focus node events</summary>
  private void _treeView_AfterFocusNode([CanBeNull] object sender, [NotNull] NavigatorTreeNodeEventArgs e)
  {
    ISelectedItems selectedItems = this.TreeView.SelectedItems;
    bool flag = false;
    if (selectedItems != null && selectedItems.Count > 0 && ManualSortingEditForm.FindFirstSortingObjectItem(selectedItems) >= 0)
      flag = true;
    this.BtnSetupSorting.Enabled = flag;
  }

  private void _treeView_BeforeColumnsSorting([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.ManualSorting = false;
    this._lastSortedColumn = (NodeColumn) null;
    this.BtnClearSorting.Checked = false;
  }

  private void _treeView_BuildTree([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    NavigatorTreeNode rootNode = this.TreeView.RootNode;
    this.OTF_EnableFiltration(rootNode?.Handler is IObjectTypeAndRelationFiltrationSupported && (rootNode.Handler.Options & NodeOptions.CanContainsComposition) > NodeOptions.None);
    this.EditingModeButtonItem.Visible = this.TreeView.SupportedEditing;
    this.BuildTree(sender, e);
    EventHandler onBuildTree = this.OnBuildTree;
    if (onBuildTree == null)
      return;
    onBuildTree(sender, e);
  }

  /// <summary>Вызывается при построении дерева</summary>
  protected virtual void BuildTree([CanBeNull] object sender, [NotNull] EventArgs e)
  {
  }

  /// <summary>Событие вызывается после построения дерева.</summary>
  [Browsable(true)]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_119")]
  public event EventHandler OnBuildTree;

  /// <summary>Список колонок был изменён</summary>
  protected virtual void Columns_ListChanged([CanBeNull] object sender, [NotNull] ListChangedEventArgs e)
  {
    this._captionColumnPropLoaded = false;
    this._captionColumn = (NodeColumn) null;
  }

  /// <summary>Фоновая загрузка состава ноды завершена</summary>
  private void NavigatorTreeViewWithObjectTypeFiltration_TreeView_PlusJobCompleted(
    [NotNull] NavigatorTreeNode node)
  {
    this.AfterChildsLoaded(node);
  }

  /// <summary>Были загружены дочерние ноды</summary>
  private void NavigatorTreeViewWithObjectTypeFiltration_TreeView_AfterPopulateNode(
    [CanBeNull] object sender,
    [NotNull] NodeEventArgs e)
  {
    this.AfterChildsLoaded(e.Node);
  }

  /// <summary>Вызывается после загрузки всех дочерних нод</summary>
  protected virtual void AfterChildsLoaded([NotNull] NavigatorTreeNode node)
  {
    EventHandler<NavigatorTreeNodeEventArgs> childsLoaded = this.ChildsLoaded;
    if (childsLoaded == null)
      return;
    childsLoaded((object) this, new NavigatorTreeNodeEventArgs(node));
  }

  /// <summary>Дескриптор корневого узла в дереве окна "Навигатора"</summary>
  [CanBeNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public IDescriptor RootDescriptor
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.RootDescriptor;
    }
    [DebuggerStepThrough] set => this.TreeView.Build(value);
  }

  /// <summary>Список видимых колонок</summary>
  [NotNull]
  [ItemNotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public NodeColumnCollection TreeListColumns
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.TreeView.ReflectTreeColumsChanges();
    }
    set
    {
      this.TreeView.SetColumns(value);
      if (!this.TreeView.DisableColumnsSorting)
        return;
      this.BtnClearSorting.Checked = true;
      this.DoCancelSort((object) null, EventArgs.Empty);
    }
  }

  /// <summary>Выполнить команду</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если команда выполнена успешно</returns>
  public virtual bool Execute(ICommandState commandState) => this.TreeView.Execute(commandState);

  /// <summary>Установить статус команде</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, статус команды установлен</returns>
  public virtual bool QueryStatus(ICommandState commandState)
  {
    return !this.Disposing && !this.IsDisposed && this.TreeView.QueryStatus(commandState);
  }

  /// <summary>Включить или запретить фильтрацию составов. При этом прячутся кнопки</summary>
  /// <param name="enabled">Разрешена или запрещена фильтрация</param>
  protected void OTF_EnableFiltration(bool enabled)
  {
    if (!enabled)
      this._otSupport.ActiveFilterGuid = Guid.Empty;
    this.FilterDropDownMenuItem.Visible = false;
    this.ChangeFiltersButtonItem.Visible = false;
    if (!enabled)
      return;
    INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
    this.FilterDropDownMenuItem.Visible = true;
    this.FilterDropDownMenuItem.ImageIndex = namedImageList.ImageIndex("imgFunnel");
    this.DisableFiltrationMenuButtonItem.ImageIndex = namedImageList.ImageIndex("imgFunnelDisabled");
    this.ChangeFiltersButtonItem.Visible = true;
    this.ChangeFiltersButtonItem.ImageIndex = namedImageList.ImageIndex("imgFunnelSetup");
    this.FillFiltersMenu();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long SelectedFilterVersionID
  {
    get => this._selectedFilterVersionID;
    set
    {
      if (this._selectedFilterVersionID == value)
        return;
      this._selectedFilterVersionID = value;
      this.SetStandardFilterImageForAllFilters();
      INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
      if (ObjectHelper.IsUnknownObjectVersionID(this._selectedFilterVersionID))
      {
        this.FilterDropDownMenuItem.ImageIndex = namedImageList.ImageIndex("imgFunnelDisabled");
      }
      else
      {
        MenuButtonItem buttonItemForFilter = this.GetMenuButtonItemForFilter(this._selectedFilterVersionID);
        this.FilterDropDownMenuItem.ImageIndex = namedImageList.ImageIndex("imgFunnelActive");
        if (buttonItemForFilter != null)
          buttonItemForFilter.ImageIndex = namedImageList.ImageIndex("imgFunnelActive");
      }
      this.FullWindowRefresh();
    }
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter SelectedFilter
  {
    get
    {
      return this.GetMenuButtonItemForFilter(this.SelectedFilterVersionID)?.Tag as Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter;
    }
  }

  private void FullWindowRefreshInternal() => this.FullWindowRefreshImplementation();

  public bool FullWindowRefreshImplementation()
  {
    bool flag1 = false;
    NodeIDPath focusedPath = this.TreeView.FocusedPath;
    NavigatorTreeNode parent1 = this.TreeView.FocusedNode?.Parent;
    ProjectObjectID data1 = focusedPath?.LastID == null || parent1?.Handler == null ? (ProjectObjectID) null : parent1.GetData<ProjectObjectID>(focusedPath.LastID);
    NavigatorTreeNode focusedNode1 = this.TreeView.FocusedNode;
    bool flag2 = focusedNode1 != null && focusedNode1.Expanded;
    if (focusedPath != null)
      this.TreeView.BuildWithPath(focusedPath.RootDescriptor, focusedPath);
    NavigatorTreeNode focusedNode2 = this.TreeView.FocusedNode;
    NavigatorTreeNode parent2 = focusedNode2?.Parent;
    if (data1 != null && focusedNode2?.Handler != null && parent2 != null && parent2.Handler == null)
    {
      ICurrentUserAndRole service = ApplicationServices.Container.GetService<ICurrentUserAndRole>();
      if (service.ProjectID != 0L && service.ProjectID != data1.ProjectID && focusedNode2.Children != null)
      {
        foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) focusedNode2.Children)
        {
          ProjectObjectID data2 = focusedNode2.GetData<ProjectObjectID>(child.NodeID);
          if (data2 != null && data2.ProjectID == service.ProjectID)
          {
            this.TreeView.FocusedNode = child;
            flag1 = true;
            break;
          }
        }
      }
    }
    if (this.TreeView.FocusedNode != null && this.TreeView.FocusedNode.Expanded != flag2)
      this.TreeView.FocusedNode.Expanded = flag2;
    return flag1;
  }

  /// <summary>Очистим сортировку у колонок дерева</summary>
  public void DoCancelSort([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    bool flag = this.BtnClearSorting.Checked;
    if (this.BtnClearSorting.Checked)
    {
      foreach (NodeColumn treeColumn in (List<NodeColumn>) this.TreeView.TreeColumns)
      {
        if (treeColumn.SortOrder != NodeColumnSortOrder.None)
        {
          this._lastSortedColumn = treeColumn.Clone() as NodeColumn;
          break;
        }
      }
    }
    if (!this.BtnClearSorting.Checked && this._lastSortedColumn != null)
    {
      NodeColumnCollection nodeColumnCollection = this.TreeView.ReflectTreeColumsChanges().Clone() as NodeColumnCollection;
      nodeColumnCollection.RemoveSortInfo();
      NodeColumn nodeColumn = nodeColumnCollection.Find(this._lastSortedColumn.Key);
      if (nodeColumn != null)
      {
        nodeColumn.SortOrder = this._lastSortedColumn.SortOrder;
        nodeColumn.SortIndex = this._lastSortedColumn.SortIndex;
      }
      this.TreeView.SetColumns(nodeColumnCollection);
      this.BtnClearSorting.Checked = flag;
      this.ManualSorting = flag;
    }
    else
    {
      NodeColumn lastSortedColumn = this._lastSortedColumn;
      this.TreeView.ColumnsClearSorting();
      this._lastSortedColumn = lastSortedColumn;
      this.BtnClearSorting.Checked = flag;
      this.ManualSorting = flag;
    }
  }

  /// <summary>Выполняем настройку ручной сортировки</summary>
  private void btSetupSorting_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.TreeView.TreeManualSortingSetupCommand();
  }

  /// <summary>Пришло событие "Изменился рендерер панелей инструментов"</summary>
  protected virtual void NavWinToolbarRendererChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.TreeToolbar.Renderer = ((BarManager) sender)?.Renderer;
  }

  [NotNull]
  private WindowSettingsBase GetSettings()
  {
    WindowSettings settings = new WindowSettings();
    settings.TreeColumns = this.TreeView.ReflectTreeColumsChanges();
    return (WindowSettingsBase) settings;
  }

  private void SetSettings([NotNull] WindowSettingsBase settings)
  {
    this.TreeView.SetColumns(settings.TreeColumns);
  }

  /// <summary>Список контролов, дизайнеры которых должны быть активированы</summary>
  /// <returns>&gt;Или список, или null, если таковых не должно быть
  /// Пара "Контрол"-"имя поля, в которые будут сохранятся правки" (полем может выступать wrapper для контрола)</returns>
  [NotNull]
  protected override List<(Control DesignModeControl, string FieldName)> GetDesignModeChildControls()
  {
    List<(Control, string)> modeChildControls = base.GetDesignModeChildControls() ?? new List<(Control, string)>();
    modeChildControls.Add(((Control) this._treeView, "TreeView"));
    return modeChildControls;
  }

  private void RefreshButtonItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.TreeView.TreeRefreshNodeCommand((ISelectedItems) null, (System.IServiceProvider) null, (object) null);
  }

  private void FilterDropDownMenuItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
  }

  private void FilterMenuButtonItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    MenuButtonItem menuButtonItem = (MenuButtonItem) sender;
    if (menuButtonItem == null)
      return;
    Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter tag = (Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter) menuButtonItem.Tag;
    if (tag == null)
      return;
    this.SelectedFilterVersionID = tag.ObjectVersionID;
  }

  private void DisableFiltrationMenuButtonItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.SelectedFilterVersionID = 0L;
  }

  private void ChangeFiltersButtonItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    using (CompositionByObjectTypesFiltersEditorForm filtersEditorForm = new CompositionByObjectTypesFiltersEditorForm())
    {
      filtersEditorForm.ObjectVersionID = NavigatorTreeViewWithObjectTypeFiltration.GetCurrentUserConfigurationVersionID();
      int num = (int) filtersEditorForm.ShowDialog();
    }
    this.RefreshFiltersCacheAndRefillFiltersMenu();
    this.FullWindowRefresh();
  }

  private void EditingModeButtonItem_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.TreeView.EditingMode = !this.TreeView.EditingMode;
    this.EditingModeButtonItem.Checked = this.TreeView.EditingMode;
  }

  private void FillFiltersMenu()
  {
    this.FilterDropDownMenuItem.Items.Clear();
    this.FilterDropDownMenuItem.Items.Add((ToolbarItemBase) this.DisableFiltrationMenuButtonItem);
    this.CreateMenuButtonItemsForFiltersAndAddToFilterDropDownMenuItem(NavigatorTreeViewWithObjectTypeFiltration.GetFiltersForCurrentUser());
    this.CreateMenuButtonItemsForFiltersAndAddToFilterDropDownMenuItem(NavigatorTreeViewWithObjectTypeFiltration.GetFiltersForCurrentRole());
    this.FilterDropDownMenuItem.Enabled = this.FilterDropDownMenuItem.Items.Count > 1;
    if (ObjectHelper.IsUnknownObjectVersionID(this.SelectedFilterVersionID))
      return;
    MenuButtonItem buttonItemForFilter = this.GetMenuButtonItemForFilter(this.SelectedFilterVersionID);
    if (buttonItemForFilter == null)
      return;
    INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
    buttonItemForFilter.ImageIndex = this.FilterDropDownMenuItem.ImageIndex = namedImageList.ImageIndex("imgFunnelActive");
  }

  [CanBeNull]
  private MenuButtonItem GetMenuButtonItemForFilter(long filterVersionID)
  {
    return this.FilterDropDownMenuItem.Items.Cast<MenuButtonItem>().FirstOrDefault<MenuButtonItem>((Func<MenuButtonItem, bool>) (o => o.Tag != null && ((Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter) o.Tag).ObjectVersionID == filterVersionID));
  }

  private void CreateMenuButtonItemsForFiltersAndAddToFilterDropDownMenuItem(
    [NotNull, ItemNotNull] Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter[] filters)
  {
    Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter objectTypesFilter = ((IEnumerable<Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter>) filters).FirstOrDefault<Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter>();
    foreach (Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter filter in filters)
    {
      MenuButtonItem buttonItemForFilter = this.CreateMenuButtonItemForFilter(filter);
      if (filter == objectTypesFilter)
        buttonItemForFilter.BeginGroup = true;
      this.FilterDropDownMenuItem.Items.Add((ToolbarItemBase) buttonItemForFilter);
    }
  }

  [NotNull]
  [ItemNotNull]
  private static Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter[] GetFiltersForCurrentUser()
  {
    ICompositionByObjectTypesFiltersClientService filtersClientService = ServiceLocator.Get<ICompositionByObjectTypesFiltersClientService>();
    Intermech.Diagnostics.Check.NotNull<ICompositionByObjectTypesFiltersClientService>(filtersClientService, "compositionByObjectTypesFiltersClientService");
    return filtersClientService.GetFiltersForCurrentUser();
  }

  [NotNull]
  private static Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter[] GetFiltersForCurrentRole()
  {
    ICompositionByObjectTypesFiltersClientService filtersClientService = ServiceLocator.Get<ICompositionByObjectTypesFiltersClientService>();
    Intermech.Diagnostics.Check.NotNull<ICompositionByObjectTypesFiltersClientService>(filtersClientService, "compositionByObjectTypesFiltersClientService");
    return filtersClientService.GetFiltersForCurrentRole();
  }

  [NotNull]
  private MenuButtonItem CreateMenuButtonItemForFilter([NotNull] Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter filter)
  {
    MenuButtonItem buttonItemForFilter = new MenuButtonItem(filter.Name);
    if (this._namedImageList != null)
      buttonItemForFilter.ImageIndex = this._namedImageList.ImageIndex("imgFunnel");
    buttonItemForFilter.Click += new EventHandler(this.FilterMenuButtonItem_Click);
    buttonItemForFilter.Tag = (object) filter;
    return buttonItemForFilter;
  }

  private void RefreshFiltersCacheAndRefillFiltersMenu()
  {
    ServiceLocator.Get<ICompositionByObjectTypesFiltersClientService>().RefreshFiltersCache();
    this.FillFiltersMenu();
  }

  private static long GetCurrentUserConfigurationVersionID()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetCustomService<ICompositionByObjectTypesFiltersServerService>().GetCurrentUserConfigurationVersionID(sessionKeeper.Session.SessionGUID);
  }

  private void SetStandardFilterImageForAllFilters()
  {
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.FilterDropDownMenuItem.Items)
    {
      if (menuButtonItem != this.DisableFiltrationMenuButtonItem && this._namedImageList != null)
        menuButtonItem.ImageIndex = this._namedImageList.ImageIndex("imgFunnel");
    }
  }

  [CanBeNull]
  IServiceContainer ITreeListColumns.Services => (IServiceContainer) this.ServicesTree;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NavigatorTreeViewWithObjectTypeFiltration));
    this._tbTreePanel = new Intermech.Bars.ToolBar();
    this._mainImageList = new ImageList(this.components);
    this._btnClearSorting = new ButtonItem();
    this._btnSetupSorting = new ButtonItem();
    this._refreshButtonItem = new ButtonItem();
    this._filterDropDownMenuItem = new DropDownMenuItem();
    this._disableFiltrationMenuButtonItem = new MenuButtonItem();
    this._changeFiltersButtonItem = new ButtonItem();
    this._editingModeButtonItem = new ButtonItem();
    this._labelSpace = new LabelItem();
    this._imagesToolbar = new ImageList(this.components);
    this.SuspendLayout();
    this._tbTreePanel.FlipLastItem = true;
    this._tbTreePanel.FullMenus = true;
    this._tbTreePanel.Guid = new Guid("3fb71a02-4b93-44ea-84a6-db6e9ca5869f");
    this._tbTreePanel.Hidden = false;
    this._tbTreePanel.ImageList = this._mainImageList;
    this._tbTreePanel.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this._btnClearSorting,
      (ToolbarItemBase) this._btnSetupSorting,
      (ToolbarItemBase) this._refreshButtonItem,
      (ToolbarItemBase) this._filterDropDownMenuItem,
      (ToolbarItemBase) this._changeFiltersButtonItem,
      (ToolbarItemBase) this._editingModeButtonItem,
      (ToolbarItemBase) this._labelSpace
    });
    this._tbTreePanel.Location = new Point(0, 0);
    this._tbTreePanel.Name = "_tbTreePanel";
    this._tbTreePanel.Size = new Size(804, 24);
    this._tbTreePanel.TabIndex = 8;
    this._tbTreePanel.Text = "";
    this._mainImageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_mainImageList.ImageStream");
    this._mainImageList.TransparentColor = Color.Transparent;
    this._mainImageList.Images.SetKeyName(0, "перечитать_все_окна.png");
    this._mainImageList.Images.SetKeyName(1, "настройки.png");
    this._mainImageList.Images.SetKeyName(2, "h.ico");
    this._mainImageList.Images.SetKeyName(3, "обновить.png");
    this._mainImageList.Images.SetKeyName(4, "обновить_фильтр.png");
    this._mainImageList.Images.SetKeyName(5, "просмотр1.png");
    this._mainImageList.Images.SetKeyName(6, "настройка_поиска.png");
    this._mainImageList.Images.SetKeyName(7, "правило_подбора_версий.png");
    this._mainImageList.Images.SetKeyName(8, "редактировать.png");
    this._mainImageList.Images.SetKeyName(9, "ручная_сортировка.png");
    this._mainImageList.Images.SetKeyName(10, "настройка_ручной_сортировки.png");
    this._mainImageList.Images.SetKeyName(11, "вид.png");
    this._mainImageList.Images.SetKeyName(12, "группировка.png");
    this._mainImageList.Images.SetKeyName(13, "свернуть.png");
    this._mainImageList.Images.SetKeyName(14, "развернуть.png");
    this._mainImageList.Images.SetKeyName(15, "свернуть_все_кроме_активной.png");
    this._btnClearSorting.AutoToggle = AutoToggleType.Single;
    this._btnClearSorting.CommandName = "btCancelSort";
    this._btnClearSorting.ImageIndex = 9;
    this._btnClearSorting.ToolTipText = "Режим ручной сортировки";
    this._btnClearSorting.Click += new EventHandler(this.DoCancelSort);
    this._btnSetupSorting.CommandName = "btSetupSorting";
    this._btnSetupSorting.ImageIndex = 10;
    this._btnSetupSorting.ToolTipText = "Выполнить настройку ручной сортировки";
    this._btnSetupSorting.Click += new EventHandler(this.btSetupSorting_Click);
    this._refreshButtonItem.CommandName = "Refresh";
    this._refreshButtonItem.ImageIndex = 3;
    this._refreshButtonItem.ToolTipText = "Обновить";
    this._refreshButtonItem.Click += new EventHandler(this.RefreshButtonItem_Click);
    this._filterDropDownMenuItem.BeginGroup = true;
    this._filterDropDownMenuItem.CommandName = "_filterDropDownMenuItem";
    this._filterDropDownMenuItem.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._disableFiltrationMenuButtonItem
    });
    this._filterDropDownMenuItem.ShowText = true;
    this._filterDropDownMenuItem.ToolTipText = "Фильтры состава по типам объектов";
    this._filterDropDownMenuItem.Click += new EventHandler(this.FilterDropDownMenuItem_Click);
    this._disableFiltrationMenuButtonItem.BeginGroup = true;
    this._disableFiltrationMenuButtonItem.CommandName = "_disableFiltrationMenuButtonItem";
    this._disableFiltrationMenuButtonItem.ShowText = true;
    this._disableFiltrationMenuButtonItem.Text = "Отключить фильтрацию";
    this._disableFiltrationMenuButtonItem.Click += new EventHandler(this.DisableFiltrationMenuButtonItem_Click);
    this._changeFiltersButtonItem.CommandName = "_changeFiltersButtonItem";
    this._changeFiltersButtonItem.ToolTipText = "Настройка фильтров состава по типам объектов";
    this._changeFiltersButtonItem.Click += new EventHandler(this.ChangeFiltersButtonItem_Click);
    this._editingModeButtonItem.BeginGroup = true;
    this._editingModeButtonItem.CommandName = "_allowEditingButtonItem";
    this._editingModeButtonItem.Image = (Image) Intermech.Client.Core.Properties.Resources.EditStandart;
    this._editingModeButtonItem.Text = "Режим редактирования";
    this._editingModeButtonItem.ToolTipText = "Режим редактирования";
    this._editingModeButtonItem.Click += new EventHandler(this.EditingModeButtonItem_Click);
    this._labelSpace.BeginGroup = true;
    this._labelSpace.CommandName = "labelSpace";
    this._labelSpace.Enabled = false;
    this._labelSpace.Stretch = true;
    this._labelSpace.Text = " ";
    this._labelSpace.ToolTipText = " ";
    this._imagesToolbar.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imagesToolbar.ImageStream");
    this._imagesToolbar.TransparentColor = Color.Transparent;
    this._imagesToolbar.Images.SetKeyName(0, "");
    this._imagesToolbar.Images.SetKeyName(1, "");
    this._imagesToolbar.Images.SetKeyName(2, "");
    this._imagesToolbar.Images.SetKeyName(3, "");
    this._imagesToolbar.Images.SetKeyName(4, "ручная_сортировка.png");
    this._imagesToolbar.Images.SetKeyName(5, "настройка_ручной_сортировки.png");
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._tbTreePanel);
    this.Name = nameof (NavigatorTreeViewWithObjectTypeFiltration);
    this.Size = new Size(804, 512 /*0x0200*/);
    this.ResumeLayout(false);
  }

  /// <summary>Делегат для создания дерева навигатора</summary>
  public delegate NavigatorTreeView NavigatorTreeViewConstructionDelegate();

  public delegate void OnInitTreeServicesDelegate([NotNull] AdvancedServiceContainer treeServices);

  private sealed class CompositionByObjectTypesFilterProvider : 
    ICompositionByObjectTypesFilterProvider
  {
    [NotNull]
    private readonly NavigatorTreeViewWithObjectTypeFiltration _navigatorTreeViewWithObjectTypeFiltration;

    public CompositionByObjectTypesFilterProvider(
      [NotNull] NavigatorTreeViewWithObjectTypeFiltration navigatorTreeViewWithObjectTypeFiltration)
    {
      this._navigatorTreeViewWithObjectTypeFiltration = navigatorTreeViewWithObjectTypeFiltration;
    }

    [CanBeNull]
    public Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter Filter
    {
      get => this._navigatorTreeViewWithObjectTypeFiltration.SelectedFilter;
    }
  }
}
