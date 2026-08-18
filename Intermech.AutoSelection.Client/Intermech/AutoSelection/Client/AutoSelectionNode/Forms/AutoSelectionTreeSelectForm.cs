// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.Forms.AutoSelectionTreeSelectForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Forms;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode.Forms;

internal class AutoSelectionTreeSelectForm : Form
{
  private long _objectId;
  private bool _multiSelect;
  private readonly List<AutoSelectionNodeCommon> _checkedNodeList = new List<AutoSelectionNodeCommon>();
  private IContainer components;
  private TreeView tvNodes;
  private Panel pnlBotom;
  private Button btnCancel;
  private Button btnOk;
  private ImageList ilTree;
  private Panel pnlTop;
  private Label lblCaption;
  private Label lblObject;

  protected virtual void InitializeData()
  {
    this.UpdateControls();
    if (this.DesignMode)
      return;
    ImageList ilTree = (ImageList) null;
    AutosSelectConsts.Images.LoadImages(ref ilTree);
    this.tvNodes.ImageList = ilTree;
  }

  protected virtual void UpdateControls()
  {
    if (this._multiSelect)
    {
      this.btnOk.Enabled = false;
      foreach (TreeNode node in this.tvNodes.Nodes)
      {
        if (node.Checked)
        {
          this.btnOk.Enabled = true;
          break;
        }
      }
    }
    else
      this.btnOk.Enabled = this.tvNodes.SelectedNode != null && this.tvNodes.SelectedNode.Level == 0;
  }

  private AutoSelectionTreeSelectForm()
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
    this.InitializeData();
  }

  public AutoSelectionTreeSelectForm(
    long objectId,
    List<AutoSelectionNodeCommon> nodeList,
    bool multiSelect)
    : this()
  {
    this.ObjectID = objectId;
    this.MultiSelect = multiSelect;
    foreach (AutoSelectionNodeCommon node in nodeList)
    {
      TreeNode treeNode = this.tvNodes.Nodes.Add(string.Empty);
      SelectionTreeViewUtils.UpdateSelectionNode(treeNode, node, false);
      treeNode.Text = node.ShortInfo;
    }
  }

  public long ObjectID
  {
    get => this._objectId;
    set
    {
      if (this._objectId == value)
        return;
      this._objectId = value;
      if (this._objectId == 0L)
      {
        this.lblCaption.Text = string.Empty;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._objectId);
          this.lblCaption.Text = !objectInfo.Empty ? objectInfo.Caption : string.Empty;
        }
      }
    }
  }

  public bool MultiSelect
  {
    get => this._multiSelect;
    set
    {
      if (this._multiSelect == value)
        return;
      this._multiSelect = value;
      this.tvNodes.CheckBoxes = value;
    }
  }

  public AutoSelectionNodeCommon[] SelectedNodes
  {
    get
    {
      List<AutoSelectionNodeCommon> selectionNodeCommonList = new List<AutoSelectionNodeCommon>();
      if (this._multiSelect)
        selectionNodeCommonList.AddRange((IEnumerable<AutoSelectionNodeCommon>) this._checkedNodeList);
      else if (this.tvNodes.SelectedNode != null)
        selectionNodeCommonList.Add(this.tvNodes.SelectedNode.Tag as AutoSelectionNodeCommon);
      return selectionNodeCommonList.ToArray();
    }
  }

  private void AutoSelectionTreeSelectForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void AutoSelectionTreeSelectForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void tvNodes_AfterCheck(object sender, TreeViewEventArgs e)
  {
    if (!this._multiSelect || e.Node == null || !(e.Node.Tag is AutoSelectionNodeCommon tag))
      return;
    if (e.Node.Checked)
      this._checkedNodeList.Add(tag);
    else
      this._checkedNodeList.Remove(tag);
    this.UpdateControls();
  }

  private void tvNodes_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this._multiSelect)
      return;
    this.UpdateControls();
    if (this.tvNodes.SelectedNode == null || this.tvNodes.SelectedNode.Parent == null)
      return;
    this.tvNodes.AfterSelect -= new TreeViewEventHandler(this.tvNodes_AfterSelect);
    try
    {
      while (this.tvNodes.SelectedNode.Parent != null)
        this.tvNodes.SelectedNode = this.tvNodes.SelectedNode.Parent;
    }
    finally
    {
      this.tvNodes.AfterSelect += new TreeViewEventHandler(this.tvNodes_AfterSelect);
    }
  }

  private void tvNodes_BeforeCheck(object sender, TreeViewCancelEventArgs e)
  {
    if (!this._multiSelect)
      return;
    e.Cancel = e.Node.Parent != null;
  }

  private void tvNodes_DoubleClick(object sender, EventArgs e)
  {
  }

  private void tvNodes_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if (e == null || e.Node == null)
      return;
    if (this.MultiSelect)
    {
      e.Node.Checked = !e.Node.Checked;
      this.UpdateControls();
    }
    else
    {
      if (this.tvNodes.SelectedNode != e.Node)
        this.tvNodes.SelectedNode = e.Node;
      this.DialogResult = DialogResult.OK;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionTreeSelectForm));
    this.tvNodes = new TreeView();
    this.ilTree = new ImageList(this.components);
    this.pnlBotom = new Panel();
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.pnlTop = new Panel();
    this.lblObject = new Label();
    this.lblCaption = new Label();
    this.pnlBotom.SuspendLayout();
    this.pnlTop.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tvNodes, "tvNodes");
    this.tvNodes.ImageList = this.ilTree;
    this.tvNodes.Name = "tvNodes";
    this.tvNodes.BeforeCheck += new TreeViewCancelEventHandler(this.tvNodes_BeforeCheck);
    this.tvNodes.AfterCheck += new TreeViewEventHandler(this.tvNodes_AfterCheck);
    this.tvNodes.AfterSelect += new TreeViewEventHandler(this.tvNodes_AfterSelect);
    this.tvNodes.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.tvNodes_NodeMouseDoubleClick);
    this.tvNodes.DoubleClick += new EventHandler(this.tvNodes_DoubleClick);
    this.ilTree.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.ilTree, "ilTree");
    this.ilTree.TransparentColor = Color.Transparent;
    this.pnlBotom.Controls.Add((Control) this.btnCancel);
    this.pnlBotom.Controls.Add((Control) this.btnOk);
    componentResourceManager.ApplyResources((object) this.pnlBotom, "pnlBotom");
    this.pnlBotom.Name = "pnlBotom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.pnlTop.BackColor = SystemColors.Control;
    this.pnlTop.Controls.Add((Control) this.lblObject);
    this.pnlTop.Controls.Add((Control) this.lblCaption);
    componentResourceManager.ApplyResources((object) this.pnlTop, "pnlTop");
    this.pnlTop.Name = "pnlTop";
    componentResourceManager.ApplyResources((object) this.lblObject, "lblObject");
    this.lblObject.Name = "lblObject";
    componentResourceManager.ApplyResources((object) this.lblCaption, "lblCaption");
    this.lblCaption.ForeColor = SystemColors.ControlText;
    this.lblCaption.Name = "lblCaption";
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.tvNodes);
    this.Controls.Add((Control) this.pnlTop);
    this.Controls.Add((Control) this.pnlBotom);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AutoSelectionTreeSelectForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.FormClosed += new FormClosedEventHandler(this.AutoSelectionTreeSelectForm_FormClosed);
    this.Load += new EventHandler(this.AutoSelectionTreeSelectForm_Load);
    this.pnlBotom.ResumeLayout(false);
    this.pnlTop.ResumeLayout(false);
    this.pnlTop.PerformLayout();
    this.ResumeLayout(false);
  }
}
