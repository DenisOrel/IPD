// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CreateByAnalog.CreateByAnalogObjectWizard
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.MRP2;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using Intermech.TechCard.Client.UI.Forms;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.CreateByAnalog;

public class CreateByAnalogObjectWizard : WizardForm, IIOSource, IIODestination
{
  /// <summary>Тип создаваемых объектов</summary>
  private readonly int _createObjectTypeId;
  /// <summary>Описание объекта "Производственная ведомость"</summary>
  private readonly ObjInfoItem _productionReportItem;
  /// <summary>
  /// Список объектов типа "Производственные копии деталей" для которых возможно создание объектов
  /// </summary>
  private readonly IEnumerable<ObjInfoIDItem> _articleCopyInfoItems;
  /// <summary>
  /// Список идентификаторов объектов (не версий) для которых возможно создание объектов
  /// </summary>
  private IEnumerable<long> _articleCopyIds;
  /// <summary>
  /// 
  /// </summary>
  private readonly IIODispatcher _ioDispatcher = (IIODispatcher) new IODispatcher();
  /// <summary>
  /// 
  /// </summary>
  private readonly IAdvancedServiceContainer _serviceContainer = (IAdvancedServiceContainer) new AdvancedServiceContainer();
  /// <summary>Содержимое ПВ-аналога</summary>
  private DataTable _productionReportAnalogData;
  /// <summary>
  /// Кэш вида ид. ПК ДСЕ -&gt; ссылка на ДСЕ для объектов ПВ-аналога
  /// </summary>
  private readonly IDictionary<long, ObjInfoIDItem> _productionReportAnalogItem2LinkCache = (IDictionary<long, ObjInfoIDItem>) new Dictionary<long, ObjInfoIDItem>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Инициализация "прочих" пользовательских контролов</summary>
  private void InitializeCustomComponent()
  {
    this._ioDispatcher.RegisterDestination((IIODestination) this);
    this._serviceContainer.AddService<IIODispatcher>(this._ioDispatcher);
    SelectObjectNavListPageControl navListPageControl = new SelectObjectNavListPageControl()
    {
      ObjectTypeId = MRP2Consts.objtypeIdProductionLists
    };
    navListPageControl.Caption = navListPageControl.Description = "Выберите производственную ведомость - аналог";
    navListPageControl.LoadPageControlData += new LoadPageControlEventHandler(this.selectArticlePage_LoadPageControlData);
    this.Pages.Add((IWizardPage) navListPageControl);
    SelectObjectTreeViewPageControl treeViewPageControl = new SelectObjectTreeViewPageControl()
    {
      ObjectTypeId = MRP2Consts.objtypeIdProductionObjects,
      ItemsMode = SelectedItemsMode.CheckedItems
    };
    treeViewPageControl.Caption = treeViewPageControl.Description = "Выберите объект ПК ДСЕ для создания объектов по аналогу";
    treeViewPageControl.LoadPageControlData += new LoadPageControlEventHandler(this.selectObjectFromProductionReportComposition_LoadPageControlData);
    treeViewPageControl.TreeViewControl.CheckoutMode = TechCheckoutMode.Manual;
    treeViewPageControl.TreeViewControl.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
    treeViewPageControl.TreeViewControl.AfterCreateNode += new EventHandler<NodeEventArgs>(this.TreeViewNode_AfterCreateEvent);
    treeViewPageControl.TreeViewControl.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.TreeViewNode_CheckStateChangingEvent);
    treeViewPageControl.TreeViewControl.CheckStateChanged += new EventHandler<NodeEventArgs>(this.TreeViewNode_CheckStateChangedEvent);
    TechRouteFilter service = new TechRouteFilter((NavigatorTreeView) treeViewPageControl.TreeViewControl)
    {
      FilterState = TechRouteFilterState.trfEnabled
    };
    treeViewPageControl.ServiceContainer.AddService<INavigatorVirtualColumnProvider>((INavigatorVirtualColumnProvider) service);
    CreateByAnalogObjectOptionsControl createByAnalogObjectOptionsControl = new CreateByAnalogObjectOptionsControl();
    createByAnalogObjectOptionsControl.Dock = DockStyle.Bottom;
    createByAnalogObjectOptionsControl.Options = this.Options;
    createByAnalogObjectOptionsControl.OptionsChanged += (EventHandler) ((sender, args) => this.Options = createByAnalogObjectOptionsControl.Options);
    treeViewPageControl.Controls.Add((System.Windows.Forms.Control) createByAnalogObjectOptionsControl);
    createByAnalogObjectOptionsControl.Dock = DockStyle.Bottom;
    createByAnalogObjectOptionsControl.SendToBack();
    this.Pages.Add((IWizardPage) treeViewPageControl);
  }

  /// <summary>
  /// Получение информации о ПВ - аналогах из соответствующего атрибута
  /// </summary>
  /// <param name="session"></param>
  /// <param name="analogInfoItems"></param>
  /// <returns></returns>
  private bool LoadProductionReportAnalogs(
    IUserSession session,
    out IList<ObjInfoItem> analogInfoItems)
  {
    analogInfoItems = (IList<ObjInfoItem>) null;
    IDBAttribute objectAttributeById = session.GetObjectAttributeByID(this._productionReportItem.ObjectID, TechCardConsts.AttributeTypes.ProductionReportAnalogID);
    if (objectAttributeById?.Values == null)
      return false;
    analogInfoItems = (IList<ObjInfoItem>) new List<ObjInfoItem>();
    for (int index = 0; index < objectAttributeById.ValuesCount; ++index)
    {
      objectAttributeById.Index = index;
      analogInfoItems.Add(new ObjInfoItem(objectAttributeById.AsInteger));
      analogInfoItems.Add(new ObjInfoItem(-objectAttributeById.AsInteger));
    }
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) analogInfoItems, session);
    return analogInfoItems.Any<ObjInfoItem>();
  }

  /// <summary>Поиск ПВ - аналогов по выходным сборкам</summary>
  /// <param name="session"></param>
  /// <param name="analogInfoItems"></param>
  /// <returns></returns>
  private bool SearchProductionReportAnalogs(
    IUserSession session,
    out IList<ObjInfoItem> analogInfoItems,
    out string errorMessage)
  {
    analogInfoItems = (IList<ObjInfoItem>) new List<ObjInfoItem>();
    errorMessage = string.Empty;
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    IList<ObjInfoItem> objInfoItemList1 = (IList<ObjInfoItem>) new List<ObjInfoItem>();
    IList<long> longList = (IList<long>) new List<long>();
    IList<ColumnDescriptor> columns = (IList<ColumnDescriptor>) new List<ColumnDescriptor>(ObjInfoDbScheme.GetSourceTableColumns());
    columns.Add(new ColumnDescriptor((object) MRP2Consts.attrIdArticleLink, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    CompositionLoadingParams loadingParams1 = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      this._productionReportItem
    }, (IEnumerable<int>) new int[1]
    {
      MRP2Consts.objtypeIdExitAssembly
    }, (IEnumerable<int>) new int[0], (IEnumerable<int>) new int[1]
    {
      MRP2Consts.reltypeIdProductComposition
    }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, true, false, 1, (VersionsRule) null, VersionsRuleSources.GetCurrentWindowRule().OwnerId);
    DataTable source1 = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams1);
    if (new ObjInfoDbScheme().ParseItems(source1 != null ? (IEnumerable<DataRow>) source1.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) objInfoItemList1))
      longList.AddRange<long>((IEnumerable<long>) source1.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, 2, 0L))).Where<long>((System.Func<long, bool>) (objectId => objectId != 0L)));
    if (!objInfoItemList1.Any<ObjInfoItem>())
    {
      errorMessage = "Для текущей производственной ведомости не найдены выходные сборки";
      return false;
    }
    List<ObjInfoIDItem> list1 = longList.Select<long, ObjInfoIDItem>((System.Func<long, ObjInfoIDItem>) (objectId => new ObjInfoIDItem(objectId))).ToList<ObjInfoIDItem>();
    List<ObjInfoIDItem> list2 = ServiceUtils.GetService<ITypedInfoService>((object) session, true).UpdateUnknownInfo((IEnumerable<ObjInfoItem>) list1, (object) session.SessionGUID).Select<ObjInfoItem, ObjInfoIDItem>((System.Func<ObjInfoItem, ObjInfoIDItem>) (item => item as ObjInfoIDItem)).ToList<ObjInfoIDItem>();
    IList<ColumnDescriptor> source2 = (IList<ColumnDescriptor>) new List<ColumnDescriptor>(ObjInfoDbScheme.GetSourceTableColumns());
    ConditionStructure[] conditions1 = new ConditionStructure[1]
    {
      new ConditionStructure(-3, RelationalOperators.In, (object) list2.Select<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (item => item.ID)).ToArray<long>(), (object) null, LogicalOperators.NONE, 0, false)
    };
    IDBObjectCollection objectCollection = session.GetObjectCollection(-1);
    objectCollection.LocalTypesMode = true;
    objectCollection.ShowAllModifications = true;
    DataTable source3 = objectCollection.Select(source2.ToArray<ColumnDescriptor>(), conditions1);
    List<ObjInfoItem> objInfoItemList2 = new List<ObjInfoItem>();
    new ObjInfoDbScheme().ParseItems(source3 != null ? (IEnumerable<DataRow>) source3.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) objInfoItemList2);
    IList<ObjInfoItem> objInfoItemList3 = (IList<ObjInfoItem>) new List<ObjInfoItem>();
    if (objInfoItemList2.Any<ObjInfoItem>())
    {
      DBRecordSetParams dbRsp = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(MRP2Consts.attrIdArticleLink, RelationalOperators.In, (object) objInfoItemList2.Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (item => Math.Abs(item.ObjectID))).ToArray<long>(), (object) null, LogicalOperators.NONE, 0, false)
      }, ObjInfoDbScheme.GetSourceTableColumns().ToArray<ColumnDescriptor>());
      DataTable objectDataEx = DataHelper.GetObjectDataEx((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MRP2Consts.objtypeIdExitAssembly), session, dbRsp, (IEnumerable<ObjInfoItem>) null);
      new ObjInfoDbScheme().ParseItems(objectDataEx != null ? (IEnumerable<DataRow>) objectDataEx.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) objInfoItemList3);
    }
    else
      objInfoItemList3.AddRange<ObjInfoItem>((IEnumerable<ObjInfoItem>) objInfoItemList1);
    if (!objInfoItemList3.Any<ObjInfoItem>())
    {
      errorMessage = "Для текущей производственной ведомости не найдены выходные сборки - аналоги";
      return false;
    }
    ConditionStructure[] conditions2 = new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.NotEqual, (object) this._productionReportItem.ObjectID, (object) null, LogicalOperators.NONE, 0, false)
    };
    CompositionLoadingParams loadingParams2 = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) objInfoItemList3, (IEnumerable<int>) new int[1]
    {
      MRP2Consts.objtypeIdProductionLists
    }, (IEnumerable<int>) new int[0], (IEnumerable<int>) new int[1]
    {
      MRP2Consts.reltypeIdProductComposition
    }, ObjInfoDbScheme.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) conditions2, false, false, 1, (VersionsRule) null, "cad001e0-306c-11d8-b4e9-00304f19f545");
    DataTable source4 = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams2);
    new ObjInfoDbScheme().ParseItems(source4 != null ? (IEnumerable<DataRow>) source4.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<ObjInfoItem>) analogInfoItems);
    if (analogInfoItems.Any<ObjInfoItem>())
      return true;
    errorMessage = "Для текущей производственной ведомости не найдены ведомости - аналоги";
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  public CreateByAnalogObjectWizard()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomComponent();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createObjectTypeId">Тип создаваемых объектов</param>
  /// <param name="articleCopyInfoItems">Список объектов типа "Производственные копии деталей" для которых возможно создание объектов</param>
  public CreateByAnalogObjectWizard(
    int createObjectTypeId,
    ObjInfoItem productionReportInfoItem,
    IEnumerable<ObjInfoIDItem> articleCopyInfoItems)
    : this()
  {
    this._createObjectTypeId = createObjectTypeId;
    this._productionReportItem = productionReportInfoItem;
    this._articleCopyInfoItems = articleCopyInfoItems;
  }

  /// <summary>
  /// 
  /// </summary>
  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyDown | IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ioEvent"></param>
  /// <returns></returns>
  public bool ProcessEvent(IIOEvent ioEvent)
  {
    if (ioEvent == null || this.Pages.Last<IWizardPage>() != this.ActivePage || !(ioEvent.Source.Control is System.Windows.Forms.Control control) || this.ActivePage.Control != control && !this.ActivePage.Control.Controls.Contains(control))
      return false;
    ioEvent = (IIOEvent) new IOEvent((IIOSource) this, ioEvent.EventFlags, ioEvent.EventType, ioEvent.EventData, ioEvent.Tag);
    IIODispatcher service = ServiceUtils.GetService<IIODispatcher>((object) this._serviceContainer.AdvancedProvider, false);
    if (service == null)
      return false;
    service.ProcessEvent(ioEvent);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  public object Control
  {
    get => (object) this;
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public System.IServiceProvider Services
  {
    get => (System.IServiceProvider) this._serviceContainer;
    set => this._serviceContainer.AdvancedProvider = value;
  }

  /// <summary>Выбранные элементы ПВ</summary>
  public ISelectedItems SelectedItems
  {
    get
    {
      if (this.ActivePage != this.Pages.Last<IWizardPage>() || !this.ActivePage.ReallyComplete)
        return (ISelectedItems) new EmptySelectedItems();
      return !(this.ActivePage is ISelectedItemsHost activePage) ? (ISelectedItems) null : activePage.SelectedItems;
    }
    set
    {
    }
  }

  /// <summary>Состав ПВ с технологическими данными</summary>
  public DataTable ProductionReportAnalogData => this._productionReportAnalogData;

  /// <summary>Опции команды</summary>
  internal CreateByAnalogObjectOptions Options { get; set; } = new CreateByAnalogObjectOptions();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreateByAnalogObjectWizard_Load(object sender, EventArgs e)
  {
    TechCardFormUtils.LoadSettings((System.Windows.Forms.Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CreateByAnalogObjectWizard_FormClosed(object sender, FormClosedEventArgs e)
  {
    TechCardFormUtils.SaveSettings((System.Windows.Forms.Control) this);
  }

  /// <summary>Загрузка содержимого закладки "ПВ-аналогов"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void selectArticlePage_LoadPageControlData(System.Windows.Forms.Control sender, LoadPageControlEventArgs e)
  {
    if (!(sender is SelectObjectNavListPageControl navListPageControl))
      return;
    e.DataLoaded = true;
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._productionReportItem))
      return;
    IList<ObjInfoItem> analogInfoItems = (IList<ObjInfoItem>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!this.LoadProductionReportAnalogs(sessionKeeper.Session, out analogInfoItems))
      {
        string caption = "Внимание";
        if (MessageBox.Show("Для текущей производственной ведомости не заданы ведомости-аналоги. Выполнить подбор аналогов по выходным сборкам ?", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        string errorMessage;
        if (!this.SearchProductionReportAnalogs(sessionKeeper.Session, out analogInfoItems, out errorMessage))
        {
          int num = (int) MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          return;
        }
      }
    }
    IDescriptor descriptor = (IDescriptor) new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MRP2Consts.objtypeIdProductionLists, "Производственные ведомости - аналоги", SomeTypedInfoHelper<ObjInfoItem>.GetItemTypeCache((IEnumerable<ObjInfoItem>) analogInfoItems))
    {
      ExpandNodes = false
    };
    navListPageControl.TechNavigatorControl.RootDescriptor = descriptor;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void selectObjectFromProductionReportComposition_LoadPageControlData(
    System.Windows.Forms.Control sender,
    LoadPageControlEventArgs e)
  {
    IDescriptor descriptor = (IDescriptor) null;
    if (e?.PreviousPage is ISelectedItemsHost previousPage)
    {
      List<ObjInfoItem> contextObjInfoItemList = new List<ObjInfoItem>();
      List<IDBTypedObjectID> result;
      previousPage.SelectedItems.TryGetItems<IDBTypedObjectID>(out result);
      if (result != null)
        result.InvokeForAll<IDBTypedObjectID>((Action<IDBTypedObjectID>) (item => contextObjInfoItemList.Add(new ObjInfoItem(item.ObjectID, item.ObjectType))));
      this._productionReportAnalogData = (DataTable) null;
      this._productionReportAnalogItem2LinkCache.Clear();
      if (contextObjInfoItemList.Any<ObjInfoItem>())
      {
        if (this._articleCopyIds == null)
        {
          IEnumerable<ObjInfoIDItem> articleCopyInfoItems = this._articleCopyInfoItems;
          this._articleCopyIds = (IEnumerable<long>) ((articleCopyInfoItems != null ? articleCopyInfoItems.Select<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (item => item.ID)).ToHashSet<long>() : (HashSet<long>) null) ?? new HashSet<long>());
        }
        ObjInfoItem objInfoItem = contextObjInfoItemList.First<ObjInfoItem>();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          this._productionReportAnalogData = CreateByAnalogObjectWizard.LoadProductionReportData(sessionKeeper.Session, new ObjInfoItem(objInfoItem.ObjectID, objInfoItem.ObjTypeID), new int[1]
          {
            this._createObjectTypeId
          });
          if (this._productionReportAnalogData != null)
          {
            this._productionReportAnalogData.AsEnumerable().InvokeForAll((Action<DataRow>) (row =>
            {
              long int64Value1 = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L);
              long int64Value2 = DataSetProcessor.GetInt64Value(row, "cadd9a8c-306c-11d8-b4e9-00304f19f545", 0L);
              if (int64Value2 == 0L)
                return;
              this._productionReportAnalogItem2LinkCache[int64Value1] = new ObjInfoIDItem(int64Value2);
            }));
            ObjInfoHelper.UpdateUnknownInfo((IEnumerable<ObjInfoItem>) this._productionReportAnalogItem2LinkCache.Values, sessionKeeper.Session);
          }
        }
        List<int> intList = new List<int>();
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MRP2Consts.objtypeIdProductionObjects));
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingID));
        intList.Add(this._createObjectTypeId);
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
        };
        int versionsObjectNode = Intermech.Navigator.Consts.CategoryVersionsObjectNode;
        int objTypeId = objInfoItem.ObjTypeID;
        long objectId = objInfoItem.ObjectID;
        int productionObjects = MRP2Consts.objtypeIdProductionObjects;
        int[] compRelTypeIDs = new int[2]
        {
          MRP2Consts.reltypeIdProductComposition,
          TechCardConsts.RelTypes.TechRelationID
        };
        string empty = string.Empty;
        TechCompositionConditionFilter compositionConditionFilter = new TechCompositionConditionFilter((IEnumerable<ConditionStructure>) conditions);
        compositionConditionFilter.QueryFilter = (IRelatedObjectQueryFilterMode) new RelatedObjectQueryFilterMode(filterDataByVersionRule: false);
        descriptor = (IDescriptor) new TechCompositionDescriptor(versionsObjectNode, objTypeId, objectId, productionObjects, (IEnumerable<int>) compRelTypeIDs, empty, RelatedObjectsRole.Composition, (ITechCompositionFilter) compositionConditionFilter, (IEnumerable<NodeColumnID>) null);
      }
    }
    IDescriptor rootDescriptor = descriptor ?? (IDescriptor) new TechCompositionDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.TechBaseObjectID, 0L, TechCardConsts.ObjectTypes.TechBaseObjectID, -1, string.Empty, RelatedObjectsRole.Composition, (ITechCompositionFilter) null);
    if (sender is SelectObjectTreeViewPageControl treeViewPageControl)
      treeViewPageControl.TreeViewControl?.Build(rootDescriptor);
    if (e == null)
      return;
    e.DataLoaded = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TreeViewNode_CheckStateChangedEvent(object sender, NodeEventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TreeViewNode_CheckStateChangingEvent(object sender, CheckStateEventArgs e)
  {
    if (e.OldValue != CheckState.Indeterminate || e.OldValue == e.NewValue)
      return;
    e.NewValue = e.OldValue;
    if (!(e.Node is TechcardNavTreeNode node))
      return;
    node.SetCheckStateInternal(e.OldValue);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TreeViewNode_AfterCreateEvent(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e?.Node;
    NavigatorTreeView navigatorTreeView = sender as NavigatorTreeView;
    if (node == null || navigatorTreeView == null || !(node is TechcardNavTreeNode treeNode))
      return;
    IDBTypedObjectID dbTypedObjId1;
    if (!TechcardClientControlsUtils.GetObjectInfo((NavigatorTreeNode) treeNode, out dbTypedObjId1) || dbTypedObjId1 == null || dbTypedObjId1.ObjectType != this._createObjectTypeId)
    {
      treeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
    }
    else
    {
      IDBTypedObjectID dbTypedObjId2;
      ObjInfoIDItem objInfoIdItem;
      if (!TechcardClientControlsUtils.GetObjectInfo(node.Parent?.Parent, out dbTypedObjId2) || !this._productionReportAnalogItem2LinkCache.TryGetValue(dbTypedObjId2.ObjectID, out objInfoIdItem))
      {
        treeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
      }
      else
      {
        if (this._articleCopyIds == null || this._articleCopyIds.Contains<long>(objInfoIdItem.ID))
          return;
        treeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
      }
    }
  }

  /// <summary>Загрузка содержимого ПВ</summary>
  /// <param name="keeperSession"></param>
  /// <param name="productionReportInfoItem"></param>
  /// <returns></returns>
  internal static DataTable LoadProductionReportData(
    [NotNull] IUserSession session,
    [NotNull] ObjInfoItem productionReportInfoItem,
    int[] objectTypedToExpand,
    IEnumerable<ColumnDescriptor> extraColumns = null)
  {
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    IList<ColumnDescriptor> columnDescriptorList = (IList<ColumnDescriptor>) new List<ColumnDescriptor>(RelObjInfoDbScheme<ObjInfoItem>.GetSourceTableColumns());
    columnDescriptorList.Add(new ColumnDescriptor((object) MRP2Consts.attrIdArticleLink, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    if (extraColumns != null)
      columnDescriptorList.AddRange<ColumnDescriptor>(extraColumns);
    HashSet<int> hashSet = new HashSet<int>();
    hashSet.AddRange<int>((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MRP2Consts.objtypeIdProductionObjects));
    hashSet.AddRange<int>((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingID));
    hashSet.AddRange<int>((IEnumerable<int>) objectTypedToExpand);
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      productionReportInfoItem
    }, (IEnumerable<int>) null, (IEnumerable<int>) hashSet.ToArray<int>(), (IEnumerable<int>) new int[2]
    {
      MRP2Consts.reltypeIdProductComposition,
      TechCardConsts.RelTypes.TechRelationID
    }, (IEnumerable<ColumnDescriptor>) columnDescriptorList, (IEnumerable<ConditionStructure>) null, true, false, -1, (VersionsRule) null, VersionsRuleSources.GetCurrentWindowRule().OwnerId);
    return service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
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
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(800, 450);
    this.Name = nameof (CreateByAnalogObjectWizard);
    this.ShowHeaderPanel = true;
    this.Text = nameof (CreateByAnalogObjectWizard);
    this.FormClosed += new FormClosedEventHandler(this.CreateByAnalogObjectWizard_FormClosed);
    this.Load += new EventHandler(this.CreateByAnalogObjectWizard_Load);
    this.ResumeLayout(false);
  }
}
