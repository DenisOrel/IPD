// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.BlankTreeViewDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Bars;
using Intermech.Docking;
using Intermech.Document.Model.ImportBlanks;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Панель с деревом зачитанных из бланка элементов</summary>
public class BlankTreeViewDlg : DockControl, ISkipTargetActivate
{
  private BlankLoader blankTree;
  /// <summary>PropertyGrid</summary>
  public PropertyGrid propertyGrid;
  private TreeListColumn treeListColumn1;
  private TreeList treeList;
  private IContainer components;

  /// <summary>Загрузчик бланка</summary>
  public BlankLoader BlankTree
  {
    [DebuggerStepThrough] get => this.blankTree;
    set
    {
      if (this.blankTree == value)
        return;
      this.blankTree = value;
      this.UpdateTree();
    }
  }

  /// <summary>Дерево TreeList для бланка</summary>
  public TreeList BlankTreeList
  {
    [DebuggerStepThrough] get => this.treeList;
  }

  /// <summary>Конструктор</summary>
  public BlankTreeViewDlg() => this.InitializeComponent();

  /// <summary>Конструктор</summary>
  public void UpdateTree()
  {
    this.treeList.BeginUpdate();
    this.treeList.BeginUnboundLoad();
    for (int index = this.treeList.Nodes.Count - 1; index > -1; --index)
    {
      if (this.treeList.Nodes[index].Tag is BlankTreeListNodeWrapper tag)
        tag.RemoveNode();
      else
        this.treeList.Nodes.RemoveAt(index);
    }
    if (this.BlankTree != null)
    {
      for (int index = 0; index < this.BlankTree.PrimitiveList.Count; ++index)
      {
        PrimitiveBase primitive = this.BlankTree.PrimitiveList[index];
        new BlankTreeListNodeWrapper(this.treeList.AppendNode((object) new object[1]
        {
          (object) BlankTreeListNodeWrapper.GetDefaultCaption(primitive)
        }, (TreeListNode) null))
        {
          BlankNode = primitive
        }.SynchronizeTree(true);
      }
      TreeListNode owner = this.treeList.AppendNode((object) new object[1]
      {
        (object) LocalizationHolder.rm.GetString("Document.Model_8")
      }, (TreeListNode) null);
      new BlankTreeListNodeWrapper(owner)
      {
        BlankNode = ((PrimitiveBase) this.BlankTree.WorkSpace)
      }.SynchronizeTree(true);
      owner[(object) 0] = (object) LocalizationHolder.rm.GetString("Document.Model_9");
    }
    this.treeList.EndUpdate();
    this.treeList.EndUnboundLoad();
  }

  /// <summary>Найти узел TreeList</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns>Узел TreeList</returns>
  public TreeListNode SearchNode(PrimitiveBase docNode)
  {
    TreeListNode treeListNode = (TreeListNode) null;
    for (int index = 0; index < this.treeList.Nodes.Count && treeListNode == null; ++index)
    {
      if (this.treeList.Nodes[index].Tag is BlankTreeListNodeWrapper tag)
        treeListNode = tag.FindNode(docNode);
    }
    return treeListNode;
  }

  private void BlankTreeViewDlg_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyData != Keys.F5)
      return;
    this.UpdateTree();
  }

  private void treeList_SelectionChanged(object sender, EventArgs e)
  {
    if (this.propertyGrid == null || this.treeList.Selection.Count <= 0)
      return;
    this.propertyGrid.SelectedObject = (object) BlankTreeListNodeWrapper.GetDocNode(this.treeList.Selection[0]);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BlankTreeViewDlg));
    this.treeList = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeList.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.treeList, "treeList");
    this.treeList.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeList.Name = "treeList";
    this.treeList.Styles.AddReplace("GroupButton", (object) new ViewStyle("GroupButton", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, DevExpress.IM.Utils.VertAlignment.Center, (Image) null, Color.White, Color.Gray));
    this.treeList.SelectionChanged += new EventHandler(this.treeList_SelectionChanged);
    this.treeList.CustomDrawNodeButton += new CustomDrawNodeButtonEventHandler(this.treeList_CustomDrawNodeButton);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    this.Controls.Add((Control) this.treeList);
    this.HideOnClose = true;
    this.Name = nameof (BlankTreeViewDlg);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.KeyDown += new KeyEventHandler(this.BlankTreeViewDlg_KeyDown);
    this.treeList.EndInit();
    this.ResumeLayout(false);
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
}
