
// Type: Intermech.PdfPrintCenter.Utils.PageInterval





namespace Intermech.PdfPrintCenter.Utils
{
    internal class PageInterval
    {
      public PageInterval(int begin, int end)
      {
        this.Begin = begin;
        this.End = end;
      }

      public int Begin { get; private set; }

      public int End { get; private set; }

      public override string ToString()
      {
        return this.Begin == this.End ? this.Begin.ToString() : $"{this.Begin}-{this.End}";
      }
    }
}
