// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRoute2TpObjView
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
using Intermech.Localization;
using Intermech.TechCard.Client.Resources;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Контрол для привязки элементов к расцеховке</summary>
public class CehRoute2TpObjView : UserControl
{
  /// <summary>Ид. версии МО</summary>
  private long _moObjId;
  /// <summary>тип объектов "Расцеховочный маршрут"</summary>
  private int _cehRouteTypeId = -1;
  /// <summary>тип объектов "Расцеховочный элемент"</summary>
  private int _elemRouteTypeId = -1;
  /// <summary>тип объектов "Техпроцесс единичный"</summary>
  private int _techProcTypeId = -1;
  /// <summary>Список расцеховочных маршрутов</summary>
  private readonly CehRoutesClassList _cehRoutesList;
  /// <summary>Список техпроцессов</summary>
  private readonly TechProcClassList _techProcList;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal Panel pnlBottom;
  internal Panel pnlButtons;
  internal Button btnApply;
  internal Button btnCancel;
  private SplitContainer splitContainer1;
  private GroupBox grbCehRoutes;
  private SplitContainer splitContainer2;
  private GroupBox grbRouteElems;
  private SplitContainer splitContainer3;
  private GroupBox grbTpFullList;
  private GroupBox grbTpLinkList;
  internal TreeList tlCehRoutes;
  private TreeListColumn treeListColumn1;
  private TreeListColumn treeListColumn2;
  internal TreeList tlElemRoutes;
  private TreeListColumn treeListColumn3;
  private TreeListColumn treeListColumn4;
  internal TreeList tlTpAll;
  private TreeListColumn treeListColumn5;
  private TreeListColumn treeListColumn6;
  private ImageList imageList;
  private TreeView tvTpLink;
  private ContextMenuStrip cmTPAll;
  private ToolStripMenuItem tsmiTPAllTPLink;
  private ContextMenuStrip cmTPLink;
  private ToolStripMenuItem tsmiTPLinkDelete;
  private ToolStripMenuItem tsmiTPLinkMain;

  /// <summary>Инициализация параметров класса</summary>
  private void InitData()
  {
    this._moObjId = 0L;
    this._cehRouteTypeId = TechCardConsts.ObjectTypes.CehRouteID;
    this._elemRouteTypeId = TechCardConsts.ObjectTypes.ElemRouteID;
    this._techProcTypeId = TechCardConsts.ObjectTypes.TechProcEdinID;
    this.InitCustomControls();
  }

  /// <summary>Инициализация контролов</summary>
  private void InitCustomControls()
  {
    this.tlCehRoutes.ShowTreeListMenu += new TreeListMenuEventHandler(TechcardClientTreeListUtils.TreeList_ShowTreeListMenu);
    this.tlElemRoutes.ShowTreeListMenu += new TreeListMenuEventHandler(TechcardClientTreeListUtils.TreeList_ShowTreeListMenu);
    this.tlTpAll.ShowTreeListMenu += new TreeListMenuEventHandler(TechcardClientTreeListUtils.TreeList_ShowTreeListMenu);
    this.LoadIcons();
  }

  /// <summary>Зашрузка икон</summary>
  private void LoadIcons()
  {
    this.imageList.Images.Clear();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this.imageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgUnchecked")]);
      this.imageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgChecked")]);
      this.imageList.Images.Add(service.ImageList.Images[service.ImageIndex("imgGrayed")]);
    }
    Bitmap bitmap = ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.GrayEmpty.bmp");
    if (bitmap == null)
      return;
    this.imageList.Images.AddStrip((Image) bitmap);
  }

  /// <summary>Заполнение списка РМ</summary>
  private void CehRoutes_Fill()
  {
    this.tlCehRoutes.FocusedNodeChanged -= new FocusedNodeChangedEventHandler(this.tlCehRoutes_FocusedNodeChanged);
    this.tlCehRoutes.BeginUpdate();
    try
    {
      this.tlCehRoutes.Nodes.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (CehRouteClass cehRoutes in (CustomTechClassList<CehRouteClass>) this._cehRoutesList)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(cehRoutes.ObjectId);
          if (dbObject != null)
            TechcardClientTreeListUtils.AddObjectToTreeList(this.tlCehRoutes, (IDBAttributable) dbObject, (object) null).Tag = (object) cehRoutes;
        }
      }
    }
    finally
    {
      this.tlCehRoutes.EndUpdate();
      this.tlCehRoutes.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.tlCehRoutes_FocusedNodeChanged);
    }
    this.tlCehRoutes_FocusedNodeChanged((object) this.tlCehRoutes, (FocusedNodeChangedEventArgs) null);
  }

  /// <summary>Получение текущего РМ</summary>
  /// <returns></returns>
  private CehRouteClass CehRoutes_GetSelected()
  {
    return this.tlCehRoutes.FocusedNode != null ? (CehRouteClass) this.tlCehRoutes.FocusedNode.Tag : (CehRouteClass) null;
  }

  /// <summary>Поиск / создание узла для ШР</summary>
  /// <param name="templRoute"></param>
  /// <returns></returns>
  private TreeListNode GetTemplateNode(TemplRouteClass templRoute)
  {
    foreach (TreeListNode node in this.tlElemRoutes.Nodes)
    {
      if (node.Tag == templRoute)
        return node;
    }
    TreeListNode treeList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(templRoute.ObjectId);
      if (dbObject == null)
        return (TreeListNode) null;
      treeList = TechcardClientTreeListUtils.AddObjectToTreeList(this.tlElemRoutes, (IDBAttributable) dbObject, (object) null);
      treeList.Tag = (object) templRoute;
    }
    return treeList;
  }

  /// <summary>Заполнение списка РЭ</summary>
  /// <param name="cehRoutesClass"></param>
  private void ElemRoutes_Fill(CehRouteClass cehRoutesClass)
  {
    this.tlElemRoutes.BeginUpdate();
    try
    {
      this.tlElemRoutes.Nodes.Clear();
      if (cehRoutesClass == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) cehRoutesClass.RouteElementList)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(routeElement.ObjectId);
          if (dbObject != null)
            TechcardClientTreeListUtils.AddObjectToTreeList(this.tlElemRoutes, (IDBAttributable) dbObject, (object) null, (TreeListNode) null).Tag = (object) routeElement;
        }
        foreach (TemplRouteClass template in cehRoutesClass.TemplateList)
        {
          foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) template.RouteElementList)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(routeElement.ObjectId);
            if (dbObject != null)
              TechcardClientTreeListUtils.AddObjectToTreeList(this.tlElemRoutes, (IDBAttributable) dbObject, (object) null, this.GetTemplateNode(template)).Tag = (object) routeElement;
          }
        }
      }
    }
    finally
    {
      this.tlElemRoutes.EndUpdate();
    }
  }

  /// <summary>Получение текущего РЭ</summary>
  /// <returns></returns>
  private CehRouteElementClass ElemRoutes_GetSelected()
  {
    return this.tlElemRoutes.FocusedNode != null && this.tlElemRoutes.FocusedNode.Tag is CehRouteElementClass ? (CehRouteElementClass) this.tlElemRoutes.FocusedNode.Tag : (CehRouteElementClass) null;
  }

  /// <summary>Заполнение списка ТП</summary>
  /// <param name="cehRoutesClass"></param>
  private void TechProcsLink_Fill(CehRouteClass cehRoutesClass)
  {
    this.tvTpLink.BeginUpdate();
    try
    {
      this.tvTpLink.Nodes.Clear();
      if (cehRoutesClass == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (TechProcClass techProc in (CustomTechClassList<TechProcClass>) this._techProcList)
        {
          if (techProc.RefObjID == cehRoutesClass.ObjectId)
          {
            IDBObject dbObject1 = sessionKeeper.Session.GetObject(techProc.ObjectId);
            if (dbObject1 != null)
            {
              TreeNode node1 = this.TechProcsLink_Node_GetParent(techProc).Nodes.Add(dbObject1.Caption);
              node1.Tag = (object) techProc;
              this.TechProcsLink_Node_SetTreeState(node1);
              foreach (CehTechClass cehTech in (CustomTechClassList<CehTechClass>) techProc.CehTechList)
              {
                IDBObject dbObject2 = sessionKeeper.Session.GetObject(cehTech.ObjectId);
                if (dbObject2 != null)
                {
                  TreeNode node2 = node1.Nodes.Add(dbObject2.Caption);
                  node2.Tag = (object) cehTech;
                  this.TechProcsLink_Node_SetTreeState(node2);
                  foreach (OperTechClass operTech in (CustomTechClassList<OperTechClass>) cehTech.OperTechList)
                  {
                    IDBObject dbObject3 = sessionKeeper.Session.GetObject(operTech.ObjectId);
                    if (dbObject3 != null)
                    {
                      TreeNode node3 = node2.Nodes.Add(dbObject3.Caption);
                      node3.Tag = (object) operTech;
                      this.TechProcsLink_Node_SetTreeState(node3);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    finally
    {
      this.tvTpLink.EndUpdate();
    }
  }

  /// <summary>Обновление статусов элементов</summary>
  /// <param name="nodes"></param>
  private void TechProcsLink_Update(TreeNodeCollection nodes)
  {
    this.tvTpLink.BeginUpdate();
    try
    {
      foreach (TreeNode node in nodes)
      {
        this.TechProcsLink_Node_SetTreeState(node);
        this.TechProcsLink_Update(node.Nodes);
      }
    }
    finally
    {
      this.tvTpLink.EndUpdate();
    }
  }

  /// <summary>Получение записи для ТП</summary>
  /// <param name="techProcClass"></param>
  /// <returns></returns>
  private TreeNode TechProcsLink_Node_GetParent(TechProcClass techProcClass)
  {
    if (this.tvTpLink.Nodes.Count == 0)
      this.tvTpLink.Nodes.Add(LocalizationHolder.rm.GetString("TechCard.Client_153")).Tag = (object) null;
    if (techProcClass.TpRouteType == Tp2RouteBaseType.Main)
      return this.tvTpLink.Nodes[0];
    if (this.tvTpLink.Nodes.Count < 2)
      this.tvTpLink.Nodes.Add(LocalizationHolder.rm.GetString("TechCard.Client_154")).Tag = (object) null;
    return this.tvTpLink.Nodes[1];
  }

  /// <summary>Заполение статуса узла дерева</summary>
  /// <param name="node"></param>
  private void TechProcsLink_Node_SetTreeState(TreeNode node)
  {
    if (node == null)
      return;
    object tag = node.Tag;
    if (tag == null || !(tag is CehTechClass))
    {
      node.StateImageIndex = -1;
    }
    else
    {
      CehTechClass cehTechClass = (CehTechClass) tag;
      CehRouteElementClass selected = this.ElemRoutes_GetSelected();
      if (selected == null || cehTechClass.AttrLinkGuid != Guid.Empty && cehTechClass.AttrLinkGuid != selected.LinkGuid)
        node.StateImageIndex = 2;
      else if (cehTechClass.AttrLinkGuid != Guid.Empty)
        node.StateImageIndex = 1;
      else
        node.StateImageIndex = cehTechClass.CehAttrID != selected.CehAttrID || selected.CehAttrID <= 0L ? 3 : 0;
    }
  }

  /// <summary>Заполение списка не привязанных / свободных ТП</summary>
  /// <param name="cehRoutesClass"></param>
  private void TechProcsUnlink_Fill(CehRouteClass cehRoutesClass)
  {
    this.tlTpAll.BeginUpdate();
    try
    {
      this.tlTpAll.Nodes.Clear();
      if (cehRoutesClass == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (TechProcClass techProc in (CustomTechClassList<TechProcClass>) this._techProcList)
        {
          if (techProc.RefObjID != cehRoutesClass.ObjectId)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(techProc.ObjectId);
            if (dbObject != null)
              TechcardClientTreeListUtils.AddObjectToTreeList(this.tlTpAll, (IDBAttributable) dbObject, (object) null).Tag = (object) techProc;
          }
        }
      }
    }
    finally
    {
      this.tlTpAll.EndUpdate();
    }
  }

  /// <summary>Конструктор</summary>
  public CehRoute2TpObjView()
  {
    this.InitializeComponent();
    this._cehRoutesList = new CehRoutesClassList((CustomTechClass) null);
    this._techProcList = new TechProcClassList((CustomTechClass) null);
    this.InitData();
  }

  /// <summary>Ид. версии маршрута обработки</summary>
  public long MoObjId
  {
    get => this._moObjId;
    set => this._moObjId = value;
  }

  /// <summary>Загрузка данных</summary>
  public void LoadData()
  {
    this._cehRoutesList.Clear();
    this._techProcList.Clear();
    if (this._moObjId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID);
      List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.TechProcBaseID);
      List<int> intList = new List<int>((IEnumerable<int>) childrenIdRecursive1);
      intList.AddRange((IEnumerable<int>) childrenIdRecursive2);
      conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.NONE, 0, false));
      long moObjId = this._moObjId;
      IUserSession session = sessionKeeper.Session;
      int[] relations = new int[2]
      {
        TechCardConsts.RelTypes.TechRelationID,
        TechCardConsts.RelTypes.TechRouteRelationID
      };
      ConditionStructure[] array = conditionStructureList.ToArray();
      foreach (TechCardUtils.SostavSortedTreeItem sostavSortedTreeItem in TechCardUtils.GetChildSostavTree(moObjId, session, (IEnumerable<int>) relations, false, array))
      {
        if (sostavSortedTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavSortedTreeItem.PartType, TechCardConsts.ObjectTypes.CehRouteID))
        {
          CehRouteClass cehRouteClass = new CehRouteClass(sostavSortedTreeItem.PartID);
          cehRouteClass.LoadData(sessionKeeper.Session);
          this._cehRoutesList.Add(cehRouteClass);
        }
        else if (sostavSortedTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavSortedTreeItem.PartType, TechCardConsts.ObjectTypes.TechProcBaseID))
        {
          TechProcClass techProcClass = new TechProcClass(sostavSortedTreeItem.PartID);
          techProcClass.LoadData(sessionKeeper.Session);
          this._techProcList.Add(techProcClass);
        }
      }
      int num = 0;
      foreach (TechProcClass techProc in (CustomTechClassList<TechProcClass>) this._techProcList)
      {
        if (techProc.TpRouteType == Tp2RouteBaseType.Main && techProc.RefLinkID != 0L)
        {
          ++num;
          if (num > 1)
            techProc.TpRouteType = Tp2RouteBaseType.Variant;
        }
      }
      this.CehRoutes_Fill();
    }
  }

  /// <summary>Сохранение данных</summary>
  public void SaveData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (CustomTechClass techProc in (CustomTechClassList<TechProcClass>) this._techProcList)
        techProc.SaveData(sessionKeeper.Session);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlCehRoutes_TreeListMenuItemClick(object sender, TreeListMenuItemClickEventArgs e)
  {
    if (e.MenuType != TreeListMenuType.Column || e.MenuItem.Tag == null || !e.MenuItem.Tag.Equals((object) TreeListStringId.MenuColumnColumnCustomization))
      return;
    if (TechCardClientTreeListCustomizationFrom.ShowModal(this._cehRouteTypeId, this.tlCehRoutes, this.grbCehRoutes.Text))
      this.CehRoutes_Fill();
    e.Handled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlCehRoutes_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    CehRouteClass selected = this.CehRoutes_GetSelected();
    this.ElemRoutes_Fill(selected);
    this.TechProcsUnlink_Fill(selected);
    this.TechProcsLink_Fill(selected);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlElemRoutes_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.TechProcsLink_Update(this.tvTpLink.Nodes);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlElemRoutes_TreeListMenuItemClick(object sender, TreeListMenuItemClickEventArgs e)
  {
    if (e.MenuType != TreeListMenuType.Column || e.MenuItem.Tag == null || !e.MenuItem.Tag.Equals((object) TreeListStringId.MenuColumnColumnCustomization))
      return;
    if (TechCardClientTreeListCustomizationFrom.ShowModal(this._elemRouteTypeId, this.tlElemRoutes, this.grbRouteElems.Text))
      this.ElemRoutes_Fill(this.CehRoutes_GetSelected());
    e.Handled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlTpAll_TreeListMenuItemClick(object sender, TreeListMenuItemClickEventArgs e)
  {
    if (e.MenuType != TreeListMenuType.Column || e.MenuItem.Tag == null || !e.MenuItem.Tag.Equals((object) TreeListStringId.MenuColumnColumnCustomization))
      return;
    if (TechCardClientTreeListCustomizationFrom.ShowModal(this._techProcTypeId, this.tlTpAll, this.grbTpFullList.Text))
      this.TechProcsUnlink_Fill(this.CehRoutes_GetSelected());
    e.Handled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tvTpLink_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if (!(sender is TreeView treeView))
      return;
    CehRouteElementClass selected = this.ElemRoutes_GetSelected();
    TreeViewHitTestInfo treeViewHitTestInfo = treeView.HitTest(e.X, e.Y);
    if (selected != null && treeViewHitTestInfo.Location != TreeViewHitTestLocations.StateImage)
      return;
    TreeNode node = e.Node;
    if (node.StateImageIndex != 0 && node.StateImageIndex != 1)
      return;
    ((CehTechClass) node.Tag).AttrLinkGuid = node.StateImageIndex != 0 || selected == null ? Guid.Empty : selected.LinkGuid;
    node.StateImageIndex = (node.StateImageIndex + 1) % 2;
  }

  private void btnApply_Click(object sender, EventArgs e) => this.SaveData();

  private void cmTPAll_Opening(object sender, CancelEventArgs e)
  {
    this.tsmiTPAllTPLink.Enabled = this.tlTpAll.FocusedNode != null && this.CehRoutes_GetSelected() != null;
  }

  private void cmTPLink_Opening(object sender, CancelEventArgs e)
  {
    object tag = this.tvTpLink.SelectedNode == null ? (object) null : this.tvTpLink.SelectedNode.Tag;
    this.tsmiTPLinkDelete.Enabled = this.CehRoutes_GetSelected() != null && tag is TechProcClass;
    this.tsmiTPLinkMain.Enabled = this.CehRoutes_GetSelected() != null && tag is TechProcClass && (tag as TechProcClass).TpRouteType == Tp2RouteBaseType.Variant;
  }

  private void tsmiTPAllTPLink_Click(object sender, EventArgs e)
  {
    TreeListNode focusedNode = this.tlTpAll.FocusedNode;
    if (focusedNode == null || this.CehRoutes_GetSelected() == null || !(focusedNode.Tag is TechProcClass tag))
      return;
    tag.RefObjID = this.CehRoutes_GetSelected().ObjectId;
    tag.TpRouteType = this.tvTpLink.Nodes.Count <= 0 || this.tvTpLink.Nodes[0].Nodes.Count <= 0 ? Tp2RouteBaseType.Main : Tp2RouteBaseType.Variant;
    CehRouteClass selected = this.CehRoutes_GetSelected();
    this.TechProcsUnlink_Fill(selected);
    this.TechProcsLink_Fill(selected);
  }

  private void tsmiTPLinkDelete_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.tvTpLink.SelectedNode;
    if (selectedNode == null || !(selectedNode.Tag is TechProcClass tag))
      return;
    tag.RefObjID = 0L;
    CehRouteClass selected = this.CehRoutes_GetSelected();
    this.TechProcsUnlink_Fill(selected);
    this.TechProcsLink_Fill(selected);
  }

  private void tsmiTPLinkMain_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.tvTpLink.SelectedNode;
    if (selectedNode == null || this.CehRoutes_GetSelected() == null || !(selectedNode.Tag is TechProcClass tag))
      return;
    foreach (TechProcClass techProc in (CustomTechClassList<TechProcClass>) this._techProcList)
      techProc.TpRouteType = Tp2RouteBaseType.Variant;
    tag.TpRouteType = Tp2RouteBaseType.Main;
    CehRouteClass selected = this.CehRoutes_GetSelected();
    this.TechProcsUnlink_Fill(selected);
    this.TechProcsLink_Fill(selected);
  }

  private void pnlBottom_Paint(object sender, PaintEventArgs e)
  {
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CehRoute2TpObjView));
    this.splitContainer1 = new SplitContainer();
    this.grbCehRoutes = new GroupBox();
    this.tlCehRoutes = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeListColumn2 = new TreeListColumn();
    this.splitContainer2 = new SplitContainer();
    this.grbRouteElems = new GroupBox();
    this.tlElemRoutes = new TreeList();
    this.treeListColumn3 = new TreeListColumn();
    this.treeListColumn4 = new TreeListColumn();
    this.splitContainer3 = new SplitContainer();
    this.grbTpFullList = new GroupBox();
    this.tlTpAll = new TreeList();
    this.treeListColumn5 = new TreeListColumn();
    this.treeListColumn6 = new TreeListColumn();
    this.cmTPAll = new ContextMenuStrip(this.components);
    this.tsmiTPAllTPLink = new ToolStripMenuItem();
    this.grbTpLinkList = new GroupBox();
    this.tvTpLink = new TreeView();
    this.cmTPLink = new ContextMenuStrip(this.components);
    this.tsmiTPLinkDelete = new ToolStripMenuItem();
    this.tsmiTPLinkMain = new ToolStripMenuItem();
    this.imageList = new ImageList(this.components);
    this.pnlBottom = new Panel();
    this.pnlButtons = new Panel();
    this.btnApply = new Button();
    this.btnCancel = new Button();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.grbCehRoutes.SuspendLayout();
    this.tlCehRoutes.BeginInit();
    this.splitContainer2.Panel1.SuspendLayout();
    this.splitContainer2.Panel2.SuspendLayout();
    this.splitContainer2.SuspendLayout();
    this.grbRouteElems.SuspendLayout();
    this.tlElemRoutes.BeginInit();
    this.splitContainer3.Panel1.SuspendLayout();
    this.splitContainer3.Panel2.SuspendLayout();
    this.splitContainer3.SuspendLayout();
    this.grbTpFullList.SuspendLayout();
    this.tlTpAll.BeginInit();
    this.cmTPAll.SuspendLayout();
    this.grbTpLinkList.SuspendLayout();
    this.cmTPLink.SuspendLayout();
    this.pnlBottom.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.grbCehRoutes);
    this.splitContainer1.Panel2.Controls.Add((Control) this.splitContainer2);
    this.grbCehRoutes.Controls.Add((Control) this.tlCehRoutes);
    componentResourceManager.ApplyResources((object) this.grbCehRoutes, "grbCehRoutes");
    this.grbCehRoutes.Name = "grbCehRoutes";
    this.grbCehRoutes.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tlCehRoutes, "tlCehRoutes");
    this.tlCehRoutes.Columns.AddRange(new TreeListColumn[2]
    {
      this.treeListColumn1,
      this.treeListColumn2
    });
    this.tlCehRoutes.Name = "tlCehRoutes";
    this.tlCehRoutes.TreeListMenuItemClick += new TreeListMenuItemClickEventHandler(this.tlCehRoutes_TreeListMenuItemClick);
    this.tlCehRoutes.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.tlCehRoutes_FocusedNodeChanged);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this.treeListColumn2, "treeListColumn2");
    this.treeListColumn2.Name = "treeListColumn2";
    componentResourceManager.ApplyResources((object) this.splitContainer2, "splitContainer2");
    this.splitContainer2.Name = "splitContainer2";
    this.splitContainer2.Panel1.Controls.Add((Control) this.grbRouteElems);
    this.splitContainer2.Panel2.Controls.Add((Control) this.splitContainer3);
    this.grbRouteElems.Controls.Add((Control) this.tlElemRoutes);
    componentResourceManager.ApplyResources((object) this.grbRouteElems, "grbRouteElems");
    this.grbRouteElems.Name = "grbRouteElems";
    this.grbRouteElems.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tlElemRoutes, "tlElemRoutes");
    this.tlElemRoutes.Columns.AddRange(new TreeListColumn[2]
    {
      this.treeListColumn3,
      this.treeListColumn4
    });
    this.tlElemRoutes.Name = "tlElemRoutes";
    this.tlElemRoutes.TreeListMenuItemClick += new TreeListMenuItemClickEventHandler(this.tlElemRoutes_TreeListMenuItemClick);
    this.tlElemRoutes.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.tlElemRoutes_FocusedNodeChanged);
    componentResourceManager.ApplyResources((object) this.treeListColumn3, "treeListColumn3");
    this.treeListColumn3.Name = "treeListColumn3";
    componentResourceManager.ApplyResources((object) this.treeListColumn4, "treeListColumn4");
    this.treeListColumn4.Name = "treeListColumn4";
    componentResourceManager.ApplyResources((object) this.splitContainer3, "splitContainer3");
    this.splitContainer3.Name = "splitContainer3";
    this.splitContainer3.Panel1.CausesValidation = false;
    this.splitContainer3.Panel1.Controls.Add((Control) this.grbTpFullList);
    this.splitContainer3.Panel2.Controls.Add((Control) this.grbTpLinkList);
    this.grbTpFullList.Controls.Add((Control) this.tlTpAll);
    componentResourceManager.ApplyResources((object) this.grbTpFullList, "grbTpFullList");
    this.grbTpFullList.Name = "grbTpFullList";
    this.grbTpFullList.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tlTpAll, "tlTpAll");
    this.tlTpAll.Columns.AddRange(new TreeListColumn[2]
    {
      this.treeListColumn5,
      this.treeListColumn6
    });
    this.tlTpAll.ContextMenuStrip = this.cmTPAll;
    this.tlTpAll.Name = "tlTpAll";
    this.tlTpAll.TreeListMenuItemClick += new TreeListMenuItemClickEventHandler(this.tlTpAll_TreeListMenuItemClick);
    componentResourceManager.ApplyResources((object) this.treeListColumn5, "treeListColumn5");
    this.treeListColumn5.Name = "treeListColumn5";
    componentResourceManager.ApplyResources((object) this.treeListColumn6, "treeListColumn6");
    this.treeListColumn6.Name = "treeListColumn6";
    this.cmTPAll.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.tsmiTPAllTPLink
    });
    this.cmTPAll.Name = "cmTPAll";
    componentResourceManager.ApplyResources((object) this.cmTPAll, "cmTPAll");
    this.cmTPAll.Opening += new CancelEventHandler(this.cmTPAll_Opening);
    this.tsmiTPAllTPLink.Name = "tsmiTPAllTPLink";
    componentResourceManager.ApplyResources((object) this.tsmiTPAllTPLink, "tsmiTPAllTPLink");
    this.tsmiTPAllTPLink.Click += new EventHandler(this.tsmiTPAllTPLink_Click);
    this.grbTpLinkList.Controls.Add((Control) this.tvTpLink);
    componentResourceManager.ApplyResources((object) this.grbTpLinkList, "grbTpLinkList");
    this.grbTpLinkList.Name = "grbTpLinkList";
    this.grbTpLinkList.TabStop = false;
    this.tvTpLink.ContextMenuStrip = this.cmTPLink;
    componentResourceManager.ApplyResources((object) this.tvTpLink, "tvTpLink");
    this.tvTpLink.FullRowSelect = true;
    this.tvTpLink.HideSelection = false;
    this.tvTpLink.HotTracking = true;
    this.tvTpLink.Name = "tvTpLink";
    this.tvTpLink.StateImageList = this.imageList;
    this.tvTpLink.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.tvTpLink_NodeMouseClick);
    this.cmTPLink.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiTPLinkDelete,
      (ToolStripItem) this.tsmiTPLinkMain
    });
    this.cmTPLink.Name = "cmTPLink";
    componentResourceManager.ApplyResources((object) this.cmTPLink, "cmTPLink");
    this.cmTPLink.Opening += new CancelEventHandler(this.cmTPLink_Opening);
    this.tsmiTPLinkDelete.Name = "tsmiTPLinkDelete";
    componentResourceManager.ApplyResources((object) this.tsmiTPLinkDelete, "tsmiTPLinkDelete");
    this.tsmiTPLinkDelete.Click += new EventHandler(this.tsmiTPLinkDelete_Click);
    this.tsmiTPLinkMain.Name = "tsmiTPLinkMain";
    componentResourceManager.ApplyResources((object) this.tsmiTPLinkMain, "tsmiTPLinkMain");
    this.tsmiTPLinkMain.Click += new EventHandler(this.tsmiTPLinkMain_Click);
    this.imageList.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.imageList, "imageList");
    this.imageList.TransparentColor = Color.Transparent;
    this.pnlBottom.Controls.Add((Control) this.pnlButtons);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    this.pnlBottom.Paint += new PaintEventHandler(this.pnlBottom_Paint);
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    this.btnApply.UseVisualStyleBackColor = true;
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (CehRoute2TpObjView);
    this.Tag = (object) "";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.ResumeLayout(false);
    this.grbCehRoutes.ResumeLayout(false);
    this.tlCehRoutes.EndInit();
    this.splitContainer2.Panel1.ResumeLayout(false);
    this.splitContainer2.Panel2.ResumeLayout(false);
    this.splitContainer2.ResumeLayout(false);
    this.grbRouteElems.ResumeLayout(false);
    this.tlElemRoutes.EndInit();
    this.splitContainer3.Panel1.ResumeLayout(false);
    this.splitContainer3.Panel2.ResumeLayout(false);
    this.splitContainer3.ResumeLayout(false);
    this.grbTpFullList.ResumeLayout(false);
    this.tlTpAll.EndInit();
    this.cmTPAll.ResumeLayout(false);
    this.grbTpLinkList.ResumeLayout(false);
    this.cmTPLink.ResumeLayout(false);
    this.pnlBottom.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
