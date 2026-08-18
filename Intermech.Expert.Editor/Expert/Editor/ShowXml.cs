// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ShowXml
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor;

public class ShowXml : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private TreeView tv;
  private Button btnAbort;
  private Label label1;

  public ShowXml() => this.InitializeComponent();

  public void Execute(XmlDocument xmlDoc)
  {
    this.tv.Nodes.Clear();
    this._ShowXml(xmlDoc, this.tv);
    this.tv.ExpandAll();
    int num = (int) this.ShowDialog();
  }

  public bool ExecSaveAbort(XmlDocument xmlDoc)
  {
    this.tv.Nodes.Clear();
    this._ShowXml(xmlDoc, this.tv);
    this.btnAbort.Visible = true;
    this.Text = LocalizationHolder.rm.GetString("Expert.Editor_681");
    this.label1.Text = LocalizationHolder.rm.GetString("Expert.Editor_682");
    this.StartPosition = FormStartPosition.CenterScreen;
    this.tv.ExpandAll();
    return this.ShowDialog() == DialogResult.Abort;
  }

  private void _ShowXml(XmlDocument doc, TreeView xView)
  {
    this.AddNodeAndChildren((XmlNode) doc.DocumentElement, (TreeNode) null, xView);
  }

  private void AddNodeAndChildren(XmlNode xnode, TreeNode tnode, TreeView xView)
  {
    string attribs = "";
    ShowXml.NodeInfo info = (ShowXml.NodeInfo) null;
    if (xnode.Attributes != null)
      attribs = this.CollectAttributes(xnode, out info);
    TreeNode tnode1 = this.AddNode(xnode, tnode, attribs, xView);
    tnode1.Tag = (object) info;
    if (!xnode.HasChildNodes)
      return;
    foreach (XmlNode childNode in xnode.ChildNodes)
      this.AddNodeAndChildren(childNode, tnode1, xView);
  }

  private TreeNode AddNode(XmlNode xnode, TreeNode tnode, string attribs, TreeView xView)
  {
    TreeNodeCollection treeNodeCollection1 = tnode == null ? xView.Nodes : tnode.Nodes;
    TreeNode treeNode;
    switch (xnode.NodeType)
    {
      case XmlNodeType.Element:
      case XmlNodeType.Document:
        treeNodeCollection1.Add(treeNode = new TreeNode(xnode.Name + attribs, 0, 0));
        break;
      case XmlNodeType.Text:
        string text1 = xnode.Value;
        if (text1.Length > 128 /*0x80*/)
          text1 = text1.Substring(0, 128 /*0x80*/) + "...";
        treeNodeCollection1.Add(treeNode = new TreeNode(text1, 2, 2));
        break;
      case XmlNodeType.CDATA:
        string str = xnode.Value;
        if (str.Length > 128 /*0x80*/)
          str = str.Substring(0, 128 /*0x80*/) + "...";
        string text2 = $"<![CDATA]{str}]]>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text2, 3, 3));
        break;
      case XmlNodeType.EntityReference:
        string text3 = $"&{xnode.Value};";
        treeNodeCollection1.Add(treeNode = new TreeNode(text3, 7, 7));
        break;
      case XmlNodeType.Entity:
        string text4 = $"<!ENTITY {xnode.Value}>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text4, 6, 6));
        break;
      case XmlNodeType.ProcessingInstruction:
      case XmlNodeType.XmlDeclaration:
        string text5 = $"<?{xnode.Name + attribs} {xnode.Value}?>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text5, 5, 5));
        break;
      case XmlNodeType.Comment:
        string text6 = $"<!--{xnode.Value}-->";
        treeNodeCollection1.Add(treeNode = new TreeNode(text6, 4, 4));
        break;
      case XmlNodeType.DocumentType:
        string text7 = $"<!DOCTYPE {xnode.Value}>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text7, 8, 8));
        break;
      case XmlNodeType.Notation:
        string text8 = $"<!NOTATION {xnode.Value}>";
        treeNodeCollection1.Add(treeNode = new TreeNode(text8, 9, 9));
        break;
      default:
        TreeNodeCollection treeNodeCollection2 = treeNodeCollection1;
        XmlNodeType nodeType = xnode.NodeType;
        TreeNode node;
        treeNode = node = new TreeNode(nodeType.ToString(), 1, 1);
        treeNodeCollection2.Add(node);
        break;
    }
    return treeNode;
  }

  private string CollectAttributes(XmlNode xnode, out ShowXml.NodeInfo info)
  {
    string str = "";
    info = (ShowXml.NodeInfo) null;
    foreach (XmlAttribute attribute in (XmlNamedNodeMap) xnode.Attributes)
    {
      if (attribute.Name == "_OBJ_ID_")
        info = new ShowXml.NodeInfo(Convert.ToInt64(attribute.Value));
      else
        str += $"    {attribute.Name}={attribute.Value}";
    }
    return str;
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
    this.panel1 = new Panel();
    this.btnAbort = new Button();
    this.btnOK = new Button();
    this.tv = new TreeView();
    this.label1 = new Label();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.btnAbort);
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 306);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(661, 36);
    this.panel1.TabIndex = 0;
    this.btnAbort.DialogResult = DialogResult.Abort;
    this.btnAbort.Location = new Point(493, 6);
    this.btnAbort.Name = "btnAbort";
    this.btnAbort.Size = new Size(75, 23);
    this.btnAbort.TabIndex = 1;
    this.btnAbort.Text = "Прервать";
    this.btnAbort.UseVisualStyleBackColor = true;
    this.btnAbort.Visible = false;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(574, 6);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 0;
    this.btnOK.Text = "Закрыть";
    this.btnOK.UseVisualStyleBackColor = true;
    this.tv.Dock = DockStyle.Fill;
    this.tv.FullRowSelect = true;
    this.tv.HideSelection = false;
    this.tv.Location = new Point(0, 0);
    this.tv.Name = "tv";
    this.tv.Size = new Size(661, 306);
    this.tv.TabIndex = 5;
    this.label1.Location = new Point(12, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(475, 21);
    this.label1.TabIndex = 2;
    this.label1.TextAlign = ContentAlignment.MiddleCenter;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(661, 342);
    this.Controls.Add((Control) this.tv);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ShowXml);
    this.Text = "Результаты проверки";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class NodeInfo
  {
    public long objId = -1;

    public NodeInfo(long oID) => this.objId = oID;
  }
}
