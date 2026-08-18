
// Type: Intermech.Navigator.Controls.SelectionWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Interfaces.QuickSearch;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using Intermech.Search.ObjectListFilters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Окно для выбора объектов из базы данных.
/// Эту форму напрямую не использовать!
/// Использовать статические методы класса Intermech.Navigator.SelectionWindow!
/// </summary>
public class SelectionWindow : 
  Form,
  ICommandTarget,
  IIODestination,
  ICurrentSelectedItemsHost,
  ISelectionWindow,
  IAttributeEditorControl,
  IToSelectItemsAnalyzers
{
  /// <summary>Контейнер сервисов</summary>
  internal AdvancedServiceContainer services;
  /// <summary>Менеджер команд</summary>
  private CommandManager commandManager;
  /// <summary>Коллекции команд по умолчанию</summary>
  private IDefaultCommands4ObjTypes _defaultCommands4ObjTypes;
  /// <summary>Диспетчер событий</summary>
  private IIODispatcher _IODispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>
  /// Сервис службы "горячих клавиш" и связанных с ними команд
  /// </summary>
  private IHotKeysManager _hotKeysManager;
  /// <summary>Guid тулбара</summary>
  private static readonly Guid NavigateToolbarGuid = new Guid("f34da14a-091a-4f96-934f-3e5ba2a5dc08");
  /// <summary>Сервис локальной службы уведомлений</summary>
  private UISwitchedNotificationService _notificationService;
  /// <summary>
  /// Текущая коллекция выделенных элементов - от дерева или от менеджера закладок
  /// </summary>
  protected ISelectedItemsHost _selectedItemsHost;
  /// <summary>Дата и время последнего вызова формы</summary>
  public DateTime accessTime = DateTime.UtcNow;
  /// <summary>Опции, управляющие поведением и внешним видом окна</summary>
  public SelectionOptions options = SelectionOptions.Default;
  /// <summary>Разрешенные типы выбираемых объектов</summary>
  public int[] EnableTypes;
  private int attributeId;
  private int? index;
  private Intermech.PropertyEditors.AttrProcessor.AttributeProcessor attributeProcessor;
  private bool inContainer;
  private bool wasChanged;
  private long selectedObjectID = -1;
  private SelectionWindow.FocusTarget _focusTargetOnActivation;
  private BaseQuickSearchProvider _currentQuickSearchProvider;
  /// <summary>
  /// Флаг для указания, что в данный момент мы находимся в режиме поиска
  /// </summary>
  /// <remarks>
  /// Введен по следующей причине:
  /// Привязаться к фокусу контрола нельзя.
  /// Кликаем на контрол ввода текста, набираем текст. Появлются результаты поиска. А потом открываем окно другого прилжения.
  /// Срабатывает таймер и проверка на наличие фокуса показывает что контролы быстрого поиска ужене в фокусе, из-за чего пропадает контрол с отображаемыми результатами и останавливается таймер,
  /// который следит за скрытием контрола результатов.
  /// Потом сворачиваем окно другого приложения, продолжаем набират текст (не тыкая в контрол набора текста). Вновь появляется контрол результов.
  /// НО!!! Если дальше мы кликнем в другое место окна (например в дерево или по закладкам), то контрол результатов не скроется (таймер не работает).
  /// Поэтому введена данная переменная. Флаг устанавливается когда выделяется один из контролов поиска и сбрасывается когда уходим из них.
  /// </remarks>
  private bool _isSearchMode;
  private bool _delayStop;
  private IContainer components;
  public TreeViewsBridge TreeViewsBridge;
  internal BarManager barManager;
  internal ToolBarContainer leftBarDock;
  internal ToolBarContainer rightBarDock;
  internal ToolBarContainer bottomBarDock;
  internal ToolBarContainer topBarDock;
  internal SplitContainer scFrame;
  public PageViewsManager ViewsManager;
  internal Panel panelTop;
  public Label lbDescription;
  public PictureBox pbObject;
  public Button btOK;
  public Button btCancel;
  public StatusStrip statusStrip;
  public ToolStripStatusLabel labelWarning;
  public ToolStripStatusLabel statusAddress;
  public NavigatorTreeView NavTreeView;
  public CheckBox cbDontShowAgain;
  private Timer _searchResultTimer;
  private FlowLayoutPanel flowLayoutPanel1;
  private ListView _lvSearchResult;
  private ColumnHeader _colText;
  private Panel _pnlSearch;
  private TextBox _txtSearch;
  private Label _lbFind;

  /// <summary>Событие "Изменились выделенные элементы"</summary>
  public event EventHandler SelectedItemsChanged;

  /// <summary>
  /// Сгенерировать событие "Изменились выделенные элементы"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void RaiseSelectedItemsChanged(object sender, EventArgs e)
  {
    if (this.SelectedItemsChanged == null)
      return;
    this.btOK.Enabled = true;
    this.SelectedItemsChanged((object) this, e);
  }

  /// <summary>Конструктор</summary>
  /// <param name="options">Опции окна</param>
  public SelectionWindow(SelectionOptions options)
  {
    this.InitializeComponent();
    if (this.TreeViewsBridge.BridgeEnabled && options.HasFlag((Enum) SelectionOptions.HideViews))
      this.TreeViewsBridge.BridgeEnabled = false;
    if (ServicesManager.GetService(typeof (IMainFormUpdate)) as IMainFormUpdate is Form service)
      service.AddOwnedForm((Form) this);
    this.options = options;
    if (!this.DesignMode)
    {
      this._IODispatcher.RegisterDestination((IIODestination) this);
      this._defaultCommands4ObjTypes = ServicesManager.GetService(typeof (IDefaultCommands4ObjTypes)) as IDefaultCommands4ObjTypes;
      this._hotKeysManager = ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager;
      this.commandManager = new CommandManager();
      this.commandManager.ActiveTarget = (ICommandTarget) this.ViewsManager;
      this.services = new AdvancedServiceContainer();
      this._notificationService = this.InitializeNotificationService();
      this.services.AddService(typeof (ISelectionWindow), (object) this);
      this.services.AddService(typeof (IToSelectItemsAnalyzers), (object) this);
      this.services.AddService(typeof (INotificationService), (object) this._notificationService);
      this.services.AddService(typeof (IIODispatcher), (object) this._IODispatcher);
      this.services.AddService(typeof (IDefaultCommands4ObjTypes), (object) this._defaultCommands4ObjTypes);
      this.services.AddService(typeof (ICommandManager), (object) this.commandManager);
      this.services.AddService(typeof (ICurrentSelectedItemsHost), (object) this);
      this.services.AddService(typeof (NavigatorTreeView), (object) this.NavTreeView);
      this.services.AddService(typeof (IViewsManager), (object) this.ViewsManager);
      this.services.AddService(typeof (SelectionOptionsHolder), (object) new SelectionOptionsHolder(this.options));
      this.services.AddService(typeof (INotificationServiceStatesHolder), (object) new NotificationServiceStatesHolder(NotificationServiceStates.InactiveDialog));
      this.services.AddService(typeof (Form), (object) this);
      AdvancedServiceContainer serviceContainer1 = new AdvancedServiceContainer((System.IServiceProvider) this.services);
      AdvancedServiceContainer serviceContainer2 = new AdvancedServiceContainer((System.IServiceProvider) this.services);
      serviceContainer1.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.NodeInTree | ViewStateFlags.InSelectionWindow));
      serviceContainer2.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.NodeInViews | ViewStateFlags.InSelectionWindow));
      this.NavTreeView.Services = (System.IServiceProvider) serviceContainer1;
      this.ViewsManager.Services = (System.IServiceProvider) serviceContainer2;
      this.NavTreeView.BeforeFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.OnBeforeFocusNodeEventHandler);
      this.NavTreeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.OnAfterFocusNode);
      this.NavTreeView.Focus();
      if ((this.options & SelectionOptions.DisableTreeSorting) == SelectionOptions.DisableTreeSorting)
        this.NavTreeView.DisableColumnsSorting = true;
      this.UpdateControls();
    }
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 60, primaryWorkingArea.Height / 100 * 70);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.NavTreeView.SelectionChanged += new EventHandler(this.NavTreeView_SelectionChanged);
  }

  public Func<bool> OKButtonEnabledFunc { get; set; }

  private void NavTreeView_SelectionChanged(object sender, EventArgs e)
  {
    this.NavTreeView.Focus();
    this.statusAddress.Text = this.GetSelectedItemAddress();
  }

  public SelectionWindow.SelectionWindowMemento GetMemento()
  {
    SelectionWindow.SelectionWindowMemento memento = new SelectionWindow.SelectionWindowMemento();
    NodeIDPath focusedPath = this.NavTreeView.FocusedPath;
    if (focusedPath != null)
      memento.NavigatorTreeViewFocusedPath = new SelectionWindow.NodeIDPathWrapper(focusedPath);
    return memento;
  }

  public void SetMemento(SelectionWindow.SelectionWindowMemento memento)
  {
    if (memento == null)
      throw new ArgumentNullException(nameof (memento));
    if (memento.NavigatorTreeViewFocusedPath == null)
      return;
    this.NavTreeView.TryBrowse(memento.NavigatorTreeViewFocusedPath.Path);
  }

  public static NodeIDPath DeserializeNodeIDPath(
    string pathAsString,
    System.IServiceProvider serviceProvider = null)
  {
    if (string.IsNullOrEmpty(pathAsString))
      throw new ArgumentException();
    List<PersistentState> persistentStateList = new List<PersistentState>();
    BinaryStateFormatter binaryStateFormatter = new BinaryStateFormatter();
    string str = pathAsString;
    char[] chArray = new char[1]{ '|' };
    foreach (string s in str.Split(chArray))
    {
      using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(s)))
        persistentStateList.Add(binaryStateFormatter.Deserialize((Stream) memoryStream));
    }
    return Intermech.Navigator.Utils.DeserializePath(persistentStateList.ToArray(), serviceProvider ?? (System.IServiceProvider) ServicesManager.ServiceContainer);
  }

  /// <summary>Показать окно в модальном режиме</summary>
  /// <returns>Результаты вызова окна</returns>
  public new virtual DialogResult ShowDialog()
  {
    try
    {
      if (this.services.GetService(typeof (INotificationServiceStatesHolder)) is INotificationServiceStatesHolder service)
      {
        service.States &= ~NotificationServiceStates.InactiveForm;
        service.States &= ~NotificationServiceStates.InactiveDialog;
      }
      this._notificationService.Forced = true;
      this.scFrame.Panel1Collapsed = (this.options & SelectionOptions.HideTree) != 0;
      this.NavTreeView.Visible = !this.scFrame.Panel1Collapsed;
      this.scFrame.Panel2Collapsed = (this.options & SelectionOptions.HideViews) != 0;
      this.ViewsManager.Visible = !this.scFrame.Panel2Collapsed;
      return base.ShowDialog();
    }
    finally
    {
      this._notificationService.Forced = false;
      if (this.services.GetService(typeof (INotificationServiceStatesHolder)) is INotificationServiceStatesHolder service)
      {
        if (!UISettings.AutoupdateNonActiveWindows)
        {
          service.States = service.States | NotificationServiceStates.InactiveForm | NotificationServiceStates.InactiveDialog;
        }
        else
        {
          service.States &= ~NotificationServiceStates.InactiveForm;
          service.States &= ~NotificationServiceStates.InactiveDialog;
        }
      }
    }
  }

  protected override void UpdateDefaultButton()
  {
  }

  /// <summary>Вернуть полный адрес первого выделенного узла</summary>
  /// <returns>Полный адрес первого выделенного узла</returns>
  protected virtual string GetSelectedItemAddress()
  {
    if (this.NavTreeView.SelectedItems is NavigatorTreeViewSelectedItems selectedItems)
    {
      NavigatorTreeNode[] nodes = selectedItems.Nodes;
      if (nodes != null && nodes.Length != 0)
        return this.NavTreeView.GetNodeAddress(nodes[0]);
    }
    return string.Empty;
  }

  /// <summary>Изменилась текущая коллекция выделенных элементов</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  protected void SelectedItemsChangedHandler(object sender, EventArgs e)
  {
    this.UpdateControls();
    this.RaiseSelectedItemsChanged(sender, e);
  }

  /// <summary>Обновить статус контролов</summary>
  protected virtual void UpdateControls()
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    bool flag6 = false;
    int num = 0;
    if (this._selectedItemsHost == null || this._selectedItemsHost.SelectedItems.Count == 0)
    {
      this.btOK.Enabled = false;
    }
    else
    {
      int count = this._selectedItemsHost.SelectedItems.Count;
      this.statusAddress.Text = this.GetSelectedItemAddress();
      this.statusAddress.ToolTipText = this.statusAddress.Text;
      bool flag7 = false;
      for (int index = 0; index < count; ++index)
      {
        INodeID itemId = this._selectedItemsHost.SelectedItems.GetItemID(index);
        if (itemId != null)
        {
          flag1 |= itemId.CategoryID == 2 | itemId.CategoryID == 1;
          flag2 |= itemId.CategoryID == 4;
          flag3 |= itemId.CategoryID == 5;
          flag4 |= itemId.CategoryID == 6;
          if (itemId.CategoryID == 4)
          {
            IMSObjectType objectType = MetaDataHelper.GetObjectType(itemId.TypeID);
            if (objectType != null && objectType.VersionsMode == ObjectVersionModes.Abstract)
            {
              ++num;
              flag6 = true;
            }
          }
          else if (itemId.CategoryID == 1 && (this.options & SelectionOptions.SelectObjects) != (SelectionOptions) 0 && this.EnableTypes != null)
          {
            if (itemId.TypeID <= 0)
            {
              flag7 = true;
            }
            else
            {
              IMSObjectType objectType = MetaDataHelper.GetObjectType(itemId.TypeID);
              if (!flag7 && Array.IndexOf<int>(this.EnableTypes, objectType.ObjectTypeID) < 0)
                flag7 = true;
            }
          }
          flag5 = true;
          if (flag1 & flag2 & flag3 & flag4 & flag5)
            break;
        }
      }
      if ((this.options & SelectionOptions.DisableSelectAbstractTypes) != 0 & flag6)
      {
        this.btOK.Enabled = false;
        this.labelWarning.Text = LocalizationHolder.rm.GetString("Client.Core_602");
        this.labelWarning.Visible = true;
        this.statusAddress.Text = string.Empty;
        this.statusAddress.ToolTipText = string.Empty;
      }
      else
      {
        this.btOK.Enabled = (this.options & SelectionOptions.SelectObjects) != 0 & flag1 | (this.options & SelectionOptions.SelectObjectTypes) != 0 & flag2 | (this.options & SelectionOptions.SelectRelations) != 0 & flag3 | (this.options & SelectionOptions.SelectRelationTypes) != 0 & flag4 | (this.options & SelectionOptions.SelectOtherNodes) != 0 & flag5 && !flag7 && (this.OKButtonEnabledFunc == null || this.OKButtonEnabledFunc != null && this.OKButtonEnabledFunc());
        this.labelWarning.Text = "";
        this.labelWarning.Visible = false;
      }
    }
  }

  public new void Update()
  {
    Guid guid = ObjectListFilter.AllObjectsFilter.Guid;
    int val1 = 0;
    XmlDocument xmlDoc = (XmlDocument) null;
    iFocusAndSelection state = (iFocusAndSelection) null;
    string str = (string) null;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (this.ViewsManager.ActiveViewPage != null && this.ViewsManager.ActiveViewPage.View is ChildrenView)
    {
      ChildrenView view = (ChildrenView) this.ViewsManager.ActiveViewPage.View;
      guid = view.SelectedFilterGuid;
      val1 = (int) view.ReadedRecordCount;
      xmlDoc = view.GetState();
      state = view.GridGetFocusAndSelection();
      str = view.SearchText;
      flag2 = view.FilterByCurrentVersionsRule;
      flag1 = view.IsSearchActive;
      flag3 = view.ShowContextVersions;
    }
    bool useDelay = this.TreeViewsBridge.UseDelay;
    this.TreeViewsBridge.UseDelay = false;
    try
    {
      NodeIDPath focusedPath = this.NavTreeView.FocusedPath;
      this.NavTreeView.Build(this.NavTreeView.RootDescriptor);
      this.NavTreeView.TryBrowse(focusedPath);
      if (this.ViewsManager.ActiveViewPage != null)
      {
        if (this.ViewsManager.ActiveViewPage.View is ChildrenView)
        {
          ChildrenView view = (ChildrenView) this.ViewsManager.ActiveViewPage.View;
          view.SuppressReloadItems();
          try
          {
            view.SelectedFilterGuid = guid;
            view.SearchText = str;
            view.FilterByCurrentVersionsRule = flag2;
            view.ShowContextVersions = flag3;
          }
          finally
          {
            view.ResumeReloadItems();
          }
          if (!flag1)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              val1 = Math.Max(val1, sessionKeeper.Session.MaxRows);
            view.ReloadItems(new int?(Math.Min(val1, 10000)));
          }
          else
            view.IsSearchActive = flag1;
          view.RestoreState(xmlDoc);
          view.GridSetFocusAndSelection(state, true);
        }
      }
    }
    finally
    {
      this.TreeViewsBridge.UseDelay = useDelay;
    }
    this.UpdateControls();
  }

  /// <summary>Клонировать</summary>
  private void CloneToolbars()
  {
    BarManager service = (BarManager) ServicesManager.GetService(typeof (BarManager));
    List<Intermech.Bars.ToolBar> toolBarList = new List<Intermech.Bars.ToolBar>((IEnumerable<Intermech.Bars.ToolBar>) service.GetToolBars());
    toolBarList.Sort(new Comparison<Intermech.Bars.ToolBar>(this.CompareToolbars));
    ToolBarContainer suitableContainer = this.barManager.FindSuitableContainer(DockStyle.Top);
    suitableContainer.SuspendLayout();
    try
    {
      for (int index = 0; index < toolBarList.Count; ++index)
      {
        if (service.MenuBar != toolBarList[index])
          this.CloneToolbar(toolBarList[index], suitableContainer);
      }
    }
    finally
    {
      suitableContainer.ResumeLayout();
    }
  }

  /// <summary>Сравнить две панели</summary>
  /// <param name="a">Панель а</param>
  /// <param name="b">Панель бэ</param>
  /// <returns>true, если панели равны</returns>
  private int CompareToolbars(Intermech.Bars.ToolBar a, Intermech.Bars.ToolBar b)
  {
    int num1 = a.Hidden.CompareTo(b.Hidden);
    if (num1 != 0)
      return num1;
    int num2 = a.DockLine.CompareTo(b.DockLine);
    return num2 != 0 ? num2 : b.DockOffset.CompareTo(a.DockOffset);
  }

  /// <summary>Клонировать тулбар</summary>
  /// <param name="toolbar">Панель-донор</param>
  /// <param name="container">Контейнер панелей</param>
  private void CloneToolbar(Intermech.Bars.ToolBar toolbar, ToolBarContainer container)
  {
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Name = toolbar.Name;
    toolBar.Guid = toolbar.Guid;
    toolBar.Visible = toolbar.Visible;
    toolBar.Text = toolbar.Text;
    toolBar.AllowVerticalDock = false;
    toolBar.AllowHorizontalDock = true;
    toolBar.DockLine = toolbar.DockLine;
    toolBar.DockOffset = toolbar.DockOffset;
    toolBar.FullMenus = toolbar.FullMenus;
    toolBar.Hidden = toolbar.Hidden;
    toolBar.ImageList = toolbar.ImageList;
    toolBar.Location = toolbar.Location;
    toolBar.MinimumFloatingSize = toolbar.MinimumFloatingSize;
    toolBar.Size = toolbar.Size;
    toolBar.Stretch = toolbar.Stretch;
    toolBar.TabIndex = toolbar.TabIndex;
    for (int index = 0; index < toolbar.Items.Count; ++index)
    {
      ToolbarItemBase toolbarItemBase = toolbar.Items[index].CloneItem();
      toolBar.Items.Add(toolbarItemBase);
      if (toolbarItemBase is ButtonItemBase)
        this.commandManager.Add(new ButtonItemBase[1]
        {
          (ButtonItemBase) toolbarItemBase
        });
    }
    container.Controls.Add((Control) toolBar);
  }

  /// <summary>Выполнить команду от сервиса ICommandManager</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если команда выполнена</returns>
  public bool Execute(ICommandState commandState) => this.ViewsManager.Execute(commandState);

  /// <summary>Запросить статус указанной команды</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если запрос был обработан</returns>
  public bool QueryStatus(ICommandState commandState)
  {
    return this.ViewsManager.QueryStatus(commandState);
  }

  /// <summary>
  /// Вызвать выполнение первой разрешённой команды контекстного меню для указанного события
  /// </summary>
  /// <param name="commands">Команды контекстного меню</param>
  /// <param name="ioEvent">Событие</param>
  /// <returns>true, если команда обработана</returns>
  internal bool ExecuteMenuCommand(List<IHotKeysCommand> commands, IIOEvent ioEvent)
  {
    if (commands == null || commands.Count == 0 || ioEvent == null || ioEvent.Source.SelectedItems == null)
      return false;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(ioEvent.Source.SelectedItems, ioEvent.Source.Services, false);
    string commandName = string.Empty;
    for (int index = 0; index < commands.Count; ++index)
    {
      if (commandsTable.Contains(commands[index].Command))
      {
        commandName = commands[index].Command;
        break;
      }
    }
    if (commandName == string.Empty)
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, ioEvent.Source.Services);
    return true;
  }

  /// <summary>
  /// Вызвать выполнение указанной команды контекстного меню для указанного события
  /// </summary>
  /// <param name="command">Команда контекстного меню</param>
  /// <param name="ioEvent">Событие</param>
  /// <returns>true, если команда обработана</returns>
  internal bool ExecuteMenuCommand(string command, IIOEvent ioEvent)
  {
    if (command == string.Empty || ioEvent == null || ioEvent.Source.SelectedItems == null)
      return false;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(ioEvent.Source.SelectedItems, ioEvent.Source.Services, false);
    if (!commandsTable.Contains(command))
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(command, commandsTable, ioEvent.Source.Services);
    return true;
  }

  /// <summary>
  /// Вызвать выполнение указанной команды контекстного меню для указанного события
  /// </summary>
  /// <param name="command">Команда контекстного меню</param>
  /// <param name="ioEvent">Событие</param>
  /// <returns>true, если команда обработана</returns>
  internal bool ExecuteMenuCommand(IDefaultCommand command, IIOEvent ioEvent)
  {
    return command != null && ioEvent != null && ioEvent.Source.SelectedItems != null && this.ExecuteMenuCommand(command.DefaultCommandName, ioEvent);
  }

  /// <summary>
  /// Переместиться в дереве (в зависимости от исходных данных в событии)
  /// </summary>
  /// <param name="Event">Событие</param>
  private bool BrowseToPath(IIOEvent Event)
  {
    if (!(Event.Tag is NodeIDPath tag))
      return false;
    this.NavTreeView.Focus();
    return this.NavTreeView.TryBrowse(tag);
  }

  /// <summary>
  /// Переместиться в дереве на предыдущий уровень (в зависимости от исходных данных в событии)
  /// </summary>
  /// <param name="Event">Событие</param>
  private bool BrowseToPrevPath(IIOEvent Event)
  {
    if (!(Event.Tag is NodeIDPath tag) || tag.Length <= 1)
      return false;
    NodeIDPath nodeIDPath = new NodeIDPath(tag);
    nodeIDPath.RemoveLast();
    this.NavTreeView.TryBrowse(nodeIDPath);
    this.ViewsManager.Focus();
    if (this.ViewsManager.ActiveViewPage != null)
      this.ViewsManager.ActiveViewPage.Control.Focus();
    return true;
  }

  /// <summary>Список поддерживаемых обработчиком событий</summary>
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyDown | IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  /// <summary>Выполнить обработку события</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если обработка выполнена успешно, false, если событие не обработано</returns>
  bool IIODestination.ProcessEvent(IIOEvent Event)
  {
    if (Event == null)
      return false;
    if ((Event.EventType == IOEventType.evKeyUp || Event.EventType == IOEventType.evKeyDown) && ((KeyEventArgs) Event.EventData).KeyCode != Keys.Return && this._hotKeysManager != null)
    {
      List<IHotKeysCommand> commands = this._hotKeysManager[((KeyEventArgs) Event.EventData).KeyCode | ((KeyEventArgs) Event.EventData).Modifiers];
      if (commands != null && commands.Count > 0)
      {
        ((KeyEventArgs) Event.EventData).Handled = true;
        return this.ExecuteMenuCommand(commands, Event);
      }
    }
    if (this.ViewsManager.ActiveViewPage != null && Event.Source.Control == this.ViewsManager.ActiveViewPage.Control)
    {
      if (Event.EventType == IOEventType.evMouseDoubleClick || (Event.EventType == IOEventType.evKeyUp || Event.EventType == IOEventType.evKeyDown) && ((KeyEventArgs) Event.EventData).KeyCode == Keys.Return)
      {
        if (Event.Source.SelectedItems != null && Event.Source.SelectedItems.Count > 0)
        {
          this.UpdateControls();
          this.RaiseSelectedItemsChanged((object) this, new EventArgs());
          INodeID itemId = Event.Source.SelectedItems.GetItemID(0);
          if (Event.Source.SelectedItems.Count == 1 && (Event.Source is IFoldersView || itemId != null || this.NavTreeView.RootDescriptor.GetRecordNodeID() == null || itemId.CategoryID != this.NavTreeView.RootDescriptor.GetRecordNodeID().CategoryID) && !this.btOK.Enabled && this.BrowseToPath(Event))
            return true;
          Event.EventFlags |= IOEventFlags.efProcessed;
          if (this.btOK.Enabled && this.btOK.Tag is DynamicSelectionEventHandler)
          {
            this.btOK.PerformClick();
            return true;
          }
          if (this.btOK.Enabled && this.btOK.DialogResult == DialogResult.OK)
          {
            this.DialogResult = DialogResult.OK;
            return true;
          }
        }
        return false;
      }
      if (Event.EventType == IOEventType.evKeyUp && ((KeyEventArgs) Event.EventData).KeyCode == Keys.Back || ((KeyEventArgs) Event.EventData).KeyCode == Keys.BrowserBack)
      {
        this.BrowseToPrevPath(Event);
        return false;
      }
    }
    return false;
  }

  /// <summary>
  /// Текущая коллекция элементов навигации у родительского элемента управления
  /// </summary>
  public ISelectedItemsHost ItemsHost
  {
    get => this._selectedItemsHost;
    set
    {
      if (this._selectedItemsHost == value)
        return;
      if (this._selectedItemsHost != null)
        this._selectedItemsHost.SelectedItemsChanged -= new EventHandler(this.SelectedItemsChangedHandler);
      this._selectedItemsHost = value;
      if (this._selectedItemsHost != null)
        this._selectedItemsHost.SelectedItemsChanged += new EventHandler(this.SelectedItemsChangedHandler);
      this.UpdateControls();
      this.RaiseSelectedItemsChanged((object) this, new EventArgs());
    }
  }

  /// <summary>
  /// Выполнить изучение элемента из указанной коллекции, вернуть результат - надо ли его выделять в контроле или нет
  /// </summary>
  /// <param name="sender">Контрол, в котором осуществляется выбор элементов</param>
  /// <param name="services">Контейнер сервисов контрола окна, предоставляющего анализируемый элемент пространства навигации</param>
  /// <param name="handler">Обработчик указанного элемента, позволяющий получать дополнительные данные для этого элемента</param>
  /// <param name="item">Анализируемый элемент из коллекции пространства навигации</param>
  /// <param name="index">Индекс данного элемента в коллекции</param>
  /// <returns>Результат проверки</returns>
  public ToSelectItemsAnalyzerResult Analyze(
    Control sender,
    System.IServiceProvider services,
    INode handler,
    INodeID item,
    int index)
  {
    List<IToSelectItemsAnalyzer> temporaryToSelAnalyzers = Intermech.Navigator.SelectionWindow._temporaryToSelAnalyzers;
    if (temporaryToSelAnalyzers == null || temporaryToSelAnalyzers.Count == 0)
      return ToSelectItemsAnalyzerResult.Skip;
    for (int index1 = 0; index1 < temporaryToSelAnalyzers.Count; ++index1)
    {
      ToSelectItemsAnalyzerResult itemsAnalyzerResult = temporaryToSelAnalyzers[index1].Analyze(sender, services, handler, item, index);
      if (itemsAnalyzerResult != ToSelectItemsAnalyzerResult.Skip)
        return itemsAnalyzerResult;
    }
    return ToSelectItemsAnalyzerResult.Skip;
  }

  /// <summary>Узел в дереве фокусируется</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void OnBeforeFocusNodeEventHandler(object sender, NavigatorTreeNodeEventArgs e)
  {
    if ((this.options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
      this.ItemsHost = (ISelectedItemsHost) null;
    else
      this.ItemsHost = (ISelectedItemsHost) this.NavTreeView;
  }

  /// <summary>Узел в дереве сфокусировался</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void OnAfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    this.GetQuickSearchProvider(e.Node);
    if ((this.options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
      this.ItemsHost = (ISelectedItemsHost) null;
    else
      this.ItemsHost = (ISelectedItemsHost) this.NavTreeView;
  }

  /// <summary>Попытка закрыть форму</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметы</param>
  private void SelectionWindow_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.ViewsManager.CanClose((object) this))
      e.Cancel = true;
    else
      this.ViewsManager.SaveChanges();
  }

  /// <summary>Изменилась текущая активная страничка в закладках</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoActiveViewPageChanged(object sender, EventArgs e)
  {
    IView view = this.ViewsManager.ActiveViewPage != null ? this.ViewsManager.ActiveViewPage.View : (IView) null;
    ISelectedItemsHost selectedItemsHost1 = view as ISelectedItemsHost;
    if (view is ChildrenView)
      selectedItemsHost1 = ((ChildrenView) view).SelectedItemsHost;
    if ((this.options & SelectionOptions.DisableSelectFromViews) != (SelectionOptions) 0 && selectedItemsHost1 != this.NavTreeView)
      selectedItemsHost1 = (ISelectedItemsHost) null;
    ISelectedItemsHost selectedItemsHost2 = selectedItemsHost1 ?? (ISelectedItemsHost) this.NavTreeView;
    if ((this.options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
    {
      this.ItemsHost = selectedItemsHost2 == this.NavTreeView ? (ISelectedItemsHost) null : selectedItemsHost2;
    }
    else
    {
      if (selectedItemsHost2 != this.NavTreeView && this.NavTreeView.Focused)
        selectedItemsHost2 = this.btOK.Enabled ? (ISelectedItemsHost) this.NavTreeView : selectedItemsHost2;
      this.ItemsHost = selectedItemsHost2;
    }
  }

  /// <summary>Фокус пришёл на дерево</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void TreeView_Enter(object sender, EventArgs e)
  {
    this._focusTargetOnActivation = SelectionWindow.FocusTarget.Tree;
    if ((this.options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
      this.ItemsHost = (ISelectedItemsHost) null;
    else
      this.ItemsHost = (ISelectedItemsHost) this.NavTreeView;
  }

  private void NavTreeView_Leave(object sender, EventArgs e)
  {
  }

  /// <summary>Фокус пришёл на закладки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ViewsManager_Enter(object sender, EventArgs e)
  {
    this._focusTargetOnActivation = SelectionWindow.FocusTarget.View;
    this.DoActiveViewPageChanged((object) this, (EventArgs) null);
  }

  /// <summary>Контейнер сервисов окна</summary>
  System.IServiceProvider ISelectionWindow.Services
  {
    [DebuggerStepThrough] get => (System.IServiceProvider) this.services;
  }

  NavigatorTreeView ISelectionWindow.Tree
  {
    [DebuggerStepThrough] get => this.NavTreeView;
  }

  /// <summary>Кнопка "ОК"</summary>
  Button ISelectionWindow.OkButton
  {
    [DebuggerStepThrough] get => this.btOK;
  }

  public long SelectedObjectID
  {
    get => this.selectedObjectID;
    set => this.selectedObjectID = value;
  }

  public int AttributeId => this.attributeId;

  public object AttributeProcessor => (object) this.attributeProcessor;

  public int? Index => this.index;

  public void InitControl(int attributeId, object attributeProcessor, int? index)
  {
    this.attributeId = attributeId;
    this.attributeProcessor = (Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) attributeProcessor;
    this.index = index;
  }

  public bool InContainer
  {
    get => this.inContainer;
    set => this.inContainer = value;
  }

  public void RefreshControl()
  {
  }

  public bool Apply()
  {
    if (this.wasChanged)
    {
      bool flag = false;
      AttributeValues attributeValues = this.attributeProcessor.FindAttributeValues(this.attributeId);
      if (attributeValues == null)
      {
        attributeValues = Intermech.PropertyEditors.AttrProcessor.AttributeProcessor.CreateAttributeValues(this.attributeId, this.attributeProcessor.Id, this.attributeProcessor.ElementKind);
        flag = true;
      }
      if (attributeValues == null)
        throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_886"));
      if (flag)
      {
        if (attributeValues.ReadOnly)
          return false;
        if (this.index.HasValue && this.index.Value < attributeValues.Values.Length)
          attributeValues.Values[this.index.Value] = (object) new ObjectPropertyClass(this.selectedObjectID);
        AttributeValuesList list = new AttributeValuesList();
        list.Add(attributeValues);
        this.attributeProcessor.SetAttributeValuesArray(list);
      }
      else if (!AttributeValues.ValueEquals(!this.index.HasValue ? attributeValues.Values[0] : attributeValues.Values[this.index.Value], (object) new ObjectPropertyClass(this.selectedObjectID)))
      {
        if (this.index.HasValue)
          this.attributeProcessor.SetValue(this.attributeId, this.index.Value, (object) new ObjectPropertyClass(this.selectedObjectID));
        else
          this.attributeProcessor.SetValue(this.attributeId, (object) new ObjectPropertyClass(this.selectedObjectID));
      }
      this.wasChanged = false;
    }
    return true;
  }

  public event AttributeValuesChangedHandler OnAttributeValueChanged;

  public event CloseDemandHandler OnCloseDemand;

  public bool WasChanged
  {
    get => this.wasChanged;
    set => this.wasChanged = value;
  }

  public void Cancel() => this.wasChanged = false;

  public bool IsDropDownResizable
  {
    get => throw new Exception("The method or operation is not implemented.");
  }

  public UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public bool GetPaintValueSupported(ITypeDescriptorContext context) => false;

  public void PaintValue(PaintValueEventArgs e)
  {
  }

  private void SelectionWindow_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    e.Cancel = true;
    IViewPage activeViewPage = this.ViewsManager.ActiveViewPage;
    if (activeViewPage == null)
      return;
    HelpProvidersClass.ShowHelpTopic(activeViewPage.HelpID, activeViewPage.HelpPath);
  }

  /// <summary>Служба уведомлений для текущего окна</summary>
  internal INotificationService NotificationService
  {
    [DebuggerStepThrough] get => (INotificationService) this._notificationService;
  }

  /// <summary>Отправить сообщение контролам внутри формы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  internal void InternalFireNotification(object sender, NotificationEventArgs e)
  {
    Intermech.Client.Core.NotificationService parent = this._notificationService.Parent;
    try
    {
      this._notificationService.Parent = (Intermech.Client.Core.NotificationService) null;
      this._notificationService.FireEvent(sender, e);
    }
    finally
    {
      this._notificationService.Parent = parent;
    }
  }

  /// <summary>Инициализировать локальный сервис уведомлений</summary>
  /// <returns>Локальный сервис уведомлений</returns>
  private UISwitchedNotificationService InitializeNotificationService()
  {
    UISwitchedNotificationService notificationService = new UISwitchedNotificationService();
    notificationService.Parent = (Intermech.Client.Core.NotificationService) ServicesManager.GetService(typeof (INotificationService));
    return notificationService;
  }

  /// <summary>Удалить локальный сервис уведомлений</summary>
  /// <param name="notificationService">Удаляемый сервис</param>
  protected virtual void DisposeNotificationService(INotificationService notificationService)
  {
    ((IDisposable) notificationService).Dispose();
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void SelectionWindow_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void SelectionWindow_FormClosed(object sender, FormClosedEventArgs e)
  {
    this._txtSearch.Text = string.Empty;
    this._lvSearchResult.Items.Clear();
    this._isSearchMode = false;
    this.StopSearch();
    FormStorage.SaveLayout((Control) this);
  }

  private void SelectionWindow_Activated(object sender, EventArgs e)
  {
    if (this._focusTargetOnActivation == SelectionWindow.FocusTarget.Tree)
      this.NavTreeView.Focus();
    else if (this._focusTargetOnActivation == SelectionWindow.FocusTarget.View && this.ViewsManager.ActiveViewPage != null && this.ViewsManager.ActiveViewPage.View is Control view)
    {
      view.Focus();
      if (view is ChildrenView)
        ((ChildrenView) view).Grid.Focus();
      this.DoActiveViewPageChanged((object) this, EventArgs.Empty);
    }
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtSearch_Enter(object sender, EventArgs e)
  {
    this._isSearchMode = true;
    if (this._currentQuickSearchProvider == null || this._lvSearchResult.Visible)
      return;
    this.Search(this._txtSearch.Text);
    this._searchResultTimer.Start();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtSearch_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Down || this._lvSearchResult.Items.Count <= 0)
      return;
    this._lvSearchResult.FocusedItem = this._lvSearchResult.Items[0];
    this._lvSearchResult.FocusedItem.Selected = true;
    this._delayStop = true;
    this._lvSearchResult.Focus();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtSearch_TextChanged(object sender, EventArgs e)
  {
    if (this._currentQuickSearchProvider == null)
      return;
    this.Search(this._txtSearch.Text);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lvSearchResult_Enter(object sender, EventArgs e) => this._isSearchMode = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lvSearchResult_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Up || !this._lvSearchResult.Items[0].Selected)
      return;
    this._txtSearch.Focus();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lvSearchResult_DoubleClick(object sender, EventArgs e) => this.GoToSelectedNode();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_SearchControls_Leave(object sender, EventArgs e)
  {
    this._isSearchMode = false;
    this.StopSearch();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="msg"></param>
  /// <param name="keyData"></param>
  /// <returns></returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    bool flag = true;
    if (keyData == Keys.Return && this._lvSearchResult.Focused)
      this.GoToSelectedNode();
    else if (keyData != Keys.Return || !this._txtSearch.Focused)
      flag = base.ProcessCmdKey(ref msg, keyData);
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_searchResultTimer_Tick(object sender, EventArgs e)
  {
    if (this._isSearchMode)
      return;
    this.SetSearchResultVisible(false);
    this._searchResultTimer.Stop();
    this.StopSearch();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedNode"></param>
  private void GetQuickSearchProvider(NavigatorTreeNode selectedNode)
  {
    this._currentQuickSearchProvider = (BaseQuickSearchProvider) null;
    if (selectedNode != null)
    {
      NavigatorTreeNode navigatorTreeNode = selectedNode;
      BaseQuickSearchProvider quickSearchProvider;
      do
      {
        IQuickSearch handler = navigatorTreeNode.Handler as IQuickSearch;
        quickSearchProvider = (BaseQuickSearchProvider) null;
        if (handler != null)
          quickSearchProvider = handler.QuickSearchProvider;
        if (quickSearchProvider == null)
          navigatorTreeNode = navigatorTreeNode.Parent;
        else
          goto label_6;
      }
      while (navigatorTreeNode != null && navigatorTreeNode.Level > 0);
      goto label_9;
label_6:
      quickSearchProvider.ParentNode = (object) navigatorTreeNode;
      if (quickSearchProvider.NeedTimerForServerRequest)
        quickSearchProvider.ServerRequestCallback(new Action<List<QuickSearchResultItem>>(this.QuickSearchCallback));
      this._currentQuickSearchProvider = quickSearchProvider;
      this._lvSearchResult.SmallImageList = quickSearchProvider.ImgList;
    }
label_9:
    this._pnlSearch.Visible = this._currentQuickSearchProvider != null;
  }

  /// <summary>
  /// Выставить настройки контрола отображения результатов поиска.
  /// </summary>
  /// <param name="value">Видимость контрола</param>
  private void SetSearchResultVisible(bool value)
  {
    this._lvSearchResult.Visible = value;
    if (!value)
      return;
    ListView lvSearchResult = this._lvSearchResult;
    Point location = this.flowLayoutPanel1.Location;
    int x1 = location.X;
    location = this._txtSearch.Location;
    int x2 = location.X;
    int x3 = x1 + x2 + 2;
    location = this.flowLayoutPanel1.Location;
    int y1 = location.Y;
    location = this._pnlSearch.Location;
    int y2 = location.Y;
    int num1 = y1 + y2;
    location = this._txtSearch.Location;
    int y3 = location.Y;
    int y4 = num1 + y3 + this._txtSearch.Height + 1;
    Point point = new Point(x3, y4);
    lvSearchResult.Location = point;
    int height = this._lvSearchResult.Items[0].GetBounds(ItemBoundsPortion.ItemOnly).Height;
    int num2 = this._lvSearchResult.Items.Count < 30 ? this._lvSearchResult.Items.Count : 30;
    this._lvSearchResult.Height = height * num2 + height / 2;
    int num3 = 0;
    foreach (ListViewItem listViewItem in this._lvSearchResult.Items)
    {
      Size size = TextRenderer.MeasureText(listViewItem.Text, this._lvSearchResult.Font);
      if (num3 <= size.Width)
        num3 = size.Width;
    }
    int width = this._lvSearchResult.SmallImageList != null ? this._lvSearchResult.SmallImageList.ImageSize.Width : 0;
    if (num3 > 0)
    {
      this._lvSearchResult.Width = num3 + this._lvSearchResult.Items[0].Position.X + width + 2;
      this._lvSearchResult.Columns[0].Width = num3 + width + 1;
    }
    else
    {
      this._lvSearchResult.Width = this._txtSearch.Width;
      this._lvSearchResult.Columns[0].Width = width + 1;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  private void QuickSearchCallback(List<QuickSearchResultItem> items)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new Action<List<QuickSearchResultItem>>(this.QuickSearchCallback), (object) items);
    }
    else
    {
      if (items == null)
        return;
      this._lvSearchResult.BeginUpdate();
      try
      {
        items.ForEach((Action<QuickSearchResultItem>) (x => this._lvSearchResult.Items.Add(new ListViewItem(x.Caption, x.ImageIndex)
        {
          Tag = (object) x
        })));
      }
      finally
      {
        this._lvSearchResult.EndUpdate();
        this.SetSearchResultVisible(this._lvSearchResult.Items.Count > 0);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  private void Search(string text)
  {
    this._lvSearchResult.BeginUpdate();
    this._lvSearchResult.Items.Clear();
    try
    {
      this._currentQuickSearchProvider.Search(text)?.ForEach((Action<QuickSearchResultItem>) (x => this._lvSearchResult.Items.Add(new ListViewItem(x.Caption, x.ImageIndex)
      {
        Tag = (object) x
      })));
    }
    finally
    {
      this._lvSearchResult.EndUpdate();
      this.SetSearchResultVisible(this._lvSearchResult.Items.Count > 0);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void StopSearch()
  {
    if (!this._delayStop && this._currentQuickSearchProvider != null)
      this._currentQuickSearchProvider.StopSearch();
    this._delayStop = false;
  }

  /// <summary>
  /// 
  /// </summary>
  private void GoToSelectedNode()
  {
    if (this._currentQuickSearchProvider == null || !this._currentQuickSearchProvider.SelectNode(this._lvSearchResult.SelectedItems[0].Tag as QuickSearchResultItem))
      return;
    this._isSearchMode = false;
    this.NavTreeView.Focus();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (ServicesManager.GetService(typeof (IMainFormUpdate)) as IMainFormUpdate is Form service)
        service.RemoveOwnedForm((Form) this);
      this.DisposeNotificationService(this.NotificationService);
      this.ViewsManager.Dispose();
      this.NavTreeView.SelectionChanged -= new EventHandler(this.NavTreeView_SelectionChanged);
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectionWindow));
    this.scFrame = new SplitContainer();
    this.NavTreeView = new NavigatorTreeView();
    this.ViewsManager = new PageViewsManager();
    this.barManager = new BarManager();
    this.leftBarDock = new ToolBarContainer();
    this.rightBarDock = new ToolBarContainer();
    this.bottomBarDock = new ToolBarContainer();
    this.topBarDock = new ToolBarContainer();
    this.panelTop = new Panel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.lbDescription = new Label();
    this._pnlSearch = new Panel();
    this._txtSearch = new TextBox();
    this._lbFind = new Label();
    this.pbObject = new PictureBox();
    this.btOK = new Button();
    this.btCancel = new Button();
    this.statusStrip = new StatusStrip();
    this.labelWarning = new ToolStripStatusLabel();
    this.statusAddress = new ToolStripStatusLabel();
    this.cbDontShowAgain = new CheckBox();
    this._searchResultTimer = new Timer(this.components);
    this._lvSearchResult = new ListView();
    this._colText = new ColumnHeader();
    this.TreeViewsBridge = new TreeViewsBridge(this.components);
    this.scFrame.BeginInit();
    this.scFrame.Panel1.SuspendLayout();
    this.scFrame.Panel2.SuspendLayout();
    this.scFrame.SuspendLayout();
    this.NavTreeView.BeginInit();
    this.panelTop.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this._pnlSearch.SuspendLayout();
    ((ISupportInitialize) this.pbObject).BeginInit();
    this.statusStrip.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.scFrame, "scFrame");
    this.scFrame.FixedPanel = FixedPanel.Panel1;
    this.scFrame.Name = "scFrame";
    this.scFrame.Panel1.Controls.Add((Control) this.NavTreeView);
    componentResourceManager.ApplyResources((object) this.scFrame.Panel1, "scFrame.Panel1");
    this.scFrame.Panel2.Controls.Add((Control) this.ViewsManager);
    componentResourceManager.ApplyResources((object) this.scFrame.Panel2, "scFrame.Panel2");
    this.NavTreeView.AllowDrop = true;
    this.NavTreeView.AllowMultiSelect = false;
    this.NavTreeView.AllowUserPinnedColumns = false;
    this.NavTreeView.DisableCheckedOutColumn = true;
    this.NavTreeView.DisableKeyDownEvents = true;
    componentResourceManager.ApplyResources((object) this.NavTreeView, "NavTreeView");
    this.NavTreeView.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("NavTreeView.HeaderStyle.HorzAlignment");
    this.NavTreeView.ImageList = (ImageList) null;
    this.NavTreeView.LineStyle = LineStyle.Dot;
    this.NavTreeView.Name = "NavTreeView";
    this.NavTreeView.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("NavTreeView.RowEvenStyle.WordWrap");
    this.NavTreeView.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("NavTreeView.RowOddStyle.WordWrap");
    this.NavTreeView.RowSelectedStyle.BackColor = SystemColors.Highlight;
    this.NavTreeView.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("NavTreeView.RowSelectedStyle.WordWrap");
    this.NavTreeView.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.NavTreeView.RowStyle.BorderColor = SystemColors.Control;
    this.NavTreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.NavTreeView.RowStyle.BorderWidth = 1;
    this.NavTreeView.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("NavTreeView.RowStyle.WordWrap");
    this.NavTreeView.SelectBeforeEdit = true;
    this.NavTreeView.ShowRootRow = false;
    this.NavTreeView.SuppressErrorMessages = true;
    this.NavTreeView.UseThemedHeaders = false;
    this.NavTreeView.Enter += new EventHandler(this.TreeView_Enter);
    this.NavTreeView.Leave += new EventHandler(this.NavTreeView_Leave);
    this.ViewsManager.ActiveViewPage = (IViewPage) null;
    this.ViewsManager.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this.ViewsManager, "ViewsManager");
    this.ViewsManager.Name = "ViewsManager";
    this.ViewsManager.ActiveViewPageChanged += new EventHandler(this.DoActiveViewPageChanged);
    this.ViewsManager.Enter += new EventHandler(this.ViewsManager_Enter);
    this.barManager.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.leftBarDock, "leftBarDock");
    this.leftBarDock.Guid = new Guid("b757870e-0701-4e28-9f2d-0df9d9a7cd84");
    this.leftBarDock.Manager = this.barManager;
    this.leftBarDock.Name = "leftBarDock";
    componentResourceManager.ApplyResources((object) this.rightBarDock, "rightBarDock");
    this.rightBarDock.Guid = new Guid("ba7bcecd-7778-4ad4-8f8d-566803af069f");
    this.rightBarDock.Manager = this.barManager;
    this.rightBarDock.Name = "rightBarDock";
    componentResourceManager.ApplyResources((object) this.bottomBarDock, "bottomBarDock");
    this.bottomBarDock.Guid = new Guid("881bdc4f-d07f-4841-971d-5d82e6ab67a3");
    this.bottomBarDock.Manager = this.barManager;
    this.bottomBarDock.Name = "bottomBarDock";
    componentResourceManager.ApplyResources((object) this.topBarDock, "topBarDock");
    this.topBarDock.Guid = new Guid("0b3d312c-796c-40fb-973e-6d6fa27f80d2");
    this.topBarDock.Manager = this.barManager;
    this.topBarDock.Name = "topBarDock";
    this.panelTop.Controls.Add((Control) this.flowLayoutPanel1);
    this.panelTop.Controls.Add((Control) this.pbObject);
    componentResourceManager.ApplyResources((object) this.panelTop, "panelTop");
    this.panelTop.Name = "panelTop";
    componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
    this.flowLayoutPanel1.Controls.Add((Control) this.lbDescription);
    this.flowLayoutPanel1.Controls.Add((Control) this._pnlSearch);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.lbDescription.AutoEllipsis = true;
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    this._pnlSearch.Controls.Add((Control) this._txtSearch);
    this._pnlSearch.Controls.Add((Control) this._lbFind);
    componentResourceManager.ApplyResources((object) this._pnlSearch, "_pnlSearch");
    this._pnlSearch.Name = "_pnlSearch";
    componentResourceManager.ApplyResources((object) this._txtSearch, "_txtSearch");
    this._txtSearch.Name = "_txtSearch";
    this._txtSearch.TextChanged += new EventHandler(this.On_txtSearch_TextChanged);
    this._txtSearch.Enter += new EventHandler(this.On_txtSearch_Enter);
    this._txtSearch.KeyDown += new KeyEventHandler(this.On_txtSearch_KeyDown);
    this._txtSearch.Leave += new EventHandler(this.On_SearchControls_Leave);
    componentResourceManager.ApplyResources((object) this._lbFind, "_lbFind");
    this._lbFind.Name = "_lbFind";
    componentResourceManager.ApplyResources((object) this.pbObject, "pbObject");
    this.pbObject.Name = "pbObject";
    this.pbObject.TabStop = false;
    componentResourceManager.ApplyResources((object) this.btOK, "btOK");
    this.btOK.Cursor = Cursors.Arrow;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Name = "btOK";
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.Cursor = Cursors.Default;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    componentResourceManager.ApplyResources((object) this.statusStrip, "statusStrip");
    this.statusStrip.GripStyle = ToolStripGripStyle.Visible;
    this.statusStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.labelWarning,
      (ToolStripItem) this.statusAddress
    });
    this.statusStrip.Name = "statusStrip";
    componentResourceManager.ApplyResources((object) this.labelWarning, "labelWarning");
    this.labelWarning.Name = "labelWarning";
    this.statusAddress.Name = "statusAddress";
    componentResourceManager.ApplyResources((object) this.statusAddress, "statusAddress");
    componentResourceManager.ApplyResources((object) this.cbDontShowAgain, "cbDontShowAgain");
    this.cbDontShowAgain.Name = "cbDontShowAgain";
    this.cbDontShowAgain.UseVisualStyleBackColor = true;
    this._searchResultTimer.Tick += new EventHandler(this.On_searchResultTimer_Tick);
    this._lvSearchResult.Columns.AddRange(new ColumnHeader[1]
    {
      this._colText
    });
    this._lvSearchResult.FullRowSelect = true;
    this._lvSearchResult.HeaderStyle = ColumnHeaderStyle.None;
    this._lvSearchResult.HideSelection = false;
    componentResourceManager.ApplyResources((object) this._lvSearchResult, "_lvSearchResult");
    this._lvSearchResult.MultiSelect = false;
    this._lvSearchResult.Name = "_lvSearchResult";
    this._lvSearchResult.Sorting = SortOrder.Ascending;
    this._lvSearchResult.TabStop = false;
    this._lvSearchResult.UseCompatibleStateImageBehavior = false;
    this._lvSearchResult.View = View.Details;
    this._lvSearchResult.DoubleClick += new EventHandler(this.On_lvSearchResult_DoubleClick);
    this._lvSearchResult.Enter += new EventHandler(this.On_lvSearchResult_Enter);
    this._lvSearchResult.KeyDown += new KeyEventHandler(this.On_lvSearchResult_KeyDown);
    this._lvSearchResult.Leave += new EventHandler(this.On_SearchControls_Leave);
    componentResourceManager.ApplyResources((object) this._colText, "_colText");
    this.TreeViewsBridge.NavTreeView = this.NavTreeView;
    this.TreeViewsBridge.ViewsManager = (IViewsManager) this.ViewsManager;
    this.AcceptButton = (IButtonControl) this.btOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this._lvSearchResult);
    this.Controls.Add((Control) this.cbDontShowAgain);
    this.Controls.Add((Control) this.statusStrip);
    this.Controls.Add((Control) this.scFrame);
    this.Controls.Add((Control) this.panelTop);
    this.Controls.Add((Control) this.btOK);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.leftBarDock);
    this.Controls.Add((Control) this.rightBarDock);
    this.Controls.Add((Control) this.bottomBarDock);
    this.Controls.Add((Control) this.topBarDock);
    this.DoubleBuffered = true;
    this.HelpButton = true;
    this.MinimizeBox = false;
    this.Name = nameof (SelectionWindow);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.HelpButtonClicked += new CancelEventHandler(this.SelectionWindow_HelpButtonClicked);
    this.Activated += new EventHandler(this.SelectionWindow_Activated);
    this.FormClosing += new FormClosingEventHandler(this.SelectionWindow_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.SelectionWindow_FormClosed);
    this.Load += new EventHandler(this.SelectionWindow_Load);
    this.scFrame.Panel1.ResumeLayout(false);
    this.scFrame.Panel2.ResumeLayout(false);
    this.scFrame.EndInit();
    this.scFrame.ResumeLayout(false);
    this.NavTreeView.EndInit();
    this.panelTop.ResumeLayout(false);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this._pnlSearch.ResumeLayout(false);
    this._pnlSearch.PerformLayout();
    ((ISupportInitialize) this.pbObject).EndInit();
    this.statusStrip.ResumeLayout(false);
    this.statusStrip.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private enum FocusTarget
  {
    Tree,
    View,
  }

  public sealed class SelectionWindowCustomControlProvider
  {
    public SelectionWindowCustomControlProvider(Control control)
    {
      this.Control = control != null ? control : throw new ArgumentNullException(nameof (control));
    }

    public Control Control { get; private set; }
  }

  public interface ISelectionWindowCustomControl
  {
    void Initilize(SelectionWindow selectionWindow);

    void Deinitilize();
  }

  [Serializable]
  public sealed class SelectionWindowMemento
  {
    public SelectionWindow.NodeIDPathWrapper NavigatorTreeViewFocusedPath { get; set; }
  }

  [Serializable]
  public sealed class NodeIDPathWrapper : ISerializable
  {
    [NonSerialized]
    private NodeIDPath _path;

    public NodeIDPathWrapper(NodeIDPath path)
    {
      this._path = path != null ? path : throw new ArgumentNullException(nameof (path));
    }

    private NodeIDPathWrapper(SerializationInfo info, StreamingContext context)
    {
      try
      {
        string str = (string) info.GetValue(nameof (Path), typeof (string));
        List<PersistentState> persistentStateList = new List<PersistentState>();
        BinaryStateFormatter binaryStateFormatter = new BinaryStateFormatter();
        char[] chArray = new char[1]{ '|' };
        foreach (string s in str.Split(chArray))
        {
          if (!string.IsNullOrEmpty(s))
          {
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(s)))
              persistentStateList.Add(binaryStateFormatter.Deserialize((Stream) memoryStream));
          }
        }
        this._path = Intermech.Navigator.Utils.DeserializePath(persistentStateList.ToArray(), (System.IServiceProvider) ServicesManager.ServiceContainer);
      }
      catch (Exception ex)
      {
        this._path = new NodeIDPath((IDescriptor) null);
      }
    }

    public NodeIDPath Path => this._path;

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      try
      {
        List<string> values = new List<string>();
        PersistentState[] persistentStateArray = Intermech.Navigator.Utils.SerializePath(this._path, (System.IServiceProvider) ServicesManager.ServiceContainer);
        BinaryStateFormatter binaryStateFormatter = new BinaryStateFormatter();
        foreach (PersistentState state in persistentStateArray)
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            binaryStateFormatter.Serialize((Stream) memoryStream, state);
            values.Add(Convert.ToBase64String(memoryStream.ToArray()));
          }
        }
        info.AddValue("Path", (object) string.Join("|", (IEnumerable<string>) values));
      }
      catch (Exception ex)
      {
      }
    }
  }
}
