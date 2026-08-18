// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDocumentAuthorField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public class PdfDocumentAuthorField : PdfSingleValueField
    {
      public PdfDocumentAuthorField()
      {
      }

      public PdfDocumentAuthorField(PdfFont font)
        : base(font)
      {
      }

      public PdfDocumentAuthorField(PdfFont font, PdfBrush brush)
        : base(font, brush)
      {
      }

      public PdfDocumentAuthorField(PdfFont font, RectangleF bounds)
        : base(font, bounds)
      {
      }

      protected internal override string GetValue(PdfGraphics graphics)
      {
        string str = (string) null;
        if (graphics.Page is PdfPage)
          return PdfDynamicField.GetPageFromGraphics(graphics).Document.DocumentInformation.Author;
        if (graphics.Page is PdfLoadedPage)
          str = PdfDynamicField.GetLoadedPageFromGraphics(graphics).Document.DocumentInformation.Author;
        return str;
      }
    }
}
