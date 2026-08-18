
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes.WorkspaceTreeNode




using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes
{
    internal abstract class WorkspaceTreeNode : PrintCenterNode
    {
      public WorkspaceTreeNode(
        PrintCenterNode parent = null,
        string objectName = "",
        string filePath = "",
        string format = "",
        bool addFilenameToCaption = false,
        List<PrintCenterNode> children = null)
        : base(parent, objectName, filePath, addFilenameToCaption, children)
      {
        this.Format = format;
      }

      public string Format { get; set; }
    }
}
