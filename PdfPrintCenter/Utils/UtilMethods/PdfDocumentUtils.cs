
// Type: Intermech.PdfPrintCenter.Utils.UtilMethods.PdfDocumentUtils




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.DocumentCreators;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using PdfiumViewer;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Intermech.PdfPrintCenter.Utils.UtilMethods
{
    internal static class PdfDocumentUtils
    {
      public static bool IsPdfFilePath(string filePath)
      {
        return !string.IsNullOrEmpty(filePath) && Path.GetExtension(filePath).ToLower() == ".pdf";
      }

      public static PdfDocument MakePdfDocumentWithChosenLayout(
        List<PrintQueuePagesNode> nodes,
        Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermark = null)
      {
        IPdfPageProducer layout = nodes.First<PrintQueuePagesNode>()?.Parent is LayoutNode parent ? parent.Layout : (IPdfPageProducer) null;
        if (layout == null)
          return (PdfDocument) null;
        List<string> inputFiles = new List<string>();
        List<string> ranges = new List<string>();
        foreach (PrintQueuePagesNode node in nodes)
        {
          for (int index = 0; index < (int) short.Parse(node.Copies); ++index)
          {
            inputFiles.Add(node.FilePath);
            ranges.Add(node.Pages);
          }
        }
        byte[] pdfWithLayout = DocCreatorsFactory.GetDocCreator(layout)?.CreateDocument(inputFiles, ranges, 1, watermark)?.PdfWithLayout;
        return pdfWithLayout == null ? (PdfDocument) null : PdfDocument.Load((Stream) new MemoryStream(pdfWithLayout));
      }

      public static PdfDocument MakePdfDocumentWithChosenLayout(
        string documentPath,
        List<PageInterval> pages,
        IPdfPageProducer layout,
        Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermark = null)
      {
        string str = string.Join<PageInterval>(",", (IEnumerable<PageInterval>) pages);
        DocCreator docCreator = DocCreatorsFactory.GetDocCreator(layout);
        byte[] numArray;
        if (docCreator == null)
          numArray = (byte[]) null;
        else
          numArray = docCreator.CreateDocument(new List<string>()
          {
            documentPath
          }, new List<string>() { str }, 1, watermark)?.PdfWithLayout;
        byte[] buffer = numArray;
        return buffer == null ? (PdfDocument) null : PdfDocument.Load((Stream) new MemoryStream(buffer));
      }
    }
}
