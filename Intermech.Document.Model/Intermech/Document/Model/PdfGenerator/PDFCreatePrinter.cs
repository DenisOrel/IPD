// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.PdfGenerator.PDFCreatePrinter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.RtfEditor;
using Intermech.Interfaces.Document;
using Intermech.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Document.Model.PdfGenerator;

public class PDFCreatePrinter
{
  private static readonly PDFCreatePrinter.Destructor Finalise = new PDFCreatePrinter.Destructor();
  private static PrintDocument pd = new PrintDocument();

  internal static void Close()
  {
  }

  public static void SaveToPdf(
    DocumentsComplect docComplect,
    Stream outputStream,
    bool showProgress = false)
  {
    PDFCreatePrinter.SaveToPdf((PrintDocument) null, docComplect.GetAllDocuments().ToArray(), outputStream, showProgress);
  }

  public static void SaveToPdf(
    DocumentsComplect docComplect,
    string fileName,
    bool autostart,
    bool showProgress = false)
  {
    PDFCreatePrinter.SaveToPdf((PrintDocument) null, docComplect.GetAllDocuments().ToArray(), fileName, autostart, showProgress);
  }

  public static void SaveToPdf(
    PrintDocument printdoc,
    ImDocumentData[] docs,
    Stream outputStream,
    bool showProgress = false)
  {
    PDFCreatePrinter.SaveToSyncDrawPdf(docs, outputStream, showProgress);
  }

  public static void SaveToPdf(
    PrintDocument printdoc,
    ImDocumentData[] docs,
    string fileName,
    bool autostart,
    bool showProgress = false)
  {
    fileName = Path.ChangeExtension(fileName, ".pdf");
    PDFCreatePrinter.SaveToSyncDrawPdf(docs, fileName, autostart, showProgress);
  }

  private static void SaveToSyncDrawPdf(
    ImDocumentData[] docs,
    string fileName,
    bool autoStart,
    bool showProgress)
  {
    PDFCreatePrinter.SaveToSyncDrawPdf(docs, (Stream) null, fileName, autoStart, showProgress);
  }

  private static void SaveToSyncDrawPdf(
    ImDocumentData[] docs,
    Stream outputStream,
    bool showProgress)
  {
    PDFCreatePrinter.SaveToSyncDrawPdf(docs, outputStream, (string) null, false, showProgress);
  }

  private static void SaveToSyncDrawPdf(
    ImDocumentData[] docs,
    Stream outputStream,
    string fileName,
    bool autoStart,
    bool showProgress)
  {
    if (showProgress)
    {
      BackgroundWorker worker = new BackgroundWorker();
      worker.WorkerReportsProgress = true;
      worker.WorkerSupportsCancellation = true;
      worker.DoWork += new DoWorkEventHandler(PDFCreatePrinter.bw_DoWork);
      worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PDFCreatePrinter.bw_RunWorkerCompleted);
      object obj = (object) new object[4]
      {
        (object) docs,
        (object) outputStream,
        (object) fileName,
        (object) autoStart
      };
      int totalpages = 0;
      foreach (ImDocument doc in docs)
      {
        if (doc != null)
        {
          foreach (Page page in (ImDocumentData) doc)
          {
            if (page != null)
              ++totalpages;
          }
        }
      }
      int num = (int) new ProgressPdfForm(worker, totalpages, obj).ShowDialog();
    }
    else
      PDFCreatePrinter.SaveToSyncDrawPdfInThread((BackgroundWorker) null, docs, outputStream, fileName, autoStart);
  }

  private static bool SaveToSyncDrawPdfInThread(
    BackgroundWorker bw,
    ImDocumentData[] docs,
    Stream outputStream,
    string fileName,
    bool autoStart)
  {
    FileStream fileStream = (FileStream) null;
    if (outputStream == null)
    {
      fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
      outputStream = (Stream) fileStream;
    }
    List<ImChunkedStream> imChunkedStreamList = new List<ImChunkedStream>();
    List<string> stringList = new List<string>();
    PdfDocument pdfDocument = new PdfDocument();
    PdfLoadedDocument pdfLoadedDocument = (PdfLoadedDocument) null;
    int percentProgress = 0;
    int num = 0;
    bool flag1 = false;
    string path = "E:\\PdfPages";
    if (flag1)
    {
      if (Directory.Exists(path))
        Directory.Delete(path, true);
      Directory.CreateDirectory(path);
    }
    bool flag2 = true;
    ImRtfEditor ternPaintBuffer = RtfInSiteEditorWrapper.CreateTernPaintBuffer();
    foreach (ImDocument doc in docs)
    {
      if (doc != null)
      {
        foreach (Page page in (ImDocumentData) doc)
        {
          if (page != null)
          {
            ++percentProgress;
            if (num > 0)
            {
              num = 0;
              Stream stream;
              if (flag1)
              {
                if (!Directory.Exists(path))
                  Directory.CreateDirectory(path);
                stream = (Stream) new FileStream($"{path}\\{(object) percentProgress}.pdf", FileMode.CreateNew);
              }
              else
                stream = (Stream) new MemoryStream();
              if (flag2)
              {
                pdfDocument.Save(outputStream);
                pdfDocument.Close(true);
                flag2 = false;
              }
              else
              {
                pdfDocument.Save(stream);
                pdfDocument.Close(true);
                if (pdfLoadedDocument == null)
                  pdfLoadedDocument = new PdfLoadedDocument(outputStream);
                PdfLoadedDocument ldDoc = new PdfLoadedDocument(stream);
                pdfLoadedDocument.Append(ldDoc);
                ldDoc.Close(true);
              }
              stream.Close();
              pdfDocument = new PdfDocument();
            }
            ++num;
            if (bw != null && bw.CancellationPending)
              return false;
            PdfSection pdfSection = pdfDocument.Sections.Add();
            pdfSection.PageSettings.Margins = new PdfMargins();
            SizeF sizeF;
            ref SizeF local = ref sizeF;
            SizeF size = page.Size;
            double pointsF1 = (double) UnitsConverter.MmToPointsF(size.Width);
            size = page.Size;
            double pointsF2 = (double) UnitsConverter.MmToPointsF(size.Height);
            local = new SizeF((float) pointsF1, (float) pointsF2);
            pdfSection.PageSettings.Orientation = (double) sizeF.Width <= (double) sizeF.Height ? PdfPageOrientation.Portrait : PdfPageOrientation.Landscape;
            pdfSection.PageSettings.Size = sizeF;
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage((Image) new Bitmap(1, 1)))
            {
              DrawContextWithUI context = new DrawContextWithUI(new DrawContext(new ImGraphics(g), false, VisualNode.NoClipRectangle, 0, false, false, new MatrixWrapper()))
              {
                Document = page.OwnerDocument as ImDocument
              };
              context.TernPaintBuffer = ternPaintBuffer;
              page.Draw((DrawContext) context);
            }
            PdfImGraphics g1 = new PdfImGraphics(pdfSection.Pages.Add().Graphics);
            page.PrintBounds = false;
            RectangleF noClipRectangle = VisualNode.NoClipRectangle;
            MatrixWrapper transformMatrix = new MatrixWrapper();
            DrawContextWithUI drawContextWithUi = new DrawContextWithUI(new DrawContext((ImGraphics) g1, false, noClipRectangle, 0, false, false, transformMatrix));
            drawContextWithUi.IsPdf = true;
            drawContextWithUi.Document = page.OwnerDocument as ImDocument;
            DrawContextWithUI context1 = drawContextWithUi;
            context1.TernPaintBuffer = ternPaintBuffer;
            page.Draw((DrawContext) context1);
            bw?.ReportProgress(percentProgress);
          }
        }
      }
    }
    ternPaintBuffer.Dispose();
    if (flag2)
    {
      pdfDocument.Save(outputStream);
      pdfDocument.Close(true);
    }
    else
    {
      Stream stream = (Stream) new MemoryStream();
      pdfDocument.Save(stream);
      pdfDocument.Close(true);
      if (pdfLoadedDocument == null)
        pdfLoadedDocument = new PdfLoadedDocument(outputStream);
      PdfLoadedDocument ldDoc = new PdfLoadedDocument(stream);
      pdfLoadedDocument.Append(ldDoc);
      pdfLoadedDocument.Save(outputStream);
      pdfLoadedDocument.Close(true);
      ldDoc.Close(true);
    }
    if (fileStream != null)
    {
      fileStream.Dispose();
      if (autoStart)
        Process.Start(fileName);
    }
    return true;
  }

  private static void MergePdf(List<ImChunkedStream> streams, List<string> paths, string fileName)
  {
    PdfLoadedDocument pdfLoadedDocument = new PdfLoadedDocument(paths[0]);
    for (int index = 1; index < paths.Count; ++index)
    {
      PdfLoadedDocument ldDoc = new PdfLoadedDocument(paths[index]);
      pdfLoadedDocument.Append(ldDoc);
      pdfLoadedDocument.Save(fileName);
      pdfLoadedDocument.Close(true);
      ldDoc.Close(true);
      if (index < paths.Count - 1)
      {
        pdfLoadedDocument.Dispose();
        pdfLoadedDocument = new PdfLoadedDocument(fileName);
      }
    }
    pdfLoadedDocument.Dispose();
  }

  private static void bw_DoWork(object sender, DoWorkEventArgs e)
  {
    Array array = e.Argument as Array;
    ImDocumentData[] docs = array.GetValue(0) as ImDocumentData[];
    Stream outputStream = array.GetValue(1) as Stream;
    string fileName = array.GetValue(2) as string;
    bool flag = (bool) array.GetValue(3);
    object[] objArray = new object[2]
    {
      (object) (PDFCreatePrinter.SaveToSyncDrawPdfInThread(sender as BackgroundWorker, docs, outputStream, fileName, false) & flag),
      (object) fileName
    };
    e.Result = (object) objArray;
  }

  private static void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    Array result = e.Result as Array;
    bool flag = (bool) result.GetValue(0);
    string fileName = result.GetValue(1) as string;
    if (!flag)
      return;
    Process.Start(fileName);
  }

  [DllImport("gdiplus.dll", SetLastError = true)]
  private static extern int GdipEmfToWmfBits(
    int hEmf,
    int uBufferSize,
    byte[] bBuffer,
    int iMappingMode,
    PDFCreatePrinter.EmfToWmfBitsFlags flags);

  private static byte[] meth(Metafile mf)
  {
    int int32 = mf.GetHenhmetafile().ToInt32();
    int wmfBits = PDFCreatePrinter.GdipEmfToWmfBits(int32, 0, (byte[]) null, 8, PDFCreatePrinter.EmfToWmfBitsFlags.EmfToWmfBitsFlagsIncludePlaceable);
    byte[] bBuffer = new byte[wmfBits];
    PDFCreatePrinter.GdipEmfToWmfBits(int32, wmfBits, bBuffer, 8, PDFCreatePrinter.EmfToWmfBitsFlags.EmfToWmfBitsFlagsIncludePlaceable);
    return bBuffer;
  }

  private sealed class Destructor
  {
    ~Destructor() => PDFCreatePrinter.Close();
  }

  [Flags]
  private enum EmfToWmfBitsFlags
  {
    EmfToWmfBitsFlagsDefault = 0,
    EmfToWmfBitsFlagsEmbedEmf = 1,
    EmfToWmfBitsFlagsIncludePlaceable = 2,
    EmfToWmfBitsFlagsNoXORClip = 4,
  }
}
