// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionLogForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection.AutoSelectionLog;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public class AutoSelectionLogForm : Form
{
  protected AutoSelectionLogRec _rootLogRec;
  private IContainer components;
  private Button btnCancel;
  private ImageList ilLogTree;
  private Panel pnlBotom;
  public SplitContainer splitContainer1;
  private TreeView tvSelectionLog;
  private PropertyGrid pgSelectionLogRec;
  private ContextMenuStrip cmsLogTree;
  private ToolStripMenuItem tsmiExpandAll;
  private ToolStripMenuItem tsmiCollapseAll;

  protected void FillAutoSelectionLog()
  {
    this.tvSelectionLog.BeginUpdate();
    try
    {
      this.tvSelectionLog.Nodes.Clear();
      if (this._rootLogRec == null)
        return;
      this.AddSelectionLogNode((TreeNode) null, this._rootLogRec);
    }
    finally
    {
      this.tvSelectionLog.EndUpdate();
      this.pgSelectionLogRec.SelectedObject = (object) this.GetSelectedRec();
    }
  }

  protected TreeNode AddSelectionLogNode(TreeNode ownerNode, AutoSelectionLogRec logRec)
  {
    if (logRec == null)
      return (TreeNode) null;
    TreeNode ownerNode1 = ownerNode != null ? ownerNode.Nodes.Add("") : this.tvSelectionLog.Nodes.Add("");
    ownerNode1.Text = logRec.ToString();
    ownerNode1.Tag = (object) logRec;
    foreach (AutoSelectionLogRec childs in (IEnumerable<IAutoSelectionLogRec>) logRec.ChildsList)
      this.AddSelectionLogNode(ownerNode1, childs);
    return ownerNode1;
  }

  protected AutoSelectionLogRec GetSelectedRec()
  {
    return this.tvSelectionLog.SelectedNode == null ? (AutoSelectionLogRec) null : this.tvSelectionLog.SelectedNode.Tag as AutoSelectionLogRec;
  }

  public AutoSelectionLogForm()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    this.InitializeComponent();
  }

  public AutoSelectionLogRec RootLogRec
  {
    get => this._rootLogRec;
    set
    {
      this._rootLogRec = value;
      this.FillAutoSelectionLog();
    }
  }

  private void tvSelectionLog_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.pgSelectionLogRec.SelectedObject = (object) this.GetSelectedRec();
  }

  private void cmsLogTree_Opening(object sender, CancelEventArgs e)
  {
    this.tsmiExpandAll.Enabled = this.tsmiCollapseAll.Enabled = this.tvSelectionLog.Nodes.Count != 0;
  }

  private void tsmiExpandAll_Click(object sender, EventArgs e) => this.tvSelectionLog.ExpandAll();

  private void tsmiCollapseAll_Click(object sender, EventArgs e)
  {
    this.tvSelectionLog.CollapseAll();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionLogForm));
    this.splitContainer1 = new SplitContainer();
    this.tvSelectionLog = new TreeView();
    this.cmsLogTree = new ContextMenuStrip(this.components);
    this.tsmiExpandAll = new ToolStripMenuItem();
    this.tsmiCollapseAll = new ToolStripMenuItem();
    this.pgSelectionLogRec = new PropertyGrid();
    this.btnCancel = new Button();
    this.ilLogTree = new ImageList(this.components);
    this.pnlBotom = new Panel();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.cmsLogTree.SuspendLayout();
    this.pnlBotom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.tvSelectionLog);
    this.splitContainer1.Panel2.Controls.Add((Control) this.pgSelectionLogRec);
    this.tvSelectionLog.ContextMenuStrip = this.cmsLogTree;
    componentResourceManager.ApplyResources((object) this.tvSelectionLog, "tvSelectionLog");
    this.tvSelectionLog.HideSelection = false;
    this.tvSelectionLog.Name = "tvSelectionLog";
    this.tvSelectionLog.AfterSelect += new TreeViewEventHandler(this.tvSelectionLog_AfterSelect);
    this.cmsLogTree.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiExpandAll,
      (ToolStripItem) this.tsmiCollapseAll
    });
    this.cmsLogTree.Name = "cmsLogTree";
    componentResourceManager.ApplyResources((object) this.cmsLogTree, "cmsLogTree");
    this.cmsLogTree.Opening += new CancelEventHandler(this.cmsLogTree_Opening);
    this.tsmiExpandAll.Name = "tsmiExpandAll";
    componentResourceManager.ApplyResources((object) this.tsmiExpandAll, "tsmiExpandAll");
    this.tsmiExpandAll.Click += new EventHandler(this.tsmiExpandAll_Click);
    this.tsmiCollapseAll.Name = "tsmiCollapseAll";
    componentResourceManager.ApplyResources((object) this.tsmiCollapseAll, "tsmiCollapseAll");
    this.tsmiCollapseAll.Click += new EventHandler(this.tsmiCollapseAll_Click);
    componentResourceManager.ApplyResources((object) this.pgSelectionLogRec, "pgSelectionLogRec");
    this.pgSelectionLogRec.Name = "pgSelectionLogRec";
    this.pgSelectionLogRec.SelectedObject = (object) this.tvSelectionLog;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.OK;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.ilLogTree.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.ilLogTree, "ilLogTree");
    this.ilLogTree.TransparentColor = Color.Transparent;
    this.pnlBotom.Controls.Add((Control) this.btnCancel);
    componentResourceManager.ApplyResources((object) this.pnlBotom, "pnlBotom");
    this.pnlBotom.Name = "pnlBotom";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.pnlBotom);
    this.Name = nameof (AutoSelectionLogForm);
    this.ShowInTaskbar = false;
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.ResumeLayout(false);
    this.cmsLogTree.ResumeLayout(false);
    this.pnlBotom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
