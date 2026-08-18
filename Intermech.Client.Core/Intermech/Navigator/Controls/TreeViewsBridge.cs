
// Type: Intermech.Navigator.Controls.TreeViewsBridge
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Компонент обеспечивает интеграцию дерева навигатора с менеджером закладок
/// </summary>
public class TreeViewsBridge : Component
{
  /// <summary>Дерево "Навигатора"</summary>
  private NavigatorTreeView navTreeView;
  /// <summary>Менеджер закладок</summary>
  private IViewsManager viewsManager;
  /// <summary>Использовать задержки</summary>
  private bool useDelay;
  /// <summary>Разрешена ли интеграция с деревом "Навигатора"</summary>
  private bool bridgeEnabled = true;
  /// <summary>Таймер</summary>
  private Timer tmDelayedUpdate;
  /// <summary>Компоненты</summary>
  private IContainer components;
  private bool? _alwaysShowFirtTab;

  /// <summary>Создать экземпляр класса</summary>
  public TreeViewsBridge()
  {
    this.InitializeComponent();
    this.InitializeFields();
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="container"></param>
  public TreeViewsBridge(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
    this.InitializeFields();
  }

  /// <summary>Очистить ресурсы</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.tmDelayedUpdate.Stop();
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
    this.tmDelayedUpdate = new Timer(this.components);
    this.tmDelayedUpdate.Interval = 250;
    this.tmDelayedUpdate.Tick += new EventHandler(this.UpdateViews);
  }

  /// <summary>
  /// Возвращает или устанавливает дерево навигатора, с которым интегрируется менеджер закладок
  /// </summary>
  [Browsable(true)]
  [DefaultValue(null)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_148")]
  public NavigatorTreeView NavTreeView
  {
    [DebuggerStepThrough] get => this.navTreeView;
    set
    {
      if (this.navTreeView == value)
        return;
      this.tmDelayedUpdate.Stop();
      if (this.navTreeView != null)
      {
        this.navTreeView.AfterFocusNode -= new EventHandler<NavigatorTreeNodeEventArgs>(this.TreeView_AfterFocusNode);
        this.navTreeView.ClearTree -= new EventHandler(this.TreeView_ClearTree);
      }
      this.navTreeView = value;
      if (this.navTreeView == null)
        return;
      this.navTreeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.TreeView_AfterFocusNode);
      this.navTreeView.ClearTree += new EventHandler(this.TreeView_ClearTree);
    }
  }

  /// <summary>
  /// Возвращает или устанавливает менеджер закладок, который интегрируется с деревом навигатора
  /// </summary>
  [Browsable(true)]
  [DefaultValue(null)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_149")]
  public IViewsManager ViewsManager
  {
    [DebuggerStepThrough] get => this.viewsManager;
    set
    {
      if (this.viewsManager == value)
        return;
      this.tmDelayedUpdate.Stop();
      this.viewsManager = value;
    }
  }

  /// <summary>
  /// Возвращает или устанавливает необходимость использовать задержку обновления
  /// списка закладок после смены сфокусированного элемента в дереве
  /// </summary>
  [Browsable(true)]
  [DefaultValue(true)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_150")]
  public bool UseDelay
  {
    [DebuggerStepThrough] get => this.useDelay;
    set => this.useDelay = value;
  }

  /// <summary>
  /// Если установить значение в false, компонент не будет передавать события от дерева в менеджер закладок
  /// </summary>
  [Browsable(true)]
  [DefaultValue(true)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_147")]
  public bool BridgeEnabled
  {
    [DebuggerStepThrough] get => this.bridgeEnabled;
    set => this.bridgeEnabled = value;
  }

  /// <summary>
  /// Возвращает или устанавливает величину задержки в миллисекундах обновления
  /// списка закладок после смены сфокусированного элемента в дереве.
  /// </summary>
  [Browsable(true)]
  [DefaultValue(250)]
  [Category("Behavior")]
  [CustomDescription("Attribute.Client.Core_151")]
  public int ViewsUpdateDelay
  {
    [DebuggerStepThrough] get => this.tmDelayedUpdate.Interval;
    set => this.tmDelayedUpdate.Interval = value;
  }

  /// <summary>Инициализировать поля</summary>
  private void InitializeFields()
  {
    this.navTreeView = (NavigatorTreeView) null;
    this.viewsManager = (IViewsManager) null;
    this.useDelay = true;
  }

  /// <summary>Выполнить обновление закладок</summary>
  /// <param name="delayed">true - обновление выполнить с задержкой по таймеру</param>
  private void UpdateViews(bool delayed)
  {
    if (!this.BridgeEnabled)
      return;
    this.tmDelayedUpdate.Stop();
    if (this.NavTreeView != null)
    {
      System.IServiceProvider services = this.NavTreeView.Services;
      if (services != null && services.GetService(typeof (IDisableDelayedUpdates)) is IDisableDelayedUpdates service && service.Disabled)
        delayed = false;
    }
    if (delayed)
      this.tmDelayedUpdate.Start();
    else
      this.UpdateViewsCore();
  }

  /// <summary>Отменить отложенное обновление закладок</summary>
  private void CancelUpdateViews() => this.tmDelayedUpdate.Stop();

  /// <summary>Обновить закладки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void UpdateViews(object sender, EventArgs e)
  {
    if (!this.BridgeEnabled)
      return;
    this.tmDelayedUpdate.Stop();
    if (this.Container == null)
      return;
    this.UpdateViewsCore();
  }

  /// <summary>Обновить закладки</summary>
  private void UpdateViewsCore()
  {
    if (!this.BridgeEnabled || this.viewsManager == null || this.navTreeView == null)
      return;
    bool alwaysShowFirstTab = UISettings.AlwaysShowFirstTab;
    if (this._alwaysShowFirtTab.HasValue)
      UISettings.AlwaysShowFirstTab = this._alwaysShowFirtTab.Value;
    try
    {
      this.viewsManager.UpdateViews(this.navTreeView.FocusedItems);
    }
    finally
    {
      UISettings.AlwaysShowFirstTab = alwaysShowFirstTab;
      this._alwaysShowFirtTab = new bool?();
    }
  }

  /// <summary>В дереве изменился сфокусированный узел</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TreeView_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    if (!this.BridgeEnabled)
      return;
    this._alwaysShowFirtTab = new bool?(UISettings.AlwaysShowFirstTab);
    this.UpdateViews(this.useDelay);
  }

  /// <summary>Дерево было очищено</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TreeView_ClearTree(object sender, EventArgs e)
  {
    if (!this.BridgeEnabled)
      return;
    this.tmDelayedUpdate.Stop();
    if (this.ViewsManager == null)
      return;
    this.ViewsManager.CloseViews();
  }
}
