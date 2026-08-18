// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.SelectTree
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class SelectTree : Form
{
  public int selNodeId = -1;
  public string selLabel = "";
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeList tree;
  private TreeListColumn Struct;
  private TreeListColumn ModParms;
  private TreeListColumn OperParms;
  private RepositoryItemTextEdit repositoryItemTextEdit1;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private ImageList IL;

  public SelectTree()
  {
    this.InitializeComponent();
    this.tree.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowButtons | ViewOptionsFlags.ShowColumns | ViewOptionsFlags.ShowHorzLines | ViewOptionsFlags.ShowRoot | ViewOptionsFlags.ShowVertLines | ViewOptionsFlags.ShowFocusedFrame;
  }

  private void CopyChilds(TreeListNode dst, TreeListNode src)
  {
    if (!src.HasChildren)
      return;
    foreach (TreeListNode node in src.Nodes)
    {
      TreeListNode treeListNode = (TreeListNode) node.Clone();
      dst.Nodes.Add(treeListNode);
      this.CopyChilds(treeListNode, node);
    }
  }

  public bool Execute(TreeListNodes nodes)
  {
    foreach (TreeListNode node in nodes)
    {
      TreeListNode treeListNode = (TreeListNode) node.Clone();
      this.tree.Nodes.Add(treeListNode);
      this.CopyChilds(treeListNode, node);
    }
    return this.ShowDialog() == DialogResult.OK;
  }

  private void button1_Click(object sender, EventArgs e)
  {
    if (this.tree.FocusedNode == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_390"), LocalizationHolder.rm.GetString("Expert.Editor_391"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.DialogResult = DialogResult.None;
    }
    else
    {
      this.selLabel = (string) this.tree.FocusedNode[(object) 0];
      this.selNodeId = this.tree.FocusedNode.Id;
      if (!(this.selLabel == ""))
        return;
      this.selLabel = new UserPrompt().Execute(LocalizationHolder.rm.GetString("Expert.Editor_392"), LocalizationHolder.rm.GetString("Expert.Editor_393"));
      if (!(this.selLabel == ""))
        return;
      this.DialogResult = DialogResult.None;
    }
  }

  private void SelectTree_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SelectTree_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectTree));
    this.tree = new TreeList();
    this.Struct = new TreeListColumn();
    this.ModParms = new TreeListColumn();
    this.OperParms = new TreeListColumn();
    this.repositoryItemTextEdit1 = new RepositoryItemTextEdit();
    this.IL = new ImageList(this.components);
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.tree.BeginInit();
    this.repositoryItemTextEdit1.BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.tree.Columns.AddRange(new TreeListColumn[3]
    {
      this.Struct,
      this.ModParms,
      this.OperParms
    });
    componentResourceManager.ApplyResources((object) this.tree, "tree");
    this.tree.Name = "tree";
    this.tree.RepositoryItems.AddRange(new RepositoryItem[1]
    {
      (RepositoryItem) this.repositoryItemTextEdit1
    });
    this.tree.SelectImageList = this.IL;
    this.tree.StateImageList = this.IL;
    componentResourceManager.ApplyResources((object) this.Struct, "Struct");
    this.Struct.Name = "Struct";
    componentResourceManager.ApplyResources((object) this.ModParms, "ModParms");
    this.ModParms.Name = "ModParms";
    componentResourceManager.ApplyResources((object) this.OperParms, "OperParms");
    this.OperParms.Name = "OperParms";
    this.repositoryItemTextEdit1.AutoHeight = false;
    this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Magenta;
    this.IL.Images.SetKeyName(0, "");
    this.IL.Images.SetKeyName(1, "");
    this.IL.Images.SetKeyName(2, "");
    this.IL.Images.SetKeyName(3, "");
    this.IL.Images.SetKeyName(4, "");
    this.IL.Images.SetKeyName(5, "");
    this.IL.Images.SetKeyName(6, "");
    this.IL.Images.SetKeyName(7, "");
    this.IL.Images.SetKeyName(8, "");
    this.IL.Images.SetKeyName(9, "");
    this.IL.Images.SetKeyName(10, "");
    this.IL.Images.SetKeyName(11, "");
    this.IL.Images.SetKeyName(12, "");
    this.IL.Images.SetKeyName(13, "");
    this.IL.Images.SetKeyName(14, "");
    this.IL.Images.SetKeyName(15, "");
    this.IL.Images.SetKeyName(16 /*0x10*/, "");
    this.IL.Images.SetKeyName(17, "");
    this.IL.Images.SetKeyName(18, "");
    this.IL.Images.SetKeyName(19, "");
    this.IL.Images.SetKeyName(20, "");
    this.IL.Images.SetKeyName(21, "");
    this.IL.Images.SetKeyName(22, "");
    this.IL.Images.SetKeyName(23, "");
    this.IL.Images.SetKeyName(24, "");
    this.IL.Images.SetKeyName(25, "");
    this.IL.Images.SetKeyName(26, "");
    this.IL.Images.SetKeyName(27, "");
    this.IL.Images.SetKeyName(28, "");
    this.IL.Images.SetKeyName(29, "");
    this.IL.Images.SetKeyName(30, "");
    this.IL.Images.SetKeyName(31 /*0x1F*/, "");
    this.IL.Images.SetKeyName(32 /*0x20*/, "");
    this.IL.Images.SetKeyName(33, "");
    this.IL.Images.SetKeyName(34, "");
    this.IL.Images.SetKeyName(35, "");
    this.IL.Images.SetKeyName(36, "");
    this.IL.Images.SetKeyName(37, "VVV2.bmp");
    this.IL.Images.SetKeyName(38, "VVV1.bmp");
    this.IL.Images.SetKeyName(39, "VVV3.bmp");
    this.IL.Images.SetKeyName(40, "SSS_18.bmp");
    this.IL.Images.SetKeyName(41, "VVV4.bmp");
    this.IL.Images.SetKeyName(42, "VVV5.bmp");
    this.IL.Images.SetKeyName(43, "VVV6.bmp");
    this.IL.Images.SetKeyName(44, "Version_rule.bmp");
    this.IL.Images.SetKeyName(45, "sort1.bmp");
    this.IL.Images.SetKeyName(46, "sort2.bmp");
    this.IL.Images.SetKeyName(47, "TT1.bmp");
    this.IL.Images.SetKeyName(48 /*0x30*/, "TT2.bmp");
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click);
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.tree);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectTree);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.Load += new EventHandler(this.SelectTree_Load);
    this.FormClosed += new FormClosedEventHandler(this.SelectTree_FormClosed);
    this.tree.EndInit();
    this.repositoryItemTextEdit1.EndInit();
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
