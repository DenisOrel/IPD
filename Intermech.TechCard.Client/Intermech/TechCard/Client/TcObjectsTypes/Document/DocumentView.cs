// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Document.DocumentView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
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
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Document;

/// <summary>Закладка "Список документов"</summary>
[ViewDescriptionProvider(typeof (DocumentView.DocumentViewDescriptionProvider))]
public class DocumentView : UserControl, IView, IIODestination
{
  /// <summary>Ид. версии текущего объекта</summary>
  private long _objectId;
  /// <summary>Заголовок закладки</summary>
  private string _caption = string.Empty;
  /// <summary>
  /// 
  /// </summary>
  private int _imageIndex;
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
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  /// <summary>
  /// 
  /// </summary>
  protected internal TechCardNavTreeViewControl tolcDocList;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    this._imageIndex = service != null ? service.ImageIndex("imgDocumentList") : -1;
    this._caption = LocalizationHolder.rm.GetString("TechCard.Client_168");
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
    this.tolcDocList.Services = (System.IServiceProvider) this._services;
  }

  /// <summary>Де-инициализация сервисов</summary>
  private void UnInitializeServices()
  {
    this._notificationService?.Unsubscribe(new NotificationEventHandler(this.NotifyEvent));
    this._ioDispatcher?.UnregisterDestination((IIODestination) this);
    if (this.tolcDocList == null)
      return;
    this.tolcDocList.Services = (System.IServiceProvider) null;
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

  /// <summary>Раз-регистрация категории</summary>
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
    this.tolcDocList.DisableIMContextMenu = false;
    this.tolcDocList.DisableColumnsSorting = true;
    this.tolcDocList.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    string caption = LocalizationHolder.rm.GetString("TechCard.Client_454");
    IDescriptor descriptor = (IDescriptor) new TechObjectListDescriptor(this._rootCategoryId, MetaDataHelper.GetCommonParentObjectTypeID(TechCardConsts.ObjectTypes.TechBaseDocID, TechCardConsts.ObjectTypes.ComplectDocBaseID), caption, (IList) null);
    NodeColumnCollection columns = Intermech.Navigator.Utils.VersionColumns(NodeColumnSortOrder.None, false);
    ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    this.tolcDocList.SetColumns(columns, descriptor);
  }

  /// <summary>Initialize custom settings</summary>
  private void InitializeCustomSettings()
  {
  }

  /// <summary>Загрузка данных</summary>
  /// <returns></returns>
  private bool LoadData()
  {
    if (this._dataLoaded)
      return false;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ComplectDocBaseID);
    childrenIdRecursive.Add(TechCardConsts.ObjectTypes.ComplectDocBaseID);
    childrenIdRecursive.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TechBaseDocID));
    childrenIdRecursive.Add(TechCardConsts.ObjectTypes.TechBaseDocID);
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false)
    };
    IDescriptor rootDescriptor = (IDescriptor) null;
    List<TechCardUtils.SostavSortedTreeItem> childSostavTree;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      childSostavTree = TechCardUtils.GetChildSostavTree(this._objectId, sessionKeeper.Session, (IEnumerable<int>) new int[2]
      {
        TechCardConsts.RelTypes.TechRelationID,
        TechCardConsts.RelTypes.SortedRelationID
      }, true, conditions, (Dictionary<string, ColumnDescriptor>) null);
    DescriptorCollection descriptors = new DescriptorCollection();
    if (childSostavTree != null)
    {
      string caption = LocalizationHolder.rm.GetString("TechCard.Client_454");
      TechCompositionSostavTreeFilter sostavTreeFilter = new TechCompositionSostavTreeFilter(RelatedObjectsRole.Composition, (IList<TechCardUtils.SostavSortedTreeItem>) childSostavTree);
      foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in childSostavTree)
      {
        if (sostavSortedTreeItem != null && sostavSortedTreeItem.ProjID == this._objectId)
        {
          IDescriptor descriptor = (IDescriptor) new TechCompositionDescriptor(this._rootCategoryId, 0, sostavSortedTreeItem.PartID, sostavSortedTreeItem.ObjectTypeID, TechCardConsts.RelTypes.TechRelationID, caption, RelatedObjectsRole.Composition, (ITechCompositionFilter) sostavTreeFilter);
          descriptors.Add(descriptor);
        }
      }
      rootDescriptor = (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ComplectDocBaseID, caption, descriptors);
    }
    if (rootDescriptor != null)
    {
      this.tolcDocList.Build(rootDescriptor);
      if (this.tolcDocList.RootNode?.Children != null && descriptors.Count > 0)
      {
        for (int index = 0; index < descriptors.Count; ++index)
          this.tolcDocList.TryBrowse(new NodeIDPath(rootDescriptor)
          {
            rootDescriptor.GetRecordNodeID(),
            descriptors[index].GetRecordNodeID()
          });
      }
    }
    this._dataLoaded = true;
    return true;
  }

  /// <summary>Конструктор</summary>
  public DocumentView()
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
    [DebuggerStepThrough] set
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
  }

  /// <summary>Activate</summary>
  /// <param name="previousView">
  /// </param>
  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    DocumentViewDlg.LoadSettings(this);
    this.LoadData();
  }

  /// <summary>Deactivate</summary>
  /// <param name="nextView">
  /// </param>
  public void Deactivate(IView nextView) => DocumentViewDlg.SaveSettings(this);

  /// <summary>Caption</summary>
  public string Caption => this._caption;

  /// <summary>ImageIndex</summary>
  public int ImageIndex => this._imageIndex;

  /// <summary>OrderID</summary>
  public int OrderID => 12;

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
      case "DocumentsCreated":
      case "DocumentsUpdated":
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentView));
    this.tolcDocList = new TechCardNavTreeViewControl();
    this.tolcDocList.BeginInit();
    this.SuspendLayout();
    this.tolcDocList.AllowDrop = true;
    this.tolcDocList.AllowMultiSelect = false;
    this.tolcDocList.AllowUserPinnedColumns = false;
    this.tolcDocList.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.tolcDocList.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("tolcDocList.CheckedNodesStates");
    this.tolcDocList.CheckoutMode = TechCheckoutMode.Manual;
    this.tolcDocList.CheckRootNode = false;
    this.tolcDocList.DisableCheckedOutColumn = true;
    this.tolcDocList.DisableIMContextMenu = true;
    this.tolcDocList.DisableKeyDownEvents = false;
    this.tolcDocList.DisableKeyUpEvents = false;
    this.tolcDocList.DisablePacketsReading = false;
    componentResourceManager.ApplyResources((object) this.tolcDocList, "tolcDocList");
    this.tolcDocList.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("tolcDocList.HeaderStyle.HorzAlignment");
    this.tolcDocList.LineStyle = LineStyle.Dot;
    this.tolcDocList.Name = "tolcDocList";
    this.tolcDocList.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcDocList.RowEvenStyle.WordWrap");
    this.tolcDocList.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcDocList.RowOddStyle.WordWrap");
    this.tolcDocList.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcDocList.RowSelectedStyle.WordWrap");
    this.tolcDocList.RowStyle.BorderColor = SystemColors.Control;
    this.tolcDocList.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.tolcDocList.RowStyle.BorderWidth = 1;
    this.tolcDocList.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcDocList.RowStyle.WordWrap");
    this.tolcDocList.SelectBeforeEdit = true;
    this.tolcDocList.ShowRootRow = false;
    this.tolcDocList.SuppressErrorMessages = true;
    this.tolcDocList.Tag = (object) " ";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tolcDocList);
    this.Name = nameof (DocumentView);
    this.Tag = (object) " ";
    this.tolcDocList.EndInit();
    this.ResumeLayout(false);
  }

  private sealed class DocumentViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      ViewDescription viewDescription = new ViewDescription();
      viewDescription.Caption = LocalizationHolder.rm.GetString("TechCard.Client_168");
      INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
      viewDescription.ImageIndex = service != null ? service.ImageIndex("imgDocumentList") : -1;
      viewDescription.OrderID = 12;
      return viewDescription;
    }
  }
}
