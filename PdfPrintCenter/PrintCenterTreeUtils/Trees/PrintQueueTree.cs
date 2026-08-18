// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.Trees.PrintQueueTree
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.DocumentPrintSettings;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
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
    internal class PrintQueueTree : PrintCenterTree
    {
        private const string CheckMark = "✔";
        private HashSet<PrintQueuePagesNode> _differentCopiesNumberSet = new HashSet<PrintQueuePagesNode>();
        private Style _badCopiesStyle;
        private IContainer components;

        public PrintQueueTree()
          : base("Очередь печати")
        {
            this.InitializeComponent();
        }

        public ILayoutSettingsService LayoutSettingsService { get; set; }

        public IPrintersSettingsService PrintersSettingsService { get; set; }

        public void AddNodes(List<PrintCenterNode> nodes, PrintParameters printParameters)
        {
            (this._treeModel as PrintQueueTreeModel).PrintParameters = printParameters;
            this.AddNodes(nodes);
            (this._treeModel as PrintQueueTreeModel).PrintParameters = (PrintParameters)null;
            this.CheckCopiesNumber();
        }

        protected virtual void AddNodes(List<PagePrintSettings> pagesSettings)
        {
            List<PrintCenterNode> list = pagesSettings.Select<PagePrintSettings, PrintCenterNode>((Func<PagePrintSettings, PrintCenterNode>)(pageSettings =>
            {
                short copies = short.Parse(pageSettings.Node.Copies);
                return (this._treeModel as PrintQueueTreeModel).AddNode((PrintCenterNode)pageSettings.Node, new PrintParameters(copies, pageSettings.PrinterName, pageSettings.Layout, pageSettings.Node.FitToPage));
            })).ToList<PrintCenterNode>();
            this.SortColumns();
            this.ExpandNodesAndParents(list.ToList<PrintCenterNode>());
            this._treeModel.SortNodes();
            this.UpdateRows();
            this.SelectSpecificNodes(list);
            this.CheckCopiesNumber();
        }

        public bool CheckNodesSelected()
        {
            List<PrintCenterNode> selectedNodes = this.GetSelectedNodes();
            List<PrintQueueTreeModel> list = this.SelectedItems.OfType<PrintQueueTreeModel>().ToList<PrintQueueTreeModel>();
            if (selectedNodes.Count != 0)
                return true;
            return list.Count != 0 && this.Nodes.Count != 0;
        }

        public List<PrintCenterNode> GetNodesFromFile(string filename)
        {
            return (this._treeModel as PrintQueueTreeModel).GetNodesFromFile(filename);
        }

        public List<PrinterNode> GetNodesSelectedForPrint()
        {
            if (this.SelectedItems.OfType<PrintQueueTreeModel>().Any<PrintQueueTreeModel>())
                return this.Nodes.OfType<PrinterNode>().ToList<PrinterNode>();
            List<PrintCenterNode> selectedNodes = this.GetSelectedNodes();
            HashSet<PrinterNode> printerNodes = selectedNodes.OfType<PrinterNode>().ToHashSet<PrinterNode>();
            HashSet<LayoutNode> layoutNodes = selectedNodes.OfType<LayoutNode>().ToHashSet<LayoutNode>();
            HashSet<PrintQueuePagesNode> hashSet = selectedNodes.OfType<PrintQueuePagesNode>().ToHashSet<PrintQueuePagesNode>();
            hashSet.RemoveWhere((Predicate<PrintQueuePagesNode>)(node => ((IEnumerable<PrintCenterNode>)layoutNodes).Contains<PrintCenterNode>(node.Parent) || ((IEnumerable<PrintCenterNode>)printerNodes).Contains<PrintCenterNode>(node.Parent.Parent)));
            layoutNodes.RemoveWhere((Predicate<LayoutNode>)(node => ((IEnumerable<PrintCenterNode>)printerNodes).Contains<PrintCenterNode>(node.Parent)));
            foreach (PrintQueuePagesNode printQueuePagesNode in hashSet)
            {
                LayoutNode parentNode = printQueuePagesNode.Parent as LayoutNode;
                LayoutNode layoutNode1 = layoutNodes.FirstOrDefault<LayoutNode>((Func<LayoutNode, bool>)(layout => layout.Layout == parentNode.Layout && layout.Parent == parentNode.Parent));
                if (layoutNode1 == null)
                {
                    LayoutNode layoutNode2 = new LayoutNode(parentNode.Parent as PrinterNode, parentNode.Layout, new List<PrintCenterNode>()
            {
              (PrintCenterNode) printQueuePagesNode
            });
                    layoutNodes.Add(layoutNode2);
                }
                else
                    layoutNode1.Children.Add((PrintCenterNode)printQueuePagesNode);
            }
            foreach (LayoutNode layoutNode in layoutNodes)
            {
                PrinterNode parentNode = layoutNode.Parent as PrinterNode;
                PrinterNode printerNode1 = printerNodes.FirstOrDefault<PrinterNode>((Func<PrinterNode, bool>)(printer => printer.PrinterName == parentNode.PrinterName));
                if (printerNode1 == null)
                {
                    PrinterNode printerNode2 = new PrinterNode(parentNode.PrinterName, new List<PrintCenterNode>()
            {
              (PrintCenterNode) layoutNode
            });
                    printerNodes.Add(printerNode2);
                }
                else
                    printerNode1.Children.Add((PrintCenterNode)layoutNode);
            }
            return printerNodes.ToList<PrinterNode>();
        }

        public override void RemoveNodes(List<PrintCenterNode> nodes)
        {
            base.RemoveNodes(nodes);
            this.CheckCopiesNumber();
        }

        public void UpdateLayouts(List<IPdfPageProducer> layouts)
        {
            foreach (PrintCenterNode printCenterNode in this.Nodes.OfType<PrinterNode>())
            {
                foreach (LayoutNode layoutNode1 in printCenterNode.Children.OfType<LayoutNode>())
                {
                    LayoutNode layoutNode = layoutNode1;
                    IPdfPageProducer pdfPageProducer = layouts.Find((Predicate<IPdfPageProducer>)(layout => layout.Caption == layoutNode.Layout.Caption));
                    if (pdfPageProducer != null)
                        layoutNode.Layout = pdfPageProducer;
                }
            }
        }

        public void UpdateLayoutsNames(List<RenamedLayout> renamedLayouts)
        {
            this.Nodes.OfType<PrinterNode>().ToList<PrinterNode>().ForEach((Action<PrinterNode>)(printerNode => printerNode.Children.OfType<LayoutNode>().ToList<LayoutNode>().ForEach((Action<LayoutNode>)(layoutNode =>
            {
                string newName = renamedLayouts.SingleOrDefault<RenamedLayout>((Func<RenamedLayout, bool>)(renamedLayout => layoutNode.LayoutName == renamedLayout.OldName))?.NewName;
                if (newName == null)
                    return;
                layoutNode.ModifyLayoutName(newName);
            }))));
            this.UpdateRows();
        }

        protected override void InitializeColumns()
        {
            base.InitializeColumns();
            this.MainColumn.Width = 250;
            this.Columns.Add(this.CreateColumn("Страницы", "columnPages"));
            this.Columns.Add(this.CreateColumn("Копии", "columnCopies", 50));
            this.Columns.Add(this.CreateColumn("Вписать в область", "columnFitToPage", 65));
            this.Columns.Add(this.CreateColumn("Файл", "columnFile"));
        }

        protected override void InitializeContextMenuStrip()
        {
            base.InitializeContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("Игнорировать проверку копий");
            toolStripMenuItem1.Name = "toolStripIgnore";
            toolStripMenuItem1.Image = (Image)Resources.PNG_Ignore;
            ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem1;
            toolStripMenuItem2.Click += new EventHandler(this.ToolStripMenuItemIgnore_Click);
            ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem("Изменить");
            toolStripMenuItem3.Name = "toolStripEdit";
            toolStripMenuItem3.Image = (Image)Resources.PNG_Edit;
            ToolStripMenuItem toolStripMenuItem4 = toolStripMenuItem3;
            toolStripMenuItem4.Click += new EventHandler(this.ToolStripMenuItemEdit_Click);
            ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem("Печать");
            toolStripMenuItem5.Name = "toolStripPrint";
            toolStripMenuItem5.Image = (Image)Resources.PNG_Print;
            ToolStripMenuItem toolStripMenuItem6 = toolStripMenuItem5;
            toolStripMenuItem6.Click += new EventHandler(this.ToolStripMenuItemPrint_Click);
            this._contextMenuStrip.Items.Add((ToolStripItem)toolStripMenuItem2);
            this._contextMenuStrip.Items.Add((ToolStripItem)toolStripMenuItem4);
            this._contextMenuStrip.Items.Add((ToolStripItem)toolStripMenuItem6);
            this._contextMenuStrip.Items.SortByName();
        }

        protected override void InitializeDataSource()
        {
            this._treeModel = (PrintCenterTreeModel)new PrintQueueTreeModel();
            this.DataSource = (object)this._treeModel;
        }

        protected override void InitializeTreeStyles()
        {
            base.InitializeTreeStyles();
            this._badCopiesStyle = new Style(this.RowStyle)
            {
                ForeColor = Color.Red
            };
            this._badCopiesStyle.Font = new Font(this._badCopiesStyle.Font.FontFamily, this._badCopiesStyle.Font.Size, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs pe) => base.OnPaint(pe);

        protected override void PrintCenterTree_GetCellData(object sender, GetCellDataEventArgs e)
        {
            base.PrintCenterTree_GetCellData(sender, e);
            if (!(e.Row.Item is PrintQueueNode printQueueNode))
                return;
            switch (e.Column.Name)
            {
                case "columnMain":
                    e.CellData.Value = (object)printQueueNode.MainColumnCaption;
                    if (!(printQueueNode is PrinterNode))
                        break;
                    e.CellData.EvenStyle = this._mainNodesStyle;
                    e.CellData.OddStyle = this._mainNodesStyle;
                    break;
                case "columnPages":
                    e.CellData.Value = (object)printQueueNode.Pages;
                    break;
                case "columnCopies":
                    e.CellData.Value = (object)printQueueNode.Copies;
                    e.CellData.AlwaysDisplayToolTip = true;
                    if (!(printQueueNode is PrintQueuePagesNode printQueuePagesNode) || printQueuePagesNode.IgnoreDifferentCopies || !this._differentCopiesNumberSet.Contains(printQueuePagesNode))
                        break;
                    e.CellData.EvenStyle = this._badCopiesStyle;
                    e.CellData.OddStyle = this._badCopiesStyle;
                    e.CellData.ToolTip = "Разное количество копий диапазонов\nстраниц одного и того же документа!";
                    break;
                case "columnFitToPage":
                    e.Column.Sortable = false;
                    if (!(printQueueNode is PrintQueuePagesNode) || !(printQueueNode as PrintQueuePagesNode).FitToPage)
                        break;
                    e.CellData.Value = (object)"✔";
                    break;
                case "columnFile":
                    e.CellData.Value = (object)printQueueNode.FileName;
                    break;
            }
        }

        protected override void PrintCenterTree_GetChildren(object sender, GetChildrenEventArgs e)
        {
            if (e.Row.Item is PrintCenterTreeModel printCenterTreeModel)
            {
                e.Children = (IList)printCenterTreeModel.Nodes;
            }
            else
            {
                if (!(e.Row.Item is PrintQueueNode printQueueNode))
                    return;
                e.Children = (IList)printQueueNode.Children;
            }
        }

        protected override void PrintCenterTree_GetContextMenuStrip(
          object sender,
          GetContextMenuStripEventArgs e)
        {
            this._contextMenuStrip.SetAllVisible();
            this.CheckDeleteToolStrip();
            this.CheckEditToolStrip();
            this.CheckIgnoreToolStrip();
            this.CheckPrintToolStrip();
            base.PrintCenterTree_GetContextMenuStrip(sender, e);
        }

        protected override void PrintCenterTree_GetAllowedRowDropLocations(
          object sender,
          GetAllowedRowDropLocationsEventArgs e)
        {
            if (e.Row.Item is PrinterNode)
                e.AllowedDropLocations = RowDropLocation.None;
            else if (!(this.GetDragData("DragData", e.Data) is DragData dragData) || dragData.Control is PrintQueueTree)
                e.AllowedDropLocations = RowDropLocation.None;
            else
                e.AllowedDropLocations = RowDropLocation.OnRow;
        }

        protected override void PrintCenterTree_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left || this.GetNodeAt(e.Location) == null || this.SelectedItems == null || this.ContainsRootNode(this.SelectedItems) || this.GetSelectedNodes().Any<PrintCenterNode>((Func<PrintCenterNode, bool>)(item => item is PrinterNode || item is LayoutNode)))
                return;
            int num = (int)this.DoDragDrop((object)new DragData((Control)this, this.SelectedItems), DragDropEffects.All);
        }

        protected override void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem toolStripMenuItem))
                return;
            this.OnClickContextMenu(new OnModifyVirtualTreeEventArgs(toolStripMenuItem.Text, this.GetSelectedNodes()));
        }

        protected virtual void ToolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            List<PrintCenterNode> list1 = (this._treeModel as PrintQueueTreeModel).GetNodesFromFile((this.SelectedItem as PrintCenterNode).FileName).ToList<PrintCenterNode>();
            List<PrintQueuePagesNode> list2 = list1.OfType<PrintQueuePagesNode>().ToList<PrintQueuePagesNode>();
            list2.Sort((Comparison<PrintQueuePagesNode>)((lhs, rhs) => PageIntervalsUtils.GetFirstNumber(lhs.Pages).CompareTo(PageIntervalsUtils.GetFirstNumber(rhs.Pages))));
            EditDocumentPrintSettingsForm printSettingsForm = new EditDocumentPrintSettingsForm(list2, this.LayoutSettingsService.LoadAllLayouts(), this.PrintersSettingsService.GetPrintersSettings().PrintersOrder);
            if (printSettingsForm.ShowDialog() != DialogResult.OK)
                return;
            this.RemoveNodes(list1);
            this.AddNodes(printSettingsForm.PagesPrintSettings);
        }

        protected virtual void ToolStripMenuItemIgnore_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem))
                return;
            List<PrintCenterNode> selectedNodes = this.GetSelectedNodes();
            selectedNodes.ForEach((Action<PrintCenterNode>)(selectedNode => (this._treeModel as PrintQueueTreeModel).GetNodesFromFile(selectedNode.FileName).Where<PrintCenterNode>((Func<PrintCenterNode, bool>)(node => node.Equals((object)selectedNode) || !selectedNodes.Contains(node))).ToList<PrintCenterNode>().ForEach((Action<PrintCenterNode>)(node =>
            {
                PrintQueuePagesNode printQueuePagesNode = node as PrintQueuePagesNode;
                printQueuePagesNode.IgnoreDifferentCopies = !printQueuePagesNode.IgnoreDifferentCopies;
            }))));
            this.UpdateRows();
        }

        protected virtual void ToolStripMenuItemPrint_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem toolStripMenuItem))
                return;
            this.OnClickContextMenu(new OnModifyVirtualTreeEventArgs(toolStripMenuItem.Text));
        }

        protected void CheckCopiesNumber()
        {
            this._differentCopiesNumberSet = (this._treeModel as PrintQueueTreeModel).GetDifferentCopiesNumberNodes();
            this.UpdateRows();
        }

        protected void CheckDeleteToolStrip()
        {
            if (!(this._contextMenuStrip.Items.Find("toolStripDelete", false)[0] is ToolStripMenuItem toolStripMenuItem) || this.SelectedItems.OfType<PrintQueueTreeModel>().Count<PrintQueueTreeModel>() == 0)
                return;
            toolStripMenuItem.Visible = false;
        }

        protected void CheckEditToolStrip()
        {
            if (!(this._contextMenuStrip.Items.Find("toolStripEdit", false)[0] is ToolStripMenuItem toolStripMenuItem))
                return;
            if (this.SelectedItems.OfType<PrintQueueTreeModel>().Count<PrintQueueTreeModel>() != 0)
            {
                toolStripMenuItem.Visible = false;
            }
            else
            {
                List<PrintCenterNode> selectedNodes = this.GetSelectedNodes();
                if (!selectedNodes.Any<PrintCenterNode>((Func<PrintCenterNode, bool>)(node => !(node is PrintQueuePagesNode))) && selectedNodes.All<PrintCenterNode>((Func<PrintCenterNode, bool>)(node => node.FileName == selectedNodes.First<PrintCenterNode>().FileName)))
                    return;
                toolStripMenuItem.Visible = false;
            }
        }

        protected void CheckIgnoreToolStrip()
        {
            if (!(this._contextMenuStrip.Items.Find("toolStripIgnore", false)[0] is ToolStripMenuItem toolStripMenuItem))
                return;
            if (this.SelectedItems.OfType<PrintQueueTreeModel>().Count<PrintQueueTreeModel>() != 0)
            {
                toolStripMenuItem.Visible = false;
            }
            else
            {
                List<PrintCenterNode> selectedNodes = this.GetSelectedNodes();
                if (selectedNodes.Any<PrintCenterNode>((Func<PrintCenterNode, bool>)(node => !(node is PrintQueuePagesNode))) || !selectedNodes.All<PrintCenterNode>((Func<PrintCenterNode, bool>)(node => node.FileName == selectedNodes.First<PrintCenterNode>().FileName)))
                    toolStripMenuItem.Visible = false;
                else
                    toolStripMenuItem.Checked = (selectedNodes.First<PrintCenterNode>() as PrintQueuePagesNode).IgnoreDifferentCopies;
            }
        }

        private void CheckPrintToolStrip()
        {
            if (!(this._contextMenuStrip.Items.Find("toolStripPrint", false)[0] is ToolStripMenuItem toolStripMenuItem))
                return;
            toolStripMenuItem.Visible = this.CheckNodesSelected();
        }

        protected override void SortColumns()
        {
            base.SortColumns();
            if (this.SortColumn == null)
                return;
            string name = this.SortColumn.Name;
            ListSortDirection sortDirection = this.SortColumn.SortDirection;
            if (!this.Nodes.All<PrintCenterNode>((Func<PrintCenterNode, bool>)(item => item is PrintQueueNode)))
                return;
            switch (name)
            {
                case "columnMain":
                    this.SortMainColumn(sortDirection);
                    break;
                case "columnPages":
                    this.SortPagesColumn(sortDirection);
                    break;
                case "columnCopies":
                    this.SortCopiesColumn(sortDirection);
                    break;
                case "columnFile":
                    this.SortFileColumn(sortDirection);
                    break;
            }
        }

        private void SortCopiesColumn(ListSortDirection sortDirection)
        {
            foreach (PrintCenterNode printCenterNode in this.Nodes.OfType<PrinterNode>())
            {
                foreach (LayoutNode layoutNode in printCenterNode.Children.OfType<LayoutNode>())
                {
                    layoutNode.Children.Sort((Comparison<PrintCenterNode>)((lhs, rhs) => int.Parse((lhs as PrintQueueNode).Copies).CompareTo(int.Parse((rhs as PrintQueueNode).Copies))));
                    if (sortDirection == ListSortDirection.Descending)
                        layoutNode.Children.Reverse();
                }
            }
        }

        private void SortFileColumn(ListSortDirection sortDirection)
        {
            foreach (PrintCenterNode printCenterNode in this.Nodes.OfType<PrinterNode>())
            {
                foreach (LayoutNode layoutNode in printCenterNode.Children.OfType<LayoutNode>())
                {
                    layoutNode.Children.Sort((Comparison<PrintCenterNode>)((lhs, rhs) => lhs.FileName.CompareTo(rhs.FileName)));
                    if (sortDirection == ListSortDirection.Descending)
                        layoutNode.Children.Reverse();
                }
            }
        }

        private void SortMainColumn(ListSortDirection sortDirection)
        {
            this.DoSortMainColumn(sortDirection, this.Nodes);
        }

        private void DoSortMainColumn(ListSortDirection sortDirection, List<PrintCenterNode> nodes)
        {
            nodes.Sort((Comparison<PrintCenterNode>)((lhs, rhs) => lhs.MainColumnCaption == rhs.MainColumnCaption ? PageIntervalsUtils.CompareIntervals(lhs.Pages, rhs.Pages) : lhs.MainColumnCaption.CompareTo(rhs.MainColumnCaption)));
            if (sortDirection == ListSortDirection.Descending)
                nodes.Reverse();
            foreach (PrintCenterNode printCenterNode in nodes.Where<PrintCenterNode>((Func<PrintCenterNode, bool>)(node => !node.IsLeaf)))
                this.DoSortMainColumn(sortDirection, printCenterNode.Children);
        }

        private void SortPagesColumn(ListSortDirection sortDirection)
        {
            foreach (PrintCenterNode printCenterNode in this.Nodes.OfType<PrinterNode>())
            {
                foreach (LayoutNode layoutNode in printCenterNode.Children.OfType<LayoutNode>())
                {
                    layoutNode.Children.Sort((Comparison<PrintCenterNode>)((lhs, rhs) => PageIntervalsUtils.CompareIntervals(lhs.Pages, rhs.Pages)));
                    if (sortDirection == ListSortDirection.Descending)
                        layoutNode.Children.Reverse();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();
    }
}
