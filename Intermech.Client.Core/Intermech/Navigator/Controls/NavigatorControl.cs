
// Type: Intermech.Navigator.Controls.NavigatorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

public class NavigatorControl : 
  UserControl,
  ICommandTarget,
  IIODestination,
  ICurrentSelectedItemsHost
{
  /// <summary>Опции, управляющие поведением и внешним видом окна</summary>
  public SelectionOptions Options = SelectionOptions.Default;
  /// <summary>
  /// Текущая коллекция выделенных элементов - от дерева или от менеджера закладок
  /// </summary>
  protected ISelectedItemsHost _selectedItemsHost;
  /// <summary>Контейнер сервисов</summary>
  private AdvancedServiceContainer _services;
  /// <summary>Менеджер команд</summary>
  private CommandManager _commandManager;
  /// <summary>Диспетчер событий</summary>
  private IIODispatcher _IODispatcher;
  /// <summary>
  /// Сервис службы "горячих клавиш" и связанных с ними команд
  /// </summary>
  private IHotKeysManager _hotKeysManager;
  /// <summary>Сервис локальной службы уведомлений</summary>
  private UISwitchedNotificationService _notificationService;
  /// <summary>Коллекции команд по умолчанию</summary>
  private IDefaultCommands4ObjTypes _defaultCommands4ObjTypes;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal SplitContainer scFrame;
  public NavigatorControlTreeView NavTreeView;
  public PageViewsManager ViewsManager;
  public TreeViewsBridge TreeViewsBridge;

  public NavigatorControl()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this._IODispatcher = (IIODispatcher) new IODispatcher();
    this._IODispatcher.RegisterDestination((IIODestination) this);
    this._defaultCommands4ObjTypes = ServicesManager.GetService(typeof (IDefaultCommands4ObjTypes)) as IDefaultCommands4ObjTypes;
    this._hotKeysManager = ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager;
    this._commandManager = new CommandManager();
    this._commandManager.ActiveTarget = (ICommandTarget) this.ViewsManager;
    this._services = new AdvancedServiceContainer();
    this._notificationService = new UISwitchedNotificationService();
    this._notificationService.Parent = (NotificationService) ServicesManager.GetService(typeof (INotificationService));
    this._services.AddService(typeof (INotificationService), (object) this._notificationService);
    this._services.AddService(typeof (IIODispatcher), (object) this._IODispatcher);
    this._services.AddService(typeof (IDefaultCommands4ObjTypes), (object) this._defaultCommands4ObjTypes);
    this._services.AddService(typeof (ICommandManager), (object) this._commandManager);
    this._services.AddService(typeof (ICurrentSelectedItemsHost), (object) this);
    this._services.AddService(typeof (NavigatorTreeView), (object) this.NavTreeView);
    this._services.AddService(typeof (IViewsManager), (object) this.ViewsManager);
    this._services.AddService(typeof (SelectionOptionsHolder), (object) new SelectionOptionsHolder(this.Options));
    this._services.AddService(typeof (INotificationServiceStatesHolder), (object) new NotificationServiceStatesHolder(NotificationServiceStates.InactiveDialog));
    AdvancedServiceContainer serviceContainer1 = new AdvancedServiceContainer((System.IServiceProvider) this._services);
    AdvancedServiceContainer serviceContainer2 = new AdvancedServiceContainer((System.IServiceProvider) this._services);
    serviceContainer1.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NodeInTree));
    serviceContainer2.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NodeInViews));
    this.NavTreeView.Services = (System.IServiceProvider) serviceContainer1;
    this.ViewsManager.Services = (System.IServiceProvider) serviceContainer2;
    this.NavTreeView.Focus();
  }

  private void OnAfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    if ((this.Options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
      this.ItemsHost = (ISelectedItemsHost) null;
    else
      this.ItemsHost = (ISelectedItemsHost) this.NavTreeView;
  }

  private void OnBeforeFocusNodeEventHandler(object sender, NavigatorTreeNodeEventArgs e)
  {
    if ((this.Options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
      this.ItemsHost = (ISelectedItemsHost) null;
    else
      this.ItemsHost = (ISelectedItemsHost) this.NavTreeView;
  }

  /// <summary>Фокус пришёл на дерево</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void TreeView_Enter(object sender, EventArgs e)
  {
    if ((this.Options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
    {
      this.ItemsHost = (ISelectedItemsHost) null;
    }
    else
    {
      this.ItemsHost = (ISelectedItemsHost) this.NavTreeView;
      this.UpdateCommandManagerItems();
    }
  }

  private void NavTreeView_Leave(object sender, EventArgs e)
  {
  }

  /// <summary>Изменилась текущая активная страничка в закладках</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoActiveViewPageChanged(object sender, EventArgs e)
  {
    IView view = this.ViewsManager.ActiveViewPage != null ? this.ViewsManager.ActiveViewPage.View : (IView) null;
    ISelectedItemsHost selectedItemsHost = view != null ? view as ISelectedItemsHost : (ISelectedItemsHost) null;
    if ((this.Options & SelectionOptions.DisableSelectFromViews) != (SelectionOptions) 0 && selectedItemsHost != this.NavTreeView)
      this.ItemsHost = (ISelectedItemsHost) null;
    this.ItemsHost = selectedItemsHost ?? (ISelectedItemsHost) this.NavTreeView;
    if ((this.Options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0 && selectedItemsHost == this.NavTreeView)
      this.ItemsHost = (ISelectedItemsHost) null;
    else
      this.UpdateCommandManagerItems();
  }

  /// <summary>Фокус пришёл на закладки</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ViewsManager_Enter(object sender, EventArgs e)
  {
    this.DoActiveViewPageChanged((object) this, (EventArgs) null);
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
      this.RaiseSelectedItemsChanged((object) this, new EventArgs());
    }
  }

  private void SelectedItemsChangedHandler(object sender, EventArgs e)
  {
    this.RaiseSelectedItemsChanged(sender, e);
  }

  /// <summary>Выполнить команду от сервиса ICommandManager</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если команда выполнена</returns>
  public bool Execute(ICommandState commandState)
  {
    return this.NavTreeView.Focused ? this.NavTreeView.Execute(commandState) : this.ViewsManager.Execute(commandState);
  }

  /// <summary>Запросить статус указанной команды</summary>
  /// <param name="commandState">Команда</param>
  /// <returns>true, если запрос был обработан</returns>
  public bool QueryStatus(ICommandState commandState)
  {
    return this.NavTreeView.Focused ? this.NavTreeView.QueryStatus(commandState) : this.ViewsManager.QueryStatus(commandState);
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
    if ((Event.EventType == IOEventType.evKeyUp || Event.EventType == IOEventType.evKeyDown) && this._hotKeysManager != null)
    {
      KeyEventArgs eventData = (KeyEventArgs) Event.EventData;
      if (Event.EventType == IOEventType.evKeyDown && eventData.KeyCode == eventData.KeyData && eventData.Modifiers == Keys.None || Event.EventType == IOEventType.evKeyUp && !eventData.KeyData.HasFlag((Enum) eventData.KeyCode) && eventData.Modifiers != Keys.None)
        return false;
      List<IHotKeysCommand> commands = this._hotKeysManager[((KeyEventArgs) Event.EventData).KeyCode | ((KeyEventArgs) Event.EventData).Modifiers];
      if (commands != null && commands.Count > 0)
      {
        ((KeyEventArgs) Event.EventData).Handled = true;
        return this.ExecuteMenuCommand(commands, Event);
      }
    }
    if (this.ViewsManager.ActiveViewPage != null && Event.Source.Control == this.ViewsManager.ActiveViewPage.Control)
    {
      if (Event.EventType == IOEventType.evMouseDoubleClick || Event.EventType == IOEventType.evKeyUp && ((KeyEventArgs) Event.EventData).KeyCode == Keys.Return)
      {
        if (Event.Source.SelectedItems == null || Event.Source.SelectedItems.Count <= 0)
          return false;
        this.RaiseSelectedItemsChanged((object) this, new EventArgs());
        INodeID itemId = Event.Source.SelectedItems.GetItemID(0);
        if (Event.Source.SelectedItems.Count == 1 && (Event.Source is IFoldersView || itemId != null || this.NavTreeView.RootDescriptor.GetRecordNodeID() == null || itemId.CategoryID != this.NavTreeView.RootDescriptor.GetRecordNodeID().CategoryID) && this.BrowseToPath(Event))
          return true;
        Event.EventFlags |= IOEventFlags.efProcessed;
        return true;
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
  /// Сгенерировать событие "Изменились выделенные элементы"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void RaiseSelectedItemsChanged(object sender, EventArgs e)
  {
    if (this.SelectedItemsChanged == null)
      return;
    this.SelectedItemsChanged((object) (this as ISelectionWindow), e);
  }

  private void UpdateCommandManagerItems()
  {
    (this._services != null ? this._services.GetService(typeof (ICommandManager)) as ICommandManager : ServicesManager.GetService(typeof (ICommandManager)) as ICommandManager)?.QueryStatus();
  }

  /// <summary>Событие "Изменились выделенные элементы"</summary>
  public event EventHandler SelectedItemsChanged;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.scFrame = new SplitContainer();
    this.NavTreeView = new NavigatorControlTreeView();
    this.ViewsManager = new PageViewsManager();
    this.TreeViewsBridge = new TreeViewsBridge(this.components);
    this.scFrame.BeginInit();
    this.scFrame.Panel1.SuspendLayout();
    this.scFrame.Panel2.SuspendLayout();
    this.scFrame.SuspendLayout();
    this.NavTreeView.BeginInit();
    this.SuspendLayout();
    this.scFrame.Dock = DockStyle.Fill;
    this.scFrame.FixedPanel = FixedPanel.Panel1;
    this.scFrame.Location = new Point(0, 0);
    this.scFrame.Margin = new Padding(4);
    this.scFrame.Name = "scFrame";
    this.scFrame.Panel1.Controls.Add((Control) this.NavTreeView);
    this.scFrame.Panel1.Padding = new Padding(2);
    this.scFrame.Panel1MinSize = 192 /*0xC0*/;
    this.scFrame.Panel2.Controls.Add((Control) this.ViewsManager);
    this.scFrame.Panel2.Padding = new Padding(2);
    this.scFrame.Panel2MinSize = 100;
    this.scFrame.Size = new Size(763, 440);
    this.scFrame.SplitterDistance = 250;
    this.scFrame.TabIndex = 1;
    this.NavTreeView.AllowDrop = true;
    this.NavTreeView.AllowMultiSelect = false;
    this.NavTreeView.AllowUserPinnedColumns = false;
    this.NavTreeView.DisableCheckedOutColumn = false;
    this.NavTreeView.DisableKeyDownEvents = true;
    this.NavTreeView.Dock = DockStyle.Fill;
    this.NavTreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.NavTreeView.LineStyle = LineStyle.Dot;
    this.NavTreeView.Location = new Point(2, 2);
    this.NavTreeView.Name = "NavTreeView";
    this.NavTreeView.RowEvenStyle.WordWrap = false;
    this.NavTreeView.RowOddStyle.WordWrap = false;
    this.NavTreeView.RowSelectedStyle.BackColor = SystemColors.Highlight;
    this.NavTreeView.RowSelectedStyle.WordWrap = false;
    this.NavTreeView.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.NavTreeView.RowStyle.BorderColor = SystemColors.Control;
    this.NavTreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.NavTreeView.RowStyle.BorderWidth = 1;
    this.NavTreeView.RowStyle.WordWrap = false;
    this.NavTreeView.SelectBeforeEdit = true;
    this.NavTreeView.ShowRootRow = false;
    this.NavTreeView.Size = new Size(246, 436);
    this.NavTreeView.SuppressErrorMessages = true;
    this.NavTreeView.TabIndex = 1;
    this.NavTreeView.UseThemedHeaders = false;
    this.NavTreeView.BeforeFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.OnBeforeFocusNodeEventHandler);
    this.NavTreeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.OnAfterFocusNode);
    this.NavTreeView.Enter += new EventHandler(this.TreeView_Enter);
    this.NavTreeView.Leave += new EventHandler(this.NavTreeView_Leave);
    this.ViewsManager.ActiveViewPage = (IViewPage) null;
    this.ViewsManager.CausesValidation = false;
    this.ViewsManager.Dock = DockStyle.Fill;
    this.ViewsManager.Font = new Font("Tahoma", 8.25f);
    this.ViewsManager.Location = new Point(2, 2);
    this.ViewsManager.Name = "ViewsManager";
    this.ViewsManager.Padding = new Padding(10);
    this.ViewsManager.Size = new Size(505, 436);
    this.ViewsManager.TabIndex = 0;
    this.ViewsManager.ActiveViewPageChanged += new EventHandler(this.DoActiveViewPageChanged);
    this.ViewsManager.Enter += new EventHandler(this.ViewsManager_Enter);
    this.TreeViewsBridge.NavTreeView = (NavigatorTreeView) this.NavTreeView;
    this.TreeViewsBridge.ViewsManager = (IViewsManager) this.ViewsManager;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.scFrame);
    this.Name = nameof (NavigatorControl);
    this.Size = new Size(763, 440);
    this.scFrame.Panel1.ResumeLayout(false);
    this.scFrame.Panel2.ResumeLayout(false);
    this.scFrame.EndInit();
    this.scFrame.ResumeLayout(false);
    this.NavTreeView.EndInit();
    this.ResumeLayout(false);
  }
}
