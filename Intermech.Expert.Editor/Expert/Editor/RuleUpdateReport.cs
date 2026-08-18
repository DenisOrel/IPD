// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.RuleUpdateReport
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for RuleUpdateReport.</summary>
public class RuleUpdateReport : Form
{
  private TreeView tv;
  private Panel panel1;
  private Button button1;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public RuleUpdateReport() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public void Execute(byte[] traceInfo)
  {
    this.ShowXml(ZlibHelper.UnpackXmlBuffer(traceInfo), this.tv);
    this.tv.ExpandAll();
    int num = (int) this.ShowDialog();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RuleUpdateReport));
    this.tv = new TreeView();
    this.panel1 = new Panel();
    this.button1 = new Button();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tv, "tv");
    this.tv.Name = "tv";
    this.panel1.BorderStyle = BorderStyle.Fixed3D;
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Cancel;
    this.button1.Name = "button1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button1;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.tv);
    this.MinimizeBox = false;
    this.Name = nameof (RuleUpdateReport);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.RuleUpdateReport_Load);
    this.FormClosed += new FormClosedEventHandler(this.RuleUpdateReport_FormClosed);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void ShowXml(XmlDocument doc, TreeView xView)
  {
    xView.Nodes.Clear();
    this.AddNodeAndChildren((XmlNode) doc.DocumentElement, (TreeNode) null, xView);
  }

  private void AddNodeAndChildren(XmlNode xnode, TreeNode tnode, TreeView xView)
  {
    string attribs = "";
    if (xnode.Attributes != null)
      attribs = this.CollectAttributes(xnode);
    TreeNode tnode1 = this.AddNode(xnode, tnode, attribs, xView);
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

  private string CollectAttributes(XmlNode xnode)
  {
    string str = "";
    foreach (XmlAttribute attribute in (XmlNamedNodeMap) xnode.Attributes)
      str += $"    {attribute.Name}={attribute.Value}";
    return str;
  }

  private void RuleUpdateReport_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void RuleUpdateReport_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }
}
