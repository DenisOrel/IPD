// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.ElementLayouter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.HtmlToPdf;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

internal abstract class ElementLayouter
{
  private PdfLayoutElement m_element;
  protected bool m_isImagePath;

  public ElementLayouter(PdfLayoutElement element)
  {
    this.m_element = element != null ? element : throw new ArgumentNullException(nameof (element));
  }

  public PdfPage GetNextPage(PdfPage currentPage)
  {
    PdfSection pdfSection = currentPage != null ? currentPage.Section : throw new ArgumentNullException(nameof (currentPage));
    int num = pdfSection.IndexOf(currentPage);
    return num == pdfSection.Count - 1 ? pdfSection.Add() : pdfSection[num + 1];
  }

  protected RectangleF GetPaginateBounds(PdfLayoutParams param)
  {
    if (param == null)
      throw new ArgumentNullException(nameof (param));
    return !param.Format.UsePaginateBounds ? new RectangleF(param.Bounds.X, 0.0f, param.Bounds.Width, param.Bounds.Height) : param.Format.PaginateBounds;
  }

  public PdfLayoutResult Layout(PdfLayoutParams param)
  {
    return param != null ? this.LayoutInternal(param) : throw new ArgumentNullException(nameof (param));
  }

  public PdfLayoutResult Layout(HtmlToPdfLayoutParams param)
  {
    return param != null ? this.LayoutInternal(param) : throw new ArgumentNullException(nameof (param));
  }

  protected abstract PdfLayoutResult LayoutInternal(PdfLayoutParams param);

  protected virtual PdfLayoutResult LayoutInternal(HtmlToPdfLayoutParams param)
  {
    return (PdfLayoutResult) null;
  }

  public PdfLayoutElement Element => this.m_element;

  internal bool IsImagePath
  {
    get => this.m_isImagePath;
    set => this.m_isImagePath = value;
  }
}
