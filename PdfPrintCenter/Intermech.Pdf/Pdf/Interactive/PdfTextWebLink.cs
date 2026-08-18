// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfTextWebLink
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfTextWebLink : PdfTextElement
{
  private PdfUriAnnotation m_uriAnnotation;
  private string m_url;

  public void DrawTextWebLink(PdfGraphics graphics, PointF location)
  {
    if (graphics.Page is PdfLoadedPage)
    {
      SizeF size = this.Font.MeasureString(this.Value);
      this.m_uriAnnotation = new PdfUriAnnotation(new RectangleF(new PointF(location.X, graphics.Page.Size.Height - (location.Y + 40f)), size), this.Url);
      this.m_uriAnnotation.Border = new PdfAnnotationBorder(0.0f, 0.0f, 0.0f);
      graphics.Page.Annotations.Add((PdfAnnotation) this.m_uriAnnotation);
      this.Draw(graphics, location);
    }
    else
    {
      PdfPage pdfPage = new PdfPage();
      SizeF size = this.Font.MeasureString(this.Value);
      this.m_uriAnnotation = new PdfUriAnnotation(new RectangleF(location, size), this.Url);
      this.m_uriAnnotation.Border = new PdfAnnotationBorder(0.0f, 0.0f, 0.0f);
      (graphics.Page as PdfPage).Annotations.Add((PdfAnnotation) this.m_uriAnnotation);
      this.Draw(graphics, location);
    }
  }

  public PdfLayoutResult DrawTextWebLink(PdfPage page, PointF location)
  {
    SizeF size = this.Font.MeasureString(this.Value);
    this.m_uriAnnotation = new PdfUriAnnotation(new RectangleF(location, size), this.Url);
    this.m_uriAnnotation.Border = new PdfAnnotationBorder(0.0f, 0.0f, 0.0f);
    page.Annotations.Add((PdfAnnotation) this.m_uriAnnotation);
    return this.Draw(page, location);
  }

  public string Url
  {
    get => this.m_url;
    set
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException("url");
        case "":
          throw new ArgumentException("Url - string can not be empty");
        default:
          this.m_url = value;
          break;
      }
    }
  }
}
