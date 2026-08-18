
// Type: Intermech.Navigator.Controls.NavWindowBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.NotificationService;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Views;
using Intermech.Search;
using NJFLib.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Базовое окно "Навигатора" - дерево + менеджер закладок
/// </summary>
public class NavWindowBase : 
  FIltratedDocControl,
  ICommandTarget,
  IHistoryProvider,
  System.IServiceProvider,
  IFiltrationClass,
  IFiltrationRuleClass,
  IIODestination,
  ITreeListColumns,
  IEditingContextNavWindow,
  INotificationWindowService
{
  /// <summary>История навигации</summary>
  private Intermech.Navigator.Controls.History _navigationHistory;
  /// <summary>Выполнена ли активация окна</summary>
  private bool _activated;
  /// <summary>Общий контейнер сервисов</summary>
  private IServiceContainer _services;
  /// <summary>Контейнер сервисов для менеджера закладок</summary>
  private AdvancedServiceContainer _servicesPages;
  /// <summary>Менеджер команд</summary>
  private ICommandManager _commandManager;
  /// <summary>Сервис адресной строки</summary>
  private IAddressService _addressService;
  /// <summary>
  /// Сервис локальной службы уведомлений (для текущего окна)
  /// </summary>
  protected Intermech.Client.Core.NotificationService _notificationService;
  /// <summary>
  /// Сервис глобальной службы уведомлений (для всего IMClient)
  /// </summary>
  private INotificationService _mainNotificationService;
  /// <summary>
  /// Сервис службы "горячих клавиш" и связанных с ними команд
  /// </summary>
  private IHotKeysManager _hotKeysManager;
  /// <summary>Кэш графических элементов "Навигатора"</summary>
  private INavGraphicsCache _navCache;
  /// <summary>
  /// Требуется ли запретить обработку события "Фильтрация составов по типам объектов и связей".
  /// Флажок нужен для того, чтобы во время перестроения дерева можно было запретить или
  /// разрешить фильтрацию
  /// </summary>
  private bool _disableNodeFiltersEvent;
  /// <summary>
  /// Требуется ли запретить обработку события "Изменился проект".
  /// </summary>
  private bool _disableProjectsEvent;
  /// <summary>Коллекции команд по умолчанию</summary>
  private IDefaultCommands4ObjTypes _defaultCommands4ObjTypes;
  /// <summary>Диспетчер событий</summary>
  private IIODispatcher _IODispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>Восстановленное имя закладки</summary>
  private string _restoredViewName = string.Empty;
  /// <summary>Восстановленные настройки этой закладки</summary>
  private string _restoredViewSettings = string.Empty;
  /// <summary>Корневой дескриптор</summary>
  private IDescriptor _descriptor;
  /// <summary>
  /// Корневой дескриптор (может динамически меняться окном при изменении правил подбора версий)
  /// </summary>
  private IDescriptor _rootRuleDescriptor;
  private NodeIDPath _focusedPath;
  private bool _rebuildOnActivation;
  private const int DefaultTreeWidth = 200;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel pnTreeView;
  public PageViewsManager ViewsManager;
  public TreeViewsBridge ViewsBridge;
  protected LabelItem labelSpace;
  public CollapsibleSplitter spTreeView;
  public NavTreeViewWithProps TreeViewControl;

  /// <summary>Тип контрола дерева, который должен создаваться при создании данного контрола
  /// Можно назначить перед вызовом конструктора данного контрола, в этом случае дерево будет создано указанного класса,
  /// при этом данное свойство после этого обнулится</summary>
  public static System.Type OverrideTreeViewClass
  {
    [DebuggerStepThrough] get => NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass;
    [DebuggerStepThrough] set
    {
      NavigatorTreeViewWithObjectTypeFiltration.OverrideTreeViewClass = value;
    }
  }

  /// <summary>Конструктор для базового окна</summary>
  public NavWindowBase()
  {
    this.InitializeComponent();
    this.TreeViewControl.TreeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.TreeView_AfterFocusNode);
    this.TreeViewControl.TreeView.BeforeFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.TreeView_BeforeFocusNode);
    this.TreeViewControl.TreeView.BuildTree += new EventHandler(this.TreeView_BuildTree);
    this.TreeViewControl.TreeView.ClearTree += new EventHandler(this.TreeView_ClearTree);
    this.TreeViewControl.TreeView.SizeChanged += new EventHandler(this.TreeView_SizeChanged);
    this.TreeViewControl.FullWindowRefresh = new Action(this.FullWindowRefresh);
    this.InitializeServices();
    this.ViewsManager.Services = (System.IServiceProvider) this._servicesPages;
    this.TreeView.EnableRowCaching = true;
    this._navigationHistory = new Intermech.Navigator.Controls.History((IHistoryProvider) this, 8);
    this._activated = false;
    this.HideOnClose = false;
  }

  /// <summary>Прямая ссылка на дерево навигации, расположенному внутри UserControl-а</summary>
  public NavigatorTreeView TreeView
  {
    [DebuggerStepThrough] get => this.TreeViewControl.TreeView;
  }

  protected Intermech.Bars.ToolBar tbTreePanel
  {
    [DebuggerStepThrough] get => this.TreeViewControl.TreeToolbar;
  }

  public ButtonItem btClearSorting
  {
    [DebuggerStepThrough] get => this.TreeViewControl.BtnClearSorting;
  }

  /// <summary>
  /// Выполнить перестроение окна по указанному пути.
  /// Свойство возвращает null, используется только set-составляющая часть.
  /// </summary>
  public NodeIDPath RootPath
  {
    get => this._focusedPath;
    set
    {
      if (value == null || value.RootDescriptor == null)
        throw new ArgumentNullException(nameof (RootPath));
      if (this._focusedPath == value)
        return;
      this._focusedPath = value;
      this.RootDescriptor = this._focusedPath.RootDescriptor;
      if (this._activated)
        this.TreeView.TryBrowse(this._focusedPath);
      else
        this._rebuildOnActivation = true;
    }
  }

  /// <summary>Корневой дескриптор</summary>
  public IDescriptor RootDescriptor
  {
    get => this._descriptor;
    set
    {
      if (this._descriptor == value)
        return;
      this._descriptor = value;
      if (this._activated)
        this.TreeView.Build(this._descriptor);
      else
        this._rebuildOnActivation = true;
    }
  }

  /// <summary>
  /// Корневой дескриптор (может динамически меняться окном при изменении правил подбора версий)
  /// </summary>
  public virtual IDescriptor RootRuleDescriptor
  {
    [DebuggerStepThrough] get => this._rootRuleDescriptor;
    set
    {
      this._rootRuleDescriptor = value;
      this.TreeView.Build(this._rootRuleDescriptor);
    }
  }

  /// <summary>Общий контейнер сервисов</summary>
  public virtual IServiceContainer Services
  {
    [DebuggerStepThrough] get => this._services;
  }

  /// <summary>Контейнер сервисов для дерева</summary>
  public virtual AdvancedServiceContainer ServicesTree
  {
    [DebuggerStepThrough] get => this.TreeViewControl.ServicesTree;
  }

  /// <summary>Контейнер сервисов для менеджера закладок</summary>
  public virtual AdvancedServiceContainer ServicesPages
  {
    [DebuggerStepThrough] get => this._servicesPages;
  }

  /// <summary>
  /// Контейнер сервисов для менеджера закладок, расположенного под деревом "Навигатора"
  /// </summary>
  public virtual AdvancedServiceContainer ServicesTreePages
  {
    [DebuggerStepThrough] get => this.TreeViewControl.ServicesTreePages;
  }

  /// <summary>
  /// Скрывает или снова включает отображение дерева навигации в окне.
  /// </summary>
  public void ToggleTree()
  {
    if (!this.ViewsManager.Visible)
      return;
    this.pnTreeView.Visible = !this.pnTreeView.Visible;
    this.spTreeView.Visible = !this.spTreeView.Visible;
  }

  /// <summary>
  /// Скрывает или снова включает отображение дерева навигации в окне.
  /// </summary>
  /// <param name="toggle">Параметр указывает скрыть или отобразить дерево навигации.</param>
  public void ToggleTree(bool toggle)
  {
    if (!this.ViewsManager.Visible)
      return;
    this.pnTreeView.Visible = toggle;
    this.spTreeView.Visible = toggle;
  }

  public void ToggleViewsManager(bool toogle)
  {
    if (!this.pnTreeView.Visible)
      return;
    this.ViewsBridge.BridgeEnabled = !toogle;
    if (this.ViewsBridge.BridgeEnabled)
    {
      this.pnTreeView.Dock = DockStyle.Left;
      if (this.pnTreeView.Tag is int tag && tag < this.Width)
        this.pnTreeView.Width = tag;
      else
        this.pnTreeView.Width = this.Width / 3;
    }
    this.ViewsManager.Visible = this.ViewsBridge.BridgeEnabled;
    this.spTreeView.Visible = this.ViewsBridge.BridgeEnabled;
    if (this.ViewsBridge.BridgeEnabled)
      return;
    this.pnTreeView.Tag = (object) this.pnTreeView.Width;
    this.pnTreeView.Dock = DockStyle.Fill;
  }

  /// <summary>Форма активирована</summary>
  public override void Activated()
  {
    base.Activated();
    ICurrentUserAndRole service1 = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    if (service1.LockEditingContextID)
      service1.LockEditingContextID = false;
    if (this._addressService != null)
      this._addressService.Enabled = true;
    if (this._activated)
      return;
    try
    {
      this.EnableNotifications(this.NotificationService, true);
      INotificationServiceStatesHolder service2 = this._services != null ? this._services.GetService(typeof (INotificationServiceStatesHolder)) as INotificationServiceStatesHolder : (INotificationServiceStatesHolder) null;
      if (service2 != null)
        service2.States &= ~NotificationServiceStates.InactiveForm;
      ICurrentNavWindow service3 = (ICurrentNavWindow) ServicesManager.GetService(typeof (ICurrentNavWindow));
      if (service3 != null)
      {
        service3.NavWindow = (object) this;
        service3.TreeView = (object) this.TreeView;
        service3.ViewsManagers = (object) this.ViewsManager;
      }
      this.SetTreeViewColumns();
      if (this._rebuildOnActivation)
      {
        this.TreeView.BuildWithPath(this._descriptor, this._focusedPath);
        this._rebuildOnActivation = false;
      }
      this.UpdateAddress(this.TreeView.FocusedAddress);
    }
    finally
    {
      this._activated = true;
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service4)
        service4.FireEvent((object) this, new NotificationEventArgs("NavigatorWindowActivated"));
    }
  }

  protected void SetTreeViewColumns()
  {
    INavigatorColumnsService navigatorColumnsService = ServiceLocator.Get<INavigatorColumnsService>();
    if (this._descriptor == null)
      return;
    INodeID recordNodeId = this._descriptor.GetRecordNodeID();
    if (recordNodeId == null)
      return;
    NavigatorColumns navigatorColumns = navigatorColumnsService.GetNavigatorColumns(recordNodeId.CategoryID, recordNodeId.TypeID, "TreeView", true);
    if (navigatorColumns == null)
      return;
    this.TreeView.SetColumns(navigatorColumns.Columns);
    if (!(navigatorColumns.Cookie is int))
      return;
    this.pnTreeView.Width = (int) navigatorColumns.Cookie;
  }

  /// <summary>Деактивация формы</summary>
  public override void Deactivated()
  {
    base.Deactivated();
    if (this._addressService != null)
      this._addressService.Enabled = false;
    if (!this._activated)
      return;
    try
    {
      this.SaveColumnsAndTreeWitdth();
      this.ClearAddress();
      ICurrentNavWindow service1 = (ICurrentNavWindow) ServicesManager.GetService(typeof (ICurrentNavWindow));
      if (service1 != null)
      {
        service1.NavWindow = (object) null;
        service1.TreeView = (object) null;
        service1.ViewsManagers = (object) null;
      }
      this.EnableNotifications(this.NotificationService, this.IsOpen || UISettings.AutoupdateNonActiveWindows);
      INotificationServiceStatesHolder service2 = this._services != null ? this._services.GetService(typeof (INotificationServiceStatesHolder)) as INotificationServiceStatesHolder : (INotificationServiceStatesHolder) null;
      if (service2 != null)
      {
        if (!UISettings.AutoupdateNonActiveWindows)
          service2.States |= NotificationServiceStates.InactiveForm;
        else
          service2.States &= ~NotificationServiceStates.InactiveForm;
      }
      if (!(this.ViewsManager.ActiveViewPage?.View is IAdvancedView view))
        return;
      view.HideHint();
    }
    finally
    {
      this._activated = false;
    }
  }

  private void SaveColumnsAndTreeWitdth()
  {
    try
    {
      INavigatorColumnsService navigatorColumnsService = ServiceLocator.Get<INavigatorColumnsService>();
      INodeID rootNodeId = this.TreeView.RootNodeID;
      if (rootNodeId == null)
        return;
      NavigatorColumns columns = navigatorColumnsService.GetNavigatorColumns(rootNodeId.CategoryID, rootNodeId.TypeID, "TreeView", true) ?? navigatorColumnsService.CreateNavigatorColumns(rootNodeId.CategoryID, rootNodeId.TypeID, "TreeView");
      columns.Columns = this.TreeView.ReflectTreeColumsChanges();
      if (this.pnTreeView.Dock != DockStyle.Fill)
        columns.Cookie = (object) this.pnTreeView.Width;
      navigatorColumnsService.CreateNavigatorColumns(columns);
    }
    catch
    {
    }
  }

  /// <summary>
  /// Возвращает строку состояния окна, которая может быть использована для восстановления окна в
  /// следующем сеансе работы приложения.
  /// </summary>
  /// <returns>Строка состояния окна навигатора.</returns>
  protected override string GetPersistString()
  {
    try
    {
      XmlDocument state = this.GetState();
      using (TextWriter w1 = (TextWriter) new StringWriter())
      {
        XmlWriter w2 = (XmlWriter) new XmlTextWriter(w1);
        state.WriteTo(w2);
        w2.Flush();
        w2.Close();
        return w1.ToString();
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_572"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (string) null;
    }
  }

  /// <summary>Возвращает состояние окна в виде XML документа.</summary>
  /// <returns>Состояние окна в виде XML</returns>
  protected virtual XmlDocument GetState()
  {
    XmlDocument xmlDoc = new XmlDocument();
    XmlNode element = (XmlNode) xmlDoc.CreateElement("Settings");
    element.AppendChild(this.GetPropertiesNode(xmlDoc));
    element.AppendChild(this.TreeView.GetSupportedColumnsNode(xmlDoc));
    element.AppendChild(this.GetColumnsNode(xmlDoc));
    element.AppendChild(this.GetPathNode(xmlDoc));
    element.AppendChild(this.GetActiveViewName(xmlDoc));
    element.AppendChild(this.GetActiveViewSettings(xmlDoc));
    xmlDoc.AppendChild((XmlNode) xmlDoc.CreateXmlDeclaration("1.0", (string) null, (string) null));
    xmlDoc.AppendChild(element);
    return xmlDoc;
  }

  /// <summary>Получить узел с настройками формы</summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Узел с настройками формы</returns>
  protected virtual XmlNode GetPropertiesNode(XmlDocument xmlDoc)
  {
    XmlElement element1 = xmlDoc.CreateElement("Properties");
    XmlNode element2 = (XmlNode) xmlDoc.CreateElement("TreeWidth");
    element2.AppendChild((XmlNode) xmlDoc.CreateTextNode(XmlConvert.ToString(this.pnTreeView.Width)));
    element1.AppendChild(element2);
    XmlNode element3 = (XmlNode) xmlDoc.CreateElement("ViewsUseDelay");
    element3.AppendChild((XmlNode) xmlDoc.CreateTextNode(XmlConvert.ToString(this.ViewsBridge.UseDelay)));
    element1.AppendChild(element3);
    XmlNode element4 = (XmlNode) xmlDoc.CreateElement("ViewsUpdateDelay");
    element4.AppendChild((XmlNode) xmlDoc.CreateTextNode(XmlConvert.ToString(this.ViewsBridge.ViewsUpdateDelay)));
    element1.AppendChild(element4);
    XmlNode element5 = (XmlNode) xmlDoc.CreateElement("SelectedFilterVersionID");
    element5.AppendChild((XmlNode) xmlDoc.CreateTextNode(this.TreeViewControl.SelectedFilterVersionID.ToString()));
    element1.AppendChild(element5);
    XmlNode element6 = (XmlNode) xmlDoc.CreateElement("ManualSorting");
    element6.AppendChild((XmlNode) xmlDoc.CreateTextNode(this.btClearSorting.Checked ? "1" : "0"));
    element1.AppendChild(element6);
    XmlNode element7 = (XmlNode) xmlDoc.CreateElement("FiltrationOwnerID");
    element7.AppendChild((XmlNode) xmlDoc.CreateTextNode(this.Get_FiltrationOwnerID()));
    element1.AppendChild(element7);
    return (XmlNode) element1;
  }

  /// <summary>Получить узел с колонками</summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Узел с колонками</returns>
  protected virtual XmlNode GetColumnsNode(XmlDocument xmlDoc)
  {
    XmlNode element = (XmlNode) xmlDoc.CreateElement("Columns");
    this.TreeView.ReflectTreeColumsChanges().SaveData(element);
    return element;
  }

  /// <summary>
  /// Получить узел с возможными в дереве данного окна колонками
  /// </summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Узел с колонками</returns>
  protected virtual XmlNode GetSupportedColumnsNode(XmlDocument xmlDoc)
  {
    XmlNode element = (XmlNode) xmlDoc.CreateElement("SupportedColumnsList");
    this.TreeView.SupportedColumns.SaveData(element);
    return element;
  }

  /// <summary>Сохранить список возможных колонок</summary>
  /// <param name="columnsNode">Узел с колонками</param>
  /// <param name="columns">Коллекция возможных колонок</param>
  protected virtual void PopulateSupportedColumnsNode(
    XmlNode columnsNode,
    NodeColumnCollection supportedColumns)
  {
    supportedColumns.SaveData(columnsNode);
  }

  /// <summary>Сохранить в XML путь к текущему узлу</summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Путь к текущему узлу</returns>
  protected virtual XmlNode GetPathNode(XmlDocument xmlDoc)
  {
    XmlNode element1 = (XmlNode) xmlDoc.CreateElement("Path");
    using (MemoryStream memoryStream = new MemoryStream())
    {
      NodeIDPath focusedPath = this.TreeView.FocusedPath;
      if (focusedPath != null && this._descriptor != null && !this._descriptor.Equals((object) this._rootRuleDescriptor))
      {
        focusedPath.RootDescriptor = this._descriptor;
        if (this._rootRuleDescriptor is VirtualObjectDescriptor)
          focusedPath.Clear();
      }
      PersistentState[] persistentStateArray = Intermech.Navigator.Utils.SerializePath(focusedPath, (System.IServiceProvider) this._services);
      if (persistentStateArray == null || persistentStateArray.Length == 0)
        return element1;
      IStateFormatter stateFormatter = (IStateFormatter) new BinaryStateFormatter();
      for (int index = 0; index < persistentStateArray.Length; ++index)
      {
        memoryStream.SetLength(0L);
        stateFormatter.Serialize((Stream) memoryStream, persistentStateArray[index]);
        XmlNode element2 = (XmlNode) xmlDoc.CreateElement("Item");
        element2.AppendChild((XmlNode) xmlDoc.CreateTextNode(Convert.ToBase64String(memoryStream.ToArray())));
        element1.AppendChild(element2);
      }
    }
    return element1;
  }

  /// <summary>Вернуть название активной странички</summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Название активной странички</returns>
  protected virtual XmlNode GetActiveViewName(XmlDocument xmlDoc)
  {
    XmlNode element = (XmlNode) xmlDoc.CreateElement("ActiveViewName");
    IViewPage activeViewPage = this.ViewsManager.ActiveViewPage;
    if (activeViewPage != null)
      element.InnerText = activeViewPage.Name;
    return element;
  }

  /// <summary>Вернуть состояние активной странички</summary>
  /// <param name="xmlDoc">Документ XML</param>
  /// <returns>Состояние активной странички</returns>
  protected virtual XmlNode GetActiveViewSettings(XmlDocument xmlDoc)
  {
    XmlNode element = (XmlNode) xmlDoc.CreateElement("ActiveViewNameSettings");
    IViewPage activeViewPage = this.ViewsManager.ActiveViewPage;
    IAdvancedView view = activeViewPage != null ? activeViewPage.View as IAdvancedView : (IAdvancedView) null;
    if (view != null)
      element.InnerText = view.GetState().InnerXml;
    return element;
  }

  /// <summary>Восстановить состояние окна</summary>
  /// <param name="xmlDoc">Документ XML, в котором хранится состояние окна</param>
  public virtual void RestoreState(XmlDocument xmlDoc)
  {
    XmlNode settingsNode = xmlDoc.SelectSingleNode("/Settings");
    this.RestoreProperties(settingsNode);
    this.TreeView.RestoreFromSupportedColumnsNode(settingsNode);
    this.RestoreColumns(settingsNode);
    this.RestoreActiveViewName(settingsNode);
    this.RestoreActiveViewSettings(settingsNode);
    this.RestorePath(settingsNode);
    if (this._services.GetService(typeof (INotificationServiceStatesHolder)) is INotificationServiceStatesHolder service1)
    {
      if (!UISettings.AutoupdateNonActiveWindows)
        service1.States |= NotificationServiceStates.InactiveForm;
      else
        service1.States &= ~NotificationServiceStates.InactiveForm;
    }
    if (this.RootDescriptor == null)
    {
      this.HideOnClose = false;
      this.Close();
    }
    else
    {
      bool flag1 = false;
      if (ServicesManager.GetService(typeof (IEnableTreeMultiSelectService)) is IEnableTreeMultiSelectService service2)
        flag1 = service2.EnableTreeMultiSelect(this.RootDescriptor, (System.IServiceProvider) this.Services);
      if (flag1)
        this.TreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
      bool flag2 = true;
      if (ServicesManager.GetService(typeof (IEnableTreeColumnsSortingService)) is IEnableTreeColumnsSortingService service3)
        flag2 = service3.EnableTreeColumnsSorting(this.RootDescriptor, (System.IServiceProvider) this.Services);
      this.TreeView.DisableColumnsSorting = !flag2;
      this.btClearSorting.Enabled = flag2;
      if (flag2)
        return;
      this.btClearSorting.Checked = true;
    }
  }

  /// <summary>Восстановим свойства окна</summary>
  /// <param name="settingsNode">Узел с настройками</param>
  protected virtual void RestoreProperties(XmlNode settingsNode)
  {
    XmlNode xmlNode1 = settingsNode.SelectSingleNode("Properties/ViewsUseDelay");
    if (xmlNode1 != null)
      this.ViewsBridge.UseDelay = XmlConvert.ToBoolean(xmlNode1.InnerText);
    XmlNode xmlNode2 = settingsNode.SelectSingleNode("Properties/ViewsUpdateDelay");
    if (xmlNode2 != null)
      this.ViewsBridge.ViewsUpdateDelay = XmlConvert.ToInt32(xmlNode2.InnerText);
    XmlNode xmlNode3 = settingsNode.SelectSingleNode("Properties/ManualSorting");
    if (xmlNode3 != null && xmlNode3.InnerText == "1")
      this.TreeViewControl.ManualSorting = true;
    else
      this.TreeViewControl.ManualSorting = false;
    XmlNode xmlNode4 = settingsNode.SelectSingleNode("Properties/SelectedFilterVersionID");
    if (xmlNode4 != null)
    {
      try
      {
        this.TreeViewControl.SelectedFilterVersionID = Convert.ToInt64(xmlNode4.InnerText);
      }
      catch (Exception ex)
      {
      }
    }
    XmlNode xmlNode5 = settingsNode.SelectSingleNode("Properties/FiltrationOwnerID");
    if (xmlNode5 != null)
    {
      string innerText = xmlNode5.InnerText;
      if (this.filtrationOwnerID.Length > 0 && this.filtrationOwnerID != innerText)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, this.filtrationOwnerID, (FiltrationSettings) null);
      }
      this.filtrationOwnerID = innerText;
      this.filtrationsApplyed = false;
    }
    XmlNode xmlNode6 = settingsNode.SelectSingleNode("Properties/TreeWidth");
    if (xmlNode6 == null)
      return;
    this.pnTreeView.Width = XmlConvert.ToInt32(xmlNode6.InnerText);
  }

  /// <summary>Восстановим возможные колонки</summary>
  /// <param name="settingsNode">Узел с настройками</param>
  protected virtual void RestoreSupportedColumns(XmlNode settingsNode)
  {
    XmlNode columnsNode = settingsNode.SelectSingleNode("SupportedColumnsList");
    if (columnsNode == null)
      return;
    this.TreeView.SupportedColumns = this.RestoreColumnCollection(columnsNode);
  }

  /// <summary>Восстановим колонки</summary>
  /// <param name="settingsNode">Узел с настройками</param>
  protected virtual void RestoreColumns(XmlNode settingsNode)
  {
    XmlNode columnsNode = settingsNode.SelectSingleNode("Columns");
    this.TreeView.SetColumns(columnsNode != null ? this.RestoreColumnCollection(columnsNode) : Intermech.Navigator.Utils.NavigatorColumns(NodeColumnSortOrder.None));
    if (!this.TreeViewControl.ManualSorting && !this.TreeView.DisableColumnsSorting)
      return;
    this.btClearSorting.Checked = true;
    this.DoCancelSort((object) null, (EventArgs) null);
  }

  /// <summary>Восстановить коллекцию колонок</summary>
  /// <param name="columnsNode">Узел с колонками</param>
  /// <returns>Коллекция колонок</returns>
  protected virtual NodeColumnCollection RestoreColumnCollection(XmlNode columnsNode)
  {
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    columnCollection.LoadData(columnsNode);
    return columnCollection;
  }

  /// <summary>
  /// Попытаться восстановить имя последней активной закладки
  /// </summary>
  /// <param name="settingsNode">Узел с настройками окна</param>
  protected virtual void RestoreActiveViewName(XmlNode settingsNode)
  {
    this._restoredViewName = string.Empty;
    XmlNode xmlNode = settingsNode.SelectSingleNode("ActiveViewName");
    if (xmlNode == null)
      return;
    this._restoredViewName = xmlNode.InnerText;
  }

  /// <summary>
  /// Попытаться восстановить настройки последней активной закладки
  /// </summary>
  /// <param name="settingsNode">Узел с настройками окна</param>
  protected virtual void RestoreActiveViewSettings(XmlNode settingsNode)
  {
    this._restoredViewSettings = string.Empty;
    XmlNode xmlNode = settingsNode.SelectSingleNode("ActiveViewNameSettings");
    if (xmlNode == null)
      return;
    this._restoredViewSettings = xmlNode.InnerText;
  }

  /// <summary>Восстановить путь, построить по нему дерево</summary>
  /// <param name="settingsNode">Узел с настройками</param>
  protected virtual void RestorePath(XmlNode settingsNode)
  {
    XmlNode xmlNode = settingsNode.SelectSingleNode("Path");
    PersistentState[] persistPath = new PersistentState[xmlNode.ChildNodes.Count];
    IStateFormatter stateFormatter = (IStateFormatter) new BinaryStateFormatter();
    for (int i = 0; i < persistPath.Length; ++i)
    {
      using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(xmlNode.ChildNodes[i].InnerText)))
      {
        PersistentState persistentState = stateFormatter.Deserialize((Stream) memoryStream);
        persistPath[i] = persistentState;
      }
    }
    this.filtrationService.FiltrationServiceOwnerID = this.Get_FiltrationOwnerID();
    NodeIDPath path = Intermech.Navigator.Utils.DeserializePath(persistPath, (System.IServiceProvider) this._services);
    if (path == null || path.RootDescriptor == null)
      return;
    ServicesManager.GetService<INotificationService>().FireEvent((object) null, (NotificationEventArgs) new NavigatorWindowOpeningEventArgs(this, (IDescriptor) null, path, (System.IServiceProvider) this.Services));
    bool useDelay = this.ViewsBridge.UseDelay;
    string filtrationServiceOwnerId = this.filtrationService.FiltrationServiceOwnerID;
    try
    {
      this.ViewsBridge.UseDelay = false;
      this.ViewsBridge.BridgeEnabled = false;
      this.TreeView.Build(path);
      this._descriptor = this.TreeView.RootDescriptor;
      this.ViewsManager.UpdateViews(this.TreeView.SelectedItems, true);
      if (this.TreeViewControl.ViewsTree.Visible)
        this.TreeViewControl.ViewsTree.UpdateViews(this.TreeView.SelectedItems, true);
      if (this.TreeView.FocusedNode != null)
        this.TreeView.FocusedNode.Expanded = true;
      if (!(this._restoredViewName != string.Empty))
        return;
      for (int index = 0; index < this.ViewsManager.ViewPages.Count; ++index)
      {
        if (this.ViewsManager.ViewPages[index].Name == this._restoredViewName)
        {
          this.ViewsManager.ActiveViewPage = this.ViewsManager.ViewPages[index];
          IAdvancedView view = this.ViewsManager.ActiveViewPage != null ? this.ViewsManager.ActiveViewPage.View as IAdvancedView : (IAdvancedView) null;
          if (!string.IsNullOrEmpty(this._restoredViewSettings) && view != null)
          {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InnerXml = this._restoredViewSettings;
            view.RestoreState(xmlDoc);
            break;
          }
          break;
        }
      }
      this._restoredViewName = string.Empty;
    }
    finally
    {
      this.ViewsBridge.BridgeEnabled = true;
      try
      {
        this.TryToActivateTree();
      }
      catch
      {
        this.ViewsBridge.UseDelay = useDelay;
        throw;
      }
      this.ViewsBridge.UseDelay = useDelay;
    }
  }

  /// <summary>
  /// Метод пробует перевести фокус ввода на дерево Навигатора, а затем обновить состояние
  /// команд ICommandManager для его сфокусированного узла
  /// </summary>
  public virtual void TryToActivateTree()
  {
    this.TreeView.Focus();
    this.TreeView.UpdateCommandManagerItems(true);
  }

  protected virtual ICommandManager InitializeCommandManager()
  {
    return (ICommandManager) ServicesManager.GetService(typeof (ICommandManager));
  }

  protected virtual void DisposeCommandManager(ICommandManager commandManager)
  {
  }

  private ICommandManager CommandManager => this._commandManager;

  protected virtual IAddressService InitializeAddressService()
  {
    return (IAddressService) ServicesManager.GetService(typeof (IAddressService));
  }

  protected virtual void DisposeAddressService(IAddressService addressService)
  {
    this._addressService = (IAddressService) null;
  }

  private IAddressService AddressService => this._addressService;

  private void ClearAddress()
  {
    if (this.AddressService == null)
      return;
    this.AddressService.Text = string.Empty;
  }

  private void BrowseAddress()
  {
    if (this.AddressService == null || string.IsNullOrEmpty(this.AddressService.Text))
      return;
    this.TreeView.TryBrowse(this.AddressService.Text);
  }

  private void UpdateAddress(string address)
  {
    if (this.AddressService == null)
      return;
    this.AddressService.Text = address;
  }

  protected virtual Intermech.Client.Core.NotificationService InitializeNotificationService()
  {
    SwitchedNotificationService notificationService = new SwitchedNotificationService();
    notificationService.Parent = (Intermech.Client.Core.NotificationService) ServicesManager.GetService(typeof (INotificationService));
    return (Intermech.Client.Core.NotificationService) notificationService;
  }

  protected virtual void DisposeNotificationService(INotificationService notificationService)
  {
    ((IDisposable) notificationService)?.Dispose();
  }

  protected virtual void EnableNotifications(INotificationService notificationService, bool enabled)
  {
    SwitchedNotificationService notificationService1 = (SwitchedNotificationService) notificationService;
    if (notificationService1 == null)
      return;
    notificationService1.Enabled = enabled;
  }

  private INotificationService NotificationService
  {
    get => (INotificationService) this._notificationService;
  }

  /// <summary>
  /// Виртуальный метод, который надо перекрывать. Вызывается сервисом тулбара "Фильтрация состава" тогда,
  /// когда происходит смена настроек фильтрации состава.
  /// </summary>
  /// <param name="NewFiltration">Новые настройки фильтрации состава</param>
  /// <param name="FiltrationValid">Являются ли эти настройки валидными</param>
  public override void FiltrationChanged(IFiltrationSettings NewFiltration, bool FiltrationValid)
  {
    if (NewFiltration != null && NewFiltration.OwnerID != this.Get_FiltrationOwnerID())
      return;
    bool flag1 = this.ViewsManager.Services.GetService(typeof (IDisableDelayedUpdates)) is IDisableDelayedUpdates service && service.Disabled;
    try
    {
      this.ViewsBridge.BridgeEnabled = false;
      NodeIDPath focusedPath = this.TreeView.FocusedPath;
      IViewPage activeViewPage = this.ViewsManager.ActiveViewPage;
      string str = activeViewPage != null ? activeViewPage.Name : string.Empty;
      iFocusAndSelection focusAndSelection = activeViewPage?.View is IAdvancedView view1 ? view1.FullFocusAndSelection : (iFocusAndSelection) null;
      if (focusAndSelection != null)
        focusAndSelection.ActivePage = str;
      bool flag2 = view1 != null && view1.DisableAutoselectFirstRow;
      if (service != null)
        service.Disabled = true;
      if (view1 != null)
        view1.DisableAutoselectFirstRow = true;
      bool flag3 = this.TreeView.FocusedNode != null && this.TreeView.FocusedNode.Expanded;
      if (focusedPath != null && focusedPath.RootDescriptor != null)
      {
        if (this.filtrationService.RuleClass != null && focusedPath.RootDescriptor != null && focusedPath.RootDescriptor is Intermech.Navigator.DBObjects.Descriptor)
          (focusedPath.RootDescriptor as Intermech.Navigator.DBObjects.Descriptor).CorrectState();
        this.TreeView.BuildWithPath(focusedPath.RootDescriptor, focusedPath);
      }
      else
      {
        if (this.filtrationService.RuleClass != null && this.RootDescriptor != null && this.RootDescriptor is Intermech.Navigator.DBObjects.Descriptor)
          (this.RootDescriptor as Intermech.Navigator.DBObjects.Descriptor).CorrectState();
        this.TreeView.BuildWithPath(this.RootDescriptor, focusedPath);
      }
      this.ViewsManager.UpdateViews(this.TreeView.SelectedItems, true);
      if (this.TreeViewControl.ViewsTree.Visible)
        this.TreeViewControl.ViewsTree.UpdateViews(this.TreeView.SelectedItems, true);
      if (focusAndSelection != null && focusAndSelection.ActivePage != string.Empty)
      {
        for (int index = 0; index < this.ViewsManager.ViewPages.Count; ++index)
        {
          if (this.ViewsManager.ViewPages[index].Name == focusAndSelection.ActivePage)
          {
            this.ViewsManager.ActiveViewPage = this.ViewsManager.ViewPages[index];
            if (this.ViewsManager.ActiveViewPage.View is IAdvancedView view2)
            {
              view2.DisableAutoselectFirstRow = flag2;
              view2.FullFocusAndSelection = focusAndSelection;
              break;
            }
            break;
          }
        }
      }
      if (this.TreeView.FocusedNode == null || this.TreeView.FocusedNode.Expanded == flag3)
        return;
      this.TreeView.FocusedNode.Expanded = flag3;
    }
    finally
    {
      this.ViewsBridge.BridgeEnabled = true;
      if (service != null)
        service.Disabled = flag1;
    }
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

  /// <summary>Список поддерживаемых обработчиком событий</summary>
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyDown | IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  /// <summary>
  /// Переместиться в дереве (в зависимости от исходных данных в событии)
  /// </summary>
  /// <param name="Event">Событие</param>
  private bool BrowseToPath(IIOEvent Event)
  {
    if (!(Event.Tag is NodeIDPath tag))
      return false;
    bool flag = this.ViewsManager.Services.GetService(typeof (IDisableDelayedUpdates)) is IDisableDelayedUpdates service && service.Disabled;
    try
    {
      this.ViewsBridge.BridgeEnabled = false;
      if (service != null)
        service.Disabled = true;
      IViewPage activeViewPage = this.ViewsManager.ActiveViewPage;
      IFoldersView view = activeViewPage != null ? activeViewPage.View as IFoldersView : (IFoldersView) null;
      string str = activeViewPage != null ? activeViewPage.Name : string.Empty;
      if (!this.TreeView.TryBrowse(tag))
        return false;
      this.ViewsManager.UpdateViews(this.TreeView.SelectedItems, true);
      if (this.TreeViewControl.ViewsTree.Visible)
        this.TreeViewControl.ViewsTree.UpdateViews(this.TreeView.SelectedItems, true);
      this.ViewsManager.Focus();
      if (str != string.Empty && view == null)
      {
        for (int index = 0; index < this.ViewsManager.ViewPages.Count; ++index)
        {
          if (this.ViewsManager.ViewPages[index].Name == str)
          {
            this.ViewsManager.ActiveViewPage = this.ViewsManager.ViewPages[index];
            break;
          }
        }
      }
      if (this.ViewsManager.ActiveViewPage != null)
        this.ViewsManager.ActiveViewPage.Control.Focus();
      return true;
    }
    finally
    {
      this.ViewsBridge.BridgeEnabled = true;
      if (service != null)
        service.Disabled = flag;
    }
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
    this.TreeView.TryBrowse(nodeIDPath);
    this.ViewsManager.Focus();
    if (this.ViewsManager.ActiveViewPage != null)
      this.ViewsManager.ActiveViewPage.Control.Focus();
    return true;
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
      List<IHotKeysCommand> commands = this._hotKeysManager[((KeyEventArgs) Event.EventData).KeyCode | ((KeyEventArgs) Event.EventData).Modifiers];
      if (commands != null && commands.Count > 0)
      {
        ((KeyEventArgs) Event.EventData).Handled = true;
        return this.ExecuteMenuCommand(commands, Event);
      }
    }
    if (this.ViewsManager.ActiveViewPage != null)
    {
      if ((Event.EventType == IOEventType.evKeyUp || Event.EventType == IOEventType.evKeyDown) && this._hotKeysManager != null && ((KeyEventArgs) Event.EventData).KeyCode != Keys.Return)
      {
        KeyEventArgs eventData = (KeyEventArgs) Event.EventData;
        if (Event.EventType == IOEventType.evKeyDown && eventData.KeyCode == eventData.KeyData && eventData.Modifiers == Keys.None || Event.EventType == IOEventType.evKeyUp && eventData.KeyCode != eventData.KeyData && eventData.Modifiers != Keys.None)
          return false;
        List<IHotKeysCommand> commands = this._hotKeysManager[((KeyEventArgs) Event.EventData).KeyCode | ((KeyEventArgs) Event.EventData).Modifiers];
        if (commands != null && commands.Count > 0)
        {
          ((KeyEventArgs) Event.EventData).Handled = true;
          return this.ExecuteMenuCommand(commands, Event);
        }
      }
      if ((Event.EventType == IOEventType.evMouseDoubleClick || ((KeyEventArgs) Event.EventData).KeyCode == Keys.Return) && !this.TreeView.Focused)
      {
        if (Event.Source.SelectedItems == null || Event.Source.SelectedItems.Count <= 0 || !(Event.Source.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
          return false;
        IDefaultCommand commands4ObjType = this._defaultCommands4ObjTypes[itemData.ObjectType, true];
        if (commands4ObjType != null && commands4ObjType.ObjectTypeID == -1 && this.BrowseToPath(Event))
          return true;
        if (commands4ObjType == null)
          return false;
        return commands4ObjType.CommandHandler == DefaultCommandHandler.ContectMenu ? this.ExecuteMenuCommand(commands4ObjType, Event) : this.BrowseToPath(Event);
      }
      if (Event.EventType == IOEventType.evKeyUp && ((KeyEventArgs) Event.EventData).KeyCode == Keys.Back || ((KeyEventArgs) Event.EventData).KeyCode == Keys.BrowserBack)
      {
        this.BrowseToPrevPath(Event);
        return false;
      }
    }
    return false;
  }

  /// <summary>Список видимых колонок (их текущее состояние)</summary>
  public NodeColumnCollection TreeListColumns
  {
    get => this.TreeViewControl.TreeListColumns;
    set => this.TreeViewControl.TreeListColumns = value;
  }

  /// <summary>
  /// Извещает подписчика/подписчиков о произошедшем событии.
  /// </summary>
  /// <param name="sender">Объект, рассылающий событие обновления.</param>
  /// <param name="e">Данные для события обновления.</param>
  /// <returns></returns>
  public bool FireEvent(object sender, NotificationEventArgs e)
  {
    if (this._notificationService == null)
      return false;
    SwitchedNotificationService notificationService = this._notificationService as SwitchedNotificationService;
    lock (NotificationEventNames.CriticalEventNames)
    {
      bool flag1 = NotificationEventNames.CriticalEventNames.Contains(e.EventName);
      if (!flag1)
        NotificationEventNames.CriticalEventNames.Add(e.EventName);
      bool flag2 = notificationService != null && notificationService.Enabled;
      Intermech.Client.Core.NotificationService parent = this._notificationService.Parent;
      try
      {
        this._notificationService.Parent = (Intermech.Client.Core.NotificationService) null;
        if (notificationService != null)
          notificationService.Enabled = true;
        this._notificationService.FireEvent(sender, e, false);
      }
      finally
      {
        this._notificationService.Parent = parent;
        if (notificationService != null)
          notificationService.Enabled = flag2;
        if (!flag1)
          NotificationEventNames.CriticalEventNames.Remove(e.EventName);
      }
    }
    return true;
  }

  public bool Execute(ICommandState commandState)
  {
    switch (commandState.CommandName)
    {
      case "ToggleTree":
        this.ToggleTree();
        return true;
      case "GotoAddress":
        this.BrowseAddress();
        return false;
      default:
        if (this.TreeViewControl.Focused)
          return this.TreeViewControl.Execute(commandState);
        if (this.TreeViewControl.TreeView.Focused)
          return this.TreeViewControl.Execute(commandState);
        if (this.TreeViewControl.ViewsTree.Focused)
          return this.TreeViewControl.Execute(commandState);
        return this.ViewsManager != null && this.ViewsManager.Execute(commandState);
    }
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (this.IsDisposed)
      return false;
    switch (commandState.CommandName)
    {
      case "ToggleTree":
        commandState.Enabled = true;
        return true;
      case "GotoAddress":
        commandState.Enabled = true;
        return true;
      default:
        if (this.TreeView.TreeFocused)
          return this.TreeViewControl.QueryStatus(commandState);
        if (this.TreeViewControl.Focused)
          return this.TreeViewControl.QueryStatus(commandState);
        if (this.TreeViewControl.ViewsTree.Focused)
          return this.TreeViewControl.ViewsTree.QueryStatus(commandState);
        return this.ViewsManager != null && this.ViewsManager.QueryStatus(commandState);
    }
  }

  /// <summary>
  /// Полностью перечитать содержимое окна, восстановив все активные закладки и выделенные элементы
  /// </summary>
  public virtual void FullWindowRefresh()
  {
    bool flag1 = this.ViewsManager.Services.GetService(typeof (IDisableDelayedUpdates)) is IDisableDelayedUpdates service && service.Disabled;
    try
    {
      this.ViewsBridge.BridgeEnabled = false;
      if (service != null)
        service.Disabled = true;
      IViewPage activeViewPage = this.ViewsManager.ActiveViewPage;
      string str = activeViewPage != null ? activeViewPage.Name : string.Empty;
      iFocusAndSelection focusAndSelection = activeViewPage?.View is IAdvancedView view1 ? view1.FullFocusAndSelection : (iFocusAndSelection) null;
      if (focusAndSelection != null)
        focusAndSelection.ActivePage = str;
      bool flag2 = view1 != null && view1.DisableAutoselectFirstRow;
      if (view1 != null)
        view1.DisableAutoselectFirstRow = true;
      bool flag3 = this.TreeViewControl.FullWindowRefreshImplementation();
      bool flag4 = false;
      if (focusAndSelection != null && focusAndSelection.ActivePage != string.Empty)
      {
        for (int index = 0; index < this.ViewsManager.ViewPages.Count; ++index)
        {
          if (this.ViewsManager.ViewPages[index].Name == focusAndSelection.ActivePage)
          {
            this.ViewsManager.ActiveViewPage = this.ViewsManager.ViewPages[index];
            if (this.ViewsManager.ActiveViewPage.View is IAdvancedView view2)
            {
              view2.DisableAutoselectFirstRow = flag2;
              view2.Reload(focusAndSelection);
              flag4 = true;
              break;
            }
            break;
          }
        }
      }
      if (!(!flag4 | flag3))
        return;
      this.ViewsManager.UpdateViews(this.TreeView.SelectedItems, true);
    }
    finally
    {
      this.ViewsBridge.BridgeEnabled = true;
      if (service != null)
        service.Disabled = flag1;
    }
  }

  /// <summary>Обработать событие от службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  public void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName == "ObjectsRemoved" && this.TreeView.RootHandler != null)
    {
      IDBObjectID data = this.TreeView.RootHandler.GetData(this.TreeView.RootNodeID, typeof (IDBObjectID)) as IDBObjectID;
      DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
      if (data != null && objectsEventArgs != null && objectsEventArgs.ObjectIDs != null && objectsEventArgs.ObjectIDs.IndexOf(data.Value) >= 0)
        this.Close();
    }
    if (!(e.EventName == "ObjectsChanged"))
      return;
    IDBObjectID itemData = this.TreeView.SelectedItems == null || this.TreeView.SelectedItems.Count <= 0 ? (IDBObjectID) null : this.TreeView.SelectedItems.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID;
    DBObjectsEventArgs objectsEventArgs1 = e as DBObjectsEventArgs;
    if (itemData == null || objectsEventArgs1 == null || objectsEventArgs1.ObjectIDs == null)
      return;
    objectsEventArgs1.ObjectIDs.IndexOf(itemData.Value);
  }

  /// <summary>Обработчик события "Закрывается IPS"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  public void ApplicationClosingEventFired(object sender, NotificationEventArgs e)
  {
    if (e.EventName != "ApplicationClosing" || !(e is ApplicationClosingEventArgs closingEventArgs))
      return;
    if (!this.ViewsManager.CanClose((object) this))
      closingEventArgs.Cancel = true;
    this.SaveColumnsAndTreeWitdth();
  }

  private void ApplicationClosedEventFired(object sender, NotificationEventArgs e)
  {
    this.ViewsManager.SaveChanges(false);
  }

  /// <summary>Обработчик события "Изменился текущий проект"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  public void ProjectChanged(object sender, NotificationEventArgs e)
  {
    if (e.EventName != nameof (ProjectChanged))
      return;
    if (this._disableProjectsEvent)
      return;
    try
    {
      this._disableProjectsEvent = true;
      this.FullWindowRefresh();
    }
    finally
    {
      this._disableProjectsEvent = false;
    }
  }

  /// <summary>
  /// Обработчик события "Изменилась фильтрация по типам объектов и связей"
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  public void ObjectTypeAndRelationFiltrationChanged(object sender, NotificationEventArgs e)
  {
    if (!this._activated || e.EventName != nameof (ObjectTypeAndRelationFiltrationChanged))
      return;
    if (this._disableNodeFiltersEvent)
      return;
    try
    {
      this._disableNodeFiltersEvent = true;
      this.FullWindowRefresh();
    }
    finally
    {
      this._disableNodeFiltersEvent = false;
    }
  }

  public bool IsActivated => this._activated;

  /// <summary>Инициализировать сервисы</summary>
  private void InitializeServices()
  {
    this._commandManager = this.InitializeCommandManager();
    this._addressService = this.InitializeAddressService();
    if (this._addressService != null)
      this._addressService.Enabled = true;
    this._mainNotificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._notificationService = this.InitializeNotificationService();
    this._navCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._hotKeysManager = ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager;
    this._defaultCommands4ObjTypes = ServicesManager.GetService(typeof (IDefaultCommands4ObjTypes)) as IDefaultCommands4ObjTypes;
    this._IODispatcher.RegisterDestination((IIODestination) this);
    this._services = (IServiceContainer) new AdvancedServiceContainer();
    this._services.AddService(typeof (NavigatorTreeView), (object) this.TreeView);
    this._services.AddService(typeof (PageViewsManager), (object) this.ViewsManager);
    this._services.AddService(typeof (ITreeListColumns), (object) this);
    this._services.AddService(typeof (INavWindowSettings), (object) new NavWindowSettings());
    this._services.AddService(typeof (TreeViewsBridge), (object) this.ViewsBridge);
    this._services.AddService(typeof (NavWindowBase), (object) this);
    this._services.AddService(typeof (INotificationServiceStatesHolder), (object) new NotificationServiceStatesHolder(NotificationServiceStates.Default));
    if (this._defaultCommands4ObjTypes != null)
      this._services.AddService(typeof (IDefaultCommands4ObjTypes), (object) this._defaultCommands4ObjTypes);
    this._services.AddService(typeof (IViewsManager), (object) this.ViewsManager);
    if (this._IODispatcher != null)
      this._services.AddService(typeof (IIODispatcher), (object) this._IODispatcher);
    this._services.AddService(typeof (IDisableDelayedUpdates), (object) new DisableDelayedUpdates(false));
    this._services.AddService(typeof (IObjectListFiltrationHolder), (object) new ObjectListFiltrationHolder());
    if (this._commandManager != null)
      this._services.AddService(typeof (ICommandManager), (object) this._commandManager);
    if (this._notificationService != null)
    {
      this._services.AddService(typeof (INotificationService), (object) this._notificationService);
      this._notificationService.OnBeforeEvent += new NotificationEventHandler(this.BeforeCreateFilterEventFired);
      this._notificationService.OnAfterEvent += new NotificationEventHandler(this.AfterCreateFilterEventFired);
      this._notificationService.Subscribe("ObjectTypeAndRelationFiltrationChanged", new NotificationEventHandler(this.ObjectTypeAndRelationFiltrationChanged));
    }
    if (this._mainNotificationService != null)
    {
      this._mainNotificationService.Subscribe(new NotificationEventHandler(this.NotificationEventFired));
      this._mainNotificationService.Subscribe("ProjectChanged", new NotificationEventHandler(this.ProjectChanged));
      this._mainNotificationService.Subscribe("ApplicationClosing", new NotificationEventHandler(this.ApplicationClosingEventFired));
      this._mainNotificationService.Subscribe("ApplicationClosed", new NotificationEventHandler(this.ApplicationClosedEventFired));
    }
    if (ServicesManager.GetService(typeof (IObjectListFiltration)) is IObjectListFiltration service1)
      this._services.AddService(typeof (IObjectListFiltration), (object) service1);
    this._services.AddService(typeof (IFiltrationClass), (object) this.filtrationClass);
    if (ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service2)
      service2.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(this.Configuration_BeforeSave);
    this._servicesPages = new AdvancedServiceContainer((System.IServiceProvider) this._services);
    this._servicesPages.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NodeInViews));
    this._servicesPages.AddService(typeof (NavigatorViewOptions), (object) new NavigatorViewOptions(NavigatorViewContext.MainViews));
    this.TreeViewControl.InitializeServices((System.IServiceProvider) this._services);
  }

  /// <summary>Деинициализировать сервисы</summary>
  private void DisposeServices()
  {
    if (this._mainNotificationService != null)
    {
      this._mainNotificationService.Unsubscribe("ApplicationClosing", new NotificationEventHandler(this.ApplicationClosingEventFired));
      this._mainNotificationService.Unsubscribe("ApplicationClosed", new NotificationEventHandler(this.ApplicationClosedEventFired));
      this._mainNotificationService.Unsubscribe(new NotificationEventHandler(this.NotificationEventFired));
      this._mainNotificationService.Unsubscribe("ProjectChanged", new NotificationEventHandler(this.ProjectChanged));
    }
    if (this._notificationService != null)
    {
      this._notificationService.Unsubscribe("ObjectTypeAndRelationFiltrationChanged", new NotificationEventHandler(this.ObjectTypeAndRelationFiltrationChanged));
      this._notificationService.OnBeforeEvent -= new NotificationEventHandler(this.BeforeCreateFilterEventFired);
      this._notificationService.OnAfterEvent -= new NotificationEventHandler(this.AfterCreateFilterEventFired);
    }
    if (this._servicesPages != null)
    {
      this._servicesPages.Dispose();
      this._servicesPages = (AdvancedServiceContainer) null;
    }
    if (ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service)
      service.ConfigurationBeforeSave -= new ConfigurationBeforeSaveEventHandler(this.Configuration_BeforeSave);
    this._IODispatcher.UnregisterDestination((IIODestination) this);
    this._hotKeysManager = (IHotKeysManager) null;
    if (this._services != null)
    {
      this._services.RemoveService(typeof (IIODispatcher));
      this._services.RemoveService(typeof (IDefaultCommands4ObjTypes));
      this._services.RemoveService(typeof (IFiltrationClass));
      this._services.RemoveService(typeof (ICommandManager));
      this._services.RemoveService(typeof (INotificationService));
      this._services.RemoveService(typeof (ITreeListColumns));
      this._services.RemoveService(typeof (IDisableDelayedUpdates));
    }
    this.DisposeFiltrationService(this.FiltrationService);
    this.DisposeNotificationService(this.NotificationService);
    this.DisposeAddressService(this.AddressService);
    this.DisposeCommandManager(this.CommandManager);
    this.filtrationService = (IFiltrationService) null;
    this._notificationService = (Intermech.Client.Core.NotificationService) null;
    this._addressService = (IAddressService) null;
    this._notificationService = (Intermech.Client.Core.NotificationService) null;
    this._services = (IServiceContainer) null;
  }

  /// <summary>
  /// Обработчик события будет вызываться перед закрытием приложения
  /// </summary>
  /// <param name="configurationManager"></param>
  private void Configuration_BeforeSave(IConfigurationManager configurationManager)
  {
    if (this.IsInContainer)
      return;
    this.Do_DeleteFiltrationSettings();
  }

  /// <summary>Форма закрывается (или прячется)</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void NavWindowBase_Closed(object sender, EventArgs e)
  {
    if (this.HideOnClose)
      return;
    this.Do_DeleteFiltrationSettings();
  }

  private void BeforeCreateFilterEventFired(object sender, NotificationEventArgs e)
  {
    this.ViewsBridge.UseDelay = false;
  }

  private void AfterCreateFilterEventFired(object sender, NotificationEventArgs e)
  {
    this.ViewsBridge.UseDelay = true;
  }

  private void HideTreeClick(object sender, EventArgs e) => this.ToggleTree();

  private void TreeView_BeforeFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    if (e.Node == null)
      return;
    this._navigationHistory.Update();
  }

  private void TreeView_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    if (!this._activated)
      return;
    this.UpdateAddress(this.TreeView.FocusedAddress);
  }

  private void TreeView_BuildTree(object sender, EventArgs e)
  {
    this.PersistState = this.TreeView.RootDescriptor is IPersistable;
  }

  private void TreeView_ClearTree(object sender, EventArgs e)
  {
    this._navigationHistory.Clear();
    this.Close();
  }

  private void TreeView_SizeChanged(object sender, EventArgs e)
  {
    this.pnTreeView.Width = this.TreeView.Width;
  }

  /// <summary>Очистим сортировку у колонок дерева</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  public void DoCancelSort(object sender, EventArgs e)
  {
    this.TreeViewControl.DoCancelSort(sender, e);
  }

  HistoryItem IHistoryProvider.CurrentItem
  {
    get
    {
      NavigatorTreeNode focusedNode = this.TreeView.FocusedNode != null ? this.TreeView.FocusedNode : (NavigatorTreeNode) null;
      INode nodeHandler = this.TreeView.FocusedNode != null ? this.TreeView.GetNodeHandler(this.TreeView.FocusedNode) : (INode) null;
      return focusedNode != null && nodeHandler != null && focusedNode.NodeID != null && focusedNode.InTree ? new HistoryItem(nodeHandler.GetAddress(focusedNode.NodeID), (object) this.TreeView.FocusedPath) : (HistoryItem) null;
    }
  }

  void IHistoryProvider.ApplyItem(HistoryItem item)
  {
    if (item != null)
    {
      this.TreeView.TryBrowse((NodeIDPath) item.Tag);
    }
    else
    {
      if (this.TreeView.RootNode == null)
        return;
      this.TreeView.RootNode.Focus();
    }
  }

  object System.IServiceProvider.GetService(System.Type serviceType)
  {
    return serviceType == typeof (INavigate) ? (object) this._navigationHistory : (object) null;
  }

  /// <summary>Фокус ввода перешёл в дерево "Навигатора"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TreeViewEnter(object sender, EventArgs e)
  {
  }

  /// <summary>вернуть раздел справки для контрола</summary>
  public override string HelpID
  {
    get
    {
      if (this.TreeView.Focused)
        return "669";
      IViewPage activeViewPage = this.ViewsManager.ActiveViewPage;
      return activeViewPage != null ? activeViewPage.HelpID : "670";
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      ICurrentNavWindow service = (ICurrentNavWindow) ServicesManager.GetService(typeof (ICurrentNavWindow));
      if (service != null && service.NavWindow != null && service.NavWindow.Equals((object) this))
      {
        service.NavWindow = (object) null;
        service.TreeView = (object) null;
        service.ViewsManagers = (object) null;
      }
      this.TreeView.Services = (System.IServiceProvider) null;
      this.ViewsManager.Services = (System.IServiceProvider) null;
      this.ViewsManager.CloseViews();
      this.DisposeServices();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NavWindowBase));
    this.pnTreeView = new Panel();
    this.TreeViewControl = new NavTreeViewWithProps();
    this.labelSpace = new LabelItem();
    this.spTreeView = new CollapsibleSplitter();
    this.ViewsManager = new PageViewsManager();
    this.ViewsBridge = new TreeViewsBridge(this.components);
    this.pnTreeView.SuspendLayout();
    this.TreeViewControl.SuspendLayout();
    this.SuspendLayout();
    this.pnTreeView.Controls.Add((Control) this.TreeViewControl);
    componentResourceManager.ApplyResources((object) this.pnTreeView, "pnTreeView");
    this.pnTreeView.Name = "pnTreeView";
    this.pnTreeView.Enter += new EventHandler(this.TreeViewEnter);
    componentResourceManager.ApplyResources((object) this.TreeViewControl, "TreeViewControl");
    this.TreeViewControl.Name = "TreeViewControl";
    this.labelSpace.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.labelSpace, "labelSpace");
    this.labelSpace.Enabled = false;
    this.labelSpace.Stretch = true;
    this.spTreeView.AnimationDelay = 1;
    this.spTreeView.AnimationStep = 2000;
    this.spTreeView.BorderStyle3D = Border3DStyle.Etched;
    this.spTreeView.ControlToHide = (Control) this.pnTreeView;
    this.spTreeView.ExpandParentForm = false;
    componentResourceManager.ApplyResources((object) this.spTreeView, "spTreeView");
    this.spTreeView.Name = "spTreeView";
    this.spTreeView.TabStop = false;
    this.spTreeView.UseAnimations = true;
    this.spTreeView.VisualStyle = VisualStyles.Mozilla;
    this.ViewsManager.ActiveViewPage = (IViewPage) null;
    this.ViewsManager.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this.ViewsManager, "ViewsManager");
    this.ViewsManager.Name = "ViewsManager";
    this.ViewsBridge.NavTreeView = this.TreeViewControl.TreeView;
    this.ViewsBridge.ViewsManager = (IViewsManager) this.ViewsManager;
    this.AllowedStates = DockLocation.Float | DockLocation.Document;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.ViewsManager);
    this.Controls.Add((Control) this.spTreeView);
    this.Controls.Add((Control) this.pnTreeView);
    this.Name = nameof (NavWindowBase);
    this.ShowImageInDocumentTab = true;
    this.Closed += new EventHandler(this.NavWindowBase_Closed);
    this.pnTreeView.ResumeLayout(false);
    this.TreeViewControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  internal class FocusedNodeAdapter : ISelectedItems, ISimpleSelectedItems
  {
    private IFocusedItem _node;

    public FocusedNodeAdapter(IFocusedItem node) => this._node = node;

    public bool IsCollage => false;

    public int Count => 1;

    public INodeID GetItemID(int index) => this._node.ItemID;

    public object GetItemData(int index, System.Type dataFormat)
    {
      return this._node.GetItemData(dataFormat);
    }

    public NodeIDPath GetParentPath(int index) => this._node.ParentPath;

    public object GetParentData(int index, System.Type dataFormat)
    {
      return this._node.GetParentData(dataFormat);
    }
  }
}
