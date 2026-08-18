// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfCreationDateField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfCreationDateField : PdfSingleValueField
{
  private string m_formatString;

  public PdfCreationDateField() => this.m_formatString = "dd'/'MM'/'yyyy";

  public PdfCreationDateField(PdfFont font)
    : base(font)
  {
    this.m_formatString = "dd'/'MM'/'yyyy";
  }

  public PdfCreationDateField(PdfFont font, PdfBrush brush)
    : base(font, brush)
  {
    this.m_formatString = "dd'/'MM'/'yyyy";
  }

  public PdfCreationDateField(PdfFont font, RectangleF bounds)
    : base(font, bounds)
  {
    this.m_formatString = "dd'/'MM'/'yyyy";
  }

  protected internal override string GetValue(PdfGraphics graphics)
  {
    string str = (string) null;
    if (graphics.Page is PdfPage)
    {
      PdfPage pageFromGraphics = PdfDynamicField.GetPageFromGraphics(graphics);
      return pageFromGraphics.Section.m_document is PdfLoadedDocument ? (pageFromGraphics.Section.m_document as PdfLoadedDocument).DocumentInformation.CreationDate.ToString(this.m_formatString) : pageFromGraphics.Document.DocumentInformation.CreationDate.ToString(this.m_formatString);
    }
    if (graphics.Page is PdfLoadedPage)
      str = (PdfDynamicField.GetLoadedPageFromGraphics(graphics).Document as PdfLoadedDocument).DocumentInformation.CreationDate.ToString(this.m_formatString);
    return str;
  }

  public string DateFormatString
  {
    get => this.m_formatString;
    set => this.m_formatString = value;
  }
}
