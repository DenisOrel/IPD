using Intermech.PdfPrintCenter.Utils;
using iTextSharp.text.pdf;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.DocumentCreators
{
    internal abstract class DocCreator
    {
        public DocCreator(IPdfPageProducer layoutDescriptor) => this.PdfPageProducer = layoutDescriptor;

        protected IPdfPageProducer PdfPageProducer { get; set; }

        public abstract AddingPagesToLayoutResult CreateDocument(
          List<string> inputFiles,
          List<string> ranges,
          int copies,
          Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermark);

        protected void EnableUnethicalReading() => PdfReader.unethicalreading = true;
    }
}
