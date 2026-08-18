// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Indexes.ImbaseIndexesView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Imbase.BackgroundTask;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Indexes;

public class ImbaseIndexesView : UserControl, IView, ISecurityCallback
{
  private ISelectedItems _iSelectedItem;
  private System.IServiceProvider _provider;
  internal static int imageIndex = -1;
  private long _catalogID = -1;
  private ImbaseIndexesView.ListItemIndex _items = new ImbaseIndexesView.ListItemIndex();
  private Font _boldFont;
  private IContainer components;
  private GroupBox _gbDelimiter;
  private ContextMenuStrip _contextMenu;
  private ToolStripMenuItem _miAdd;
  private ToolStripMenuItem _miDel;
  private ToolStripMenuItem _miClear;
  private ToolStripMenuItem _miUpdate;
  private SplitContainer splitContainer1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private ToolStrip toolStrip1;
  private ToolStripButton _tsBtnAdd;
  private ToolStripButton _tsBtnDelete;
  private ToolStripButton _tsBtnClear;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton _tsBtnUpdate;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton _tsBtnOK;
  private ToolStripButton _tsBtnCancel;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripButton _tsbtnSecurity;
  private DataGridView _dgv;
  private DataGridViewCheckBoxColumn colUnique;
  private DataGridViewImageColumn colIco;
  private DataGridViewTextBoxColumn colAttribute;
  private NotUniqueRecordsCtrl _notUniqueRecordsCtrl;

  public ImbaseIndexesView()
  {
    this.InitializeComponent();
    this._boldFont = new Font(this._dgv.Font.Name, this._dgv.Font.Size, FontStyle.Bold);
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, false);
    if (service == null)
      return;
    int index = service.ImageIndex("imgKeys");
    if (index == -1)
      return;
    this._tsbtnSecurity.Image = service.ImageList.Images[index];
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.ToFirstState();
    this._iSelectedItem = items;
    this._provider = this._notUniqueRecordsCtrl.Provider = provider;
    this._notUniqueRecordsCtrl.GetNotUnique -= new NotUniqueRecordsCtrl.GetNotUniqueEventHandler(this.On_notUniqueRecordsCtrl_GetNotUnique);
    this._notUniqueRecordsCtrl.GetNotUnique += new NotUniqueRecordsCtrl.GetNotUniqueEventHandler(this.On_notUniqueRecordsCtrl_GetNotUnique);
    if (items == null || items.Count <= 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectID == 0L)
      return;
    this._notUniqueRecordsCtrl.CatalogID = this._catalogID = itemData.ObjectID;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
          throw new Exception(LocalizationHolder.rm.GetString(sc_7833.ssp_imbase_7834()));
        Guid sessionGuid = sessionKeeper.Session.SessionGUID;
        List<long> catalogIDs = new List<long>();
        catalogIDs.Add(this._catalogID);
        string[] colsNames = new string[2]
        {
          IndexesField.F_ATTRIBUTE_ID,
          IndexesField.F_FLAG
        };
        DataTable indexes = customService.GetIndexes(sessionGuid, catalogIDs, colsNames);
        if (indexes != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
          {
            IndexesFlags flags = IndexesFlags.None;
            int int32 = Convert.ToInt32(row[IndexesField.F_FLAG]);
            if (int32 != -1)
              flags = (IndexesFlags) int32;
            this.AddRow(Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]), flags);
          }
          if (this._dgv.Rows.Count > 0)
            this._dgv.Rows[0].Selected = true;
        }
      }
      this.CheckButtonState();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  public void Activate(IView previousView)
  {
  }

  public void Deactivate(IView nextView)
  {
    if (this._items.HasChanges)
    {
      string caption = LocalizationHolder.rm.GetString(sc_7833.ssp_imbase_7835());
      if (MessageBox.Show((IWin32Window) this.Parent, LocalizationHolder.rm.GetString("Imbase_ImbaseIndexesView_SaveIndexes_Message"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.On_btnOK_Click((object) this._tsBtnOK, (EventArgs) null);
      else
        this.ToFirstState();
    }
    this._notUniqueRecordsCtrl.GetNotUnique -= new NotUniqueRecordsCtrl.GetNotUniqueEventHandler(this.On_notUniqueRecordsCtrl_GetNotUnique);
  }

  public string Caption => LocalizationHolder.rm.GetString("Imbase_ImbaseIndexesView_Caption");

  public int ImageIndex => ImbaseIndexesView.imageIndex;

  public int OrderID => 15;

  private void On_btnAdd_Click(object sender, EventArgs e)
  {
    using (ImbaseAddIndexDialog imbaseAddIndexDialog = new ImbaseAddIndexDialog(this._catalogID, this._items.GetIncludedIndexesIDs))
    {
      if (imbaseAddIndexDialog.ShowDialog() == DialogResult.Cancel)
        return;
      List<int> selectedAttrs = imbaseAddIndexDialog.SelectedAttrs;
      DataGridViewRow dataGridViewRow = (DataGridViewRow) null;
      foreach (int num in selectedAttrs)
      {
        if (this._items.ContainsKey(num))
        {
          this._items.ChangeStatus(num, this._items[num].HasChanges ? IndexesStatus.Changed : IndexesStatus.None);
          this._dgv.Rows.Add(this._items[num].Row);
          dataGridViewRow = this._items[num].Row;
        }
        else
          dataGridViewRow = this.AddRow(num, IndexesFlags.None, IndexesStatus.Added);
      }
      foreach (DataGridViewBand selectedRow in (BaseCollection) this._dgv.SelectedRows)
        selectedRow.Selected = false;
      if (dataGridViewRow == null)
        return;
      dataGridViewRow.Selected = true;
    }
  }

  private void On_btnCancel_Click(object sender, EventArgs e)
  {
    this.Initialize(this._iSelectedItem, this._provider);
  }

  private void On_btnClear_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        return;
      foreach (DataGridViewBand row in (IEnumerable) this._dgv.Rows)
      {
        int int32 = Convert.ToInt32(row.Tag);
        customService.PurgeSecurityForIndex(sessionKeeper.Session.SessionGUID, this._catalogID, int32);
      }
      this._items.ChangeStatus(-1, IndexesStatus.Removed);
      this._dgv.Rows.Clear();
      this.CheckButtonState();
    }
  }

  private void On_btnClear_EnabledChanged(object sender, EventArgs e)
  {
    this._miClear.Enabled = this._tsBtnClear.Enabled;
  }

  private void On_btnDelete_Click(object sender, EventArgs e)
  {
    if (this._dgv.SelectedRows.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        return;
      foreach (DataGridViewRow selectedRow in (BaseCollection) this._dgv.SelectedRows)
      {
        string caption = LocalizationHolder.rm.GetString(sc_7833.ssp_imbase_7836());
        string format = LocalizationHolder.rm.GetString("Imbase_Index_Delete");
        int int32 = Convert.ToInt32(selectedRow.Tag);
        if (MessageBox.Show((IWin32Window) this.Parent, string.Format(format, (object) MetaDataHelper.GetAttributeTypeName(int32)), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          this._items.ChangeStatus(int32, IndexesStatus.Removed);
          this._dgv.Rows.Remove(selectedRow);
          customService.PurgeSecurityForIndex(sessionKeeper.Session.SessionGUID, this._catalogID, int32);
        }
      }
    }
  }

  private void On_btnDelete_EnabledChanged(object sender, EventArgs e)
  {
    this._miDel.Enabled = this._tsBtnDelete.Enabled;
  }

  private void On_btnOK_Click(object sender, EventArgs e)
  {
    IBackgroundTaskView service = (IBackgroundTaskView) ServicesManager.GetService(typeof (IBackgroundTaskView));
    if (service == null)
      return;
    Dictionary<int, bool> added = new Dictionary<int, bool>(0);
    List<int> removed = new List<int>(0);
    Dictionary<int, bool> changed = new Dictionary<int, bool>(0);
    this._items.SortItemsByStatus(added, removed, changed);
    if (added.Count > 0)
    {
      IndexesHelper helper = new IndexesHelper(this._catalogID, IndexesStatus.Added)
      {
        AddedIndexes = added
      };
      service.AddTask((IBackgroundTask) new ImbaseIndexesBackgroundTask(helper));
    }
    if (removed.Count > 0)
    {
      IndexesHelper helper = new IndexesHelper(this._catalogID, IndexesStatus.Removed)
      {
        RemovedIndexes = removed
      };
      service.AddTask((IBackgroundTask) new ImbaseIndexesBackgroundTask(helper));
    }
    if (changed.Count > 0)
    {
      IndexesHelper helper = new IndexesHelper(this._catalogID, IndexesStatus.Changed)
      {
        ChangedIndexes = changed
      };
      service.AddTask((IBackgroundTask) new ImbaseIndexesBackgroundTask(helper));
    }
    this._items.AfterSave();
    this.On_dgv_SelectionChanged(sender, e);
  }

  private void On_btnUpdate_Click(object sender, EventArgs e)
  {
    IBackgroundTaskView service = (IBackgroundTaskView) ServicesManager.GetService(typeof (IBackgroundTaskView));
    if (service == null)
      return;
    IndexesHelper helper = new IndexesHelper(this._catalogID)
    {
      Actions = IndexesStatus.Update
    };
    service.AddTask((IBackgroundTask) new ImbaseIndexesBackgroundTask(helper));
    Thread.Sleep(100);
    this.Initialize(this._iSelectedItem, this._provider);
  }

  private void tsbtnSecurity_Click(object sender, EventArgs e)
  {
    if (this._dgv.SelectedRows.Count != 1)
      return;
    List<object> objectList = new List<object>()
    {
      (object) ImbaseHelper.CreateCategoryId(this._catalogID, (long) Convert.ToInt32(this._dgv.SelectedRows[0].Tag))
    };
    using (SecurityEditorForm securityEditorForm = new SecurityEditorForm())
      securityEditorForm.Execute(objectList.ToArray(), (ISecurityCallback) this, false);
  }

  private void On_btnUpdate_EnabledChanged(object sender, EventArgs e)
  {
    this._miUpdate.Enabled = this._tsBtnUpdate.Enabled;
  }

  private void On_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
  {
    if (e.ColumnIndex != 0 && e.ColumnIndex != 1 || e.RowIndex <= -1)
      return;
    int int32 = Convert.ToInt32(this._dgv.Rows[e.RowIndex].Tag);
    this._items[int32].Unique = Convert.ToBoolean(this._dgv.Rows[e.RowIndex].Cells[0].EditedFormattedValue);
    if (this._items[int32].HasChanges)
    {
      if (this._items[int32].Status == IndexesStatus.None)
        this._items[int32].Status = IndexesStatus.Changed;
    }
    else
      this._items[int32].Status = IndexesStatus.None;
    this.On_dgv_SelectionChanged(sender, (EventArgs) e);
  }

  private void On_dgv_SelectionChanged(object sender, EventArgs e)
  {
    if (this._dgv.SelectedRows.Count == 1)
    {
      int int32 = Convert.ToInt32(this._dgv.SelectedRows[0].Tag);
      this._notUniqueRecordsCtrl.AttrID = this._items[int32].Status == IndexesStatus.None ? int32 : 0;
      this._notUniqueRecordsCtrl.Fill(this._items[int32].NotUniqueData);
      if (Convert.ToBoolean(this._dgv.SelectedRows[0].Cells[0].EditedFormattedValue))
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel2.Show();
      }
      else
      {
        this.splitContainer1.Panel2Collapsed = true;
        this.splitContainer1.Panel2.Hide();
      }
    }
    else
    {
      this._notUniqueRecordsCtrl.AttrID = 0;
      this.splitContainer1.Panel2Collapsed = true;
      this.splitContainer1.Panel2.Hide();
    }
    this.CheckButtonState();
  }

  private void On_notUniqueRecordsCtrl_GetNotUnique(int attrID, DataTable dtNotUnique)
  {
    if (!this._items.ContainsKey(attrID))
      return;
    this._items[attrID].NotUniqueData = dtNotUnique;
  }

  private DataGridViewRow AddRow(int attrId, IndexesFlags flags, IndexesStatus status = IndexesStatus.None)
  {
    DataGridViewRow row = (DataGridViewRow) null;
    bool isUnique = flags.HasFlag((Enum) IndexesFlags.UniqueValue);
    if (attrId != 0)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
      int index = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.FieldType);
      Image image = index > -1 ? Statics.IconSrv.ImageList.Images[index] : (Image) null;
      bool flag = false;
      string name;
      if (attrId == Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID)
      {
        name = LocalizationHolder.rm.GetString("Imbase_AttrGUIDRecord_Name");
        isUnique = flag = true;
      }
      else
        name = attributeType.Name;
      DataGridViewRow dataGridViewRow1 = new DataGridViewRow();
      dataGridViewRow1.Tag = (object) attrId;
      DataGridViewRow dataGridViewRow2 = dataGridViewRow1;
      dataGridViewRow2.CreateCells(this._dgv, (object) isUnique, (object) image, (object) name);
      row = this._dgv.Rows[this._dgv.Rows.Add(dataGridViewRow2)];
      row.Cells[0].ReadOnly = flag;
      this._items.Add(attrId, row, isUnique, status);
      if (status == IndexesStatus.Added)
      {
        row.Cells[2].Style.ForeColor = Color.Blue;
        row.Cells[2].Style.Font = this._boldFont;
      }
    }
    return row;
  }

  private void CheckButtonState()
  {
    this._tsBtnDelete.Enabled = this._tsbtnSecurity.Enabled = this._dgv.SelectedRows.Count > 0;
    this._tsBtnClear.Enabled = this._tsBtnUpdate.Enabled = this._dgv.Rows.Count > 0;
    this._tsBtnOK.Enabled = this._tsBtnCancel.Enabled = this._items.HasChanges;
  }

  private void ToFirstState()
  {
    this._dgv.Rows.Clear();
    this._items.Clear();
    this.CheckButtonState();
    this._notUniqueRecordsCtrl.AttrID = 0;
  }

  public int MaintainedCategory => 30;

  public Tuple<int, object> Applicability => (Tuple<int, object>) null;

  public IDBSecurity GetSecurity(IUserSession session, object id)
  {
    long int64 = Convert.ToInt64(id);
    if (int64 == 0L)
      return (IDBSecurity) null;
    IDBSecurity security = (IDBSecurity) null;
    long objectId;
    int id1;
    ImbaseHelper.GetObjectAndId(int64, out objectId, out id1);
    if (session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
      security = customService.GetSecurityForIndex(session.SessionGUID, objectId, id1);
    return security;
  }

  protected override void Dispose(bool disposing)
  {
    if (this._boldFont != null)
    {
      this._boldFont.Dispose();
      this._boldFont = (Font) null;
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseIndexesView));
    DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
    this._gbDelimiter = new GroupBox();
    this._contextMenu = new ContextMenuStrip(this.components);
    this._miAdd = new ToolStripMenuItem();
    this._miDel = new ToolStripMenuItem();
    this._miClear = new ToolStripMenuItem();
    this._miUpdate = new ToolStripMenuItem();
    this.splitContainer1 = new SplitContainer();
    this._dgv = new DataGridView();
    this.colUnique = new DataGridViewCheckBoxColumn();
    this.colIco = new DataGridViewImageColumn();
    this.toolStrip1 = new ToolStrip();
    this._tsBtnAdd = new ToolStripButton();
    this._tsBtnDelete = new ToolStripButton();
    this._tsBtnClear = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._tsBtnUpdate = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this._tsBtnOK = new ToolStripButton();
    this._tsBtnCancel = new ToolStripButton();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this._tsbtnSecurity = new ToolStripButton();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.colAttribute = new DataGridViewTextBoxColumn();
    this._notUniqueRecordsCtrl = new NotUniqueRecordsCtrl();
    this._contextMenu.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    ((ISupportInitialize) this._dgv).BeginInit();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._gbDelimiter, "_gbDelimiter");
    this._gbDelimiter.Name = "_gbDelimiter";
    this._gbDelimiter.TabStop = false;
    this._contextMenu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._miAdd,
      (ToolStripItem) this._miDel,
      (ToolStripItem) this._miClear,
      (ToolStripItem) this._miUpdate
    });
    this._contextMenu.Name = "_contextMenu";
    componentResourceManager.ApplyResources((object) this._contextMenu, "_contextMenu");
    this._miAdd.Image = (Image) Intermech.Imbase.Properties.Resources.plus;
    componentResourceManager.ApplyResources((object) this._miAdd, "_miAdd");
    this._miAdd.Name = "_miAdd";
    this._miAdd.Click += new EventHandler(this.On_btnAdd_Click);
    componentResourceManager.ApplyResources((object) this._miDel, "_miDel");
    this._miDel.Image = (Image) Intermech.Imbase.Properties.Resources.min;
    this._miDel.Name = "_miDel";
    this._miDel.Click += new EventHandler(this.On_btnDelete_Click);
    componentResourceManager.ApplyResources((object) this._miClear, "_miClear");
    this._miClear.Image = (Image) Intermech.Imbase.Properties.Resources.clean;
    this._miClear.Name = "_miClear";
    this._miClear.Click += new EventHandler(this.On_btnClear_Click);
    this._miUpdate.Image = (Image) Intermech.Imbase.Properties.Resources.Synch;
    componentResourceManager.ApplyResources((object) this._miUpdate, "_miUpdate");
    this._miUpdate.Name = "_miUpdate";
    this._miUpdate.Click += new EventHandler(this.On_btnUpdate_Click);
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this._dgv);
    this.splitContainer1.Panel1.Controls.Add((Control) this.toolStrip1);
    this.splitContainer1.Panel2.Controls.Add((Control) this._notUniqueRecordsCtrl);
    this._dgv.AllowUserToAddRows = false;
    this._dgv.AllowUserToDeleteRows = false;
    this._dgv.AllowUserToResizeRows = false;
    this._dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this._dgv.BackgroundColor = SystemColors.Window;
    this._dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._dgv.Columns.AddRange((DataGridViewColumn) this.colUnique, (DataGridViewColumn) this.colIco, (DataGridViewColumn) this.colAttribute);
    this._dgv.ContextMenuStrip = this._contextMenu;
    componentResourceManager.ApplyResources((object) this._dgv, "_dgv");
    this._dgv.Name = "_dgv";
    this._dgv.RowHeadersVisible = false;
    this._dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._dgv.CellContentClick += new DataGridViewCellEventHandler(this.On_dgv_CellContentClick);
    this._dgv.SelectionChanged += new EventHandler(this.On_dgv_SelectionChanged);
    this.colUnique.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
    this.colUnique.Frozen = true;
    componentResourceManager.ApplyResources((object) this.colUnique, "colUnique");
    this.colUnique.Name = "colUnique";
    this.colIco.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    gridViewCellStyle.NullValue = (object) null;
    gridViewCellStyle.Padding = new Padding(3, 0, 0, 0);
    this.colIco.DefaultCellStyle = gridViewCellStyle;
    componentResourceManager.ApplyResources((object) this.colIco, "colIco");
    this.colIco.Name = "colIco";
    this.colIco.ReadOnly = true;
    this.toolStrip1.Items.AddRange(new ToolStripItem[10]
    {
      (ToolStripItem) this._tsBtnAdd,
      (ToolStripItem) this._tsBtnDelete,
      (ToolStripItem) this._tsBtnClear,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._tsBtnUpdate,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this._tsBtnOK,
      (ToolStripItem) this._tsBtnCancel,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this._tsbtnSecurity
    });
    componentResourceManager.ApplyResources((object) this.toolStrip1, "toolStrip1");
    this.toolStrip1.Name = "toolStrip1";
    this._tsBtnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._tsBtnAdd.Image = (Image) Intermech.Imbase.Properties.Resources.plus;
    componentResourceManager.ApplyResources((object) this._tsBtnAdd, "_tsBtnAdd");
    this._tsBtnAdd.Name = "_tsBtnAdd";
    this._tsBtnAdd.Click += new EventHandler(this.On_btnAdd_Click);
    this._tsBtnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._tsBtnDelete, "_tsBtnDelete");
    this._tsBtnDelete.Image = (Image) Intermech.Imbase.Properties.Resources.min;
    this._tsBtnDelete.Name = "_tsBtnDelete";
    this._tsBtnDelete.Click += new EventHandler(this.On_btnDelete_Click);
    this._tsBtnDelete.EnabledChanged += new EventHandler(this.On_btnDelete_EnabledChanged);
    this._tsBtnClear.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._tsBtnClear, "_tsBtnClear");
    this._tsBtnClear.Image = (Image) Intermech.Imbase.Properties.Resources.clean;
    this._tsBtnClear.Name = "_tsBtnClear";
    this._tsBtnClear.Click += new EventHandler(this.On_btnClear_Click);
    this._tsBtnClear.EnabledChanged += new EventHandler(this.On_btnClear_EnabledChanged);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this._tsBtnUpdate.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._tsBtnUpdate, "_tsBtnUpdate");
    this._tsBtnUpdate.Image = (Image) Intermech.Imbase.Properties.Resources.Synch;
    this._tsBtnUpdate.Name = "_tsBtnUpdate";
    this._tsBtnUpdate.Click += new EventHandler(this.On_btnUpdate_Click);
    this._tsBtnUpdate.EnabledChanged += new EventHandler(this.On_btnUpdate_EnabledChanged);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator2, "toolStripSeparator2");
    this._tsBtnOK.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._tsBtnOK.Image = (Image) Intermech.Imbase.Properties.Resources.Apply;
    componentResourceManager.ApplyResources((object) this._tsBtnOK, "_tsBtnOK");
    this._tsBtnOK.Name = "_tsBtnOK";
    this._tsBtnOK.Click += new EventHandler(this.On_btnOK_Click);
    this._tsBtnCancel.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._tsBtnCancel.Image = (Image) Intermech.Imbase.Properties.Resources.Cancel;
    componentResourceManager.ApplyResources((object) this._tsBtnCancel, "_tsBtnCancel");
    this._tsBtnCancel.Name = "_tsBtnCancel";
    this._tsBtnCancel.Click += new EventHandler(this.On_btnCancel_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator3, "toolStripSeparator3");
    this._tsbtnSecurity.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._tsbtnSecurity, "_tsbtnSecurity");
    this._tsbtnSecurity.Name = "_tsbtnSecurity";
    this._tsbtnSecurity.Click += new EventHandler(this.tsbtnSecurity_Click);
    this.dataGridViewTextBoxColumn1.FillWeight = 98.47716f;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.colAttribute.FillWeight = 98.47716f;
    componentResourceManager.ApplyResources((object) this.colAttribute, "colAttribute");
    this.colAttribute.Name = "colAttribute";
    this.colAttribute.ReadOnly = true;
    this._notUniqueRecordsCtrl.AttrID = 0;
    this._notUniqueRecordsCtrl.CatalogID = 0L;
    componentResourceManager.ApplyResources((object) this._notUniqueRecordsCtrl, "_notUniqueRecordsCtrl");
    this._notUniqueRecordsCtrl.Name = "_notUniqueRecordsCtrl";
    this._notUniqueRecordsCtrl.Provider = (System.IServiceProvider) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this._gbDelimiter);
    this.Name = nameof (ImbaseIndexesView);
    this._contextMenu.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    ((ISupportInitialize) this._dgv).EndInit();
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
  }

  private class ItemIndex
  {
    private bool _isFirstUnique;

    private bool UniqueChanges => this._isFirstUnique != this.Unique;

    internal bool HasChanges => this.UniqueChanges || this.Status != 0;

    internal int Id { get; }

    internal DataTable NotUniqueData { get; set; }

    internal DataGridViewRow Row { get; }

    internal IndexesStatus Status { get; set; }

    internal bool Unique { get; set; }

    internal ItemIndex(int id, DataGridViewRow row, bool isUnique = false, IndexesStatus status = IndexesStatus.None)
    {
      this.Id = id;
      this.Row = row;
      this._isFirstUnique = this.Unique = isUnique;
      this.Status = status;
    }

    internal void AfterSave()
    {
      if (this.Status == IndexesStatus.Added)
      {
        this.Row.Cells[2].Style.ForeColor = Color.Black;
        this.Row.Cells[2].Style.Font = new Font(this.Row.Cells[2].Style.Font.Name, this.Row.Cells[2].Style.Font.Size, FontStyle.Regular);
      }
      this._isFirstUnique = this.Unique;
      this.Status = IndexesStatus.None;
    }
  }

  private class ListItemIndex
  {
    private Dictionary<int, ImbaseIndexesView.ItemIndex> _items = new Dictionary<int, ImbaseIndexesView.ItemIndex>();

    internal ImbaseIndexesView.ItemIndex this[int id]
    {
      get
      {
        return !this._items.ContainsKey(id) ? new ImbaseIndexesView.ItemIndex(-1, (DataGridViewRow) null) : this._items[id];
      }
    }

    internal List<int> GetIncludedIndexesIDs
    {
      get
      {
        return this._items.Values.Where<ImbaseIndexesView.ItemIndex>((System.Func<ImbaseIndexesView.ItemIndex, bool>) (item => item.Status != IndexesStatus.Removed)).Select<ImbaseIndexesView.ItemIndex, int>((System.Func<ImbaseIndexesView.ItemIndex, int>) (item => item.Id)).ToList<int>();
      }
    }

    internal bool HasChanges
    {
      get
      {
        return this._items.Values.Count > 0 && this._items.Values.FirstOrDefault<ImbaseIndexesView.ItemIndex>((System.Func<ImbaseIndexesView.ItemIndex, bool>) (x => x.HasChanges)) != null;
      }
    }

    internal void Add(int id, DataGridViewRow row, bool isUnique = false, IndexesStatus status = IndexesStatus.None)
    {
      this._items.Add(id, new ImbaseIndexesView.ItemIndex(id, row, isUnique, status));
    }

    internal void AfterSave()
    {
      int[] array = new int[this._items.Count];
      this._items.Keys.CopyTo(array, 0);
      foreach (int key in array)
      {
        if (this._items[key].Status == IndexesStatus.Removed)
          this._items.Remove(key);
        else
          this._items[key].AfterSave();
      }
    }

    internal void ChangeStatus(int id, IndexesStatus status)
    {
      if (id != -1)
      {
        if (status == IndexesStatus.Removed && this._items[id].Status == IndexesStatus.Added)
          this._items.Remove(id);
        else
          this._items[id].Status = status;
      }
      else
      {
        ImbaseIndexesView.ItemIndex[] array = new ImbaseIndexesView.ItemIndex[this._items.Values.Count];
        this._items.Values.CopyTo(array, 0);
        foreach (ImbaseIndexesView.ItemIndex itemIndex in array)
          this.ChangeStatus(itemIndex.Id, status);
      }
    }

    internal void Clear() => this._items.Clear();

    internal bool ContainsKey(int id) => this._items.ContainsKey(id);

    internal void SortItemsByStatus(
      Dictionary<int, bool> added,
      List<int> removed,
      Dictionary<int, bool> changed)
    {
      foreach (ImbaseIndexesView.ItemIndex itemIndex in this._items.Values)
      {
        if (itemIndex.Status == IndexesStatus.Added)
          added.Add(itemIndex.Id, itemIndex.Unique);
        else if (itemIndex.Status == IndexesStatus.Removed)
          removed.Add(itemIndex.Id);
        else if (itemIndex.Status == IndexesStatus.Changed)
          changed.Add(itemIndex.Id, itemIndex.Unique);
      }
    }
  }
}
