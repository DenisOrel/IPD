
// Type: Intermech.Navigator.EditingContextsEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Descriptos;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Управление контекстом редактирования</summary>
public class EditingContextsEditor : UserControl
{
  /// <summary>Режим работы - только чтение</summary>
  private bool _readOnly;
  /// <summary>Показывать связанные контексты</summary>
  private bool _showLinkedContexts;
  /// <summary>Права доступа в редакторе.</summary>
  private EditingContextsAccessRights _accessRights;
  /// <summary>
  /// Коллекция значков для типов объектов
  /// [(Int32)Идентификатор типа объекта] = [(Icon)Значок]
  /// </summary>
  private Dictionary<int, Icon> _typesIcons = new Dictionary<int, Icon>();
  /// <summary>Сервис именованных изображений</summary>
  private INamedImageList _images;
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService _objtypesIcons;
  /// <summary>Кэш графических объектов "Навигатора"</summary>
  private INavGraphicsCache _navGraphicsCache;
  /// <summary>Текущий пользователь и его роль</summary>
  private ICurrentUserAndRole _userRole;
  /// <summary>Кэш имён пользователей</summary>
  private IUserNamesCache _userNamesCache;
  /// <summary>Редактируемая копия контекста</summary>
  private EditingContextsObjectContainer _context = new EditingContextsObjectContainer();
  /// <summary>Зафиксированная копия контекста</summary>
  private EditingContextsObjectContainer _contextSource = new EditingContextsObjectContainer();
  /// <summary>
  /// Является ли текущий контекст редактирования извещением
  /// </summary>
  private bool _isECO;
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider _services;
  /// <summary>Корневой элемент для дерева</summary>
  private List<object> _rootItem = new List<object>(1);
  /// <summary>Есть ли изменения в контексте</summary>
  private bool _isChanged;
  /// <summary>Шрифт в дереве</summary>
  private static Font _treeBoldFont;
  /// <summary>
  /// Идентификатор типа объектов "Контексты редактирования"
  /// </summary>
  private static int _editingContextsType = -1;
  /// <summary>Список колонок для обращения к базе данных</summary>
  private static List<ColumnDescriptor> _columns;
  /// <summary>Журнал событий для контекста редактирования</summary>
  private EditingContextsLog _log = new EditingContextsLog();
  /// <summary>
  /// Строка, на которую "сваливаются" перетаскиваемые объекты
  /// </summary>
  private Row _dropTargetRow;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Column columnCAPTION;
  private Column columnF_OBJECT_ID;
  private Column columnF_VERSION_ID;
  private Column columnLC_STEP_ID;
  private Column columnF_OBJECT_TYPE;
  private Column columnF_OWNER_ID;
  private Column columnF_CHECKEDOUT_BY;
  protected MenuBar menubarContext;
  protected ContextMenuBarItem menuContext;
  protected MenuButtonItem mnpAdd;
  protected MenuButtonItem mnpReplace;
  private ImageList imagesMenus;
  private TabControl pages;
  private TabPage pageContext;
  private TabPage pageVersionsRule;
  private Intermech.Bars.ToolBar menuEditingContextsBar;
  private ButtonItem btnCard;
  private ButtonItem btnAdd;
  private ButtonItem btnReplace;
  private ButtonItem btnDelete;
  private ButtonItem btnRefresh;
  private Intermech.VirtualTreeView.VirtualTreeView treeContexts;
  private HeaderControl headerControl;
  private MenuBar menuBar;
  private ContextMenuBarItem contextMenuBarItem;
  private MenuButtonItem mnpAddCriterion;
  private MenuButtonItem mnpDeleteCriterion;
  private MenuButtonItem mnpAddValue;
  private MenuButtonItem mnpDelValue;
  private MenuButtonItem mnpMoveUp;
  private MenuButtonItem mnpMoveDown;
  private ButtonItem btnOpen;
  private ButtonItem btnShowLinked;
  private ButtonItem btnAddComposition;
  private ButtonItem btnPaste;
  protected MenuButtonItem mnpPaste;
  protected MenuButtonItem mnpAddComposition;
  protected MenuButtonItem mnpCard;
  protected MenuButtonItem mnpOpenInNewWindow;
  protected MenuButtonItem mnpDelete;
  protected MenuButtonItem mnpRefresh;

  /// <summary>Конструктор</summary>
  public EditingContextsEditor()
  {
    this.InitializeComponent();
    this.pages.TabPages.Remove(this.pageVersionsRule);
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    if (EditingContextsEditor._columns == null)
    {
      EditingContextsEditor._columns = new List<ColumnDescriptor>();
      EditingContextsEditor._columns.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.ASC, 0));
      EditingContextsEditor._columns.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.ASC, 1));
      EditingContextsEditor._columns.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
      EditingContextsEditor._columns.Add(new ColumnDescriptor((object) -20, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
      EditingContextsEditor._columns.Add(new ColumnDescriptor((object) -5, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
      EditingContextsEditor._columns.Add(new ColumnDescriptor((object) -16, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
      EditingContextsEditor._columns.Add(new ColumnDescriptor((object) -17, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
      EditingContextsEditor._columns.Add(new ColumnDescriptor((object) -15, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Name, SortOrders.NONE, -1));
    }
    if (ServicesManager.GetService(typeof (IGuidMapper)) is IGuidMapper)
      this.Init();
    this.SelectedItems = this.CreateSelectedItems();
  }

  /// <summary>
  /// Событие возникает, если в редакторе происходят изменения
  /// </summary>
  public event EditingContextsEditor.EditingContextsChangedEventHandler OnChanged;

  /// <summary>Сгенерировать событие "OnChanged"</summary>
  protected virtual void RaiseOnChanged()
  {
    if (this.OnChanged == null)
      return;
    this.OnChanged((object) this, new EventArgs());
  }

  public ISimpleSelectedItems SelectedItems { get; private set; }

  /// <summary>Показывать связанные контексты</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool ShowLinkedContexts
  {
    [DebuggerStepThrough] get => this._showLinkedContexts;
    set
    {
      this._showLinkedContexts = value;
      this.FillEditor(false);
    }
  }

  /// <summary>Режим работы - только чтение</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => this._readOnly;
    set => this._readOnly = value;
  }

  /// <summary>Права доступа к текущему контексту</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual EditingContextsAccessRights AccessRights
  {
    [DebuggerStepThrough] get => this._accessRights;
  }

  /// <summary>
  /// Идентификатор версии объекта текущего контекста редактирования
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual long CurrentContextObjectID
  {
    get => this._context.ContextID;
    set
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.Clear();
        if (!(sessionKeeper.Session.GetObject(value, false) is IDBEditingContextsObject editingContextsObject))
          return;
        this._context.Assign((object) editingContextsObject.GetEditingContextsObjectContainer(true, false));
        this._isECO = MetaDataHelper.IsObjectTypeChildOf(editingContextsObject.ObjectType, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
      }
      this.FillEditor(true);
      this.Fix();
    }
  }

  /// <summary>
  /// Является ли текущий контекст редактирования извещением
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool IsECO
  {
    [DebuggerStepThrough] get => this._isECO;
  }

  /// <summary>Текущий контест редактирования</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal virtual EditingContextsObjectContainer InternalContext => this._context;

  /// <summary>Текущий контест редактирования является упрощённым</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool SimpleContext => this._context.SimpleContext;

  /// <summary>Текущий контест редактирования</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual EditingContextsObjectContainer Context
  {
    get => this._context.Clone() as EditingContextsObjectContainer;
    set
    {
      this._context.Assign((object) value);
      if (this._context.ContextID != 0L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._context.ContextID);
          this._isECO = !objectInfo.Empty && MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
        }
      }
      else
        this._isECO = false;
      this.FillEditor(true);
      this.Fix();
    }
  }

  /// <summary>Контейнер сервисов</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual System.IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  /// <summary>Журнал событий для контекста редактирования</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual EditingContextsLog Log
  {
    [DebuggerStepThrough] get
    {
      if (this._log == null)
        this._log = new EditingContextsLog();
      return this._log;
    }
  }

  /// <summary>Контекст редактирования был изменён</summary>
  [Category("Appearance")]
  [Browsable(true)]
  public virtual bool IsChanged
  {
    [DebuggerStepThrough] get => this._isChanged;
    set
    {
      this._isChanged = value;
      this.RaiseOnChanged();
      this.UpdateControls();
    }
  }

  /// <summary>Скрыть заголовок редактора</summary>
  [Category("Appearance")]
  [Browsable(true)]
  public virtual bool DisableHeader
  {
    [DebuggerStepThrough] get => this.headerControl.Visible;
    set => this.headerControl.Visible = value;
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    this.menuEditingContextsBar.Renderer = (sender as BarManager).Renderer;
  }

  /// <summary>Выполнить инициализацию компонента</summary>
  public virtual void Init()
  {
    if (EditingContextsEditor._editingContextsType == -1)
      EditingContextsEditor._editingContextsType = MetaDataHelper.GetObjectTypeID("cad0146b-306c-11d8-b4e9-00304f19f545");
    this._images = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this._userNamesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
    if (this._images != null)
    {
      this.mnpCard.Image = this._images.ImageList.Images[this._images.ImageIndex("imgCard")];
      this.btnCard.Image = this.mnpCard.Image;
      this.btnOpen.Image = this._images.ImageList.Images[this._images.ImageIndex("imgNavigator")];
      this.mnpOpenInNewWindow.Image = this.btnOpen.Image;
      this.btnPaste.Image = this._images.ImageList.Images[this._images.ImageIndex("imgPaste")];
      this.mnpPaste.Image = this.btnPaste.Image;
    }
    this._isChanged = false;
    this._rootItem.Clear();
    this._rootItem.Add((object) this._context);
    this.treeContexts.AllowDrop = true;
    this.treeContexts.AllowMultiSelect = true;
    this.treeContexts.AllowRowResize = false;
    this.treeContexts.BorderStyle = BorderStyle.Fixed3D;
    this.treeContexts.Columns.Clear();
    this.treeContexts.Columns.Add(this.columnCAPTION);
    this.treeContexts.Columns.Add(this.columnF_OBJECT_ID);
    this.treeContexts.Columns.Add(this.columnF_VERSION_ID);
    this.treeContexts.Columns.Add(this.columnF_OBJECT_TYPE);
    this.treeContexts.Columns.Add(this.columnLC_STEP_ID);
    this.treeContexts.Columns.Add(this.columnF_OWNER_ID);
    this.treeContexts.Columns.Add(this.columnF_CHECKEDOUT_BY);
    this.treeContexts.DisableHeaderContextMenu = true;
    this.treeContexts.LineStyle = LineStyle.Dot;
    this.treeContexts.MainColumn = this.columnCAPTION;
    this.treeContexts.SelectBeforeEdit = true;
    this.treeContexts.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.treeContexts.ShowRootRow = false;
    this.treeContexts.SuppressErrorMessages = true;
    this.treeContexts.FocusRowChanged += new EventHandler(this.treeContexts_FocusRowChanged);
    this.treeContexts.DoubleClick += new EventHandler(this.treeContexts_CellDoubleClick);
    this.treeContexts.GetChildren += new GetChildrenHandler(this.treeContexts_GetChildren);
    this.treeContexts.GetRowData += new GetRowDataHandler(this.treeContexts_GetRowData);
    this.treeContexts.GetCellData += new GetCellDataHandler(this.treeContexts_GetCellData);
    this.treeContexts.SelectionChanged += new EventHandler(this.treeContexts_SelectionChanged);
    this.treeContexts.ShowContextMenu += new MouseEventHandler(this.treeContexts_ShowContextMenu);
    this.treeContexts.DragEnter += new DragEventHandler(this.treeContexts_DragEnter);
    this.treeContexts.DragOver += new DragEventHandler(this.treeContexts_DragOver);
    this.treeContexts.DragDrop += new DragEventHandler(this.treeContexts_DragDrop);
    this.treeContexts.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.treeContexts_GetAllowedRowDropLocations);
    this.treeContexts.GetRowDropEffect += new GetRowDropEffectHandler(this.treeContexts_GetRowDropEffect);
    this.columnCAPTION.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnCAPTION.MinWidth = 100;
    this.columnF_OBJECT_ID.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnF_OBJECT_ID.MinWidth = 90;
    this.columnF_VERSION_ID.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnF_VERSION_ID.MinWidth = 50;
    this.columnF_OBJECT_TYPE.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnF_OBJECT_TYPE.MinWidth = 100;
    this.columnLC_STEP_ID.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnLC_STEP_ID.MinWidth = 100;
    this.columnF_OWNER_ID.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnF_OWNER_ID.MinWidth = 100;
    this.columnF_CHECKEDOUT_BY.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnF_CHECKEDOUT_BY.MinWidth = 100;
    this.treeContexts.DataSource = (object) this._rootItem;
    this.UpdateControls();
  }

  /// <summary>
  /// Выполнить проверку прав доступа к редактируемому контексту
  /// </summary>
  public virtual void CheckAccessRights()
  {
    if (this._context.ContextID == 0L || !EditingContextHelper.CheckEditingContextEditRight(this._context.ContextID))
      return;
    this._accessRights = EditingContextsAccessRights.FullAccess;
  }

  /// <summary>
  /// Получить из дерева описание контекста, которому принадлежит указанная строка
  /// </summary>
  /// <returns>Описание контекста, которому принадлежит указанная строка</returns>
  public virtual ObjectVersionDescription GetContextForRow(Row row)
  {
    if (row == null)
      return (ObjectVersionDescription) null;
    while (row.Level > 1 && row.ParentRow != null)
      row = row.ParentRow;
    return this._context.GetDescription(row == null || !(row.Item is long) ? 0L : (long) row.Item);
  }

  /// <summary>Получить из дерева первый выделенный объект</summary>
  /// <returns>Первый выделенный в дереве объект</returns>
  public virtual ObjectVersionDescription GetSelectedObject()
  {
    ObjectVersionDescription selectedObject = (ObjectVersionDescription) null;
    if (this._context == null || this._context.Descriptions == null || this._context.Descriptions.Count == 0 || this.treeContexts.SelectedRow == null)
      return selectedObject;
    Row selectedRow = this.treeContexts.SelectedRow;
    if (selectedRow.Item is long)
      selectedObject = this._context.GetDescription((long) selectedRow.Item);
    return selectedObject;
  }

  /// <summary>Получить из дерева версию для выделенной строки</summary>
  /// <returns>Версия выделенной строки</returns>
  public virtual EditingContextsObjectVersion GetSelectedObjectVersion()
  {
    EditingContextsObjectVersion selectedObjectVersion = (EditingContextsObjectVersion) null;
    if (this._context == null || this._context.Descriptions == null || this._context.Descriptions.Count == 0 || this.treeContexts.SelectedRow == null)
      return selectedObjectVersion;
    Row selectedRow = this.treeContexts.SelectedRow;
    if (selectedRow.Item is long && selectedRow.Level == 2)
    {
      long verContextID = (long) selectedRow.ParentRow.Item;
      selectedObjectVersion = this._context.GetVersion((long) selectedRow.Item, verContextID);
    }
    return selectedObjectVersion;
  }

  /// <summary>Получить из дерева версию для указанной строки</summary>
  /// <param name="row">Строка</param>
  /// <returns>Версия указанной строки</returns>
  public virtual EditingContextsObjectVersion GetRowObjectVersion(Row row)
  {
    EditingContextsObjectVersion rowObjectVersion = (EditingContextsObjectVersion) null;
    if (this._context == null || this._context.Descriptions == null || this._context.Descriptions.Count == 0 || row == null || !(row.Item is long) || row.Level != 2)
      return rowObjectVersion;
    long verContextID = (long) row.ParentRow.Item;
    rowObjectVersion = this._context.GetVersion((long) row.Item, verContextID);
    return rowObjectVersion;
  }

  /// <summary>Собрать в дереве список всех выделенных объектов</summary>
  /// <returns>Список всех выделенных в дереве объектов</returns>
  public virtual List<ObjectVersionDescription> GetSelectedObjects()
  {
    List<ObjectVersionDescription> selectedObjects = new List<ObjectVersionDescription>();
    if (this._context == null || this._context.Descriptions == null || this._context.Descriptions.Count == 0 || this.treeContexts.SelectedRows.Count == 0)
      return selectedObjects;
    for (int index = 0; index < this.treeContexts.SelectedRows.Count; ++index)
    {
      Row selectedRow = this.treeContexts.SelectedRows[index];
      if (selectedRow.Item is long)
      {
        ObjectVersionDescription description = this._context.GetDescription((long) selectedRow.Item);
        if (description != null)
          selectedObjects.Add(description);
      }
      if (selectedRow.Item is EditingContextsObjectVersion)
      {
        ObjectVersionDescription description = this._context.GetDescription((long) selectedRow.Item);
        if (description != null)
          selectedObjects.Add(description);
      }
    }
    return selectedObjects;
  }

  public event EventHandler SelectedItemsChanged;

  /// <summary>Установить статус всех контролов</summary>
  public virtual void UpdateControls()
  {
    bool flag1 = this._context.ContextID != 0L;
    bool flag2 = !this._readOnly && (this._accessRights & EditingContextsAccessRights.FullAccess) != 0;
    if (!this._readOnly)
    {
      int accessRights = (int) this._accessRights;
    }
    ObjectVersionDescription selectedObject = this.GetSelectedObject();
    EditingContextsObjectVersion selectedObjectVersion = this.GetSelectedObjectVersion();
    bool flag3 = selectedObject != null && MetaDataHelper.IsObjectTypeEditingContext(selectedObject.F_OBJECT_TYPE);
    bool flag4 = selectedObject != null && !flag3 && this._context.ExistsLinkedVersion(selectedObject.F_OBJECT_ID);
    bool flag5 = selectedObjectVersion != null && Math.Abs(selectedObjectVersion.F_CONTEXT_ID) == Math.Abs(this._context.ContextID);
    int num1 = selectedObject == null ? 0 : (flag5 ? 1 : (Math.Abs(selectedObject.F_OBJECT_ID) == Math.Abs(this._context.ContextID) ? 1 : 0));
    int num2 = this._context != null ? (this.btnShowLinked.Checked ? this._context.AllVersionsCount : this._context.ContextVersionsCount) : 0;
    string str = this.SimpleContext ? string.Format(LocalizationHolder.rm.GetString("Client.Core_1446"), (object) num2) : string.Format(LocalizationHolder.rm.GetString("Client.Core_1447"), (object) num2);
    if (this.pageContext.Text != str)
      this.pageContext.Text = str;
    List<ObjectVersionDescription> selectedObjects = this.GetSelectedObjects();
    bool flag6 = false;
    for (int index = 0; index < selectedObjects.Count; ++index)
    {
      if ((selectedObjects[index].Options & ObjectVersionDescriptionOptions.FromECOComposition) == ObjectVersionDescriptionOptions.FromECOComposition)
      {
        flag6 = selectedObjects[index].ECOs != null && selectedObjects[index].ECOs.IndexOf(this._context.ContextID) >= 0;
        break;
      }
    }
    this.btnAdd.Enabled = flag1 & flag2;
    this.btnAdd.Visible = flag2;
    this.mnpAdd.Enabled = this.btnAdd.Enabled;
    this.mnpAdd.Visible = this.btnAdd.Visible;
    this.btnAddComposition.Enabled = this.btnAdd.Enabled;
    this.btnAddComposition.Visible = this.btnAdd.Visible;
    this.mnpAddComposition.Enabled = this.btnAdd.Enabled;
    this.mnpAddComposition.Visible = this.btnAdd.Visible;
    this.btnReplace.Enabled = flag1 & flag2 & flag5 && !flag3 && !flag4 && !flag6;
    this.btnReplace.Visible = flag2;
    this.mnpReplace.Enabled = this.btnReplace.Enabled;
    this.mnpReplace.Visible = this.btnReplace.Visible;
    this.btnDelete.Enabled = flag1 & flag2 & flag5 && !flag3 && !flag6;
    this.btnDelete.Visible = flag2;
    this.mnpDelete.Enabled = this.btnDelete.Enabled;
    this.mnpDelete.Visible = this.btnDelete.Visible;
    this.btnRefresh.Enabled = !this._isChanged;
    this.btnRefresh.Visible = true;
    this.mnpRefresh.Enabled = this.btnRefresh.Enabled;
    this.mnpRefresh.Visible = true;
    this.btnCard.Enabled = selectedObject != null && !this._readOnly;
    this.btnCard.Visible = !this._readOnly;
    this.mnpCard.Enabled = this.btnCard.Enabled;
    this.mnpCard.Visible = this.btnCard.Visible;
    this.btnOpen.Enabled = this.btnCard.Enabled && !this._readOnly;
    this.btnOpen.Visible = !this._readOnly;
    this.mnpOpenInNewWindow.Enabled = this.btnOpen.Enabled;
    this.mnpOpenInNewWindow.Visible = this.btnOpen.Visible;
    this.btnShowLinked.Enabled = flag1 && !this.SimpleContext;
    this.btnShowLinked.Checked = this.ShowLinkedContexts;
    this.btnShowLinked.Visible = !this.SimpleContext;
    this.btnPaste.Enabled = this.btnAdd.Enabled;
    this.btnPaste.Visible = this.btnAdd.Visible;
    this.mnpPaste.Enabled = this.btnPaste.Enabled;
    this.mnpPaste.Visible = this.btnAdd.Visible;
  }

  /// <summary>Очистить редактор</summary>
  public virtual void Clear()
  {
    this._context.Clear();
    this.FillEditor(false);
  }

  /// <summary>Заполнить редактор содержимым контекста</summary>
  /// <param name="checkAccess">Надо ли выполнить проверку прав доступа к объекту</param>
  public virtual void FillEditor(bool checkAccess)
  {
    if (checkAccess)
      this.CheckAccessRights();
    try
    {
      this.treeContexts.DataSource = (object) null;
      this.treeContexts.UpdateRows(true);
    }
    catch
    {
    }
    this._rootItem.Clear();
    if (this._context != null)
    {
      if (!this._readOnly)
      {
        int accessRights = (int) this._accessRights;
      }
      this._rootItem.Add((object) this._context);
      try
      {
        this.treeContexts.DataSource = (object) this._rootItem;
        this.treeContexts.UpdateRows(true);
      }
      catch
      {
      }
      if (this._context.GetContextsID() != null)
        this.treeContexts.RootRow.ExpandChildren(true);
    }
    this.UpdateControls();
    this.RaiseOnChanged();
  }

  /// <summary>
  /// Зафиксировать изменения в редакторе
  /// (в базу данных при этом ничего не вносится)
  /// </summary>
  public void Fix()
  {
    this._contextSource.Assign((object) this._context);
    this._isChanged = false;
    this.UpdateControls();
    this.RaiseOnChanged();
  }

  /// <summary>
  /// Отменить изменения в редакторе
  /// (в базу данных при этом ничего не вносится)
  /// </summary>
  public void Undo()
  {
    this.Context = this._contextSource;
    this.RaiseOnChanged();
  }

  /// <summary>
  /// Добавить в контекст объекты из буфера обмена, а также их составы (при необходимости)
  /// </summary>
  /// <param name="mode">Режим добавления</param>
  /// <param name="silentMode">true - действия выполняются без диалога с пользователем</param>
  public virtual void PasteObjects(EditingContextsCompositionLevel mode, bool silentMode)
  {
    object dataObject = (ServicesManager.GetService(typeof (IClipboard)) as IClipboard).GetDataObject();
    if (dataObject == null || !(dataObject is DBObjectTypedIDCollection))
    {
      if (silentMode)
        return;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1448"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      if (!(dataObject is IDBObjectTypedIDCollection typedIdCollection))
        return;
      this.AddObjects((IList<IDBTypedObjectID>) typedIdCollection.GetTypedObjects(), mode, silentMode);
    }
  }

  /// <summary>
  /// Добавить в контекст указанные объекты, а также их составы (при необходимости)
  /// </summary>
  /// <param name="items">Список добавляемых версий объектов</param>
  /// <param name="mode">Режим добавления</param>
  /// <param name="silentMode">true - действия выполняются без диалога с пользователем</param>
  public virtual void AddItems(
    ISelectedItems items,
    EditingContextsCompositionLevel mode,
    bool silentMode)
  {
    if (items == null || items.Count == 0)
      return;
    List<IDBTypedObjectID> objects = new List<IDBTypedObjectID>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        objects.Add(itemData);
    }
    if (objects.Count <= 0)
      return;
    this.AddObjects((IList<IDBTypedObjectID>) objects, mode, silentMode);
  }

  /// <summary>
  /// Добавить в контекст указанные объекты, а также их составы (при необходимости)
  /// </summary>
  /// <param name="objects">Список добавляемых версий объектов</param>
  /// <param name="mode">Режим добавления</param>
  /// <param name="silentMode">true - действия выполняются без диалога с пользователем</param>
  public virtual void AddObjects(
    IList<IDBTypedObjectID> objects,
    EditingContextsCompositionLevel mode,
    bool silentMode)
  {
    if (objects == null || objects.Count == 0)
      return;
    this.Log.Clear();
    int num1 = this._context.ContextID != 0L ? 1 : 0;
    bool flag1 = (this._accessRights & EditingContextsAccessRights.FullAccess) != 0;
    IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
    if (num1 == 0 || !flag1)
      return;
    int num2 = 0;
    int num3 = 0;
    bool flag2 = false;
    List<long> longList1 = new List<long>();
    string format = LocalizationHolder.rm.GetString("Client.Core_1449") + LocalizationHolder.rm.GetString("Client.Core_1450") + LocalizationHolder.rm.GetString("Client.Core_1451");
    Intermech.PropertyEditors.ProgressForm progressForm = !silentMode ? Intermech.PropertyEditors.ProgressForm.Execute(LocalizationHolder.rm.GetString("Client.Core_1452"), string.Format(format, (object) objects.Count, (object) 0), 0, objects.Count, false, string.Empty, (EventHandler) null) : (Intermech.PropertyEditors.ProgressForm) null;
    List<long> longList2 = new List<long>();
    long num4 = 0;
    try
    {
      ObjectVersionDescription versionDescription = new ObjectVersionDescription();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService customService = sessionKeeper.Session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService;
        for (int index1 = 0; index1 < objects.Count; ++index1)
        {
          if (progressForm != null)
          {
            progressForm.Maximum = objects.Count;
            if ((index1 + 1) % 50 == 0)
              progressForm.SetProgressValue(index1, string.Format(format, (object) objects.Count, (object) (index1 + 1)));
          }
          IDBTypedObjectID dbTypedObjectId1 = objects[index1];
          ObjectVersionDescription newVerDesc = (ObjectVersionDescription) null;
          bool flag3 = false;
          IMSObjectType objectType = MetaDataHelper.GetObjectType(dbTypedObjectId1.ObjectType);
          bool flag4 = false;
          if (!flag4 && this._context.ExistsVersion(dbTypedObjectId1.ObjectID, false))
          {
            this.Log.Add(EditingContextsLogError.ExistsVersion, dbTypedObjectId1.ObjectID);
            flag4 = true;
          }
          if (this._context.ExistsObject(dbTypedObjectId1.ID) && !this._context.ExistsVersion(dbTypedObjectId1.ObjectID, true))
          {
            this.Log.Add(EditingContextsLogError.ExistsAnotherVersionLinked, dbTypedObjectId1.ObjectID);
            flag4 = true;
          }
          if (!flag4 && MetaDataHelper.IsObjectTypeEditingContext(dbTypedObjectId1.ObjectType))
          {
            this.Log.Add(EditingContextsLogError.IsEditingContext, dbTypedObjectId1.ObjectID);
            flag4 = true;
          }
          if (!flag4 && !this.IsECO && (objectType == null || objectType.VersionsMode != ObjectVersionModes.MultiVersion))
            this.Log.Add(EditingContextsLogError.NonversionObject, dbTypedObjectId1.ObjectID);
          if (this._context.ExistsObject(dbTypedObjectId1.ID) && !this._context.ExistsLinkedVersion(dbTypedObjectId1.ObjectID) || this._context.ExistsVersion(dbTypedObjectId1.ObjectID, false) || MetaDataHelper.IsObjectTypeEditingContext(dbTypedObjectId1.ObjectType) || !this.IsECO && (objectType == null || objectType.VersionsMode != ObjectVersionModes.MultiVersion))
          {
            longList1.Add(Math.Abs(dbTypedObjectId1.ObjectID));
            flag3 = true;
            if (mode == EditingContextsCompositionLevel.OnlyObjects)
            {
              ++num2;
              continue;
            }
          }
          if (longList1.Contains(Math.Abs(dbTypedObjectId1.ObjectID)))
            flag3 = true;
          EditingContextsObjectVersion newVersion = new EditingContextsObjectVersion(this._context.ContextID, 0L, 0L, Math.Abs(this._context.ModificationID));
          if (!flag3)
          {
            if (!MetaDataHelper.IsObjectTypeEditingContext(dbTypedObjectId1.ObjectType))
              newVerDesc = ObjectVersionDescriptionsHelper.LoadDescription(sessionKeeper.Session, typeof (ObjectVersionDescription), Math.Abs(dbTypedObjectId1.ObjectID)) as ObjectVersionDescription;
            if (newVerDesc == null || !this.SimpleContext && newVerDesc.F_MODIFICATION_ID != 0L && Math.Abs(newVerDesc.F_MODIFICATION_ID) != Math.Abs(this._context.ModificationID))
            {
              this.Log.Add(EditingContextsLogError.ExistsAnotherVersion, newVerDesc.F_OBJECT_ID);
              ++num2;
              continue;
            }
            newVersion.F_ID = newVerDesc.F_ID;
            newVersion.F_OBJECT_ID = newVerDesc.F_OBJECT_ID;
          }
          bool flag5 = !flag3 && this._context.AddVersion(newVersion, newVerDesc);
          if (flag5)
          {
            longList1.Add(newVerDesc.F_OBJECT_ID);
            ++num3;
          }
          else
            ++num2;
          if (mode == EditingContextsCompositionLevel.FirstLevel && dbTypedObjectId1.Owner != 0L || mode == EditingContextsCompositionLevel.AllLevels)
          {
            if (longList2.Contains(Math.Abs(dbTypedObjectId1.ObjectID)))
            {
              ++num4;
            }
            else
            {
              longList2.Add(Math.Abs(dbTypedObjectId1.ObjectID));
              if (customService != null)
              {
                DataTable dataTable = customService.LoadCompositions((object) sessionKeeper.Session.SessionGUID, dbTypedObjectId1.ObjectID, (IEnumerable<ColumnDescriptor>) EditingContextsEditor._columns, service.FiltrationServiceOwnerID);
                if (dataTable != null)
                {
                  for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                  {
                    DataRow row = dataTable.Rows[index2];
                    DBTypedObjectID dbTypedObjectId2 = new DBTypedObjectID(DataSetProcessor.GetInt32Value(row, 0, -1), DataSetProcessor.GetInt64Value(row, 2, -1L), DataSetProcessor.GetInt64Value(row, 1, 0L), string.Empty, 0L, DataSetProcessor.GetInt64Value(row, 4, 0L), DataSetProcessor.GetInt64Value(row, 5, 0L), DataSetProcessor.GetStringValue(row, 6, string.Empty), DataSetProcessor.GetInt64Value(row, 7, 0L));
                    if (dbTypedObjectId2.ObjectID != 0L && dbTypedObjectId2.ID != 0L && dbTypedObjectId2.ObjectType != -1)
                      objects.Add((IDBTypedObjectID) dbTypedObjectId2);
                  }
                  dataTable.Dispose();
                }
              }
            }
          }
          flag2 |= flag5;
        }
      }
    }
    finally
    {
      if (progressForm != null)
      {
        progressForm.CanCloseForm = true;
        progressForm.Close();
        progressForm.Dispose();
      }
    }
    if (flag2)
    {
      this._context.ClearCacheTables();
      this._isChanged = true;
      this.FillEditor(false);
      this.treeContexts.SelectedRows.Clear();
      for (int childIndex1 = 0; childIndex1 < this.treeContexts.RootRow.NumChildren; ++childIndex1)
      {
        Row row1 = this.treeContexts.RootRow.ChildRowByIndex(childIndex1);
        for (int childIndex2 = 0; childIndex2 < row1.NumChildren; ++childIndex2)
        {
          Row row2 = row1.ChildRowByIndex(childIndex2);
          long fObjectId = row2.Item is EditingContextsObjectVersion contextsObjectVersion ? contextsObjectVersion.F_OBJECT_ID : 0L;
          if (longList1.Contains(Math.Abs(fObjectId)))
            this.treeContexts.SelectedRows.Add(row2);
        }
      }
      this.UpdateControls();
      this.RaiseOnChanged();
    }
    if (silentMode)
      return;
    IMMessageBoxButton[] messageBoxButtonArray;
    if (this.Log.Count != 0)
      messageBoxButtonArray = new IMMessageBoxButton[2]
      {
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1374"), DialogResult.OK),
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1453"), DialogResult.Yes)
      };
    else
      messageBoxButtonArray = new IMMessageBoxButton[1]
      {
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1374"), DialogResult.OK)
      };
    IMMessageBoxButton[] Buttons = messageBoxButtonArray;
    if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1317"), string.Format(LocalizationHolder.rm.GetString("Client.Core_1454"), (object) num3, (object) num2), Buttons, IMMessageBoxImage.Information) != DialogResult.Yes)
      return;
    EditingContextsEventLogForm.Execute(this.Log);
  }

  /// <summary>
  /// Требуется информация о дочерних элементах указанной строки
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Level == 0)
    {
      List<long> longList = (List<long>) null;
      if (this._context != null)
      {
        longList = this._context.GetContextsID();
        if (longList != null && !this._showLinkedContexts)
        {
          for (int index = longList.Count - 1; index >= 1; --index)
            longList.RemoveAt(index);
        }
      }
      e.Children = (IList) longList;
    }
    if (e.Row.Level != 1 || this._userRole == null)
      return;
    e.Children = this._context != null ? (IList) this._context.GetVersionsID((long) e.Row.Item, this._userRole.UserID) : (IList) null;
  }

  /// <summary>Требуются данные для строки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_GetRowData(object sender, GetRowDataEventArgs e)
  {
    ObjectVersionDescription versionDescription = (ObjectVersionDescription) null;
    if (e.Row.Level == 1)
      versionDescription = this._context.GetDescription((long) e.Row.Item);
    if (e.Row.Level == 2)
      versionDescription = this._context.GetDescription((long) e.Row.Item);
    if (e.Row.Level != 1 && e.Row.Level != 2 || versionDescription == null)
      return;
    e.RowData.ImageList = this._objtypesIcons.ImageList;
    e.RowData.IconSize = 32 /*0x20*/;
    e.RowData.Image = Images32x16_Cache.GetImage32x16(4, versionDescription.F_OBJECT_TYPE, (NavigatorTreeNode) null);
  }

  /// <summary>Требуются данные для ячейки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_GetCellData(object sender, GetCellDataEventArgs e)
  {
    ObjectVersionDescription versionDescription = (ObjectVersionDescription) null;
    EditingContextsObjectVersion rowObjectVersion = this.GetRowObjectVersion(e.Row);
    if (e.Row.Level == 1)
      versionDescription = this._context.GetDescription((long) e.Row.Item);
    if (e.Row.Level == 2)
      versionDescription = this._context.GetDescription((long) e.Row.Item);
    if (e.Row.Level == 2 && versionDescription != null && rowObjectVersion != null && (versionDescription.Options & ObjectVersionDescriptionOptions.FromECOComposition) == ObjectVersionDescriptionOptions.FromECOComposition)
      this._context.ExistsLinkedVersion(versionDescription.F_OBJECT_ID);
    if (e.Row.Level == 2 && versionDescription != null && rowObjectVersion != null && (versionDescription.Options & ObjectVersionDescriptionOptions.FromECOComposition) == ObjectVersionDescriptionOptions.FromECOComposition)
    {
      Math.Abs(rowObjectVersion.F_CONTEXT_ID);
      Math.Abs(this._context.ContextID);
    }
    if (e.Row.Level != 1 && e.Row.Level != 2 || versionDescription == null)
      return;
    if (EditingContextsEditor._treeBoldFont == null)
      EditingContextsEditor._treeBoldFont = new Font(this.treeContexts.Font, FontStyle.Bold);
    if (this._userRole != null && (versionDescription.F_CHKOUT_BY > 0L || versionDescription.F_OBJECT_ID < 0L))
    {
      Color color1 = this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor;
      Color color2 = (this._navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckOut) == GradientUsing.CheckOut ? this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkEndColor : this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor;
      LinearGradientMode linearGradientMode = this._navGraphicsCache.CurrentColorsScheme.CheckedOutGradientMode;
      Color foregroundCheckedOut = this._navGraphicsCache.CurrentColorsScheme.ForegroundCheckedOut;
      if (versionDescription.F_CHKOUT_BY != this._userRole.UserID)
      {
        color1 = this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor;
        color2 = (this._navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckedOutOther) == GradientUsing.CheckedOutOther ? this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkEndColor : this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor;
        linearGradientMode = this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherGradientMode;
        Color foregroundCheckedOutOther = this._navGraphicsCache.CurrentColorsScheme.ForegroundCheckedOutOther;
      }
      StyleDelta delta1 = new StyleDelta();
      delta1.BackColor = color1;
      delta1.GradientColor = color2;
      delta1.GradientMode = linearGradientMode;
      if (e.Row.Level == 1 && e.Row.ChildIndex == 0 && (e.Column.Name == "columnCAPTION" || e.Column.Name == "columnF_OBJECT_ID"))
        delta1.Font = EditingContextsEditor._treeBoldFont;
      if (e.Row.Level == 1 && (versionDescription.Options & ObjectVersionDescriptionOptions.FromECOComposition) == ObjectVersionDescriptionOptions.FromECOComposition)
        delta1.ForeColor = Color.Gray;
      if (e.Column.Name == "columnF_OBJECT_ID" || e.Column.Name == "columnF_VERSION_ID")
        delta1.HorzAlignment = StringAlignment.Far;
      e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, delta1);
      StyleDelta delta2 = new StyleDelta();
      delta2.BackColor = color1;
      delta2.GradientColor = color2;
      delta2.GradientMode = linearGradientMode;
      if (e.Row.Level == 1 && e.Row.ChildIndex == 0 && (e.Column.Name == "columnCAPTION" || e.Column.Name == "columnF_OBJECT_ID"))
        delta2.Font = EditingContextsEditor._treeBoldFont;
      if (e.Row.Level == 1 && (versionDescription.Options & ObjectVersionDescriptionOptions.FromECOComposition) == ObjectVersionDescriptionOptions.FromECOComposition)
        delta2.ForeColor = Color.Gray;
      if (e.Column.Name == "columnF_OBJECT_ID" || e.Column.Name == "columnF_VERSION_ID")
        delta2.HorzAlignment = StringAlignment.Far;
      e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, delta2);
      if (e.Row.Level == 2 && versionDescription != null && rowObjectVersion != null && versionDescription.ECOs != null && (versionDescription.Options & ObjectVersionDescriptionOptions.FromECOComposition) == ObjectVersionDescriptionOptions.FromECOComposition && (e.Column.Name == "columnCAPTION" || e.Column.Name == "columnF_OBJECT_ID" || e.Column.Name == "columnF_VERSION_ID") && (versionDescription.ECOs.IndexOf(rowObjectVersion.F_CONTEXT_ID) >= 0 || versionDescription.ECOs.IndexOf(-rowObjectVersion.F_CONTEXT_ID) >= 0))
      {
        e.CellData.OddStyle = new Style(e.CellData.OddStyle ?? e.Row.Tree.RowOddStyle, new StyleDelta()
        {
          ForeColor = Color.Gray
        });
        e.CellData.EvenStyle = new Style(e.CellData.EvenStyle ?? e.Row.Tree.RowEvenStyle, new StyleDelta()
        {
          ForeColor = Color.Gray
        });
      }
    }
    else
    {
      if (e.Row.Level == 1 && e.Row.ChildIndex == 0 && (e.Column.Name == "columnCAPTION" || e.Column.Name == "columnF_OBJECT_ID" || e.Column.Name == "columnF_VERSION_ID"))
      {
        StyleDelta delta3 = new StyleDelta();
        delta3.Font = EditingContextsEditor._treeBoldFont;
        if (e.Column.Name == "columnF_OBJECT_ID" || e.Column.Name == "columnF_VERSION_ID")
          delta3.HorzAlignment = StringAlignment.Far;
        e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, delta3);
        StyleDelta delta4 = new StyleDelta();
        delta4.Font = EditingContextsEditor._treeBoldFont;
        if (e.Column.Name == "columnF_OBJECT_ID" || e.Column.Name == "columnF_VERSION_ID")
          delta4.HorzAlignment = StringAlignment.Far;
        e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, delta4);
      }
      if (e.Row.Level == 2 && versionDescription != null && rowObjectVersion != null && versionDescription.ECOs != null && (versionDescription.Options & ObjectVersionDescriptionOptions.FromECOComposition) == ObjectVersionDescriptionOptions.FromECOComposition && (e.Column.Name == "columnCAPTION" || e.Column.Name == "columnF_OBJECT_ID" || e.Column.Name == "columnF_VERSION_ID") && (versionDescription.ECOs.IndexOf(rowObjectVersion.F_CONTEXT_ID) >= 0 || versionDescription.ECOs.IndexOf(-rowObjectVersion.F_CONTEXT_ID) >= 0))
      {
        e.CellData.OddStyle = new Style(e.CellData.OddStyle ?? e.Row.Tree.RowOddStyle, new StyleDelta()
        {
          ForeColor = Color.Gray
        });
        e.CellData.EvenStyle = new Style(e.CellData.EvenStyle ?? e.Row.Tree.RowEvenStyle, new StyleDelta()
        {
          ForeColor = Color.Gray
        });
      }
    }
    if (e.Column.Name == "columnCAPTION")
      e.CellData.Value = (object) CaptionTransform.GetCaption(versionDescription.CAPTION, versionDescription.F_VERSION_ID);
    else if (e.Column.Name == "columnF_OBJECT_ID")
    {
      object fObjectId = (object) versionDescription.F_OBJECT_ID;
      e.CellData.Value = fObjectId;
    }
    else if (e.Column.Name == "columnF_VERSION_ID")
      e.CellData.Value = (object) versionDescription.F_VERSION_ID;
    else if (e.Column.Name == "columnF_OBJECT_TYPE")
      e.CellData.Value = (object) MetaDataHelper.GetObjectTypeName(versionDescription.F_OBJECT_TYPE);
    else if (e.Column.Name == "columnLC_STEP_ID")
      e.CellData.Value = (object) MetaDataHelper.GetLCStepName(versionDescription.F_LCSTEP_ID);
    else if (e.Column.Name == "columnF_OWNER_ID")
    {
      e.CellData.Value = (object) this._userNamesCache.GetUserName(versionDescription.F_OWNER_ID);
    }
    else
    {
      if (!(e.Column.Name == "columnF_CHECKEDOUT_BY"))
        return;
      e.CellData.Value = (object) this._userNamesCache.GetUserName(versionDescription.F_CHKOUT_BY);
    }
  }

  /// <summary>Требуется показать контекстное меню в ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this.UpdateControls();
    this.menuContext.Show((Control) this.treeContexts, e.Location);
  }

  /// <summary>Сделан двойной клик в ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_CellDoubleClick(object sender, EventArgs e)
  {
    this.UpdateControls();
    if (!this.btnReplace.Enabled)
      return;
    this.DoReplace(sender, e);
  }

  /// <summary>Изменилась сфокусированная строка</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_FocusRowChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Изменились выделенные строки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_SelectionChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
    this.OnSelectedItemsChanged();
  }

  /// <summary>
  /// Перечитать дерево, убрать/показать связанные контексты редактирования
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoShowLinked(object sender, EventArgs e)
  {
    this.ShowLinkedContexts = this.btnShowLinked.Checked;
  }

  /// <summary>Добавить версию в контекст</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoAdd(object sender, EventArgs e)
  {
    IServiceContainer nodesContext = (IServiceContainer) new ServiceContainer();
    IObjectTypeNodeFilter serviceInstance = (IObjectTypeNodeFilter) new ObjectTypeNodeFilter();
    nodesContext.AddService(typeof (IObjectTypeNodeFilter), (object) serviceInstance);
    DescriptorCollection descriptorCollection = new DescriptorCollection();
    bool flag1 = sender != null && e == null;
    bool flag2 = sender == null && e == null;
    int num = this._context.ContextID != 0L ? 1 : 0;
    bool flag3 = (this._accessRights & EditingContextsAccessRights.FullAccess) != 0;
    if (num == 0 || !flag3)
      return;
    string description = LocalizationHolder.rm.GetString("Client.Core_1455");
    string caption = LocalizationHolder.rm.GetString("Client.Core_1456");
    SelectionOptions options = SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule;
    if (flag1)
    {
      description = LocalizationHolder.rm.GetString("Client.Core_1457");
      caption = LocalizationHolder.rm.GetString("Client.Core_1458");
      options = SelectionOptions.Default | SelectionOptions.DisableMultiselect | SelectionOptions.ForceFilterObjectsByRule;
    }
    if (flag2)
    {
      description = LocalizationHolder.rm.GetString("Client.Core_1459");
      caption = LocalizationHolder.rm.GetString("Client.Core_1460");
      options = SelectionOptions.Default | SelectionOptions.DisableMultiselect | SelectionOptions.ForceFilterObjectsByRule;
    }
    List<int> intList1 = new List<int>(0);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      List<int> intList2 = new List<int>(0);
      foreach (IMSObjectType objectTypes in MetaDataHelper.GetObjectTypesList())
      {
        if (MetaDataHelper.CanAddObjTypeToEditingContext(objectTypes.ObjectTypeID, false) && !intList2.Contains(objectTypes.ObjectTypeID))
        {
          intList2.Add(objectTypes.ObjectTypeID);
          serviceInstance.EnabledObjectTypes.Add(objectTypes.ObjectTypeID);
          int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objectTypes.ObjectTypeID);
          if (objectTypeParentId != -1)
          {
            IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeParentId);
            if (objectType != null && objectType.VersionsMode == ObjectVersionModes.Abstract)
            {
              intList2.Add(objectType.ObjectTypeID);
              serviceInstance.EnabledObjectTypes.Add(objectType.ObjectTypeID);
            }
          }
        }
      }
      for (int index1 = 0; index1 < intList2.Count; ++index1)
      {
        int childTypeID = intList2[index1];
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(childTypeID);
        if (objectTypeParentsId == null || objectTypeParentsId.Count == 0)
        {
          if (intList1.IndexOf(childTypeID) < 0)
            intList1.Add(childTypeID);
        }
        else
        {
          if (intList1.IndexOf(childTypeID) < 0)
            intList1.Add(childTypeID);
          for (int index2 = 0; index2 < objectTypeParentsId.Count; ++index2)
          {
            if (intList2.Contains(objectTypeParentsId[index2]))
            {
              intList1.Remove(childTypeID);
              childTypeID = objectTypeParentsId[index2];
              if (intList1.IndexOf(childTypeID) < 0)
                intList1.Add(childTypeID);
            }
            else if (intList1.IndexOf(childTypeID) < 0)
              intList1.Add(childTypeID);
          }
        }
      }
      for (int index = 0; index < intList1.Count; ++index)
        descriptorCollection.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(intList1[index]));
    }
    SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new ContextObjectsSelectedItemsAnalyzer(), true);
    object[] objArray = SelectionWindow.Select(caption, description, (IDescriptor) new ObjectTypesDescriptor(intList1.ToArray(), LocalizationHolder.rm.GetString("Client.Core_283")), typeof (IDBTypedObjectID), (System.IServiceProvider) nodesContext, options);
    if (objArray == null)
      return;
    List<IDBTypedObjectID> objects = new List<IDBTypedObjectID>(objArray.Length);
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is IDBTypedObjectID dbTypedObjectId)
        objects.Add(dbTypedObjectId);
    }
    EditingContextsCompositionLevel mode = EditingContextsCompositionLevel.OnlyObjects;
    if (flag1)
      mode = EditingContextsCompositionLevel.FirstLevel;
    if (flag2)
      mode = EditingContextsCompositionLevel.AllLevels;
    this.AddObjects((IList<IDBTypedObjectID>) objects, mode, false);
  }

  /// <summary>Добавить с составом</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoAddComposition(object sender, EventArgs e)
  {
    DialogResult dialogResult = IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1461"), LocalizationHolder.rm.GetString("Client.Core_1462") + LocalizationHolder.rm.GetString("Client.Core_1463"), new IMMessageBoxButton[3]
    {
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1464"), DialogResult.No),
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1465"), DialogResult.Yes),
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1466"), DialogResult.Cancel)
    }, IMMessageBoxImage.Question);
    switch (dialogResult)
    {
      case DialogResult.Yes:
      case DialogResult.No:
        this.DoAdd(dialogResult == DialogResult.No ? (object) this : (object) (EditingContextsEditor) null, (EventArgs) null);
        break;
    }
  }

  /// <summary>Заменить версию в контексте</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoReplace(object sender, EventArgs e)
  {
    int num = this._context.ContextID != 0L ? 1 : 0;
    bool flag1 = (this._accessRights & EditingContextsAccessRights.FullAccess) != 0;
    ObjectVersionDescription selectedObject = this.GetSelectedObject();
    EditingContextsObjectVersion selectedObjectVersion = this.GetSelectedObjectVersion();
    bool flag2 = selectedObject != null && MetaDataHelper.IsObjectTypeEditingContext(selectedObject.F_OBJECT_TYPE);
    bool flag3 = selectedObjectVersion != null && Math.Abs(selectedObjectVersion.F_CONTEXT_ID) == Math.Abs(this._context.ContextID);
    if (((num == 0 || !flag1 ? 1 : (!flag3 ? 1 : 0)) | (flag2 ? 1 : 0)) != 0 || this._context.ExistsLinkedVersion(selectedObject.F_OBJECT_ID))
      return;
    long version = ObjectVersionSelection.SelectVersion(selectedObject.F_ID, this.SimpleContext, new List<long>(1)
    {
      selectedObject.F_OBJECT_ID
    });
    if (version == 0L || version == selectedObject.F_OBJECT_ID || version == -selectedObject.F_OBJECT_ID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(ObjectVersionDescriptionsHelper.LoadDescription(sessionKeeper.Session, typeof (ObjectVersionDescription), version) is ObjectVersionDescription versionDescription))
        return;
      if (!this.SimpleContext)
        this.CheckExistInAnotherContext(session, versionDescription);
      EditingContextsObjectVersion newVersion = new EditingContextsObjectVersion(this._context.ContextID, versionDescription.F_ID, Math.Abs(versionDescription.F_OBJECT_ID), this._context.ModificationID);
      this._context.ReplaceVersion(selectedObject.F_OBJECT_ID, newVersion, versionDescription);
    }
    this._isChanged = true;
    this.treeContexts.UpdateRowData();
    this.treeContexts.UpdateRows();
    this.UpdateControls();
    this.RaiseOnChanged();
  }

  private void CheckExistInAnotherContext(
    IUserSession userSession,
    ObjectVersionDescription version)
  {
    long num = (userSession.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService).ExistsInContexts((object) userSession.SessionGUID, version.F_MODIFICATION_ID, version.F_OBJECT_ID);
    if (num != 0L)
      throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("EditingContext_VersionExistInAnotherContext"), (object) version.F_OBJECT_ID, (object) num));
  }

  /// <summary>Добавить объекты из буфера обмена</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoPaste(object sender, EventArgs e)
  {
    int num = this._context.ContextID != 0L ? 1 : 0;
    bool flag = (this._accessRights & EditingContextsAccessRights.FullAccess) != 0;
    if (num == 0 || !flag)
      return;
    this.PasteObjects(EditingContextsCompositionLevel.OnlyObjects, false);
  }

  /// <summary>Удалить выделенные версии из контекста</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoDelete(object sender, EventArgs e)
  {
    int num = this._context.ContextID != 0L ? 1 : 0;
    bool flag1 = (this._accessRights & EditingContextsAccessRights.FullAccess) != 0;
    if (num == 0 || !flag1)
      return;
    bool flag2 = false;
    bool flag3 = false;
    for (int index = 0; index < this.treeContexts.SelectedRows.Count; ++index)
    {
      Row selectedRow = this.treeContexts.SelectedRows[index];
      if (selectedRow != null && selectedRow.Level == 2)
      {
        long verContextID = (long) selectedRow.ParentRow.Item;
        long objectID = selectedRow.Item is long ? (long) selectedRow.Item : 0L;
        ObjectVersionDescription description = this._context.GetDescription(objectID);
        EditingContextsObjectVersion version = this._context.GetVersion(objectID, verContextID);
        if (description != null && !MetaDataHelper.IsObjectTypeEditingContext(description.F_OBJECT_TYPE) && Math.Abs(version.F_CONTEXT_ID) == Math.Abs(this._context.ContextID))
        {
          if (!flag3)
          {
            if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1467"), LocalizationHolder.rm.GetString("Client.Core_1468"), new IMMessageBoxButton[2]
            {
              new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1469"), DialogResult.Yes),
              new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1470"), DialogResult.No)
            }, IMMessageBoxImage.Information) != DialogResult.Yes)
              return;
            flag3 = true;
          }
          flag2 = this._context.DeleteVersion(description.F_OBJECT_ID) | flag2;
        }
      }
    }
    if (!flag2)
      return;
    this._context.ClearCacheTables();
    this._isChanged = true;
    this.treeContexts.UpdateRows(true);
    this.treeContexts.UpdateRowData();
    this.UpdateControls();
    this.RaiseOnChanged();
  }

  /// <summary>Обновить содержимое контекста</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  internal void DoRefresh(object sender, EventArgs e)
  {
    if (this._isChanged || this.IsDisposed || this.CurrentContextObjectID == 0L)
      return;
    this.CurrentContextObjectID = this.CurrentContextObjectID;
  }

  /// <summary>Открыть карточку версии объекта</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoShowCard(object sender, EventArgs e)
  {
    ObjectVersionDescription selectedObject = this.GetSelectedObject();
    if (selectedObject == null || selectedObject.F_OBJECT_ID == 0L)
      return;
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, selectedObject.F_OBJECT_ID, false);
  }

  /// <summary>Открыть версию объекта в новом окне</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoOpen(object sender, EventArgs e)
  {
    ObjectVersionDescription selectedObject = this.GetSelectedObject();
    if (selectedObject == null || selectedObject.F_OBJECT_ID == 0L)
      return;
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(selectedObject.F_OBJECT_ID), this.Services);
  }

  /// <summary>В дерево пришло событие drag'n'drop</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_DragEnter(object sender, DragEventArgs e)
  {
    this._dropTargetRow = (Row) null;
    e.Effect = DragDropEffects.None;
    if (!this.treeContexts.AllowDrop || !e.Data.GetDataPresent(typeof (IIOSource)))
      return;
    e.Effect = DragDropEffects.All;
  }

  /// <summary>Над деревом перетаскиваются объекты</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_DragOver(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!this.treeContexts.AllowDrop || this._accessRights != EditingContextsAccessRights.FullAccess || !e.Data.GetDataPresent(typeof (IOSource)))
      return;
    Point client = this.treeContexts.PointToClient(new Point(e.X, e.Y));
    Row rowAt = this.treeContexts.GetRowAt(client.X, client.Y);
    if (rowAt == null || this._dropTargetRow == null)
      return;
    ObjectVersionDescription contextForRow = this.GetContextForRow(rowAt);
    if (contextForRow == null || Math.Abs(this._context.ContextID) != Math.Abs(contextForRow.F_OBJECT_ID))
      return;
    e.Effect = DragDropEffects.All;
  }

  /// <summary>В дереве завершён drag'n'drop</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_DragDrop(object sender, DragEventArgs e)
  {
    if (this._dropTargetRow == null || !this.treeContexts.AllowDrop || this._accessRights != EditingContextsAccessRights.FullAccess || !e.Data.GetDataPresent(typeof (IOSource)))
      return;
    Point client = this.treeContexts.PointToClient(new Point(e.X, e.Y));
    Row rowAt = this.treeContexts.GetRowAt(client.X, client.Y);
    if (rowAt == null || this._dropTargetRow == null)
      return;
    ObjectVersionDescription contextForRow = this.GetContextForRow(rowAt);
    if (contextForRow == null || Math.Abs(this._context.ContextID) != Math.Abs(contextForRow.F_OBJECT_ID) || !(e.Data.GetData(typeof (IOSource)) is IOSource data) || data.SelectedItems == null || data.SelectedItems.Count == 0)
      return;
    this.AddItems(data.SelectedItems, EditingContextsCompositionLevel.OnlyObjects, false);
  }

  /// <summary>Определить, куда сваливать перетаскиваемую строку</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_GetAllowedRowDropLocations(
    object sender,
    GetAllowedRowDropLocationsEventArgs e)
  {
    this._dropTargetRow = e.Row;
    e.AllowedDropLocations = this._dropTargetRow != null ? RowDropLocation.OnRow : RowDropLocation.BelowRow;
  }

  /// <summary>
  /// Определить условия "сброса" перетаскиваемых объектов на указанную строку
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_GetRowDropEffect(object sender, GetRowDropEffectEventArgs e)
  {
    this._dropTargetRow = (Row) null;
    if (!this.treeContexts.AllowDrop || this._accessRights != EditingContextsAccessRights.FullAccess || !e.Data.GetDataPresent(typeof (IOSource)))
      return;
    this._dropTargetRow = e.Row;
    ObjectVersionDescription contextForRow = this.GetContextForRow(this._dropTargetRow);
    if (contextForRow == null || Math.Abs(this._context.ContextID) != Math.Abs(contextForRow.F_OBJECT_ID))
      return;
    e.DropEffect = DragDropEffects.All;
  }

  /// <summary>Отпущена клавиша</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeContexts_KeyUp(object sender, KeyEventArgs e)
  {
    ObjectVersionDescription selectedObject = this.GetSelectedObject();
    if (selectedObject == null || selectedObject.F_OBJECT_ID == 0L || e.KeyCode != Keys.F4)
      return;
    this.DoShowCard(sender, (EventArgs) e);
  }

  private ISimpleSelectedItems CreateSelectedItems()
  {
    return (ISimpleSelectedItems) new EditingContextsEditor.SimpleSelectedItems(this.GetSelectedObjectEnumerator());
  }

  private IEnumerable<IDBTypedObjectID> GetSelectedObjectEnumerator()
  {
    foreach (Row selectedRow in this.treeContexts.SelectedRows)
    {
      if (selectedRow.Level == 2)
      {
        ObjectVersionDescription description = this._context.GetDescription((long) selectedRow.Item);
        yield return (IDBTypedObjectID) new DBTypedObjectID(description.F_OBJECT_TYPE, description.F_OBJECT_ID, description.F_ID, description.CAPTION, description.F_OWNER_ID, description.F_VERSION_ID, description.F_BASE_VERSION, (string) null, description.F_MODIFICATION_ID);
      }
    }
  }

  private void OnSelectedItemsChanged()
  {
    EventHandler selectedItemsChanged = this.SelectedItemsChanged;
    if (selectedItemsChanged == null)
      return;
    selectedItemsChanged((object) this, new EventArgs());
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.menuEditingContextsBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditingContextsEditor));
    this.imagesMenus = new ImageList(this.components);
    this.columnCAPTION = new Column();
    this.columnF_OBJECT_ID = new Column();
    this.columnF_VERSION_ID = new Column();
    this.columnF_OBJECT_TYPE = new Column();
    this.columnLC_STEP_ID = new Column();
    this.columnF_OWNER_ID = new Column();
    this.columnF_CHECKEDOUT_BY = new Column();
    this.menubarContext = new MenuBar();
    this.menuContext = new ContextMenuBarItem();
    this.mnpCard = new MenuButtonItem();
    this.mnpOpenInNewWindow = new MenuButtonItem();
    this.mnpAdd = new MenuButtonItem();
    this.mnpAddComposition = new MenuButtonItem();
    this.mnpReplace = new MenuButtonItem();
    this.mnpPaste = new MenuButtonItem();
    this.mnpDelete = new MenuButtonItem();
    this.mnpRefresh = new MenuButtonItem();
    this.pages = new TabControl();
    this.pageContext = new TabPage();
    this.treeContexts = new Intermech.VirtualTreeView.VirtualTreeView();
    this.menuEditingContextsBar = new Intermech.Bars.ToolBar();
    this.btnOpen = new ButtonItem();
    this.btnCard = new ButtonItem();
    this.btnAdd = new ButtonItem();
    this.btnAddComposition = new ButtonItem();
    this.btnReplace = new ButtonItem();
    this.btnPaste = new ButtonItem();
    this.btnDelete = new ButtonItem();
    this.btnRefresh = new ButtonItem();
    this.btnShowLinked = new ButtonItem();
    this.pageVersionsRule = new TabPage();
    this.headerControl = new HeaderControl();
    this.menuBar = new MenuBar();
    this.contextMenuBarItem = new ContextMenuBarItem();
    this.mnpAddCriterion = new MenuButtonItem();
    this.mnpDeleteCriterion = new MenuButtonItem();
    this.mnpAddValue = new MenuButtonItem();
    this.mnpDelValue = new MenuButtonItem();
    this.mnpMoveUp = new MenuButtonItem();
    this.mnpMoveDown = new MenuButtonItem();
    this.pages.SuspendLayout();
    this.pageContext.SuspendLayout();
    this.treeContexts.BeginInit();
    this.headerControl.SuspendLayout();
    this.SuspendLayout();
    this.imagesMenus.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesMenus.ImageStream");
    this.imagesMenus.TransparentColor = Color.Transparent;
    this.imagesMenus.Images.SetKeyName(0, "ball_green_plus.ico");
    this.imagesMenus.Images.SetKeyName(1, "replace2.png");
    this.imagesMenus.Images.SetKeyName(2, "delete.ico");
    this.imagesMenus.Images.SetKeyName(3, "обновить.png");
    this.imagesMenus.Images.SetKeyName(4, "object_16x16.ico");
    this.imagesMenus.Images.SetKeyName(5, "Правила подбора версий 2.ico");
    this.imagesMenus.Images.SetKeyName(6, "EditingContext.ico");
    this.imagesMenus.Images.SetKeyName(7, "text_tree.ico");
    componentResourceManager.ApplyResources((object) this.columnCAPTION, "columnCAPTION");
    this.columnCAPTION.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnCAPTION.HeaderStyle.HorzAlignment");
    this.columnCAPTION.HeaderStyle.VertAlignment = (StringAlignment) componentResourceManager.GetObject("columnCAPTION.HeaderStyle.VertAlignment");
    this.columnCAPTION.Movable = false;
    this.columnCAPTION.Name = "columnCAPTION";
    this.columnCAPTION.Sortable = false;
    componentResourceManager.ApplyResources((object) this.columnF_OBJECT_ID, "columnF_OBJECT_ID");
    this.columnF_OBJECT_ID.CellStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnF_OBJECT_ID.CellStyle.HorzAlignment");
    this.columnF_OBJECT_ID.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnF_OBJECT_ID.HeaderStyle.HorzAlignment");
    this.columnF_OBJECT_ID.Movable = false;
    this.columnF_OBJECT_ID.Name = "columnF_OBJECT_ID";
    this.columnF_OBJECT_ID.Sortable = false;
    componentResourceManager.ApplyResources((object) this.columnF_VERSION_ID, "columnF_VERSION_ID");
    this.columnF_VERSION_ID.CellStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnF_VERSION_ID.CellStyle.HorzAlignment");
    this.columnF_VERSION_ID.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnF_VERSION_ID.HeaderStyle.HorzAlignment");
    this.columnF_VERSION_ID.Movable = false;
    this.columnF_VERSION_ID.Name = "columnF_VERSION_ID";
    this.columnF_VERSION_ID.Sortable = false;
    componentResourceManager.ApplyResources((object) this.columnF_OBJECT_TYPE, "columnF_OBJECT_TYPE");
    this.columnF_OBJECT_TYPE.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnF_OBJECT_TYPE.HeaderStyle.HorzAlignment");
    this.columnF_OBJECT_TYPE.Movable = false;
    this.columnF_OBJECT_TYPE.Name = "columnF_OBJECT_TYPE";
    this.columnF_OBJECT_TYPE.Sortable = false;
    componentResourceManager.ApplyResources((object) this.columnLC_STEP_ID, "columnLC_STEP_ID");
    this.columnLC_STEP_ID.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnLC_STEP_ID.HeaderStyle.HorzAlignment");
    this.columnLC_STEP_ID.Movable = false;
    this.columnLC_STEP_ID.Name = "columnLC_STEP_ID";
    this.columnLC_STEP_ID.Sortable = false;
    componentResourceManager.ApplyResources((object) this.columnF_OWNER_ID, "columnF_OWNER_ID");
    this.columnF_OWNER_ID.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnF_OWNER_ID.HeaderStyle.HorzAlignment");
    this.columnF_OWNER_ID.Movable = false;
    this.columnF_OWNER_ID.Name = "columnF_OWNER_ID";
    this.columnF_OWNER_ID.Sortable = false;
    componentResourceManager.ApplyResources((object) this.columnF_CHECKEDOUT_BY, "columnF_CHECKEDOUT_BY");
    this.columnF_CHECKEDOUT_BY.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnF_CHECKEDOUT_BY.HeaderStyle.HorzAlignment");
    this.columnF_CHECKEDOUT_BY.Name = "columnF_CHECKEDOUT_BY";
    this.columnF_CHECKEDOUT_BY.Sortable = false;
    componentResourceManager.ApplyResources((object) this.menubarContext, "menubarContext");
    this.menubarContext.FullMenus = true;
    this.menubarContext.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menubarContext.Hidden = false;
    this.menubarContext.ImageList = this.imagesMenus;
    this.menubarContext.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.menuContext
    });
    this.menubarContext.Name = "menubarContext";
    this.menubarContext.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.menuContext, "menuContext");
    this.menuContext.Items.AddRange(new ToolbarItemBase[8]
    {
      (ToolbarItemBase) this.mnpCard,
      (ToolbarItemBase) this.mnpOpenInNewWindow,
      (ToolbarItemBase) this.mnpAdd,
      (ToolbarItemBase) this.mnpAddComposition,
      (ToolbarItemBase) this.mnpReplace,
      (ToolbarItemBase) this.mnpPaste,
      (ToolbarItemBase) this.mnpDelete,
      (ToolbarItemBase) this.mnpRefresh
    });
    this.menuContext.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpCard, "mnpCard");
    this.mnpCard.ShowText = true;
    this.mnpCard.Click += new EventHandler(this.DoShowCard);
    componentResourceManager.ApplyResources((object) this.mnpOpenInNewWindow, "mnpOpenInNewWindow");
    this.mnpOpenInNewWindow.ShowText = true;
    this.mnpOpenInNewWindow.Click += new EventHandler(this.DoOpen);
    this.mnpAdd.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpAdd, "mnpAdd");
    this.mnpAdd.ImageIndex = 0;
    this.mnpAdd.ShowText = true;
    this.mnpAdd.Click += new EventHandler(this.DoAdd);
    componentResourceManager.ApplyResources((object) this.mnpAddComposition, "mnpAddComposition");
    this.mnpAddComposition.ImageIndex = 7;
    this.mnpAddComposition.ShowText = true;
    this.mnpAddComposition.Click += new EventHandler(this.DoAddComposition);
    this.mnpReplace.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpReplace, "mnpReplace");
    this.mnpReplace.ImageIndex = 1;
    this.mnpReplace.ShowText = true;
    this.mnpReplace.Click += new EventHandler(this.DoReplace);
    this.mnpPaste.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpPaste, "mnpPaste");
    this.mnpPaste.ShowText = true;
    this.mnpPaste.Click += new EventHandler(this.DoPaste);
    this.mnpDelete.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpDelete, "mnpDelete");
    this.mnpDelete.ImageIndex = 2;
    this.mnpDelete.ShowText = true;
    this.mnpDelete.Click += new EventHandler(this.DoDelete);
    this.mnpRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpRefresh, "mnpRefresh");
    this.mnpRefresh.ImageIndex = 3;
    this.mnpRefresh.ShowText = true;
    this.mnpRefresh.Click += new EventHandler(this.DoRefresh);
    this.pages.Controls.Add((Control) this.pageContext);
    this.pages.Controls.Add((Control) this.pageVersionsRule);
    componentResourceManager.ApplyResources((object) this.pages, "pages");
    this.pages.ImageList = this.imagesMenus;
    this.pages.Name = "pages";
    this.pages.SelectedIndex = 0;
    this.pageContext.Controls.Add((Control) this.treeContexts);
    this.pageContext.Controls.Add((Control) this.menuEditingContextsBar);
    componentResourceManager.ApplyResources((object) this.pageContext, "pageContext");
    this.pageContext.Name = "pageContext";
    this.pageContext.UseVisualStyleBackColor = true;
    this.treeContexts.AllowDrop = true;
    this.treeContexts.AllowRowResize = false;
    this.treeContexts.AllowUserPinnedColumns = false;
    this.treeContexts.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeContexts, "treeContexts");
    this.treeContexts.ImageList = (ImageList) null;
    this.treeContexts.MainColumn = this.columnCAPTION;
    this.treeContexts.Name = "treeContexts";
    this.treeContexts.SelectBeforeEdit = true;
    this.treeContexts.ShowRootRow = false;
    this.treeContexts.SortAscendingIcon = (Icon) componentResourceManager.GetObject("treeContexts.SortAscendingIcon");
    this.treeContexts.SortDescendingIcon = (Icon) componentResourceManager.GetObject("treeContexts.SortDescendingIcon");
    this.treeContexts.SuppressErrorMessages = true;
    this.treeContexts.KeyUp += new KeyEventHandler(this.treeContexts_KeyUp);
    this.menuEditingContextsBar.AddRemoveButtonsVisible = false;
    this.menuEditingContextsBar.AllowHorizontalDock = false;
    this.menuEditingContextsBar.Closable = false;
    this.menuEditingContextsBar.DockLine = 3;
    this.menuEditingContextsBar.DrawActionsButton = false;
    this.menuEditingContextsBar.FullMenus = true;
    this.menuEditingContextsBar.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.menuEditingContextsBar.Hidden = false;
    this.menuEditingContextsBar.ImageList = this.imagesMenus;
    this.menuEditingContextsBar.Items.AddRange(new ToolbarItemBase[9]
    {
      (ToolbarItemBase) this.btnOpen,
      (ToolbarItemBase) this.btnCard,
      (ToolbarItemBase) this.btnAdd,
      (ToolbarItemBase) this.btnAddComposition,
      (ToolbarItemBase) this.btnReplace,
      (ToolbarItemBase) this.btnPaste,
      (ToolbarItemBase) this.btnDelete,
      (ToolbarItemBase) this.btnRefresh,
      (ToolbarItemBase) this.btnShowLinked
    });
    componentResourceManager.ApplyResources((object) this.menuEditingContextsBar, "menuEditingContextsBar");
    this.menuEditingContextsBar.MinimumFloatingSize = new Size(250, 30);
    this.menuEditingContextsBar.Movable = false;
    this.menuEditingContextsBar.Name = "menuEditingContextsBar";
    this.menuEditingContextsBar.Overflow = ToolBarOverflow.Wrap;
    this.menuEditingContextsBar.Stretch = true;
    this.menuEditingContextsBar.Tearable = false;
    this.btnOpen.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnOpen, "btnOpen");
    this.btnOpen.Click += new EventHandler(this.DoOpen);
    componentResourceManager.ApplyResources((object) this.btnCard, "btnCard");
    this.btnCard.Click += new EventHandler(this.DoShowCard);
    this.btnAdd.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.ImageIndex = 0;
    this.btnAdd.Click += new EventHandler(this.DoAdd);
    componentResourceManager.ApplyResources((object) this.btnAddComposition, "btnAddComposition");
    this.btnAddComposition.ImageIndex = 7;
    this.btnAddComposition.Click += new EventHandler(this.DoAddComposition);
    this.btnReplace.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnReplace, "btnReplace");
    this.btnReplace.ImageIndex = 1;
    this.btnReplace.Click += new EventHandler(this.DoReplace);
    this.btnPaste.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnPaste, "btnPaste");
    this.btnPaste.Click += new EventHandler(this.DoPaste);
    this.btnDelete.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.ImageIndex = 2;
    this.btnDelete.Click += new EventHandler(this.DoDelete);
    this.btnRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnRefresh, "btnRefresh");
    this.btnRefresh.ImageIndex = 3;
    this.btnRefresh.Click += new EventHandler(this.DoRefresh);
    this.btnShowLinked.AutoToggle = AutoToggleType.Single;
    this.btnShowLinked.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btnShowLinked, "btnShowLinked");
    this.btnShowLinked.ImageIndex = 6;
    this.btnShowLinked.Click += new EventHandler(this.DoShowLinked);
    componentResourceManager.ApplyResources((object) this.pageVersionsRule, "pageVersionsRule");
    this.pageVersionsRule.Name = "pageVersionsRule";
    this.pageVersionsRule.UseVisualStyleBackColor = true;
    this.headerControl.BackColor = SystemColors.Control;
    this.headerControl.Controls.Add((Control) this.menuBar);
    componentResourceManager.ApplyResources((object) this.headerControl, "headerControl");
    this.headerControl.ForeColor = SystemColors.ControlText;
    this.headerControl.HeaderFont = new Font("Tahoma", 12f, FontStyle.Bold);
    this.headerControl.Name = "headerControl";
    componentResourceManager.ApplyResources((object) this.menuBar, "menuBar");
    this.menuBar.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuBar.Hidden = false;
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem
    });
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.contextMenuBarItem, "contextMenuBarItem");
    this.contextMenuBarItem.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpAddCriterion,
      (ToolbarItemBase) this.mnpDeleteCriterion,
      (ToolbarItemBase) this.mnpAddValue,
      (ToolbarItemBase) this.mnpDelValue,
      (ToolbarItemBase) this.mnpMoveUp,
      (ToolbarItemBase) this.mnpMoveDown
    });
    this.contextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpAddCriterion, "mnpAddCriterion");
    this.mnpAddCriterion.ImageIndex = 0;
    this.mnpAddCriterion.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpDeleteCriterion, "mnpDeleteCriterion");
    this.mnpDeleteCriterion.ImageIndex = 1;
    this.mnpDeleteCriterion.ShowText = true;
    this.mnpAddValue.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpAddValue, "mnpAddValue");
    this.mnpAddValue.ImageIndex = 2;
    this.mnpAddValue.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpDelValue, "mnpDelValue");
    this.mnpDelValue.ImageIndex = 3;
    this.mnpDelValue.ShowText = true;
    this.mnpMoveUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveUp, "mnpMoveUp");
    this.mnpMoveUp.ImageIndex = 4;
    this.mnpMoveUp.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveDown, "mnpMoveDown");
    this.mnpMoveDown.ImageIndex = 5;
    this.mnpMoveDown.ShowText = true;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.pages);
    this.Controls.Add((Control) this.headerControl);
    this.Controls.Add((Control) this.menubarContext);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.MinimumSize = new Size(300, 250);
    this.Name = nameof (EditingContextsEditor);
    this.pages.ResumeLayout(false);
    this.pageContext.ResumeLayout(false);
    this.treeContexts.EndInit();
    this.headerControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Делегат события об изменении в редакторе</summary>
  /// <param name="sender">Контрол (редактор контекстов)</param>
  /// <param name="e">Аргументы события</param>
  public delegate void EditingContextsChangedEventHandler(object sender, EventArgs e);

  public sealed class SimpleSelectedItems : ISimpleSelectedItems
  {
    public SimpleSelectedItems(IEnumerable<IDBTypedObjectID> objects)
    {
      this.Objects = objects != null ? objects : throw new ArgumentNullException(nameof (objects));
    }

    public IEnumerable<IDBTypedObjectID> Objects { get; private set; }

    public int Count => this.Objects.Count<IDBTypedObjectID>();

    public object GetItemData(int index, System.Type dataFormat)
    {
      return dataFormat == typeof (IDBTypedObjectID) ? (object) this.Objects.ElementAt<IDBTypedObjectID>(index) : (object) null;
    }
  }
}
