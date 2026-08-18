// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfStaticField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Collections.Generic;
using System.Drawing;


namespace Syncfusion.Pdf
{
    public abstract class PdfStaticField : PdfAutomaticField
    {
      private List<PdfGraphics> m_graphicsList;
      private PdfTemplate m_template;

      public PdfStaticField() => this.m_graphicsList = new List<PdfGraphics>();

      public PdfStaticField(PdfFont font)
        : base(font)
      {
        this.m_graphicsList = new List<PdfGraphics>();
      }

      public PdfStaticField(PdfFont font, PdfBrush brush)
        : base(font, brush)
      {
        this.m_graphicsList = new List<PdfGraphics>();
      }

      public PdfStaticField(PdfFont font, RectangleF bounds)
        : base(font, bounds)
      {
        this.m_graphicsList = new List<PdfGraphics>();
      }

      protected internal override void PerformDraw(
        PdfGraphics graphics,
        PointF location,
        float scalingX,
        float scalingY)
      {
        base.PerformDraw(graphics, location, scalingX, scalingY);
        string s = this.GetValue(graphics);
        PointF location1 = new PointF(location.X + this.Location.X, location.Y + this.Location.Y);
        if (this.m_template == null)
        {
          this.m_template = new PdfTemplate(this.GetSize());
          this.m_template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, this.GetSize()), this.StringFormat);
          graphics.DrawPdfTemplate(this.m_template, location1, new SizeF(this.m_template.Width * scalingX, this.m_template.Height * scalingY));
          this.m_graphicsList.Add(graphics);
        }
        else
        {
          if (this.m_graphicsList.Contains(graphics))
            return;
          graphics.DrawPdfTemplate(this.m_template, location1, new SizeF(this.m_template.Width * scalingX, this.m_template.Height * scalingY));
          this.m_graphicsList.Add(graphics);
        }
      }
    }
}
