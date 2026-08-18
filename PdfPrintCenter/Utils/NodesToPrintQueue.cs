
// Type: Intermech.PdfPrintCenter.Utils.NodesToPrintQueue




using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class NodesToPrintQueue
    {
      public NodesToPrintQueue(WorkspacePagesTreeNode node, PrintParameters printParameters)
        : this(new List<WorkspacePagesTreeNode>() { node }, printParameters)
      {
      }

      public NodesToPrintQueue(List<WorkspacePagesTreeNode> nodes, PrintParameters printParameters)
      {
        this.Nodes = nodes;
        this.PrintParameters = printParameters;
      }

      public List<WorkspacePagesTreeNode> Nodes { get; set; }

      public PrintParameters PrintParameters { get; set; }
    }
}
