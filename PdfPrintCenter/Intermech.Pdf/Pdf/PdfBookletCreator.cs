// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfBookletCreator
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public sealed class PdfBookletCreator
{
  private PdfBookletCreator()
  {
    throw new NotSupportedException("Instantination of BookletCreator class is not supported");
  }

  public static PdfDocument CreateBooklet(PdfLoadedDocument loadedDocument, SizeF pageSize)
  {
    return PdfBookletCreator.CreateBooklet(loadedDocument, pageSize, false);
  }

  public static PdfDocument CreateBooklet(
    PdfLoadedDocument loadedDocument,
    SizeF pageSize,
    bool twoSide)
  {
    if (loadedDocument == null)
      throw new ArgumentNullException(nameof (loadedDocument));
    if (pageSize == SizeF.Empty)
      throw new ArgumentOutOfRangeException(nameof (pageSize), "Parameter can not be empty");
    SizeF size = new SizeF(pageSize.Width / 2f, pageSize.Height);
    PointF empty = PointF.Empty;
    PointF location = new PointF(size.Width, 0.0f);
    PdfDocument booklet = new PdfDocument();
    booklet.PageSettings.Margins.All = 0.0f;
    int count = loadedDocument.Pages.Count;
    PdfLoadedPageCollection pages = loadedDocument.Pages;
    int num = count / 2 + count % 2;
    bool flag = false;
    if (twoSide)
      flag = num % 2 == 0;
    for (int index1 = 0; index1 < num; ++index1)
    {
      booklet.PageSettings.Size = pageSize;
      if ((double) pageSize.Width > (double) pageSize.Height)
        booklet.PageSettings.Orientation = PdfPageOrientation.Landscape;
      PdfPage pdfPage = booklet.Pages.Add();
      int[] nextPair = PdfBookletCreator.GetNextPair(index1, count, twoSide);
      int index2 = nextPair[twoSide & flag ? 1 : 0];
      if (index2 >= 0)
      {
        PdfTemplate template = pages[index2].CreateTemplate();
        pdfPage.Graphics.DrawPdfTemplate(template, empty, size);
      }
      int index3 = nextPair[flag ? 0 : 1];
      if (index3 >= 0)
      {
        PdfTemplate template = pages[index3].CreateTemplate();
        pdfPage.Graphics.DrawPdfTemplate(template, location, size);
      }
    }
    return booklet;
  }

  public static void CreateBooklet(string from, string into, SizeF pageSize)
  {
    PdfBookletCreator.CreateBooklet(from, into, pageSize, false);
  }

  public static PdfDocument CreateBooklet(
    PdfLoadedDocument loadedDocument,
    SizeF pageSize,
    bool twoSide,
    PdfMargins margin)
  {
    if (loadedDocument == null)
      throw new ArgumentNullException(nameof (loadedDocument));
    if (pageSize == SizeF.Empty)
      throw new ArgumentOutOfRangeException(nameof (pageSize), "Parameter can not be empty");
    SizeF size = new SizeF(pageSize.Width / 2f, pageSize.Height);
    PointF empty = PointF.Empty;
    PointF location = new PointF(size.Width, 0.0f);
    PdfDocument booklet = new PdfDocument();
    booklet.PageSettings.Margins = margin;
    int count = loadedDocument.Pages.Count;
    PdfLoadedPageCollection pages = loadedDocument.Pages;
    int num = count / 2 + count % 2;
    bool flag = false;
    if (twoSide)
      flag = num % 2 == 0;
    for (int index1 = 0; index1 < num; ++index1)
    {
      booklet.PageSettings.Size = pageSize;
      PdfPage pdfPage = booklet.Pages.Add();
      int[] nextPair = PdfBookletCreator.GetNextPair(index1, count, twoSide);
      int index2 = nextPair[twoSide & flag ? 1 : 0];
      if (index2 >= 0)
      {
        PdfTemplate template = pages[index2].CreateTemplate();
        pdfPage.Graphics.DrawPdfTemplate(template, empty, size);
      }
      int index3 = nextPair[flag ? 0 : 1];
      if (index3 >= 0)
      {
        PdfTemplate template = pages[index3].CreateTemplate();
        pdfPage.Graphics.DrawPdfTemplate(template, location, size);
      }
    }
    return booklet;
  }

  public static void CreateBooklet(string from, string into, SizeF pageSize, bool twoSide)
  {
    if (from == null)
      throw new ArgumentNullException(nameof (from));
    if (from == string.Empty)
      throw new ArgumentOutOfRangeException(nameof (from), "Parameter can not be empty");
    if (into == null)
      throw new ArgumentNullException(nameof (into));
    if (into == string.Empty)
      throw new ArgumentOutOfRangeException(nameof (into), "Parameter can not be empty");
    if (pageSize == SizeF.Empty)
      throw new ArgumentOutOfRangeException(nameof (pageSize), "Parameter can not be empty");
    PdfDocument booklet = PdfBookletCreator.CreateBooklet(new PdfLoadedDocument(from), pageSize, twoSide);
    booklet.Save(into);
    booklet.Close();
  }

  private static int[] GetNextPair(int index, int count, bool twoSide)
  {
    int[] nextPair = new int[2];
    int num = count - index - (count + 1) % 2;
    if (num == count)
      num = -1;
    if (twoSide && index % 2 > 0)
    {
      nextPair[1] = index;
      nextPair[0] = num;
      return nextPair;
    }
    nextPair[0] = index;
    nextPair[1] = num;
    return nextPair;
  }
}
