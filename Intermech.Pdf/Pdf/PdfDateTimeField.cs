// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDateTimeField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public class PdfDateTimeField : PdfStaticField
    {
      private DateTime m_date;
      private string m_formatString;

      public PdfDateTimeField()
      {
        this.m_date = DateTime.Now;
        this.m_formatString = "dd'/'MM'/'yyyy hh':'mm':'ss";
      }

      public PdfDateTimeField(PdfFont font)
        : base(font)
      {
        this.m_date = DateTime.Now;
        this.m_formatString = "dd'/'MM'/'yyyy hh':'mm':'ss";
      }

      public PdfDateTimeField(PdfFont font, PdfBrush brush)
        : base(font, brush)
      {
        this.m_date = DateTime.Now;
        this.m_formatString = "dd'/'MM'/'yyyy hh':'mm':'ss";
      }

      public PdfDateTimeField(PdfFont font, RectangleF bounds)
        : base(font, bounds)
      {
        this.m_date = DateTime.Now;
        this.m_formatString = "dd'/'MM'/'yyyy hh':'mm':'ss";
      }

      protected internal override string GetValue(PdfGraphics graphics)
      {
        return this.m_date.ToString(this.m_formatString);
      }

      public string DateFormatString
      {
        get => this.m_formatString;
        set => this.m_formatString = value;
      }
    }
}
