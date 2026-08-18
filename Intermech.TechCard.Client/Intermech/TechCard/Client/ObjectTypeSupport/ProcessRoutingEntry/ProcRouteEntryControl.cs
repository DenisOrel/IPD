// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.ProcRouteEntryControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.MRP2;
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
public class ProcRouteEntryControl : UserControl
{
  /// <summary>
  /// 
  /// </summary>
  private bool _dataLoaded;
  /// <summary>
  /// 
  /// </summary>
  private DataTable _productionReportData;
  /// <summary>Кэш применяемости для ПВ</summary>
  private readonly IDictionary<long, List<DataRow>> _productionDataChild2ParentRowCache = (IDictionary<long, List<DataRow>>) new Dictionary<long, List<DataRow>>();
  /// <summary>Кэш состава для ПВ</summary>
  private readonly IDictionary<long, List<DataRow>> _productionDataParent2ChildRowCache = (IDictionary<long, List<DataRow>>) new Dictionary<long, List<DataRow>>();
  /// <summary>Кэш с информацией по объектам ПВ (Тип объекта , UID)</summary>
  private readonly IDictionary<long, Tuple<int, string>> _productionDataObject2InfoCache = (IDictionary<long, Tuple<int, string>>) new Dictionary<long, Tuple<int, string>>();
  /// <summary>
  /// 
  /// </summary>
  private readonly IServiceContainer _services = (IServiceContainer) new ServiceContainer();
  /// <summary>Список изделий для текущей входимости</summary>
  private IList<ObjInfoItem> _articleObjectItems;
  /// <summary>
  /// Список возможных производственных ведомостей для текущей входимости (списка изделий)
  /// </summary>
  private IList<ObjInfoItem> _productionReportList;
  /// <summary>
  /// 
  /// </summary>
  private readonly IDictionary<ObjInfoItem, string> _exitAssemblyItemCache = (IDictionary<ObjInfoItem, string>) new Dictionary<ObjInfoItem, string>();
  /// <summary>
  /// 
  /// </summary>
  private readonly IDictionary<ObjInfoItem, string> _productObjectCopyItemCache = (IDictionary<ObjInfoItem, string>) new Dictionary<ObjInfoItem, string>();
  /// <summary>Объект "Входимость маршрута обработки"</summary>
  private ProcRouteEntryObject _procRouteEntryObject = new ProcRouteEntryObject(-1L);
  /// <summary>Фоновая задача загрузки данных</summary>
  private BackgroundWorker _backgroundWorker;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer splitContainerMain;
  private Panel pnlTop;
  private Label lblProductionReport;
  private CheckBox chbProductionReportVersionMode;
  private Button btnProductionReportObject;
  private TextBox tbxProductionReportObject;
  private Label lblProductionReportVersionNumber;
  private TechCardNavTreeViewControl techNavTreeViewExitAssemblies;
  private TechCardNavTreeViewControl techNavTreeViewProductionCopyObjects;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    if (ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false) == null)
      return;
    this.techNavTreeViewExitAssemblies.Services = (System.IServiceProvider) this._services;
    this.techNavTreeViewProductionCopyObjects.Services = (System.IServiceProvider) this._services;
    this.techNavTreeViewExitAssemblies.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    this.techNavTreeViewProductionCopyObjects.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    NodeColumnCollection columns = Intermech.Navigator.Utils.VersionColumns(NodeColumnSortOrder.Ascending, false);
    IDescriptor descriptor1 = (IDescriptor) new TechObjectListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MRP2Consts.objtypeIdExitAssembly, string.Empty, (IList) null);
    this.techNavTreeViewExitAssemblies.SetColumns(columns, descriptor1);
    IDescriptor descriptor2 = (IDescriptor) new TechObjectListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MRP2Consts.objtypeIdProductionCopy, string.Empty, (IList) null);
    this.techNavTreeViewProductionCopyObjects.SetColumns(columns, descriptor2);
    this._backgroundWorker = new BackgroundWorker()
    {
      WorkerReportsProgress = true,
      WorkerSupportsCancellation = true
    };
    this._backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
    this._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
  }

  private void InitializeCustomSettings() => this.LoadSettings();

  /// <summary>Загрузка параметров контрола</summary>
  private void LoadSettings(string sectionName = null)
  {
    if (string.IsNullOrEmpty(sectionName))
      sectionName = typeof (ProcRouteEntryControl).ToString();
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(sectionName);
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this.techNavTreeViewExitAssemblies);
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this.techNavTreeViewProductionCopyObjects);
  }

  /// <summary>Сохранение параметров контрола</summary>
  private void SaveSettings(string sectionName = null)
  {
    if (string.IsNullOrEmpty(sectionName))
      sectionName = typeof (ProcRouteEntryControl).ToString();
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(sectionName) ?? service.Create(sectionName);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this.techNavTreeViewExitAssemblies);
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this.techNavTreeViewProductionCopyObjects);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ClearControlsData()
  {
    this.chbProductionReportVersionMode.Checked = false;
    this.tbxProductionReportObject.Text = string.Empty;
    this.lblProductionReportVersionNumber.Text = string.Empty;
    this.techNavTreeViewExitAssemblies.Build((IDescriptor) new TechObjectListDescriptor(1, TechCardConsts.ObjectTypes.ProcRoutingEntryID, "Входимости - выходные сборки", (IList) null));
    this.techNavTreeViewProductionCopyObjects.Build((IDescriptor) new TechObjectListDescriptor(1, MRP2Consts.objtypeIdProductionCopy, "Входимости - ПК ДСЕ", (IList) null));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    if (!(sender is BackgroundWorker backgroundWorker))
      return;
    if (backgroundWorker.CancellationPending)
    {
      e.Cancel = true;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
        ColumnDescriptor[] columns = new ColumnDescriptor[4]
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -7, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.ASC, 0),
          new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ProductionObjectUIDAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
        };
        CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) this.GetProductionReportVersionInfo(sessionKeeper.Session), (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MRP2Consts.objtypeIdProductionObjects).ToArray(), (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.ProductReportRelationID
        }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, true, false, -1, (VersionsRule) null, "cad001e0-306c-11d8-b4e9-00304f19f545");
        this._productionReportData = service.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams);
        if (this._productionReportData == null)
          return;
        int columnIndex1 = this._productionReportData.Columns.IndexOf("F_OBJECT_ID");
        int columnIndex2 = this._productionReportData.Columns.IndexOf("F_OBJECT_TYPE");
        int columnIndex3 = this._productionReportData.Columns.IndexOf(TechCardConsts.AttributeTypes.ProductionObjectUIDAttrGuid.ToString());
        int columnIndex4 = this._productionReportData.Columns.IndexOf("F_PROJ_ID");
        foreach (DataRow row in (InternalDataCollectionBase) this._productionReportData.Rows)
        {
          long int64Value1 = DataSetProcessor.GetInt64Value(row, columnIndex1, 0L);
          long int64Value2 = DataSetProcessor.GetInt64Value(row, columnIndex4, 0L);
          if (int64Value1 != 0L)
          {
            this._productionDataObject2InfoCache[int64Value1] = new Tuple<int, string>(DataSetProcessor.GetInt32Value(row, columnIndex2, -1), DataSetProcessor.GetStringValue(row, columnIndex3, string.Empty));
            List<DataRow> dataRowList;
            if (!this._productionDataChild2ParentRowCache.TryGetValue(int64Value1, out dataRowList))
            {
              dataRowList = new List<DataRow>();
              this._productionDataChild2ParentRowCache[int64Value1] = dataRowList;
            }
            dataRowList.Add(row);
            if (int64Value2 != 0L)
            {
              if (!this._productionDataParent2ChildRowCache.TryGetValue(int64Value2, out dataRowList))
              {
                dataRowList = new List<DataRow>();
                this._productionDataParent2ChildRowCache[int64Value2] = dataRowList;
              }
              dataRowList.Add(row);
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    StatusPopup.Hide((Control) this.techNavTreeViewExitAssemblies);
    if (!e.Cancelled && e.Error == null)
      this._dataLoaded = true;
    this.FillExitAssemblyData();
    this.FillProductCopyData();
  }

  /// <summary>
  /// 
  /// </summary>
  private void DoLoadProductionReportData()
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
    this.ClearProductionData();
    this._backgroundWorker.RunWorkerAsync();
    StatusPopup.Show(ResourceHolder.LoadingImage, LocalizationHolder.rm.GetString("TechCard.Client_481"), (Control) this.techNavTreeViewExitAssemblies);
  }

  private void ClearProductionData()
  {
    this._productionReportData = (DataTable) null;
    this._productionDataChild2ParentRowCache.Clear();
    this._productionDataParent2ChildRowCache.Clear();
    this._productionDataObject2InfoCache.Clear();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="navTreeNode"></param>
  /// <param name="objectItemCache"></param>
  /// <returns></returns>
  private string GetProductionObjectUid(
    NavigatorTreeNode navTreeNode,
    IDictionary<ObjInfoItem, string> objectItemCache)
  {
    INode nodeHandler = navTreeNode.Tree.GetNodeHandler(navTreeNode);
    if (nodeHandler == null)
      return string.Empty;
    IDBTypedObjectID data = navTreeNode.NodeID is NodeID nodeId ? nodeHandler.GetData((INodeID) nodeId, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    string str;
    return data == null || data.ObjectID == 0L || data.ObjectID == -1L || !objectItemCache.TryGetValue(new ObjInfoItem(data.ObjectID), out str) ? string.Empty : str;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  private IList<ObjInfoItem> GetProductionReportVersionInfo(IUserSession session)
  {
    List<long> objectIDs = new List<long>();
    if (this.ProcRouteEntryObject.MemberOfProductionReportVersion != 0L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(this.ProcRouteEntryObject.MemberOfProductionReportVersion, false);
      objectIDs.Add(objectActualCopy != null ? objectActualCopy.ObjectID : 0L);
    }
    else
    {
      foreach (long allObjectVersions in session.GetAllObjectVersionsList(this.ProcRouteEntryObject.MemberOfProductionReportObject, true, false, false))
      {
        IDBObject objectActualCopy = session.GetObjectActualCopy(allObjectVersions, false);
        if (objectActualCopy != null)
          objectIDs.Add(objectActualCopy.ObjectID);
      }
    }
    return (IList<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) objectIDs);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void FillProductionReportData(IUserSession session)
  {
    IDBObject objectById = session.GetObjectByID(this.ProcRouteEntryObject.MemberOfProductionReportObject, false);
    if (objectById != null)
      this.tbxProductionReportObject.Text = TechCardConsts.Utils.GetObjectString(objectById, false);
    this.chbProductionReportVersionMode.CheckedChanged -= new EventHandler(this.chbProductionReportVersionMode_CheckedChanged);
    try
    {
      if (this.ProcRouteEntryObject.MemberOfProductionReportVersion != 0L)
      {
        this.chbProductionReportVersionMode.Checked = true;
        this.lblProductionReportVersionNumber.Text = $"Номер версии ({session.GetObject(this.ProcRouteEntryObject.MemberOfProductionReportVersion, false)?.VersionID.ToString() ?? string.Empty})";
      }
      else
      {
        this.chbProductionReportVersionMode.Checked = false;
        this.lblProductionReportVersionNumber.Text = string.Empty;
      }
    }
    finally
    {
      this.chbProductionReportVersionMode.CheckedChanged += new EventHandler(this.chbProductionReportVersionMode_CheckedChanged);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void FillExitAssemblyData()
  {
    this._exitAssemblyItemCache.Clear();
    if (this._productionReportData != null)
    {
      int idxFldProjObjId = this._productionReportData.Columns.IndexOf("F_PROJ_ID");
      HashSet<ObjInfoItem> allObjectHierarchy = new HashSet<ObjInfoItem>();
      GetParentObjects((IEnumerable<long>) ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) this._articleObjectItems));
      HashSet<int> hashSet = MetaDataHelper.GetObjectTypeChildrenIDRecursive(MRP2Consts.objtypeIdExitAssembly).ToHashSet<int>();
      HashSet<string> stringSet = new HashSet<string>();
      foreach (ObjInfoItem key in allObjectHierarchy)
      {
        if (hashSet.Contains(key.ObjTypeID))
        {
          Tuple<int, string> tuple;
          this._productionDataObject2InfoCache.TryGetValue(key.ObjectID, out tuple);
          string str = tuple?.Item2 ?? string.Empty;
          if (!stringSet.Contains(str))
          {
            this._exitAssemblyItemCache[key] = str;
            stringSet.Add(str);
          }
        }
      }

      void GetParentObjects(IEnumerable<long> objectIdList)
      {
        foreach (long objectId in objectIdList)
        {
          if (objectId != 0L)
          {
            ObjInfoItem objInfoItem = new ObjInfoItem(objectId);
            if (!allObjectHierarchy.Contains(objInfoItem))
            {
              Tuple<int, string> tuple;
              if (this._productionDataObject2InfoCache.TryGetValue(objectId, out tuple))
                objInfoItem.ObjTypeID = tuple.Item1;
              allObjectHierarchy.Add(objInfoItem);
              List<DataRow> source;
              if (this._productionDataChild2ParentRowCache.TryGetValue(objectId, out source))
                GetParentObjects(source.Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, idxFldProjObjId, 0L))));
            }
          }
        }
      }
    }
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) this._exitAssemblyItemCache.Keys);
    TechDictDescriptor techDictDescriptor = new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MRP2Consts.objtypeIdExitAssembly, "Входимости - выходные сборки", objectTypeCache);
    techDictDescriptor.ExpandNodes = false;
    TechDictDescriptor rootDescriptor = techDictDescriptor;
    IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> checkedNodesStates = this.techNavTreeViewExitAssemblies.CheckedNodesStates;
    this.techNavTreeViewExitAssemblies.Build((IDescriptor) rootDescriptor);
    if (checkedNodesStates == null || checkedNodesStates.Count == 0)
      return;
    this.techNavTreeViewExitAssemblies.CheckedNodesStates = checkedNodesStates;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void FillProductCopyData()
  {
    this._productObjectCopyItemCache.Clear();
    List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
    foreach (NavigatorTreeNode checkedNode in this.techNavTreeViewExitAssemblies.CheckedNodes)
    {
      INode nodeHandler = checkedNode.Tree.GetNodeHandler(checkedNode);
      if (nodeHandler != null)
      {
        IDBTypedObjectID data = checkedNode.NodeID is NodeID nodeId ? nodeHandler.GetData((INodeID) nodeId, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
        if (data != null)
          objInfoList.Add(new ObjInfoItem(data.ObjectID, data.ObjectType));
      }
    }
    if (objInfoList.Count != 0 && this._productionReportData != null)
    {
      int idxFldObjectId = this._productionReportData.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex = this._productionReportData.Columns.IndexOf("F_PROJ_ID");
      HashSet<ObjInfoItem> allObjectHierarchy = new HashSet<ObjInfoItem>();
      GetChildObjects((IEnumerable<long>) ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) objInfoList));
      if (allObjectHierarchy.Any<ObjInfoItem>())
      {
        foreach (long hash in ObjInfoHelper.GetObjectIDs((IEnumerable<ObjInfoItem>) this._articleObjectItems).ToHashSet<long>())
        {
          List<DataRow> dataRowList;
          if (this._productionDataChild2ParentRowCache.TryGetValue(hash, out dataRowList))
          {
            foreach (DataRow row in dataRowList)
            {
              ObjInfoItem key = new ObjInfoItem(DataSetProcessor.GetInt64Value(row, columnIndex, 0L));
              if (allObjectHierarchy.Contains(key))
              {
                Tuple<int, string> tuple;
                this._productionDataObject2InfoCache.TryGetValue(key.ObjectID, out tuple);
                if (tuple != null)
                {
                  key.ObjTypeID = tuple.Item1;
                  this._productObjectCopyItemCache[key] = tuple.Item2;
                }
              }
            }
          }
        }
      }

      void GetChildObjects(IEnumerable<long> parentObjectIdList)
      {
        foreach (long parentObjectId in parentObjectIdList)
        {
          if (parentObjectId != 0L)
          {
            ObjInfoItem objInfoItem = new ObjInfoItem(parentObjectId);
            if (!allObjectHierarchy.Contains(objInfoItem))
            {
              Tuple<int, string> tuple;
              if (this._productionDataObject2InfoCache.TryGetValue(parentObjectId, out tuple))
                objInfoItem.ObjTypeID = tuple.Item1;
              allObjectHierarchy.Add(objInfoItem);
              List<DataRow> source;
              if (this._productionDataParent2ChildRowCache.TryGetValue(parentObjectId, out source))
                GetChildObjects(source.Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, idxFldObjectId, 0L))));
            }
          }
        }
      }
    }
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) this._productObjectCopyItemCache.Keys);
    TechDictDescriptor techDictDescriptor = new TechDictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, MRP2Consts.objtypeIdProductionCopy, "Входимости - ПК ДСЕ", objectTypeCache);
    techDictDescriptor.ExpandNodes = false;
    TechDictDescriptor rootDescriptor = techDictDescriptor;
    IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> checkedNodesStates = this.techNavTreeViewProductionCopyObjects.CheckedNodesStates;
    this.techNavTreeViewProductionCopyObjects.Build((IDescriptor) rootDescriptor);
    if (checkedNodesStates == null || checkedNodesStates.Count == 0)
      return;
    this.techNavTreeViewProductionCopyObjects.CheckedNodesStates = checkedNodesStates;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  private void FillRouteEntryObjectData([NotNull] IUserSession session)
  {
    this.FillProductionReportData(session);
    this.DoLoadProductionReportData();
  }

  /// <summary>Select production report's version</summary>
  private void SelectProductionReportVersionMode()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!this.chbProductionReportVersionMode.Checked)
      {
        this.ProcRouteEntryObject.MemberOfProductionReportVersion = 0L;
        this.FillRouteEntryObjectData(sessionKeeper.Session);
      }
      else
      {
        List<long> objectVersionsList = sessionKeeper.Session.GetAllObjectVersionsList(this.ProcRouteEntryObject.MemberOfProductionReportObject, true, false, false);
        if (objectVersionsList.Count == 1)
        {
          this.ProcRouteEntryObject.MemberOfProductionReportVersion = objectVersionsList.FirstOrDefault<long>();
          this.FillRouteEntryObjectData(sessionKeeper.Session);
        }
        else
        {
          List<ObjInfoItem> itemInfoList = SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList((IEnumerable<long>) objectVersionsList);
          List<long> enumeration = TechCardClientConst.SelectObjectDlg(MRP2Consts.objtypeIdProductionLists, (IList<ObjInfoItem>) itemInfoList, LocalizationHolder.rm.GetString("TechCard.Client_215"), LocalizationHolder.rm.GetString("TechCard.Client_216"));
          if (enumeration.Count == 0)
            this.chbProductionReportVersionMode.Checked = false;
          this.ProcRouteEntryObject.MemberOfProductionReportVersion = enumeration.FirstOrDefault<long>();
          this.FillRouteEntryObjectData(sessionKeeper.Session);
        }
      }
    }
  }

  /// <summary>Select</summary>
  private void SelectProductionReportObject()
  {
    bool flag = false;
    if (this._productionReportList == null)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString(sc_19571.ssp_techcard_19572()), LocalizationHolder.rm.GetString("TechCard.Client_213"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
        flag = true;
    }
    else
      flag = true;
    if (flag)
    {
      if (this._productionReportList == null)
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
            MRP2Consts.objtypeIdProductionLists
          }, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
          {
            TechCardConsts.RelTypes.ProductReportRelationID
          }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, false, false, -1, (VersionsRule) null, "cad005aa-306c-11d8-b4e9-00304f19f545");
          DataTable source = service.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams);
          this._productionReportList = (IList<ObjInfoItem>) new List<ObjInfoItem>();
          if (source != null)
            this._productionReportList.AddRange<ObjInfoItem>((IEnumerable<ObjInfoItem>) source.AsEnumerable().Select<DataRow, ObjInfoItem>((System.Func<DataRow, ObjInfoItem>) (row => new ObjInfoItem(Convert.ToInt64(row[0]), Convert.ToInt32(row[1])))));
        }
      }
      if (this._productionReportList.Count == 0)
      {
        string text;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ObjInfoItem objInfoItem = this._articleObjectItems.FirstOrDefault<ObjInfoItem>();
          long objectId = objInfoItem != null ? objInfoItem.ObjectID : 0L;
          text = string.Format(LocalizationHolder.rm.GetString(sc_19571.ssp_techcard_19573()), (object) TechCardConsts.Utils.GetObjectString(objectId, sessionKeeper.Session), (object) objectId);
        }
        if (MessageBox.Show(text, LocalizationHolder.rm.GetString("TechCard.Client_142"), MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
          flag = false;
      }
      else
      {
        List<long> enumeration = TechCardClientConst.SelectObjectDlg(MRP2Consts.objtypeIdProductionLists, this._productionReportList, LocalizationHolder.rm.GetString("TechCard.Client_ProductionReports"), LocalizationHolder.rm.GetString("TechCard.Client_SelectProductionReports"));
        // ISSUE: explicit non-virtual call
        if (enumeration != null && __nonvirtual (enumeration.Count) > 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            this.ProcRouteEntryObject.MemberOfProductionReportVersion = enumeration.FirstOrDefault<long>();
            this.ProcRouteEntryObject.MemberOfProductionReportObject = sessionKeeper.Session.GetObjectInfo(this.ProcRouteEntryObject.MemberOfProductionReportVersion).ID;
            this.FillRouteEntryObjectData(sessionKeeper.Session);
          }
        }
      }
    }
    if (flag)
      return;
    long num = TechCardClientConst.SelectObjectDlg(new Guid("cadd9a5c-306c-11d8-b4e9-00304f19f545"), LocalizationHolder.rm.GetString(sc_19571.ssp_techcard_19574()));
    if (num == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.ProcRouteEntryObject.MemberOfProductionReportVersion = num;
      this.ProcRouteEntryObject.MemberOfProductionReportObject = sessionKeeper.Session.GetObjectInfo(this.ProcRouteEntryObject.MemberOfProductionReportVersion).ID;
      this.FillRouteEntryObjectData(sessionKeeper.Session);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public ProcRouteEntryControl()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
    this.InitializeCustomSettings();
  }

  /// <summary>Загрузка данных</summary>
  /// <returns></returns>
  public bool StartLoadData([NotNull] IUserSession session)
  {
    this.ClearControlsData();
    ProcRouteEntryObject routeEntryObject = this.ProcRouteEntryObject;
    if ((routeEntryObject != null ? (!routeEntryObject.LoadData(session) ? 1 : 0) : 1) != 0)
      return false;
    this._articleObjectItems = (IList<ObjInfoItem>) TechCardObjUtils.Article.GetArticles4Object(new ObjInfoItem(this.ProcRouteEntryObject.ObjectId), session);
    this.FillRouteEntryObjectData(session);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  public void CancelLoadData()
  {
    if (this._backgroundWorker != null && this._backgroundWorker.WorkerSupportsCancellation && this._backgroundWorker.IsBusy)
      this._backgroundWorker.CancelAsync();
    StatusPopup.Hide((Control) this.techNavTreeViewExitAssemblies);
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

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnProductionReportObject_Click(object sender, EventArgs e)
  {
    this.SelectProductionReportObject();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chbProductionReportVersionMode_CheckedChanged(object sender, EventArgs e)
  {
    this.SelectProductionReportVersionMode();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techNavTreeViewExitAssemblies_AfterCreateNode(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e?.Node;
    if (node == null || !(sender is NavigatorTreeView) || !(node is TechcardNavTreeNode navTreeNode))
      return;
    string productionObjectUid = this.GetProductionObjectUid((NavigatorTreeNode) navTreeNode, this._exitAssemblyItemCache);
    CheckState state = this.ProcRouteEntryObject.MemberOfExitAssembly.ToString().Equals(productionObjectUid) ? CheckState.Checked : CheckState.Unchecked;
    navTreeNode.SetCheckStateInternal(state);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techNavTreeViewExitAssemblies_CheckStateChanging(
    object sender,
    CheckStateEventArgs e)
  {
    if (!(e.Node is TechcardNavTreeNode node))
      return;
    if (e.OldValue != e.NewValue)
    {
      foreach (NavigatorTreeNode checkedNode in node.Tree.CheckedNodes)
      {
        if (!checkedNode.Equals((object) e.Node) && checkedNode.CheckState == CheckState.Checked && checkedNode is TechcardNavTreeNode techcardNavTreeNode)
          techcardNavTreeNode.SetCheckStateInternal(CheckState.Unchecked);
      }
    }
    if (e.OldValue == e.NewValue || e.NewValue != CheckState.Checked)
      return;
    string productionObjectUid1 = this.GetProductionObjectUid(e.Node, this._exitAssemblyItemCache);
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) e.Node.Parent.Children)
    {
      if (!child.Equals((object) e.Node) && child.CheckState == CheckState.Checked && child is TechcardNavTreeNode navTreeNode)
      {
        string productionObjectUid2 = this.GetProductionObjectUid((NavigatorTreeNode) navTreeNode, this._exitAssemblyItemCache);
        if (productionObjectUid1 == productionObjectUid2)
          navTreeNode.SetCheckStateInternal(CheckState.Checked);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techNavTreeViewExitAssemblies_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (!(e.Node is TechcardNavTreeNode node))
      return;
    NavigatorTreeNode[] checkedNodes = node.Tree.CheckedNodes;
    string str = checkedNodes == null || checkedNodes.Length == 0 ? string.Empty : this.GetProductionObjectUid(checkedNodes[0], this._exitAssemblyItemCache);
    this.ProcRouteEntryObject.MemberOfExitAssembly = GuidHelper.IsGuid(str) ? new Guid(str) : Guid.Empty;
    this.FillProductCopyData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techNavTreeViewProductionCopyObjects_AfterCreateNode(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e?.Node;
    if (node == null || !(sender is NavigatorTreeView) || !(node is TechcardNavTreeNode navTreeNode))
      return;
    string productionObjectUid = this.GetProductionObjectUid((NavigatorTreeNode) navTreeNode, this._productObjectCopyItemCache);
    CheckState state = !GuidHelper.IsGuid(productionObjectUid) || !this.ProcRouteEntryObject.MemberOfAssemblyCopy.Contains<Guid>(new Guid(productionObjectUid)) ? CheckState.Unchecked : CheckState.Checked;
    navTreeNode.SetCheckStateInternal(state);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techNavTreeViewProductionCopyObjects_CheckStateChanged(
    object sender,
    NodeEventArgs e)
  {
    if (!(e.Node is TechcardNavTreeNode))
      return;
    string productionObjectUid = this.GetProductionObjectUid(e.Node, this._productObjectCopyItemCache);
    if (!GuidHelper.IsGuid(productionObjectUid))
      return;
    Guid guid = new Guid(productionObjectUid);
    List<Guid> guidList = new List<Guid>(this.ProcRouteEntryObject.MemberOfAssemblyCopy);
    switch (e.Node.CheckState)
    {
      case CheckState.Unchecked:
        guidList.Remove(guid);
        break;
      case CheckState.Checked:
        if (!guidList.Contains(guid))
        {
          guidList.Add(guid);
          break;
        }
        break;
    }
    this.ProcRouteEntryObject.MemberOfAssemblyCopy = (IEnumerable<Guid>) guidList;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void techNavTreeViewProductionCopyObjects_CheckStateChanging(
    object sender,
    CheckStateEventArgs e)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProcRouteEntryControl));
    this.splitContainerMain = new SplitContainer();
    this.techNavTreeViewExitAssemblies = new TechCardNavTreeViewControl();
    this.techNavTreeViewProductionCopyObjects = new TechCardNavTreeViewControl();
    this.pnlTop = new Panel();
    this.lblProductionReportVersionNumber = new Label();
    this.chbProductionReportVersionMode = new CheckBox();
    this.btnProductionReportObject = new Button();
    this.tbxProductionReportObject = new TextBox();
    this.lblProductionReport = new Label();
    this.splitContainerMain.BeginInit();
    this.splitContainerMain.Panel1.SuspendLayout();
    this.splitContainerMain.Panel2.SuspendLayout();
    this.splitContainerMain.SuspendLayout();
    this.techNavTreeViewExitAssemblies.BeginInit();
    this.techNavTreeViewProductionCopyObjects.BeginInit();
    this.pnlTop.SuspendLayout();
    this.SuspendLayout();
    this.splitContainerMain.Dock = DockStyle.Fill;
    this.splitContainerMain.Location = new Point(0, 61);
    this.splitContainerMain.Name = "splitContainerMain";
    this.splitContainerMain.Orientation = Orientation.Horizontal;
    this.splitContainerMain.Panel1.Controls.Add((Control) this.techNavTreeViewExitAssemblies);
    this.splitContainerMain.Panel2.Controls.Add((Control) this.techNavTreeViewProductionCopyObjects);
    this.splitContainerMain.Size = new Size(665, 419);
    this.splitContainerMain.SplitterDistance = 211;
    this.splitContainerMain.TabIndex = 0;
    this.techNavTreeViewExitAssemblies.AllowDrop = true;
    this.techNavTreeViewExitAssemblies.AllowMultiSelect = false;
    this.techNavTreeViewExitAssemblies.AllowUserPinnedColumns = false;
    this.techNavTreeViewExitAssemblies.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.techNavTreeViewExitAssemblies.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("techNavTreeViewExitAssemblies.CheckedNodesStates");
    this.techNavTreeViewExitAssemblies.CheckoutMode = TechCheckoutMode.Manual;
    this.techNavTreeViewExitAssemblies.CheckRootNode = false;
    this.techNavTreeViewExitAssemblies.ContextMenuBarItem = (ContextMenuBarItem) null;
    this.techNavTreeViewExitAssemblies.DisableCheckedOutColumn = true;
    this.techNavTreeViewExitAssemblies.DisableIMContextMenu = true;
    this.techNavTreeViewExitAssemblies.DisableKeyUpEvents = true;
    this.techNavTreeViewExitAssemblies.Dock = DockStyle.Fill;
    this.techNavTreeViewExitAssemblies.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.techNavTreeViewExitAssemblies.ImageList = (ImageList) null;
    this.techNavTreeViewExitAssemblies.LineStyle = LineStyle.Dot;
    this.techNavTreeViewExitAssemblies.Location = new Point(0, 0);
    this.techNavTreeViewExitAssemblies.Name = "techNavTreeViewExitAssemblies";
    this.techNavTreeViewExitAssemblies.RowEvenStyle.WordWrap = false;
    this.techNavTreeViewExitAssemblies.RowOddStyle.WordWrap = false;
    this.techNavTreeViewExitAssemblies.RowSelectedStyle.WordWrap = false;
    this.techNavTreeViewExitAssemblies.RowStyle.BorderColor = SystemColors.Control;
    this.techNavTreeViewExitAssemblies.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.techNavTreeViewExitAssemblies.RowStyle.BorderWidth = 1;
    this.techNavTreeViewExitAssemblies.RowStyle.WordWrap = false;
    this.techNavTreeViewExitAssemblies.SelectBeforeEdit = true;
    this.techNavTreeViewExitAssemblies.ShowRootRow = false;
    this.techNavTreeViewExitAssemblies.Size = new Size(665, 211);
    this.techNavTreeViewExitAssemblies.SuppressErrorMessages = true;
    this.techNavTreeViewExitAssemblies.TabIndex = 0;
    this.techNavTreeViewExitAssemblies.AfterCreateNode += new EventHandler<NodeEventArgs>(this.techNavTreeViewExitAssemblies_AfterCreateNode);
    this.techNavTreeViewExitAssemblies.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.techNavTreeViewExitAssemblies_CheckStateChanging);
    this.techNavTreeViewExitAssemblies.CheckStateChanged += new EventHandler<NodeEventArgs>(this.techNavTreeViewExitAssemblies_CheckStateChanged);
    this.techNavTreeViewProductionCopyObjects.AllowDrop = true;
    this.techNavTreeViewProductionCopyObjects.AllowMultiSelect = false;
    this.techNavTreeViewProductionCopyObjects.AllowUserPinnedColumns = false;
    this.techNavTreeViewProductionCopyObjects.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.techNavTreeViewProductionCopyObjects.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("techNavTreeViewProductionCopyObjects.CheckedNodesStates");
    this.techNavTreeViewProductionCopyObjects.CheckoutMode = TechCheckoutMode.Manual;
    this.techNavTreeViewProductionCopyObjects.CheckRootNode = false;
    this.techNavTreeViewProductionCopyObjects.ContextMenuBarItem = (ContextMenuBarItem) null;
    this.techNavTreeViewProductionCopyObjects.DisableCheckedOutColumn = true;
    this.techNavTreeViewProductionCopyObjects.DisableIMContextMenu = true;
    this.techNavTreeViewProductionCopyObjects.DisableKeyUpEvents = true;
    this.techNavTreeViewProductionCopyObjects.Dock = DockStyle.Fill;
    this.techNavTreeViewProductionCopyObjects.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.techNavTreeViewProductionCopyObjects.ImageList = (ImageList) null;
    this.techNavTreeViewProductionCopyObjects.LineStyle = LineStyle.Dot;
    this.techNavTreeViewProductionCopyObjects.Location = new Point(0, 0);
    this.techNavTreeViewProductionCopyObjects.Name = "techNavTreeViewProductionCopyObjects";
    this.techNavTreeViewProductionCopyObjects.RowEvenStyle.WordWrap = false;
    this.techNavTreeViewProductionCopyObjects.RowOddStyle.WordWrap = false;
    this.techNavTreeViewProductionCopyObjects.RowSelectedStyle.WordWrap = false;
    this.techNavTreeViewProductionCopyObjects.RowStyle.BorderColor = SystemColors.Control;
    this.techNavTreeViewProductionCopyObjects.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.techNavTreeViewProductionCopyObjects.RowStyle.BorderWidth = 1;
    this.techNavTreeViewProductionCopyObjects.RowStyle.WordWrap = false;
    this.techNavTreeViewProductionCopyObjects.SelectBeforeEdit = true;
    this.techNavTreeViewProductionCopyObjects.ShowRootRow = false;
    this.techNavTreeViewProductionCopyObjects.Size = new Size(665, 204);
    this.techNavTreeViewProductionCopyObjects.SuppressErrorMessages = true;
    this.techNavTreeViewProductionCopyObjects.TabIndex = 0;
    this.techNavTreeViewProductionCopyObjects.AfterCreateNode += new EventHandler<NodeEventArgs>(this.techNavTreeViewProductionCopyObjects_AfterCreateNode);
    this.techNavTreeViewProductionCopyObjects.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.techNavTreeViewProductionCopyObjects_CheckStateChanging);
    this.techNavTreeViewProductionCopyObjects.CheckStateChanged += new EventHandler<NodeEventArgs>(this.techNavTreeViewProductionCopyObjects_CheckStateChanged);
    this.pnlTop.Controls.Add((Control) this.lblProductionReportVersionNumber);
    this.pnlTop.Controls.Add((Control) this.chbProductionReportVersionMode);
    this.pnlTop.Controls.Add((Control) this.btnProductionReportObject);
    this.pnlTop.Controls.Add((Control) this.tbxProductionReportObject);
    this.pnlTop.Controls.Add((Control) this.lblProductionReport);
    this.pnlTop.Dock = DockStyle.Top;
    this.pnlTop.Location = new Point(0, 0);
    this.pnlTop.Name = "pnlTop";
    this.pnlTop.Size = new Size(665, 61);
    this.pnlTop.TabIndex = 1;
    this.lblProductionReportVersionNumber.AutoSize = true;
    this.lblProductionReportVersionNumber.Location = new Point(354, 39);
    this.lblProductionReportVersionNumber.Name = "lblProductionReportVersionNumber";
    this.lblProductionReportVersionNumber.Size = new Size(0, 13);
    this.lblProductionReportVersionNumber.TabIndex = 4;
    this.chbProductionReportVersionMode.AutoSize = true;
    this.chbProductionReportVersionMode.Location = new Point(182, 38);
    this.chbProductionReportVersionMode.Name = "chbProductionReportVersionMode";
    this.chbProductionReportVersionMode.Size = new Size(155, 17);
    this.chbProductionReportVersionMode.TabIndex = 3;
    this.chbProductionReportVersionMode.Text = "Входимость в версию ПВ";
    this.chbProductionReportVersionMode.UseVisualStyleBackColor = true;
    this.chbProductionReportVersionMode.CheckedChanged += new EventHandler(this.chbProductionReportVersionMode_CheckedChanged);
    this.btnProductionReportObject.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnProductionReportObject.Location = new Point(621, 10);
    this.btnProductionReportObject.Name = "btnProductionReportObject";
    this.btnProductionReportObject.Size = new Size(24, 24);
    this.btnProductionReportObject.TabIndex = 2;
    this.btnProductionReportObject.Text = "...";
    this.btnProductionReportObject.UseVisualStyleBackColor = true;
    this.btnProductionReportObject.Click += new EventHandler(this.btnProductionReportObject_Click);
    this.tbxProductionReportObject.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbxProductionReportObject.Location = new Point(182, 12);
    this.tbxProductionReportObject.Name = "tbxProductionReportObject";
    this.tbxProductionReportObject.ReadOnly = true;
    this.tbxProductionReportObject.Size = new Size(439, 20);
    this.tbxProductionReportObject.TabIndex = 1;
    this.lblProductionReport.AutoSize = true;
    this.lblProductionReport.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblProductionReport.Location = new Point(14, 15);
    this.lblProductionReport.Name = "lblProductionReport";
    this.lblProductionReport.Size = new Size(162, 13);
    this.lblProductionReport.TabIndex = 0;
    this.lblProductionReport.Text = "Производственная ведомость";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainerMain);
    this.Controls.Add((Control) this.pnlTop);
    this.Name = nameof (ProcRouteEntryControl);
    this.Size = new Size(665, 480);
    this.splitContainerMain.Panel1.ResumeLayout(false);
    this.splitContainerMain.Panel2.ResumeLayout(false);
    this.splitContainerMain.EndInit();
    this.splitContainerMain.ResumeLayout(false);
    this.techNavTreeViewExitAssemblies.EndInit();
    this.techNavTreeViewProductionCopyObjects.EndInit();
    this.pnlTop.ResumeLayout(false);
    this.pnlTop.PerformLayout();
    this.ResumeLayout(false);
  }
}
