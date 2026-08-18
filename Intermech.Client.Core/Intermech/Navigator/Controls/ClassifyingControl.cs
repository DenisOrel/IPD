
// Type: Intermech.Navigator.Controls.ClassifyingControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Thumbnail;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Classifiers;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Реализует элемент управления, позволяющий классифицировать объекты базы данных.
/// </summary>
public class ClassifyingControl : UserControl, ISupportInitialize, IIODestination, ISelectedItemsHost
{
  private static readonly Guid RootCategoryGuid = new Guid("73037988-86CF-47c3-B31E-AE4BA0C299D7");
  private static int RootCategoryID = -1;
  /// <summary>Диспетчер событий</summary>
  private IIODispatcher _ioDispatcher;
  private long[] rootClassifiers;
  private long[] startRootClassifiers;
  private AdvancedServiceContainer services;
  private bool initialized;
  private static Tuple<long[], long[]> _cacheLastSelected;
  private readonly bool _multiselect;
  private List<NavigatorTreeNode> _fakeChangeNodes = new List<NavigatorTreeNode>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer scHost;
  private NavigatorTreeView treeView;
  private PageViewsManager viewsManager;
  private TreeViewsBridge treeViewsBridge;
  private MenuBar contextMenu;
  private ContextMenuBarItem contextMenuClassifs;
  private MenuButtonItem mnpRefresh;
  private GroupBox groupBox1;
  private RichTextBox tbNote;
  private Splitter splitter1;
  protected ToolStrip _ts;
  protected ToolStripTextBox tsTxtSearch;
  protected ToolStripButton tsBtnSearch;
  protected ToolStripLabel _tsFltLabel;
  private ToolStripButton tsClean;

  public event ClassifierSelectedEventHandler ClassifierSelected;

  public event EventHandler SelectedItemsChanged;

  public ClassifyingControl()
  {
    this.InitializeComponent();
    this._multiselect = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.MultiSelectClassifierParamName, false, DBConfigMode.GlobalOnly);
    this.treeView.CheckBoxStyle = this._multiselect ? NavigatorTreeViewCheckBoxStyle.TwoState : NavigatorTreeViewCheckBoxStyle.None;
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
      this.mnpRefresh.Image = service.ImageList.Images[service.ImageIndex("imgRefresh")];
    if (this.DesignMode)
      return;
    this.services = new AdvancedServiceContainer();
    this._ioDispatcher = (IIODispatcher) new IODispatcher();
    this._ioDispatcher.RegisterDestination((IIODestination) this);
    this.services.AddService(typeof (IIODispatcher), (object) this._ioDispatcher);
    this.services.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog));
    this.treeView.Services = (System.IServiceProvider) this.services;
    this.viewsManager.Services = (System.IServiceProvider) this.services;
    this.viewsManager.ViewsUpdated += new EventHandler(this.ViewsManager_ViewsUpdated);
  }

  public bool SelectClassifier(IUserSession session, long classifierID)
  {
    return this.SelectClassifier(session, new long[1]
    {
      classifierID
    });
  }

  /// <summary>Выделить классификаторы в окне</summary>
  public bool SelectClassifier(IUserSession session, long[] classifierIDs)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.SortedRelationTypeID);
    foreach (long classifierId in classifierIDs)
    {
      List<long> parentIds = this.GetParentIDs(relationCollection, MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")), classifierId);
      if (parentIds == null)
      {
        this.ExpandNode(this.treeView.Nodes[0].Children, classifierId, true);
      }
      else
      {
        parentIds.Insert(0, classifierId);
        NavigatorTreeNodes currentLevel = this.treeView.Nodes[0].Children;
        for (int index = parentIds.Count - 1; index >= 0; --index)
        {
          currentLevel = this.ExpandNode(currentLevel, parentIds[index], index == 0);
          if (currentLevel == null)
            break;
        }
      }
    }
    return true;
  }

  private NavigatorTreeNodes ExpandNode(
    NavigatorTreeNodes currentLevel,
    long objectID,
    bool select)
  {
    if (currentLevel == null)
      return (NavigatorTreeNodes) null;
    foreach (NavigatorTreeNode navigatorTreeNode in (List<NavigatorTreeNode>) currentLevel)
    {
      if (((NodeID) navigatorTreeNode.NodeID).ObjectID == objectID)
      {
        if (!select)
        {
          navigatorTreeNode.Expand();
        }
        else
        {
          if (this._multiselect)
            navigatorTreeNode.CheckState = CheckState.Checked;
          navigatorTreeNode.FocusThenExpand();
        }
        if (navigatorTreeNode.HasChildren)
          return navigatorTreeNode.Children;
      }
    }
    return (NavigatorTreeNodes) null;
  }

  private List<long> GetParentIDs(
    IDBRelationCollection rellColl,
    List<int> localChildObjectTypes,
    long childObjectID)
  {
    List<long> parentIds1 = new List<long>();
    rellColl.ChildObjectTypes = (IList<int>) localChildObjectTypes;
    DataTable dataTable = rellColl.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    }), childObjectID);
    if (dataTable.Rows.Count == 0)
      return (List<long>) null;
    long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
    parentIds1.Add(int64);
    List<long> parentIds2 = this.GetParentIDs(rellColl, localChildObjectTypes, int64);
    if (parentIds2 != null)
      parentIds1.AddRange((IEnumerable<long>) parentIds2);
    return parentIds1;
  }

  [DefaultValue(null)]
  public long[] RootClassifiers
  {
    get => this.rootClassifiers;
    set
    {
      if (this.rootClassifiers == value)
        return;
      if (this.rootClassifiers == null)
        this.startRootClassifiers = value;
      this.rootClassifiers = value;
      if (this.rootClassifiers == null)
        return;
      this.UpdateTreeView();
      if (ClassifyingControl._cacheLastSelected == null || ClassifyingControl._cacheLastSelected.Item1.Length != this.rootClassifiers.Length || ((IEnumerable<long>) this.rootClassifiers).Except<long>((IEnumerable<long>) ClassifyingControl._cacheLastSelected.Item1).ToArray<long>().Length != 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.SelectClassifier(sessionKeeper.Session, ClassifyingControl._cacheLastSelected.Item2);
    }
  }

  void ISupportInitialize.BeginInit()
  {
  }

  void ISupportInitialize.EndInit()
  {
    this.initialized = true;
    this.UpdateTreeView();
  }

  [Browsable(false)]
  public ISelectedItems SelectedItems { get; private set; }

  private void UpdateTreeView()
  {
    if (!this.initialized || this.DesignMode)
      return;
    this.treeViewsBridge.UseDelay = false;
    try
    {
      this.treeView.SetColumns((NodeColumnCollection) null);
      this.treeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
      this.treeView.Build((IDescriptor) new ListDescriptor(ClassifyingControl.RootCategoryID, 0, LocalizationHolder.rm.GetString("Client.Core_1104"), (IList) this.rootClassifiers));
      if (this._multiselect)
      {
        this.treeView.Nodes[0].ShowCheckState = false;
        this.treeView.CheckStateChanged += new EventHandler<NodeEventArgs>(this.TreeView_CheckStateChanged);
        this.treeView.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.TreeView_CheckStateChanging);
      }
      else
        this.treeView.SelectedItemsChanged += new EventHandler(this.TreeView_SelectedItemsChanged);
      this.treeView.Enter += new EventHandler(this.TreeView_Enter);
    }
    finally
    {
      this.treeViewsBridge.UseDelay = true;
    }
  }

  private void TreeView_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (this._multiselect)
    {
      if (this._fakeChangeNodes.Contains(e.Node))
      {
        this._fakeChangeNodes.Remove(e.Node);
        return;
      }
      if (e.Node.CheckState == CheckState.Indeterminate)
        return;
    }
    this.TreeView_SelectedItemsChanged(sender, (EventArgs) e);
  }

  private void TreeView_CheckStateChanging(object sender, CheckStateEventArgs e)
  {
    if (!this._multiselect || e.OldValue == CheckState.Checked || e.NewValue == CheckState.Checked)
      return;
    this._fakeChangeNodes.Add(e.Node);
  }

  private void TreeView_SelectedItemsChanged(object sender, EventArgs e)
  {
    this.SelectedItems = this._multiselect ? this.treeView.CheckedItems : this.treeView.SelectedItems;
    this.RaiseSelectedItemsChanged();
  }

  private void RaiseSelectedItemsChanged()
  {
    this.tbNote.Clear();
    IDBObjectID selectionID = (IDBObjectID) null;
    bool enableClassify = true;
    if (this.SelectedItems != null)
    {
      List<string> classifyAttributes = new List<string>();
      List<long> longList = new List<long>();
      string note = string.Empty;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        for (int index = 0; index < this.SelectedItems.Count; ++index)
        {
          if (this.SelectedItems.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData)
          {
            selectionID = itemData;
            IDBObject dbClassif = sessionKeeper.Session.GetObject(selectionID.Value);
            if (this.SelectedItems.Count == 1)
              note = this.GetNotes(dbClassif);
            this.CollectClassifyAttributes(dbClassif, classifyAttributes);
            longList.Add(dbClassif.ObjectID);
            if (enableClassify)
            {
              NodeIDPath parentPath = this.SelectedItems.GetParentPath(index);
              if (parentPath != null)
              {
                if (parentPath.Length == 1)
                {
                  IDBAttribute attributeByGuid = dbClassif.GetAttributeByGuid(new Guid("cad0156e-306c-11d8-b4e9-00304f19f545"));
                  enableClassify = attributeByGuid == null || !attributeByGuid.AsBoolean;
                }
                else if (parentPath.Length > 1 && parentPath[1] is NodeID nodeId)
                {
                  IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(nodeId.ObjectID).GetAttributeByGuid(new Guid("cad0156e-306c-11d8-b4e9-00304f19f545"));
                  if (attributeByGuid != null && attributeByGuid.AsBoolean)
                  {
                    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad0014d-306c-11d8-b4e9-00304f19f545");
                    IDBAttribute attributeById = dbClassif.GetAttributeByID(attributeTypeId);
                    if (attributeById != null && !string.IsNullOrEmpty(attributeById.AsString))
                      enableClassify = sessionKeeper.Session.ObjectsSelect(new Guid("cad00150-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams(new ConditionStructure[2]
                      {
                        new ConditionStructure(attributeTypeId, RelationalOperators.StartString, (object) attributeById.AsString, LogicalOperators.AND, 0, true),
                        new ConditionStructure(-2, RelationalOperators.NotEqual, (object) itemData.Value, LogicalOperators.AND, 0, false)
                      }, new object[1]{ (object) -2 })).Rows.Count == 0;
                  }
                }
              }
              ClassifyingControl._cacheLastSelected = new Tuple<long[], long[]>(this.rootClassifiers, longList.ToArray());
            }
          }
        }
        this.WriteTbNote(note, classifyAttributes);
        ClassifierSelectedEventHandler classifierSelected = this.ClassifierSelected;
        if (classifierSelected != null)
          classifierSelected((object) this, new ClassifierSelectedEventArgs(selectionID, enableClassify));
      }
    }
    EventHandler selectedItemsChanged = this.SelectedItemsChanged;
    if (selectedItemsChanged == null)
      return;
    selectedItemsChanged((object) this, new EventArgs());
  }

  private string GetNotes(IDBObject dbClassif)
  {
    string notes = LocalizationHolder.rm.GetString("Client.Core_1427");
    IDBAttribute attributeByGuid = dbClassif.GetAttributeByGuid(new Guid("cad00021-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid != null && attributeByGuid.AsString != string.Empty)
      notes = $"{notes} {attributeByGuid.AsString}";
    return notes;
  }

  private void CollectClassifyAttributes(IDBObject dbClassif, List<string> classifyAttributes)
  {
    IDBAttribute attributeByGuid = dbClassif.GetAttributeByGuid(new Guid("cad001d7-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || attributeByGuid.ValuesCount <= 0)
      return;
    for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
    {
      attributeByGuid.Index = index;
      if (attributeByGuid.AsString != string.Empty)
      {
        FormulaRecord attributeAndFormula = CalcFormulaRules.GetAttributeAndFormula(attributeByGuid.AsString);
        if (attributeAndFormula.AttributeGuid != string.Empty && !classifyAttributes.Contains(attributeAndFormula.AttributeGuid))
          classifyAttributes.Add(attributeAndFormula.AttributeGuid);
      }
    }
  }

  private void WriteTbNote(string note, List<string> classifyAttributes)
  {
    List<string> stringList = new List<string>();
    if (!string.IsNullOrEmpty(note))
      stringList.Add(note);
    string str = LocalizationHolder.rm.GetString("Client.Core_1428");
    foreach (string classifyAttribute in classifyAttributes)
    {
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(new Guid(classifyAttribute));
      if (!string.IsNullOrEmpty(attributeTypeName))
        str = $"{str} {attributeTypeName},";
    }
    if (str[str.Length - 1] == ',')
      str = str.Remove(str.Length - 1, 1);
    stringList.Add(str);
    this.tbNote.Lines = stringList.ToArray();
  }

  private void ViewsManager_ViewsUpdated(object sender, EventArgs e)
  {
    if (this.scHost.Panel2Collapsed)
      return;
    for (int index = 0; index < this.viewsManager.ViewPages.Count; ++index)
    {
      if (this.viewsManager.ViewPages[index].Control is ThumbnailView)
      {
        (this.viewsManager.ViewPages[index].Control as ThumbnailView).SelectedItemsChanged += new EventHandler(this.ClassifyingControl_SelectedItemsChanged);
        this.ClassifyingControl_SelectedItemsChanged((object) this, EventArgs.Empty);
        break;
      }
    }
  }

  private void ClassifyingControl_SelectedItemsChanged(object sender, EventArgs e)
  {
    for (int index = 0; index < this.viewsManager.ViewPages.Count; ++index)
    {
      if (this.viewsManager.ViewPages[index].Control is ThumbnailView)
      {
        ThumbnailView control = this.viewsManager.ViewPages[index].Control as ThumbnailView;
        Control activeControl = control.ActiveControl;
        if (activeControl != null && activeControl.Focused)
        {
          this.SelectedItems = control.SelectedItems;
          this.RaiseSelectedItemsChanged();
          break;
        }
      }
    }
  }

  internal static void Start()
  {
    ClassifyingControl.RootCategoryID = ((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper))).Register(ClassifyingControl.RootCategoryGuid);
    IFactory service1 = (IFactory) ServicesManager.GetService(typeof (IFactory));
    service1.AddNodeType(ClassifyingControl.RootCategoryID, typeof (ObjectsListNode));
    ClassifyingControl.ViewsProvider provider = new ClassifyingControl.ViewsProvider();
    service1.AddViewsProvider(ClassifyingControl.RootCategoryID, (IViewsProvider) provider);
    service1.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID("cad0014e-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) provider);
    service1.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) provider);
    service1.AddViewsProvider(1, MetaDataHelper.GetObjectTypeID("cad00150-306c-11d8-b4e9-00304f19f545"), (IViewsProvider) provider);
    ICategoryTypeIconService service2 = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    service2.AddIcon(service2.GetIcon(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00157-306c-11d8-b4e9-00304f19f545"))), ClassifyingControl.RootCategoryID);
  }

  internal static void Stop()
  {
  }

  public IOEventTypes SupportedEvents
  {
    get => IOEventTypes.evKeyUp | IOEventTypes.evMouseDoubleClick;
    set
    {
    }
  }

  public bool ProcessEvent(IIOEvent Event)
  {
    if (Event == null || this.viewsManager.ActiveViewPage == null || Event.Source.Control != this.viewsManager.ActiveViewPage.Control)
      return false;
    if (Event.EventType == IOEventType.evMouseDoubleClick || Event.EventType == IOEventType.evKeyUp && ((KeyEventArgs) Event.EventData).KeyCode == Keys.Return)
    {
      if (Event.Source.SelectedItems != null && Event.Source.SelectedItems.Count > 0)
      {
        INodeID itemId = Event.Source.SelectedItems.GetItemID(0);
        if (Event.Source.SelectedItems.Count == 1 && (Event.Source is IFoldersView || itemId == null || this.treeView.RootDescriptor.GetRecordNodeID() == null || itemId.CategoryID != this.treeView.RootDescriptor.GetRecordNodeID().CategoryID) && this.BrowseToPath(Event))
          return true;
        Event.EventFlags |= IOEventFlags.efProcessed;
      }
      return false;
    }
    if ((Event.EventType != IOEventType.evKeyUp || ((KeyEventArgs) Event.EventData).KeyCode != Keys.Back) && ((KeyEventArgs) Event.EventData).KeyCode != Keys.BrowserBack)
      return false;
    this.BrowseToPrevPath(Event);
    return false;
  }

  /// <summary>
  /// Переместиться в дереве (в зависимости от исходных данных в событии)
  /// </summary>
  /// <param name="Event">Событие</param>
  private bool BrowseToPath(IIOEvent Event)
  {
    if (!(Event.Tag is NodeIDPath tag))
      return false;
    this.treeView.Focus();
    this.treeView.TryBrowse(tag);
    return true;
  }

  /// <summary>
  /// Переместиться в дереве на предыдущий уровень (в зависимости от исходных данных в событии)
  /// </summary>
  /// <param name="Event">Событие</param>
  private bool BrowseToPrevPath(IIOEvent Event)
  {
    if (!(Event.Tag is NodeIDPath tag) || tag.Length <= 1)
      return false;
    NodeIDPath nodeIDPath = new NodeIDPath(tag);
    nodeIDPath.RemoveLast();
    if (!this.treeView.TryBrowse(nodeIDPath))
      return false;
    this.viewsManager.Focus();
    if (this.viewsManager.ActiveViewPage != null)
      this.viewsManager.ActiveViewPage.Control.Focus();
    return true;
  }

  private void ViewsManager_Enter(object sender, EventArgs e)
  {
    if (this.scHost.Panel2Collapsed)
      return;
    for (int index = 0; index < this.viewsManager.ViewPages.Count; ++index)
    {
      if (this.viewsManager.ViewPages[index].Control is ThumbnailView && this.viewsManager.ViewPages[index].Control is ThumbnailView control && control.SelectedItems != null)
      {
        this.SelectedItems = control.SelectedItems;
        this.RaiseSelectedItemsChanged();
        break;
      }
    }
  }

  private void TreeView_Enter(object sender, EventArgs e)
  {
    this.SelectedItems = this.treeView.SelectedItems;
    this.RaiseSelectedItemsChanged();
  }

  private void MnpRefresh_Click(object sender, EventArgs e) => this.UpdateTreeView();

  private void TreeView_ShowContextMenu(object sender, MouseEventArgs e)
  {
    this.contextMenuClassifs.Show((Control) this.treeView, e.Location);
  }

  private void TreeView_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    bool flag = true;
    if (e != null && e.Node != null)
    {
      if (e.Node.NodeID is NodeID)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(((NodeID) e.Node.NodeID).ObjectID, false);
          if (dbObject != null)
          {
            IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cadd99b5-306c-11d8-b4e9-00304f19f545"), false);
            if (attributeByGuid != null)
            {
              if (Convert.ToBoolean(attributeByGuid.Value))
                flag = false;
            }
          }
        }
      }
      else if (e.Node.NodeID is ListDescriptorNodeID)
        flag = false;
    }
    this.scHost.Panel2Collapsed = flag;
  }

  private void Search_Click(object sender, EventArgs e)
  {
    if (string.IsNullOrEmpty(this.tsTxtSearch.Text))
      return;
    NavigatorTreeNode node = this.treeView.Nodes[0];
    if (node == null)
      return;
    List<long> longList = new List<long>();
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
    {
      if (child.NodeID is NodeID nodeId && !string.IsNullOrEmpty(nodeId.Caption) && nodeId.Caption.StartsWith(this.tsTxtSearch.Text, StringComparison.CurrentCultureIgnoreCase))
        longList.Add(((NodeID) child.NodeID).ObjectID);
    }
    this.RootClassifiers = longList.ToArray();
    this.tsTxtSearch.Enabled = false;
    this.tsBtnSearch.Enabled = false;
    this.tsClean.Enabled = true;
  }

  private void tsTxtSearch_KeyUp(object sender, KeyEventArgs e)
  {
    if (!this.tsTxtSearch.Enabled || e.KeyCode != Keys.Return)
      return;
    this.Search_Click((object) this, new EventArgs());
  }

  private void tsClean_Click(object sender, EventArgs e)
  {
    this.RootClassifiers = this.startRootClassifiers;
    this.tsTxtSearch.Enabled = true;
    this.tsTxtSearch.Text = string.Empty;
    this.tsBtnSearch.Enabled = true;
    this.tsClean.Enabled = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ClassifyingControl));
    this.scHost = new SplitContainer();
    this._ts = new ToolStrip();
    this._tsFltLabel = new ToolStripLabel();
    this.tsTxtSearch = new ToolStripTextBox();
    this.tsBtnSearch = new ToolStripButton();
    this.tsClean = new ToolStripButton();
    this.treeView = new NavigatorTreeView();
    this.splitter1 = new Splitter();
    this.viewsManager = new PageViewsManager();
    this.groupBox1 = new GroupBox();
    this.tbNote = new RichTextBox();
    this.contextMenu = new MenuBar();
    this.contextMenuClassifs = new ContextMenuBarItem();
    this.mnpRefresh = new MenuButtonItem();
    this.treeViewsBridge = new TreeViewsBridge(this.components);
    this.scHost.BeginInit();
    this.scHost.Panel1.SuspendLayout();
    this.scHost.Panel2.SuspendLayout();
    this.scHost.SuspendLayout();
    this._ts.SuspendLayout();
    this.treeView.BeginInit();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.scHost, "scHost");
    this.scHost.Name = "scHost";
    this.scHost.Panel1.Controls.Add((Control) this._ts);
    this.scHost.Panel1.Controls.Add((Control) this.treeView);
    this.scHost.Panel2.Controls.Add((Control) this.splitter1);
    this.scHost.Panel2.Controls.Add((Control) this.viewsManager);
    this.scHost.Panel2.Controls.Add((Control) this.groupBox1);
    this.scHost.Panel2.Controls.Add((Control) this.contextMenu);
    this._ts.GripStyle = ToolStripGripStyle.Hidden;
    this._ts.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._tsFltLabel,
      (ToolStripItem) this.tsTxtSearch,
      (ToolStripItem) this.tsBtnSearch,
      (ToolStripItem) this.tsClean
    });
    this._ts.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
    componentResourceManager.ApplyResources((object) this._ts, "_ts");
    this._ts.Name = "_ts";
    this._tsFltLabel.Name = "_tsFltLabel";
    componentResourceManager.ApplyResources((object) this._tsFltLabel, "_tsFltLabel");
    this.tsTxtSearch.BorderStyle = BorderStyle.FixedSingle;
    this.tsTxtSearch.Margin = new Padding(0);
    this.tsTxtSearch.Name = "tsTxtSearch";
    componentResourceManager.ApplyResources((object) this.tsTxtSearch, "tsTxtSearch");
    this.tsTxtSearch.KeyUp += new KeyEventHandler(this.tsTxtSearch_KeyUp);
    this.tsBtnSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsBtnSearch, "tsBtnSearch");
    this.tsBtnSearch.Name = "tsBtnSearch";
    this.tsBtnSearch.Click += new EventHandler(this.Search_Click);
    this.tsClean.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this.tsClean, "tsClean");
    this.tsClean.Image = (Image) Intermech.Client.Core.Properties.Resources.Clean;
    this.tsClean.Name = "tsClean";
    this.tsClean.Click += new EventHandler(this.tsClean_Click);
    this.treeView.AllowDrop = true;
    this.treeView.AllowMultiSelect = false;
    this.treeView.AllowUserPinnedColumns = false;
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.DisableCheckedOutColumn = true;
    this.treeView.DisableKeyDownEvents = true;
    this.treeView.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("treeView.HeaderStyle.HorzAlignment");
    this.treeView.ImageList = (ImageList) null;
    this.treeView.LineStyle = LineStyle.Dot;
    this.treeView.Name = "treeView";
    this.treeView.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("treeView.RowEvenStyle.WordWrap");
    this.treeView.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("treeView.RowOddStyle.WordWrap");
    this.treeView.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("treeView.RowSelectedStyle.WordWrap");
    this.treeView.RowStyle.BorderColor = SystemColors.Control;
    this.treeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.treeView.RowStyle.BorderWidth = 1;
    this.treeView.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("treeView.RowStyle.WordWrap");
    this.treeView.SelectBeforeEdit = true;
    this.treeView.ShowRootRow = false;
    this.treeView.SuppressErrorMessages = true;
    this.treeView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.TreeView_AfterFocusNode);
    this.treeView.ShowContextMenu += new MouseEventHandler(this.TreeView_ShowContextMenu);
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.viewsManager.ActiveViewPage = (IViewPage) null;
    this.viewsManager.AllowedViews = new string[2]
    {
      "ClassifyThumbnailView",
      "ThumbnailView"
    };
    this.viewsManager.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this.viewsManager, "viewsManager");
    this.viewsManager.Name = "viewsManager";
    this.viewsManager.Enter += new EventHandler(this.ViewsManager_Enter);
    this.groupBox1.Controls.Add((Control) this.tbNote);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.tbNote.BackColor = SystemColors.Control;
    this.tbNote.BorderStyle = BorderStyle.None;
    componentResourceManager.ApplyResources((object) this.tbNote, "tbNote");
    this.tbNote.Name = "tbNote";
    this.tbNote.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.contextMenu, "contextMenu");
    this.contextMenu.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.contextMenu.Hidden = true;
    this.contextMenu.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuClassifs
    });
    this.contextMenu.Name = "contextMenu";
    this.contextMenu.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.contextMenuClassifs, "contextMenuClassifs");
    this.contextMenuClassifs.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.mnpRefresh
    });
    this.contextMenuClassifs.ShowText = true;
    this.mnpRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpRefresh, "mnpRefresh");
    this.mnpRefresh.ShowText = true;
    this.mnpRefresh.Click += new EventHandler(this.MnpRefresh_Click);
    this.treeViewsBridge.NavTreeView = this.treeView;
    this.treeViewsBridge.ViewsManager = (IViewsManager) this.viewsManager;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.scHost);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (ClassifyingControl);
    this.scHost.Panel1.ResumeLayout(false);
    this.scHost.Panel1.PerformLayout();
    this.scHost.Panel2.ResumeLayout(false);
    this.scHost.EndInit();
    this.scHost.ResumeLayout(false);
    this._ts.ResumeLayout(false);
    this._ts.PerformLayout();
    this.treeView.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Реализует провайдер закладок для корня дерева классификаторов. Предоставляет закладку со списком изображений классификаторов.
  /// </summary>
  private class ViewsProvider : IViewsProvider
  {
    ViewsInfo IViewsProvider.GetViews(ISelectedItems items, System.IServiceProvider services)
    {
      ViewsInfo views = new ViewsInfo();
      IViewState service = services != null ? services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
      bool flag1 = service != null && (service.ViewState & ViewStateFlags.InParametersCard) > ViewStateFlags.None;
      bool flag2 = service != null && (service.ViewState & ViewStateFlags.InDialog) > ViewStateFlags.None;
      if (items.Count == 0)
        return ViewsInfo.Empty;
      INodeID itemId = items.GetItemID(0);
      if (!flag1)
      {
        if (flag2 && itemId.CategoryID == 1)
          views.Add("ClassifyThumbnailView", new ViewInfo(0, typeof (ClassifyThumbnailView)));
        if (itemId.CategoryID == ClassifyingControl.RootCategoryID)
          views.Add("ClassifyThumbnailView", new ViewInfo(0, typeof (ClassifyThumbnailView)));
      }
      return views;
    }
  }
}
