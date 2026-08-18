
// Type: Intermech.PdfPrintCenter.PrintCenterTreeUtils.DocumentPrintSettings.PagePrintSettings




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTreeUtils.DocumentPrintSettings
{
    internal class PagePrintSettings
    {
      public PagePrintSettings(string printerName, IPdfPageProducer layout, PrintQueuePagesNode node)
      {
        this.PrinterName = printerName;
        this.Layout = layout;
        this.Node = new PrintQueuePagesNode(node);
      }

      public IPdfPageProducer Layout { get; set; }

      public PrintQueuePagesNode Node { get; private set; }

      public string PrinterName { get; set; }

      public override bool Equals(object obj)
      {
        return obj is PagePrintSettings pagePrintSettings && this.PrinterName == pagePrintSettings.PrinterName && this.Layout.ToString() == pagePrintSettings.Layout.ToString() && this.Node.Copies == pagePrintSettings.Node.Copies && this.Node.IgnoreDifferentCopies == pagePrintSettings.Node.IgnoreDifferentCopies;
      }

      public override int GetHashCode()
      {
        return ((-1054404960 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.PrinterName)) * -1521134295 + EqualityComparer<object>.Default.GetHashCode((object) this.Layout)) * -1521134295 + EqualityComparer<PrintQueuePagesNode>.Default.GetHashCode(this.Node);
      }
    }
}
