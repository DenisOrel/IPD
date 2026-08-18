// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfHTMLTextElement
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Images;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class PdfHTMLTextElement
{
  private PdfBrush m_brush;
  private PdfFont m_font;
  private string m_htmlText;
  private TextAlign m_textAlign;

  public PdfHTMLTextElement()
  {
    this.m_font = (PdfFont) new PdfStandardFont(PdfFontFamily.Helvetica, 3f);
    this.m_brush = PdfBrushes.Black;
    this.m_htmlText = "";
    this.m_textAlign = TextAlign.Left;
  }

  public PdfHTMLTextElement(string htmlText, PdfFont font, PdfBrush brush)
  {
    this.m_htmlText = htmlText;
    this.m_font = font;
    this.m_brush = brush;
  }

  public void Draw(PdfGraphics graphics, RectangleF layoutRectangle)
  {
    if (graphics == null)
      throw new ArgumentNullException(nameof (graphics));
    if ((double) layoutRectangle.Height < 0.0)
      throw new ArgumentNullException("height");
    RichTextBoxExt richTextBox = new RichTextBoxExt();
    richTextBox.RenderHTML(this.m_htmlText, this.m_font, this.m_brush);
    richTextBox.SelectAll();
    richTextBox.SelectionAlignment = this.m_textAlign;
    richTextBox.Width = (int) layoutRectangle.Width;
    PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor();
    float width = pdfUnitConvertor.ConvertUnits(layoutRectangle.Width, PdfGraphicsUnit.Point, PdfGraphicsUnit.Pixel);
    float height = pdfUnitConvertor.ConvertUnits(layoutRectangle.Height, PdfGraphicsUnit.Point, PdfGraphicsUnit.Pixel);
    PdfImage pdfImage = PdfImage.FromImage(RtfToImage.ConvertToMetafile(richTextBox, width, height));
    richTextBox.Dispose();
    PdfGraphics graphics1 = graphics;
    PointF location = new PointF(layoutRectangle.X, layoutRectangle.Y);
    pdfImage.Draw(graphics1, location);
  }

  public PdfLayoutResult Draw(
    PdfPage page,
    RectangleF layoutRectangle,
    PdfMetafileLayoutFormat format)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    if ((double) layoutRectangle.Height < 0.0)
      throw new ArgumentNullException("height");
    RichTextBoxExt richTextBox = new RichTextBoxExt();
    richTextBox.RenderHTML(this.m_htmlText, this.m_font, this.m_brush);
    richTextBox.SelectAll();
    richTextBox.SelectionAlignment = this.m_textAlign;
    richTextBox.Width = (int) layoutRectangle.Width;
    PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor();
    float width = pdfUnitConvertor.ConvertUnits(layoutRectangle.Width, PdfGraphicsUnit.Point, PdfGraphicsUnit.Pixel);
    float height = pdfUnitConvertor.ConvertUnits(layoutRectangle.Height, PdfGraphicsUnit.Point, PdfGraphicsUnit.Pixel);
    PdfImage pdfImage = PdfImage.FromImage(RtfToImage.ConvertToMetafile(richTextBox, width, height));
    richTextBox.Dispose();
    PdfPage page1 = page;
    PointF location = new PointF(layoutRectangle.X, layoutRectangle.Y);
    PdfMetafileLayoutFormat format1 = format;
    return pdfImage.Draw(page1, location, (PdfLayoutFormat) format1);
  }

  public void Draw(PdfGraphics graphics, PointF location, float width, float height)
  {
    RectangleF layoutRectangle = new RectangleF(location, new SizeF(width, height));
    this.Draw(graphics, layoutRectangle);
  }

  public PdfLayoutResult Draw(
    PdfPage page,
    PointF location,
    float width,
    PdfMetafileLayoutFormat format)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor();
    width = pdfUnitConvertor.ConvertToPixels(width, PdfGraphicsUnit.Point);
    RichTextBoxExt richTextBoxExt = new RichTextBoxExt();
    richTextBoxExt.RenderHTML(this.m_htmlText, this.m_font, this.m_brush);
    richTextBoxExt.SelectAll();
    richTextBoxExt.SelectionAlignment = this.m_textAlign;
    Image image = RtfToImage.ConvertToImage(richTextBoxExt.Rtf, width, -1f, PdfImageType.Metafile);
    RectangleF layoutRectangle = new RectangleF(location, new SizeF(pdfUnitConvertor.ConvertFromPixels(width, PdfGraphicsUnit.Point), pdfUnitConvertor.ConvertFromPixels((float) image.Height, PdfGraphicsUnit.Point)));
    richTextBoxExt.Dispose();
    return this.Draw(page, layoutRectangle, format);
  }

  public PdfLayoutResult Draw(
    PdfPage page,
    PointF location,
    float width,
    float height,
    PdfMetafileLayoutFormat format)
  {
    RectangleF layoutRectangle = new RectangleF(location, new SizeF(width, height));
    return this.Draw(page, layoutRectangle, format);
  }

  public PdfBrush Brush
  {
    get => this.m_brush;
    set => this.m_brush = value;
  }

  public PdfFont Font
  {
    get => this.m_font;
    set => this.m_font = value;
  }

  public string HTMLText
  {
    get => this.m_htmlText;
    set => this.m_htmlText = value;
  }

  public TextAlign TextAlign
  {
    get => this.m_textAlign;
    set => this.m_textAlign = value;
  }
}
