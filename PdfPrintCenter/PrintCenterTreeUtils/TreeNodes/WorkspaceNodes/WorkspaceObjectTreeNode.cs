
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes.WorkspaceObjectTreeNode




using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes
{
    internal class WorkspaceObjectTreeNode : WorkspaceTreeNode
    {
      public WorkspaceObjectTreeNode(
        string objectName,
        string filePath,
        bool addFilenameToCaption,
        List<PrintCenterNode> children = null)
        : base(objectName: objectName, filePath: filePath, addFilenameToCaption: addFilenameToCaption, children: children)
      {
        if (children == null)
          this.Children = new List<PrintCenterNode>();
        this.SetMainColumnCaption();
      }

      protected override void SetMainColumnCaption()
      {
        this.MainColumnCaption = this.ObjectName;
        if (!this.AddFilenameToCaption)
          return;
        this.MainColumnCaption = $"{this.MainColumnCaption} ({this.FileName})";
      }
    }
}
