
// Type: Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties.Property





namespace Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools.HtmlWriterTools.Properties
{
    internal abstract class Property
    {
      public Property(string attributeName, string value)
      {
        this.Name = attributeName.ToLower();
        this.Value = value;
      }

      public string Name { get; private set; }

      public string Value { get; private set; }
    }
}
