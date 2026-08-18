// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ReplaceAttributesView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Docking;
using Intermech.Imbase.BackgroundTask;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class ReplaceAttributesView : DockControl, IImbaseView
{
  private long _targetId;
  private NavigatorTreeNode _parentINode;
  private TreeNode _parentTreeNode;
  private Icon _ico;
  private CancellationTokenSource _cts;
  private DataTable _dtLinks;
  private ConcurrentDictionary<int, Tuple<IMSAttributeType, ConcurrentDictionary<long, string>>> _attrCache = new ConcurrentDictionary<int, Tuple<IMSAttributeType, ConcurrentDictionary<long, string>>>();
  private IContainer components;
  private System.Windows.Forms.Timer _timer;
  private Panel pnlFirst;
  private TableLayoutPanel tlpFirst;
  private ListView _lvAttrs;
  private ColumnHeader colAttrsName;
  private ListView _lvTables;
  private ColumnHeader colTablesName;
  private Panel _pnlProgress;
  private ProgressBar _progress;
  private Panel _pnlScanInfo;
  private Label _lbCompleted;
  private Label _lbTaskInfo;
  private ContextMenuStrip cmsTable;
  private ToolStripMenuItem tmsiOpenTableInNewWindow;
  private Panel pnlSecond;
  private Panel pnlButton;
  private TableLayoutPanel tlpnlButton;
  private Button btnNext;
  private ListView _replaceAttrs;
  private ColumnHeader attrNameColumn;
  private Button btnReplace;

  public Icon Icon
  {
    get
    {
      return this._ico ?? (this._ico = Intermech.Imbase.ResourceHelper.GetResourceData<Icon>(this.GetType().Assembly, "Intermech.Imbase.Resources.FindInTables.ico"));
    }
  }

  public ReplaceAttributesView()
  {
    this.InitializeComponent();
    this._lvTables.Columns[0].Width = -2;
    this._lvAttrs.Columns[0].Width = -2;
  }

  public static void Show(object parentNode, bool modal)
  {
    ReplaceAttributesView view = new ReplaceAttributesView();
    view.SetData(parentNode);
    if (modal)
    {
      ImbaseViewForm.FindOrCreateViewForm(ImbaseViewForm.FormType.FindInTables, (IImbaseView) view, view.Icon).Show();
    }
    else
    {
      if (!(ServicesManager.GetService(typeof (DockManager)) is DockManager service))
        return;
      view.Manager = service;
      view.Float();
      if (!(view.Parent?.Parent is Form))
        return;
      Control parent = view.Parent.Parent;
      Size minimumSize = view.MinimumSize;
      int width = minimumSize.Width + 20;
      minimumSize = view.MinimumSize;
      int height = minimumSize.Height + 40;
      Size size = new Size(width, height);
      parent.MinimumSize = size;
    }
  }

  public void FirstShown(object sender, EventArgs e) => this.OnBeforeFirstShown(sender, e);

  public void ViewClosing(object sender, CancelEventArgs e) => this._cts?.Cancel();

  private void SetData(object parentNode)
  {
    this._parentINode = parentNode as NavigatorTreeNode;
    if (this._parentINode != null)
    {
      this._targetId = ((NodeID) this._parentINode.NodeID).ObjectID;
    }
    else
    {
      this._parentTreeNode = parentNode as TreeNode;
      if (this._parentTreeNode == null)
        return;
      this._targetId = this._parentTreeNode.Tag is NodeInfo tag ? tag.ObjectId : 0L;
    }
  }

  private DataTable GetTableIDs(IUserSession session, string strKey)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor3 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) strKey, LogicalOperators.AND, 0, false),
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[3]
    {
      columnDescriptor1,
      columnDescriptor2,
      columnDescriptor3
    });
    DataTable tableIds = objectCollection.Select(paramSet);
    if (tableIds != null && tableIds.Rows.Count > 0)
    {
      tableIds.Columns[0].ColumnName = sc_7988.ssp_imbase_7989();
      tableIds.Columns[2].ColumnName = "TableID";
    }
    return tableIds;
  }

  private IMSAttributeType[] GetTableAttributes(long tableId)
  {
    List<IMSAttributeType> imsAttributeTypeList = new List<IMSAttributeType>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, tableId, true);
      if (tables == null || !tables.Tables.Contains("IMS_ATTR_TYPES"))
        return imsAttributeTypeList.ToArray();
      foreach (DataRow row in (InternalDataCollectionBase) tables.Tables["IMS_ATTR_TYPES"].Rows)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(Convert.ToString(row["F_ATTRIBUTE_GUID"])));
        if (!string.IsNullOrEmpty(attributeType.Name))
          imsAttributeTypeList.Add(attributeType);
      }
    }
    return imsAttributeTypeList.ToArray();
  }

  private void FillAllowableAttrsList(IMSAttributeType attributeType)
  {
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, false);
    this._replaceAttrs.Items.Clear();
    this._replaceAttrs.SmallImageList = service?.ImageList;
    int i = service != null ? service.IndexOf(3, -1, (object) attributeType.FieldType) : -1;
    ListViewItem[] array = MetaDataHelper.GetAttributeTypesList().Where<IMSAttributeType>((System.Func<IMSAttributeType, bool>) (x => x.FieldType == attributeType.FieldType && x.AttributeID != attributeType.AttributeID)).Select<IMSAttributeType, ListViewItem>((System.Func<IMSAttributeType, ListViewItem>) (x => new ListViewItem(x.Name)
    {
      Tag = (object) x,
      ImageIndex = i
    })).ToArray<ListViewItem>();
    this._replaceAttrs.BeginUpdate();
    this._replaceAttrs.Items.AddRange(array);
    this._replaceAttrs.EndUpdate();
  }

  private void OnBeforeFirstShown(object sender, EventArgs e)
  {
    this._timer.Tick += new EventHandler(this.On_timer_Tick);
    this._timer.Start();
  }

  private void On_timer_Tick(object sender, EventArgs e)
  {
    this._timer.Stop();
    this._pnlProgress.Visible = true;
    this._attrCache.Clear();
    this._cts = new CancellationTokenSource();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string asString = sessionKeeper.Session.GetObjectActualCopy(this._targetId, false)?.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId)?.AsString;
      if (string.IsNullOrEmpty(asString))
        return;
      this._dtLinks = this.GetTableIDs(sessionKeeper.Session, asString);
      if (this._dtLinks == null)
        return;
      string strCompleted = LocalizationHolder.rm.GetString("Imbase.Processed.Msg");
      int index = -1;
      double count = Convert.ToDouble(this._dtLinks.Rows.Count);
      ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
      ParallelOptions po = new ParallelOptions()
      {
        CancellationToken = this._cts.Token,
        MaxDegreeOfParallelism = Environment.ProcessorCount
      };
      Task.Factory.StartNew((Action) (() => Parallel.ForEach<DataRow>((IEnumerable<DataRow>) this._dtLinks.AsEnumerable(), po, (Action<DataRow>) (r =>
      {
        try
        {
          po.CancellationToken.ThrowIfCancellationRequested();
          string str = Convert.ToString(r[ObligatoryObjectAttributes.CAPTION.ToString()]);
          long int64 = Convert.ToInt64(r["TableID"]);
          foreach (IMSAttributeType tableAttribute in this.GetTableAttributes(int64))
          {
            if (!this._attrCache.TryAdd(tableAttribute.AttributeID, new Tuple<IMSAttributeType, ConcurrentDictionary<long, string>>(tableAttribute, new ConcurrentDictionary<long, string>()
            {
              [int64] = str
            })))
              this._attrCache[tableAttribute.AttributeID].Item2.TryAdd(int64, str);
          }
          if (!this.InvokeRequired)
            return;
          this.Invoke((Delegate) (() =>
          {
            this._lbCompleted.Text = string.Format(strCompleted, (object) this._progress.Value);
            this._progress.Value = Convert.ToInt32(Math.Floor((double) ++index / count * 100.0));
            this._progress.Refresh();
          }));
        }
        catch (OperationCanceledException ex)
        {
        }
        catch (Exception ex)
        {
          exceptions.Enqueue(ex);
        }
      })))).ContinueWith<object>((System.Func<Task, object>) (t => this.Invoke((Delegate) (() =>
      {
        if (exceptions.Count > 0)
          throw new AggregateException((IEnumerable<Exception>) exceptions);
        ICategoryTypeIconService categoryTypeIconService = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, false);
        this._lvAttrs.SmallImageList = categoryTypeIconService?.ImageList;
        if (this._attrCache.Count > 0)
        {
          this._lvAttrs.BeginUpdate();
          this._lvAttrs.Items.AddRange(this._attrCache.Values.Select<Tuple<IMSAttributeType, ConcurrentDictionary<long, string>>, ListViewItem>((System.Func<Tuple<IMSAttributeType, ConcurrentDictionary<long, string>>, ListViewItem>) (x =>
          {
            ListViewItem listViewItem = new ListViewItem(x.Item1.Name);
            listViewItem.Tag = (object) x.Item1;
            ICategoryTypeIconService categoryTypeIconService1 = categoryTypeIconService;
            listViewItem.ImageIndex = categoryTypeIconService1 != null ? categoryTypeIconService1.IndexOf(3, -1, (object) x.Item1.FieldType) : -1;
            return listViewItem;
          })).ToArray<ListViewItem>());
          this._lvAttrs.EndUpdate();
        }
        this._pnlProgress.Visible = false;
        this.pnlFirst.Dock = DockStyle.Fill;
        this.pnlButton.Visible = true;
        this.btnNext.Text = LocalizationHolder.rm.GetString("Imbase_Wizards_Next");
      }))), this._cts.Token);
    }
  }

  private void _lvAttrs_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._lvTables.Items.Clear();
    Tuple<IMSAttributeType, ConcurrentDictionary<long, string>> tuple;
    if (this._lvAttrs.SelectedItems.Count <= 0 || !(this._lvAttrs.SelectedItems[0].Tag is IMSAttributeType tag) || !this._attrCache.TryGetValue(tag.AttributeID, out tuple))
      return;
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, false);
    int imgIndx = service != null ? service.IndexOf(4, Intermech.Imbase.Consts.ImbaseTableTypeID, (object) null) : -1;
    this._lvTables.SmallImageList = service?.ImageList;
    ListViewItem[] array = tuple.Item2.Select<KeyValuePair<long, string>, ListViewItem>((System.Func<KeyValuePair<long, string>, ListViewItem>) (x => new ListViewItem(x.Value)
    {
      Tag = (object) x.Key,
      ImageIndex = imgIndx,
      ToolTipText = x.Key.ToString()
    })).ToArray<ListViewItem>();
    this._lvTables.BeginUpdate();
    this._lvTables.Items.AddRange(array);
    this._lvTables.EndUpdate();
    this.btnNext.Enabled = true;
  }

  private void tmsiOpenTableInNewWindow_Click(object sender, EventArgs e)
  {
    if (this._lvTables.SelectedItems.Count <= 0 || !(this._lvTables.SelectedItems[0].Tag is long tag))
      return;
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(tag), (System.IServiceProvider) ServicesManager.ServiceContainer);
  }

  private void btnNext_Click(object sender, EventArgs e)
  {
    if (this.pnlFirst.Visible)
    {
      if (this._lvAttrs.SelectedItems.Count <= 0)
        return;
      this.pnlFirst.Visible = false;
      this.pnlSecond.Visible = true;
      if (!(this._lvAttrs.SelectedItems[0].Tag is IMSAttributeType tag))
        return;
      this.FillAllowableAttrsList(tag);
      this._replaceAttrs.Columns[0].Width = -2;
      this.btnReplace.Enabled = false;
      this.btnNext.Text = LocalizationHolder.rm.GetString("Imbase_Wizards_Prev");
      this.btnReplace.Enabled = false;
    }
    else
    {
      this.pnlSecond.Visible = false;
      this.pnlFirst.Visible = true;
      this.btnNext.Text = LocalizationHolder.rm.GetString("Imbase_Wizards_Next");
      this.btnReplace.Enabled = false;
    }
  }

  private void _replaceAttrs_Click(object sender, EventArgs e)
  {
    if (this._replaceAttrs.SelectedItems.Count <= 0)
      return;
    this.btnReplace.Enabled = true;
  }

  private void btnReplace_Click(object sender, EventArgs e)
  {
    Tuple<IMSAttributeType, ConcurrentDictionary<long, string>> tuple;
    if (this._lvAttrs.SelectedItems.Count <= 0 || !(this._lvAttrs.SelectedItems[0].Tag is IMSAttributeType tag1) || this._replaceAttrs.SelectedItems.Count <= 0 || !(this._replaceAttrs.SelectedItems[0].Tag is IMSAttributeType tag2) || MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase_ReplaceAttribute"), (object) tag1.Name, (object) tag2.Name), LocalizationHolder.rm.GetString("Imbase.Client_1133"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes || !this._attrCache.TryGetValue(tag1.AttributeID, out tuple))
      return;
    Tuple<IMSAttributeType, IMSAttributeType, long[]> inputData = new Tuple<IMSAttributeType, IMSAttributeType, long[]>(tag1, tag2, tuple.Item2.Keys.ToArray<long>());
    if (!(ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service))
      return;
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IReplaceAttributeTaskService)) is IReplaceAttributeTaskService customService)
    {
      ReplaceAttributeBackgroundTask attributeBackgroundTask = new ReplaceAttributeBackgroundTask((IServiceForBackgroundTask) customService);
      attributeBackgroundTask.Name = string.Format(LocalizationHolder.rm.GetString("Imbase_ReplaceAttribute_TaskName"), (object) tag1.Name, (object) tag2.Name);
      ReplaceAttributeBackgroundTask task = attributeBackgroundTask;
      service.AddTask((IBackgroundTask) task);
      task.StartTask((object) inputData);
      this.Close();
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("Imbase_Message");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_TablesIndexerService_Null"), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReplaceAttributesView));
    this._timer = new System.Windows.Forms.Timer(this.components);
    this.pnlFirst = new Panel();
    this.tlpFirst = new TableLayoutPanel();
    this._lvAttrs = new ListView();
    this.colAttrsName = new ColumnHeader();
    this._lvTables = new ListView();
    this.colTablesName = new ColumnHeader();
    this.cmsTable = new ContextMenuStrip(this.components);
    this.tmsiOpenTableInNewWindow = new ToolStripMenuItem();
    this._pnlProgress = new Panel();
    this._progress = new ProgressBar();
    this._pnlScanInfo = new Panel();
    this._lbCompleted = new Label();
    this._lbTaskInfo = new Label();
    this.pnlSecond = new Panel();
    this._replaceAttrs = new ListView();
    this.attrNameColumn = new ColumnHeader();
    this.pnlButton = new Panel();
    this.tlpnlButton = new TableLayoutPanel();
    this.btnNext = new Button();
    this.btnReplace = new Button();
    this.pnlFirst.SuspendLayout();
    this.tlpFirst.SuspendLayout();
    this.cmsTable.SuspendLayout();
    this._pnlProgress.SuspendLayout();
    this._pnlScanInfo.SuspendLayout();
    this.pnlSecond.SuspendLayout();
    this.pnlButton.SuspendLayout();
    this.tlpnlButton.SuspendLayout();
    this.SuspendLayout();
    this._timer.Interval = 500;
    this.pnlFirst.Controls.Add((Control) this.tlpFirst);
    componentResourceManager.ApplyResources((object) this.pnlFirst, "pnlFirst");
    this.pnlFirst.Name = "pnlFirst";
    componentResourceManager.ApplyResources((object) this.tlpFirst, "tlpFirst");
    this.tlpFirst.Controls.Add((Control) this._lvAttrs, 0, 0);
    this.tlpFirst.Controls.Add((Control) this._lvTables, 1, 0);
    this.tlpFirst.Name = "tlpFirst";
    this._lvAttrs.Columns.AddRange(new ColumnHeader[1]
    {
      this.colAttrsName
    });
    componentResourceManager.ApplyResources((object) this._lvAttrs, "_lvAttrs");
    this._lvAttrs.FullRowSelect = true;
    this._lvAttrs.HeaderStyle = ColumnHeaderStyle.None;
    this._lvAttrs.HideSelection = false;
    this._lvAttrs.MultiSelect = false;
    this._lvAttrs.Name = "_lvAttrs";
    this._lvAttrs.Sorting = SortOrder.Ascending;
    this._lvAttrs.UseCompatibleStateImageBehavior = false;
    this._lvAttrs.View = View.Details;
    this._lvAttrs.SelectedIndexChanged += new EventHandler(this._lvAttrs_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.colAttrsName, "colAttrsName");
    this._lvTables.Columns.AddRange(new ColumnHeader[1]
    {
      this.colTablesName
    });
    this._lvTables.ContextMenuStrip = this.cmsTable;
    componentResourceManager.ApplyResources((object) this._lvTables, "_lvTables");
    this._lvTables.FullRowSelect = true;
    this._lvTables.HeaderStyle = ColumnHeaderStyle.None;
    this._lvTables.HideSelection = false;
    this._lvTables.MultiSelect = false;
    this._lvTables.Name = "_lvTables";
    this._lvTables.ShowItemToolTips = true;
    this._lvTables.Sorting = SortOrder.Ascending;
    this._lvTables.UseCompatibleStateImageBehavior = false;
    this._lvTables.View = View.Details;
    componentResourceManager.ApplyResources((object) this.colTablesName, "colTablesName");
    this.cmsTable.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.tmsiOpenTableInNewWindow
    });
    this.cmsTable.Name = "cmsTable";
    componentResourceManager.ApplyResources((object) this.cmsTable, "cmsTable");
    this.tmsiOpenTableInNewWindow.Name = "tmsiOpenTableInNewWindow";
    componentResourceManager.ApplyResources((object) this.tmsiOpenTableInNewWindow, "tmsiOpenTableInNewWindow");
    this.tmsiOpenTableInNewWindow.Click += new EventHandler(this.tmsiOpenTableInNewWindow_Click);
    this._pnlProgress.Controls.Add((Control) this._progress);
    this._pnlProgress.Controls.Add((Control) this._pnlScanInfo);
    componentResourceManager.ApplyResources((object) this._pnlProgress, "_pnlProgress");
    this._pnlProgress.Name = "_pnlProgress";
    componentResourceManager.ApplyResources((object) this._progress, "_progress");
    this._progress.Name = "_progress";
    this._progress.Step = 1;
    this._pnlScanInfo.Controls.Add((Control) this._lbCompleted);
    this._pnlScanInfo.Controls.Add((Control) this._lbTaskInfo);
    componentResourceManager.ApplyResources((object) this._pnlScanInfo, "_pnlScanInfo");
    this._pnlScanInfo.Name = "_pnlScanInfo";
    componentResourceManager.ApplyResources((object) this._lbCompleted, "_lbCompleted");
    this._lbCompleted.Name = "_lbCompleted";
    componentResourceManager.ApplyResources((object) this._lbTaskInfo, "_lbTaskInfo");
    this._lbTaskInfo.Name = "_lbTaskInfo";
    this.pnlSecond.Controls.Add((Control) this._replaceAttrs);
    componentResourceManager.ApplyResources((object) this.pnlSecond, "pnlSecond");
    this.pnlSecond.Name = "pnlSecond";
    this._replaceAttrs.Columns.AddRange(new ColumnHeader[1]
    {
      this.attrNameColumn
    });
    componentResourceManager.ApplyResources((object) this._replaceAttrs, "_replaceAttrs");
    this._replaceAttrs.FullRowSelect = true;
    this._replaceAttrs.HeaderStyle = ColumnHeaderStyle.None;
    this._replaceAttrs.HideSelection = false;
    this._replaceAttrs.Name = "_replaceAttrs";
    this._replaceAttrs.Sorting = SortOrder.Ascending;
    this._replaceAttrs.UseCompatibleStateImageBehavior = false;
    this._replaceAttrs.View = View.Details;
    this._replaceAttrs.Click += new EventHandler(this._replaceAttrs_Click);
    componentResourceManager.ApplyResources((object) this.attrNameColumn, "attrNameColumn");
    this.pnlButton.Controls.Add((Control) this.tlpnlButton);
    componentResourceManager.ApplyResources((object) this.pnlButton, "pnlButton");
    this.pnlButton.Name = "pnlButton";
    componentResourceManager.ApplyResources((object) this.tlpnlButton, "tlpnlButton");
    this.tlpnlButton.Controls.Add((Control) this.btnNext, 1, 0);
    this.tlpnlButton.Controls.Add((Control) this.btnReplace, 3, 0);
    this.tlpnlButton.Name = "tlpnlButton";
    componentResourceManager.ApplyResources((object) this.btnNext, "btnNext");
    this.btnNext.Name = "btnNext";
    this.btnNext.UseVisualStyleBackColor = true;
    this.btnNext.Click += new EventHandler(this.btnNext_Click);
    componentResourceManager.ApplyResources((object) this.btnReplace, "btnReplace");
    this.btnReplace.Name = "btnReplace";
    this.btnReplace.UseVisualStyleBackColor = true;
    this.btnReplace.Click += new EventHandler(this.btnReplace_Click);
    this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlFirst);
    this.Controls.Add((Control) this.pnlSecond);
    this.Controls.Add((Control) this._pnlProgress);
    this.Controls.Add((Control) this.pnlButton);
    this.DoubleBuffered = true;
    this.FloatingSize = new Size(700, 400);
    this.Name = nameof (ReplaceAttributesView);
    this.PersistState = false;
    this.ShowImageInDocumentTab = true;
    this.BeforeFirstShown += new EventHandler(this.OnBeforeFirstShown);
    this.pnlFirst.ResumeLayout(false);
    this.tlpFirst.ResumeLayout(false);
    this.cmsTable.ResumeLayout(false);
    this._pnlProgress.ResumeLayout(false);
    this._pnlScanInfo.ResumeLayout(false);
    this.pnlSecond.ResumeLayout(false);
    this.pnlButton.ResumeLayout(false);
    this.tlpnlButton.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
