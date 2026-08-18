
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes.WorkspacePagesTreeNode





namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes
{
    internal class WorkspacePagesTreeNode : WorkspaceTreeNode
    {
      public WorkspacePagesTreeNode(
        WorkspaceObjectTreeNode parent,
        string filePath,
        IntermechPageSize pageSize)
        : base((PrintCenterNode) parent, filePath: filePath, format: pageSize.Name)
      {
        this.AddFilenameToCaption = this.Parent.AddFilenameToCaption;
        this.ObjectName = this.Parent.ObjectName;
        this.PageSize = pageSize;
        this.Pages = this.PageSize.Range;
        this.SetMainColumnCaption();
      }

      public IntermechPageSize PageSize { get; private set; }

      protected override void SetMainColumnCaption() => this.MainColumnCaption = this.Pages;
    }
}
