// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.TableTest
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Summary description for TableTest.</summary>
public class TableTest : Form
{
  private Panel panel1;
  private ButtonEdit beObject;
  private Label label2;
  private ButtonEdit beTable;
  private Label label1;
  private Panel panel2;
  private Panel panel3;
  private Panel panel4;
  private Button bCalc;
  private Button bClose;
  private TreeView xmlView;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public TableTest()
  {
    this.InitializeComponent();
    this.CheckButton();
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableTest));
    this.panel1 = new Panel();
    this.beTable = new ButtonEdit();
    this.label2 = new Label();
    this.beObject = new ButtonEdit();
    this.label1 = new Label();
    this.panel2 = new Panel();
    this.panel3 = new Panel();
    this.xmlView = new TreeView();
    this.panel4 = new Panel();
    this.bClose = new Button();
    this.bCalc = new Button();
    this.panel1.SuspendLayout();
    this.beTable.Properties.BeginInit();
    this.beObject.Properties.BeginInit();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panel4.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.beTable);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.beObject);
    this.panel1.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.beTable, "beTable");
    this.beTable.Name = "beTable";
    this.beTable.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beTable.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.beTable.ButtonClick += new ButtonPressedEventHandler(this.beTable_ButtonClick);
    this.beTable.KeyDown += new KeyEventHandler(this.be_KeyDown);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.beObject, "beObject");
    this.beObject.Name = "beObject";
    this.beObject.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beObject.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.beObject.ButtonClick += new ButtonPressedEventHandler(this.beObject_ButtonClick);
    this.beObject.KeyDown += new KeyEventHandler(this.be_KeyDown);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panel2.Controls.Add((Control) this.panel3);
    this.panel2.Controls.Add((Control) this.panel4);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.panel3.Controls.Add((Control) this.xmlView);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.xmlView, "xmlView");
    this.xmlView.Name = "xmlView";
    this.panel4.Controls.Add((Control) this.bClose);
    this.panel4.Controls.Add((Control) this.bCalc);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this.bClose, "bClose");
    this.bClose.Name = "bClose";
    this.bClose.Click += new EventHandler(this.bClose_Click);
    componentResourceManager.ApplyResources((object) this.bCalc, "bCalc");
    this.bCalc.Name = "bCalc";
    this.bCalc.Click += new EventHandler(this.bCalc_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (TableTest);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.beTable.Properties.EndInit();
    this.beObject.Properties.EndInit();
    this.panel2.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void bClose_Click(object sender, EventArgs e) => this.Close();

  private void be_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.KeyCode.Equals((object) Keys.Delete))
      return;
    (sender as ButtonEdit).EditValue = (object) -1;
    this.CheckButton();
  }

  private void ShowXml(XmlDocument doc)
  {
    this.xmlView.Nodes.Clear();
    this.AddNodeAndChildren((XmlNode) doc.DocumentElement, (TreeNode) null);
  }

  private void AddNodeAndChildren(XmlNode xnode, TreeNode tnode)
  {
    string attribs = "";
    if (xnode.Attributes != null)
      attribs = this.CollectAttributes(xnode);
    TreeNode tnode1 = this.AddNode(xnode, tnode, attribs);
    if (!xnode.HasChildNodes)
      return;
    foreach (XmlNode childNode in xnode.ChildNodes)
      this.AddNodeAndChildren(childNode, tnode1);
  }

  private TreeNode AddNode(XmlNode xnode, TreeNode tnode, string attribs)
  {
    TreeNodeCollection treeNodeCollection1 = tnode == null ? this.xmlView.Nodes : tnode.Nodes;
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
      str += $"    {attribute.Name}=\"{attribute.Value}\"";
    return str;
  }

  private void CheckButton()
  {
    this.bCalc.Enabled = this.beObject.EditValue != null && !this.beObject.EditValue.Equals((object) -1) && this.beTable.EditValue != null && !this.beTable.EditValue.Equals((object) -1);
  }

  private void beObject_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_60"), string.Empty, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    this.beObject.EditValue = (object) numArray[0];
    this.CheckButton();
  }

  private void beTable_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_61"), string.Empty, ExpertConsts.Consts.objTable, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    this.beTable.EditValue = (object) numArray[0];
    this.CheckButton();
  }

  private void bCalc_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(ServicesManager.GetService(typeof (IExpertUser)) is IExpertUser service))
        return;
      using (IExpertTask expertTask = service.GetExpertTask())
      {
        expertTask.TraceFlags = ExpertTraceFlags.TraceTables;
        ArrayList arrayList = new ArrayList((ICollection) expertTask.CalcTableTest(Convert.ToInt64(this.beObject.EditValue), Convert.ToInt64(this.beTable.EditValue)));
        this.ShowXml(expertTask.GetTraceInfo());
      }
    }
  }
}
