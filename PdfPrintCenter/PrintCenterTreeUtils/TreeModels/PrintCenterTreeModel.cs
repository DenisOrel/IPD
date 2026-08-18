// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels.PrintCenterTreeModel
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels
{
    internal abstract class PrintCenterTreeModel
    {
        public PrintCenterTreeModel() => this.Nodes = new List<PrintCenterNode>();

        public List<PrintCenterNode> Nodes { get; private set; }

        public abstract PrintCenterNode AddNode(PrintCenterNode node);

        public List<PrintCenterNode> AddNodes(List<PrintCenterNode> nodes)
        {
            return this.AddNodesRecursively(nodes).ToList<PrintCenterNode>();
        }

        public void RemoveEmptyNodes() => this.RemoveEmptyNodes(this.Nodes);

        public void RemoveNodes(List<PrintCenterNode> nodesToRemove)
        {
            nodesToRemove.ToList<PrintCenterNode>().ForEach((Action<PrintCenterNode>)(node =>
            {
                PrintCenterNode parent = node.Parent;
                if (parent != null)
                    parent.Children.Remove(node);
                else
                    this.Nodes.Remove(node);
            }));
        }

        public virtual void SortNodes() => this.SortNodes(this.Nodes);

        protected IEnumerable<PrintCenterNode> AddNodesRecursively(List<PrintCenterNode> nodes)
        {
            foreach (PrintCenterNode node in nodes)
            {
                if (!node.IsLeaf)
                {
                    foreach (PrintCenterNode printCenterNode in this.AddNodesRecursively(node.Children))
                        yield return printCenterNode;
                }
                else
                    yield return this.AddNode(node);
            }
        }

        protected NodeType FindNode<NodeType>(List<PrintCenterNode> nodes, string mainColumnCaption) where NodeType : PrintCenterNode
        {
            return nodes.SingleOrDefault<PrintCenterNode>((Func<PrintCenterNode, bool>)(node => node is NodeType && node.MainColumnCaption == mainColumnCaption)) as NodeType;
        }

        protected void RemoveEmptyNodes(List<PrintCenterNode> nodes)
        {
            nodes.RemoveAll((Predicate<PrintCenterNode>)(node => !node.IsLeaf && node.Children.Count == 0));
            nodes.ToList<PrintCenterNode>().ForEach((Action<PrintCenterNode>)(node =>
            {
                if (node.IsLeaf)
                    return;
                this.RemoveEmptyNodes(node.Children);
                if (node.Children.Count != 0)
                    return;
                nodes.Remove(node);
            }));
        }

        protected virtual void SortNodes(List<PrintCenterNode> nodes)
        {
            nodes.Sort(new System.Comparison<PrintCenterNode>(this.Comparison));
            nodes.ForEach((Action<PrintCenterNode>)(node =>
            {
                if (node.IsLeaf)
                    return;
                this.SortNodes(node.Children);
            }));
        }

        internal int Comparison(PrintCenterNode lhs, PrintCenterNode rhs)
        {
            int num = lhs.MainColumnCaption.CompareTo(rhs.MainColumnCaption);
            if (lhs is PrintQueuePagesNode && rhs is PrintQueuePagesNode && num != 0 || !lhs.IsLeaf && !rhs.IsLeaf)
                return num;
            string str1;
            string str2;
            if (lhs is PrintQueuePagesNode && rhs is PrintQueuePagesNode)
            {
                str1 = (lhs as PrintQueuePagesNode).Pages;
                str2 = (rhs as PrintQueuePagesNode).Pages;
            }
            else
            {
                str1 = (lhs as WorkspacePagesTreeNode).MainColumnCaption;
                str2 = (rhs as WorkspacePagesTreeNode).MainColumnCaption;
            }
            return PageIntervalsUtils.GetFirstNumber(str1).CompareTo(PageIntervalsUtils.GetFirstNumber(str2));
        }
    }
}
