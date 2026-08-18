
// Type: Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.DocumentCreators.DocCreatorsFactory





namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.DocumentCreators
{
    internal static class DocCreatorsFactory
    {
      public static DocCreator GetDocCreator(IPdfPageProducer pdfPageProducer)
      {
        switch (pdfPageProducer)
        {
          case LayoutAsItIs layoutAsItIs:
            return (DocCreator) new AsItIsDocCreator(layoutAsItIs);
          case LayoutDescriptor layoutDescriptor:
            return (DocCreator) new ConcreteLayoutDocCreator(layoutDescriptor);
          default:
            return (DocCreator) null;
        }
      }
    }
}
