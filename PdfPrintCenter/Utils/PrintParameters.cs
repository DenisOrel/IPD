
// Type: Intermech.PdfPrintCenter.Utils.PrintParameters




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class PrintParameters
    {
      public PrintParameters(
        short copies,
        string printerName,
        IPdfPageProducer layout,
        bool fitToPage)
      {
        this.Copies = copies;
        this.PrinterName = printerName;
        this.Layout = layout;
        this.FitToPage = fitToPage;
      }

      public short Copies { get; private set; }

      public IPdfPageProducer Layout { get; private set; }

      public string PrinterName { get; private set; }

      public bool FitToPage { get; private set; }
    }
}
