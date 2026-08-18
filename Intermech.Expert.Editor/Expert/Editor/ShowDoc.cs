// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ShowDoc
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors;
using Intermech.Document.Model;
using Intermech.Document.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Summary description for ShowDoc.</summary>
public class ShowDoc : Form
{
  /// <summary>
  /// 
  /// </summary>
  private DocTraceInfo _docItem;
  /// <summary>Режим заполнения / обновления документов</summary>
  private bool _docUpdateMode;
  private ImageList imageList1;
  private IContainer components;
  private ImDocument doc;
  private SaveFileDialog saveDlg;
  private TabControl pcDocInfo;
  private TabPage tabPageDoc;
  private TabPage tabPageInfo;
  private TreeView xmlView;
  private Panel panel1;
  private Label labelList;
  private SimpleButton btnLast;
  private SimpleButton btnNext;
  private SimpleButton btnPrev;
  private SimpleButton btnFirst;
  private SimpleButton btnSaveDoc;
  private Panel panel2;
  private ContextMenuStrip localMenu;
  private ToolStripMenuItem saveInfoMenuItem;
  private SaveFileDialog saveTraceDialog;
  private DocumentControl docControl;
  private TabPage tabPageReport;
  private ListBox lbReport;
  private TreeView tvDocItems;
  private Splitter splitter;

  /// <summary>Инициализация кастом контролов</summary>
  private void InitCustomControls()
  {
    this.docControl = new DocumentControl();
    this.docControl.GetCustomElementContextMenu += new GetCustomElementContextMenu_EventHandler(this.docControl_GetCustomElementContextMenu);
    this.docControl.Dock = DockStyle.Fill;
    this.tabPageDoc.Controls.Add((Control) this.docControl);
    this.docControl.ReadOnly = true;
    this.docControl.ActivePageChanged += new ActivePageChanged_EventHandler(this.docControl_ActivePageChanged);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ShowDoc));
    this.imageList1 = new ImageList(this.components);
    this.saveDlg = new SaveFileDialog();
    this.pcDocInfo = new TabControl();
    this.localMenu = new ContextMenuStrip(this.components);
    this.saveInfoMenuItem = new ToolStripMenuItem();
    this.tabPageInfo = new TabPage();
    this.xmlView = new TreeView();
    this.tabPageDoc = new TabPage();
    this.panel2 = new Panel();
    this.panel1 = new Panel();
    this.labelList = new Label();
    this.btnLast = new SimpleButton();
    this.btnNext = new SimpleButton();
    this.btnPrev = new SimpleButton();
    this.btnFirst = new SimpleButton();
    this.btnSaveDoc = new SimpleButton();
    this.tabPageReport = new TabPage();
    this.lbReport = new ListBox();
    this.saveTraceDialog = new SaveFileDialog();
    this.tvDocItems = new TreeView();
    this.splitter = new Splitter();
    this.pcDocInfo.SuspendLayout();
    this.localMenu.SuspendLayout();
    this.tabPageInfo.SuspendLayout();
    this.tabPageDoc.SuspendLayout();
    this.panel1.SuspendLayout();
    this.tabPageReport.SuspendLayout();
    this.SuspendLayout();
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Magenta;
    this.imageList1.Images.SetKeyName(0, "");
    this.imageList1.Images.SetKeyName(1, "");
    this.imageList1.Images.SetKeyName(2, "");
    this.imageList1.Images.SetKeyName(3, "");
    this.imageList1.Images.SetKeyName(4, "");
    this.saveDlg.DefaultExt = "IMDX";
    this.saveDlg.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.saveDlg, "saveDlg");
    this.pcDocInfo.ContextMenuStrip = this.localMenu;
    this.pcDocInfo.Controls.Add((Control) this.tabPageInfo);
    this.pcDocInfo.Controls.Add((Control) this.tabPageDoc);
    this.pcDocInfo.Controls.Add((Control) this.tabPageReport);
    componentResourceManager.ApplyResources((object) this.pcDocInfo, "pcDocInfo");
    this.pcDocInfo.Name = "pcDocInfo";
    this.pcDocInfo.SelectedIndex = 0;
    this.localMenu.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.saveInfoMenuItem
    });
    this.localMenu.Name = "localMenu";
    componentResourceManager.ApplyResources((object) this.localMenu, "localMenu");
    this.saveInfoMenuItem.Name = "saveInfoMenuItem";
    componentResourceManager.ApplyResources((object) this.saveInfoMenuItem, "saveInfoMenuItem");
    this.saveInfoMenuItem.Click += new EventHandler(this.saveInfoMenuItem_Click);
    this.tabPageInfo.Controls.Add((Control) this.xmlView);
    componentResourceManager.ApplyResources((object) this.tabPageInfo, "tabPageInfo");
    this.tabPageInfo.Name = "tabPageInfo";
    this.tabPageInfo.UseVisualStyleBackColor = true;
    this.xmlView.ContextMenuStrip = this.localMenu;
    componentResourceManager.ApplyResources((object) this.xmlView, "xmlView");
    this.xmlView.Name = "xmlView";
    this.tabPageDoc.Controls.Add((Control) this.panel2);
    this.tabPageDoc.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.tabPageDoc, "tabPageDoc");
    this.tabPageDoc.Name = "tabPageDoc";
    this.tabPageDoc.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.panel1.Controls.Add((Control) this.labelList);
    this.panel1.Controls.Add((Control) this.btnLast);
    this.panel1.Controls.Add((Control) this.btnNext);
    this.panel1.Controls.Add((Control) this.btnPrev);
    this.panel1.Controls.Add((Control) this.btnFirst);
    this.panel1.Controls.Add((Control) this.btnSaveDoc);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.labelList, "labelList");
    this.labelList.Name = "labelList";
    componentResourceManager.ApplyResources((object) this.btnLast, "btnLast");
    this.btnLast.ImageIndex = 4;
    this.btnLast.ImageList = this.imageList1;
    this.btnLast.Name = "btnLast";
    this.btnLast.TabStop = false;
    this.btnLast.Click += new EventHandler(this.btnLast_Click);
    componentResourceManager.ApplyResources((object) this.btnNext, "btnNext");
    this.btnNext.ImageIndex = 3;
    this.btnNext.ImageList = this.imageList1;
    this.btnNext.Name = "btnNext";
    this.btnNext.TabStop = false;
    this.btnNext.Click += new EventHandler(this.btnNext_Click);
    componentResourceManager.ApplyResources((object) this.btnPrev, "btnPrev");
    this.btnPrev.ImageIndex = 2;
    this.btnPrev.ImageList = this.imageList1;
    this.btnPrev.Name = "btnPrev";
    this.btnPrev.TabStop = false;
    this.btnPrev.Click += new EventHandler(this.btnPrev_Click);
    componentResourceManager.ApplyResources((object) this.btnFirst, "btnFirst");
    this.btnFirst.ImageIndex = 1;
    this.btnFirst.ImageList = this.imageList1;
    this.btnFirst.Name = "btnFirst";
    this.btnFirst.TabStop = false;
    this.btnFirst.Click += new EventHandler(this.btnFirst_Click);
    this.btnSaveDoc.ImageIndex = 0;
    this.btnSaveDoc.ImageList = this.imageList1;
    componentResourceManager.ApplyResources((object) this.btnSaveDoc, "btnSaveDoc");
    this.btnSaveDoc.Name = "btnSaveDoc";
    this.btnSaveDoc.TabStop = false;
    this.btnSaveDoc.Click += new EventHandler(this.btnSaveDoc_Click);
    this.tabPageReport.Controls.Add((Control) this.lbReport);
    componentResourceManager.ApplyResources((object) this.tabPageReport, "tabPageReport");
    this.tabPageReport.Name = "tabPageReport";
    this.tabPageReport.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.lbReport, "lbReport");
    this.lbReport.FormattingEnabled = true;
    this.lbReport.Name = "lbReport";
    this.saveTraceDialog.DefaultExt = "XML";
    this.saveTraceDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.saveTraceDialog, "saveTraceDialog");
    componentResourceManager.ApplyResources((object) this.tvDocItems, "tvDocItems");
    this.tvDocItems.FullRowSelect = true;
    this.tvDocItems.HideSelection = false;
    this.tvDocItems.MinimumSize = new Size(50, 0);
    this.tvDocItems.Name = "tvDocItems";
    this.tvDocItems.Nodes.AddRange(new TreeNode[1]
    {
      (TreeNode) componentResourceManager.GetObject("tvDocItems.Nodes")
    });
    this.tvDocItems.AfterSelect += new TreeViewEventHandler(this.tvDocItems_AfterSelect);
    componentResourceManager.ApplyResources((object) this.splitter, "splitter");
    this.splitter.Name = "splitter";
    this.splitter.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.splitter);
    this.Controls.Add((Control) this.pcDocInfo);
    this.Controls.Add((Control) this.tvDocItems);
    this.Name = nameof (ShowDoc);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.ShowDoc_FormClosed);
    this.pcDocInfo.ResumeLayout(false);
    this.localMenu.ResumeLayout(false);
    this.tabPageInfo.ResumeLayout(false);
    this.tabPageDoc.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.tabPageReport.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      this.xmlView.Nodes.Clear();
      if (this.docControl != null)
      {
        this.docControl.GetCustomElementContextMenu -= new GetCustomElementContextMenu_EventHandler(this.docControl_GetCustomElementContextMenu);
        this.docControl.ActivePageChanged -= new ActivePageChanged_EventHandler(this.docControl_ActivePageChanged);
      }
      if (this._docItem != null)
      {
        this._docItem.ClearData();
        this._docItem = (DocTraceInfo) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary>Получение информации о тек. документе</summary>
  /// <returns></returns>
  private DocTraceInfo GetSelectedDocItem()
  {
    TreeNode selectedNode = this.tvDocItems.SelectedNode;
    return selectedNode == null ? (DocTraceInfo) null : selectedNode.Tag as DocTraceInfo;
  }

  /// <summary>Заполнение информации по док. в дэрэво</summary>
  /// <param name="docItem"></param>
  private void FillDocItem(DocTraceInfo docItem)
  {
    this._docUpdateMode = true;
    this.tvDocItems.BeginUpdate();
    try
    {
      this.tvDocItems.Nodes.Clear();
      this.FillDocItemData((TreeNode) null, docItem);
    }
    finally
    {
      this._docUpdateMode = false;
      this.tvDocItems.EndUpdate();
    }
    if (this.tvDocItems.Nodes.Count > 0)
      this.tvDocItems.SelectedNode = this.tvDocItems.Nodes[0];
    this.UpdateDocItemData();
  }

  /// <summary>Заполнение информации по док. в дэрэво</summary>
  /// <param name="ownerNode"></param>
  /// <param name="docItem"></param>
  private void FillDocItemData(TreeNode ownerNode, DocTraceInfo docItem)
  {
    if (docItem == null)
      return;
    TreeNode treeNode = new TreeNode(docItem.Text);
    treeNode.Tag = (object) docItem;
    if (ownerNode != null)
      ownerNode.Nodes.Add(treeNode);
    else
      this.tvDocItems.Nodes.Add(treeNode);
    foreach (DocTraceInfo childItem in docItem.ChildItems)
      this.FillDocItemData(treeNode, childItem);
  }

  /// <summary>Обновление содержимого документов</summary>
  private void UpdateDocItemData()
  {
    DocTraceInfo selectedDocItem = this.GetSelectedDocItem();
    if (selectedDocItem == null || selectedDocItem.Doc == null)
    {
      this.docControl.SetDocument((ImDocument) null, false, false);
      this.docControl.Parent = (Control) null;
    }
    else
    {
      this.docControl.SetDocument(selectedDocItem.Doc, false, false);
      this.docControl.Parent = (Control) this.tabPageDoc;
    }
    this.UpdateDocPages();
    this.ShowXml(selectedDocItem?.TraceInfo, this.xmlView);
    this.lbReport.BeginUpdate();
    try
    {
      this.lbReport.Items.Clear();
      if (selectedDocItem == null || selectedDocItem.Report == null)
        return;
      this.lbReport.Items.AddRange((object[]) selectedDocItem.Report);
    }
    finally
    {
      this.lbReport.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateDocPages()
  {
    ImDocument document = this.docControl.Document;
    if (document == null)
    {
      this.labelList.Text = string.Empty;
      this.btnSaveDoc.Enabled = this.btnFirst.Enabled = this.btnPrev.Enabled = this.btnNext.Enabled = this.btnLast.Enabled = false;
    }
    else
    {
      int count = document.Nodes.Count;
      int pageNumber = this.docControl.ActivePage.PageNumber;
      this.labelList.Text = $"{Convert.ToString(pageNumber)}/{Convert.ToString(count)}";
      this.btnFirst.Enabled = pageNumber > 1;
      this.btnPrev.Enabled = pageNumber > 1;
      this.btnNext.Enabled = pageNumber < count;
      this.btnLast.Enabled = pageNumber < count;
      this.btnSaveDoc.Enabled = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void GotoDocPageNo(int pageNo)
  {
    if (this.docControl == null || this.docControl.Document == null)
      return;
    this.docControl.ActivePage = (Page) this.docControl.Document.Nodes[pageNo];
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="doc"></param>
  /// <param name="xView"></param>
  private void ShowXml(XmlDocument doc, TreeView xView)
  {
    if (xView == null)
      return;
    xView.BeginUpdate();
    try
    {
      xView.Nodes.Clear();
      if (doc == null)
        return;
      this.AddNodeAndChildren((XmlNode) doc.DocumentElement, (TreeNode) null, xView);
    }
    finally
    {
      xView.EndUpdate();
      xView.CollapseAll();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xnode"></param>
  /// <param name="tnode"></param>
  /// <param name="xView"></param>
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

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xnode"></param>
  /// <param name="tnode"></param>
  /// <param name="attribs"></param>
  /// <param name="xView"></param>
  /// <returns></returns>
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

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xnode"></param>
  /// <returns></returns>
  private string CollectAttributes(XmlNode xnode)
  {
    string str = "";
    foreach (XmlAttribute attribute in (XmlNamedNodeMap) xnode.Attributes)
      str += $"    {attribute.Name}={attribute.Value}";
    return str;
  }

  /// <summary>
  /// 
  /// </summary>
  public ShowDoc()
  {
    this.InitializeComponent();
    this.InitCustomControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="docStream"></param>
  /// <param name="info"></param>
  public void Execute(MemoryStream docStream, XmlDocument info)
  {
    MemoryStream memoryStream = new MemoryStream();
    docStream.WriteTo((Stream) memoryStream);
    memoryStream.Position = 0L;
    this.doc = ImDocument.LoadFromXml((Stream) memoryStream, true, true, false);
    this.Execute(this.doc, info);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="doc"></param>
  /// <param name="info"></param>
  public void Execute(ImDocument doc, XmlDocument info)
  {
    this.Execute(doc, info, (List<string>) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="doc"></param>
  /// <param name="info"></param>
  /// <param name="report"></param>
  public void Execute(ImDocument doc, XmlDocument info, List<string> report)
  {
    this.DocItem = new DocTraceInfo(string.Empty, doc, info, report?.ToArray());
    this.tvDocItems.Visible = false;
    this.splitter.Visible = false;
    int num = (int) this.ShowDialog();
  }

  /// <summary>
  /// 
  /// </summary>
  public DocTraceInfo DocItem
  {
    get => this._docItem;
    set
    {
      if (this._docItem == value)
        return;
      this._docItem = value;
      this.FillDocItem(value);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void docControl_GetCustomElementContextMenu(
    object sender,
    GetCustomElementContextMenu_EventArgs e)
  {
    e.ContextMenuItems.Clear();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tvDocItems_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this._docUpdateMode)
      return;
    this.UpdateDocItemData();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnFirst_Click(object sender, EventArgs e)
  {
    if (this.docControl.Document == null)
      return;
    this.GotoDocPageNo(0);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnLast_Click(object sender, EventArgs e)
  {
    if (this.docControl.Document == null)
      return;
    this.GotoDocPageNo(this.docControl.Document.Nodes.Count - 1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnPrev_Click(object sender, EventArgs e)
  {
    if (this.docControl.Document == null)
      return;
    this.GotoDocPageNo(this.docControl.ActivePage.PageNumber - 1 - 1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnNext_Click(object sender, EventArgs e)
  {
    if (this.docControl.Document == null)
      return;
    this.GotoDocPageNo(this.docControl.ActivePage.PageNumber - 1 + 1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void docControl_ActivePageChanged(object sender, EventArgs e) => this.UpdateDocPages();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnSaveDoc_Click(object sender, EventArgs e)
  {
    ImDocument document = this.docControl.Document;
    if (document == null || this.saveDlg.ShowDialog() != DialogResult.OK)
      return;
    Stream stream;
    if ((stream = this.saveDlg.OpenFile()) == null)
      return;
    document.SaveToXml(stream);
    stream.Close();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void saveInfoMenuItem_Click(object sender, EventArgs e)
  {
    DocTraceInfo selectedDocItem = this.GetSelectedDocItem();
    if (selectedDocItem == null || selectedDocItem.TraceInfo == null || this.saveTraceDialog.ShowDialog() != DialogResult.OK)
      return;
    Stream outStream;
    if ((outStream = this.saveTraceDialog.OpenFile()) == null)
      return;
    selectedDocItem.TraceInfo.Save(outStream);
    outStream.Close();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ShowDoc_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.tvDocItems.Nodes.Clear();
    this.xmlView.Nodes.Clear();
    this.lbReport.Items.Clear();
    this.docControl.SetDocument((ImDocument) null, false, false);
    if (this._docItem == null)
      return;
    this._docItem.ClearData();
    this._docItem = (DocTraceInfo) null;
  }
}
