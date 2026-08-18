// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes.CehRoutesElemsListView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Localization;
using DevExpress.IM.XtraTreeList.Menu;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.TechCard.Client.Tools.Controls;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Ceh_Routes;

/// <summary>Контрол выбора элементов РМ</summary>
public class CehRoutesElemsListView : UserControl
{
  /// <summary>Идентификатор версии изделия</summary>
  private long _articleObjectId;
  /// <summary>
  /// Ид. версии вида производства
  /// </summary>
  private long _productionObjectId;
  /// <summary>Режим выбора РЭ (по одному РМ/по нескольким РМ)</summary>
  private bool _multiRoute = true;
  /// <summary>
  /// 
  /// </summary>
  private readonly List<long> _workTypeList;
  private readonly Dictionary<long, List<long>> _cehZagotLists;
  /// <summary>Список маршрутов обработки</summary>
  private readonly ProcRouteClassList _procRouteNodeList;
  private readonly RouteElemClassList _routeElemNodes;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  /// <summary>Панель с кнопками</summary>
  public Panel pnlButtons;
  /// <summary>Кнопка "Отмена"</summary>
  public Button btnCancel;
  /// <summary>Кнопка "ОК"</summary>
  public Button btnApply;
  private SplitContainer splitContainer1;
  private SplitContainer splitContainer;
  private GroupBox grbCehRoutes;
  private GroupBox grbElemRoutes;
  /// <summary>Список РЭ</summary>
  public TreeList tlElemRoutes;
  private TreeListColumn treeListColumn1;
  private TreeListColumn treeListColumn2;
  private GroupBox grbProcRoutes;
  private ContextMenuStrip cmsElemRoutes;
  private ToolStripMenuItem tsmiSelectAll;
  private ToolStripMenuItem tsmiClearAll;
  private ToolStripMenuItem tsmiInvert;
  internal TechCardGrid igrdProcRoutes;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private iGCellStyle igrdProcRoutesCol0CellStyle;
  private iGColHdrStyle igrdProcRoutesCol0ColHdrStyle;
  internal TechCardGrid igrdCehRoutes;

  /// <summary>Initialize services</summary>
  private void InitializeServices()
  {
  }

  /// <summary>Initialize custom controls</summary>
  private void InitializeCustomComponents()
  {
    this.igrdProcRoutes.ShowGridHeaderMenu += new iGColHdrMouseUpEventHandler(TechCardClientIGridUtils.ShowTechGridHeaderMenu);
    this.igrdCehRoutes.ShowGridHeaderMenu += new iGColHdrMouseUpEventHandler(TechCardClientIGridUtils.ShowTechGridHeaderMenu);
    this.tlElemRoutes.ShowTreeListMenu += new TreeListMenuEventHandler(TechcardClientTreeListUtils.TreeList_ShowTreeListMenu);
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this.tlElemRoutes.StateImageList = service.ImageList;
      this.tlElemRoutes.CheckedStateIndex = service.ImageIndex("imgChecked");
      this.tlElemRoutes.UncheckedStateIndex = service.ImageIndex("imgUnchecked");
      this.tlElemRoutes.GrayedStateIndex = service.ImageIndex("imgGrayed");
    }
    else
      this.tlElemRoutes.StateImageList = (ImageList) null;
  }

  /// <summary>Get proc route</summary>
  /// <returns></returns>
  private ProcRouteClass GetSelectedProcRouteNode()
  {
    return TechCardClientIGridUtils.GetRowData(this.igrdProcRoutes.CurRow) as ProcRouteClass;
  }

  /// <summary>Get selected ceh route</summary>
  /// <returns></returns>
  private CehRouteClass GetSelectedCehRouteClass()
  {
    return TechCardClientIGridUtils.GetRowData(this.igrdCehRoutes.CurRow) as CehRouteClass;
  }

  /// <summary>Get route template node</summary>
  /// <param name="tempRoute"></param>
  /// <returns></returns>
  private TreeListNode GetTempRouteNode(TemplRouteClass tempRoute)
  {
    if (tempRoute == null)
      return (TreeListNode) null;
    foreach (TreeListNode node in this.tlElemRoutes.Nodes)
    {
      if (object.Equals(TechcardClientTreeListUtils.GetNodeData(node), (object) tempRoute))
        return node;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(tempRoute.ObjectId);
      return dbObject == null || !dbObject.isParentType(TechCardConsts.ObjectTypes.TemplRouteBaseGUID) ? (TreeListNode) null : TechcardClientTreeListUtils.AddObjectToTreeList(this.tlElemRoutes, (IDBAttributable) dbObject, (object) tempRoute);
    }
  }

  /// <summary>Fill proc routes list</summary>
  private void FillProcRoutesList()
  {
    this.igrdProcRoutes.BeginUpdate();
    try
    {
      this.igrdProcRoutes.Rows.Clear();
      if (this.igrdProcRoutes.Cols.Count == 0 || this._procRouteNodeList.Count == 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (ProcRouteClass procRouteNode in (List<ProcRouteClass>) this._procRouteNodeList)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(procRouteNode.ObjectId);
          if (dbObject != null && dbObject.isParentType(TechCardConsts.ObjectTypes.ProcRoutingGUID))
            TechCardClientIGridUtils.AddObjectToGrid(this.igrdProcRoutes, (IDBAttributable) dbObject, (object) procRouteNode);
        }
      }
    }
    finally
    {
      this.igrdProcRoutes.EndUpdate();
      if (this.igrdProcRoutes.Rows.Count > 0)
      {
        this.igrdProcRoutes.CurRow = this.igrdProcRoutes.Rows[0];
        this.igrdProcRoutes_CurRowChanged((object) this.igrdProcRoutes, (EventArgs) null);
      }
    }
  }

  /// <summary>Fill ceh routes list</summary>
  private void FillCehRoutesList()
  {
    this.igrdCehRoutes.BeginUpdate();
    try
    {
      this.igrdCehRoutes.Rows.Clear();
      this.igrdCehRoutes.CurRow = (iGRow) null;
      ProcRouteClass selectedProcRouteNode = this.GetSelectedProcRouteNode();
      if (this.igrdCehRoutes.Cols.Count == 0 || selectedProcRouteNode == null)
        return;
      CehRoutesClassList cehRouteList = selectedProcRouteNode.CehRouteList;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (CehRouteClass data in (CustomTechClassList<CehRouteClass>) cehRouteList)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(data.ObjectId);
          if (dbObject != null && MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, TechCardConsts.ObjectTypes.CehRouteID))
            TechCardClientIGridUtils.AddObjectToGrid(this.igrdCehRoutes, (IDBAttributable) dbObject, (object) data);
        }
      }
    }
    finally
    {
      this.igrdCehRoutes.EndUpdate();
      if (this.igrdCehRoutes.Rows.Count > 0)
        this.igrdCehRoutes.CurRow = this.igrdCehRoutes.Rows[0];
      this.igrdCehRoutes_CurRowChanged((object) this.igrdCehRoutes, (EventArgs) null);
    }
  }

  /// <summary>Fill production list</summary>
  private void FillProdSostavList()
  {
    this._workTypeList.Clear();
    if (this._productionObjectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in TechCardUtils.GetChildSostavTree(this._productionObjectId, sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, true))
      {
        if (sostavTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavTreeItem.ObjectTypeID, TechCardConsts.ObjectTypes.WorkTypeObjectID))
          this._workTypeList.Add(sostavTreeItem.PartID);
      }
    }
  }

  /// <summary>Fill elem routes list</summary>
  private void FillElemRoutesList()
  {
    this.tlElemRoutes.BeginUpdate();
    try
    {
      this.tlElemRoutes.CheckStateChanged -= new NodeEventHandler(this.tlElemRoutes_CheckStateChanged);
      this.tlElemRoutes.Nodes.Clear();
      CehRouteClass selectedCehRouteClass = this.GetSelectedCehRouteClass();
      if (this.tlElemRoutes.Columns.Count == 0 || selectedCehRouteClass == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) selectedCehRouteClass.RouteElementList)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(routeElement.ObjectId);
          if (dbObject != null && dbObject.isParentType(TechCardConsts.ObjectTypes.ElemRouteGUID))
            TechcardClientTreeListUtils.AddObjectToTreeList(this.tlElemRoutes, (IDBAttributable) dbObject, (object) routeElement, (TreeListNode) null);
        }
        foreach (TemplRouteClass template in selectedCehRouteClass.TemplateList)
        {
          TreeListNode tempRouteNode = this.GetTempRouteNode(template);
          foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) template.RouteElementList)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(routeElement.ObjectId);
            if (dbObject != null && dbObject.isParentType(TechCardConsts.ObjectTypes.ElemRouteGUID))
              TechcardClientTreeListUtils.AddObjectToTreeList(this.tlElemRoutes, (IDBAttributable) dbObject, (object) routeElement, tempRouteNode);
          }
        }
      }
      this.tlElemRoutes.FullExpand();
    }
    finally
    {
      this.tlElemRoutes.EndUpdate();
      this.tlElemRoutes.CheckStateChanged += new NodeEventHandler(this.tlElemRoutes_CheckStateChanged);
      this.UpdateStatusElemRoutesList();
      this.UpdateButtons();
    }
  }

  /// <summary>Получение списка цехов-заходов для РЭ</summary>
  /// <param name="routeElem">Класс РЭ</param>
  /// <returns></returns>
  private List<long> FillCehZagotList(CehRouteElementClass routeElem)
  {
    if (this._cehZagotLists.ContainsKey(routeElem.LinkID))
      return this._cehZagotLists[routeElem.LinkID];
    List<long> longList = new List<long>();
    this._cehZagotLists.Add(routeElem.LinkID, longList);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(routeElem.LinkID, false);
      if (relation == null)
        return longList;
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      conditionStructureList.Add(new ConditionStructure(TechCardConsts.AttributeTypes.ElemRouteLinkAttrID, RelationalOperators.Equal, (object) relation.GUID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Relation));
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehZahodObjectID);
      conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto));
      long objectId = routeElem.ObjectId;
      IUserSession session = sessionKeeper.Session;
      int[] relations = new int[1]
      {
        TechCardConsts.RelTypes.TechRouteRelationID
      };
      ConditionStructure[] array = conditionStructureList.ToArray();
      foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in TechCardUtils.GetChildSostavTree(objectId, session, (IEnumerable<int>) relations, false, array))
      {
        if (MetaDataHelper.IsObjectTypeChildOf(sostavSortedTreeItem.PartType, TechCardConsts.ObjectTypes.CehZahodObjectID))
          longList.Add(sostavSortedTreeItem.PartID);
      }
    }
    return longList;
  }

  /// <summary>Update elem routes list</summary>
  private void UpdateStatusElemRoutesList()
  {
    this.tlElemRoutes.BeginUpdate();
    try
    {
      this.tlElemRoutes.CheckStateChanged -= new NodeEventHandler(this.tlElemRoutes_CheckStateChanged);
      foreach (TreeListNode node in this.tlElemRoutes.Nodes)
        this.UpdateStatusElemRoutesNode(node);
    }
    finally
    {
      this.tlElemRoutes.CheckStateChanged += new NodeEventHandler(this.tlElemRoutes_CheckStateChanged);
      this.tlElemRoutes.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="node"></param>
  private void UpdateStatusElemRoutesNode(TreeListNode node)
  {
    if (node == null)
      return;
    ProcRouteClass selectedProcRouteNode = this.GetSelectedProcRouteNode();
    CehRouteClass selectedCehRouteClass = this.GetSelectedCehRouteClass();
    node.CheckState = TechcardClientTreeListUtils.GetNodeData(node) is CehRouteElementClass nodeData ? (this._workTypeList.Contains(nodeData.WorkTypeID) ? (this.FillCehZagotList(nodeData).Count != 0 ? (this._routeElemNodes.IndexOf(selectedProcRouteNode.ObjectId, selectedCehRouteClass.ObjectId, nodeData.LinkID, nodeData.ObjectId) == -1 ? CheckState.Indeterminate : CheckState.Checked) : (this._routeElemNodes.IndexOf(selectedProcRouteNode.ObjectId, selectedCehRouteClass.ObjectId, nodeData.LinkID, nodeData.ObjectId) == -1 ? CheckState.Unchecked : CheckState.Checked)) : CheckState.Indeterminate) : CheckState.Indeterminate;
    foreach (TreeListNode node1 in node.Nodes)
      this.UpdateStatusElemRoutesNode(node1);
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateButtons() => this.btnApply.Enabled = this._routeElemNodes.Count > 0;

  /// <summary>Конструктор</summary>
  public CehRoutesElemsListView()
  {
    this.InitializeComponent();
    this.InitializeServices();
    this.InitializeCustomComponents();
    this._workTypeList = new List<long>();
    this._cehZagotLists = new Dictionary<long, List<long>>();
    this._procRouteNodeList = new ProcRouteClassList();
    this._routeElemNodes = new RouteElemClassList();
  }

  /// <summary>Dispose</summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Идентификатор версии изделия</summary>
  public long ArticleObjectId
  {
    get => this._articleObjectId;
    set => this._articleObjectId = value;
  }

  /// <summary>Ид. версии вида производства</summary>
  public long ProductionObjectId
  {
    get => this._productionObjectId;
    set
    {
      if (this._productionObjectId == value)
        return;
      this._productionObjectId = value;
      this.FillProdSostavList();
    }
  }

  /// <summary>Ceh route object id</summary>
  internal long RouteObjectId
  {
    get
    {
      CehRouteClass selectedCehRouteClass = this.GetSelectedCehRouteClass();
      return selectedCehRouteClass != null ? selectedCehRouteClass.ObjectId : 0L;
    }
    set
    {
      if (value == 0L)
        return;
      CehRouteClass selectedCehRouteClass = this.GetSelectedCehRouteClass();
      if (selectedCehRouteClass != null && selectedCehRouteClass.ObjectId != value)
      {
        foreach (iGRow row in (IEnumerable) this.igrdCehRoutes.Rows)
        {
          if (TechCardClientIGridUtils.GetRowData(row) is CehRouteClass rowData && rowData.ObjectId == value)
          {
            this.igrdCehRoutes.CurRow = row;
            this.FillElemRoutesList();
            break;
          }
        }
      }
      else
        this.FillElemRoutesList();
    }
  }

  /// <summary>Список выбранных РЭ</summary>
  internal long MoObjectId
  {
    get
    {
      ProcRouteClass selectedProcRouteNode = this.GetSelectedProcRouteNode();
      return selectedProcRouteNode == null ? 0L : selectedProcRouteNode.ObjectId;
    }
    set
    {
      if (value == 0L)
        return;
      ProcRouteClass selectedProcRouteNode = this.GetSelectedProcRouteNode();
      if (selectedProcRouteNode != null && selectedProcRouteNode.ObjectId != value)
      {
        foreach (iGRow row in (IEnumerable) this.igrdProcRoutes.Rows)
        {
          if (TechCardClientIGridUtils.GetRowData(row) is ProcRouteClass rowData && rowData.ObjectId == value)
          {
            this.igrdProcRoutes.CurRow = row;
            this.FillCehRoutesList();
            this.igrdProcRoutes_CurRowChanged((object) this.igrdProcRoutes, (EventArgs) null);
            break;
          }
        }
      }
      else
        this.FillCehRoutesList();
    }
  }

  /// <summary>Режим выбора РЭ (по одному РМ/по нескольким РМ)</summary>
  public bool MultiRoute
  {
    get => this._multiRoute;
    set => this._multiRoute = value;
  }

  /// <summary>Route elem nodes</summary>
  internal RouteElemClassList RouteElemNodes
  {
    get => this._routeElemNodes;
    set
    {
      this._routeElemNodes.Clear();
      if (value != null)
        this._routeElemNodes.AddRange((IEnumerable<RouteElemClass>) value);
      this.UpdateStatusElemRoutesList();
      this.UpdateButtons();
    }
  }

  /// <summary>Загрузка инфы</summary>
  public void LoadData()
  {
    this._procRouteNodeList.Clear();
    if (this._articleObjectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingID);
      conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive1.ToArray(), LogicalOperators.NONE, 0, false));
      long articleObjectId = this._articleObjectId;
      IUserSession session = sessionKeeper.Session;
      int[] relations = new int[2]
      {
        TechCardConsts.RelTypes.TechRelationID,
        TechCardConsts.RelTypes.SimpleRelationID
      };
      ConditionStructure[] array = conditionStructureList.ToArray();
      foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in TechCardUtils.GetChildSostavTree(articleObjectId, session, (IEnumerable<int>) relations, false, array))
      {
        if (sostavSortedTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavSortedTreeItem.PartType, TechCardConsts.ObjectTypes.ProcRoutingID))
          this._procRouteNodeList.Add(new ProcRouteClass(sostavSortedTreeItem.PartID));
      }
      conditionStructureList.Clear();
      List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID);
      conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive2.ToArray(), LogicalOperators.NONE, 0, false));
      foreach (ProcRouteClass procRouteNode in (List<ProcRouteClass>) this._procRouteNodeList)
      {
        foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in TechCardUtils.GetChildSostavTree(procRouteNode.ObjectId, sessionKeeper.Session, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, false, conditionStructureList.ToArray()))
        {
          if (sostavSortedTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavSortedTreeItem.PartType, TechCardConsts.ObjectTypes.CehRouteID))
          {
            CehRouteClass cehRouteClass = new CehRouteClass(sostavSortedTreeItem.PartID);
            cehRouteClass.LoadData(sessionKeeper.Session);
            procRouteNode.CehRouteList.Add(cehRouteClass);
          }
        }
      }
      this.FillProcRoutesList();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlElemRoute_TreeListMenuItemClick(object sender, TreeListMenuItemClickEventArgs e)
  {
    if (e.MenuType != TreeListMenuType.Column || e.MenuItem.Tag == null || !e.MenuItem.Tag.Equals((object) TreeListStringId.MenuColumnColumnCustomization))
      return;
    if (TechCardClientTreeListCustomizationFrom.ShowModal(TechCardConsts.ObjectTypes.ElemRouteID, this.tlElemRoutes, this.grbElemRoutes.Text))
      this.FillElemRoutesList();
    e.Handled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlElemRoutes_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (e.Node == null)
      return;
    ProcRouteClass selectedProcRouteNode = this.GetSelectedProcRouteNode();
    CehRouteClass selectedCehRouteClass = this.GetSelectedCehRouteClass();
    if (!(TechcardClientTreeListUtils.GetNodeData(e.Node) is CehRouteElementClass nodeData1))
      return;
    int index = this._routeElemNodes.IndexOf(selectedProcRouteNode.ObjectId, selectedCehRouteClass.ObjectId, nodeData1.LinkID, nodeData1.ObjectId);
    if (e.Node.CheckState == CheckState.Unchecked)
    {
      if (index != -1)
        this._routeElemNodes.RemoveAt(index);
    }
    else if (index == -1)
    {
      RouteElemClass routeElemClass = new RouteElemClass(selectedProcRouteNode.ObjectId, selectedCehRouteClass.ObjectId, nodeData1.LinkID, nodeData1.ObjectId);
      routeElemClass.RouteElemOrderID = nodeData1.OrderID;
      if (TechcardClientTreeListUtils.GetNodeData(e.Node.ParentNode) is TemplRouteClass nodeData2)
        routeElemClass.TemplateOrderID = nodeData2.OrderID;
      this._routeElemNodes.Add(routeElemClass);
      this._routeElemNodes.Sort((IComparer<RouteElemClass>) new RouteElemClass.SortComparer());
    }
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlElemRoutes_CheckStateChanging(object sender, CheckStateEventArgs e)
  {
    e.NewValue = e.OldValue == CheckState.Indeterminate ? e.OldValue : e.NewValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmsElemRoutes_Opening(object sender, CancelEventArgs e)
  {
    bool flag = this.tlElemRoutes.Nodes.Count > 0;
    this.tsmiSelectAll.Enabled = flag;
    this.tsmiClearAll.Enabled = flag;
    this.tsmiInvert.Enabled = flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiSelectAll_Click(object sender, EventArgs e)
  {
    this.tlElemRoutes.BeginUpdate();
    try
    {
      foreach (TreeListNode node1 in this.tlElemRoutes.Nodes)
      {
        foreach (TreeListNode node2 in node1.Nodes)
          node2.CheckState = CheckState.Checked;
      }
    }
    finally
    {
      this.tlElemRoutes.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiClearAll_Click(object sender, EventArgs e)
  {
    this.tlElemRoutes.BeginUpdate();
    try
    {
      foreach (TreeListNode node1 in this.tlElemRoutes.Nodes)
      {
        foreach (TreeListNode node2 in node1.Nodes)
          node2.CheckState = CheckState.Unchecked;
      }
    }
    finally
    {
      this.tlElemRoutes.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiInvert_Click(object sender, EventArgs e)
  {
    this.tlElemRoutes.BeginUpdate();
    try
    {
      foreach (TreeListNode node1 in this.tlElemRoutes.Nodes)
      {
        foreach (TreeListNode node2 in node1.Nodes)
          node2.CheckState = node2.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
      }
    }
    finally
    {
      this.tlElemRoutes.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void igrdProcRoutes_HeaderMenuCustomizeClick(object sender, EventArgs e)
  {
    if (!TechCardGridCustomizeForm.ShowModal(TechCardConsts.ObjectTypes.ProcRoutingID, this.igrdProcRoutes))
      return;
    this.FillProcRoutesList();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void igrdProcRoutes_CurRowChanged(object sender, EventArgs e)
  {
    if (this.GetSelectedProcRouteNode() != null && !this.MultiRoute)
      this._routeElemNodes.Clear();
    this.FillCehRoutesList();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void igrdCehRoutes_HeaderMenuCustomizeClick(object sender, EventArgs e)
  {
    if (!TechCardGridCustomizeForm.ShowModal(TechCardConsts.ObjectTypes.CehRouteID, this.igrdCehRoutes))
      return;
    this.FillCehRoutesList();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void igrdCehRoutes_CurRowChanged(object sender, EventArgs e)
  {
    if (this.GetSelectedCehRouteClass() != null && !this.MultiRoute)
      this._routeElemNodes.Clear();
    this.FillElemRoutesList();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CehRoutesElemsListView));
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    this.igrdProcRoutesCol0CellStyle = new iGCellStyle(true);
    this.igrdProcRoutesCol0ColHdrStyle = new iGColHdrStyle(true);
    this.splitContainer1 = new SplitContainer();
    this.grbProcRoutes = new GroupBox();
    this.igrdProcRoutes = new TechCardGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    this.splitContainer = new SplitContainer();
    this.grbCehRoutes = new GroupBox();
    this.igrdCehRoutes = new TechCardGrid();
    this.grbElemRoutes = new GroupBox();
    this.tlElemRoutes = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeListColumn2 = new TreeListColumn();
    this.cmsElemRoutes = new ContextMenuStrip(this.components);
    this.tsmiSelectAll = new ToolStripMenuItem();
    this.tsmiClearAll = new ToolStripMenuItem();
    this.tsmiInvert = new ToolStripMenuItem();
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.grbProcRoutes.SuspendLayout();
    ((ISupportInitialize) this.igrdProcRoutes).BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.grbCehRoutes.SuspendLayout();
    ((ISupportInitialize) this.igrdCehRoutes).BeginInit();
    this.grbElemRoutes.SuspendLayout();
    this.tlElemRoutes.BeginInit();
    this.cmsElemRoutes.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.grbProcRoutes);
    this.splitContainer1.Panel2.Controls.Add((Control) this.splitContainer);
    this.grbProcRoutes.Controls.Add((Control) this.igrdProcRoutes);
    componentResourceManager.ApplyResources((object) this.grbProcRoutes, "grbProcRoutes");
    this.grbProcRoutes.Name = "grbProcRoutes";
    this.grbProcRoutes.TabStop = false;
    iGcolPattern1.CellStyle = this.igrdProcRoutesCol0CellStyle;
    iGcolPattern1.ColHdrStyle = this.igrdProcRoutesCol0ColHdrStyle;
    this.igrdProcRoutes.Cols.AddRange(new iGColPattern[1]
    {
      iGcolPattern1
    });
    this.igrdProcRoutes.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this.igrdProcRoutes.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this.igrdProcRoutes.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this.igrdProcRoutes.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    componentResourceManager.ApplyResources((object) this.igrdProcRoutes, "igrdProcRoutes");
    this.igrdProcRoutes.Header.Height = (int) componentResourceManager.GetObject("igrdProcRoutes.Header.Height");
    this.igrdProcRoutes.Name = "igrdProcRoutes";
    this.igrdProcRoutes.ReadOnly = true;
    this.igrdProcRoutes.RowMode = true;
    this.igrdProcRoutes.RowModeHasCurCell = true;
    this.igrdProcRoutes.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this.igrdProcRoutes.CurRowChanged += new EventHandler(this.igrdProcRoutes_CurRowChanged);
    this.igrdProcRoutes.HeaderMenuCustomizeClick += new EventHandler(this.igrdProcRoutes_HeaderMenuCustomizeClick);
    componentResourceManager.ApplyResources((object) this.splitContainer, "splitContainer");
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.Controls.Add((Control) this.grbCehRoutes);
    this.splitContainer.Panel2.Controls.Add((Control) this.grbElemRoutes);
    this.grbCehRoutes.Controls.Add((Control) this.igrdCehRoutes);
    componentResourceManager.ApplyResources((object) this.grbCehRoutes, "grbCehRoutes");
    this.grbCehRoutes.Name = "grbCehRoutes";
    this.grbCehRoutes.TabStop = false;
    iGcolPattern2.CellStyle = this.igrdProcRoutesCol0CellStyle;
    iGcolPattern2.ColHdrStyle = this.igrdProcRoutesCol0ColHdrStyle;
    this.igrdCehRoutes.Cols.AddRange(new iGColPattern[1]
    {
      iGcolPattern2
    });
    this.igrdCehRoutes.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this.igrdCehRoutes.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this.igrdCehRoutes.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height1");
    this.igrdCehRoutes.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight1");
    componentResourceManager.ApplyResources((object) this.igrdCehRoutes, "igrdCehRoutes");
    this.igrdCehRoutes.Header.Height = (int) componentResourceManager.GetObject("igrdCehRoutes.Header.Height");
    this.igrdCehRoutes.Name = "igrdCehRoutes";
    this.igrdCehRoutes.ReadOnly = true;
    this.igrdCehRoutes.RowMode = true;
    this.igrdCehRoutes.RowModeHasCurCell = true;
    this.igrdCehRoutes.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this.igrdCehRoutes.CurRowChanged += new EventHandler(this.igrdCehRoutes_CurRowChanged);
    this.igrdCehRoutes.HeaderMenuCustomizeClick += new EventHandler(this.igrdCehRoutes_HeaderMenuCustomizeClick);
    this.grbElemRoutes.Controls.Add((Control) this.tlElemRoutes);
    componentResourceManager.ApplyResources((object) this.grbElemRoutes, "grbElemRoutes");
    this.grbElemRoutes.Name = "grbElemRoutes";
    this.grbElemRoutes.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tlElemRoutes, "tlElemRoutes");
    this.tlElemRoutes.CheckBoxes = CheckBoxesStyle.ThreeState;
    this.tlElemRoutes.Columns.AddRange(new TreeListColumn[2]
    {
      this.treeListColumn1,
      this.treeListColumn2
    });
    this.tlElemRoutes.ContextMenuStrip = this.cmsElemRoutes;
    this.tlElemRoutes.Name = "tlElemRoutes";
    this.tlElemRoutes.TreeListMenuItemClick += new TreeListMenuItemClickEventHandler(this.tlElemRoute_TreeListMenuItemClick);
    this.tlElemRoutes.CheckStateChanged += new NodeEventHandler(this.tlElemRoutes_CheckStateChanged);
    this.tlElemRoutes.CheckStateChanging += new CheckStateChangingEventHandler(this.tlElemRoutes_CheckStateChanging);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this.treeListColumn2, "treeListColumn2");
    this.treeListColumn2.Name = "treeListColumn2";
    this.cmsElemRoutes.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsmiSelectAll,
      (ToolStripItem) this.tsmiClearAll,
      (ToolStripItem) this.tsmiInvert
    });
    this.cmsElemRoutes.Name = "cmsElemRoutes";
    componentResourceManager.ApplyResources((object) this.cmsElemRoutes, "cmsElemRoutes");
    this.cmsElemRoutes.Opening += new CancelEventHandler(this.cmsElemRoutes_Opening);
    this.tsmiSelectAll.Name = "tsmiSelectAll";
    componentResourceManager.ApplyResources((object) this.tsmiSelectAll, "tsmiSelectAll");
    this.tsmiSelectAll.Click += new EventHandler(this.tsmiSelectAll_Click);
    this.tsmiClearAll.Name = "tsmiClearAll";
    componentResourceManager.ApplyResources((object) this.tsmiClearAll, "tsmiClearAll");
    this.tsmiClearAll.Click += new EventHandler(this.tsmiClearAll_Click);
    this.tsmiInvert.Name = "tsmiInvert";
    componentResourceManager.ApplyResources((object) this.tsmiInvert, "tsmiInvert");
    this.tsmiInvert.Click += new EventHandler(this.tsmiInvert_Click);
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.pnlButtons);
    this.Name = nameof (CehRoutesElemsListView);
    this.Tag = (object) "  ";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.ResumeLayout(false);
    this.grbProcRoutes.ResumeLayout(false);
    ((ISupportInitialize) this.igrdProcRoutes).EndInit();
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.ResumeLayout(false);
    this.grbCehRoutes.ResumeLayout(false);
    ((ISupportInitialize) this.igrdCehRoutes).EndInit();
    this.grbElemRoutes.ResumeLayout(false);
    this.tlElemRoutes.EndInit();
    this.cmsElemRoutes.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
