
// Type: Intermech.PdfPrintCenter.Utils.WorkspaceAddNodesResult




using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class WorkspaceAddNodesResult
    {
      public WorkspaceAddNodesResult(
        string filename,
        WorkspaceObjectTreeNode rootNode,
        List<string> addedNodesPages,
        List<string> nodesWithEmptyPages)
      {
        this.FileName = filename;
        this.RootNode = rootNode;
        this.AddedNodesPages = addedNodesPages;
        this.NodesWithEmptyPages = nodesWithEmptyPages;
      }

      public string FileName { get; private set; }

      public WorkspaceObjectTreeNode RootNode { get; private set; }

      public List<string> AddedNodesPages { get; private set; }

      public List<string> NodesWithEmptyPages { get; private set; }
    }
}
