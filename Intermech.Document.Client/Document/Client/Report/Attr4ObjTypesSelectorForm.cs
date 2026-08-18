// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.Attr4ObjTypesSelectorForm
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Report;

public class Attr4ObjTypesSelectorForm : Form
{
  private static readonly string _formTextDefault = LocalizationHolder.rm.GetString("Document.Client_28");
  private List<int> _attributeIDs = new List<int>();
  private List<int> _selectedAttributeIDs = new List<int>();
  private List<int> _objTypesIDs = new List<int>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel2;
  private Panel panel1;
  private Button btnCancel;
  private Button btnApply;
  private SplitContainer splitContainer1;
  private ListView listView;
  private ColumnHeader columnHeader1;
  private CheckBox checkBox;
  private TreeView treeView;

  public List<int> SelectedAttributes => this._selectedAttributeIDs;

  public Attr4ObjTypesSelectorForm(string caption)
  {
    this.InitializeComponent();
    this.Text = caption;
    this.InitializeFormData();
  }

  public Attr4ObjTypesSelectorForm()
    : this(Attr4ObjTypesSelectorForm._formTextDefault)
  {
  }

  private void InitializeFormData()
  {
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    if (service != null)
      this.treeView.ImageList = service.ImageList;
    this.treeView.BeginUpdate();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectTypeCollection(-1).Select(string.Empty);
      if (dataTable.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          this.FillObjectType(sessionKeeper.Session, service, Convert.ToInt32(row["F_OBJECT_TYPE"]), (TreeNode) null);
      }
    }
    this.treeView.EndUpdate();
  }

  /// <summary>Добавить тип объектов в дерево</summary>
  /// <param name="session"></param>
  /// <param name="iconService"></param>
  /// <param name="objectTypeID"></param>
  /// <param name="parentNode"></param>
  private void FillObjectType(
    IUserSession session,
    ICategoryTypeIconService iconService,
    int objectTypeID,
    TreeNode parentNode)
  {
    IDBObjectType objectType = session.GetObjectType(objectTypeID);
    DataTable dataTable = session.GetObjectTypeCollection(objectTypeID).Select(string.Empty);
    int num = iconService != null ? iconService.IndexOf(4, objectTypeID) : -1;
    TreeNode treeNode;
    if (dataTable.Rows.Count <= 0)
      treeNode = new TreeNode(objectType.ObjectTypeName, num, num);
    else
      treeNode = new TreeNode(objectType.ObjectTypeName, num, num, new TreeNode[1]
      {
        new TreeNode()
      });
    TreeNode node = treeNode;
    node.Tag = (object) new Attr4ObjTypesSelectorForm.NodeID(objectTypeID, dataTable.Rows.Count > 0);
    if (parentNode != null)
      parentNode.Nodes.Add(node);
    else
      this.treeView.Nodes.Add(node);
  }

  /// <summary>Выбор типа объекта в дереве</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
  {
    Attr4ObjTypesSelectorForm.NodeID tag = (Attr4ObjTypesSelectorForm.NodeID) e.Node.Tag;
    if (e.Node.Checked)
      this._objTypesIDs.Add(tag.ID);
    else
      this._objTypesIDs.Remove(tag.ID);
    this.RebuildObjectTypes();
    if (this.checkBox.Checked)
      return;
    this.FillListView(true);
  }

  private void RebuildObjectTypes()
  {
    this._attributeIDs = new List<int>();
    if (this._objTypesIDs.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int objTypesId in this._objTypesIDs)
        stringBuilder.Append($"{objTypesId},");
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      foreach (DataRow dataRow in (sessionKeeper.Session as IClientSession).ClientCache.GetTable("IMS_ATTR4OBJ_TYPES").Select($"{"F_OBJECT_TYPE"} IN ({stringBuilder.ToString()})"))
      {
        int int32 = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
        if (!this._attributeIDs.Contains(int32))
          this._attributeIDs.Add(int32);
      }
    }
  }

  private void FillListView(bool fromList)
  {
    this.listView.BeginUpdate();
    this.listView.Items.Clear();
    this._selectedAttributeIDs = new List<int>();
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    if (service != null)
    {
      this.listView.SmallImageList = service.ImageList;
      this.listView.LargeImageList = service.ImageList;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IClientCache clientCache = (sessionKeeper.Session as IClientSession).ClientCache;
      if (fromList)
      {
        if (this._attributeIDs.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (int attributeId in this._attributeIDs)
            stringBuilder.Append($"{attributeId},");
          stringBuilder.Remove(stringBuilder.Length - 1, 1);
          foreach (DataRow dataRow in clientCache.GetTable("IMS_ATTRIBUTES").Select($"{"F_ATTRIBUTE_ID"} IN ({stringBuilder.ToString()})"))
            this.AddAtribute(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), Convert.ToString(dataRow["F_NAME"]), service);
        }
      }
      else
      {
        foreach (DataRow dataRow in clientCache.GetTable("IMS_ATTRIBUTES").Select(string.Empty, "F_NAME ASC"))
          this.AddAtribute(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), Convert.ToString(dataRow["F_NAME"]), service);
      }
    }
    this.listView.EndUpdate();
    this.SetButtons();
  }

  private void AddAtribute(int attributeID, string name, ICategoryTypeIconService iconService)
  {
    this.listView.Items.Add(new ListViewItem(name)
    {
      Tag = (object) attributeID,
      ImageIndex = iconService != null ? iconService.IndexOf(3, -1, (object) attributeID) : -1
    });
  }

  /// <summary>Все атрибуты</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_CheckedChanged(object sender, EventArgs e)
  {
    if (this.checkBox.Checked)
    {
      this.FillListView(false);
    }
    else
    {
      this.RebuildObjectTypes();
      this.FillListView(true);
    }
  }

  private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    Attr4ObjTypesSelectorForm.NodeID tag = (Attr4ObjTypesSelectorForm.NodeID) e.Node.Tag;
    if (tag.Expanded)
      return;
    this.treeView.BeginUpdate();
    e.Node.Nodes.Clear();
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectTypeCollection(tag.ID).Select(string.Empty);
      if (dataTable.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          this.FillObjectType(sessionKeeper.Session, service, Convert.ToInt32(row["F_OBJECT_TYPE"]), e.Node);
      }
    }
    this.treeView.EndUpdate();
    tag.Expanded = true;
  }

  private void SetButtons() => this.btnApply.Enabled = this._selectedAttributeIDs.Count > 0;

  private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
  {
    int tag = (int) e.Item.Tag;
    if (e.Item.Checked)
      this._selectedAttributeIDs.Add(tag);
    else
      this._selectedAttributeIDs.Remove(tag);
    this.SetButtons();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Attr4ObjTypesSelectorForm));
    this.splitContainer1 = new SplitContainer();
    this.treeView = new TreeView();
    this.listView = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.panel2 = new Panel();
    this.checkBox = new CheckBox();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.panel1 = new Panel();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.treeView);
    this.splitContainer1.Panel2.Controls.Add((Control) this.listView);
    this.treeView.CheckBoxes = true;
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.Name = "treeView";
    this.treeView.AfterCheck += new TreeViewEventHandler(this.treeView_AfterCheck);
    this.treeView.BeforeExpand += new TreeViewCancelEventHandler(this.treeView_BeforeExpand);
    this.listView.CheckBoxes = true;
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    componentResourceManager.ApplyResources((object) this.listView, "listView");
    this.listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.listView.Name = "listView";
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.ItemChecked += new ItemCheckedEventHandler(this.listView_ItemChecked);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    this.panel2.Controls.Add((Control) this.checkBox);
    this.panel2.Controls.Add((Control) this.btnCancel);
    this.panel2.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.checkBox, "checkBox");
    this.checkBox.Name = "checkBox";
    this.checkBox.UseVisualStyleBackColor = true;
    this.checkBox.CheckedChanged += new EventHandler(this.checkBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    this.panel1.Controls.Add((Control) this.splitContainer1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (Attr4ObjTypesSelectorForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Tag = (object) " ";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class NodeID
  {
    public int ID;
    public bool Expanded;
    public bool ChildPresent;

    public NodeID(int id, bool childPresent)
    {
      this.ID = id;
      this.ChildPresent = childPresent;
      this.Expanded = false;
    }
  }
}
