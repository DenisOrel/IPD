// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.SelectNodeDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Controls;
using Intermech.Interfaces.Document;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог выбора узлов документа</summary>
public class SelectNodeDlg : Form
{
  private IContainer components;
  private Button btnCancel;
  private TreeList treeList;
  private TreeListColumn columnName;
  private Button btnOk;
  private DocumentTreeNode treeRoot;
  private System.Type nodeType;
  private NodeFilter nodeFilter;

  /// <summary>Конструктор</summary>
  public SelectNodeDlg()
  {
    this.InitializeComponent();
    Color color1 = this.treeList.Styles["Row"].ForeColor;
    int num1 = 2;
    int red1 = Math.Min((int) color1.R + ((int) byte.MaxValue - (int) color1.R) / num1, (int) byte.MaxValue);
    int num2 = Math.Min((int) color1.G + ((int) byte.MaxValue - (int) color1.G) / num1, (int) byte.MaxValue);
    int num3 = Math.Min((int) color1.B + ((int) byte.MaxValue - (int) color1.B) / num1, (int) byte.MaxValue);
    int green1 = num2;
    int blue1 = num3;
    color1 = Color.FromArgb(red1, green1, blue1);
    this.treeList.Styles["DisabledNode"].ForeColor = color1;
    Color color2 = this.treeList.Styles["FocusedRow"].ForeColor;
    int red2 = Math.Min((int) color2.R + ((int) byte.MaxValue - (int) color2.R) / num1, (int) byte.MaxValue);
    int num4 = Math.Min((int) color2.G + ((int) byte.MaxValue - (int) color2.G) / num1, (int) byte.MaxValue);
    int num5 = Math.Min((int) color2.B + ((int) byte.MaxValue - (int) color2.B) / num1, (int) byte.MaxValue);
    int green2 = num4;
    int blue2 = num5;
    color2 = Color.FromArgb(red2, green2, blue2);
    this.treeList.Styles["DisabledFocusedNode"].ForeColor = color2;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectNodeDlg));
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.treeList = new TreeList();
    this.columnName = new TreeListColumn();
    this.treeList.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    componentResourceManager.ApplyResources((object) this.treeList, "treeList");
    this.treeList.Columns.AddRange(new TreeListColumn[1]
    {
      this.columnName
    });
    this.treeList.Name = "treeList";
    this.treeList.Styles.AddReplace("GroupButton", (object) new ViewStyle("GroupButton", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, Color.White, Color.Gray));
    this.treeList.Styles.AddReplace("DisabledFocusedNode", (object) new ViewStyle("DisabledFocusedNode", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "FocusedRow", StyleOptions.StyleEnabled | StyleOptions.UseForeColor, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.WindowText));
    this.treeList.Styles.AddReplace("DisabledNode", (object) new ViewStyle("DisabledNode", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "Row", StyleOptions.StyleEnabled, true, false, false, DevExpress.IM.Utils.HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this.treeList.GetCustomNodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.treeList_GetCustomNodeCellStyle);
    this.treeList.SelectionChanged += new EventHandler(this.treeList_SelectionChanged);
    this.treeList.CompareNodeValues += new CompareNodeValuesEventHandler(this.treeList_CompareNodeValues);
    this.treeList.CustomDrawNodeButton += new CustomDrawNodeButtonEventHandler(this.treeList_CustomDrawNodeButton);
    this.treeList.AfterFocusNode += new NodeEventHandler(this.treeList_AfterFocusNode);
    componentResourceManager.ApplyResources((object) this.columnName, "columnName");
    this.columnName.Name = "columnName";
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.treeList);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectNodeDlg);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.treeList.EndInit();
    this.ResumeLayout(false);
  }

  private void treeList_CompareNodeValues(object sender, CompareNodeValuesEventArgs e)
  {
    TreeListNodeWrapper tag1 = e.Node1.Tag as TreeListNodeWrapper;
    TreeListNodeWrapper tag2 = e.Node2.Tag as TreeListNodeWrapper;
    if (tag1 != null && tag2 != null)
      e.Result = tag1.SortIndex - tag2.SortIndex;
    else
      e.Result = 0;
  }

  /// <summary>Корень дерева которое нужно отобразить</summary>
  public DocumentTreeNode TreeRoot
  {
    [DebuggerStepThrough] get => this.treeRoot;
    set
    {
      if (this.treeRoot == value)
        return;
      this.treeRoot = value;
      this.UpdateTree();
    }
  }

  /// <summary>Очистить дерево</summary>
  public void ClearTree()
  {
    for (int index = this.treeList.Nodes.Count - 1; index > -1; --index)
    {
      if (this.treeList.Nodes[index].Tag is TreeListNodeWrapper tag)
        tag.RemoveNode();
      else
        this.treeList.Nodes.RemoveAt(index);
    }
  }

  /// <summary>Обновить дерево</summary>
  public void UpdateTree()
  {
    this.treeList.BeginUpdate();
    this.treeList.BeginUnboundLoad();
    this.ClearTree();
    if (this.treeRoot != null)
      new TreeListNodeWrapper(this.treeList.AppendNode((object) new object[1]
      {
        (object) this.treeRoot.GetDefautCaption()
      }, (TreeListNode) null), this.nodeFilter)
      {
        DocumentNode = this.treeRoot,
        SortIndex = 0
      }.SynchronizeTree(true);
    if (this.treeList.Nodes.Count > 0)
      this.treeList.Nodes[0].Expanded = true;
    this.treeList.EndUpdate();
    this.treeList.EndUnboundLoad();
  }

  /// <summary>Раскрыть узлы дерева на заданное количество уровней</summary>
  /// <param name="node">Узел TreeList с которого нужно раскрыть</param>
  /// <param name="levels">Количестов уровеней от заданного узла</param>
  public void ExpandNodeToLevel(TreeListNode node, int levels)
  {
    if (levels <= -1)
      return;
    node.Expanded = true;
    if (levels <= 0)
      return;
    for (int index = 0; index < node.Nodes.Count; ++index)
      this.ExpandNodeToLevel(node.Nodes[index], levels - 1);
  }

  /// <summary>Найти узел TreeList для заданного узла документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Узел TreeList</returns>
  public TreeListNode SearchNode(DocumentTreeNode docNode)
  {
    TreeListNode treeListNode = (TreeListNode) null;
    for (int index = 0; index < this.treeList.Nodes.Count && treeListNode == null; ++index)
    {
      if (this.treeList.Nodes[index].Tag is TreeListNodeWrapper tag)
        treeListNode = tag.FindNode(docNode);
    }
    return treeListNode;
  }

  /// <summary>Выполнить диалог выбора узла</summary>
  /// <param name="nodeType">Тип выбираемого узла</param>
  /// <param name="currentNode">Текущий выбранный узел</param>
  /// <param name="rootNode">Корень дерева выбора</param>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="expandLevel">Количество уровней которые нужно раскрыть</param>
  /// <returns>Выбранный узел</returns>
  public DocumentTreeNode SelectNode(
    System.Type nodeType,
    DocumentTreeNode currentNode,
    DocumentTreeNode rootNode,
    string caption,
    int expandLevel,
    TypeNodeFilter filter = null)
  {
    if (caption != null)
      this.Text = caption;
    this.nodeType = nodeType;
    this.nodeFilter = (NodeFilter) (filter ?? new TypeNodeFilter(nodeType));
    this.TreeRoot = rootNode;
    if (this.treeList.Nodes.Count > 0)
    {
      this.ExpandNodeToLevel(this.treeList.Nodes[0], expandLevel);
      if (currentNode != null)
        this.treeList.FocusedNode = this.SearchNode(currentNode);
      DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
      if (this.ShowDialog() == DialogResult.OK)
      {
        if (this.treeList.FocusedNode != null)
        {
          DocumentTreeNode docNode = TreeListNodeWrapper.GetDocNode(this.treeList.FocusedNode);
          if (docNode != null)
          {
            if (docNode.FilterCheck((System.Type[]) null, new System.Type[1]
            {
              nodeType
            }))
              documentTreeNode = docNode;
          }
        }
      }
      else
        documentTreeNode = currentNode;
      this.treeList.BeginUpdate();
      this.treeList.BeginUnboundLoad();
      this.ClearTree();
      this.treeList.EndUpdate();
      this.treeList.EndUnboundLoad();
      return documentTreeNode;
    }
    int num = (int) IMMessageBox.Show(caption, "   Нет данных для выбора.   ", new IMMessageBoxButton[1]
    {
      new IMMessageBoxButton("OK", DialogResult.OK)
    });
    return (DocumentTreeNode) null;
  }

  /// <summary>Создать и выполнить диалог</summary>
  /// <param name="nodeType">Тип выбираемого узла</param>
  /// <param name="currentNode">Текущий выбранный узел</param>
  /// <param name="rootNode">Корень дерева выбора</param>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="expandLevel">Количество уровней которые нужно раскрыть</param>
  /// <param name="filter">Фильтр узлов дерева</param>
  /// <returns>Выбранный узел</returns>
  public static DocumentTreeNode Execute(
    System.Type nodeType,
    DocumentTreeNode currentNode,
    DocumentTreeNode rootNode,
    string caption,
    int expandLevel,
    TypeNodeFilter filter = null)
  {
    if (rootNode == null)
      throw new ArgumentNullException(nameof (rootNode));
    SelectNodeDlg selectNodeDlg = new SelectNodeDlg();
    DocumentTreeNode documentTreeNode = selectNodeDlg.SelectNode(nodeType, currentNode, rootNode, caption, expandLevel, filter);
    if (selectNodeDlg.IsDisposed)
      return documentTreeNode;
    selectNodeDlg.Dispose();
    return documentTreeNode;
  }

  private void treeList_CustomDrawNodeButton(object sender, CustomDrawNodeButtonEventArgs e)
  {
    e.Graphics.FillRectangle(e.Style.BackBrush, e.Bounds);
    Rectangle bounds1 = e.Bounds;
    --bounds1.Width;
    --bounds1.Height;
    e.Graphics.DrawRectangle(e.Style.ForePen, bounds1);
    int int32 = Convert.ToInt32((float) ((double) e.Bounds.Width * 2.0 / 5.0 / 2.0));
    Point pt1;
    ref Point local1 = ref pt1;
    Rectangle bounds2 = e.Bounds;
    int x1 = bounds2.X + int32;
    bounds2 = e.Bounds;
    int y1 = bounds2.Y;
    bounds2 = e.Bounds;
    int num1 = bounds2.Height / 2;
    int y2 = y1 + num1;
    local1 = new Point(x1, y2);
    Point pt2;
    ref Point local2 = ref pt2;
    int x2 = bounds1.Right - int32;
    Rectangle bounds3 = e.Bounds;
    int y3 = bounds3.Y;
    bounds3 = e.Bounds;
    int num2 = bounds3.Height / 2;
    int y4 = y3 + num2;
    local2 = new Point(x2, y4);
    e.Graphics.DrawLine(e.Style.ForePen, pt1, pt2);
    if (!e.Expanded)
    {
      pt1 = new Point(e.Bounds.X + e.Bounds.Width / 2, e.Bounds.Y + int32);
      ref Point local3 = ref pt2;
      Rectangle bounds4 = e.Bounds;
      int x3 = bounds4.X;
      bounds4 = e.Bounds;
      int num3 = bounds4.Width / 2;
      int x4 = x3 + num3;
      int y5 = bounds1.Bottom - int32;
      local3 = new Point(x4, y5);
      e.Graphics.DrawLine(e.Style.ForePen, pt1, pt2);
    }
    e.Handled = true;
  }

  private void treeList_SelectionChanged(object sender, EventArgs e)
  {
  }

  private void treeList_AfterFocusNode(object sender, NodeEventArgs e)
  {
    if (this.treeList.FocusedNode != null)
    {
      DocumentTreeNode documentNode = ((TreeListNodeWrapper) this.treeList.FocusedNode.Tag).DocumentNode;
      Button btnOk = this.btnOk;
      int num;
      if (documentNode != null)
        num = documentNode.FilterCheck((System.Type[]) null, new System.Type[1]
        {
          this.nodeType
        }) ? 1 : 0;
      else
        num = 0;
      btnOk.Enabled = num != 0;
    }
    else
      this.btnOk.Enabled = false;
  }

  private void treeList_GetCustomNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
  {
    if (!(e.Node.Tag is TreeListNodeWrapper tag) || tag.CheckNode())
      return;
    TreeList treeList = e.Node.TreeList;
    if (treeList.FocusedNode == e.Node)
      e.Style = treeList.Styles["DisabledFocusedNode"];
    else
      e.Style = treeList.Styles["DisabledNode"];
  }
}
