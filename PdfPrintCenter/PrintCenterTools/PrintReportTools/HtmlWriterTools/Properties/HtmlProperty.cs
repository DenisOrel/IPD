
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties.HtmlProperty





namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties
{
    internal class HtmlProperty : Property
    {
      public HtmlProperty(HtmlAttributes name, string value)
        : base(name.ToString().ToLower(), value)
      {
      }

      public HtmlProperty(string name, string value)
        : base(name, value)
      {
      }
    }
}
