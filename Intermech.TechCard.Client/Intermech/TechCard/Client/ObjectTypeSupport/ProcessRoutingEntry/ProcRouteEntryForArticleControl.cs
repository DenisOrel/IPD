// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.ProcRouteEntryForArticleControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Resources;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;

/// <summary>
/// Редактор "атрибутов входимости" для объекта "Входимость маршрута обработки"
/// </summary>
public class ProcRouteEntryForArticleControl : UserControl
{
  /// <summary>
  /// 
  /// </summary>
  private bool _dataLoaded;
  /// <summary>Кэш применяемости для изделия</summary>
  private List<RelObjInfoItem> _projArticleInfoItems;
  /// <summary>
  /// 
  /// </summary>
  private readonly IServiceContainer _services = (IServiceContainer) new ServiceContainer();
  /// <summary>
  /// Словарь изделий для текущей входимости, с коллекцией всех версий
  /// </summary>
  private List<ObjInfoIDItem> _articleObjectItems;
  /// <summary>
  /// Список возможных заказов для текущей входимости (списка изделий)
  /// </summary>
  private IList<ObjInfoItem> _orderList;
  /// <summary>Состав выбранного заказа</summary>
  private List<RelObjInfoItem> _orderCompositionItems;
  /// <summary>Объект "Входимость маршрута обработки"</summary>
  private ProcRouteEntryObject _procRouteEntryObject = new ProcRouteEntryObject(-1L);
  /// <summary>Источник данных для построения дерева</summary>
  private IEnumerable<RelObjInfoItem> _treeBuildSource;
  /// <summary>Фоновая задача загрузки данных</summary>
  private BackgroundWorker _backgroundWorker;
  /// <summary>Выбранный режим фильтрации</summary>
  private int _assemblyTreeFilterMode = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pnlTop;
  private Label lbOrder;
  private Button btnSetOrder;
  private TextBox tbOrderCaption;
  private TechCardNavTreeViewControl techNavTreeViewArticleObjects;
  private CheckBox cbVersionMode;
  private ComboBox cbAssemblyFilterMode;
  private Label lbAssemblyFilterMode;
  private MenuBar menuBarTree;
  private ContextMenuBarItem contextMenuTree;
  private MenuButtonItem mbiCheckedAll;
  private MenuButtonItem mbiUncheckedAll;
  private MenuButtonItem mbiInvertChecked;
  private MenuButtonItem mbiOpenInNewWindow;
  private MenuButtonItem mbiProperty;
  private MenuButtonItem mbiSearch;

  /// <summary>
  /// 
  /// </summary>
  public ProcRouteEntryForArticleControl()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
    this.InitializeCustomSettings();
    this.InitializeIconContextMenu();
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    if (ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false) == null)
      return;
    this.techNavTreeViewArticleObjects.Services = (System.IServiceProvider) this._services;
    this.techNavTreeViewArticleObjects.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    this.techNavTreeViewArticleObjects.SetColumns(Intermech.Navigator.Utils.VersionColumns(NodeColumnSortOrder.Ascending, false), (IDescriptor) new TechObjectListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, TechCardConsts.ObjectTypes.ArticleBaseID, string.Empty, (IList) null));
    this._backgroundWorker = new BackgroundWorker()
    {
      WorkerReportsProgress = true,
      WorkerSupportsCancellation = true
    };
    this._backgroundWorker.DoWork += new DoWorkEventHandler(this.BackgroundWorker_DoWork);
    this._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
    foreach (AssemblyTreeFilterMode assemblyTreeFilterMode in Enum.GetValues(typeof (AssemblyTreeFilterMode)))
      this.cbAssemblyFilterMode.Items.Add((object) assemblyTreeFilterMode.GetDescription<AssemblyTreeFilterMode>());
  }

  private void InitializeCustomSettings() => this.LoadSettings();

  /// <summary>Инициализация иконок команд контекстных меню</summary>
  private void InitializeIconContextMenu()
  {
    if (!(ApplicationServices.Container.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    this.menuBarTree.ImageList = service.ImageList;
    this.mbiCheckedAll.ImageIndex = service.ImageIndex("imgChecked");
    this.mbiUncheckedAll.ImageIndex = service.ImageIndex("imgUnchecked");
  }

  /// <summary>Загрузка параметров контрола</summary>
  private void LoadSettings(string sectionName = null)
  {
    if (string.IsNullOrEmpty(sectionName))
      sectionName = typeof (ProcRouteEntryForArticleControl).ToString();
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(sectionName);
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this.techNavTreeViewArticleObjects);
    AssemblyTreeFilterMode result;
    if (config != null && config.HasProperty(this.techNavTreeViewArticleObjects.Name + "_AssemblyFilterMode") && Enum.TryParse<AssemblyTreeFilterMode>(config.GetProperty(this.techNavTreeViewArticleObjects.Name + "_AssemblyFilterMode"), out result))
      this._assemblyTreeFilterMode = (int) result;
    this.cbAssemblyFilterMode.SelectedItem = (object) this.AssemblyFilterMode.GetDescription<AssemblyTreeFilterMode>();
    this.cbAssemblyFilterMode.SelectionChangeCommitted += new EventHandler(this.СbAssemblyFilterMode_SelectionChangeCommitted);
  }

  /// <summary>Сохранение параметров контрола</summary>
  private void SaveSettings(string sectionName = null)
  {
    if (string.IsNullOrEmpty(sectionName))
      sectionName = typeof (ProcRouteEntryForArticleControl).ToString();
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(sectionName) ?? service.Create(sectionName);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this.techNavTreeViewArticleObjects);
    config.SetProperty(this.techNavTreeViewArticleObjects.Name + "_AssemblyFilterMode", this.AssemblyFilterMode.ToString());
  }

  /// <summary>Очистка данных контролов</summary>
  private void ClearControlsData()
  {
    this.tbOrderCaption.Text = string.Empty;
    this.techNavTreeViewArticleObjects.Build((IDescriptor) new TechObjectListDescriptor(1, TechCardConsts.ObjectTypes.ArticleBaseID, "Входимости - Сборки", (IList) null));
  }

  /// <summary>Фоновый процесс получения состава заказа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    if (!(sender is BackgroundWorker backgroundWorker))
      return;
    if (backgroundWorker.CancellationPending)
    {
      e.Cancel = true;
    }
    else
    {
      if (this.ProcRouteEntryObject.MemberOfOrderVersion == 0L && this.ProcRouteEntryObject.MemberOfOrderObject == 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        List<ObjInfoItem> orderObject = this.GetOrderObject(session, this.cbVersionMode.Checked);
        if (orderObject == null || orderObject.Count == 0)
          return;
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
        CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) orderObject, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new int[2]
        {
          TechCardConsts.ObjectTypes.ArticleBaseID,
          TechCardConsts.ObjectTypes.MaterialBaseID
        }).ToArray(), (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.ProektRelationID
        }, RelObjInfoDbScheme<ObjInfoIDItem>.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) null, true, false, -1, (VersionsRule) null, this.cbVersionMode.Checked ? "cad001e0-306c-11d8-b4e9-00304f19f545" : "cad005aa-306c-11d8-b4e9-00304f19f545");
        DataTable source = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
        if (source == null)
          return;
        this._orderCompositionItems = new List<RelObjInfoItem>();
        new RelObjInfoDbScheme<ObjInfoIDItem>(true).ParseInfoItems(session, (IEnumerable<DataRow>) source.AsEnumerable(), (ICollection<RelObjInfoItem>) this._orderCompositionItems);
      }
    }
  }

  /// <summary>
  /// Получить информацию по конкретной версии заказа или все версии
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  private List<ObjInfoItem> GetOrderObject(IUserSession session, bool versionMode)
  {
    List<ObjInfoItem> orderObject = new List<ObjInfoItem>();
    if (this.ProcRouteEntryObject.MemberOfOrderVersion != 0L)
    {
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this.ProcRouteEntryObject.MemberOfOrderVersion);
      if (!objectInfo.Empty)
        orderObject.Add(new ObjInfoItem(objectInfo.ObjectID, objectInfo.ObjectTypeID));
    }
    if (orderObject.Count != 0)
      return orderObject;
    DataTable allObjectVersions = session.GetAllObjectVersions(this.ProcRouteEntryObject.MemberOfOrderObject, true, false, false, "F_OBJECT_ID", "F_OBJECT_TYPE");
    if (allObjectVersions != null && allObjectVersions.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) allObjectVersions.Rows)
        orderObject.Add(new ObjInfoItem(Convert.ToInt64(row["F_OBJECT_ID"]), Convert.ToInt32(row["F_OBJECT_TYPE"])));
    }
    return orderObject;
  }

  /// <summary>Фоновая  задача завершена</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    StatusPopup.Hide((Control) this.techNavTreeViewArticleObjects);
    if (e.Error == null)
      this._dataLoaded = true;
    IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
    QuickObjectInfo quickObjectInfo = this.ProcRouteEntryObject.MemberOfOrderVersion != 0L ? service.GetObjectInfo(this.ProcRouteEntryObject.MemberOfOrderVersion) : service.GetObjectInfoByID(this.ProcRouteEntryObject.MemberOfOrderObject);
    if (!quickObjectInfo.Empty)
      this.tbOrderCaption.Text = quickObjectInfo.Caption;
    if (this._orderCompositionItems == null)
      return;
    this.BuildAssemblyTree((IEnumerable<RelObjInfoItem>) this._orderCompositionItems);
  }

  /// <summary>
  /// 
  /// </summary>
  private void DoLoadOrderData()
  {
    if (this._backgroundWorker == null)
      return;
    if (this._backgroundWorker.IsBusy)
    {
      int num = 0;
      while (this._backgroundWorker.CancellationPending)
      {
        Thread.Sleep(100);
        Application.DoEvents();
        ++num;
        if (num >= 5)
          break;
      }
    }
    if (this._backgroundWorker.IsBusy)
      return;
    this._dataLoaded = false;
    this._orderCompositionItems = (List<RelObjInfoItem>) null;
    this._backgroundWorker.RunWorkerAsync();
    StatusPopup.Show(ResourceHolder.LoadingImage, LocalizationHolder.rm.GetString("TechCard.Client_481"), (Control) this.techNavTreeViewArticleObjects);
  }

  /// <summary>Загрузка данных по применяемости первого уровня</summary>
  /// <param name="session"></param>
  private void FillProjArticleData()
  {
    if (this.ProcRouteEntryObject.MemberOfOrderVersion != 0L || this.ProcRouteEntryObject.MemberOfOrderObject != 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
      CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) this._articleObjectItems, (IEnumerable<int>) null, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.ProektRelationID
      }, RelObjInfoDbScheme<ObjInfoIDItem>.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) null, false, false, 1, (VersionsRule) null, this.cbVersionMode.Checked ? "cad001e0-306c-11d8-b4e9-00304f19f545" : "cad00601-306c-11d8-b4e9-00304f19f545");
      DataTable source = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
      this._projArticleInfoItems = new List<RelObjInfoItem>();
      new RelObjInfoDbScheme<ObjInfoIDItem>(false).ParseInfoItems(session, source != null ? (IEnumerable<DataRow>) source.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<RelObjInfoItem>) this._projArticleInfoItems);
    }
    this.BuildAssemblyTree((IEnumerable<RelObjInfoItem>) this._projArticleInfoItems);
  }

  /// <summary>Построить дерево входимости в сборки</summary>
  /// <param name="articleCompositionItems"></param>
  private void BuildAssemblyTree(
    IEnumerable<RelObjInfoItem> articleCompositionItems)
  {
    this.techNavTreeViewArticleObjects.ClearTreeCore(false);
    if (articleCompositionItems == null)
      return;
    this._treeBuildSource = articleCompositionItems;
    IObjectsInfoCache service1 = ApplicationServices.Container.GetService<IObjectsInfoCache>();
    List<ObjInfoItem> source1 = new List<ObjInfoItem>();
    foreach (long objectID in this.ProcRouteEntryObject.MemberOfAssemblyVersion)
    {
      QuickObjectInfo objectInfo = service1.GetObjectInfo(objectID);
      if (!objectInfo.Empty)
        source1.Add(new ObjInfoItem(objectInfo.ObjectID, objectInfo.ObjectTypeID));
    }
    foreach (long ID in this.ProcRouteEntryObject.MemberOfAssemblyObject)
    {
      QuickObjectInfo objectInfoById = service1.GetObjectInfoByID(ID);
      if (!objectInfoById.Empty)
        source1.Add(new ObjInfoItem(objectInfoById.ObjectID, objectInfoById.ObjectTypeID));
    }
    List<ObjInfoItem> list = source1.ToList<ObjInfoItem>();
    Dictionary<ObjInfoIDItem, List<ObjInfoItem>> dictionary = new Dictionary<ObjInfoIDItem, List<ObjInfoItem>>();
    foreach (ObjInfoIDItem articleObjectItem in this._articleObjectItems)
    {
      foreach (RelObjInfoItem articleCompositionItem in articleCompositionItems)
      {
        RelObjInfoItem projArticleInfoItem = articleCompositionItem;
        if (projArticleInfoItem.PartInfo.ObjectID == articleObjectItem.ObjectID)
        {
          List<ObjInfoItem> objInfoItemList;
          if (!dictionary.TryGetValue(articleObjectItem, out objInfoItemList))
          {
            objInfoItemList = new List<ObjInfoItem>();
            dictionary.Add(articleObjectItem, objInfoItemList);
          }
          switch (this.AssemblyFilterMode)
          {
            case AssemblyTreeFilterMode.OnlyChecked:
              if ((TypedInfoItem) source1.FirstOrDefault<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (a => Math.Abs(a.ObjectID) == Math.Abs(projArticleInfoItem.ProjInfo.ObjectID))) != (TypedInfoItem) null)
              {
                objInfoItemList.Add(projArticleInfoItem.ProjInfo);
                goto case AssemblyTreeFilterMode.OnlyIncorrectAssembly;
              }
              goto case AssemblyTreeFilterMode.OnlyIncorrectAssembly;
            case AssemblyTreeFilterMode.OnlyNoChecked:
              if ((TypedInfoItem) source1.FirstOrDefault<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (a => Math.Abs(a.ObjectID) == Math.Abs(projArticleInfoItem.ProjInfo.ObjectID))) == (TypedInfoItem) null)
              {
                objInfoItemList.Add(projArticleInfoItem.ProjInfo);
                goto case AssemblyTreeFilterMode.OnlyIncorrectAssembly;
              }
              goto case AssemblyTreeFilterMode.OnlyIncorrectAssembly;
            case AssemblyTreeFilterMode.OnlyIncorrectAssembly:
              list.RemoveAll((Predicate<ObjInfoItem>) (a => Math.Abs(a.ObjectID) == Math.Abs(projArticleInfoItem.ProjInfo.ObjectID)));
              continue;
            default:
              objInfoItemList.Add(projArticleInfoItem.ProjInfo);
              goto case AssemblyTreeFilterMode.OnlyIncorrectAssembly;
          }
        }
      }
    }
    DescriptorCollection descriptors = new DescriptorCollection();
    if (this.AssemblyFilterMode != AssemblyTreeFilterMode.OnlyIncorrectAssembly)
    {
      foreach (IGrouping<long, ObjInfoIDItem> grouping in this._articleObjectItems.GroupBy<ObjInfoIDItem, long>((System.Func<ObjInfoIDItem, long>) (a => a.ID)))
      {
        List<ObjInfoItem> source2 = new List<ObjInfoItem>();
        foreach (ObjInfoIDItem key in (IEnumerable<ObjInfoIDItem>) grouping)
        {
          List<ObjInfoItem> collection;
          if (dictionary.TryGetValue(key, out collection))
            source2.AddRange((IEnumerable<ObjInfoItem>) collection);
        }
        QuickObjectInfo objectInfoById = service1.GetObjectInfoByID(grouping.Key);
        Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache(source2.Distinct<ObjInfoItem>());
        TechDictDescriptor techDictDescriptor = new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, objectInfoById.ObjectTypeID, objectInfoById.Caption, objectTypeCache);
        techDictDescriptor.ExpandNodes = false;
        IDescriptor descriptor = (IDescriptor) techDictDescriptor;
        descriptors.Add(descriptor);
      }
    }
    if (list.Count > 0 && this.AssemblyFilterMode == AssemblyTreeFilterMode.NoFilter || this.AssemblyFilterMode == AssemblyTreeFilterMode.OnlyIncorrectAssembly)
    {
      Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) list);
      TechDictDescriptor techDictDescriptor = new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, TechCardConsts.ObjectTypes.ArticleBaseID, AssemblyTreeFilterMode.OnlyIncorrectAssembly.GetDescription<AssemblyTreeFilterMode>(), objectTypeCache);
      techDictDescriptor.ExpandNodes = false;
      IDescriptor descriptor = (IDescriptor) techDictDescriptor;
      descriptors.Add(descriptor);
    }
    this.techNavTreeViewArticleObjects.Build((IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ArticleBaseID, "Входимости - Сборка", descriptors));
    if (!(ServiceUtils.GetService((object) ApplicationServices.Container, typeof (INavigatorTreeViewClientService), false) is INavigatorTreeViewClientService service2) || this.techNavTreeViewArticleObjects.RootNode == null)
      return;
    service2.ExpandAll(this.techNavTreeViewArticleObjects.RootNode);
  }

  /// <summary>Загрузить все данные</summary>
  /// <param name="session"></param>
  private void FillRouteEntryObjectData()
  {
    this.FillProjArticleData();
    this.DoLoadOrderData();
  }

  /// <summary>Выбрать заказ</summary>
  private void SelectOrder()
  {
    bool flag = false;
    if (this._orderList == null)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString(sc_19576.ssp_techcard_19577()), LocalizationHolder.rm.GetString("TechCard.Client_213"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
        flag = true;
    }
    else
      flag = true;
    long objectID = 0;
    if (flag)
    {
      if (this._orderList == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
          ColumnDescriptor[] columns = new ColumnDescriptor[2]
          {
            new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
          };
          CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) this._articleObjectItems, (IEnumerable<int>) new int[1]
          {
            MetaDataHelper.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545")
          }, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
          {
            TechCardConsts.RelTypes.ProektRelationID
          }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, false, false, -1, (VersionsRule) null, "cad001e0-306c-11d8-b4e9-00304f19f545");
          DataTable source = service.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams);
          this._orderList = (IList<ObjInfoItem>) new List<ObjInfoItem>();
          if (source != null)
            this._orderList.AddRange<ObjInfoItem>((IEnumerable<ObjInfoItem>) source.AsEnumerable().Select<DataRow, ObjInfoItem>((System.Func<DataRow, ObjInfoItem>) (row => new ObjInfoItem(Convert.ToInt64(row[0]), Convert.ToInt32(row[1])))));
        }
      }
      if (this._orderList.Count == 0)
      {
        string text;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ObjInfoIDItem objInfoIdItem = this._articleObjectItems.FirstOrDefault<ObjInfoIDItem>();
          long objectId = objInfoIdItem != null ? objInfoIdItem.ObjectID : 0L;
          text = string.Format(LocalizationHolder.rm.GetString(sc_19576.ssp_techcard_19578()), (object) TechCardConsts.Utils.GetObjectString(objectId, sessionKeeper.Session), (object) objectId);
        }
        if (MessageBox.Show(text, LocalizationHolder.rm.GetString("TechCard.Client_142"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
          flag = false;
      }
      else
      {
        List<long> enumeration = TechCardClientConst.SelectObjectDlg(MetaDataHelper.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545"), this._orderList, MetaDataHelper.GetObjectTypeName(new Guid("cad00580-306c-11d8-b4e9-00304f19f545")), LocalizationHolder.rm.GetString("TechCard.Client_97"));
        // ISSUE: explicit non-virtual call
        if (enumeration != null && __nonvirtual (enumeration.Count) > 0)
          objectID = enumeration.FirstOrDefault<long>();
      }
    }
    if (!flag)
      objectID = TechCardClientConst.SelectObjectDlg(new Guid("cad00580-306c-11d8-b4e9-00304f19f545"), LocalizationHolder.rm.GetString(sc_19576.ssp_techcard_19579()));
    if (objectID == 0L)
      return;
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(objectID);
    if (objectInfo.Empty)
      return;
    if (this.cbVersionMode.Checked)
      this.ProcRouteEntryObject.MemberOfOrderVersion = Math.Abs(objectInfo.ObjectID);
    this.ProcRouteEntryObject.MemberOfOrderObject = objectInfo.ID;
    this.FillRouteEntryObjectData();
  }

  private void ChangeVersionMode()
  {
    try
    {
      IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
      if (!this.cbVersionMode.Checked)
      {
        this.ProcRouteEntryObject.MemberOfOrderVersion = 0L;
      }
      else
      {
        QuickObjectInfo objectInfoById = service.GetObjectInfoByID(this.ProcRouteEntryObject.MemberOfOrderObject);
        if (!objectInfoById.Empty)
          this.ProcRouteEntryObject.MemberOfOrderVersion = Math.Abs(objectInfoById.ObjectID);
      }
      IEnumerable<long> source1 = !this.cbVersionMode.Checked ? this.ProcRouteEntryObject.MemberOfAssemblyVersion : this.ProcRouteEntryObject.MemberOfAssemblyObject;
      if (source1 == null || !source1.Any<long>())
        return;
      List<long> source2 = new List<long>();
      foreach (long num in source1)
      {
        QuickObjectInfo quickObjectInfo = !this.cbVersionMode.Checked ? service.GetObjectInfo(num) : service.GetObjectInfoByID(num);
        if (!quickObjectInfo.Empty)
          source2.Add(this.cbVersionMode.Checked ? Math.Abs(quickObjectInfo.ObjectID) : quickObjectInfo.ID);
      }
      if (this.cbVersionMode.Checked)
      {
        this.ProcRouteEntryObject.MemberOfAssemblyVersion = (IEnumerable<long>) source2;
        this.ProcRouteEntryObject.MemberOfAssemblyObject = (IEnumerable<long>) new long[0];
      }
      else
      {
        this.ProcRouteEntryObject.MemberOfAssemblyVersion = (IEnumerable<long>) new long[0];
        this.ProcRouteEntryObject.MemberOfAssemblyObject = source2.Distinct<long>();
      }
    }
    finally
    {
      this.FillRouteEntryObjectData();
    }
  }

  /// <summary>Загрузка данных</summary>
  /// <returns></returns>
  public bool StartLoadData([NotNull] IUserSession session)
  {
    this.ClearControlsData();
    ProcRouteEntryObject routeEntryObject = this.ProcRouteEntryObject;
    if ((routeEntryObject != null ? (!routeEntryObject.LoadData(session) ? 1 : 0) : 1) != 0)
      return false;
    this.cbVersionMode.CheckStateChanged -= new EventHandler(this.CbVersionMode_CheckStateChanged);
    this.cbVersionMode.Checked = this.ProcRouteEntryObject.MemberBindingToVersions;
    this.cbVersionMode.CheckStateChanged += new EventHandler(this.CbVersionMode_CheckStateChanged);
    this.ArticleObjectItems = new List<ObjInfoIDItem>();
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      new ObjInfoItem(this.ProcRouteEntryObject.ObjectId)
    }, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.ObjectTypes.ProcRoutingID
    }, (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, RelObjInfoDbScheme<ObjInfoIDItem>.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) null, false, false, 2, (VersionsRule) null, "cad001e0-306c-11d8-b4e9-00304f19f545");
    DataTable source = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
    List<RelObjInfoItem> objects = new List<RelObjInfoItem>();
    new RelObjInfoDbScheme<ObjInfoIDItem>(false).ParseInfoItems(session, source != null ? (IEnumerable<DataRow>) source.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<RelObjInfoItem>) objects);
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new int[2]
    {
      TechCardConsts.ObjectTypes.ArticleBaseID,
      TechCardConsts.ObjectTypes.MaterialBaseID
    });
    foreach (RelObjInfoItem relObjInfoItem in objects)
    {
      if (childrenIdRecursive.Contains(relObjInfoItem.ProjInfo.ObjTypeID))
        this.ArticleObjectItems.Add(relObjInfoItem.ProjInfo as ObjInfoIDItem);
    }
    this.FillRouteEntryObjectData();
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  public void CancelLoadData()
  {
    if (this._backgroundWorker != null && this._backgroundWorker.WorkerSupportsCancellation && this._backgroundWorker.IsBusy)
      this._backgroundWorker.CancelAsync();
    StatusPopup.Hide((Control) this.techNavTreeViewArticleObjects);
  }

  /// <summary>
  /// Идентификатор версии объекта "Входимость маршрута обработки"
  /// </summary>
  [Browsable(false)]
  public ProcRouteEntryObject ProcRouteEntryObject
  {
    get => this._procRouteEntryObject;
    set
    {
      this._procRouteEntryObject = value ?? throw new ArgumentNullException(nameof (ProcRouteEntryObject));
    }
  }

  [Browsable(false)]
  public List<ObjInfoIDItem> ArticleObjectItems
  {
    get => this._articleObjectItems;
    set => this._articleObjectItems = value;
  }

  /// <summary>Режим фильтрации дерева</summary>
  private AssemblyTreeFilterMode AssemblyFilterMode
  {
    get
    {
      if (this._assemblyTreeFilterMode != -1)
        return (AssemblyTreeFilterMode) this._assemblyTreeFilterMode;
      this._assemblyTreeFilterMode = 0;
      return AssemblyTreeFilterMode.NoFilter;
    }
    set
    {
      if ((AssemblyTreeFilterMode) this._assemblyTreeFilterMode == value)
        return;
      this._assemblyTreeFilterMode = (int) value;
      this.BuildAssemblyTree(this._treeBuildSource);
    }
  }

  /// <summary>Выбрать заказ</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BtnSetOrder_Click(object sender, EventArgs e) => this.SelectOrder();

  /// <summary>Очистить заказ</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TbOrderCaption_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
      return;
    this.ProcRouteEntryObject.MemberOfOrderVersion = 0L;
    this.ProcRouteEntryObject.MemberOfOrderObject = 0L;
    this.ClearControlsData();
    this.FillProjArticleData();
  }

  /// <summary>Создан Node в дереве</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ArticleNavTreeView_AfterCreateNode(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e?.Node;
    if (node == null)
      return;
    if (!(node.NodeID is NodeID nodeId))
    {
      node.ShowCheckState = false;
    }
    else
    {
      bool flag = this.cbVersionMode.Checked ? this._procRouteEntryObject.MemberOfAssemblyVersion.Contains<long>(Math.Abs(nodeId.ObjectID)) : this._procRouteEntryObject.MemberOfAssemblyObject.Contains<long>(nodeId.ID);
      node._checkState = flag ? CheckState.Checked : CheckState.Unchecked;
    }
  }

  /// <summary>Изменился статус отметки привязки для сборки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ArticleNavTreeView_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (!(e.Node.NodeID is NodeID nodeId))
      return;
    this.ProcRouteEntryObject.SetModifyStateAssembly(new ObjInfoIDItem(Math.Abs(nodeId.ObjectID), nodeId.ObjectTypeID, nodeId.ID), this.cbVersionMode.Checked, e.Node.CheckState == CheckState.Checked);
  }

  /// <summary>Изменился статус режима привязки к версиям</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CbVersionMode_CheckStateChanged(object sender, EventArgs e)
  {
    if (this.cbVersionMode.CheckState == CheckState.Unchecked && MessageBox.Show("Данные привязок к версиям будут удалены. Продолжить?", LocalizationHolder.rm.GetString("TechCard.Client_142"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
    {
      this.cbVersionMode.CheckStateChanged -= new EventHandler(this.CbVersionMode_CheckStateChanged);
      this.cbVersionMode.Checked = true;
      this.cbVersionMode.CheckStateChanged += new EventHandler(this.CbVersionMode_CheckStateChanged);
    }
    else
      this.ChangeVersionMode();
  }

  private void СbAssemblyFilterMode_SelectionChangeCommitted(object sender, EventArgs e)
  {
    foreach (AssemblyTreeFilterMode assemblyTreeFilterMode in Enum.GetValues(typeof (AssemblyTreeFilterMode)))
    {
      if (this.cbAssemblyFilterMode.SelectedItem.ToString() == assemblyTreeFilterMode.GetDescription<AssemblyTreeFilterMode>())
      {
        this.AssemblyFilterMode = assemblyTreeFilterMode;
        break;
      }
    }
  }

  /// <summary>Показать контекстное меню</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ContextMenuTree_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    Point client = this.techNavTreeViewArticleObjects.PointToClient(e.Position);
    NavigatorTreeNode nodeAt = this.techNavTreeViewArticleObjects.GetNodeAt(client.X, client.Y);
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.contextMenuTree.Items)
      toolbarItemBase.Visible = nodeAt != null;
    if (nodeAt == null)
      return;
    this.mbiCheckedAll.Visible = nodeAt.ShowCheckState;
    this.mbiUncheckedAll.Visible = nodeAt.ShowCheckState;
    this.mbiInvertChecked.Visible = nodeAt.ShowCheckState;
    if (nodeAt.NodeID is NodeID)
      return;
    this.mbiOpenInNewWindow.Visible = false;
    this.mbiProperty.Visible = false;
  }

  private void MbiCheckedAll_Click(object sender, EventArgs e)
  {
    this.SetCheckedStateAllNodes(CheckState.Checked);
  }

  private void MbiUncheckedAll_Click(object sender, EventArgs e)
  {
    this.SetCheckedStateAllNodes(CheckState.Unchecked);
  }

  private void MbiInvertChecked_Click(object sender, EventArgs e)
  {
    this.SetCheckedStateAllNodes(CheckState.Indeterminate);
  }

  private void SetCheckedStateAllNodes(CheckState newCheckState)
  {
    NavigatorTreeNode parent = this.techNavTreeViewArticleObjects.FocusedNode?.Parent;
    if (parent == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) parent.Children)
    {
      if (child.ShowCheckState && child.CheckState != newCheckState)
      {
        switch (newCheckState)
        {
          case CheckState.Unchecked:
          case CheckState.Checked:
            child.CheckState = newCheckState;
            continue;
          case CheckState.Indeterminate:
            child.CheckState = child.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
            continue;
          default:
            continue;
        }
      }
    }
  }

  /// <summary>Открыть в отдельном окне</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void MbiOpenInNewWindow_Click(object sender, EventArgs e)
  {
    if (!(this.techNavTreeViewArticleObjects.FocusedNode?.NodeID is NodeID nodeId))
      return;
    TechCardClientConst.OpenObjectInNewWindow(nodeId.ObjectID);
  }

  /// <summary>Карточка</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void MbiProperty_Click(object sender, EventArgs e)
  {
    this.NavigatorContextMenuInvoke(sender);
  }

  /// <summary>Найти в дереве</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void MbiSearch_Click(object sender, EventArgs e)
  {
    this.NavigatorContextMenuInvoke(sender);
  }

  private void NavigatorContextMenuInvoke(object sender)
  {
    if (!(sender is MenuButtonItem menuButtonItem))
      return;
    string commandName = menuButtonItem.CommandName;
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.techNavTreeViewArticleObjects.FocusedItems, this.techNavTreeViewArticleObjects.Services);
    if (commandsTable == null || !commandsTable.Contains(commandName))
      return;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, this.techNavTreeViewArticleObjects.Services);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.SaveSettings();
      this._backgroundWorker.Dispose();
      this._backgroundWorker = (BackgroundWorker) null;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProcRouteEntryForArticleControl));
    this.pnlTop = new Panel();
    this.cbAssemblyFilterMode = new ComboBox();
    this.lbAssemblyFilterMode = new Label();
    this.cbVersionMode = new CheckBox();
    this.btnSetOrder = new Button();
    this.tbOrderCaption = new TextBox();
    this.lbOrder = new Label();
    this.techNavTreeViewArticleObjects = new TechCardNavTreeViewControl();
    this.contextMenuTree = new ContextMenuBarItem();
    this.mbiCheckedAll = new MenuButtonItem();
    this.mbiUncheckedAll = new MenuButtonItem();
    this.mbiInvertChecked = new MenuButtonItem();
    this.mbiOpenInNewWindow = new MenuButtonItem();
    this.mbiProperty = new MenuButtonItem();
    this.mbiSearch = new MenuButtonItem();
    this.menuBarTree = new MenuBar();
    this.pnlTop.SuspendLayout();
    this.techNavTreeViewArticleObjects.BeginInit();
    this.SuspendLayout();
    this.pnlTop.Controls.Add((Control) this.cbAssemblyFilterMode);
    this.pnlTop.Controls.Add((Control) this.lbAssemblyFilterMode);
    this.pnlTop.Controls.Add((Control) this.cbVersionMode);
    this.pnlTop.Controls.Add((Control) this.btnSetOrder);
    this.pnlTop.Controls.Add((Control) this.tbOrderCaption);
    this.pnlTop.Controls.Add((Control) this.lbOrder);
    this.pnlTop.Dock = DockStyle.Top;
    this.pnlTop.Location = new Point(0, 0);
    this.pnlTop.Name = "pnlTop";
    this.pnlTop.Size = new Size(665, 71);
    this.pnlTop.TabIndex = 1;
    this.cbAssemblyFilterMode.FormattingEnabled = true;
    this.cbAssemblyFilterMode.Location = new Point(382, 41);
    this.cbAssemblyFilterMode.Name = "cbAssemblyFilterMode";
    this.cbAssemblyFilterMode.Size = new Size(237, 21);
    this.cbAssemblyFilterMode.TabIndex = 6;
    this.lbAssemblyFilterMode.AutoSize = true;
    this.lbAssemblyFilterMode.Location = new Point(329, 44);
    this.lbAssemblyFilterMode.Name = "lbAssemblyFilterMode";
    this.lbAssemblyFilterMode.Size = new Size(47, 13);
    this.lbAssemblyFilterMode.TabIndex = 5;
    this.lbAssemblyFilterMode.Text = "Фильтр";
    this.cbVersionMode.AutoSize = true;
    this.cbVersionMode.Checked = true;
    this.cbVersionMode.CheckState = CheckState.Checked;
    this.cbVersionMode.Location = new Point(48 /*0x30*/, 44);
    this.cbVersionMode.Name = "cbVersionMode";
    this.cbVersionMode.Size = new Size(137, 17);
    this.cbVersionMode.TabIndex = 4;
    this.cbVersionMode.Text = "Входимость в версию";
    this.cbVersionMode.UseVisualStyleBackColor = true;
    this.btnSetOrder.Location = new Point(625, 12);
    this.btnSetOrder.Name = "btnSetOrder";
    this.btnSetOrder.Size = new Size(24, 24);
    this.btnSetOrder.TabIndex = 2;
    this.btnSetOrder.Text = "...";
    this.btnSetOrder.UseVisualStyleBackColor = true;
    this.btnSetOrder.Click += new EventHandler(this.BtnSetOrder_Click);
    this.tbOrderCaption.Location = new Point(48 /*0x30*/, 15);
    this.tbOrderCaption.Name = "tbOrderCaption";
    this.tbOrderCaption.ReadOnly = true;
    this.tbOrderCaption.Size = new Size(570, 20);
    this.tbOrderCaption.TabIndex = 1;
    this.tbOrderCaption.KeyDown += new KeyEventHandler(this.TbOrderCaption_KeyDown);
    this.lbOrder.AutoSize = true;
    this.lbOrder.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lbOrder.Location = new Point(4, 18);
    this.lbOrder.Name = "lbOrder";
    this.lbOrder.Size = new Size(38, 13);
    this.lbOrder.TabIndex = 0;
    this.lbOrder.Text = "Заказ";
    this.techNavTreeViewArticleObjects.AllowDrop = true;
    this.techNavTreeViewArticleObjects.AllowMultiSelect = false;
    this.techNavTreeViewArticleObjects.AllowUserPinnedColumns = false;
    this.techNavTreeViewArticleObjects.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.techNavTreeViewArticleObjects.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("techNavTreeViewArticleObjects.CheckedNodesStates");
    this.techNavTreeViewArticleObjects.CheckoutMode = TechCheckoutMode.Manual;
    this.techNavTreeViewArticleObjects.CheckRootNode = false;
    this.techNavTreeViewArticleObjects.ContextMenuBarItem = this.contextMenuTree;
    this.techNavTreeViewArticleObjects.DisableCheckedOutColumn = true;
    this.techNavTreeViewArticleObjects.DisableDragAndDrop = true;
    this.techNavTreeViewArticleObjects.DisableIMContextMenu = true;
    this.techNavTreeViewArticleObjects.DisableKeyUpEvents = true;
    this.techNavTreeViewArticleObjects.Dock = DockStyle.Fill;
    this.techNavTreeViewArticleObjects.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.techNavTreeViewArticleObjects.ImageList = (ImageList) null;
    this.techNavTreeViewArticleObjects.ItemsMode = SelectedItemsMode.Default;
    this.techNavTreeViewArticleObjects.LineStyle = LineStyle.Dot;
    this.techNavTreeViewArticleObjects.Location = new Point(0, 71);
    this.techNavTreeViewArticleObjects.Name = "techNavTreeViewArticleObjects";
    this.techNavTreeViewArticleObjects.RowEvenStyle.WordWrap = false;
    this.techNavTreeViewArticleObjects.RowOddStyle.WordWrap = false;
    this.techNavTreeViewArticleObjects.RowSelectedStyle.WordWrap = false;
    this.techNavTreeViewArticleObjects.RowStyle.BorderColor = SystemColors.Control;
    this.techNavTreeViewArticleObjects.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.techNavTreeViewArticleObjects.RowStyle.BorderWidth = 1;
    this.techNavTreeViewArticleObjects.RowStyle.WordWrap = false;
    this.techNavTreeViewArticleObjects.SelectBeforeEdit = true;
    this.techNavTreeViewArticleObjects.ShowRootRow = false;
    this.techNavTreeViewArticleObjects.Size = new Size(665, 409);
    this.techNavTreeViewArticleObjects.SuppressErrorMessages = true;
    this.techNavTreeViewArticleObjects.TabIndex = 2;
    this.techNavTreeViewArticleObjects.AfterCreateNode += new EventHandler<NodeEventArgs>(this.ArticleNavTreeView_AfterCreateNode);
    this.techNavTreeViewArticleObjects.CheckStateChanged += new EventHandler<NodeEventArgs>(this.ArticleNavTreeView_CheckStateChanged);
    this.contextMenuTree.CommandName = "contextMenuTree";
    this.contextMenuTree.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mbiCheckedAll,
      (ToolbarItemBase) this.mbiUncheckedAll,
      (ToolbarItemBase) this.mbiInvertChecked,
      (ToolbarItemBase) this.mbiOpenInNewWindow,
      (ToolbarItemBase) this.mbiProperty,
      (ToolbarItemBase) this.mbiSearch
    });
    this.contextMenuTree.ShowText = true;
    this.contextMenuTree.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ContextMenuTree_BeforePopup);
    this.mbiCheckedAll.BeginGroup = true;
    this.mbiCheckedAll.CommandName = "mbiCheckedAllEtp";
    this.mbiCheckedAll.ShowText = true;
    this.mbiCheckedAll.Text = "Отметить все";
    this.mbiCheckedAll.Click += new EventHandler(this.MbiCheckedAll_Click);
    this.mbiUncheckedAll.CommandName = "UncheckedAllEtp";
    this.mbiUncheckedAll.ShowText = true;
    this.mbiUncheckedAll.Text = "Снять все отметки";
    this.mbiUncheckedAll.Click += new EventHandler(this.MbiUncheckedAll_Click);
    this.mbiInvertChecked.CommandName = "mbiInvertCheckedEtp";
    this.mbiInvertChecked.ShowText = true;
    this.mbiInvertChecked.Text = "Инвертировать отметки";
    this.mbiInvertChecked.Click += new EventHandler(this.MbiInvertChecked_Click);
    this.mbiOpenInNewWindow.BeginGroup = true;
    this.mbiOpenInNewWindow.CommandName = "OpenInNewWindow";
    this.mbiOpenInNewWindow.ShowText = true;
    this.mbiOpenInNewWindow.Text = "Открыть в отдельном окне";
    this.mbiOpenInNewWindow.Click += new EventHandler(this.MbiOpenInNewWindow_Click);
    this.mbiProperty.CommandName = "ParametersCard";
    this.mbiProperty.Shortcut = Shortcut.F4;
    this.mbiProperty.ShowText = true;
    this.mbiProperty.Text = "Свойства (Карточка)";
    this.mbiProperty.Click += new EventHandler(this.MbiProperty_Click);
    this.mbiSearch.CommandName = "SeekInTree";
    this.mbiSearch.Shortcut = Shortcut.CtrlF;
    this.mbiSearch.ShowText = true;
    this.mbiSearch.Text = "Найти в дереве";
    this.mbiSearch.Click += new EventHandler(this.MbiSearch_Click);
    this.menuBarTree.Guid = new Guid("94360a41-12fe-4eeb-a647-04580f3467e7");
    this.menuBarTree.Hidden = false;
    this.menuBarTree.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuTree
    });
    this.menuBarTree.Location = new Point(0, 71);
    this.menuBarTree.Name = "menuBarTree";
    this.menuBarTree.OwnerForm = (Form) null;
    this.menuBarTree.Size = new Size(665, 26);
    this.menuBarTree.TabIndex = 3;
    this.menuBarTree.Text = "menuBarEtpTree";
    this.menuBarTree.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.menuBarTree);
    this.Controls.Add((Control) this.techNavTreeViewArticleObjects);
    this.Controls.Add((Control) this.pnlTop);
    this.Name = nameof (ProcRouteEntryForArticleControl);
    this.Size = new Size(665, 480);
    this.pnlTop.ResumeLayout(false);
    this.pnlTop.PerformLayout();
    this.techNavTreeViewArticleObjects.EndInit();
    this.ResumeLayout(false);
  }
}
