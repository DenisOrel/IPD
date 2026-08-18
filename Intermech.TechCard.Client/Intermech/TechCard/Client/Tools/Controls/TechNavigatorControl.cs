// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.TechNavigatorControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls;

/// <summary>Контрол TechCard для закладок</summary>
/// <summary>Технологический контрол типа навигатора</summary>
public class TechNavigatorControl : UserControl, IIODestination, ICurrentSelectedItemsHost
{
  /// <summary>Дескриптор</summary>
  private IDescriptor _rootDescriptor;
  /// <summary>Менеджер команд</summary>
  private ICommandManager _commandManager;
  /// <summary>Контейнер сервисов</summary>
  private IServiceContainer _services;
  /// <summary>Диспетчер событий</summary>
  private readonly IIODispatcher _ioDispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>Служба уведомлений</summary>
  private INotificationService _notificationService;
  /// <summary>Список событий</summary>
  private Dictionary<string, Delegate> _eventTable;
  /// <summary>
  /// Текущая коллекция выделенных элементов - от дерева или от менеджера закладок
  /// </summary>
  private ISelectedItemsHost _selectedItemsHost;
  /// <summary>Опции, управляющие поведением и внешним видом окна</summary>
  public SelectionOptions Options = SelectionOptions.Default;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeViewsBridge viewsBridge;
  private SplitContainer mainSplitCntnr;
  private Panel pnTreeView;
  private NavigatorTreeView treeView;
  private Intermech.Bars.ToolBar tbTreePanel;
  private PageViewsManager viewsManager;

  /// <summary>Инициализация объектов</summary>
  private void InitData()
  {
    this._rootDescriptor = (IDescriptor) null;
    this._eventTable = new Dictionary<string, Delegate>();
    this._eventTable.Add("SelectedItemsChanged", (Delegate) null);
    this._eventTable.Add("DoubleClick", (Delegate) null);
  }

  /// <summary>Инициализация сервисов контрола</summary>
  private void InitializeServices()
  {
    this._commandManager = ServiceUtils.GetService<ICommandManager>((object) ApplicationServices.Container, false);
    this._notificationService = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    this._services = (IServiceContainer) new ServiceContainer();
    this._services.AddService(typeof (IViewState), (object) new ViewStateService());
    if (this._commandManager != null)
      this._services.AddService(typeof (ICommandManager), (object) this._commandManager);
    if (this._notificationService != null)
      this._services.AddService(typeof (INotificationService), (object) this._notificationService);
    if (this._ioDispatcher != null)
    {
      this._ioDispatcher.RegisterDestination((IIODestination) this);
      this._services.AddService(typeof (IIODispatcher), (object) this._ioDispatcher);
    }
    if (this.treeView != null)
      this._services.AddService(typeof (NavigatorTreeView), (object) this.treeView);
    if (this.viewsManager != null)
      this._services.AddService(typeof (IViewsManager), (object) this.viewsManager);
    this._services.AddService(typeof (ICurrentSelectedItemsHost), (object) this);
  }

  /// <summary>Инициализация контролов</summary>
  private void InitializeCustomControls()
  {
    this.TreeView.Services = (System.IServiceProvider) this._services;
    this.TreeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    this.TreeView.BeforeFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.OnBeforeFocusNodeEventHandler);
    this.TreeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.OnAfterFocusNode);
    this.ViewsManager.Services = (System.IServiceProvider) this._services;
    this.ViewsManager.Enter += new EventHandler(this.ViewsManager_Enter);
    this.ViewsManager.ActiveViewPageChanged += new EventHandler(this.DoActiveViewPageChanged);
  }

  /// <summary>Изменилась текущая коллекция выделенных элементов</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void SelectedItemsChangedHandler(object sender, EventArgs e)
  {
    this.RaiseSelectedItemsChanged(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void RaiseSelectedItemsChanged(object sender, EventArgs e)
  {
    this.FireEvent("SelectedItemsChanged", sender, e);
  }

  /// <summary>Конструктор</summary>
  public TechNavigatorControl()
  {
    this.InitializeComponent();
    this.InitData();
    if (this.DesignMode)
      return;
    this.InitializeServices();
    this.InitializeCustomControls();
  }

  /// <summary>ViewsBridge</summary>
  public TreeViewsBridge ViewsBridge => this.viewsBridge;

  /// <summary>TreeView</summary>
  public NavigatorTreeView TreeView => this.treeView;

  /// <summary>ViewsManager</summary>
  public PageViewsManager ViewsManager => this.viewsManager;

  /// <summary>RootDescriptor</summary>
  public IDescriptor RootDescriptor
  {
    get => this._rootDescriptor;
    set
    {
      if (object.Equals((object) this._rootDescriptor, (object) value))
        return;
      this._rootDescriptor = value;
      this.TreeView.Build(value);
    }
  }

  /// <summary>Активная закладка</summary>
  public IViewPage ActiveViewPage => this.viewsManager.ActiveViewPage;

  /// <summary>Контейнер сервисов</summary>
  public IServiceContainer Services => this._services;

  /// <summary>Список поддерживаемых обработчиком событий</summary>
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyDown | IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  /// <summary>Обработка событий</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если обработка выполнена успешно, false, если событие не обработано</returns>
  public bool ProcessEvent(IIOEvent Event)
  {
    if (Event == null || this.viewsManager.ActiveViewPage == null || Event.Source.Control != this.viewsManager.ActiveViewPage.Control || Event.EventType != IOEventType.evMouseDoubleClick && (Event.EventType != IOEventType.evKeyDown || ((KeyEventArgs) Event.EventData).KeyCode != Keys.Return) || Event.Source.SelectedItems == null || Event.Source.SelectedItems.Count <= 0)
      return false;
    this.DoubleClickProceedEvent(Event);
    return true;
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

  /// <summary>Загрузка параметров</summary>
  public void LoadLayout(IDictionary dictionary)
  {
    object obj = dictionary[(object) "SplitterDistance"];
    if (obj == null)
      return;
    this.mainSplitCntnr.SplitterDistance = (int) obj;
  }

  /// <summary>Сохранение параметров</summary>
  public void SaveLayout(IDictionary dictionary)
  {
    dictionary[(object) "SplitterDistance"] = (object) this.mainSplitCntnr.SplitterDistance;
  }

  /// <summary>Событие изменения выделенных элементов</summary>
  public event EventHandler SelectedItemsChanged
  {
    add
    {
      this._eventTable["SelectedItemsChanged"] = Delegate.Combine(this._eventTable["SelectedItemsChanged"], (Delegate) value);
    }
    remove
    {
      this._eventTable["SelectedItemsChanged"] = Delegate.Remove(this._eventTable["SelectedItemsChanged"], (Delegate) value);
    }
  }

  /// <summary>DoubleClick</summary>
  public event TechNavigatorEventHandler DoubleClick
  {
    add
    {
      this._eventTable["DoubleClick"] = Delegate.Combine(this._eventTable["DoubleClick"], (Delegate) value);
    }
    remove
    {
      this._eventTable["DoubleClick"] = Delegate.Remove(this._eventTable["DoubleClick"], (Delegate) value);
    }
  }

  /// <summary>Узел в дереве фокусируется</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void OnBeforeFocusNodeEventHandler(object sender, NavigatorTreeNodeEventArgs e)
  {
    if ((this.Options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
      this.ItemsHost = (ISelectedItemsHost) null;
    else
      this.ItemsHost = (ISelectedItemsHost) this.TreeView;
  }

  /// <summary>Узел в дереве сфокусировался</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void OnAfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    if ((this.Options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
      this.ItemsHost = (ISelectedItemsHost) null;
    else
      this.ItemsHost = (ISelectedItemsHost) this.TreeView;
  }

  /// <summary>Фокус пришёл на закладки</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void ViewsManager_Enter(object sender, EventArgs e)
  {
    this.DoActiveViewPageChanged((object) this, (EventArgs) null);
  }

  /// <summary>Изменилась текущая активная страничка в закладках</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void DoActiveViewPageChanged(object sender, EventArgs e)
  {
    IView view = this.ViewsManager.ActiveViewPage?.View;
    ISelectedItemsHost selectedItemsHost1 = view as ISelectedItemsHost;
    if (view is ChildrenView childrenView)
      selectedItemsHost1 = childrenView.SelectedItemsHost;
    if ((this.Options & SelectionOptions.DisableSelectFromViews) != (SelectionOptions) 0 && selectedItemsHost1 != this.TreeView)
      selectedItemsHost1 = (ISelectedItemsHost) null;
    ISelectedItemsHost selectedItemsHost2 = selectedItemsHost1 ?? (ISelectedItemsHost) this.TreeView;
    if ((this.Options & SelectionOptions.DisableSelectFromTree) != (SelectionOptions) 0)
    {
      this.ItemsHost = selectedItemsHost2 == this.TreeView ? (ISelectedItemsHost) null : selectedItemsHost2;
    }
    else
    {
      NavigatorTreeView treeView = this.TreeView;
      if (treeView != null && treeView != selectedItemsHost2 && treeView.Focused)
        selectedItemsHost2 = (ISelectedItemsHost) this.TreeView;
      this.ItemsHost = selectedItemsHost2;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="eventName"></param>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FireEvent(string eventName, object sender, EventArgs e)
  {
    if (this._eventTable[eventName] is EventHandler eventHandler)
    {
      eventHandler(sender, e);
    }
    else
    {
      if (!(this._eventTable[eventName] is TechNavigatorEventHandler navigatorEventHandler))
        return;
      navigatorEventHandler(sender, e as TechNavigatorEventArgs);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="Event"></param>
  private void DoubleClickProceedEvent(IIOEvent Event)
  {
    TechNavigatorEventArgs e = new TechNavigatorEventArgs(Event);
    this.FireEvent("DoubleClick", Event.Source.Control, (EventArgs) e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ViewsManager_ActiveViewPageChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.viewsBridge?.Dispose();
      this.viewsBridge = (TreeViewsBridge) null;
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
    this.viewsBridge = new TreeViewsBridge(this.components);
    this.treeView = new NavigatorTreeView();
    this.viewsManager = new PageViewsManager();
    this.mainSplitCntnr = new SplitContainer();
    this.pnTreeView = new Panel();
    this.tbTreePanel = new Intermech.Bars.ToolBar();
    this.treeView.BeginInit();
    this.mainSplitCntnr.BeginInit();
    this.mainSplitCntnr.Panel1.SuspendLayout();
    this.mainSplitCntnr.Panel2.SuspendLayout();
    this.mainSplitCntnr.SuspendLayout();
    this.pnTreeView.SuspendLayout();
    this.SuspendLayout();
    this.viewsBridge.NavTreeView = this.treeView;
    this.viewsBridge.ViewsManager = (IViewsManager) this.viewsManager;
    this.treeView.AllowDrop = true;
    this.treeView.AllowMultiSelect = false;
    this.treeView.AllowUserPinnedColumns = false;
    this.treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.treeView.DisableCheckedOutColumn = true;
    this.treeView.ImageList = (ImageList) null;
    this.treeView.LineStyle = LineStyle.Dot;
    this.treeView.Location = new Point(0, 24);
    this.treeView.Name = "treeView";
    this.treeView.SelectBeforeEdit = true;
    this.treeView.ShowRootRow = false;
    this.treeView.Size = new Size(250, 283);
    this.treeView.SuppressErrorMessages = true;
    this.treeView.TabIndex = 0;
    this.viewsManager.ActiveViewPage = (IViewPage) null;
    this.viewsManager.AllowedViews = new string[0];
    this.viewsManager.CausesValidation = false;
    this.viewsManager.Dock = DockStyle.Fill;
    this.viewsManager.Font = new Font("Tahoma", 8.25f);
    this.viewsManager.Location = new Point(0, 0);
    this.viewsManager.Name = "viewsManager";
    this.viewsManager.Padding = new Padding(10, 0, 0, 0);
    this.viewsManager.Size = new Size(281, 307);
    this.viewsManager.TabIndex = 11;
    this.viewsManager.ActiveViewPageChanged += new EventHandler(this.ViewsManager_ActiveViewPageChanged);
    this.mainSplitCntnr.Dock = DockStyle.Fill;
    this.mainSplitCntnr.FixedPanel = FixedPanel.Panel1;
    this.mainSplitCntnr.Location = new Point(0, 0);
    this.mainSplitCntnr.Name = "mainSplitCntnr";
    this.mainSplitCntnr.Panel1.Controls.Add((Control) this.pnTreeView);
    this.mainSplitCntnr.Panel2.Controls.Add((Control) this.viewsManager);
    this.mainSplitCntnr.Size = new Size(535, 307);
    this.mainSplitCntnr.SplitterDistance = 250;
    this.mainSplitCntnr.TabIndex = 11;
    this.pnTreeView.Controls.Add((Control) this.treeView);
    this.pnTreeView.Controls.Add((Control) this.tbTreePanel);
    this.pnTreeView.Dock = DockStyle.Fill;
    this.pnTreeView.Location = new Point(0, 0);
    this.pnTreeView.Name = "pnTreeView";
    this.pnTreeView.Size = new Size(250, 307);
    this.pnTreeView.TabIndex = 9;
    this.tbTreePanel.Dock = DockStyle.Fill;
    this.tbTreePanel.FlipLastItem = true;
    this.tbTreePanel.FullMenus = true;
    this.tbTreePanel.Guid = new Guid("3fb71a02-4b93-44ea-84a6-db6e9ca5869f");
    this.tbTreePanel.Hidden = false;
    this.tbTreePanel.Location = new Point(0, 0);
    this.tbTreePanel.Name = "tbTreePanel";
    this.tbTreePanel.Size = new Size(250, 18);
    this.tbTreePanel.TabIndex = 7;
    this.tbTreePanel.Text = "toolBar1";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.mainSplitCntnr);
    this.Name = nameof (TechNavigatorControl);
    this.Size = new Size(535, 307);
    this.treeView.EndInit();
    this.mainSplitCntnr.Panel1.ResumeLayout(false);
    this.mainSplitCntnr.Panel2.ResumeLayout(false);
    this.mainSplitCntnr.EndInit();
    this.mainSplitCntnr.ResumeLayout(false);
    this.pnTreeView.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
