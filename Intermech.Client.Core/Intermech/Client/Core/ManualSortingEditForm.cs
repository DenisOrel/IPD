
// Type: Intermech.Client.Core.ManualSortingEditForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Форма для редактирования значений атрибута "Сортировка" - ручная сортировка в связях
/// </summary>
public class ManualSortingEditForm : Form, IIODestination
{
  /// <summary>Идентификатор атрибута "Сортировка"</summary>
  private static int _sortingAttrID = -1;
  /// <summary>
  /// Форма сама определяет, в каком режиме она работает (по правам доступа к атрибуту "Сортировка")
  /// </summary>
  private List<ManualSortingEditorMode> FEditorModes = new List<ManualSortingEditorMode>();
  /// <summary>Диспетчер событий</summary>
  private IIODispatcher _IODispatcher = (IIODispatcher) new IODispatcher();
  private List<IDescriptor> _rootDescriptors = new List<IDescriptor>();
  /// <summary>Кэш графических элементов "Навигатора"</summary>
  private INavGraphicsCache _navGraphicsCache;
  /// <summary>Текущие настройки отображения и сортировки составов</summary>
  private ICurrentUserAndRole _userRole;
  /// <summary>
  /// Идентификаторы типов связи, поддерживающих работу с атрибутом "Сортировка"
  /// </summary>
  private List<int> _relationTypeIDs = new List<int>();
  /// <summary>
  /// Список дополнительных атрибутов, которые будут загружаться в узлы состава
  /// </summary>
  private List<int> _advAttributes = new List<int>();
  /// <summary>Пакеты атрибутов связей состава</summary>
  private List<RelationAttributesPackage> _relAttrs = new List<RelationAttributesPackage>();
  /// <summary>Список закладок с составами</summary>
  private List<AdvRelationsView> parentCompositions = new List<AdvRelationsView>();
  /// <summary>Коллекция выделенных элементов</summary>
  private ISelectedItems FSelectedItems;
  /// <summary>Контейнер сервисов</summary>
  internal AdvancedServiceContainer FViewServices;
  /// <summary>Коллекция разных настроек контролов формы</summary>
  private HybridDictionary FControlsSettings = new HybridDictionary(0, true);
  /// <summary>Описание родительского объекта</summary>
  private MyFullObjectElement FParentItem = new MyFullObjectElement();
  /// <summary>Коллекция для хранения ID изменённых связей</summary>
  private List<long> FChRels = new List<long>(0);
  /// <summary>Выполняется ли загрузка информации в контролы</summary>
  private bool FIsLoading;
  /// <summary>Если флажок установлен, то с формой работать нельзя</summary>
  private bool FError;
  /// <summary>Были ли изменения в форме</summary>
  private bool FIsChanged;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelComposition;
  private ImageList imagesToolbars;
  private ImageList imagesState;
  private MenuBar menuComposition;
  private ContextMenuBarItem contextMenuComposition;
  private MenuButtonItem mnpMoveUp;
  private MenuButtonItem mnpMoveDown;
  private MenuButtonItem mnpCustomize;
  private MenuButtonItem mnpMoveTop;
  private MenuButtonItem mnpMoveBottom;
  private Intermech.Bars.ToolBar toolBarRight;
  private ButtonItem btMoveUp;
  private ButtonItem btMoveDown;
  private ButtonItem btMoveTop;
  private ButtonItem btMoveBottom;
  private Panel panelBottom;
  private Button btnCancel;
  private Button btnApply;
  private TabControl pages;
  private ButtonItem btAutoSortComposition;
  private MenuButtonItem mnpAutoSortComposition;
  private Label labelWarning;
  private Label labelPicture;

  /// <summary>Создать экземпляр формы</summary>
  /// <param name="FormCaption">Заголовок формы</param>
  /// <param name="selectedItems">Коллекция выделенных элементов, для которых вызвана форма</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  public ManualSortingEditForm(
    string FormCaption,
    ISelectedItems selectedItems,
    System.IServiceProvider viewServices)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 728);
    this.Init(FormCaption, selectedItems, viewServices);
  }

  /// <summary>Выполнить инициализацию формы</summary>
  /// <param name="FormCaption">Заголовок формы</param>
  /// <param name="selectedItems">Коллекция выделенных элементов, для которых вызвана форма</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  protected virtual void Init(
    string FormCaption,
    ISelectedItems selectedItems,
    System.IServiceProvider viewServices)
  {
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.FSelectedItems = selectedItems;
    this.FViewServices = new AdvancedServiceContainer(viewServices);
    this._IODispatcher.RegisterDestination((IIODestination) this);
    this.FViewServices.AddService(typeof (IIODispatcher), (object) this._IODispatcher);
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    int width1 = workingArea.Width;
    Size size = this.Size;
    int width2 = size.Width;
    int x = (width1 - width2) / 2;
    int height1 = workingArea.Height;
    size = this.Size;
    int height2 = size.Height;
    int y = (height1 - height2) / 2;
    this.Location = new Point(x, y);
    FormStorage.LoadLayout((Control) this);
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._userRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    ManualSortingEditForm._sortingAttrID = MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545");
    this._advAttributes.Add(ManualSortingEditForm._sortingAttrID);
    this._advAttributes.Add(-7);
    this.mnpCustomize.Image = Holder.NamedImageList != null ? Holder.NamedImageList.ImageList.Images[Holder.NamedImageList.ImageIndex("imgViewSettings")] : this.mnpCustomize.Image;
    this.FIsChanged = false;
    this.FError = !this.LoadFormData(selectedItems, viewServices);
    if (this.FError)
      this.Clear();
    this.UpdateControls();
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="FormCaption">Заголовок формы</param>
  /// <param name="selectedItems">Коллекция выделенных элементов, для которых вызвана форма</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <param name="ChRels">Массив изменённых связей</param>
  /// <returns>Результ вызова формы</returns>
  public static DialogResult Execute(
    string FormCaption,
    ISelectedItems selectedItems,
    System.IServiceProvider viewServices,
    out long[] ChRels)
  {
    using (ManualSortingEditForm manualSortingEditForm = new ManualSortingEditForm(FormCaption, selectedItems, viewServices))
    {
      ChRels = new long[0];
      if (manualSortingEditForm.FError)
        return DialogResult.Abort;
      int num = (int) manualSortingEditForm.ShowDialog();
      ChRels = manualSortingEditForm.FChRels.ToArray();
      return (DialogResult) num;
    }
  }

  /// <summary>Очистка внутренних структур</summary>
  internal void Clear()
  {
    if (this.FParentItem != null)
      this.FParentItem.Clear();
    for (int index = 0; index < this.parentCompositions.Count; ++index)
      this.parentCompositions[index].Deactivate((IView) null);
    this._relationTypeIDs.Clear();
    this.parentCompositions.Clear();
    this._rootDescriptors.Clear();
    this.pages.TabPages.Clear();
    this._relAttrs.Clear();
  }

  /// <summary>
  /// Отыскать в текущем описании типа связи наиболее подходящий родительский тип объекта
  /// </summary>
  /// <param name="relType">Допустимый тип связи</param>
  /// <param name="childObjType">Дочерний тип объекта</param>
  /// <returns>Наиболее подходящий родительский тип объекта или дочерний тип</returns>
  public static int GetBaseParentObjectType(ChildRelationType relType, int childObjType)
  {
    if (relType == null || childObjType < 0 || relType[childObjType] != null)
      return childObjType;
    for (int index = 0; index < relType.ChildObjectTypes.Count; ++index)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(childObjType, relType.ChildObjectTypes[index].ObjectTypeID))
        return relType.ChildObjectTypes[index].ObjectTypeID;
    }
    return childObjType;
  }

  /// <summary>
  /// Считать хоть-что нибудь из коллекции по указанному ключу
  /// </summary>
  /// <param name="collection">Коллекция настроек</param>
  /// <param name="key">Ключ</param>
  /// <param name="defaultValue">Значение по умолчанию</param>
  /// <returns>Что-нибудь да и вернёт</returns>
  public static object GetDicValue(HybridDictionary collection, object key, object defaultValue)
  {
    return collection == null || key == null ? defaultValue : collection[key] ?? defaultValue;
  }

  /// <summary>
  /// Извлечь из коллекции выделенных элементов идентификаторы типов связей, которые поддерживают работу с атрибутом "Сортировка"
  /// </summary>
  /// <param name="selectedItems">Коллекция выделенных элементов</param>
  /// <returns>Список идентификаторов типов связей, которые поддерживают работу с атрибутом "Сортировка"</returns>
  public static List<int> ExtractSortedRelationTypesID(ISelectedItems selectedItems)
  {
    List<int> sortedRelationTypesId = new List<int>();
    if (selectedItems == null || selectedItems.Count == 0)
      return sortedRelationTypesId;
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      if (selectedItems.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData && MetaDataHelper.HasRelationTypeSorting(itemData.RelationType) && !sortedRelationTypesId.Contains(itemData.RelationType))
        sortedRelationTypesId.Add(itemData.RelationType);
    }
    return sortedRelationTypesId;
  }

  /// <summary>
  /// Найти индекс первого элемента со связью, которая поддерживает работу с ручной сортировкой
  /// </summary>
  /// <param name="selectedItems">Коллекция выделенных элементов</param>
  /// <returns>Индекс первого элемента со связью, которая поддерживает работу с ручной сортировкой или -1</returns>
  public static int FindFirstSortingRelationItem(ISelectedItems selectedItems)
  {
    if (selectedItems == null || selectedItems.Count == 0)
      return -1;
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      if (selectedItems.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData && MetaDataHelper.HasRelationTypeSorting(itemData.RelationType))
        return index;
    }
    return -1;
  }

  /// <summary>
  /// Найти индекс первого элемента с объектом, состав которого поддерживает работу с ручной сортировкой
  /// </summary>
  /// <param name="selectedItems">Коллекция выделенных элементов</param>
  /// <returns>Индекс первого элемента первого элемента с объектом, состав которого поддерживает работу с ручной сортировкой, или -1</returns>
  public static int FindFirstSortingObjectItem(ISelectedItems selectedItems)
  {
    if (selectedItems == null || selectedItems.Count != 1)
      return -1;
    for (int index = 0; index < selectedItems.Count; ++index)
    {
      if (selectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && MetaDataHelper.HasObjectTypeSortingRelTypes(itemData.ObjectType))
        return index;
    }
    return -1;
  }

  /// <summary>Сохраним настройки формы в настройках пользователя</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ManualSortingEditForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Восстановим настройки формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ManualSortingEditForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// Выполнить проверку выделенных узлов, определить минимальный и максимальный ID узлов
  /// </summary>
  /// <param name="nodes">Список выделенных описаний узлов</param>
  /// <param name="MinPos">Минимальная позиция у выделенных узлов</param>
  /// <param name="MaxPos">Максимальная позиция у выделенных узлов</param>
  private void CheckSelection(Dictionary<INodeID, int> nodes, out int MinPos, out int MaxPos)
  {
    MinPos = -1;
    MaxPos = -1;
    if (nodes == null || nodes.Count == 0)
      return;
    int num1 = 0;
    foreach (KeyValuePair<INodeID, int> node in nodes)
    {
      int num2 = node.Value;
      ++num1;
      if (num1 == 1)
      {
        MinPos = MaxPos = num2;
      }
      else
      {
        if (num2 > MaxPos)
          MaxPos = num2;
        if (num2 < MinPos)
          MinPos = num2;
      }
    }
  }

  /// <summary>Обновить контролы</summary>
  private void UpdateControls()
  {
    if (this.FParentItem == null)
      return;
    ManualSortingEditorMode sortingEditorMode = this.pages.SelectedIndex < 0 || this.pages.SelectedIndex > this.FEditorModes.Count - 1 ? ManualSortingEditorMode.mseReadOnly : this.FEditorModes[this.pages.SelectedIndex];
    this.btnApply.Enabled = true;
    this.btnApply.Visible = this.btnApply.Enabled;
    this.btnApply.Text = this.FIsChanged ? LocalizationHolder.rm.GetString("Client.Core_218") : LocalizationHolder.rm.GetString("Client.Core_217");
    this.btnCancel.Enabled = this.FIsChanged;
    Dictionary<INodeID, int> selectedPositions = this.pages.SelectedIndex < 0 || this.parentCompositions.Count <= 0 ? (Dictionary<INodeID, int>) null : this.parentCompositions[this.pages.SelectedIndex].SelectedPositions;
    int MinPos;
    int MaxPos;
    this.CheckSelection(selectedPositions, out MinPos, out MaxPos);
    int itemsCount = this.pages.SelectedIndex < 0 || this.parentCompositions.Count <= 0 ? 0 : this.parentCompositions[this.pages.SelectedIndex].ItemsCount;
    int num = selectedPositions != null ? (selectedPositions.Count > 0 ? 1 : 0) : 0;
    bool flag1 = num != 0 && sortingEditorMode == ManualSortingEditorMode.mseAdminMode && itemsCount > 0 && MinPos > 0;
    bool flag2 = num != 0 && sortingEditorMode == ManualSortingEditorMode.mseAdminMode && itemsCount > 0 && MaxPos < itemsCount - 1;
    bool flag3 = this._userRole.Rule.IndexOfParentObjectType(this.FParentItem.ObjectType, true) >= 0 && sortingEditorMode == ManualSortingEditorMode.mseAdminMode && itemsCount > 0 && this.pages.SelectedIndex >= 0 && this.parentCompositions[this.pages.SelectedIndex].ItemsCount > 1;
    this.btMoveUp.Enabled = flag1;
    this.mnpMoveUp.Enabled = flag1;
    this.btMoveTop.Enabled = flag1;
    this.mnpMoveTop.Enabled = flag1;
    this.btMoveDown.Enabled = flag2;
    this.mnpMoveDown.Enabled = flag2;
    this.btMoveBottom.Enabled = flag2;
    this.mnpMoveBottom.Enabled = flag2;
    this.btAutoSortComposition.Enabled = flag3;
    this.mnpAutoSortComposition.Enabled = this.btAutoSortComposition.Enabled;
    this.mnpCustomize.Enabled = true;
    this.labelWarning.Visible = sortingEditorMode == ManualSortingEditorMode.mseReadOnly;
    this.labelPicture.Visible = sortingEditorMode == ManualSortingEditorMode.mseReadOnly;
  }

  /// <summary>Загрузить данные в форму</summary>
  /// <param name="selectedItems">Коллекция выделенных элементов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <returns>true, если загрузка прошла успешно</returns>
  internal bool LoadFormData(ISelectedItems selectedItems, System.IServiceProvider viewServices)
  {
    this.Clear();
    this.FIsChanged = false;
    int sortingObjectItem = ManualSortingEditForm.FindFirstSortingObjectItem(selectedItems);
    try
    {
      this.FIsLoading = true;
      if (sortingObjectItem < 0 || this.FSelectedItems == null || this.FSelectedItems.Count <= 0)
        return false;
      ICategoryTypeIconService service = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
      this.pages.ImageList = service.ImageList;
      int num1 = 0;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      IDBTypedObjectID itemData = this.FSelectedItems.GetItemData(sortingObjectItem, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._relationTypeIDs = this._userRole.Rule.GetObjectTypeVisibleRelations(itemData.ObjectType, true);
        for (int index = 0; index < this._relationTypeIDs.Count; ++index)
        {
          if (!MetaDataHelper.HasRelationTypeSorting(this._relationTypeIDs[index]))
            this._relationTypeIDs.RemoveAt(index);
        }
        if (this._relationTypeIDs.Count == 0)
        {
          num1 = 1;
          empty1 = LocalizationHolder.rm.GetString("Client.Core_619");
          empty2 = LocalizationHolder.rm.GetString("Client.Core_132");
        }
        int relationTypeId = MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
        if (num1 == 0)
        {
          if (this._relationTypeIDs.IndexOf(relationTypeId) >= 0)
          {
            IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeId);
            relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
            DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(-21, RelationalOperators.Equal, (object) itemData.ObjectID, LogicalOperators.NONE, 0, true)
            }, new ColumnDescriptor[1]
            {
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
            }, recordCount: 1);
            DataTable dataTable = relationCollection.Select(paramSet);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
              num1 = 2;
              empty1 = LocalizationHolder.rm.GetString("ManualSortingForm.SpecificationWarning");
              empty2 = LocalizationHolder.rm.GetString("Client.Core_132");
            }
            dataTable?.Dispose();
          }
        }
      }
      if (num1 != 0)
      {
        int num2 = (int) MessageBox.Show(empty1, empty2, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return false;
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
        this.FParentItem = dbObject != null ? new MyFullObjectElement(dbObject.ID, dbObject.ObjectID, dbObject.ObjectType, 0L, -1, dbObject.Caption, false, dbObject.GUID, dbObject.OwnerID, 0L, dbObject.LCStep, (long) dbObject.VersionID, Convert.ToInt64(dbObject.IsBaseVersion), Array.Empty<object>()) : (MyFullObjectElement) null;
        int num3 = 0;
        if (this.FParentItem != null)
        {
          for (int index = 0; index < this._relationTypeIDs.Count; ++index)
          {
            AdvRelationsDescriptor rootDescriptor = new AdvRelationsDescriptor(Intermech.Navigator.Consts.CategoryAdvRelationsNode, 0, "cad001e2-306c-11d8-b4e9-00304f19f545", (List<long>) null, this.FParentItem.ObjectID, this.FParentItem.ObjectType, this._relationTypeIDs[index], string.Empty, 0L, this.FParentItem.Owner, this.FParentItem.Sorting, this.FParentItem.LCStepID, this._advAttributes, this.FParentItem.Version, this.FParentItem.BaseVersion);
            AdvRelationsView advRelationsView = new AdvRelationsView();
            advRelationsView.DisableHeaderContextMenu = true;
            advRelationsView.BackColor = SystemColors.Window;
            advRelationsView.StateStreamPrefix = $"RelTypeID.{this._relationTypeIDs[index].ToString()}.";
            advRelationsView.BlockUISettingsDisableChildrenViewGrouping = true;
            advRelationsView.Initialize((IDescriptor) rootDescriptor, (System.IServiceProvider) this.FViewServices);
            advRelationsView.Activate((IView) null);
            if (advRelationsView.ItemsCount == 0)
            {
              advRelationsView.Deactivate((IView) null);
            }
            else
            {
              TabPage tabPage = new TabPage();
              tabPage.Location = new Point(0, 0);
              tabPage.ImageIndex = service.IndexOf(6, this._relationTypeIDs[index]);
              tabPage.Name = "TabPage" + num3.ToString();
              tabPage.Padding = new Padding(3);
              tabPage.Size = new Size((int) byte.MaxValue, (int) byte.MaxValue);
              tabPage.Text = MetaDataHelper.GetRelationTypeName(this._relationTypeIDs[index]);
              tabPage.UseVisualStyleBackColor = true;
              tabPage.Controls.Add((Control) advRelationsView);
              advRelationsView.Control = (object) advRelationsView;
              advRelationsView.DisableColumnsGrouping = true;
              advRelationsView.DisableDelayedUpdates = true;
              advRelationsView._disableSaveGroupBox = true;
              advRelationsView.DisableGroupBox = true;
              advRelationsView.DisableIMContextMenu = true;
              advRelationsView.DisableKeyDownEvents = false;
              advRelationsView.DisableStatusBar = true;
              advRelationsView.DisableToolBar = true;
              advRelationsView.Dock = DockStyle.Fill;
              advRelationsView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
              advRelationsView.Location = new Point(0, 0);
              advRelationsView.Name = "Composition" + num3.ToString();
              advRelationsView.Size = new Size((int) byte.MaxValue, (int) byte.MaxValue);
              advRelationsView.SelectedItemsChanged += new EventHandler(this.parentComposition_SelectedItemsChanged);
              advRelationsView.SortingGroupingChanged += new EventHandler(this.SortingGroupingChanged);
              advRelationsView.ShowCustomContextMenu += new EventHandler<ContextMenuEventArgs>(this.parentComposition_ShowCustomContextMenu);
              this.parentCompositions.Add(advRelationsView);
              this._relAttrs.Add(advRelationsView.RelationsAttributes);
              this._rootDescriptors.Add((IDescriptor) rootDescriptor);
              this.pages.Controls.Add((Control) tabPage);
              advRelationsView.DisableColumnsSorting = true;
              this.NumerateSortAttribute((List<long>) null, num3, 0L);
              if (this.parentCompositions[num3].ItemsCount > 0)
              {
                long relationID = this.parentCompositions[num3][0] is AdvRelationsNodeID advRelationsNodeId ? advRelationsNodeId.PrjLinkID : -1L;
                if (relationID != 0L)
                {
                  IDBAttribute relationAttributeById = sessionKeeper.Session.GetRelationAttributeByID(relationID, ManualSortingEditForm._sortingAttrID);
                  this.FEditorModes.Add(relationAttributeById == null || relationAttributeById.ReadOnly ? ManualSortingEditorMode.mseReadOnly : ManualSortingEditorMode.mseAdminMode);
                  advRelationsView.DisableColumnsSorting = this.FEditorModes[num3] != ManualSortingEditorMode.mseAdminMode || advRelationsView.ItemsCount <= 1;
                }
              }
              ++num3;
            }
          }
        }
        if (this.parentCompositions.Count > 0)
        {
          this.pages.SelectedIndex = 0;
          this.Text = $"{LocalizationHolder.rm.GetString("Client.Core_620")}{this.FParentItem.Caption}\"";
        }
      }
      if (this.parentCompositions.Count == 0)
      {
        int num4 = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_4220.ssp_imclient_4221()), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return false;
      }
    }
    finally
    {
      this.FIsLoading = false;
      this.UpdateControls();
    }
    return true;
  }

  /// <summary>Сохранить изменения в составе в базу данных</summary>
  private void StoreComposition()
  {
    if (this.FError || this.FIsLoading || !this.FIsChanged)
      return;
    if (this._relAttrs.Count == 0)
      return;
    try
    {
      this.FIsLoading = true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IRelationAttributesPackageWriter customService = sessionKeeper.Session.GetCustomService(typeof (IRelationAttributesPackageWriter)) as IRelationAttributesPackageWriter;
        for (int index1 = 0; index1 < this.parentCompositions.Count; ++index1)
        {
          if (this.FEditorModes[index1] == ManualSortingEditorMode.mseAdminMode)
          {
            List<long> chRels;
            customService.WriteRelationAttributesPackage(sessionKeeper.Session.SessionGUID, this._relAttrs[index1], out chRels);
            if (chRels != null)
            {
              for (int index2 = 0; index2 < chRels.Count; ++index2)
              {
                if (!this.FChRels.Contains(chRels[index2]))
                  this.FChRels.Add(chRels[index2]);
              }
            }
          }
        }
        this.FChRels.Add(this.FParentItem.ObjectID);
      }
      this.FIsChanged = false;
    }
    finally
    {
      this.FIsLoading = false;
      this.UpdateControls();
    }
  }

  /// <summary>Перенумеровать атрибут "Сортировка" у связей</summary>
  /// <param name="rels">Список связей или null</param>
  /// <param name="pageIndex">Номер странички</param>
  /// <param name="startValue">Стартовое значение атрибута "Сортировка"</param>
  protected virtual void NumerateSortAttribute(List<long> rels, int pageIndex, long startValue)
  {
    if (this._relAttrs[pageIndex] == null || this.parentCompositions[pageIndex] == null || this.parentCompositions[pageIndex].ItemsCount == 0)
      return;
    rels = rels == null ? this.parentCompositions[pageIndex].Relations : rels;
    AdvRelationsView parentComposition = this.parentCompositions[pageIndex];
    long num1 = startValue;
    long num2 = 1000000;
    for (int index = 0; index < rels.Count; ++index)
    {
      long rel = rels[index];
      AdvRelationsNodeID advRelationsNodeId = parentComposition[rel];
      this._relAttrs[pageIndex][rel, ManualSortingEditForm._sortingAttrID] = (object) num1;
      advRelationsNodeId.Sorting = num1;
      advRelationsNodeId[ManualSortingEditForm._sortingAttrID] = (object) num1;
      num1 += num2;
    }
  }

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    if (this.FError || !this.FIsChanged)
    {
      this.DialogResult = DialogResult.Cancel;
    }
    else
    {
      this.StoreComposition();
      this.DialogResult = !this.FIsChanged ? DialogResult.OK : DialogResult.None;
    }
  }

  /// <summary>
  /// Получить отсортированный по новому значению "Сортировка" список связей
  /// </summary>
  /// <param name="pageIndex">Индекс странички с составом</param>
  /// <returns>Отсортированный по новому значению "Сортировка" список связей</returns>
  protected virtual List<long> GetSortedRelations(int pageIndex)
  {
    List<long> sortedRelations = new List<long>();
    SortedDictionary<long, long> sortedDictionary = new SortedDictionary<long, long>();
    List<long> relations = this.parentCompositions[pageIndex].Relations;
    for (int index = 0; index < relations.Count; ++index)
      sortedDictionary.Add(Convert.ToInt64(this._relAttrs[pageIndex][relations[index], ManualSortingEditForm._sortingAttrID]), relations[index]);
    foreach (KeyValuePair<long, long> keyValuePair in sortedDictionary)
      sortedRelations.Add(keyValuePair.Value);
    return sortedRelations;
  }

  /// <summary>Переместить выделенные связи вверх в составе</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoMoveUp(object sender, EventArgs e)
  {
    ManualSortingEditorMode sortingEditorMode = this.pages.SelectedIndex >= 0 ? this.FEditorModes[this.pages.SelectedIndex] : ManualSortingEditorMode.mseReadOnly;
    if (sortingEditorMode != ManualSortingEditorMode.mseAdminMode)
      return;
    Dictionary<INodeID, int> selectedPositions = this.parentCompositions[this.pages.SelectedIndex].SelectedPositions;
    int MinPos;
    this.CheckSelection(selectedPositions, out MinPos, out int _);
    int itemsCount = this.parentCompositions[this.pages.SelectedIndex].ItemsCount;
    if ((selectedPositions.Count <= 0 || sortingEditorMode != ManualSortingEditorMode.mseAdminMode || itemsCount <= 0 ? 0 : (MinPos > 0 ? 1 : 0)) == 0)
      return;
    List<long> sortedRelations = this.GetSortedRelations(this.pages.SelectedIndex);
    List<long> relationsFromComposition = this.parentCompositions[this.pages.SelectedIndex].SelectedRelationsFromComposition;
    try
    {
      for (int index = 1; index < sortedRelations.Count; ++index)
      {
        long PrjLinkID1 = sortedRelations[index];
        long PrjLinkID2 = sortedRelations[index - 1];
        if (relationsFromComposition.Contains(PrjLinkID1))
        {
          this.parentCompositions[this.pages.SelectedIndex].GridSwapNodes((INodeID) this.parentCompositions[this.pages.SelectedIndex][PrjLinkID1], (INodeID) this.parentCompositions[this.pages.SelectedIndex][PrjLinkID2]);
          sortedRelations[index - 1] = PrjLinkID1;
          sortedRelations[index] = PrjLinkID2;
          this.FIsChanged = true;
        }
      }
    }
    finally
    {
      this.NumerateSortAttribute(sortedRelations, this.pages.SelectedIndex, 0L);
      if (e != null)
        this.UpdateControls();
    }
  }

  /// <summary>Переместить выделенные связи вниз в составе</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoMoveDown(object sender, EventArgs e)
  {
    ManualSortingEditorMode sortingEditorMode = this.pages.SelectedIndex >= 0 ? this.FEditorModes[this.pages.SelectedIndex] : ManualSortingEditorMode.mseReadOnly;
    if (sortingEditorMode != ManualSortingEditorMode.mseAdminMode)
      return;
    Dictionary<INodeID, int> selectedPositions = this.parentCompositions[this.pages.SelectedIndex].SelectedPositions;
    int MaxPos;
    this.CheckSelection(selectedPositions, out int _, out MaxPos);
    int itemsCount = this.parentCompositions[this.pages.SelectedIndex].ItemsCount;
    if ((selectedPositions.Count <= 0 || sortingEditorMode != ManualSortingEditorMode.mseAdminMode || itemsCount <= 0 ? 0 : (MaxPos < itemsCount - 1 ? 1 : 0)) == 0)
      return;
    List<long> sortedRelations = this.GetSortedRelations(this.pages.SelectedIndex);
    List<long> relationsFromComposition = this.parentCompositions[this.pages.SelectedIndex].SelectedRelationsFromComposition;
    try
    {
      for (int index = sortedRelations.Count - 2; index >= 0; --index)
      {
        long PrjLinkID1 = sortedRelations[index];
        long PrjLinkID2 = sortedRelations[index + 1];
        if (relationsFromComposition.Contains(PrjLinkID1))
        {
          this.parentCompositions[this.pages.SelectedIndex].GridSwapNodes((INodeID) this.parentCompositions[this.pages.SelectedIndex][PrjLinkID1], (INodeID) this.parentCompositions[this.pages.SelectedIndex][PrjLinkID2]);
          sortedRelations[index + 1] = PrjLinkID1;
          sortedRelations[index] = PrjLinkID2;
          this.FIsChanged = true;
        }
      }
    }
    finally
    {
      this.NumerateSortAttribute(sortedRelations, this.pages.SelectedIndex, 0L);
      if (e != null)
        this.UpdateControls();
    }
  }

  /// <summary>Переместить выделенные связи к началу списка</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoMoveTop(object sender, EventArgs e)
  {
    ManualSortingEditorMode sortingEditorMode = this.pages.SelectedIndex >= 0 ? this.FEditorModes[this.pages.SelectedIndex] : ManualSortingEditorMode.mseReadOnly;
    if (sortingEditorMode != ManualSortingEditorMode.mseAdminMode)
      return;
    Dictionary<INodeID, int> selectedPositions = this.parentCompositions[this.pages.SelectedIndex].SelectedPositions;
    int MinPos;
    this.CheckSelection(selectedPositions, out MinPos, out int _);
    int itemsCount = this.parentCompositions[this.pages.SelectedIndex].ItemsCount;
    if ((selectedPositions.Count <= 0 || sortingEditorMode != ManualSortingEditorMode.mseAdminMode || itemsCount <= 0 ? 0 : (MinPos > 0 ? 1 : 0)) == 0)
      return;
    List<long> sortedRelations = this.GetSortedRelations(this.pages.SelectedIndex);
    List<long> relationsFromComposition = this.parentCompositions[this.pages.SelectedIndex].SelectedRelationsFromComposition;
    int num1 = 0;
    try
    {
      this.parentCompositions[this.pages.SelectedIndex].GridBeginUpdate();
      for (int index = 1; index < sortedRelations.Count; ++index)
      {
        long num2 = sortedRelations[index];
        if (relationsFromComposition.Contains(num2) && num1 == 0)
        {
          num1 = index;
          break;
        }
      }
      for (int index = 0; index < num1; ++index)
        this.DoMoveUp((object) this, (EventArgs) null);
    }
    finally
    {
      this.parentCompositions[this.pages.SelectedIndex].GridEndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>Переместить выделенные связи в конец списка</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoMoveBottom(object sender, EventArgs e)
  {
    ManualSortingEditorMode sortingEditorMode = this.pages.SelectedIndex >= 0 ? this.FEditorModes[this.pages.SelectedIndex] : ManualSortingEditorMode.mseReadOnly;
    if (sortingEditorMode != ManualSortingEditorMode.mseAdminMode)
      return;
    Dictionary<INodeID, int> selectedPositions = this.parentCompositions[this.pages.SelectedIndex].SelectedPositions;
    int MaxPos;
    this.CheckSelection(selectedPositions, out int _, out MaxPos);
    int itemsCount = this.parentCompositions[this.pages.SelectedIndex].ItemsCount;
    if ((selectedPositions.Count <= 0 || sortingEditorMode != ManualSortingEditorMode.mseAdminMode || itemsCount <= 0 ? 0 : (MaxPos < itemsCount - 1 ? 1 : 0)) == 0)
      return;
    List<long> sortedRelations = this.GetSortedRelations(this.pages.SelectedIndex);
    List<long> relationsFromComposition = this.parentCompositions[this.pages.SelectedIndex].SelectedRelationsFromComposition;
    int num1 = 0;
    try
    {
      this.parentCompositions[this.pages.SelectedIndex].GridBeginUpdate();
      for (int index = sortedRelations.Count - 2; index >= 0; --index)
      {
        long num2 = sortedRelations[index];
        if (relationsFromComposition.Contains(num2) && num1 == 0)
        {
          num1 = index;
          break;
        }
      }
      for (int index = 0; index < itemsCount - num1; ++index)
        this.DoMoveDown((object) this, (EventArgs) null);
    }
    finally
    {
      this.parentCompositions[this.pages.SelectedIndex].GridEndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>Вызвать диалог "Настройка отображения"</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoCustomizeView(object sender, EventArgs e)
  {
    int selectedIndex = this.pages.SelectedIndex;
    if (selectedIndex < 0)
      return;
    List<long> relations = this.parentCompositions[this.pages.SelectedIndex].Relations;
    this.parentCompositions[this.pages.SelectedIndex].SetColumnsCommand(this.FSelectedItems, (System.IServiceProvider) this.FViewServices, (object) null);
    bool fisChanged = this.FIsChanged;
    try
    {
      this.LoadFormData(this.FSelectedItems, (System.IServiceProvider) this.FViewServices);
    }
    finally
    {
      this.FIsChanged = fisChanged;
    }
    if (this.FIsChanged)
    {
      for (int index = 0; index < relations.Count; ++index)
      {
        AdvRelationsNodeID node = this.parentCompositions[this.pages.SelectedIndex][relations[index]];
        if (node != null)
          this.parentCompositions[this.pages.SelectedIndex].GridSetNodeIndex((INodeID) node, index);
      }
      this.NumerateSortAttribute(relations, this.pages.SelectedIndex, 0L);
      this.UpdateControls();
    }
    if (selectedIndex >= this.parentCompositions.Count)
      return;
    this.pages.SelectedIndex = selectedIndex;
  }

  /// <summary>Показать контекстное меню в составе</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void parentComposition_ShowCustomContextMenu(object sender, ContextMenuEventArgs e)
  {
    this.UpdateControls();
    this.contextMenuComposition.Show(e.Control, e.Location);
  }

  /// <summary>Изменились выделенные строки в составе</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void parentComposition_SelectedItemsChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>Список поддерживаемых событий</summary>
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyDown;
    set
    {
    }
  }

  /// <summary>Обработать события</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если событие обработано</returns>
  public bool ProcessEvent(IIOEvent Event)
  {
    if (Event == null || Event.EventType != IOEventType.evKeyDown)
      return false;
    KeyEventArgs eventData = Event.EventData as KeyEventArgs;
    switch (eventData.KeyData)
    {
      case Keys.Up | Keys.Control:
        this.UpdateControls();
        if (this.btMoveUp.Enabled)
          this.DoMoveUp((object) this, (EventArgs) null);
        eventData.Handled = true;
        return true;
      case Keys.Down | Keys.Control:
        this.UpdateControls();
        if (this.btMoveDown.Enabled)
          this.DoMoveDown((object) this, (EventArgs) null);
        eventData.Handled = true;
        return true;
      default:
        return false;
    }
  }

  /// <summary>Форма закрывается</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ManualSortingEditForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.FIsChanged || e.CloseReason != CloseReason.UserClosing && e.CloseReason != CloseReason.None)
      return;
    e.Cancel = MessageBox.Show(LocalizationHolder.rm.GetString(sc_4220.ssp_imclient_4222()), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes;
  }

  /// <summary>Изменилась текущая страничка</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void pages_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Открывается контекстное меню</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы событий</param>
  private void contextMenuComposition_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.UpdateControls();
  }

  /// <summary>
  /// Получить список групп дочерних типов объектов в составе указанной странички
  /// </summary>
  /// <param name="index">Индекс странички с составом</param>
  /// <returns>Список групп дочерних типов объектов в составе указанной странички</returns>
  private List<int> GetCurrentObjTypeGroups(int index)
  {
    List<int> currentObjTypeGroups = new List<int>();
    if (index < 0 || index >= this.parentCompositions.Count)
      return currentObjTypeGroups;
    AdvRelationsView parentComposition = this.parentCompositions[index];
    int index1 = this._userRole.Rule.IndexOfParentObjectType(this.FParentItem.ObjectType, true);
    if (index1 < 0)
      return currentObjTypeGroups;
    ChildRelationType relType = this._userRole.Rule.ParentObjectTypes[index1][this._relationTypeIDs[index]];
    if (relType == null || parentComposition.ItemsCount == 0)
      return currentObjTypeGroups;
    for (int index2 = 0; index2 < parentComposition.ItemsCount; ++index2)
    {
      int int32 = Convert.ToInt32((parentComposition[index2] as AdvRelationsNodeID)[-7]);
      int parentObjectType = ManualSortingEditForm.GetBaseParentObjectType(relType, int32);
      if (currentObjTypeGroups.Count == 0 || currentObjTypeGroups[currentObjTypeGroups.Count - 1] != parentObjectType)
        currentObjTypeGroups.Add(parentObjectType);
    }
    return currentObjTypeGroups;
  }

  /// <summary>
  /// Получить отсортированный список групп дочерних типов объектов в составе указанной странички
  /// </summary>
  /// <param name="index">Индекс странички с составом</param>
  /// <returns>Отсортированный список групп дочерних типов объектов в составе указанной странички</returns>
  private List<int> GetSortedObjTypeGroups(int index)
  {
    List<int> sortedObjTypeGroups = new List<int>();
    if (index < 0 || index >= this.parentCompositions.Count)
      return sortedObjTypeGroups;
    AdvRelationsView parentComposition = this.parentCompositions[index];
    int index1 = this._userRole.Rule.IndexOfParentObjectType(this.FParentItem.ObjectType, true);
    if (index1 < 0)
      return sortedObjTypeGroups;
    ChildRelationType relType = this._userRole.Rule.ParentObjectTypes[index1][this._relationTypeIDs[index]];
    if (relType == null || parentComposition.ItemsCount == 0)
      return sortedObjTypeGroups;
    for (int index2 = 0; index2 < parentComposition.ItemsCount; ++index2)
    {
      int int32 = Convert.ToInt32((parentComposition[index2] as AdvRelationsNodeID)[-7]);
      int parentObjectType = ManualSortingEditForm.GetBaseParentObjectType(relType, int32);
      if (!sortedObjTypeGroups.Contains(parentObjectType))
        sortedObjTypeGroups.Add(parentObjectType);
    }
    sortedObjTypeGroups.Sort((IComparer<int>) new ManualSortingEditForm.CompareChildTypes(relType));
    return sortedObjTypeGroups;
  }

  /// <summary>Выполнить автоматическую сортировку текущего состава</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoAutoSortComposition(object sender, EventArgs e)
  {
    if (this.pages.SelectedIndex < 0)
      return;
    int selectedIndex = this.pages.SelectedIndex;
    if (this.FEditorModes[selectedIndex] != ManualSortingEditorMode.mseAdminMode)
      return;
    AdvRelationsView parentComposition = this.parentCompositions[selectedIndex];
    int index1 = this._userRole.Rule.IndexOfParentObjectType(this.FParentItem.ObjectType, true);
    if (index1 < 0)
      return;
    ChildRelationType relType = this._userRole.Rule.ParentObjectTypes[index1][this._relationTypeIDs[selectedIndex]];
    if (relType == null || parentComposition.ItemsCount <= 1)
      return;
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    List<int> intList = new List<int>();
    for (int index2 = 0; index2 < parentComposition.ItemsCount; ++index2)
    {
      AdvRelationsNodeID advRelationsNodeId = parentComposition[index2] as AdvRelationsNodeID;
      int int32 = Convert.ToInt32(advRelationsNodeId[-7]);
      int parentObjectType = ManualSortingEditForm.GetBaseParentObjectType(relType, int32);
      if (!dictionary.ContainsKey(parentObjectType))
      {
        dictionary.Add(parentObjectType, new List<long>());
        intList.Add(parentObjectType);
      }
      dictionary[parentObjectType].Add(advRelationsNodeId.PrjLinkID);
    }
    intList.Sort((IComparer<int>) new ManualSortingEditForm.CompareChildTypes(relType));
    long startValue = 0;
    for (int index3 = 0; index3 < intList.Count; ++index3)
    {
      this.NumerateSortAttribute(dictionary[intList[index3]], selectedIndex, startValue);
      startValue += Math.Max(1000000000L, (long) dictionary[intList[index3]].Count * 1000000L);
    }
    try
    {
      parentComposition.GridBeginUpdate();
      int index4 = 0;
      for (int index5 = 0; index5 < intList.Count; ++index5)
      {
        for (int index6 = 0; index6 < dictionary[intList[index5]].Count; ++index6)
        {
          INodeID node = (INodeID) parentComposition[dictionary[intList[index5]][index6]];
          parentComposition.GridSetNodeIndex(node, index4);
          ++index4;
        }
      }
    }
    finally
    {
      parentComposition.GridEndUpdate();
      parentComposition.BuildRelations();
      this.FIsChanged = true;
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Проверить соответствие состава с указанным индексом на соответствие текущему правилу сортировки составов
  /// </summary>
  /// <param name="index">Индекс состава</param>
  /// <returns>true, если состав корректен</returns>
  public bool IsCompositionCorrect(int index)
  {
    if (index < 0 || index >= this.parentCompositions.Count)
      return false;
    this.GetCurrentObjTypeGroups(index);
    this.GetSortedObjTypeGroups(index);
    return false;
  }

  /// <summary>В гриде изменился порядок сортировки в колонках</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void SortingGroupingChanged(object sender, EventArgs e)
  {
    if ((this.pages.SelectedIndex >= 0 ? (int) this.FEditorModes[this.pages.SelectedIndex] : 2) != 0)
      return;
    this.NumerateSortAttribute(this.parentCompositions[this.pages.SelectedIndex].Relations, this.pages.SelectedIndex, 0L);
    this.DoAutoSortComposition(sender, e);
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
    this.toolBarRight.Renderer = renderer;
    this.menuComposition.Renderer = renderer;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBarRight.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.menuComposition.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ManualSortingEditForm));
    this.panelComposition = new Panel();
    this.pages = new TabControl();
    this.toolBarRight = new Intermech.Bars.ToolBar();
    this.imagesToolbars = new ImageList();
    this.btMoveUp = new ButtonItem();
    this.btMoveDown = new ButtonItem();
    this.btMoveTop = new ButtonItem();
    this.btMoveBottom = new ButtonItem();
    this.btAutoSortComposition = new ButtonItem();
    this.panelBottom = new Panel();
    this.labelPicture = new Label();
    this.labelWarning = new Label();
    this.menuComposition = new MenuBar();
    this.contextMenuComposition = new ContextMenuBarItem();
    this.mnpMoveUp = new MenuButtonItem();
    this.mnpMoveDown = new MenuButtonItem();
    this.mnpMoveTop = new MenuButtonItem();
    this.mnpMoveBottom = new MenuButtonItem();
    this.mnpAutoSortComposition = new MenuButtonItem();
    this.mnpCustomize = new MenuButtonItem();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.imagesState = new ImageList();
    this.panelComposition.SuspendLayout();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.panelComposition.Controls.Add((Control) this.pages);
    this.panelComposition.Controls.Add((Control) this.toolBarRight);
    this.panelComposition.Controls.Add((Control) this.panelBottom);
    componentResourceManager.ApplyResources((object) this.panelComposition, "panelComposition");
    this.panelComposition.Name = "panelComposition";
    componentResourceManager.ApplyResources((object) this.pages, "pages");
    this.pages.Multiline = true;
    this.pages.Name = "pages";
    this.pages.SelectedIndex = 0;
    this.pages.SelectedIndexChanged += new EventHandler(this.pages_SelectedIndexChanged);
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
    this.toolBarRight.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.btMoveUp,
      (ToolbarItemBase) this.btMoveDown,
      (ToolbarItemBase) this.btMoveTop,
      (ToolbarItemBase) this.btMoveBottom,
      (ToolbarItemBase) this.btAutoSortComposition
    });
    this.toolBarRight.MinimumFloatingSize = new Size(250, 30);
    this.toolBarRight.Name = "toolBarRight";
    this.toolBarRight.Overflow = ToolBarOverflow.Wrap;
    this.toolBarRight.Stretch = true;
    this.toolBarRight.Tearable = false;
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_up_blue.png");
    this.imagesToolbars.Images.SetKeyName(1, "arrow_down_blue.png");
    this.imagesToolbars.Images.SetKeyName(2, "arrow_top_blue.png");
    this.imagesToolbars.Images.SetKeyName(3, "arrow_bottom_blue.png");
    this.imagesToolbars.Images.SetKeyName(4, "настройка_отображения.png");
    this.imagesToolbars.Images.SetKeyName(5, "manual_sort_eng.png");
    this.imagesToolbars.Images.SetKeyName(6, "warning.png");
    this.btMoveUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btMoveUp, "btMoveUp");
    this.btMoveUp.ImageIndex = 0;
    this.btMoveUp.Click += new EventHandler(this.DoMoveUp);
    componentResourceManager.ApplyResources((object) this.btMoveDown, "btMoveDown");
    this.btMoveDown.ImageIndex = 1;
    this.btMoveDown.Click += new EventHandler(this.DoMoveDown);
    this.btMoveTop.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btMoveTop, "btMoveTop");
    this.btMoveTop.ImageIndex = 2;
    this.btMoveTop.Click += new EventHandler(this.DoMoveTop);
    componentResourceManager.ApplyResources((object) this.btMoveBottom, "btMoveBottom");
    this.btMoveBottom.ImageIndex = 3;
    this.btMoveBottom.Click += new EventHandler(this.DoMoveBottom);
    this.btAutoSortComposition.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.btAutoSortComposition, "btAutoSortComposition");
    this.btAutoSortComposition.ImageIndex = 5;
    this.btAutoSortComposition.Importance = ToolBarItemImportance.High;
    this.btAutoSortComposition.Click += new EventHandler(this.DoAutoSortComposition);
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.labelPicture);
    this.panelBottom.Controls.Add((Control) this.labelWarning);
    this.panelBottom.Controls.Add((Control) this.menuComposition);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.labelPicture, "labelPicture");
    this.labelPicture.ImageList = this.imagesToolbars;
    this.labelPicture.Name = "labelPicture";
    componentResourceManager.ApplyResources((object) this.labelWarning, "labelWarning");
    this.labelWarning.Name = "labelWarning";
    componentResourceManager.ApplyResources((object) this.menuComposition, "menuComposition");
    this.menuComposition.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuComposition.Hidden = false;
    this.menuComposition.ImageList = this.imagesToolbars;
    this.menuComposition.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuComposition
    });
    this.menuComposition.Name = "menuComposition";
    this.menuComposition.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuComposition, "contextMenuComposition");
    this.contextMenuComposition.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpMoveUp,
      (ToolbarItemBase) this.mnpMoveDown,
      (ToolbarItemBase) this.mnpMoveTop,
      (ToolbarItemBase) this.mnpMoveBottom,
      (ToolbarItemBase) this.mnpAutoSortComposition,
      (ToolbarItemBase) this.mnpCustomize
    });
    this.contextMenuComposition.ShowText = true;
    this.contextMenuComposition.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuComposition_BeforePopup);
    componentResourceManager.ApplyResources((object) this.mnpMoveUp, "mnpMoveUp");
    this.mnpMoveUp.ImageIndex = 0;
    this.mnpMoveUp.ShowText = true;
    this.mnpMoveUp.Click += new EventHandler(this.DoMoveUp);
    componentResourceManager.ApplyResources((object) this.mnpMoveDown, "mnpMoveDown");
    this.mnpMoveDown.ImageIndex = 1;
    this.mnpMoveDown.ShowText = true;
    this.mnpMoveDown.Click += new EventHandler(this.DoMoveDown);
    this.mnpMoveTop.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveTop, "mnpMoveTop");
    this.mnpMoveTop.ImageIndex = 2;
    this.mnpMoveTop.ShowText = true;
    this.mnpMoveTop.Click += new EventHandler(this.DoMoveTop);
    componentResourceManager.ApplyResources((object) this.mnpMoveBottom, "mnpMoveBottom");
    this.mnpMoveBottom.ImageIndex = 3;
    this.mnpMoveBottom.ShowText = true;
    this.mnpMoveBottom.Click += new EventHandler(this.DoMoveBottom);
    this.mnpAutoSortComposition.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpAutoSortComposition, "mnpAutoSortComposition");
    this.mnpAutoSortComposition.ImageIndex = 5;
    this.mnpAutoSortComposition.ShowText = true;
    this.mnpAutoSortComposition.Click += new EventHandler(this.DoAutoSortComposition);
    this.mnpCustomize.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpCustomize, "mnpCustomize");
    this.mnpCustomize.ImageIndex = 4;
    this.mnpCustomize.ShowText = true;
    this.mnpCustomize.Click += new EventHandler(this.DoCustomizeView);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Hand;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Hand;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.imagesState.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesState.ImageStream");
    this.imagesState.TransparentColor = Color.Transparent;
    this.imagesState.Images.SetKeyName(0, "object_16x16.ico");
    this.imagesState.Images.SetKeyName(1, "link_16x16.ico");
    this.AcceptButton = (IButtonControl) this.btnApply;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panelComposition);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ManualSortingEditForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.ManualSortingEditForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.ManualSortingEditForm_FormClosed);
    this.Load += new EventHandler(this.ManualSortingEditForm_Load);
    this.panelComposition.ResumeLayout(false);
    this.panelBottom.ResumeLayout(false);
    this.panelBottom.PerformLayout();
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Класс, определяющий, в каком порядке должны располагаться дочерние типы объектов в группирующем словарике
  /// </summary>
  private class CompareChildTypes : IComparer<int>
  {
    /// <summary>
    /// Описание дочернего типа связи, состав которой группируется
    /// </summary>
    private ChildRelationType _relType;

    /// <summary>Создать экземпляр класса</summary>
    /// <param name="relType">Описание дочернего типа связи, состав которой группируется</param>
    public CompareChildTypes(ChildRelationType relType) => this._relType = relType;

    /// <summary>Сравнить два дочерних типа объектов</summary>
    /// <param name="x">Первый дочерний тип объектов</param>
    /// <param name="y">Второй дочерний тип объектов</param>
    /// <returns>-1, 0, 1</returns>
    public int Compare(int x, int y)
    {
      int parentObjectType1 = ManualSortingEditForm.GetBaseParentObjectType(this._relType, x);
      int parentObjectType2 = ManualSortingEditForm.GetBaseParentObjectType(this._relType, y);
      return this._relType.IndexOf(parentObjectType1).CompareTo(this._relType.IndexOf(parentObjectType2));
    }
  }
}
