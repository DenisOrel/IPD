// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDynamicField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public abstract class PdfDynamicField : PdfAutomaticField
    {
      public PdfDynamicField()
      {
      }

      public PdfDynamicField(PdfFont font)
        : base(font)
      {
      }

      public PdfDynamicField(PdfFont font, PdfBrush brush)
        : base(font, brush)
      {
      }

      public PdfDynamicField(PdfFont font, RectangleF bounds)
        : base(font, bounds)
      {
      }

      internal static PdfLoadedPage GetLoadedPageFromGraphics(PdfGraphics graphics)
      {
        if (graphics.Page is PdfLoadedPage page)
          return page;
        throw new NotSupportedException("The field was placed on not PdfPage class instance.");
      }

      internal static PdfPage GetPageFromGraphics(PdfGraphics graphics)
      {
        if (graphics.Page is PdfPage page)
          return page;
        throw new NotSupportedException("The field was placed on not PdfPage class instance.");
      }
    }
}
