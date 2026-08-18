
// Type: Intermech.Client.Core.FormDesigner.Controls.ObjectsList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[Designer(typeof (ObjectsListDesigner))]
[RefreshProperties(RefreshProperties.All)]
[ToolboxItem(false)]
public class ObjectsList : UserControl, IFormDesignerControl, IObjectsListSupport, IIDListSupport
{
  private NodeColumnCollection _columnCollection;
  private ListContext _listContext;
  private long _selectionID;
  private Guid _selectionGuid = Guid.Empty;
  private int _objTypeID = -1;
  private int _relTypeID = -1;
  private Guid _objTypeGuid = Guid.Empty;
  private Guid _relTypeGuid = Guid.Empty;
  private EventHandler _formDeactivate;
  private EventHandler _loadDataCompleted;
  private IFormDesignerControl _parent;
  /// <summary>
  /// Может быть подписка и на LoadDataCompleted и на FormDeactivate,
  /// поэтому, чтобы не подписываться 2 раза на событие изменения родителя и закладки (если нужно),
  /// выставляем этот флаг при первом подписании.
  /// </summary>
  private bool _isSubscribeOnTabPageParentChanged;
  private bool _isSubscribeSelectedItemsChanged;
  /// <summary>Oбъект, который будет являться источником информации</summary>
  private ObjectsList _sourceCtrl;
  private EventHandler _selectedItemsChanged;
  /// <summary>Сохраненные настроки для колонок ChildrenView</summary>
  private SavedColumnsSettings _savedColumnsSettings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ObjectsViewBase _childrenView;

  /// <summary>Контрол ChildrenView.</summary>
  /// <remarks>
  /// 1027104 - Для ObjectsList опубликовать "метод, позволяющий программно выбрать (встать) на нужную строчку с искомым значением" | добавить метод для чтения значения атрибута из текущей строки.
  /// По указанию Д.Жукова создано свойство открывающее контрол для работы с ним из вне.
  /// </remarks>
  public ChildrenView ChildrenView => (ChildrenView) this._childrenView;

  /// <summary>Набор определенных пользователем колонок.</summary>
  [DefaultValue(null)]
  public NodeColumnCollection ColumnCollection
  {
    get
    {
      NodeColumnCollection columnCollection = (NodeColumnCollection) null;
      if (this._columnCollection != null)
      {
        columnCollection = new NodeColumnCollection();
        foreach (NodeColumn column in (System.Collections.Generic.List<NodeColumn>) this._columnCollection)
        {
          if (column.Attribute != null)
            columnCollection.Add(column);
        }
      }
      return columnCollection;
    }
    set
    {
      if (value != null)
      {
        if (this.ColumnsAliases != null && this.ColumnsAliases.Count > 0)
        {
          Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>(this.ColumnsAliases.Count);
          foreach (NodeColumn nodeColumn in (System.Collections.Generic.List<NodeColumn>) value)
          {
            Guid attributeGuid = nodeColumn.Attribute.AttributeGuid;
            if (this.ColumnsAliases.ContainsKey(attributeGuid))
              dictionary.Add(attributeGuid, this.ColumnsAliases[attributeGuid]);
          }
          this.ColumnsAliases = dictionary;
        }
      }
      else
        this.ColumnsAliases = (Dictionary<Guid, string>) null;
      this._columnCollection = value;
    }
  }

  /// <summary>Список переименованных колонок.</summary>
  [DefaultValue(null)]
  public Dictionary<Guid, string> ColumnsAliases { get; set; }

  /// <summary>
  /// Набор определенных пользователем колонок, по которым будет производиться сортировка по умолчанию.
  /// </summary>
  [DefaultValue(null)]
  public NodeColumnCollection DefaultSortingColumns { get; set; }

  /// <summary>
  /// Наименование объекта, который будет являться источником информации.
  /// </summary>
  [DefaultValue("")]
  public string DataSourceName { get; set; }

  /// <summary>Группировка колонок.</summary>
  [DefaultValue(true)]
  public bool DisableColumnsGrouping
  {
    get => this._childrenView.DisableColumnsGrouping;
    set => this._childrenView.DisableColumnsGrouping = value;
  }

  /// <summary>Включение/отключение режима редактирования.</summary>
  [DefaultValue(false)]
  public bool EditMode { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(ListContext.Objects)]
  public ListContext List
  {
    get => this._listContext;
    set
    {
      if (this._listContext == ListContext.Objects && value != ListContext.Objects || this._listContext != ListContext.Objects && value == ListContext.Objects)
        this._relTypeID = -1;
      this._listContext = value;
    }
  }

  /// <summary>Дальнейшие действия после двойного клика.</summary>
  [DefaultValue(AfterDoubleClickAction.Card)]
  public AfterDoubleClickAction AfterDoubleClick { get; set; }

  /// <summary>Идентификатор типа объектов.</summary>
  /// <remarks>Правильно хранить Guid типа объектов. Оставлено для поддержания работы ранее созданного.</remarks>
  [Browsable(false)]
  public int ObjectsTypeID
  {
    get => this._objTypeID;
    set
    {
      if (this._objTypeID == value)
        return;
      this._objTypeGuid = MetaDataHelper.GetObjectTypeGuid(value);
      this._objTypeID = this._objTypeGuid != Guid.Empty ? value : -1;
    }
  }

  /// <summary>Глобальный идентификатор типа объектов.</summary>
  public Guid ObjectTypesGuid
  {
    get => this._objTypeGuid;
    set
    {
      if (!(this._objTypeGuid != value))
        return;
      this._objTypeID = MetaDataHelper.GetObjectTypeID(value);
      this._objTypeGuid = this._objTypeID != -1 ? value : Guid.Empty;
    }
  }

  /// <summary>Глобальный идентификатор типа связи.</summary>
  public Guid RelationsTypeGuid
  {
    get => this._relTypeGuid;
    set
    {
      if (!(this._relTypeGuid != value))
        return;
      this._relTypeID = MetaDataHelper.GetRelationTypeID(value);
      this._relTypeGuid = this._relTypeID != -1 ? value : Guid.Empty;
    }
  }

  /// <summary>Идентификатор типа связи.</summary>
  public int RelationsTypeID
  {
    get => this._relTypeID;
    set
    {
      if (this._relTypeID == value)
        return;
      this._relTypeGuid = MetaDataHelper.GetRelationTypeGuid(value);
      this._relTypeID = this._relTypeGuid != Guid.Empty ? value : -1;
    }
  }

  /// <summary>Глобальный идентификатор выборки.</summary>
  public Guid SelectionGuid
  {
    get => this._selectionGuid;
    set
    {
      this._selectionID = 0L;
      this._selectionGuid = Guid.Empty;
      if (!(value != Guid.Empty))
        return;
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(value);
      if (objectInfo.Empty)
        return;
      this._selectionID = Math.Abs(objectInfo.ObjectID);
      this._selectionGuid = value;
    }
  }

  /// <summary>Идентификатор выборки.</summary>
  public long SelectionID
  {
    get => this._selectionID;
    set
    {
      this._selectionGuid = Guid.Empty;
      this._selectionID = 0L;
      if (value == 0L)
        return;
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(value);
      if (objectInfo.Empty)
        return;
      this._selectionGuid = objectInfo.VersionGuid;
      this._selectionID = value;
    }
  }

  /// <summary>Список выделенных объектов.</summary>
  public ISelectedItems SelectedItems => this._childrenView.SelectedItems;

  /// <summary>Список идентификаторов выделенных объектов.</summary>
  public System.Collections.Generic.List<long> SelecetdItemsIDs
  {
    get
    {
      System.Collections.Generic.List<long> selecetdItemsIds = new System.Collections.Generic.List<long>();
      ISelectedItems selectedItems = this._childrenView.SelectedItems;
      if (selectedItems != null)
      {
        for (int index = 0; index < selectedItems.Count; ++index)
        {
          if (selectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && !selecetdItemsIds.Contains(itemData.ObjectID) && itemData.ObjectID != 0L)
            selecetdItemsIds.Add(itemData.ObjectID);
        }
      }
      return selecetdItemsIds;
    }
  }

  /// <summary>Отображение строки состояния.</summary>
  [DefaultValue(true)]
  public bool ShowStatusBar
  {
    get => !this._childrenView.DisableStatusBar;
    set => this._childrenView.DisableStatusBar = !value;
  }

  /// <summary>Показ контекстного меню</summary>
  [DefaultValue(true)]
  public bool ShowContextMenu
  {
    get => !this._childrenView.DisableIMContextMenu;
    set => this._childrenView.DisableIMContextMenu = !value;
  }

  /// <summary>Контрол-источник.</summary>
  /// <remark>Работаем только через свойство, чтобы не подписываться по нескольку раз на одно событие</remark>
  private ObjectsList SourceCtrl
  {
    get => this._sourceCtrl;
    set
    {
      if (value != null)
      {
        if (this._sourceCtrl != null && this._sourceCtrl != value)
          this._sourceCtrl.SelectedItemsChanged -= new EventHandler(this.OnsourceCtrl_SelectedItemsChanged);
        this._sourceCtrl = value;
        this._sourceCtrl.SelectedItemsChanged += new EventHandler(this.OnsourceCtrl_SelectedItemsChanged);
      }
      else
      {
        if (this._sourceCtrl == value)
          return;
        this._sourceCtrl.SelectedItemsChanged -= new EventHandler(this.OnsourceCtrl_SelectedItemsChanged);
        this._sourceCtrl = value;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue("")]
  public new object Tag
  {
    get => base.Tag;
    set => base.Tag = value;
  }

  /// <summary>
  /// Использование наименований колонок определенных пользователем.
  /// </summary>
  [DefaultValue(false)]
  public bool UseColumnsAliases { get; set; }

  /// <summary>Конструктор.</summary>
  public ObjectsList()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this.UseColumnsAliases = false;
    this.CanContainsChildren = false;
    this.AfterDoubleClick = AfterDoubleClickAction.Card;
    this._childrenView.BlockUISettingsDisableChildrenViewGrouping = true;
    this._childrenView.StateStreamPrefix = Convert.ToString((object) Guid.NewGuid());
    this._childrenView.OnNodeColumnRename += new NodeColumnRenameEventHandler(this.On_childrenView_OnNodeColumnRename);
  }

  /// <summary>Изменение выделенного элемента.</summary>
  public event EventHandler SelectedItemsChanged
  {
    add
    {
      this._selectedItemsChanged += value;
      this._childrenView.DisableDelayedUpdates = false;
    }
    remove
    {
      if (this._selectedItemsChanged == null)
        return;
      this._selectedItemsChanged -= value;
      if (this._selectedItemsChanged != null)
        return;
      this._childrenView.DisableDelayedUpdates = true;
    }
  }

  /// <summary>Переименование колонок.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_childrenView_OnNodeColumnRename(object sender, NodeColumnRenameEventArgs e)
  {
    if (!this.UseColumnsAliases || this.ColumnsAliases == null || e == null)
      return;
    NodeColumn column = e.Column;
    if (column == null)
      return;
    Guid attributeGuid = column.Attribute.AttributeGuid;
    if (!this.ColumnsAliases.ContainsKey(attributeGuid))
      return;
    column.Caption = this.ColumnsAliases[attributeGuid];
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_childrenView_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (this._selectedItemsChanged == null)
      return;
    this._selectedItemsChanged((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnFormDeactivate(object sender, EventArgs e) => this.ReleaseChildrenView();

  /// <summary>Данные загружены.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnLoadDataCompleted(object sender, EventArgs e)
  {
    Form form = (sender as Control).FindForm();
    if (form == null)
      return;
    DesForm desForm = form as DesForm;
    long objID = desForm.Info.ElementIdentifier;
    if (this.SourceCtrl == null && !string.IsNullOrEmpty(this.DataSourceName))
    {
      foreach (Control control in form.Controls.Find(this.DataSourceName, true))
      {
        if (control is ObjectsList objectsList)
        {
          this.SourceCtrl = objectsList;
          break;
        }
      }
    }
    if (this.SourceCtrl != null)
      objID = this.SourceCtrl.SelecetdItemsIDs.Count == 1 ? this.SourceCtrl.SelecetdItemsIDs[0] : 0L;
    else if (!string.IsNullOrEmpty(this.DataSourceName))
      objID = 0L;
    if (this._isSubscribeSelectedItemsChanged)
    {
      this._childrenView.SelectedItemsChanged -= new EventHandler(this.On_childrenView_SelectedItemsChanged);
      this._isSubscribeSelectedItemsChanged = false;
    }
    System.IServiceProvider serviceProvider = desForm.ServiceProvider ?? (System.IServiceProvider) ServicesManager.ServiceContainer;
    this.CorrectServices(serviceProvider, this._childrenView);
    ObjectsListService objectsListService = (ObjectsListService) null;
    if (this._listContext == ListContext.Composition)
    {
      if (this.AfterDoubleClick == AfterDoubleClickAction.InTree)
      {
        objectsListService = new ObjectsListService(this._selectionID, objID, this._objTypeID, this._relTypeID, RelatedObjectsRole.Composition, this.ColumnCollection);
        ISelectedItems service = serviceProvider.GetService<ISelectedItems>(false);
        if (service != null)
        {
          NodeIDPath parentPath = service.GetParentPath(0);
          INodeID itemId = service.GetItemID(0);
          this._childrenView.Initialize(parentPath, (INode) new ObjectsListVirtualNode(ObjectsListConsts.CompositionNodeID, objectsListService), itemId, serviceProvider);
        }
        else
          this._childrenView.Initialize((IDescriptor) new ListDescriptor(ObjectsListConsts.ObjectsNodeID, this._objTypeID, string.Empty, (IList) new System.Collections.Generic.List<long>()), serviceProvider);
        this._childrenView.EditingMode = this.EditMode;
      }
      else
      {
        objectsListService = new ObjectsListService(this._selectionID, objID, this._objTypeID, this._relTypeID, RelatedObjectsRole.Composition, this.ColumnCollection);
        this._childrenView.Initialize((IDescriptor) new ObjectsListNodeDescriptor(ObjectsListConsts.CompositionNodeID, objectsListService), serviceProvider);
        this._childrenView.EditingMode = this.EditMode;
      }
    }
    else if (this._listContext == ListContext.Objects)
    {
      objectsListService = new ObjectsListService(this._selectionID, objID, this._objTypeID, this._relTypeID, RelatedObjectsRole.Composition, this.ColumnCollection);
      this._childrenView.Initialize((IDescriptor) new ObjectsListNodeDescriptor(ObjectsListConsts.ObjectsNodeID, objectsListService), serviceProvider);
      this._childrenView.EditingMode = this.EditMode;
    }
    else if (this._listContext == ListContext.Applicability)
    {
      objectsListService = new ObjectsListService(this._selectionID, objID, this._objTypeID, this._relTypeID, RelatedObjectsRole.Applicability, this.ColumnCollection);
      this._childrenView.Initialize((IDescriptor) new ObjectsListNodeDescriptor(ObjectsListConsts.ApplicabilityNodeID, objectsListService), serviceProvider);
      this._childrenView.EditingMode = this.EditMode;
    }
    NodeColumnCollection columnCollection = this.ColumnCollection ?? this._childrenView.Node.GetDefaultColumns(ContentType.NonFolders);
    if (this._savedColumnsSettings != null)
    {
      columnCollection = this.SetSavedSettings(columnCollection, this._savedColumnsSettings);
      this._savedColumnsSettings = (SavedColumnsSettings) null;
    }
    if (this.DefaultSortingColumns != null)
      columnCollection = this.SetOrderAndIndex(columnCollection, this.DefaultSortingColumns);
    this._childrenView.SetColumns(columnCollection, false);
    objectsListService.Columns = this._childrenView.GetNodeColumns();
    if (this._childrenView.Node is IContextAware node && node.Services is AdvancedServiceContainer services && !(services.GetService(typeof (ObjectsListService)) is ObjectsListService))
      services.AddService(typeof (ObjectsListService), (object) objectsListService);
    this._childrenView.SelectedItemsChanged += new EventHandler(this.On_childrenView_SelectedItemsChanged);
    this._isSubscribeSelectedItemsChanged = true;
    if (this.SourceCtrl != null)
    {
      this.OnsourceCtrl_SelectedItemsChanged((object) this.SourceCtrl, EventArgs.Empty);
    }
    else
    {
      this._childrenView._dataLoaded = false;
      this._childrenView.Activate((IView) null);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnsourceCtrl_SelectedItemsChanged(object sender, EventArgs e)
  {
    if (!(sender is ObjectsList objectsList))
      return;
    this.ReleaseChildrenView();
    if (this._childrenView.Node != null)
      this._childrenView.Node.Refresh();
    System.Collections.Generic.List<long> selecetdItemsIds = objectsList.SelecetdItemsIDs;
    long objID = selecetdItemsIds.Count == 1 ? selecetdItemsIds[0] : 0L;
    if (!(this._childrenView.Node is IContextAware node))
      return;
    if (node.Services is AdvancedServiceContainer services)
    {
      if (services.GetService(typeof (ObjectsListService)) is ObjectsListService objectsListService)
      {
        objectsListService.ObjectID = objID;
      }
      else
      {
        objectsListService = new ObjectsListService(this._selectionID, objID, this._objTypeID, this._relTypeID, RelatedObjectsRole.Composition, this.ColumnCollection);
        services.AddService(typeof (ObjectsListService), (object) objectsListService);
      }
      if (this._listContext == ListContext.Composition)
      {
        System.Collections.Generic.List<INodeID> selectedNodeIds = objectsList.ChildrenView.SelectedNodeIDs;
        if (selectedNodeIds != null && selectedNodeIds.Count == 1)
        {
          NodeIDPath parentPath = !(objectsList.SelectedItems is ChildrenViewSelectedItems selectedItems) || selectedItems.Count != 1 ? (NodeIDPath) null : selectedItems.GetParentPath(0);
          if (parentPath != null)
            this._childrenView.Initialize(parentPath, (INode) new ObjectsListVirtualNode(ObjectsListConsts.CompositionNodeID, objectsListService), selectedNodeIds[0], (System.IServiceProvider) this._childrenView.Services);
        }
      }
    }
    this._childrenView.Activate((IView) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTabPage_ParentChanged(object sender, EventArgs e)
  {
    if (!(sender is TabPage))
      return;
    this.Unsubscribe();
    this.SubscribeLoadData(this.Parent);
    this.SubscribeFormDeactivate(this.Parent);
  }

  /// <summary>Событие, возникающее при деактивации вьюшки.</summary>
  /// <remark>
  /// Событие исходит от формы.
  /// Но на событие должны давать возможность подписываться только контролы, которые могут быть контейнерами контролов.
  /// Необходимость возникла из-за случая, когда во время деактивации вьюшки нужно провести деактивацию контрола.
  /// Поэтому, если контрол лежит на форме, то он получает сообщение от самой формы, а если контрол лежит на другом контроле, то он получает сообщение от родителя, а родитель в итоге от формы.
  /// </remark>
  public event EventHandler FormDeactivate
  {
    add
    {
      if (!this.CanContainsChildren)
        return;
      this._formDeactivate += value;
    }
    remove
    {
      if (!this.CanContainsChildren)
        return;
      this._formDeactivate -= value;
    }
  }

  /// <summary>Загрузка данных завершена.</summary>
  public event EventHandler LoadDataCompleted
  {
    add
    {
      if (!this.CanContainsChildren)
        return;
      this._loadDataCompleted += value;
    }
    remove
    {
      if (!this.CanContainsChildren)
        return;
      this._loadDataCompleted -= value;
    }
  }

  /// <summary>Возможность контрола иметь дочерние контролы.</summary>
  public bool CanContainsChildren { get; private set; }

  /// <summary>Изменение родительского контрола.</summary>
  /// <param name="e"></param>
  protected override void OnParentChanged(EventArgs e)
  {
    base.OnParentChanged(e);
    this.Unsubscribe();
    this.SubscribeLoadData(this.Parent);
    this.SubscribeFormDeactivate(this.Parent);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="srvProvider"></param>
  private void CorrectServices(System.IServiceProvider srvProvider)
  {
    if (srvProvider == null || !(srvProvider.GetService(typeof (SelectionOptionsHolder)) is SelectionOptionsHolder service))
      return;
    if (this.DisableColumnsGrouping)
      service.Options |= SelectionOptions.HideViewsGroupingBox;
    else if ((service.Options & SelectionOptions.HideViewsGroupingBox) != (SelectionOptions) 0)
      service.Options ^= SelectionOptions.HideViewsGroupingBox;
    if (!this.ShowStatusBar)
      service.Options |= SelectionOptions.HideViewsStatusBar;
    else if ((service.Options & SelectionOptions.HideViewsStatusBar) != (SelectionOptions) 0)
      service.Options ^= SelectionOptions.HideViewsStatusBar;
    service.Options |= SelectionOptions.HideViewsToolbar;
  }

  private void CorrectServices(System.IServiceProvider provider, ObjectsViewBase childrenView)
  {
    SelectionOptions options = SelectionOptions.Default;
    if (provider != null && provider.GetService(typeof (SelectionOptionsHolder)) is SelectionOptionsHolder service)
      options = service.Options;
    if (childrenView.Services == null)
      return;
    if (!(childrenView.Services.GetService(typeof (SelectionOptionsHolder)) is SelectionOptionsHolder))
      childrenView.Services.AddService(typeof (SelectionOptionsHolder), (object) new SelectionOptionsHolder(options));
    this.CorrectServices((System.IServiceProvider) childrenView.Services);
  }

  /// <summary>Обнуление данных в ChildrenView.</summary>
  private void ReleaseChildrenView()
  {
    this._childrenView.Deactivate((IView) null);
    this._childrenView._dataLoaded = false;
  }

  /// <summary>
  /// Выставление сохраненных настроек для колонок ChildrenView.
  /// </summary>
  /// <param name="currentColumns">Текущий набор колонок</param>
  /// <param name="settings">Настройки</param>
  /// <returns>Измененный набор колонок</returns>
  private NodeColumnCollection SetSavedSettings(
    NodeColumnCollection currentColumns,
    SavedColumnsSettings settings)
  {
    foreach (NodeColumn currentColumn in (System.Collections.Generic.List<NodeColumn>) currentColumns)
    {
      int columnsWidth = settings.GetColumnsWidth(currentColumn.Attribute.AttributeID);
      if (columnsWidth >= 0)
        currentColumn.Width = columnsWidth;
    }
    return currentColumns;
  }

  /// <summary>
  /// Выставление сортировки по умолчанию для колонок ChildrenView.
  /// </summary>
  /// <param name="currentColumns">Текущий набор колонок</param>
  /// <param name="defaultSortingColumns">Колонки с настройками сортировки</param>
  /// <returns>Измененный набор колонок</returns>
  private NodeColumnCollection SetOrderAndIndex(
    NodeColumnCollection currentColumns,
    NodeColumnCollection defaultSortingColumns)
  {
    foreach (NodeColumn currentColumn in (System.Collections.Generic.List<NodeColumn>) currentColumns)
    {
      NodeColumn nodeColumn = defaultSortingColumns.Find(currentColumn.Key);
      if (nodeColumn != null)
      {
        currentColumn.SortIndex = nodeColumn.SortIndex;
        currentColumn.SortOrder = nodeColumn.SortOrder;
      }
      else
      {
        currentColumn.SortOrder = NodeColumnSortOrder.None;
        currentColumn.SortIndex = -1;
      }
    }
    return currentColumns;
  }

  /// <summary>
  /// 
  /// </summary>
  private void ResetObjectTypesGuid() => this.ObjectTypesGuid = Guid.Empty;

  /// <summary>
  /// 
  /// </summary>
  private void ResetRelationsTypeGuid() => this.RelationsTypeGuid = Guid.Empty;

  /// <summary>
  /// 
  /// </summary>
  private void ResetSelectionGuid() => this.SelectionGuid = Guid.Empty;

  /// <summary>Необходимость сериализации свойства ObjectTypesGuid.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeObjectTypesGuid() => this.ObjectTypesGuid != Guid.Empty;

  /// <summary>Необходимость сериализации свойства ObjectsTypeID.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeObjectsTypeID() => false;

  /// <summary>Необходимость сериализации свойства RelationsTypeID.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeRelationsTypeID() => false;

  /// <summary>
  /// Необходимость сериализации свойства RelationsTypeGuid.
  /// </summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeRelationsTypeGuid() => this._relTypeGuid != Guid.Empty;

  /// <summary>Необходимость сериализации свойства SelectionID.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeSelectionID() => false;

  /// <summary>Необходимость сериализации свойства SelectionGuid.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeSelectionGuid() => this._selectionGuid != Guid.Empty;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="parent"></param>
  private void SubscribeFormDeactivate(Control parent)
  {
    switch (parent)
    {
      case IFormDesignerControl formDesignerControl:
        if (this._parent == null)
          this._parent = formDesignerControl;
        this._parent.FormDeactivate += new EventHandler(this.OnFormDeactivate);
        break;
      case TabPage tabPage:
        if (tabPage.Parent == null)
        {
          if (this._isSubscribeOnTabPageParentChanged)
            break;
          tabPage.ParentChanged += new EventHandler(this.OnTabPage_ParentChanged);
          this._isSubscribeOnTabPageParentChanged = true;
          break;
        }
        this.SubscribeFormDeactivate(tabPage.Parent);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="parent"></param>
  private void SubscribeLoadData(Control parent)
  {
    switch (parent)
    {
      case IFormDesignerControl formDesignerControl:
        if (this._parent == null)
          this._parent = formDesignerControl;
        this._parent.LoadDataCompleted += new EventHandler(this.OnLoadDataCompleted);
        break;
      case TabPage tabPage:
        if (tabPage.Parent == null)
        {
          if (this._isSubscribeOnTabPageParentChanged)
            break;
          tabPage.ParentChanged += new EventHandler(this.OnTabPage_ParentChanged);
          this._isSubscribeOnTabPageParentChanged = true;
          break;
        }
        this.SubscribeLoadData(tabPage.Parent);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void Unsubscribe()
  {
    if (this._parent == null)
      return;
    this._parent.LoadDataCompleted -= new EventHandler(this.OnLoadDataCompleted);
    this._parent.FormDeactivate -= new EventHandler(this.OnFormDeactivate);
    this._parent = (IFormDesignerControl) null;
    this._isSubscribeOnTabPageParentChanged = false;
  }

  /// <summary>Запомнить считанные настройки колонок ChildrenView.</summary>
  /// <param name="settings">Настройки колонок</param>
  internal void SetSavedSettings(SavedColumnsSettings settings)
  {
    this._savedColumnsSettings = settings;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.SourceCtrl = (ObjectsList) null;
      if (this._childrenView != null)
      {
        try
        {
          this._childrenView.SelectedItemsChanged -= new EventHandler(this.On_childrenView_SelectedItemsChanged);
        }
        catch
        {
        }
      }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectsList));
    this._childrenView = new ObjectsViewBase();
    this.SuspendLayout();
    this._childrenView.AllowCustomGroupValues = true;
    this._childrenView.Control = (object) this._childrenView;
    this._childrenView.DisableColumnsGrouping = true;
    this._childrenView.DisableColumnsSettings = true;
    this._childrenView.DisableFiltration = true;
    this._childrenView.DisableKeyDownEvents = false;
    this._childrenView.DisableToolBar = true;
    componentResourceManager.ApplyResources((object) this._childrenView, "_childrenView");
    this._childrenView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._childrenView.Name = "_childrenView";
    this.Controls.Add((Control) this._childrenView);
    this.DoubleBuffered = true;
    this.Name = nameof (ObjectsList);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ResumeLayout(false);
  }
}
