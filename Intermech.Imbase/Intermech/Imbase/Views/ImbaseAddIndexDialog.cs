// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseAddIndexDialog
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class ImbaseAddIndexDialog : Form
{
  private long _iNodeID = -1;
  private Dictionary<long, List<ListViewItem>> _attrsDict = new Dictionary<long, List<ListViewItem>>();
  private List<int> _excludedAttrs = new List<int>();
  private List<int> _selectedAttrIDs = new List<int>();
  private IContainer components;
  private Panel _pnlResultButtons;
  private Button _btnCancel;
  private Button _btnOk;
  private SplitContainer _splAttributes;
  private TreeView _tvCatalogs;
  private ListView _lvAttrs;
  private ColumnHeader colName;
  private Button _btnAllAttrs;
  private ListView _lvSelected;
  private ColumnHeader columnHeader1;
  private TableLayoutPanel _pnlSelectButtons;
  private Button _btnSelect;
  private Button _btnDel;

  public List<int> SelectedAttrs => this._selectedAttrIDs;

  public ImbaseAddIndexDialog(long iNodeID, List<int> excludedAttrs)
  {
    this.InitializeComponent();
    this._iNodeID = iNodeID;
    this._excludedAttrs = excludedAttrs;
    this._tvCatalogs.ImageList = TreeBuilder.ImageList;
    this._lvAttrs.SmallImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this._lvSelected.SmallImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
  }

  private void On_btnAdd_Click(object sender, EventArgs e)
  {
    if (this._lvAttrs.SelectedItems.Count > 0)
    {
      this._lvAttrs.BeginUpdate();
      this._lvSelected.BeginUpdate();
      try
      {
        foreach (ListViewItem listViewItem in this._lvAttrs.SelectedItems.Cast<ListViewItem>().ToList<ListViewItem>())
        {
          int int32 = Convert.ToInt32(listViewItem.Name);
          this._lvAttrs.Items.Remove(listViewItem);
          if (!this._selectedAttrIDs.Contains(int32))
          {
            this._lvSelected.Items.Add(listViewItem);
            this._selectedAttrIDs.Add(int32);
          }
        }
        this._btnOk.Enabled = true;
      }
      finally
      {
        this._lvAttrs.EndUpdate();
        this._lvSelected.EndUpdate();
      }
    }
    this.UpdateOkButtonState();
  }

  private void On_btnDel_Click(object sender, EventArgs e)
  {
    if (this._lvSelected.SelectedItems.Count > 0)
    {
      this._lvAttrs.BeginUpdate();
      this._lvSelected.BeginUpdate();
      try
      {
        long key = 0;
        int num = -1;
        TreeNode selectedNode = this._tvCatalogs.SelectedNode;
        if (selectedNode != null)
        {
          NodeInfo tag = selectedNode.Tag as NodeInfo;
          key = tag.ObjectId;
          num = tag.TypeId;
        }
        foreach (ListViewItem listViewItem in this._lvSelected.SelectedItems.Cast<ListViewItem>().ToList<ListViewItem>())
        {
          ListViewItem item = listViewItem;
          int int32 = Convert.ToInt32(item.Name);
          this._lvSelected.Items.Remove(item);
          this._selectedAttrIDs.Remove(int32);
          if (num == Intermech.Imbase.Consts.ImbaseTableRefTypeID && key != 0L && this._attrsDict[key].FirstOrDefault<ListViewItem>((System.Func<ListViewItem, bool>) (x => x.Name == item.Name)) != null)
            this._lvAttrs.Items.Add(item);
        }
      }
      finally
      {
        this._lvAttrs.EndUpdate();
        this._lvSelected.EndUpdate();
      }
    }
    this.UpdateOkButtonState();
  }

  private void On_showAllAttrs_Click(object sender, EventArgs e)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      attributesSelectDlg.LoadAttrDialogForObjectsTypes(new List<int>()
      {
        Intermech.Imbase.Consts.ImbaseCatalogTypeID
      });
      attributesSelectDlg.AllowedAttrsTypesFilter = new List<FieldTypes>()
      {
        FieldTypes.ftString,
        FieldTypes.ftInteger,
        FieldTypes.ftDouble,
        FieldTypes.ftDateTime,
        FieldTypes.ftMemo,
        FieldTypes.ftBoolean,
        FieldTypes.ftMeasured,
        FieldTypes.ftGuid
      };
      if (attributesSelectDlg.ShowDialog() == DialogResult.Cancel || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      foreach (Guid attrTypeGuid in attributesSelectDlg.SelectedAttributesGuid)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeGuid);
        if (attributeType != null && !this._selectedAttrIDs.Contains(attributeType.AttributeID))
        {
          int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.FieldType);
          this._lvSelected.Items.Add(new ListViewItem(attributeType.Name, imageIndex)
          {
            Name = Convert.ToString(attributeType.AttributeID)
          });
          this._selectedAttrIDs.Add(attributeType.AttributeID);
        }
      }
      this._btnOk.Enabled = true;
    }
  }

  private void On_lvAttrs_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnSelect.Enabled = this._lvAttrs.Items.Count > 0 && this._lvAttrs.SelectedItems.Count > 0;
  }

  private void On_lvSelected_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._btnDel.Enabled = this._lvSelected.Items.Count > 0 && this._lvSelected.SelectedItems.Count > 0;
  }

  private void Onlv_SizeChanged(object sender, EventArgs e)
  {
    try
    {
      this._lvAttrs.Columns[0].Width = -2;
      this._lvSelected.Columns[0].Width = -2;
    }
    catch
    {
    }
  }

  private void On_tvCatalogs_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this._lvAttrs.BeginUpdate();
    try
    {
      this._lvAttrs.Items.Clear();
      this._btnOk.Enabled = this._lvSelected.Items.Count > 0;
      TreeNode node = e.Node;
      if (node != null)
      {
        NodeInfo tag = node.Tag as NodeInfo;
        if (tag.TypeId == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        {
          List<ListViewItem> items;
          if (!this._attrsDict.ContainsKey(tag.ObjectId))
          {
            items = this.GetItems(tag.ObjectId, false);
            this._attrsDict.Add(tag.ObjectId, items);
          }
          else
            items = this._attrsDict[tag.ObjectId];
          if (items != null)
          {
            if (items.Count > 0)
            {
              ListViewItem[] array = items.Where<ListViewItem>((System.Func<ListViewItem, bool>) (x => !this._selectedAttrIDs.Contains(Convert.ToInt32(x.Name)))).ToArray<ListViewItem>();
              if (array.Length != 0)
                this._lvAttrs.Items.AddRange(array);
            }
          }
        }
      }
    }
    finally
    {
      this._lvAttrs.EndUpdate();
    }
    this._btnSelect.Enabled = false;
  }

  private void BuildTree(DataTable dt)
  {
    this._tvCatalogs.BeginUpdate();
    try
    {
      this._tvCatalogs.Nodes.Clear();
      if (dt == null || dt.Rows.Count <= 0)
        return;
      DataView dataView = new DataView(dt)
      {
        Sort = "F_PATH ASC"
      };
      int count = dataView.Count;
      Hashtable hashtable = new Hashtable(count);
      int columnIndex1 = dt.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex2 = dt.Columns.IndexOf("CAPTION");
      int columnIndex3 = dt.Columns.IndexOf("F_OBJECT_TYPE");
      int columnIndex4 = dt.Columns.IndexOf("F_PATH");
      int columnIndex5 = dt.Columns.IndexOf("F_SORT");
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      for (int recordIndex = 0; recordIndex < count; ++recordIndex)
      {
        DataRow row = dataView[recordIndex].Row;
        NodeInfo nodeInfo = new NodeInfo(Convert.ToInt64(row[columnIndex1]), Convert.ToInt32(row[columnIndex3]));
        if (row[columnIndex5] != DBNull.Value)
          nodeInfo.Order = Convert.ToInt32(row[columnIndex5]);
        int iconIndex = TreeBuilder.GetIconIndex(Convert.ToInt32(row[columnIndex3]));
        TreeNode node = new TreeNode(Convert.ToString(row[columnIndex2]), 0, 1)
        {
          Tag = (object) nodeInfo,
          SelectedImageIndex = iconIndex,
          ImageIndex = iconIndex
        };
        string key1 = Convert.ToString(row[columnIndex4]);
        int length = key1.Length - 2;
        string key2 = key1.Substring(0, length);
        if (hashtable[(object) key2] is TreeNode treeNode)
        {
          treeNode.Nodes.Add(node);
          treeNode.Expand();
        }
        else
          this._tvCatalogs.Nodes.Add(node);
        if (!hashtable.Contains((object) key1))
          hashtable.Add((object) key1, (object) node);
      }
      this._tvCatalogs.Sort();
      foreach (TreeNode node in this._tvCatalogs.Nodes)
        node.Collapse(false);
      if (this._tvCatalogs.Nodes.Count != 1)
        return;
      this._tvCatalogs.Nodes[0].Expand();
    }
    finally
    {
      this._tvCatalogs.EndUpdate();
    }
  }

  private List<ListViewItem> GetItems(long linkID, bool getLinkAttrs)
  {
    List<ListViewItem> items = new List<ListViewItem>();
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long tableReference = TableLoadHelper.GetTableReference(sessionKeeper.Session, linkID);
      if (tableReference != 0L)
      {
        DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, tableReference, true);
        if (tables != null && tables.Tables.Count > 0)
        {
          if (!this._excludedAttrs.Contains(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID))
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID);
            int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.FieldType);
            items.Add(new ListViewItem(LocalizationHolder.rm.GetString("Imbase_AttrGUIDRecord_Name"), imageIndex)
            {
              Name = Convert.ToString(attributeType.AttributeID)
            });
            intList.Add(attributeType.AttributeID);
          }
          foreach (DataRow row in (InternalDataCollectionBase) tables.Tables["IMS_ATTR_TYPES"].Rows)
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(Convert.ToString(row["F_ATTRIBUTE_GUID"])));
            if (attributeType != null && !this._excludedAttrs.Contains(attributeType.AttributeID))
            {
              int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.FieldType);
              items.Add(new ListViewItem(attributeType.Name, imageIndex)
              {
                Name = Convert.ToString(attributeType.AttributeID)
              });
              intList.Add(attributeType.AttributeID);
            }
          }
        }
      }
      if (getLinkAttrs)
      {
        List<FieldTypes> fieldTypesList = new List<FieldTypes>()
        {
          FieldTypes.ftString,
          FieldTypes.ftInteger,
          FieldTypes.ftDouble,
          FieldTypes.ftDateTime,
          FieldTypes.ftMemo,
          FieldTypes.ftBoolean,
          FieldTypes.ftMeasured,
          FieldTypes.ftGuid
        };
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(linkID, false);
        if (objectActualCopy != null)
        {
          GetAttributeValuesModes modes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckVisibility;
          foreach (AttributeValues attributesValue in objectActualCopy.GetAttributesValues(modes))
          {
            int attributeId = attributesValue.AttributeID;
            if (!this._excludedAttrs.Contains(attributeId) && fieldTypesList.Contains(attributesValue.AttributeType) && !intList.Contains(attributeId) && attributeId != Intermech.Imbase.Consts.ClassifFolderKeyAttId && attributeId != Intermech.Imbase.Consts.ImbaseTemplateAttID)
            {
              int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributesValue.AttributeType);
              items.Add(new ListViewItem(attributesValue.AttributeName, imageIndex)
              {
                Name = Convert.ToString(attributesValue.AttributeID)
              });
              intList.Add(attributesValue.AttributeID);
            }
          }
        }
      }
    }
    return items;
  }

  private void UpdateOkButtonState() => this._btnOk.Enabled = this._lvSelected.Items.Count > 0;

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IFolderFilterService)) is IFolderFilterService customService))
        return;
      this.BuildTree(customService.LoadAllCatalogTable(sessionKeeper.Session.SessionGUID, this._iNodeID, false));
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseAddIndexDialog));
    this._splAttributes = new SplitContainer();
    this._tvCatalogs = new TreeView();
    this._lvSelected = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this._pnlSelectButtons = new TableLayoutPanel();
    this._btnSelect = new Button();
    this._btnDel = new Button();
    this._lvAttrs = new ListView();
    this.colName = new ColumnHeader();
    this._pnlResultButtons = new Panel();
    this._btnAllAttrs = new Button();
    this._btnCancel = new Button();
    this._btnOk = new Button();
    this._splAttributes.BeginInit();
    this._splAttributes.Panel1.SuspendLayout();
    this._splAttributes.Panel2.SuspendLayout();
    this._splAttributes.SuspendLayout();
    this._pnlSelectButtons.SuspendLayout();
    this._pnlResultButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._splAttributes, "_splAttributes");
    this._splAttributes.Name = "_splAttributes";
    this._splAttributes.Panel1.Controls.Add((Control) this._tvCatalogs);
    this._splAttributes.Panel2.Controls.Add((Control) this._lvSelected);
    this._splAttributes.Panel2.Controls.Add((Control) this._pnlSelectButtons);
    this._splAttributes.Panel2.Controls.Add((Control) this._lvAttrs);
    componentResourceManager.ApplyResources((object) this._tvCatalogs, "_tvCatalogs");
    this._tvCatalogs.HideSelection = false;
    this._tvCatalogs.ItemHeight = 18;
    this._tvCatalogs.Name = "_tvCatalogs";
    this._tvCatalogs.AfterSelect += new TreeViewEventHandler(this.On_tvCatalogs_AfterSelect);
    this._lvSelected.Activation = ItemActivation.OneClick;
    this._lvSelected.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    componentResourceManager.ApplyResources((object) this._lvSelected, "_lvSelected");
    this._lvSelected.HideSelection = false;
    this._lvSelected.Name = "_lvSelected";
    this._lvSelected.UseCompatibleStateImageBehavior = false;
    this._lvSelected.View = View.Details;
    this._lvSelected.SelectedIndexChanged += new EventHandler(this.On_lvSelected_SelectedIndexChanged);
    this._lvSelected.SizeChanged += new EventHandler(this.Onlv_SizeChanged);
    this._lvSelected.DoubleClick += new EventHandler(this.On_btnDel_Click);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this._pnlSelectButtons, "_pnlSelectButtons");
    this._pnlSelectButtons.Controls.Add((Control) this._btnSelect, 1, 0);
    this._pnlSelectButtons.Controls.Add((Control) this._btnDel, 2, 0);
    this._pnlSelectButtons.Name = "_pnlSelectButtons";
    componentResourceManager.ApplyResources((object) this._btnSelect, "_btnSelect");
    this._btnSelect.Name = "_btnSelect";
    this._btnSelect.UseVisualStyleBackColor = true;
    this._btnSelect.Click += new EventHandler(this.On_btnAdd_Click);
    componentResourceManager.ApplyResources((object) this._btnDel, "_btnDel");
    this._btnDel.Name = "_btnDel";
    this._btnDel.UseVisualStyleBackColor = true;
    this._btnDel.Click += new EventHandler(this.On_btnDel_Click);
    this._lvAttrs.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName
    });
    componentResourceManager.ApplyResources((object) this._lvAttrs, "_lvAttrs");
    this._lvAttrs.HideSelection = false;
    this._lvAttrs.Name = "_lvAttrs";
    this._lvAttrs.UseCompatibleStateImageBehavior = false;
    this._lvAttrs.View = View.Details;
    this._lvAttrs.SelectedIndexChanged += new EventHandler(this.On_lvAttrs_SelectedIndexChanged);
    this._lvAttrs.SizeChanged += new EventHandler(this.Onlv_SizeChanged);
    this._lvAttrs.DoubleClick += new EventHandler(this.On_btnAdd_Click);
    componentResourceManager.ApplyResources((object) this.colName, "colName");
    this._pnlResultButtons.Controls.Add((Control) this._btnAllAttrs);
    this._pnlResultButtons.Controls.Add((Control) this._btnCancel);
    this._pnlResultButtons.Controls.Add((Control) this._btnOk);
    componentResourceManager.ApplyResources((object) this._pnlResultButtons, "_pnlResultButtons");
    this._pnlResultButtons.Name = "_pnlResultButtons";
    componentResourceManager.ApplyResources((object) this._btnAllAttrs, "_btnAllAttrs");
    this._btnAllAttrs.Name = "_btnAllAttrs";
    this._btnAllAttrs.UseVisualStyleBackColor = true;
    this._btnAllAttrs.Click += new EventHandler(this.On_showAllAttrs_Click);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOk, "_btnOk");
    this._btnOk.DialogResult = DialogResult.OK;
    this._btnOk.Name = "_btnOk";
    this._btnOk.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this._splAttributes);
    this.Controls.Add((Control) this._pnlResultButtons);
    this.DoubleBuffered = true;
    this.Name = nameof (ImbaseAddIndexDialog);
    this.ShowInTaskbar = false;
    this._splAttributes.Panel1.ResumeLayout(false);
    this._splAttributes.Panel2.ResumeLayout(false);
    this._splAttributes.EndInit();
    this._splAttributes.ResumeLayout(false);
    this._pnlSelectButtons.ResumeLayout(false);
    this._pnlResultButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
