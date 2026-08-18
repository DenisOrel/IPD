
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.Trees.WorkspaceTree




using Infralution.Controls.VirtualTree;
using Intermech.PdfPrintCenter.Connector;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using Intermech.PdfPrintCenter.Properties;
using Intermech.PdfPrintCenter.Utils;
using Intermech.PdfPrintCenter.Utils.Events;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.Trees
{
    internal class WorkspaceTree : PrintCenterTree
    {
      private IContainer components;

      public WorkspaceTree()
        : base("Документы")
      {
        this.InitializeComponent();
      }

      public List<WorkspaceAddNodesResult> AddNodes(List<PDMDocumentInfo> documents)
      {
        if (!documents.Any<PDMDocumentInfo>())
          return (List<WorkspaceAddNodesResult>) null;
        WorkspaceTreeModel treeModel = this._treeModel as WorkspaceTreeModel;
        List<WorkspaceAddNodesResult> workspaceAddNodesResultList = new List<WorkspaceAddNodesResult>();
        List<PrintCenterNode> nodes = new List<PrintCenterNode>();
        foreach (PDMDocumentInfo document in documents)
        {
          bool addFilenameToCaption = true;
          if (document.FilePaths.Count == 1)
            addFilenameToCaption = false;
          foreach (string filePath in document.FilePaths)
          {
            WorkspaceAddNodesResult workspaceAddNodesResult = treeModel.AddNode(document.ObjectName, filePath, addFilenameToCaption);
            if (workspaceAddNodesResult?.RootNode != null)
            {
              this.ExpandNode((PrintCenterNode) workspaceAddNodesResult.RootNode);
              this._treeModel.SortNodes();
              this.UpdateRows();
              nodes.Add((PrintCenterNode) workspaceAddNodesResult.RootNode);
            }
            workspaceAddNodesResultList.Add(workspaceAddNodesResult);
          }
        }
        nodes.Sort(new Comparison<PrintCenterNode>(this._treeModel.Comparison));
        this.SelectSpecificNodes(nodes);
        return workspaceAddNodesResultList;
      }

      protected override void InitializeColumns()
      {
        base.InitializeColumns();
        this.MainColumn.Width = 290;
        this.Columns.Add(this.CreateColumn("Формат", "columnFormat"));
        this.Columns.Add(this.CreateColumn("Файл", "columnFile"));
      }

      protected override void InitializeContextMenuStrip()
      {
        base.InitializeContextMenuStrip();
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Добавить");
        toolStripMenuItem1.Name = "toolStripAdd";
        toolStripMenuItem1.Image = (Image) Resources.PNG_Add;
        ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem1;
        toolStripMenuItem2.Click += new EventHandler(this.ToolStripMenuItemAdd_Click);
        ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Авто");
        toolStripMenuItem3.Name = "toolStripAutoAdd";
        toolStripMenuItem3.Image = (Image) Resources.PNG_AutoAdd;
        ToolStripMenuItem toolStripMenuItem4 = toolStripMenuItem3;
        toolStripMenuItem4.Click += new EventHandler(this.ToolStripMenuItemAutoAdd_Click);
        this._contextMenuStrip.Items.Add((ToolStripItem) toolStripMenuItem2);
        this._contextMenuStrip.Items.Add((ToolStripItem) toolStripMenuItem4);
        this._contextMenuStrip.Items.SortByName();
      }

      protected override void InitializeDataSource()
      {
        this._treeModel = (PrintCenterTreeModel) new WorkspaceTreeModel();
        this.DataSource = (object) this._treeModel;
      }

      protected override void OnPaint(PaintEventArgs pe) => base.OnPaint(pe);

      protected override void PrintCenterTree_GetCellData(object sender, GetCellDataEventArgs e)
      {
        base.PrintCenterTree_GetCellData(sender, e);
        if (!(e.Row.Item is WorkspaceTreeNode workspaceTreeNode))
          return;
        switch (e.Column.Name)
        {
          case "columnMain":
            e.CellData.Value = (object) workspaceTreeNode.MainColumnCaption;
            if (!(workspaceTreeNode is WorkspaceObjectTreeNode))
              break;
            e.CellData.EvenStyle = this._mainNodesStyle;
            e.CellData.OddStyle = this._mainNodesStyle;
            break;
          case "columnFormat":
            e.Column.Sortable = false;
            e.CellData.Value = (object) workspaceTreeNode.Format;
            break;
          case "columnFile":
            if (workspaceTreeNode is WorkspaceObjectTreeNode)
              break;
            e.CellData.Value = (object) workspaceTreeNode.FileName;
            break;
        }
      }

      protected override void PrintCenterTree_GetChildren(object sender, GetChildrenEventArgs e)
      {
        if (e.Row.Item is PrintCenterTreeModel)
        {
          PrintCenterTreeModel printCenterTreeModel = e.Row.Item as PrintCenterTreeModel;
          e.Children = (IList) printCenterTreeModel.Nodes;
        }
        else
        {
          if (!(e.Row.Item is WorkspaceTreeNode))
            return;
          WorkspaceTreeNode workspaceTreeNode = e.Row.Item as WorkspaceTreeNode;
          e.Children = (IList) workspaceTreeNode.Children;
        }
      }

      protected override void PrintCenterTree_GetContextMenuStrip(
        object sender,
        GetContextMenuStripEventArgs e)
      {
        this._contextMenuStrip.SetAllVisible();
        this.CheckAddToolStrip();
        this.CheckAutoAddToolStrip();
        this.CheckDeleteToolStrip();
        base.PrintCenterTree_GetContextMenuStrip(sender, e);
      }

      protected override void PrintCenterTree_GetAllowedRowDropLocations(
        object sender,
        GetAllowedRowDropLocationsEventArgs e)
      {
        if (!(this.GetDragData("DragData", e.Data) is DragData dragData) || dragData.Control is WorkspaceTree)
          e.AllowedDropLocations = RowDropLocation.None;
        else
          e.AllowedDropLocations = RowDropLocation.OnRow;
      }

      protected override void PrintCenterTree_MouseMove(object sender, MouseEventArgs e)
      {
        if ((e.Button & MouseButtons.Left) != MouseButtons.Left || this.GetNodeAt(e.Location) == null || this.SelectedItems == null || this.ContainsRootNode(this.SelectedItems))
          return;
        int num = (int) this.DoDragDrop((object) new DragData((Control) this, this.SelectedItems), DragDropEffects.All);
      }

      protected void ToolStripMenuItemAdd_Click(object sender, EventArgs e)
      {
        this.OnClickToolStripMenuItem(sender);
      }

      protected void ToolStripMenuItemAutoAdd_Click(object sender, EventArgs e)
      {
        this.OnClickToolStripMenuItem(sender);
      }

      protected override void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
      {
        this.OnClickToolStripMenuItem(sender);
      }

      protected void CheckAddToolStrip()
      {
        if (!(this._contextMenuStrip.Items.Find("toolStripAdd", false)[0] is ToolStripMenuItem toolStripMenuItem) || this.SelectedItems.OfType<WorkspaceTreeModel>().Count<WorkspaceTreeModel>() == 0)
          return;
        toolStripMenuItem.Visible = false;
      }

      protected void CheckAutoAddToolStrip()
      {
        if (!(this._contextMenuStrip.Items.Find("toolStripAutoAdd", false)[0] is ToolStripMenuItem toolStripMenuItem) || this.SelectedItems.OfType<WorkspaceTreeModel>().Count<WorkspaceTreeModel>() == 0)
          return;
        toolStripMenuItem.Visible = false;
      }

      protected void CheckDeleteToolStrip()
      {
        if (!(this._contextMenuStrip.Items.Find("toolStripDelete", false)[0] is ToolStripMenuItem toolStripMenuItem) || this.SelectedItems.OfType<WorkspaceTreeModel>().Count<WorkspaceTreeModel>() == 0 && this.SelectedItems.OfType<WorkspacePagesTreeNode>().Count<WorkspacePagesTreeNode>() == 0)
          return;
        toolStripMenuItem.Visible = false;
      }

      protected void OnClickToolStripMenuItem(object sender)
      {
        this.OnClickContextMenu(new OnModifyVirtualTreeEventArgs((sender as ToolStripMenuItem).Text, this.GetSelectedNodes()));
      }

      protected override void SortColumns()
      {
        base.SortColumns();
        string name = this.SortColumn.Name;
        ListSortDirection sortDirection = this.SortColumn.SortDirection;
        if (!this.Nodes.All<PrintCenterNode>((Func<PrintCenterNode, bool>) (item => item is WorkspaceTreeNode)))
          return;
        switch (name)
        {
          case "columnMain":
            this.SortMainColumn(sortDirection);
            break;
          case "columnFile":
            this.SortFileColumn(sortDirection);
            break;
        }
      }

      private void SortFileColumn(ListSortDirection sortDirection)
      {
        foreach (WorkspaceObjectTreeNode workspaceObjectTreeNode in this.Nodes.OfType<WorkspaceObjectTreeNode>())
        {
          workspaceObjectTreeNode.Children.Sort((Comparison<PrintCenterNode>) ((lhs, rhs) => lhs.FileName.CompareTo(rhs.FileName)));
          if (sortDirection == ListSortDirection.Descending)
            workspaceObjectTreeNode.Children.Reverse();
        }
      }

      private void SortMainColumn(ListSortDirection sortDirection)
      {
        this.Nodes.Sort((Comparison<PrintCenterNode>) ((lhs, rhs) => lhs.MainColumnCaption.CompareTo(rhs.MainColumnCaption)));
        if (sortDirection == ListSortDirection.Descending)
          this.Nodes.Reverse();
        foreach (WorkspaceObjectTreeNode workspaceObjectTreeNode in this.Nodes.OfType<WorkspaceObjectTreeNode>())
        {
          workspaceObjectTreeNode.Children.Sort((Comparison<PrintCenterNode>) ((lhs, rhs) => PageIntervalsUtils.CompareIntervals(lhs.Pages, rhs.Pages)));
          if (sortDirection == ListSortDirection.Descending)
            workspaceObjectTreeNode.Children.Reverse();
        }
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
    }
}
