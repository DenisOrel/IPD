// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageNumberField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public class PdfPageNumberField : PdfMultipleNumberValueField
    {
      public PdfPageNumberField()
      {
      }

      public PdfPageNumberField(PdfFont font)
        : base(font)
      {
      }

      public PdfPageNumberField(PdfFont font, PdfBrush brush)
        : base(font, brush)
      {
      }

      public PdfPageNumberField(PdfFont font, RectangleF bounds)
        : base(font, bounds)
      {
      }

      protected internal override string GetValue(PdfGraphics graphics)
      {
        string str = (string) null;
        if (graphics.Page is PdfPage)
        {
          PdfPage pageFromGraphics = PdfDynamicField.GetPageFromGraphics(graphics);
          if (!(pageFromGraphics.Section.m_document is PdfLoadedDocument))
            return this.InternalGetValue(pageFromGraphics);
          PdfLoadedDocument document = pageFromGraphics.Section.m_document as PdfLoadedDocument;
          int count = (pageFromGraphics.Section.m_document as PdfLoadedDocument).Pages.Count;
          for (int index = 0; index < count; ++index)
          {
            if (document.Pages[index] is PdfPage && (document.Pages[index] as PdfPage).Dictionary.Equals((object) graphics.Page.Dictionary))
              str = (index + 1).ToString();
          }
          return str;
        }
        if (graphics.Page is PdfLoadedPage)
          str = this.InternalLoadedGetValue(PdfDynamicField.GetLoadedPageFromGraphics(graphics));
        return str;
      }

      protected string InternalGetValue(PdfPage page)
      {
        return PdfNumbersConvertor.Convert(page.Section.Parent.Document.Pages.IndexOf(page) + 1, this.NumberStyle);
      }

      protected string InternalLoadedGetValue(PdfLoadedPage page)
      {
        return PdfNumbersConvertor.Convert((page.Document as PdfLoadedDocument).Pages.IndexOf((PdfPageBase) page) + 1, this.NumberStyle);
      }
    }
}
