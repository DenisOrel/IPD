// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLinkAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfLinkAnnotation : PdfAnnotation
    {
      private PdfHighlightMode m_highlightMode;

      public PdfLinkAnnotation()
      {
      }

      public PdfLinkAnnotation(RectangleF rectangle)
        : base(rectangle)
      {
      }

      private string GetHighlightMode(PdfHighlightMode mode)
      {
        switch (mode)
        {
          case PdfHighlightMode.NoHighlighting:
            return "N";
          case PdfHighlightMode.Invert:
            return "I";
          case PdfHighlightMode.Outline:
            return "O";
          case PdfHighlightMode.Push:
            return "P";
          default:
            return (string) null;
        }
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Link"));
      }

      public PdfHighlightMode HighlightMode
      {
        get => this.m_highlightMode;
        set
        {
          this.m_highlightMode = value;
          this.Dictionary.SetName("H", this.GetHighlightMode(this.m_highlightMode));
        }
      }
    }
}
