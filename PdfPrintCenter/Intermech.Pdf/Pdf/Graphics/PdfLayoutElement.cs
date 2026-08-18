// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfLayoutElement
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.HtmlToPdf;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public abstract class PdfLayoutElement : PdfGraphicsElement
{
  private bool m_bEmbedFonts;

  public event BeginPageLayoutEventHandler BeginPageLayout;

  public event EndPageLayoutEventHandler EndPageLayout;

  public PdfLayoutResult Draw(PdfPage page, PointF location)
  {
    return this.Draw(page, location.X, location.Y);
  }

  public PdfLayoutResult Draw(PdfPage page, RectangleF layoutRectangle)
  {
    return this.Draw(page, layoutRectangle, (PdfLayoutFormat) null);
  }

  public PdfLayoutResult Draw(PdfPage page, PointF location, PdfLayoutFormat format)
  {
    return this.Draw(page, location.X, location.Y, format);
  }

  public PdfLayoutResult Draw(PdfPage page, RectangleF layoutRectangle, PdfLayoutFormat format)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    return this.Layout(new PdfLayoutParams()
    {
      Page = page,
      Bounds = layoutRectangle,
      Format = format != null ? format : new PdfLayoutFormat()
    });
  }

  internal PdfLayoutResult Draw(PdfPage page, RectangleF layoutRectangle, bool embedFonts)
  {
    this.m_bEmbedFonts = embedFonts;
    return this.Draw(page, layoutRectangle, (PdfLayoutFormat) null);
  }

  public PdfLayoutResult Draw(PdfPage page, float x, float y)
  {
    return this.Draw(page, x, y, (PdfLayoutFormat) null);
  }

  internal PdfLayoutResult Draw(
    PdfPage page,
    RectangleF bounds,
    float[] pageOffsets,
    PdfLayoutFormat format)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    return this.Layout(new HtmlToPdfLayoutParams()
    {
      VerticalOffsets = pageOffsets,
      Page = page,
      Bounds = bounds,
      Format = format != null ? format : new PdfLayoutFormat()
    });
  }

  public PdfLayoutResult Draw(PdfPage page, float x, float y, PdfLayoutFormat format)
  {
    RectangleF layoutRectangle = new RectangleF(x, y, 0.0f, 0.0f);
    return this.Draw(page, layoutRectangle, format);
  }

  protected abstract PdfLayoutResult Layout(PdfLayoutParams param);

  protected virtual PdfLayoutResult Layout(HtmlToPdfLayoutParams param) => (PdfLayoutResult) null;

  internal void OnBeginPageLayout(BeginPageLayoutEventArgs e)
  {
    if (this.BeginPageLayout == null)
      return;
    this.BeginPageLayout((object) this, e);
  }

  internal void OnEndPageLayout(EndPageLayoutEventArgs e)
  {
    if (this.EndPageLayout == null)
      return;
    this.EndPageLayout((object) this, e);
  }

  internal bool EmbedFontResource => this.m_bEmbedFonts;

  internal bool RaiseBeginPageLayout => this.BeginPageLayout != null;

  internal bool RaiseEndPageLayout => this.EndPageLayout != null;
}
