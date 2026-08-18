// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DocumentTypesWeightsEditor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using Intermech.AVS.Properties;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Редактирование "весов" типов объектов-документов, которые можно добавлять
/// в раздел "Документация" спецификации
/// </summary>
public class DocumentTypesWeightsEditor : UserControl
{
  /// <summary>Корневой узел в дереве</summary>
  protected List<object> rootItem = new List<object>();
  /// <summary>
  /// Свойство позволяет узнать, можно ли выполнять редактирование "веса" в списке типов объектов-документов
  /// </summary>
  protected bool readOnly = true;
  /// <summary>
  /// Свойство позволяет узнать, были ли изменения в редактируемой коллекции
  /// </summary>
  protected bool isChanged;
  /// <summary>
  /// Коллекция типов объектов-документов, которая редактируется в данном элементе управления
  /// </summary>
  protected DocumentTypeWeightCollection items = new DocumentTypeWeightCollection();
  /// <summary>
  /// Коллекция значков для типов объектов
  /// [(Int32)Идентификатор типа объекта] = [(Icon)Значок]
  /// </summary>
  protected Dictionary<int, Icon> typesIcons = new Dictionary<int, Icon>();
  /// <summary>Сервис именованных изображений</summary>
  protected INamedImageList images;
  /// <summary>Коллекция изображений для разных категорий</summary>
  protected ICategoryTypeIconService objtypesIcons;
  /// <summary>Кэш графических объектов "Навигатора"</summary>
  protected INavGraphicsCache navGraphicsCache;
  /// <summary>Текущий пользователь и его роль</summary>
  protected ICurrentUserAndRole userRole;
  /// <summary>Служба уведомлений</summary>
  protected INotificationService notificationSvc;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  protected NotificationEventHandler notifyHandler;
  /// <summary>Обработчик событий от Bars</summary>
  protected EventHandler barEventsHandler;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelMain;
  private Intermech.Bars.ToolBar toolBarRight;
  private ImageList imagesToolbars;
  private Intermech.VirtualTreeView.VirtualTreeView treeObjectsTypes;
  protected Column columnObjectTypes;
  private ButtonItem btnTypeUp;
  private ButtonItem btnTypeDown;
  private ButtonItem btnTypeTop;
  private ButtonItem btnTypeBottom;
  private MenuBar menuObjectTypes;
  private ContextMenuBarItem contextMenuObjects;
  private MenuButtonItem mnpTypeUp;
  private MenuButtonItem mnpTypeDown;
  private MenuButtonItem mnpTypeTop;
  private MenuButtonItem mnpTypeBottom;
  private MenuButtonItem mnpRefresh;
  private ButtonItem btnRefresh;
  private MenuButtonItem mnpAdd;
  private MenuButtonItem mnpRemove;
  private ButtonItem bAdd;
  private ButtonItem bRemove;

  /// <summary>Конструктор</summary>
  public DocumentTypesWeightsEditor() => this.InitializeComponent();

  /// <summary>Конструктор</summary>
  /// <param name="items">Редактируемая коллекция</param>
  public DocumentTypesWeightsEditor(DocumentTypeWeightCollection items)
  {
    this.InitializeComponent();
    if (this.IsDesignerHosted())
      return;
    this.Init(items);
  }

  /// <summary>
  /// Свойство позволяет узнать, можно ли выполнять редактирование "веса" в списке типов объектов-документов
  /// </summary>
  [Category("Appearance")]
  [Browsable(true)]
  [Description("Свойство позволяет узнать, можно ли выполнять редактирование \"веса\" в списке типов объектов-документов")]
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => this.readOnly;
    [DebuggerStepThrough] set
    {
      this.readOnly = value;
      this.UpdateControls();
      this.RaiseOnChanged();
    }
  }

  /// <summary>
  /// Свойство позволяет узнать, были ли изменения в редактируемой коллекции
  /// </summary>
  [Category("Appearance")]
  [Browsable(true)]
  [Description("Свойство позволяет узнать, были ли изменения в редактируемой коллекции")]
  public virtual bool IsChanged
  {
    [DebuggerStepThrough] get => this.isChanged;
    [DebuggerStepThrough] set
    {
      this.isChanged = value;
      this.UpdateControls();
      this.RaiseOnChanged();
    }
  }

  /// <summary>
  /// Коллекция типов объектов-документов, которая редактируется в данном элементе управления
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("Коллекция типов объектов-документов, которая редактируется в данном элементе управления")]
  public virtual DocumentTypeWeightCollection Items
  {
    [DebuggerStepThrough] get => this.items;
    set => this.Init(value);
  }

  /// <summary>
  /// Событие возникает, если в редакторе "весов" типов объектов происходят изменения
  /// </summary>
  [Description("Событие возникает, если в редакторе \"весов\" типов объектов происходят изменения")]
  public event DocumentTypesWeightsChangedEventHandler OnChanged;

  /// <summary>Сгенерировать событие "OnChanged"</summary>
  protected virtual void RaiseOnChanged()
  {
    DocumentTypesWeightsChangedEventHandler onChanged = this.OnChanged;
    if (onChanged == null)
      return;
    onChanged((object) this, new DocumentTypesWeightsEventArgs(this.items));
  }

  /// <summary>Выполнить инициализацию компонента</summary>
  /// <param name="items">Редактируемая коллекция</param>
  public void Init(DocumentTypeWeightCollection items)
  {
    this.items = new DocumentTypeWeightCollection(items);
    this.isChanged = false;
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service && this.barEventsHandler == null)
    {
      this.barEventsHandler = new EventHandler(this.ToolbarRendererChanged);
      service.RendererChanged += this.barEventsHandler;
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    if (this.images == null)
    {
      this.images = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      this.objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
      this.navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
      this.userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      this.notificationSvc = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    }
    if (this.notifyHandler == null)
    {
      this.notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
      this.notificationSvc.Subscribe(this.notifyHandler);
    }
    this.rootItem.Clear();
    this.rootItem.Add((object) this.items);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet);
      this.Items.SyncMetaData();
      this.FillTree(true);
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad0026f-306c-11d8-b4e9-00304f19f545"), false);
      IDBAttribute attributeById = dbObject?.GetAttributeByID(DocumentTypeWeightHelper.attrDocumentTypesWeights);
      this.readOnly = attributeById == null || attributeById.ReadOnly;
      if (!this.readOnly)
      {
        if (dbObject != null)
        {
          if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
          {
            if (dbObject.CheckoutBy != sessionKeeper.Session.UserID)
              this.readOnly = true;
          }
          else if (dbObject.ObjectModifyMode != ObjectModifyModes.InBase)
            this.readOnly = true;
        }
      }
    }
    this.RaiseOnChanged();
    this.UpdateControls();
  }

  /// <summary>Установить статус всех контролов</summary>
  public virtual void UpdateControls()
  {
    EnabledNodesActions enabledNodesActions = this.EnabledActions();
    this.bAdd.Enabled = !this.readOnly && enabledNodesActions.HasFlag((Enum) EnabledNodesActions.Add);
    this.mnpAdd.Enabled = this.bAdd.Enabled;
    this.bRemove.Enabled = !this.readOnly && enabledNodesActions.HasFlag((Enum) EnabledNodesActions.Remove);
    this.mnpRemove.Enabled = this.bRemove.Enabled;
    this.btnTypeUp.Enabled = !this.readOnly && (enabledNodesActions & EnabledNodesActions.MoveUp) > EnabledNodesActions.None;
    this.mnpTypeUp.Enabled = this.btnTypeUp.Enabled;
    this.btnTypeDown.Enabled = !this.readOnly && (enabledNodesActions & EnabledNodesActions.MoveDown) > EnabledNodesActions.None;
    this.mnpTypeDown.Enabled = this.btnTypeDown.Enabled;
    this.btnTypeTop.Enabled = !this.readOnly && (enabledNodesActions & EnabledNodesActions.MoveTop) > EnabledNodesActions.None;
    this.mnpTypeTop.Enabled = this.btnTypeTop.Enabled;
    this.btnTypeBottom.Enabled = !this.readOnly && (enabledNodesActions & EnabledNodesActions.MoveBottom) > EnabledNodesActions.None;
    this.mnpTypeBottom.Enabled = this.btnTypeBottom.Enabled;
    this.btnRefresh.Enabled = true;
    this.mnpRefresh.Enabled = this.btnRefresh.Enabled;
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Icon GetObjTypeIcon(int objTypeID, Color backColor)
  {
    if (!MetaDataHelper.ExistsObjectType(objTypeID))
      return (Icon) null;
    objTypeID = Math.Max(objTypeID, -1);
    if (this.typesIcons.ContainsKey(objTypeID))
      return this.typesIcons[objTypeID];
    if (this.objtypesIcons.IndexOf(4, objTypeID) < 0)
      return (Icon) null;
    Icon objTypeIcon = ImagesResizeHelper.ResizeIconTo32x16(this.objtypesIcons.GetIcon(4, objTypeID), backColor);
    this.typesIcons.Add(objTypeID, objTypeIcon);
    return objTypeIcon;
  }

  /// <summary>Восстановить список выделенных узлов в дереве</summary>
  /// <param name="parentRow">Строка, с которой надо начинать поиск</param>
  /// <param name="selection">Список выделенных данных</param>
  protected virtual void RestoreSelection(Row parentRow, List<DocumentTypeWeight> selection)
  {
    if (selection == null || selection.Count == 0)
      return;
    parentRow = parentRow ?? this.treeObjectsTypes.RootRow;
    if (parentRow.Item is DocumentTypeWeight documentTypeWeight && selection.Contains(documentTypeWeight))
    {
      this.treeObjectsTypes.SelectedRows.Add(parentRow);
      parentRow.EnsureVisible();
    }
    for (int childIndex = 0; childIndex < parentRow.NumChildren; ++childIndex)
      this.RestoreSelection(parentRow.ChildRowByIndex(childIndex), selection);
  }

  /// <summary>
  /// Получить список действий, разрешённых над текущей коллекцией выделенных узлов
  /// </summary>
  /// <returns>Список действий, разрешённых над текущей коллекцией выделенных узлов</returns>
  protected virtual EnabledNodesActions EnabledActions()
  {
    EnabledNodesActions enabledAction;
    this.SelectedItems(out enabledAction);
    return enabledAction;
  }

  /// <summary>
  /// Получить список данных в выделенных узлах дерева, которые принадлежат одному родительскому узлу
  /// (родительский узел получаем у первого выделенного в дереве узла)
  /// </summary>
  /// <returns>Список данных в выделенных узлах дерева, которые принадлежат одному родительскому узлу</returns>
  protected virtual List<DocumentTypeWeight> SelectedItemsData()
  {
    List<DocumentTypeWeight> documentTypeWeightList = new List<DocumentTypeWeight>(this.treeObjectsTypes.SelectedRows.Count);
    for (int index = 0; index < this.treeObjectsTypes.SelectedRows.Count; ++index)
    {
      if (this.treeObjectsTypes.SelectedRows[index].Item is DocumentTypeWeight documentTypeWeight)
        documentTypeWeightList.Add(documentTypeWeight.Clone() as DocumentTypeWeight);
    }
    return documentTypeWeightList;
  }

  /// <summary>
  /// Получить список выделенных узлов дерева, которые принадлежат одному родительскому узлу
  /// (родительский узел получаем у первого выделенного в дереве узла)
  /// </summary>
  /// <returns>Список выделенных узлов дерева, которые принадлежат одному родительскому узлу</returns>
  protected virtual List<Row> SelectedItems() => this.SelectedItems(out EnabledNodesActions _);

  /// <summary>
  /// Получить список выделенных узлов дерева, которые принадлежат одному родительскому узлу
  /// (родительский узел получаем у первого выделенного в дереве узла)
  /// </summary>
  /// <param name="enabledAction">Перечень допустимых операций над указанной коллекцией узлов</param>
  /// <returns>Список выделенных узлов дерева, которые принадлежат одному родительскому узлу</returns>
  protected virtual List<Row> SelectedItems(out EnabledNodesActions enabledAction)
  {
    enabledAction = EnabledNodesActions.None;
    List<Row> rowList = new List<Row>();
    if (this.treeObjectsTypes.SelectedRows.Count == 0)
    {
      enabledAction |= EnabledNodesActions.Add;
      return rowList;
    }
    Row parentRow = this.treeObjectsTypes.SelectedRows[0].ParentRow;
    for (int index = 0; index < this.treeObjectsTypes.SelectedRows.Count; ++index)
    {
      if (this.treeObjectsTypes.SelectedRows[index].ParentRow == parentRow && this.treeObjectsTypes.SelectedRows[index].Item is DocumentTypeWeight)
        rowList.Add(this.treeObjectsTypes.SelectedRows[index]);
    }
    if (parentRow == null || !(parentRow.Item is DocumentTypeWeight))
    {
      enabledAction |= EnabledNodesActions.Add;
      enabledAction |= EnabledNodesActions.Remove;
    }
    rowList.Sort((IComparer<Row>) new RowsComparer());
    if (this.ReadOnly || rowList.Count == 0)
      return rowList;
    int childIndex1 = rowList[0].ChildIndex;
    int childIndex2 = rowList[rowList.Count - 1].ChildIndex;
    if (parentRow != null && childIndex1 > 0)
      enabledAction = enabledAction | EnabledNodesActions.MoveUp | EnabledNodesActions.MoveTop;
    if (parentRow != null && childIndex2 < parentRow.NumChildren - 1)
      enabledAction = enabledAction | EnabledNodesActions.MoveDown | EnabledNodesActions.MoveBottom;
    return rowList;
  }

  /// <summary>Заполнить дерево</summary>
  /// <param name="resetDatasource">Переназначать источник данных</param>
  protected virtual void FillTree(bool resetDatasource)
  {
    if (resetDatasource)
      this.treeObjectsTypes.DataSource = (object) this.rootItem;
    this.treeObjectsTypes.UpdateRows(true);
    this.UpdateControls();
  }

  /// <summary>Получить данные о строке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (!(e.Row.Item is DocumentTypeWeight))
      return;
    DocumentTypeWeight documentTypeWeight = e.Row.Item as DocumentTypeWeight;
    e.RowData.Icon = this.GetObjTypeIcon(documentTypeWeight.DocumentTypeID, Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue));
    e.RowData.IconSize = e.RowData.Icon.Width;
  }

  /// <summary>Получить данные о дочерних узлах</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Item is List<object>)
      e.Children = (IList) (e.Row.Item as List<object>)[e.Row.ChildIndex];
    if (!(e.Row.Item is DocumentTypeWeight))
      return;
    DocumentTypeWeight documentTypeWeight = e.Row.Item as DocumentTypeWeight;
    e.Children = (IList) documentTypeWeight.Items;
  }

  /// <summary>Получить данные о ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (!(e.Row.Item is DocumentTypeWeight))
      return;
    string objectTypeName = MetaDataHelper.GetObjectTypeName((e.Row.Item as DocumentTypeWeight).DocumentTypeID);
    e.CellData.Value = (object) objectTypeName;
  }

  /// <summary>Изменилась выделенная или сфокусированная строка</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_SelectionChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Отобразить контекстное меню</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeParentObjects_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this.UpdateControls();
    this.contextMenuObjects.Show((Control) this.treeObjectsTypes, e.Location);
  }

  /// <summary>Переместить выделенные типы объектов вверх по дереву</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMoveUp(object sender, EventArgs e)
  {
    EnabledNodesActions enabledAction;
    List<Row> rowList = this.SelectedItems(out enabledAction);
    if (rowList.Count == 0 || (enabledAction & EnabledNodesActions.MoveUp) == EnabledNodesActions.None)
      return;
    for (int index = 0; index < rowList.Count; ++index)
      (rowList[index].Item as DocumentTypeWeight).Owner.Shift(rowList[index].ChildIndex, -1);
    this.FillTree(false);
    this.IsChanged = true;
  }

  /// <summary>Переместить выделенные типы объектов вниз по дереву</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMoveDown(object sender, EventArgs e)
  {
    EnabledNodesActions enabledAction;
    List<Row> rowList = this.SelectedItems(out enabledAction);
    if (rowList.Count == 0 || (enabledAction & EnabledNodesActions.MoveDown) == EnabledNodesActions.None)
      return;
    for (int index = rowList.Count - 1; index >= 0; --index)
      (rowList[index].Item as DocumentTypeWeight).Owner.Shift(rowList[index].ChildIndex, 1);
    this.FillTree(false);
    this.IsChanged = true;
  }

  /// <summary>
  /// Переместить выделенные типы объектов вверх к началу уровня
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMoveTop(object sender, EventArgs e)
  {
    EnabledNodesActions enabledAction;
    List<Row> rowList = this.SelectedItems(out enabledAction);
    if (rowList.Count == 0 || (enabledAction & EnabledNodesActions.MoveTop) == EnabledNodesActions.None)
      return;
    for (int index = 0; index < rowList.Count; ++index)
      (rowList[index].Item as DocumentTypeWeight).Owner.Shift(rowList[index].ChildIndex, -2147483647 /*0x80000001*/);
    this.FillTree(false);
    this.IsChanged = true;
  }

  /// <summary>
  /// Переместить выделенные типы объектов вниз к концу уровня
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMoveBottom(object sender, EventArgs e)
  {
    EnabledNodesActions enabledAction;
    List<Row> rowList = this.SelectedItems(out enabledAction);
    if (rowList.Count == 0 || (enabledAction & EnabledNodesActions.MoveBottom) == EnabledNodesActions.None)
      return;
    for (int index = rowList.Count - 1; index >= 0; --index)
      (rowList[index].Item as DocumentTypeWeight).Owner.Shift(rowList[index].ChildIndex, 2147483646);
    this.FillTree(false);
    this.IsChanged = true;
  }

  /// <summary>
  /// Обновить дерево типов объектов (синхронизировать с кэшем метаданных)
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoRefresh(object sender, EventArgs e)
  {
    List<DocumentTypeWeight> selection = this.SelectedItemsData();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet, true);
    this.Items.SyncMetaData();
    this.Init(this.Items);
    this.RestoreSelection((Row) null, selection);
    this.IsChanged = !this.ReadOnly;
    this.UpdateControls();
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this.toolBarRight.Renderer = renderer;
    this.menuObjectTypes.Renderer = renderer;
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    DBObjectsEventArgs objectsEventArgs = e as DBObjectsEventArgs;
    DBObjectsCheckOutEventArgs checkOutEventArgs = e as DBObjectsCheckOutEventArgs;
    bool flag = false;
    if (objectsEventArgs != null && objectsEventArgs.ObjectIDs != null && (objectsEventArgs.EventName == "ObjectsChanged" || objectsEventArgs.EventName == "ObjectsCheckedIn" || objectsEventArgs.EventName == "ObjectsChangesCancelled"))
      flag = objectsEventArgs.ObjectIDs.Contains(DocumentTypeWeightHelper.objectCommonSpecificationsTemplate) || objectsEventArgs.ObjectIDs.Contains(-DocumentTypeWeightHelper.objectCommonSpecificationsTemplate);
    if (checkOutEventArgs != null && checkOutEventArgs.NewObjectIDs != null && checkOutEventArgs.EventName == "ObjectsCheckedOut")
      flag = checkOutEventArgs.NewObjectIDs.Contains(DocumentTypeWeightHelper.objectCommonSpecificationsTemplate) || checkOutEventArgs.NewObjectIDs.Contains(-DocumentTypeWeightHelper.objectCommonSpecificationsTemplate);
    if (!flag)
      return;
    this.Init(this.Items);
  }

  private void DoAdd(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Выберите необходимый тип объекта\t", typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    DocumentTypeWeight documentTypeWeight = new DocumentTypeWeight((int) selectorForm.IDList[0]);
    documentTypeWeight.SyncMetaData();
    this.items.Add(documentTypeWeight);
    this.FillTree(false);
    this.IsChanged = true;
  }

  private void DoRemove(object sender, EventArgs e)
  {
    EnabledNodesActions enabledAction;
    List<Row> rowList = this.SelectedItems(out enabledAction);
    if (rowList.Count == 0 || !enabledAction.HasFlag((Enum) EnabledNodesActions.Remove) || !(rowList[0].Item is DocumentTypeWeight documentTypeWeight) || !this.items.Contains(documentTypeWeight))
      return;
    this.items.Remove(documentTypeWeight);
    this.FillTree(false);
    this.IsChanged = true;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.notifyHandler != null)
      {
        this.notificationSvc.Unsubscribe(this.notifyHandler);
        this.notifyHandler = (NotificationEventHandler) null;
      }
      if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
      {
        this.toolBarRight.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        this.menuObjectTypes.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
        service.RendererChanged -= this.barEventsHandler;
        this.barEventsHandler = (EventHandler) null;
      }
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentTypesWeightsEditor));
    this.panelMain = new Panel();
    this.treeObjectsTypes = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnObjectTypes = new Column();
    this.menuObjectTypes = new MenuBar();
    this.imagesToolbars = new ImageList(this.components);
    this.contextMenuObjects = new ContextMenuBarItem();
    this.mnpAdd = new MenuButtonItem();
    this.mnpRemove = new MenuButtonItem();
    this.mnpTypeUp = new MenuButtonItem();
    this.mnpTypeDown = new MenuButtonItem();
    this.mnpTypeTop = new MenuButtonItem();
    this.mnpTypeBottom = new MenuButtonItem();
    this.mnpRefresh = new MenuButtonItem();
    this.toolBarRight = new Intermech.Bars.ToolBar();
    this.bAdd = new ButtonItem();
    this.bRemove = new ButtonItem();
    this.btnTypeUp = new ButtonItem();
    this.btnTypeDown = new ButtonItem();
    this.btnTypeTop = new ButtonItem();
    this.btnTypeBottom = new ButtonItem();
    this.btnRefresh = new ButtonItem();
    this.panelMain.SuspendLayout();
    this.treeObjectsTypes.BeginInit();
    this.SuspendLayout();
    this.panelMain.Controls.Add((Control) this.treeObjectsTypes);
    this.panelMain.Controls.Add((Control) this.menuObjectTypes);
    this.panelMain.Controls.Add((Control) this.toolBarRight);
    componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
    this.panelMain.Name = "panelMain";
    this.treeObjectsTypes.AllowDrop = true;
    this.treeObjectsTypes.AllowIndividualRowResize = false;
    this.treeObjectsTypes.AllowRowResize = false;
    this.treeObjectsTypes.AllowUserPinnedColumns = false;
    this.treeObjectsTypes.AutoFitColumns = true;
    this.treeObjectsTypes.Columns.Add(this.columnObjectTypes);
    this.treeObjectsTypes.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeObjectsTypes, "treeObjectsTypes");
    this.treeObjectsTypes.ImageList = (ImageList) null;
    this.treeObjectsTypes.LineStyle = LineStyle.Dot;
    this.treeObjectsTypes.MainColumn = this.columnObjectTypes;
    this.treeObjectsTypes.Name = "treeObjectsTypes";
    this.treeObjectsTypes.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.treeObjectsTypes.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.treeObjectsTypes.SelectBeforeEdit = true;
    this.treeObjectsTypes.ShowRootRow = false;
    this.treeObjectsTypes.SuppressErrorMessages = true;
    this.treeObjectsTypes.ShowContextMenu += new MouseEventHandler(this.treeParentObjects_ShowContextMenu);
    this.treeObjectsTypes.FocusRowChanged += new EventHandler(this.treeParentObjects_SelectionChanged);
    this.treeObjectsTypes.GetCellData += new GetCellDataHandler(this.treeParentObjects_GetCellData);
    this.treeObjectsTypes.GetChildren += new GetChildrenHandler(this.treeParentObjects_GetChildren);
    this.treeObjectsTypes.GetRowData += new GetRowDataHandler(this.treeParentObjects_GetRowData);
    this.treeObjectsTypes.SelectionChanged += new EventHandler(this.treeParentObjects_SelectionChanged);
    this.columnObjectTypes.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnObjectTypes, "columnObjectTypes");
    this.columnObjectTypes.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnObjectTypes.HeaderStyle.HorzAlignment");
    this.columnObjectTypes.Movable = false;
    this.columnObjectTypes.Name = "columnObjectTypes";
    this.columnObjectTypes.Sortable = false;
    componentResourceManager.ApplyResources((object) this.menuObjectTypes, "menuObjectTypes");
    this.menuObjectTypes.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuObjectTypes.Hidden = false;
    this.menuObjectTypes.ImageList = this.imagesToolbars;
    this.menuObjectTypes.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuObjects
    });
    this.menuObjectTypes.Name = "menuObjectTypes";
    this.menuObjectTypes.OwnerForm = (Form) null;
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_up_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "arrow_down_blue.ico");
    this.imagesToolbars.Images.SetKeyName(2, "arrow_top_blue.ico");
    this.imagesToolbars.Images.SetKeyName(3, "arrow_bottom_blue.ico");
    this.imagesToolbars.Images.SetKeyName(4, "refresh.png");
    componentResourceManager.ApplyResources((object) this.contextMenuObjects, "contextMenuObjects");
    this.contextMenuObjects.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this.mnpAdd,
      (ToolbarItemBase) this.mnpRemove,
      (ToolbarItemBase) this.mnpTypeUp,
      (ToolbarItemBase) this.mnpTypeDown,
      (ToolbarItemBase) this.mnpTypeTop,
      (ToolbarItemBase) this.mnpTypeBottom,
      (ToolbarItemBase) this.mnpRefresh
    });
    this.contextMenuObjects.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpAdd, "mnpAdd");
    this.mnpAdd.Image = (Image) Resources.addSmall;
    this.mnpAdd.ShowText = true;
    this.mnpAdd.Click += new EventHandler(this.DoAdd);
    componentResourceManager.ApplyResources((object) this.mnpRemove, "mnpRemove");
    this.mnpRemove.Image = (Image) Resources.deleteSmall;
    this.mnpRemove.ShowText = true;
    this.mnpRemove.Click += new EventHandler(this.DoRemove);
    this.mnpTypeUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpTypeUp, "mnpTypeUp");
    this.mnpTypeUp.ImageIndex = 0;
    this.mnpTypeUp.ShowText = true;
    this.mnpTypeUp.Click += new EventHandler(this.DoMoveUp);
    componentResourceManager.ApplyResources((object) this.mnpTypeDown, "mnpTypeDown");
    this.mnpTypeDown.ImageIndex = 1;
    this.mnpTypeDown.ShowText = true;
    this.mnpTypeDown.Click += new EventHandler(this.DoMoveDown);
    this.mnpTypeTop.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpTypeTop, "mnpTypeTop");
    this.mnpTypeTop.ImageIndex = 2;
    this.mnpTypeTop.ShowText = true;
    this.mnpTypeTop.Click += new EventHandler(this.DoMoveTop);
    componentResourceManager.ApplyResources((object) this.mnpTypeBottom, "mnpTypeBottom");
    this.mnpTypeBottom.ImageIndex = 3;
    this.mnpTypeBottom.ShowText = true;
    this.mnpTypeBottom.Click += new EventHandler(this.DoMoveBottom);
    this.mnpRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpRefresh, "mnpRefresh");
    this.mnpRefresh.ImageIndex = 4;
    this.mnpRefresh.ShowText = true;
    this.mnpRefresh.Click += new EventHandler(this.DoRefresh);
    this.toolBarRight.AddRemoveButtonsVisible = false;
    this.toolBarRight.AllowHorizontalDock = false;
    componentResourceManager.ApplyResources((object) this.toolBarRight, "toolBarRight");
    this.toolBarRight.DockLine = 3;
    this.toolBarRight.DrawActionsButton = false;
    this.toolBarRight.Flow = ToolBarLayout.Vertical;
    this.toolBarRight.FullMenus = true;
    this.toolBarRight.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarRight.Hidden = false;
    this.toolBarRight.ImageList = this.imagesToolbars;
    this.toolBarRight.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this.bAdd,
      (ToolbarItemBase) this.bRemove,
      (ToolbarItemBase) this.btnTypeUp,
      (ToolbarItemBase) this.btnTypeDown,
      (ToolbarItemBase) this.btnTypeTop,
      (ToolbarItemBase) this.btnTypeBottom,
      (ToolbarItemBase) this.btnRefresh
    });
    this.toolBarRight.MinimumFloatingSize = new Size(250, 30);
    this.toolBarRight.Name = "toolBarRight";
    this.toolBarRight.Overflow = ToolBarOverflow.Wrap;
    this.toolBarRight.Stretch = true;
    this.toolBarRight.Tearable = false;
    componentResourceManager.ApplyResources((object) this.bAdd, "bAdd");
    this.bAdd.Image = (Image) Resources.addSmall;
    this.bAdd.Click += new EventHandler(this.DoAdd);
    componentResourceManager.ApplyResources((object) this.bRemove, "bRemove");
    this.bRemove.Image = (Image) Resources.deleteSmall;
    this.bRemove.Click += new EventHandler(this.DoRemove);
    this.btnTypeUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnTypeUp, "btnTypeUp");
    this.btnTypeUp.ImageIndex = 0;
    this.btnTypeUp.Click += new EventHandler(this.DoMoveUp);
    componentResourceManager.ApplyResources((object) this.btnTypeDown, "btnTypeDown");
    this.btnTypeDown.ImageIndex = 1;
    this.btnTypeDown.Click += new EventHandler(this.DoMoveDown);
    this.btnTypeTop.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnTypeTop, "btnTypeTop");
    this.btnTypeTop.ImageIndex = 2;
    this.btnTypeTop.Click += new EventHandler(this.DoMoveTop);
    componentResourceManager.ApplyResources((object) this.btnTypeBottom, "btnTypeBottom");
    this.btnTypeBottom.ImageIndex = 3;
    this.btnTypeBottom.Click += new EventHandler(this.DoMoveBottom);
    this.btnRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnRefresh, "btnRefresh");
    this.btnRefresh.ImageIndex = 4;
    this.btnRefresh.Click += new EventHandler(this.DoRefresh);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.panelMain);
    this.MinimumSize = new Size(25, 25);
    this.Name = nameof (DocumentTypesWeightsEditor);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.panelMain.ResumeLayout(false);
    this.treeObjectsTypes.EndInit();
    this.ResumeLayout(false);
  }
}
