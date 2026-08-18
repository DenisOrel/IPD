// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.AttributesGroupSelector
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class AttributesGroupSelector : Form
{
  private IContainer components;
  private Button btnCancel;
  private Button btnOk;
  private TreeView tvAttributesGroups;
  private IntList AttrGroupID = new IntList();
  private ImageList ilAttrGroups;

  public DialogResult Execute(IntList aList)
  {
    this.AttrGroupID = aList;
    this.FillForm();
    this.SelectObjects();
    return this.ShowDialog();
  }

  public AttributesGroupSelector() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttributesGroupSelector));
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.tvAttributesGroups = new TreeView();
    this.ilAttrGroups = new ImageList(this.components);
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.tvAttributesGroups, "tvAttributesGroups");
    this.tvAttributesGroups.CheckBoxes = true;
    this.tvAttributesGroups.ImageList = this.ilAttrGroups;
    this.tvAttributesGroups.Name = "tvAttributesGroups";
    this.ilAttrGroups.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilAttrGroups.ImageStream");
    this.ilAttrGroups.TransparentColor = Color.Transparent;
    this.ilAttrGroups.Images.SetKeyName(0, "group_atribut.bmp");
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.tvAttributesGroups);
    this.Controls.Add((Control) this.btnOk);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AttributesGroupSelector);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Load += new EventHandler(this.AttributesGroupSelector_Load);
    this.FormClosing += new FormClosingEventHandler(this.AttributesGroupSelector_FormClosing);
    this.ResumeLayout(false);
  }

  private void FillForm()
  {
    DataTable dataTable = DataHolders.AttributeGroupsHolder.LoadData(false);
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (Convert.ToInt32(row["F_GROUP_ID"]) != -1)
        this.FillAttribitesGroup(row, (TreeNode) null);
    }
    this.tvAttributesGroups.Sort();
  }

  private void FillAttribitesGroup(DataRow curRow, TreeNode parentNode)
  {
    TreeNode treeNode = new TreeNode();
    treeNode.Text = curRow["F_GROUP_NAME"].ToString();
    int int32 = Convert.ToInt32(curRow["F_GROUP_ID"]);
    treeNode.Tag = (object) int32;
    treeNode.ImageIndex = 0;
    if (parentNode == null)
      this.tvAttributesGroups.Nodes.Add(treeNode);
    else
      parentNode.Nodes.Add(treeNode);
    foreach (DataRow row in (InternalDataCollectionBase) this.GetChildrenAttributeGroupsTable(int32).Rows)
      this.FillAttribitesGroup(row, treeNode);
    treeNode.Expand();
  }

  private DataTable GetChildrenAttributeGroupsTable(int parentGroupID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetAttributesGroupCollection(parentGroupID).Select("");
  }

  private void SelectObjects()
  {
    if (this.AttrGroupID == null || this.AttrGroupID.Count <= 0)
      return;
    foreach (TreeNode node in this.tvAttributesGroups.Nodes)
      this.CheckNode(node);
  }

  private void CheckNode(TreeNode curNode)
  {
    if (this.AttrGroupID.Contains((object) Convert.ToInt32(curNode.Tag)))
      curNode.Checked = true;
    foreach (TreeNode node in curNode.Nodes)
      this.CheckNode(node);
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    this.AttrGroupID.Clear();
    foreach (TreeNode node in this.tvAttributesGroups.Nodes)
      this.CollectObjects(node);
  }

  public void CollectObjects(TreeNode curNode)
  {
    if (curNode.Checked)
      this.AttrGroupID.Add((object) Convert.ToInt32(curNode.Tag));
    foreach (TreeNode node in curNode.Nodes)
      this.CollectObjects(node);
  }

  private void AttributesGroupSelector_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void AttributesGroupSelector_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }
}
