
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes.PrintQueueNode




using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes
{
    internal abstract class PrintQueueNode : PrintCenterNode
    {
      public PrintQueueNode(
        PrintCenterNode parent = null,
        string pages = "",
        short copies = 0,
        string objectName = "",
        string filePath = "",
        bool addFilenameToCaption = false,
        List<PrintCenterNode> children = null)
        : base(parent, objectName, filePath, addFilenameToCaption, children)
      {
        this.Pages = pages;
        this.Copies = copies == (short) 0 ? "" : copies.ToString();
      }

      public string Copies { get; set; }
    }
}
