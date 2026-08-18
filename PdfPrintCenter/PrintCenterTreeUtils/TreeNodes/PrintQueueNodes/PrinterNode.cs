
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes.PrinterNode




using System.Collections.Generic;
using System.Linq;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes
{
    internal class PrinterNode : PrintQueueNode
    {
      public PrinterNode(string printerName, List<PrintCenterNode> children = null)
        : base(children: children)
      {
        this.PrinterName = printerName;
        if (children == null)
          this.Children = new List<PrintCenterNode>();
        this.SetMainColumnCaption();
      }

      public string PrinterName { get; private set; }

      public Dictionary<string, List<PrintQueuePagesNode>> GroupNodesByFileName()
      {
        Dictionary<string, List<PrintQueuePagesNode>> dictionary = new Dictionary<string, List<PrintQueuePagesNode>>();
        foreach (PrintCenterNode printCenterNode in this.Children.OfType<LayoutNode>())
        {
          foreach (PrintQueuePagesNode printQueuePagesNode in printCenterNode.Children.OfType<PrintQueuePagesNode>())
          {
            if (!dictionary.ContainsKey(printQueuePagesNode.FileName))
              dictionary.Add(printQueuePagesNode.FileName, new List<PrintQueuePagesNode>());
            dictionary[printQueuePagesNode.FileName].Add(printQueuePagesNode);
          }
        }
        return dictionary;
      }

      protected override void SetMainColumnCaption() => this.MainColumnCaption = this.PrinterName;
    }
}
