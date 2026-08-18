// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.ClearImbaseObjectsDlg
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.TableWizard;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class ClearImbaseObjectsDlg : Form
{
  private bool _lock;
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnAdd;
  private Button _btnClose;
  private Button _btnDel;
  private TabControl _tc;
  private TabPage _tpRefs;
  private TabPage _tpFolders;
  private ContextMenuStrip _contextMenu;
  private ImageList _imgList;
  private ToolStripMenuItem _miSelect;
  private ToolStripMenuItem _miClear;
  private ToolStripMenuItem _miAdd;
  private ToolStripMenuItem _miDel;
  private TabPage _tpTables;
  private ListView _lvFolders;
  private ColumnHeader _colID;
  private ColumnHeader _colText;
  private ListView _lvRefs;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ListView _lvTables;
  private ColumnHeader columnHeader3;
  private ColumnHeader columnHeader4;

  public ClearImbaseObjectsDlg()
  {
    this.InitializeComponent();
    this.ScanObjects(Intermech.Imbase.Consts.ImbaseTableRefTypeID, this._lvRefs);
    this.ScanObjects(Intermech.Imbase.Consts.ImbaseFolderTypeID, this._lvFolders);
    this.ScanObjects();
  }

  private void On_btnAdd_Click(object sender, EventArgs e)
  {
    List<long> longList = new List<long>();
    ListView listView = (ListView) null;
    ListView.CheckedListViewItemCollection viewItemCollection = (ListView.CheckedListViewItemCollection) null;
    switch (this._tc.SelectedTab.Name)
    {
      case "_tpFolders":
        listView = this._lvFolders;
        viewItemCollection = this._lvFolders.CheckedItems;
        break;
      case "_tpRefs":
        listView = this._lvRefs;
        viewItemCollection = this._lvRefs.CheckedItems;
        break;
      case "_tpTables":
        listView = this._lvTables;
        viewItemCollection = this._lvTables.CheckedItems;
        break;
    }
    if (viewItemCollection != null)
    {
      foreach (ListViewItem listViewItem in viewItemCollection)
        longList.Add(Convert.ToInt64(listViewItem.Text));
    }
    if (longList.Count <= 0)
      return;
    AdvancedServiceContainer nodesContext = new AdvancedServiceContainer();
    nodesContext.AddService(typeof (ImbaseDisableCatalogsComposition), (object) new ImbaseDisableCatalogsComposition(DisableImbaseCategory.Folder));
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase.Client_5"), string.Empty, (IDescriptor) new ImbaseRootNodeDescriptor(), (System.IServiceProvider) nodesContext, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    long num1 = numArray[0];
    bool flag = false;
    if (listView != this._lvTables)
    {
      List<long> relationIDs = new List<long>(longList.Count);
      List<long> projIDs = new List<long>(longList.Count);
      List<int> relTypeIDs = new List<int>(longList.Count);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545")));
        if (relationCollection != null)
        {
          foreach (long partObjectID in longList)
          {
            IDBRelation dbRelation = relationCollection.Create(num1, partObjectID, DateTime.Now);
            relationIDs.Add(dbRelation.RelationID);
            projIDs.Add(num1);
            relTypeIDs.Add(dbRelation.RelationType);
          }
          flag = true;
        }
      }
      if (relationIDs.Count > 0 && ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
        service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs));
    }
    else
    {
      try
      {
        using (ImbaseTableWizardForm imbaseTableWizardForm = new ImbaseTableWizardForm(num1, longList[0], true))
          flag = imbaseTableWizardForm.ShowDialog() == DialogResult.OK && imbaseTableWizardForm.ObjectID != 0L;
      }
      catch (NullCollectionException ex)
      {
        int num2 = (int) MessageBox.Show((IWin32Window) this, ex.Msg, ex.Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
    if (!flag)
      return;
    while (listView.CheckedItems.Count > 0)
      listView.Items.Remove(listView.CheckedItems[0]);
  }

  private void On_btnDel_Click(object sender, EventArgs e)
  {
    ListView sender1 = (ListView) null;
    switch (this._tc.SelectedTab.Name)
    {
      case "_tpFolders":
        sender1 = this._lvFolders;
        int imbaseFolderTypeId = Intermech.Imbase.Consts.ImbaseFolderTypeID;
        break;
      case "_tpRefs":
        sender1 = this._lvRefs;
        int imbaseTableRefTypeId = Intermech.Imbase.Consts.ImbaseTableRefTypeID;
        break;
      case "_tpTables":
        sender1 = this._lvTables;
        int imbaseTableTypeId = Intermech.Imbase.Consts.ImbaseTableTypeID;
        break;
    }
    if (sender1.CheckedItems.Count <= 0)
      return;
    string caption = LocalizationHolder.rm.GetString("Imbase_DeleteObjects");
    if (MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Imbase_DeleteObjects_Msg"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    List<long> objectIDs = new List<long>(sender1.CheckedItems.Count);
    sender1.BeginUpdate();
    this._lock = true;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        while (sender1.CheckedItems.Count > 0)
        {
          ListViewItem checkedItem = sender1.CheckedItems[0];
          long int64 = Convert.ToInt64(checkedItem.Text);
          sender1.Items.Remove(checkedItem);
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(int64, false);
          if (objectActualCopy != null)
          {
            objectActualCopy.Delete(0L);
            objectIDs.Add(int64);
          }
        }
      }
    }
    finally
    {
      this._lock = false;
      sender1.EndUpdate();
    }
    if (objectIDs.Count > 0 && ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
      service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs));
    if (sender1 != this._lvTables)
      this.ScanObjects();
    this.On_lv_ItemCheck((object) sender1, (ItemCheckEventArgs) null);
  }

  private void On_lv_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (this._lock)
      return;
    ListView listView = sender as ListView;
    if (e != null)
    {
      if (listView.CheckedItems.Count == 0 && e.NewValue == CheckState.Checked)
        this._miClear.Enabled = this._miAdd.Enabled = this._btnAdd.Enabled = this._miDel.Enabled = this._btnDel.Enabled = true;
      else if (listView.CheckedItems.Count == 1 && e.NewValue == CheckState.Unchecked)
        this._miClear.Enabled = this._miAdd.Enabled = this._btnAdd.Enabled = this._miDel.Enabled = this._btnDel.Enabled = false;
      else if (listView == this._lvTables)
        this._miAdd.Enabled = this._btnAdd.Enabled = listView.CheckedItems.Count == 2 && e.NewValue == CheckState.Unchecked;
      if (listView.CheckedItems.Count == listView.Items.Count && e.NewValue == CheckState.Unchecked)
      {
        this._miSelect.Enabled = true;
      }
      else
      {
        if (listView.CheckedItems.Count != listView.Items.Count - 1 || e.NewValue != CheckState.Checked)
          return;
        this._miSelect.Enabled = false;
      }
    }
    else
    {
      this._miClear.Enabled = this._miDel.Enabled = this._btnDel.Enabled = listView.CheckedItems.Count > 0;
      this._miSelect.Enabled = listView.Items.Count != 0 && listView.CheckedItems.Count != listView.Items.Count;
      this._miAdd.Enabled = this._btnAdd.Enabled = listView == this._lvTables ? listView.CheckedItems.Count == 1 : listView.CheckedItems.Count > 0;
    }
  }

  private void On_lv_SizeChanged(object sender, EventArgs e)
  {
    if (!(sender is ListView listView) || listView.Columns.Count != 2 || listView.Columns[1] == null)
      return;
    listView.Columns[1].Width = -2;
  }

  private void On_tc_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.On_lv_ItemCheck(this._tc.SelectedTab.Name == "_tpFolders" ? (object) this._lvFolders : (this._tc.SelectedTab.Name == "_tpRefs" ? (object) this._lvRefs : (object) this._lvTables), (ItemCheckEventArgs) null);
  }

  private void OnSelectItems(object sender, EventArgs e)
  {
    bool flag = Convert.ToInt16((sender as ToolStripMenuItem).Tag) == (short) 0;
    this._lock = true;
    ListView sender1 = this._tc.SelectedTab.Name == "_tpFolders" ? this._lvFolders : (this._tc.SelectedTab.Name == "_tpRefs" ? this._lvRefs : this._lvTables);
    foreach (ListViewItem listViewItem in sender1.Items)
      listViewItem.Checked = flag;
    this._lock = false;
    this.On_lv_ItemCheck((object) sender1, (ItemCheckEventArgs) null);
  }

  private void CreateItems(DataTable dt, ListView lv)
  {
    if (dt == null)
      return;
    lv.BeginUpdate();
    lv.Sorting = SortOrder.None;
    try
    {
      this._lvTables.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
      {
        object obj1 = row[0];
        if (obj1 != null)
        {
          long result = 0;
          if (long.TryParse(obj1.ToString(), out result))
          {
            object obj2 = row[1];
            lv.Items.Add(new ListViewItem(result.ToString())
            {
              SubItems = {
                obj2 != null ? obj2.ToString() : string.Empty
              }
            });
          }
        }
      }
    }
    finally
    {
      lv.Sort();
      lv.EndUpdate();
    }
  }

  private void ScanObjects()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
      {
        this.CreateItems(customService.GetUnlinkedTables(sessionKeeper.Session.SessionGUID), this._lvTables);
      }
      else
      {
        string caption = LocalizationHolder.rm.GetString("Imbase.Client_1136");
        int num = (int) MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Imbase_NullImbaseServer"), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }
  }

  private void ScanObjects(int typeID, ListView lv)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(typeID);
      if (objectCollection == null)
        return;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(-2, RelationalOperators.NotEntersInType, (object) Intermech.Imbase.Consts.ImbaseFolderTypeID, LogicalOperators.AND, 0, false),
        new ConditionStructure(-2, RelationalOperators.NotEntersInType, (object) Intermech.Imbase.Consts.ImbaseCatalogTypeID, LogicalOperators.NONE, 0, false)
      }, new object[2]{ (object) -2, (object) -50 });
      this.CreateItems(objectCollection.Select(paramSet), lv);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ClearImbaseObjectsDlg));
    this._pnlBottom = new Panel();
    this._btnAdd = new Button();
    this._btnClose = new Button();
    this._btnDel = new Button();
    this._tc = new TabControl();
    this._tpFolders = new TabPage();
    this._lvFolders = new ListView();
    this._colID = new ColumnHeader();
    this._colText = new ColumnHeader();
    this._contextMenu = new ContextMenuStrip(this.components);
    this._miSelect = new ToolStripMenuItem();
    this._miClear = new ToolStripMenuItem();
    this._miAdd = new ToolStripMenuItem();
    this._miDel = new ToolStripMenuItem();
    this._tpRefs = new TabPage();
    this._lvRefs = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this._tpTables = new TabPage();
    this._lvTables = new ListView();
    this.columnHeader3 = new ColumnHeader();
    this.columnHeader4 = new ColumnHeader();
    this._imgList = new ImageList(this.components);
    this._pnlBottom.SuspendLayout();
    this._tc.SuspendLayout();
    this._tpFolders.SuspendLayout();
    this._contextMenu.SuspendLayout();
    this._tpRefs.SuspendLayout();
    this._tpTables.SuspendLayout();
    this.SuspendLayout();
    this._pnlBottom.Controls.Add((Control) this._btnAdd);
    this._pnlBottom.Controls.Add((Control) this._btnClose);
    this._pnlBottom.Controls.Add((Control) this._btnDel);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnAdd, "_btnAdd");
    this._btnAdd.Name = "_btnAdd";
    this._btnAdd.UseVisualStyleBackColor = true;
    this._btnAdd.Click += new EventHandler(this.On_btnAdd_Click);
    componentResourceManager.ApplyResources((object) this._btnClose, "_btnClose");
    this._btnClose.DialogResult = DialogResult.Cancel;
    this._btnClose.Name = "_btnClose";
    this._btnClose.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnDel, "_btnDel");
    this._btnDel.Name = "_btnDel";
    this._btnDel.UseVisualStyleBackColor = true;
    this._btnDel.Click += new EventHandler(this.On_btnDel_Click);
    this._tc.Controls.Add((Control) this._tpFolders);
    this._tc.Controls.Add((Control) this._tpRefs);
    this._tc.Controls.Add((Control) this._tpTables);
    componentResourceManager.ApplyResources((object) this._tc, "_tc");
    this._tc.ImageList = this._imgList;
    this._tc.Name = "_tc";
    this._tc.SelectedIndex = 0;
    this._tc.SelectedIndexChanged += new EventHandler(this.On_tc_SelectedIndexChanged);
    this._tpFolders.Controls.Add((Control) this._lvFolders);
    componentResourceManager.ApplyResources((object) this._tpFolders, "_tpFolders");
    this._tpFolders.Name = "_tpFolders";
    this._tpFolders.UseVisualStyleBackColor = true;
    this._lvFolders.CheckBoxes = true;
    this._lvFolders.Columns.AddRange(new ColumnHeader[2]
    {
      this._colID,
      this._colText
    });
    this._lvFolders.ContextMenuStrip = this._contextMenu;
    componentResourceManager.ApplyResources((object) this._lvFolders, "_lvFolders");
    this._lvFolders.FullRowSelect = true;
    this._lvFolders.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._lvFolders.HideSelection = false;
    this._lvFolders.Name = "_lvFolders";
    this._lvFolders.Sorting = SortOrder.Ascending;
    this._lvFolders.UseCompatibleStateImageBehavior = false;
    this._lvFolders.View = View.Details;
    this._lvFolders.ItemCheck += new ItemCheckEventHandler(this.On_lv_ItemCheck);
    this._lvFolders.SizeChanged += new EventHandler(this.On_lv_SizeChanged);
    componentResourceManager.ApplyResources((object) this._colID, "_colID");
    componentResourceManager.ApplyResources((object) this._colText, "_colText");
    this._contextMenu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._miSelect,
      (ToolStripItem) this._miClear,
      (ToolStripItem) this._miAdd,
      (ToolStripItem) this._miDel
    });
    this._contextMenu.Name = "_contextMenu";
    componentResourceManager.ApplyResources((object) this._contextMenu, "_contextMenu");
    this._miSelect.Name = "_miSelect";
    componentResourceManager.ApplyResources((object) this._miSelect, "_miSelect");
    this._miSelect.Tag = (object) "0";
    this._miSelect.Click += new EventHandler(this.OnSelectItems);
    componentResourceManager.ApplyResources((object) this._miClear, "_miClear");
    this._miClear.Name = "_miClear";
    this._miClear.Tag = (object) "1";
    this._miClear.Click += new EventHandler(this.OnSelectItems);
    componentResourceManager.ApplyResources((object) this._miAdd, "_miAdd");
    this._miAdd.Name = "_miAdd";
    this._miAdd.Click += new EventHandler(this.On_btnAdd_Click);
    componentResourceManager.ApplyResources((object) this._miDel, "_miDel");
    this._miDel.Name = "_miDel";
    this._miDel.Click += new EventHandler(this.On_btnDel_Click);
    this._tpRefs.Controls.Add((Control) this._lvRefs);
    componentResourceManager.ApplyResources((object) this._tpRefs, "_tpRefs");
    this._tpRefs.Name = "_tpRefs";
    this._tpRefs.UseVisualStyleBackColor = true;
    this._lvRefs.CheckBoxes = true;
    this._lvRefs.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this._lvRefs.ContextMenuStrip = this._contextMenu;
    componentResourceManager.ApplyResources((object) this._lvRefs, "_lvRefs");
    this._lvRefs.FullRowSelect = true;
    this._lvRefs.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._lvRefs.HideSelection = false;
    this._lvRefs.Name = "_lvRefs";
    this._lvRefs.Sorting = SortOrder.Ascending;
    this._lvRefs.UseCompatibleStateImageBehavior = false;
    this._lvRefs.View = View.Details;
    this._lvRefs.ItemCheck += new ItemCheckEventHandler(this.On_lv_ItemCheck);
    this._lvRefs.SizeChanged += new EventHandler(this.On_lv_SizeChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    this._tpTables.Controls.Add((Control) this._lvTables);
    componentResourceManager.ApplyResources((object) this._tpTables, "_tpTables");
    this._tpTables.Name = "_tpTables";
    this._tpTables.UseVisualStyleBackColor = true;
    this._lvTables.CheckBoxes = true;
    this._lvTables.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader3,
      this.columnHeader4
    });
    this._lvTables.ContextMenuStrip = this._contextMenu;
    componentResourceManager.ApplyResources((object) this._lvTables, "_lvTables");
    this._lvTables.FullRowSelect = true;
    this._lvTables.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._lvTables.HideSelection = false;
    this._lvTables.MultiSelect = false;
    this._lvTables.Name = "_lvTables";
    this._lvTables.Sorting = SortOrder.Ascending;
    this._lvTables.UseCompatibleStateImageBehavior = false;
    this._lvTables.View = View.Details;
    this._lvTables.ItemCheck += new ItemCheckEventHandler(this.On_lv_ItemCheck);
    this._lvTables.SizeChanged += new EventHandler(this.On_lv_SizeChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    componentResourceManager.ApplyResources((object) this.columnHeader4, "columnHeader4");
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "ImbaseFolders.ico");
    this._imgList.Images.SetKeyName(1, "ImbaseTableRefs.ico");
    this._imgList.Images.SetKeyName(2, "ImbaseTables.ico");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnClose;
    this.Controls.Add((Control) this._tc);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (ClearImbaseObjectsDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._pnlBottom.ResumeLayout(false);
    this._tc.ResumeLayout(false);
    this._tpFolders.ResumeLayout(false);
    this._contextMenu.ResumeLayout(false);
    this._tpRefs.ResumeLayout(false);
    this._tpTables.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
