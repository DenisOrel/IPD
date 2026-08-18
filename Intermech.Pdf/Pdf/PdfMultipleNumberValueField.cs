// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfMultipleNumberValueField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public abstract class PdfMultipleNumberValueField : PdfMultipleValueField
    {
      private PdfNumberStyle m_numberStyle;

      public PdfMultipleNumberValueField() => this.m_numberStyle = PdfNumberStyle.Numeric;

      public PdfMultipleNumberValueField(PdfFont font)
        : base(font)
      {
        this.m_numberStyle = PdfNumberStyle.Numeric;
      }

      public PdfMultipleNumberValueField(PdfFont font, PdfBrush brush)
        : base(font, brush)
      {
        this.m_numberStyle = PdfNumberStyle.Numeric;
      }

      public PdfMultipleNumberValueField(PdfFont font, RectangleF bounds)
        : base(font, bounds)
      {
        this.m_numberStyle = PdfNumberStyle.Numeric;
      }

      public PdfNumberStyle NumberStyle
      {
        get => this.m_numberStyle;
        set => this.m_numberStyle = value;
      }
    }
}
