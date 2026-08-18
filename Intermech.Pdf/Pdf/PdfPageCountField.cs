// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageCountField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public class PdfPageCountField : PdfSingleValueField
    {
      private PdfNumberStyle m_numberStyle;

      public PdfPageCountField() => this.m_numberStyle = PdfNumberStyle.Numeric;

      public PdfPageCountField(PdfFont font)
        : base(font)
      {
        this.m_numberStyle = PdfNumberStyle.Numeric;
      }

      public PdfPageCountField(PdfFont font, PdfBrush brush)
        : base(font, brush)
      {
        this.m_numberStyle = PdfNumberStyle.Numeric;
      }

      public PdfPageCountField(PdfFont font, RectangleF bounds)
        : base(font, bounds)
      {
        this.m_numberStyle = PdfNumberStyle.Numeric;
      }

      protected internal override string GetValue(PdfGraphics graphics)
      {
        string str = (string) null;
        if (graphics.Page is PdfPage)
        {
          PdfPage pageFromGraphics = PdfDynamicField.GetPageFromGraphics(graphics);
          if (!(pageFromGraphics.Section.m_document is PdfLoadedDocument))
            return PdfNumbersConvertor.Convert(pageFromGraphics.Section.Parent.Document.Pages.Count, this.NumberStyle);
          PdfDocumentBase document = pageFromGraphics.Section.m_document;
          return (pageFromGraphics.Section.m_document as PdfLoadedDocument).Pages.Count.ToString();
        }
        if (graphics.Page is PdfLoadedPage)
          str = PdfNumbersConvertor.Convert((PdfDynamicField.GetLoadedPageFromGraphics(graphics).Document as PdfLoadedDocument).Pages.Count, this.NumberStyle);
        return str;
      }

      public PdfNumberStyle NumberStyle
      {
        get => this.m_numberStyle;
        set => this.m_numberStyle = value;
      }
    }
}
