
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrObjectsListBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core.FormDesigner.Controls;

[Designer(typeof (AttrListBaseDesigner))]
[RefreshProperties(RefreshProperties.All)]
[ToolboxItem(false)]
public class AttrObjectsListBase : 
  AttrsControl,
  IObjectsListSupport,
  IIDListSupport,
  IFormDesignerControl
{
  private NodeColumnCollection _columnCollection;
  private ListContext _listContext;
  private int _objTypeID;
  private int _relTypeID;
  private Guid _objTypeGuid;
  private Guid _relTypeGuid;
  protected IAttributePropertyDescriber _describer;
  private EventHandler _formDeactivate;
  private EventHandler _loadDataCompleted;
  private IFormDesignerControl _parent;
  private iGRow _tmpItem;
  private ServiceContainer _services;
  private bool skip;
  private int _maxCountValue;
  /// <summary>
  /// Может быть подписка и на LoadDataCompleted и на FormDeactivate,
  /// поэтому, чтобы не подписываться 2 раза на событие изменения родителя и закладки (если нужно),
  /// выставляем этот флаг при первом подписании.
  /// </summary>
  private bool _isSubscribeOnTabPageParentChanged;
  private bool _isSubscribeSelectedItemsChanged;
  private EventHandler _selectedItemsChanged;
  /// <summary>Сохраненные настроки для колонок ChildrenView</summary>
  private SavedColumnsSettings _savedColumnsSettings;
  private BorderStyle _BorderStyle;
  protected ControlButton _btnAdd;
  protected ControlButton _btnDel;
  protected ControlButton _btnEdit;
  protected ControlButton _btnClear;
  protected ControlButton _btnForm;
  private MenuBar _bar;
  private MenuBarItem _controlMenu;
  private MenuButtonItem _navigatorItems;
  private MenuButtonItem _mbiAdd;
  private MenuButtonItem _mbiDel;
  private MenuButtonItem _mbiEdit;
  private MenuButtonItem _mbiClear;
  private MenuButtonItem _mbiForm;
  private MenuButtonItem _mbiPaste;
  /// <summary>Список колонок для отображения</summary>
  private NodeColumnCollection dataCurrentColumns;
  /// <summary>
  /// Таблица с расшифрованными данными по объектам списка для заполнения грида.
  /// </summary>
  private DataTable dataValuesRawTable;
  /// <summary>Список колонок в valuesRawTable</summary>
  private System.Collections.Generic.List<object> dataColumnsRawTableList;
  /// <summary>
  /// Индекс в dataColumnsRawTableList для колонки с ObjectID
  /// </summary>
  private int dataColumnsRawTableIdIndex;
  /// <summary>
  /// Индекс в dataColumnsRawTableList для колонки с Caption; может отсутствовать
  /// </summary>
  private int dataColumnsRawTableCaptionIndex;
  /// <summary>Допустимые типы объектов</summary>
  private int[] dataTypeIDs;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected ContextMenuStrip _menu;
  protected ToolStripMenuItem _miAdd;
  protected ToolStripMenuItem _miDel;
  protected ToolStripMenuItem _miEdit;
  protected ToolStripMenuItem _miClear;
  private iGrid iGrid;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private iGCellStyle iGridCol0CellStyle;
  private iGColHdrStyle iGridCol0ColHdrStyle;

  /// <summary>
  /// Набор определенных пользователем колонок, по которым будет производиться сортировка по умолчанию.
  /// </summary>
  [Browsable(false)]
  [DefaultValue(null)]
  public NodeColumnCollection DefaultSortingColumns { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DefaultValue(false)]
  public bool SelectFromImbase { get; set; }

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Window")]
  public new Color BackColor
  {
    get => this.iGrid.BackColor;
    set => this.iGrid.BackColor = value;
  }

  private iGBorderStyle BorderStyleToGridBorderStyle(BorderStyle borderStyle)
  {
    iGBorderStyle gridBorderStyle;
    switch (borderStyle)
    {
      case BorderStyle.FixedSingle:
        gridBorderStyle = iGBorderStyle.Flat;
        break;
      case BorderStyle.Fixed3D:
        gridBorderStyle = iGBorderStyle.Standard;
        break;
      default:
        gridBorderStyle = iGBorderStyle.None;
        break;
    }
    return gridBorderStyle;
  }

  private BorderStyle GridBorderStyleToBorderStyle(iGBorderStyle gridBorderStyle)
  {
    BorderStyle borderStyle;
    switch (gridBorderStyle)
    {
      case iGBorderStyle.Standard:
        borderStyle = BorderStyle.Fixed3D;
        break;
      case iGBorderStyle.Flat:
        borderStyle = BorderStyle.FixedSingle;
        break;
      default:
        borderStyle = BorderStyle.None;
        break;
    }
    return borderStyle;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.FixedSingle)]
  public new BorderStyle BorderStyle
  {
    get
    {
      if (this._BorderStyle != this.GridBorderStyleToBorderStyle(this.iGrid.BorderStyle))
        this.iGrid.BorderStyle = this.BorderStyleToGridBorderStyle(this._BorderStyle);
      return this._BorderStyle;
    }
    set
    {
      this._BorderStyle = value;
      this.iGrid.BorderStyle = this.BorderStyleToGridBorderStyle(this._BorderStyle);
    }
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => this.iGrid.ForeColor;
    set => this.iGrid.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this.iGrid);
    set => this._toolTip.SetToolTip((Control) this.iGrid, value);
  }

  /// <summary>
  /// Отображение горизонтальной линейки прокрутки в элементе управления.
  /// </summary>
  [DefaultValue(false)]
  public bool HorizontalScrollbar
  {
    get => this.iGrid.HScrollBar.Enabled;
    set => this.iGrid.HScrollBar.Enabled = value;
  }

  /// <summary>
  /// Использование наименований колонок определенных пользователем.
  /// </summary>
  [DefaultValue(false)]
  public bool UseColumnsAliases { get; set; }

  /// <summary>Показ контекстного меню</summary>
  [DefaultValue(true)]
  public bool ShowContextMenu { get; set; }

  /// <summary>Глобальный идентификатор типа объектов.</summary>
  [Browsable(false)]
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

  /// <summary>Идентификатор типа объектов.</summary>
  /// <remarks>Осипенко legacy: Правильно хранить Guid типа объектов. Оставлено для поддержания работы ранее созданного.</remarks>
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

  /// <summary>Глобальный идентификатор типа связи.</summary>
  [Browsable(false)]
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
  [Browsable(false)]
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

  /// <summary>Группировка колонок.</summary>
  [Browsable(false)]
  [DefaultValue(true)]
  public bool DisableColumnsGrouping { get; }

  /// <summary>Включение/отключение режима редактирования.</summary>
  [Browsable(false)]
  [DefaultValue(false)]
  public bool EditMode { get; }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(ListContext.Objects)]
  public ListContext List
  {
    get => this._listContext;
    set => this._listContext = value;
  }

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
  /// Опция возможности назначения максимального разрешенного для ввода кол-ва значений
  /// </summary>
  [Browsable(false)]
  public int MaxCountValue
  {
    get
    {
      if (this.AttributeInfo != null)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid);
        if (attributeType != null && attributeType.MultiValueMode == MultiValueModes.SingleValue)
          this._maxCountValue = 1;
      }
      return this._maxCountValue;
    }
    set
    {
      if (this.AttributeInfo != null)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid);
        if (attributeType != null && attributeType.MultiValueMode == MultiValueModes.SingleValue)
        {
          this._maxCountValue = 1;
          return;
        }
      }
      this._maxCountValue = value;
    }
  }

  /// <summary>
  /// Очистка закэшированных опций атрибута применительно к типу объекта/связи,
  /// которые не тягаются вместе с AttributeInfo, но могут быть дополнительно (и однократно) зачитаны в процессе работы контрола
  /// </summary>
  protected override void ClearAttributeInfoCachedOptions()
  {
    base.ClearAttributeInfoCachedOptions();
    if (this.AttributeInfo == null)
      return;
    this._maxCountValue = 0;
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      object[] getValues;
      if (this.iGrid.Rows.Count == 0)
      {
        getValues = new object[1]{ (object) DBNull.Value };
      }
      else
      {
        System.Collections.Generic.List<object> objectList = new System.Collections.Generic.List<object>();
        if (this._describer != null)
        {
          for (int index = 0; index < this.iGrid.Rows.Count; ++index)
          {
            object attributeValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, this.iGrid.Rows[index].Tag);
            if (attributeValue != null && attributeValue != DBNull.Value)
              objectList.Add(attributeValue);
          }
        }
        else
        {
          for (int index = 0; index < this.iGrid.Rows.Count; ++index)
          {
            object forAttributeValues = this.GetItemForAttributeValues(this.iGrid.Rows[index].Tag);
            if (forAttributeValues != DBNull.Value && forAttributeValues != null)
              objectList.Add(forAttributeValues);
          }
        }
        object[] objArray;
        if (objectList.Count != 0)
          objArray = objectList.ToArray();
        else
          objArray = new object[1]{ (object) DBNull.Value };
        getValues = objArray;
      }
      return getValues;
    }
  }

  /// <summary>Наличие Descriptor'а у атрибута.</summary>
  /// <remark>Необходимость в свойстве появилась в следующем случае:
  /// При связывании атрибута с контролом необходимо выставить доступнонсть редактирования атрибута.
  /// Если у атрибута свойство "Запрет редактирования в ручную" = "Да", необходимо запретить редактирование атрибута с помощью контрола.
  /// НО!!! Если значение можно не ввести с клавиатуры, а выбрать из списка, то необходимо разрешить модификацию атрибута,
  /// несмотря на запрет.
  /// С помощью Descriptor'а можно значение выбирать из списка, следовательно перед тем как присваивать значение свойству Enabled,
  /// необходимо проверить наличие Descriptor'а</remark>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal bool HasDescriptor => this._describer != null;

  protected override bool ValueIsEmpty
  {
    get
    {
      bool valueIsEmpty = true;
      if (this._attrValues != null)
      {
        object[] values = this._attrValues.Values;
        if (values != null && values.Length != 0)
        {
          object obj = values[0];
          valueIsEmpty = values.Length == 1 && (obj == null || obj == DBNull.Value);
        }
      }
      return valueIsEmpty;
    }
  }

  public event NodeColumnRenameEventHandler OnNodeColumnRename;

  public AttrObjectsListBase()
  {
    MenuButtonItem menuButtonItem1 = new MenuButtonItem();
    menuButtonItem1.Enabled = true;
    menuButtonItem1.CommandName = "miAdd";
    menuButtonItem1.Tag = (object) 0;
    this._mbiAdd = menuButtonItem1;
    MenuButtonItem menuButtonItem2 = new MenuButtonItem();
    menuButtonItem2.Enabled = false;
    menuButtonItem2.CommandName = "miDel";
    this._mbiDel = menuButtonItem2;
    MenuButtonItem menuButtonItem3 = new MenuButtonItem();
    menuButtonItem3.Enabled = false;
    menuButtonItem3.CommandName = "miEdit";
    menuButtonItem3.Tag = (object) 1;
    this._mbiEdit = menuButtonItem3;
    MenuButtonItem menuButtonItem4 = new MenuButtonItem();
    menuButtonItem4.Enabled = false;
    menuButtonItem4.CommandName = "miClear";
    this._mbiClear = menuButtonItem4;
    MenuButtonItem menuButtonItem5 = new MenuButtonItem();
    menuButtonItem5.Enabled = false;
    menuButtonItem5.CommandName = "miForm";
    this._mbiForm = menuButtonItem5;
    MenuButtonItem menuButtonItem6 = new MenuButtonItem();
    menuButtonItem6.Enabled = false;
    menuButtonItem6.CommandName = "miPaste";
    menuButtonItem6.BeginGroup = true;
    this._mbiPaste = menuButtonItem6;
    this.dataColumnsRawTableIdIndex = -1;
    this.dataColumnsRawTableCaptionIndex = -1;
    // ISSUE: explicit constructor call
    base.\u002Ector();
    this.InitializeComponent();
    this.OnNodeColumnRename += new NodeColumnRenameEventHandler(this.On_childrenView_OnNodeColumnRename);
    base.BackColor = Color.Transparent;
    this._btnAdd = new ControlButton("Add", 3)
    {
      Enabled = false,
      Tag = (object) 0
    };
    this._btnAdd.Click += new EventHandler(this.OnAddEdit_Click);
    this._btnDel = new ControlButton("Del", 4)
    {
      Enabled = false
    };
    this._btnDel.Click += new EventHandler(this.OnDel_Click);
    this._btnEdit = new ControlButton("Edit", 5)
    {
      Enabled = false,
      Tag = (object) 1
    };
    this._btnEdit.Click += new EventHandler(this.OnAddEdit_Click);
    this._btnClear = new ControlButton("Clean", 6)
    {
      Enabled = false
    };
    this._btnClear.Click += new EventHandler(this.OnClear_Click);
    this._btnForm = new ControlButton("Form", 7)
    {
      Enabled = false
    };
    this._btnForm.Click += new EventHandler(this.On_btnForm_Click);
    this.AddTopButtons(new System.Collections.Generic.List<ControlButton>()
    {
      this._btnAdd,
      this._btnDel,
      this._btnEdit,
      this._btnClear,
      this._btnForm
    });
    this.CreateContextMenu();
    this.InitServices();
    this.Name = string.Empty;
    this.MenuItemClick += new EventHandler(this.On_btnAddEdit_Click);
    this.iGrid.ContextMenuStrip = (ContextMenuStrip) null;
    this.UseColumnsAliases = false;
    this.CanContainsChildren = false;
    this.ShowContextMenu = true;
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

  protected virtual void OnClear_Click(object sender, EventArgs e) => this.ClearItems();

  protected virtual void OnDel_Click(object sender, EventArgs e) => this.DeleteItem();

  protected virtual void OnAddEdit_Click(object sender, EventArgs e)
  {
    if (this.MenuItemClick == null)
      return;
    this.MenuItemClick(sender, e);
  }

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Доступость пункта меню "Вставить".</summary>
  private bool IsPasteEnabled
  {
    get
    {
      IDBObjectTypedIDCollection typedIdCollection = (IDBObjectTypedIDCollection) null;
      if (this.EnabledCtrl && this.AttributeInfo != null && this._attrValues != null)
        typedIdCollection = (ApplicationServices.Container.GetService(typeof (IClipboard)) as IClipboard).GetDataObject() as IDBObjectTypedIDCollection;
      return typedIdCollection != null;
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
    if (column == null || column.Attribute == null)
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
  private void OnFormDeactivate(object sender, EventArgs e)
  {
  }

  /// <summary>Обнуление данных в ChildrenView.</summary>
  private void ReleaseChildrenView()
  {
  }

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

  public NodeColumnCollection GetNodeColumns()
  {
    NodeColumnCollection nodeColumns = new NodeColumnCollection();
    foreach (iGCol iGcol in (IEnumerable<iGCol>) this.iGrid.Cols.Cast<iGCol>().OrderBy<iGCol, int>((System.Func<iGCol, int>) (o => o.Order)))
    {
      if (iGcol.Tag is NodeColumn)
      {
        ((NodeColumn) iGcol.Tag).Width = iGcol.Width;
        nodeColumns.Add((NodeColumn) iGcol.Tag);
      }
    }
    return nodeColumns;
  }

  private NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = (NodeColumnCollection) null;
    INode node = this._objTypeID == -1 ? (INode) new ObjectTypesNode() : (INode) new ObjectTypeNode(this._objTypeID, AccessRights.Enabled);
    node.GetSupportedColumns(ContentType.NonFolders, string.Empty);
    if (defaultColumns == null)
      defaultColumns = node.GetDefaultColumns(ContentType.NonFolders);
    return defaultColumns;
  }

  private void SetColumns(NodeColumnCollection columns, bool reloadGrid)
  {
    if (this.DesignMode)
      return;
    this.GridSetColumns(columns == null || columns.Count <= 0 ? this.GetDefaultColumns() : columns, false);
  }

  /// <summary>Получить строку-ключ для указанной колонки</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Строка-ключ колонки</returns>
  private string GetColumnKey(NodeColumn column)
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    return $"{column.SchemeGuid.ToString()},{service.ColumnIDToPersistName(column.SchemeGuid, column.ID)}";
  }

  /// <summary>Требуется получение нового имени у колонки</summary>
  protected void RaiseNodeColumnRename(NodeColumn column)
  {
    NodeColumnRenameEventArgs e = new NodeColumnRenameEventArgs(column, string.Empty);
    if (this.OnNodeColumnRename != null)
      this.OnNodeColumnRename((object) this, e);
    if (string.IsNullOrEmpty(e.NewName))
      return;
    column.Caption = e.NewName;
  }

  /// <summary>
  /// Создает колонки в гриде по коллекции колонок навигатора.
  /// </summary>
  /// <param name="columns">Коллекция колонок навигатора</param>
  /// <param name="reloadData">
  /// Признак необходимости перечитать данные в гриде, если новая
  /// коллекция колонок не соответствует отображаемым данным</param>
  protected void GridSetColumns(NodeColumnCollection columns, bool reloadData)
  {
    try
    {
      this.iGrid.BeginUpdate();
      this.iGrid.Redraw = false;
      this.iGrid.Cols.Clear();
      for (int index = 0; index < columns.Count; ++index)
      {
        iGCol col = this.iGrid.Cols.Add();
        col.SortType = iGSortType.None;
        col.Key = this.GetColumnKey(columns[index]);
        col.Tag = (object) columns[index];
        this.RaiseNodeColumnRename(columns[index]);
        col.Text = !UISettings.ShowShortAttributeNames ? (object) columns[index].Caption : (object) columns[index].ShortCaption;
        col.Order = index;
        col.Width = columns[index].Width;
        col.CellStyle.ReadOnly = iGBool.True;
        this.AdjustColumnForImages(col);
      }
    }
    finally
    {
      this.iGrid.Redraw = true;
      this.iGrid.EndUpdate();
      int num = reloadData ? 1 : 0;
    }
  }

  /// <summary>
  /// Назначить параметры колонок для отображения изображений
  /// </summary>
  /// <param name="col"></param>
  private void AdjustColumnForImages(iGCol col)
  {
    if (!((NodeColumn) col.Tag).ID.Equals((object) ObligatoryObjectAttributes.F_LEVEL_ID))
      return;
    col.CellStyle.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
  }

  /// <summary>Назначить изображение для ячейки</summary>
  /// <param name="cell"></param>
  /// <param name="val"></param>
  private void AdjustCellForImages(iGCell cell, object val)
  {
    if (!((NodeColumn) cell.Col.Tag).ID.Equals((object) ObligatoryObjectAttributes.F_LEVEL_ID))
      return;
    if (val is ValueWithDescription)
      cell.ImageIndex = Statics.IconSrv.IndexOf(8, Convert.ToInt32(((ValueWithDescription) val).Value));
    else
      cell.ImageIndex = Statics.IconSrv.IndexOf(8, 0);
  }

  /// <summary>Данные загружены.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnLoadDataCompleted(object sender, EventArgs e)
  {
    Form form = (sender as Control).FindForm();
    if (form == null)
      return;
    DesForm desForm = form as DesForm;
    long elementIdentifier = desForm.Info.ElementIdentifier;
    if (this._isSubscribeSelectedItemsChanged)
      this._isSubscribeSelectedItemsChanged = false;
    if (desForm.ServiceProvider != null)
      return;
    IServiceContainer serviceContainer = ServicesManager.ServiceContainer;
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

  protected event EventHandler MenuItemClick;

  private void iGrid_DoubleClick(object sender, EventArgs e)
  {
    if (!this.EnabledCtrl || this.iGrid.CurRow == null)
      return;
    this.OnAddEdit_Click((object) this._btnEdit, e);
  }

  private void iGrid_KeyDown(object sender, KeyEventArgs e)
  {
    if (this._attrValues == null || e.KeyCode != Keys.Delete)
      return;
    this.DeleteItem();
  }

  private void iGrid_SelectionChanged(object sender, EventArgs e)
  {
    this.CheckAccessibilityButtons();
  }

  private void AttrObjectsListBase_Enter(object sender, EventArgs e)
  {
    if (this._tmpItem == null)
      return;
    this.iGrid.CurRow = this._tmpItem;
    this._tmpItem = (iGRow) null;
  }

  private void _miAdd_Click(object sender, EventArgs e)
  {
    if (this.MenuItemClick == null)
      return;
    this.MenuItemClick(sender, e);
  }

  private void _miDel_Click(object sender, EventArgs e) => this.DeleteItem();

  private void _miClear_Click(object sender, EventArgs e) => this.ClearItems();

  /// <summary>Значение атрибута.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      base.Values = value;
      this._tmpItem = (iGRow) null;
      this.iGrid.Rows.Clear();
      this.dataCurrentColumns = this.ColumnCollection ?? this.GetDefaultColumns();
      if (this._savedColumnsSettings != null)
      {
        this.dataCurrentColumns = this.SetSavedSettings(this.dataCurrentColumns, this._savedColumnsSettings);
        this._savedColumnsSettings = (SavedColumnsSettings) null;
      }
      if (this.DefaultSortingColumns != null)
        this.dataCurrentColumns = this.SetOrderAndIndex(this.dataCurrentColumns, this.DefaultSortingColumns);
      this.SetColumns(this.dataCurrentColumns, false);
      this._describer = (IAttributePropertyDescriber) null;
      System.Collections.Generic.List<int> linkedObjectTypes = this._attrValues != null ? MetaDataHelper.GetLinkedObjectTypes(this._attrValues.AttributeID) : (System.Collections.Generic.List<int>) null;
      if (linkedObjectTypes != null && linkedObjectTypes.Count == 0)
        linkedObjectTypes.Add(-1);
      this.dataTypeIDs = linkedObjectTypes?.ToArray();
      this.dataValuesRawTable = (DataTable) null;
      this.dataColumnsRawTableList = this.GetDBColumns(this.dataCurrentColumns, out this.dataColumnsRawTableIdIndex, out this.dataColumnsRawTableCaptionIndex);
      if (this.dataTypeIDs != null && this.dataTypeIDs.Length != 0 && this._attrValues != null)
      {
        this._miAdd.Enabled = this._menu.Enabled = true;
        this._describer = !(ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service) || this.IsDesignMode ? (IAttributePropertyDescriber) null : service.GetDescriber(this._attrValues.AttributeID);
        if (!this.ValueIsEmpty)
        {
          System.Collections.Generic.List<long> objectIdList = new System.Collections.Generic.List<long>();
          if (this._describer != null && this.ParentInfo != null)
          {
            foreach (object actualValue in this._attrValues.Values)
            {
              object propDescriptorValue = this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, actualValue);
              if (propDescriptorValue != null)
              {
                if (!(this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, propDescriptorValue) is long attributeValue))
                  throw new Exception("Not valid AttrObjectList Value");
                objectIdList.Add(attributeValue);
              }
              else if (actualValue is long)
                objectIdList.Add(Convert.ToInt64(actualValue));
              else
                objectIdList.Add(-1L);
            }
          }
          else
          {
            foreach (object obj in this._attrValues.Values)
            {
              if (obj is long)
                objectIdList.Add(Convert.ToInt64(obj));
              else
                objectIdList.Add(-1L);
            }
          }
          this.ReadRawData(objectIdList, this.dataCurrentColumns, this.dataColumnsRawTableList, out this.dataValuesRawTable);
        }
      }
      try
      {
        if (this.ValueIsEmpty)
          return;
        if (this._describer != null && this.ParentInfo != null)
        {
          int index = -1;
          foreach (object actualValue in this._attrValues.Values)
          {
            ++index;
            object propDescriptorValue = this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, actualValue);
            if (propDescriptorValue != null)
              this.DataCreateOrModifyItemForRow(this.iGrid, this.dataValuesRawTable.Rows[index], true, propDescriptorValue);
            else
              this.DataCreateOrModifyItemForRow(this.iGrid, this.dataValuesRawTable.Rows[index]);
          }
        }
        else
        {
          int index = -1;
          foreach (object obj in this._attrValues.Values)
          {
            ++index;
            this.DataCreateOrModifyItemForRow(this.iGrid, this.dataValuesRawTable.Rows[index]);
          }
        }
      }
      finally
      {
        this.CheckAccessibilityButtons();
      }
    }
  }

  /// <summary>Вернуть набор колонок для запроса из базы</summary>
  /// <param name="dataCurrentColumns"></param>
  /// <returns></returns>
  private System.Collections.Generic.List<object> GetDBColumns(
    NodeColumnCollection dataCurrentColumns,
    out int idIndex,
    out int idCaption)
  {
    bool flag1 = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      flag1 = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    idIndex = -1;
    idCaption = -1;
    System.Collections.Generic.List<object> dbColumns = new System.Collections.Generic.List<object>();
    bool flag2 = false;
    bool flag3 = false;
    for (int index = 0; index < dataCurrentColumns.Count; ++index)
    {
      dbColumns.Add(dataCurrentColumns[index].ID);
      if (!flag2 && dataCurrentColumns[index].ID.Equals((object) ObligatoryObjectAttributes.F_OBJECT_ID))
      {
        flag2 = true;
        if (flag1)
          idIndex = index;
      }
      if (!flag3 && dataCurrentColumns[index].ID.Equals((object) ObligatoryObjectAttributes.F_ID))
      {
        flag3 = true;
        if (!flag1)
          idIndex = index;
      }
      if (dataCurrentColumns[index].ID.Equals((object) ObligatoryObjectAttributes.CAPTION))
        idCaption = index;
    }
    if (!flag2)
    {
      dbColumns.Add((object) ObligatoryObjectAttributes.F_OBJECT_ID);
      if (flag1)
        idIndex = dbColumns.Count - 1;
    }
    if (!flag3)
    {
      dbColumns.Add((object) ObligatoryObjectAttributes.F_ID);
      if (!flag1)
        idIndex = dbColumns.Count - 1;
    }
    return dbColumns;
  }

  /// <summary>Читаем таблицу данных по списку объектов</summary>
  /// <param name="objectIdList"></param>
  /// <param name="objTypes">Obsolete: допустимые типы объектов: -1 все типы или список допустимых типов</param>
  /// <param name="_columns"></param>
  /// <param name="_rawTable"></param>
  /// <param name="_columnsRawTableList"></param>
  /// <returns></returns>
  private bool ReadRawData(
    System.Collections.Generic.List<long> objectIdList,
    NodeColumnCollection _columns,
    System.Collections.Generic.List<object> _columnsRawTableList,
    out DataTable _rawTable)
  {
    bool flag1 = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      flag1 = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    _rawTable = (DataTable) null;
    DataTable dataTable1 = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Dictionary<int, System.Collections.Generic.List<long>> dictionary = new Dictionary<int, System.Collections.Generic.List<long>>();
      for (int index = 0; index < objectIdList.Count; ++index)
      {
        long objectId = objectIdList[index];
        if (!flag1)
        {
          IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(objectId, false);
          if (objectBaseVersionById != null)
            objectId = objectBaseVersionById.ObjectID;
        }
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectId);
        if (!objectInfo.Empty)
        {
          System.Collections.Generic.List<long> longList;
          if (dictionary.ContainsKey(objectInfo.ObjectTypeID))
          {
            longList = dictionary[objectInfo.ObjectTypeID];
          }
          else
          {
            longList = new System.Collections.Generic.List<long>();
            dictionary[objectInfo.ObjectTypeID] = longList;
          }
          longList.Add(objectInfo.ObjectID);
          longList.Add(-objectInfo.ObjectID);
        }
      }
      object[] array = _columnsRawTableList.ToArray();
      foreach (KeyValuePair<int, System.Collections.Generic.List<long>> keyValuePair in dictionary)
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(keyValuePair.Key);
        if (objectCollection != null)
        {
          objectCollection.ShowAllModifications = true;
          DBRecordSetParams paramSet = new DBRecordSetParams(new System.Collections.Generic.List<ConditionStructure>()
          {
            new ConditionStructure(-2, RelationalOperators.In, (object) keyValuePair.Value.ToArray(), LogicalOperators.NONE, 0, false)
          }.ToArray(), array, 0L, (object) null, -1);
          DataTable dataTable2 = objectCollection.SelectWithDescriptions(paramSet);
          if (dataTable1 == null)
          {
            dataTable1 = dataTable2;
          }
          else
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
              dataTable1.Rows.Add(row.ItemArray);
          }
        }
      }
      _rawTable = dataTable1.Clone();
      for (int index = 0; index < objectIdList.Count; ++index)
      {
        bool flag2 = false;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        {
          if (Math.Abs(Convert.ToInt64(row[this.dataColumnsRawTableIdIndex])).Equals(Math.Abs(objectIdList[index])))
          {
            _rawTable.Rows.Add(row.ItemArray);
            flag2 = true;
            break;
          }
        }
        if (!flag2)
        {
          DataRow row = _rawTable.NewRow();
          row[this.dataColumnsRawTableIdIndex] = (object) objectIdList[index];
          if (this.dataColumnsRawTableCaptionIndex != -1 && objectIdList[index] == -1L)
            row[this.dataColumnsRawTableCaptionIndex] = (object) "Объект не найден";
          _rawTable.Rows.Add(row);
        }
      }
    }
    return true;
  }

  /// <summary>Доступность контрола.</summary>
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      this.CheckAccessibilityButtons();
      if (this.IsDesignMode)
        return;
      if (!value)
      {
        Color color = this.iGrid.BackColor;
        int argb1 = color.ToArgb();
        color = Color.White;
        int argb2 = color.ToArgb();
        if (argb1 != argb2)
          return;
        this.iGrid.BackColor = SystemColors.Control;
      }
      else
      {
        if (!(this.iGrid.BackColor == SystemColors.Control))
          return;
        this.iGrid.BackColor = Color.White;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLeaveControl(EventArgs e)
  {
    this._tmpItem = this.iGrid.CurRow;
    this.iGrid.CurRow = (iGRow) null;
    base.OnLeaveControl(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void SetDesignText(string text)
  {
    base.SetDesignText(text);
    if (!string.IsNullOrEmpty(text))
    {
      this.iGrid.Rows.Clear();
      this.iGrid.Cols.Clear();
      iGCol iGcol = this.iGrid.Cols.Add();
      iGcol.Text = (object) text;
      iGcol.Width = this.ClientSize.Width;
    }
    else
    {
      this.iGrid.Rows.Clear();
      this.iGrid.Cols.Clear();
    }
  }

  private void On_btnAddEdit_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    bool flag = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    int int32 = Convert.ToInt32(sender is MenuButtonItem menuButtonItem ? menuButtonItem.Tag : (sender as ControlButton).Tag);
    System.Collections.Generic.List<long> existItems = this.FillIDsList(this.iGrid);
    if (this._describer != null)
    {
      Dictionary<long, object> valueFromDescriber = this.GetValueFromDescriber(int32 == 0 ? (object) null : this.iGrid.CurRow.Tag);
      if (valueFromDescriber == null)
        return;
      foreach (KeyValuePair<long, object> keyValuePair in valueFromDescriber)
      {
        if (existItems.Contains(keyValuePair.Key) && (int32 == 0 || existItems.IndexOf(keyValuePair.Key) != this.iGrid.CurRow.Index))
        {
          int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("FormDesigner_ListControls_ValueExist"), (object) Convert.ToString(keyValuePair.Value)), LocalizationHolder.rm.GetString("FormDesigner_ListControls_DublicationValue"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          DataTable _rawTable = (DataTable) null;
          this.ReadRawData(new System.Collections.Generic.List<long>()
          {
            keyValuePair.Key
          }, this.dataCurrentColumns, this.dataColumnsRawTableList, out _rawTable);
          DataRow row = _rawTable.Rows[0];
          if (int32 == 0)
          {
            this.iGrid.CurRow = this.DataCreateOrModifyItemForRow(this.iGrid, row, true, keyValuePair.Value);
            if (this.dataValuesRawTable == null)
              this.dataValuesRawTable = _rawTable;
            else
              this.dataValuesRawTable.Rows.Add(row.ItemArray);
          }
          else
          {
            this.DataCreateOrModifyItemForRow(this.iGrid, row, true, keyValuePair.Value, this.iGrid.CurRow);
            this.dataValuesRawTable.Rows[this.iGrid.CurRow.Index].ItemArray = row.ItemArray;
          }
          this.Modified = true;
        }
      }
    }
    else if (this.SelectFromImbase)
    {
      if (!(ApplicationServices.Container.GetService(typeof (IImbaseFilterSelector)) is IImbaseFilterSelector service))
      {
        this.Error = "Не удалось получить сервис выбора объектов из IMBASE.";
      }
      else
      {
        System.Collections.Generic.List<long> newItems = new System.Collections.Generic.List<long>(0);
        System.Collections.Generic.List<long> catalogIDs = (System.Collections.Generic.List<long>) null;
        ImbaseCatalogSelectMode mode = ImbaseCatalogSelectMode.imcmNone;
        try
        {
          int typeID = -1;
          if (this.ParentInfo.ElementKind == AttributableElements.Object)
          {
            if (this.ParentTypeID == -1)
            {
              QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this.ParentInfo.ElementIdentifier);
              if (!objectInfo.Empty)
                this.ParentTypeID = objectInfo.ObjectTypeID;
            }
            typeID = this.ParentTypeID;
          }
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            ImbaseExtendedItem imbaseExtendedItem = ExtendedServiceHelper.GetImbaseExtendedItem(sessionKeeper.Session, typeID, this._attrValues.AttributeID);
            if (imbaseExtendedItem != null)
            {
              catalogIDs = imbaseExtendedItem.CatalogIDs;
              mode = imbaseExtendedItem.SelectMode;
            }
          }
        }
        catch
        {
        }
        finally
        {
          if (catalogIDs == null || catalogIDs.Count == 0)
          {
            catalogIDs = (System.Collections.Generic.List<long>) null;
            this.Error = LocalizationHolder.rm.GetString("AttrTextBtnComp.ImbaseCatalog.NotRef");
          }
        }
        if (catalogIDs == null)
          return;
        Dictionary<TypedInfoItem, IEnumerable<AttributeValues>> dict = new Dictionary<TypedInfoItem, IEnumerable<AttributeValues>>(2);
        IElementInfo elementInfo = (IElementInfo) null;
        if (this.DesForm != null)
        {
          elementInfo = this.DesForm.Info;
          System.Collections.Generic.List<AttributeValues> changedAttributes1 = this.DesForm.GetBaseElementChangedAttributes;
          if (changedAttributes1.Count > 0)
          {
            if (elementInfo.ElementKind == AttributableElements.Object)
              dict.Add((TypedInfoItem) new ObjInfoItem(elementInfo.ElementIdentifier, this.DesForm.ElementTypeID), (IEnumerable<AttributeValues>) changedAttributes1);
            else
              dict.Add((TypedInfoItem) new RelInfoItem(elementInfo.ElementIdentifier, this.DesForm.ElementTypeID), (IEnumerable<AttributeValues>) changedAttributes1);
          }
          System.Collections.Generic.List<AttributeValues> changedAttributes2 = this.DesForm.GetAdditionalElementChangedAttributes;
          if (changedAttributes2.Count > 0)
            dict.Add((TypedInfoItem) new RelInfoItem(this.DesForm.RelationInfo.ElementIdentifier), (IEnumerable<AttributeValues>) changedAttributes2);
        }
        long objID = this.ParentInfo.ElementKind == AttributableElements.Object ? this.ParentInfo.ElementIdentifier : (elementInfo != null ? elementInfo.ElementIdentifier : 0L);
        int[] needObjTypes = (int[]) null;
        if (mode == ImbaseCatalogSelectMode.imcmCreateObject)
          needObjTypes = MetaDataHelper.GetLinkedObjectTypes(this._attrValues.AttributeID)?.ToArray();
        System.Collections.Generic.List<long> longList = service.SelectImbaseObjects(catalogIDs, needObjTypes, objID, (System.Collections.Generic.List<long>) null, mode, dict, this._attrValues.AttributeID);
        if (longList == null || longList.Count <= 0)
          return;
        for (int index = 0; index < longList.Count; ++index)
        {
          long id = longList[index];
          if (!flag)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(longList[index], false);
              if (dbObject != null)
                id = dbObject.ID;
            }
          }
          newItems.Add(id);
        }
        this.AddItems(existItems, newItems, int32 == 0);
        this.Modified = true;
      }
    }
    else
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid);
      if (attributeType == null)
        return;
      int usersTypeId = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).UsersTypeID;
      System.Collections.Generic.List<long> newItems = new System.Collections.Generic.List<long>(0);
      if (Convert.ToInt32(attributeType.SizeType) == usersTypeId)
      {
        IDescriptor rootDescriptor = (IDescriptor) new UsersGroupsDescriptor();
        if (!(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1129"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects) is IDBTypedObjectID[] dbTypedObjectIdArray) || dbTypedObjectIdArray.Length == 0 || dbTypedObjectIdArray[0].ObjectType != usersTypeId)
          return;
        foreach (IDBTypedObjectID dbTypedObjectId in dbTypedObjectIdArray)
          newItems.Add(flag ? dbTypedObjectId.ObjectID : dbTypedObjectId.ID);
      }
      else
      {
        IDescriptor rootDescriptor;
        if (attributeType.SizeType == -1L)
          rootDescriptor = (IDescriptor) new ObjectTypesNodeDescriptor();
        else if (attributeType.SizeType == 0L)
        {
          ArrayList typeListByAttrId = ObjectEditor.GetObjTypeListByAttrId(attributeType.AttributeID);
          DescriptorCollection descriptors = new DescriptorCollection();
          if (typeListByAttrId != null)
          {
            int result = 0;
            foreach (object obj in typeListByAttrId)
            {
              if (int.TryParse(Convert.ToString(obj), out result))
              {
                if (result == usersTypeId)
                  descriptors.Add((IDescriptor) new UsersGroupsDescriptor());
                else
                  descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(result));
              }
            }
          }
          rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_1266"), descriptors);
        }
        else
        {
          rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Convert.ToInt32(attributeType.SizeType));
          Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(Convert.ToInt32(attributeType.SizeType), true), true);
        }
        if (!(Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1130"), rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0)
          return;
        foreach (IDBObjectID dbObjectId in dbObjectIdArray)
          newItems.Add(flag ? dbObjectId.Value : dbObjectId.ID);
      }
      this.AddItems(existItems, newItems, int32 == 0);
      this.Modified = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miDel_Click(object sender, EventArgs e) => this.DeleteItem();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miClear_Click(object sender, EventArgs e) => this.ClearItems();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnForm_Click(object sender, EventArgs e)
  {
    if (this.iGrid.CurRow == null)
      return;
    long result = 0;
    if (this._describer != null)
    {
      if (!long.TryParse(Convert.ToString(this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, this.iGrid.CurRow.Tag)), out result))
        result = 0L;
    }
    else
      result = this.iGrid.CurRow.Tag is ObjectIDToCaption tag ? tag.ObjectID : 0L;
    if (result == 0L)
      return;
    long ObjectID = result;
    if (this.AttributeInfo != null && this._attrValues != null && this._attrValues.AttributeType == FieldTypes.ftObjectLinkByID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(result, false);
        if (objectBaseVersionById != null)
          ObjectID = objectBaseVersionById.ObjectID;
      }
    }
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, ObjectID, false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miPaste_Click(object sender, EventArgs e)
  {
    if (this._describer != null)
      throw new ApplicationException(LocalizationHolder.rm.GetString("FormDesigner_AttrsWithDescriber_Paste_Error"));
    bool flag1 = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      flag1 = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid);
    if (attributeType == null)
      return;
    int attributeId = attributeType.AttributeID;
    IDBObjectTypedIDCollection dataObject = (ApplicationServices.Container.GetService(typeof (IClipboard)) as IClipboard).GetDataObject() as IDBObjectTypedIDCollection;
    System.Collections.Generic.List<long> newItems = new System.Collections.Generic.List<long>(0);
    if (attributeType.SizeType == -1L)
    {
      for (int index = 0; index < dataObject.Count; ++index)
        newItems.Add(flag1 ? dataObject.GetTypedObjectID(index).ObjectID : dataObject.GetTypedObjectID(index).ID);
    }
    else if (attributeType.SizeType == 0L)
    {
      System.Collections.Generic.List<int> possibleTypes = this.GetPossibleTypes(attributeId);
      if (possibleTypes != null)
      {
        for (int index = 0; index < dataObject.Count; ++index)
        {
          IDBTypedObjectID typedObjectId = dataObject.GetTypedObjectID(index);
          bool flag2 = true;
          foreach (int parentType in possibleTypes)
          {
            if (typedObjectId.ObjectType == parentType || MetaDataHelper.IsObjectTypeChildOf(typedObjectId.ObjectType, parentType))
            {
              flag2 = false;
              break;
            }
          }
          if (!flag2)
            newItems.Add(flag1 ? typedObjectId.ObjectID : typedObjectId.ID);
        }
      }
    }
    else
    {
      int int32 = Convert.ToInt32(attributeType.SizeType);
      for (int index = 0; index < dataObject.Count; ++index)
      {
        IDBTypedObjectID typedObjectId = dataObject.GetTypedObjectID(index);
        if (typedObjectId.ObjectType == int32)
          newItems.Add(flag1 ? typedObjectId.ObjectID : typedObjectId.ID);
      }
    }
    this.AddItems(this.FillIDsList(this.iGrid), newItems);
    this.Modified = true;
  }

  private void iGrid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.AddNavigatorContextMenu();
  }

  /// <summary>Получение элемент.</summary>
  /// <param name="item">Элемент в списке элементов</param>
  /// <returns>Значение </returns>
  private object OnGetItemForAttributeValuesFromDescriber(object item)
  {
    object attributeValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, item);
    return attributeValue == null || attributeValue == DBNull.Value || !(attributeValue is long) ? (object) DBNull.Value : attributeValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="existItems"></param>
  /// <param name="newItems"></param>
  /// <param name="isAdd"></param>
  private void AddItems(System.Collections.Generic.List<long> existItems, System.Collections.Generic.List<long> newItems, bool isAdd = true)
  {
    bool _objectVersionProcessed = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      _objectVersionProcessed = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    int num1 = 0;
    string format = LocalizationHolder.rm.GetString("FormDesigner_ListControls_ValueExist");
    string str = LocalizationHolder.rm.GetString("FormDesigner_IdenticalMessage_Skip");
    string caption = LocalizationHolder.rm.GetString("FormDesigner_ListControls_DublicationValue");
    try
    {
      string empty = string.Empty;
      if (!isAdd)
      {
        ObjectIDToCaption objectIdToCaption = new ObjectIDToCaption(newItems[0], _objectVersionProcessed);
        if (!existItems.Contains(objectIdToCaption.ObjectID))
        {
          existItems.Remove((this.iGrid.CurRow.Tag as ObjectIDToCaption).ObjectID);
          existItems.Add(objectIdToCaption.ObjectID);
          DataTable _rawTable = (DataTable) null;
          this.ReadRawData(new System.Collections.Generic.List<long>()
          {
            objectIdToCaption.ObjectID
          }, this.dataCurrentColumns, this.dataColumnsRawTableList, out _rawTable);
          DataRow row = _rawTable.Rows[0];
          this.DataCreateOrModifyItemForRow(this.iGrid, row, igModifiedRow: this.iGrid.CurRow);
          this.dataValuesRawTable.Rows[this.iGrid.CurRow.Index].ItemArray = row.ItemArray;
          ++num1;
        }
        else if (existItems.IndexOf(objectIdToCaption.ObjectID) != this.iGrid.CurRow.Index)
          this.skip = MessageBox.Show($"{string.Format(format, (object) objectIdToCaption.ToString())}\n{str}", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes;
      }
      for (int index = num1; index < newItems.Count; ++index)
      {
        if (this._maxCountValue != 0 && this.dataValuesRawTable != null && this.dataValuesRawTable.Rows.Count >= this._maxCountValue)
        {
          int num2 = (int) MessageBox.Show("Превышено максимальное количество значений атрибута", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          break;
        }
        ObjectIDToCaption objectIdToCaption = new ObjectIDToCaption(newItems[index], _objectVersionProcessed);
        if (existItems.Contains(objectIdToCaption.ObjectID))
        {
          if (!this.skip)
            this.skip = MessageBox.Show($"{string.Format(format, (object) objectIdToCaption.ToString())}\n{str}", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes;
        }
        else
        {
          DataTable _rawTable = (DataTable) null;
          this.ReadRawData(new System.Collections.Generic.List<long>()
          {
            objectIdToCaption.ObjectID
          }, this.dataCurrentColumns, this.dataColumnsRawTableList, out _rawTable);
          DataRow row = _rawTable.Rows[0];
          this.DataCreateOrModifyItemForRow(this.iGrid, row);
          if (this.dataValuesRawTable == null)
            this.dataValuesRawTable = _rawTable;
          else
            this.dataValuesRawTable.Rows.Add(row.ItemArray);
        }
      }
    }
    finally
    {
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void AddNavigatorContextMenu()
  {
    if (this._navigatorItems != null)
    {
      if (this._controlMenu.Items.Contains((ToolbarItemBase) this._navigatorItems))
        this._controlMenu.Items.Remove((ToolbarItemBase) this._navigatorItems);
      this._navigatorItems.Dispose();
    }
    if (!this.ShowContextMenu)
      return;
    MenuButtonItem menuButtonItem = new MenuButtonItem(LocalizationHolder.rm.GetString("FormDesigner_NavigatorCommands"));
    menuButtonItem.BeginGroup = true;
    this._navigatorItems = menuButtonItem;
    bool flag = false;
    if (this.iGrid.SelectedCells.Count > 0)
    {
      MenuBarItem menu = Intermech.Navigator.ContextMenu.Services.GetMenu(Intermech.Navigator.ContextMenu.Services.GetItems(this.FillIDsList(this.iGrid, true).ToArray()), (System.IServiceProvider) this._services);
      if (menu != null && menu.Items.Count > 0)
      {
        foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) menu.Items)
          this._navigatorItems.Items.Add(toolbarItemBase.CloneItem());
        flag = true;
      }
    }
    this._navigatorItems.Enabled = flag;
    this._controlMenu.Items.Add((ToolbarItemBase) this._navigatorItems);
  }

  /// <summary>
  /// 
  /// </summary>
  private void CreateContextMenu()
  {
    if (Holder.BarManager != null && Holder.BarManager.MenuBar != null)
      this._bar.ImageList = Holder.BarManager.MenuBar.ImageList;
    this._mbiAdd.Text = LocalizationHolder.rm.GetString("Client.Core_94a");
    this._mbiAdd.Image = FormDesignerUtils.ButtonImages.ContainsKey("Add") ? FormDesignerUtils.ButtonImages["Add"] : (Image) null;
    this._mbiAdd.Click += new EventHandler(this.On_btnAddEdit_Click);
    this._mbiDel.Text = LocalizationHolder.rm.GetString("Client.Core_96");
    this._mbiDel.Image = FormDesignerUtils.ButtonImages.ContainsKey("Del") ? FormDesignerUtils.ButtonImages["Del"] : (Image) null;
    this._mbiDel.Click += new EventHandler(this.On_miDel_Click);
    this._mbiEdit.Text = LocalizationHolder.rm.GetString("Client.Core_470");
    this._mbiEdit.Image = FormDesignerUtils.ButtonImages.ContainsKey("Edit") ? FormDesignerUtils.ButtonImages["Edit"] : (Image) null;
    this._mbiEdit.Click += new EventHandler(this.On_btnAddEdit_Click);
    this._mbiClear.Text = LocalizationHolder.rm.GetString("Client.Core_1128");
    this._mbiClear.Image = FormDesignerUtils.ButtonImages.ContainsKey("Clean") ? FormDesignerUtils.ButtonImages["Clean"] : (Image) null;
    this._mbiClear.Click += new EventHandler(this.On_miClear_Click);
    this._mbiForm.Text = LocalizationHolder.rm.GetString("AttrTextBtn.Button.ObjectCard.ToolTip");
    this._mbiForm.Image = FormDesignerUtils.ButtonImages.ContainsKey("Form") ? FormDesignerUtils.ButtonImages["Form"] : (Image) null;
    this._mbiForm.Click += new EventHandler(this.On_btnForm_Click);
    INamedImageList service = ApplicationServices.Container.GetService(typeof (INamedImageList)) as INamedImageList;
    this._mbiPaste.Text = LocalizationHolder.rm.GetString("Client.Core_99");
    this._mbiPaste.Click += new EventHandler(this.On_miPaste_Click);
    this._mbiPaste.ImageIndex = service != null ? service.ImageIndex("imgPaste") : -1;
    this._controlMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[6]
    {
      this._mbiAdd,
      this._mbiDel,
      this._mbiEdit,
      this._mbiClear,
      this._mbiForm,
      this._mbiPaste
    });
    this._bar.SetPopupMenu((Control) this.iGrid, this._controlMenu);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrID"></param>
  /// <returns></returns>
  private System.Collections.Generic.List<int> GetPossibleTypes(int attrID)
  {
    System.Collections.Generic.List<int> intList = (System.Collections.Generic.List<int>) null;
    ArrayList typeListByAttrId = ObjectEditor.GetObjTypeListByAttrId(attrID);
    if (typeListByAttrId != null)
    {
      intList = new System.Collections.Generic.List<int>(typeListByAttrId.Count);
      int result = 0;
      foreach (object obj in typeListByAttrId)
      {
        if (int.TryParse(Convert.ToString(obj), out result))
          intList.Add(result);
      }
    }
    return intList == null || intList.Count <= 0 ? (System.Collections.Generic.List<int>) null : intList;
  }

  /// <summary>
  /// Заполнить список идентификаторов добавленных элементов.
  /// Список необходим для того, чтобы повторно не добавлять существующие элементы.
  /// </summary>
  private System.Collections.Generic.List<long> FillIDsList(iGrid grid, bool selectedRowsOnly = false)
  {
    bool flag = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      flag = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    System.Collections.Generic.List<long> longList = new System.Collections.Generic.List<long>();
    int num1 = !selectedRowsOnly ? grid.Rows.Count : grid.SelectedCells.Count;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int index2 = !selectedRowsOnly ? index1 : grid.SelectedCells[index1].RowIndex;
        object obj = this._describer == null || grid.Rows[index2].Tag is ObjectIDToCaption ? this.GetItemForAttributeValues(grid.Rows[index2].Tag) : this.OnGetItemForAttributeValuesFromDescriber(grid.Rows[index2].Tag);
        if (obj == DBNull.Value)
          obj = (object) -1L;
        long int64 = Convert.ToInt64(obj);
        long num2 = int64;
        if (int64 != -1L && flag)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(Math.Abs(int64), false);
          if (objectActualCopy != null)
            num2 = objectActualCopy.ObjectID;
          else
            continue;
        }
        if (!longList.Contains(num2))
          longList.Add(num2);
      }
    }
    return longList;
  }

  /// <summary>Выбор значения через дескриптор.</summary>
  /// <param name="selectedItem">Выбранный в списке элемент</param>
  /// <returns>Список новых элементов</returns>
  private Dictionary<long, object> GetValueFromDescriber(object selectedItem)
  {
    Dictionary<long, object> dictionary = (Dictionary<long, object>) null;
    if (this._describer.GetPropDescriptorEditor(this._attrValues.AttributeID) is UITypeEditor descriptorEditor)
    {
      using (ServiceContainer provider = new ServiceContainer())
      {
        using (DropDownEditorForm serviceInstance = new DropDownEditorForm())
        {
          provider.AddService(typeof (IWindowsFormsEditorService), (object) serviceInstance);
          ITypeDescriptorContext context = (ITypeDescriptorContext) new ControlsContext(this.Values, this._describer, this.ParentInfo);
          switch (descriptorEditor.GetEditStyle(context))
          {
            case UITypeEditorEditStyle.Modal:
            case UITypeEditorEditStyle.DropDown:
              object newValueObj = descriptorEditor.EditValue(context, (System.IServiceProvider) provider, selectedItem);
              if ((newValueObj == null ? 0 : (newValueObj is object[] ? 1 : (!this.CompareEditorValues(newValueObj, selectedItem) ? 1 : 0))) != 0)
              {
                if (!(newValueObj is object[] objArray))
                  objArray = new object[1]{ newValueObj };
                dictionary = new Dictionary<long, object>(objArray.Length);
                foreach (object propertyValue in objArray)
                {
                  object attributeValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, propertyValue);
                  if (attributeValue != null && attributeValue != DBNull.Value && attributeValue is long)
                  {
                    long int64 = Convert.ToInt64(attributeValue);
                    if (!dictionary.ContainsKey(int64))
                      dictionary.Add(int64, propertyValue);
                  }
                }
                break;
              }
              break;
          }
        }
      }
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<long, object>) null : dictionary;
  }

  private bool CompareEditorValues(object newValueObj, object oldValueObj)
  {
    object attributeValue1 = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, newValueObj);
    object attributeValue2 = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, oldValueObj);
    if (attributeValue2 is long && attributeValue1 is long num)
      return (long) attributeValue2 == num;
    return (attributeValue2 == null || attributeValue2 == DBNull.Value) && (attributeValue1 == null || attributeValue1 == DBNull.Value);
  }

  /// <summary>Инициализируем сервисы.</summary>
  private void InitServices()
  {
    object service1 = ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole));
    if (service1 != null)
      this._services.AddService(typeof (ICurrentUserAndRole), service1);
    object service2 = (object) (ApplicationServices.Container.GetService(typeof (IDefaultCommands4ObjTypes)) as IDefaultCommands4ObjTypes);
    if (service2 != null)
      this._services.AddService(typeof (IDefaultCommands4ObjTypes), service2);
    this._services.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InParametersCard));
    this._services.AddService(typeof (IIODispatcher), (object) new IODispatcher());
    if (ApplicationServices.Container.GetService(typeof (INotificationService)) is INotificationService service3)
      this._services.AddService(typeof (INotificationService), (object) service3);
    if (service3 is SwitchedNotificationService notificationService)
      notificationService.Enabled = true;
    object service4 = (object) (ApplicationServices.Container.GetService(typeof (IFiltrationService)) as IFiltrationService);
    if (service4 == null)
      return;
    this._services.AddService(typeof (IFiltrationService), service4);
  }

  /// <summary>Освобождаем сервисы.</summary>
  private void ReleaseServices()
  {
    this._services.RemoveService(typeof (ICurrentUserAndRole));
    this._services.RemoveService(typeof (IDefaultCommands4ObjTypes));
    this._services.RemoveService(typeof (INotificationService));
    this._services.RemoveService(typeof (IFiltrationService));
  }

  /// <summary>Проверка доступности кнопок и пунктов меню.</summary>
  protected void CheckAccessibilityButtons()
  {
    if (this._btnForm != null)
      this._btnForm.Enabled = this._mbiForm.Enabled = this.iGrid.Rows.Count != 0 && this.iGrid.SelectedCells.Count == 1;
    this._mbiPaste.Enabled = this.IsPasteEnabled;
    if (this._enabled)
    {
      this._mbiAdd.Enabled = this.MaxCountValue == 0 || this.MaxCountValue > this.iGrid.Rows.Count;
      if (this.iGrid.Rows.Count == 0)
        this._mbiEdit.Enabled = this._mbiDel.Enabled = this._mbiClear.Enabled = false;
      else if (this.iGrid.SelectedCells.Count == 0)
      {
        this._mbiEdit.Enabled = this._mbiDel.Enabled = false;
        this._mbiClear.Enabled = true;
      }
      else
      {
        this._mbiDel.Enabled = this._mbiClear.Enabled = true;
        this._mbiEdit.Enabled = this.iGrid.SelectedCells.Count == 1;
      }
    }
    else
      this._mbiAdd.Enabled = this._mbiEdit.Enabled = this._mbiDel.Enabled = this._mbiClear.Enabled = false;
    if (this._enabled)
    {
      this._btnAdd.Enabled = this._miAdd.Enabled = this.MaxCountValue == 0 || this.MaxCountValue > this.iGrid.Rows.Count;
      if (!this.DesignMode)
      {
        if (this.iGrid.Rows.Count == 0)
        {
          this._btnEdit.Enabled = this._btnDel.Enabled = this._btnClear.Enabled = false;
          this._miEdit.Enabled = this._miDel.Enabled = this._miClear.Enabled = false;
          this.Error = !this._disableNulls || !this.EnabledCtrl ? string.Empty : this._errMsg_NullValue;
        }
        else
        {
          if (this.iGrid.SelectedCells.Count == 0)
          {
            this._btnEdit.Enabled = this._btnDel.Enabled = false;
            this._btnClear.Enabled = true;
            this._miEdit.Enabled = this._miDel.Enabled = false;
            this._miClear.Enabled = true;
          }
          else
          {
            this._btnDel.Enabled = this._btnClear.Enabled = true;
            this._miDel.Enabled = this._miClear.Enabled = true;
            this._btnEdit.Enabled = this._miEdit.Enabled = this.iGrid.SelectedCells.Count == 1;
          }
          this.Error = string.Empty;
        }
      }
    }
    else
    {
      this._btnAdd.Enabled = this._btnEdit.Enabled = this._btnDel.Enabled = this._btnClear.Enabled = false;
      this._miAdd.Enabled = this._miEdit.Enabled = this._miDel.Enabled = this._miClear.Enabled = false;
      this.Error = string.Empty;
    }
    this.Invalidate();
  }

  protected iGRow DataCreateOrModifyItemForRow(
    iGrid grid,
    DataRow itemRow,
    bool fromDescriber = false,
    object item = null,
    iGRow igModifiedRow = null)
  {
    bool _objectVersionProcessed = true;
    if (this.AttributeInfo != null && this._attrValues != null)
      _objectVersionProcessed = this._attrValues.AttributeType != FieldTypes.ftObjectLinkByID;
    iGRow modifyItemForRow = igModifiedRow == null ? grid.Rows.Add() : igModifiedRow;
    int columnsRawTableIdIndex = this.dataColumnsRawTableIdIndex;
    for (int index = 0; index < this.dataCurrentColumns.Count; ++index)
    {
      modifyItemForRow.Cells[index].Value = (object) Convert.ToString(itemRow[index]);
      this.AdjustCellForImages(modifyItemForRow.Cells[index], itemRow[index]);
    }
    if (fromDescriber)
    {
      modifyItemForRow.Tag = item;
    }
    else
    {
      ObjectIDToCaption objectIdToCaption = new ObjectIDToCaption(Convert.ToInt64(itemRow[columnsRawTableIdIndex]), _objectVersionProcessed);
      modifyItemForRow.Tag = (object) objectIdToCaption;
    }
    return modifyItemForRow;
  }

  /// <summary>Получение элемента.</summary>
  /// <param name="value">Элемент в списке элементов</param>
  /// <returns>Значение</returns>
  protected object GetItemForAttributeValues(object value)
  {
    object obj = (object) DBNull.Value;
    return !(value is ObjectIDToCaption objectIdToCaption) ? obj : (object) objectIdToCaption.ObjectID;
  }

  /// <summary>Удаление элемента.</summary>
  /// <returns></returns>
  protected bool DeleteItem()
  {
    bool flag = false;
    if (this.iGrid.CurRow != null)
    {
      string caption = LocalizationHolder.rm.GetString("AttrListBox_DeleteItem_Caption");
      if (MessageBox.Show(LocalizationHolder.rm.GetString("AttrListBox_DeleteItem_Message"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.No)
      {
        for (int index = this.iGrid.SelectedCells.Count - 1; index >= 0; --index)
        {
          int rowIndex = this.iGrid.SelectedCells[index].RowIndex;
          this.dataValuesRawTable.Rows[rowIndex].Delete();
          this.iGrid.Rows.RemoveAt(rowIndex);
        }
        this.CheckAccessibilityButtons();
        this.Modified = flag = true;
      }
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected bool ClearItems()
  {
    bool flag = false;
    string caption = LocalizationHolder.rm.GetString("AttrListBox_ClearList_Caption");
    if (MessageBox.Show(LocalizationHolder.rm.GetString("AttrListBox_ClearList_Message"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.No)
    {
      this.iGrid.CurRow = (iGRow) null;
      this.iGrid.Rows.Clear();
      this.dataValuesRawTable = (DataTable) null;
      this.CheckAccessibilityButtons();
      this.Modified = flag = true;
    }
    return flag;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.Enter -= new EventHandler(this.AttrObjectsListBase_Enter);
      this.iGrid.SelectionChanged -= new EventHandler(this.iGrid_SelectionChanged);
      this.iGrid.DoubleClick -= new EventHandler(this.iGrid_DoubleClick);
      this.iGrid.KeyDown -= new KeyEventHandler(this.iGrid_KeyDown);
      this._miAdd.Click -= new EventHandler(this.OnAddEdit_Click);
      this._miDel.Click -= new EventHandler(this.OnDel_Click);
      this._miEdit.Click -= new EventHandler(this.OnAddEdit_Click);
      this._miClear.Click -= new EventHandler(this.OnClear_Click);
      if (this._btnAdd != null)
        this._btnAdd.Click -= new EventHandler(this.OnAddEdit_Click);
      if (this._btnDel != null)
        this._btnDel.Click -= new EventHandler(this.OnDel_Click);
      if (this._btnEdit != null)
        this._btnEdit.Click -= new EventHandler(this.OnAddEdit_Click);
      if (this._btnClear != null)
        this._btnClear.Click -= new EventHandler(this.OnClear_Click);
      this.MenuItemClick -= new EventHandler(this.On_btnAddEdit_Click);
      this._bar.Dispose();
      this.ReleaseServices();
      this._services.Dispose();
      if (this._btnForm != null)
        this._btnForm.Click -= new EventHandler(this.On_btnForm_Click);
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
    iGColPattern iGcolPattern = new iGColPattern();
    this.iGridCol0CellStyle = new iGCellStyle(true);
    this.iGridCol0ColHdrStyle = new iGColHdrStyle(true);
    this._menu = new ContextMenuStrip(this.components);
    this._miAdd = new ToolStripMenuItem();
    this._miDel = new ToolStripMenuItem();
    this._miEdit = new ToolStripMenuItem();
    this._miClear = new ToolStripMenuItem();
    this.iGrid = new iGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    ((ISupportInitialize) this._err).BeginInit();
    this._menu.SuspendLayout();
    ((ISupportInitialize) this.iGrid).BeginInit();
    this.SuspendLayout();
    this._menu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._miAdd,
      (ToolStripItem) this._miDel,
      (ToolStripItem) this._miEdit,
      (ToolStripItem) this._miClear
    });
    this._menu.Name = "_menu";
    this._menu.Size = new Size(152, 108);
    this._miAdd.Name = "_miAdd";
    this._miAdd.Size = new Size(151, 26);
    this._miAdd.Text = "Добавить";
    this._miAdd.Click += new EventHandler(this._miAdd_Click);
    this._miDel.Name = "_miDel";
    this._miDel.Size = new Size(151, 26);
    this._miDel.Text = "Удалить";
    this._miDel.Click += new EventHandler(this._miDel_Click);
    this._miEdit.Name = "_miEdit";
    this._miEdit.Size = new Size(151, 26);
    this._miEdit.Text = "Изменить";
    this._miEdit.Click += new EventHandler(this._miAdd_Click);
    this._miClear.Name = "_miClear";
    this._miClear.Size = new Size(151, 26);
    this._miClear.Text = "Очистить";
    this._miClear.Click += new EventHandler(this._miClear_Click);
    iGcolPattern.CellStyle = this.iGridCol0CellStyle;
    iGcolPattern.ColHdrStyle = this.iGridCol0ColHdrStyle;
    this.iGrid.Cols.AddRange(new iGColPattern[1]
    {
      iGcolPattern
    });
    this.iGrid.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this.iGrid.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this.iGrid.Dock = DockStyle.Fill;
    this.iGrid.Header.Height = 19;
    this.iGrid.Location = new Point(0, 0);
    this.iGrid.Name = "iGrid";
    this.iGrid.RowMode = true;
    this.iGrid.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this.iGrid.SelectionMode = iGSelectionMode.MultiExtended;
    this.iGrid.Size = new Size(251, 179);
    this.iGrid.TabIndex = 1;
    this.iGrid.CellMouseUp += new iGCellMouseUpEventHandler(this.iGrid_CellMouseUp);
    this.iGrid.SelectionChanged += new EventHandler(this.iGrid_SelectionChanged);
    this.iGrid.DoubleClick += new EventHandler(this.iGrid_DoubleClick);
    this.iGrid.KeyDown += new KeyEventHandler(this.iGrid_KeyDown);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.iGrid);
    this._err.SetIconAlignment((Control) this, ErrorIconAlignment.TopLeft);
    this._err.SetIconPadding((Control) this, -16);
    this.Name = nameof (AttrObjectsListBase);
    this.Size = new Size(251, 179);
    this.Enter += new EventHandler(this.AttrObjectsListBase_Enter);
    ((ISupportInitialize) this._err).EndInit();
    this._menu.ResumeLayout(false);
    ((ISupportInitialize) this.iGrid).EndInit();
    this.ResumeLayout(false);
  }
}
