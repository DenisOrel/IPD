// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SelectAttributeControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Форма "Настройка отображения" - позволяет редактировать список отображаемых в "Навигаторе" колонок атрибутов
/// </summary>
public class SelectAttributeControl : UserControl
{
  private static Guid ObjectColumnSchemeGuid = new Guid("{A862393F-0C3B-413e-8F93-2AF3C87B0CE4}");
  private static Guid AvsVirtualColumnSchemeGuid = new Guid("{D201D543-F7C3-4B31-BE58-93ADC0445C85}");
  private static Guid RelationColumnSchemeGuid = new Guid("{E92DFDEF-E046-41b4-9B11-AC3575C6673A}");
  /// <summary>Если true, то идёт работа внутри обработчика событий</summary>
  protected bool inEvent;
  private ViewType viewType = ViewType.All;
  /// <summary>
  /// Узел, для которого вызывается окно настройки отображения
  /// </summary>
  protected INode node;
  /// <summary>
  /// Для какого содержимого вызывается настройка отображения
  /// </summary>
  protected ContentType content;
  /// <summary>
  /// Описание родительского элемента, содержимое которого изучается
  /// </summary>
  protected INodeID nodeID;
  /// <summary>Коллекция фильтров для списка атрибутов</summary>
  protected static List<MyElement> filters = new List<MyElement>(1);
  /// <summary>
  /// Коллекция пар значений [(Guid)Схема] = [(string)Название схемы]
  /// </summary>
  protected HybridDictionary FSchemesNames = new HybridDictionary(0);
  /// <summary>Список поддерживаемых колонок</summary>
  protected NodeColumnCollection FSupportedColumns;
  /// <summary>Кэш списка поддерживаемых колонок</summary>
  internal static NodeColumnCollection FSupportedColumnsCache = (NodeColumnCollection) null;
  /// <summary>Словарик для быстрого поиска колонок</summary>
  protected Dictionary<int, NodeColumn> DictSupportedColumns = new Dictionary<int, NodeColumn>();
  /// <summary>Кэш словаря быстрого поиска колонок</summary>
  internal static Dictionary<int, NodeColumn> DictSupportedColumnsCache = (Dictionary<int, NodeColumn>) null;
  /// <summary>Обратный словарик для быстрого поиска индекса колонок</summary>
  protected Dictionary<NodeColumn, int> DictRevSupportedColumns = new Dictionary<NodeColumn, int>();
  /// <summary>Кэш обратного словаря быстрого поиска индекса колонок</summary>
  internal static Dictionary<NodeColumn, int> DictRevSupportedColumnsCache = (Dictionary<NodeColumn, int>) null;
  /// <summary>Были ли изменения в настраиваемом виде</summary>
  protected bool FIsChanged;
  /// <summary>Форма закрывается по нажатию "ОК"</summary>
  protected bool _okPressed;
  /// <summary>Коллекция разных настроек контролов формы</summary>
  protected HybridDictionary FControlsSettings = new HybridDictionary(0, true);
  /// <summary>Коллекция изображений для разных категорий</summary>
  protected ICategoryTypeIconService FAttrTypesIcons;
  /// <summary>Сервис для регистрации своих категорий</summary>
  protected IGuidMapper FGuidMapper;
  /// <summary>ID категории своих значков</summary>
  protected static int FIconsCategory = 0;
  /// <summary>Индексы своих значков</summary>
  protected static int[] FIcons = (int[]) null;
  /// <summary>Сортировать ли список доступных колонок</summary>
  protected bool AutoSortAvailableColumns;
  /// <summary>Менеджер контекстного поиска в дереве</summary>
  protected SelectAttributeControl.iAttrContextSearchManager _contextSearchManager;
  /// <summary>Идентификатор родительского типа объектов</summary>
  protected int parentObjectTypeID = -1;
  /// <summary>Идентификаторы допустимых типов связей</summary>
  protected List<int> parentRelationTypesID = new List<int>();
  /// <summary>Список типов атрибутов для родительского типа объекта</summary>
  protected List<IMSAttribute4ObjectType> objectTypeAttrs;
  /// <summary>Список типов атрибутов для допустимых типов связей</summary>
  protected List<IMSAttribute4RelationType> relationsTypeAttrs = new List<IMSAttribute4RelationType>();
  /// <summary>Список видимых групп</summary>
  protected List<SelectAttributeControl.TreeNodeSchemeItem> treeGroups = new List<SelectAttributeControl.TreeNodeSchemeItem>();
  /// <summary>Видимые группы и коллекции колонок</summary>
  protected Dictionary<Guid, List<int>> treeColumns = new Dictionary<Guid, List<int>>();
  /// <summary>Строки дерева, соответствующие группам колонок</summary>
  protected Dictionary<Guid, Row> treeGroupRows = new Dictionary<Guid, Row>();
  /// <summary>
  /// Словарик атрибутов, которые доступны текущему пользователю
  /// </summary>
  protected Dictionary<int, int> filteredAttributes = new Dictionary<int, int>();
  /// <summary>
  /// Кэш словаря атрибутов, которые доступны текущему пользователю
  /// </summary>
  internal static Dictionary<int, int> filteredAttributesCache = (Dictionary<int, int>) null;
  /// <summary>
  /// Прямоугольник, в котором "всё началось" - для списка доступных атрибутов
  /// </summary>
  protected Rectangle dragBoxFromMouseDownAll;
  /// <summary>
  /// Прямоугольник, в котором "всё началось" - для списка видимых атрибутов
  /// </summary>
  protected Rectangle dragBoxFromMouseDownVisible;
  /// <summary>Смещение</summary>
  protected Point screenOffset;
  protected static Dictionary<Guid, INodeColumnScheme> FColumnSchemesCache = (Dictionary<Guid, INodeColumnScheme>) null;
  private RelationColumnsScheme relationColumnsScheme = new RelationColumnsScheme();
  private ObjectColumnsScheme objectColumnsScheme = new ObjectColumnsScheme();
  private NodeColumnCollection supportedNodeColumnCollection = new NodeColumnCollection();
  private NodeColumnCollection selectedNodeColumnCollection = new NodeColumnCollection();
  private ReadOnlyCollection<object> possibleAttributesIDs;
  private List<int> _objTypes;
  private List<int> _relTypes;
  private List<AVSColumnScheme> _schemesList = new List<AVSColumnScheme>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel panel1;
  protected ComboBox cbFind;
  protected Label label1;
  protected ComboBox comboSearch;
  protected Label labelSearch;
  protected Intermech.Bars.ToolBar toolBarTree;
  protected ImageList imagesToolbars;
  protected ButtonItem btCollapse;
  protected ButtonItem btExpand;
  protected ButtonItem btShowAll;
  protected Intermech.VirtualTreeView.VirtualTreeView treeAvailable;
  protected Column columnAttribute;
  protected Panel panelLeft;
  protected Timer timerReload;
  protected ToolTip toolTip;
  protected ImageList images;

  /// <summary>Сервис для управления схемами колонок</summary>
  protected IColumnSchemes FColumnSchemes
  {
    get => ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
  }

  /// <summary>
  /// Извещение для внешних подписчиков о щелчке мышью на дереве доступных атрибутов
  /// </summary>
  public event MouseEventHandler AttributesTreeClicked;

  /// <summary>
  /// Извещение для внешних подписчиков о двойном щелчке мышью на дереве доступных атрибутов
  /// </summary>
  public event EventHandler AttributesTreeDoubleClicked;

  /// <summary>
  /// Создать экземпляр формы (конструктор предназначенный для классов-потомков, чтобы у них дизайнер форм работал)
  /// </summary>
  public SelectAttributeControl()
  {
    this.InitializeComponent();
    if (this.IsDesignerHosted())
      return;
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
      this.treeAvailable.MouseClick += (MouseEventHandler) ((s, e) => this.OnAvailableAttributesTreeClicked(s, e));
    }
    this._contextSearchManager = new SelectAttributeControl.iAttrContextSearchManager(this, this.treeAvailable);
  }

  public void Select(NodeColumnCollection supportedColumns = null, List<AVSColumnScheme> schemesList = null)
  {
    List<int> relationTypeIDs = new List<int>();
    List<int> objectTypeIDs = new List<int>();
    if (this.RelTypes != null)
      relationTypeIDs.AddRange((IEnumerable<int>) this.RelTypes);
    if (this.ObjTypes != null)
      objectTypeIDs.AddRange((IEnumerable<int>) this.ObjTypes);
    List<AVSColumnScheme> avsColumnSchemeList = new List<AVSColumnScheme>();
    RelationColumnsScheme relationColumnsScheme = new RelationColumnsScheme();
    relationColumnsScheme.SchemeGuid = SelectAttributeControl.RelationColumnSchemeGuid;
    this.relationColumnsScheme = relationColumnsScheme;
    this.relationColumnsScheme.AddRelationTypes((IList<int>) relationTypeIDs);
    if (this.ViewType.HasFlag((Enum) ViewType.Links))
      avsColumnSchemeList.Add((AVSColumnScheme) this.relationColumnsScheme);
    ObjectColumnsScheme objectColumnsScheme = new ObjectColumnsScheme();
    objectColumnsScheme.SchemeGuid = SelectAttributeControl.ObjectColumnSchemeGuid;
    this.objectColumnsScheme = objectColumnsScheme;
    this.objectColumnsScheme.AddObjectTypes((IList<int>) objectTypeIDs);
    if (this.ViewType.HasFlag((Enum) ViewType.Objects))
      avsColumnSchemeList.Add((AVSColumnScheme) this.objectColumnsScheme);
    if (!this.CustomColumnSchemes.IsNullOrEmpty<AVSColumnScheme>())
      avsColumnSchemeList.AddRange((IEnumerable<AVSColumnScheme>) this.CustomColumnSchemes);
    bool flag = this.IsCached && SelectAttributeControl.FColumnSchemesCache == null;
    if (flag)
    {
      SelectAttributeControl.FColumnSchemesCache = new Dictionary<Guid, INodeColumnScheme>();
      SelectAttributeControl.FColumnSchemesCache[Intermech.Navigator.Consts.ObjectColumnSchemeGuid] = this.FColumnSchemes[Intermech.Navigator.Consts.ObjectColumnSchemeGuid];
      SelectAttributeControl.FColumnSchemesCache[Intermech.Navigator.Consts.RelationColumnSchemeGuid] = this.FColumnSchemes[Intermech.Navigator.Consts.RelationColumnSchemeGuid];
    }
    foreach (AVSColumnScheme scheme in avsColumnSchemeList)
    {
      this.FColumnSchemes.Register(scheme.SchemeGuid, (INodeColumnScheme) scheme);
      if (flag)
        SelectAttributeControl.FColumnSchemesCache[scheme.SchemeGuid] = this.FColumnSchemes[scheme.SchemeGuid];
      this.possibleAttributesIDs = scheme.PossibleAttributesIDs;
      foreach (object possibleAttributesId in this.possibleAttributesIDs)
        this.supportedNodeColumnCollection.Add(scheme.CreateColumn(scheme.SchemeGuid, possibleAttributesId));
    }
    if (this.ViewType.HasFlag((Enum) ViewType.Objects))
      Intermech.Navigator.DBObjects.Helper.AddAllColumns(this.supportedNodeColumnCollection);
    if (this.ViewType.HasFlag((Enum) ViewType.Links))
      Intermech.Navigator.DBObjects.Helper.AddAllColumnsRelation(this.supportedNodeColumnCollection);
    if (supportedColumns == null)
      supportedColumns = this.supportedNodeColumnCollection;
    if (schemesList == null)
      schemesList = avsColumnSchemeList;
    this._schemesList = schemesList;
    this.Init(supportedColumns, true);
  }

  /// <summary>Режим кэширования включен</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsCached { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> ObjTypes
  {
    get => this._objTypes;
    set => this._objTypes = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> RelTypes
  {
    get => this._relTypes;
    set => this._relTypes = value;
  }

  /// <summary>Дополнительные схемы колонок</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<AVSColumnScheme> CustomColumnSchemes { get; set; }

  /// <summary>Текущая схема колонок</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("Текущая схема колонок")]
  public virtual INodeColumnScheme SelectedScheme
  {
    get
    {
      Row row = this.treeAvailable.SelectedRow;
      while (row != null && !(row.Item is SelectAttributeControl.TreeNodeSchemeItem))
        row = row.ParentRow;
      return row == null || row.Item == null ? (INodeColumnScheme) null : (row.Item as SelectAttributeControl.TreeNodeSchemeItem).Scheme;
    }
  }

  /// <summary>Описание текущей схемы колонок</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("Описание текущей схемы колонок")]
  protected virtual SelectAttributeControl.TreeNodeSchemeItem SelectedSchemeItem
  {
    get
    {
      Row row = this.treeAvailable.SelectedRow;
      while (row != null && !(row.Item is SelectAttributeControl.TreeNodeSchemeItem))
        row = row.ParentRow;
      return row == null || row.Item == null ? (SelectAttributeControl.TreeNodeSchemeItem) null : row.Item as SelectAttributeControl.TreeNodeSchemeItem;
    }
  }

  /// <summary>Выбранный идентификатор атрибута</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int SelectedAttributeId
  {
    get
    {
      Intermech.Interfaces.Attributes.AttributeInfo selectedAttribute = this.SelectedAttribute;
      return selectedAttribute != null ? selectedAttribute.AttributeId : -1;
    }
    set
    {
      List<int> intList = new List<int>();
      foreach (KeyValuePair<int, NodeColumn> dictSupportedColumn in this.DictSupportedColumns)
      {
        if (dictSupportedColumn.Value.Attribute != null && dictSupportedColumn.Value.Attribute.AttributeID == value)
          intList.Add(dictSupportedColumn.Key);
      }
      Row row1 = (Row) null;
      if (this.treeAvailable.RootRow != null)
      {
        for (int childIndex1 = 0; childIndex1 < this.treeAvailable.RootRow.ChildItems.Count; ++childIndex1)
        {
          Row row2 = this.treeAvailable.RootRow.ChildRowByIndex(childIndex1);
          for (int childIndex2 = 0; childIndex2 < row2.ChildItems.Count; ++childIndex2)
          {
            Row row3 = row2.ChildRowByIndex(childIndex2);
            if (row3.Item is int num && intList.Contains(num))
            {
              row1 = row3;
              break;
            }
          }
        }
      }
      this.treeAvailable.SelectedRow = row1;
    }
  }

  /// <summary>Выбранный аттрибут</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Intermech.Interfaces.Attributes.AttributeInfo SelectedAttribute
  {
    get
    {
      NodeColumn selectedNodeColumn = this.SelectedNodeColumn;
      if (selectedNodeColumn == null)
        return (Intermech.Interfaces.Attributes.AttributeInfo) null;
      if (!(selectedNodeColumn.Source is Intermech.Interfaces.Attributes.AttributeInfo selectedAttribute))
      {
        INodeColumnScheme selectedScheme = this.SelectedScheme;
        if (selectedScheme != null)
        {
          selectedAttribute = selectedScheme.FindColumnAttributeInfo(selectedNodeColumn);
          selectedNodeColumn.Source = (INodeColumnSource) selectedAttribute;
        }
      }
      return selectedAttribute;
    }
  }

  /// <summary>
  /// Список выделенных объектов (объекты типа TreeNodeSchemeItem и Int32)
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("Список выделенных объектов (объекты типа TreeNodeSchemeItem и Int32)")]
  public virtual NodeColumn SelectedNodeColumn
  {
    get
    {
      if (this.SelectedItems.Count > 0 && this.SelectedItems[0] is int)
      {
        NodeColumn nodeColumn = this.GetNodeColumn((int) this.SelectedItems[0]);
        if (nodeColumn != null)
          return nodeColumn;
      }
      return (NodeColumn) null;
    }
  }

  /// <summary>
  /// Список выделенных объектов (объекты типа TreeNodeSchemeItem и Int32)
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("Список выделенных объектов (объекты типа TreeNodeSchemeItem и Int32)")]
  protected virtual List<object> SelectedItems
  {
    get
    {
      List<object> selectedItems = new List<object>();
      for (int index = 0; index < this.treeAvailable.SelectedRows.Count; ++index)
      {
        Row selectedRow = this.treeAvailable.SelectedRows[index];
        if ((selectedRow.ParentRow == null || !selectedRow.ParentRow.Selected) && selectedRow.Item != null)
          selectedItems.Add(selectedRow.Item);
      }
      return selectedItems;
    }
  }

  /// <summary>Список индексов доступных колонок</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("Список доступных колонок")]
  protected virtual List<int> AvailableColumns
  {
    get
    {
      List<int> availableColumns = new List<int>();
      for (int index = 0; index < this.treeGroups.Count; ++index)
      {
        if (this.treeGroups[index].Columns != null)
          availableColumns.AddRange((IEnumerable<int>) this.treeGroups[index].Columns);
      }
      return availableColumns;
    }
  }

  /// <summary>Количество выделенных строк, которые содержат данные</summary>
  protected virtual int SelectedRowsCount
  {
    get
    {
      int selectedRowsCount = 0;
      for (int index = 0; index < this.treeAvailable.SelectedRows.Count; ++index)
      {
        if (this.treeAvailable.SelectedRows[index].Item != null)
          ++selectedRowsCount;
      }
      return selectedRowsCount;
    }
  }

  /// <summary>Тип отображения аттрибутов</summary>
  public ViewType ViewType
  {
    get => this.viewType;
    set => this.viewType = value;
  }

  /// <summary>Преобразовать ID колонки в Int32</summary>
  /// <param name="ID">ID колонки</param>
  /// <returns>Int32</returns>
  protected virtual int ConvertIDtoInt32(object ID)
  {
    if (ID is ObligatoryObjectAttributes)
      return (int) ID;
    if (ID is PortalAttributeType)
    {
      int num = ((PortalAttributeType) ID).ID;
      if (num > 0)
        num = -num - 10000;
      return num;
    }
    int result;
    if (!int.TryParse(ID.ToString(), out result))
      result = 0;
    return result;
  }

  /// <summary>По индексу вернуть описание колонки</summary>
  /// <param name="index">Индекс</param>
  /// <returns>Описание колонки или null</returns>
  protected internal virtual NodeColumn GetNodeColumn(int index)
  {
    return this.DictSupportedColumns.ContainsKey(index) ? this.DictSupportedColumns[index] : (NodeColumn) null;
  }

  /// <summary>По колонке вернуть её индекс</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Индекс колонки или -1</returns>
  protected internal virtual int GetNodeColumnIndex(NodeColumn column)
  {
    if (this.DictRevSupportedColumns.ContainsKey(column))
      return this.DictRevSupportedColumns[column];
    if (!this.IsCached)
      return -1;
    int count = SelectAttributeControl.DictSupportedColumnsCache.Keys.Count;
    SelectAttributeControl.DictSupportedColumnsCache.Add(count, column);
    SelectAttributeControl.DictRevSupportedColumnsCache.Add(column, count);
    return count;
  }

  /// <summary>Сравниватель для групп</summary>
  /// <returns></returns>
  protected virtual IComparer<string> GetGroupsComparison()
  {
    return (IComparer<string>) StringComparer.CurrentCulture;
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool DestroyIcon(IntPtr handle);

  /// <summary>Инициализация переменных</summary>
  /// <param name="node">Узел, для которого вызывается окно настройки отображения</param>
  /// <param name="content">Для какого содержимого вызывается настройка отображения</param>
  /// <param name="supportedColumns">Список всех колонок атрибутов</param>
  /// <param name="columns">Список выбранных колонок атрибутов</param>
  /// <param name="fullFormReset">Выполнить полную инициализацию настроек формы</param>
  protected void InitVariables(NodeColumnCollection supportedColumns, bool fullFormReset)
  {
    this._contextSearchManager = new SelectAttributeControl.iAttrContextSearchManager(this, this.treeAvailable);
    this.FSupportedColumns = !this.IsCached || SelectAttributeControl.FSupportedColumnsCache == null ? new NodeColumnCollection() : SelectAttributeControl.FSupportedColumnsCache;
    if (supportedColumns != null && this.FSupportedColumns.Count == 0)
    {
      for (int index = 0; index < supportedColumns.Count; ++index)
      {
        NodeColumn supportedColumn = supportedColumns[index];
        if (this.FSupportedColumns.Find(supportedColumn.Key) == null)
          this.FSupportedColumns.Add(supportedColumn);
      }
      if (this.IsCached)
      {
        SelectAttributeControl.FSupportedColumnsCache = new NodeColumnCollection();
        SelectAttributeControl.FSupportedColumnsCache.AddRange(this.FSupportedColumns.Where<NodeColumn>((System.Func<NodeColumn, bool>) (c => SelectAttributeControl.FColumnSchemesCache.ContainsKey(c.SchemeGuid))));
      }
    }
    if (this.IsCached && SelectAttributeControl.FSupportedColumnsCache != null)
      this.FSupportedColumns = SelectAttributeControl.FSupportedColumnsCache;
    this.FSupportedColumns.Sort(true);
    if (!this.IsCached || SelectAttributeControl.DictRevSupportedColumnsCache == null)
    {
      this.DictSupportedColumns.Clear();
      this.DictRevSupportedColumns.Clear();
      for (int index = 0; index < this.FSupportedColumns.Count; ++index)
      {
        this.DictSupportedColumns.Add(index, this.FSupportedColumns[index]);
        this.DictRevSupportedColumns.Add(this.FSupportedColumns[index], index);
      }
      SelectAttributeControl.DictSupportedColumnsCache = this.DictSupportedColumns;
      SelectAttributeControl.DictRevSupportedColumnsCache = this.DictRevSupportedColumns;
    }
    else if (this.IsCached && SelectAttributeControl.DictSupportedColumnsCache != null)
    {
      this.DictSupportedColumns = SelectAttributeControl.DictSupportedColumnsCache;
      this.DictRevSupportedColumns = SelectAttributeControl.DictRevSupportedColumnsCache;
    }
    if (!fullFormReset)
      return;
    if (!this.IsCached || SelectAttributeControl.filteredAttributesCache == null)
    {
      this.filteredAttributes.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable = sessionKeeper.Session.GetAttributeTypeCollection(-1, true).Select("");
        if (dataTable != null)
        {
          int columnIndex = dataTable.Columns.IndexOf("F_ATTRIBUTE_ID");
          if (columnIndex >= 0)
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              int int32Value = DataSetProcessor.GetInt32Value(dataTable.Rows[index], columnIndex, 0);
              if (int32Value != 0)
                this.filteredAttributes.Add(int32Value, int32Value);
            }
          }
        }
      }
      SelectAttributeControl.filteredAttributesCache = this.filteredAttributes;
    }
    else if (this.IsCached && SelectAttributeControl.filteredAttributesCache != null)
      this.filteredAttributes = SelectAttributeControl.filteredAttributesCache;
    if (this.FGuidMapper == null)
      this.FGuidMapper = ServicesManager.GetService(typeof (IGuidMapper)) as IGuidMapper;
    if (this.FAttrTypesIcons == null)
      this.FAttrTypesIcons = Statics.IconSrv;
    if (SelectAttributeControl.FIconsCategory != 0)
      return;
    SelectAttributeControl.FIconsCategory = this.FGuidMapper.Register(Guid.NewGuid());
    SelectAttributeControl.FIcons = new int[this.images.Images.Count];
    for (int index = 0; index < this.images.Images.Count; ++index)
    {
      using (Icon icon = Icon.FromHandle((this.images.Images[index] as Bitmap).GetHicon()))
      {
        SelectAttributeControl.FIcons[index] = this.FAttrTypesIcons != null ? this.FAttrTypesIcons.AddIcon(icon, SelectAttributeControl.FIconsCategory, index) : -1;
        SelectAttributeControl.DestroyIcon(icon.Handle);
      }
    }
  }

  /// <summary>Инициализация контролов на форме</summary>
  protected void InitControls()
  {
    this.InitFiltersList();
    this.FillAttrsLists();
    this.UpdateControls();
  }

  /// <summary>Инициализация данных</summary>
  /// <param name="node">Узел, для которого вызывается окно настройки отображения</param>
  /// <param name="content">Для какого содержимого вызывается настройка отображения</param>
  /// <param name="supportedColumns">Список всех колонок атрибутов</param>
  /// <param name="columns">Список выбранных колонок атрибутов</param>
  /// <param name="fullFormReset">Выполнить полную инициализацию настроек формы</param>
  /// <param name="nodeIDs">Элементы, содержимое которых будет получено по настроенным колонкам</param>
  protected virtual void Init(NodeColumnCollection supportedColumns, bool fullFormReset)
  {
    if (supportedColumns == null || supportedColumns.Count == 0)
      throw new Exception("Не задано ни одной допустимой колонки Навигатора в списке SupportedColumns.");
    this.InitVariables(supportedColumns, fullFormReset);
    this.InitControls();
  }

  /// <summary>Заполнить список фильтров</summary>
  protected virtual void InitFiltersList()
  {
    bool inEvent = this.inEvent;
    this.inEvent = true;
    string str = this.comboSearch.Text;
    try
    {
      SelectAttributeControl.filters.Clear();
      this.comboSearch.Items.Clear();
      this.FSchemesNames.Clear();
      Regex regex = new Regex("\\S", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
      SelectAttributeControl.filters.Add(new MyElement((object) regex, "Все группы атрибутов", (object) true));
      List<string> stringList = new List<string>(0);
      for (int index = 0; index < this.FSupportedColumns.Count; ++index)
      {
        INodeColumnScheme fcolumnScheme;
        if (this.IsCached)
        {
          if (SelectAttributeControl.FColumnSchemesCache.ContainsKey(this.FSupportedColumns[index].SchemeGuid))
          {
            fcolumnScheme = SelectAttributeControl.FColumnSchemesCache[this.FSupportedColumns[index].SchemeGuid];
          }
          else
          {
            fcolumnScheme = this.FColumnSchemes[this.FSupportedColumns[index].SchemeGuid];
            SelectAttributeControl.FColumnSchemesCache[this.FSupportedColumns[index].SchemeGuid] = fcolumnScheme;
          }
        }
        else
          fcolumnScheme = this.FColumnSchemes[this.FSupportedColumns[index].SchemeGuid];
        if (fcolumnScheme != null)
        {
          this.FSchemesNames[(object) this.FSupportedColumns[index].SchemeGuid] = (object) fcolumnScheme.Name;
          if ((this.btShowAll.Checked || !(this.FSupportedColumns[index].SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid) && !(this.FSupportedColumns[index].SchemeGuid == Intermech.Navigator.Consts.RelationColumnSchemeGuid)) && stringList.IndexOf(fcolumnScheme.Name) < 0)
            stringList.Add(fcolumnScheme.Name);
        }
      }
      stringList.Sort(this.GetGroupsComparison());
      stringList.Sort(this.GetGroupsComparison());
      for (int index = stringList.Count - 1; index >= 0; --index)
        SelectAttributeControl.filters.Insert(1, new MyElement((object) null, $"Группа '{stringList[index]}'", (object) stringList[index]));
      for (int index = 0; index < SelectAttributeControl.filters.Count; ++index)
        this.comboSearch.Items.Add((object) SelectAttributeControl.filters[index]);
      bool flag = false;
      for (int index = 0; index < SelectAttributeControl.filters.Count; ++index)
      {
        if (SelectAttributeControl.filters[index].Caption == str)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        str = string.Empty;
      if (this.comboSearch.Items.Count <= 0)
        return;
      this.comboSearch.SelectedIndex = 0;
    }
    finally
    {
      this.comboSearch.Text = str != string.Empty ? str : this.comboSearch.Text;
      this.inEvent = inEvent;
    }
  }

  /// <summary>Вернуть индекс изображения для указанной колонки</summary>
  /// <param name="attr">Колонка</param>
  /// <returns>Индекс изображения</returns>
  protected virtual int GetAttributeImageIndex(NodeColumn attr)
  {
    return this.GetTypeImageIndex(attr.AttrType);
  }

  /// <summary>Вернуть номер значка для указанного атрибута</summary>
  /// <param name="attrType">Тип данных атрибута</param>
  /// <returns>Номер значка для указанного атрибута</returns>
  protected int GetTypeImageIndex(FieldTypes attrType)
  {
    return this.FAttrTypesIcons == null ? -1 : this.FAttrTypesIcons.IndexOf(3, -1, (object) attrType);
  }

  /// <summary>Получить номер изображения в группе</summary>
  /// <param name="groupName"> Имя группы колонок </param>
  /// <returns> -1 если надо использовать иконку по-умолчанию, иначе номер иконки в списке FIcons </returns>
  protected virtual int GetGroupImageIndex(string groupName)
  {
    int ficon = SelectAttributeControl.FIcons[2];
    if (groupName.IndexOf("связь", StringComparison.InvariantCultureIgnoreCase) >= 0)
      ficon = SelectAttributeControl.FIcons[3];
    if (groupName.IndexOf("элемент", StringComparison.InvariantCultureIgnoreCase) >= 0)
      ficon = SelectAttributeControl.FIcons[4];
    if (groupName.IndexOf("журнал", StringComparison.InvariantCultureIgnoreCase) >= 0)
      ficon = SelectAttributeControl.FIcons[5];
    return ficon;
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public virtual void UpdateControls()
  {
    int selectedRowsCount = this.SelectedRowsCount;
    this.btCollapse.Enabled = this.treeAvailable.RootRow != null && this.treeAvailable.RootRow.NumChildren > 0;
    this.btExpand.Enabled = this.btCollapse.Enabled;
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void AppearanceTuningForm_Load(object sender, EventArgs e)
  {
    Intermech.Client.Core.FormStorage.LoadLayout((Control) this, (IDictionary) this.FControlsSettings);
    if (this.FControlsSettings == null)
      this.FControlsSettings = new HybridDictionary(0, true);
    this.SetControlsState(this.FControlsSettings);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void AppearanceTuningForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.GetControlsState(this.FControlsSettings);
    Intermech.Client.Core.FormStorage.SaveLayout((Control) this, (IDictionary) this.FControlsSettings);
  }

  /// <summary>
  /// Считать хоть-что нибудь из коллекции по указанному ключу
  /// </summary>
  /// <param name="collection">Коллекция настроек</param>
  /// <param name="key">Ключ</param>
  /// <param name="defaultValue">Значение по умолчанию</param>
  /// <returns>Что-нибудь да и вернёт</returns>
  private object GetDicValue(HybridDictionary collection, object key, object defaultValue)
  {
    return collection == null || key == null ? defaultValue : collection[key] ?? defaultValue;
  }

  /// <summary>
  /// Собрать у контролов разные настройки типа ширины, т.п.
  /// </summary>
  /// <param name="controlsState">Коллекция с настройками контролов</param>
  protected virtual void GetControlsState(HybridDictionary controlsState)
  {
  }

  /// <summary>
  /// Установить контролам разные настройки типа ширины, т.п.
  /// </summary>
  /// <param name="controlsState">Коллекция с настройками контролов</param>
  protected virtual void SetControlsState(HybridDictionary controlsState)
  {
  }

  /// <summary>
  /// Удалить недопустимые группы, а у остальных - удалить колонки
  /// </summary>
  /// <param name="eraseAttrs">Удалять атрибуты в группах</param>
  protected virtual void RemoveInvalidGroups(bool eraseAttrs)
  {
    this.FSchemesNames.GetEnumerator().Reset();
    List<Guid> guidList1 = new List<Guid>();
    List<Guid> guidList2 = new List<Guid>();
    for (int index = 0; index < this.FSupportedColumns.Count; ++index)
    {
      Guid schemeGuid = this.FSupportedColumns[index].SchemeGuid;
      if (guidList1.IndexOf(schemeGuid) < 0 && guidList2.IndexOf(schemeGuid) < 0)
      {
        if (!this.btShowAll.Checked && (schemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || schemeGuid == Intermech.Navigator.Consts.RelationColumnSchemeGuid))
        {
          guidList2.Add(schemeGuid);
        }
        else
        {
          guidList1.Add(schemeGuid);
          if (eraseAttrs && this.treeColumns.ContainsKey(schemeGuid))
            this.treeColumns[schemeGuid].Clear();
        }
      }
    }
    for (int index = this.treeGroups.Count - 1; index >= 0; --index)
    {
      if (guidList2.IndexOf(this.treeGroups[index].Guid) >= 0)
      {
        this.treeColumns.Remove(this.treeGroups[index].Guid);
        this.treeGroupRows.Remove(this.treeGroups[index].Guid);
        this.treeGroups.RemoveAt(index);
      }
    }
    this.treeGroups.Sort();
  }

  /// <summary>Создать словарики и списки групп колонок</summary>
  /// <param name="eraseGroups">true - первоначально удалить всю информацию о группах</param>
  /// <param name="eraseAttrs">Удалять атрибуты в группах</param>
  protected virtual void CreateTreeGroups(bool eraseGroups, bool eraseAttrs)
  {
    IDictionaryEnumerator enumerator = this.FSchemesNames.GetEnumerator();
    Guid guid = new Guid("{67D5959C-ED2C-4e35-88FE-340AE0278469}");
    enumerator.Reset();
    if (eraseGroups)
      this.RemoveGroups();
    else
      this.RemoveInvalidGroups(eraseAttrs);
    for (int index = 0; index < this.FSupportedColumns.Count; ++index)
    {
      Guid schemeGuid = this.FSupportedColumns[index].SchemeGuid;
      INodeColumnScheme fcolumnScheme;
      if (this.IsCached)
      {
        if (SelectAttributeControl.FColumnSchemesCache.ContainsKey(schemeGuid))
        {
          fcolumnScheme = SelectAttributeControl.FColumnSchemesCache[schemeGuid];
        }
        else
        {
          fcolumnScheme = this.FColumnSchemes[schemeGuid];
          SelectAttributeControl.FColumnSchemesCache[schemeGuid] = fcolumnScheme;
        }
      }
      else
        fcolumnScheme = this.FColumnSchemes[this.FSupportedColumns[index].SchemeGuid];
      if (fcolumnScheme != null)
      {
        bool flag = !this.treeGroupRows.ContainsKey(schemeGuid);
        List<int> columns;
        if (!this.treeColumns.ContainsKey(schemeGuid))
        {
          columns = new List<int>();
          this.treeColumns.Add(schemeGuid, columns);
          flag = true;
        }
        else
          columns = this.treeColumns[schemeGuid];
        if ((this.btShowAll.Checked || !(schemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid) && !(schemeGuid == Intermech.Navigator.Consts.RelationColumnSchemeGuid)) && flag)
        {
          this.treeGroups.Add(new SelectAttributeControl.TreeNodeSchemeItem(schemeGuid, fcolumnScheme, columns));
          this.treeGroupRows.Add(schemeGuid, (Row) null);
        }
      }
    }
    this.treeGroups.Sort();
  }

  /// <summary>Найти строку в дереве для указанной группы колонок</summary>
  /// <param name="schemeGuid">Guid группы колонок</param>
  /// <returns>Строка для указанной группы колонок или null</returns>
  protected virtual Row FindSchemeRow(Guid schemeGuid)
  {
    return !this.treeGroupRows.ContainsKey(schemeGuid) ? (Row) null : this.treeGroupRows[schemeGuid];
  }

  /// <summary>Удалить пустые допустимые группы</summary>
  protected virtual void RemoveEmptyGroups()
  {
    for (int index = this.treeGroups.Count - 1; index >= 0; --index)
    {
      if (this.treeGroups[index].Columns == null || this.treeGroups[index].Columns.Count == 0)
      {
        this.treeGroupRows.Remove(this.treeGroups[index].Guid);
        this.treeColumns.Remove(this.treeGroups[index].Guid);
        this.treeGroups.RemoveAt(index);
      }
    }
  }

  /// <summary>Выполнить сортировку групп и их элементов</summary>
  protected virtual void SortGroups()
  {
    for (int index = this.treeGroups.Count - 1; index >= 0; --index)
      this.treeGroups[index].Columns.Sort();
  }

  /// <summary>Удалить все допустимые группы</summary>
  protected virtual void RemoveGroups()
  {
    this.treeGroups.Clear();
    this.treeColumns.Clear();
    this.treeGroupRows.Clear();
  }

  /// <summary>Выбрать в дереве первый узел (с учётом сортировки)</summary>
  protected virtual void FocusFirstNode()
  {
    if (this.treeAvailable.RootRow.NumChildren == 0)
      return;
    this.treeAvailable.RootRow.ExpandChildren(true);
    this.treeAvailable.FocusRow = this.treeAvailable.RootRow.ChildRowByIndex(0);
    if (this.treeAvailable.FocusRow.NumChildren == 0)
      return;
    this.treeAvailable.FocusRow = this.treeAvailable.FocusRow.ChildRowByIndex(0);
    this.treeAvailable.SelectedRow = this.treeAvailable.FocusRow;
  }

  /// <summary>Выбрать в дереве первый узел указанной группы</summary>
  protected virtual void FocusGroup(Guid schemeGuid)
  {
    Row schemeRow = this.FindSchemeRow(schemeGuid);
    if (schemeRow == null)
    {
      this.FocusFirstNode();
    }
    else
    {
      this.treeAvailable.FocusRow = schemeRow;
      this.treeAvailable.SelectedRow = schemeRow;
    }
  }

  /// <summary>
  /// Раскрыть/свернуть строки, соответствующие указанным схемам колонок
  /// </summary>
  /// <param name="schemes">Список Guid схем колонок</param>
  /// <param name="expanded">true - раскрыть строки, иначе свернуть</param>
  protected virtual void ExpandRows(List<Guid> schemes, bool expanded)
  {
    if (schemes == null)
      return;
    for (int childIndex = 0; childIndex < this.treeAvailable.RootRow.NumChildren; ++childIndex)
    {
      Row row = this.treeAvailable.RootRow.ChildRowByIndex(childIndex);
      SelectAttributeControl.TreeNodeSchemeItem treeNodeSchemeItem = row.Item as SelectAttributeControl.TreeNodeSchemeItem;
      if (schemes.IndexOf(treeNodeSchemeItem.Guid) >= 0)
        row.Expanded = expanded;
    }
  }

  /// <summary>Заполнить списки доступных и видимых атрибутов ()</summary>
  protected virtual void FillAttrsLists()
  {
    string upper = this.cbFind.Text.ToUpper();
    bool inEvent = this.inEvent;
    try
    {
      this.inEvent = true;
      MyElement myElement = (MyElement) null;
      if (this.comboSearch.SelectedIndex >= 0)
        myElement = this.comboSearch.Items[this.comboSearch.SelectedIndex] as MyElement;
      Regex regex = (Regex) null;
      if (myElement != null)
        regex = myElement.Value as Regex;
      this.CreateTreeGroups(true, true);
      for (int index = 0; index < this.FSupportedColumns.Count; ++index)
      {
        NodeColumn fsupportedColumn = this.FSupportedColumns[index];
        int key = this.ConvertIDtoInt32(fsupportedColumn.ID);
        if ((this.filteredAttributes.ContainsKey(key) || key <= -10000) && (this.btShowAll.Checked || !fsupportedColumn.SystemAttr && !(fsupportedColumn.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid) && !(fsupportedColumn.SchemeGuid == Intermech.Navigator.Consts.RelationColumnSchemeGuid)) && (regex == null || regex.IsMatch(fsupportedColumn.Caption)) && (!(upper != string.Empty) || fsupportedColumn.Caption.ToUpper().IndexOf(upper) >= 0))
          this.treeColumns[fsupportedColumn.SchemeGuid].Add(this.GetNodeColumnIndex(fsupportedColumn));
      }
    }
    finally
    {
      this.RemoveEmptyGroups();
      this.SortGroups();
      this.FillTree(true);
      this.treeAvailable.RootRow.ExpandChildren(true);
      this.inEvent = inEvent;
    }
  }

  /// <summary>Заполнить список доступных атрибутов заново</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoReloadAttrsList(object sender, EventArgs e)
  {
    if (this.inEvent)
      return;
    string upper = this.cbFind.Text.ToUpper();
    bool inEvent = this.inEvent;
    try
    {
      this.inEvent = true;
      MyElement myElement = (MyElement) null;
      if (this.comboSearch.SelectedIndex >= 0)
        myElement = this.comboSearch.Items[this.comboSearch.SelectedIndex] as MyElement;
      Regex regex = (Regex) null;
      if (myElement != null)
        regex = myElement.Value as Regex;
      if (myElement != null && myElement.Tag != null && myElement.Tag.Equals((object) true))
      {
        regex = (Regex) null;
        myElement = (MyElement) null;
      }
      this.CreateTreeGroups(false, true);
      for (int index = 0; index < this.FSupportedColumns.Count; ++index)
      {
        NodeColumn fsupportedColumn = this.FSupportedColumns[index];
        int key = this.ConvertIDtoInt32(fsupportedColumn.ID);
        if (this.filteredAttributes.ContainsKey(key) || key <= -10000)
        {
          bool flag = true;
          if (myElement != null)
          {
            string tag = (string) myElement.Tag;
            if ((this.IsCached ? SelectAttributeControl.FColumnSchemesCache[fsupportedColumn.SchemeGuid] : this.FColumnSchemes[fsupportedColumn.SchemeGuid]).Name != tag)
              continue;
          }
          if (regex != null)
          {
            if (regex.IsMatch(fsupportedColumn.Caption))
              flag = true;
            else
              continue;
          }
          if (upper != string.Empty)
            flag = fsupportedColumn.Caption.Trim().ToUpper().IndexOf(upper) >= 0;
          if (!this.btShowAll.Checked)
          {
            if (fsupportedColumn.SystemAttr)
              flag = false;
            if (fsupportedColumn.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || fsupportedColumn.SchemeGuid == Intermech.Navigator.Consts.RelationColumnSchemeGuid)
              flag = false;
          }
          if (flag)
            this.treeColumns[fsupportedColumn.SchemeGuid].Add(this.GetNodeColumnIndex(fsupportedColumn));
        }
      }
    }
    finally
    {
      this.RemoveEmptyGroups();
      this.InitFiltersList();
      this.FillTree(true);
      this.FocusFirstNode();
      if (sender == this.cbFind && this.treeAvailable.RootRow != null)
        this.treeAvailable.RootRow.ExpandChildren(true);
      this.inEvent = inEvent;
    }
  }

  /// <summary>Добавить в указанный список колонку атрибута</summary>
  /// <param name="list">Список, в который надо добавлять описание</param>
  /// <param name="attr">Описание колонки атрибута</param>
  /// <returns>Добавленный элемент или null</returns>
  protected virtual ListViewItem AddListItem(ListView list, NodeColumn attr)
  {
    if (list == null || attr == null)
      return (ListViewItem) null;
    attr.Tag = (object) null;
    int attributeImageIndex = this.GetAttributeImageIndex(attr);
    this.GetTypeImageIndex(attr.AttrType);
    string str = "связь";
    ListViewItem listViewItem = list.Items.Add($"  {attr.Caption}", attributeImageIndex);
    int num = this.ConvertIDtoInt32(attr.ID);
    bool flag = num > 0 && this.nodeID != null;
    if (this.nodeID != null && this.node != null && num > 0)
    {
      if ((this.node.Options & NodeOptions.CanContainsComposition) == NodeOptions.CanContainsComposition & attr.Caption.EndsWith(str))
      {
        for (int index = 0; index < this.relationsTypeAttrs.Count; ++index)
        {
          if (this.relationsTypeAttrs[index].AttributeID == num)
          {
            flag = false;
            break;
          }
        }
      }
      if (this.objectTypeAttrs != null & flag)
      {
        for (int index = 0; index < this.objectTypeAttrs.Count; ++index)
        {
          if (this.objectTypeAttrs[index].AttributeID == num)
          {
            flag = false;
            break;
          }
        }
      }
    }
    if (flag)
    {
      attr.Tag = (object) true;
      listViewItem.Font = new Font(list.Font, FontStyle.Bold);
      listViewItem.ForeColor = Color.Red;
    }
    listViewItem.Tag = (object) this.GetNodeColumnIndex(attr);
    return listViewItem;
  }

  /// <summary>Заполнить дерево допустимых атрибутов</summary>
  /// <param name="resetDatasource">Переназначать источник данных</param>
  protected virtual void FillTree(bool resetDatasource)
  {
    if (resetDatasource)
      this.treeAvailable.DataSource = (object) this;
    this.treeAvailable.UpdateRows(true);
    this.treeAvailable.UpdateRowData();
    this.UpdateControls();
  }

  /// <summary>Получить политику дочерних узлов</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAvailable_GetChildPolicy(object sender, GetChildPolicyEventArgs e)
  {
    e.ChildPolicy = RowChildPolicy.Normal;
  }

  /// <summary>Получить дочерние узлы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAvailable_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Level == 0)
    {
      e.Children = (IList) this.treeGroups;
    }
    else
    {
      if (e.Row.Level != 1)
        return;
      e.Children = (IList) ((SelectAttributeControl.TreeNodeSchemeItem) e.Row.Item).Columns;
    }
  }

  /// <summary>Получить необходимую информацию о строках в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAvailable_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.Row.Level == 1)
    {
      SelectAttributeControl.TreeNodeSchemeItem treeNodeSchemeItem = (SelectAttributeControl.TreeNodeSchemeItem) e.Row.Item;
      if (treeNodeSchemeItem == null)
        return;
      this.treeGroupRows[treeNodeSchemeItem.Guid] = e.Row;
      e.RowData.ImageList = this.FAttrTypesIcons != null ? this.FAttrTypesIcons.ImageList : (ImageList) null;
      int groupImageIndex = this.GetGroupImageIndex(treeNodeSchemeItem.Scheme.Name);
      e.RowData.ImageIndex = groupImageIndex;
      e.RowData.IconSize = 16 /*0x10*/;
    }
    else
    {
      if (e.Row.Level != 2 || e.Row.Item == null)
        return;
      NodeColumn nodeColumn = this.GetNodeColumn((int) e.Row.Item);
      if (nodeColumn == null)
        return;
      int typeImageIndex = this.GetTypeImageIndex(nodeColumn.AttrType);
      e.RowData.ImageList = this.FAttrTypesIcons != null ? this.FAttrTypesIcons.ImageList : (ImageList) null;
      e.RowData.ImageIndex = typeImageIndex;
      e.RowData.IconSize = 16 /*0x10*/;
    }
  }

  /// <summary>Получить необходимую информацию о ячейках в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAvailable_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Row.Level == 1)
    {
      SelectAttributeControl.TreeNodeSchemeItem treeNodeSchemeItem = (SelectAttributeControl.TreeNodeSchemeItem) e.Row.Item;
      e.CellData.Value = (object) treeNodeSchemeItem.Scheme.Name;
      e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, new StyleDelta()
      {
        Font = new Font(e.Row.Tree.RowOddStyle.Font, FontStyle.Bold)
      });
      e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, new StyleDelta()
      {
        Font = new Font(e.Row.Tree.RowEvenStyle.Font, FontStyle.Bold)
      });
    }
    else
    {
      if (e.Row.Level != 2 || e.Row.Item == null)
        return;
      NodeColumn nodeColumn = this.GetNodeColumn((int) e.Row.Item);
      if (nodeColumn == null)
        return;
      e.CellData.Value = (object) nodeColumn.Caption;
      e.CellData.ToolTip = nodeColumn.Hint;
    }
  }

  /// <summary>
  /// Изменились выделенные строки в дереве допустимых колонок
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAvailable_SelectionChanged(object sender, EventArgs e)
  {
    if (this.inEvent)
      return;
    this.UpdateControls();
  }

  /// <summary>Свернуть все узлы в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCollapse(object sender, EventArgs e)
  {
    this.treeAvailable?.RootRow?.CollapseChildren(true);
    this.treeAvailable?.RootRow?.EnsureVisible();
  }

  /// <summary>Раскрыть все узлы в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoExpand(object sender, EventArgs e)
  {
    if (this.treeAvailable.RootRow == null)
      return;
    this.treeAvailable.RootRow.ExpandChildren(true);
  }

  /// <summary>
  /// Перестраиваем список атрибутов с учётом нового фильтра
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void cbFind_TextChanged(object sender, EventArgs e)
  {
    this.timerReload.Enabled = false;
    this.timerReload.Enabled = true;
  }

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnApply_Click(object sender, EventArgs e) => this._okPressed = true;

  /// <summary>Форма закрывается</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void AppearanceTuningForm_FormClosing(object sender, FormClosingEventArgs e)
  {
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected void ToolbarRendererChanged(object sender, EventArgs e)
  {
    this.toolBarTree.Renderer = (sender as BarManager).Renderer;
  }

  /// <summary>Событие от таймера</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void timerReload_Tick(object sender, EventArgs e)
  {
    this.timerReload.Enabled = false;
    this.DoReloadAttrsList((object) this.cbFind, (EventArgs) null);
  }

  private void treeAvailable_DoubleClick(object sender, EventArgs e)
  {
    if (this.SelectedNodeColumn == null)
      return;
    EventHandler treeDoubleClicked = this.AttributesTreeDoubleClicked;
    if (treeDoubleClicked == null)
      return;
    treeDoubleClicked(sender, e);
  }

  private void OnAvailableAttributesTreeClicked(object sender, MouseEventArgs e)
  {
    MouseEventHandler attributesTreeClicked = this.AttributesTreeClicked;
    if (attributesTreeClicked == null)
      return;
    attributesTreeClicked(sender, e);
  }

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBarTree.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
    }
    foreach (AVSColumnScheme schemes in this._schemesList)
      this.FColumnSchemes.Unregister(schemes.SchemeGuid);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectAttributeControl));
    this.panel1 = new Panel();
    this.cbFind = new ComboBox();
    this.label1 = new Label();
    this.comboSearch = new ComboBox();
    this.labelSearch = new Label();
    this.toolBarTree = new Intermech.Bars.ToolBar();
    this.imagesToolbars = new ImageList(this.components);
    this.btCollapse = new ButtonItem();
    this.btExpand = new ButtonItem();
    this.btShowAll = new ButtonItem();
    this.treeAvailable = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnAttribute = new Column();
    this.panelLeft = new Panel();
    this.timerReload = new Timer(this.components);
    this.toolTip = new ToolTip(this.components);
    this.images = new ImageList(this.components);
    this.panel1.SuspendLayout();
    this.treeAvailable.BeginInit();
    this.panelLeft.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.cbFind);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.comboSearch);
    this.panel1.Controls.Add((Control) this.labelSearch);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.cbFind.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.cbFind.AutoCompleteSource = AutoCompleteSource.ListItems;
    componentResourceManager.ApplyResources((object) this.cbFind, "cbFind");
    this.cbFind.DropDownStyle = ComboBoxStyle.Simple;
    this.cbFind.FormattingEnabled = true;
    this.cbFind.Name = "cbFind";
    this.cbFind.Sorted = true;
    this.cbFind.TextChanged += new EventHandler(this.cbFind_TextChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.comboSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.comboSearch.AutoCompleteSource = AutoCompleteSource.ListItems;
    componentResourceManager.ApplyResources((object) this.comboSearch, "comboSearch");
    this.comboSearch.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboSearch.FormattingEnabled = true;
    this.comboSearch.Name = "comboSearch";
    this.comboSearch.Sorted = true;
    this.comboSearch.SelectedIndexChanged += new EventHandler(this.DoReloadAttrsList);
    componentResourceManager.ApplyResources((object) this.labelSearch, "labelSearch");
    this.labelSearch.Name = "labelSearch";
    this.toolBarTree.AddRemoveButtonsVisible = false;
    this.toolBarTree.AllowHorizontalDock = false;
    componentResourceManager.ApplyResources((object) this.toolBarTree, "toolBarTree");
    this.toolBarTree.DockLine = 3;
    this.toolBarTree.DrawActionsButton = false;
    this.toolBarTree.FullMenus = true;
    this.toolBarTree.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarTree.Hidden = false;
    this.toolBarTree.ImageList = this.imagesToolbars;
    this.toolBarTree.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.btCollapse,
      (ToolbarItemBase) this.btExpand,
      (ToolbarItemBase) this.btShowAll
    });
    this.toolBarTree.MinimumFloatingSize = new Size(250, 30);
    this.toolBarTree.Name = "toolBarTree";
    this.toolBarTree.Overflow = ToolBarOverflow.Wrap;
    this.toolBarTree.Stretch = true;
    this.toolBarTree.Tearable = false;
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "arrow_left_blue.ico");
    this.imagesToolbars.Images.SetKeyName(2, "arrow_all_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(3, "arrow_all_left_blue.ico");
    this.imagesToolbars.Images.SetKeyName(4, "arrow_up_blue.ico");
    this.imagesToolbars.Images.SetKeyName(5, "arrow_down_blue.ico");
    this.imagesToolbars.Images.SetKeyName(6, "arrow_top_blue.ico");
    this.imagesToolbars.Images.SetKeyName(7, "arrow_bottom_blue.ico");
    this.imagesToolbars.Images.SetKeyName(8, "Collapse.ico");
    this.imagesToolbars.Images.SetKeyName(9, "Expand.ico");
    this.imagesToolbars.Images.SetKeyName(10, "ObjectsFilter.ico");
    componentResourceManager.ApplyResources((object) this.btCollapse, "btCollapse");
    this.btCollapse.ImageIndex = 8;
    this.btCollapse.Click += new EventHandler(this.DoCollapse);
    componentResourceManager.ApplyResources((object) this.btExpand, "btExpand");
    this.btExpand.ImageIndex = 9;
    this.btExpand.Click += new EventHandler(this.DoExpand);
    this.btShowAll.AutoToggle = AutoToggleType.Single;
    this.btShowAll.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btShowAll, "btShowAll");
    this.btShowAll.ImageIndex = 10;
    this.btShowAll.ShowText = true;
    this.btShowAll.Click += new EventHandler(this.DoReloadAttrsList);
    this.treeAvailable.AllowDrop = true;
    this.treeAvailable.AllowMultiSelect = false;
    this.treeAvailable.AllowUserPinnedColumns = false;
    this.treeAvailable.BackColor = SystemColors.Control;
    this.treeAvailable.Columns.Add(this.columnAttribute);
    this.treeAvailable.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.treeAvailable, "treeAvailable");
    this.treeAvailable.EnableRowCaching = false;
    this.treeAvailable.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("treeAvailable.HeaderStyle.HorzAlignment");
    this.treeAvailable.ImageList = (ImageList) null;
    this.treeAvailable.LineStyle = LineStyle.Dot;
    this.treeAvailable.MainColumn = this.columnAttribute;
    this.treeAvailable.Name = "treeAvailable";
    this.treeAvailable.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("treeAvailable.RowEvenStyle.WordWrap");
    this.treeAvailable.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("treeAvailable.RowOddStyle.WordWrap");
    this.treeAvailable.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("treeAvailable.RowSelectedStyle.WordWrap");
    this.treeAvailable.RowStyle.BorderColor = SystemColors.Control;
    this.treeAvailable.RowStyle.BorderStyle = Border3DStyle.Flat;
    this.treeAvailable.RowStyle.BorderWidth = 0;
    this.treeAvailable.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("treeAvailable.RowStyle.WordWrap");
    this.treeAvailable.SelectBeforeEdit = true;
    this.treeAvailable.ShowRootRow = false;
    this.treeAvailable.SuppressErrorMessages = true;
    this.treeAvailable.GetCellData += new GetCellDataHandler(this.treeAvailable_GetCellData);
    this.treeAvailable.GetChildPolicy += new GetChildPolicyHandler(this.treeAvailable_GetChildPolicy);
    this.treeAvailable.GetChildren += new GetChildrenHandler(this.treeAvailable_GetChildren);
    this.treeAvailable.GetRowData += new GetRowDataHandler(this.treeAvailable_GetRowData);
    this.treeAvailable.SelectionChanged += new EventHandler(this.treeAvailable_SelectionChanged);
    this.treeAvailable.DoubleClick += new EventHandler(this.treeAvailable_DoubleClick);
    this.columnAttribute.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnAttribute, "columnAttribute");
    this.columnAttribute.CellStyle.BorderWidth = 0;
    this.columnAttribute.Movable = false;
    this.columnAttribute.Name = "columnAttribute";
    this.columnAttribute.Sortable = false;
    this.panelLeft.Controls.Add((Control) this.treeAvailable);
    this.panelLeft.Controls.Add((Control) this.toolBarTree);
    this.panelLeft.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.panelLeft, "panelLeft");
    this.panelLeft.Name = "panelLeft";
    this.timerReload.Interval = (int) byte.MaxValue;
    this.timerReload.Tick += new EventHandler(this.timerReload_Tick);
    this.images.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("images.ImageStream");
    this.images.TransparentColor = Color.Transparent;
    this.images.Images.SetKeyName(0, "");
    this.images.Images.SetKeyName(1, "");
    this.images.Images.SetKeyName(2, "object_16x16.ico");
    this.images.Images.SetKeyName(3, "link_16x16.ico");
    this.images.Images.SetKeyName(4, "sinfo_16.ico");
    this.images.Images.SetKeyName(5, "EventLog.ico");
    this.Controls.Add((Control) this.panelLeft);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (SelectAttributeControl);
    this.panel1.ResumeLayout(false);
    this.treeAvailable.EndInit();
    this.panelLeft.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Константы для формы AppearanceTuningForm</summary>
  internal static class AppearanceTuningFormConsts
  {
    /// <summary>Заголовок формы - "Настройка отображения"</summary>
    internal static readonly string FormCaption = "Настройка отображения";
    /// <summary>Ключ в коллекции настроек - сплиттер главной панели</summary>
    internal const int ccSplitterMain = 3001;
  }

  /// <summary>
  /// Вспомогательный класс, в котором хранится группа колонок или описание колонки
  /// </summary>
  protected class TreeNodeSchemeItem : 
    IComparable,
    IComparable<SelectAttributeControl.TreeNodeSchemeItem>
  {
    /// <summary>Guid схемы</summary>
    public Guid Guid = Guid.Empty;
    /// <summary>Схема колонок</summary>
    public INodeColumnScheme Scheme;
    /// <summary>Список колонок в схеме</summary>
    public List<int> Columns;

    /// <summary>Создать экземпляр класса</summary>
    /// <param name="guid">Guid схемы колонок</param>
    /// <param name="scheme">Схема колонок</param>
    /// <param name="columns">Список колонок</param>
    public TreeNodeSchemeItem(Guid guid, INodeColumnScheme scheme, List<int> columns)
    {
      this.Guid = guid;
      this.Scheme = scheme;
      this.Columns = columns;
    }

    /// <summary>Выполнить сравнение с указанным объектом</summary>
    /// <param name="obj">Объект для сравнения</param>
    /// <returns>true - объекты равны</returns>
    public override bool Equals(object obj)
    {
      return obj is SelectAttributeControl.TreeNodeSchemeItem treeNodeSchemeItem && this.Scheme != null && this.Guid == treeNodeSchemeItem.Guid;
    }

    /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
    /// <returns>32-битный хэш-код экземпляра класса</returns>
    public override int GetHashCode() => this.Guid.GetHashCode();

    /// <summary>Значение экземпляра класса в виде строки</summary>
    /// <returns></returns>
    public override string ToString()
    {
      return $"[{this.Guid.ToString()}] \"{(this.Scheme != null ? (object) this.Scheme.Name : (object) string.Empty)}\" ({(this.Columns != null ? (object) this.Columns.Count.ToString() : (object) "0")})";
    }

    /// <summary>Сравнить с указанным объектом</summary>
    /// <param name="obj">Объект для сравнения</param>
    /// <returns>-1, 0, 1</returns>
    public int CompareTo(object obj)
    {
      return this.CompareTo(obj as SelectAttributeControl.TreeNodeSchemeItem);
    }

    /// <summary>Сравнить с указанным объектом</summary>
    /// <param name="other">Объект для сравнения</param>
    /// <returns>-1, 0, 1</returns>
    public int CompareTo(SelectAttributeControl.TreeNodeSchemeItem other)
    {
      if (other == null || other.Scheme == null || this.Scheme == null)
        return 1;
      Guid guid = new Guid("{67D5959C-ED2C-4e35-88FE-340AE0278469}");
      if (!(this.Guid == guid) && !(other.Guid == guid))
        return this.Scheme.Name.CompareTo(other.Scheme.Name);
      if (this.Guid == guid && other.Guid == guid)
        return 0;
      return this.Guid == guid ? -1 : 1;
    }
  }

  /// <summary>Номера изображений в списке атрибутов</summary>
  public abstract class CustomizeColumnsImages
  {
    /// <summary>
    /// Атрибут представляет собой ссылки на учётные записи пользователей
    /// </summary>
    public const int img_ftUserID = 0;
    /// <summary>Уровень продвижения</summary>
    public const int img_ftLevelID = 1;
  }

  /// <summary>Менеджер контекстного поиска в гриде</summary>
  public class iAttrContextSearchManager
  {
    /// <summary>Владелец</summary>
    private SelectAttributeControl owner;
    /// <summary>Дерево, с которым связан менеджер контекстного поиска</summary>
    private Intermech.VirtualTreeView.VirtualTreeView fTree;
    /// <summary>
    /// Таймер, по событию от которого происходит сброс набираемой строки
    /// </summary>
    private Timer fResetTimer;
    /// <summary>Искомый текст</summary>
    private StringBuilder fText = new StringBuilder();

    /// <summary>Создать экземпляр менеджера контекстного поиска</summary>
    /// <param name="owner">Владелец</param>
    /// <param name="tree">Дерево, с которым будет связан менеджер контекстного поиска</param>
    public iAttrContextSearchManager(SelectAttributeControl owner, Intermech.VirtualTreeView.VirtualTreeView tree)
    {
      this.owner = tree != null && owner != null ? owner : throw new ArgumentNullException("Нельзя создавать менеджер контекстного поиска без ссылки на дерево (tree = null)");
      this.fTree = tree;
      this.fTree.KeyPress += new KeyPressEventHandler(this.fTree_KeyPress);
      this.fTree.KeyUp += new KeyEventHandler(this.fTree_KeyDown);
      this.fTree.MouseDown += new MouseEventHandler(this.fMouseDown);
      this.fTree.MouseUp += new MouseEventHandler(this.fMouseUp);
      this.fTree.MouseDoubleClick += new MouseEventHandler(this.fMouseDoubleClick);
      this.fResetTimer = new Timer();
      this.fResetTimer.Interval = 10000;
      this.fResetTimer.Tick += new EventHandler(this.fResetTimer_Tick);
    }

    /// <summary>Выполняется ли контекстный поиск в гриде</summary>
    public bool InProgress => this.fText.Length > 0;

    /// <summary>Нажата клавиша в дереве</summary>
    /// <param name="sender">Отправитель</param>
    /// <param name="e">Аргументы события</param>
    private void fTree_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (this.fResetTimer.Enabled)
        this.fResetTimer.Stop();
      this.fResetTimer.Start();
      if (e.KeyChar == '\b')
      {
        if (this.fText.Length > 0)
          this.fText.Remove(this.fText.Length - 1, 1);
        this.OnSetText();
        e.Handled = true;
      }
      else if (e.KeyChar <= '\u001F')
      {
        this.Cancel();
      }
      else
      {
        this.fText.Append(e.KeyChar);
        this.OnSetText();
        this.SelectNextCell();
        e.Handled = true;
      }
    }

    /// <summary>Нажата клавиша в гриде</summary>
    /// <param name="sender">Отправитель</param>
    /// <param name="e">Аргументы события</param>
    private void fTree_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Back || e.Modifiers == Keys.None && e.KeyData != Keys.Up && e.KeyData != Keys.Down && e.KeyData != Keys.Escape && e.KeyData != Keys.Return && e.KeyData != Keys.Return && e.KeyData != Keys.Home && e.KeyData != Keys.End && e.KeyData != Keys.Left && e.KeyData != Keys.Right && e.KeyData != Keys.Tab && e.KeyData != Keys.Prior && e.KeyData != Keys.Next && e.KeyData != Keys.BrowserBack && e.KeyData != Keys.BrowserFavorites && e.KeyData != Keys.BrowserForward && e.KeyData != Keys.BrowserHome && e.KeyData != Keys.BrowserStop)
        return;
      this.Cancel();
    }

    /// <summary>Нажата клавиша крыски</summary>
    /// <param name="sender">Отправитель</param>
    /// <param name="e">Аргументы события</param>
    private void fMouseDown(object sender, MouseEventArgs e) => this.Cancel();

    /// <summary>Отпущена клавиша крыски</summary>
    /// <param name="sender">Отправитель</param>
    /// <param name="e">Аргументы события</param>
    private void fMouseUp(object sender, MouseEventArgs e) => this.Cancel();

    /// <summary>Двойной клик крыской</summary>
    /// <param name="sender">Отправитель</param>
    /// <param name="e">Аргументы события</param>
    private void fMouseDoubleClick(object sender, EventArgs e) => this.Cancel();

    /// <summary>Выделить очередную строку в дереве</summary>
    protected virtual void SelectNextCell()
    {
      if (this.fText.Length == 0 || this.fTree.RootRow.NumChildren == 0)
        return;
      Row row1 = this.fTree.FocusRow ?? this.fTree.RootRow.ChildRowByIndex(0);
      string str1 = this.fText.ToString().ToUpper().Trim();
      Row row2 = (Row) null;
      while (row1 != null)
      {
        string str2 = string.Empty;
        if (row1.Level == 1)
          str2 = ((SelectAttributeControl.TreeNodeSchemeItem) row1.Item).Scheme.Name.ToUpper().Trim();
        if (row1.Level == 2)
          str2 = this.owner.GetNodeColumn((int) row1.Item).Caption.ToUpper().Trim();
        if (!string.IsNullOrEmpty(str2) && str2.StartsWith(str1))
        {
          row2 = row1;
          break;
        }
        int childIndex1 = row1.ChildIndex;
        if (row1.NumChildren > 0)
        {
          row1 = row1.ChildRowByIndex(0);
        }
        else
        {
          int childIndex2 = row1.ParentRow.NumChildren > childIndex1 + 1 ? childIndex1 + 1 : -1;
          if (childIndex2 < 0)
          {
            do
            {
              row1 = row1.ParentRow;
              if (row1.ParentRow != null)
              {
                int childIndex3 = row1.ChildIndex;
                childIndex2 = row1.ParentRow.NumChildren > childIndex3 + 1 ? childIndex3 + 1 : -1;
              }
              else
                break;
            }
            while (childIndex2 <= 0);
          }
          if (childIndex2 < 0)
            return;
          row1 = row1.ParentRow.ChildRowByIndex(childIndex2);
        }
      }
      if (row2 == null)
        return;
      this.fTree.SelectedRows.Clear();
      this.fTree.SelectedRow = row2;
      this.fTree.FocusRow = row2;
    }

    /// <summary>Перерисуем прямоугольник с искомым текстом</summary>
    private void OnSetText()
    {
      this.fTree.Invalidate(this.GetTextAreaBounds(this.fText.ToString()));
    }

    /// <summary>Рассчитать размеры области с искомым текстом</summary>
    /// <param name="value">Искомый текст, который надо красиво показать пользователю</param>
    /// <returns>Размеры области с текстом</returns>
    private Rectangle GetTextAreaBounds(string value)
    {
      Font font = new Font(this.fTree.Font, FontStyle.Bold);
      int width = font.Height * 15 + 6;
      int height = font.Height + 6;
      Rectangle empty = Rectangle.Empty;
      Rectangle clientRectangle = this.fTree.ClientRectangle;
      return new Rectangle(clientRectangle.Right - width, clientRectangle.Bottom - height, width, height);
    }

    /// <summary>Сбросить контекстный поиск</summary>
    /// <param name="sender">Отправитель</param>
    /// <param name="e">Аргументы события</param>
    private void fResetTimer_Tick(object sender, EventArgs e) => this.Cancel();

    /// <summary>Прервать контекстный поиск в гриде</summary>
    public virtual void Cancel()
    {
      this.fText.Length = 0;
      this.fTree.Invalidate();
    }
  }
}
