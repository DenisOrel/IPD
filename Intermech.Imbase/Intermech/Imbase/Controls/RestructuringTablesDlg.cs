// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.RestructuringTablesDlg
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class RestructuringTablesDlg : Form
{
  private TreeBuilder _treeBuilder = new TreeBuilder();
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOk;
  private SplitContainer _spltContainer1;
  private TreeView _trv;
  private SplitContainer splitContainer2;
  private ListView _lv;
  private ColumnHeader colName;
  private PropertyGrid _pgStructure;
  private Panel pnlChangeButtons;
  private Button _btnDel;
  private Button _btnAdd;

  public List<RestructuringTablesAttrSettings> Settings
  {
    get
    {
      List<RestructuringTablesAttrSettings> settings = new List<RestructuringTablesAttrSettings>(this._lv.Items.Count);
      foreach (ListViewItem listViewItem in this._lv.Items)
        settings.Add((listViewItem.Tag as RestructuringPropGridDescriptor).Settings);
      return settings;
    }
  }

  public long SourceID
  {
    get => this._trv.SelectedNode == null ? 0L : (this._trv.SelectedNode.Tag as NodeInfo).ObjectId;
  }

  public RestructuringTablesDlg()
  {
    this.InitializeComponent();
    this._lv.SmallImageList = Statics.IconSrv.ImageList;
    this._treeBuilder.TreeView = this._trv;
    this.LoadImbaseTree();
  }

  private void On_btnAdd_Click(object sender, EventArgs e)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      attributesSelectDlg.ShowCreateAttrBtn = true;
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) new ForbiddenAttrs(this.GetAddedAttrsIDs());
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[7]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftExternalLink,
        FieldTypes.ftPassword,
        FieldTypes.ftAutoInc
      });
      if (attributesSelectDlg.ShowDialog((IWin32Window) this) != DialogResult.OK || attributesSelectDlg.SelectedAttributesGuid.Count <= 0)
        return;
      bool flag = false;
      foreach (Guid guid in attributesSelectDlg.SelectedAttributesGuid)
      {
        if (this.AddItem(guid) != null)
          flag = true;
      }
      if (!flag)
        return;
      if (this._lv.Items.Count > 0)
      {
        this._lv.SelectedItems.Clear();
        ListViewItem listViewItem = this._lv.Items[this._lv.Items.Count - 1];
        listViewItem.Selected = true;
        this._pgStructure.SelectedObject = listViewItem.Tag;
      }
      else
        this._pgStructure.SelectedObject = (object) null;
    }
  }

  private void On_btnDelete_Click(object sender, EventArgs e)
  {
    if (this._lv.SelectedItems.Count <= 0)
      return;
    string caption = LocalizationHolder.rm.GetString("Imbase.AttributeDeleting");
    if (MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Imbase.AttributeDeleting.QuestionMessage"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    foreach (ListViewItem selectedItem in this._lv.SelectedItems)
      this._lv.Items.Remove(selectedItem);
    if (this._lv.Items.Count == 0)
    {
      this._pgStructure.SelectedObject = (object) null;
    }
    else
    {
      if (this._lv.SelectedItems.Count != 0)
        return;
      this._lv.Items[this._lv.Items.Count - 1].Selected = true;
    }
  }

  private void On_lv_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnDel.Enabled = this._lv.SelectedItems.Count > 0;
    this._pgStructure.SelectedObject = !this._btnDel.Enabled ? (object) null : (this._lv.SelectedItems.Count == 1 ? this._lv.SelectedItems[0].Tag : (object) null);
    this._btnOk.Enabled = this._trv.SelectedNode != null && this._btnDel.Enabled;
  }

  private void On_lv_SizeChanged(object sender, EventArgs e)
  {
    if (this._lv == null || this._lv.Columns.Count <= 0 || this._lv.Columns[0] == null)
      return;
    this._lv.Columns[0].Width = -2;
  }

  private void On_trv_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this._btnOk.Enabled = this._trv.SelectedNode != null && this._lv.SelectedItems.Count > 0;
  }

  private ListViewItem AddItem(Guid guid)
  {
    ListViewItem listViewItem = (ListViewItem) null;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(guid);
        if (attributeType != null)
        {
          object defaultValue = (object) null;
          if (attributeType.DefaultValue != DBNull.Value && attributeType.DefaultValue != null)
          {
            DataTable possibleValues = attributeType.GetPossibleValues();
            if (possibleValues != null && possibleValues.Rows.Count > 0)
            {
              foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
              {
                if (string.IsNullOrEmpty(Convert.ToString(row["F_DESCRIPTION"])))
                  row["F_DESCRIPTION"] = row[attributeType.PossibleValueFieldName];
              }
              DataRow[] dataRowArray = possibleValues.Select($"{attributeType.PossibleValueFieldName}='{attributeType.DefaultValue}'");
              if (dataRowArray.Length != 0)
                defaultValue = dataRowArray[0][attributeType.PossibleValueFieldName];
            }
            else
              defaultValue = (object) attributeType.DefaultValueDescription;
          }
          else if (attributeType.AttributeType == FieldTypes.ftBoolean)
            defaultValue = (object) false;
          int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.AttributeType);
          listViewItem = this._lv.Items.Add(new ListViewItem(attributeType.Name, imageIndex)
          {
            Name = attributeType.PropertiesStructure.AttributeGuid.ToString(),
            Tag = (object) new RestructuringPropGridDescriptor((Control) this, attributeType, 2, 0, defaultValue, Convert.ToInt32((object) attributeType.Options), string.Empty)
          });
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    return listViewItem;
  }

  private List<int> GetAddedAttrsIDs()
  {
    List<int> addedAttrsIds = new List<int>(this._lv.Items.Count);
    foreach (ListViewItem listViewItem in this._lv.Items)
      addedAttrsIds.Add((listViewItem.Tag as RestructuringPropGridDescriptor).Settings.AttributeID);
    return addedAttrsIds;
  }

  private void LoadImbaseTree()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        return;
      long[] catalogsList = customService.GetCatalogsList(sessionKeeper.Session.SessionGUID);
      if (catalogsList == null || catalogsList.Length == 0)
        return;
      DataTable foldersForObjects = customService.GetFoldersForObjects(sessionKeeper.Session.SessionGUID, catalogsList, (long[]) null);
      if (foldersForObjects == null)
        return;
      this._trv.BeginUpdate();
      try
      {
        this._treeBuilder.CreateTree(foldersForObjects);
      }
      finally
      {
        this._trv.EndUpdate();
      }
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._treeBuilder != null)
      {
        this._treeBuilder.TreeView = (TreeView) null;
        this._treeBuilder.Dispose();
        this._treeBuilder = (TreeBuilder) null;
      }
      this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RestructuringTablesDlg));
    this._btnCancel = new Button();
    this._btnOk = new Button();
    this._spltContainer1 = new SplitContainer();
    this._trv = new TreeView();
    this.splitContainer2 = new SplitContainer();
    this._lv = new ListView();
    this.colName = new ColumnHeader();
    this._pgStructure = new PropertyGrid();
    this.pnlChangeButtons = new Panel();
    this._btnDel = new Button();
    this._btnAdd = new Button();
    this._spltContainer1.BeginInit();
    this._spltContainer1.Panel1.SuspendLayout();
    this._spltContainer1.Panel2.SuspendLayout();
    this._spltContainer1.SuspendLayout();
    this.splitContainer2.BeginInit();
    this.splitContainer2.Panel1.SuspendLayout();
    this.splitContainer2.Panel2.SuspendLayout();
    this.splitContainer2.SuspendLayout();
    this.pnlChangeButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOk, "_btnOk");
    this._btnOk.DialogResult = DialogResult.OK;
    this._btnOk.Name = "_btnOk";
    this._btnOk.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._spltContainer1, "_spltContainer1");
    this._spltContainer1.Name = "_spltContainer1";
    this._spltContainer1.Panel1.Controls.Add((Control) this._trv);
    this._spltContainer1.Panel2.Controls.Add((Control) this.splitContainer2);
    componentResourceManager.ApplyResources((object) this._trv, "_trv");
    this._trv.FullRowSelect = true;
    this._trv.HideSelection = false;
    this._trv.Name = "_trv";
    this._trv.AfterSelect += new TreeViewEventHandler(this.On_trv_AfterSelect);
    componentResourceManager.ApplyResources((object) this.splitContainer2, "splitContainer2");
    this.splitContainer2.Name = "splitContainer2";
    this.splitContainer2.Panel1.Controls.Add((Control) this._lv);
    this.splitContainer2.Panel2.Controls.Add((Control) this._pgStructure);
    this._lv.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName
    });
    componentResourceManager.ApplyResources((object) this._lv, "_lv");
    this._lv.FullRowSelect = true;
    this._lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._lv.HideSelection = false;
    this._lv.Name = "_lv";
    this._lv.Sorting = SortOrder.Ascending;
    this._lv.UseCompatibleStateImageBehavior = false;
    this._lv.View = View.Details;
    this._lv.SelectedIndexChanged += new EventHandler(this.On_lv_SelectedIndexChanged);
    this._lv.SizeChanged += new EventHandler(this.On_lv_SizeChanged);
    componentResourceManager.ApplyResources((object) this.colName, "colName");
    componentResourceManager.ApplyResources((object) this._pgStructure, "_pgStructure");
    this._pgStructure.Name = "_pgStructure";
    this._pgStructure.ToolbarVisible = false;
    this.pnlChangeButtons.Controls.Add((Control) this._btnOk);
    this.pnlChangeButtons.Controls.Add((Control) this._btnCancel);
    this.pnlChangeButtons.Controls.Add((Control) this._btnDel);
    this.pnlChangeButtons.Controls.Add((Control) this._btnAdd);
    componentResourceManager.ApplyResources((object) this.pnlChangeButtons, "pnlChangeButtons");
    this.pnlChangeButtons.Name = "pnlChangeButtons";
    componentResourceManager.ApplyResources((object) this._btnDel, "_btnDel");
    this._btnDel.Name = "_btnDel";
    this._btnDel.UseVisualStyleBackColor = true;
    this._btnDel.Click += new EventHandler(this.On_btnDelete_Click);
    componentResourceManager.ApplyResources((object) this._btnAdd, "_btnAdd");
    this._btnAdd.Name = "_btnAdd";
    this._btnAdd.UseVisualStyleBackColor = true;
    this._btnAdd.Click += new EventHandler(this.On_btnAdd_Click);
    this.AcceptButton = (IButtonControl) this._btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._spltContainer1);
    this.Controls.Add((Control) this.pnlChangeButtons);
    this.DoubleBuffered = true;
    this.Name = nameof (RestructuringTablesDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._spltContainer1.Panel1.ResumeLayout(false);
    this._spltContainer1.Panel2.ResumeLayout(false);
    this._spltContainer1.EndInit();
    this._spltContainer1.ResumeLayout(false);
    this.splitContainer2.Panel1.ResumeLayout(false);
    this.splitContainer2.Panel2.ResumeLayout(false);
    this.splitContainer2.EndInit();
    this.splitContainer2.ResumeLayout(false);
    this.pnlChangeButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
