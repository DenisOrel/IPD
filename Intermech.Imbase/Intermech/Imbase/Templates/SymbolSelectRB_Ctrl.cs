// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.SymbolSelectRB_Ctrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Templates;

public class SymbolSelectRB_Ctrl : UserControl
{
  private DialogResult _dlgRes = DialogResult.Cancel;
  private int _maxHeight;
  private string _originalFilter = string.Empty;
  private IContainer components;
  private TreeView _trv;
  private ImageList _img;
  private Panel _pnlBottom;
  private Button _btnOK;
  private Button _btnCancel;
  private GroupBox groupBox1;

  public SymbolSelectRB_Ctrl(string templatesBody)
  {
    this.InitializeComponent();
    this._maxHeight = Screen.PrimaryScreen.WorkingArea.Height / 4;
    this.BuildLayout(templatesBody);
  }

  public string ButtonOKText
  {
    get => this._btnOK.Text;
    set => this._btnOK.Text = value;
  }

  public DialogResult DlgRes => this._dlgRes;

  public string Filter
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder(32 /*0x20*/);
      foreach (TreeNode node in this._trv.Nodes)
      {
        TreeNode tag = node.Tag as TreeNode;
        if (tag.Tag != null)
          stringBuilder.Append((char) tag.Tag);
      }
      return stringBuilder.ToString();
    }
    set
    {
      this._originalFilter = value;
      if (this._trv.Nodes.Count > 0)
      {
        foreach (TreeNode node1 in this._trv.Nodes)
        {
          if (node1.Nodes.Count != 0)
          {
            this.SetImageForSelectedItem(node1.Nodes[0]);
            foreach (TreeNode node2 in node1.Nodes)
            {
              if (node2.Tag != null && value.IndexOf((char) node2.Tag) != -1)
                this.SetImageForSelectedItem(node2);
            }
          }
        }
      }
      this._btnOK.Enabled = false;
    }
  }

  public event EventHandler BtnClickEvent;

  private void On_btn_Click(object sender, EventArgs e)
  {
    this._dlgRes = (sender as Button).DialogResult;
    if (this._dlgRes == DialogResult.Cancel)
      this.Filter = this._originalFilter;
    EventHandler btnClickEvent = this.BtnClickEvent;
    if (btnClickEvent == null)
      return;
    btnClickEvent(sender, e);
  }

  private void On_trv_AfterCollapse(object sender, TreeViewEventArgs e)
  {
    e.Node.SelectedImageIndex = e.Node.ImageIndex = 0;
  }

  private void On_trv_AfterExpand(object sender, TreeViewEventArgs e)
  {
    e.Node.SelectedImageIndex = e.Node.ImageIndex = 1;
  }

  private void On_trv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    this.SetImageForSelectedItem(e.Node);
  }

  private void On_trv_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Space)
      return;
    this.SetImageForSelectedItem(this._trv.SelectedNode);
  }

  private void BuildLayout(string value)
  {
    int num = this._pnlBottom.Height + 6;
    string[] strArray = value.Replace(Environment.NewLine, "\n").Split('\n');
    Font font = new Font(this._trv.Font, FontStyle.Bold);
    TreeNode node1 = (TreeNode) null;
    for (int index = 0; index < strArray.Length; ++index)
    {
      string text = strArray[index];
      if (!string.IsNullOrEmpty(text))
      {
        if (text[0] != '\t')
        {
          node1 = new TreeNode(text, 0, 0);
          node1.NodeFont = font;
          this._trv.Nodes.Add(node1);
          TreeNode node2 = new TreeNode(LocalizationHolder.rm.GetString("Imbase_Templates_Filter_NotSelected"), 3, 3);
          node1.Nodes.Add(node2);
          node1.Tag = (object) node2;
          node1.Expand();
          num += this._trv.ItemHeight * 2;
        }
        else
        {
          node1.Nodes.Add(new TreeNode(text.Substring(5), 2, 2)
          {
            Tag = (object) text[2]
          });
          num += this._trv.ItemHeight;
        }
      }
    }
    this.Height = num < this._maxHeight ? num : this._maxHeight;
  }

  private void SetImageForSelectedItem(TreeNode node)
  {
    if (node.Parent == null || node.ImageIndex == 3)
      return;
    node.ImageIndex = node.SelectedImageIndex = 3;
    if (node.Parent.Tag is TreeNode tag)
      tag.ImageIndex = tag.SelectedImageIndex = 2;
    node.Parent.Tag = (object) node;
    this._btnOK.Enabled = string.Compare(this._originalFilter, this.Filter) != 0;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SymbolSelectRB_Ctrl));
    this._trv = new TreeView();
    this._img = new ImageList(this.components);
    this._pnlBottom = new Panel();
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this.groupBox1 = new GroupBox();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._trv, "_trv");
    this._trv.BorderStyle = BorderStyle.None;
    this._trv.ImageList = this._img;
    this._trv.Name = "_trv";
    this._trv.AfterCollapse += new TreeViewEventHandler(this.On_trv_AfterCollapse);
    this._trv.AfterExpand += new TreeViewEventHandler(this.On_trv_AfterExpand);
    this._trv.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.On_trv_NodeMouseClick);
    this._trv.KeyDown += new KeyEventHandler(this.On_trv_KeyDown);
    this._img.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_img.ImageStream");
    this._img.TransparentColor = Color.Transparent;
    this._img.Images.SetKeyName(0, "Collapsed.ico");
    this._img.Images.SetKeyName(1, "Expanded.ico");
    this._img.Images.SetKeyName(2, "Unchecked.ico");
    this._img.Images.SetKeyName(3, "Checked.ico");
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Controls.Add((Control) this._btnOK);
    this._pnlBottom.Controls.Add((Control) this._btnCancel);
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.Tag = (object) "";
    this._btnOK.UseVisualStyleBackColor = true;
    this._btnOK.Click += new EventHandler(this.On_btn_Click);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.Tag = (object) "";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this.On_btn_Click);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._trv);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (SymbolSelectRB_Ctrl);
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
