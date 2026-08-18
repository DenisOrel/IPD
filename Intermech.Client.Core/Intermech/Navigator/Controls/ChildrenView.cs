
// Type: Intermech.Navigator.Controls.ChildrenView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Navigator.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Notifications;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Search;
using Intermech.Search.ChildrenViewEditing;
using Intermech.Search.Configuration;
using Intermech.Search.iGrid;
using Intermech.Search.ObjectListFilters;
using Intermech.Search.SimilarCharacterHighlighting;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using Intermech.UI.Winforms;
using NJFLib.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>Реализация закладки "Состоит из"</summary>
public class ChildrenView : 
  UserControl,
  IAdvancedView,
  IView,
  IEmbeddedViews,
  IViewData,
  ICommandTarget,
  ISelectedItemsHost,
  INodeView,
  IIOSource,
  IReportView,
  INavigatorContextSearch,
  ISelectedItemsText
{
  private bool _isReloadItemsSuppressed;
  /// <summary>Внешняя функция для того, чтобы можно было произвести манипуляции над сервисами контекстного меню закладки
  /// по месту использования контрола</summary>
  public ChildrenView.GetMenuServiceContainerDelegate OnGetMenuServiceContainer;
  private readonly StringFormat LineAlignmentCenterStringFormat = new StringFormat()
  {
    LineAlignment = StringAlignment.Center
  };
  private bool _isColumnsObsoleted;
  private bool _disableCloneSelectedItems;
  private bool _suppressUpdateStatusbar;
  internal const string ObjectTypeIconColumnKey = "Special_StateImage";
  internal const string CheckedOutByColumnKey = "Special_CheckedOut";
  /// <summary>
  /// Ключ настроек, запрещающих группирование строк в гриде
  /// </summary>
  public const string DisableGroupBoxNavWindowSettingsKey = "7DE281F6CD74456B8E1060967E0FCBF9";
  /// <summary>
  /// Путь к главному узлу XML, в котором хранятся все узлы с настройками
  /// </summary>
  private const string RootXmlStateNode = "ChildrenView";
  /// <summary>
  /// Путь к узлу XML, в котором хранятся настройки самой закладки
  /// </summary>
  private const string PropertiesXmlStateNode = "State";
  /// <summary>
  /// Путь к узлу XML, в котором хранятся настройки закладки
  /// </summary>
  private const string EmbeddedViewsXmlStateNode = "EmbeddedViews";
  private ICommandsProvider _commandsProvider;
  private bool _editingMode;
  /// <summary>Прямоугольник, в котором "всё началось"</summary>
  private Rectangle _startDragRectangle;
  /// <summary>
  /// Коллекция выделенных узлов - назначение для drag'n'drop
  /// </summary>
  protected NodeItems _dropTargetNodeItems;
  /// <summary>
  /// Коллекция служб по отрисовке, по спискам изображений для колонок
  /// </summary>
  internal HybridDictionary _painterDictionary;
  /// <summary>Сервис по отображению статусов элементов (iGrid)</summary>
  private GridStatusesPainter _gridStatusesPainter;
  /// <summary>Открыты ли вложенные закладки</summary>
  private bool _embeddedViewsOpened;
  /// <summary>Загружены ли данные в грид</summary>
  protected internal bool _dataLoaded;
  /// <summary>
  /// Пока значение этого флажка равно true, грид всегда читает все данные (пакетное чтение отключается)
  /// </summary>
  private bool _readAllMode;
  /// <summary>Состояние вложенной панели с закладками</summary>
  private ChildrenView.EmbeddedViewsState _embeddedViewsState;
  /// <summary>Предотвращение сортировки</summary>
  private bool _preventSorting;
  /// <summary>Была ли попытка выполнить сортировку</summary>
  private bool _tryingSorting;
  /// <summary>Запрет изменения выбора элементов</summary>
  private int _preventSelectionChanged;
  /// <summary>Открыто контекстное меню</summary>
  private bool _contextMenuActive;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  private NotificationEventHandler _notifyHandler;
  /// <summary>Включён ли режим ручной сортировки</summary>
  private bool _manualSorting;
  /// <summary>Коллекция выделенных элементов грида</summary>
  internal ChildrenViewSelectedItems _gridSelectedItems;
  /// <summary>Менеджер контекстного поиска в ячейках грида</summary>
  private ChildrenViewContextSearchManager _childrenViewContextSearchManager;
  /// <summary>
  /// Списки сортируемых и группирующих колонок, которые были до включения режима ручной сортировки
  /// </summary>
  private GroupingAndSortingColumns _lastGroupingAndSortingColumns;
  /// <summary>Зачитывать данные при активации закладки</summary>
  protected bool _readDataOnActivate = true;
  /// <summary>Сервис по фильтрации списков объектов</summary>
  private ObjectListFiltration _objectListFiltration;
  /// <summary>Контейнер сервисов</summary>
  protected AdvancedServiceContainer _services;
  /// <summary>Диспетчер событий</summary>
  protected IIODispatcher _ioDispatcher;
  /// <summary>Служба уведомлений</summary>
  protected INotificationService _notificationService;
  /// <summary>Служба настроек вида</summary>
  protected INavigatorColumnsService _navigatorColumnsService;
  /// <summary>Описание корневого узла списка</summary>
  protected internal INode _parentNode;
  /// <summary>Идентификатор корневого узла списка</summary>
  protected INodeID _nodeID;
  /// <summary>Путь к корневому узлу списка</summary>
  protected internal NodeIDPath _parentPath;
  /// <summary>Список выделенных элементов родительского узла</summary>
  protected internal NodeItems _parentSelItem;
  /// <summary>Путь к родительскому узлу</summary>
  protected NodeIDPath _path;
  /// <summary>Родительский узел</summary>
  protected INode _node;
  /// <summary>
  /// Сервис для хранения изображений элементов навигации, привязанных к
  /// категориям, типам и состояниям элементов.
  /// </summary>
  private ICategoryTypeStateImageService _categoryTypeStateImageService;
  /// <summary>
  /// Сервис для хранения изображений элементов навигации, привязанных к
  /// категориям, типам и состояниям элементов.
  /// </summary>
  protected ICategoryTypeIconService _categoryTypeIconService;
  /// <summary>Кэш графических элементов "Навигатора"</summary>
  protected INavGraphicsCache _navGraphicsCache;
  /// <summary>Работа с базой данных</summary>
  public ChildrenViewDataAdapter _dataAdapter;
  /// <summary>Информация о текущем пользователе и роли</summary>
  private ICurrentUserAndRole _currentUserAndRole;
  /// <summary>Состояние вьюшки</summary>
  private IViewState _viewState;
  /// <summary>Коллекция именованных значков</summary>
  public static INamedImageList _namedImageList;
  /// <summary>Кэш имён пользователей</summary>
  public static IUserNamesCache _userNamesCache;
  /// <summary>Показывать комбо фильтраций, или нет</summary>
  private bool _disableFiltration;
  /// <summary>Запретить контекстный поиск в ячейках грида</summary>
  private bool _disableContextSearch;
  /// <summary>Запретить появляться панели группировки</summary>
  private bool _disableGroupBox;
  /// <summary>
  /// HACK: true - значение _disableGroupBox не будет сохраняться в настройки текущего окна "Навигатора"
  /// </summary>
  protected internal bool _disableSaveGroupBox;
  /// <summary>
  /// Разрешить гриду особую обработку некоторых типов данных в ячейках при группировании
  /// </summary>
  private bool _allowCustomGroupValues = true;
  /// <summary>
  /// Запретить отложенное обновление статуса компонентов и вложенных закладок
  /// </summary>
  private bool _disableDelayedUpdates;
  /// <summary>Запретить показ статус-бара</summary>
  private bool _disableStatusBar;
  /// <summary>Запретить показ панели управления</summary>
  private bool _disableToolBar;
  /// <summary>
  /// Запретить показ колонки с информацией о взятом на изменение объекте
  /// </summary>
  private bool _disableCheckedOutColumn;
  /// <summary>
  /// Префикс к имени потока, сохраняющего настройки колонок грида
  /// </summary>
  private string _stateStreamPrefix = string.Empty;
  /// <summary>Запретить сортировку в колонках</summary>
  private bool _disableColumnsSorting;
  /// <summary>Запретить группировку в колонках</summary>
  private bool _disableColumnsGrouping;
  /// <summary>Строк перед сортировкой</summary>
  private int _rowsBeforeSort;
  /// <summary>Фокус и выделенные записи перед сортировкой</summary>
  private iFocusAndSelection _stateBeforeSort;
  /// <summary>Происходит ли группировка</summary>
  private bool _grouping;
  /// <summary>Количество группирующих строк</summary>
  protected internal int _groupRowsCount;
  /// <summary>Количество свёрнутых в группы строк</summary>
  internal int _collapsedRowsCount;
  private iFocusAndSelection _stateBeforeGroup;
  /// <summary>Нажата ли клавиша "Return" ("Enter")</summary>
  private bool _returnKeyPressed;
  /// <summary>Ячейка, которая используется для хинта</summary>
  private iGCell _hintCell;
  /// <summary>Узел, который используется для хинта</summary>
  private INodeID _hintNodeID;
  /// <summary>Ячейка заголовка, которая используется для хинта</summary>
  private iGColHdr _hintHeader;
  /// <summary>Колонка, которая используется для хинта</summary>
  private int _hintColumn = -1;
  /// <summary>Текст подсказки</summary>
  private string _hintText = string.Empty;
  /// <summary>Контейнер сервисов контекстного меню закладки</summary>
  private IServiceContainer _menuServiceContainer;
  /// <summary>
  /// Флажок, позволяющий запретить рассылку событий об изменении выделенных элементов в гриде
  /// </summary>
  private bool _disableSelectedItemsChanged;
  /// <summary>
  /// Класс для проверки выделенных элементов на возможность выполнения определённых команд
  /// </summary>
  private CheckInOutCommandsProvider _checkInOutCommandsProvider;
  /// <summary>Сколько записей требуется считать</summary>
  private int _fetchCount = -2;
  /// <summary>Фокус и выделенные записи перед обновлением</summary>
  private iFocusAndSelection _refreshState;
  /// <summary>
  /// Значение true означает то, что грид выполняет чтение информации из источника (метод ReloadItems)
  /// </summary>
  private bool _reloadingItems;
  /// <summary>
  /// Флажок позволяет запретить восстановление состояния в гриде (отмеченные записи)
  /// </summary>
  internal bool _disableRestoreState;
  /// <summary>
  /// Значение true означает то, что грид выполняет чтение информации из источника (метод InternalReloadItems)
  /// </summary>
  private bool _internalReloadingItems;
  /// <summary>
  /// Значение true означает то, что грид читает очередную порцию данных (метод InternalFetchItems)
  /// </summary>
  private bool _internalFetchingItems;
  /// <summary>
  /// Можно ли искать унаследованные настройки отображения "Навигатора" для закладки
  /// </summary>
  protected bool _useInheritedNavViews = true;
  /// <summary>
  /// 
  /// </summary>
  protected ContentType _viewContextType = ContentType.Folders | ContentType.NonFolders;
  private bool _isInheritedNavigatorColumns;
  private LazyService<IConfigurationOptionRepository> _configurationOptionRepository = new LazyService<IConfigurationOptionRepository>();
  private System.IServiceProvider _currentContextMenuServiceProvider;
  private CommandsTable _currentCommandsTable;
  private static MenuButtonItem _toggleEditingModeMenuButtonItem;
  private bool _allowEditing;
  private double _splitterPosition;
  private ChildrenViewOldSearchSelectionFeature _childrenViewOldSearchSelectionFeature;
  private ChildrenView.ChildrenViewSelectedItemsHostFeature _childrenViewSelectedItemsHostFeature;
  private ChildrenViewRowGroupTextMaker _childrenViewRowGroupTextMaker;
  private bool _showContextVersions;
  private bool _isActive;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Intermech.Bars.ToolBar _toolBar;
  private Panel _embeddedViewsPanel;
  protected DropDownMenuItem _embeddedViewsDropDownMenuItem;
  protected StatusStrip _statusStrip;
  protected ToolStripStatusLabel _readedRecordsCountToolStripStatusLabel;
  protected ToolStripStatusLabel _selectedRecordsCountToolStripStatusLabel;
  protected ToolStripDropDownButton _readNextToolStripDropDownButton;
  protected ButtonItem _toggleManualSortingButtonItem;
  protected internal TenTec.Windows.iGridLib.iGrid _grid;
  protected ToolTip _toolTip;
  protected ButtonItem _collapseAllGroupsButtonItem;
  protected ButtonItem _expandAllGroupsButtonItem;
  protected PageViewsManager _pageViewsManager;
  protected Timer _delayedUpdateTimer;
  protected Timer _postProcessTimer;
  protected ToolStripStatusLabel _checkedOutByToolStripStatusLabel;
  protected ToolStripStatusLabel _objectCaptionToolStripStatusLabel;
  protected ToolStripDropDownButton _readAllToolStripDropDownButton;
  protected ToolStripStatusLabel _groupsCountToolStripStatusLabel;
  private ToolStripStatusLabel labelDivider2;
  private ToolStripStatusLabel labelDivider3;
  private ToolStripStatusLabel labelDivider4;
  private ToolStripStatusLabel labelDivider5;
  private CollapsibleSplitter _embeddedViewsCollapsibleSplitter;
  protected ComboBoxItem _filtersComboBoxItem;
  protected ButtonItem _manualSortingSetupButtonItem;
  protected ButtonItem _toggleGroupingButtonItem;
  protected ButtonItem _refreshButtonItem;
  protected MenuBar _gridHeaderMenuBar;
  protected ContextMenuBarItem _gridHeaderContextMenuBarItem;
  protected MenuButtonItem _changeGridColumnsMenuButtonItem;
  protected ButtonItem _collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem;
  protected PictureBox _pictureBox;
  protected ButtonItem _currentVersionsRuleButtonItem;
  private ToolStripStatusLabel _objectTypeNameToolStripStatusLabel;
  private ToolStripStatusLabel labelDivider6;
  private ComboBoxItem _searchComboBoxItem;
  private ButtonItem _searchButtonItem;
  private ButtonItem _changeSearchSettingsButtonItem;
  private ButtonItem _clearSearchResultsButtonItem;
  protected ButtonItem _editingModeButtonItem;
  private DropDownMenuItem _refreshFiltersDropDownMenuItem;
  private MenuButtonItem _createCommonFilterMenuButtonItem;
  private MenuButtonItem _createPersonalFilterMenuButtonItem;
  private MenuButtonItem _removeFilterMenuButtonItem;
  private MenuButtonItem _filterCardMenuButtonItem;
  protected ButtonItem buttonHeightSet;
  private ImageList _mainImageList;
  private ChildrenViewEditingComponent _childrenViewEditingComponent;
  private ChildrenViewObjectListFiltersComponent _objectListFiltersComponent;
  private ButtonItem _showContextVersionsButtonItem;
  private ButtonItem _cancelSearchButtonItem;
  private ChildrenViewSearchComponent _searchComponent;
  private ChildrenViewAutoCompleteSearchComponent _autoCompleteSearchComponent;
  private ChildrenViewSimilarCharacterHighlightingComponent _similarCharacterHighlightingComponent;

  public ChildrenView()
  {
    this.InitializeComponent();
    this._grid.LostFocus += new EventHandler(this.Grid_LostFocus);
    this._searchComponent.Attach(this);
    this._autoCompleteSearchComponent.Attach(this);
    if (ChildrenView._toggleEditingModeMenuButtonItem == null)
    {
      ChildrenView._toggleEditingModeMenuButtonItem = new MenuButtonItem("Режим редактирования", new EventHandler(ChildrenView.ToggleEditingModeMenuButtonItem_Click));
      ChildrenView._toggleEditingModeMenuButtonItem.Shortcut = Shortcut.CtrlShiftF2;
      ChildrenView._toggleEditingModeMenuButtonItem.ShortcutActive = true;
      ChildrenView._toggleEditingModeMenuButtonItem.Visible = false;
      if (ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service)
      {
        MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
        {
          ChildrenView._toggleEditingModeMenuButtonItem
        };
        service.RegisterMenuItems(MainMenuItemSite.ViewMiddle, MainMenuItemPosition.Default, menuButtonItemArray);
      }
    }
    this._commandsProvider = this.GetCommandsProvider();
    this._objectListFiltration = new ObjectListFiltration(this);
    this._childrenViewOldSearchSelectionFeature = new ChildrenViewOldSearchSelectionFeature(this);
    this._childrenViewSelectedItemsHostFeature = new ChildrenView.ChildrenViewSelectedItemsHostFeature(this);
    this._childrenViewRowGroupTextMaker = new ChildrenViewRowGroupTextMaker(this);
    this._pageViewsManager.Padding = Padding.Empty;
    this._services = new AdvancedServiceContainer();
    this._services.AddService(typeof (ChildrenView), (object) this);
    this.Options = ChildrenViewOptions.ShowSetColumnsCommand;
    this._embeddedViewsPanel.Visible = false;
    this._embeddedViewsCollapsibleSplitter.Visible = false;
    this._embeddedViewsState = ChildrenView.EmbeddedViewsState.InvalidSize | ChildrenView.EmbeddedViewsState.InvalidData;
    this._embeddedViewsDropDownMenuItem.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_511");
    this._grid.SelectionMode = iGSelectionMode.MultiExtended;
    this._grid.RowMode = true;
    this.Grid.AutoWidthColMode = iGAutoWidthColMode.HeaderAndCells;
    this._painterDictionary = new HybridDictionary();
    this.SetPainters(this._painterDictionary);
    this._childrenViewContextSearchManager = new ChildrenViewContextSearchManager(this);
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service1)
    {
      service1.RendererChanged += new EventHandler(this.BarManager_RendererChanged);
      this.BarManager_RendererChanged((object) service1, EventArgs.Empty);
    }
    this._services.AddService(typeof (IObjectListFiltration), (object) this._objectListFiltration);
    this._services.AddService(typeof (IReportView), (object) this);
    this._embeddedViewsDropDownMenuItem.ImageIndex = -1;
    this._embeddedViewsDropDownMenuItem.Icon = (Icon) null;
    this._embeddedViewsDropDownMenuItem.ImageIndex = 11;
    this._embeddedViewsDropDownMenuItem.Checked = false;
    this._embeddedViewsDropDownMenuItem.Text = LocalizationHolder.rm.GetString("Client.Core_1394");
    this._changeGridColumnsMenuButtonItem.Image = Holder.NamedImageList != null ? Holder.NamedImageList.ImageList.Images[Holder.NamedImageList.ImageIndex("imgViewSettings")] : this._changeGridColumnsMenuButtonItem.Image;
    this.ApplySettings();
    this.UpdateStatusbar();
    this.UpdateToolbar();
    this.AllowEditing = true;
    this._childrenViewEditingComponent.Control = (System.Windows.Forms.Control) this;
    this._childrenViewEditingComponent.AttributePropertyDescriberService = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;
    this._childrenViewEditingComponent.NotificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._objectListFiltersComponent.ChildrenView = this;
    this._similarCharacterHighlightingComponent.ChildrenView = this;
  }

  /// <summary>
  /// Событие возникает, когда грид может показать пользовательское контекстное меню
  /// </summary>
  [CustomDescription("Attribute.Client.Core_97")]
  public event EventHandler<ContextMenuEventArgs> ShowCustomContextMenu;

  /// <summary>
  /// Событие возникает после того, как в гриде изменился порядок сортировки/группировки колонок
  /// </summary>
  [CustomDescription("Attribute.Client.Core_98")]
  public event EventHandler SortingGroupingChanged;

  /// <summary>
  /// Событие возникает, когда пользователь завершает drag'n'drop в гриде
  /// </summary>
  [CustomDescription("Attribute.Client.Core_99")]
  public event EventHandler<DragEventArgs> GridDragDrop;

  /// <summary>Событие возникает при изменении данных в гриде</summary>
  [CustomDescription("Attribute.Client.Core_100")]
  public event EventHandler<DataHelperEventArgs> OnDataTableChangedDelegate;

  /// <summary>
  /// Событие возникает, когда грид перестраивает коллекцию своих колонок и требуется переименование колонок
  /// </summary>
  [CustomDescription("Attribute.Client.Core_235")]
  public event NodeColumnRenameEventHandler OnNodeColumnRename;

  internal ComboBox SearchComboBox => this._searchComboBoxItem.ComboBox;

  public ComboBoxItem SearchComboBoxItem => this._searchComboBoxItem;

  public ButtonItem SearchButtonItem => this._searchButtonItem;

  public ButtonItem ChangeSearchSettingsButtonItem => this._changeSearchSettingsButtonItem;

  public ButtonItem ClearSearchResultsButtonItem => this._clearSearchResultsButtonItem;

  public ButtonItem CancelSearchButtonItem => this._cancelSearchButtonItem;

  public ChildrenViewSearchComponent SearchComponent => this._searchComponent;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Guid SelectedFilterGuid
  {
    get => this._objectListFiltersComponent.SelectedFilter.Guid;
    set => this._objectListFiltersComponent.SelectFilter(value);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsGlobalIndexSearchActived { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public GlobalIndexSearchValue GlobalIndexSearchValue { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool FilterByCurrentVersionsRule
  {
    get
    {
      return this._currentVersionsRuleButtonItem.Visible && this._currentVersionsRuleButtonItem.Enabled && this._currentVersionsRuleButtonItem.Checked;
    }
    set => this._currentVersionsRuleButtonItem.Checked = value;
  }

  public PageViewsManager PageViewsManager => this._pageViewsManager;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool AllowEditing
  {
    get => this._allowEditing;
    set
    {
      if (this._allowEditing == value)
        return;
      this._allowEditing = value;
      ChildrenView._toggleEditingModeMenuButtonItem.Visible = this._allowEditing;
      this._editingModeButtonItem.Visible = this._allowEditing;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool EditingMode
  {
    get => this._editingMode;
    set
    {
      if (this._editingMode == value)
        return;
      this._editingMode = value;
      this._editingModeButtonItem.Checked = this._editingMode;
      this._childrenViewEditingComponent.Enabled = this._editingMode;
      this._grid.Invalidate();
    }
  }

  /// <summary>Контейнер сервисов</summary>
  public AdvancedServiceContainer Services
  {
    [DebuggerStepThrough] get => this._services;
  }

  /// <summary>Ссылка на встроенный грид</summary>
  [Category("Appearance")]
  [Browsable(true)]
  public TenTec.Windows.iGridLib.iGrid Grid
  {
    [DebuggerStepThrough] get => this._grid;
  }

  /// <summary>
  /// Запретить отображать кнопки в ячейках, содержащих значения многозначных атрибутов
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_233")]
  public bool DisableMultiValuesAttrButton { get; set; }

  /// <summary>
  /// Запретить команды по настройке отображения и сбросу этих настроек
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_234")]
  public bool DisableColumnsSettings { get; set; }

  /// <summary>Запретить контекстный поиск в гриде</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_81")]
  public bool DisableContextSearch
  {
    [DebuggerStepThrough] get => this._disableContextSearch;
    set
    {
      this._disableContextSearch = value;
      if (this._disableContextSearch)
        this._childrenViewContextSearchManager = (ChildrenViewContextSearchManager) null;
      else
        this._childrenViewContextSearchManager = new ChildrenViewContextSearchManager(this);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool BlockUISettingsDisableChildrenViewGrouping { get; set; }

  /// <summary>Запретить появляться панели группировки</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_82")]
  public virtual bool DisableGroupBox
  {
    [DebuggerStepThrough] get => this._disableGroupBox;
    set
    {
      this._disableGroupBox = value;
      this._grid.GroupBox.Visible = !this._disableGroupBox;
      if (this._disableSaveGroupBox)
        return;
      INavWindowSettings service = this._services != null ? this._services.GetService(typeof (INavWindowSettings)) as INavWindowSettings : (INavWindowSettings) null;
      if (service == null)
        return;
      string key = $"{"7DE281F6CD74456B8E1060967E0FCBF9"}_{this.Name}";
      service[(object) key] = (object) this.DisableGroupBox;
    }
  }

  /// <summary>
  /// Разрешить гриду особую обработку некоторых типов данных в ячейках при группировании
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  public bool AllowCustomGroupValues
  {
    [DebuggerStepThrough] get => this._allowCustomGroupValues;
    set
    {
      this._allowCustomGroupValues = value;
      this.SetColumns(this.GetNodeColumns(), true);
    }
  }

  /// <summary>
  /// Запретить появляться контекстному меню IMClient-а (если компоненту в дизайнере ручками назначается другое меню)
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_84")]
  public bool DisableIMContextMenu { get; set; }

  /// <summary>Запретить появляться контекстному меню на заголовке</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  public bool DisableHeaderContextMenu { get; set; }

  /// <summary>Значение true запрещает показ статусной строки</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_85")]
  public bool DisableStatusBar
  {
    [DebuggerStepThrough] get => this._disableStatusBar;
    set
    {
      this._disableStatusBar = value;
      this._statusStrip.Visible = !this._disableStatusBar;
    }
  }

  /// <summary>
  /// Значение true запрещает показ панели управления (в верхней части компонента)
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_86")]
  public bool DisableToolBar
  {
    [DebuggerStepThrough] get => this._disableToolBar;
    set
    {
      this._disableToolBar = value;
      this._toolBar.Visible = !this._disableToolBar;
    }
  }

  /// <summary>
  /// Запретить показ колонки с информацией о взятом на изменение объекте
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  public bool DisableCheckedOutColumn
  {
    [DebuggerStepThrough] get => this._disableCheckedOutColumn || !UISettings.ShowGridChkoutColumn;
    set
    {
      this._disableCheckedOutColumn = value;
      this.SetColumns(this.GetNodeColumns(), true);
    }
  }

  /// <summary>
  /// Префикс к имени потока, сохраняющего настройки колонок грида
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CustomDescription("Attribute.Client.Core_87")]
  public virtual string StateStreamPrefix
  {
    [DebuggerStepThrough] get => this._stateStreamPrefix;
    set => this._stateStreamPrefix = value;
  }

  /// <summary>Опции закладки</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ChildrenViewOptions Options { get; set; }

  /// <summary>Запретить сортировку в колонках</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_88")]
  public virtual bool DisableColumnsSorting
  {
    [DebuggerStepThrough] get => this._disableColumnsSorting;
    set
    {
      this._disableColumnsSorting = value;
      this.SetColumns(this.GetNodeColumns(), this.reloadGridOnDisableColumnsSortingOrGrouping);
    }
  }

  /// <summary>Запретить группировку в колонках</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_89")]
  public virtual bool DisableColumnsGrouping
  {
    [DebuggerStepThrough] get => this._disableColumnsGrouping;
    set
    {
      this._disableColumnsGrouping = value;
      this._toggleGroupingButtonItem.Checked = !this._disableColumnsGrouping;
      this._collapseAllGroupsButtonItem.Visible = !this._disableColumnsGrouping;
      this._expandAllGroupsButtonItem.Visible = !this._disableColumnsGrouping;
      this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem.Visible = !this._disableColumnsGrouping;
      this._grid.GroupBox.Visible = !this._disableColumnsGrouping;
      this.SetColumns(this.GetNodeColumns(), this.reloadGridOnDisableColumnsSortingOrGrouping);
    }
  }

  /// <summary>
  /// Запретить генерацию событий IIOEvent типа "IOEventTypes.evKeyDown"
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(true)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_92")]
  public bool DisableKeyDownEvents { get; set; }

  /// <summary>
  /// Запретить генерацию событий IIOEvent типа "IOEventTypes.evKeyUp"
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_93")]
  public bool DisableKeyUpEvents { get; set; }

  /// <summary>
  /// Запретить генерацию событий IIOEvent типа "IOEventTypes.evDoubleClick"
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_94")]
  public bool DisableDoubleClicks { get; set; }

  /// <summary>Запретить пакетное чтение в гриде</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_95")]
  public virtual bool DisablePacketsReading { get; set; }

  /// <summary>
  /// Запретить добавление родительских элементов в коллекцию SelectedItems для контекстных меню
  /// в случае, если в гриде нет своих выделенных элементов
  /// </summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_96")]
  public bool DisableParentSelectedItems { get; set; }

  public event EventHandler DisableFiltrationChanged;

  /// <summary>Запретить фильтрацию списков объектов</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  public virtual bool DisableFiltration
  {
    [DebuggerStepThrough] get => this._disableFiltration;
    set
    {
      if (this._disableFiltration == value)
        return;
      this._disableFiltration = value;
      this._objectListFiltersComponent.IsAttached = this.IsFiltrationEnabled();
      this.UpdateCurrentVersionsRuleButtonItem();
      this.UpdateShowContextVersionsButtonItem();
      EventHandler filtrationChanged = this.DisableFiltrationChanged;
      if (filtrationChanged == null)
        return;
      filtrationChanged((object) this, EventArgs.Empty);
    }
  }

  /// <summary>Запретить включать комманду "ручная сортировка" для элементов содержимого грида
  /// Антон: Для грида вообще совершенно бессмысленная комманда, т.к. настраивает сортировку состава отображаемых элементов,
  /// который в гриде не виден. Но дабы вдруг чего не поломать делаю настройку, возможность вообще убрать эту ф-ию обсужу
  /// с Протащиком когда он выйдет из отпуска</summary>
  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_292")]
  public bool DisableManualSortingSetup { get; set; }

  /// <summary>
  /// Высота вложенных закладок (в процентном соотношении).
  /// После изменения данного свойства высота закладок будет меняться,
  /// только если они открыты.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private bool EMVOpened
  {
    [DebuggerStepThrough] get => this._embeddedViewsOpened;
    set
    {
      if (this._embeddedViewsOpened == value)
        return;
      if (this._embeddedViewsOpened)
        this.OpenEmbeddedViews();
      else
        this.CloseEmbeddedViews();
    }
  }

  /// <summary>
  /// Высота вложенных закладок (в пикселях).
  /// После изменения данного свойства высота закладок будет меняться,
  /// только если они открыты.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int EMVAbsHeight
  {
    [DebuggerStepThrough] get => this._embeddedViewsPanel.Height;
    set
    {
      if (this.EMVAbsHeight == value)
        return;
      int orContainerHeight = this.GetHeightOrContainerHeight();
      if (value < orContainerHeight)
        this._embeddedViewsPanel.Height = value;
      else
        this._embeddedViewsPanel.Height = orContainerHeight / 2;
      this.SetSplitterPosition();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ISelectedItemsHost SelectedItemsHost
  {
    get => (ISelectedItemsHost) this._childrenViewSelectedItemsHostFeature;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ComboBoxItem FiltersComboBoxItem => this._filtersComboBoxItem;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DropDownMenuItem RefreshFiltersDropDownMenuItem => this._refreshFiltersDropDownMenuItem;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public MenuButtonItem CreateCommonFilterMenuButtonItem => this._createCommonFilterMenuButtonItem;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public MenuButtonItem CreatePersonalFilterMenuButtonItem
  {
    get => this._createPersonalFilterMenuButtonItem;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public MenuButtonItem FilterCardMenuButtonItem => this._filterCardMenuButtonItem;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public MenuButtonItem RemoveFilterMenuButtonItem => this._removeFilterMenuButtonItem;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ShowContextVersions
  {
    get => this._showContextVersions;
    set
    {
      if (this._showContextVersions == value)
        return;
      this._showContextVersions = value;
      this._showContextVersionsButtonItem.Checked = this._showContextVersions;
      if (this.Services == null)
        return;
      ObjectsSelectionOptionsHolder service = this.Services.GetService(typeof (ObjectsSelectionOptionsHolder)) as ObjectsSelectionOptionsHolder;
      if (this._showContextVersions)
      {
        if (service != null)
        {
          if (!service.Options.HasFlag((Enum) ObjectsSelectionOptions.ShowAllModifications))
          {
            this.Services.RemoveService(typeof (ObjectsSelectionOptionsHolder));
            this.Services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(service.Options | ObjectsSelectionOptions.ShowAllModifications));
          }
        }
        else
          this.Services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications));
      }
      else if (service != null)
      {
        this.Services.RemoveService(typeof (ObjectsSelectionOptionsHolder));
        this.Services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(service.Options & ~ObjectsSelectionOptions.ShowAllModifications));
      }
      this.ReloadItems();
    }
  }

  /// <summary>
  /// Контрол или его родители находятся в DesignMode
  /// <remarks>
  /// Для вложенных контролов свойство DesignMode не всегда актуально.
  /// Родительская форма может быть в DesignMode, а многоуровневые вложенные дочерние контролы нет.
  /// При этом обращение к сервисам и БД нельзя использовать...
  /// IsDesignerHosted проверяет режим DesignMode в общем, а не только конкретное состояние контрола
  /// </remarks>
  /// </summary>
  public virtual iFocusAndSelection FocusAndSelection
  {
    get => this.GridGetFocusAndSelection();
    set
    {
      this.GridSetFocusAndSelection(value, true);
      this.CollapseAllGroupsExceptGroupsWithSelections();
    }
  }

  public virtual iFocusAndSelection FullFocusAndSelection
  {
    get => this.GridGetFullFocusAndSelection();
    set
    {
      this.GridSetFullFocusAndSelection(value, true);
      this.CollapseAllGroupsExceptGroupsWithSelections();
    }
  }

  public virtual void Reload(iFocusAndSelection state)
  {
    this.ReloadItems();
    if (state == null)
      return;
    this.FullFocusAndSelection = state;
  }

  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_80")]
  public bool DisableAutoselectFirstRow { get; set; }

  [Category("Appearance")]
  [DefaultValue(false)]
  [Browsable(true)]
  [CustomDescription("Attribute.Client.Core_83")]
  public bool DisableDelayedUpdates
  {
    get
    {
      IDisableDelayedUpdates service = this._services != null ? this._services.GetService(typeof (IDisableDelayedUpdates)) as IDisableDelayedUpdates : (IDisableDelayedUpdates) null;
      if (this._disableDelayedUpdates)
        return true;
      return service != null && service.Disabled;
    }
    set => this._disableDelayedUpdates = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ReadedRecordCount => this._dataAdapter.ReadedRecordCount;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string SearchText
  {
    get => this._searchComboBoxItem.ComboBox.Text;
    set => this._searchComboBoxItem.ComboBox.Text = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsSearchActive
  {
    get => this._clearSearchResultsButtonItem.Visible;
    set
    {
      if (value)
        this._searchButtonItem.PerformClick();
      else
        this._clearSearchResultsButtonItem.PerformClick();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ChildrenViewDataAdapter DataAdapter => this._dataAdapter;

  public void SuppressReloadItems() => this._isReloadItemsSuppressed = true;

  public void ResumeReloadItems() => this._isReloadItemsSuppressed = false;

  public virtual void HideHint() => this.GridCancelHint();

  public virtual XmlDocument GetState()
  {
    XmlDocument xmlDoc = new XmlDocument();
    XmlNode element = (XmlNode) xmlDoc.CreateElement(nameof (ChildrenView));
    element.AppendChild(this.GridXMLGetViewState(xmlDoc));
    element.AppendChild(this.GridXMLGetEmbeddedViewsState(xmlDoc));
    xmlDoc.AppendChild((XmlNode) xmlDoc.CreateXmlDeclaration("1.0", (string) null, (string) null));
    xmlDoc.AppendChild(element);
    return xmlDoc;
  }

  public virtual void RestoreState(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return;
    XmlNode xmlNode = xmlDoc.SelectSingleNode("/ChildrenView");
    if (xmlNode == null)
      return;
    this.GridXMLRestoreViewState(xmlNode.SelectSingleNode("State"));
    this.GridXMLRestoreEmbeddedViewsState(xmlNode.SelectSingleNode("EmbeddedViews"));
  }

  public virtual void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    this._readAllMode = false;
    this.Initialize(items.GetParentPath(0), (INode) items.GetItemData(0, typeof (INode)), items.GetItemID(0), services);
  }

  public event EventHandler Activated;

  public virtual void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this._childrenViewOldSearchSelectionFeature.Activate();
    INavWindowSettings service = this._services != null ? this._services.GetService(typeof (INavWindowSettings)) as INavWindowSettings : (INavWindowSettings) null;
    if (service != null)
    {
      string key = $"{"7DE281F6CD74456B8E1060967E0FCBF9"}_{this.Name}";
      object obj = service[(object) key];
      if (obj != null && obj is bool flag)
        this.DisableGroupBox = flag;
    }
    this._notificationService = this._notificationService == null ? this._services.GetService(typeof (INotificationService)) as INotificationService : this._notificationService;
    if (this._notificationService != null && this._notifyHandler == null)
    {
      this._notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
      this._notificationService.Subscribe(this._notifyHandler);
    }
    this.SetCurrentVersionsRuleButtonItem();
    this.UpdateCurrentVersionsRuleButtonItem();
    this.UpdateShowContextVersionsButtonItem();
    NodeColumnCollection nodeColumns = this.GetNodeColumns();
    if (this._dataAdapter != null && nodeColumns.Count == 0 || !this._dataLoaded)
    {
      bool selectedItemsChanged = this._disableSelectedItemsChanged;
      try
      {
        this._disableSelectedItemsChanged = true;
        this.GridLoadState((Stream) null);
      }
      finally
      {
        this._disableSelectedItemsChanged = selectedItemsChanged;
        if (this._readDataOnActivate)
          this.InternalFetchItems();
      }
    }
    if (this._gridStatusesPainter != null)
      this._gridStatusesPainter.Node = this.Node;
    this._isActive = true;
    EventHandler activated = this.Activated;
    if (activated == null)
      return;
    activated((object) this, EventArgs.Empty);
  }

  private void SetCurrentVersionsRuleButtonItem()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._currentVersionsRuleButtonItem.Checked = sessionKeeper.Session.Configurations.ReadBool("Core", "ObjectLists", "VersionsRule", false, DBConfigMode.UserOnly);
  }

  public virtual void Deactivate(IView nextView)
  {
    this._pageViewsManager.SaveChanges(nextView != null);
    if (!this._isColumnsObsoleted)
      this.GridSaveState((Stream) null);
    this._isActive = false;
  }

  public virtual string Caption => LocalizationHolder.rm.GetString("Client.Core_480");

  public virtual int ImageIndex
  {
    [DebuggerStepThrough] get => -1;
  }

  public virtual int OrderID
  {
    [DebuggerStepThrough] get => 20;
  }

  public void SetSelectedForRow(iGRow row, bool selected)
  {
    this._grid.SelectionChanged -= new EventHandler(this.Grid_SelectionChanged);
    try
    {
      row.SetSelectedForAllCells(selected);
      this.Grid_SelectionChanged((object) this._grid, EventArgs.Empty);
    }
    finally
    {
      this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
    }
  }

  bool IEmbeddedViews.IsOpen
  {
    get => (this._embeddedViewsState & ChildrenView.EmbeddedViewsState.Open) != 0;
  }

  public iFocusAndSelection EmbeddedFocusAndSelection
  {
    get
    {
      if (!((IEmbeddedViews) this).IsOpen)
        return (iFocusAndSelection) null;
      IViewPage activeViewPage = this._pageViewsManager.ActiveViewPage;
      if (activeViewPage?.View is IAdvancedView view)
        return view.FullFocusAndSelection;
      if (activeViewPage == null)
        return (iFocusAndSelection) null;
      return new iFocusAndSelection((INodeID) null, (List<INodeID>) null, -1, (List<int>) null, activeViewPage.Name, (iFocusAndSelection) null)
      {
        ChildrenViewHeight = this._embeddedViewsCollapsibleSplitter.SplitPosition
      };
    }
    set
    {
      if (!((IEmbeddedViews) this).IsOpen || value == null)
        return;
      IAdvancedView view = this._pageViewsManager.ActiveViewPage != null ? this._pageViewsManager.ActiveViewPage.View as IAdvancedView : (IAdvancedView) null;
      if (view == null)
        return;
      view.FullFocusAndSelection = value;
    }
  }

  public virtual void OpenEmbeddedViews()
  {
    iGCell curCell = this._grid.CurCell;
    iGRow row = (curCell != null || this._grid.SelectedCells.Count <= 0 ? curCell : this._grid.SelectedCells[0])?.Row;
    this.OpenEmbeddedViews(this.CalculateEmbeddedViewsHeight());
    row?.EnsureVisible();
  }

  public virtual void OpenEmbeddedViews(int height)
  {
    if (this._dataAdapter == null || (this._embeddedViewsState & ChildrenView.EmbeddedViewsState.Open) == ChildrenView.EmbeddedViewsState.Open)
      return;
    this._embeddedViewsCollapsibleSplitter.Visible = true;
    this._embeddedViewsPanel.Visible = true;
    this._embeddedViewsState |= ChildrenView.EmbeddedViewsState.Open;
    this._embeddedViewsDropDownMenuItem.Checked = true;
    if (!this.EMVOpened)
    {
      this.EMVAbsHeight = height;
      this._embeddedViewsState &= ~ChildrenView.EmbeddedViewsState.InvalidSize;
    }
    if ((this._embeddedViewsState & ChildrenView.EmbeddedViewsState.InvalidData) != ChildrenView.EmbeddedViewsState.None)
    {
      this.UpdateEmbeddedViews();
      this._embeddedViewsState &= ~ChildrenView.EmbeddedViewsState.InvalidData;
    }
    this._embeddedViewsOpened = true;
  }

  public virtual void CloseEmbeddedViews()
  {
    this._embeddedViewsOpened = false;
    this._embeddedViewsState = ChildrenView.EmbeddedViewsState.InvalidData;
    this._pageViewsManager.ActiveViewPageChanged -= new EventHandler(this.PageViewsManager_ActiveViewPageChanged);
    this._pageViewsManager.CloseViews();
    this._embeddedViewsPanel.Visible = false;
    this._embeddedViewsCollapsibleSplitter.Visible = false;
    this._embeddedViewsDropDownMenuItem.ImageIndex = -1;
    this._embeddedViewsDropDownMenuItem.Icon = (Icon) null;
    this._embeddedViewsDropDownMenuItem.ImageIndex = 11;
    this._embeddedViewsDropDownMenuItem.Checked = false;
    this._embeddedViewsDropDownMenuItem.Text = LocalizationHolder.rm.GetString("Client.Core_1394");
    this.UpdateEmbeddedViewsContols();
  }

  void IViewData.Refresh() => this.ReloadItems();

  public virtual bool QueryStatus(ICommandState commandState)
  {
    this._disableCloneSelectedItems = true;
    try
    {
      if (commandState == null || commandState.Sender != null)
        return false;
      this._checkInOutCommandsProvider = this._checkInOutCommandsProvider == null ? new CheckInOutCommandsProvider() : this._checkInOutCommandsProvider;
      string[] source = new string[5]
      {
        "Delete",
        "Exclude",
        "Copy",
        "Cut",
        "Paste"
      };
      if ((this._embeddedViewsState & ChildrenView.EmbeddedViewsState.Open) != ChildrenView.EmbeddedViewsState.None && !this.Focused && !this._grid.Focused && this._pageViewsManager.ActiveViewPage != null && (!this._searchComboBoxItem.ComboBox.Focused && !this._childrenViewEditingComponent.IsEditorVisible || !((IEnumerable<string>) source).Contains<string>(commandState.CommandName)))
        return this.QueryStatusEmbedded(commandState);
      ViewStateFlags viewStateFlags = this._services.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.ReadOnly;
      switch (commandState.CommandName)
      {
        case "AdminCancelChanges":
          this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetMenuServiceContainer());
          commandState.Enabled = this._checkInOutCommandsProvider.AllowAdminCancel;
          return true;
        case "CancelChanges":
          this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetMenuServiceContainer());
          commandState.Enabled = this._checkInOutCommandsProvider.AllowCancel;
          return true;
        case "CheckIn":
          this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetMenuServiceContainer());
          commandState.Enabled = this._checkInOutCommandsProvider.AllowCheckIn;
          return true;
        case "CheckOut":
          this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetMenuServiceContainer());
          commandState.Enabled = this._checkInOutCommandsProvider.AllowCheckOut;
          return true;
        case "Copy":
          if (this._childrenViewEditingComponent.IsEditorVisible)
            return false;
          bool flag1 = true;
          for (int index = 0; index < this.SelectedItems.Count; ++index)
          {
            if (this.SelectedItems.GetItemData(index, typeof (IDBObjectID)) == null)
            {
              flag1 = false;
              break;
            }
          }
          commandState.Enabled = this.SelectedItems.Count > 0 & flag1 && this._grid.Focused;
          return true;
        case "Cut":
          if (this._searchComboBoxItem.ComboBox.Focused || this._childrenViewEditingComponent.IsEditorVisible)
            return false;
          bool flag2 = true;
          for (int index = 0; index < this.SelectedItems.Count; ++index)
          {
            if (this.SelectedItems.GetParentData(index, typeof (IDBObjectID)) == null)
            {
              flag2 = false;
              break;
            }
          }
          commandState.Enabled = this.SelectedItems.Count > 0 & flag2 && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None;
          return true;
        case "Delete":
          if (this._searchComboBoxItem.ComboBox.Focused || this._childrenViewEditingComponent.IsEditorVisible)
            return false;
          commandState.Enabled = ContextCommandProvider.CanDeleteObjects(this.SelectedItems, (System.IServiceProvider) this._services);
          return true;
        case "Exclude":
          if (this._searchComboBoxItem.ComboBox.Focused || this._childrenViewEditingComponent.IsEditorVisible)
            return false;
          IDBRelationID itemData = this.SelectedItems.Count > 0 ? this.SelectedItems.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID : (IDBRelationID) null;
          commandState.Enabled = this.SelectedItems.Count > 0 && itemData != null && itemData.Value != -1L && !this.SelectedItems.IsCollage && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None;
          return true;
        case "FetchTree":
          commandState.Enabled = !this.Eof;
          this._readNextToolStripDropDownButton.Enabled = commandState.Enabled;
          this._readAllToolStripDropDownButton.Enabled = !OptimizationSettings.HideNavigatorReadAllButton && this._readNextToolStripDropDownButton.Enabled;
          this._readNextToolStripDropDownButton.Visible = this._readNextToolStripDropDownButton.Enabled;
          this._readAllToolStripDropDownButton.Visible = !OptimizationSettings.HideNavigatorReadAllButton && this._readAllToolStripDropDownButton.Enabled;
          this._readNextToolStripDropDownButton.ToolTipText = this._readNextToolStripDropDownButton.Enabled ? LocalizationHolder.rm.GetString("Client.Core_513") : string.Empty;
          this._readAllToolStripDropDownButton.ToolTipText = this._readAllToolStripDropDownButton.Enabled ? LocalizationHolder.rm.GetString("Client.Core_514") : string.Empty;
          return true;
        case "Find":
          return this.QueryStatus("NavigatorContextSearch");
        case "ParametersCard":
          commandState.Enabled = this.SelectedItems.Count == 1 && (viewStateFlags & ViewStateFlags.InParametersCard) == ViewStateFlags.None;
          return true;
        case "Paste":
          if (this._searchComboBoxItem.ComboBox.Focused || this._childrenViewEditingComponent.IsEditorVisible)
            return false;
          bool flag3 = ((IClipboard) ServicesManager.GetService(typeof (IClipboard))).GetDataObject() is IDBObjectTypedIDCollection dataObject && dataObject.Count > 0 && (viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None && this.Grid.Focused;
          commandState.Enabled = flag3;
          return this.Grid.Focused;
        case "Print":
        case "PrintDocument":
        case "ViewDocument":
          commandState.Enabled = this.SelectedItems.Count > 0 && (viewStateFlags & ViewStateFlags.InDialog) == ViewStateFlags.None;
          return true;
        case "Refresh":
          commandState.Enabled = true;
          return true;
        case "SaveChanges":
          this._checkInOutCommandsProvider.CheckSelectedItems(this.SelectedItems, (System.IServiceProvider) this.GetMenuServiceContainer());
          commandState.Enabled = this._checkInOutCommandsProvider.AllowSave;
          return true;
        default:
          return this.QueryStatusCurrentContext(commandState);
      }
    }
    finally
    {
      this._disableCloneSelectedItems = false;
    }
  }

  private bool QueryStatus(string commandName)
  {
    return !string.IsNullOrEmpty(commandName) && this._currentCommandsTable != null && this._currentCommandsTable.Contains(commandName) && this._currentContextMenuServiceProvider != null;
  }

  public virtual bool Execute(ICommandState commandState)
  {
    return (this._embeddedViewsState & ChildrenView.EmbeddedViewsState.Open) != ChildrenView.EmbeddedViewsState.None && !this.Focused && !this._grid.Focused && this._pageViewsManager.ActiveViewPage != null ? this.ExecuteEmbedded(commandState) : this.Execute(commandState.CommandName);
  }

  public bool Execute(string commandName)
  {
    switch (commandName)
    {
      case "AdminCancelChanges":
      case "CancelChanges":
      case "CheckIn":
      case "CheckOut":
      case "ParametersCard":
      case "Print":
      case "PrintDocument":
      case "SaveChanges":
      case "ViewDocument":
        if (commandName == "Print")
          commandName = "PrintDocument";
        return this.ExecuteMenuCommand(commandName);
      case "Copy":
      case "Cut":
      case "Delete":
      case "Exclude":
      case "Paste":
        return !this._searchComboBoxItem.ComboBox.Focused && !this._childrenViewEditingComponent.IsEditorVisible && this.ExecuteMenuCommand(commandName);
      case "FetchTree":
        this.FetchItems();
        return true;
      case "Find":
        return this.ExecuteMenuCommand("NavigatorContextSearch");
      case "Refresh":
        this._rowsBeforeSort = this._grid.Rows.Count;
        this.ReloadItems();
        return true;
      case null:
        throw new ArgumentNullException(nameof (commandName));
      default:
        return this.ExecuteMenuCommand(commandName);
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual ISelectedItems SelectedItems
  {
    get
    {
      if (this._gridSelectedItems == null)
        this._gridSelectedItems = new ChildrenViewSelectedItems(this._path, this.Node, this);
      return this._disableCloneSelectedItems ? (ISelectedItems) this._gridSelectedItems : (ISelectedItems) this._gridSelectedItems.Clone();
    }
  }

  public event EventHandler SelectedItemsChanged;

  int INodeView.Count => (int) this._dataAdapter.ReadedRecordCount;

  INodeID INodeView.this[int index] => this._dataAdapter[index];

  void INodeView.Append(NodeIDCollection partialNodeIDs)
  {
    List<string> stringList = new List<string>();
    foreach (iGRow row in (IEnumerable) this._grid.Rows)
    {
      if ((row.Type == iGRowType.AutoGroupRow || row.Type == iGRowType.ManualGroupRow) && !row.Expanded && row.RowTextCell != null)
        stringList.Add(row.RowTextCell.Text);
    }
    try
    {
      this._dataAdapter.Append(partialNodeIDs, this._grid.Rows.Count == 0 && this._grid.SelectedCells.Count == 0 && this._stateBeforeSort == null && this._stateBeforeGroup == null && this._refreshState == null && !this._childrenViewOldSearchSelectionFeature.Enabled);
    }
    finally
    {
      foreach (iGRow row in (IEnumerable) this._grid.Rows)
      {
        if ((row.Type == iGRowType.AutoGroupRow || row.Type == iGRowType.ManualGroupRow) && row.RowTextCell != null && row.RowTextCell.Text != null && stringList.Contains(row.RowTextCell.Text))
          row.Expanded = false;
      }
    }
    if (this.DisableDelayedUpdates)
    {
      this.DelayedUpdateTimer_Tick((object) this, (EventArgs) null);
    }
    else
    {
      this._delayedUpdateTimer.Stop();
      this._delayedUpdateTimer.Start();
    }
  }

  void INodeView.Update(IList indexes)
  {
    bool flag = false;
    List<INodeID> selectedNodeIds = this.SelectedNodeIDs;
    for (int index = 0; index < indexes.Count; ++index)
    {
      INodeID nodeId = this._dataAdapter[index];
      if (!flag && selectedNodeIds.IndexOf(nodeId) >= 0)
      {
        flag = true;
        break;
      }
    }
    try
    {
      this._dataAdapter.Update(indexes);
    }
    finally
    {
      if (flag)
      {
        this._gridSelectedItems.Invalidate();
        if (this.DisableDelayedUpdates)
        {
          this.DelayedUpdateTimer_Tick((object) this, (EventArgs) null);
        }
        else
        {
          this._delayedUpdateTimer.Stop();
          this._delayedUpdateTimer.Start();
        }
      }
    }
  }

  void INodeView.Replace(IList indexes, NodeIDCollection replacementNodeIDs)
  {
    try
    {
      this._dataAdapter.Replace(indexes, replacementNodeIDs);
    }
    finally
    {
      if (this._grid.CurCell != null)
        this._grid.CurCell.Row.EnsureVisible();
      this._gridSelectedItems.Invalidate();
      if (this.DisableDelayedUpdates)
      {
        this.DelayedUpdateTimer_Tick((object) this, (EventArgs) null);
      }
      else
      {
        this._delayedUpdateTimer.Stop();
        this._delayedUpdateTimer.Start();
      }
    }
  }

  void INodeView.Remove(IList indexes)
  {
    this._dataAdapter.Remove(indexes);
    this._gridSelectedItems.Invalidate();
    if (!this._disableSelectedItemsChanged && this.SelectedItemsChanged != null)
      this.SelectedItemsChanged((object) this, EventArgs.Empty);
    if (this.DisableDelayedUpdates)
    {
      this.DelayedUpdateTimer_Tick((object) this, (EventArgs) null);
    }
    else
    {
      this._delayedUpdateTimer.Stop();
      this._delayedUpdateTimer.Start();
    }
  }

  public object Control
  {
    get => (object) this;
    set
    {
    }
  }

  System.IServiceProvider IIOSource.Services
  {
    get => (System.IServiceProvider) this.GetMenuServiceContainer();
    set
    {
    }
  }

  ISelectedItems IIOSource.SelectedItems
  {
    get => this.SelectedItems;
    set
    {
    }
  }

  public virtual INodeQuery NodeQuery
  {
    get => this.Node == null ? (INodeQuery) null : this.Node.GetQuery(this.ViewContentType);
  }

  public event EventHandler CurrentColumnChanged;

  string INavigatorContextSearch.CurrentColumnText
  {
    get => iGridExtensions.GetCurrentColumnText(this._grid);
  }

  IEnumerable<Tuple<int, int, string>> INavigatorContextSearch.GetCellValues(
    bool currentColumnOnly,
    bool fromBeggining,
    bool backward)
  {
    return iGridExtensions.GetCellValues(this._grid, currentColumnOnly, fromBeggining, backward);
  }

  void INavigatorContextSearch.SelectCells(Tuple<int, int>[] cells)
  {
    iGridExtensions.SelectCells(this._grid, cells);
  }

  public string GetSelectedItemsText(
    SelectedItemsTextOptions options,
    string cellsSeparator,
    string rowsSeparator)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (options == SelectedItemsTextOptions.None && cellsSeparator == string.Empty && rowsSeparator == string.Empty)
    {
      string str = this._grid.CurCell.Value == null || this._grid.CurCell.Value == DBNull.Value || !(this._grid.CurCell.Value.GetType() != typeof (byte[])) ? string.Empty : this._grid.CurCell.Value.ToString();
      stringBuilder.Append(str);
      return stringBuilder.ToString();
    }
    if ((options & SelectedItemsTextOptions.ColumnsCaptions) == SelectedItemsTextOptions.ColumnsCaptions || (options & SelectedItemsTextOptions.ColumnsCaptions) == SelectedItemsTextOptions.ColumnsCaptionsOnly)
    {
      NodeColumnCollection nodeColumns = this.GetNodeColumns();
      for (int index = 0; index < nodeColumns.Count; ++index)
      {
        if (!(nodeColumns[index].Key == "Special_StateImage") && !(nodeColumns[index].Key == "Special_CheckedOut"))
        {
          stringBuilder.Append(nodeColumns[index].Caption);
          if (index < nodeColumns.Count - 1)
            stringBuilder.Append(cellsSeparator);
        }
      }
      if ((options & SelectedItemsTextOptions.ColumnsCaptions) == SelectedItemsTextOptions.ColumnsCaptionsOnly)
        return stringBuilder.ToString();
    }
    if (this._grid.SelectedCells == null || this._grid.SelectedCells.Count == 0)
      return stringBuilder.ToString();
    List<int> intList = new List<int>();
    for (int index = 0; index < this._grid.SelectedCells.Count; ++index)
    {
      int rowIndex = this._grid.SelectedCells[index].RowIndex;
      if (intList.IndexOf(rowIndex) < 0)
        intList.Add(rowIndex);
    }
    intList.Sort();
    bool flag = true;
    for (int index = 0; index < intList.Count && (index <= 0 || (options & SelectedItemsTextOptions.FirstItemOnly) != SelectedItemsTextOptions.FirstItemOnly); ++index)
    {
      if (index > 0 && stringBuilder.Length > 0)
        stringBuilder.Append(rowsSeparator);
      iGRow row = this._grid.Rows[intList[index]];
      for (int colIndex = 0; colIndex < row.Cells.Count; ++colIndex)
      {
        iGCell cell = row.Cells[colIndex];
        if (!(cell.ColKey == "Special_StateImage") && !(cell.ColKey == "Special_CheckedOut"))
        {
          string str = cell.Value == null || cell.Value == DBNull.Value || !(cell.Value.GetType() != typeof (byte[])) ? string.Empty : cell.Value.ToString();
          if (flag)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(rowsSeparator);
            flag = false;
          }
          if (colIndex > 0)
            stringBuilder.Append(cellsSeparator);
          stringBuilder.Append(str);
        }
      }
    }
    return stringBuilder.ToString();
  }

  public void RemoveNodeColumn(NodeColumn nodeColumn)
  {
    iGCol iGcol = this._grid.Cols.Cast<iGCol>().FirstOrDefault<iGCol>((System.Func<iGCol, bool>) (o => o.Tag == nodeColumn));
    if (iGcol == null)
      return;
    this._grid.Cols.RemoveAt(iGcol.Key);
  }

  public NodeColumnCollection GetNodeColumns()
  {
    NodeColumnCollection nodeColumns = new NodeColumnCollection();
    foreach (iGCol iGcol in (IEnumerable<iGCol>) this._grid.Cols.Cast<iGCol>().OrderBy<iGCol, int>((System.Func<iGCol, int>) (o => o.Order)))
    {
      if (iGcol.Tag is NodeColumn)
        nodeColumns.Add((NodeColumn) iGcol.Tag);
    }
    return nodeColumns;
  }

  public NodeColumnCollection GetSpecialNodeColumns()
  {
    NodeColumnCollection specialNodeColumns = new NodeColumnCollection();
    if (this._grid.Cols["Special_StateImage"] != null)
    {
      NodeColumn nodeColumn = new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE, typeof (int), FieldTypes.ftSystem, string.Empty, ColumnContents.String);
      int index = this._grid.SortObject.IndexOf("Special_StateImage");
      if (index >= 0)
      {
        iGSortItem iGsortItem = this._grid.SortObject[index];
        nodeColumn.SortIndex = iGsortItem.Index;
        nodeColumn.SortOrder = iGsortItem.Index >= 0 ? iGsortItem.SortOrder.ConvertToNodeColumnSortOrder() : NodeColumnSortOrder.None;
      }
      specialNodeColumns.Add(nodeColumn);
    }
    return specialNodeColumns;
  }

  public ChildrenViewCellData GetCellData(int rowIndex, int columnIndex)
  {
    return this._grid.Rows[rowIndex].Tag is ChildrenViewRowData tag1 && this._grid.Cols[columnIndex].Tag is NodeColumn tag2 && tag1.CellDataDictionary.ContainsKey(tag2.Key) ? tag1.CellDataDictionary[tag2.Key] : (ChildrenViewCellData) null;
  }

  public ChildrenViewCellData GetCellData(iGCell cell)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    return this.GetCellData(cell.RowIndex, cell.ColIndex);
  }

  public ChildrenViewRowData GetRowData(int rowIndex)
  {
    return this._grid.Rows[rowIndex].Tag as ChildrenViewRowData;
  }

  public ChildrenViewRowData GetRowData(iGRow row)
  {
    return row != null ? row.Tag as ChildrenViewRowData : throw new ArgumentNullException(nameof (row));
  }

  public NodeColumn GetNodeColumn(int columnIndex)
  {
    return this._grid.Cols[columnIndex].Tag as NodeColumn;
  }

  public iGRow GetRowWithNodeID(INodeID nodeID)
  {
    if (nodeID == null)
      throw new ArgumentNullException(nameof (nodeID));
    return this._grid.Rows.Cast<iGRow>().FirstOrDefault<iGRow>((System.Func<iGRow, bool>) (o => o.Tag is ChildrenViewRowData && object.Equals((object) ((ChildrenViewRowData) o.Tag).NodeID, (object) nodeID)));
  }

  public iGRow GetRowWithNodeID(int nodeIDIndex)
  {
    return nodeIDIndex >= 0 ? this.GetRowWithNodeID(this._dataAdapter[nodeIDIndex]) : throw new ArgumentException();
  }

  public INodeID GetNodeIDForRow(int rowIndex)
  {
    if (rowIndex < 0)
      throw new ArgumentException();
    return this.GetNodeIDForRow(this._grid.Rows[rowIndex]);
  }

  public INodeID GetNodeIDForRow(iGRow row)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    return !(row.Tag is ChildrenViewRowData) ? (INodeID) null : ((ChildrenViewRowData) row.Tag).NodeID;
  }

  protected virtual ICommandsProvider GetCommandsProvider()
  {
    return (ICommandsProvider) new ChildrenViewCommandsProvider(this);
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="rootDescriptor">Описание корневого узла</param>
  /// <param name="services">Контейнер сервисов</param>
  public virtual void Initialize(IDescriptor rootDescriptor, System.IServiceProvider services)
  {
    this._readAllMode = false;
    NodeIDPath parentPath = new NodeIDPath(rootDescriptor);
    INode parentNode = (INode) new EtherealNode(rootDescriptor);
    INodeQuery query = parentNode.GetQuery(ContentType.Folders);
    if (query == null)
      return;
    query.Execute((object) null, 1);
    INodeID recordNodeId = query.GetRecordNodeID(0);
    this.Initialize(parentPath, parentNode, recordNodeId, services);
  }

  /// <summary>Инициализировать закладку</summary>
  /// <param name="parentPath">Путь к родительскому узлу</param>
  /// <param name="parentNode">Родительский узел</param>
  /// <param name="nodeId">Коревой узел</param>
  /// <param name="services">Контейнер сервисов</param>
  public virtual void Initialize(
    NodeIDPath parentPath,
    INode parentNode,
    INodeID nodeId,
    System.IServiceProvider services)
  {
    this._readAllMode = false;
    if (services != this._services)
      this._services.AdvancedProvider = services;
    this._ioDispatcher = this._services.GetService(typeof (IIODispatcher)) as IIODispatcher;
    if (this._ioDispatcher == null)
      this._ioDispatcher = ServicesManager.GetService(typeof (IIODispatcher)) as IIODispatcher;
    this._categoryTypeStateImageService = ServicesManager.GetService(typeof (ICategoryTypeStateImageService)) as ICategoryTypeStateImageService;
    this._categoryTypeIconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._navGraphicsCache.UIColorsSchemeChanged -= new EventHandler(this.NavGraphicsCache_UIColorsSchemeChanged);
    this._navGraphicsCache.UIColorsSchemeChanged += new EventHandler(this.NavGraphicsCache_UIColorsSchemeChanged);
    this._currentUserAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    if (this._navigatorColumnsService != null)
      this._navigatorColumnsService.ColumnsChanged -= new EventHandler<NavigatorColumnsChangedEventArgs>(this.NavigatorColumnsService_ColumnsChanged);
    this._navigatorColumnsService = ServicesManager.GetService(typeof (INavigatorColumnsService)) as INavigatorColumnsService;
    this._navigatorColumnsService.ColumnsChanged += new EventHandler<NavigatorColumnsChangedEventArgs>(this.NavigatorColumnsService_ColumnsChanged);
    this._grid.BackColor = this._navGraphicsCache.CurrentColorsScheme.Background;
    this._grid.ForeColor = this._navGraphicsCache.CurrentColorsScheme.Foreground;
    this._grid.HighlightBackColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelected;
    this._grid.HighlightForeColor = this._navGraphicsCache.CurrentColorsScheme.ForegroundSelected;
    this._grid.HighlightBackColorNoFocus = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelectedInactive;
    this._grid.HighlightForeColorNoFocus = this._navGraphicsCache.CurrentColorsScheme.ForegroundSelectedInactive;
    this._viewState = this._services.GetService(typeof (IViewState)) as IViewState;
    if (this._services.GetService(typeof (SelectionOptionsHolder)) is SelectionOptionsHolder service1)
    {
      this._grid.SelectionMode = (service1.Options & SelectionOptions.DisableMultiselect) == (SelectionOptions) 0 ? iGSelectionMode.MultiExtended : iGSelectionMode.One;
      this.DisableGroupBox = (service1.Options & SelectionOptions.HideViewsGroupingBox) != 0;
      this.DisableToolBar = (service1.Options & SelectionOptions.HideViewsToolbar) != 0;
      this.DisableStatusBar = (service1.Options & SelectionOptions.HideViewsStatusBar) != 0;
    }
    this._notificationService = services.GetService(typeof (INotificationService)) as INotificationService;
    ChildrenView._namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    ChildrenView._userNamesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
    this._parentNode = parentNode;
    this._nodeID = nodeId;
    this._parentSelItem = new NodeItems(parentPath, parentNode, new NodeIDCollection()
    {
      nodeId
    }, services);
    this._path = new NodeIDPath(parentPath, this._nodeID);
    this._node = (INode) null;
    this._dataAdapter = new ChildrenViewDataAdapter(this);
    this._grid.AfterContentsGrouped -= new EventHandler(this.Grid_AfterContentsGrouped);
    this._grid.AfterContentsSorted -= new EventHandler(this.Grid_AfterContentsSorted);
    try
    {
      this._grid.Rows.Clear();
      this._grid.GroupObject.Clear();
      this._grid.SortObject.Clear();
      this._grid.Cols.Clear();
    }
    finally
    {
      this._grid.AfterContentsGrouped += new EventHandler(this.Grid_AfterContentsGrouped);
      this._grid.AfterContentsSorted += new EventHandler(this.Grid_AfterContentsSorted);
    }
    this._toolBar.ImageList = ((INamedImageList) ServicesManager.GetService(typeof (INamedImageList))).ImageList;
    this._pageViewsManager.Services = (System.IServiceProvider) this._services;
    this._gridSelectedItems = new ChildrenViewSelectedItems(this._path, this.Node, this);
    this._dropTargetNodeItems = new NodeItems(this._path, this.Node, new NodeIDCollection(), (System.IServiceProvider) this._services);
    if (this._parentNode != null && this._parentSelItem.Count > 0 && this._parentSelItem.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData && !MetaDataHelper.IsObjectTypeChildOf(itemData.Value, MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545")) && !MetaDataHelper.IsObjectTypeChildOf(itemData.Value, MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545")) && !MetaDataHelper.IsObjectTypeChildOf(itemData.Value, MetaDataHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545")))
      this._readAllMode = this._parentNode is IObjectTypeAndRelationFiltrationSupported;
    INavWindowSettings service2 = this._services != null ? this._services.GetService(typeof (INavWindowSettings)) as INavWindowSettings : (INavWindowSettings) null;
    if (service2 != null)
    {
      string key = $"{"7DE281F6CD74456B8E1060967E0FCBF9"}_{this.Name}";
      object obj = service2[(object) key];
      if (obj != null && obj is bool flag)
        this.DisableGroupBox = flag;
    }
    this._internalReloadingItems = true;
    this._internalFetchingItems = true;
    try
    {
      this.InitializeCurrentVersionsRuleButtonItem();
      this.SetDisableChildrenViewGrouping();
    }
    finally
    {
      this._internalReloadingItems = false;
      this._internalFetchingItems = false;
    }
    ChildrenViewEditingComponent editingComponent = this._childrenViewEditingComponent;
    if (!(this._services.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service3))
      service3 = ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService;
    editingComponent.AttributePropertyDescriberService = service3;
    this._objectListFiltersComponent.CategoryTypeIconService = this.GetService<ICategoryTypeIconService>();
    this._objectListFiltersComponent.CurrentUserAndRole = this.GetService<ICurrentUserAndRole>();
    this._objectListFiltersComponent.ObjectListFiltersClientService = this.GetService<IObjectListFiltersClientService>();
    this._objectListFiltersComponent.NamedImageList = this.GetService<INamedImageList>();
    this._objectListFiltersComponent.NavGraphicsCache = this.GetService<INavGraphicsCache>();
    this._objectListFiltersComponent.IsAttached = this.IsFiltrationEnabled();
    this._objectListFiltersComponent.ParentObjectTypeID = this.GetParentObjectTypeID();
    this.UpdateControls();
  }

  private T GetService<T>() where T : class
  {
    return this._services.GetService(typeof (T)) is T service ? service : ServicesManager.GetService(typeof (T)) as T;
  }

  private int GetParentObjectTypeID()
  {
    IDBObjectTypeID itemData = this._parentSelItem != null ? this._parentSelItem.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID : (IDBObjectTypeID) null;
    return itemData == null ? -1 : itemData.Value;
  }

  /// <summary>Возможность спрятать панель свойств выбранного элемента</summary>
  public void ToggleSplitterState()
  {
    if (!this._embeddedViewsCollapsibleSplitter.Visible)
      return;
    this._embeddedViewsCollapsibleSplitter.ToggleState();
  }

  /// <summary>Контейнер сервисов закладки</summary>
  /// <returns>Контейнер сервисов закладки</returns>
  protected virtual IServiceContainer GetServiceContainer()
  {
    return (IServiceContainer) new AdvancedServiceContainer(this._services.AdvancedProvider);
  }

  /// <summary>
  /// Указать, что контейнер сервисов контекстного меню закладки уже не является корректным
  /// </summary>
  protected virtual void InvalidateMenuServiceContainer()
  {
    this._menuServiceContainer = (IServiceContainer) null;
  }

  /// <summary>Контейнер сервисов контекстного меню закладки</summary>
  /// <returns>Контейнер сервисов контекстного меню закладки</returns>
  protected virtual IServiceContainer GetMenuServiceContainer()
  {
    if (this._menuServiceContainer != null)
      return this._menuServiceContainer;
    IServiceContainer originalMenuServiceContainer = this.GetServiceContainer();
    originalMenuServiceContainer.AddService(typeof (ChildrenView), (object) this);
    originalMenuServiceContainer.AddService(typeof (ICommandsProvider), (object) this._commandsProvider);
    originalMenuServiceContainer.AddService(typeof (ISelectedItemsHost), (object) this);
    originalMenuServiceContainer.AddService(typeof (INavigatorContextSearch), (object) this);
    originalMenuServiceContainer.AddService(typeof (IReportView), (object) this);
    originalMenuServiceContainer.AddService(typeof (IObjectListFiltration), (object) this._objectListFiltration);
    this._menuServiceContainer = originalMenuServiceContainer;
    if (this.OnGetMenuServiceContainer != null)
    {
      originalMenuServiceContainer = this.OnGetMenuServiceContainer((object) this, originalMenuServiceContainer);
    }
    else
    {
      ChildrenView.GetMenuServiceContainerDelegate service = this._services.GetService<ChildrenView.GetMenuServiceContainerDelegate>(false);
      if (service != null)
        originalMenuServiceContainer = service((object) this, originalMenuServiceContainer);
    }
    return originalMenuServiceContainer;
  }

  /// <summary>Вернуть дескриптор для пустого пути</summary>
  /// <returns>Дескриптор для пустого пути</returns>
  protected internal virtual IDescriptor GetEmptyPathDescriptor() => (IDescriptor) null;

  /// <summary>
  /// Коллекция выделенных в закладке в текущий момент времени элементов (список строится заново, без кэширования)
  /// </summary>
  public virtual ISelectedItems CurrentSelectedItems()
  {
    return ChildrenViewSelectedItems.GetSelectedItems(this);
  }

  /// <summary>Вернуть коллекцию выделенных в гриде описаний узлов</summary>
  public virtual List<INodeID> SelectedNodeIDs
  {
    get
    {
      List<INodeID> selectedNodeIds = new List<INodeID>();
      for (int index = 0; index < this._grid.SelectedCells.Count; ++index)
      {
        INodeID nodeIdForRow = this.GetNodeIDForRow(this._grid.SelectedCells[index].Row);
        if (nodeIdForRow != null)
          selectedNodeIds.Add(nodeIdForRow);
      }
      return selectedNodeIds;
    }
  }

  /// <summary>
  /// Вернуть коллекцию выделенных в гриде описаний узлов и их позиции в гриде
  /// </summary>
  public virtual Dictionary<INodeID, int> SelectedPositions
  {
    get
    {
      Dictionary<INodeID, int> selectedPositions = new Dictionary<INodeID, int>();
      for (int index = 0; index < this._grid.SelectedCells.Count; ++index)
      {
        iGCell selectedCell = this._grid.SelectedCells[index];
        iGRow row = selectedCell.Row;
        INodeID nodeIdForRow = this.GetNodeIDForRow(selectedCell.Row);
        if (nodeIdForRow != null)
          selectedPositions.Add(nodeIdForRow, row.Index);
      }
      return selectedPositions;
    }
  }

  /// <summary>Очистить в гриде список выделенных ячеек</summary>
  /// <param name="lockGrid">Блокировать прорисовку в гриде</param>
  protected internal virtual void GridDeselectAll(bool lockGrid)
  {
    try
    {
      if (lockGrid)
      {
        this._grid.BeginUpdate();
        this._grid.Redraw = false;
      }
      this._grid.PerformAction(iGActions.DeselectAll);
      this._grid.CurRow = (iGRow) null;
    }
    finally
    {
      if (lockGrid)
      {
        this._grid.Redraw = true;
        this._grid.EndUpdate();
      }
    }
  }

  /// <summary>
  /// Выделить в гриде указанные узлы, перейти к первому из них
  /// </summary>
  /// <param name="nodeIDs">Список выделяемых узлов</param>
  public void SelectNodes(List<INodeID> nodeIDs)
  {
    this._grid.BeginUpdate();
    try
    {
      this._grid.PerformAction(iGActions.DeselectAll);
      if (nodeIDs == null || nodeIDs.Count == 0)
        return;
      for (int index = 0; index < nodeIDs.Count; ++index)
      {
        iGRow rowWithNodeId = this.GetRowWithNodeID(nodeIDs[index]);
        if (rowWithNodeId != null)
        {
          this.SetSelectedForRow(rowWithNodeId, true);
          if (this._grid.SelectedCells.Count == 1)
          {
            rowWithNodeId.EnsureVisible();
            this._grid.SetCurRow(rowWithNodeId.Index);
          }
        }
      }
    }
    finally
    {
      this._grid.EndUpdate();
    }
  }

  /// <summary>
  /// Выделить в списке элементы с указанными номерами строк
  /// </summary>
  /// <param name="items">Номера строк, которые надо найти в гриде и выделить</param>
  /// <param name="withEvent">true - генерировать уведомления и события об изменении коллекции выделенных элементов в гриде</param>
  public void SelectItems(List<int> items, bool withEvent)
  {
    try
    {
      this.GridBeginUpdate();
      this._grid.PerformAction(iGActions.DeselectAll);
      if (items == null || items.Count == 0)
        return;
      for (int index = items.Count - 1; index >= 0; --index)
      {
        iGRow row = this._grid.Rows[items[index]];
        if (row != null)
          this.SetSelectedForRow(row, true);
        if (index == 0)
        {
          row.EnsureVisible();
          this._grid.CurRow = row;
        }
      }
    }
    finally
    {
      this.GridEndUpdate();
      if (withEvent)
        this.SelectionChanged();
    }
  }

  /// <summary>
  /// Вызвать выполнение указанной команды контекстного меню для указанного события
  /// </summary>
  /// <param name="commandName">Команда контекстного меню</param>
  /// <returns>true, если команда обработана</returns>
  protected bool ExecuteMenuCommand(string commandName)
  {
    if (string.IsNullOrEmpty(commandName) || this._currentCommandsTable == null || !this._currentCommandsTable.Contains(commandName) || this._currentContextMenuServiceProvider == null)
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, this._currentCommandsTable, this._currentContextMenuServiceProvider);
    return true;
  }

  /// <summary>Выполняет команду контекстного меню "Обновить".</summary>
  /// <param name="items">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  protected internal virtual void RefreshViewCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this._rowsBeforeSort = this._grid.Rows.Count;
    this.ReloadItems();
  }

  /// <summary>
  /// Выполняет команду контекстного меню "Сброс настроек отображения"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  public virtual void ResetColumnsCommand(object sender, EventArgs e)
  {
    this.ResetColumnsCommand((ISelectedItems) this._gridSelectedItems, (System.IServiceProvider) this._services, (object) null);
  }

  /// <summary>
  /// Выполняет команду контекстного меню "Сброс настроек отображения"
  /// </summary>
  /// <param name="selectedItems">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public virtual void ResetColumnsCommand(
    ISelectedItems selectedItems,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (this.DisableColumnsSettings || MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1395"), LocalizationHolder.rm.GetString("Client.Core_1396"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    NavigatorColumns navigatorColumns = this._navigatorColumnsService.GetNavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix, false);
    if (navigatorColumns != null)
      this._navigatorColumnsService.RemoveNavigatorColumns(navigatorColumns.Category, navigatorColumns.Type, navigatorColumns.Suffix);
    iFocusAndSelection focusAndSelection = this.GridGetFocusAndSelection();
    try
    {
      this.GridLoadState((Stream) null);
      this.InternalReloadItems();
    }
    finally
    {
      this.GridSetFocusAndSelection(focusAndSelection, true);
    }
  }

  /// <summary>
  /// Выполняет команду контекстного меню "Настройка отображения"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  public virtual void ChangeGridColumnsMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.SetColumnsCommand((ISelectedItems) this._gridSelectedItems, (System.IServiceProvider) this._services, (object) null);
  }

  /// <summary>
  /// Выполняет команду контекстного меню "Настройка отображения"
  /// </summary>
  /// <param name="selectedItems">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  public virtual void SetColumnsCommand(
    ISelectedItems selectedItems,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (this.DisableColumnsSettings)
      return;
    this.GridReflectColumnsProperties();
    NodeColumnCollection nodeColumns = this.GetNodeColumns();
    List<int> groupColumns = this.GridGetGroupColumns(nodeColumns);
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    for (int index = 0; index < groupColumns.Count; ++index)
      columnCollection.Add(nodeColumns[groupColumns[index]].Clone() as NodeColumn);
    NodeColumnCollection supportedColumns = this.GetSupportedColumns();
    if (supportedColumns == null || supportedColumns.Count == 0)
      supportedColumns = new NodeColumnCollection((IEnumerable<NodeColumn>) nodeColumns);
    if (this.ExecuteAppearanceTuningForm(this.Node, this.ViewContentType, supportedColumns, nodeColumns, (object) this._parentNode, (object) this._nodeID) != DialogResult.OK)
      return;
    iFocusAndSelection focusAndSelection = this.GridGetFocusAndSelection();
    try
    {
      if (columnCollection.Count > 0)
      {
        for (int index = columnCollection.Count - 1; index >= 0; --index)
        {
          if (nodeColumns.Find(columnCollection[index].Key) == null)
            columnCollection.RemoveAt(index);
        }
      }
      this.SetColumns(nodeColumns, columnCollection.Count == 0);
      if (columnCollection.Count > 0)
      {
        groupColumns.Clear();
        for (int index = 0; index < columnCollection.Count; ++index)
        {
          NodeColumn nodeColumn = nodeColumns.Find(columnCollection[index].Key);
          groupColumns.Add(nodeColumns.IndexOf(nodeColumn));
        }
        this.GridSetGroups(this.GetNodeColumns(), groupColumns, true);
      }
    }
    finally
    {
      this.GridSetFocusAndSelection(focusAndSelection, true);
    }
    this._isColumnsObsoleted = false;
    this.GridSaveState((Stream) null);
    if (this._navigatorColumnsService == null)
      return;
    this._navigatorColumnsService.SaveToUserConfig();
  }

  /// <summary>Поддерживаемые узлом колонки.</summary>
  /// <returns></returns>
  public virtual NodeColumnCollection GetSupportedColumns()
  {
    return this.Node == null ? new NodeColumnCollection() : this.Node.GetSupportedColumns(this.ViewContentType, string.Empty);
  }

  /// <summary>Вызвать форму "Настройка отображения"</summary>
  /// <param name="node">Узел, для которого вызывается окно настройки отображения</param>
  /// <param name="content">Для какого содержимого вызывается настройка отображения</param>
  /// <param name="supportedColumns">Список всех колонок атрибутов</param>
  /// <param name="columns">Список выбранных колонок атрибутов</param>
  /// <param name="nodeIDs">Элементы, содержимое которых будет получено по настроенным колонкам</param>
  /// <returns>Результат вызова формы как модального окна</returns>
  protected virtual DialogResult ExecuteAppearanceTuningForm(
    INode node,
    ContentType content,
    NodeColumnCollection supportedColumns,
    NodeColumnCollection columns,
    params object[] nodeIDs)
  {
    return AppearanceTuningForm.Execute(this.Node, this.ViewContentType, this.StateStreamPrefix, supportedColumns, columns, (object) this._parentNode, (object) this._nodeID, (object) this._path);
  }

  /// <summary>
  /// Покызывает или скрывает панель с дополнительными видами в зависимости
  /// от ее текущего состояния.
  /// </summary>
  public virtual void ToggleEmbeddedViews()
  {
    if (!this._embeddedViewsState.HasFlag((Enum) ChildrenView.EmbeddedViewsState.Open))
    {
      if (this._splitterPosition < 0.01)
        this._splitterPosition = 0.5;
      this.OpenEmbeddedViews();
    }
    else
      this.CloseEmbeddedViews();
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (this.IsDisposed)
    {
      if (this._notificationService == null || this._notifyHandler == null)
        return;
      this._notificationService.Unsubscribe(this._notifyHandler);
      this._notifyHandler = (NotificationEventHandler) null;
    }
    else if (!this._isActive)
    {
      this._dataLoaded = false;
    }
    else
    {
      if (this._services.GetService(typeof (INotificationServiceStatesHolder)) is INotificationServiceStatesHolder service && ((service.States & NotificationServiceStates.InactiveDialog) == NotificationServiceStates.InactiveDialog || (service.States & NotificationServiceStates.InactiveForm) == NotificationServiceStates.InactiveForm) && (!(e is ICriticalEventArgs criticalEventArgs) || !criticalEventArgs.IsCritical) && !NotificationEventNames.CriticalEventNames.Contains(e.EventName))
        return;
      if (e.EventName == "AttributeRemoved" && e is DBAttributesEventArgs)
      {
        DBAttributesEventArgs attributesEventArgs = (DBAttributesEventArgs) e;
        if (attributesEventArgs.AttributeIDs != null)
        {
          foreach (NodeColumn nodeColumn in (List<NodeColumn>) this.GetNodeColumns())
          {
            if (nodeColumn.Attribute != null && attributesEventArgs.AttributeIDs.Contains(nodeColumn.Attribute.AttributeID))
              this.RemoveNodeColumn(nodeColumn);
          }
        }
      }
      if (e.EventName == "ProjectChanged")
      {
        this.ApplySettings();
        this._rowsBeforeSort = this._grid.Rows.Count;
        this.ReloadItems();
      }
      try
      {
        if (e.EventName == "ToSelectItemsChanges")
        {
          this.RefreshWithToSelectItemsAnalyzers();
        }
        else
        {
          this._grid.BeginUpdate();
          this._grid.Redraw = false;
          NodeViewCapabilities capabilities = new NodeViewCapabilities(this.ViewContentType, this.GetNodeColumns(), true);
          if (this.Node is INodeNotifications node)
          {
            switch (node.Process(e, (object) capabilities.Columns))
            {
              case ProcessResult.RefreshNode:
                this.ReloadItems();
                return;
              case ProcessResult.RefreshNodeAndColumns:
                this.ReloadItems();
                return;
            }
          }
          IUpdateAnalyser analyser = this.Node != null ? this.Node.GetAnalyser(capabilities, sender, e) : (IUpdateAnalyser) null;
          if (analyser != null)
          {
            this._delayedUpdateTimer.Stop();
            UpdateManager.UpdateView((INodeView) this, analyser);
          }
          if (!(e.EventName == "ConfigurationOptionChanged"))
            return;
          this._grid.Invalidate();
        }
      }
      finally
      {
        this._grid.Redraw = true;
        this._grid.EndUpdate();
        if (this._grid.CurRow != null)
          this._grid.CurRow.EnsureVisible();
        if (e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsCheckedOut" || e.EventName == "ObjectsChangesCancelled")
        {
          this._currentContextMenuServiceProvider = (System.IServiceProvider) this.GetMenuServiceContainer();
          this._currentCommandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable((ISelectedItems) this._gridSelectedItems.Clone(), this._currentContextMenuServiceProvider);
        }
      }
    }
  }

  /// <summary>
  /// true - свойство сигнализирует о том, что все записи для списка уже зачитаны
  /// </summary>
  protected virtual bool Eof => this._dataAdapter == null || this._dataAdapter.Eof;

  /// <summary>
  /// Возвращает тип элементов навигации, которые зачитываются и отображаются в гриде.
  /// </summary>
  public virtual ContentType ViewContentType
  {
    [DebuggerStepThrough] get => this._viewContextType;
    [DebuggerStepThrough] set => this._viewContextType = value;
  }

  /// <summary>
  /// Возвращает элемент навигации, чье содержимое отображается в гриде.
  /// </summary>
  public INode Node
  {
    get
    {
      if (this._node == null)
        this._node = this.GetNode();
      return this._node;
    }
  }

  /// <summary>
  /// Создает или получает извне элемент навигации, чье содержимое отображается в гриде.
  /// </summary>
  /// <returns></returns>
  protected virtual INode GetNode()
  {
    if (this._nodeID == null)
      return (INode) null;
    INode node = this._parentNode.GetChild(this._nodeID) ?? (INode) this._parentNode.GetData(this._nodeID, typeof (INode));
    IContextAware contextAware = node as IContextAware;
    IContextAware parentNode = this._parentNode as IContextAware;
    if (contextAware != null)
    {
      AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer((System.IServiceProvider) this._services);
      RelationPair rootObjectKey = this.GetRootObjectKey();
      if (rootObjectKey != null && !rootObjectKey.Empty && rootObjectKey.TOP_OBJECT_ID != 0L)
        serviceContainer.AddService(typeof (RelationPair), (object) rootObjectKey);
      if (parentNode != null)
        serviceContainer.AdvancedProvider = parentNode.Services;
      contextAware.Services = (System.IServiceProvider) serviceContainer;
    }
    return node;
  }

  /// <summary>Возвращает количество записей в пакете данных.</summary>
  protected virtual int FetchCount
  {
    get
    {
      if (this.IsDesignerHosted() || this.DisablePacketsReading || this.Node != null && (this.Node.Options & NodeOptions.CanContainsComposition) == NodeOptions.CanContainsComposition && (this.Node.Options & NodeOptions.CanContainsObjectsList) != NodeOptions.CanContainsObjectsList || this._readAllMode || this._fetchCount != -2)
        return 2147483646;
      if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service)
        return service.MaxRows;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.MaxRows;
    }
  }

  /// <summary>Полностью очистить грид</summary>
  public virtual void Clear() => this.ClearRows();

  /// <summary>Перечитывает содержимое грида.</summary>
  public virtual void ReloadItems(int? count = null)
  {
    if (this._reloadingItems)
      return;
    if (this._isReloadItemsSuppressed)
      return;
    try
    {
      this._reloadingItems = true;
      if (this._dataAdapter == null)
        return;
      this._refreshState = this.GridGetFullFocusAndSelection();
      this._disableRestoreState = false;
      try
      {
        this._toolBar.BeginUpdate();
        this.InternalReloadItems(count);
      }
      finally
      {
        if (!this._disableRestoreState)
          this.GridSetFullFocusAndSelection(this._refreshState, !this._childrenViewOldSearchSelectionFeature.Enabled);
        this._refreshState = (iFocusAndSelection) null;
        this._toolBar.EndUpdate();
      }
    }
    finally
    {
      this._reloadingItems = false;
    }
  }

  /// <summary>Перечитать содержимое грида</summary>
  private void InternalReloadItems(int? count = null)
  {
    if (this.IsDesignerHosted())
      return;
    if (this._internalReloadingItems)
      return;
    try
    {
      this._internalReloadingItems = true;
      this._grid.BeginUpdate();
      this._grid.Redraw = false;
      this._node = (INode) null;
      this.ClearData();
      int fetchCount = this.FetchCount;
      this.FetchRows(count.HasValue ? count.Value : (this._rowsBeforeSort <= 0 || this._rowsBeforeSort <= fetchCount ? fetchCount : this._rowsBeforeSort));
      this._rowsBeforeSort = 0;
    }
    finally
    {
      this._grid.Redraw = true;
      this._grid.EndUpdate();
      this._internalReloadingItems = false;
      this._readNextToolStripDropDownButton.Enabled = !this.Eof;
      this._readAllToolStripDropDownButton.Enabled = !OptimizationSettings.HideNavigatorReadAllButton && this._readNextToolStripDropDownButton.Enabled;
      this.UpdateStatusbar();
      this.UpdateControls();
    }
  }

  /// <summary>Читать очередную порцию данных</summary>
  private void InternalFetchItems()
  {
    if (this.IsDesignerHosted())
      return;
    if (this._internalFetchingItems)
      return;
    try
    {
      this._dataLoaded = false;
      this._internalFetchingItems = true;
      this._grid.BeginUpdate();
      this._grid.Redraw = false;
      this.FetchRows(this.FetchCount);
      this._dataLoaded = true;
    }
    finally
    {
      if (this._grid.Rows.Count > 0 && !this.DisableAutoselectFirstRow && this._grid.SelectedCells.Count == 0)
      {
        this._grid.PerformAction(iGActions.GoFirstRow);
        this._grid.PerformAction(iGActions.CollapseAll);
      }
      this._grid.Redraw = true;
      this._grid.EndUpdate();
      this._internalFetchingItems = false;
      if (this.DisableDelayedUpdates)
      {
        this.DelayedUpdateTimer_Tick((object) this, (EventArgs) null);
      }
      else
      {
        this._delayedUpdateTimer.Stop();
        this._delayedUpdateTimer.Start();
      }
    }
  }

  /// <summary>
  /// Произошло изменение в таблице источника данных для компонента
  /// </summary>
  protected internal virtual void RaiseDataTableChanged()
  {
    if (this.OnDataTableChangedDelegate == null)
      return;
    this.OnDataTableChangedDelegate((object) this, new DataHelperEventArgs((DataTable) null, this.GetNodeColumns()));
  }

  /// <summary>Требуется получение нового имени у колонки Навигатора</summary>
  protected virtual void RaiseNodeColumnRename(NodeColumn column)
  {
    NodeColumnRenameEventArgs e = new NodeColumnRenameEventArgs(column, string.Empty);
    if (this.OnNodeColumnRename != null)
      this.OnNodeColumnRename((object) this, e);
    if (string.IsNullOrEmpty(e.NewName))
      return;
    column.Caption = e.NewName;
  }

  /// <summary>
  /// Поддерживается ли взаимодействие с сервисом IsSimpleSelectedItems
  /// </summary>
  protected internal virtual bool IsSimpleSelectedItemsSuppoted
  {
    get
    {
      return (this._services != null ? this._services.GetService(typeof (IViewState)) as IViewState : (IViewState) null) != null && (this._viewState.ViewState & ViewStateFlags.InDialog) != ViewStateFlags.InDialog && (this._viewState.ViewState & ViewStateFlags.InObjectCreatorDialog) != ViewStateFlags.InObjectCreatorDialog && (this._viewState.ViewState & ViewStateFlags.InParametersCard) != ViewStateFlags.InParametersCard && (this._viewState.ViewState & ViewStateFlags.InSelectionWindow) != ViewStateFlags.InSelectionWindow;
    }
  }

  /// <summary>Отображает контекстное меню для заголовка грида</summary>
  /// <param name="location">Позиция мыши в момент вызова контекстного меню</param>
  /// <returns>true - меню было показано</returns>
  protected virtual bool ShowContextMenu4Header(Point location)
  {
    if (this.Node == null || this.DisableHeaderContextMenu)
      return false;
    this._gridHeaderContextMenuBarItem.Show((System.Windows.Forms.Control) this._grid, location);
    return true;
  }

  /// <summary>Отображает контекстное меню навигатора в гриде.</summary>
  /// <param name="location">Позиция мыши в момент вызова контекстного меню</param>
  /// <returns>true - меню было показано</returns>
  protected virtual bool ShowContextMenu(Point location)
  {
    this.InvalidateMenuServiceContainer();
    bool disableDelayedUpdates = this._disableDelayedUpdates;
    int selectionChanged = this._preventSelectionChanged;
    try
    {
      this._contextMenuActive = true;
      this._disableDelayedUpdates = true;
      this._preventSelectionChanged = 1;
      iGColHdr headerCursor = this.GetHeaderCursor(location);
      iGCell cellCursor = this.GetCellCursor(location);
      iGRow row = cellCursor?.Row;
      int num = cellCursor != null ? cellCursor.ColIndex : -1;
      INodeID nodeAtCursor = this.GetNodeAtCursor(location);
      NodeColumn tag = cellCursor != null ? this._grid.Cols[cellCursor.ColIndex].Tag as NodeColumn : (NodeColumn) null;
      if (headerCursor != null)
        return this.ShowContextMenu4Header(location);
      if (headerCursor == null && cellCursor != null)
      {
        bool flag = false;
        if (!cellCursor.Selected)
        {
          if (row != null && !row.IsAnyCellSelected() && !this._childrenViewOldSearchSelectionFeature.Enabled)
            this.GridDeselectAll(true);
          if (row != null && !this._childrenViewOldSearchSelectionFeature.Enabled)
            this.SetSelectedForRow(row, true);
          flag = true;
        }
        if (this._grid.CurCell != null && (this._grid.CurCell.RowIndex != cellCursor.RowIndex || this._grid.CurCell.ColIndex != cellCursor.ColIndex) && cellCursor.Row != null && (this._grid.CurRow == null || this._grid.CurRow.Index != cellCursor.Row.Index))
        {
          this._grid.CurRow = cellCursor.Row;
          flag = true;
        }
        this._grid.CurCell = cellCursor;
        if (flag)
        {
          this._gridSelectedItems.Invalidate();
          this.SelectionChanged();
        }
      }
      if (headerCursor == null && cellCursor == null && this._grid.CurCell != null && !this._grid.CurCell.Selected)
      {
        row = this._grid.CurCell.Row;
        iGRow curRow = this._grid.CurRow;
        if (row != null && !row.IsAnyCellSelected())
          this.GridDeselectAll(true);
        if (row != null && !this._childrenViewOldSearchSelectionFeature.Enabled)
          this.SetSelectedForRow(row, true);
        this._gridSelectedItems.Invalidate();
        this.SelectionChanged();
      }
      if (this.DisableIMContextMenu)
        return false;
      IServiceContainer serviceContainer = this.GetMenuServiceContainer();
      if (headerCursor == null && cellCursor != null && cellCursor.Value != null && nodeAtCursor != null && num >= 0 && row != null && (this._grid.CurCell != null || this._grid.CurRow != null))
        serviceContainer.AddService(typeof (IFocusedItem), (object) new FocusedItem(tag, nodeAtCursor, this._path, this.Node, (System.IServiceProvider) this._services));
      ISelectedItems itemsForContextMenu = this.GetItemsForContextMenu(location);
      if (this.ShowCustomContextMenu != null)
        this.ShowCustomContextMenu((object) this, new ContextMenuEventArgs(location, (System.Windows.Forms.Control) this._grid));
      MenuBarItem menuBarItem = (MenuBarItem) null;
      if (System.Windows.Forms.Control.ModifierKeys != Keys.Control)
        menuBarItem = Intermech.Navigator.ContextMenu.Services.GetMenuForObjectType(itemsForContextMenu, (System.IServiceProvider) serviceContainer);
      if (menuBarItem == null)
        menuBarItem = Intermech.Navigator.ContextMenu.Services.GetMenu(itemsForContextMenu, (System.IServiceProvider) serviceContainer);
      if (menuBarItem == null)
        return false;
      try
      {
        menuBarItem.Show((System.Windows.Forms.Control) this._grid, location);
        Application.DoEvents();
      }
      finally
      {
        if (this.IsSimpleSelectedItemsSuppoted)
        {
          if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
            ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
          ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) itemsForContextMenu);
        }
      }
      return true;
    }
    finally
    {
      this._preventSelectionChanged = selectionChanged;
      this._contextMenuActive = false;
      this._disableDelayedUpdates = disableDelayedUpdates;
    }
  }

  /// <summary>
  /// Получить итемы, для которых отображаем контекстное меню
  /// </summary>
  /// <returns></returns>
  protected virtual ISelectedItems GetItemsForContextMenu(Point location)
  {
    ISelectedItems itemsForContextMenu = (ISelectedItems) new ChildrenViewSelectedItems(this._path, this.Node, this);
    if (itemsForContextMenu.Count == 0 && !this.DisableParentSelectedItems)
      itemsForContextMenu = (ISelectedItems) this._parentSelItem;
    return itemsForContextMenu;
  }

  /// <summary>
  /// Категория для названия потока с сохранёнными настройками
  /// </summary>
  protected virtual int StateStreamCategoryID
  {
    get => this._path == null || this._path.LastID == null ? 0 : this._path.LastID.CategoryID;
  }

  /// <summary>
  /// Можно ли искать унаследованные настройки отображения "Навигатора" для закладки
  /// </summary>
  protected virtual bool UseInheritedNavViews
  {
    [DebuggerStepThrough] get => this._useInheritedNavViews;
    set => this._useInheritedNavViews = value;
  }

  /// <summary>Тип для названия потока с сохранёнными настройками</summary>
  protected virtual int StateStreamCategoryType
  {
    get => this._path == null || this._path.LastID == null ? 0 : this._path.LastID.TypeID;
  }

  /// <summary>Тип для названия потока с сохранёнными настройками</summary>
  protected virtual string StateStreamCategoryTypeID => this.StateStreamCategoryType.ToString();

  /// <summary>Загрузить настройки грида</summary>
  /// <param name="stateStream">Поток, из которого требуется загрузить состояние грида,
  /// или null, если грузить из потока по умолчанию</param>
  public virtual void GridLoadState(Stream stateStream)
  {
    bool flag = true;
    List<int> groups = new List<int>();
    if (this._navigatorColumnsService == null)
      return;
    NavigatorColumns navigatorColumns = this._navigatorColumnsService.GetNavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix, this.UseInheritedNavViews);
    if (navigatorColumns != null)
      this._isInheritedNavigatorColumns = navigatorColumns.Inherited;
    NodeColumnCollection columns1 = (NodeColumnCollection) null;
    try
    {
      if (this.DisableColumnsSettings)
        navigatorColumns = (NavigatorColumns) null;
      if (navigatorColumns == null || navigatorColumns.Empty || navigatorColumns.Columns == null || navigatorColumns.Columns.Count <= 0)
        return;
      columns1 = navigatorColumns.Columns.Clone() as NodeColumnCollection;
      columns1.RemoveInvalidColumns();
      if (navigatorColumns.Groups != null && !this.DisableGroupBox)
        groups = new List<int>((IEnumerable<int>) navigatorColumns.Groups);
      flag = false;
    }
    finally
    {
      if (flag)
      {
        columns1 = this.Node != null ? this.Node.GetDefaultColumns(this.ViewContentType) : (NodeColumnCollection) null;
        if (columns1 == null || columns1.Count == 0)
        {
          NodeColumnCollection columns2 = new NodeColumnCollection();
          Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(columns2, true, false);
          columns1 = columns2;
        }
        columns1.RemoveInvalidColumns();
        groups.Clear();
      }
      this.GridSetColumns(columns1, false);
      this.GridSetGroups(columns1, groups, false);
    }
  }

  /// <summary>Сохраним состояние грида</summary>
  /// <param name="stateStream">Поток, в который надо сохранять состояние. Если указать null,
  /// грид сохранится в свой стандартный поток</param>
  public virtual void GridSaveState(Stream stateStream, NodeColumnCollection nodeColumns = null)
  {
    if (this.DisableColumnsSettings || this._navigatorColumnsService == null)
      return;
    this._navigatorColumnsService.ColumnsChanged -= new EventHandler<NavigatorColumnsChangedEventArgs>(this.NavigatorColumnsService_ColumnsChanged);
    try
    {
      NavigatorColumns navigatorColumns1 = this._navigatorColumnsService.GetNavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix, this.UseInheritedNavViews) ?? new NavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix);
      NavigatorColumns navigatorColumns2 = new NavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix);
      navigatorColumns2.Columns = nodeColumns ?? this.GetNodeColumns();
      if (nodeColumns == null)
      {
        navigatorColumns2.Groups = this.GridGetGroupColumns(navigatorColumns2.Columns);
      }
      else
      {
        List<int> intList = new List<int>();
        for (int index = 0; index < nodeColumns.OrderBy<NodeColumn, int>((System.Func<NodeColumn, int>) (o => o.GroupIndex)).ToArray<NodeColumn>().Length; ++index)
        {
          if (nodeColumns[index].GroupIndex >= 0)
            intList.Add(index);
        }
        navigatorColumns2.Groups = intList;
      }
      if (navigatorColumns1.Equals((object) navigatorColumns2))
        return;
      this._navigatorColumnsService.CreateNavigatorColumns((NavigatorColumns) navigatorColumns2.Clone());
    }
    finally
    {
      this._navigatorColumnsService.ColumnsChanged += new EventHandler<NavigatorColumnsChangedEventArgs>(this.NavigatorColumnsService_ColumnsChanged);
    }
  }

  /// <summary>
  /// Установить новую коллекцию колонок гриду, перечитать содержимое грида при необходимости
  /// </summary>
  /// <param name="columns">Новая коллекция колонок</param>
  /// <param name="reloadGrid">Перечитать содержимое грида</param>
  public virtual void SetColumns(NodeColumnCollection columns, bool reloadGrid)
  {
    if (this.IsDesignerHosted())
      return;
    NodeColumnCollection columns1 = columns == null || columns.Count <= 0 ? (this.Node != null ? this.Node.GetDefaultColumns(this.ViewContentType) : new NodeColumnCollection()) : columns;
    this.GridSetColumns(columns1, false);
    this.GridSetGroups(columns1, (List<int>) null, reloadGrid);
  }

  /// <summary>
  /// Выполнить синхронизацию состояния грида с источником данных и перечитать грид, если что-то изменилось
  /// </summary>
  protected virtual void GridReloadIfNeed()
  {
    if (!this.GridReflectColumnsProperties())
      return;
    this.InternalFetchItems();
  }

  /// <summary>
  /// Возвращает номера сгруппированных колонок (они будут соответствовать полученному списку колонок)
  /// </summary>
  /// <param name="columns">Коллекция колонок источника данных (изменённая согласно их отображению в гриде)</param>
  /// <returns>Номера сгруппированных колонок</returns>
  protected List<int> GridGetGroupColumns(NodeColumnCollection columns)
  {
    List<int> groupColumns = new List<int>();
    for (int index = 0; index < this._grid.GroupObject.Count; ++index)
    {
      int columnIndex = this.GetColumnIndex(this._grid.Cols[this._grid.GroupObject[index].ColIndex].Key, columns);
      if (columnIndex >= 0)
        groupColumns.Add(columnIndex);
    }
    return groupColumns;
  }

  /// <summary>
  /// Создает колонки в гриде по коллекции колонок навигатора.
  /// </summary>
  /// <param name="columns">Коллекция колонок навигатора</param>
  /// <param name="reloadData">
  /// Признак необходимости перечитать данные в гриде, если новая
  /// коллекция колонок не соответствует отображаемым данным</param>
  protected virtual void GridSetColumns(NodeColumnCollection columns, bool reloadData)
  {
    bool preventSorting = this._preventSorting;
    bool grouping = this._grouping;
    try
    {
      this._grouping = true;
      this._grid.BeginUpdate();
      this._grid.Redraw = false;
      this._preventSorting = true;
      this.ClearData();
      this._grid.Cols.Clear();
      IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
      this.SetPainters(this._painterDictionary);
      for (int index = 0; index < columns.Count; ++index)
      {
        iGCol iGcol = this._grid.Cols.Add();
        if (columns[index] != null && columns[index].AttrType == FieldTypes.ftDateTime)
          iGcol.SortType = iGSortType.ByCustomer;
        iGcol.Key = this.GetColumnKey(columns[index]);
        iGcol.Tag = (object) columns[index];
        this.RaiseNodeColumnRename(columns[index]);
        iGcol.Text = !UISettings.ShowShortAttributeNames ? (object) columns[index].Caption : (object) columns[index].ShortCaption;
        iGcol.Order = index;
        iGcol.Width = columns[index].Width;
        iGcol.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Foreground | iGCustomDrawFlags.Background;
        if (this._painterDictionary[(object) (columns[index].ID.ToString() + ".images")] is IGridColumnImageList)
        {
          iGcol.CellStyle.ImageList = (this._painterDictionary[(object) (columns[index].ID.ToString() + ".images")] as IGridColumnImageList).ImageList;
          iGcol.CellStyle.ImageAlign = iGContentAlignment.MiddleLeft;
          iGcol.CellStyle.TextPosToImage = iGTextPosToImage.Horizontally;
          iGcol.CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
          INodeColumnTransform defaultTransform = service.GetDefaultTransform(columns[index].SchemeGuid, columns[index].ID);
          System.Type type = defaultTransform != null ? defaultTransform.DataType : columns[index].DataType;
          if (type == typeof (int) || type == typeof (long) || type == typeof (double) || type == typeof (DateTime))
            iGcol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
          else
            iGcol.CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
        }
        else
        {
          INodeColumnTransform defaultTransform = service.GetDefaultTransform(columns[index].SchemeGuid, columns[index].ID);
          System.Type type = defaultTransform != null ? defaultTransform.DataType : columns[index].DataType;
          if (type == typeof (int) || type == typeof (long) || type == typeof (double) || type == typeof (DateTime))
            iGcol.CellStyle.TextAlign = iGContentAlignment.MiddleRight;
          else
            iGcol.CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
        }
        if (this.AllowCustomGroupValues && this.GridAllowCustomGroupValues(columns[index]))
          iGcol.CustomGrouping = true;
        if (this.DisableColumnsSorting || columns[index].DataType == typeof (byte[]) || columns[index].DisableSorting)
        {
          columns[index].SortOrder = NodeColumnSortOrder.None;
          columns[index].SortIndex = -1;
          iGcol.AllowGrouping = false;
          iGcol.SortType = iGSortType.None;
        }
        if (this.DisableColumnsGrouping || columns.Count < 2 || columns[index].DisableGrouping)
          iGcol.AllowGrouping = false;
        iGcol.AllowMoving = true;
        iGcol.AllowSizing = true;
        iGcol.CellStyle.ReadOnly = iGBool.True;
        if (columns[index].DataType == typeof (byte[]))
          iGcol.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Foreground | iGCustomDrawFlags.Background;
        IMSAttributeType attribute = iGcol.Tag is NodeColumn tag ? tag.Attribute : (IMSAttributeType) null;
        if (attribute != null && (!this.DisableMultiValuesAttrButton && attribute.MultiValueMode == MultiValueModes.MultiValues || attribute.MultiValueMode == MultiValueModes.MultiValuesFromList))
        {
          iGcol.CellStyle.TypeFlags = iGCellTypeFlags.HasEllipsisBtn;
          iGcol.CellStyle.ReadOnly = iGBool.False;
        }
        iGcol.CellStyle.SingleClickEdit = iGBool.False;
        if (this.EditingMode)
        {
          iGcol.CellStyle.ReadOnly = iGBool.False;
          iGcol.CellStyle.SingleClickEdit = iGBool.True;
          iGcol.CellStyle.TypeFlags = iGCellTypeFlags.None;
        }
      }
      int num1 = 0;
      iGCol iGcol1 = this._grid.Cols.Add();
      iGcol1.Key = "Special_StateImage";
      iGcol1.Text = (object) "       Тип объекта";
      iGcol1.Order = 0;
      iGcol1.Width = 38;
      iGcol1.MinWidth = 38;
      iGcol1.MaxWidth = 38;
      iGcol1.CellStyle = new iGCellStyle();
      iGcol1.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
      iGcol1.CellStyle.Flags = iGCellFlags.DisplayImage;
      iGcol1.CellStyle.ReadOnly = iGBool.True;
      iGcol1.AllowGrouping = true;
      iGcol1.AllowMoving = true;
      iGcol1.AllowSizing = false;
      iGcol1.CustomGrouping = true;
      int num2 = num1 + 1;
      if (!this.DisableCheckedOutColumn)
      {
        iGCol iGcol2 = this._grid.Cols.Add();
        iGcol2.Key = "Special_CheckedOut";
        iGcol2.Text = (object) "";
        iGcol2.Order = 1;
        iGcol2.SortType = iGSortType.None;
        iGcol2.Width = 18;
        iGcol2.MinWidth = 18;
        iGcol2.MaxWidth = 18;
        iGcol2.CellStyle = new iGCellStyle();
        iGcol2.CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
        iGcol2.CellStyle.Flags = iGCellFlags.DisplayImage;
        if (ChildrenView._namedImageList != null)
          iGcol2.CellStyle.ImageList = ChildrenView._namedImageList.ImageList;
        iGcol2.CellStyle.ReadOnly = iGBool.True;
        iGcol2.AllowGrouping = false;
        iGcol2.AllowMoving = false;
        iGcol2.AllowSizing = false;
        ++num2;
      }
      this._grid.FrozenArea.ColCount = num2;
      for (int index = 0; index < this._grid.Cols.Count; ++index)
      {
        iGCol col = this._grid.Cols[index];
        if (col.Tag is NodeColumn tag && this._painterDictionary[(object) (tag.ID.ToString() + ".images")] is IGridColumnImageList)
        {
          IGridColumnImageList painter = this._painterDictionary[(object) (tag.ID.ToString() + ".images")] as IGridColumnImageList;
          col.CellStyle.ImageList = painter.ImageList;
          col.CellStyle.ImageAlign = iGContentAlignment.MiddleLeft;
          col.CellStyle.TextPosToImage = iGTextPosToImage.Horizontally;
          col.CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
          col.CellStyle.Flags = !painter.DrawOnlyIcon ? iGCellFlags.DisplayText | iGCellFlags.DisplayImage : iGCellFlags.DisplayImage;
        }
      }
      NodeColumnCollection sortedColumns = NodeColumnCollection.GetSortedColumns(columns);
      for (int index = 0; index < sortedColumns.Count; ++index)
      {
        NodeColumn column = sortedColumns[index];
        this._grid.SortObject.Add(this.GetColumnKey(column), this.GetSortOrder(column.SortOrder));
      }
      this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.All;
    }
    finally
    {
      this.InvalidateMenuServiceContainer();
      this._preventSorting = preventSorting;
      this._grouping = grouping;
      this._grid.Redraw = true;
      this._grid.EndUpdate();
      if (reloadData)
      {
        this.InternalReloadItems();
        this.SelectionChanged();
      }
    }
  }

  /// <summary>
  /// Создает группы колонок в гриде по указанной коллекции колонок источника данных
  /// </summary>
  /// <param name="columns">Коллекция колонок из источника данных</param>
  /// <param name="groups">Список номеров группированных колонок (согласно указанному списку колонок)</param>
  /// <param name="reloadData">Признак необходимости перечитать данные в гриде</param>
  protected void GridSetGroups(NodeColumnCollection columns, List<int> groups, bool reloadData)
  {
    bool preventSorting = this._preventSorting;
    bool grouping = this._grouping;
    try
    {
      this._grid.BeginUpdate();
      this._grid.Redraw = false;
      this._preventSorting = true;
      this._grouping = true;
      this._grid.GroupObject.Clear();
      if (groups == null)
        return;
      int count = columns != null ? columns.Count : 0;
      if (count == 0)
        return;
      for (int index = 0; index < groups.Count; ++index)
      {
        int group = groups[index];
        if (group >= 0 && group < count)
        {
          iGCol column = this.GridGetColumn(columns[group]);
          if (column != null)
          {
            column.SortOrder = columns[group].SortOrder == NodeColumnSortOrder.Ascending ? iGSortOrder.Ascending : (columns[group].SortOrder == NodeColumnSortOrder.Descending ? iGSortOrder.Descending : iGSortOrder.None);
            if (column.SortOrder != iGSortOrder.None)
            {
              this._grid.GroupObject.Add(column.Index, column.SortOrder);
              columns[group].GroupIndex = index;
            }
          }
        }
      }
    }
    finally
    {
      this.InvalidateMenuServiceContainer();
      this._preventSorting = preventSorting;
      this._grouping = grouping;
      this._grid.Redraw = true;
      this._grid.EndUpdate();
      if (reloadData)
      {
        this.InternalReloadItems();
        this.SelectionChanged();
      }
    }
  }

  /// <summary>
  /// Получить ссылку на узел, находящийся в гриде по указанным координатам
  /// </summary>
  /// <param name="pos">Координаты курсора мыши</param>
  /// <returns>Узел или null</returns>
  public INodeID GetNodeAtCursor(Point pos)
  {
    iGCell iGcell = this._grid.Cells.FromPoint(pos.X, pos.Y);
    return iGcell == null ? (INodeID) null : this.GetNodeIDForRow(iGcell.Row);
  }

  /// <summary>
  /// Получить ссылку на ячейку, находящуюся в гриде по указанным координатам
  /// </summary>
  /// <param name="pos">Координаты курсора мыши</param>
  /// <returns>Ячейка или null</returns>
  protected iGCell GetCellCursor(Point pos) => this._grid.Cells.FromPoint(pos.X, pos.Y);

  /// <summary>Обновить элементы управления</summary>
  protected virtual void UpdateControls()
  {
    this._toggleManualSortingButtonItem.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._embeddedViewsDropDownMenuItem.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._objectListFiltersComponent.IsEnabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._editingModeButtonItem.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._readNextToolStripDropDownButton.Enabled = !this.Eof;
    this._readAllToolStripDropDownButton.Enabled = !OptimizationSettings.HideNavigatorReadAllButton && this._readNextToolStripDropDownButton.Enabled;
    this._readNextToolStripDropDownButton.Visible = this._readNextToolStripDropDownButton.Enabled;
    this._readAllToolStripDropDownButton.Visible = !OptimizationSettings.HideNavigatorReadAllButton && this._readAllToolStripDropDownButton.Enabled;
    this._readNextToolStripDropDownButton.ToolTipText = this._readNextToolStripDropDownButton.Enabled ? LocalizationHolder.rm.GetString("Client.Core_513") : string.Empty;
    this._readAllToolStripDropDownButton.ToolTipText = this._readAllToolStripDropDownButton.Enabled ? LocalizationHolder.rm.GetString("Client.Core_514") : string.Empty;
    this._refreshButtonItem.Enabled = true;
    this._toggleGroupingButtonItem.Checked = !this.DisableGroupBox;
    this._toggleGroupingButtonItem.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._collapseAllGroupsButtonItem.Enabled = this._grid.GroupObject.Count > 0 && this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._expandAllGroupsButtonItem.Enabled = this._collapseAllGroupsButtonItem.Enabled;
    this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem.Enabled = this._collapseAllGroupsButtonItem.Enabled;
    this._manualSortingSetupButtonItem.Enabled = this._parentSelItem != null && this._parentSelItem.Services != null && this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._refreshButtonItem.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._changeGridColumnsMenuButtonItem.Enabled = (this.Options & ChildrenViewOptions.ShowSetColumnsCommand) != (ChildrenViewOptions) 0 && !this.DisableColumnsSettings;
    this._groupsCountToolStripStatusLabel.Visible = this._grid.GroupObject.Count > 0;
    this._childrenViewRowGroupTextMaker.UpdateRowGroupText();
    this.UpdateCurrentVersionsRuleButtonItem();
    this.UpdateShowContextVersionsButtonItem();
    this._grid.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._pageViewsManager.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._statusStrip.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
  }

  /// <summary>Очистить источник данных и грид</summary>
  protected void ClearData()
  {
    this._groupRowsCount = 0;
    this._collapsedRowsCount = 0;
    int num = this._grid.SelectedCells.Count == 0 ? 1 : 0;
    if (this._dataAdapter != null)
      this._dataAdapter.ClearRows();
    this._dataLoaded = false;
    this.UpdateControls();
    this.UpdateLinkedControls();
    this.UpdateStatusbar();
    if (num != 0)
      return;
    this.GridSelectionChanged();
  }

  /// <summary>
  /// Обновляет элементы тулбара, зависящие от выделенных в гриде элементов.
  /// </summary>
  protected virtual void UpdateToolbar()
  {
  }

  /// <summary>Идентификатор ноды сфокусированной записи</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INodeID FocusedNodeID
  {
    get
    {
      iGCell iGcell = this._grid.CurCell ?? (this._grid.SelectedCells.Count > 0 ? this._grid.SelectedCells[0] : (iGCell) null);
      return iGcell == null ? (INodeID) null : this.GetNodeIDForRow(iGcell.Row);
    }
  }

  /// <summary>Сохранить фокус и выделенные узлы</summary>
  /// <returns>Точка восстановления</returns>
  public iFocusAndSelection GridGetFocusAndSelection()
  {
    iGCell curCell = this._grid.CurCell;
    iGCell iGcell = curCell != null || this._grid.SelectedCells.Count <= 0 ? curCell : this._grid.SelectedCells[0];
    INodeID nodeIdForRow1 = iGcell != null ? this.GetNodeIDForRow(iGcell.Row) : (INodeID) null;
    int focusedIndex = iGcell != null ? iGcell.Row.Index : -1;
    List<INodeID> selectedRows = new List<INodeID>();
    List<int> selectedIndexes = new List<int>();
    for (int index = 0; index < this._grid.SelectedCells.Count; ++index)
    {
      iGRow row = this._grid.SelectedCells[index].Row;
      if (row.Type == iGRowType.Normal)
      {
        INodeID nodeIdForRow2 = this.GetNodeIDForRow(row);
        if (nodeIdForRow2 != null)
        {
          selectedRows.Add(nodeIdForRow2);
          if (selectedIndexes.IndexOf(row.Index) < 0)
            selectedIndexes.Add(row.Index);
        }
      }
    }
    selectedIndexes.Sort();
    return new iFocusAndSelection(nodeIdForRow1, selectedRows, focusedIndex, selectedIndexes, string.Empty, (iFocusAndSelection) null)
    {
      ChildrenViewHeight = this.EMVAbsHeight,
      CollapsedGroups = this.GetCollapsedGroups()
    };
  }

  private HashSet<string> GetCollapsedGroups()
  {
    return new HashSet<string>((IEnumerable<string>) this.GetGroups().Where<Tuple<iGRow, string>>((System.Func<Tuple<iGRow, string>, bool>) (o => !o.Item1.Expanded)).Select<Tuple<iGRow, string>, string>((System.Func<Tuple<iGRow, string>, string>) (o => o.Item2)).Distinct<string>().ToArray<string>());
  }

  /// <summary>Восстановить фокус и выделенные записи</summary>
  /// <param name="selectFirst">Выделять первую строку, если нет ни одной выделенной строки</param>
  /// <param name="state">Точка восстановления</param>
  public void GridSetFocusAndSelection(iFocusAndSelection state, bool selectFirst)
  {
    if (this._dataAdapter == null)
      return;
    bool flag1 = this._grid.SelectedCells.Count > 0;
    bool flag2 = false;
    try
    {
      this._grid.Redraw = false;
      this._grid.BeginUpdate();
      if (state == null)
        return;
      this.SetGroupState(state.CollapsedGroups);
      if (state.SelectedRows != null)
      {
        for (int index = 0; index < state.SelectedRows.Count; ++index)
        {
          iGRow rowWithNodeId = this.GetRowWithNodeID(state.SelectedRows[index]);
          if (rowWithNodeId != null)
          {
            if (!flag2)
            {
              this._grid.PerformAction(iGActions.DeselectAll);
              flag2 = true;
            }
            if (rowWithNodeId != null)
              this.SetSelectedForRow(rowWithNodeId, true);
            flag1 = true;
          }
        }
      }
      if (state.FocusedRow == null)
        return;
      iGRow rowWithNodeId1 = this.GetRowWithNodeID(state.FocusedRow);
      if (rowWithNodeId1 == null)
        return;
      if (!flag2)
        this._grid.PerformAction(iGActions.DeselectAll);
      this.GridShowGridRow(rowWithNodeId1);
      flag1 = true;
    }
    finally
    {
      if (!flag1 && this._grid.Rows.Count > 0 && this._grid.GroupObject.Count == 0 && this._stateBeforeSort == null && this._stateBeforeGroup == null && this._grid.SelectedCells.Count == 0 && state != null && state.FocusedIndex >= 0)
      {
        int index = Math.Min(state.FocusedIndex, this._grid.Rows.Count - 1);
        if (index >= 0 && index < this._grid.Rows.Count)
          this.GridShowGridRow(this._grid.Rows[index]);
      }
      if (selectFirst && !flag1 && this._grid.Rows.Count > 0 && this._grid.GroupObject.Count == 0 && this._stateBeforeSort == null && this._stateBeforeGroup == null && !this.DisableAutoselectFirstRow && this._grid.SelectedCells.Count == 0)
        this.GridShowGridRow(this._grid.Rows[0]);
      this._grid.EndUpdate();
      this._grid.Redraw = true;
      this.SelectionChanged();
      if (state != null)
        this.EMVAbsHeight = state.ChildrenViewHeight;
    }
  }

  private void SetGroupState(HashSet<string> collapsedGroupKeys)
  {
    foreach (Tuple<iGRow, string> group in this.GetGroups())
      group.Item1.Expanded = !collapsedGroupKeys.Contains(group.Item2);
  }

  private IEnumerable<Tuple<iGRow, string>> GetGroups()
  {
    Stack<string> stack = new Stack<string>();
    bool groupEnd = false;
    iGRow iGrow = (iGRow) null;
    foreach (iGRow row in (IEnumerable) this._grid.Rows)
    {
      if (this.IsGroup(row))
      {
        if (groupEnd && iGrow != null)
        {
          int num = iGrow.Level - row.Level;
          for (int index = 0; index < num; ++index)
            stack.Pop();
        }
        if (stack.Count > 0)
          stack.Push(stack.Peek() + row.RowTextCell.Text);
        else
          stack.Push(row.RowTextCell.Text);
        yield return new Tuple<iGRow, string>(row, stack.Peek());
      }
      else
        groupEnd = true;
      iGrow = row;
    }
  }

  private bool IsGroup(iGRow row)
  {
    iGRowPattern pattern = row.Pattern;
    return pattern.Type == iGRowType.AutoGroupRow || pattern.Type == iGRowType.ManualGroupRow;
  }

  /// <summary>Грид завершил сортировку</summary>
  protected virtual void AfterContentsSorted()
  {
    this._tryingSorting = true;
    if (this._preventSorting)
      return;
    if (this.GridReflectColumnsProperties() || this._rowsBeforeSort > 0)
    {
      try
      {
        this._grid.BeginUpdate();
        this._grid.Redraw = false;
        this.ReloadItems();
      }
      finally
      {
        this._grid.Redraw = true;
        this._grid.EndUpdate();
        this.GridSetFocusAndSelection(this._stateBeforeSort, true);
        this._stateBeforeSort = (iFocusAndSelection) null;
      }
    }
    this._tryingSorting = false;
    this._isColumnsObsoleted = false;
    this.GridSaveState((Stream) null);
    this.RaiseSortingGroupingChanged();
  }

  /// <summary>Грид выполнил группировку данных</summary>
  protected virtual void AfterContentsGrouped()
  {
    if (this._grouping)
      return;
    try
    {
      if (!this.GridReflectColumnsProperties())
        return;
      try
      {
        this._grouping = true;
        this._grid.BeginUpdate();
        this._grid.Redraw = false;
        this.ClearData();
        this.FetchItems();
      }
      finally
      {
        this._grid.Redraw = true;
        this._grid.EndUpdate();
        this._grouping = false;
      }
    }
    finally
    {
      this.GridSetFocusAndSelection(this._stateBeforeGroup, true);
      this._stateBeforeGroup = (iFocusAndSelection) null;
      if (this._grid.GroupObject.Count == 0)
        this._groupRowsCount = 0;
      this.UpdateControls();
      this._isColumnsObsoleted = false;
      this.GridSaveState((Stream) null);
      this.RaiseSortingGroupingChanged();
    }
  }

  /// <summary>Произошло изменение в ширине колонок</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GridColWidthChanged(object sender, iGColWidthEventArgs e)
  {
    NodeColumn nodeColumn = this.GetNodeColumn(e.ColIndex);
    if (nodeColumn != null)
      nodeColumn.Width = e.Width;
    this.GridReloadIfNeed();
    this._isColumnsObsoleted = false;
    this.GridSaveState((Stream) null);
  }

  /// <summary>Перемещена колонка</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void GridColumnMoved(object sender, iGColHdrEndDragEventArgs e)
  {
    if (this._grid.Cols[e.ColIndex] != null && this._grid.Cols[e.ColIndex].Order != e.NewOrder)
    {
      NodeColumnCollection nodeColumns = new NodeColumnCollection();
      foreach (iGCol iGcol in this._grid.Cols.Cast<iGCol>().Select<iGCol, Tuple<iGCol, int>>((Func<iGCol, int, Tuple<iGCol, int>>) ((o, index) =>
      {
        int num = index == e.ColIndex ? e.NewOrder + (o.Order < e.NewOrder ? 1 : -1) : o.Order;
        return new Tuple<iGCol, int>(o, num);
      })).OrderBy<Tuple<iGCol, int>, int>((System.Func<Tuple<iGCol, int>, int>) (o => o.Item2)).Select<Tuple<iGCol, int>, iGCol>((System.Func<Tuple<iGCol, int>, iGCol>) (o => o.Item1)))
      {
        if (iGcol.Tag is NodeColumn)
          nodeColumns.Add((NodeColumn) iGcol.Tag);
      }
      foreach (NodeColumn nodeColumn in (List<NodeColumn>) nodeColumns)
      {
        iGCol col = this._grid.Cols[nodeColumn.Key];
        if (col != null)
          col.Tag = (object) nodeColumn;
      }
      this._isColumnsObsoleted = false;
      this.GridSaveState((Stream) null, nodeColumns);
    }
    this.GridReloadIfNeed();
  }

  /// <summary>
  /// Назначить службы по отрисовке и спискам изображений.
  /// Внимание! Данный метод вызывается каждый раз при формировании списка колонок в гриде!
  /// Следует проверять коллекцию painters на наличие классов с вашими ключами!
  /// </summary>
  /// <param name="painters">Коллекция служб</param>
  protected virtual void SetPainters(HybridDictionary painters)
  {
    if (painters[(object) "F_STATUSES"] == null)
    {
      this._gridStatusesPainter = new GridStatusesPainter();
      painters.Add((object) "F_STATUSES", (object) this._gridStatusesPainter);
    }
    IDictionary<int, IGridCellDrawing> dictionary = ServiceUtils.GetService<IGridCellDrawingProvider>((object) this.Services, false)?.GetCellDrawings() ?? (IDictionary<int, IGridCellDrawing>) null;
    if (dictionary == null)
      return;
    foreach (KeyValuePair<int, IGridCellDrawing> keyValuePair in (IEnumerable<KeyValuePair<int, IGridCellDrawing>>) dictionary)
    {
      object key = keyValuePair.Value is IGridColumnImageList ? (object) (keyValuePair.Key.ToString() + ".images") : (object) keyValuePair.Key;
      painters[key] = (object) keyValuePair.Value;
    }
  }

  public event EventHandler<ChildrenView.CustomDrawCellTextEventArgs> CustomDrawCellText;

  /// <summary>Пользовательская отрисовка значков в ячейках</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
  {
    if (this._dataAdapter == null || this.Node == null)
      return;
    Region clip = e.Graphics.Clip;
    e.Graphics.SetClip(e.Bounds, CombineMode.Replace);
    try
    {
      iGRow row = e.RowIndex >= 0 ? this._grid.Rows[e.RowIndex] : (iGRow) null;
      iGCell cell1 = this._grid.Cells[e.RowIndex, e.ColIndex];
      iGCol col = row != null ? this._grid.Cols[e.ColIndex] : (iGCol) null;
      Rectangle bounds;
      if (this._grid.FrozenArea.ColCount > 0)
      {
        iGCol iGcol = this._grid.Cols.Cast<iGCol>().FirstOrDefault<iGCol>((System.Func<iGCol, bool>) (o => o.Order == this._grid.FrozenArea.ColCount - 1));
        if (iGcol != null && col.Order >= this._grid.FrozenArea.ColCount && e.Bounds.X < iGcol.X + iGcol.Width)
        {
          Graphics graphics = e.Graphics;
          int x1 = iGcol.X + iGcol.Width;
          bounds = e.Bounds;
          int y = bounds.Y;
          bounds = e.Bounds;
          int width1 = bounds.Width;
          int num1 = iGcol.X + iGcol.Width;
          bounds = e.Bounds;
          int x2 = bounds.X;
          int num2 = num1 - x2;
          int width2 = width1 - num2;
          bounds = e.Bounds;
          int height = bounds.Height;
          Rectangle rect = new Rectangle(x1, y, width2, height);
          graphics.SetClip(rect);
        }
      }
      NodeColumn tag = col != null ? col.Tag as NodeColumn : (NodeColumn) null;
      INodeID nodeIdForRow = row != null ? this.GetNodeIDForRow(e.RowIndex) : (INodeID) null;
      int colIndex = e.ColIndex;
      if (this._grid.Cols[e.ColIndex].Key == "Special_StateImage" && nodeIdForRow != null)
      {
        IImageState data1 = this.Node != null ? (IImageState) this.Node.GetData(nodeIdForRow, typeof (IImageState)) : (IImageState) null;
        if (data1 != null)
        {
          object data2 = data1.Data;
        }
        object state = data1?.State;
        this._categoryTypeStateImageService = this._categoryTypeStateImageService == null ? ServicesManager.GetService(typeof (ICategoryTypeStateImageService)) as ICategoryTypeStateImageService : this._categoryTypeStateImageService;
        Icon categoryTypeIcon = this.GetCategoryTypeIcon(nodeIdForRow, this._node, state);
        if (categoryTypeIcon == null)
          return;
        Graphics graphics = e.Graphics;
        Icon icon = categoryTypeIcon;
        bounds = e.Bounds;
        int x = bounds.Left + 3;
        bounds = e.Bounds;
        int y = bounds.Top + 1;
        graphics.DrawIcon(icon, x, y);
      }
      else
      {
        NodeColumnCollection nodeColumns = this.GetNodeColumns();
        if (colIndex >= 0 && colIndex < nodeColumns.Count && tag != null && col != null && row != null && row.Type == iGRowType.Normal)
        {
          object id = tag.ID;
          if (this._painterDictionary.Contains(id))
          {
            int rowIndex = e.RowIndex;
            if (this._painterDictionary[id] is IGridCellPainter painter)
            {
              painter.PaintCell(nodeIdForRow, e, nodeColumns, this._grid);
              return;
            }
          }
        }
        iGCell cell2 = this._grid.Cells[e.RowIndex, e.ColIndex];
        int num = 0;
        if (cell2.ImageList != null && cell2.ImageIndex >= 0)
        {
          using (Image image1 = cell2.ImageList.Images[cell2.ImageIndex])
          {
            if (image1 != null)
            {
              Graphics graphics = e.Graphics;
              Image image2 = image1;
              bounds = e.Bounds;
              int x = bounds.X + 1;
              bounds = e.Bounds;
              int y = bounds.Y + 1;
              int width = image1.Width;
              bounds = e.Bounds;
              int height = bounds.Height - 1;
              graphics.DrawImage(image2, x, y, width, height);
              num = image1.Width + 6;
            }
          }
        }
        Color color = cell2.EffectiveForeColor != Color.Empty ? cell2.EffectiveForeColor : (cell2.ForeColor != Color.Empty ? cell2.ForeColor : this._grid.ForeColor);
        if (cell2.Selected)
          color = !this._grid.Focused ? this._grid.HighlightForeColorNoFocus : this._grid.HighlightForeColor;
        if (!this._grid.Enabled)
          color = this._grid.ForeColorDisabled;
        Font font1 = cell2.EffectiveFont ?? cell2.Font ?? this._grid.Font;
        Rectangle textBounds;
        ref Rectangle local = ref textBounds;
        bounds = e.Bounds;
        int x3 = bounds.X + num;
        bounds = e.Bounds;
        int y1 = bounds.Y;
        bounds = e.Bounds;
        int width3 = bounds.Width;
        bounds = e.Bounds;
        int height1 = bounds.Height;
        local = new Rectangle(x3, y1, width3, height1);
        ChildrenView.CustomDrawCellTextEventArgs e1 = new ChildrenView.CustomDrawCellTextEventArgs(e.Graphics, cell2, textBounds, color, font1);
        EventHandler<ChildrenView.CustomDrawCellTextEventArgs> customDrawCellText = this.CustomDrawCellText;
        if (customDrawCellText != null)
          customDrawCellText((object) this, e1);
        if (e1.HasDrawn || string.IsNullOrEmpty(cell2.Text))
          return;
        using (SolidBrush solidBrush1 = new SolidBrush(color))
        {
          Graphics graphics = e.Graphics;
          string text = cell2.Text;
          Font font2 = font1;
          SolidBrush solidBrush2 = solidBrush1;
          bounds = e.Bounds;
          double x4 = (double) (bounds.X + num);
          bounds = e.Bounds;
          double y2 = (double) bounds.Y;
          double width4 = (double) (2 * cell2.Text.Length) * (double) font1.Size;
          bounds = e.Bounds;
          double height2 = (double) bounds.Height;
          RectangleF layoutRectangle = new RectangleF((float) x4, (float) y2, (float) width4, (float) height2);
          StringFormat centerStringFormat = this.LineAlignmentCenterStringFormat;
          graphics.DrawString(text, font2, (Brush) solidBrush2, layoutRectangle, centerStringFormat);
        }
      }
    }
    finally
    {
      e.Graphics.SetClip(clip.GetBounds(e.Graphics));
    }
  }

  /// <summary>Вернуть значок для указанных категории и типа</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="handler">Родительский узел (обработчик)</param>
  /// <param name="state">Состояние</param>
  /// <returns>Значок для указанных категории и типа</returns>
  private Icon GetCategoryTypeIcon(INodeID nodeID, INode handler, object state)
  {
    INavigatorIconInformation data = nodeID == null || handler == null ? (INavigatorIconInformation) null : handler.GetData(nodeID, typeof (INavigatorIconInformation)) as INavigatorIconInformation;
    return Images32x16_Cache.GetIcon32x16(nodeID.CategoryID, nodeID.TypeID, (object) data);
  }

  /// <summary>Пользовательская отрисовка фона в ячейках</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void CustomDrawCellBackground(object sender, iGCustomDrawCellEventArgs e)
  {
    if (e.Selected)
    {
      Rectangle bounds = e.Bounds;
      Brush brush = (Brush) new SolidBrush(this.ActiveControl == null || !this.ActiveControl.Focused ? SystemColors.Control : SystemColors.Highlight);
      if (brush == null)
        return;
      try
      {
        e.Graphics.FillRectangle(brush, bounds);
      }
      finally
      {
        brush.Dispose();
      }
    }
    else
    {
      INodeID nodeIdForRow = this.GetNodeIDForRow(e.RowIndex);
      if (nodeIdForRow == null)
        return;
      IDBCheckedOutByID data = this.Node != null ? (IDBCheckedOutByID) this.Node.GetData(nodeIdForRow, typeof (IDBCheckedOutByID)) : (IDBCheckedOutByID) null;
      if (this._node != null)
      {
        INode node = this._node;
      }
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      NavGradientBrush navGradientBrush = (NavGradientBrush) null;
      Rectangle bounds = e.Bounds;
      if (data != null && service != null)
      {
        if (data.CheckedOutBy != service.UserID && data.CheckedOutBy > 0L)
        {
          bool useGradient = (this._navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckedOutOther) == GradientUsing.CheckedOutOther;
          navGradientBrush = this._navGraphicsCache.GetNavGradientBrush(this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkEndColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherGradientMode, bounds, useGradient);
        }
        if (data.ObjectID < 0L && data.CheckedOutBy == service.UserID)
        {
          bool useGradient = (this._navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckOut) == GradientUsing.CheckOut;
          navGradientBrush = this._navGraphicsCache.GetNavGradientBrush(this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkEndColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutGradientMode, bounds, useGradient);
        }
      }
      if (navGradientBrush == null)
        return;
      try
      {
        e.Graphics.FillRectangle(navGradientBrush.Brush, bounds);
      }
      finally
      {
        navGradientBrush.Dispose();
      }
    }
  }

  /// <summary>Динамическая подстановка цвета текста</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GridDynamicForeColor(object sender, iGDynamicColorEventArgs e)
  {
    iGRow row = e.RowIndex >= 0 ? this._grid.Rows[e.RowIndex] : (iGRow) null;
    if (e.ColIndex >= 0 && row != null)
    {
      iGCell cell = row.Cells[e.ColIndex];
    }
    INodeID nodeIdForRow = row != null ? this.GetNodeIDForRow(row) : (INodeID) null;
    if (nodeIdForRow != null)
    {
      IDBCheckedOutByID data = this.Node != null ? (IDBCheckedOutByID) this.Node.GetData(nodeIdForRow, typeof (IDBCheckedOutByID)) : (IDBCheckedOutByID) null;
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      if (data != null && service != null)
      {
        if (data.CheckedOutBy != service.UserID && data.CheckedOutBy > 0L)
          e.Color = this._navGraphicsCache.CurrentColorsScheme.ForegroundCheckedOutOther;
        if (data.ObjectID < 0L && data.CheckedOutBy == service.UserID)
          e.Color = this._navGraphicsCache.CurrentColorsScheme.ForegroundCheckedOut;
      }
    }
    ChildrenViewCellData cellData = this.GetCellData(e.RowIndex, e.ColIndex);
    if (!this._childrenViewEditingComponent.Enabled || cellData == null)
      return;
    bool? nullable = cellData.ReadOnly;
    if (!nullable.HasValue)
      return;
    nullable = cellData.ReadOnly;
    if (!nullable.Value)
      return;
    e.Color = Color.Gray;
  }

  /// <summary>Динамическая подстановка шрифта</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GridDynamicFont(object sender, iGDynamicFontEventArgs e)
  {
    if (this.IsDisposed || this._dataAdapter == null)
      return;
    iGRow row = e.RowIndex >= 0 ? this._grid.Rows[e.RowIndex] : (iGRow) null;
    iGCell cell = e.ColIndex < 0 || row == null ? (iGCell) null : row.Cells[e.ColIndex];
    INodeID nodeIdForRow = row != null ? this.GetNodeIDForRow(e.RowIndex) : (INodeID) null;
    INodeStatusesInfo service = this.Node != null ? (INodeStatusesInfo) this.Node.GetService(typeof (INodeStatusesInfo)) : (INodeStatusesInfo) null;
    NodeColumn tag = cell == null || cell.Col == null ? (NodeColumn) null : cell.Col.Tag as NodeColumn;
    Font font = (Font) null;
    if (UISettings.NavigatorLinksMode != NavigatorLinksMode.None && cell != null && tag != null && tag.Attribute != null && tag.Attribute.FieldType == FieldTypes.ftObjectLink && nodeIdForRow != null && this.Node != null)
    {
      object obj = cell.Value;
      if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
        font = new Font(e.Font != null ? e.Font : cell.EffectiveFont, FontStyle.Underline);
    }
    IDBObjectFiltrationState data = this.Node != null ? (IDBObjectFiltrationState) this.Node.GetData(nodeIdForRow, typeof (IDBObjectFiltrationState)) : (IDBObjectFiltrationState) null;
    ObjectFiltrationState columnValue = data != null ? data.State : ObjectFiltrationState.fsNotRequired;
    if (cell != null && this._grid.CurCell != null && this._grid.CurCell.ColIndex == cell.ColIndex && this._grid.CurCell.RowIndex == cell.RowIndex)
      font = new Font(font ?? (e.Font != null ? e.Font : cell.EffectiveFont), FontStyle.Bold);
    if (service != null && data != null)
      font = service.GetFont((System.IServiceProvider) this._services, nodeIdForRow, (object) columnValue, font ?? this._grid.Font);
    if (font == null)
      return;
    e.Font = font;
  }

  /// <summary>Двойной клик крыской</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GridMouseDoubleClick(object sender, EventArgs e)
  {
    if (this._childrenViewEditingComponent.Enabled || this._ioDispatcher == null || this._dataAdapter == null || this.DisableDoubleClicks)
      return;
    Point pos = new Point(((MouseEventArgs) e).X, ((MouseEventArgs) e).Y);
    iGColHdr iGcolHdr = this._grid.Header.Cells.FromPoint(pos.X, pos.Y);
    iGCell cellCursor = this.GetCellCursor(pos);
    if (cellCursor != null)
    {
      iGRow row = cellCursor.Row;
    }
    if (cellCursor != null)
    {
      int colIndex = cellCursor.ColIndex;
    }
    INodeID nodeAtCursor = this.GetNodeAtCursor(pos);
    int num = cellCursor != null ? cellCursor.ColIndex : -1;
    if (iGcolHdr != null || cellCursor == null || cellCursor.Value == null || nodeAtCursor == null || num < 0)
      return;
    this._ioDispatcher.ProcessEvent((IIOEvent) new IOEvent((IIOSource) this, IOEventFlags.efNone, IOEventType.evMouseDoubleClick, (object) e, (object) this.GetSelectedNodeIDPath()));
  }

  /// <summary>Закрыть хинт</summary>
  internal void GridCancelHint()
  {
    this._hintCell = (iGCell) null;
    this._hintNodeID = (INodeID) null;
    this._hintColumn = -1;
    this._hintHeader = (iGColHdr) null;
    this._hintText = string.Empty;
    this._toolTip.Hide((IWin32Window) this._grid);
    this._toolTip.SetToolTip((System.Windows.Forms.Control) this._grid, this._hintText);
  }

  /// <summary>Обменять местами в гриде два указанных узла</summary>
  /// <param name="nodeFirst">Узел первый</param>
  /// <param name="nodeSecond">Узел второй</param>
  public void GridSwapNodes(INodeID nodeFirst, INodeID nodeSecond)
  {
    if (nodeFirst == nodeSecond || nodeFirst == null || nodeSecond == null)
      return;
    iGRow rowWithNodeId1 = this.GetRowWithNodeID(nodeFirst);
    iGRow rowWithNodeId2 = this.GetRowWithNodeID(nodeSecond);
    int index1 = rowWithNodeId1.Index;
    int index2 = rowWithNodeId2.Index;
    rowWithNodeId1.Move(index2);
    rowWithNodeId2.Move(index1);
  }

  /// <summary>Переместить узел в указанную позицию</summary>
  /// <param name="node">Перемещаемый узел</param>
  /// <param name="index">Новая позиция</param>
  public void GridSetNodeIndex(INodeID node, int index)
  {
    if (node == null)
      return;
    iGRow rowWithNodeId = this.GetRowWithNodeID(node);
    if (rowWithNodeId.Index == index)
      return;
    rowWithNodeId.Move(index);
  }

  /// <summary>Начать обновление грида</summary>
  public void GridBeginUpdate()
  {
    this._grid.BeginUpdate();
    this._grid.Redraw = false;
  }

  /// <summary>Завершить обновление грида</summary>
  public void GridEndUpdate()
  {
    this._grid.Redraw = true;
    this._grid.EndUpdate();
  }

  /// <summary>
  /// Получить информацию о происхождении корневого узла, на основании которого
  /// построено содержимое закладки
  /// </summary>
  /// <returns>Информация о происхождении корневого узла или null</returns>
  public RelationPair GetRootObjectKey()
  {
    if (this._nodeID == null || this._parentNode == null)
      return (RelationPair) null;
    IDBRelationID data1 = this._parentNode.GetData(this._nodeID, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID data2 = this._parentNode.GetData(this._nodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (data1 != null || data2 != null)
    {
      IDBTypedObjectID compositionTopObject = this.GetCompositionTopObject();
      RelationPair rootObjectKey = new RelationPair(0L, compositionTopObject != null ? compositionTopObject.ObjectID : 0L, compositionTopObject != null ? compositionTopObject.ObjectType : -1, data1 == null || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(data1.RelationType) ? 0L : data1.Value, this._currentUserAndRole.UserID, data2 != null ? data2.ObjectID : 0L, data1 == null || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(data1.RelationType) ? -1 : data1.RelationType, data2 != null ? data2.ObjectType : -1);
      if (rootObjectKey.TOP_OBJECT_ID != 0L)
        return rootObjectKey;
    }
    return (RelationPair) null;
  }

  /// <summary>
  /// Метод позволяет отыскать корневой объект состава, на основе которого
  /// построено содержимое текущей закладки (уровень вложенности закладки не
  /// играет роли). В случае ошибки вернёт null.
  /// </summary>
  /// <returns>Описание версии корневого объекта состава или null</returns>
  public IDBTypedObjectID GetCompositionTopObject()
  {
    IDBTypedObjectID compositionTopObject = (IDBTypedObjectID) null;
    if (this._gridSelectedItems != null && this._gridSelectedItems.Count > 0 && this._gridSelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      compositionTopObject = itemData;
    if (this._nodeID == null || this._parentNode == null)
      return compositionTopObject;
    IDBTypedObjectID data1 = this._parentNode.GetData(this._nodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBRelationID data2 = this._parentNode.GetData(this._nodeID, typeof (IDBRelationID)) as IDBRelationID;
    if (data1 != null)
      compositionTopObject = data1;
    if (data2 == null || data2.Value == 0L || !MetaDataHelper.IsPdmPartiallyConfigurableRelationType(data2.RelationType))
      return compositionTopObject;
    ChildrenView parentChildrenView = this.ParentChildrenView;
    IDBTypedObjectID dbTypedObjectId;
    if (parentChildrenView == null)
    {
      NavigatorTreeNode parentTreeNode = this.ParentTreeNode;
      if (parentTreeNode == null || parentTreeNode.Tree == null)
        return compositionTopObject;
      dbTypedObjectId = parentTreeNode.Tree.GetTopCompositionObject(parentTreeNode);
    }
    else
      dbTypedObjectId = parentChildrenView?.GetCompositionTopObject();
    return dbTypedObjectId ?? compositionTopObject;
  }

  /// <summary>
  /// Вернуть полный путь к текущей строке в гриде при условии, что она
  /// содержит связь. В путь попадёт также информация из дерева Навигатора, если
  /// его сервис доступен в контейнере. Путь будет рассчитан вверх до корневого
  /// узла в дереве Навигатора, содержащего родительский объект указанного типа
  /// </summary>
  /// <param name="parentObjectTypeID">Идентификатор родительского типа объекта, который является корневым в составе.
  /// Если указать константу Intermech.Consts.UnknownObjectTypeId, то будет возвращён полный путь состава
  /// без учёта родительского типа</param>
  /// <param name="useInheritance">Если указать true, допускается прерывать поиск на объектах, тип которых унаследован от указанного родительского типа</param>
  /// <returns>Полный путь или null</returns>
  public RelationPath GetTypedParentObjectNodePath(int parentObjectTypeID, bool useInheritance)
  {
    if (this._nodeID == null || this._parentNode == null || this._gridSelectedItems == null || this._gridSelectedItems.Count == 0)
      return (RelationPath) null;
    IDBRelationID itemData1 = this._gridSelectedItems.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID itemData2 = this._gridSelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (itemData1 == null || itemData1.Value == 0L || itemData2 == null || itemData2.ObjectID == 0L)
      return (RelationPath) null;
    RelationPath parentObjectNodePath1 = new RelationPath();
    SimpleRelationPair simpleRelationPair = new SimpleRelationPair(itemData1.Value, itemData1.RelationType, itemData2.ObjectID, itemData2.ObjectType);
    parentObjectNodePath1.Items.Add(simpleRelationPair);
    if (parentObjectTypeID != -1 && (useInheritance && MetaDataHelper.IsObjectTypeChildOf(itemData2.ObjectType, parentObjectTypeID) || !useInheritance && itemData2.ObjectType == parentObjectTypeID))
      return parentObjectNodePath1;
    ChildrenView parentChildrenView = this.ParentChildrenView;
    RelationPath parentObjectNodePath2;
    if (parentChildrenView == null || parentChildrenView == this)
    {
      NavigatorTreeNode parentTreeNode = this.ParentTreeNode;
      if (parentTreeNode == null || parentTreeNode.Tree == null)
        return parentObjectNodePath1;
      parentObjectNodePath2 = NavigatorTreeViewHelper.GetTypedParentObjectNodePath(parentTreeNode, parentObjectTypeID, useInheritance);
    }
    else
      parentObjectNodePath2 = parentChildrenView?.GetTypedParentObjectNodePath(parentObjectTypeID, useInheritance);
    if (parentObjectNodePath2 != null && !parentObjectNodePath2.Empty)
    {
      parentObjectNodePath2.Items.AddRange((IEnumerable<SimpleRelationPair>) parentObjectNodePath1.Items);
      parentObjectNodePath1 = parentObjectNodePath2;
    }
    return parentObjectNodePath1;
  }

  /// <summary>Запрос на редактирование</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void GridRequestEdit(object sender, iGRequestEditEventArgs e)
  {
    e.DoDefault = false;
  }

  private void ChildrenView_Enter(object sender, EventArgs e)
  {
    this.SetDisableChildrenViewGrouping();
    if (ServicesManager.GetService(typeof (INavigatorContextSearch)) != null)
      ServicesManager.RemoveService(typeof (INavigatorContextSearch));
    ServicesManager.AddService(typeof (INavigatorContextSearch), (object) this);
    if (ServicesManager.GetService(typeof (ChildrenView)) != null)
      ServicesManager.RemoveService(typeof (ChildrenView));
    ServicesManager.AddService(typeof (ChildrenView), (object) this);
  }

  private void ChildrenView_Leave(object sender, EventArgs e)
  {
    if (ServicesManager.GetService(typeof (INavigatorContextSearch)) != null)
      ServicesManager.RemoveService(typeof (INavigatorContextSearch));
    ChildrenView._toggleEditingModeMenuButtonItem.Visible = false;
    if (ServicesManager.GetService(typeof (ChildrenView)) == null)
      return;
    ServicesManager.RemoveService(typeof (ChildrenView));
  }

  private void SearchComponent_SearchStateChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void CurrentVersionsRuleButtonItem_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.Configurations.WriteBool("Core", "ObjectLists", "VersionsRule", this._currentVersionsRuleButtonItem.Checked, sessionKeeper.Session.UserID);
    this.ReloadItems();
  }

  private void ShowContextVersionsButtonItem_Click(object sender, EventArgs e)
  {
    this.ShowContextVersions = !this.ShowContextVersions;
  }

  private static void ToggleEditingModeMenuButtonItem_Click(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (ChildrenView)) is ChildrenView service))
      return;
    service.EditingMode = !service.EditingMode;
  }

  private void ManualSortingButtonItem_Click(object sender, EventArgs e)
  {
    if (this._toggleManualSortingButtonItem.Checked)
    {
      this._lastGroupingAndSortingColumns = this.GridGetGroupingAndSortingColumns();
      this._grid.SortObject.Clear();
    }
    if (!this._toggleManualSortingButtonItem.Checked && this._lastGroupingAndSortingColumns != null)
    {
      this._manualSorting = this._toggleManualSortingButtonItem.Checked;
      this.GridSetGroupingAndSortingColumns(this._lastGroupingAndSortingColumns);
    }
    else
    {
      this.GridReflectColumnsProperties();
      this.InternalReloadItems();
      this.SelectionChanged();
      this._manualSorting = this._toggleManualSortingButtonItem.Checked;
    }
  }

  private void ManualSortingSetupButtonItem_Click(object sender, EventArgs e)
  {
    this.ManualSortingSetupCommand((ISelectedItems) this._parentSelItem, this._parentSelItem.Services, (object) null);
  }

  private void RefreshButtonItem_Click(object sender, EventArgs e)
  {
    this._rowsBeforeSort = this._grid.Rows.Count;
    this.ReloadItems();
  }

  private void ViewDropDownMenuItem_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if ((this._embeddedViewsState & ChildrenView.EmbeddedViewsState.Open) != ChildrenView.EmbeddedViewsState.None)
      return;
    this.OpenEmbeddedViews();
  }

  private void ViewDropDownMenuItem_Click(object sender, EventArgs e) => this.ToggleEmbeddedViews();

  private void OpenEmbeddedViewMenuButtonItem_Click(object sender, EventArgs e)
  {
    this._pageViewsManager.ActiveViewPage = this._pageViewsManager.ViewPages[this._embeddedViewsDropDownMenuItem.Items.IndexOf((ToolbarItemBase) sender)];
    this.OpenEmbeddedViews();
  }

  private void GroupingButtonItem_Click(object sender, EventArgs e) => this.GridClearGrouping();

  private void CollapseAllGroupsButtonItem_Click(object sender, EventArgs e)
  {
    this.CollapseAllGroups();
  }

  private void ExpandAllGroupsButtonItem_Click(object sender, EventArgs e)
  {
    this.ExpandAllGroups();
  }

  private void CollapseAllGroupsExceptGroupsWithSelectionsButtonItem_Click(
    object sender,
    EventArgs e)
  {
    this.CollapseAllGroupsExceptGroupsWithSelections();
  }

  private void EditingModeButtonItem_Click(object sender, EventArgs e)
  {
    this.EditingMode = !this.EditingMode;
    this._dataAdapter.UpdateGrid(this.GetNodeColumns());
  }

  private void Grid_LostFocus(object sender, EventArgs e) => this.QueryCopyCutPasteStatuses();

  private void Grid_CustomSort(object sender, iGCustomSortEventArgs e)
  {
    if (!this._preventSorting)
      return;
    e.Result = 0;
  }

  private void Grid_BeforeContentsSorted(object sender, EventArgs e) => this.BeforeContentsSorted();

  private void Grid_AfterContentsSorted(object sender, EventArgs e) => this.AfterContentsSorted();

  private void Grid_CustomGroupValue(object sender, iGCustomGroupValueEventArgs e)
  {
    iGCol col = this._grid.Cols[e.ColIndex];
    if (col != null && col.Key == "Special_StateImage")
    {
      INodeID nodeIdForRow = this.GetNodeIDForRow(e.RowIndex);
      if (nodeIdForRow is Intermech.Navigator.DBObjects.NodeID)
        e.Value = (object) MetaDataHelper.GetObjectTypeName(((Intermech.Navigator.DBObjects.NodeID) nodeIdForRow).ObjectTypeID);
      else
        e.Value = (object) string.Empty;
    }
    else
    {
      NodeColumn nodeColumn = this.GetNodeColumn(e.ColIndex);
      if (nodeColumn != null && nodeColumn.AttrType == FieldTypes.ftDateTime)
      {
        object obj = this._grid.Cells[e.RowIndex, e.ColIndex].Value;
        DateTime result;
        e.Value = obj == null ? (object) string.Empty : (!(obj is DateTime) ? (!DateTime.TryParse(obj.ToString(), out result) ? (object) string.Empty : (object) result.Date) : obj);
      }
      if (nodeColumn == null || nodeColumn.AttrType != FieldTypes.ftMeasured)
        return;
      ChildrenViewCellData cellData = this.GetCellData(e.RowIndex, e.ColIndex);
      if (cellData != null)
      {
        if (!(cellData.Value is MeasuredValue result) && cellData.Value is string)
          MeasuredValueHelper.TryParse((string) cellData.Value, out result, nodeColumn.Attribute.SizeType, MeasureHelper.Measures);
        e.Value = result != null ? (object) new ChildrenView.ComparableMeasuredValue(result) : (object) ChildrenView.ComparableMeasuredValue.Empty;
      }
      else
        e.Value = (object) ChildrenView.ComparableMeasuredValue.Empty;
    }
  }

  private void Grid_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
  {
    if (e.Expanded)
      --this._collapsedRowsCount;
    else
      ++this._collapsedRowsCount;
  }

  private void Grid_AfterAutoGroupRowCreated(object sender, iGAfterAutoGroupRowCreatedEventArgs e)
  {
    ++this._groupRowsCount;
    ++this._collapsedRowsCount;
    iGCell cell1 = this._grid.RowTextCol.Cells[e.AutoGroupRowIndex];
    iGCell cell2 = this._grid.Cells[e.GroupedRowIndex, e.GroupedColIndex];
    if (this._grid.Cols[e.GroupedColIndex].Tag is NodeColumn tag)
    {
      if (tag.ID.Equals((object) ObligatoryObjectAttributes.F_OBJECT_TYPE))
      {
        cell1.Style.ImageList = this._categoryTypeIconService.ImageList;
        cell1.ImageIndex = this._grid.Cells[e.GroupedRowIndex, "Special_StateImage"].ImageIndex;
      }
      else if (this._painterDictionary[(object) (tag.ID.ToString() + ".images")] is IGridColumnImageList)
      {
        cell1.Style.ImageList = (this._painterDictionary[(object) (tag.ID.ToString() + ".images")] as IGridColumnImageList).ImageList;
        cell1.ImageIndex = cell2.ImageIndex;
      }
    }
    this.UpdateStatusbar();
  }

  private void Grid_BeforeContentsGrouped(object sender, EventArgs e)
  {
    if (this._grouping)
      return;
    try
    {
      this._grouping = true;
      this._stateBeforeGroup = this.GridGetFocusAndSelection();
    }
    finally
    {
      this._grouping = false;
    }
  }

  private void Grid_AfterContentsGrouped(object sender, EventArgs e) => this.AfterContentsGrouped();

  private void Grid_ColWidthEndChange(object sender, iGColWidthEventArgs e)
  {
    this.GridColWidthChanged(sender, e);
  }

  private void Grid_ColHdrEndDrag(object sender, iGColHdrEndDragEventArgs e)
  {
    this.GridColumnMoved(sender, e);
  }

  private void Grid_ColDividerDoubleClick(object sender, iGColDividerDoubleClickEventArgs e)
  {
    iGCol col = this._grid.Cols[e.ColIndex];
    if (this._grid.Rows.Count <= e.RowIndex)
      return;
    iGCell cell = this._grid.Rows[e.RowIndex].Cells[e.ColIndex];
    string[] strArray = col.Text.ToString().Split(new string[2]
    {
      " ",
      ","
    }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray == null || strArray.Length == 0)
      return;
    SizeF textBounds = this.CalculateTextBounds(cell.EffectiveFont, strArray[0]);
    col.AutoWidth();
    if ((double) col.Width < (double) textBounds.Width)
      col.Width = Convert.ToInt32(textBounds.Width);
    if (col.Width < 5)
      col.Width = 5;
    e.DoDefault = false;
  }

  private void Grid_CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
  {
    this.CustomDrawCellForeground(sender, e);
  }

  private void Grid_CustomDrawCellBackground(object sender, iGCustomDrawCellEventArgs e)
  {
    this.CustomDrawCellBackground(sender, e);
  }

  private void Grid_DynamicBackColor(object sender, iGDynamicColorEventArgs e)
  {
    iGRow row = e.RowIndex >= 0 ? this._grid.Rows[e.RowIndex] : (iGRow) null;
    iGCell cell = e.ColIndex < 0 || row == null ? (iGCell) null : row.Cells[e.ColIndex];
    if (cell == null || this._grid.CurCell == null || this._grid.CurCell.ColIndex != cell.ColIndex || this._grid.CurCell.RowIndex != cell.RowIndex)
      return;
    e.Color = this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkColor;
  }

  private void Grid_DynamicForeColor(object sender, iGDynamicColorEventArgs e)
  {
    this.GridDynamicForeColor(sender, e);
  }

  private void Grid_DynamicFont(object sender, iGDynamicFontEventArgs e)
  {
    this.GridDynamicFont(sender, e);
  }

  private void Grid_Enter(object sender, EventArgs e)
  {
    this.QueryCopyCutPasteStatuses();
    this.AddSimpleSelectedItemsToGlobalServiceContainer();
  }

  private void Grid_Leave(object sender, EventArgs e)
  {
    if (this.IsSimpleSelectedItemsSuppoted && ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
      ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
    this.DisableControls();
    this.QueryCopyCutPasteStatuses();
  }

  private void Grid_SelectionChanged(object sender, EventArgs e)
  {
    if (EventsViewConsts.IsFile)
      return;
    this.GridSelectionChanged();
  }

  private void Grid_MouseMove(object sender, MouseEventArgs e)
  {
    object sender1 = sender;
    Point location1;
    if (e != null)
    {
      Point location2 = e.Location;
      location1 = e.Location;
    }
    else
      location1 = System.Windows.Forms.Control.MousePosition;
    this.GridShowHint(sender1, location1);
    if (UISettings.NavigatorLinksMode == NavigatorLinksMode.None || this._childrenViewEditingComponent.Enabled || this.IsDisposed || this._dataAdapter == null || this.IsDisposed || this._dataAdapter == null || this._contextMenuActive)
      return;
    Point point;
    if (e != null)
    {
      Point location3 = e.Location;
      point = e.Location;
    }
    else
      point = System.Windows.Forms.Control.MousePosition;
    Point pos = point;
    iGColHdr iGcolHdr = this._grid.Header.Cells.FromPoint(pos.X, pos.Y);
    iGCell cellCursor = this.GetCellCursor(pos);
    if (cellCursor != null)
    {
      iGRow row = cellCursor.Row;
    }
    iGCol col = cellCursor?.Col;
    if (cellCursor != null)
    {
      int colIndex1 = cellCursor.ColIndex;
    }
    INodeID nodeAtCursor = this.GetNodeAtCursor(pos);
    if (cellCursor != null)
    {
      int colIndex2 = cellCursor.ColIndex;
    }
    NodeColumn tag = col != null ? col.Tag as NodeColumn : (NodeColumn) null;
    if (cellCursor != null && iGcolHdr == null && tag != null && tag.Attribute != null && tag.Attribute.FieldType == FieldTypes.ftObjectLink && nodeAtCursor != null && this.Node != null)
    {
      object obj = cellCursor.Value;
      if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
        this._grid.Cursor = Cursors.Hand;
      else
        this._grid.Cursor = Cursors.Default;
    }
    else
      this._grid.Cursor = Cursors.Default;
  }

  private void Grid_MouseUp(object sender, MouseEventArgs e)
  {
    this.AddSimpleSelectedItemsToGlobalServiceContainer();
    this._startDragRectangle = Rectangle.Empty;
    if (e.Button == MouseButtons.Right && !this.ShowContextMenu(e.Location))
    {
      if (this.ShowCustomContextMenu != null)
      {
        try
        {
          this.ShowCustomContextMenu((object) this, new ContextMenuEventArgs(e.Location, (System.Windows.Forms.Control) this._grid));
          return;
        }
        finally
        {
          if (this.IsSimpleSelectedItemsSuppoted)
          {
            ChildrenViewSelectedItems serviceInstance = new ChildrenViewSelectedItems(this._path, this.Node, this);
            if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
              ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
            ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) serviceInstance);
          }
        }
      }
    }
    if (UISettings.NavigatorLinksMode == NavigatorLinksMode.None || this._childrenViewEditingComponent.Enabled || (UISettings.NavigatorLinksMode != NavigatorLinksMode.LeftMouseClick || e.Button != MouseButtons.Left) && (UISettings.NavigatorLinksMode != NavigatorLinksMode.MiddleMouseClick || e.Button != MouseButtons.Middle) || this.IsDisposed || this._dataAdapter == null || this._contextMenuActive)
      return;
    Point point1;
    if (e != null)
    {
      Point location = e.Location;
      point1 = e.Location;
    }
    else
      point1 = System.Windows.Forms.Control.MousePosition;
    Point point2 = point1;
    iGColHdr iGcolHdr = this._grid.Header.Cells.FromPoint(point2.X, point2.Y);
    iGCell cellCursor = this.GetCellCursor(point2);
    Rectangle rectangle = cellCursor != null ? cellCursor.TextBounds : Rectangle.Empty;
    if (cellCursor != null)
    {
      iGRow row = cellCursor.Row;
    }
    iGCol col = cellCursor?.Col;
    if (cellCursor != null)
    {
      int colIndex1 = cellCursor.ColIndex;
    }
    INodeID nodeAtCursor = this.GetNodeAtCursor(point2);
    if (cellCursor != null)
    {
      int colIndex2 = cellCursor.ColIndex;
    }
    NodeColumn tag = col != null ? col.Tag as NodeColumn : (NodeColumn) null;
    if (cellCursor == null || iGcolHdr != null || tag == null || tag.Attribute == null || tag.Attribute.FieldType != FieldTypes.ftObjectLink || nodeAtCursor == null || this.Node == null || !rectangle.Contains(point2))
      return;
    object obj = cellCursor.Value;
    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
      return;
    IDBObjectID data = this.Node != null ? this.Node.GetData(nodeAtCursor, typeof (IDBObjectID)) as IDBObjectID : (IDBObjectID) null;
    if (data == null || data.Value == 0L)
      return;
    long objID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(data.Value, false);
      if (objectActualCopy == null)
        return;
      IDBAttribute attributeById = objectActualCopy.GetAttributeByID(tag.Attribute.AttributeID);
      if (attributeById == null)
        return;
      objID = DataSetProcessor.GetInt64Value(attributeById.Value, 0L);
      if (objID == 0L)
        return;
    }
    try
    {
      if (this.Services.GetService(typeof (ChildrenViewActionContext)) != null)
        this.Services.RemoveService(typeof (ChildrenViewActionContext));
      this.Services.AddService(typeof (ChildrenViewActionContext), (object) new ChildrenViewActionContext(nodeAtCursor));
      Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objID), (System.IServiceProvider) this.Services);
    }
    finally
    {
      if (this.Services.GetService(typeof (ChildrenViewActionContext)) != null)
        this.Services.RemoveService(typeof (ChildrenViewActionContext));
    }
  }

  private void Grid_MouseDoubleClick(object sender, EventArgs e)
  {
    this.GridMouseDoubleClick(sender, e);
  }

  private void Grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
  {
    if (e.RowIndex >= 0 && e.ColIndex >= 0 && this._grid.Rows.Count > 0)
    {
      Size dragSize = SystemInformation.DragSize;
      this._startDragRectangle = new Rectangle(new Point(e.MousePos.X - dragSize.Width / 2, e.MousePos.Y - dragSize.Height / 2), dragSize);
    }
    else
      this._startDragRectangle = Rectangle.Empty;
  }

  private void Grid_CellMouseMove(object sender, iGCellMouseMoveEventArgs e)
  {
    if (this._startDragRectangle.IsEmpty)
      return;
    this.GridStartDragDrop(e.MousePos);
  }

  private void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
  {
    this._startDragRectangle = Rectangle.Empty;
  }

  private void Grid_CellMouseLeave(object sender, iGCellMouseEnterLeaveEventArgs e)
  {
    this._startDragRectangle = Rectangle.Empty;
    this.GridCancelHint();
  }

  private void Grid_CurCellChanged(object sender, EventArgs e)
  {
    EventHandler currentColumnChanged = this.CurrentColumnChanged;
    if (currentColumnChanged == null)
      return;
    currentColumnChanged((object) this, EventArgs.Empty);
  }

  private void Grid_MouseLeave(object sender, EventArgs e)
  {
    this._startDragRectangle = Rectangle.Empty;
    this._grid.Cursor = Cursors.Default;
    this.GridCancelHint();
  }

  private void Grid_KeyDown(object sender, KeyEventArgs e)
  {
    if (this._childrenViewEditingComponent.IsEditorVisible || this._contextMenuActive)
      return;
    bool flag = this._childrenViewContextSearchManager != null && this._childrenViewContextSearchManager.InProgress;
    if (e.KeyCode == Keys.Apps && !this._preventSorting && !this.DisableKeyDownEvents && !flag && this._grid.CurCell != null)
    {
      iGCell curCell = this._grid.CurCell;
      Point location = new Point(curCell.Bounds.Left + 3, curCell.Bounds.Top + 3);
      if (!this.ShowContextMenu(location))
      {
        if (this.ShowCustomContextMenu != null)
        {
          try
          {
            this.ShowCustomContextMenu((object) this, new ContextMenuEventArgs(location, (System.Windows.Forms.Control) this._grid));
          }
          finally
          {
            if (this.IsSimpleSelectedItemsSuppoted)
            {
              ChildrenViewSelectedItems serviceInstance = new ChildrenViewSelectedItems(this._path, this.Node, this);
              if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
                ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
              ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) serviceInstance);
            }
          }
        }
      }
      e.Handled = true;
    }
    else
    {
      if (this._ioDispatcher != null && !this._preventSorting && !this.DisableKeyDownEvents && !flag)
      {
        NodeIDPath ATag = this.GetSelectedNodeIDPath();
        if (e.KeyCode == Keys.BrowserBack)
          ATag = this._path;
        this._ioDispatcher.ProcessEvent((IIOEvent) new IOEvent((IIOSource) this, IOEventFlags.efNone, IOEventType.evKeyDown, (object) e, (object) ATag));
      }
      if (e.Shift && e.Control || !this._preventSorting)
        return;
      this._preventSorting = false;
      if (!this._tryingSorting)
        return;
      this.BeforeContentsSorted();
      this.AfterContentsSorted();
    }
  }

  private void Grid_KeyPress(object sender, KeyPressEventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void Grid_KeyUp(object sender, KeyEventArgs e)
  {
  }

  protected virtual void Grid_DragEnter(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!this._grid.AllowDrop || !e.Data.GetDataPresent(typeof (IOSource)) || (e.Data.GetData(typeof (IOSource)) as IOSource).Control == this)
      return;
    e.Effect = DragDropEffects.Copy;
  }

  protected virtual void Grid_DragDrop(object sender, DragEventArgs e)
  {
    if (this.GridDragDrop != null)
    {
      this.GridDragDrop(sender, e);
    }
    else
    {
      e.Effect = DragDropEffects.None;
      if (!this._grid.AllowDrop || !e.Data.GetDataPresent(typeof (IOSource)))
        return;
      INodeID nodeAtCursor = this.GetNodeAtCursor(this._grid.PointToClient(new Point(e.X, e.Y)));
      this._dropTargetNodeItems.NodeIDs.Clear();
      this._dropTargetNodeItems.NodeIDs.Add(nodeAtCursor);
      ISelectedItems selectedItems = nodeAtCursor != null ? (ISelectedItems) this._dropTargetNodeItems : (ISelectedItems) this._parentSelItem;
      IOSource data = e.Data.GetData(typeof (IOSource)) as IOSource;
      CommandsTable commandsTable1 = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(data.SelectedItems, data.Services, false);
      if (!commandsTable1.Contains("Copy"))
        return;
      IClipboard service = ServicesManager.GetService(typeof (IClipboard)) as IClipboard;
      service.Push();
      try
      {
        Intermech.Navigator.ContextMenu.Services.InvokeCommand("Copy", commandsTable1, data.Services);
        System.IServiceProvider services = (System.IServiceProvider) this._services;
        CommandsTable commandsTable2 = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(selectedItems, services, false);
        if (!commandsTable2.Contains("Paste"))
          return;
        bool dropNotofications = UISettings.DragDropNotofications;
        try
        {
          UISettings.DragDropNotofications = true;
          DragDropEventArgs e1 = new DragDropEventArgs("DragDrop", false, false, data.SelectedItems, data.Services, data.Control, selectedItems, services, (object) this);
          this._notificationService.FireEvent((object) this, (NotificationEventArgs) e1);
          if (e1.Handled)
            return;
          try
          {
            Intermech.Navigator.ContextMenu.Services.InvokeCommand(sc_3885.ssp_imclient_3886(), commandsTable2, services);
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
        }
        finally
        {
          UISettings.DragDropNotofications = dropNotofications;
        }
      }
      finally
      {
        service.Pop();
      }
    }
  }

  /// <summary>
  /// Перечитывать данные в гриде при изменении запрета сортировки или группировки
  /// </summary>
  protected virtual bool reloadGridOnDisableColumnsSortingOrGrouping => true;

  private void Grid_EllipsisBtnClick(object sender, iGEllipsisBtnClickEventArgs e)
  {
    iGCol col = this._grid.Cols[e.ColIndex];
    iGRow row = this._grid.Rows[e.RowIndex];
    INodeID nodeIdForRow = row != null ? this.GetNodeIDForRow(row) : (INodeID) null;
    IDBTypedObjectID data1 = this.Node != null ? (IDBTypedObjectID) this.Node.GetData(nodeIdForRow, typeof (IDBTypedObjectID)) : (IDBTypedObjectID) null;
    IDBRelationID data2 = this.Node != null ? (IDBRelationID) this.Node.GetData(nodeIdForRow, typeof (IDBRelationID)) : (IDBRelationID) null;
    NodeColumn tag = col != null ? col.Tag as NodeColumn : (NodeColumn) null;
    IMSAttributeType attribute = tag?.Attribute;
    if (attribute == null || (this.DisableMultiValuesAttrButton || attribute.MultiValueMode != MultiValueModes.MultiValues) && (attribute.MultiValueMode != MultiValueModes.MultiValuesFromList || data1 == null && data2 == null))
      return;
    int attributeId = attribute.AttributeID;
    AttributeSourceTypes attrSource = tag.AttrSource;
    long num1 = attrSource == AttributeSourceTypes.Object ? (data1 != null ? data1.ObjectID : 0L) : (data2 != null ? data2.Value : 0L);
    int idSource = (int) attrSource;
    long id = num1;
    using (MultivaluesAttrShowForm multivaluesAttrShowForm = new MultivaluesAttrShowForm(attributeId, (AttributeSourceTypes) idSource, id))
    {
      int num2 = (int) multivaluesAttrShowForm.ShowDialog();
      if (multivaluesAttrShowForm.Descriptions == null)
        return;
      this._grid.Cells[e.RowIndex, e.ColIndex].Value = (object) string.Join(", ", multivaluesAttrShowForm.Descriptions);
    }
  }

  private void Grid_RequestEdit(object sender, iGRequestEditEventArgs e)
  {
    this.GridRequestEdit(sender, e);
  }

  private void ReadNextToolStripDropDownButton_Click(object sender, EventArgs e)
  {
    try
    {
      this._readNextToolStripDropDownButton.Enabled = false;
      this._readAllToolStripDropDownButton.Enabled = false;
      this.FetchItems();
    }
    finally
    {
      this.UpdateControls();
    }
  }

  private void ReadAllToolStripDropDownButton_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString(sc_3885.ssp_imclient_3887()), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    this._readAllMode = true;
    try
    {
      this._readNextToolStripDropDownButton.Enabled = false;
      this._readAllToolStripDropDownButton.Enabled = false;
      this.FetchItems();
      if (this._grid.GroupObject.Count <= 0)
        return;
      this.CollapseAllGroups();
    }
    finally
    {
      this.UpdateControls();
    }
  }

  private void ToogleEmbeddedViewsToolStripDropDownButton_Click(object sender, EventArgs e)
  {
    this.ToggleEmbeddedViews();
  }

  private void PageViewsManager_Resize(object sender, EventArgs e)
  {
  }

  private void PageViewsManager_ActiveViewPageChanged(object sender, EventArgs e)
  {
    this.UpdateEmbeddedViewsContols();
  }

  private void DelayedUpdateTimer_Tick(object sender, EventArgs e)
  {
    this._delayedUpdateTimer.Stop();
    this._gridSelectedItems?.Invalidate();
    this.AddSimpleSelectedItemsToGlobalServiceContainer();
    this.UpdateControls();
    this.UpdateLinkedControls();
    this.UpdateCommandManagerItems();
  }

  private void PostProcessTimer_Tick(object sender, EventArgs e)
  {
    this._postProcessTimer.Stop();
    if (this._postProcessTimer.Tag == null)
      return;
    try
    {
      ((MethodInvoker) this._postProcessTimer.Tag)();
    }
    finally
    {
      this._postProcessTimer.Tag = (object) null;
    }
  }

  private void NavGraphicsCache_UIColorsSchemeChanged(object sender, EventArgs e)
  {
    this._grid.BackColor = this._navGraphicsCache.CurrentColorsScheme.Background;
    this._grid.ForeColor = this._navGraphicsCache.CurrentColorsScheme.Foreground;
    this._grid.HighlightBackColor = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelected;
    this._grid.HighlightForeColor = this._navGraphicsCache.CurrentColorsScheme.ForegroundSelected;
    this._grid.HighlightBackColorNoFocus = this._navGraphicsCache.CurrentColorsScheme.BackgroundSelectedInactive;
    this._grid.HighlightForeColorNoFocus = this._navGraphicsCache.CurrentColorsScheme.ForegroundSelectedInactive;
  }

  private void BarManager_RendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this._toolBar.Renderer = renderer;
    this._gridHeaderMenuBar.Renderer = renderer;
  }

  private void NavigatorColumnsService_ColumnsChanged(
    object sender,
    NavigatorColumnsChangedEventArgs e)
  {
    if (!this.IsDisposed)
    {
      if (e.ColumnsKey != null && !this._isInheritedNavigatorColumns && (e.ColumnsKey.Category != this.StateStreamCategoryID || e.ColumnsKey.Type != this.StateStreamCategoryType || !(e.ColumnsKey.Suffix == this.StateStreamPrefix)) || this._navigatorColumnsService == null || this._dataAdapter == null)
        return;
      NavigatorColumns navigatorColumns = this._navigatorColumnsService.GetNavigatorColumns(this.StateStreamCategoryID, this.StateStreamCategoryType, this.StateStreamPrefix, this.UseInheritedNavViews);
      if (navigatorColumns == null || navigatorColumns.Columns == null || NodeColumnCollection.Equals(navigatorColumns.Columns, this.GetNodeColumns()))
        return;
      this._isColumnsObsoleted = true;
    }
    else
      this._navigatorColumnsService.ColumnsChanged -= new EventHandler<NavigatorColumnsChangedEventArgs>(this.NavigatorColumnsService_ColumnsChanged);
  }

  private void ChildrenView_Resize(object sender, EventArgs e)
  {
    if (!this._embeddedViewsPanel.Visible)
      return;
    this.EMVAbsHeight = this.CalculateEmbeddedViewsHeight();
  }

  private void EmbeddedViewsCollapsibleSplitter_SplitterMoved(object sender, SplitterEventArgs e)
  {
    this.SetSplitterPosition();
  }

  private void QueryCopyCutPasteStatuses()
  {
    if (this.IsDesignerHosted())
      return;
    try
    {
      string[] strArray = new string[3]
      {
        "Copy",
        "Cut",
        "Paste"
      };
      ICommandManager commandManager = ServiceLocator.Get<ICommandManager>();
      foreach (string commandName in strArray)
      {
        ICommandState command = commandManager.FindCommand(commandName);
        if (command != null)
          commandManager.QueryStatus(command);
      }
    }
    catch (Exception ex)
    {
      ServiceLocator.Get<IOutputView>().WriteString("Ошибки", ex.Message);
    }
  }

  private void SetDisableChildrenViewGrouping()
  {
    if (this.BlockUISettingsDisableChildrenViewGrouping || this.DisableGroupBox == UISettings.DisableChildrenViewGrouping)
      return;
    this.DisableGroupBox = UISettings.DisableChildrenViewGrouping;
    this.DisableColumnsGrouping = UISettings.DisableChildrenViewGrouping;
  }

  /// <summary>
  /// Вернуть полный путь к текущей строке в гриде при условии, что она
  /// содержит связь. В путь попадёт также информация из дерева Навигатора, если
  /// его сервис доступен в контейнере. Путь будет рассчитан вверх до корневого
  /// конфигурируемого узла в дереве Навигатора
  /// </summary>
  /// <returns>Полный путь или null</returns>
  private RelationPath GetConfiguredNodePath()
  {
    if (this._nodeID == null || this._parentNode == null || this._gridSelectedItems == null || this._gridSelectedItems.Count == 0)
      return (RelationPath) null;
    IDBRelationID itemData1 = this._gridSelectedItems.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID itemData2 = this._gridSelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (itemData1 == null || itemData1.Value == 0L || itemData2 == null || itemData2.ObjectID == 0L)
      return (RelationPath) null;
    RelationPath configuredNodePath1 = new RelationPath();
    SimpleRelationPair simpleRelationPair = new SimpleRelationPair(itemData1.Value, itemData1.RelationType, itemData2.ObjectID, itemData2.ObjectType);
    configuredNodePath1.Items.Add(simpleRelationPair);
    ChildrenView parentChildrenView = this.ParentChildrenView;
    RelationPath configuredNodePath2;
    if (parentChildrenView == null)
    {
      NavigatorTreeNode parentTreeNode = this.ParentTreeNode;
      if (parentTreeNode == null || parentTreeNode.Tree == null)
        return configuredNodePath1;
      configuredNodePath2 = NavigatorTreeViewHelper.GetConfiguredNodePath(parentTreeNode);
    }
    else
      configuredNodePath2 = parentChildrenView?.GetConfiguredNodePath();
    if (configuredNodePath2 != null && !configuredNodePath2.Empty)
    {
      configuredNodePath2.Items.AddRange((IEnumerable<SimpleRelationPair>) configuredNodePath1.Items);
      configuredNodePath1 = configuredNodePath2;
    }
    return configuredNodePath1;
  }

  /// <summary>
  /// Ссылка на родительскую закладку, узел которой используется для построения
  /// данной закладки (или null)
  /// </summary>
  private ChildrenView ParentChildrenView
  {
    get
    {
      if (this._parentNode == null)
        return (ChildrenView) null;
      return !(this._parentNode is IContextAware parentNode) || parentNode.Services == null ? (ChildrenView) null : parentNode.Services.GetService(typeof (ChildrenView)) as ChildrenView;
    }
  }

  /// <summary>
  /// Ссылка на дерево Навигатора (или null), узел которого является родительским
  /// узлом для данной закладки (уровень вложенности закладки не играет роли)
  /// </summary>
  private NavigatorTreeView ParentTreeView
  {
    get => this._services.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView;
  }

  /// <summary>
  /// Узел в дереве, на основе которого построена закладка (уровень вложенности закладки не играет роли)
  /// </summary>
  private NavigatorTreeNode ParentTreeNode => this.ParentTreeView?.FocusedNode;

  /// <summary>
  /// Интерфейс, позволяющий принудительно перестроить в контроле коллекцию выделенных
  /// элементов, если контрол поддерживает работу с сервисом IToSelectItemsAnalyzers
  /// </summary>
  private void RefreshWithToSelectItemsAnalyzers()
  {
    IToSelectItemsAnalyzers service = this.Services != null ? this.Services.GetService(typeof (IToSelectItemsAnalyzers)) as IToSelectItemsAnalyzers : (IToSelectItemsAnalyzers) null;
    if (service == null || this._grid.Rows.Count == 0 || this.Node == null)
      return;
    List<int> items = new List<int>();
    for (int index = 0; index < this._grid.Rows.Count; ++index)
    {
      iGRow row = this._grid.Rows[index];
      INodeID nodeIdForRow = this.GetNodeIDForRow(row);
      if (nodeIdForRow != null && service.Analyze((System.Windows.Forms.Control) this, (System.IServiceProvider) this.Services, this.Node, nodeIdForRow, index) != ToSelectItemsAnalyzerResult.Skip)
        items.Add(row.Index);
    }
    if (items.Count == 0)
      return;
    this.SelectItems(items, true);
  }

  /// <summary>
  /// Обновляем все что связано с выделением и дергаем событие
  /// </summary>
  internal void SelectionChanged()
  {
    if (this._gridSelectedItems != null)
      this._gridSelectedItems.Invalidate();
    this.UpdateControls();
    this.UpdateLinkedControls();
    if (this._disableSelectedItemsChanged || this.SelectedItemsChanged == null)
      return;
    this.SelectedItemsChanged((object) this, EventArgs.Empty);
  }

  /// <summary>Найти INodeID сфокусированной записи</summary>
  /// <returns>INodeID сфокусированной записи</returns>
  private INodeID GetSelectedINodeID()
  {
    return this._grid.CurRow == null ? (INodeID) null : this.GetNodeIDForRow(this._grid.CurRow.Index);
  }

  /// <summary>Найти путь к сфокусированной записи (если она папка)</summary>
  /// <returns>Путь к сфокусированной записи</returns>
  private NodeIDPath GetSelectedNodeIDPath()
  {
    NodeIDPath selectedNodeIdPath = (NodeIDPath) null;
    INodeID selectedInodeId = this.GetSelectedINodeID();
    if (selectedInodeId != null)
      selectedNodeIdPath = new NodeIDPath(this._path, selectedInodeId);
    return selectedNodeIdPath;
  }

  /// <summary>Вернуть индекс текущей выделенной строки или -1</summary>
  /// <returns>Индекс текущей выделенной строки или -1</returns>
  internal int GridSelectedRowIndex()
  {
    if (this._grid.CurRow != null && this._grid.CurRow.Index >= 0 && this._dataAdapter != null && (long) this._grid.CurRow.Index < this._dataAdapter.ReadedRecordCount + (long) this._groupRowsCount)
      return this._grid.CurRow.Index;
    return this._grid.CurCell != null && this._grid.CurCell.RowIndex >= 0 && (long) this._grid.CurCell.RowIndex < this._dataAdapter.ReadedRecordCount + (long) this._groupRowsCount ? this._grid.CurCell.RowIndex : -1;
  }

  /// <summary>Получить узел с настройками для самой закладки</summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Узел с настройками для самой закладки</returns>
  private XmlNode GridXMLGetViewState(XmlDocument xmlDoc)
  {
    XmlElement element1 = xmlDoc.CreateElement("State");
    int int32 = Convert.ToInt32(this.EMVOpened);
    XmlNode element2 = (XmlNode) xmlDoc.CreateElement("OpenedEMViews");
    element2.AppendChild((XmlNode) xmlDoc.CreateTextNode(int32.ToString()));
    element1.AppendChild(element2);
    XmlElement element3 = xmlDoc.CreateElement("SplitterPosition");
    element3.AppendChild((XmlNode) xmlDoc.CreateTextNode(this._splitterPosition.ToString()));
    element1.AppendChild((XmlNode) element3);
    return (XmlNode) element1;
  }

  /// <summary>Загрузить настройки самой закладки из указанного узла</summary>
  /// <param name="node">Узел с настройками самой закладки</param>
  private void GridXMLRestoreViewState(XmlNode node)
  {
    if (node == null)
      return;
    XmlNode xmlNode1 = node.SelectSingleNode("OpenedEMViews");
    int int32 = xmlNode1 != null ? Convert.ToInt32(xmlNode1.InnerText) : 0;
    XmlNode xmlNode2 = node.SelectSingleNode("SplitterPosition");
    if (xmlNode2 != null)
      this._splitterPosition = Convert.ToDouble(xmlNode2.InnerText);
    if (int32 != 1)
      return;
    this.OpenEmbeddedViews();
  }

  /// <summary>Получить узел с настройками для вложенных закладок</summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Узел с настройками для вложенных закладок</returns>
  private XmlNode GridXMLGetEmbeddedViewsState(XmlDocument xmlDoc)
  {
    return (XmlNode) xmlDoc.CreateElement("EmbeddedViews");
  }

  /// <summary>
  /// Загрузить настройки для вложенных закладок из указанного узла
  /// </summary>
  /// <param name="node">Узел с настройками для вложенных закладок</param>
  private void GridXMLRestoreEmbeddedViewsState(XmlNode node)
  {
  }

  /// <summary>
  /// Получить статус указанной команды в текущем контексте закладки
  /// </summary>
  /// <param name="commandState">Статус команды</param>
  /// <returns>true, если статус корректно установлен</returns>
  private bool QueryStatusCurrentContext(ICommandState commandState)
  {
    if (string.IsNullOrEmpty(commandState.CommandName))
      return false;
    bool flag = this._currentCommandsTable != null && this._currentCommandsTable.Contains(commandState.CommandName);
    commandState.Enabled = flag;
    return flag;
  }

  /// <summary>
  /// Получить статус указанной команды из вложенных закладок
  /// </summary>
  /// <param name="commandState">Статус команды</param>
  /// <returns>true, если статус корректно установлен</returns>
  private bool QueryStatusEmbedded(ICommandState commandState)
  {
    return (this._embeddedViewsState & ChildrenView.EmbeddedViewsState.Open) != ChildrenView.EmbeddedViewsState.None && this._pageViewsManager.QueryStatus(commandState);
  }

  /// <summary>Выполнить указанную команду во вложенных закладках</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если команда успешно обработана</returns>
  private bool ExecuteEmbedded(ICommandState commandState)
  {
    return (this._embeddedViewsState & ChildrenView.EmbeddedViewsState.Open) != ChildrenView.EmbeddedViewsState.None && this._pageViewsManager.Execute(commandState);
  }

  /// <summary>
  /// Выполняет команду контекстного меню "Настройка ручной сортировки"
  /// </summary>
  /// <param name="selectedItems">Список выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="additionalInfo">Дополнительная информация</param>
  internal void ManualSortingSetupCommand(
    ISelectedItems selectedItems,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ISelectedItems selectedItems1 = selectedItems;
    if (selectedItems1 == null || selectedItems1.Count == 0)
      selectedItems1 = (ISelectedItems) this._parentSelItem;
    long[] ChRels = (long[]) null;
    if (ManualSortingEditForm.Execute(string.Empty, selectedItems1, (System.IServiceProvider) this._services, out ChRels) != DialogResult.OK || ChRels.Length == 0)
      return;
    this._notificationService.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("SortedRelationsChanged", (IList<long>) ChRels));
    this._notificationService.FireEvent((object) this, new NotificationEventArgs("FiltrationChanged"));
  }

  /// <summary>Индексы выделенных строк</summary>
  internal List<int> GetSelectedHandles()
  {
    List<int> intList = new List<int>();
    List<int> selectedHandles = new List<int>();
    for (int index = 0; index < this._grid.SelectedCells.Count; ++index)
    {
      iGCell selectedCell = this._grid.SelectedCells[index];
      intList.Add(selectedCell.RowIndex);
    }
    intList.Sort();
    for (int index = 0; index < intList.Count; ++index)
    {
      if (selectedHandles.Count == 0 || intList[index] != selectedHandles[selectedHandles.Count - 1])
        selectedHandles.Add(intList[index]);
    }
    return selectedHandles;
  }

  /// <summary>
  /// Обновляет виды на дополнительной панели при изменении выделения в гриде.
  /// </summary>
  private void UpdateEmbeddedViews()
  {
    this._pageViewsManager.ActiveViewPageChanged -= new EventHandler(this.PageViewsManager_ActiveViewPageChanged);
    this._navigatorColumnsService.ColumnsChanged -= new EventHandler<NavigatorColumnsChangedEventArgs>(this.NavigatorColumnsService_ColumnsChanged);
    try
    {
      this._pageViewsManager.UpdateViews(this.SelectedItems, true);
      this._delayedUpdateTimer.Stop();
      this._embeddedViewsDropDownMenuItem.Items.Clear();
      string str = this._pageViewsManager.ActiveViewPage == null || this._pageViewsManager.ActiveViewPage.View == null ? LocalizationHolder.rm.GetString("Client.Core_1394") : this._pageViewsManager.ActiveViewPage.View.Caption;
      if (this._pageViewsManager.ViewPages.Count > 0)
      {
        for (int index = 0; index < this._pageViewsManager.ViewPages.Count; ++index)
        {
          this._embeddedViewsDropDownMenuItem.Image = (Image) null;
          if (this._pageViewsManager.ViewPages[index].ViewDescription != null)
          {
            this._embeddedViewsDropDownMenuItem.Items.Add(this._pageViewsManager.ViewPages[index].ViewDescription.Caption, new EventHandler(this.OpenEmbeddedViewMenuButtonItem_Click));
            this._embeddedViewsDropDownMenuItem.Items[index].ImageIndex = this._pageViewsManager.ViewPages[index].ViewDescription.ImageIndex;
          }
          else
          {
            this._embeddedViewsDropDownMenuItem.Items.Add(this._pageViewsManager.ViewPages[index].View.Caption, new EventHandler(this.OpenEmbeddedViewMenuButtonItem_Click));
            this._embeddedViewsDropDownMenuItem.Items[index].ImageIndex = this._pageViewsManager.ViewPages[index].View.ImageIndex;
          }
          this._embeddedViewsDropDownMenuItem.Items[index].Checked = this._embeddedViewsDropDownMenuItem.Items[index].Text == str;
        }
      }
      this.UpdateEmbeddedViewsContols();
    }
    finally
    {
      this._pageViewsManager.ActiveViewPageChanged += new EventHandler(this.PageViewsManager_ActiveViewPageChanged);
      this._navigatorColumnsService.ColumnsChanged += new EventHandler<NavigatorColumnsChangedEventArgs>(this.NavigatorColumnsService_ColumnsChanged);
    }
  }

  /// <summary>
  /// Обновляет элементы управления панелью дополнительных видов.
  /// </summary>
  private void UpdateEmbeddedViewsContols()
  {
    if (this._pageViewsManager.ActiveViewPage != null)
    {
      string str = this._pageViewsManager.ActiveViewPage == null || this._pageViewsManager.ActiveViewPage.View == null ? string.Empty : this._pageViewsManager.ActiveViewPage.View.Caption;
      this._embeddedViewsDropDownMenuItem.ToolTipText = string.Format(LocalizationHolder.rm.GetString("Client.Core_512"), (object) str);
      for (int index = 0; index < this._embeddedViewsDropDownMenuItem.Items.Count; ++index)
        this._embeddedViewsDropDownMenuItem.Items[index].Checked = this._embeddedViewsDropDownMenuItem.Items[index].Text == str;
    }
    else
    {
      this._embeddedViewsDropDownMenuItem.Items.Clear();
      this._embeddedViewsDropDownMenuItem.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_511");
      for (int index = 0; index < this._embeddedViewsDropDownMenuItem.Items.Count; ++index)
        this._embeddedViewsDropDownMenuItem.Items[index].Checked = false;
    }
    if (this._pageViewsManager.ActiveViewPage != null && this._pageViewsManager.ActiveViewPage.View != null)
    {
      this._embeddedViewsDropDownMenuItem.ImageIndex = this._pageViewsManager.ActiveViewPage.View.ImageIndex;
      this._embeddedViewsDropDownMenuItem.Text = this._pageViewsManager.ActiveViewPage.View.Caption;
      this._embeddedViewsDropDownMenuItem.Checked = true;
      this._embeddedViewsOpened = true;
    }
    else
    {
      this._embeddedViewsDropDownMenuItem.ImageIndex = -1;
      this._embeddedViewsDropDownMenuItem.Icon = (Icon) null;
      this._embeddedViewsDropDownMenuItem.ImageIndex = 11;
      this._embeddedViewsDropDownMenuItem.Checked = false;
      this._embeddedViewsDropDownMenuItem.Text = LocalizationHolder.rm.GetString("Client.Core_1394");
      this._embeddedViewsOpened = false;
    }
  }

  /// <summary>
  /// Очистить источник данных и грид, затем обновить родительский узел.
  /// </summary>
  private void ClearRows()
  {
    if (this._dataAdapter == null)
      return;
    this._dataAdapter.ClearRows();
    if (this.Node != null)
      this.Node.Refresh();
    this._groupRowsCount = 0;
    this._collapsedRowsCount = 0;
    this.GridSelectionChanged();
  }

  /// <summary>Читает очередную порцию данных</summary>
  private void FetchRows(int count)
  {
    if (this._dataAdapter == null)
      return;
    this._grid.AfterContentsGrouped -= new EventHandler(this.Grid_AfterContentsGrouped);
    this._grid.AfterContentsSorted -= new EventHandler(this.Grid_AfterContentsSorted);
    try
    {
      this._dataAdapter.LoadRows(count, this._grid.Rows.Count == 0 && this._grid.SelectedCells.Count == 0 && this._stateBeforeSort == null && this._stateBeforeGroup == null && this._refreshState == null && !this._childrenViewOldSearchSelectionFeature.Enabled);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      this._grid.AfterContentsGrouped += new EventHandler(this.Grid_AfterContentsGrouped);
      this._grid.AfterContentsSorted += new EventHandler(this.Grid_AfterContentsSorted);
    }
  }

  /// <summary>Читает очередную порцию данных в грид.</summary>
  private void FetchItems()
  {
    ++this._preventSelectionChanged;
    try
    {
      this.InternalFetchItems();
    }
    finally
    {
      --this._preventSelectionChanged;
    }
  }

  /// <summary>
  /// Ставит метод на выполнение по срабатыванию таймера с малым временем ожидания.
  /// </summary>
  /// <param name="method"></param>
  private void QueuePostProcess(MethodInvoker method)
  {
    this._postProcessTimer.Tag = (object) method;
    this._postProcessTimer.Start();
  }

  /// <summary>
  /// Получить списки группирующих и сортируемых колонок в гриде
  /// </summary>
  /// <returns>Списки группирующих и сортируемых колонок в гриде</returns>
  private GroupingAndSortingColumns GridGetGroupingAndSortingColumns()
  {
    this.GridReflectColumnsProperties();
    NodeColumnCollection sortedColums = new NodeColumnCollection();
    NodeColumnCollection groupingColums = new NodeColumnCollection();
    NodeColumnCollection nodeColumns1 = this.GetNodeColumns();
    for (int index = 0; index < nodeColumns1.Count; ++index)
    {
      if (nodeColumns1[index].SortOrder != NodeColumnSortOrder.None && nodeColumns1[index].SortIndex >= 0)
        sortedColums.Add(nodeColumns1[index].Clone() as NodeColumn);
    }
    NodeColumnCollection nodeColumns2 = this.GetNodeColumns();
    List<int> groupColumns = this.GridGetGroupColumns(nodeColumns2);
    for (int index = 0; index < groupColumns.Count; ++index)
      groupingColums.Add(nodeColumns2[groupColumns[index]].Clone() as NodeColumn);
    return new GroupingAndSortingColumns(groupingColums, sortedColums);
  }

  /// <summary>
  /// Применить указанные настройки сортировки/группирования к списку колонок
  /// </summary>
  /// <param name="value">Списки группирующих и сортируемых колонок в гриде</param>
  private void GridSetGroupingAndSortingColumns(GroupingAndSortingColumns value)
  {
    if (value == null || value.GroupingColums == null || value.SortedColums == null)
      return;
    iFocusAndSelection focusAndSelection = this.GridGetFocusAndSelection();
    try
    {
      NodeColumnCollection nodeColumns = this.GetNodeColumns();
      for (int index = value.SortedColums.Count - 1; index >= 0; --index)
      {
        if (!nodeColumns.Contains(value.SortedColums[index]))
          value.SortedColums.RemoveAt(index);
      }
      for (int index = value.GroupingColums.Count - 1; index >= 0; --index)
      {
        if (!nodeColumns.Contains(value.GroupingColums[index]))
          value.GroupingColums.RemoveAt(index);
      }
      NodeColumnCollection.CorrectSortIndex(value.SortedColums);
      NodeColumnCollection.CorrectSortIndex(value.GroupingColums);
      List<int> groups = new List<int>();
      for (int index = 0; index < nodeColumns.Count; ++index)
      {
        NodeColumn nodeColumn1 = nodeColumns[index];
        NodeColumn nodeColumn2 = value.SortedColums.Find(nodeColumn1.Key);
        if (nodeColumn2 != null)
        {
          nodeColumn1.SortIndex = nodeColumn2.SortIndex;
          nodeColumn1.SortOrder = nodeColumn2.SortOrder;
          nodeColumn1.GroupIndex = nodeColumn2.GroupIndex;
        }
        else
        {
          nodeColumn1.SortOrder = NodeColumnSortOrder.None;
          nodeColumn1.SortIndex = -1;
          nodeColumn1.GroupIndex = -1;
        }
      }
      this.SetColumns(nodeColumns, value.GroupingColums.Count == 0);
      if (value.GroupingColums.Count > 0)
      {
        groups.Clear();
        for (int index = 0; index < value.GroupingColums.Count; ++index)
          groups.Add(nodeColumns.IndexOf(value.GroupingColums[index]));
        this.GridSetGroups(this.GetNodeColumns(), groups, true);
      }
    }
    finally
    {
      this.GridSetFocusAndSelection(focusAndSelection, true);
    }
    this._isColumnsObsoleted = false;
    this.GridSaveState((Stream) null);
  }

  /// <summary>Получить строку-ключ для указанной колонки</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Строка-ключ колонки</returns>
  private string GetColumnKey(NodeColumn column)
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    return $"{column.SchemeGuid.ToString()},{service.ColumnIDToPersistName(column.SchemeGuid, column.ID)}";
  }

  /// <summary>Вернуть порядок сортировки согласно iGrid</summary>
  /// <param name="sortOrder">Наш порядок сортировки</param>
  /// <returns>Порядок сортировки для iGrid</returns>
  private iGSortOrder GetSortOrder(NodeColumnSortOrder sortOrder)
  {
    if (sortOrder == NodeColumnSortOrder.Ascending)
      return iGSortOrder.Ascending;
    return sortOrder == NodeColumnSortOrder.Descending ? iGSortOrder.Descending : iGSortOrder.None;
  }

  /// <summary>Вернуть наш порядок сортировки</summary>
  /// <param name="sortOrder">Порядок сортировки для iGrid</param>
  /// <returns>Наш порядок сортировки</returns>
  private NodeColumnSortOrder GetSortOrder(iGSortOrder sortOrder)
  {
    if (sortOrder == iGSortOrder.Ascending)
      return NodeColumnSortOrder.Ascending;
    return sortOrder == iGSortOrder.Descending ? NodeColumnSortOrder.Descending : NodeColumnSortOrder.None;
  }

  /// <summary>
  /// Возвращает колонку в гриде, соответствующую указанной колонке в источнике данных
  /// </summary>
  /// <param name="column">Колонка источника данных</param>
  /// <returns>Колонка грида или null</returns>
  private iGCol GridGetColumn(NodeColumn column)
  {
    return column == null ? (iGCol) null : this._grid.Cols[this.GetColumnKey(column)];
  }

  /// <summary>
  /// Возвращает колонку источника данных, соответствующую указанной колонке в в гриде
  /// </summary>
  /// <param name="column">Колонка грида</param>
  /// <returns>Колонка источника данных или null</returns>
  private NodeColumn iGridGetColumn(iGCol column)
  {
    return column == null ? (NodeColumn) null : column.Tag as NodeColumn;
  }

  /// <summary>
  /// Найти колонку с соответствующим ключом в указанной коллекции
  /// </summary>
  /// <param name="columnKey">Ключ колонки</param>
  /// <param name="columns">Коллекция колонок источника данных</param>
  /// <returns>Индекс найденной колонки или -1</returns>
  private int GetColumnIndex(string columnKey, NodeColumnCollection columns)
  {
    if (columns == null || columns.Count == 0 || columnKey == string.Empty)
      return -1;
    for (int index = 0; index < columns.Count; ++index)
    {
      if (this.GetColumnKey(columns[index]) == columnKey)
        return index;
    }
    return -1;
  }

  /// <summary>
  /// Разрешать для указанной колонки особую обработку значений при группировании
  /// </summary>
  /// <param name="column">Колонка</param>
  /// <returns>true - для значений из данной колонки при группировании будет выполняться особая обработка</returns>
  private bool GridAllowCustomGroupValues(NodeColumn column)
  {
    return column != null && (column.DataType == typeof (DateTime) || column.AttrType == FieldTypes.ftMeasured);
  }

  /// <summary>
  /// Перенести изменения от колонок грида в источник данных
  /// </summary>
  /// <returns>true, если у любой колонки изменились ключевое свойство - сортировка</returns>
  private bool GridReflectColumnsProperties()
  {
    bool flag = false;
    for (int index = 0; index < this._grid.GroupObject.Count; ++index)
      flag |= this.GridReflectColumnSorting(this._grid.GroupObject[index], false);
    for (int index = 0; index < this._grid.SortObject.Count; ++index)
      flag |= this.GridReflectColumnSorting(this._grid.SortObject[index], true);
    for (int index = 0; index < this._grid.Cols.Count; ++index)
      flag |= this.GridReflectColumnProperties(this._grid.Cols[index]);
    if (flag)
      this.InvalidateMenuServiceContainer();
    return flag;
  }

  /// <summary>
  /// Перенести изменения свойств колонки в гриде в соответствующую колонку источника данных
  /// </summary>
  /// <param name="gridColumn">Колонка грида</param>
  /// <returns>true, если у колонки изменились свойства</returns>
  private bool GridReflectColumnProperties(iGCol gridColumn)
  {
    bool flag = false;
    if (gridColumn == null || !(gridColumn.Tag is NodeColumn tag))
      return flag;
    tag.Width = gridColumn.Width;
    if (this._grid.SortObject.IndexOf(gridColumn.Index) < 0 && this._grid.GroupObject.IndexOf(gridColumn.Index) < 0)
    {
      tag.SortOrder = NodeColumnSortOrder.None;
      tag.SortIndex = -1;
      tag.GroupIndex = -1;
    }
    return flag;
  }

  /// <summary>
  /// Уточнить общий индекс сортировки для колонки с указанным номером
  /// </summary>
  /// <param name="colIndex">Номер колонки в гриде</param>
  /// <returns>Общий индекс сортировки для колонки с указанным номером</returns>
  private int GetColGroupSortIndex(int colIndex)
  {
    if (this._grid.GroupObject.Contains(colIndex))
      return this._grid.GroupObject.IndexOf(colIndex);
    if (!this._grid.SortObject.Contains(colIndex))
      return -1;
    int num = this._grid.SortObject.IndexOf(colIndex);
    int colGroupSortIndex = this._grid.GroupObject.Count + num;
    for (int index = 0; index < num; ++index)
    {
      if (this._grid.GroupObject.Contains(this._grid.SortObject[index].ColIndex))
        --colGroupSortIndex;
    }
    return colGroupSortIndex;
  }

  /// <summary>
  /// Перенести изменения в сортировке от колонки в гриде в соответствующую колонку источника данных
  /// </summary>
  /// <param name="sortItem">Сортируемый элемент грида</param>
  /// <param name="checkGroupIndex">true - выполняется проверка GroupIndex у колонок</param>
  /// <returns>true, если у колонки изменились ключевое свойство - сортировка</returns>
  private bool GridReflectColumnSorting(iGSortItem sortItem, bool checkGroupIndex)
  {
    bool flag = false;
    iGCol col = sortItem != null ? this._grid.Cols[sortItem.ColIndex] : (iGCol) null;
    int colGroupSortIndex = this.GetColGroupSortIndex(col.Index);
    if (col == null || !(col.Tag is NodeColumn tag))
      return flag;
    tag.Width = col.Width;
    if (tag.GroupIndex != col.GroupIndex)
      tag.GroupIndex = col.GroupIndex;
    NodeColumnSortOrder sortOrder = this.GetSortOrder(sortItem.SortOrder);
    if (tag.SortOrder != sortOrder && (!checkGroupIndex || tag.GroupIndex < 0))
    {
      flag = true;
      tag.SortOrder = sortOrder;
    }
    if (tag.SortIndex != colGroupSortIndex)
    {
      flag = true;
      tag.SortIndex = colGroupSortIndex;
    }
    if (tag.SortOrder == NodeColumnSortOrder.None && tag.SortIndex >= 0)
    {
      flag = true;
      tag.SortIndex = -1;
    }
    return flag;
  }

  /// <summary>
  /// Рассчитать ширину и высоту текста для указанного контрола
  /// </summary>
  /// <param name="font">Шрифт контрола</param>
  /// <param name="text">Текст</param>
  /// <returns>Ширина и высота текста</returns>
  private SizeF CalculateTextBounds(Font font, string text)
  {
    Graphics graphics = this.CreateGraphics();
    int width = Screen.PrimaryScreen.WorkingArea.Width / 100 * 50;
    SizeF textBounds = graphics.MeasureString(text, font, width, StringFormat.GenericDefault);
    graphics.Dispose();
    return textBounds;
  }

  /// <summary>
  /// Получить ссылку на ячейку заголовка, находящуюся в гриде по указанным координатам
  /// </summary>
  /// <param name="pos">Координаты курсора мыши</param>
  /// <returns>Ячейка заголовка или null</returns>
  private iGColHdr GetHeaderCursor(Point pos) => this._grid.Header.Cells.FromPoint(pos.X, pos.Y);

  /// <summary>
  /// Обновляет связанные контролы, зависящие от выделенных в гриде элементов.
  /// </summary>
  private void UpdateLinkedControls()
  {
    this.UpdateStatusbar();
    this.UpdateToolbar();
    if ((this._embeddedViewsState & ChildrenView.EmbeddedViewsState.Open) != ChildrenView.EmbeddedViewsState.None)
      this.UpdateEmbeddedViews();
    else
      this._embeddedViewsState |= ChildrenView.EmbeddedViewsState.InvalidData;
  }

  /// <summary>
  /// Чтобы лишний раз не дёргать опрос статуса ВСЕХ команд у ICommandManager,
  /// вызовем опрос только для команд, которые реализуются в ChildrenView
  /// </summary>
  private void UpdateCommandManagerItems()
  {
    ICommandManager service = this._services != null ? this._services.GetService(typeof (ICommandManager)) as ICommandManager : (ICommandManager) null;
    if (service == null)
      return;
    try
    {
      this._disableCloneSelectedItems = true;
      service.QueryStatus();
    }
    finally
    {
      this._disableCloneSelectedItems = false;
    }
  }

  /// <summary>Запретить элементы управления</summary>
  private void DisableControls()
  {
    ICommandManager service = this._services != null ? this._services.GetService(typeof (ICommandManager)) as ICommandManager : (ICommandManager) null;
    if (service == null)
      return;
    service.QueryStatus();
    ICommandState command1 = service.FindCommand("FetchTree");
    if (command1 != null)
      command1.Enabled = false;
    ICommandState command2 = service.FindCommand("Refresh");
    if (command2 == null)
      return;
    command2.Enabled = false;
  }

  public void Group()
  {
    if (this._grid.GroupObject.Count <= 0)
      return;
    this._groupRowsCount = 0;
    this._collapsedRowsCount = 0;
    bool suppressUpdateStatusbar = this._suppressUpdateStatusbar;
    this._suppressUpdateStatusbar = true;
    try
    {
      this._grid.Group();
    }
    finally
    {
      this._suppressUpdateStatusbar = suppressUpdateStatusbar;
      this.UpdateStatusbar();
    }
  }

  /// <summary>
  /// Обновляет элементы статусной строки, зависящие от выделенных в гриде элементов.
  /// </summary>
  private void UpdateStatusbar()
  {
    if (EventsViewConsts.IsFile || this._dataAdapter == null || this._suppressUpdateStatusbar)
      return;
    string str1 = string.Format(LocalizationHolder.rm.GetString("Client.Core_1175"), (object) this._dataAdapter.ReadedRecordCount);
    if (this._readedRecordsCountToolStripStatusLabel.Text != str1)
      this._readedRecordsCountToolStripStatusLabel.Text = str1;
    string str2 = string.Format(LocalizationHolder.rm.GetString("Client.Core_516"), (object) (this._gridSelectedItems != null ? this._gridSelectedItems.Count : 0));
    if (this._selectedRecordsCountToolStripStatusLabel.Text != str2)
      this._selectedRecordsCountToolStripStatusLabel.Text = str2;
    string str3 = string.Format(LocalizationHolder.rm.GetString("Client.Core_517"), (object) this._groupRowsCount);
    if (this._groupsCountToolStripStatusLabel.Text != str3)
      this._groupsCountToolStripStatusLabel.Text = str3;
    iGCell curCell = this._grid.CurCell;
    iGCell iGcell = curCell != null || this._grid.SelectedCells.Count <= 0 ? curCell : this._grid.SelectedCells[0];
    INodeID nodeIdForRow = iGcell != null ? this.GetNodeIDForRow(iGcell.Row) : (INodeID) null;
    IDBCheckedOutByID data1 = this._node == null || nodeIdForRow == null ? (IDBCheckedOutByID) null : this._node.GetData(nodeIdForRow, typeof (IDBCheckedOutByID)) as IDBCheckedOutByID;
    IDBTypedObjectID data2 = this._node == null || nodeIdForRow == null ? (IDBTypedObjectID) null : this._node.GetData(nodeIdForRow, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBRelationID data3 = this._node == null || nodeIdForRow == null ? (IDBRelationID) null : this._node.GetData(nodeIdForRow, typeof (IDBRelationID)) as IDBRelationID;
    if (data3 != null && data3.Value == -1L)
      ;
    this._checkedOutByToolStripStatusLabel.Text = data1 == null || data1.CheckedOutBy == 0L ? string.Empty : ChildrenView._userNamesCache.GetUserName(data1.CheckedOutBy);
    if (data1 != null && data1.CheckedOutBy != 0L)
    {
      if (data1.CheckedOutBy == this._currentUserAndRole.UserID)
      {
        int index = ChildrenView._namedImageList.ImageIndex("imgUserCurrent");
        if (index >= 0)
          this._checkedOutByToolStripStatusLabel.Image = ChildrenView._namedImageList.ImageList.Images[index];
      }
      else
      {
        int index = ChildrenView._namedImageList.ImageIndex("imgUserOther");
        if (index >= 0)
          this._checkedOutByToolStripStatusLabel.Image = ChildrenView._namedImageList.ImageList.Images[index];
      }
    }
    this._checkedOutByToolStripStatusLabel.Visible = this._checkedOutByToolStripStatusLabel.Text != string.Empty;
    this.labelDivider5.Visible = this._checkedOutByToolStripStatusLabel.Visible;
    string str4 = string.Empty;
    string str5 = string.Empty;
    if (data2 != null)
    {
      str4 = data2.Caption.Replace('\n', ' ').Replace('\t', ' ').Replace('\r', ' ');
      IMSObjectType objectType = MetaDataHelper.GetObjectType(data2.ObjectType);
      str5 = objectType != null ? $"{objectType.ObjectName}:" : string.Empty;
    }
    this._objectCaptionToolStripStatusLabel.Text = str4;
    this._objectCaptionToolStripStatusLabel.ToolTipText = data2 != null ? data2.Caption : string.Empty;
    if (string.IsNullOrEmpty(str5))
    {
      this._objectTypeNameToolStripStatusLabel.Text = string.Empty;
      this._objectTypeNameToolStripStatusLabel.Visible = false;
      this.labelDivider6.Visible = false;
    }
    else
    {
      this._objectTypeNameToolStripStatusLabel.Text = str5;
      this._objectTypeNameToolStripStatusLabel.Visible = true;
      this.labelDivider6.Visible = true;
    }
  }

  /// <summary>
  /// Сохранить фокус и выделенные узлы, включая информацию из вложенных закладок
  /// </summary>
  /// <returns>Точка восстановления</returns>
  private iFocusAndSelection GridGetFullFocusAndSelection()
  {
    iGCell curCell = this._grid.CurCell;
    iGCell iGcell = curCell != null || this._grid.SelectedCells.Count <= 0 ? curCell : this._grid.SelectedCells[0];
    INodeID nodeIdForRow1 = iGcell != null ? this.GetNodeIDForRow(iGcell.Row) : (INodeID) null;
    int focusedIndex = iGcell != null ? iGcell.Row.Index : -1;
    List<INodeID> selectedRows = new List<INodeID>();
    List<int> selectedIndexes = new List<int>();
    for (int index = 0; index < this._grid.SelectedCells.Count; ++index)
    {
      iGRow row = this._grid.SelectedCells[index].Row;
      if (row.Type == iGRowType.Normal)
      {
        INodeID nodeIdForRow2 = this.GetNodeIDForRow(row);
        if (nodeIdForRow2 != null)
        {
          selectedRows.Add(nodeIdForRow2);
          if (selectedIndexes.IndexOf(row.Index) < 0)
            selectedIndexes.Add(row.Index);
        }
      }
    }
    selectedIndexes.Sort();
    string str = string.Empty;
    if (((IEmbeddedViews) this).IsOpen)
    {
      IViewPage activeViewPage = this._pageViewsManager.ActiveViewPage;
      str = activeViewPage != null ? activeViewPage.Name : string.Empty;
    }
    iFocusAndSelection focusAndSelection = this.EmbeddedFocusAndSelection;
    if (focusAndSelection != null)
      focusAndSelection.ActivePage = str;
    return new iFocusAndSelection(nodeIdForRow1, selectedRows, focusedIndex, selectedIndexes, string.Empty, focusAndSelection)
    {
      ChildrenViewHeight = this.EMVAbsHeight,
      CollapsedGroups = this.GetCollapsedGroups()
    };
  }

  /// <summary>
  /// Показать строку в гриде, даже если строка находится в составе группирующих строк
  /// </summary>
  /// <param name="row">Отображаемая строка</param>
  private void GridShowGridRow(iGRow row)
  {
    if (row != null && !this._childrenViewOldSearchSelectionFeature.Enabled)
      this.SetSelectedForRow(row, true);
    if (this._grid.Cols.Count >= 2)
      this._grid.SetCurCell(row.Index, 1);
    this._grid.CurRow = row;
    row.EnsureVisible();
  }

  /// <summary>
  /// Восстановить фокус и выделенные записи, включая информацию о вложенных закладках
  /// </summary>
  /// <param name="selectFirst">Выделять первую строку, если нет ни одной выделенной строки</param>
  /// <param name="state">Точка восстановления</param>
  private void GridSetFullFocusAndSelection(iFocusAndSelection state, bool selectFirst)
  {
    if (this._dataAdapter == null)
      return;
    bool flag = this._grid.SelectedCells.Count > 0;
    try
    {
      this._grid.Redraw = false;
      this._grid.BeginUpdate();
      this._grid.PerformAction(iGActions.DeselectAll);
      if (state == null)
        return;
      this.SetGroupState(state.CollapsedGroups);
      if (state.SelectedRows != null)
      {
        this._grid.SelectionChanged -= new EventHandler(this.Grid_SelectionChanged);
        try
        {
          for (int index = 0; index < state.SelectedRows.Count; ++index)
          {
            iGRow rowWithNodeId = this.GetRowWithNodeID(state.SelectedRows[index]);
            if (rowWithNodeId != null)
            {
              rowWithNodeId.SetSelectedForAllCells(true);
              flag = true;
            }
          }
          this.Grid_SelectionChanged((object) this, EventArgs.Empty);
        }
        finally
        {
          this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
        }
      }
      if (state.FocusedRow == null)
        return;
      iGRow rowWithNodeId1 = this.GetRowWithNodeID(state.FocusedRow);
      if (rowWithNodeId1 == null)
        return;
      this.GridShowGridRow(rowWithNodeId1);
    }
    finally
    {
      if (!flag && this._grid.Rows.Count > 0 && this._grid.GroupObject.Count == 0 && this._stateBeforeSort == null && this._stateBeforeGroup == null && this._grid.SelectedCells.Count == 0 && state != null && state.FocusedIndex >= 0)
      {
        int index = Math.Min(state.FocusedIndex, this._grid.Rows.Count - 1);
        if (index >= 0 && index < this._grid.Rows.Count)
          this.GridShowGridRow(this._grid.Rows[index]);
      }
      if (selectFirst && !flag && this._grid.Rows.Count > 0 && this._grid.GroupObject.Count == 0 && this._stateBeforeSort == null && this._stateBeforeGroup == null && !this.DisableAutoselectFirstRow && (state == null || state.SelectedRows == null || state.SelectedRows.Count == 0))
        this.GridShowGridRow(this._grid.Rows[0]);
      this.SelectionChanged();
      if (state != null && state.SubviewSelection != null)
        this.OpenEmbeddedViews();
      if (((IEmbeddedViews) this).IsOpen && state != null && state.SubviewSelection != null && state.SubviewSelection.ActivePage != string.Empty)
      {
        for (int index = 0; index < this._pageViewsManager.ViewPages.Count; ++index)
        {
          if (this._pageViewsManager.ViewPages[index].Name == state.SubviewSelection.ActivePage)
          {
            this._pageViewsManager.ActiveViewPage = this._pageViewsManager.ViewPages[index];
            break;
          }
        }
      }
      if (state != null && state.SubviewSelection != null)
        this.EmbeddedFocusAndSelection = state.SubviewSelection;
      this._grid.EndUpdate();
      this._grid.Redraw = true;
      if (state != null)
        this.EMVAbsHeight = state.ChildrenViewHeight;
    }
  }

  /// <summary>
  /// В гриде изменился порядок сортировки/группировки колонок
  /// </summary>
  private void RaiseSortingGroupingChanged()
  {
    if (this.SortingGroupingChanged == null)
      return;
    this.SortingGroupingChanged((object) this, new EventArgs());
  }

  /// <summary>Грид начинает сортировку информации</summary>
  private void BeforeContentsSorted()
  {
    this._tryingSorting = true;
    if (this._preventSorting)
      return;
    if (this._manualSorting)
    {
      this._manualSorting = false;
      this._toggleManualSortingButtonItem.Checked = false;
      this._lastGroupingAndSortingColumns = (GroupingAndSortingColumns) null;
    }
    this._rowsBeforeSort = this._grid.Rows.Count;
    if (this._rowsBeforeSort > 0)
    {
      this._stateBeforeSort = this.GridGetFocusAndSelection();
      this.ClearData();
    }
    this._tryingSorting = false;
  }

  /// <summary>Удалить группирование колонок или разрешить его</summary>
  private void GridClearGrouping()
  {
    UISettings.DisableChildrenViewGrouping = !UISettings.DisableChildrenViewGrouping;
    this.SetDisableChildrenViewGrouping();
  }

  /// <summary>Раскрыть все группы</summary>
  private void ExpandAllGroups()
  {
    this._grid.PerformAction(iGActions.ExpandAll);
    this._collapsedRowsCount = 0;
  }

  /// <summary>Свернуть все группы</summary>
  private void CollapseAllGroups()
  {
    this._grid.PerformAction(iGActions.CollapseAll);
    this._collapsedRowsCount = this._groupRowsCount;
  }

  /// <summary>Свернуть все группы, кроме группы текущей ячейки</summary>
  private void CollapseAllGroupsExceptGroupsWithSelections()
  {
    iGCell curCell = this._grid.CurCell;
    iGRow row = (curCell != null || this._grid.SelectedCells.Count <= 0 ? curCell : this._grid.SelectedCells[0])?.Row;
    this._grid.PerformAction(iGActions.CollapseAll);
    this._collapsedRowsCount = this._groupRowsCount;
    if (row == null || row.Type != iGRowType.Normal)
      return;
    iGRow iGrow = row;
    int num = int.MaxValue;
    for (; row != null && row.Index >= 0; row = row.Index > 0 ? this._grid.Rows[row.Index - 1] : (iGRow) null)
    {
      if (row.Type != iGRowType.Normal && row.Level < num)
      {
        row.Expanded = true;
        num = row.Level;
        if (num == 0)
          break;
      }
    }
    iGrow.EnsureVisible();
  }

  /// <summary>Изменились выделенные в гриде ячейки</summary>
  private void GridSelectionChanged()
  {
    ISelectedItems items = (ISelectedItems) new ChildrenViewSelectedItems(this._path, this.Node, this);
    if (items.Count == 0)
      items = (ISelectedItems) this._parentSelItem;
    this._currentContextMenuServiceProvider = (System.IServiceProvider) this.GetMenuServiceContainer();
    this._currentCommandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, this._currentContextMenuServiceProvider);
    if (this._contextMenuActive)
      return;
    this._gridSelectedItems.Invalidate();
    this.AddSimpleSelectedItemsToGlobalServiceContainer();
    if (!this._disableSelectedItemsChanged && this.SelectedItemsChanged != null)
      this.SelectedItemsChanged((object) this, EventArgs.Empty);
    if (this.DisableDelayedUpdates)
    {
      this.DelayedUpdateTimer_Tick((object) this, (EventArgs) null);
    }
    else
    {
      this.UpdateControls();
      this.UpdateStatusbar();
      this.UpdateToolbar();
      this._delayedUpdateTimer.Stop();
      this._delayedUpdateTimer.Start();
    }
  }

  private void AddSimpleSelectedItemsToGlobalServiceContainer()
  {
    if (!this.IsSimpleSelectedItemsSuppoted || !this._grid.Focused)
      return;
    ChildrenViewSelectedItems serviceInstance = new ChildrenViewSelectedItems(this._path, this.Node, this);
    if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) != null)
      ServicesManager.RemoveService(typeof (ISimpleSelectedItems));
    ServicesManager.AddService(typeof (ISimpleSelectedItems), (object) serviceInstance);
  }

  /// <summary>Крыса передвинулась внутри грида</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="location">Позиция крыски в координатах засланца</param>
  private void GridShowHint(object sender, Point location)
  {
    if (this.IsDisposed || this._dataAdapter == null || this._contextMenuActive)
      return;
    Point point1 = location;
    iGColHdr iGcolHdr = this._grid.Header.Cells.FromPoint(point1.X, point1.Y);
    iGCell cellCursor = this.GetCellCursor(point1);
    if (cellCursor != null)
    {
      iGRow row = cellCursor.Row;
    }
    iGCol col = cellCursor?.Col;
    int num1 = cellCursor != null ? cellCursor.ColIndex : -1;
    INodeID nodeAtCursor = this.GetNodeAtCursor(point1);
    if (cellCursor != null)
    {
      int colIndex = cellCursor.ColIndex;
    }
    NodeColumn tag = col != null ? col.Tag as NodeColumn : (NodeColumn) null;
    INodeStatusesInfo service = this.Node != null ? (INodeStatusesInfo) this.Node.GetService(typeof (INodeStatusesInfo)) : (INodeStatusesInfo) null;
    Rectangle rectangle1;
    Rectangle rectangle2;
    if (this._grid.HScrollBar.Visible)
    {
      int left = this._grid.ClientRectangle.Left;
      rectangle1 = this._grid.ClientRectangle;
      int top = rectangle1.Top;
      rectangle1 = this._grid.ClientRectangle;
      int width = rectangle1.Width;
      rectangle1 = this._grid.ClientRectangle;
      int height = rectangle1.Height - this._grid.HScrollBar.Height;
      rectangle2 = new Rectangle(left, top, width, height);
    }
    else
      rectangle2 = this._grid.ClientRectangle;
    Rectangle rectangle3 = rectangle2;
    if (this._childrenViewContextSearchManager != null && this._childrenViewContextSearchManager.InProgress || iGcolHdr == null && cellCursor == null || !rectangle3.Contains(point1) || iGcolHdr == null && cellCursor != null && (cellCursor.Value == null || cellCursor.Value == DBNull.Value || nodeAtCursor == null))
    {
      this._hintCell = (iGCell) null;
      this._hintNodeID = (INodeID) null;
      this._hintColumn = -1;
      this._hintHeader = (iGColHdr) null;
      this._hintText = string.Empty;
      this._toolTip.Hide((IWin32Window) this._grid);
      this._toolTip.SetToolTip((System.Windows.Forms.Control) this._grid, this._hintText);
    }
    else if (iGcolHdr != null && (cellCursor == null || cellCursor.Value == null || cellCursor.Value == DBNull.Value))
    {
      if (this._hintHeader != null && this._hintHeader == iGcolHdr && this._hintHeader.RowIndex == iGcolHdr.RowIndex && this._hintHeader.ColIndex == iGcolHdr.ColIndex || !(this._hintText != iGcolHdr.Value.ToString()))
        return;
      this._hintCell = (iGCell) null;
      this._hintNodeID = (INodeID) null;
      this._hintColumn = -1;
      this._hintHeader = iGcolHdr;
      this._hintText = this._hintHeader.Value.ToString();
      Point point2;
      ref Point local = ref point2;
      rectangle1 = this._hintHeader.Bounds;
      int left = rectangle1.Left;
      rectangle1 = this._hintHeader.Bounds;
      int top = rectangle1.Top;
      rectangle1 = this._hintHeader.Bounds;
      int height = rectangle1.Height;
      int y = top + height;
      local = new Point(left, y);
      this._toolTip.Show(this._hintText, (IWin32Window) this._grid, point2, 10000);
    }
    else if (cellCursor != null && tag != null && tag.ID.Equals((object) "F_STATUSES") && service != null)
    {
      if (this.Node != null)
        this.Node.GetService(typeof (FiltrateVersionsLog));
      int y1 = point1.Y;
      rectangle1 = cellCursor.Bounds;
      int y2 = rectangle1.Y;
      int num2 = y1 - y2;
      if (num2 < 0 || num2 > 18)
        return;
      int x1 = point1.X;
      rectangle1 = cellCursor.Bounds;
      int x2 = rectangle1.X;
      int num3 = x1 - x2 - 2;
      int iconIndex = num3 / 18;
      if (num3 < iconIndex * 18 + 2)
        return;
      byte[] columnValue = (byte[]) cellCursor.Value;
      System.IServiceProvider services = this.Node is IContextAware ? (this.Node as IContextAware).Services : (System.IServiceProvider) null;
      string description = service.GetDescription(services, nodeAtCursor, (object) columnValue, iconIndex);
      if (this._hintCell != null && (this._hintCell.RowIndex != cellCursor.RowIndex || this._hintCell.ColIndex != cellCursor.ColIndex) || description == string.Empty)
      {
        this._toolTip.Hide((IWin32Window) this._grid);
        this._hintText = string.Empty;
      }
      if (!(description != string.Empty) || !(description != this._hintText))
        return;
      this._hintText = description;
      this._hintCell = cellCursor;
      this._hintNodeID = nodeAtCursor;
      this._hintColumn = num1;
      this._hintHeader = (iGColHdr) null;
      Point point3;
      ref Point local = ref point3;
      rectangle1 = cellCursor.Bounds;
      int x3 = rectangle1.Left + num3;
      rectangle1 = cellCursor.Bounds;
      int top = rectangle1.Top;
      rectangle1 = cellCursor.Bounds;
      int height = rectangle1.Height;
      int y3 = top + height;
      local = new Point(x3, y3);
      this._toolTip.Show(this._hintText, (IWin32Window) this._grid, point3, 10000);
    }
    else if (cellCursor == null || cellCursor.Value == null || cellCursor.Value == DBNull.Value)
    {
      this._hintCell = (iGCell) null;
      this._hintNodeID = (INodeID) null;
      this._hintColumn = -1;
      this._hintHeader = (iGColHdr) null;
      this._hintText = string.Empty;
      this._toolTip.Hide((IWin32Window) this._grid);
      this._toolTip.SetToolTip((System.Windows.Forms.Control) this._grid, this._hintText);
    }
    else
    {
      if (this._hintCell != null && cellCursor.ColIndex == this._hintCell.ColIndex && cellCursor.RowIndex == this._hintCell.RowIndex && num1 == this._hintColumn)
        return;
      this._hintCell = cellCursor;
      this._hintNodeID = nodeAtCursor;
      this._hintColumn = num1;
      this._hintHeader = (iGColHdr) null;
      this._hintText = cellCursor.Value.ToString();
      SizeF textBounds = this.CalculateTextBounds(cellCursor.EffectiveFont, this._hintText);
      iGCellFlags flags = this._grid.Cols[cellCursor.ColIndex].CellStyle.Flags;
      if (cellCursor.Style != null && cellCursor.Style.Flags != iGCellFlags.NotSet)
        flags = cellCursor.Style.Flags;
      TenTec.Windows.iGridLib.iGrid grid = this._grid;
      rectangle1 = cellCursor.TextBounds;
      Point location1 = rectangle1.Location;
      Point screen = grid.PointToScreen(location1);
      Rectangle rect = new Rectangle(screen.X, screen.Y, Convert.ToInt32(textBounds.Width), Convert.ToInt32(textBounds.Height));
      Rectangle workingArea = Screen.GetWorkingArea(new Point(0, 0));
      double width1 = (double) textBounds.Width;
      rectangle1 = cellCursor.TextBounds;
      double width2 = (double) rectangle1.Width;
      if ((width1 > width2 || !workingArea.Contains(rect)) && (flags & iGCellFlags.DisplayText) == iGCellFlags.DisplayText || cellCursor.ColKey == "Special_CheckedOut" || cellCursor.ColKey == "Special_StateImage")
      {
        Point point4;
        ref Point local = ref point4;
        rectangle1 = cellCursor.TextBounds;
        int x = rectangle1.Left - 1;
        rectangle1 = cellCursor.TextBounds;
        int top = rectangle1.Top;
        rectangle1 = cellCursor.TextBounds;
        int height = rectangle1.Height;
        int y = top + height + 2;
        local = new Point(x, y);
        this._toolTip.Show(this._hintText, (IWin32Window) this._grid, point4, 10000);
      }
      else
      {
        this._hintCell = (iGCell) null;
        this._hintNodeID = (INodeID) null;
        this._hintColumn = -1;
        this._hintHeader = (iGColHdr) null;
        this._hintText = string.Empty;
        try
        {
          this._toolTip.Hide((IWin32Window) this._grid);
        }
        catch
        {
        }
        this._toolTip.SetToolTip((System.Windows.Forms.Control) this._grid, this._hintText);
      }
    }
  }

  /// <summary>Попробовать начать drag'n'drop с указанной точки</summary>
  /// <param name="location">Точка начала drag'n'drop</param>
  /// <returns>true, если drag'n'drop начался</returns>
  private bool GridStartDragDrop(Point location)
  {
    if (!(this._startDragRectangle != Rectangle.Empty) || this._startDragRectangle.Contains(location.X, location.Y))
      return false;
    if (this.GetNodeAtCursor(new Point(location.X, location.Y)) == null)
    {
      this._startDragRectangle = Rectangle.Empty;
      return false;
    }
    int num = (int) this._grid.DoDragDrop((object) new IOSource(this.Control, ((IIOSource) this).Services, ((IIOSource) this).SelectedItems), DragDropEffects.All);
    return true;
  }

  private System.Windows.Forms.Control GetRootParent()
  {
    System.Windows.Forms.Control rootParent = (System.Windows.Forms.Control) null;
    for (System.Windows.Forms.Control parent = this.Parent; parent != null; parent = parent.Parent)
      rootParent = parent;
    return rootParent;
  }

  private void ApplySettings()
  {
    if (!ServiceLocator.IsRegistered<IConfigurationOptionRepository>() || this.IsDisposed)
      return;
    this._grid.Font = ServiceLocator.Get<IConfigurationOptionRepository>().Find(ConfigurationOptionKeys.UI_GridFont) as Font;
    int num = FontHelper.MeasureStringFast(this._grid.Font, "Ay").Height + 6;
    this._grid.DefaultRow.Height = num;
    this._grid.DefaultRow.NormalCellHeight = num;
    this._grid.DefaultAutoGroupRow.Height = num + 3;
  }

  private void SetSplitterPosition()
  {
    int orContainerHeight = this.GetHeightOrContainerHeight();
    if (orContainerHeight == 0)
      return;
    this._splitterPosition = (double) this._embeddedViewsPanel.Height / (double) orContainerHeight;
  }

  private int CalculateEmbeddedViewsHeight()
  {
    return (int) (this._splitterPosition * (double) this.GetHeightOrContainerHeight());
  }

  private int GetHeightOrContainerHeight()
  {
    int height = this.Height;
    if (height == 0 && this.Parent != null && this.Parent.Parent != null)
      height = this.Parent.Parent.Height;
    return height;
  }

  public bool IsFiltrationEnabled()
  {
    SelectionOptionsHolder service = this.Services != null ? this.Services.GetService(typeof (SelectionOptionsHolder)) as SelectionOptionsHolder : (SelectionOptionsHolder) null;
    return this.Node != null && this.Node.Options.HasFlag((Enum) NodeOptions.CanContainsObjectsList) && (service == null || service != null && !service.Options.HasFlag((Enum) SelectionOptions.DisableObjectListFilter)) && !this.DisableFiltration;
  }

  private void InitializeCurrentVersionsRuleButtonItem()
  {
    SelectionOptionsHolder service = this._services != null ? this._services.GetService(typeof (SelectionOptionsHolder)) as SelectionOptionsHolder : (SelectionOptionsHolder) null;
    if (service == null || !service.Options.HasFlag((Enum) SelectionOptions.ForceFilterObjectsByRule))
      return;
    this._currentVersionsRuleButtonItem.Checked = true;
  }

  private void UpdateCurrentVersionsRuleButtonItem()
  {
    bool flag = this.IsFiltrationEnabled();
    this._currentVersionsRuleButtonItem.Visible = flag;
    if (!flag)
      return;
    this._currentVersionsRuleButtonItem.Enabled = this.IsCurrentVersionsRuleButtonItemEnabled() && this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
  }

  private void UpdateShowContextVersionsButtonItem()
  {
    this._showContextVersionsButtonItem.Enabled = this.SearchComponent.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this._showContextVersionsButtonItem.Visible = this.IsFiltrationEnabled();
  }

  private bool IsCurrentVersionsRuleButtonItemEnabled()
  {
    VersionsRule currentRule = !(ServicesManager.GetService(typeof (IFiltrationService)) is IFiltrationService service1) || service1.Filtration == null ? (VersionsRule) null : service1.Filtration.CurrentRule;
    ICurrentUserAndRole service2 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    List<ConditionStructure> structures = currentRule == null || service2 == null ? (List<ConditionStructure>) null : ConditionsHelper.CreateStructures(currentRule, service2.CachedEditingContextModificationID);
    return structures != null && structures.Count > 0;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INode ParentNode => this._parentNode;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INodeID NodeID => this._nodeID;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeIDPath Path => this._path;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._notificationService != null && this._notifyHandler != null)
      {
        this._notificationService.Unsubscribe(this._notifyHandler);
        this._notifyHandler = (NotificationEventHandler) null;
      }
      this.CloseEmbeddedViews();
      if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
      {
        this._toolBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        this._gridHeaderMenuBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        service.RendererChanged -= new EventHandler(this.BarManager_RendererChanged);
      }
      if (this._navGraphicsCache != null)
        this._navGraphicsCache.UIColorsSchemeChanged -= new EventHandler(this.NavGraphicsCache_UIColorsSchemeChanged);
      if (this._navigatorColumnsService != null)
        this._navigatorColumnsService.ColumnsChanged -= new EventHandler<NavigatorColumnsChangedEventArgs>(this.NavigatorColumnsService_ColumnsChanged);
      if (this._services != null)
        this._services.AdvancedProvider = (System.IServiceProvider) null;
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChildrenView));
    this._embeddedViewsPanel = new Panel();
    this._pageViewsManager = new PageViewsManager();
    this._delayedUpdateTimer = new Timer();
    this._postProcessTimer = new Timer();
    this._statusStrip = new StatusStrip();
    this._selectedRecordsCountToolStripStatusLabel = new ToolStripStatusLabel();
    this.labelDivider6 = new ToolStripStatusLabel();
    this._objectTypeNameToolStripStatusLabel = new ToolStripStatusLabel();
    this._objectCaptionToolStripStatusLabel = new ToolStripStatusLabel();
    this.labelDivider4 = new ToolStripStatusLabel();
    this._checkedOutByToolStripStatusLabel = new ToolStripStatusLabel();
    this.labelDivider5 = new ToolStripStatusLabel();
    this._readedRecordsCountToolStripStatusLabel = new ToolStripStatusLabel();
    this.labelDivider2 = new ToolStripStatusLabel();
    this._groupsCountToolStripStatusLabel = new ToolStripStatusLabel();
    this.labelDivider3 = new ToolStripStatusLabel();
    this._readNextToolStripDropDownButton = new ToolStripDropDownButton();
    this._readAllToolStripDropDownButton = new ToolStripDropDownButton();
    this._toolTip = new ToolTip();
    this._grid = new TenTec.Windows.iGridLib.iGrid();
    this._toolBar = new Intermech.Bars.ToolBar();
    this._mainImageList = new ImageList();
    this._toggleManualSortingButtonItem = new ButtonItem();
    this._manualSortingSetupButtonItem = new ButtonItem();
    this._refreshButtonItem = new ButtonItem();
    this._embeddedViewsDropDownMenuItem = new DropDownMenuItem();
    this._toggleGroupingButtonItem = new ButtonItem();
    this._collapseAllGroupsButtonItem = new ButtonItem();
    this._expandAllGroupsButtonItem = new ButtonItem();
    this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem = new ButtonItem();
    this.buttonHeightSet = new ButtonItem();
    this._filtersComboBoxItem = new ComboBoxItem();
    this._refreshFiltersDropDownMenuItem = new DropDownMenuItem();
    this._createCommonFilterMenuButtonItem = new MenuButtonItem();
    this._createPersonalFilterMenuButtonItem = new MenuButtonItem();
    this._filterCardMenuButtonItem = new MenuButtonItem();
    this._removeFilterMenuButtonItem = new MenuButtonItem();
    this._searchComboBoxItem = new ComboBoxItem();
    this._searchButtonItem = new ButtonItem();
    this._cancelSearchButtonItem = new ButtonItem();
    this._clearSearchResultsButtonItem = new ButtonItem();
    this._changeSearchSettingsButtonItem = new ButtonItem();
    this._currentVersionsRuleButtonItem = new ButtonItem();
    this._showContextVersionsButtonItem = new ButtonItem();
    this._editingModeButtonItem = new ButtonItem();
    this._embeddedViewsCollapsibleSplitter = new CollapsibleSplitter();
    this._pictureBox = new PictureBox();
    this._gridHeaderMenuBar = new MenuBar();
    this._gridHeaderContextMenuBarItem = new ContextMenuBarItem();
    this._changeGridColumnsMenuButtonItem = new MenuButtonItem();
    this._childrenViewEditingComponent = new ChildrenViewEditingComponent();
    this._objectListFiltersComponent = new ChildrenViewObjectListFiltersComponent();
    this._searchComponent = new ChildrenViewSearchComponent();
    this._autoCompleteSearchComponent = new ChildrenViewAutoCompleteSearchComponent();
    this._similarCharacterHighlightingComponent = new ChildrenViewSimilarCharacterHighlightingComponent();
    this._embeddedViewsPanel.SuspendLayout();
    this._statusStrip.SuspendLayout();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._embeddedViewsPanel.Controls.Add((System.Windows.Forms.Control) this._pageViewsManager);
    componentResourceManager.ApplyResources((object) this._embeddedViewsPanel, "_embeddedViewsPanel");
    this._embeddedViewsPanel.Name = "_embeddedViewsPanel";
    this._pageViewsManager.ActiveViewPage = (IViewPage) null;
    this._pageViewsManager.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "_pageViewsManager");
    this._pageViewsManager.HeaderAlignment = Intermech.Docking.TabAlignment.Bottom;
    this._pageViewsManager.Name = "_pageViewsManager";
    this._pageViewsManager.ActiveViewPageChanged += new EventHandler(this.PageViewsManager_ActiveViewPageChanged);
    this._pageViewsManager.Resize += new EventHandler(this.PageViewsManager_Resize);
    this._delayedUpdateTimer.Interval = 250;
    this._delayedUpdateTimer.Tick += new EventHandler(this.DelayedUpdateTimer_Tick);
    this._postProcessTimer.Interval = 5;
    this._postProcessTimer.Tick += new EventHandler(this.PostProcessTimer_Tick);
    componentResourceManager.ApplyResources((object) this._statusStrip, "_statusStrip");
    this._statusStrip.GripMargin = new Padding(0);
    this._statusStrip.Items.AddRange(new ToolStripItem[13]
    {
      (ToolStripItem) this._selectedRecordsCountToolStripStatusLabel,
      (ToolStripItem) this.labelDivider6,
      (ToolStripItem) this._objectTypeNameToolStripStatusLabel,
      (ToolStripItem) this._objectCaptionToolStripStatusLabel,
      (ToolStripItem) this.labelDivider4,
      (ToolStripItem) this._checkedOutByToolStripStatusLabel,
      (ToolStripItem) this.labelDivider5,
      (ToolStripItem) this._readedRecordsCountToolStripStatusLabel,
      (ToolStripItem) this.labelDivider2,
      (ToolStripItem) this._groupsCountToolStripStatusLabel,
      (ToolStripItem) this.labelDivider3,
      (ToolStripItem) this._readNextToolStripDropDownButton,
      (ToolStripItem) this._readAllToolStripDropDownButton
    });
    this._statusStrip.Name = "_statusStrip";
    this._statusStrip.ShowItemToolTips = true;
    this._statusStrip.SizingGrip = false;
    componentResourceManager.ApplyResources((object) this._selectedRecordsCountToolStripStatusLabel, "_selectedRecordsCountToolStripStatusLabel");
    this._selectedRecordsCountToolStripStatusLabel.Name = "_selectedRecordsCountToolStripStatusLabel";
    this._selectedRecordsCountToolStripStatusLabel.Overflow = ToolStripItemOverflow.Never;
    componentResourceManager.ApplyResources((object) this.labelDivider6, "labelDivider6");
    this.labelDivider6.Name = "labelDivider6";
    componentResourceManager.ApplyResources((object) this._objectTypeNameToolStripStatusLabel, "_objectTypeNameToolStripStatusLabel");
    this._objectTypeNameToolStripStatusLabel.Name = "_objectTypeNameToolStripStatusLabel";
    componentResourceManager.ApplyResources((object) this._objectCaptionToolStripStatusLabel, "_objectCaptionToolStripStatusLabel");
    this._objectCaptionToolStripStatusLabel.Name = "_objectCaptionToolStripStatusLabel";
    this._objectCaptionToolStripStatusLabel.Overflow = ToolStripItemOverflow.Never;
    this._objectCaptionToolStripStatusLabel.Spring = true;
    componentResourceManager.ApplyResources((object) this.labelDivider4, "labelDivider4");
    this.labelDivider4.Name = "labelDivider4";
    this._checkedOutByToolStripStatusLabel.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._checkedOutByToolStripStatusLabel, "_checkedOutByToolStripStatusLabel");
    this._checkedOutByToolStripStatusLabel.Name = "_checkedOutByToolStripStatusLabel";
    this._checkedOutByToolStripStatusLabel.Overflow = ToolStripItemOverflow.Never;
    componentResourceManager.ApplyResources((object) this.labelDivider5, "labelDivider5");
    this.labelDivider5.BackColor = SystemColors.Control;
    this.labelDivider5.Name = "labelDivider5";
    this._readedRecordsCountToolStripStatusLabel.BackColor = SystemColors.Control;
    this._readedRecordsCountToolStripStatusLabel.BorderStyle = Border3DStyle.Sunken;
    componentResourceManager.ApplyResources((object) this._readedRecordsCountToolStripStatusLabel, "_readedRecordsCountToolStripStatusLabel");
    this._readedRecordsCountToolStripStatusLabel.Name = "_readedRecordsCountToolStripStatusLabel";
    this._readedRecordsCountToolStripStatusLabel.Overflow = ToolStripItemOverflow.Never;
    componentResourceManager.ApplyResources((object) this.labelDivider2, "labelDivider2");
    this.labelDivider2.BackColor = SystemColors.Control;
    this.labelDivider2.Name = "labelDivider2";
    this._groupsCountToolStripStatusLabel.BackColor = SystemColors.Control;
    this._groupsCountToolStripStatusLabel.BorderStyle = Border3DStyle.Sunken;
    componentResourceManager.ApplyResources((object) this._groupsCountToolStripStatusLabel, "_groupsCountToolStripStatusLabel");
    this._groupsCountToolStripStatusLabel.Name = "_groupsCountToolStripStatusLabel";
    this._groupsCountToolStripStatusLabel.Overflow = ToolStripItemOverflow.Never;
    componentResourceManager.ApplyResources((object) this.labelDivider3, "labelDivider3");
    this.labelDivider3.BackColor = SystemColors.Control;
    this.labelDivider3.Name = "labelDivider3";
    this._readNextToolStripDropDownButton.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._readNextToolStripDropDownButton, "_readNextToolStripDropDownButton");
    this._readNextToolStripDropDownButton.ForeColor = Color.Red;
    this._readNextToolStripDropDownButton.Name = "_readNextToolStripDropDownButton";
    this._readNextToolStripDropDownButton.Overflow = ToolStripItemOverflow.Never;
    this._readNextToolStripDropDownButton.ShowDropDownArrow = false;
    this._readNextToolStripDropDownButton.Click += new EventHandler(this.ReadNextToolStripDropDownButton_Click);
    this._readAllToolStripDropDownButton.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this._readAllToolStripDropDownButton, "_readAllToolStripDropDownButton");
    this._readAllToolStripDropDownButton.ForeColor = Color.Red;
    this._readAllToolStripDropDownButton.Name = "_readAllToolStripDropDownButton";
    this._readAllToolStripDropDownButton.Overflow = ToolStripItemOverflow.Never;
    this._readAllToolStripDropDownButton.ShowDropDownArrow = false;
    this._readAllToolStripDropDownButton.Click += new EventHandler(this.ReadAllToolStripDropDownButton_Click);
    this._grid.AllowDrop = true;
    this._grid.AutoWidthColMode = iGAutoWidthColMode.Cells;
    this._grid.BackColorEvenRows = SystemColors.Window;
    this._grid.BackColorOddRows = SystemColors.Window;
    this._grid.Cursor = Cursors.Default;
    this._grid.DefaultAutoGroupRow.Height = 25;
    this._grid.DefaultCol.Width = (int) componentResourceManager.GetObject("resource.Width");
    this._grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("_grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("_grid.Header.Height");
    this._grid.HighlightBackColorNoFocus = SystemColors.Highlight;
    this._grid.HighlightForeColorNoFocus = SystemColors.HighlightText;
    this._grid.HotTracking = false;
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this._grid.Name = "_grid";
    this._grid.PageCapacity = 500;
    this._grid.PressedMouseMoveMode = iGPressedMouseMoveMode.Normal;
    this._grid.ProcessTab = false;
    this._grid.RowMode = true;
    this._grid.RowModeHasCurCell = true;
    this._grid.RowTextStartColNear = 211;
    this._grid.SelectionMode = iGSelectionMode.MultiExtended;
    this._grid.ShowControlsInAllCells = false;
    this._grid.CellMouseDown += new iGCellMouseDownEventHandler(this.Grid_CellMouseDown);
    this._grid.CellMouseUp += new iGCellMouseUpEventHandler(this.Grid_CellMouseUp);
    this._grid.CellMouseLeave += new iGCellMouseEnterLeaveEventHandler(this.Grid_CellMouseLeave);
    this._grid.CellMouseMove += new iGCellMouseMoveEventHandler(this.Grid_CellMouseMove);
    this._grid.EllipsisBtnClick += new iGEllipsisBtnClickEventHandler(this.Grid_EllipsisBtnClick);
    this._grid.CustomDrawCellForeground += new iGCustomDrawCellEventHandler(this.Grid_CustomDrawCellForeground);
    this._grid.CustomDrawCellBackground += new iGCustomDrawCellEventHandler(this.Grid_CustomDrawCellBackground);
    this._grid.DynamicBackColor += new iGDynamicColorEventHandler(this.Grid_DynamicBackColor);
    this._grid.DynamicForeColor += new iGDynamicColorEventHandler(this.Grid_DynamicForeColor);
    this._grid.DynamicFont += new iGDynamicFontEventHandler(this.Grid_DynamicFont);
    this._grid.ColDividerDoubleClick += new iGColDividerDoubleClickEventHandler(this.Grid_ColDividerDoubleClick);
    this._grid.ColWidthEndChange += new iGColWidthEventHandler(this.Grid_ColWidthEndChange);
    this._grid.ColHdrEndDrag += new iGColHdrEndDragEventHandler(this.Grid_ColHdrEndDrag);
    this._grid.CurCellChanged += new EventHandler(this.Grid_CurCellChanged);
    this._grid.SelectionChanged += new EventHandler(this.Grid_SelectionChanged);
    this._grid.CustomGroupValue += new iGCustomGroupValueEventHandler(this.Grid_CustomGroupValue);
    this._grid.CustomSort += new iGCustomSortEventHandler(this.Grid_CustomSort);
    this._grid.BeforeContentsGrouped += new EventHandler(this.Grid_BeforeContentsGrouped);
    this._grid.AfterContentsGrouped += new EventHandler(this.Grid_AfterContentsGrouped);
    this._grid.BeforeContentsSorted += new EventHandler(this.Grid_BeforeContentsSorted);
    this._grid.AfterContentsSorted += new EventHandler(this.Grid_AfterContentsSorted);
    this._grid.AfterRowStateChanged += new iGAfterRowStateChangedEventHandler(this.Grid_AfterRowStateChanged);
    this._grid.AfterAutoGroupRowCreated += new iGAfterAutoGroupRowCreatedEventHandler(this.Grid_AfterAutoGroupRowCreated);
    this._grid.RequestEdit += new iGRequestEditEventHandler(this.Grid_RequestEdit);
    this._grid.DragDrop += new DragEventHandler(this.Grid_DragDrop);
    this._grid.DragEnter += new DragEventHandler(this.Grid_DragEnter);
    this._grid.DoubleClick += new EventHandler(this.Grid_MouseDoubleClick);
    this._grid.Enter += new EventHandler(this.Grid_Enter);
    this._grid.KeyDown += new KeyEventHandler(this.Grid_KeyDown);
    this._grid.KeyPress += new KeyPressEventHandler(this.Grid_KeyPress);
    this._grid.KeyUp += new KeyEventHandler(this.Grid_KeyUp);
    this._grid.Leave += new EventHandler(this.Grid_Leave);
    this._grid.MouseLeave += new EventHandler(this.Grid_MouseLeave);
    this._grid.MouseMove += new MouseEventHandler(this.Grid_MouseMove);
    this._grid.MouseUp += new MouseEventHandler(this.Grid_MouseUp);
    this._toolBar.FullMenus = true;
    this._toolBar.Guid = new Guid("2337b74f-5d86-4565-809f-c0fa244e17e8");
    this._toolBar.Hidden = false;
    this._toolBar.ImageList = this._mainImageList;
    this._toolBar.Items.AddRange(new ToolbarItemBase[19]
    {
      (ToolbarItemBase) this._toggleManualSortingButtonItem,
      (ToolbarItemBase) this._manualSortingSetupButtonItem,
      (ToolbarItemBase) this._refreshButtonItem,
      (ToolbarItemBase) this._embeddedViewsDropDownMenuItem,
      (ToolbarItemBase) this._toggleGroupingButtonItem,
      (ToolbarItemBase) this._collapseAllGroupsButtonItem,
      (ToolbarItemBase) this._expandAllGroupsButtonItem,
      (ToolbarItemBase) this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem,
      (ToolbarItemBase) this.buttonHeightSet,
      (ToolbarItemBase) this._filtersComboBoxItem,
      (ToolbarItemBase) this._refreshFiltersDropDownMenuItem,
      (ToolbarItemBase) this._searchComboBoxItem,
      (ToolbarItemBase) this._searchButtonItem,
      (ToolbarItemBase) this._cancelSearchButtonItem,
      (ToolbarItemBase) this._clearSearchResultsButtonItem,
      (ToolbarItemBase) this._changeSearchSettingsButtonItem,
      (ToolbarItemBase) this._currentVersionsRuleButtonItem,
      (ToolbarItemBase) this._showContextVersionsButtonItem,
      (ToolbarItemBase) this._editingModeButtonItem
    });
    componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
    this._toolBar.Name = "_toolBar";
    this._toolBar.Overflow = ToolBarOverflow.Wrap;
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
    this._mainImageList.Images.SetKeyName(16 /*0x10*/, "Favorities-icon.png");
    this._toggleManualSortingButtonItem.AutoToggle = AutoToggleType.Single;
    componentResourceManager.ApplyResources((object) this._toggleManualSortingButtonItem, "_toggleManualSortingButtonItem");
    this._toggleManualSortingButtonItem.ImageIndex = 9;
    this._toggleManualSortingButtonItem.Click += new EventHandler(this.ManualSortingButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._manualSortingSetupButtonItem, "_manualSortingSetupButtonItem");
    this._manualSortingSetupButtonItem.ImageIndex = 10;
    this._manualSortingSetupButtonItem.Click += new EventHandler(this.ManualSortingSetupButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._refreshButtonItem, "_refreshButtonItem");
    this._refreshButtonItem.ImageIndex = 3;
    this._refreshButtonItem.Click += new EventHandler(this.RefreshButtonItem_Click);
    this._embeddedViewsDropDownMenuItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._embeddedViewsDropDownMenuItem, "_embeddedViewsDropDownMenuItem");
    this._embeddedViewsDropDownMenuItem.ImageIndex = 11;
    this._embeddedViewsDropDownMenuItem.ShowText = true;
    this._embeddedViewsDropDownMenuItem.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ViewDropDownMenuItem_BeforePopup);
    this._embeddedViewsDropDownMenuItem.Click += new EventHandler(this.ViewDropDownMenuItem_Click);
    this._toggleGroupingButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._toggleGroupingButtonItem, "_toggleGroupingButtonItem");
    this._toggleGroupingButtonItem.ImageIndex = 12;
    this._toggleGroupingButtonItem.Click += new EventHandler(this.GroupingButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsButtonItem, "_collapseAllGroupsButtonItem");
    this._collapseAllGroupsButtonItem.ImageIndex = 13;
    this._collapseAllGroupsButtonItem.Click += new EventHandler(this.CollapseAllGroupsButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._expandAllGroupsButtonItem, "_expandAllGroupsButtonItem");
    this._expandAllGroupsButtonItem.ImageIndex = 14;
    this._expandAllGroupsButtonItem.Click += new EventHandler(this.ExpandAllGroupsButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem, "_collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem");
    this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem.ImageIndex = 15;
    this._collapseAllGroupsExpectGroupsWithFocusedItemsButtonItem.Click += new EventHandler(this.CollapseAllGroupsExceptGroupsWithSelectionsButtonItem_Click);
    componentResourceManager.ApplyResources((object) this.buttonHeightSet, "buttonHeightSet");
    this.buttonHeightSet.Enabled = false;
    this.buttonHeightSet.IconSize = new Size(1, 37);
    this.buttonHeightSet.Image = (Image) Intermech.Client.Core.Properties.Resources.pixel;
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._filtersComboBoxItem, "_filtersComboBoxItem");
    this._filtersComboBoxItem.DropDownStyle = ComboBoxStyle.DropDownList;
    this._filtersComboBoxItem.Importance = ToolBarItemImportance.Highest;
    this._filtersComboBoxItem.Locked = true;
    this._filtersComboBoxItem.MinimumControlWidth = 250;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    this._filtersComboBoxItem.Stretch = true;
    componentResourceManager.ApplyResources((object) this._refreshFiltersDropDownMenuItem, "_refreshFiltersDropDownMenuItem");
    this._refreshFiltersDropDownMenuItem.ImageIndex = 4;
    this._refreshFiltersDropDownMenuItem.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this._createCommonFilterMenuButtonItem,
      (ToolbarItemBase) this._createPersonalFilterMenuButtonItem,
      (ToolbarItemBase) this._filterCardMenuButtonItem,
      (ToolbarItemBase) this._removeFilterMenuButtonItem
    });
    this._refreshFiltersDropDownMenuItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._createCommonFilterMenuButtonItem, "_createCommonFilterMenuButtonItem");
    this._createCommonFilterMenuButtonItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._createPersonalFilterMenuButtonItem, "_createPersonalFilterMenuButtonItem");
    this._createPersonalFilterMenuButtonItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._filterCardMenuButtonItem, "_filterCardMenuButtonItem");
    this._filterCardMenuButtonItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._removeFilterMenuButtonItem, "_removeFilterMenuButtonItem");
    this._removeFilterMenuButtonItem.Image = (Image) Intermech.Client.Core.Properties.Resources.DeleteStandart;
    this._removeFilterMenuButtonItem.ShowText = true;
    this._searchComboBoxItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._searchComboBoxItem, "_searchComboBoxItem");
    this._searchComboBoxItem.Importance = ToolBarItemImportance.High;
    this._searchComboBoxItem.MinimumControlWidth = 50;
    this._searchComboBoxItem.MinimumSize = 250;
    this._searchComboBoxItem.Padding.Bottom = 0;
    this._searchComboBoxItem.Padding.Left = 1;
    this._searchComboBoxItem.Padding.Right = 1;
    this._searchComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._searchButtonItem, "_searchButtonItem");
    this._searchButtonItem.ImageIndex = 5;
    this._searchButtonItem.Importance = ToolBarItemImportance.High;
    componentResourceManager.ApplyResources((object) this._cancelSearchButtonItem, "_cancelSearchButtonItem");
    this._cancelSearchButtonItem.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
    this._cancelSearchButtonItem.ShowText = true;
    this._cancelSearchButtonItem.Visible = false;
    componentResourceManager.ApplyResources((object) this._clearSearchResultsButtonItem, "_clearSearchResultsButtonItem");
    this._clearSearchResultsButtonItem.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
    this._clearSearchResultsButtonItem.ShowText = true;
    this._clearSearchResultsButtonItem.Visible = false;
    componentResourceManager.ApplyResources((object) this._changeSearchSettingsButtonItem, "_changeSearchSettingsButtonItem");
    this._changeSearchSettingsButtonItem.ImageIndex = 6;
    this._changeSearchSettingsButtonItem.Importance = ToolBarItemImportance.High;
    this._currentVersionsRuleButtonItem.AutoToggle = AutoToggleType.Single;
    this._currentVersionsRuleButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._currentVersionsRuleButtonItem, "_currentVersionsRuleButtonItem");
    this._currentVersionsRuleButtonItem.ImageIndex = 7;
    this._currentVersionsRuleButtonItem.Click += new EventHandler(this.CurrentVersionsRuleButtonItem_Click);
    this._showContextVersionsButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._showContextVersionsButtonItem, "_showContextVersionsButtonItem");
    this._showContextVersionsButtonItem.Image = (Image) componentResourceManager.GetObject("_showContextVersionsButtonItem.Image");
    this._showContextVersionsButtonItem.Click += new EventHandler(this.ShowContextVersionsButtonItem_Click);
    this._editingModeButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._editingModeButtonItem, "_editingModeButtonItem");
    this._editingModeButtonItem.ImageIndex = 8;
    this._editingModeButtonItem.Visible = false;
    this._editingModeButtonItem.Click += new EventHandler(this.EditingModeButtonItem_Click);
    this._embeddedViewsCollapsibleSplitter.AnimationDelay = 20;
    this._embeddedViewsCollapsibleSplitter.AnimationStep = 20;
    this._embeddedViewsCollapsibleSplitter.BorderStyle3D = Border3DStyle.Etched;
    this._embeddedViewsCollapsibleSplitter.ControlToHide = (System.Windows.Forms.Control) this._embeddedViewsPanel;
    componentResourceManager.ApplyResources((object) this._embeddedViewsCollapsibleSplitter, "_embeddedViewsCollapsibleSplitter");
    this._embeddedViewsCollapsibleSplitter.ExpandParentForm = false;
    this._embeddedViewsCollapsibleSplitter.Name = "spViewsManager";
    this._embeddedViewsCollapsibleSplitter.TabStop = false;
    this._embeddedViewsCollapsibleSplitter.UseAnimations = false;
    this._embeddedViewsCollapsibleSplitter.VisualStyle = VisualStyles.Mozilla;
    this._embeddedViewsCollapsibleSplitter.SplitterMoved += new SplitterEventHandler(this.EmbeddedViewsCollapsibleSplitter_SplitterMoved);
    componentResourceManager.ApplyResources((object) this._pictureBox, "_pictureBox");
    this._pictureBox.Name = "_pictureBox";
    this._pictureBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "_gridHeaderMenuBar");
    this._gridHeaderMenuBar.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this._gridHeaderMenuBar.Hidden = false;
    this._gridHeaderMenuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._gridHeaderContextMenuBarItem
    });
    this._gridHeaderMenuBar.Name = "_gridHeaderMenuBar";
    this._gridHeaderMenuBar.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this._gridHeaderContextMenuBarItem, "_gridHeaderContextMenuBarItem");
    this._gridHeaderContextMenuBarItem.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._changeGridColumnsMenuButtonItem
    });
    this._gridHeaderContextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this._changeGridColumnsMenuButtonItem, "_changeGridColumnsMenuButtonItem");
    this._changeGridColumnsMenuButtonItem.Image = (Image) componentResourceManager.GetObject("_changeGridColumnsMenuButtonItem.Image");
    this._changeGridColumnsMenuButtonItem.ImageIndex = 2;
    this._changeGridColumnsMenuButtonItem.ShowText = true;
    this._changeGridColumnsMenuButtonItem.Click += new EventHandler(this.ChangeGridColumnsMenuButtonItem_Click);
    this._searchComponent.SearchStateChanged += new EventHandler(this.SearchComponent_SearchStateChanged);
    this.Controls.Add((System.Windows.Forms.Control) this._grid);
    this.Controls.Add((System.Windows.Forms.Control) this._statusStrip);
    this.Controls.Add((System.Windows.Forms.Control) this._embeddedViewsCollapsibleSplitter);
    this.Controls.Add((System.Windows.Forms.Control) this._toolBar);
    this.Controls.Add((System.Windows.Forms.Control) this._embeddedViewsPanel);
    this.Controls.Add((System.Windows.Forms.Control) this._pictureBox);
    this.Controls.Add((System.Windows.Forms.Control) this._gridHeaderMenuBar);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (ChildrenView);
    this.Enter += new EventHandler(this.ChildrenView_Enter);
    this.Leave += new EventHandler(this.ChildrenView_Leave);
    this.Resize += new EventHandler(this.ChildrenView_Resize);
    this._embeddedViewsPanel.ResumeLayout(false);
    this._statusStrip.ResumeLayout(false);
    this._statusStrip.PerformLayout();
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public delegate IServiceContainer GetMenuServiceContainerDelegate(
    object sender,
    IServiceContainer originalMenuServiceContainer);

  public sealed class CustomDrawCellTextEventArgs : EventArgs
  {
    public CustomDrawCellTextEventArgs(
      Graphics graphics,
      iGCell cell,
      Rectangle textBounds,
      Color foreColor,
      Font font)
    {
      if (graphics == null)
        throw new ArgumentNullException(nameof (graphics));
      if (cell == null)
        throw new ArgumentNullException(nameof (cell));
      if (textBounds == Rectangle.Empty)
        throw new ArgumentException();
      if (foreColor == Color.Empty)
        throw new ArgumentException();
      if (font == null)
        throw new ArgumentNullException(nameof (font));
      this.Graphics = graphics;
      this.Cell = cell;
      this.TextBounds = textBounds;
      this.ForeColor = foreColor;
      this.Font = font;
    }

    public Graphics Graphics { get; private set; }

    public iGCell Cell { get; private set; }

    public Rectangle TextBounds { get; private set; }

    public Color ForeColor { get; private set; }

    public Font Font { get; private set; }

    public bool HasDrawn { get; set; }
  }

  private sealed class ComparableMeasuredValue : 
    IComparable<ChildrenView.ComparableMeasuredValue>,
    IComparable
  {
    public static readonly ChildrenView.ComparableMeasuredValue Empty = new ChildrenView.ComparableMeasuredValue(new MeasuredValue(0.0, 0L, string.Empty));

    public ComparableMeasuredValue(MeasuredValue measuredValue)
    {
      this.MeasuredValue = MeasureHelper.ConvertToBaseMeasure(measuredValue);
    }

    public MeasuredValue MeasuredValue { get; set; }

    public int CompareTo(ChildrenView.ComparableMeasuredValue other)
    {
      if (this.MeasuredValue.Value > other.MeasuredValue.Value)
        return 1;
      return this.MeasuredValue.Value != other.MeasuredValue.Value ? -1 : 0;
    }

    public override string ToString()
    {
      if (!ObjectHelper.IsUnknownObjectVersionID(this.MeasuredValue.MeasureID))
        return this.MeasuredValue.ToString();
      return !(this.MeasuredValue.Caption != "0") ? string.Empty : this.MeasuredValue.ToString();
    }

    public override bool Equals(object obj)
    {
      if (this == obj)
        return true;
      return obj is ChildrenView.ComparableMeasuredValue comparableMeasuredValue && this.MeasuredValue.Value == comparableMeasuredValue.MeasuredValue.Value && this.MeasuredValue.MeasureID == comparableMeasuredValue.MeasuredValue.MeasureID;
    }

    public override int GetHashCode()
    {
      return this.MeasuredValue.Value.GetHashCode() ^ this.MeasuredValue.MeasureID.GetHashCode();
    }

    public int CompareTo(object obj) => this.CompareTo((ChildrenView.ComparableMeasuredValue) obj);
  }

  public sealed class RowGroup
  {
    public RowGroup(iGRow row)
    {
      this.Row = row != null ? row : throw new ArgumentNullException(nameof (row));
      this.OriginalRowText = this.Row.RowTextCell != null ? this.Row.RowTextCell.Text : string.Empty;
    }

    public iGRow Row { get; private set; }

    public string OriginalRowText { get; private set; }

    public int RowCount { get; set; }

    public void UpdateText()
    {
      if (this.Row.RowTextCell == null)
        return;
      this.Row.RowTextCell.Value = (object) this;
    }

    public override string ToString() => $"({this.RowCount}) {this.OriginalRowText}";
  }

  /// <summary>Описывает состояние панели с дополнительными видами.</summary>
  [Flags]
  private enum EmbeddedViewsState
  {
    /// <summary>Нет состояния</summary>
    None = 0,
    /// <summary>Открыто</summary>
    Open = 1,
    /// <summary>Не задан размер</summary>
    InvalidSize = 2,
    /// <summary>Не заданы данные</summary>
    InvalidData = 4,
  }

  /// <summary>
  /// Класс для упорядочивания колонок грида по их видимому индексу
  /// </summary>
  private class GridColumnOrderComparer : IComparer, IComparer<iGCol>
  {
    /// <summary>Сравнить между собой две колонки</summary>
    /// <param name="x">Первая колонка</param>
    /// <param name="y">Вторая колонка</param>
    /// <returns>-1 - колонка x меньше колонки y, 0 - колонки равны, 1 - колонка x больше колонки y</returns>
    public int Compare(object x, object y) => this.Compare(x as iGCol, y as iGCol);

    /// <summary>Сравнить между собой две колонки</summary>
    /// <param name="x">Первая колонка</param>
    /// <param name="y">Вторая колонка</param>
    /// <returns>-1 - колонка x меньше колонки y, 0 - колонки равны, 1 - колонка x больше колонки y</returns>
    public int Compare(iGCol x, iGCol y) => x == null || y == null ? 0 : x.Order.CompareTo(y.Order);
  }

  private class ChildrenViewSelectedItemsHostFeature : ISelectedItemsHost
  {
    private ChildrenView _childrenView;
    private ISelectedItemsHost _embeddedSelectedItemsHost;
    private ISelectedItemsHost _currentSelectedItemsHost;

    public ChildrenViewSelectedItemsHostFeature(ChildrenView childrenView)
    {
      this._childrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
      this._childrenView.SelectedItemsChanged += new EventHandler(this.ChildrenView_SelectedItemsChanged);
      this._childrenView.PageViewsManager.ActiveViewPageChanged += new EventHandler(this.ChildrenViewPageViewsManager_ActiveViewPageChanged);
    }

    public ISelectedItems SelectedItems
    {
      get
      {
        return this._currentSelectedItemsHost == null || this._currentSelectedItemsHost.SelectedItems == null || this._currentSelectedItemsHost.SelectedItems.Count <= 0 ? this._childrenView.SelectedItems : this._currentSelectedItemsHost.SelectedItems;
      }
    }

    public event EventHandler SelectedItemsChanged;

    private void ChildrenView_SelectedItemsChanged(object sender, EventArgs e)
    {
      this._currentSelectedItemsHost = (ISelectedItemsHost) this._childrenView;
      this.OnSelectedItemsChanged();
    }

    private void ChildrenViewPageViewsManager_ActiveViewPageChanged(object sender, EventArgs e)
    {
      if (this._embeddedSelectedItemsHost != null)
        this._embeddedSelectedItemsHost.SelectedItemsChanged -= new EventHandler(this.EmbeddedSelectedItemsHost_SelectedItemsChanged);
      this._embeddedSelectedItemsHost = this._childrenView.PageViewsManager.ActiveViewPage == null ? (ISelectedItemsHost) null : this._childrenView.PageViewsManager.ActiveViewPage.View as ISelectedItemsHost;
      if (this._embeddedSelectedItemsHost == null)
        return;
      this._embeddedSelectedItemsHost.SelectedItemsChanged += new EventHandler(this.EmbeddedSelectedItemsHost_SelectedItemsChanged);
    }

    private void EmbeddedSelectedItemsHost_SelectedItemsChanged(object sender, EventArgs e)
    {
      this._currentSelectedItemsHost = this._embeddedSelectedItemsHost;
      this.OnSelectedItemsChanged();
    }

    private void OnSelectedItemsChanged()
    {
      EventHandler selectedItemsChanged = this.SelectedItemsChanged;
      if (selectedItemsChanged == null)
        return;
      selectedItemsChanged((object) this._childrenView, EventArgs.Empty);
    }
  }
}
