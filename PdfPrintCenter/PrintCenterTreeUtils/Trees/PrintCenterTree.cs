
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.Trees.PrintCenterTree




using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.Properties;
using Intermech.PdfPrintCenter.Utils;
using Intermech.PdfPrintCenter.Utils.Events;
using Intermech.UI.Winforms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.Trees
{
    internal class PrintCenterTree : Infralution.Controls.VirtualTree.VirtualTree
    {
      protected readonly string _rootCaption;
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      protected PrintCenterTreeModel _treeModel;
      protected ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
      protected Style _mainNodesStyle;
      protected Style _rootNodeStyle;
      private IContainer components;

      public PrintCenterTree(string rootCaption)
      {
        this.InitializeComponent();
        if (DesignerServices.IsInDesignMode((Component) this, true))
          return;
        this.InitializeColumns();
        this.InitializeContextMenuStrip();
        this.InitializeDataSource();
        this.InitializeEventHandlers();
        this.InitializeHeaderContextMenu();
        this.InitializeTreeStyles();
        this._rootCaption = rootCaption;
      }

      public List<PrintCenterNode> Nodes => this._treeModel.Nodes;

      public bool NodesSelecting { get; private set; }

      public event Delegates.VirtualTreeModifyHandler VirtualTreeModify;

      public virtual void AddNodes(List<PrintCenterNode> nodes)
      {
        this.RemoveDuplicateChildNodes(nodes);
        List<PrintCenterNode> nodes1 = this._treeModel.AddNodes(nodes);
        this.ExpandNodesAndParents(nodes1);
        this._treeModel.SortNodes();
        this.UpdateRows();
        this.SelectSpecificNodes(nodes1);
        this.Focus();
      }

      public bool Contains(string fileName) => this.Contains(this._treeModel.Nodes, fileName);

      public List<PrintCenterNode> GetSelectedNodes()
      {
        return this.SelectedItems.OfType<PrintCenterNode>().ToList<PrintCenterNode>();
      }

      public virtual void RemoveNodes(List<PrintCenterNode> nodes)
      {
        nodes.ForEach((Action<PrintCenterNode>) (node =>
        {
          Row row = this.FindRow((IList) node.NodePath);
          if (row == null)
            return;
          this.SelectedRows.Remove(row);
        }));
        this._treeModel.RemoveNodes(nodes);
        this._treeModel.RemoveEmptyNodes();
        this.UpdateRows();
      }

      public void SelectSpecificNodes(List<PrintCenterNode> nodes)
      {
        this.SelectedRows.Clear();
        nodes.RemoveAll((Predicate<PrintCenterNode>) (node => node == null || this.FindRow((IList) node.NodePath) == null));
        if (nodes.Count == 0)
          return;
        this.NodesSelecting = true;
        for (int index = 0; index < nodes.Count - 1; ++index)
          this.SelectedRows.Add(this.FindRow((IList) nodes[index].NodePath));
        this.NodesSelecting = false;
        this.SelectedRows.Add(this.FindRow((IList) nodes.Last<PrintCenterNode>().NodePath));
        this.Focus();
        this.UpdateRows();
      }

      protected virtual void InitializeColumns()
      {
        Column column = this.CreateColumn(name: "columnMain");
        this.Columns.Add(column);
        this.MainColumn = column;
      }

      protected virtual void InitializeContextMenuStrip()
      {
        this._contextMenuStrip.Size = new Size(100, 100);
        ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Удалить");
        toolStripMenuItem1.Name = "toolStripDelete";
        toolStripMenuItem1.Image = (Image) Resources.PNG_Remove;
        ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem1;
        toolStripMenuItem2.Click += new EventHandler(this.ToolStripMenuItemDelete_Click);
        this._contextMenuStrip.Items.Add((ToolStripItem) toolStripMenuItem2);
      }

      protected virtual void InitializeDataSource()
      {
      }

      protected virtual void InitializeEventHandlers()
      {
        this.GetCellData += new GetCellDataHandler(this.PrintCenterTree_GetCellData);
        this.GetChildren += new GetChildrenHandler(this.PrintCenterTree_GetChildren);
        this.GetContextMenuStrip += new GetContextMenuStripHandler(this.PrintCenterTree_GetContextMenuStrip);
        this.RowCollapse += new RowEventHandler(this.PrintCenterTree_RowCollapse);
        this.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.PrintCenterTree_GetAllowedRowDropLocations);
        this.GetRowDropEffect += new GetRowDropEffectHandler(this.PrintCenterTree_GetRowDropEffect);
        this.MouseMove += new MouseEventHandler(this.PrintCenterTree_MouseMove);
        this.RowDrop += new RowDropHandler(this.PrintCenterTree_RowDrop);
        this.SortColumnChanged += new EventHandler(this.PrintCenterTree_SortColumnChanged);
      }

      protected void InitializeHeaderContextMenu()
      {
        ToolStripItemCollection items = this.HeaderContextMenu.Items;
        List<ToolStripSeparator> source = new List<ToolStripSeparator>();
        foreach (object obj in (ArrangedElementCollection) items)
        {
          if (obj is ToolStripSeparator toolStripSeparator)
            source.Add(toolStripSeparator);
          else if (obj is ToolStripMenuItem toolStripMenuItem)
          {
            object tag = toolStripMenuItem.Tag;
            if (tag != null && tag is string str)
            {
              switch (str)
              {
                case "autoFitMenuItem":
                  toolStripMenuItem.Text = "Автоподгонка по размеру содержимого";
                  continue;
                case "bestFitAllMenuItem":
                  toolStripMenuItem.Text = "Все столбцы по размеру содержимого";
                  continue;
                case "bestFitMenuItem":
                  toolStripMenuItem.Text = "Столбец по размеру содержимого";
                  continue;
                case "customizeMenuItem":
                case "sortAscendingMenuItem":
                case "sortDescendingMenuItem":
                  toolStripMenuItem.Visible = false;
                  continue;
                case "pinnedMenuItem":
                  toolStripMenuItem.Text = "Закрепить столбец";
                  continue;
                default:
                  continue;
              }
            }
          }
        }
        source.First<ToolStripSeparator>().Visible = false;
        source.Last<ToolStripSeparator>().Visible = false;
      }

      protected virtual void InitializeTreeStyles()
      {
        this.HeaderHotStyle.HorzAlignment = StringAlignment.Near;
        this.HeaderStyle.HorzAlignment = StringAlignment.Near;
        this._mainNodesStyle = new Style(this.RowStyle);
        this._mainNodesStyle.Font = new Font(this._mainNodesStyle.Font, FontStyle.Bold);
        this._rootNodeStyle = new Style(this.RowStyle);
        this._rootNodeStyle.Font = new Font(this._rootNodeStyle.Font.FontFamily, 9.5f, FontStyle.Bold);
      }

      protected virtual void OnClickContextMenu(OnModifyVirtualTreeEventArgs e)
      {
        Delegates.VirtualTreeModifyHandler virtualTreeModify = this.VirtualTreeModify;
        if (virtualTreeModify == null)
          return;
        virtualTreeModify((object) this, e);
      }

      protected override void OnPaint(PaintEventArgs pe) => base.OnPaint(pe);

      protected virtual void PrintCenterTree_GetCellData(object sender, GetCellDataEventArgs e)
      {
        if (!(e.Row.Item is PrintCenterTreeModel) || !(e.Column.Name == "columnMain"))
          return;
        e.CellData.Value = (object) this._rootCaption;
        e.CellData.EvenStyle = this._rootNodeStyle;
        e.CellData.OddStyle = this._rootNodeStyle;
      }

      protected virtual void PrintCenterTree_GetChildren(object sender, GetChildrenEventArgs e)
      {
      }

      protected virtual void PrintCenterTree_GetContextMenuStrip(
        object sender,
        GetContextMenuStripEventArgs e)
      {
        this.ContextMenuStrip = this._contextMenuStrip;
        this.ContextMenuStrip.Show(Cursor.Position);
        this.ContextMenuStrip = (ContextMenuStrip) null;
      }

      protected void PrintCenterTree_RowCollapse(object sender, RowEventArgs e)
      {
        if (!(e.Row.Item is PrintCenterTreeModel))
          return;
        e.Row.Expanded = true;
      }

      protected virtual void PrintCenterTree_GetAllowedRowDropLocations(
        object sender,
        GetAllowedRowDropLocationsEventArgs e)
      {
      }

      protected virtual void PrintCenterTree_GetRowDropEffect(
        object sender,
        GetRowDropEffectEventArgs e)
      {
        e.DropEffect = DragDropEffects.Move;
      }

      protected virtual void PrintCenterTree_MouseMove(object sender, MouseEventArgs e)
      {
      }

      protected virtual void PrintCenterTree_RowDrop(object sender, RowDropEventArgs e)
      {
        if (!(this.GetDragData("DragData", e.Data) is DragData dragData))
          return;
        this.OnClickContextMenu(new OnModifyVirtualTreeEventArgs("Drag'n'Drop", dragData.SelectedNodes.OfType<PrintCenterNode>().ToList<PrintCenterNode>(), e.Row.Item));
      }

      protected virtual void PrintCenterTree_SortColumnChanged(object sender, EventArgs e)
      {
        this.SortColumns();
      }

      protected virtual void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
      {
      }

      protected bool Contains(List<PrintCenterNode> nodes, string fileName)
      {
        if (nodes.Any<PrintCenterNode>((Func<PrintCenterNode, bool>) (node => node.FileName == fileName)))
          return true;
        foreach (PrintCenterNode node in nodes)
        {
          if (!node.IsLeaf && this.Contains(node.Children, fileName))
            return true;
        }
        return false;
      }

      protected bool ContainsRootNode(IList items)
      {
        return items.OfType<PrintCenterTreeModel>().Any<PrintCenterTreeModel>();
      }

      protected Column CreateColumn(string caption = "", string name = "column", int width = 100)
      {
        return new Column()
        {
          Caption = caption,
          Name = name,
          Width = width
        };
      }

      protected void ExpandNode(PrintCenterNode node) => this.FindRow((IList) node.NodePath).Expand();

      protected void ExpandNodesAndParents(List<PrintCenterNode> nodes)
      {
        nodes.ForEach((Action<PrintCenterNode>) (node =>
        {
          if (node == null)
            return;
          this.FindRow((IList) node.NodePath).Expand();
          this.ExpandParents(node);
        }));
      }

      protected void ExpandParents(PrintCenterNode node)
      {
        for (; node.Parent != null; node = node.Parent)
          this.ExpandNode(node.Parent);
      }

      protected object GetDragData(string type, IDataObject data)
      {
        object dragData = (object) null;
        string format = ((IEnumerable<string>) data.GetFormats()).SingleOrDefault<string>((Func<string, bool>) (item => item.Contains(type)));
        if (format != null)
          dragData = data.GetData(format);
        return dragData;
      }

      protected PrintCenterNode GetNodeAt(Point location)
      {
        Hashtable rows = new Hashtable();
        this.GetRows(this.TopRowIndex, this.BottomRowIndex + 1, rows);
        if (rows.Count > 0)
        {
          foreach (DictionaryEntry dictionaryEntry in rows)
          {
            Infralution.Controls.VirtualTree.RowWidget rowWidget = this.PinnedPanel.GetRowWidget(dictionaryEntry.Value as Row);
            if (rowWidget != null && rowWidget.Bounds.Top <= location.Y && rowWidget.Bounds.Bottom >= location.Y)
              return (dictionaryEntry.Value as Row).Item as PrintCenterNode;
          }
        }
        return (PrintCenterNode) null;
      }

      protected void RemoveDuplicateChildNodes(List<PrintCenterNode> nodes)
      {
        nodes.RemoveAll((Predicate<PrintCenterNode>) (node => node.Parents.Count<PrintCenterNode>((Func<PrintCenterNode, bool>) (parent => nodes.Contains(parent))) != 0));
      }

      protected virtual void SortColumns()
      {
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
