
// Type: Intermech.Navigator.AppearanceTuningForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections;
using Intermech.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Форма "Настройка отображения" - позволяет редактировать список отображаемых в "Навигаторе" колонок атрибутов
/// </summary>
public class AppearanceTuningForm : Form
{
  /// <summary>
  /// Ограничение на количество колонок
  /// (изменив данное значение следует откорректировать текст метки labelManyColumns)
  /// </summary>
  public const int ColumnsCountLimit = 1000;
  /// <summary>Если true, то идёт работа внутри обработчика событий</summary>
  protected bool inEvent;
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
  protected INodeID _nodeID;
  /// <summary>Коллекция фильтров для списка атрибутов</summary>
  protected static List<MyElement> filters = new List<MyElement>(1);
  /// <summary>
  /// Коллекция пар значений [(Guid)Схема] = [(string)Название схемы]
  /// </summary>
  protected HybridDictionary FSchemesNames = new HybridDictionary(0);
  /// <summary>Список поддерживаемых колонок</summary>
  protected NodeColumnCollection FSupportedColumns;
  /// <summary>Словарик для быстрого поиска колонок</summary>
  protected Dictionary<int, NodeColumn> DictSupportedColumns = new Dictionary<int, NodeColumn>();
  /// <summary>Обратный словарик для быстрого поиска индекса колонок</summary>
  protected Dictionary<string, int> DictRevSupportedColumns = new Dictionary<string, int>();
  /// <summary>Список выбранных колонок</summary>
  public NodeColumnCollection FColumns;
  /// <summary>Были ли изменения в настраиваемом виде</summary>
  protected bool FIsChanged;
  /// <summary>Форма закрывается по нажатию "ОК"</summary>
  protected bool _okPressed;
  /// <summary>Коллекция разных настроек контролов формы</summary>
  protected HybridDictionary FControlsSettings = new HybridDictionary(0, true);
  /// <summary>Коллекция изображений для разных категорий</summary>
  protected ICategoryTypeIconService FAttrTypesIcons;
  /// <summary>Информация о текущем пользователе и его роли</summary>
  protected ICurrentUserAndRole FUserRole;
  /// <summary>Сервис для регистрации своих категорий</summary>
  protected IGuidMapper FGuidMapper;
  /// <summary>Сервис для управления схемами колонок</summary>
  protected IColumnSchemes FColumnSchemes;
  /// <summary>ID категории своих значков</summary>
  protected static int FIconsCategory = 0;
  /// <summary>Индексы своих значков</summary>
  protected static int[] FIcons = (int[]) null;
  /// <summary>Сортировать ли список доступных колонок</summary>
  protected bool AutoSortAvailableColumns;
  /// <summary>Менеджер контекстного поиска в дереве</summary>
  protected AppearanceTuningForm.iAttrContextSearchManager _contextSearchManager;
  /// <summary>Идентификатор родительского типа объектов</summary>
  protected int parentObjectTypeID = -1;
  /// <summary>
  /// Идентификатор родительского типа объектов, если выполняется настройка отображения для выборки, связанной с типом объектов
  /// </summary>
  protected int bindedObjectTypeID = -1;
  /// <summary>Идентификаторы допустимых типов связей</summary>
  protected List<int> parentRelationTypesID = new List<int>();
  /// <summary>Список типов атрибутов для родительского типа объекта</summary>
  protected List<IMSAttribute4ObjectType> objectTypeAttrs;
  /// <summary>Список типов атрибутов для допустимых типов связей</summary>
  protected List<IMSAttribute4RelationType> relationsTypeAttrs = new List<IMSAttribute4RelationType>();
  /// <summary>Список видимых групп</summary>
  protected List<AppearanceTuningForm.TreeNodeSchemeItem> treeGroups = new List<AppearanceTuningForm.TreeNodeSchemeItem>();
  /// <summary>Видимые группы и коллекции колонок</summary>
  protected Dictionary<Guid, List<int>> treeColumns = new Dictionary<Guid, List<int>>();
  /// <summary>Строки дерева, соответствующие группам колонок</summary>
  protected Dictionary<Guid, Row> treeGroupRows = new Dictionary<Guid, Row>();
  /// <summary>
  /// Словарик атрибутов, которые доступны текущему пользователю
  /// </summary>
  protected Dictionary<int, int> filteredAttributes = new Dictionary<int, int>();
  /// <summary>Выполняется ли обработка событий</summary>
  protected bool inWidthEvents;
  /// <summary>Запретить обработчик события</summary>
  protected bool disableWidthEvents;
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
  private NodeIDPath _nodeIDPath;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Button btnCancel;
  protected Button btnApply;
  protected Panel panelLeft;
  protected Panel panelRight;
  protected ImageList imagesToolbars;
  protected ToolTip toolTip;
  protected MenuBar menuAvailable;
  protected ContextMenuBarItem contextMenuAvailable;
  protected MenuButtonItem mnpAdd;
  protected MenuButtonItem mnpAddAll;
  protected Intermech.Bars.ToolBar toolBarLeft;
  protected ButtonItem btAdd;
  protected ButtonItem btAddAll;
  protected ButtonItem btDelete;
  protected ButtonItem btDeleteAll;
  protected MenuBar menuVisible;
  protected ContextMenuBarItem contextMenuVisible;
  protected MenuButtonItem mnpMoveUp;
  protected MenuButtonItem mnpMoveDown;
  protected MenuButtonItem mnpDelete;
  protected MenuButtonItem mnpDeleteAll;
  protected ListView listVisible;
  protected ColumnHeader columnVisibleName;
  protected Panel panelTrack;
  protected NumericUpDown edWidth;
  protected Label labelWidth;
  protected Intermech.Bars.ToolBar toolBarRight;
  protected ButtonItem btMoveUp;
  protected ButtonItem btMoveDown;
  protected Panel panel1;
  protected ComboBox comboSearch;
  protected Label labelSearch;
  protected SplitContainer panelMain;
  protected Panel panelBottom;
  protected ImageList images;
  protected ComboBox cbFind;
  protected Label label1;
  protected Button btnDefault;
  protected Label labelWarning;
  protected Timer timerReload;
  protected ButtonItem btMoveTop;
  protected ButtonItem btMoveBottom;
  protected MenuButtonItem mnpMoveTop;
  protected MenuButtonItem mnpMoveBottom;
  protected Intermech.VirtualTreeView.VirtualTreeView treeAvailable;
  protected Column columnAttribute;
  protected Intermech.Bars.ToolBar toolBarTree;
  protected ButtonItem btCollapse;
  protected ButtonItem btExpand;
  protected MenuButtonItem mnpCollapse;
  protected MenuButtonItem mnpExpand;
  protected ButtonItem btShowAll;
  protected MenuButtonItem mnpShowAll;
  protected Label labelManyColumns;
  private Panel panelWarning;
  private PictureBox pictureWarning;
  protected Label labelWarning2;
  protected Button _applyParentNodeDisplaySettingsButton;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AppearanceTuningForm.TreeNodeSchemeItem[] TreeGroups => this.treeGroups.ToArray();

  /// <summary>
  /// Создать экземпляр формы (конструктор предназначенный для классов-потомков, чтобы у них дизайнер форм работал)
  /// </summary>
  protected AppearanceTuningForm()
  {
    this.InitializeComponent();
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.panelBottom.SendToBack();
    this._contextSearchManager = new AppearanceTuningForm.iAttrContextSearchManager(this, this.treeAvailable);
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 679);
  }

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="node">Узел, для которого вызывается окно настройки отображения</param>
  /// <param name="content">Для какого содержимого вызывается настройка отображения</param>
  /// <param name="supportedColumns">Список всех колонок атрибутов</param>
  /// <param name="columns">Список выбранных колонок атрибутов</param>
  /// <param name="nodeIDs">Элементы, содержимое которых будет получено по настроенным колонкам</param>
  public AppearanceTuningForm(
    INode node,
    ContentType content,
    NodeColumnCollection supportedColumns,
    NodeColumnCollection columns,
    params object[] nodeIDs)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 679);
    this.Init(node, content, supportedColumns, columns, true, nodeIDs);
  }

  /// <summary>
  /// Вызвать форму "Настройка отображения" (позволяет редактировать список отображаемых в "Навигаторе" колонок атрибутов)
  /// </summary>
  /// <param name="node">Узел, для которого вызывается окно настройки отображения</param>
  /// <param name="content">Для какого содержимого вызывается настройка отображения</param>
  /// <param name="supportedColumns">Список всех колонок атрибутов</param>
  /// <param name="columns">Список выбранных колонок атрибутов</param>
  /// <param name="nodeIDs">Элементы, содержимое которых будет получено по настроенным колонкам</param>
  /// <returns>Результат вызова формы как модального окна</returns>
  [STAThread]
  public static DialogResult Execute(
    INode node,
    ContentType content,
    NodeColumnCollection supportedColumns,
    NodeColumnCollection columns,
    params object[] nodeIDs)
  {
    return AppearanceTuningForm.Execute(node, content, (string) null, supportedColumns, columns, nodeIDs);
  }

  public static DialogResult Execute(
    INode node,
    ContentType contentType,
    string stateStreamSuffix,
    NodeColumnCollection supportedColumns,
    NodeColumnCollection columns,
    params object[] nodeIDs)
  {
    using (AppearanceTuningForm appearanceTuningForm = new AppearanceTuningForm(node, contentType, supportedColumns, columns, nodeIDs))
    {
      appearanceTuningForm.StateStreamSuffix = stateStreamSuffix;
      try
      {
        return appearanceTuningForm.ShowDialog();
      }
      finally
      {
        NodeColumnCollection.CorrectSortIndex(columns);
      }
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string StateStreamSuffix { get; set; }

  /// <summary>Текущая схема колонок</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CustomDescription("Attribute.Client.Core_236")]
  protected virtual INodeColumnScheme SelectedScheme
  {
    get
    {
      Row row = this.treeAvailable.SelectedRow;
      while (row != null && !(row.Item is AppearanceTuningForm.TreeNodeSchemeItem))
        row = row.ParentRow;
      return row == null || row.Item == null ? (INodeColumnScheme) null : (row.Item as AppearanceTuningForm.TreeNodeSchemeItem).Scheme;
    }
  }

  /// <summary>Описание текущей схемы колонок</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CustomDescription("Attribute.Client.Core_237")]
  protected virtual AppearanceTuningForm.TreeNodeSchemeItem SelectedSchemeItem
  {
    get
    {
      Row row = this.treeAvailable.SelectedRow;
      while (row != null && !(row.Item is AppearanceTuningForm.TreeNodeSchemeItem))
        row = row.ParentRow;
      return row == null || row.Item == null ? (AppearanceTuningForm.TreeNodeSchemeItem) null : row.Item as AppearanceTuningForm.TreeNodeSchemeItem;
    }
  }

  /// <summary>
  /// Список выделенных объектов (объекты типа TreeNodeSchemeItem и Int32)
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CustomDescription("Attribute.Client.Core_238")]
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
  [CustomDescription("Attribute.Client.Core_239")]
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

  /// <summary>Преобразовать ID колонки в Int32</summary>
  /// <param name="columnID">ID колонки</param>
  /// <returns>Int32</returns>
  protected virtual int ConvertIDtoInt32(object columnID)
  {
    if (columnID is ObligatoryObjectAttributes)
      return (int) columnID;
    if (columnID is string)
    {
      try
      {
        return (string) columnID == "F_STATUSES" ? -77 : ((string) columnID == "F_CAPTION" ? -50 : (int) ObligatoryObjectAttributesHelper.GetObligatoryObjectAttribute((string) columnID));
      }
      catch
      {
      }
    }
    if (columnID is PortalAttributeType)
    {
      int num = ((PortalAttributeType) columnID).ID;
      if (num > 0)
        num = -num - 10000;
      return num;
    }
    int result;
    if (!int.TryParse(columnID.ToString(), out result))
      result = 0;
    return result;
  }

  /// <summary>По индексу вернуть описание колонки</summary>
  /// <param name="index">Индекс</param>
  /// <returns>Описание колонки или null</returns>
  public virtual NodeColumn GetNodeColumn(int index)
  {
    return this.DictSupportedColumns.ContainsKey(index) ? this.DictSupportedColumns[index] : (NodeColumn) null;
  }

  /// <summary>По колонке вернуть её индекс</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Индекс колонки или -1</returns>
  protected internal virtual int GetNodeColumnIndex(NodeColumn column)
  {
    return this.DictRevSupportedColumns.ContainsKey(column.Caption) ? this.DictRevSupportedColumns[column.Caption] : -1;
  }

  /// <summary>Сравниватель для групп</summary>
  /// <returns></returns>
  protected virtual IComparer<string> GetGroupsComparison()
  {
    return (IComparer<string>) StringComparer.CurrentCulture;
  }

  /// <summary>Инициализация переменных</summary>
  /// <param name="node">Узел, для которого вызывается окно настройки отображения</param>
  /// <param name="content">Для какого содержимого вызывается настройка отображения</param>
  /// <param name="supportedColumns">Список всех колонок атрибутов</param>
  /// <param name="columns">Список выбранных колонок атрибутов</param>
  /// <param name="fullFormReset">Выполнить полную инициализацию настроек формы</param>
  protected void InitVariables(
    INode node,
    ContentType content,
    NodeColumnCollection supportedColumns,
    NodeColumnCollection columns,
    bool fullFormReset)
  {
    this._contextSearchManager = new AppearanceTuningForm.iAttrContextSearchManager(this, this.treeAvailable);
    this.node = node;
    this.content = content;
    this.FSupportedColumns = new NodeColumnCollection();
    this.FColumns = columns;
    if (supportedColumns != null)
    {
      for (int index1 = 0; index1 < supportedColumns.Count; ++index1)
      {
        NodeColumn supportedColumn = supportedColumns[index1];
        if (this.FSupportedColumns.FindCaption(supportedColumn.Caption) == null)
        {
          int index2 = this.FColumns != null ? this.FColumns.IndexOf(supportedColumn) : -1;
          if (index2 < 0 && this.FColumns != null)
            index2 = this.FColumns.IndexOf(this.FColumns.FindCaption(supportedColumn.Caption));
          if (index2 >= 0)
            supportedColumn.Width = this.FColumns[index2].Width;
          this.FSupportedColumns.Add(supportedColumn);
        }
      }
    }
    this.DictSupportedColumns.Clear();
    this.DictRevSupportedColumns.Clear();
    if (this.FSupportedColumns != null)
    {
      this.FSupportedColumns.Sort(true);
      for (int index = 0; index < this.FSupportedColumns.Count; ++index)
      {
        this.DictSupportedColumns.Add(index, this.FSupportedColumns[index]);
        this.DictRevSupportedColumns.Add(this.FSupportedColumns[index].Caption, index);
      }
    }
    this.btnApply.DialogResult = DialogResult.OK;
    if (fullFormReset)
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
      if (this.FGuidMapper == null)
        this.FGuidMapper = ServicesManager.GetService(typeof (IGuidMapper)) as IGuidMapper;
      this.FColumnSchemes = ServicesManager.GetService(typeof (IColumnSchemes)) as IColumnSchemes;
      if (this.FAttrTypesIcons == null)
        this.FAttrTypesIcons = Statics.IconSrv;
      if (this.FUserRole == null)
        this.FUserRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      if (AppearanceTuningForm.FIconsCategory == 0)
      {
        AppearanceTuningForm.FIconsCategory = this.FGuidMapper.Register(Guid.NewGuid());
        AppearanceTuningForm.FIcons = new int[this.images.Images.Count];
        for (int index = 0; index < this.images.Images.Count; ++index)
        {
          if (this.FAttrTypesIcons != null)
          {
            using (Icon icon = ImageHelper.BitmapToIcon(this.images.Images[index] as Bitmap))
              AppearanceTuningForm.FIcons[index] = this.FAttrTypesIcons.AddIcon(icon, AppearanceTuningForm.FIconsCategory, index);
          }
          else
            AppearanceTuningForm.FIcons[index] = -1;
        }
      }
      this.listVisible.LargeImageList = this.FAttrTypesIcons != null ? this.FAttrTypesIcons.ImageList : (ImageList) null;
      this.listVisible.SmallImageList = this.listVisible.LargeImageList;
      this.Text = AppearanceTuningForm.AppearanceTuningFormConsts.FormCaption;
      Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
      this.Size = new Size(primaryWorkingArea.Width / 100 * 70, primaryWorkingArea.Height / 100 * 60);
      this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    }
    if (Holder.NamedImageList == null)
      return;
    this.Icon = ImagesResizeHelper.GetIconFromImage(Holder.NamedImageList.ImageList.Images[Holder.NamedImageList.ImageIndex("imgViewSettings")]);
  }

  /// <summary>Инициализация контролов на форме</summary>
  protected void InitControls()
  {
    this.listVisible.ShowGroups = false;
    this.InitFiltersList();
    this.FillAttrsLists();
    this.mnpShowAll.Checked = this.btShowAll.Checked;
    this.UpdateControls();
  }

  /// <summary>Инициализация данных</summary>
  /// <param name="node">Узел, для которого вызывается окно настройки отображения</param>
  /// <param name="content">Для какого содержимого вызывается настройка отображения</param>
  /// <param name="supportedColumns">Список всех колонок атрибутов</param>
  /// <param name="columns">Список выбранных колонок атрибутов</param>
  /// <param name="fullFormReset">Выполнить полную инициализацию настроек формы</param>
  /// <param name="nodeIDs">Элементы, содержимое которых будет получено по настроенным колонкам</param>
  protected virtual void Init(
    INode node,
    ContentType content,
    NodeColumnCollection supportedColumns,
    NodeColumnCollection columns,
    bool fullFormReset,
    params object[] nodeIDs)
  {
    if (supportedColumns == null || supportedColumns.Count == 0)
      throw new Exception(LocalizationHolder.rm.GetString("Client.Core_1423"));
    supportedColumns.SyncWithMaster(columns);
    this.InitVariables(node, content, supportedColumns, columns, fullFormReset);
    if (this.node != null && nodeIDs != null && nodeIDs.Length > 1)
    {
      INode nodeId = nodeIDs[0] as INode;
      this._nodeID = nodeIDs[1] as INodeID;
      if (nodeId != null && this._nodeID != null)
      {
        if (nodeId.GetData(this._nodeID, typeof (IDBObjectTypeSelectionID)) is IDBObjectTypeSelectionID data1)
          this.bindedObjectTypeID = data1.BindedObjectTypeID;
        if (nodeId.GetData(this._nodeID, typeof (IDBTypedObjectID)) is IDBTypedObjectID data2)
          this.parentObjectTypeID = data2.ObjectType;
      }
      if (this.parentObjectTypeID == -1 && this._nodeID != null && this._nodeID.CategoryID == 4)
        this.parentObjectTypeID = this._nodeID.TypeID;
      this.objectTypeAttrs = new List<IMSAttribute4ObjectType>();
      this.relationsTypeAttrs.Clear();
      if (this.parentObjectTypeID != -1)
      {
        this.objectTypeAttrs = MetaDataHelper.GetAttribute4ObjectTypeList(this.parentObjectTypeID);
        for (int index = this.objectTypeAttrs.Count - 1; index >= 0; --index)
        {
          if (this.objectTypeAttrs[index].OptimizationMode == OptimizationModes.Write)
            this.objectTypeAttrs.RemoveAt(index);
        }
        this.parentRelationTypesID = MetaDataHelper.GetApplicabilityRelationTypesID(this.parentObjectTypeID);
        if (this.parentRelationTypesID.Count > 0)
        {
          for (int index1 = 0; index1 < this.parentRelationTypesID.Count; ++index1)
          {
            List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(this.parentRelationTypesID[index1]);
            for (int index2 = 0; index2 < relationTypeList.Count; ++index2)
            {
              if (relationTypeList[index2].OptimizationMode != OptimizationModes.Write && !this.relationsTypeAttrs.Contains(relationTypeList[index2]))
                this.relationsTypeAttrs.Add(relationTypeList[index2]);
            }
          }
        }
      }
      if (this.bindedObjectTypeID != -1 && this.bindedObjectTypeID != this.parentObjectTypeID)
      {
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(this.bindedObjectTypeID);
        for (int index = attribute4ObjectTypeList.Count - 1; index >= 0; --index)
        {
          if (attribute4ObjectTypeList[index].OptimizationMode == OptimizationModes.Write)
            attribute4ObjectTypeList.RemoveAt(index);
        }
        for (int index = 0; index < attribute4ObjectTypeList.Count; ++index)
        {
          if (!this.objectTypeAttrs.Contains(attribute4ObjectTypeList[index]))
            this.objectTypeAttrs.Add(attribute4ObjectTypeList[index]);
        }
        List<int> applicabilityRelationTypesId = MetaDataHelper.GetApplicabilityRelationTypesID(this.bindedObjectTypeID);
        if (applicabilityRelationTypesId.Count > 0)
        {
          for (int index3 = 0; index3 < applicabilityRelationTypesId.Count; ++index3)
          {
            List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(applicabilityRelationTypesId[index3]);
            for (int index4 = 0; index4 < relationTypeList.Count; ++index4)
            {
              if (relationTypeList[index4].OptimizationMode != OptimizationModes.Write && !this.relationsTypeAttrs.Contains(relationTypeList[index4]))
                this.relationsTypeAttrs.Add(relationTypeList[index4]);
            }
          }
        }
      }
    }
    if (nodeIDs != null && nodeIDs.Length >= 3)
      this._nodeIDPath = nodeIDs[2] as NodeIDPath;
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
      AppearanceTuningForm.filters.Clear();
      this.comboSearch.Items.Clear();
      this.FSchemesNames.Clear();
      Regex regex = new Regex("\\S", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
      AppearanceTuningForm.filters.Add(new MyElement((object) regex, LocalizationHolder.rm.GetString("Client.Core_219"), (object) true));
      List<string> stringList = new List<string>(0);
      for (int index = 0; index < this.FSupportedColumns.Count; ++index)
      {
        INodeColumnScheme fcolumnScheme = this.FColumnSchemes[this.FSupportedColumns[index].SchemeGuid];
        this.FSchemesNames[(object) this.FSupportedColumns[index].SchemeGuid] = (object) fcolumnScheme.Name;
        if ((this.btShowAll.Checked || !(this.FSupportedColumns[index].SchemeGuid == Consts.ObjectColumnSchemeGuid) && !(this.FSupportedColumns[index].SchemeGuid == Consts.RelationColumnSchemeGuid)) && fcolumnScheme != null && stringList.IndexOf(fcolumnScheme.Name) < 0)
          stringList.Add(fcolumnScheme.Name);
      }
      stringList.Sort(this.GetGroupsComparison());
      for (int index = 0; index < this.FColumns.Count; ++index)
      {
        INodeColumnScheme fcolumnScheme = this.FColumnSchemes[this.FColumns[index].SchemeGuid];
        this.FSchemesNames[(object) this.FColumns[index].SchemeGuid] = (object) fcolumnScheme.Name;
        if ((this.btShowAll.Checked || !(this.FColumns[index].SchemeGuid == Consts.ObjectColumnSchemeGuid) && !(this.FColumns[index].SchemeGuid == Consts.RelationColumnSchemeGuid)) && fcolumnScheme != null && stringList.IndexOf(fcolumnScheme.Name) < 0)
          stringList.Add(fcolumnScheme.Name);
      }
      stringList.Sort(this.GetGroupsComparison());
      for (int index = stringList.Count - 1; index >= 0; --index)
        AppearanceTuningForm.filters.Insert(1, new MyElement((object) null, string.Format(LocalizationHolder.rm.GetString("Client.Core_549"), (object) stringList[index]), (object) stringList[index]));
      for (int index = 0; index < AppearanceTuningForm.filters.Count; ++index)
        this.comboSearch.Items.Add((object) AppearanceTuningForm.filters[index]);
      bool flag = false;
      for (int index = 0; index < AppearanceTuningForm.filters.Count; ++index)
      {
        if (AppearanceTuningForm.filters[index].Caption == str)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        str = string.Empty;
      if (AppearanceTuningForm.filters.Count <= 0)
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
    int ficon = AppearanceTuningForm.FIcons[2];
    if (groupName.IndexOf(LocalizationHolder.rm.GetString("Client.Core_550"), StringComparison.InvariantCultureIgnoreCase) >= 0)
      ficon = AppearanceTuningForm.FIcons[3];
    if (groupName.IndexOf(LocalizationHolder.rm.GetString("Client.Core_551"), StringComparison.InvariantCultureIgnoreCase) >= 0)
      ficon = AppearanceTuningForm.FIcons[4];
    if (groupName.IndexOf(LocalizationHolder.rm.GetString("Client.Core_552"), StringComparison.InvariantCultureIgnoreCase) >= 0)
      ficon = AppearanceTuningForm.FIcons[5];
    return ficon;
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public virtual void UpdateControls()
  {
    int selectedRowsCount = this.SelectedRowsCount;
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    int count = selectedItems.Count;
    ListViewItem listViewItem1 = (ListViewItem) null;
    ListViewItem listViewItem2 = (ListViewItem) null;
    if (selectedItems != null && count > 0)
    {
      listViewItem1 = selectedItems[0];
      listViewItem2 = selectedItems[count - 1];
    }
    ListViewItem listViewItem3 = listViewItem1;
    NodeColumn nodeColumn = (NodeColumn) null;
    if (listViewItem3 != null)
      nodeColumn = this.GetNodeColumn((int) listViewItem3.Tag);
    this.btAdd.Enabled = selectedRowsCount > 0;
    this.mnpAdd.Enabled = this.btAdd.Enabled;
    this.btAddAll.Enabled = this.treeAvailable.RootRow.NumChildren > 0;
    this.mnpAddAll.Enabled = this.btAddAll.Enabled;
    this.btDelete.Enabled = count > 0;
    this.mnpDelete.Enabled = this.btDelete.Enabled;
    this.btDeleteAll.Enabled = this.listVisible.Items.Count > 0;
    this.mnpDeleteAll.Enabled = this.btDeleteAll.Enabled;
    this.btMoveUp.Enabled = listViewItem1 != null && listViewItem1.Index > 0;
    this.mnpMoveUp.Enabled = this.btMoveUp.Enabled;
    this.btMoveTop.Enabled = this.btMoveUp.Enabled;
    this.mnpMoveTop.Enabled = this.btMoveUp.Enabled;
    this.btMoveDown.Enabled = listViewItem1 != null && listViewItem2.Index < this.listVisible.Items.Count - 1;
    this.mnpMoveDown.Enabled = this.btMoveDown.Enabled;
    this.btMoveBottom.Enabled = this.btMoveDown.Enabled;
    this.mnpMoveBottom.Enabled = this.btMoveDown.Enabled;
    this.btnDefault.Enabled = this.node != null;
    this.btnDefault.Visible = this.btnDefault.Enabled;
    this.btnApply.Enabled = this.FColumns.Count > 0 && this.FIsChanged && this.listVisible.Items.Count <= 1000;
    this.btnCancel.Enabled = true;
    this.btCollapse.Enabled = this.treeAvailable.RootRow != null && this.treeAvailable.RootRow.NumChildren > 0;
    this.btExpand.Enabled = this.btCollapse.Enabled;
    this.edWidth.Enabled = nodeColumn != null;
    if (!this.inWidthEvents)
    {
      try
      {
        this.disableWidthEvents = true;
        this.edWidth.Value = nodeColumn != null ? (Decimal) nodeColumn.Width : 0M;
      }
      finally
      {
        this.disableWidthEvents = false;
      }
    }
    bool flag = false;
    for (int index = 0; index < this.FColumns.Count; ++index)
    {
      flag = this.FColumns[index].Tag != null;
      if (flag)
        break;
    }
    this.labelWarning.Visible = flag;
    this.labelManyColumns.Visible = this.listVisible.Items.Count > 1000;
    string str = this.listVisible.Items.Count > 0 ? string.Format(LocalizationHolder.rm.GetString("Client.Core_1424"), (object) this.listVisible.Items.Count) : LocalizationHolder.rm.GetString("Client.Core_1425");
    if (this.listVisible.Columns[0].Text != str)
      this.listVisible.Columns[0].Text = str;
    this.panelWarning.Visible = this.FUserRole != null && this.FUserRole.BlockedCompositions && this._nodeID != null && (this._nodeID.CategoryID == 1 || this._nodeID.CategoryID == 4);
    this._applyParentNodeDisplaySettingsButton.Enabled = this._nodeIDPath != null && (this._nodeIDPath.Length > 0 && this._nodeIDPath.LastID != this._nodeID || this._nodeIDPath.Length > 1);
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void AppearanceTuningForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this, (IDictionary) this.FControlsSettings);
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
    FormStorage.SaveLayout((Control) this, (IDictionary) this.FControlsSettings);
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
    if (controlsState == null)
      return;
    controlsState[(object) 3001] = (object) this.panelMain.SplitterDistance;
  }

  /// <summary>
  /// Установить контролам разные настройки типа ширины, т.п.
  /// </summary>
  /// <param name="controlsState">Коллекция с настройками контролов</param>
  protected virtual void SetControlsState(HybridDictionary controlsState)
  {
    if (controlsState == null)
      return;
    this.panelMain.SplitterDistance = (int) this.GetDicValue(controlsState, (object) 3001, (object) (this.panelMain.ClientRectangle.Width / 3 * 2));
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
        if (!this.btShowAll.Checked && (schemeGuid == Consts.ObjectColumnSchemeGuid || schemeGuid == Consts.RelationColumnSchemeGuid))
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
    this.FSchemesNames.GetEnumerator().Reset();
    if (eraseGroups)
      this.RemoveGroups();
    else
      this.RemoveInvalidGroups(eraseAttrs);
    for (int index = 0; index < this.FSupportedColumns.Count; ++index)
    {
      Guid schemeGuid = this.FSupportedColumns[index].SchemeGuid;
      INodeColumnScheme fcolumnScheme = this.FColumnSchemes[schemeGuid];
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
      if ((this.btShowAll.Checked || !(schemeGuid == Consts.ObjectColumnSchemeGuid) && !(schemeGuid == Consts.RelationColumnSchemeGuid)) && flag)
      {
        this.treeGroups.Add(new AppearanceTuningForm.TreeNodeSchemeItem(schemeGuid, fcolumnScheme, columns));
        this.treeGroupRows.Add(schemeGuid, (Row) null);
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
      AppearanceTuningForm.TreeNodeSchemeItem treeNodeSchemeItem = row.Item as AppearanceTuningForm.TreeNodeSchemeItem;
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
      this.listVisible.BeginUpdate();
      this.listVisible.Items.Clear();
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
        if ((this.filteredAttributes.ContainsKey(key) || key <= -10000) && (this.btShowAll.Checked || !fsupportedColumn.SystemAttr && !(fsupportedColumn.SchemeGuid == Consts.ObjectColumnSchemeGuid) && !(fsupportedColumn.SchemeGuid == Consts.RelationColumnSchemeGuid)) && (regex == null || regex.IsMatch(fsupportedColumn.Caption)) && (!(upper != string.Empty) || fsupportedColumn.Caption.ToUpper().IndexOf(upper) >= 0) && this.FColumns.Find(fsupportedColumn.Key) == null)
          this.treeColumns[fsupportedColumn.SchemeGuid].Add(this.GetNodeColumnIndex(fsupportedColumn));
      }
      List<int> intList = new List<int>(this.FColumns.Count);
      for (int index = 0; index < this.FColumns.Count; ++index)
      {
        NodeColumn fcolumn = this.FColumns[index];
        int nodeColumnIndex = this.GetNodeColumnIndex(fcolumn);
        if (nodeColumnIndex < 0)
        {
          if (!this.DictRevSupportedColumns.ContainsKey(fcolumn.Caption) && this.FSupportedColumns.IndexOf(fcolumn) < 0)
          {
            if (this.FSupportedColumns.FindCaption(fcolumn.Caption) == null)
            {
              this.FSupportedColumns.Add(fcolumn);
              this.DictSupportedColumns.Add(this.FSupportedColumns.Count - 1, fcolumn);
              this.DictRevSupportedColumns.Add(fcolumn.Caption, this.FSupportedColumns.Count - 1);
            }
            nodeColumnIndex = this.GetNodeColumnIndex(fcolumn);
          }
          if (nodeColumnIndex < 0)
            intList.Add(index);
        }
        if (nodeColumnIndex >= 0)
          this.AddListItem(this.listVisible, fcolumn);
      }
      for (int index = intList.Count - 1; index >= 0; --index)
        this.FColumns.RemoveAt(intList[index]);
    }
    finally
    {
      this.RemoveEmptyGroups();
      this.SortGroups();
      this.FillTree(true);
      this.treeAvailable.RootRow.ExpandChildren(true);
      this.listVisible.Scrollable = false;
      this.listVisible.Scrollable = true;
      this.listVisible.EndUpdate();
      this.inEvent = inEvent;
    }
  }

  /// <summary>Выбрана команда в меню</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void mnpShowAll_Click(object sender, EventArgs e)
  {
    if (this.inEvent)
      return;
    this.btShowAll.Checked = this.mnpShowAll.Checked;
    this.DoReloadAttrsList(sender, e);
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
      this.mnpShowAll.Checked = this.btShowAll.Checked;
      MyElement myElement = (MyElement) null;
      if (this.comboSearch.SelectedIndex >= 0)
        myElement = this.comboSearch.Items[this.comboSearch.SelectedIndex] as MyElement;
      Regex regex = (Regex) null;
      if (myElement != null)
        regex = myElement.Value as Regex;
      if (myElement.Tag != null && myElement.Tag.Equals((object) true))
      {
        regex = (Regex) null;
        myElement = (MyElement) null;
      }
      this.CreateTreeGroups(false, true);
      for (int index = 0; index < this.FSupportedColumns.Count; ++index)
      {
        NodeColumn fsupportedColumn1 = this.FSupportedColumns[index];
        NodeColumn fsupportedColumn2 = this.FSupportedColumns[index];
        if (this.FColumns.Find(fsupportedColumn2.Key) == null)
        {
          if (!this.btShowAll.Enabled)
          {
            int key = this.ConvertIDtoInt32(fsupportedColumn2.ID);
            if (!this.filteredAttributes.ContainsKey(key) && key > -10000)
              continue;
          }
          bool flag = true;
          if (myElement != null)
          {
            string tag = (string) myElement.Tag;
            if (this.FColumnSchemes[fsupportedColumn2.SchemeGuid].Name != tag)
              continue;
          }
          if (regex != null)
          {
            if (regex.IsMatch(fsupportedColumn2.Caption))
              flag = true;
            else
              continue;
          }
          if (upper != string.Empty)
            flag = fsupportedColumn2.Caption.Trim().ToUpper().IndexOf(upper) >= 0;
          if (!this.btShowAll.Checked)
          {
            if (fsupportedColumn2.SystemAttr)
              flag = false;
            if (fsupportedColumn2.SchemeGuid == Consts.ObjectColumnSchemeGuid || fsupportedColumn2.SchemeGuid == Consts.RelationColumnSchemeGuid)
              flag = false;
          }
          if (flag && this.FColumns.Find(fsupportedColumn2.Key) == null)
            this.treeColumns[fsupportedColumn2.SchemeGuid].Add(this.GetNodeColumnIndex(fsupportedColumn2));
        }
      }
    }
    finally
    {
      this.RemoveEmptyGroups();
      this.listVisible.EndUpdate();
      this.InitFiltersList();
      this.FillTree(true);
      this.FocusFirstNode();
      if (sender == this.cbFind && this.treeAvailable.RootRow != null)
        this.treeAvailable.RootRow.ExpandChildren(true);
      this.UpdateControls();
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
    int imageIndex = attr.Attribute != null ? this.GetTypeImageIndex(attr.Attribute.FieldType) : -1;
    string str1 = LocalizationHolder.rm.GetString("Client.Core_319");
    bool flag1 = attr.Caption.EndsWith(str1);
    string str2 = attr.Caption;
    Intermech.Interfaces.Attributes.AttributeInfo source = attr.Source as Intermech.Interfaces.Attributes.AttributeInfo;
    if (!flag1)
    {
      string str3 = source != null && source.AttrSrc == FieldSource.Relation || this.SelectedScheme is RelationColumnScheme ? "(связь)" : (source != null && source.AttrSrc == FieldSource.Object || this.SelectedScheme is ObjectColumnScheme ? "(объект)" : "(графа)");
      str2 = $"{str2} {str3}";
    }
    ListViewItem listViewItem = list.Items.Add("  " + str2, imageIndex);
    int num = this.ConvertIDtoInt32(attr.ID);
    bool flag2 = num > 0 && this._nodeID != null;
    if (this._nodeID != null && this.node != null && num > 0)
    {
      if ((this.node.Options & NodeOptions.CanContainsComposition) == NodeOptions.CanContainsComposition & flag1)
      {
        for (int index = 0; index < this.relationsTypeAttrs.Count; ++index)
        {
          if (this.relationsTypeAttrs[index].AttributeID == num)
          {
            flag2 = false;
            break;
          }
        }
      }
      if (this.objectTypeAttrs != null & flag2)
      {
        for (int index = 0; index < this.objectTypeAttrs.Count; ++index)
        {
          if (this.objectTypeAttrs[index].AttributeID == num)
          {
            flag2 = false;
            break;
          }
        }
      }
    }
    if (flag2)
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
      e.Children = (IList) ((AppearanceTuningForm.TreeNodeSchemeItem) e.Row.Item).Columns;
    }
  }

  /// <summary>Получить необходимую информацию о строках в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAvailable_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.Row.Level == 1)
    {
      AppearanceTuningForm.TreeNodeSchemeItem treeNodeSchemeItem = (AppearanceTuningForm.TreeNodeSchemeItem) e.Row.Item;
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
      int num = nodeColumn.Attribute != null ? this.GetTypeImageIndex(nodeColumn.Attribute.FieldType) : -1;
      e.RowData.ImageList = this.FAttrTypesIcons != null ? this.FAttrTypesIcons.ImageList : (ImageList) null;
      e.RowData.ImageIndex = num;
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
      AppearanceTuningForm.TreeNodeSchemeItem treeNodeSchemeItem = (AppearanceTuningForm.TreeNodeSchemeItem) e.Row.Item;
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
    bool flag = false;
    foreach (Row row in this.treeAvailable.SelectedRows.ToArray<Row>())
    {
      if (row.Item is int num && row.ParentRow != null && row.ParentRow.Item is AppearanceTuningForm.TreeNodeSchemeItem treeNodeSchemeItem && (treeNodeSchemeItem.Columns == null || treeNodeSchemeItem.Columns != null && !treeNodeSchemeItem.Columns.Contains(num)))
      {
        this.treeAvailable.SelectedRows.Remove(row);
        flag = true;
      }
    }
    if (flag)
      this.FillTree(false);
    this.UpdateControls();
  }

  /// <summary>
  /// Требуется показать контекстное меню в ячейке дерева допустимых колонок
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeAvailable_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this.contextMenuAvailable.Show((Control) this.treeAvailable, e.Location);
  }

  /// <summary>
  /// Пересчитать ширину колонок в списке выбранных атрибутов
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoResizeColumnWidthVisible(object sender, EventArgs e)
  {
    int num = this.listVisible.ClientRectangle.Width - 30;
    if (num <= 0)
      return;
    this.listVisible.Columns[0].Width = num;
  }

  /// <summary>
  /// Изменился выделенный элемент в списке видимых атрибутов
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoVisibleSelChanged(object sender, EventArgs e)
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
    if (this.treeAvailable.RootRow == null)
      return;
    this.treeAvailable.RootRow.CollapseChildren(true);
    this.treeAvailable.RootRow.EnsureVisible();
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

  /// <summary>Задать ширину указанным колонкам</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoSetColumnsWidth(object sender, EventArgs e)
  {
    if (this.disableWidthEvents)
      return;
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    if (selectedItems == null || selectedItems.Count <= 0)
      return;
    for (int index1 = 0; index1 < selectedItems.Count; ++index1)
    {
      NodeColumn nodeColumn = this.GetNodeColumn((int) selectedItems[index1].Tag);
      if (nodeColumn != null)
      {
        nodeColumn.Width = (int) this.edWidth.Value;
        int index2 = this.FColumns.IndexOf(nodeColumn);
        if (index2 < 0)
          index2 = this.FColumns.IndexOf(this.FColumns.FindCaption(nodeColumn.Caption));
        if (index2 >= 0)
          this.FColumns[index2].Width = nodeColumn.Width;
      }
    }
    this.FIsChanged = true;
    try
    {
      this.inWidthEvents = true;
      this.UpdateControls();
    }
    finally
    {
      this.inWidthEvents = false;
    }
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
  private void btnApply_Click(object sender, EventArgs e)
  {
    NodeColumnCollection.CorrectSortIndex(this.FColumns);
    this._okPressed = true;
  }

  /// <summary>Форма закрывается</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void AppearanceTuningForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.FIsChanged || this._okPressed || e.CloseReason != CloseReason.UserClosing && e.CloseReason != CloseReason.None || MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1426"), LocalizationHolder.rm.GetString("Client.Core_1261"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
      return;
    e.Cancel = true;
  }

  /// <summary>Добавить указанные элементы в список видимых колонок</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void btAdd_Click(object sender, EventArgs e)
  {
    if (this.inEvent || this.treeAvailable.SelectedRows.Count == 0)
      return;
    ListViewItem listViewItem = (ListViewItem) null;
    Dictionary<Guid, List<int>> dictionary = new Dictionary<Guid, List<int>>();
    if (this.treeAvailable.SelectedRows.Count == 1 && this.treeAvailable.SelectedRows[0].Level == 1)
      return;
    try
    {
      this.inEvent = true;
      this.listVisible.BeginUpdate();
      for (int index = 0; index < this.treeAvailable.SelectedRows.Count; ++index)
      {
        Row selectedRow = this.treeAvailable.SelectedRows[index];
        if (selectedRow.Level != 1)
        {
          Row parentRow = selectedRow.ParentRow;
          AppearanceTuningForm.TreeNodeSchemeItem treeNodeSchemeItem = parentRow != null ? parentRow.Item as AppearanceTuningForm.TreeNodeSchemeItem : (AppearanceTuningForm.TreeNodeSchemeItem) null;
          if (treeNodeSchemeItem != null)
          {
            NodeColumn nodeColumn = this.GetNodeColumn((int) selectedRow.Item);
            if (nodeColumn != null)
            {
              if (this.FColumns.IndexOf(nodeColumn) < 0)
              {
                this.FColumns.Add(nodeColumn);
                listViewItem = this.AddListItem(this.listVisible, nodeColumn);
              }
              if (!dictionary.ContainsKey(treeNodeSchemeItem.Guid))
                dictionary.Add(treeNodeSchemeItem.Guid, new List<int>());
              dictionary[treeNodeSchemeItem.Guid].Add(selectedRow.ChildIndex);
            }
          }
        }
      }
      foreach (KeyValuePair<Guid, List<int>> keyValuePair in dictionary)
      {
        keyValuePair.Value.Sort();
        if (this.treeColumns.ContainsKey(keyValuePair.Key))
        {
          List<int> treeColumn = this.treeColumns[keyValuePair.Key];
          for (int index = keyValuePair.Value.Count - 1; index >= 0; --index)
          {
            if (keyValuePair.Value[index] >= 0 && keyValuePair.Value[index] < treeColumn.Count)
              treeColumn.RemoveAt(keyValuePair.Value[index]);
          }
        }
      }
      this.RemoveEmptyGroups();
    }
    finally
    {
      this.listVisible.EndUpdate();
      this.treeAvailable.SelectedRow = (Row) null;
      listViewItem?.EnsureVisible();
      this.FillTree(false);
      this.FIsChanged = true;
      this.UpdateControls();
      this.inEvent = false;
    }
  }

  /// <summary>
  /// Удалить выделенные элемнеты из списка видимых атрибутов
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void btDelete_Click(object sender, EventArgs e)
  {
    if (this.inEvent)
      return;
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    if (selectedItems == null || selectedItems.Count <= 0)
      return;
    List<ListViewItem> listViewItemList = new List<ListViewItem>(selectedItems.Count);
    List<NodeColumn> nodeColumnList = new List<NodeColumn>();
    List<Guid> schemes = new List<Guid>();
    try
    {
      this.inEvent = true;
      this.listVisible.BeginUpdate();
      this.CreateTreeGroups(false, false);
      for (int index = selectedItems.Count - 1; index >= 0; --index)
      {
        ListViewItem listViewItem = selectedItems[index];
        listViewItemList.Add(listViewItem);
        int tag = (int) listViewItem.Tag;
        NodeColumn nodeColumn = this.GetNodeColumn(tag);
        if (tag != -1 && nodeColumn != null)
        {
          nodeColumnList.Add(nodeColumn);
          this.FColumns.Remove(this.FColumns.FindCaption(nodeColumn.Caption));
          this.FColumns.Remove(nodeColumn);
          if (this.FColumns.Find(nodeColumn.Key) == null)
            this.treeColumns[nodeColumn.SchemeGuid].Add(tag);
          if (schemes.IndexOf(nodeColumn.SchemeGuid) < 0)
            schemes.Add(nodeColumn.SchemeGuid);
        }
      }
      for (int index = 0; index < listViewItemList.Count; ++index)
        this.listVisible.Items.Remove(listViewItemList[index]);
      listViewItemList.Clear();
    }
    finally
    {
      List<Guid> guidList = new List<Guid>();
      this.listVisible.EndUpdate();
      this.FIsChanged = true;
      this.RemoveEmptyGroups();
      this.FillTree(false);
      this.ExpandRows(schemes, true);
      this.UpdateControls();
      this.inEvent = false;
    }
  }

  /// <summary>Добавить все атрибуты в список видимых колонок</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void btAddAll_Click(object sender, EventArgs e)
  {
    if (this.inEvent)
      return;
    try
    {
      this.inEvent = true;
      this.listVisible.BeginUpdate();
      List<int> availableColumns = this.AvailableColumns;
      for (int index = 0; index < availableColumns.Count; ++index)
      {
        NodeColumn nodeColumn = this.GetNodeColumn(availableColumns[index]);
        if (nodeColumn != null && this.FColumns.IndexOf(nodeColumn) < 0)
        {
          this.FColumns.Add(nodeColumn);
          this.AddListItem(this.listVisible, nodeColumn);
        }
      }
      this.RemoveGroups();
    }
    finally
    {
      this.listVisible.EndUpdate();
      this.FillTree(false);
      this.FIsChanged = true;
      this.UpdateControls();
      this.inEvent = false;
    }
  }

  /// <summary>Удалить все видимые колонки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void btDeleteAll_Click(object sender, EventArgs e)
  {
    if (this.inEvent)
      return;
    if (this.listVisible.Items.Count <= 0)
      return;
    try
    {
      this.inEvent = true;
      this.FColumns.Clear();
      this.FillAttrsLists();
    }
    finally
    {
      this.FIsChanged = true;
      this.UpdateControls();
      this.inEvent = false;
    }
  }

  /// <summary>
  /// Переместить выделенные колонки по списку видимых колонок вверх
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btMoveUp_Click(object sender, EventArgs e)
  {
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    if (selectedItems == null || selectedItems.Count <= 0)
      return;
    ListViewItem listViewItem1 = selectedItems[0];
    bool inEvent = this.inEvent;
    try
    {
      this.inEvent = true;
      if (e != null)
        this.listVisible.BeginUpdate();
      for (int index1 = 0; index1 < selectedItems.Count; ++index1)
      {
        ListViewItem listViewItem2 = selectedItems[index1];
        int index2 = listViewItem2.Index;
        if (index2 != 0)
        {
          this.listVisible.Items.Remove(listViewItem2);
          this.listVisible.Items.Insert(index2 - 1, listViewItem2);
          NodeColumn nodeColumn = this.GetNodeColumn((int) listViewItem2.Tag);
          int num = this.FColumns.IndexOf(nodeColumn);
          if (num < 0)
            num = this.FColumns.IndexOf(this.FColumns.FindCaption(nodeColumn.Caption));
          if (num > 0)
          {
            this.FColumns.Remove(this.FColumns.FindCaption(nodeColumn.Caption));
            this.FColumns.Remove(nodeColumn);
            this.FColumns.Insert(num - 1, nodeColumn);
          }
        }
      }
      this.listVisible.TopItem = listViewItem1;
    }
    finally
    {
      if (e != null)
        this.listVisible.EndUpdate();
      this.FIsChanged = true;
      if (e != null)
        this.UpdateControls();
      this.inEvent = inEvent;
    }
  }

  /// <summary>
  /// Переместить выделенные колонки по списку видимых колонок вниз
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btMoveDown_Click(object sender, EventArgs e)
  {
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    if (selectedItems == null || selectedItems.Count <= 0)
      return;
    ListViewItem listViewItem1 = selectedItems[0];
    ListViewItem listViewItem2 = selectedItems[selectedItems.Count - 1];
    bool inEvent = this.inEvent;
    try
    {
      this.inEvent = true;
      if (e != null)
        this.listVisible.BeginUpdate();
      for (int index1 = selectedItems.Count - 1; index1 >= 0; --index1)
      {
        ListViewItem listViewItem3 = selectedItems[index1];
        int index2 = listViewItem3.Index;
        if (index2 != this.listVisible.Items.Count - 1)
        {
          this.listVisible.Items.Remove(listViewItem3);
          this.listVisible.Items.Insert(index2 + 1, listViewItem3);
          NodeColumn nodeColumn = this.GetNodeColumn((int) listViewItem3.Tag);
          int num = this.FColumns.IndexOf(nodeColumn);
          if (num < 0)
            num = this.FColumns.IndexOf(this.FColumns.FindCaption(nodeColumn.Caption));
          if (num < this.FColumns.Count - 1)
          {
            this.FColumns.Remove(this.FColumns.FindCaption(nodeColumn.Caption));
            this.FColumns.Remove(nodeColumn);
            this.FColumns.Insert(num + 1, nodeColumn);
          }
        }
      }
      this.listVisible.TopItem = listViewItem1;
    }
    finally
    {
      if (e != null)
        this.listVisible.EndUpdate();
      this.FIsChanged = true;
      if (e != null)
        this.UpdateControls();
      this.inEvent = inEvent;
    }
  }

  /// <summary>
  /// Переместить выделенные колонки в начало списка видимых колонок
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMoveTop(object sender, EventArgs e)
  {
    if (this.inEvent)
      return;
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    if (selectedItems == null || selectedItems.Count <= 0)
      return;
    ListViewItem listViewItem1 = selectedItems[0];
    ListViewItem listViewItem2 = selectedItems[selectedItems.Count - 1];
    int num = e != null ? listViewItem1.Index : this.listVisible.Items.Count - listViewItem2.Index - 1;
    if (num == 0)
      return;
    try
    {
      this.listVisible.BeginUpdate();
      for (int index = 0; index < Math.Abs(num); ++index)
      {
        if (e != null)
          this.btMoveUp_Click((object) this, (EventArgs) null);
        else
          this.btMoveDown_Click((object) this, (EventArgs) null);
      }
    }
    finally
    {
      this.listVisible.EndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Переместить выделенные колонки в конец списка видимых колонок
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMoveBottom(object sender, EventArgs e) => this.DoMoveTop(sender, (EventArgs) null);

  /// <summary>Список доступных атрибутов - нажата кнопка мыши</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMouseDown_All(object sender, MouseEventArgs e)
  {
    if (this.treeAvailable.SelectedRows.Count > 0)
    {
      Size dragSize = SystemInformation.DragSize;
      dragSize.Width += 4;
      dragSize.Height += 4;
      this.dragBoxFromMouseDownAll = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
    }
    else
      this.dragBoxFromMouseDownAll = Rectangle.Empty;
  }

  /// <summary>Список доступных атрибутов - перемещён курсор мыши</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMouseMove_All(object sender, MouseEventArgs e)
  {
    if ((e.Button & MouseButtons.Left) != MouseButtons.Left || !(this.dragBoxFromMouseDownAll != Rectangle.Empty) || this.dragBoxFromMouseDownAll.Contains(e.X, e.Y))
      return;
    if (this.treeAvailable.GetRowAt(e.X, e.Y) == null)
    {
      this.dragBoxFromMouseDownAll = Rectangle.Empty;
    }
    else
    {
      this.screenOffset = SystemInformation.WorkingArea.Location;
      int num = (int) this.treeAvailable.DoDragDrop((object) this.treeAvailable, DragDropEffects.Move | DragDropEffects.Scroll);
    }
  }

  /// <summary>Список доступных атрибутов - отпущена кнопка мыши</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMouseUp_All(object sender, MouseEventArgs e)
  {
    this.dragBoxFromMouseDownAll = Rectangle.Empty;
  }

  /// <summary>
  /// Перетаскивание "пришло" в клиентскую область списка доступных атрибутов
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoDragEnter_All(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!this.treeAvailable.AllowDrop || !e.Data.GetDataPresent(typeof (ListView)))
      return;
    if (e.Data.GetData(typeof (ListView)) is ListView)
      e.Effect = DragDropEffects.All;
    else
      e.Effect = DragDropEffects.None;
  }

  private void treeAvailable_GetRowDropEffect(object sender, GetRowDropEffectEventArgs e)
  {
    e.DropEffect = DragDropEffects.None;
    if (!this.treeAvailable.AllowDrop || !e.Data.GetDataPresent(typeof (ListView)))
      return;
    if (e.Data.GetData(typeof (ListView)) is ListView)
      e.DropEffect = DragDropEffects.All;
    else
      e.DropEffect = DragDropEffects.None;
  }

  private void treeAvailable_GetAllowedRowDropLocations(
    object sender,
    GetAllowedRowDropLocationsEventArgs e)
  {
    e.AllowedDropLocations = RowDropLocation.AboveRow | RowDropLocation.BelowRow | RowDropLocation.OnRow;
  }

  /// <summary>
  /// Список доступных атрибутов - "перетаскивание" завершено
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoDragDrop_All(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!this.treeAvailable.AllowDrop || !e.Data.GetDataPresent(typeof (ListView)))
      return;
    e.Data.GetData(typeof (ListView));
    e.Effect = DragDropEffects.Move;
    this.UpdateControls();
    if (!this.btDelete.Enabled)
      return;
    this.btDelete_Click((object) this, (EventArgs) null);
  }

  /// <summary>Список видимых атрибутов - нажата кнопка мыши</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMouseDown_Visible(object sender, MouseEventArgs e)
  {
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    if (selectedItems != null & selectedItems.Count > 0)
    {
      Size dragSize = SystemInformation.DragSize;
      dragSize.Width += 4;
      dragSize.Height += 4;
      this.dragBoxFromMouseDownVisible = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
    }
    else
      this.dragBoxFromMouseDownVisible = Rectangle.Empty;
  }

  /// <summary>Список видимых атрибутов - перемещён курсор мыши</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMouseMove_Visible(object sender, MouseEventArgs e)
  {
    if ((e.Button & MouseButtons.Left) != MouseButtons.Left || !(this.dragBoxFromMouseDownVisible != Rectangle.Empty) || this.dragBoxFromMouseDownVisible.Contains(e.X, e.Y))
      return;
    ListViewItem itemAt = this.listVisible.GetItemAt(e.X, e.Y);
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    if (itemAt == null || selectedItems == null || selectedItems.Count == 0)
    {
      this.dragBoxFromMouseDownVisible = Rectangle.Empty;
    }
    else
    {
      this.screenOffset = SystemInformation.WorkingArea.Location;
      int num = (int) this.listVisible.DoDragDrop((object) this.listVisible, DragDropEffects.Move | DragDropEffects.Scroll);
    }
  }

  /// <summary>Список доступных атрибутов - отпущена кнопка мыши</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoMouseUp_Visible(object sender, MouseEventArgs e)
  {
    this.dragBoxFromMouseDownVisible = Rectangle.Empty;
  }

  /// <summary>
  /// Перетаскивание "пришло" в клиентскую область списка видимых атрибутов
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoDragEnter_Visible(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!this.listVisible.AllowDrop || !e.Data.GetDataPresent(typeof (Intermech.VirtualTreeView.VirtualTreeView)) && !e.Data.GetDataPresent(typeof (ListView)))
      return;
    e.Effect = DragDropEffects.Move;
  }

  /// <summary>Список видимых атрибутов - "перетаскивание" завершено</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoDragDrop_Visible(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (!this.listVisible.AllowDrop || !e.Data.GetDataPresent(typeof (Intermech.VirtualTreeView.VirtualTreeView)) && !e.Data.GetDataPresent(typeof (ListView)))
      return;
    Intermech.VirtualTreeView.VirtualTreeView data = e.Data.GetData(typeof (Intermech.VirtualTreeView.VirtualTreeView)) as Intermech.VirtualTreeView.VirtualTreeView;
    e.Data.GetData(typeof (ListView));
    e.Effect = DragDropEffects.Move;
    this.UpdateControls();
    if (data != null && this.btAdd.Enabled)
      this.btAdd_Click((object) this, (EventArgs) null);
    Point client = this.listVisible.PointToClient(new Point(e.X, e.Y));
    ListViewItem itemAt = this.listVisible.GetItemAt(client.X, client.Y);
    ListView.SelectedListViewItemCollection selectedItems = this.listVisible.SelectedItems;
    if (itemAt == null || selectedItems == null || selectedItems.Count == 0)
      return;
    int num = selectedItems[0].Index - itemAt.Index;
    if (num == 0)
      return;
    try
    {
      this.listVisible.BeginUpdate();
      for (int index = num < 0 ? 1 : 0; index < Math.Abs(num); ++index)
      {
        if (num > 0)
          this.btMoveUp_Click((object) this, (EventArgs) null);
        else
          this.btMoveDown_Click((object) this, (EventArgs) null);
      }
    }
    finally
    {
      this.listVisible.EndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>Сбросить настройки колонок по умолчанию</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoResetColumns(object sender, EventArgs e)
  {
    if (this.node == null)
      return;
    this.FColumns.Clear();
    this.FColumns.AddRange((IEnumerable<NodeColumn>) this.node.GetDefaultColumns(this.content));
    if (this.FColumns.Count == 0)
      this.FColumns.AddRange((IEnumerable<NodeColumn>) this.node.GetDefaultColumns(ContentType.Folders));
    if (this.FColumns.Count == 0)
      this.FColumns.AddRange((IEnumerable<NodeColumn>) this.node.GetDefaultColumns(ContentType.NonFolders));
    this.FSupportedColumns.SyncWithMaster(this.FColumns);
    for (int index = this.FColumns.Count - 1; index >= 0; --index)
    {
      if (this.FSupportedColumns.Find(this.FColumns[index].ID) == null)
        this.FColumns.RemoveAt(index);
    }
    if (this.FColumns.Count == 0 && this.FSupportedColumns.Count > 0)
      this.FColumns.Add(this.FSupportedColumns[0]);
    this.Init(this.node, this.content, this.FSupportedColumns, this.FColumns, false);
    this.FIsChanged = true;
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
    this.toolBarLeft.Renderer = renderer;
    this.toolBarRight.Renderer = renderer;
    this.toolBarTree.Renderer = renderer;
    this.menuAvailable.Renderer = renderer;
    this.menuVisible.Renderer = renderer;
  }

  /// <summary>Событие от таймера</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void timerReload_Tick(object sender, EventArgs e)
  {
    this.timerReload.Enabled = false;
    this.DoReloadAttrsList((object) this.cbFind, (EventArgs) null);
  }

  private void ApplyParentNodeDisplaySettingsButton_Click(object sender, EventArgs e)
  {
    INavigatorColumnsService service = ServicesManager.GetService(typeof (INavigatorColumnsService)) as INavigatorColumnsService;
    NodeColumnCollection collection = (NodeColumnCollection) null;
    foreach (INodeID nodeId in this._nodeIDPath.Cast<INodeID>().Reverse<INodeID>())
    {
      if (nodeId != this._nodeID)
      {
        NavigatorColumns navigatorColumns;
        if (nodeId is NodeID && (Constants.SelectionObjectTypeID == nodeId.TypeID || MetaDataHelper.GetObjectTypeChildrenIDRecursive(Constants.SelectionObjectTypeID).Contains(nodeId.TypeID)))
        {
          int categoryId1 = nodeId.CategoryID;
          int typeId = nodeId.TypeID;
          long num = Math.Abs(((NodeID) nodeId).ObjectID);
          string prefix = num.ToString();
          if (this.node.GetData(this._nodeID, typeof (IBinding)) is IBinding data && data is IBindingStateStream bindingStateStream)
          {
            int categoryId2 = bindingStateStream.CategoryID;
            int categoryType = bindingStateStream.CategoryType;
            if (!string.IsNullOrEmpty(bindingStateStream.Prefix))
              prefix = bindingStateStream.Prefix;
            navigatorColumns = service.GetNavigatorColumns(categoryId2, categoryType, prefix, true);
          }
          else if (nodeId is NodeID && MetaDataHelper.IsObjectTypeChildOf(nodeId.TypeID, Constants.ArchiveObjectTypeID))
          {
            string stateStreamPrefix = Constants.ArchiveStateStreamPrefix;
            num = Math.Abs(((NodeID) nodeId).ObjectID);
            string str = num.ToString();
            string suffix = stateStreamPrefix + str;
            navigatorColumns = service.GetNavigatorColumns(nodeId.CategoryID, nodeId.TypeID, suffix, true);
          }
          else
            navigatorColumns = service.GetNavigatorColumns(nodeId.CategoryID, nodeId.TypeID, this.StateStreamSuffix, true);
        }
        else
          navigatorColumns = service.GetNavigatorColumns(nodeId.CategoryID, nodeId.TypeID, this.StateStreamSuffix, true);
        if (navigatorColumns != null && navigatorColumns.Columns != null && navigatorColumns.Columns.Count > 0)
        {
          collection = navigatorColumns.Columns;
          break;
        }
      }
    }
    if (collection != null && collection.Count > 0)
    {
      if (collection.Count > 0)
      {
        this.FColumns.Clear();
        this.FColumns.AddRange((IEnumerable<NodeColumn>) collection);
        this.Init(this.node, this.content, this.FSupportedColumns, this.FColumns, false);
        this.FIsChanged = true;
        this.UpdateControls();
      }
      else
      {
        int num1 = (int) MessageBox.Show("Невозможно выполнить команду, среди колонок родительского узла не найдено подходящих.", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
    else
    {
      int num2 = (int) MessageBox.Show("Невозможно выполнить команду, не найдено настроек для родительских узлов.", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBarRight.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolBarLeft.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolBarTree.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuAvailable.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuVisible.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AppearanceTuningForm));
    this.panelMain = new SplitContainer();
    this.panelLeft = new Panel();
    this.treeAvailable = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnAttribute = new Column();
    this.toolBarLeft = new Intermech.Bars.ToolBar();
    this.imagesToolbars = new ImageList(this.components);
    this.btAdd = new ButtonItem();
    this.btAddAll = new ButtonItem();
    this.btDelete = new ButtonItem();
    this.btDeleteAll = new ButtonItem();
    this.toolBarTree = new Intermech.Bars.ToolBar();
    this.btCollapse = new ButtonItem();
    this.btExpand = new ButtonItem();
    this.btShowAll = new ButtonItem();
    this.panel1 = new Panel();
    this.label1 = new Label();
    this.cbFind = new ComboBox();
    this.comboSearch = new ComboBox();
    this.labelSearch = new Label();
    this.menuAvailable = new MenuBar();
    this.contextMenuAvailable = new ContextMenuBarItem();
    this.mnpAdd = new MenuButtonItem();
    this.mnpAddAll = new MenuButtonItem();
    this.mnpCollapse = new MenuButtonItem();
    this.mnpExpand = new MenuButtonItem();
    this.mnpShowAll = new MenuButtonItem();
    this.panelRight = new Panel();
    this.listVisible = new ListView();
    this.columnVisibleName = new ColumnHeader();
    this.labelManyColumns = new Label();
    this.labelWarning = new Label();
    this.menuVisible = new MenuBar();
    this.contextMenuVisible = new ContextMenuBarItem();
    this.mnpMoveTop = new MenuButtonItem();
    this.mnpMoveUp = new MenuButtonItem();
    this.mnpMoveDown = new MenuButtonItem();
    this.mnpMoveBottom = new MenuButtonItem();
    this.mnpDelete = new MenuButtonItem();
    this.mnpDeleteAll = new MenuButtonItem();
    this.toolBarRight = new Intermech.Bars.ToolBar();
    this.btMoveTop = new ButtonItem();
    this.btMoveUp = new ButtonItem();
    this.btMoveDown = new ButtonItem();
    this.btMoveBottom = new ButtonItem();
    this.panelTrack = new Panel();
    this.labelWidth = new Label();
    this.edWidth = new NumericUpDown();
    this.panelBottom = new Panel();
    this._applyParentNodeDisplaySettingsButton = new Button();
    this.btnDefault = new Button();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.images = new ImageList(this.components);
    this.toolTip = new ToolTip(this.components);
    this.panelWarning = new Panel();
    this.labelWarning2 = new Label();
    this.pictureWarning = new PictureBox();
    this.timerReload = new Timer(this.components);
    this.panelMain.BeginInit();
    this.panelMain.Panel1.SuspendLayout();
    this.panelMain.Panel2.SuspendLayout();
    this.panelMain.SuspendLayout();
    this.panelLeft.SuspendLayout();
    this.treeAvailable.BeginInit();
    this.panel1.SuspendLayout();
    this.panelRight.SuspendLayout();
    this.panelTrack.SuspendLayout();
    this.edWidth.BeginInit();
    this.panelBottom.SuspendLayout();
    this.panelWarning.SuspendLayout();
    ((ISupportInitialize) this.pictureWarning).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
    this.panelMain.Name = "panelMain";
    this.panelMain.Panel1.Controls.Add((Control) this.panelLeft);
    this.panelMain.Panel2.Controls.Add((Control) this.panelRight);
    this.panelLeft.Controls.Add((Control) this.treeAvailable);
    this.panelLeft.Controls.Add((Control) this.toolBarLeft);
    this.panelLeft.Controls.Add((Control) this.toolBarTree);
    this.panelLeft.Controls.Add((Control) this.panel1);
    this.panelLeft.Controls.Add((Control) this.menuAvailable);
    componentResourceManager.ApplyResources((object) this.panelLeft, "panelLeft");
    this.panelLeft.Name = "panelLeft";
    this.treeAvailable.AllowDrop = true;
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
    this.treeAvailable.ShowContextMenu += new MouseEventHandler(this.treeAvailable_ShowContextMenu);
    this.treeAvailable.CellDoubleClick += new EventHandler(this.btAdd_Click);
    this.treeAvailable.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.treeAvailable_GetAllowedRowDropLocations);
    this.treeAvailable.GetCellData += new GetCellDataHandler(this.treeAvailable_GetCellData);
    this.treeAvailable.GetChildPolicy += new GetChildPolicyHandler(this.treeAvailable_GetChildPolicy);
    this.treeAvailable.GetChildren += new GetChildrenHandler(this.treeAvailable_GetChildren);
    this.treeAvailable.GetRowData += new GetRowDataHandler(this.treeAvailable_GetRowData);
    this.treeAvailable.GetRowDropEffect += new GetRowDropEffectHandler(this.treeAvailable_GetRowDropEffect);
    this.treeAvailable.SelectionChanged += new EventHandler(this.treeAvailable_SelectionChanged);
    this.treeAvailable.DragDrop += new DragEventHandler(this.DoDragDrop_All);
    this.treeAvailable.DragEnter += new DragEventHandler(this.DoDragEnter_All);
    this.treeAvailable.MouseDown += new MouseEventHandler(this.DoMouseDown_All);
    this.treeAvailable.MouseMove += new MouseEventHandler(this.DoMouseMove_All);
    this.treeAvailable.MouseUp += new MouseEventHandler(this.DoMouseUp_All);
    this.columnAttribute.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnAttribute, "columnAttribute");
    this.columnAttribute.CellStyle.BorderWidth = 0;
    this.columnAttribute.Movable = false;
    this.columnAttribute.Name = "columnAttribute";
    this.columnAttribute.Sortable = false;
    this.toolBarLeft.AddRemoveButtonsVisible = false;
    this.toolBarLeft.AllowHorizontalDock = false;
    componentResourceManager.ApplyResources((object) this.toolBarLeft, "toolBarLeft");
    this.toolBarLeft.DockLine = 3;
    this.toolBarLeft.DrawActionsButton = false;
    this.toolBarLeft.Flow = ToolBarLayout.Vertical;
    this.toolBarLeft.FullMenus = true;
    this.toolBarLeft.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarLeft.Hidden = false;
    this.toolBarLeft.ImageList = this.imagesToolbars;
    this.toolBarLeft.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.btAdd,
      (ToolbarItemBase) this.btAddAll,
      (ToolbarItemBase) this.btDelete,
      (ToolbarItemBase) this.btDeleteAll
    });
    this.toolBarLeft.MinimumFloatingSize = new Size(250, 30);
    this.toolBarLeft.Name = "toolBarLeft";
    this.toolBarLeft.Overflow = ToolBarOverflow.Wrap;
    this.toolBarLeft.Stretch = true;
    this.toolBarLeft.Tearable = false;
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "arrow_left_blue.ico");
    this.imagesToolbars.Images.SetKeyName(2, "arrow_all_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(3, "arrow_all_left_blue.ico");
    this.imagesToolbars.Images.SetKeyName(4, "arrow_up_blue.ico");
    this.imagesToolbars.Images.SetKeyName(5, "arrow_down_blue.ico");
    this.imagesToolbars.Images.SetKeyName(6, "");
    this.imagesToolbars.Images.SetKeyName(7, "");
    this.imagesToolbars.Images.SetKeyName(8, "");
    this.imagesToolbars.Images.SetKeyName(9, "");
    this.imagesToolbars.Images.SetKeyName(10, "");
    componentResourceManager.ApplyResources((object) this.btAdd, "btAdd");
    this.btAdd.ImageIndex = 0;
    this.btAdd.Click += new EventHandler(this.btAdd_Click);
    componentResourceManager.ApplyResources((object) this.btAddAll, "btAddAll");
    this.btAddAll.ImageIndex = 2;
    this.btAddAll.Click += new EventHandler(this.btAddAll_Click);
    this.btDelete.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btDelete, "btDelete");
    this.btDelete.ImageIndex = 1;
    this.btDelete.Click += new EventHandler(this.btDelete_Click);
    componentResourceManager.ApplyResources((object) this.btDeleteAll, "btDeleteAll");
    this.btDeleteAll.ImageIndex = 3;
    this.btDeleteAll.Click += new EventHandler(this.btDeleteAll_Click);
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
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.cbFind);
    this.panel1.Controls.Add((Control) this.comboSearch);
    this.panel1.Controls.Add((Control) this.labelSearch);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.cbFind.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.cbFind.AutoCompleteSource = AutoCompleteSource.ListItems;
    componentResourceManager.ApplyResources((object) this.cbFind, "cbFind");
    this.cbFind.DropDownStyle = ComboBoxStyle.Simple;
    this.cbFind.FormattingEnabled = true;
    this.cbFind.Name = "cbFind";
    this.cbFind.Sorted = true;
    this.toolTip.SetToolTip((Control) this.cbFind, componentResourceManager.GetString("cbFind.ToolTip"));
    this.cbFind.TextChanged += new EventHandler(this.cbFind_TextChanged);
    this.comboSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.comboSearch.AutoCompleteSource = AutoCompleteSource.ListItems;
    componentResourceManager.ApplyResources((object) this.comboSearch, "comboSearch");
    this.comboSearch.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboSearch.FormattingEnabled = true;
    this.comboSearch.Name = "comboSearch";
    this.comboSearch.Sorted = true;
    this.toolTip.SetToolTip((Control) this.comboSearch, componentResourceManager.GetString("comboSearch.ToolTip"));
    this.comboSearch.SelectedIndexChanged += new EventHandler(this.DoReloadAttrsList);
    componentResourceManager.ApplyResources((object) this.labelSearch, "labelSearch");
    this.labelSearch.Name = "labelSearch";
    componentResourceManager.ApplyResources((object) this.menuAvailable, "menuAvailable");
    this.menuAvailable.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuAvailable.Hidden = false;
    this.menuAvailable.ImageList = this.imagesToolbars;
    this.menuAvailable.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuAvailable
    });
    this.menuAvailable.Name = "menuAvailable";
    this.menuAvailable.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuAvailable, "contextMenuAvailable");
    this.contextMenuAvailable.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.mnpAdd,
      (ToolbarItemBase) this.mnpAddAll,
      (ToolbarItemBase) this.mnpCollapse,
      (ToolbarItemBase) this.mnpExpand,
      (ToolbarItemBase) this.mnpShowAll
    });
    this.contextMenuAvailable.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpAdd, "mnpAdd");
    this.mnpAdd.ImageIndex = 0;
    this.mnpAdd.ShowText = true;
    this.mnpAdd.Click += new EventHandler(this.btAdd_Click);
    componentResourceManager.ApplyResources((object) this.mnpAddAll, "mnpAddAll");
    this.mnpAddAll.ImageIndex = 2;
    this.mnpAddAll.ShowText = true;
    this.mnpAddAll.Click += new EventHandler(this.btAddAll_Click);
    this.mnpCollapse.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpCollapse, "mnpCollapse");
    this.mnpCollapse.ImageIndex = 8;
    this.mnpCollapse.ShowText = true;
    this.mnpCollapse.Click += new EventHandler(this.DoCollapse);
    componentResourceManager.ApplyResources((object) this.mnpExpand, "mnpExpand");
    this.mnpExpand.ImageIndex = 9;
    this.mnpExpand.ShowText = true;
    this.mnpExpand.Click += new EventHandler(this.DoExpand);
    this.mnpShowAll.AutoToggle = AutoToggleType.Single;
    this.mnpShowAll.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpShowAll, "mnpShowAll");
    this.mnpShowAll.ImageIndex = 10;
    this.mnpShowAll.ShowText = true;
    this.mnpShowAll.Click += new EventHandler(this.mnpShowAll_Click);
    this.panelRight.Controls.Add((Control) this.listVisible);
    this.panelRight.Controls.Add((Control) this.labelManyColumns);
    this.panelRight.Controls.Add((Control) this.labelWarning);
    this.panelRight.Controls.Add((Control) this.menuVisible);
    this.panelRight.Controls.Add((Control) this.toolBarRight);
    this.panelRight.Controls.Add((Control) this.panelTrack);
    componentResourceManager.ApplyResources((object) this.panelRight, "panelRight");
    this.panelRight.Name = "panelRight";
    this.listVisible.AllowDrop = true;
    this.listVisible.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnVisibleName
    });
    componentResourceManager.ApplyResources((object) this.listVisible, "listVisible");
    this.listVisible.FullRowSelect = true;
    this.listVisible.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.listVisible.HideSelection = false;
    this.listVisible.Name = "listVisible";
    this.menuVisible.SetPopupMenu((Control) this.listVisible, (MenuBarItem) this.contextMenuVisible);
    this.listVisible.ShowGroups = false;
    this.listVisible.UseCompatibleStateImageBehavior = false;
    this.listVisible.View = View.Details;
    this.listVisible.SelectedIndexChanged += new EventHandler(this.DoVisibleSelChanged);
    this.listVisible.DragDrop += new DragEventHandler(this.DoDragDrop_Visible);
    this.listVisible.DragEnter += new DragEventHandler(this.DoDragEnter_Visible);
    this.listVisible.DoubleClick += new EventHandler(this.btDelete_Click);
    this.listVisible.MouseDown += new MouseEventHandler(this.DoMouseDown_Visible);
    this.listVisible.MouseMove += new MouseEventHandler(this.DoMouseMove_Visible);
    this.listVisible.MouseUp += new MouseEventHandler(this.DoMouseUp_Visible);
    this.listVisible.Resize += new EventHandler(this.DoResizeColumnWidthVisible);
    componentResourceManager.ApplyResources((object) this.columnVisibleName, "columnVisibleName");
    this.labelManyColumns.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this.labelManyColumns, "labelManyColumns");
    this.labelManyColumns.ForeColor = Color.Maroon;
    this.labelManyColumns.Name = "labelManyColumns";
    this.toolTip.SetToolTip((Control) this.labelManyColumns, componentResourceManager.GetString("labelManyColumns.ToolTip"));
    this.labelWarning.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this.labelWarning, "labelWarning");
    this.labelWarning.ForeColor = Color.Maroon;
    this.labelWarning.Name = "labelWarning";
    this.toolTip.SetToolTip((Control) this.labelWarning, componentResourceManager.GetString("labelWarning.ToolTip"));
    componentResourceManager.ApplyResources((object) this.menuVisible, "menuVisible");
    this.menuVisible.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuVisible.Hidden = false;
    this.menuVisible.ImageList = this.imagesToolbars;
    this.menuVisible.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuVisible
    });
    this.menuVisible.Name = "menuVisible";
    this.menuVisible.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuVisible, "contextMenuVisible");
    this.contextMenuVisible.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpMoveTop,
      (ToolbarItemBase) this.mnpMoveUp,
      (ToolbarItemBase) this.mnpMoveDown,
      (ToolbarItemBase) this.mnpMoveBottom,
      (ToolbarItemBase) this.mnpDelete,
      (ToolbarItemBase) this.mnpDeleteAll
    });
    this.contextMenuVisible.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveTop, "mnpMoveTop");
    this.mnpMoveTop.ImageIndex = 6;
    this.mnpMoveTop.ShowText = true;
    this.mnpMoveTop.Click += new EventHandler(this.DoMoveTop);
    componentResourceManager.ApplyResources((object) this.mnpMoveUp, "mnpMoveUp");
    this.mnpMoveUp.ImageIndex = 4;
    this.mnpMoveUp.ShowText = true;
    this.mnpMoveUp.Click += new EventHandler(this.btMoveUp_Click);
    this.mnpMoveDown.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveDown, "mnpMoveDown");
    this.mnpMoveDown.ImageIndex = 5;
    this.mnpMoveDown.ShowText = true;
    this.mnpMoveDown.Click += new EventHandler(this.btMoveDown_Click);
    componentResourceManager.ApplyResources((object) this.mnpMoveBottom, "mnpMoveBottom");
    this.mnpMoveBottom.ImageIndex = 7;
    this.mnpMoveBottom.ShowText = true;
    this.mnpMoveBottom.Click += new EventHandler(this.DoMoveBottom);
    this.mnpDelete.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpDelete, "mnpDelete");
    this.mnpDelete.ImageIndex = 1;
    this.mnpDelete.ShowText = true;
    this.mnpDelete.Click += new EventHandler(this.btDelete_Click);
    componentResourceManager.ApplyResources((object) this.mnpDeleteAll, "mnpDeleteAll");
    this.mnpDeleteAll.ImageIndex = 3;
    this.mnpDeleteAll.ShowText = true;
    this.mnpDeleteAll.Click += new EventHandler(this.btDeleteAll_Click);
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
    this.toolBarRight.Items.AddRange(new ToolbarItemBase[4]
    {
      (ToolbarItemBase) this.btMoveTop,
      (ToolbarItemBase) this.btMoveUp,
      (ToolbarItemBase) this.btMoveDown,
      (ToolbarItemBase) this.btMoveBottom
    });
    this.toolBarRight.MinimumFloatingSize = new Size(250, 30);
    this.toolBarRight.Name = "toolBarRight";
    this.toolBarRight.Overflow = ToolBarOverflow.Wrap;
    this.toolBarRight.Stretch = true;
    this.toolBarRight.Tearable = false;
    componentResourceManager.ApplyResources((object) this.btMoveTop, "btMoveTop");
    this.btMoveTop.Image = (Image) componentResourceManager.GetObject("btMoveTop.Image");
    this.btMoveTop.Click += new EventHandler(this.DoMoveTop);
    componentResourceManager.ApplyResources((object) this.btMoveUp, "btMoveUp");
    this.btMoveUp.ImageIndex = 4;
    this.btMoveUp.Click += new EventHandler(this.btMoveUp_Click);
    this.btMoveDown.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btMoveDown, "btMoveDown");
    this.btMoveDown.ImageIndex = 5;
    this.btMoveDown.Click += new EventHandler(this.btMoveDown_Click);
    componentResourceManager.ApplyResources((object) this.btMoveBottom, "btMoveBottom");
    this.btMoveBottom.Image = (Image) componentResourceManager.GetObject("btMoveBottom.Image");
    this.btMoveBottom.Click += new EventHandler(this.DoMoveBottom);
    this.panelTrack.Controls.Add((Control) this.labelWidth);
    this.panelTrack.Controls.Add((Control) this.edWidth);
    componentResourceManager.ApplyResources((object) this.panelTrack, "panelTrack");
    this.panelTrack.Name = "panelTrack";
    componentResourceManager.ApplyResources((object) this.labelWidth, "labelWidth");
    this.labelWidth.Name = "labelWidth";
    componentResourceManager.ApplyResources((object) this.edWidth, "edWidth");
    this.edWidth.Maximum = new Decimal(new int[4]
    {
      10000,
      0,
      0,
      0
    });
    this.edWidth.Name = "edWidth";
    this.toolTip.SetToolTip((Control) this.edWidth, componentResourceManager.GetString("edWidth.ToolTip"));
    this.edWidth.ValueChanged += new EventHandler(this.DoSetColumnsWidth);
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this._applyParentNodeDisplaySettingsButton);
    this.panelBottom.Controls.Add((Control) this.btnDefault);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this._applyParentNodeDisplaySettingsButton, "_applyParentNodeDisplaySettingsButton");
    this._applyParentNodeDisplaySettingsButton.Cursor = Cursors.Default;
    this._applyParentNodeDisplaySettingsButton.Name = "_applyParentNodeDisplaySettingsButton";
    this.toolTip.SetToolTip((Control) this._applyParentNodeDisplaySettingsButton, componentResourceManager.GetString("_applyParentNodeDisplaySettingsButton.ToolTip"));
    this._applyParentNodeDisplaySettingsButton.Click += new EventHandler(this.ApplyParentNodeDisplaySettingsButton_Click);
    componentResourceManager.ApplyResources((object) this.btnDefault, "btnDefault");
    this.btnDefault.Cursor = Cursors.Default;
    this.btnDefault.Name = "btnDefault";
    this.toolTip.SetToolTip((Control) this.btnDefault, componentResourceManager.GetString("btnDefault.ToolTip"));
    this.btnDefault.Click += new EventHandler(this.DoResetColumns);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.images.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("images.ImageStream");
    this.images.TransparentColor = Color.Transparent;
    this.images.Images.SetKeyName(0, "");
    this.images.Images.SetKeyName(1, "");
    this.images.Images.SetKeyName(2, "object_16x16.ico");
    this.images.Images.SetKeyName(3, "link_16x16.ico");
    this.images.Images.SetKeyName(4, "sinfo_16.ico");
    this.images.Images.SetKeyName(5, "EventLog.ico");
    this.panelWarning.Controls.Add((Control) this.labelWarning2);
    this.panelWarning.Controls.Add((Control) this.pictureWarning);
    componentResourceManager.ApplyResources((object) this.panelWarning, "panelWarning");
    this.panelWarning.Name = "panelWarning";
    this.labelWarning2.BorderStyle = BorderStyle.FixedSingle;
    componentResourceManager.ApplyResources((object) this.labelWarning2, "labelWarning2");
    this.labelWarning2.ForeColor = Color.Maroon;
    this.labelWarning2.Name = "labelWarning2";
    componentResourceManager.ApplyResources((object) this.pictureWarning, "pictureWarning");
    this.pictureWarning.Name = "pictureWarning";
    this.pictureWarning.TabStop = false;
    this.timerReload.Interval = (int) byte.MaxValue;
    this.timerReload.Tick += new EventHandler(this.timerReload_Tick);
    this.AcceptButton = (IButtonControl) this.btnApply;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.panelMain);
    this.Controls.Add((Control) this.panelWarning);
    this.Controls.Add((Control) this.panelBottom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AppearanceTuningForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosing += new FormClosingEventHandler(this.AppearanceTuningForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.AppearanceTuningForm_FormClosed);
    this.Load += new EventHandler(this.AppearanceTuningForm_Load);
    this.panelMain.Panel1.ResumeLayout(false);
    this.panelMain.Panel2.ResumeLayout(false);
    this.panelMain.EndInit();
    this.panelMain.ResumeLayout(false);
    this.panelLeft.ResumeLayout(false);
    this.treeAvailable.EndInit();
    this.panel1.ResumeLayout(false);
    this.panelRight.ResumeLayout(false);
    this.panelTrack.ResumeLayout(false);
    this.edWidth.EndInit();
    this.panelBottom.ResumeLayout(false);
    this.panelWarning.ResumeLayout(false);
    ((ISupportInitialize) this.pictureWarning).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Константы для формы AppearanceTuningForm</summary>
  internal static class AppearanceTuningFormConsts
  {
    /// <summary>Заголовок формы - "Настройка отображения"</summary>
    internal static readonly string FormCaption = LocalizationHolder.rm.GetString("Client.Core_548");
    /// <summary>Ключ в коллекции настроек - сплиттер главной панели</summary>
    internal const int ccSplitterMain = 3001;
  }

  /// <summary>
  /// Вспомогательный класс, в котором хранится группа колонок или описание колонки
  /// </summary>
  public class TreeNodeSchemeItem : IComparable, IComparable<AppearanceTuningForm.TreeNodeSchemeItem>
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
      return obj is AppearanceTuningForm.TreeNodeSchemeItem treeNodeSchemeItem && this.Scheme != null && this.Guid == treeNodeSchemeItem.Guid;
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
      return this.CompareTo(obj as AppearanceTuningForm.TreeNodeSchemeItem);
    }

    /// <summary>Сравнить с указанным объектом</summary>
    /// <param name="other">Объект для сравнения</param>
    /// <returns>-1, 0, 1</returns>
    public int CompareTo(AppearanceTuningForm.TreeNodeSchemeItem other)
    {
      return other == null || other.Scheme == null || this.Scheme == null ? 1 : this.Scheme.Name.CompareTo(other.Scheme.Name);
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
    private AppearanceTuningForm owner;
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
    public iAttrContextSearchManager(AppearanceTuningForm owner, Intermech.VirtualTreeView.VirtualTreeView tree)
    {
      this.owner = tree != null && owner != null ? owner : throw new ArgumentNullException(LocalizationHolder.rm.GetString("Client.Core_553"));
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
      try
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
            str2 = ((AppearanceTuningForm.TreeNodeSchemeItem) row1.Item).Scheme.Name.ToUpper().Trim();
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
      finally
      {
        this.owner.UpdateControls();
      }
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
