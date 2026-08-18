// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.SymbolSelectChB_Ctrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Templates;

public class SymbolSelectChB_Ctrl : Form
{
  private int _maxHeight;
  private IContainer components;
  private ImageList _img;
  private Panel _pnl;
  private Panel _pnlBottom;
  private Button _btnOK;
  private Button _btnCancel;
  private TreeView _trv;

  public SymbolSelectChB_Ctrl(string templatesBody)
  {
    this.InitializeComponent();
    this._maxHeight = Screen.PrimaryScreen.WorkingArea.Height / 4;
    this.BuildLayout(templatesBody);
  }

  public string Filter
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder(32 /*0x20*/);
      foreach (TreeNode node in this._trv.Nodes)
      {
        if (node.ImageIndex != 2)
        {
          foreach (TreeNode treeNode in node.Tag as List<TreeNode>)
            stringBuilder.Append((char) treeNode.Tag);
        }
      }
      char[] charArray = stringBuilder.ToString().ToCharArray();
      Array.Sort<char>(charArray);
      stringBuilder.Remove(0, stringBuilder.Length);
      stringBuilder.Append(charArray);
      return stringBuilder.ToString();
    }
    set
    {
      if (string.IsNullOrEmpty(value))
        return;
      foreach (TreeNode node1 in this._trv.Nodes)
      {
        node1.Tag = (object) new List<TreeNode>(node1.Nodes.Count);
        foreach (TreeNode node2 in node1.Nodes)
        {
          if (node2.Tag == null || value.IndexOf((char) node2.Tag) == -1)
            this.SetImageForSelectedItem(node2, false);
          else
            this.SetImageForSelectedItem(node2, true);
        }
      }
    }
  }

  private void OnAfterCollapse(object sender, TreeViewEventArgs e)
  {
    e.Node.SelectedImageIndex = e.Node.ImageIndex = 0;
  }

  private void OnAfterExpand(object sender, TreeViewEventArgs e)
  {
    e.Node.SelectedImageIndex = e.Node.ImageIndex = 1;
  }

  private void On_trv_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Space)
      return;
    TreeNode selectedNode = this._trv.SelectedNode;
    this.SetImageForSelectedItem(selectedNode, selectedNode.ImageIndex == 3);
  }

  private void On_trv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    this.SetImageForSelectedItem(e.Node, e.Node.ImageIndex == 3);
  }

  private void BuildLayout(string value)
  {
    int num = this._pnlBottom.Height + 3;
    string[] strArray = value.Replace(Environment.NewLine, "\n").Split('\n');
    Font font = new Font(this._trv.Font, FontStyle.Bold);
    TreeNode node = (TreeNode) null;
    for (int index = 0; index < strArray.Length; ++index)
    {
      string text = strArray[index];
      if (!string.IsNullOrEmpty(text))
      {
        if (text[0] != '\t')
        {
          node = new TreeNode(text, 1, 1);
          node.NodeFont = font;
          node.Tag = (object) new List<TreeNode>();
          this._trv.Nodes.Add(node);
          node.Expand();
        }
        else
          node.Nodes.Add(new TreeNode(text.Substring(5), 3, 3)
          {
            Tag = (object) text[2]
          });
        num += this._trv.ItemHeight;
      }
    }
    this.Height = num + 20 < this._maxHeight ? num + 20 : this._maxHeight;
  }

  private void SetImageForSelectedItem(TreeNode node, bool bCh)
  {
    if (node.Parent == null)
      return;
    node.ImageIndex = node.SelectedImageIndex = bCh ? 2 : 3;
    List<TreeNode> tag = node.Parent.Tag as List<TreeNode>;
    if (bCh)
      tag.Add(node);
    else
      tag.Remove(node);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SymbolSelectChB_Ctrl));
    this._img = new ImageList(this.components);
    this._pnl = new Panel();
    this._trv = new TreeView();
    this._pnlBottom = new Panel();
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this._pnl.SuspendLayout();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this._img.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_img.ImageStream");
    this._img.TransparentColor = Color.Transparent;
    this._img.Images.SetKeyName(0, "Collapsed.ico");
    this._img.Images.SetKeyName(1, "Expanded.ico");
    this._img.Images.SetKeyName(2, "Checked.ico");
    this._img.Images.SetKeyName(3, "Unchecked.ico");
    componentResourceManager.ApplyResources((object) this._pnl, "_pnl");
    this._pnl.BorderStyle = BorderStyle.FixedSingle;
    this._pnl.Controls.Add((Control) this._trv);
    this._pnl.Controls.Add((Control) this._pnlBottom);
    this._pnl.Name = "_pnl";
    componentResourceManager.ApplyResources((object) this._trv, "_trv");
    this._trv.BorderStyle = BorderStyle.None;
    this._trv.ImageList = this._img;
    this._trv.Name = "_trv";
    this._trv.AfterCollapse += new TreeViewEventHandler(this.OnAfterCollapse);
    this._trv.AfterExpand += new TreeViewEventHandler(this.OnAfterExpand);
    this._trv.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.On_trv_NodeMouseClick);
    this._trv.KeyDown += new KeyEventHandler(this.On_trv_KeyDown);
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.Tag = (object) "0";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Tag = (object) "1";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._pnl);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.None;
    this.Name = nameof (SymbolSelectChB_Ctrl);
    this.ShowInTaskbar = false;
    this._pnl.ResumeLayout(false);
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
