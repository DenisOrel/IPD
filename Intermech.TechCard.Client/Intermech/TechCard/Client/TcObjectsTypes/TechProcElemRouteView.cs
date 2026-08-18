// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcElemRouteView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>Контрол для выбора РЭ расцеховки для ТП</summary>
public class TechProcElemRouteView : UserControl
{
  /// <summary>Техпроцесс</summary>
  private readonly TechProcClass _techProcess;
  /// <summary>Список расцеховочных маршрутов</summary>
  private readonly CehRoutesClassList _cehRoutesList;
  /// <summary>Ид. версии объекта техпроцесса</summary>
  private long _techProcessId;
  /// <summary>Category guid for root descriptor</summary>
  private Guid _rootCategoryGuid = Guid.Empty;
  /// <summary>Category id for root descriptor</summary>
  private int _rootCategoryId;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal Panel pnlBottom;
  internal Panel pnlButtons;
  internal Button btnApply;
  internal Button btnCancel;
  private SplitContainer splitContainer1;
  private GroupBox grbCehRoutes;
  private ImageList imageList;
  private GroupBox grbElemRoutes;
  internal TechCardNavObjListControl tcnolcCehRoutes;
  /// <summary>
  /// 
  /// </summary>
  protected internal TechCardNavTreeViewControl tolcElemRouteList;

  /// <summary>Инициализация данных класса</summary>
  private void InitializeData()
  {
  }

  /// <summary>Инициализация контролов класса</summary>
  private void InitializeCustomControls()
  {
    this.RegisterCategory();
    this.InitializeServices();
    this.tolcElemRouteList.DisableColumnsSorting = true;
    this.tolcElemRouteList.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    IDescriptor descriptor = (IDescriptor) new TechObjectListDescriptor(this._rootCategoryId, TechCardConsts.ObjectTypes.ElemRouteID, string.Empty, (IList) null);
    NodeColumnCollection columns = Intermech.Navigator.Utils.VersionColumns(NodeColumnSortOrder.Ascending, false);
    ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    this.tolcElemRouteList.SetColumns(columns, descriptor);
  }

  /// <summary>Инициализация сервисов</summary>
  protected virtual void InitializeServices()
  {
    this.tolcElemRouteList.Services = (System.IServiceProvider) new ServiceContainer();
  }

  /// <summary>Де-инициализация сервисов</summary>
  protected virtual void UnInitializeServices()
  {
    if (this.tolcElemRouteList == null)
      return;
    this.tolcElemRouteList.Services = (System.IServiceProvider) null;
  }

  /// <summary>Регистрация категории</summary>
  protected virtual void RegisterCategory()
  {
    this._rootCategoryGuid = Guid.NewGuid();
    IGuidMapper service = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    this._rootCategoryId = service.Register(this._rootCategoryGuid);
  }

  /// <summary>Раз-регистрация категории</summary>
  protected virtual void UnRegisterCategory()
  {
    ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false)?.Unregister(this._rootCategoryId);
  }

  /// <summary>Заполнение списка РМ</summary>
  private void CehRoutes_Fill()
  {
    List<long> list = this._cehRoutesList.Where<CehRouteClass>((System.Func<CehRouteClass, bool>) (item => item != null)).Select<CehRouteClass, long>((System.Func<CehRouteClass, long>) (item => item.ObjectId)).ToList<long>();
    this.tcnolcCehRoutes.Grid.BeginUpdate();
    try
    {
      this.tcnolcCehRoutes.LoadData(list, TechCardConsts.ObjectTypes.CehRouteID, TechObjectListMode.UniqueValue);
      this.tcnolcCehRoutes.Activate((IView) null);
    }
    finally
    {
      if (this.tcnolcCehRoutes.Grid.Rows.Count > 0)
      {
        iGRow row = this.tcnolcCehRoutes.Grid.Rows[0];
        this.tcnolcCehRoutes.GridSelectRowCells(row, true);
        this.tcnolcCehRoutes.Grid.CurRow = row;
      }
      this.tcnolcCehRoutes.Grid.EndUpdate();
    }
    this.tcnolcCehRoutes_ItemsChanged((object) this, EventArgs.Empty);
  }

  /// <summary>Получение тек.</summary>
  /// <returns></returns>
  private CehRouteClass CehRoutes_GetSelected()
  {
    if (this.tcnolcCehRoutes.SelectedItems == null || this.tcnolcCehRoutes.SelectedItems.Count == 0)
      return (CehRouteClass) null;
    IDBTypedObjectID typedObjId = this.tcnolcCehRoutes.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    return typedObjId == null ? (CehRouteClass) null : this._cehRoutesList.FirstOrDefault<CehRouteClass>((System.Func<CehRouteClass, bool>) (item => item != null && item.ObjectId == typedObjId.ObjectID));
  }

  /// <summary>Заполнение списка РЭ текущего маршрута</summary>
  /// <param name="cehRoutesClass"></param>
  private void ElemRoutes_Fill(CehRouteClass cehRoutesClass)
  {
    string caption = this.Text = LocalizationHolder.rm.GetString("TechCard.Client_541");
    DescriptorCollection descriptors = new DescriptorCollection();
    if (cehRoutesClass != null)
    {
      foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) cehRoutesClass.RouteElementList)
      {
        RelObjInfoItem relationInfoItem = new RelObjInfoItem(routeElement.LinkID, TechCardConsts.RelTypes.TechRouteRelationID)
        {
          PartInfo = new ObjInfoItem(routeElement.ObjectId)
        };
        descriptors.Add((IDescriptor) new RelObjInfoDescriptor(relationInfoItem));
      }
      foreach (CustomTechClass template in cehRoutesClass.TemplateList)
      {
        IDescriptor descriptor = (IDescriptor) new TechCompositionDescriptor(this._rootCategoryId, 0, template.ObjectId, TechCardConsts.ObjectTypes.ElemRouteID, TechCardConsts.RelTypes.TechRelationID, caption, RelatedObjectsRole.Composition, (ITechCompositionFilter) null);
        descriptors.Add(descriptor);
      }
    }
    this.tolcElemRouteList.Build((IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ElemRouteID, caption, descriptors));
    if (this.tolcElemRouteList.RootNode?.Children == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this.tolcElemRouteList.RootNode.Children)
      child.Expanded = true;
  }

  /// <summary>Поиск РЭ по ид. его связи</summary>
  /// <param name="linkId">Ид. связи РЭ</param>
  private CehRouteElementClass ElemRoutes_GetByLinkID(long linkId)
  {
    if (linkId == 0L)
      return (CehRouteElementClass) null;
    CehRouteClass selected = this.CehRoutes_GetSelected();
    if (selected == null)
      return (CehRouteElementClass) null;
    CehRouteElementClass byLinkId1 = selected.RouteElementList.FirstOrDefault<CehRouteElementClass>((System.Func<CehRouteElementClass, bool>) (item => item != null && item.LinkID == linkId));
    if (byLinkId1 != null)
      return byLinkId1;
    foreach (CehRouteTemplateClass template in selected.TemplateList)
    {
      if (template is TemplRouteClass templRouteClass)
      {
        CehRouteElementClass byLinkId2 = templRouteClass.RouteElementList.FirstOrDefault<CehRouteElementClass>((System.Func<CehRouteElementClass, bool>) (item => item != null && item.LinkID == linkId));
        if (byLinkId2 != null)
          return byLinkId2;
      }
    }
    return (CehRouteElementClass) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="elemRouteClass"></param>
  /// <returns></returns>
  private CheckState ElemRoutes_GetState(CehRouteElementClass elemRouteClass)
  {
    return elemRouteClass == null || elemRouteClass.CehAttrID == 0L || this._techProcess.CehTechList.GetIndexByAttrLink(elemRouteClass.LinkGuid) != -1 ? CheckState.Indeterminate : CheckState.Unchecked;
  }

  /// <summary>Обновление кнопок контрола</summary>
  private void UpdateButtons()
  {
    CehRouteElementList routeElemList = (CehRouteElementList) null;
    this.GetCehRouteElems(ref routeElemList);
    this.btnApply.Enabled = routeElemList != null && routeElemList.Count > 0;
  }

  /// <summary>Конструктор</summary>
  public TechProcElemRouteView()
  {
    this.InitializeComponent();
    this._techProcess = new TechProcClass(0L);
    this._cehRoutesList = new CehRoutesClassList((CustomTechClass) null);
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
    this.InitializeData();
  }

  /// <summary>Загрузка информации по техпроцессу</summary>
  public void LoadData()
  {
    this._cehRoutesList.Clear();
    if (this._techProcessId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._techProcess.ObjectId = this._techProcessId;
      this._techProcess.LoadData(sessionKeeper.Session);
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID).ToArray(), LogicalOperators.NONE, 0, false)
      };
      DataTable parentSostavData = DataHelper.GetParentSostavData(new ObjInfoItem(this._techProcessId), sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRouteRelationID
      }, false, (IEnumerable<ConditionStructure>) conditions);
      if (parentSostavData != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) parentSostavData.Rows)
        {
          long int64Value = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L);
          if (int64Value != 0L)
          {
            CehRouteClass cehRouteClass = new CehRouteClass(int64Value);
            cehRouteClass.LoadData(sessionKeeper.Session);
            this._cehRoutesList.Add(cehRouteClass);
          }
        }
      }
    }
    this.CehRoutes_Fill();
  }

  /// <summary>Получение выбранных РЭ</summary>
  public void GetCehRouteElems(ref CehRouteElementList routeElemList)
  {
    if (routeElemList == null)
      routeElemList = new CehRouteElementList((CustomTechClass) null);
    if (this.tolcElemRouteList.CheckedNodes == null)
      return;
    foreach (NavigatorTreeNode checkedNode in this.tolcElemRouteList.CheckedNodes)
    {
      NavigatorTreeNode treeNode;
      IDBRelationID dbRelationId;
      if ((treeNode = checkedNode) != null && treeNode.CheckState == CheckState.Checked && TechcardClientControlsUtils.GetRelationInfo(treeNode, out dbRelationId) && dbRelationId != null)
      {
        CehRouteElementClass byLinkId = this.ElemRoutes_GetByLinkID(dbRelationId.Value);
        if (byLinkId != null)
          routeElemList.Add(byLinkId);
      }
    }
  }

  /// <summary>Ид. версии техпроцесса</summary>
  public long TechProcId
  {
    [DebuggerStepThrough] get => this._techProcessId;
    set => this._techProcessId = value;
  }

  /// <summary>Данные по техпроцессу</summary>
  public TechProcClass TechProcObj
  {
    [DebuggerStepThrough] get => this._techProcess;
  }

  /// <summary>Текущий маршрут расцеховки</summary>
  public CehRouteClass CehRoutesObj
  {
    [DebuggerStepThrough] get => this.CehRoutes_GetSelected();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tcnolcCehRoutes_ItemsChanged(object sender, EventArgs e)
  {
    this.ElemRoutes_Fill(this.CehRoutes_GetSelected());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tolcElemRouteList_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (e == null)
      return;
    this.UpdateButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tolcElemRouteList_CheckStateChanging(object sender, CheckStateEventArgs e)
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
  private void tolcElemRouteList_AfterCreateNode(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e?.Node;
    NavigatorTreeView navigatorTreeView = sender as NavigatorTreeView;
    if (node == null || navigatorTreeView == null || !(node is TechcardNavTreeNode techcardNavTreeNode))
      return;
    INode nodeHandler = navigatorTreeView.GetNodeHandler(node);
    if (nodeHandler == null)
    {
      techcardNavTreeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
    }
    else
    {
      IDBTypedObjectID data1 = techcardNavTreeNode.NodeID is NodeID nodeId ? nodeHandler.GetData((INodeID) nodeId, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
      if (data1 == null)
        techcardNavTreeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
      else if (!MetaDataHelper.IsObjectTypeChildOf(data1.ObjectType, TechCardConsts.ObjectTypes.ElemRouteID))
        techcardNavTreeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
      else if (!(nodeHandler.GetData((INodeID) nodeId, typeof (IDBRelationID)) is IDBRelationID data2))
      {
        techcardNavTreeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
      }
      else
      {
        CehRouteElementClass byLinkId = this.ElemRoutes_GetByLinkID(data2.Value);
        node.CheckState = this.ElemRoutes_GetState(byLinkId);
      }
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.UnInitializeServices();
      this.UnRegisterCategory();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechProcElemRouteView));
    this.splitContainer1 = new SplitContainer();
    this.grbCehRoutes = new GroupBox();
    this.tcnolcCehRoutes = new TechCardNavObjListControl();
    this.grbElemRoutes = new GroupBox();
    this.tolcElemRouteList = new TechCardNavTreeViewControl();
    this.pnlBottom = new Panel();
    this.pnlButtons = new Panel();
    this.btnApply = new Button();
    this.btnCancel = new Button();
    this.imageList = new ImageList(this.components);
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.grbCehRoutes.SuspendLayout();
    this.grbElemRoutes.SuspendLayout();
    this.tolcElemRouteList.BeginInit();
    this.pnlBottom.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.grbCehRoutes);
    this.splitContainer1.Panel2.Controls.Add((Control) this.grbElemRoutes);
    this.grbCehRoutes.Controls.Add((Control) this.tcnolcCehRoutes);
    componentResourceManager.ApplyResources((object) this.grbCehRoutes, "grbCehRoutes");
    this.grbCehRoutes.Name = "grbCehRoutes";
    this.grbCehRoutes.TabStop = false;
    this.tcnolcCehRoutes.AllowCustomGroupValues = true;
    this.tcnolcCehRoutes.Control = (object) this.tcnolcCehRoutes;
    this.tcnolcCehRoutes.CustomContextMenuStrip = (ContextMenuStrip) null;
    this.tcnolcCehRoutes.DisableColumnsGrouping = true;
    this.tcnolcCehRoutes.DisableGroupBox = true;
    this.tcnolcCehRoutes.DisableIMContextMenu = true;
    this.tcnolcCehRoutes.DisableKeyDownEvents = false;
    this.tcnolcCehRoutes.DisableStatusBar = true;
    this.tcnolcCehRoutes.DisableToolBar = true;
    componentResourceManager.ApplyResources((object) this.tcnolcCehRoutes, "tcnolcCehRoutes");
    this.tcnolcCehRoutes.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.tcnolcCehRoutes.Name = "tcnolcCehRoutes";
    this.tcnolcCehRoutes.ViewContentType = ContentType.NonFolders;
    this.tcnolcCehRoutes.ItemsChanged += new EventHandler(this.tcnolcCehRoutes_ItemsChanged);
    this.grbElemRoutes.Controls.Add((Control) this.tolcElemRouteList);
    componentResourceManager.ApplyResources((object) this.grbElemRoutes, "grbElemRoutes");
    this.grbElemRoutes.Name = "grbElemRoutes";
    this.grbElemRoutes.TabStop = false;
    this.tolcElemRouteList.AllowDrop = true;
    this.tolcElemRouteList.AllowMultiSelect = false;
    this.tolcElemRouteList.AllowUserPinnedColumns = false;
    this.tolcElemRouteList.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.tolcElemRouteList.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("tolcElemRouteList.CheckedNodesStates");
    this.tolcElemRouteList.CheckoutMode = TechCheckoutMode.Manual;
    this.tolcElemRouteList.CheckRootNode = false;
    this.tolcElemRouteList.DisableCheckedOutColumn = true;
    this.tolcElemRouteList.DisableIMContextMenu = true;
    this.tolcElemRouteList.DisableKeyDownEvents = true;
    this.tolcElemRouteList.DisableKeyUpEvents = true;
    this.tolcElemRouteList.DisablePacketsReading = false;
    componentResourceManager.ApplyResources((object) this.tolcElemRouteList, "tolcElemRouteList");
    this.tolcElemRouteList.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("tolcElemRouteList.HeaderStyle.HorzAlignment");
    this.tolcElemRouteList.LineStyle = LineStyle.Dot;
    this.tolcElemRouteList.Name = "tolcElemRouteList";
    this.tolcElemRouteList.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcElemRouteList.RowEvenStyle.WordWrap");
    this.tolcElemRouteList.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcElemRouteList.RowOddStyle.WordWrap");
    this.tolcElemRouteList.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcElemRouteList.RowSelectedStyle.WordWrap");
    this.tolcElemRouteList.RowStyle.BorderColor = SystemColors.Control;
    this.tolcElemRouteList.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.tolcElemRouteList.RowStyle.BorderWidth = 1;
    this.tolcElemRouteList.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("tolcElemRouteList.RowStyle.WordWrap");
    this.tolcElemRouteList.SelectBeforeEdit = true;
    this.tolcElemRouteList.ShowRootRow = false;
    this.tolcElemRouteList.SuppressErrorMessages = true;
    this.tolcElemRouteList.Tag = (object) " ";
    this.tolcElemRouteList.AfterCreateNode += new EventHandler<NodeEventArgs>(this.tolcElemRouteList_AfterCreateNode);
    this.tolcElemRouteList.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.tolcElemRouteList_CheckStateChanging);
    this.tolcElemRouteList.CheckStateChanged += new EventHandler<NodeEventArgs>(this.tolcElemRouteList_CheckStateChanged);
    this.pnlBottom.Controls.Add((Control) this.pnlButtons);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    this.pnlButtons.Controls.Add((Control) this.btnApply);
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    this.btnApply.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.imageList.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.imageList, "imageList");
    this.imageList.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (TechProcElemRouteView);
    this.Tag = (object) "";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.grbCehRoutes.ResumeLayout(false);
    this.grbElemRoutes.ResumeLayout(false);
    this.tolcElemRouteList.EndInit();
    this.pnlBottom.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
