using Intermech.PdfPrintCenter.Utils;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.DocumentCreators
{
    internal class AsItIsDocCreator : DocCreator
    {
        public AsItIsDocCreator(LayoutAsItIs layoutAsItIs) : base((IPdfPageProducer)layoutAsItIs)
        {
        }

        public override AddingPagesToLayoutResult CreateDocument(
          List<string> filenames,
          List<string> pageRanges,
          int copies,
          Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermark)
        {
            this.EnableUnethicalReading();
            using (MemoryStream os = new MemoryStream())
            {
                using (Document document = new Document())
                {
                    try
                    {
                        PdfWriter instance = PdfWriter.GetInstance(document, (Stream)os);
                        instance.CloseStream = false;
                        document.Open();
                        PdfContentByte directContent = instance.DirectContent;
                        for (int index = 0; index < pageRanges.Count; ++index)
                        {
                            string filename = filenames[index];
                            string pageRange = pageRanges[index];
                            PdfReader reader = new PdfReader(filename);
                            foreach (int num in PdfUtils.RangeToList(pageRange))
                            {
                                Rectangle sizeWithRotation = reader.GetPageSizeWithRotation(num);
                                document.SetPageSize(sizeWithRotation);
                                document.NewPage();
                                PdfImportedPage importedPage = instance.GetImportedPage(reader, num);
                                if (importedPage.Rotation == 90 || importedPage.Rotation == 270)
                                    directContent.AddTemplate((PdfTemplate)importedPage, 0.0f, -1f, 1f, 0.0f, 0.0f, sizeWithRotation.Height);
                                else if (importedPage.Rotation == 180)
                                    directContent.AddTemplate((PdfTemplate)importedPage, -1f, 0.0f, 0.0f, -1f, sizeWithRotation.Width, sizeWithRotation.Height);
                                else
                                    directContent.AddTemplate((PdfTemplate)importedPage, 0.0f, 0.0f);
                                if (watermark != null)
                                    instance.PrintWatermark(watermark, sizeWithRotation);
                            }
                        }
                    }
                    finally
                    {
                        document.Close();
                    }
                }
                return new AddingPagesToLayoutResult(os.ToArray());
            }
        }
    }
}
