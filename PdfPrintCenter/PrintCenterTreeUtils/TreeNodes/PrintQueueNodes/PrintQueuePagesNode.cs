
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes.PrintQueuePagesNode





namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes
{
    internal class PrintQueuePagesNode : PrintQueueNode
    {
      public PrintQueuePagesNode(PrintQueuePagesNode node)
        : this(node.Parent as LayoutNode, node.PageSize, short.Parse(node.Copies), node.ObjectName, node.FilePath, node.AddFilenameToCaption, node.IgnoreDifferentCopies)
      {
      }

      public PrintQueuePagesNode(
        LayoutNode parent,
        IntermechPageSize pageSize,
        short copies,
        string objectName,
        string filePath,
        bool fitToPage,
        bool addFilenameToCaption,
        bool ignore = false)
        : base((PrintCenterNode) parent, pageSize.Range, copies, objectName, filePath, addFilenameToCaption)
      {
        this.PageSize = pageSize;
        this.FitToPage = fitToPage;
        this.IgnoreDifferentCopies = ignore;
        this.SetMainColumnCaption();
      }

      public IntermechPageSize PageSize { get; private set; }

      public bool IgnoreDifferentCopies { get; set; }

      public bool FitToPage { get; set; }

      protected override void SetMainColumnCaption()
      {
        this.MainColumnCaption = this.ObjectName;
        if (!this.AddFilenameToCaption)
          return;
        this.MainColumnCaption = $"{this.MainColumnCaption} ({this.FileName})";
      }
    }
}
