// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.DocumentTreeViewDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraTreeList;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Docking;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Панель с деревом документа</summary>
public class DocumentTreeViewDlg : DockControl, ISkipTargetActivate
{
  public static Guid DockGuid = new Guid("{33B1558D-67D6-4B97-A6A7-BD1AF7B5395A}");
  private MenuBar menuBar1;
  private ContextMenuBarItem elementContextMenu;
  private Intermech.VirtualTreeView.VirtualTreeView treeList;
  private Column columnElements;
  private CellEditor cellEditorTBe;
  private TextBox textBox1;
  private CellEditor cellEditorCBe;
  private CheckBox checkBox1;
  /// <summary>Количество блокировок обновления дерева</summary>
  public int BlockUpdateSelection;
  private List<DocumentTreeNode> prevSelection;
  /// <summary>Контрол документа</summary>
  public DocumentControl documentControl;
  private DocumentTreeNode treeRoot;
  private IContainer components;
  private Rectangle dragBoxFromMouseDown;
  private int levelDrag;
  private bool needUpdateTree = true;
  private Icon[] treeIcons;

  /// <summary>Конструктор</summary>
  public DocumentTreeViewDlg()
  {
    this.InitializeComponent();
    this.HideOnClose = true;
    this.treeIcons = new Icon[14];
    Bitmap[] bitmapArray = new Bitmap[10]
    {
      (Bitmap) (ImDocument.Icon as Bitmap).Clone(),
      (Bitmap) (Page.Icon as Bitmap).Clone(),
      (Bitmap) (TextBoxCreator.Icon as Bitmap).Clone(),
      (Bitmap) (LabelCreator.Icon as Bitmap).Clone(),
      (Bitmap) (TableCreator.Icon as Bitmap).Clone(),
      (Bitmap) (PolylineCreator.Icon as Bitmap).Clone(),
      (Bitmap) (ContainerCreator.Icon as Bitmap).Clone(),
      (Bitmap) (TableCreator.RowIcon as Bitmap).Clone(),
      PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.DocumentsComplect.png") as Bitmap,
      null
    };
    for (int index = 0; index < bitmapArray.Length - 1; ++index)
    {
      using (Bitmap bmp = new Bitmap(bitmapArray[index].Width, bitmapArray[index].Height))
      {
        using (Graphics graphics = Graphics.FromImage((Image) bmp))
        {
          graphics.DrawImage((Image) bitmapArray[index], 0, 0);
          this.treeIcons[index] = ImageHelper.BitmapToIcon(bmp);
        }
      }
    }
    this.Guid = DocumentTreeViewDlg.DockGuid;
  }

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    if (!this.Visible)
      return;
    this.UpdateTree();
    this.UpdateSelection();
  }

  /// <summary>Корень дерева которое нужно отобразить</summary>
  public DocumentTreeNode TreeRoot
  {
    [DebuggerStepThrough] get => this.treeRoot;
    set
    {
      if (this.treeRoot == value)
        return;
      if (value != null)
      {
        DocumentTreeNode documentTreeNode = value;
        while (documentTreeNode.Parent != null)
          documentTreeNode = documentTreeNode.Parent;
        this.treeRoot = documentTreeNode;
      }
      else
        this.treeRoot = value;
      this.UpdateTree();
    }
  }

  /// <summary>Обновить стуктуру дерева</summary>
  public void UpdateTree()
  {
    if (this.treeList.DataSource != this.TreeRoot)
    {
      if (this.treeList.DataSource is DocumentTreeNode dataSource)
      {
        if (dataSource is ImDocumentData)
          (dataSource as ImDocumentData).PageUnlocked -= new PageUnlocked_EventHandler(this.DocumentTreeViewDlg_PageUnlocked);
        dataSource.ChildNodeAdded -= new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded);
        dataSource.ChildNodeRemoved -= new ChildNodeRemoved_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved);
        dataSource.NameChanged -= new NameChanged_EventHandler(this.DocumentTreeViewDlg_NameChanged);
        dataSource.NodeRemoved -= new NodeRemoved_EventHandler(this.DocumentTreeViewDlg_NodeRemoved);
        dataSource.ChildNodePositionChanged -= new ChildNodePositionChanged_EventHandler(this.DocumentTreeViewDlg_ChildNodePositionChanged);
        if (dataSource is VisualNode)
          (dataSource as VisualNode).VisibleChanged -= new VisibleChanged_EventHandler(this.DocumentTreeViewDlg_VisibleChanged);
      }
      this.treeList.DataSource = (object) this.TreeRoot;
      if (this.TreeRoot != null)
      {
        if (this.TreeRoot is ImDocumentData)
          (this.TreeRoot as ImDocumentData).PageUnlocked += new PageUnlocked_EventHandler(this.DocumentTreeViewDlg_PageUnlocked);
        this.TreeRoot.ChildNodeAdded += new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded);
        this.TreeRoot.ChildNodeRemoved += new ChildNodeRemoved_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved);
        this.TreeRoot.NameChanged += new NameChanged_EventHandler(this.DocumentTreeViewDlg_NameChanged);
        this.TreeRoot.NodeRemoved += new NodeRemoved_EventHandler(this.DocumentTreeViewDlg_NodeRemoved);
        this.TreeRoot.ChildNodePositionChanged += new ChildNodePositionChanged_EventHandler(this.DocumentTreeViewDlg_ChildNodePositionChanged);
        if (this.TreeRoot is VisualNode)
          (this.TreeRoot as VisualNode).VisibleChanged += new VisibleChanged_EventHandler(this.DocumentTreeViewDlg_VisibleChanged);
      }
    }
    this.treeList.UpdateRows(true);
  }

  private void DocumentTreeViewDlg_PageUnlocked(object sender, PageUnlockedArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PageUnlocked_EventHandler(this.DocumentTreeViewDlg_PageUnlocked), sender, (object) e);
    else if (this.treeList.InvokeRequired)
      this.treeList.BeginInvoke((Delegate) new PageUnlocked_EventHandler(this.DocumentTreeViewDlg_PageUnlocked), sender, (object) e);
    else
      this.treeList.FindRow((object) e.Page)?.UpdateChildren(true, false);
  }

  /// <summary>Дерево TreeList</summary>
  public Intermech.VirtualTreeView.VirtualTreeView DocumentTreeList
  {
    [DebuggerStepThrough] get => this.treeList;
  }

  /// <summary>Контрол документа</summary>
  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get => this.documentControl;
    set
    {
      if (this.documentControl != null)
        this.documentControl.SelectionChanged -= new SelectionChanged_EventHandler(this.DocumentSelectionChanged);
      this.documentControl = value;
      if (this.documentControl == null)
        return;
      this.documentControl.SelectionChanged += new SelectionChanged_EventHandler(this.DocumentSelectionChanged);
    }
  }

  /// <summary>Обновить выделение</summary>
  public virtual void UpdateSelection()
  {
    if (this.BlockUpdateSelection > 0 || this.DocumentControl == null)
      return;
    this.SetSelection(this.DocumentControl.SelectedNodes);
  }

  private void SetSelection(List<DocumentTreeNode> docNodes)
  {
    ++this.BlockUpdateSelection;
    if (docNodes == null || docNodes.Count == 0)
    {
      this.treeList.SelectedItems.Clear();
    }
    else
    {
      List<DocumentTreeNode> documentTreeNodeList = !docNodes[0].IsVirtualNode ? docNodes : docNodes[0].GetNodesFromVirtualNode();
      for (int index = 0; index < documentTreeNodeList.Count; ++index)
        this.treeList.FindRow((object) documentTreeNodeList[index])?.ExpandAncestors();
      this.treeList.SelectedItems = (IList) documentTreeNodeList;
      if (this.treeList.SelectedItems.Count > 0)
      {
        Row row = this.treeList.FindRow(this.treeList.SelectedItems[0]);
        if (row != null)
          this.treeList.FocusRow = row;
      }
    }
    --this.BlockUpdateSelection;
  }

  /// <summary>Обработать нажатие клавиши</summary>
  /// <param name="msg">Сообщение</param>
  /// <param name="keyData">Данные нажатой клавиши</param>
  /// <returns>true, если дальнейшая обработка не требуется</returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (keyData != Keys.Escape)
      return base.ProcessCmdKey(ref msg, keyData);
    this.DocumentControl.GotoParentElement();
    return true;
  }

  /// <summary>Обработчик события документа SelectionChanged</summary>
  /// <param name="sender">Объект вызвавший событие</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DocumentSelectionChanged(object sender, SelectionChanged_EventArgs e)
  {
    this.UpdateSelection();
  }

  private void DocumentTreeViewDlg_Closed(object sender, EventArgs e)
  {
    this.DocumentControl = (DocumentControl) null;
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

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.TreeRoot = (DocumentTreeNode) null;
      this.DocumentControl = (DocumentControl) null;
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Required method for Designer support - do not modify
  /// the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentTreeViewDlg));
    this.menuBar1 = new MenuBar();
    this.elementContextMenu = new ContextMenuBarItem();
    this.treeList = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnElements = new Column();
    this.cellEditorTBe = new CellEditor();
    this.textBox1 = new TextBox();
    this.cellEditorCBe = new CellEditor();
    this.checkBox1 = new CheckBox();
    this.treeList.BeginInit();
    this.SuspendLayout();
    this.menuBar1.Guid = new Guid("eccc0fb4-9268-4b1b-bb4b-8e5bb1b01f00");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.elementContextMenu
    });
    componentResourceManager.ApplyResources((object) this.menuBar1, "menuBar1");
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) null;
    componentResourceManager.ApplyResources((object) this.elementContextMenu, "elementContextMenu");
    this.elementContextMenu.ShowText = true;
    this.elementContextMenu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.elementContextMenu_BeforePopup);
    this.elementContextMenu.AfterPopup += new EventHandler(this.elementContextMenu_AfterPopup);
    this.treeList.AllowDrop = true;
    this.treeList.AllowIndividualRowResize = false;
    this.treeList.AutoFitColumns = true;
    this.treeList.Columns.Add(this.columnElements);
    this.treeList.DisableHeaderContextMenu = false;
    componentResourceManager.ApplyResources((object) this.treeList, "treeList");
    this.treeList.Editors.Add(this.cellEditorTBe);
    this.treeList.Editors.Add(this.cellEditorCBe);
    this.treeList.ImageList = (ImageList) null;
    this.treeList.MainColumn = this.columnElements;
    this.treeList.Name = "treeList";
    this.menuBar1.SetPopupMenu((Control) this.treeList, (MenuBarItem) this.elementContextMenu);
    this.treeList.RowStyle.ForeColor = SystemColors.WindowText;
    this.treeList.SelectBeforeEdit = true;
    this.treeList.ShowColumnHeaders = false;
    this.treeList.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.treeList_GetAllowedRowDropLocations);
    this.treeList.GetCellData += new GetCellDataHandler(this.treeList_GetCellData);
    this.treeList.GetChildren += new GetChildrenHandler(this.treeList_GetChildren);
    this.treeList.GetParent += new GetParentHandler(this.treeList_GetParent);
    this.treeList.GetRowData += new GetRowDataHandler(this.treeList_GetRowData);
    this.treeList.GetRowDropEffect += new GetRowDropEffectHandler(this.treeList_GetRowDropEffect);
    this.treeList.RowDrop += new RowDropHandler(this.treeList_RowDrop);
    this.treeList.SelectionChanged += new EventHandler(this.treeList_SelectionChanged);
    this.treeList.SetCellValue += new SetCellValueHandler(this.treeList_SetCellValue);
    this.treeList.DragEnter += new DragEventHandler(this.treeList_DragEnter);
    this.treeList.DragOver += new DragEventHandler(this.treeList_DragOver);
    this.treeList.MouseDown += new MouseEventHandler(this.treeList_MouseDown);
    this.treeList.MouseMove += new MouseEventHandler(this.treeList_MouseMove);
    componentResourceManager.ApplyResources((object) this.columnElements, "columnElements");
    this.columnElements.CellEvenStyle.Font = (Font) componentResourceManager.GetObject("columnElements.CellEvenStyle.Font");
    this.columnElements.CellOddStyle.Font = (Font) componentResourceManager.GetObject("columnElements.CellOddStyle.Font");
    this.columnElements.CellStyle.ForeColor = SystemColors.WindowText;
    this.columnElements.Name = "columnElements";
    this.cellEditorTBe.Control = (Control) this.textBox1;
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.Name = "textBox1";
    this.cellEditorCBe.Control = (Control) this.checkBox1;
    this.cellEditorCBe.DisplayMode = CellEditorDisplayMode.Always;
    this.cellEditorCBe.UseCellHeight = false;
    this.cellEditorCBe.UseCellWidth = false;
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.Controls.Add((Control) this.treeList);
    this.Controls.Add((Control) this.menuBar1);
    this.HideOnClose = true;
    this.Name = nameof (DocumentTreeViewDlg);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Closed += new EventHandler(this.DocumentTreeViewDlg_Closed);
    this.treeList.EndInit();
    this.ResumeLayout(false);
  }

  private void elementContextMenu_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (this.treeList == null)
      return;
    DocumentTreeNode[] context = new DocumentTreeNode[this.treeList.SelectedItems.Count];
    this.treeList.SelectedItems.CopyTo((Array) context, 0);
    if (context.Length == 0)
      return;
    this.elementContextMenu.Items.Clear();
    NodeContextMenu.AddToContextMenu(this.elementContextMenu, this.DocumentControl.GetContexMenu(context));
  }

  private void elementContextMenu_AfterPopup(object sender, EventArgs e)
  {
    NodeContextMenu.ContextForContextMenu = (DocumentTreeNode[]) null;
    NodeContextMenu.ContextMenuCommand = false;
  }

  /// <summary>Получение детей узла дерева</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeList_GetChildren(object sender, GetChildrenEventArgs e)
  {
    try
    {
      if (!this.Visible || e.Row.Item == null || !(e.Row.Item is DocumentTreeNode))
        return;
      DocumentTreeNodeCollection nodes = (e.Row.Item as DocumentTreeNode).Nodes;
      List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
      if (nodes != null)
      {
        for (int index = 0; index < nodes.Count; ++index)
        {
          DocumentTreeNode documentTreeNode = nodes[index];
          if (documentTreeNode != null)
            documentTreeNodeList.Add(documentTreeNode);
        }
      }
      e.Children = (IList) documentTreeNodeList;
      if (nodes == null)
        return;
      for (int index = 0; index < nodes.Count; ++index)
      {
        if (nodes[index] != null)
        {
          nodes[index].ChildNodeAdded -= new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded);
          nodes[index].ChildNodeRemoved -= new ChildNodeRemoved_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved);
          nodes[index].NameChanged -= new NameChanged_EventHandler(this.DocumentTreeViewDlg_NameChanged);
          nodes[index].NodeRemoved -= new NodeRemoved_EventHandler(this.DocumentTreeViewDlg_NodeRemoved);
          nodes[index].ChildNodePositionChanged -= new ChildNodePositionChanged_EventHandler(this.DocumentTreeViewDlg_ChildNodePositionChanged);
          if (nodes[index] is ImDocumentData)
            (nodes[index] as ImDocumentData).PageUnlocked -= new PageUnlocked_EventHandler(this.DocumentTreeViewDlg_PageUnlocked);
          if (nodes[index] is VisualNode)
            (nodes[index] as VisualNode).VisibleChanged -= new VisibleChanged_EventHandler(this.DocumentTreeViewDlg_VisibleChanged);
          if (nodes[index] is ImDocumentData)
            (nodes[index] as ImDocumentData).PageUnlocked += new PageUnlocked_EventHandler(this.DocumentTreeViewDlg_PageUnlocked);
          nodes[index].ChildNodeAdded += new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded);
          nodes[index].ChildNodeRemoved += new ChildNodeRemoved_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved);
          nodes[index].NameChanged += new NameChanged_EventHandler(this.DocumentTreeViewDlg_NameChanged);
          nodes[index].NodeRemoved += new NodeRemoved_EventHandler(this.DocumentTreeViewDlg_NodeRemoved);
          nodes[index].ChildNodePositionChanged += new ChildNodePositionChanged_EventHandler(this.DocumentTreeViewDlg_ChildNodePositionChanged);
          if (nodes[index] is VisualNode)
            (nodes[index] as VisualNode).VisibleChanged += new VisibleChanged_EventHandler(this.DocumentTreeViewDlg_VisibleChanged);
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void DocumentTreeViewDlg_ChildNodePositionChanged(
    object sender,
    ChildNodePositionChanged_EventArgs e)
  {
    PageData pageData = (PageData) null;
    if (sender is PageElementNode)
      pageData = (sender as PageElementNode).Page;
    if (sender is PageData)
      pageData = sender as PageData;
    if (pageData != null && pageData.IsLocked || sender is TableElement tableElement && tableElement.IsDistributing || !this.needUpdateTree)
      return;
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    this.treeList.FindRow((object) (sender as DocumentTreeNode))?.UpdateChildren(true, false);
  }

  private void DocumentTreeViewDlg_VisibleChanged(object sender, VisibleChanged_EventArgs e)
  {
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    if (sender is TableElement tableElement && tableElement.IsDistributing)
      return;
    Row row = this.treeList.FindRow(sender);
    if (row == null)
      return;
    this.treeList.UpdateRowData(row);
  }

  private void DocumentTreeViewDlg_NodeRemoved(object sender, Removed_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new NodeRemoved_EventHandler(this.DocumentTreeViewDlg_NodeRemoved), sender, (object) e);
    }
    else
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (e.Node == null)
        return;
      if (e.Node is ImDocumentData)
        (e.Node as ImDocumentData).PageUnlocked -= new PageUnlocked_EventHandler(this.DocumentTreeViewDlg_PageUnlocked);
      e.Node.ChildNodeAdded -= new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded);
      e.Node.ChildNodeRemoved -= new ChildNodeRemoved_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved);
      e.Node.NameChanged -= new NameChanged_EventHandler(this.DocumentTreeViewDlg_NameChanged);
      e.Node.NodeRemoved -= new NodeRemoved_EventHandler(this.DocumentTreeViewDlg_NodeRemoved);
      e.Node.ChildNodePositionChanged -= new ChildNodePositionChanged_EventHandler(this.DocumentTreeViewDlg_ChildNodePositionChanged);
      if (!(e.Node is VisualNode))
        return;
      (e.Node as VisualNode).VisibleChanged -= new VisibleChanged_EventHandler(this.DocumentTreeViewDlg_VisibleChanged);
    }
  }

  private void DocumentTreeViewDlg_NameChanged(object sender, NameChanged_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new NameChanged_EventHandler(this.DocumentTreeViewDlg_NameChanged), sender, (object) e);
    }
    else
    {
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      if ((sender as DocumentTreeNode).Parent == null)
        return;
      Row row = this.treeList.FindRow((object) (sender as DocumentTreeNode));
      if (row == null)
        return;
      this.treeList.UpdateRowData(row);
    }
  }

  private void DocumentTreeViewDlg_ChildNodeRemoved(object sender, ChildNode_EventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new ChildNodeRemoved_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved), sender, (object) e);
    else if (this.treeList.InvokeRequired)
    {
      this.treeList.BeginInvoke((Delegate) new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved), sender, (object) e);
    }
    else
    {
      PageData pageData = (PageData) null;
      if (sender is PageElementNode)
        pageData = (sender as PageElementNode).Page;
      if (sender is PageData)
        pageData = sender as PageData;
      if (pageData != null && pageData.IsLocked || sender is TableElement tableElement && tableElement.IsDistributing)
        return;
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      if (!(sender is DocumentTreeNode))
        return;
      this.treeList.FindRow((object) (sender as DocumentTreeNode))?.UpdateChildren(true, true);
    }
  }

  private void DocumentTreeViewDlg_ChildNodeAdded(object sender, ChildNode_EventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded), sender, (object) e);
    else if (this.treeList.InvokeRequired)
    {
      this.treeList.BeginInvoke((Delegate) new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded), sender, (object) e);
    }
    else
    {
      if (sender is TableElement tableElement && tableElement.IsDistributing)
        return;
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      PageData pageData = (PageData) null;
      if (sender is PageElementNode)
        pageData = (sender as PageElementNode).Page;
      if (sender is PageData)
        pageData = sender as PageData;
      if (pageData != null && pageData.IsLocked || !(sender is DocumentTreeNode))
        return;
      this.treeList.FindRow((object) (sender as DocumentTreeNode))?.UpdateChildren(true, false);
    }
  }

  /// <summary>Получение текста строк</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeList_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new GetCellDataHandler(this.treeList_GetCellData), sender, (object) e);
    }
    else
    {
      if (!this.Visible || e.Column != this.columnElements)
        return;
      StyleDelta delta = new StyleDelta();
      delta.ForeColor = SystemColors.WindowText;
      if (e.Row.Item is VisualNode visualNode)
      {
        if (!visualNode.Visible)
          delta.ForeColor = SystemColors.GrayText;
        else if (!visualNode.IsVisibleNow)
          delta.ForeColor = Color.FromArgb(80 /*0x50*/, 80 /*0x50*/, 80 /*0x50*/);
      }
      e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, delta);
      e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, delta);
      e.CellData.Value = (object) (e.Row.Item as DocumentTreeNode).GetDefautCaption();
    }
  }

  /// <summary>Вернуть список выбранных узлов документа в дереве</summary>
  /// <returns>Возвращает список выбранных DocumentTreeNode. Если выбранных нет, то возвращает пустой список</returns>
  public List<DocumentTreeNode> GetSelectedDocNodes()
  {
    List<DocumentTreeNode> selectedDocNodes = new List<DocumentTreeNode>();
    selectedDocNodes.AddRange(this.treeList.SelectedItems.OfType<DocumentTreeNode>());
    return selectedDocNodes;
  }

  private void treeList_SelectionChanged(object sender, EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new EventHandler(this.treeList_SelectionChanged), sender, (object) e);
    }
    else
    {
      if (this.BlockUpdateSelection > 0)
        return;
      if (this.DocumentControl != null)
      {
        ++this.BlockUpdateSelection;
        List<DocumentTreeNode> selectedDocNodes = this.GetSelectedDocNodes();
        if ((this.DocumentControl.Document == null ? 0 : (this.DocumentControl.Document.HasLockedNodes((IList<DocumentTreeNode>) selectedDocNodes) ? 1 : 0)) != 0)
        {
          if (selectedDocNodes.Count == 1)
          {
            this.prevSelection = (List<DocumentTreeNode>) null;
            this.DocumentControl.SelectNode(selectedDocNodes[0], false, Point.Empty);
          }
          else
          {
            this.SetSelection(this.prevSelection);
            this.DocumentControl.SetSelection(this.prevSelection, false, Point.Empty, true, false);
          }
        }
        else if (selectedDocNodes.Count > 5)
          this.prevSelection = selectedDocNodes;
        this.DocumentControl.SetSelection(selectedDocNodes, false, Point.Empty, true, false);
      }
      --this.BlockUpdateSelection;
      this.treeList.Select();
    }
  }

  /// <summary>Получение родителя элемента</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeList_GetParent(object sender, GetParentEventArgs e)
  {
    try
    {
      if (!this.Visible || e.Item == null || !(e.Item is DocumentTreeNode))
        return;
      e.Parent = (object) (e.Item as DocumentTreeNode).Parent;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void treeList_MouseDown(object sender, MouseEventArgs e)
  {
    Size dragSize = SystemInformation.DragSize;
    dragSize.Width += 5;
    dragSize.Height += 5;
    this.dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
  }

  private void treeList_GetRowDropEffect(object sender, GetRowDropEffectEventArgs e)
  {
    e.DropEffect = DragDropEffects.None;
    Row row1 = e.Row;
    DocumentTreeNode documentTreeNode1 = row1.Item as DocumentTreeNode;
    DocumentTreeNode parent1 = row1.Item is DocumentTreeNode documentTreeNode2 ? documentTreeNode2.Parent : (DocumentTreeNode) null;
    if (parent1 == null || !(e.Data.GetData(typeof (RowSelectionList)) is RowSelectionList data) || data.Count <= 0)
      return;
    Row row2 = data[0];
    DocumentTreeNode parent2 = row2.Item is DocumentTreeNode documentTreeNode3 ? documentTreeNode3.Parent : (DocumentTreeNode) null;
    if ((!(documentTreeNode1.Parent is Page) && !(row1.Item is Page) || !(parent2 is Page)) && (row1.Level != row2.Level || row1.ParentRow != row2.ParentRow))
      return;
    for (int index = 0; index < data.Count; ++index)
    {
      if (data[index].Item != null)
      {
        DocumentTreeNode child = data[index].Item as DocumentTreeNode;
        if (data[index].Level == row1.Level)
        {
          if (!parent1.CanAddChildElement(child))
            return;
          if (child is TableData tableData && parent1 is TableElement)
          {
            int headersCount = (parent1 as TableElement).HeadersCount;
            if (tableData.IsRow && (tableData.TableCellType == CellType.Header && documentTreeNode1.Index > headersCount || tableData.TableCellType == CellType.DataCell && documentTreeNode1.Index < headersCount))
              return;
          }
        }
        else if (!documentTreeNode1.CanAddChildElement(child))
          return;
      }
    }
    if (Control.ModifierKeys != Keys.Control)
      e.DropEffect = DragDropEffects.Move;
    else
      e.DropEffect = DragDropEffects.Copy;
  }

  private void treeList_DragOver(object sender, DragEventArgs e)
  {
    if (this.treeList.GetRowAt(e.X, e.Y) == null)
      e.Effect = DragDropEffects.None;
    else if (Control.ModifierKeys != Keys.Control)
      e.Effect = DragDropEffects.Move;
    else
      e.Effect = DragDropEffects.Copy;
  }

  private void treeList_GetAllowedRowDropLocations(
    object sender,
    GetAllowedRowDropLocationsEventArgs e)
  {
    e.AllowedDropLocations = RowDropLocation.OnRow;
  }

  public override Cursor Cursor
  {
    get => base.Cursor;
    set => base.Cursor = value;
  }

  private void treeList_MouseMove(object sender, MouseEventArgs e)
  {
    if (this.documentControl != null && this.documentControl.Document != null)
    {
      if (this.documentControl.Document.BackThreadIsActive && this.documentControl.HasSuspendedSelection)
      {
        if (this.Cursor != Cursors.AppStarting)
          this.Cursor = Cursors.AppStarting;
      }
      else if (this.Cursor != this.DefaultCursor)
        this.Cursor = this.DefaultCursor;
    }
    if (this.dragBoxFromMouseDown.Contains(e.Location.X, e.Location.Y) || e.Button != MouseButtons.Left || this.treeList.SelectedItems == null)
      return;
    this.levelDrag = -1;
    Row[] rows = this.treeList.SelectedRows.GetRows();
    if (rows == null || rows.Length == 0)
      return;
    int level = rows[0].Level;
    for (int index = 0; index < rows.Length; ++index)
    {
      if (level != rows[index].Level)
        return;
      Row row = rows[index];
      if (rows[index].Item == null || !(rows[index].Item is DocumentTreeNode))
        return;
      DocumentTreeNode documentTreeNode = rows[index].Item as DocumentTreeNode;
      if (!(rows[index].Item as DocumentTreeNode).CanRemove())
        return;
      if (documentTreeNode is RectangleElement)
      {
        RectangleElement rectangleElement = documentTreeNode as RectangleElement;
        if (rectangleElement.ParentCell != null && rectangleElement.ParentCell.IsRow)
          return;
      }
    }
    this.levelDrag = level;
    if (Control.ModifierKeys != Keys.Control)
    {
      int num1 = (int) this.treeList.DoDragDrop((object) this.treeList.SelectedRows, DragDropEffects.Move);
    }
    else
    {
      int num2 = (int) this.treeList.DoDragDrop((object) this.treeList.SelectedRows, DragDropEffects.Copy);
    }
  }

  /// <summary>Окончание Drag and Drop</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeList_RowDrop(object sender, RowDropEventArgs e)
  {
    if (!e.Data.GetDataPresent(typeof (RowSelectionList)))
      return;
    Row row = e.Row;
    if (row.Item == null)
      return;
    DocumentTreeNode documentTreeNode = row.Item as DocumentTreeNode;
    DocumentTreeNode parent = (row.Item as DocumentTreeNode).Parent;
    if (parent == null)
      return;
    RowSelectionList data = e.Data.GetData(typeof (RowSelectionList)) as RowSelectionList;
    this.needUpdateTree = false;
    for (int index1 = data.Count - 1; index1 >= 0; --index1)
    {
      if (data[index1].Item != null)
      {
        DocumentTreeNode child = data[index1].Item as DocumentTreeNode;
        if (e.DropEffect == DragDropEffects.Copy)
          child = child.Clone();
        if (data[index1].Level == row.Level)
        {
          int index2 = documentTreeNode.Index;
          if (child.Index < documentTreeNode.Index && child.Index > -1 && child.Parent == documentTreeNode.Parent)
            --index2;
          if (index1 == 0)
          {
            this.needUpdateTree = true;
            parent.InsertChildNode(index2, child, false, true, true, true);
          }
          else
            parent.InsertChildNode(index2, child, false, true, false, false);
          documentTreeNode = child;
        }
        else if (index1 == 0)
        {
          this.needUpdateTree = true;
          documentTreeNode.InsertChildNode(0, child, false, true, true, true);
        }
        else
          documentTreeNode.InsertChildNode(0, child, false, true, false, false);
      }
    }
  }

  /// <summary>Устанавливает значения при редактировании их в дереве</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeList_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (!this.Visible || e.Column != this.columnElements)
      return;
    (e.Row.Item as DocumentTreeNode).Name = (string) e.NewValue;
  }

  private void treeList_DragEnter(object sender, DragEventArgs e)
  {
  }

  private void treeList_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.Row.Item is ImDocument)
      e.RowData.Icon = this.treeIcons[0];
    else if (e.Row.Item is Page)
      e.RowData.Icon = this.treeIcons[1];
    else if (e.Row.Item is TextBoxElement)
      e.RowData.Icon = this.treeIcons[2];
    else if (e.Row.Item is LabelElement)
      e.RowData.Icon = this.treeIcons[3];
    else if (e.Row.Item is TableElement)
    {
      if ((e.Row.Item as TableElement).IsRow)
        e.RowData.Icon = this.treeIcons[7];
      else
        e.RowData.Icon = this.treeIcons[4];
    }
    else if (e.Row.Item is Polyline)
      e.RowData.Icon = this.treeIcons[5];
    else if (e.Row.Item is ContainerElement)
    {
      e.RowData.Icon = this.treeIcons[6];
    }
    else
    {
      if (!(e.Row.Item is DocumentsComplect))
        return;
      e.RowData.Icon = this.treeIcons[8];
    }
  }
}
