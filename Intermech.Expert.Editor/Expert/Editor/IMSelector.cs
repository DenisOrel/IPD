// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.IMSelector
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Imbase.Controls;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class IMSelector : Form
{
  public bool NoCatalog;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private TreeBuilder treeBuilder;
  private ToolTip toolTip1;
  private Splitter splitter1;
  private Panel panel2;
  private Panel panel3;
  private ImagePanel imagePanel;
  private Splitter splitter2;
  private TreeView tv;
  private TableView tableView;

  public IMSelector() => this.InitializeComponent();

  private void treeBuilder_Selected(object sender, TreeViewSelectEventArgs e)
  {
    if (e.NodeInfo != null)
    {
      this.imagePanel.ObjectId = e.NodeInfo.ObjectId;
      if (e.NodeInfo.IsTableReference)
        this.tableView.ObjectId = e.NodeInfo.ObjectId;
      else
        this.tableView.ObjectId = -1L;
    }
    else
      this.imagePanel.ObjectId = 0L;
  }

  public bool Execute4Objects(int objType, ref List<long> refIds)
  {
    this.treeBuilder.ShowTreeForType(objType);
    this.treeBuilder.LoadFullTree(refIds);
    this.NoCatalog = false;
    if (this.ShowDialog() != DialogResult.OK)
      return false;
    refIds = new List<long>((IEnumerable<long>) this.treeBuilder.Checked);
    return true;
  }

  public bool Execute4Attribute(int objType, int attrType, ref List<long> refIds)
  {
    long[] foldersList = ((IImbaseSelector) ServicesManager.GetService(typeof (IImbaseSelector))).CatalogsForObjectAtt(objType, attrType);
    if (foldersList == null || foldersList.Length == 0)
    {
      this.NoCatalog = true;
      return false;
    }
    this.NoCatalog = false;
    this.treeBuilder.ShowList(foldersList);
    this.treeBuilder.LoadFullTree(refIds);
    if (this.ShowDialog() != DialogResult.OK)
      return false;
    refIds = new List<long>((IEnumerable<long>) this.treeBuilder.Checked);
    return true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IMSelector));
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.treeBuilder = new TreeBuilder(this.components);
    this.tv = new TreeView();
    this.toolTip1 = new ToolTip(this.components);
    this.splitter1 = new Splitter();
    this.panel2 = new Panel();
    this.panel3 = new Panel();
    this.imagePanel = new ImagePanel();
    this.splitter2 = new Splitter();
    this.tableView = new TableView();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.OK;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Cancel;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.treeBuilder.Catalogs = new long[0];
    this.treeBuilder.Checked = new long[0];
    this.treeBuilder.TreeView = this.tv;
    this.treeBuilder.Selected += new SelectEventHandler(this.treeBuilder_Selected);
    this.tv.CheckBoxes = true;
    componentResourceManager.ApplyResources((object) this.tv, "tv");
    this.tv.Name = "tv";
    this.tv.Sorted = true;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.panel2.Controls.Add((Control) this.panel3);
    this.panel2.Controls.Add((Control) this.tv);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.panel3.Controls.Add((Control) this.imagePanel);
    this.panel3.Controls.Add((Control) this.splitter2);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    this.imagePanel.BackColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.imagePanel, "imagePanel");
    this.imagePanel.Name = "imagePanel";
    this.imagePanel.ObjectId = -1L;
    componentResourceManager.ApplyResources((object) this.splitter2, "splitter2");
    this.splitter2.Name = "splitter2";
    this.splitter2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tableView, "tableView");
    this.tableView.FollowSelectMode = ImFollowSelectMode.imfsmFirstRow;
    this.tableView.Name = "tableView";
    this.tableView.RecordId = -1L;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableView);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.Name = nameof (IMSelector);
    this.Tag = (object) " ";
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
