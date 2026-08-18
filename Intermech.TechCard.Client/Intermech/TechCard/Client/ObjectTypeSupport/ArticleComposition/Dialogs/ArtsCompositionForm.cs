// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs.ArtsCompositionForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Client.Core.Navigator.Controls;
using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Data;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Classes.Tasks;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Views;
using Intermech.TechCard.Client.Tools.Controls;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs;

/// <summary>Форма для создания контекстной сборочной единицы</summary>
/// <summary>Форма для создания контекстной сборочной единицы</summary>
public class ArtsCompositionForm : ArtsCompositionBaseForm
{
  /// <summary>Настройки контекстных</summary>
  private IArtsCompositionParams _compositionParams;
  /// <summary>
  /// 
  /// </summary>
  private ArtsCompositionApplicabilityParams _applicabilityParams;
  /// <summary>
  /// 
  /// </summary>
  protected TechNavigatorControl _techNavControl;
  /// <summary>
  ///  Коллекция пар значений [Версия объекта] = [Количества в КСЕ и ТП]
  /// </summary>
  private readonly ArtsCompositionForm.ElementQuantityList _elemQtyList = new ArtsCompositionForm.ElementQuantityList();
  /// <summary>Список таблиц с загруженными данными</summary>
  private readonly IList<Tuple<int, int, DataTable>> _virtualDataTables = (IList<Tuple<int, int, DataTable>>) new List<Tuple<int, int, DataTable>>(5);
  /// <summary>Параметры создания объектов</summary>
  private readonly ArtsCompositionForm.ObjCreateParams _objCreateParams;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList imagesToolbars;
  private Panel panelPage1;
  private Panel pnlTop;
  private ToolStrip tsMain;
  private ToolStripLabel tslblContext;
  private ToolStripComboBox tscbContext;
  private ToolStripSeparator toolStripSeparator1;
  private Panel pnlTopHeader;
  private Label lblCaption;
  private PictureBox pictCaption;
  private Panel pnlButtons;
  private Button btnCancel;
  private Button btnApply;
  private Panel pnlClient;
  private ContextMenuBarItem contextMenuComposition;
  private MenuButtonItem mnpAddToCC;
  private MenuButtonItem mnpRefresh;
  private MenuButtonItem menuButtonItem1;
  private ContextMenuStrip cmsArticles;
  private ToolStripMenuItem tsmiArticleAdd;
  private Panel pnlProgress;
  private Label lblProgressInfo;
  private ProgressBar prgbarProgress;
  private ToolStripSeparator tsmiArticleSep1;
  private ToolStripMenuItem tsmiArticleRefresh;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    if (this.DesignMode)
      return;
    this._techNavControl = new TechNavigatorControl();
    this.pnlClient.Controls.Add((Control) this._techNavControl);
    this._techNavControl.Dock = DockStyle.Fill;
    this._techNavControl.BringToFront();
    this._techNavControl.DoubleClick += new TechNavigatorEventHandler(this.TechNavControlDoubleClickEvent);
    this._techNavControl.SelectedItemsChanged += new EventHandler(this.DoUpdateControls);
    this._techNavControl.ViewsManager.ActiveViewPageChanged += new EventHandler(this.TechActiveViewPageChanged);
    this._techNavControl.Location = new Point(8, 8);
    this._techNavControl.Name = "techNavControl";
    this._techNavControl.ViewsManager.AllowedViews = new string[7]
    {
      "ChildrenView",
      "SelectionView",
      "ObjectPropertiesView",
      "PropertiesView",
      "ContainsView",
      "ApplicabilityView",
      "ArtsCompositionApplicabilityView"
    };
    this._techNavControl.ViewsManager.SuppressedViews = new string[23]
    {
      "ExpFormView",
      "ScriptEdit",
      "EditingContextsView",
      "DocumsObject",
      "ArchiveStructureObject",
      "ArchiveSigns",
      "Graphs",
      "OpenKeysView",
      "EditorForm",
      "FormDesignerViewObject",
      "FormDesignerViewRelation",
      "VersionRulesEditorView",
      "EventLogPropertiesView",
      "AutosTreeView",
      "VersionRulesView",
      "ProjectTeamsView",
      "GroupingObjectsSearchView",
      "EventsConfigView",
      "ImageView",
      "VisualizerView",
      "ObjectVisualizerView",
      "FilesView",
      "ObjectFilesView"
    };
    this._techNavControl.TabIndex = 0;
    this._techNavControl.TreeView.BeginInit();
    try
    {
      this._techNavControl.TreeView.ShowContextMenu += new MouseEventHandler(this.DoShowTreeContextMenu);
      this._techNavControl.TreeView.DoubleClick += new EventHandler(this.TreeMouseDoubleClick);
      this._techNavControl.TreeView.AllowDrop = false;
      this._techNavControl.TreeView.AllowUserPinnedColumns = false;
      this._techNavControl.TreeView.BackgroundImageMode = ImageDrawMode.Tile;
      this._techNavControl.TreeView.BorderStyle = BorderStyle.Fixed3D;
      this._techNavControl.TreeView.DisableCheckedOutColumn = true;
      this._techNavControl.TreeView.DisableIMContextMenu = true;
      this._techNavControl.TreeView.DisableKeyDownEvents = true;
      this._techNavControl.TreeView.DisableKeyUpEvents = true;
      this._techNavControl.TreeView.LineStyle = LineStyle.Dot;
      this._techNavControl.TreeView.MultiSelect = true;
      this._techNavControl.TreeView.RowSelectedStyle.BackColor = SystemColors.Highlight;
      this._techNavControl.TreeView.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
      this._techNavControl.TreeView.RowStyle.BorderColor = SystemColors.Control;
      this._techNavControl.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
      this._techNavControl.TreeView.RowStyle.BorderWidth = 1;
      this._techNavControl.TreeView.SelectBeforeEdit = true;
      this._techNavControl.TreeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
      this._techNavControl.TreeView.ShowRootRow = false;
      this._techNavControl.TreeView.SuppressErrorMessages = true;
      this._techNavControl.TreeView.SetColumns(Intermech.Navigator.Utils.ContextStatusColumns());
    }
    finally
    {
      this._techNavControl.TreeView.EndInit();
    }
    BarManager service = ServiceUtils.GetService<BarManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
    this.ToolbarRendererChanged((object) service, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="serviceContainer"></param>
  private void InitializeControlServices(IServiceContainer serviceContainer)
  {
    ServiceUtils.GetService<IArtsCompositionParamsService>((object) ApplicationServices.Container, false)?.LoadSettings(out this._compositionParams);
    ArtsCompositionVirtualColumnProvider serviceInstance = new ArtsCompositionVirtualColumnProvider(ArtsCompositionColumnScheme.Consts.SchemeGuid);
    serviceInstance.FillDataTableEvent += new FillDataTableEventHandler(this.VirtualColumnProviderOnFillDataTableEvent);
    serviceContainer.AddService(typeof (INavigatorVirtualColumnProvider), (object) serviceInstance);
    serviceContainer.AddService(typeof (INavigatorSchemeColumnProvider), (object) new ArtsCompositionSchemeColumnProvider());
    IArtsCompositionImageService compositionImageService = (IArtsCompositionImageService) new ArtsCompositionImageService(this._compositionParams ?? (IArtsCompositionParams) new ArtsCompositionParams());
    serviceContainer.AddService(typeof (IArtsCompositionImageService), (object) compositionImageService);
    serviceContainer.AddService(typeof (IGridCellDrawingProvider), (object) new ArtsCompositionCellDrawingProvider(compositionImageService));
    serviceContainer.AddService(typeof (INavigatorTreeViewCellWidgetProvider), (object) new ArtsCompositionTreeViewCellWidgetProvider());
  }

  /// <summary>Инициализация размеров формы / контролов</summary>
  protected override void InitializeFormLayout()
  {
    base.InitializeFormLayout();
    this.pnlProgress.Visible = false;
  }

  /// <summary>Загрузить данные в форму</summary>
  /// <returns>true, если загрузка прошла успешно</returns>
  protected override bool LoadControlData()
  {
    base.LoadControlData();
    this.LoadFormTreeData();
    this.LoadContextsList(this.tscbContext.ComboBox);
    this.LoadApplicabilityData();
    this.UpdateControls();
    return true;
  }

  /// <summary>Загрузить данные в дерево навигатора</summary>
  /// <returns>true, если загрузка прошла успешно</returns>
  protected virtual void LoadFormTreeData()
  {
    ArtsCompositionDataProvider.PluginData.CurrentSet = 0;
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this._artObjectId);
    IDescriptor descriptor = (IDescriptor) new TechCompositionDescriptor(this._rootCategoryID, objectInfo.ObjectTypeID, this._artObjectId, MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, TechCardConsts.ObjectTypes.ArticleCopyBaseID) ? TechCardConsts.ObjectTypes.ArticleCopyBaseID : TechCardConsts.ObjectTypes.ArticleBaseID, (IEnumerable<int>) TechCardConsts.RelTypes.ArtsCompositionRelations, "", RelatedObjectsRole.Composition, (ITechCompositionFilter) null, (IEnumerable<NodeColumnID>) null);
    this._techNavControl.TreeView.OnGetSupportedColumnsEventHandler += new Intermech.Navigator.Controls.GetSupportedColumnsEventHandler(this.GetSupportedColumnsEventHandler);
    this._techNavControl.RootDescriptor = descriptor;
  }

  /// <summary>Обновить дерево КСЕ</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  protected virtual void DoRefreshComposition(object sender, EventArgs e)
  {
    ArtsCompositionDataProvider.PluginData.CurrentSet = 0;
    this._techNavControl.TreeView.Build((IDescriptor) new TechCompositionDescriptor(this._rootCategoryID, TechCardConsts.ObjectTypes.ArticleBaseID, this._artObjectId, TechCardConsts.ObjectTypes.ArticleBaseID, (IEnumerable<int>) TechCardConsts.RelTypes.ArtsCompositionRelations, "", RelatedObjectsRole.Composition, (ITechCompositionFilter) null, (IEnumerable<NodeColumnID>) null));
    this.UpdateControls();
    this._dataProvider.LoadedDesignData = false;
  }

  /// <summary>Изменились выделенные элементы в деревьях</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void DoUpdateControls(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Загрузка расположения и размеров формы</summary>
  protected override void LoadSettings(bool loadFormPosition)
  {
    base.LoadSettings(loadFormPosition);
    this._techNavControl?.LoadLayout((IDictionary) this._formSettings);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name);
    if (this._techNavControl == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, this._techNavControl.TreeView);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  protected override void SaveSettings(bool saveFormPosition)
  {
    this._techNavControl.SaveLayout((IDictionary) this._formSettings);
    base.SaveSettings(saveFormPosition);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name) ?? service.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, this._techNavControl.TreeView);
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadApplicabilityData()
  {
    this._applicabilityParams = new ArtsCompositionApplicabilityParams(new ObjInfoItem(this._techObjectId), this._services);
    this._techNavControl.Services.RemoveService<ArtsCompositionApplicabilityParams>();
    this._techNavControl.Services.AddService(typeof (ArtsCompositionApplicabilityParams), (object) this._applicabilityParams);
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateApplicabilityData()
  {
    this._applicabilityParams.TechElemObj2ArticleList.Clear();
    if (this._dataProvider.TableTech == null)
      return;
    int columnIndex1 = 2;
    int columnIndex2 = 0;
    int columnIndex3 = this._dataProvider.TableTech.Columns.IndexOf(TechCardConsts.AttributeTypes.ObjectRefAttrGuid.ToString());
    foreach (DataRow row in (InternalDataCollectionBase) this._dataProvider.TableTech.Rows)
      this._applicabilityParams.TechElemObj2ArticleList.Add(new Tuple<ObjInfoItem, ObjInfoItem>(new ObjInfoItem(DataSetProcessor.GetInt64Value(row[columnIndex1], 0L), DataSetProcessor.GetInt32Value(row[columnIndex2], -1)), new ObjInfoItem(DataSetProcessor.GetInt64Value(row[columnIndex3], 0L))));
    ISelectedItems selectedItems = this._techNavControl.ItemsHost?.SelectedItems;
    IDBObjectID itemData = selectedItems == null || selectedItems.Count <= 0 ? (IDBObjectID) null : selectedItems.GetItemData<IDBObjectID>(0, false);
    if (itemData == null)
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ApplicabilityUpdated", itemData.Value));
  }

  /// <summary>Подготовить панель к показу</summary>
  /// <param name="panel">Панель</param>
  private void ShowPanel(Panel panel)
  {
    panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    panel.Left = 0;
    panel.Top = 0;
    if (panel.Parent == null)
      return;
    Panel panel1 = panel;
    Size clientSize = panel.Parent.ClientSize;
    int width = clientSize.Width;
    panel1.Width = width;
    Panel panel2 = panel;
    clientSize = panel.Parent.ClientSize;
    int height = clientSize.Height;
    panel2.Height = height;
  }

  /// <summary>Обновить контролы</summary>
  protected override void UpdateControls()
  {
    this.ShowPanel(this.panelPage1);
    this.btnCancel.Enabled = true;
    ISelectedItems selectedItems = this.GetSelectedItems();
    this.btnApply.Enabled = this._objCreateParams != null && selectedItems != null && selectedItems.Count != 0;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoBeforeLoadData(object sender, EventArgs args)
  {
    this.pnlProgress.Height = 43;
    this.pnlProgress.Visible = true;
    this.prgbarProgress.Value = 0;
    this.prgbarProgress.Minimum = 0;
    this.prgbarProgress.Maximum = 100;
    this.btnApply.Enabled = false;
    this.btnCancel.Enabled = false;
    if (!this._dataProvider.LoadedDesignData || !this._dataProvider.LoadedTechData)
      this.lblProgressInfo.Text = LocalizationHolder.rm.GetString("TechCard.Client_390");
    Application.DoEvents();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoAfterLoadData(object sender, EventArgs args)
  {
    Application.DoEvents();
    if (this._dataProvider.LoadedDesignData && this._dataProvider.LoadedTechData)
    {
      this.FillElemQtyList();
      this.UpdateVirtualDataTables();
      this.UpdateApplicabilityData();
    }
    this.pnlProgress.Visible = false;
    this.btnCancel.Enabled = true;
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoProcessChanged(object sender, ProgressChangedEventArgs args)
  {
    if (!this.pnlProgress.Visible)
      return;
    this.prgbarProgress.Value = args.ProgressPercentage;
  }

  /// <summary>
  /// Проанализировать исходные данные и заполнить список сравнения состава
  /// </summary>
  /// <returns>0 - всё ОК, -1 - таблицы пустые, -2 - прерван процесс, -3 - данные не найдены (возможны ошибки)</returns>
  private void FillElemQtyList()
  {
    this._elemQtyList.Clear();
    if (this._dataProvider.TableTech == null)
      return;
    if (this._dataProvider.TableDesign != null)
    {
      foreach (ElementQuantity elementQuantity in this._compositionParams.DesignQuantityMode == ArtsCompositionQuantityMode.FullExpanded ? (IEnumerable<ElementQuantity>) this.GetElemQtyDesignList(this._dataProvider.TableDesign) : (IEnumerable<ElementQuantity>) this.GetElemQtyDesignFirstLevelCountList(this._dataProvider.TableDesign))
      {
        RelObjInfoItem typedInfoItem = (RelObjInfoItem) elementQuantity.TypedInfoItem;
        if (typedInfoItem.RelationID == 0L)
          typedInfoItem.RelationID = long.MinValue + (long) this._elemQtyList.Count;
        this._elemQtyList[typedInfoItem] = elementQuantity;
      }
    }
    IList<ElementQuantity> elemQtyTechList = this.GetElemQtyTechList(this._dataProvider.TableTech);
    if (elemQtyTechList.Count == 0)
      return;
    IDictionary<long, ObjInfoIDItem> objInfoIdItems2Update = (IDictionary<long, ObjInfoIDItem>) new Dictionary<long, ObjInfoIDItem>();
    foreach (ElementQuantity elementQuantity1 in (IEnumerable<ElementQuantity>) elemQtyTechList)
    {
      bool flag = false;
      long num = elementQuantity1.TechQuantity != null ? elementQuantity1.TechQuantity.MeasureID : -1L;
      foreach (ElementQuantity elementQuantity2 in this._elemQtyList.GetValuesByObject((ObjInfoItem) elementQuantity1.TypedInfoItem))
      {
        if ((elementQuantity2.DesignQuantity != null ? elementQuantity2.DesignQuantity.MeasureID : -1L) == num)
        {
          elementQuantity2.TechQuantity = elementQuantity1.TechQuantity;
          flag = true;
        }
      }
      if (!flag)
      {
        ArtsCompositionForm.ElementQuantityList elemQtyList = this._elemQtyList;
        RelObjInfoItem key = new RelObjInfoItem(long.MinValue + (long) this._elemQtyList.Count);
        key.PartInfo = (ObjInfoItem) elementQuantity1.TypedInfoItem;
        ElementQuantity elementQuantity3 = elementQuantity1;
        elemQtyList.TryAdd(key, elementQuantity3);
        if (elementQuantity1.TypedInfoItem is ObjInfoIDItem typedInfoItem && typedInfoItem.ID == 0L)
          objInfoIdItems2Update[typedInfoItem.ObjectID] = typedInfoItem;
      }
    }
    if (objInfoIdItems2Update.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<ITypedInfoService>((object) sessionKeeper.Session, false)?.UpdateUnknownInfo((IEnumerable<ObjInfoItem>) objInfoIdItems2Update.Values, (object) sessionKeeper.Session.SessionGUID)?.ForEach((Action<ObjInfoItem>) (item => objInfoIdItems2Update[item.ObjectID].ID = ((ObjInfoIDItem) item).ID));
  }

  /// <summary>
  /// Получение количества (в конструкторском поле) для таблицы
  /// </summary>
  /// <param name="dataTable"></param>
  /// <param name="idxColumnObjectId"></param>
  /// <returns></returns>
  private IList<ElementQuantity> GetElemQtyDesignList(DataTable dataTable)
  {
    int columnIndex1 = 2;
    int columnIndex2 = 1;
    int columnIndex3 = dataTable.Columns.IndexOf("F_ID");
    IList<ElementQuantity> elemQtyDesignList = (IList<ElementQuantity>) new List<ElementQuantity>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      try
      {
        long relationId = Math.Abs(DataSetProcessor.GetInt64Value(row, columnIndex2, 0L));
        int int32Value = DataSetProcessor.GetInt32Value(row, 0, -1);
        long objectId = Math.Abs(DataSetProcessor.GetInt64Value(row, columnIndex1, 0L));
        long int64Value = columnIndex3 != -1 ? DataSetProcessor.GetInt64Value(row, columnIndex3, 0L) : 0L;
        string stringValue = DataSetProcessor.GetStringValue(row, 3, string.Empty);
        ElementQuantity elementQuantity = new ElementQuantity((ITypedInfoItem) new RelObjInfoItem(relationId)
        {
          PartInfo = (ObjInfoItem) new ObjInfoIDItem(objectId, int32Value, int64Value)
        }, stringValue, string.Empty);
        elemQtyDesignList.Add(elementQuantity);
      }
      catch
      {
      }
    }
    return elemQtyDesignList;
  }

  /// <summary>
  /// Получение количества (в конструкторском поле) для таблицы,
  /// с подсчетом количества в рамках одного родителя (
  /// </summary>
  /// <param name="dataTable"></param>
  /// <param name="idxColumnObjectId"></param>
  /// <returns></returns>
  private IList<ElementQuantity> GetElemQtyDesignFirstLevelCountList(DataTable dataTable)
  {
    int columnIndex1 = 2;
    int columnIndex2 = 1;
    int columnIndex3 = dataTable.Columns.IndexOf("F_ID");
    int columnIndex4 = dataTable.Columns.IndexOf("F_PROJ_ID");
    Dictionary<Tuple<long, long, long>, MeasuredValue> dictionary = new Dictionary<Tuple<long, long, long>, MeasuredValue>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      try
      {
        long int64Value = DataSetProcessor.GetInt64Value(row, columnIndex4, 0L);
        long num1 = Math.Abs(DataSetProcessor.GetInt64Value(row, columnIndex1, 0L));
        string stringValue = DataSetProcessor.GetStringValue(row, 3, string.Empty);
        long num2 = -1;
        MeasuredValue operand1 = (MeasuredValue) null;
        if (stringValue != string.Empty)
        {
          operand1 = MeasureHelper.ConvertToBaseMeasure(MeasureHelper.ConvertToMeasuredValue(stringValue));
          num2 = operand1.MeasureID;
        }
        long num3 = num1;
        long num4 = num2;
        Tuple<long, long, long> key = new Tuple<long, long, long>(int64Value, num3, num4);
        MeasuredValue operand2;
        if (dictionary.TryGetValue(key, out operand2))
          operand1 = operand1 == null ? operand2 : MeasureHelper.Add(operand1, operand2, false);
        dictionary[key] = operand1;
      }
      catch
      {
      }
    }
    IList<ElementQuantity> firstLevelCountList = (IList<ElementQuantity>) new List<ElementQuantity>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      try
      {
        long relationId = Math.Abs(DataSetProcessor.GetInt64Value(row, columnIndex2, 0L));
        long int64Value1 = DataSetProcessor.GetInt64Value(row, columnIndex4, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(row, 0, -1);
        long objectId = Math.Abs(DataSetProcessor.GetInt64Value(row, columnIndex1, 0L));
        long int64Value2 = columnIndex3 != -1 ? DataSetProcessor.GetInt64Value(row, columnIndex3, 0L) : 0L;
        ElementQuantity elementQuantity = new ElementQuantity((ITypedInfoItem) new RelObjInfoItem(relationId)
        {
          PartInfo = (ObjInfoItem) new ObjInfoIDItem(objectId, int32Value, int64Value2)
        }, string.Empty, string.Empty);
        firstLevelCountList.Add(elementQuantity);
        string stringValue = DataSetProcessor.GetStringValue(row, 3, string.Empty);
        long num = -1;
        if (stringValue != string.Empty)
          num = MeasureHelper.ConvertToBaseMeasure(MeasureHelper.ConvertToMeasuredValue(stringValue)).MeasureID;
        MeasuredValue measuredValue;
        if (dictionary.TryGetValue(new Tuple<long, long, long>(int64Value1, objectId, num), out measuredValue))
          elementQuantity.DesignQuantity = measuredValue;
      }
      catch
      {
      }
    }
    return firstLevelCountList;
  }

  /// <summary>
  /// Получение количества (в конструкторском поле) для таблицы
  /// </summary>
  /// <param name="dataTable"></param>
  /// <param name="idxColumnObjectId"></param>
  /// <returns></returns>
  private IList<ElementQuantity> GetElemQtyTechList(DataTable dataTable)
  {
    int columnIndex = dataTable.Columns.IndexOf(TechCardConsts.AttributeTypes.ObjectRefAttrGuid.ToString());
    Dictionary<Tuple<long, long>, ElementQuantity> dictionary = new Dictionary<Tuple<long, long>, ElementQuantity>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      try
      {
        long objectId = Math.Abs(DataSetProcessor.GetInt64Value(row, columnIndex, 0L));
        string stringValue = DataSetProcessor.GetStringValue(row, 3, string.Empty);
        long num = -1;
        MeasuredValue operand2 = (MeasuredValue) null;
        if (stringValue != string.Empty)
        {
          operand2 = MeasureHelper.ConvertToBaseMeasure(MeasureHelper.ConvertToMeasuredValue(stringValue));
          num = operand2.MeasureID;
        }
        Tuple<long, long> key = new Tuple<long, long>(objectId, num);
        ElementQuantity elementQuantity;
        if (!dictionary.TryGetValue(key, out elementQuantity))
        {
          elementQuantity = new ElementQuantity((ITypedInfoItem) new ObjInfoIDItem(objectId), string.Empty, stringValue);
          dictionary[key] = elementQuantity;
        }
        else
          elementQuantity.TechQuantity = elementQuantity.TechQuantity == null ? operand2 : MeasureHelper.Add(elementQuantity.TechQuantity, operand2, false);
      }
      catch
      {
      }
    }
    return (IList<ElementQuantity>) dictionary.Values.ToList<ElementQuantity>();
  }

  /// <summary>Получение списка выделенных элементов</summary>
  /// <returns></returns>
  protected ISelectedItems GetSelectedItems() => this._techNavControl.ItemsHost?.SelectedItems;

  /// <summary>Добавление выделенных элементов</summary>
  /// <param name="items"></param>
  private bool AddSelectedItems(ISelectedItems items)
  {
    if (this._objCreateParams?.AddCallBack == null || items == null || items.Count == 0)
      return false;
    if (this._objCreateParams.NeedAddCount)
      this.StartLoadData();
    ArtsCompositionMeasureForm compositionMeasureForm1 = (ArtsCompositionMeasureForm) null;
    List<MeasureDescriptor> measureDescriptorList = (List<MeasureDescriptor>) null;
    if (this._objCreateParams.NeedAddCount)
    {
      ArtsCompositionMeasureForm compositionMeasureForm2 = new ArtsCompositionMeasureForm((IWin32Window) this);
      compositionMeasureForm2.ShowAbortButton = items.Count > 1;
      IArtsCompositionParams compositionParams = this._compositionParams;
      compositionMeasureForm2.ShowRemainQtyControls = compositionParams != null && compositionParams.ShowRemainQty;
      compositionMeasureForm1 = compositionMeasureForm2;
      measureDescriptorList = new List<MeasureDescriptor>(0);
      long int64 = Convert.ToInt64(MetaDataHelper.GetAttributeType(TechCardConsts.AttributeTypes.CountAttrTypeID).SizeType);
      if (int64 == -1L)
      {
        measureDescriptorList.AddRange((IEnumerable<MeasureDescriptor>) MeasureHelper.Measures);
      }
      else
      {
        List<long> longList = new List<long>(1);
        if (int64 > 0L)
        {
          longList.Add(int64);
        }
        else
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(TechCardConsts.AttributeTypes.CountAttrTypeID, true);
            longList.AddRange((IEnumerable<long>) (long[]) attributeType.PropertiesStructure.MetadataExtensions[(object) "MU_PHYSICAL_ID"]);
          }
        }
        foreach (MeasureDescriptor measure in MeasureHelper.Measures)
        {
          if (longList.Contains(measure.PhysicalQuantityID))
            measureDescriptorList.Add(measure);
        }
      }
    }
    bool flag1 = false;
    long artObjectId = this._artObjectId;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new List<int>((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes));
    childrenIdRecursive.AddRange((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes);
    GenericListHelper.MakeUnique<int>(childrenIdRecursive);
    List<Tuple<IDBTypedObjectID, IDBTypedObjectID, IDBRelationID>> tupleList = new List<Tuple<IDBTypedObjectID, IDBTypedObjectID, IDBRelationID>>();
    for (int index = 0; index < items.Count; ++index)
      tupleList.Add(new Tuple<IDBTypedObjectID, IDBTypedObjectID, IDBRelationID>(items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID, items.GetParentData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID, items.GetItemData(index, typeof (IDBRelationID)) as IDBRelationID));
    MeasuredValue aMeasureValue = (MeasuredValue) null;
    MeasureDialogResult measureDialogResult = MeasureDialogResult.Add;
    for (int index = 0; index < tupleList.Count && !(this._objCreateParams.CreateMode == ArtsCompositionForm.ObjCreateParams.ObjCreateMode.Replace & flag1); ++index)
    {
      IDBTypedObjectID dbTypedObjectId1 = tupleList[index].Item1;
      if (dbTypedObjectId1 != null && dbTypedObjectId1.ObjectID != 0L && childrenIdRecursive.BinarySearch(dbTypedObjectId1.ObjectType) >= 0)
      {
        long objectId = dbTypedObjectId1.ObjectID;
        long projArtId = 0;
        long projRelId = 0;
        int projRelTypeId = -1;
        IDBTypedObjectID dbTypedObjectId2 = tupleList[index].Item2;
        if (dbTypedObjectId2 != null)
          projArtId = dbTypedObjectId2.ObjectID;
        IDBRelationID dbRelationId = tupleList[index].Item3;
        if (dbRelationId != null && dbRelationId.Value != 0L && dbRelationId.Value != -1L)
        {
          projRelId = dbRelationId.Value;
          projRelTypeId = dbRelationId.RelationType;
          if (dbTypedObjectId2 == null)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBRelation relation = sessionKeeper.Session.GetRelation(dbRelationId.Value, false);
              if (relation != null)
                projArtId = relation.ProjID;
            }
          }
        }
        MeasuredValue measuredValue = (MeasuredValue) null;
        if (this._objCreateParams.NeedAddCount)
        {
          bool flag2 = true;
          ElementQuantity elementQuantity;
          if (!this._elemQtyList.TryGetValue(new RelObjInfoItem(Math.Abs(projRelId)), out elementQuantity))
            elementQuantity = this._elemQtyList.GetValuesByObject(new ObjInfoItem(objectId)).FirstOrDefault<ElementQuantity>();
          if (elementQuantity?.DesignQuantity != null)
          {
            if (elementQuantity.TechQuantity != null && elementQuantity.DesignQuantity.Value <= elementQuantity.TechQuantity.Value)
            {
              string caption = LocalizationHolder.rm.GetString("TechCard.Client_213");
              switch (MessageBox.Show((IWin32Window) this, string.Format(LocalizationHolder.rm.GetString(sc_19389.ssp_techcard_19390()), (object) dbTypedObjectId1.Caption, (object) dbTypedObjectId1.ObjectID), caption, tupleList.Count > 1 ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
              {
                case DialogResult.Cancel:
                  return false;
                case DialogResult.No:
                  continue;
              }
            }
            else if (Math.Abs(elementQuantity.DesignQuantity.Value - 1.0) < double.Epsilon)
            {
              measuredValue = elementQuantity.DesignQuantity;
              flag2 = tupleList.Count != 1;
            }
          }
          if (measureDialogResult == MeasureDialogResult.AddForAll || measureDialogResult == MeasureDialogResult.AddAllQuantityForAll)
            flag2 = false;
          if (flag2)
          {
            aMeasureValue = measuredValue;
            compositionMeasureForm1.Text = string.Format(LocalizationHolder.rm.GetString("TechCard.Client_393"), (object) dbTypedObjectId1.Caption, (object) dbTypedObjectId1.ObjectID);
            measureDialogResult = compositionMeasureForm1.ExecuteDialog(ref aMeasureValue, elementQuantity?.DesignQuantity, elementQuantity?.TechQuantity, measureDescriptorList.ToArray(), tupleList.Count > 1);
          }
          switch (measureDialogResult)
          {
            case MeasureDialogResult.AddAllQuantityForAll:
              if (elementQuantity?.DesignQuantity != null && elementQuantity.TechQuantity != null)
              {
                aMeasureValue = MeasureHelper.Substract(elementQuantity.DesignQuantity, elementQuantity.TechQuantity);
                break;
              }
              break;
            case MeasureDialogResult.Cancel:
              continue;
            case MeasureDialogResult.Terminate:
              return false;
          }
        }
        ArtsCompositionsUtils.ArticleItemInfo artInfo = new ArtsCompositionsUtils.ArticleItemInfo(artObjectId, projArtId, projRelId, projRelTypeId, objectId, aMeasureValue);
        List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated;
        if (this._objCreateParams.AddCallBack != null && this._objCreateParams.AddCallBack(this._objCreateParams.ProjTechObj, artInfo, out objCreated))
        {
          flag1 = true;
          if (this._dataProvider.TableTech == null)
            return flag1;
          if (objCreated != null && objCreated.Count != 0)
          {
            foreach (ArtsCompositionsUtils.ArticleCreatedItem articleCreatedItem in objCreated)
            {
              if (articleCreatedItem?.Count != null)
              {
                DataRow row = this._dataProvider.TableTech.NewRow();
                string columnName1 = -7.ToString();
                row[columnName1] = (object) articleCreatedItem.TechObjTypeID;
                string columnName2 = -20.ToString();
                row[columnName2] = (object) articleCreatedItem.ProjLinkID;
                int num = -2;
                string columnName3 = num.ToString();
                row[columnName3] = (object) articleCreatedItem.TechObjID;
                num = TechCardConsts.AttributeTypes.CountAttrTypeID;
                string columnName4 = num.ToString();
                MeasuredValue baseMeasure = MeasureHelper.ConvertToBaseMeasure(articleCreatedItem.Count);
                row[columnName4] = baseMeasure == null ? (object) 0 : (object) baseMeasure.ToString();
                string columnName5 = TechCardConsts.AttributeTypes.ObjectRefAttrGuid.ToString();
                row[columnName5] = (object) articleCreatedItem.ArtObjID;
                this._dataProvider.TableTech.Rows.Add(row);
              }
            }
            this.FillElemQtyList();
            this.UpdateVirtualDataTables(true);
            this.UpdateApplicabilityData();
          }
        }
      }
    }
    return flag1;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void InitializeCustomServices()
  {
    ArtsCompositionBaseForm.PluginsService.RegisterClientPlugin(ArtsCompositionDataProvider.PluginData.PluginGuid, (IClientPluginsDataTransfer) ArtsCompositionDataProvider.PluginData);
    base.InitializeCustomServices();
    this.InitializeControlServices(this._techNavControl.Services);
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void ReleaseCustomServices()
  {
    ArtsCompositionBaseForm.PluginsService.UnregisterClientPlugin(ArtsCompositionDataProvider.PluginData.PluginGuid);
    base.ReleaseCustomServices();
  }

  /// <summary>Создать экземпляр формы</summary>
  public ArtsCompositionForm(
    ArtsCompositionForm.ObjCreateParams objCreateParams,
    ArtsCompositionDataProvider dataProvider)
    : base(dataProvider)
  {
    this._objCreateParams = objCreateParams;
    this.InitializeComponent();
    this.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.InitializeData();
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="frmCaption">Заголовок формы</param>
  /// <param name="projDbObjId">Идентификатор конструкторской сборочной единицы</param>
  /// <param name="techDbObjId">Идентификатор версии технологического объекта (На данный момент ТП)</param>
  /// <param name="objCreateParams">Параметры создания объектов</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  /// <returns>Результат вызова формы</returns>
  public static DialogResult Execute(
    string frmCaption,
    long projDbObjId,
    long techDbObjId,
    ArtsCompositionForm.ObjCreateParams objCreateParams,
    System.IServiceProvider viewServices)
  {
    ArtsCompositionBaseForm.PluginsService = ArtsCompositionBaseForm.PluginsService ?? ServiceUtils.GetService<IClientPluginsService>((object) ApplicationServices.Container, false);
    ArtsCompositionBaseForm.FiltrationService = ArtsCompositionBaseForm.FiltrationService ?? ServiceUtils.GetService<IFiltrationService>((object) ApplicationServices.Container, false);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(projDbObjId, false);
      if (dbObject == null)
        return DialogResult.Cancel;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new List<int>((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes));
      childrenIdRecursive.AddRange((IEnumerable<int>) TechCardConsts.ObjectTypes.TechArtCompositionTypes);
      if (childrenIdRecursive.IndexOf(dbObject.ObjectType) < 0)
        return DialogResult.Cancel;
    }
    IArtsCompositionParams settings = (IArtsCompositionParams) null;
    ServiceUtils.GetService<IArtsCompositionParamsService>((object) ApplicationServices.Container, false)?.LoadSettings(out settings);
    ArtsCompositionDataProvider dataProvider = new ArtsCompositionDataProvider((AsyncTaskBase<ObjInfoItem, DataTable>) new AsyncTask<ObjInfoItem, DataTable>((IAsyncTaskAction<ObjInfoItem, DataTable>) new ArtsCompositionTaskActionDesign(ArtsCompositionDataProvider.PluginData.AddContexts, SearchDirection.RecursiveContains)
    {
      ObjectGrouping = ((settings != null ? (int) settings.DesignQuantityMode : 0) == 0)
    }, SynchronizationContext.Current), (AsyncTaskBase<ObjInfoItem, DataTable>) new AsyncTask<ObjInfoItem, DataTable>((IAsyncTaskAction<ObjInfoItem, DataTable>) new ArtsCompositionTaskActionTechProc(ArtsCompositionDataProvider.PluginData.AddContexts2), SynchronizationContext.Current));
    using (ArtsCompositionForm artsCompositionForm = new ArtsCompositionForm(objCreateParams, dataProvider))
    {
      if (!artsCompositionForm.Initialize(projDbObjId, techDbObjId, viewServices))
        return DialogResult.Abort;
      artsCompositionForm.Text = frmCaption != string.Empty ? frmCaption : LocalizationHolder.rm.GetString("TechCard.Client_95");
      return artsCompositionForm.ShowDialog();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <returns></returns>
  public NodeColumnCollection GetSupportedColumnsEventHandler(object sender)
  {
    NodeColumnCollection navigatorColumns = Intermech.Navigator.Utils.GetNavigatorColumns(sender);
    NodeColumnCollection columnCollection = ArtsCompositionColumnScheme.GetColumnCollection();
    if (columnCollection != null && navigatorColumns != null)
      navigatorColumns.AddRange((IEnumerable<NodeColumn>) columnCollection);
    return navigatorColumns;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private async void ArtsCompositionForm_Shown(object sender, EventArgs e)
  {
    if (!this._objCreateParams.NeedAddCount)
      return;
    await Task.Run(new Action(((ArtsCompositionBaseForm) this).StartLoadData)).ConfigureAwait(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    this.AddSelectedItems(this.GetSelectedItems());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e) => this.Close();

  /// <summary>Закрытие формы</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void ArtsCompositionCreatorForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (e.CloseReason != CloseReason.UserClosing)
      return;
    this.CancelLoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ArtsCompositionForm_Load(object sender, EventArgs e)
  {
    this._techNavControl.LoadLayout((IDictionary) this._formSettings);
  }

  /// <summary>Сохраним настройки формы в настройках пользователя</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ArtsCompositionCreatorForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.SaveSettings(true);
  }

  /// <summary>Изменился контекст состава в дереве КСЕ</summary>
  /// <param name="sender"></param>
  /// <param name="e">Параметры</param>
  private void cbContext_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.tscbContext.SelectedIndex < 0 || !(this.tscbContext.Items[this.tscbContext.SelectedIndex] is MyElement myElement))
      return;
    ArtsCompositionDataProvider.PluginData.AddContexts[1] = (long) myElement.Value;
    this.DoRefreshComposition((object) this, (EventArgs) null);
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Обработаем событие double click на контроле закладки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechNavControlDoubleClickEvent(object sender, TechNavigatorEventArgs e)
  {
    if (!(this._techNavControl.ActiveViewPage?.Control is ChildrenView) || !this.AddSelectedItems(this.GetSelectedItems()) || this._objCreateParams == null || this._objCreateParams.CreateMode != ArtsCompositionForm.ObjCreateParams.ObjCreateMode.Replace)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  /// <summary>Двойной клик мышью в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TreeMouseDoubleClick(object sender, EventArgs e)
  {
    if (this._techNavControl.TreeView == null || !this.AddSelectedItems(this._techNavControl.TreeView.SelectedItems) || this._objCreateParams == null || this._objCreateParams.CreateMode != ArtsCompositionForm.ObjCreateParams.ObjCreateMode.Replace)
      return;
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  /// <summary>Обработка события на изменения активной закладки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechActiveViewPageChanged(object sender, EventArgs e)
  {
    if (!(this._techNavControl.ActiveViewPage?.Control is ChildrenView control))
      return;
    control.DisableIMContextMenu = true;
    control.ShowCustomContextMenu -= new EventHandler<ContextMenuEventArgs>(this.DoShowViewContextMenu);
    control.ShowCustomContextMenu += new EventHandler<ContextMenuEventArgs>(this.DoShowViewContextMenu);
  }

  /// <summary>
  /// Отображение пользовательского меню для дерева навигатора
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DoShowTreeContextMenu(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right || e.Y < this._techNavControl.TreeView.HeaderHeight)
      return;
    ContextMenuStrip cmsArticles = this.cmsArticles;
    if (cmsArticles == null)
      return;
    if (cmsArticles.Visible)
      cmsArticles.Close();
    cmsArticles.Show((Control) this._techNavControl.TreeView, e.Location);
  }

  /// <summary>
  /// Отображение пользовательского меню для закладок навигатора
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DoShowViewContextMenu(object sender, ContextMenuEventArgs e)
  {
    ContextMenuStrip cmsArticles = this.cmsArticles;
    if (cmsArticles == null)
      return;
    if (cmsArticles.Visible)
      cmsArticles.Close();
    cmsArticles.Show(e.Control, e.Location);
  }

  /// <summary>Открытие контекстного меню</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmsArticles_Opening(object sender, CancelEventArgs e)
  {
    ISelectedItems selectedItems = this.GetSelectedItems();
    this.tsmiArticleAdd.Enabled = this._objCreateParams != null && this._objCreateParams.CreateMode == ArtsCompositionForm.ObjCreateParams.ObjCreateMode.Add && selectedItems != null && selectedItems.Count != 0;
  }

  /// <summary>Команда "Добавить"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiArticleAdd_Click(object sender, EventArgs e)
  {
    this.AddSelectedItems(this.GetSelectedItems());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiArticleRefresh_Click(object sender, EventArgs e)
  {
    this.DoRefreshComposition((object) this, (EventArgs) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="fillDataTableEventArgs"></param>
  private void VirtualColumnProviderOnFillDataTableEvent(
    object sender,
    FillDataTableEventArgs fillDataTableEventArgs)
  {
    DataTable dataTable = fillDataTableEventArgs?.DataTable;
    if (dataTable == null)
      return;
    int idxColumnPrjLinkId = ((IEnumerable<object>) fillDataTableEventArgs.Mapping.Fields).IndexOfFirst<object>((Predicate<object>) (item => item is NodeColumnID nodeColumnId1 && nodeColumnId1.ID.Equals((object) ObligatoryObjectAttributes.F_PRJLINK_ID)));
    int idxColumnObjectId = ((IEnumerable<object>) fillDataTableEventArgs.Mapping.Fields).IndexOfFirst<object>((Predicate<object>) (item => item is NodeColumnID nodeColumnId2 && nodeColumnId2.ID.Equals((object) ObligatoryObjectAttributes.F_OBJECT_ID)));
    if (idxColumnObjectId == -1)
      return;
    this.UpdateVirtualDataTable(idxColumnPrjLinkId, idxColumnObjectId, dataTable, false);
    this._virtualDataTables.Add(new Tuple<int, int, DataTable>(idxColumnPrjLinkId, idxColumnObjectId, dataTable));
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateVirtualDataTables(bool forceMode = false)
  {
    foreach (Tuple<int, int, DataTable> virtualDataTable in (IEnumerable<Tuple<int, int, DataTable>>) this._virtualDataTables)
      this.UpdateVirtualDataTable(virtualDataTable.Item1, virtualDataTable.Item2, virtualDataTable.Item3, forceMode);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="idxColumnObjectId"></param>
  /// <param name="dataTable"></param>
  /// <param name="forceMode"></param>
  private void UpdateVirtualDataTable(
    int idxColumnPrjLinkId,
    int idxColumnObjectId,
    DataTable dataTable,
    bool forceMode)
  {
    int columnIndex1 = dataTable.Columns.IndexOf(ArtsCompositionVirtualColumnProvider.VirtualColumnCountTech.FieldName);
    int columnIndex2 = dataTable.Columns.IndexOf(ArtsCompositionVirtualColumnProvider.VirtualColumnCountArt.FieldName);
    int columnIndex3 = dataTable.Columns.IndexOf(ArtsCompositionVirtualColumnProvider.VirtualColumnCountRemain.FieldName);
    int columnIndex4 = dataTable.Columns.IndexOf(ArtsCompositionVirtualColumnProvider.VirtualColumnItemStatus.FieldName);
    string str = string.Empty;
    if (this._objCreateParams.NeedAddCount && this._elemQtyList.Count == 0)
      str = (string) null;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long relationId = idxColumnPrjLinkId != -1 ? Math.Abs(DataSetProcessor.GetInt64Value(row[idxColumnPrjLinkId], 0L)) : 0L;
      long objectId = Math.Abs(DataSetProcessor.GetInt64Value(row[idxColumnObjectId], 0L));
      ElementQuantity versionQty = (ElementQuantity) null;
      if (relationId != 0L)
        this._elemQtyList.TryGetValue(new RelObjInfoItem(relationId), out versionQty);
      if (versionQty == null)
        versionQty = this._elemQtyList.GetValuesByObject(new ObjInfoItem(objectId)).FirstOrDefault<ElementQuantity>();
      if (columnIndex1 != -1)
      {
        if (!(row[columnIndex1] is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue == NodeDelayedValue.EmptyValue)
          nodeDelayedValue = new NodeDelayedValue((object) str);
        if (Convert.ToString(nodeDelayedValue.Value).IsEmpty() | forceMode)
          nodeDelayedValue.Value = versionQty != null ? (object) versionQty.TechQuantity ?? (object) string.Empty : (object) str;
        row[columnIndex1] = (object) nodeDelayedValue;
      }
      if (columnIndex2 != -1)
      {
        if (!(row[columnIndex2] is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue == NodeDelayedValue.EmptyValue)
          nodeDelayedValue = new NodeDelayedValue((object) str);
        if (Convert.ToString(nodeDelayedValue.Value).IsEmpty() | forceMode)
          nodeDelayedValue.Value = versionQty != null ? (object) versionQty.DesignQuantity ?? (object) string.Empty : (object) str;
        row[columnIndex2] = (object) nodeDelayedValue;
      }
      if (columnIndex3 != -1)
      {
        if (!(row[columnIndex3] is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue == NodeDelayedValue.EmptyValue)
          nodeDelayedValue = new NodeDelayedValue((object) str);
        if (Convert.ToString(nodeDelayedValue.Value).IsEmpty() | forceMode)
          nodeDelayedValue.Value = versionQty != null ? (object) versionQty.RemainQuantity ?? (object) string.Empty : (object) str;
        row[columnIndex3] = (object) nodeDelayedValue;
      }
      if (columnIndex4 != -1)
      {
        if (!(row[columnIndex4] is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue == NodeDelayedValue.EmptyValue)
          nodeDelayedValue = (NodeDelayedValue) new NodeDelayedEnumValue();
        if (nodeDelayedValue.Value == null | forceMode && versionQty != null)
        {
          ObjInfoIDItem currentInfoIdItem = versionQty.TypedInfoItem is RelObjInfoItem typedInfoItem1 ? typedInfoItem1.PartInfo as ObjInfoIDItem : versionQty.TypedInfoItem as ObjInfoIDItem;
          nodeDelayedValue.Value = (object) ArtsCompositionItemStatusHelper.CalcStatus(versionQty, !((TypedInfoItem) currentInfoIdItem != (TypedInfoItem) null) || versionQty.TechQuantity != null ? (ElementQuantity) null : this._elemQtyList.Values.FirstOrDefault<ElementQuantity>((System.Func<ElementQuantity, bool>) (item => (item.TypedInfoItem is RelObjInfoItem typedInfoItem2 ? (ObjInfoIDItem) typedInfoItem2.PartInfo : (ObjInfoIDItem) item.TypedInfoItem).ID == currentInfoIdItem.ID && item.TechQuantity != null)));
        }
        row[columnIndex4] = (object) nodeDelayedValue;
      }
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      BarManager service = ServiceUtils.GetService<BarManager>((object) ApplicationServices.Container, false);
      if (service != null)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ArtsCompositionForm));
    this.imagesToolbars = new ImageList(this.components);
    this.panelPage1 = new Panel();
    this.pnlClient = new Panel();
    this.pnlProgress = new Panel();
    this.lblProgressInfo = new Label();
    this.prgbarProgress = new ProgressBar();
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.pnlTop = new Panel();
    this.pnlTopHeader = new Panel();
    this.lblCaption = new Label();
    this.pictCaption = new PictureBox();
    this.tsMain = new ToolStrip();
    this.tslblContext = new ToolStripLabel();
    this.tscbContext = new ToolStripComboBox();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.contextMenuComposition = new ContextMenuBarItem();
    this.mnpAddToCC = new MenuButtonItem();
    this.mnpRefresh = new MenuButtonItem();
    this.menuButtonItem1 = new MenuButtonItem();
    this.cmsArticles = new ContextMenuStrip(this.components);
    this.tsmiArticleAdd = new ToolStripMenuItem();
    this.tsmiArticleSep1 = new ToolStripSeparator();
    this.tsmiArticleRefresh = new ToolStripMenuItem();
    this.panelPage1.SuspendLayout();
    this.pnlProgress.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.pnlTop.SuspendLayout();
    this.pnlTopHeader.SuspendLayout();
    ((ISupportInitialize) this.pictCaption).BeginInit();
    this.tsMain.SuspendLayout();
    this.cmsArticles.SuspendLayout();
    this.SuspendLayout();
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "arrow_left_blue.ico");
    this.imagesToolbars.Images.SetKeyName(2, "refresh.ico");
    this.panelPage1.Controls.Add((Control) this.pnlClient);
    this.panelPage1.Controls.Add((Control) this.pnlProgress);
    this.panelPage1.Controls.Add((Control) this.pnlButtons);
    this.panelPage1.Controls.Add((Control) this.pnlTop);
    componentResourceManager.ApplyResources((object) this.panelPage1, "panelPage1");
    this.panelPage1.Name = "panelPage1";
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    this.pnlProgress.Controls.Add((Control) this.lblProgressInfo);
    this.pnlProgress.Controls.Add((Control) this.prgbarProgress);
    componentResourceManager.ApplyResources((object) this.pnlProgress, "pnlProgress");
    this.pnlProgress.Name = "pnlProgress";
    componentResourceManager.ApplyResources((object) this.lblProgressInfo, "lblProgressInfo");
    this.lblProgressInfo.Name = "lblProgressInfo";
    componentResourceManager.ApplyResources((object) this.prgbarProgress, "prgbarProgress");
    this.prgbarProgress.Name = "prgbarProgress";
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.CausesValidation = false;
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    componentResourceManager.ApplyResources((object) this.pnlTop, "pnlTop");
    this.pnlTop.Controls.Add((Control) this.pnlTopHeader);
    this.pnlTop.Controls.Add((Control) this.tsMain);
    this.pnlTop.Name = "pnlTop";
    this.pnlTopHeader.Controls.Add((Control) this.lblCaption);
    this.pnlTopHeader.Controls.Add((Control) this.pictCaption);
    componentResourceManager.ApplyResources((object) this.pnlTopHeader, "pnlTopHeader");
    this.pnlTopHeader.Name = "pnlTopHeader";
    componentResourceManager.ApplyResources((object) this.lblCaption, "lblCaption");
    this.lblCaption.ForeColor = SystemColors.GrayText;
    this.lblCaption.Name = "lblCaption";
    componentResourceManager.ApplyResources((object) this.pictCaption, "pictCaption");
    this.pictCaption.Name = "pictCaption";
    this.pictCaption.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tsMain, "tsMain");
    this.tsMain.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tslblContext,
      (ToolStripItem) this.tscbContext,
      (ToolStripItem) this.toolStripSeparator1
    });
    this.tsMain.Name = "tsMain";
    this.tslblContext.Name = "tslblContext";
    componentResourceManager.ApplyResources((object) this.tslblContext, "tslblContext");
    this.tscbContext.DropDownStyle = ComboBoxStyle.DropDownList;
    this.tscbContext.Name = "tscbContext";
    componentResourceManager.ApplyResources((object) this.tscbContext, "tscbContext");
    this.tscbContext.SelectedIndexChanged += new EventHandler(this.cbContext_SelectedIndexChanged);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    componentResourceManager.ApplyResources((object) this.contextMenuComposition, "contextMenuComposition");
    this.contextMenuComposition.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.mnpAddToCC,
      (ToolbarItemBase) this.mnpRefresh,
      (ToolbarItemBase) this.menuButtonItem1
    });
    this.contextMenuComposition.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpAddToCC, "mnpAddToCC");
    this.mnpAddToCC.ImageIndex = 0;
    this.mnpAddToCC.ShowText = true;
    this.mnpRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpRefresh, "mnpRefresh");
    this.mnpRefresh.ImageIndex = 2;
    this.mnpRefresh.ShowText = true;
    componentResourceManager.ApplyResources((object) this.menuButtonItem1, "menuButtonItem1");
    this.menuButtonItem1.ShowText = true;
    this.cmsArticles.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsmiArticleAdd,
      (ToolStripItem) this.tsmiArticleSep1,
      (ToolStripItem) this.tsmiArticleRefresh
    });
    this.cmsArticles.Name = "cmsArticles";
    componentResourceManager.ApplyResources((object) this.cmsArticles, "cmsArticles");
    this.cmsArticles.Opening += new CancelEventHandler(this.cmsArticles_Opening);
    this.tsmiArticleAdd.Name = "tsmiArticleAdd";
    componentResourceManager.ApplyResources((object) this.tsmiArticleAdd, "tsmiArticleAdd");
    this.tsmiArticleAdd.Click += new EventHandler(this.tsmiArticleAdd_Click);
    this.tsmiArticleSep1.Name = "tsmiArticleSep1";
    componentResourceManager.ApplyResources((object) this.tsmiArticleSep1, "tsmiArticleSep1");
    this.tsmiArticleRefresh.Name = "tsmiArticleRefresh";
    componentResourceManager.ApplyResources((object) this.tsmiArticleRefresh, "tsmiArticleRefresh");
    this.tsmiArticleRefresh.Click += new EventHandler(this.tsmiArticleRefresh_Click);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panelPage1);
    this.Name = nameof (ArtsCompositionForm);
    this.FormClosing += new FormClosingEventHandler(this.ArtsCompositionCreatorForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.ArtsCompositionCreatorForm_FormClosed);
    this.Load += new EventHandler(this.ArtsCompositionForm_Load);
    this.Shown += new EventHandler(this.ArtsCompositionForm_Shown);
    this.panelPage1.ResumeLayout(false);
    this.panelPage1.PerformLayout();
    this.pnlProgress.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.pnlTop.ResumeLayout(false);
    this.pnlTop.PerformLayout();
    this.pnlTopHeader.ResumeLayout(false);
    ((ISupportInitialize) this.pictCaption).EndInit();
    this.tsMain.ResumeLayout(false);
    this.tsMain.PerformLayout();
    this.cmsArticles.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Параметры создания объектов</summary>
  public class ObjCreateParams
  {
    /// <summary>Описание родительского объекта ТП</summary>
    public readonly IDBTypedObjectID ProjTechObj;
    /// <summary>Делегат добавления изделия</summary>
    public readonly ArtsCompositionForm.AddArticleMethod AddCallBack;
    /// <summary>Признак добавления изделий с количеством</summary>
    public readonly bool NeedAddCount;
    /// <summary>Режим создания объекта</summary>
    public readonly ArtsCompositionForm.ObjCreateParams.ObjCreateMode CreateMode;

    /// <summary>Конструктор</summary>
    /// <param name="projTechObj">Описание родительского объекта ТП</param>
    /// <param name="needAddCount">Признак добавления изделий с количеством</param>
    /// <param name="addCallBack">Делегат добавления изделия</param>
    public ObjCreateParams(
      IDBTypedObjectID projTechObj,
      bool needAddCount,
      ArtsCompositionForm.AddArticleMethod addCallBack)
      : this(projTechObj, needAddCount, ArtsCompositionForm.ObjCreateParams.ObjCreateMode.Add, addCallBack)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="projTechObj">Описание родительского объекта ТП</param>
    /// <param name="needAddCount">Признак добавления изделий с количеством</param>
    /// <param name="createMode">Режим создания объекта</param>
    /// <param name="addCallBack">Делегат добавления изделия</param>
    public ObjCreateParams(
      IDBTypedObjectID projTechObj,
      bool needAddCount,
      ArtsCompositionForm.ObjCreateParams.ObjCreateMode createMode,
      ArtsCompositionForm.AddArticleMethod addCallBack)
    {
      this.ProjTechObj = projTechObj;
      this.NeedAddCount = needAddCount;
      this.CreateMode = createMode;
      this.AddCallBack = addCallBack;
    }

    /// <summary>Режимы добавления объектов</summary>
    public enum ObjCreateMode
    {
      /// <summary>Создать объект</summary>
      Add,
      /// <summary>Заменить объект</summary>
      Replace,
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private class ElementQuantityList : ConcurrentDictionary<RelObjInfoItem, ElementQuantity>
  {
    /// <summary>
    /// Получение всех значений для текущего дочернего объекта
    /// </summary>
    /// <param name="objInfoItem"></param>
    /// <returns></returns>
    public IEnumerable<ElementQuantity> GetValuesByObject(ObjInfoItem objInfoItem)
    {
      foreach (KeyValuePair<RelObjInfoItem, ElementQuantity> keyValuePair in (ConcurrentDictionary<RelObjInfoItem, ElementQuantity>) this)
      {
        if ((TypedInfoItem) keyValuePair.Key.PartInfo == (TypedInfoItem) objInfoItem)
          yield return keyValuePair.Value;
      }
    }
  }

  /// <summary>Делегат добавления изделия</summary>
  /// <param name="projTechObj">Описание род. объекта ТП</param>
  /// <param name="artInfo">Описание изделия</param>
  /// <param name="objCreated">Список созданных изделий и их количество</param>
  /// <returns></returns>
  public delegate bool AddArticleMethod(
    IDBTypedObjectID projTechObj,
    ArtsCompositionsUtils.ArticleItemInfo artInfo,
    out List<ArtsCompositionsUtils.ArticleCreatedItem> objCreated);
}
