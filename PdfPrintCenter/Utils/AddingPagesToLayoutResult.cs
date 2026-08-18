using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Utils
{
    internal class AddingPagesToLayoutResult
    {
      public AddingPagesToLayoutResult()
      {
        this.BadRanges = new List<string>();
        this.PdfWithLayout = (byte[]) null;
      }

      public AddingPagesToLayoutResult(params string[] badRanges)
        : this()
      {
        this.BadRanges.AddRange((IEnumerable<string>) badRanges);
      }

      public AddingPagesToLayoutResult(byte[] pdfWithLayout)
        : this()
      {
        this.PdfWithLayout = pdfWithLayout;
      }

      public List<string> BadRanges { get; private set; }

      public byte[] PdfWithLayout { get; private set; }

      public void AddBadRanges(params string[] badRanges)
      {
        this.BadRanges.AddRange((IEnumerable<string>) badRanges);
      }
    }
}
