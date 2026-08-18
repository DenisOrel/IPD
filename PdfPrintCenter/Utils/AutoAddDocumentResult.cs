
// Type: Intermech.PdfPrintCenter.Utils.AutoAddDocumentResult




using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class AutoAddDocumentResult
    {
      public AutoAddDocumentResult()
      {
        this.OnMinLayout = new List<NodesToPrintQueue>();
        this.NotOnMinLayout = new List<NodesToPrintQueue>();
      }

      public AutoAddDocumentResult(
        List<NodesToPrintQueue> onMinLayout,
        List<NodesToPrintQueue> notOnMinLayout)
      {
        this.OnMinLayout = onMinLayout;
        this.NotOnMinLayout = notOnMinLayout;
      }

      public List<NodesToPrintQueue> NotOnMinLayout { get; private set; }

      public List<NodesToPrintQueue> OnMinLayout { get; private set; }
    }
}
