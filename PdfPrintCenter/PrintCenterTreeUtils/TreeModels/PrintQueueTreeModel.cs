// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels.PrintQueueTreeModel
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using Intermech.PdfPrintCenter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels
{
    internal class PrintQueueTreeModel : PrintCenterTreeModel
    {
        public PrintParameters PrintParameters { get; set; }

        public PrintCenterNode AddNode(PrintCenterNode node, PrintParameters printParameters)
        {
            this.PrintParameters = printParameters;
            PrintCenterNode printCenterNode = this.AddNode(node);
            this.PrintParameters = (PrintParameters)null;
            return printCenterNode;
        }

        public override PrintCenterNode AddNode(PrintCenterNode node)
        {
            IntermechPageSize pageSize = (IntermechPageSize)null;
            bool ignore = false;
            switch (node)
            {
                case WorkspacePagesTreeNode _:
                    string mainColumnCaption = node.Parent.MainColumnCaption;
                    pageSize = (node as WorkspacePagesTreeNode).PageSize;
                    List<PrintCenterNode> nodesFromFile = this.GetNodesFromFile(mainColumnCaption);
                    if (nodesFromFile.Count<PrintCenterNode>() != 0)
                    {
                        ignore = (nodesFromFile.First<PrintCenterNode>() as PrintQueuePagesNode).IgnoreDifferentCopies;
                        break;
                    }
                    break;
                case PrintQueuePagesNode _:
                    pageSize = (node as PrintQueuePagesNode).PageSize;
                    ignore = (node as PrintQueuePagesNode).IgnoreDifferentCopies;
                    break;
            }
            PrinterNode parent1 = this.FindNode<PrinterNode>(this.Nodes, this.PrintParameters.PrinterName);
            if (parent1 == null)
            {
                parent1 = new PrinterNode(this.PrintParameters.PrinterName);
                this.Nodes.Add((PrintCenterNode)parent1);
            }
            LayoutNode parent2 = this.FindNode<LayoutNode>(parent1.Children, this.PrintParameters.Layout.ToString());
            if (parent2 == null)
            {
                parent2 = new LayoutNode(parent1, this.PrintParameters.Layout);
                parent1.Children.Add((PrintCenterNode)parent2);
            }
            PrintQueuePagesNode printQueuePagesNode = new PrintQueuePagesNode(parent2, pageSize, this.PrintParameters.Copies, node.ObjectName, node.FilePath, this.PrintParameters.FitToPage, node.AddFilenameToCaption, ignore);
            parent2.Children.Add((PrintCenterNode)printQueuePagesNode);
            return (PrintCenterNode)printQueuePagesNode;
        }

        public HashSet<string> GetAllFileNames()
        {
            HashSet<string> allFileNames = new HashSet<string>();
            foreach (PrintCenterNode node in this.Nodes)
            {
                foreach (PrintCenterNode child1 in node.Children)
                {
                    foreach (PrintCenterNode child2 in child1.Children)
                        allFileNames.Add(child2.FileName);
                }
            }
            return allFileNames;
        }

        public HashSet<PrintQueuePagesNode> GetDifferentCopiesNumberNodes()
        {
            HashSet<PrintQueuePagesNode> resultSet = new HashSet<PrintQueuePagesNode>();
            foreach (string allFileName in this.GetAllFileNames())
            {
                List<PrintQueuePagesNode> nodesFromFile = this.GetNodesFromFile(allFileName).OfType<PrintQueuePagesNode>().ToList<PrintQueuePagesNode>();
                if (nodesFromFile.Any<PrintQueuePagesNode>((Func<PrintQueuePagesNode, bool>)(node => node.Copies != nodesFromFile.First<PrintQueuePagesNode>().Copies)))
                    nodesFromFile.ForEach((Action<PrintQueuePagesNode>)(node => resultSet.Add(node)));
            }
            return resultSet;
        }

        public virtual List<PrintCenterNode> GetNodesFromFile(string filename)
        {
            return this.GetNodesFromFile(filename, this.Nodes).ToList<PrintCenterNode>();
        }

        private IEnumerable<PrintCenterNode> GetNodesFromFile(
          string filename,
          List<PrintCenterNode> nodes)
        {
            foreach (PrintCenterNode node in nodes)
            {
                if (!node.IsLeaf)
                {
                    foreach (PrintCenterNode printCenterNode in this.GetNodesFromFile(filename, node.Children))
                        yield return printCenterNode;
                }
                else if (node.FileName == filename)
                    yield return node;
            }
        }
    }
}
