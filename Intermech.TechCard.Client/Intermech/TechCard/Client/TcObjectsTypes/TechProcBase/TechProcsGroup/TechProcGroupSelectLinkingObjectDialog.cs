// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.TechProcGroupSelectLinkingObjectDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.NavigatorSupport.NodeFactories;
using Intermech.TechCard.Client.Resources;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>
/// Диалог выбора объектов для привязки техпроцессов к изделиям
/// </summary>
/// <remarks></remarks>
/// &gt;
public class TechProcGroupSelectLinkingObjectDialog : Form
{
  /// <summary>Информация по изделиям</summary>
  private IEnumerable<ObjInfoItem> _articleObjInfoItems;
  /// <summary>Состав изделий для отображения</summary>
  private readonly IList<RelObjInfoItem> _articleRelInfoItems = (IList<RelObjInfoItem>) new List<RelObjInfoItem>();
  /// <summary>Список "привязанных" объектов</summary>
  private readonly IList<RelObjInfoItem> _linkedRelInfoItems = (IList<RelObjInfoItem>) new List<RelObjInfoItem>();
  /// <summary>
  /// 
  /// </summary>
  private ICommandManager _commandManager;
  /// <summary>
  /// 
  /// </summary>
  private IServiceContainer _services;
  /// <summary>
  /// 
  /// </summary>
  private INotificationService _notificationService;
  /// <summary>Фоновая задача загрузки данных</summary>
  private BackgroundWorker _backgroundWorker;
  /// <summary>Дочерние (привязываемые) типы объектов</summary>
  private IEnumerable<int> _childObjectTypes;
  /// <summary>Флаг загрузки данных</summary>
  private bool _dataLoaded;
  /// <summary>Флаг возможности добавления МО</summary>
  private bool _allowAddProcRouting;
  /// <summary>Techcard treeview</summary>
  private TechCardNavTreeViewControl _treeView;
  private ContextMenuStrip cmsMain;
  private ToolStripMenuItem tsmiSelectAll;
  private ToolStripMenuItem tsmiClearAll;
  private ToolStripMenuItem tsmiInvertAll;
  private ToolStripSeparator tsmiSep1;
  private ToolStripMenuItem tsmiExpandAll;
  private ToolStripMenuItem tsmiCollapseAll;
  private Panel pnlBottom;
  private Button btnCancel;
  private Button btnApply;
  private ToolStripMenuItem tsmiAddProcRouting;
  private ToolStripSeparator tsmiSep0;
  private IContainer components;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechProcGroupSelectLinkingObjectDialog));
    this.cmsMain = new ContextMenuStrip(this.components);
    this.tsmiSelectAll = new ToolStripMenuItem();
    this.tsmiClearAll = new ToolStripMenuItem();
    this.tsmiInvertAll = new ToolStripMenuItem();
    this.tsmiSep1 = new ToolStripSeparator();
    this.tsmiExpandAll = new ToolStripMenuItem();
    this.tsmiCollapseAll = new ToolStripMenuItem();
    this.pnlBottom = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.tsmiAddProcRouting = new ToolStripMenuItem();
    this.tsmiSep0 = new ToolStripSeparator();
    this.cmsMain.SuspendLayout();
    this.pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this.cmsMain.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this.tsmiAddProcRouting,
      (ToolStripItem) this.tsmiSep0,
      (ToolStripItem) this.tsmiSelectAll,
      (ToolStripItem) this.tsmiClearAll,
      (ToolStripItem) this.tsmiInvertAll,
      (ToolStripItem) this.tsmiSep1,
      (ToolStripItem) this.tsmiExpandAll,
      (ToolStripItem) this.tsmiCollapseAll
    });
    this.cmsMain.Name = "cmsMain";
    componentResourceManager.ApplyResources((object) this.cmsMain, "cmsMain");
    this.cmsMain.Opening += new CancelEventHandler(this.cmsMain_Opening);
    this.tsmiSelectAll.Name = "tsmiSelectAll";
    componentResourceManager.ApplyResources((object) this.tsmiSelectAll, "tsmiSelectAll");
    this.tsmiSelectAll.Click += new EventHandler(this.tsmiSelectAll_Click);
    this.tsmiClearAll.Name = "tsmiClearAll";
    componentResourceManager.ApplyResources((object) this.tsmiClearAll, "tsmiClearAll");
    this.tsmiClearAll.Click += new EventHandler(this.tsmiClearAll_Click);
    this.tsmiInvertAll.Name = "tsmiInvertAll";
    componentResourceManager.ApplyResources((object) this.tsmiInvertAll, "tsmiInvertAll");
    this.tsmiInvertAll.Click += new EventHandler(this.tsmiInvertAll_Click);
    this.tsmiSep1.Name = "tsmiSep1";
    componentResourceManager.ApplyResources((object) this.tsmiSep1, "tsmiSep1");
    this.tsmiExpandAll.Name = "tsmiExpandAll";
    componentResourceManager.ApplyResources((object) this.tsmiExpandAll, "tsmiExpandAll");
    this.tsmiExpandAll.Click += new EventHandler(this.tsmiExpandAll_Click);
    this.tsmiCollapseAll.Name = "tsmiCollapseAll";
    componentResourceManager.ApplyResources((object) this.tsmiCollapseAll, "tsmiCollapseAll");
    this.tsmiCollapseAll.Click += new EventHandler(this.tsmiCollapseAll_Click);
    this.pnlBottom.Controls.Add((Control) this.btnCancel);
    this.pnlBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    this.tsmiAddProcRouting.Name = "tsmiAddProcRouting";
    componentResourceManager.ApplyResources((object) this.tsmiAddProcRouting, "tsmiAddProcRouting");
    this.tsmiAddProcRouting.Click += new EventHandler(this.tsmiAddProccessRoute_Click);
    this.tsmiSep0.Name = "tsmiSep0";
    componentResourceManager.ApplyResources((object) this.tsmiSep0, "tsmiSep0");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlBottom);
    this.Name = nameof (TechProcGroupSelectLinkingObjectDialog);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.TechProcGroupSelectLinkingObjectDialog_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.TechProcGroupLinkArt2ObjDialog_FormClosed);
    this.cmsMain.ResumeLayout(false);
    this.pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Initialize services</summary>
  private void InitializeServices()
  {
    this._commandManager = ServiceUtils.GetService<ICommandManager>((object) ApplicationServices.Container, false);
    this._notificationService = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    this._services = (IServiceContainer) new ServiceContainer();
    this._services.AddService(typeof (IViewState), (object) new ViewStateService());
    this._services.AddService(typeof (INodesFactorySupported), (object) new TechCompositionFromRelObjInfoItemFactorySupport((IEnumerable<RelObjInfoItem>) this._articleRelInfoItems));
    if (this._commandManager != null)
      this._services.AddService(typeof (ICommandManager), (object) this._commandManager);
    if (this._notificationService != null)
      this._services.AddService(typeof (INotificationService), (object) this._notificationService);
    this._backgroundWorker = new BackgroundWorker()
    {
      WorkerReportsProgress = true,
      WorkerSupportsCancellation = true
    };
    this._backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
    this._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
  }

  /// <summary>Initialize custom controls</summary>
  private void InitializeCustomComponent()
  {
    this.Text = LocalizationHolder.rm.GetString("TechCard.SelectArticleLinkingObject");
    IMainFormUpdate service = ServiceUtils.GetService<IMainFormUpdate>((object) ApplicationServices.Container, false);
    if (service?.MainForm != null)
      this.Icon = service.MainForm.Icon;
    this._treeView = new TechCardNavTreeViewControl();
    this._treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._treeView.Location = new Point(8, 8);
    this._treeView.MultiSelect = false;
    this._treeView.ContextMenuStrip = this.cmsMain;
    this._treeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this._treeView.Name = "treeView";
    this._treeView.TabIndex = 0;
    this._treeView.DisableColumnsSorting = true;
    this.Controls.Add((Control) this._treeView);
    this._treeView.Dock = DockStyle.Fill;
    this._treeView.BringToFront();
    this._treeView.AfterCreateNode += new EventHandler<NodeEventArgs>(this.Node_AfterCreateEvent);
    this._treeView.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.Node_CheckStateChangingEvent);
    this._treeView.CheckStateChanged += new EventHandler<NodeEventArgs>(this.Node_CheckStateChangedEvent);
    this._treeView.Services = (System.IServiceProvider) this._services;
    this._treeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    string caption = "";
    IDescriptor descriptor = (IDescriptor) new TechCompositionDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.TechBaseObjectID, 0L, TechCardConsts.ObjectTypes.TechBaseObjectID, -1, caption, RelatedObjectsRole.Composition, (ITechCompositionFilter) null);
    this._treeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending), descriptor);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    StatusPopup.Hide((Control) this._treeView);
    if (e.Cancelled || e.Error != null)
      return;
    this._dataLoaded = true;
    this.UpdateTreeViewData();
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
      if (this._articleObjInfoItems == null || !this._articleObjInfoItems.Any<ObjInfoItem>())
        return;
      List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
      List<RelObjInfoItem> filteredRelObjInfoItems = new List<RelObjInfoItem>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ObjInfoHelper.UpdateUnknownTypes(this._articleObjInfoItems, sessionKeeper.Session);
        ProcRouteHelper.GetDefaultProcRouteForArticles((IList<long>) this._articleObjInfoItems.Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (item => item.ObjectID)).ToList<long>(), sessionKeeper.Session);
        if (backgroundWorker.CancellationPending)
        {
          e.Cancel = true;
          return;
        }
        ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
        List<int> intList = new List<int>();
        List<int> collection = new List<int>();
        collection.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.MarshrObrabID));
        collection.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehBaseRouteID));
        foreach (int childObjectType in this._childObjectTypes)
          collection.AddRange(MetaDataHelper.GetObjectTypeParentApplicabilities(childObjectType).Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (item => item.ApplicabilityMode != ApplicabilityModes.Disabled && item.RelationTypeID == TechCardConsts.RelTypes.TechRelationID)).Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (item => item.InObjectType)));
        intList.AddRange((IEnumerable<int>) collection);
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ArticleBaseID));
        collection.AddRange(this._childObjectTypes);
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) collection.ToArray(), LogicalOperators.NONE, 0, false)
        };
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
        columnDescriptorList.AddRange(RelObjInfoDbScheme<ObjInfoItem>.GetSourceTableColumns());
        columnDescriptorList.Add(new ColumnDescriptor((object) TechCardConsts.AttributeTypes.SortAttrTypeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, 0));
        CompositionLoadingParams loadingParams = new CompositionLoadingParams(this._articleObjInfoItems, (IEnumerable<int>) null, (IEnumerable<int>) intList.ToArray(), (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<ConditionStructure>) conditions, true, false, 6, (VersionsRule) null, VersionsRuleSources.GetCurrentWindowRule().OwnerId);
        DataTable table = service.LoadComplexCompositions((object) sessionKeeper.Session, loadingParams);
        if (table == null)
          return;
        if (backgroundWorker.CancellationPending)
        {
          e.Cancel = true;
          return;
        }
        DataTable source = DataHelper.SortCompositionData(table, columnDescriptorList.ToArray(), this._articleObjInfoItems.ToList<ObjInfoItem>());
        new RelObjInfoDbScheme<ObjInfoItem>(true).ParseInfoItems(sessionKeeper.Session, source != null ? (IEnumerable<DataRow>) source.AsEnumerable() : (IEnumerable<DataRow>) null, (ICollection<RelObjInfoItem>) relObjInfoItemList);
      }
      Dictionary<ObjInfoItem, List<RelObjInfoItem>> dictionary = relObjInfoItemList.GroupBy<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (relObjInfoItem => relObjInfoItem.PartInfo)).ToDictionary<IGrouping<ObjInfoItem, RelObjInfoItem>, ObjInfoItem, List<RelObjInfoItem>>((System.Func<IGrouping<ObjInfoItem, RelObjInfoItem>, ObjInfoItem>) (group => group.Key), (System.Func<IGrouping<ObjInfoItem, RelObjInfoItem>, List<RelObjInfoItem>>) (group => group.ToList<RelObjInfoItem>()));
      HashSet<int> linkingObjectTypes = this._childObjectTypes.Select<int, List<IMSApplicability>>((System.Func<int, List<IMSApplicability>>) (item => MetaDataHelper.GetObjectTypeParentApplicabilities(item))).SelectMany<List<IMSApplicability>, IMSApplicability>((System.Func<List<IMSApplicability>, IEnumerable<IMSApplicability>>) (item => (IEnumerable<IMSApplicability>) item)).ToList<IMSApplicability>().Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (item => item.RelationTypeID == TechCardConsts.RelTypes.TechRelationID && item.ApplicabilityMode != ApplicabilityModes.Disabled)).Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (item => item.InObjectType)).ToHashSet<int>();
      linkingObjectTypes.Add(TechCardConsts.ObjectTypes.MarshrObrabID);
      linkingObjectTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) linkingObjectTypes.ToList<int>()).ToHashSet<int>();
      foreach (RelObjInfoItem relObjInfoItem in relObjInfoItemList.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => linkingObjectTypes.Contains(item.PartInfo.ObjTypeID))))
      {
        filteredRelObjInfoItems.Add(relObjInfoItem);
        List<ObjInfoItem> source1 = new List<ObjInfoItem>()
        {
          relObjInfoItem.ProjInfo
        };
        List<ObjInfoItem> source2 = new List<ObjInfoItem>();
        HashSet<ObjInfoItem> allProObjInfoItems = new HashSet<ObjInfoItem>();
        while (source1.Any<ObjInfoItem>())
        {
          source2.Clear();
          foreach (ObjInfoItem key in source1)
          {
            allProObjInfoItems.Add(key);
            List<RelObjInfoItem> source3;
            if (dictionary.TryGetValue(key, out source3))
            {
              filteredRelObjInfoItems.AddRange(source3.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => !filteredRelObjInfoItems.Contains(item))));
              source2.AddRange(source3.Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)));
            }
          }
          source1.Clear();
          source1.AddRange(source2.Where<ObjInfoItem>((System.Func<ObjInfoItem, bool>) (item => !allProObjInfoItems.Contains(item))));
        }
      }
      HashSet<int> childObjectTypes = this._childObjectTypes.ToHashSet<int>();
      this._linkedRelInfoItems.AddRange<RelObjInfoItem>(relObjInfoItemList.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => childObjectTypes.Contains(item.PartInfo.ObjTypeID))));
      this._articleRelInfoItems.AddRange<RelObjInfoItem>((IEnumerable<RelObjInfoItem>) filteredRelObjInfoItems);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadData()
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
    this._articleRelInfoItems.Clear();
    this._backgroundWorker.RunWorkerAsync();
    this.UpdateTreeViewData();
    StatusPopup.Show(ResourceHolder.LoadingImage, LocalizationHolder.rm.GetString("TechCard.Client_481"), (Control) this._treeView);
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateButtons()
  {
    this.btnApply.Enabled = this._treeView.RootNode.GetDescendants().Any<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (item => item.CheckState == CheckState.Checked));
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateTreeViewData()
  {
    string caption = LocalizationHolder.rm.GetString("TechCard.LinkObject2Article");
    DescriptorCollection descriptors = new DescriptorCollection();
    if (this._dataLoaded)
    {
      foreach (ObjInfoItem articleObjInfoItem in this._articleObjInfoItems)
      {
        Intermech.Navigator.DBObjects.Descriptor descriptor1 = new Intermech.Navigator.DBObjects.Descriptor(articleObjInfoItem.ObjectID);
        descriptor1.Services = (System.IServiceProvider) this._services;
        Intermech.Navigator.DBObjects.Descriptor descriptor2 = descriptor1;
        descriptors.Add((IDescriptor) descriptor2);
      }
    }
    IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ArticleBaseID, caption, descriptors);
    NodeIDPath focusedPath = this._treeView.FocusedPath;
    IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper> checkedNodesStates = this._treeView.CheckedNodesStates;
    foreach (NodeColumn column in (List<NodeColumn>) this._treeView.GetColumns())
    {
      if (column.SortOrder != NodeColumnSortOrder.None)
        column.SortOrder = NodeColumnSortOrder.None;
    }
    this._treeView.Build(rootDescriptor);
    if (focusedPath != null && focusedPath.Length > 1)
    {
      NodeIDPath nodeIDPath = new NodeIDPath(rootDescriptor);
      foreach (INodeID NodeID in focusedPath)
        nodeIDPath.Add(NodeID);
      this._treeView.TryBrowse(nodeIDPath);
    }
    else if (this._treeView.RootNode != null)
    {
      if (this._treeView.RootNode.Children == null || this._treeView.RootNode.Children.Count == 0)
      {
        if (descriptors.Count > 0)
          this._treeView.TryBrowse(new NodeIDPath(rootDescriptor)
          {
            rootDescriptor.GetRecordNodeID()
          });
      }
      else
        this._treeView.RootNode.Expand();
    }
    this._treeView.CheckedNodesStates = checkedNodesStates;
    this.UpdateButtons();
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  protected virtual void LoadSettings(bool loadFormPosition)
  {
    if (loadFormPosition)
    {
      Form form = new Form();
      form.Name = this.Name.Equals(string.Empty) ? this.GetType().ToString() : this.Name;
      Intermech.Client.Core.FormStorage.LoadLayout((Control) form);
      this.Location = form.Location;
      this.Size = form.Size;
    }
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this._treeView);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  protected virtual void SaveSettings(bool saveFormPosition)
  {
    if (saveFormPosition)
    {
      Form form = new Form();
      form.Name = this.Name.Equals(string.Empty) ? this.GetType().ToString() : this.Name;
      form.Location = this.Location;
      form.Size = this.Size;
      Intermech.Client.Core.FormStorage.SaveLayout((Control) form);
    }
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name) ?? service.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this._treeView);
  }

  /// <summary>Конструктор</summary>
  public TechProcGroupSelectLinkingObjectDialog()
  {
    this.InitializeComponent();
    this.InitializeServices();
    this.InitializeCustomComponent();
  }

  /// <summary>Вызов диалога</summary>
  /// <param name="articleObjInfoItems">Описание изделий</param>
  /// <param name="childObjectTypes">Типы дочерних / привязываемых объектов</param>
  /// <returns></returns>
  public bool ShowDialog(
    IEnumerable<ObjInfoItem> articleObjInfoItems,
    IEnumerable<int> childObjectTypes)
  {
    this.LoadSettings(true);
    if (articleObjInfoItems == null)
      return false;
    this._articleObjInfoItems = articleObjInfoItems;
    this._childObjectTypes = childObjectTypes;
    this.LoadMetaDataInfo();
    this.LoadData();
    int num = (int) this.ShowDialog();
    this.SaveSettings(true);
    return this.DialogResult == DialogResult.OK;
  }

  /// <summary>
  /// 
  /// </summary>
  private void LoadMetaDataInfo()
  {
    this._allowAddProcRouting = this._childObjectTypes.Any<int>((System.Func<int, bool>) (item =>
    {
      IMSApplicability applicability = MetaDataHelper.GetApplicability(TechCardConsts.ObjectTypes.ProcRoutingID, item, TechCardConsts.RelTypes.TechRelationID);
      return (applicability != null ? (int) applicability.ApplicabilityMode : -1) != -1;
    }));
  }

  /// <summary>Список привязанных объектов</summary>
  public IList<ObjInfoItem> LinkedObjInfoItems { get; set; }

  /// <summary>Информация о выбранных объектов в контексте изделия</summary>
  public IDictionary<ObjInfoItem, ObjInfoItem> SelectedLinkedObjectInfo
  {
    get
    {
      Dictionary<ObjInfoItem, ObjInfoItem> linkedObjectInfo = new Dictionary<ObjInfoItem, ObjInfoItem>();
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes);
      foreach (NavigatorTreeNode treeNode in this._treeView.RootNode.GetDescendants().Where<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (item => item.CheckState == CheckState.Checked)))
      {
        IDBTypedObjectID dbTypedObjId1;
        if (TechcardClientControlsUtils.GetObjectInfo(treeNode, out dbTypedObjId1))
        {
          NavigatorTreeNode parent = treeNode.Parent;
          IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) null;
          while (parent != null)
          {
            IDBTypedObjectID dbTypedObjId2;
            if (!TechcardClientControlsUtils.GetObjectInfo(parent, out dbTypedObjId2))
            {
              parent = parent.Parent;
            }
            else
            {
              if (childrenIdRecursive.Contains(dbTypedObjId2.ObjectType))
              {
                dbTypedObjectId = dbTypedObjId2;
                break;
              }
              parent = parent.Parent;
            }
          }
          linkedObjectInfo[new ObjInfoItem(dbTypedObjId1.ObjectID, dbTypedObjId1.ObjectType)] = dbTypedObjectId != null ? new ObjInfoItem(dbTypedObjectId.ObjectID, dbTypedObjectId.ObjectType) : (ObjInfoItem) null;
        }
      }
      return (IDictionary<ObjInfoItem, ObjInfoItem>) linkedObjectInfo;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Node_AfterCreateEvent(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e?.Node;
    NavigatorTreeView navigatorTreeView = sender as NavigatorTreeView;
    IDBTypedObjectID dbObjectId;
    if (node == null || navigatorTreeView == null || !(node is TechcardNavTreeNode treeNode) || !TechcardClientControlsUtils.GetObjectInfo((NavigatorTreeNode) treeNode, out dbObjectId))
      return;
    if (dbObjectId == null || this._childObjectTypes.All<int>((System.Func<int, bool>) (item =>
    {
      IMSApplicability applicability = MetaDataHelper.GetApplicability(dbObjectId.ObjectType, item, TechCardConsts.RelTypes.TechRelationID);
      return (applicability != null ? (int) applicability.ApplicabilityMode : -1) == -1;
    })))
    {
      treeNode.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
    }
    else
    {
      long linkingProcObjId = dbObjectId.ObjectID;
      if (linkingProcObjId == 0L)
        return;
      IEnumerable<RelObjInfoItem> source = this._linkedRelInfoItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => item.ProjInfo.ObjectID == linkingProcObjId));
      bool flag = this.LinkedObjInfoItems != null ? source.Any<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => this.LinkedObjInfoItems.Contains(item.PartInfo))) : source.Any<RelObjInfoItem>();
      treeNode.SetCheckStateInternal(flag ? CheckState.Indeterminate : CheckState.Unchecked);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Node_CheckStateChangingEvent(object sender, CheckStateEventArgs e)
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
  private void Node_CheckStateChangedEvent(object sender, NodeEventArgs e)
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
  private void cmsMain_Opening(object sender, CancelEventArgs e)
  {
    bool flag = this._treeView?.RootNode != null && this._treeView.RootNode.Children.Count > 0;
    this.tsmiExpandAll.Enabled = this.tsmiCollapseAll.Enabled = flag;
    this.tsmiClearAll.Enabled = this.tsmiSelectAll.Enabled = this.tsmiInvertAll.Enabled = flag;
    this.tsmiSep0.Visible = this.tsmiAddProcRouting.Enabled = this.tsmiAddProcRouting.Visible = false;
    IDBTypedObjectID dbTypedObjectId;
    if (!this._allowAddProcRouting || !TechcardClientControlsUtils.GetObjectInfo(this._treeView?.FocusedNode, out dbTypedObjectId, out IDBRelationID _, false) || !MetaDataHelper.IsObjectTypeChildOf(dbTypedObjectId.ObjectType, TechCardConsts.ObjectTypes.ArticleBaseID))
      return;
    this.tsmiSep0.Visible = this.tsmiAddProcRouting.Enabled = this.tsmiAddProcRouting.Visible = true;
  }

  /// <summary>Добавление маршрута обработки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiAddProccessRoute_Click(object sender, EventArgs e)
  {
    IDBTypedObjectID dbTypedObjectId;
    if (!TechcardClientControlsUtils.GetObjectInfo(this._treeView?.FocusedNode, out dbTypedObjectId, out IDBRelationID _, false))
      return;
    if (Intermech.Consts.IsUndefinedObjectId(ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, true).CreateObjectByTypeDialog(TechCardConsts.ObjectTypes.ProcRoutingID, new ObjectRelationLink[1]
    {
      new ObjectRelationLink(dbTypedObjectId.ObjectID, TechCardConsts.RelTypes.TechRelationID)
    })))
      return;
    this.LoadData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiExpandAll_Click(object sender, EventArgs e)
  {
    if (this._treeView?.RootNode == null || !(this._treeView.RootNode is TechcardNavTreeNode rootNode))
      return;
    rootNode.ExpandNode(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiCollapseAll_Click(object sender, EventArgs e)
  {
    if (this._treeView?.RootNode == null || !(this._treeView.RootNode is TechcardNavTreeNode rootNode))
      return;
    rootNode.CollapseNode(true);
    rootNode.ExpandNode(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiSelectAll_Click(object sender, EventArgs e)
  {
    if (this._treeView?.RootNode == null)
      return;
    try
    {
      foreach (NavigatorTreeNode navigatorTreeNode in this._treeView.RootNode.GetDescendants(true).Where<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (item => item.CheckState == CheckState.Unchecked)))
      {
        if (navigatorTreeNode is TechcardNavTreeNode techcardNavTreeNode && techcardNavTreeNode.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None)
          techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Select, false);
      }
      this._treeView.UpdateRows();
    }
    finally
    {
      this.UpdateButtons();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiClearAll_Click(object sender, EventArgs e)
  {
    if (this._treeView?.RootNode == null)
      return;
    try
    {
      foreach (NavigatorTreeNode navigatorTreeNode in this._treeView.RootNode.GetDescendants(true).Where<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (item => item.CheckState == CheckState.Checked)))
      {
        if (navigatorTreeNode is TechcardNavTreeNode techcardNavTreeNode && techcardNavTreeNode.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None)
          techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Clear, false);
      }
      this._treeView.UpdateRows();
    }
    finally
    {
      this.UpdateButtons();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiInvertAll_Click(object sender, EventArgs e)
  {
    if (this._treeView?.RootNode == null)
      return;
    try
    {
      foreach (NavigatorTreeNode navigatorTreeNode in this._treeView.RootNode.GetDescendants(true).Where<NavigatorTreeNode>((System.Func<NavigatorTreeNode, bool>) (item => item.CheckState != CheckState.Indeterminate)))
      {
        if (navigatorTreeNode is TechcardNavTreeNode techcardNavTreeNode && techcardNavTreeNode.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None)
          techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Invert, false);
      }
      this._treeView.UpdateRows();
    }
    finally
    {
      this.UpdateButtons();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechProcGroupLinkArt2ObjDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this._treeView != null)
      this._treeView.Services = (System.IServiceProvider) null;
    if (this._backgroundWorker != null && this._backgroundWorker.WorkerSupportsCancellation && this._backgroundWorker.IsBusy)
    {
      this._backgroundWorker.CancelAsync();
      this._backgroundWorker.Dispose();
      this._backgroundWorker = (BackgroundWorker) null;
    }
    StatusPopup.Hide((Control) this._treeView);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechProcGroupSelectLinkingObjectDialog_FormClosing(
    object sender,
    FormClosingEventArgs e)
  {
  }
}
