// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views.ArtsCompositionApplicabilityView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.ContextMenu.Extensions;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views;

/// <summary>Закладка "Применяемость в ТП"</summary>
[ViewDescriptionProvider(typeof (ArtsCompositionApplicabilityView.ArtsCompositionApplicabilityViewDescriptionProvider))]
public class ArtsCompositionApplicabilityView : UserControl, IView, IIODestination
{
  /// <summary>Ид. версии текущего объекта</summary>
  private long _objectId;
  /// <summary>
  /// 
  /// </summary>
  private ArtsCompositionApplicabilityParams _applicabilityParams;
  /// <summary>
  /// 
  /// </summary>
  private bool _dataLoaded;
  /// <summary>Category guid for root descriptor</summary>
  private Guid _rootCategoryGuid = Guid.Empty;
  /// <summary>Category id for root descriptor</summary>
  private int _rootCategoryId;
  /// <summary>
  /// 
  /// </summary>
  private IServiceContainer _services;
  /// <summary>
  /// 
  /// </summary>
  private INotificationService _notificationService;
  /// <summary>
  /// Сервис службы "горячих клавиш" и связанных с ними команд
  /// </summary>
  private IHotKeysManager _hotKeysManager;
  /// <summary>Диспетчер событий</summary>
  private readonly IIODispatcher _ioDispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>
  /// 
  /// </summary>
  private ILocalCommandsProvider _localCommandsProvider;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  /// <summary>
  /// 
  /// </summary>
  protected internal TechCardNavTreeViewControl tolcObjectList;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    this.ImageIndex = service != null ? service.ImageIndex("imgEntersTo") : -1;
    this.Caption = LocalizationHolder.rm.GetString("TechCard.Client_535");
  }

  /// <summary>Инициализация сервисов</summary>
  private void InitializeServices()
  {
    this._hotKeysManager = ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager;
    this._notificationService = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    this._notificationService?.Subscribe(new NotificationEventHandler(this.NotifyEvent));
    this._services = (IServiceContainer) new ServiceContainer();
    this._services.AddService(typeof (IViewState), (object) new ViewStateService());
    if (this._notificationService != null)
      this._services.AddService(typeof (INotificationService), (object) this._notificationService);
    if (this._ioDispatcher != null)
    {
      this._ioDispatcher.RegisterDestination((IIODestination) this);
      this._services.AddService(typeof (IIODispatcher), (object) this._ioDispatcher);
    }
    this.tolcObjectList.Services = (System.IServiceProvider) this._services;
    this.InitializeCustomProviders();
  }

  /// <summary>Деинициализация сервисов</summary>
  private void UnInitializeServices()
  {
    this.DisposeCustomProviders();
    this._notificationService?.Unsubscribe(new NotificationEventHandler(this.NotifyEvent));
    this._ioDispatcher?.UnregisterDestination((IIODestination) this);
    if (this.tolcObjectList == null)
      return;
    this.tolcObjectList.Services = (System.IServiceProvider) null;
  }

  /// <summary>Регистрация категории</summary>
  private void RegisterCategory()
  {
    this._rootCategoryGuid = Guid.NewGuid();
    IGuidMapper service = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    this._rootCategoryId = service.Register(this._rootCategoryGuid);
  }

  /// <summary>Раз регистрация категории</summary>
  private void UnregisterCategory()
  {
    ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false)?.Unregister(this._rootCategoryId);
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    this.RegisterCategory();
    this.InitializeServices();
    this.tolcObjectList.DisableIMContextMenu = false;
    this.tolcObjectList.DisableColumnsSorting = true;
    this.tolcObjectList.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(ArtsCompositionApplicabilityViewUtils.OnGetSupportedColumnsEventHandler);
    IDescriptor descriptor = (IDescriptor) new TechObjectListDescriptor(this._rootCategoryId, TechCardConsts.RelTypes.TechRelationID, LocalizationHolder.rm.GetString("TechCard.Client_536"), (IList) null);
    NodeColumnCollection columns = Intermech.Navigator.Utils.VersionColumns(NodeColumnSortOrder.None, false);
    ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    this.tolcObjectList.SetColumns(columns, descriptor);
  }

  /// <summary>Initialize custom settings</summary>
  private void InitializeCustomSettings()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomProviders()
  {
    if (this._localCommandsProvider == null)
      this._localCommandsProvider = (ILocalCommandsProvider) new ArtsCompositionApplicabilityCommandProvider();
    this._services.StackLocalContextCommandsProvider(this._localCommandsProvider);
  }

  /// <summary>
  /// 
  /// </summary>
  private void DisposeCustomProviders()
  {
    if (this._localCommandsProvider == null)
      return;
    this._services.DisposeLocalContextCommandsTemplates(this._localCommandsProvider);
  }

  /// <summary>Загрузка данных</summary>
  /// <returns></returns>
  private bool LoadData()
  {
    if (this._dataLoaded)
      return false;
    IDescriptor rootDescriptor = (IDescriptor) null;
    DescriptorCollection descriptors = new DescriptorCollection();
    if (this._applicabilityParams != null)
    {
      string caption = LocalizationHolder.rm.GetString("TechCard.Client_536");
      foreach (ObjInfoItem objInfoItem in this._applicabilityParams.TechElemObj2ArticleList.Where<Tuple<ObjInfoItem, ObjInfoItem>>((Func<Tuple<ObjInfoItem, ObjInfoItem>, bool>) (item => (TypedInfoItem) item.Item2 != (TypedInfoItem) null && item.Item2.ObjectID == this.ObjectID)).Select<Tuple<ObjInfoItem, ObjInfoItem>, ObjInfoItem>((Func<Tuple<ObjInfoItem, ObjInfoItem>, ObjInfoItem>) (item => item.Item1)).ToList<ObjInfoItem>())
      {
        if (!((TypedInfoItem) objInfoItem == (TypedInfoItem) null))
        {
          IDescriptor descriptor = (IDescriptor) new TechCompositionDescriptor(this._rootCategoryId, 0, objInfoItem.ObjectID, objInfoItem.ObjTypeID, (IEnumerable<int>) new int[1]
          {
            TechCardConsts.RelTypes.TechRelationID
          }, caption, RelatedObjectsRole.Applicability, (ITechCompositionFilter) null, (IEnumerable<NodeColumnID>) null);
          descriptors.Add(descriptor);
        }
      }
      rootDescriptor = (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.EdinicaSostavaID, caption, descriptors);
    }
    if (rootDescriptor != null)
    {
      this.tolcObjectList.Build(rootDescriptor);
      if (this.tolcObjectList.RootNode?.Children != null && descriptors.Count > 0)
      {
        for (int index = 0; index < descriptors.Count; ++index)
          this.tolcObjectList.TryBrowse(new NodeIDPath(rootDescriptor)
          {
            rootDescriptor.GetRecordNodeID(),
            descriptors[index].GetRecordNodeID()
          });
      }
    }
    this._dataLoaded = true;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadSettings()
  {
    IConfiguration config = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false)?.Open(nameof (ArtsCompositionApplicabilityView));
    if (config == null || this.tolcObjectList == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this.tolcObjectList);
  }

  /// <summary>
  /// 
  /// </summary>
  private void SaveSettings()
  {
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(nameof (ArtsCompositionApplicabilityView)) ?? service.Create(nameof (ArtsCompositionApplicabilityView));
    if (config == null || this.tolcObjectList == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this.tolcObjectList);
  }

  /// <summary>Конструктор</summary>
  public ArtsCompositionApplicabilityView()
  {
    this.InitializeComponent();
    this.InitializeData();
    if (this.DesignMode)
      return;
    this.InitializeCustomSettings();
    this.InitializeCustomControls();
  }

  /// <summary>Ид. версии объекта</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this._objectId;
    [DebuggerStepThrough] private set
    {
      if (this._objectId == value)
        return;
      this._objectId = value;
      this._dataLoaded = false;
    }
  }

  /// <summary>Initialize</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items == null)
      return;
    this.ObjectID = items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData ? itemData.Value : 0L;
    this._applicabilityParams = ServiceUtils.GetService<ArtsCompositionApplicabilityParams>((object) provider, false);
    if (this._applicabilityParams == null)
      return;
    this._services.RemoveService(typeof (ArtsCompositionApplicabilityParams));
    this._services.AddService(typeof (ArtsCompositionApplicabilityParams), (object) this._applicabilityParams);
  }

  /// <summary>Activate</summary>
  /// <param name="previousView">
  /// </param>
  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this.LoadSettings();
    this.LoadData();
  }

  /// <summary>Deactivate</summary>
  /// <param name="nextView">
  /// </param>
  public void Deactivate(IView nextView) => this.SaveSettings();

  /// <summary>Caption</summary>
  public string Caption { get; private set; } = "";

  /// <summary>ImageIndex</summary>
  public int ImageIndex { get; private set; }

  /// <summary>OrderID</summary>
  public int OrderID => 100;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void NotifyEvent(object sender, NotificationEventArgs e)
  {
    if (e == null)
      return;
    DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
    switch (e.EventName)
    {
      case "ProjectChanged":
        this._dataLoaded = false;
        this.Activate((IView) null);
        break;
      case "ApplicabilityUpdated":
        if (objectsEventArgs == null || !objectsEventArgs.ObjectIDs.Contains(this._objectId))
          break;
        this._dataLoaded = false;
        this.Activate((IView) null);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  IOEventTypes IIODestination.SupportedEvents
  {
    get => IOEventTypes.evKeyDown | IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  /// <summary>Обработать указанное событие</summary>
  /// <param name="Event">Событие</param>
  bool IIODestination.ProcessEvent(IIOEvent Event)
  {
    if (Event == null || Event.EventType != IOEventType.evKeyUp && Event.EventType != IOEventType.evKeyDown || this._hotKeysManager == null)
      return false;
    KeyEventArgs eventData = (KeyEventArgs) Event.EventData;
    if (Event.EventType == IOEventType.evKeyDown && eventData.KeyCode == eventData.KeyData && eventData.Modifiers == Keys.None)
      return false;
    List<IHotKeysCommand> commands = this._hotKeysManager[((KeyEventArgs) Event.EventData).KeyCode | ((KeyEventArgs) Event.EventData).Modifiers];
    if (commands == null || commands.Count <= 0)
      return false;
    ((KeyEventArgs) Event.EventData).Handled = true;
    return this.ExecuteMenuCommand(commands, Event);
  }

  /// <summary>
  /// Вызвать выполнение первой разрешённой команды контекстного меню для указанного события
  /// </summary>
  /// <param name="commands">Команды контекстного меню</param>
  /// <param name="ioEvent">Событие</param>
  /// <returns>true, если команда обработана</returns>
  private bool ExecuteMenuCommand(List<IHotKeysCommand> commands, IIOEvent ioEvent)
  {
    if (commands == null || commands.Count == 0 || ioEvent?.Source.SelectedItems == null)
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

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.UnInitializeServices();
      this.UnregisterCategory();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ArtsCompositionApplicabilityView));
    this.tolcObjectList = new TechCardNavTreeViewControl();
    this.tolcObjectList.BeginInit();
    this.SuspendLayout();
    this.tolcObjectList.AllowDrop = true;
    this.tolcObjectList.AllowMultiSelect = false;
    this.tolcObjectList.AllowUserPinnedColumns = false;
    this.tolcObjectList.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.tolcObjectList.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("tolcObjectList.CheckedNodesStates");
    this.tolcObjectList.CheckoutMode = TechCheckoutMode.Manual;
    this.tolcObjectList.CheckRootNode = false;
    this.tolcObjectList.ContextMenuBarItem = (ContextMenuBarItem) null;
    this.tolcObjectList.DisableCheckedOutColumn = true;
    this.tolcObjectList.DisableIMContextMenu = true;
    this.tolcObjectList.DisablePacketsReading = false;
    componentResourceManager.ApplyResources((object) this.tolcObjectList, "tolcObjectList");
    this.tolcObjectList.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("tolcDocList.HeaderStyle.HorzAlignment");
    this.tolcObjectList.ImageList = (ImageList) null;
    this.tolcObjectList.LineStyle = LineStyle.Dot;
    this.tolcObjectList.Name = "tolcObjectList";
    this.tolcObjectList.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcDocList.RowEvenStyle.WordWrap");
    this.tolcObjectList.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcDocList.RowOddStyle.WordWrap");
    this.tolcObjectList.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcDocList.RowSelectedStyle.WordWrap");
    this.tolcObjectList.RowStyle.BorderColor = SystemColors.Control;
    this.tolcObjectList.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.tolcObjectList.RowStyle.BorderWidth = 1;
    this.tolcObjectList.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcDocList.RowStyle.WordWrap");
    this.tolcObjectList.SelectBeforeEdit = true;
    this.tolcObjectList.ShowRootRow = false;
    this.tolcObjectList.SuppressErrorMessages = true;
    this.tolcObjectList.Tag = (object) " ";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tolcObjectList);
    this.Name = nameof (ArtsCompositionApplicabilityView);
    this.Tag = (object) " ";
    this.tolcObjectList.EndInit();
    this.ResumeLayout(false);
  }

  private sealed class ArtsCompositionApplicabilityViewDescriptionProvider : 
    BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      ViewDescription viewDescription = new ViewDescription();
      viewDescription.Caption = LocalizationHolder.rm.GetString("TechCard.Client_535");
      INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
      viewDescription.ImageIndex = service != null ? service.ImageIndex("imgEntersTo") : -1;
      viewDescription.OrderID = 100;
      return viewDescription;
    }
  }
}
