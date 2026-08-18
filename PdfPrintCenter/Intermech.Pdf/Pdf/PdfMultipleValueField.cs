// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfMultipleValueField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public abstract class PdfMultipleValueField : PdfDynamicField
{
  private Dictionary<PdfGraphics, PdfTemplateValuePair> m_list;

  public PdfMultipleValueField()
  {
    this.m_list = new Dictionary<PdfGraphics, PdfTemplateValuePair>();
  }

  public PdfMultipleValueField(PdfFont font)
    : base(font)
  {
    this.m_list = new Dictionary<PdfGraphics, PdfTemplateValuePair>();
  }

  public PdfMultipleValueField(PdfFont font, PdfBrush brush)
    : base(font, brush)
  {
    this.m_list = new Dictionary<PdfGraphics, PdfTemplateValuePair>();
  }

  public PdfMultipleValueField(PdfFont font, RectangleF bounds)
    : base(font, bounds)
  {
    this.m_list = new Dictionary<PdfGraphics, PdfTemplateValuePair>();
  }

  protected internal override void PerformDraw(
    PdfGraphics graphics,
    PointF location,
    float scalingX,
    float scalingY)
  {
    base.PerformDraw(graphics, location, scalingX, scalingY);
    string s = this.GetValue(graphics);
    if (this.m_list.ContainsKey(graphics))
    {
      PdfTemplateValuePair templateValuePair = this.m_list[graphics];
      if (!(templateValuePair.Value != s))
        return;
      SizeF size = this.GetSize();
      templateValuePair.Template.Reset(size);
      templateValuePair.Template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, size), this.StringFormat);
    }
    else
    {
      PdfTemplate template = new PdfTemplate(this.GetSize());
      this.m_list[graphics] = new PdfTemplateValuePair(template, s);
      template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, this.GetSize()), this.StringFormat);
      PointF location1;
      ref PointF local = ref location1;
      double x1 = (double) location.X;
      PointF location2 = this.Location;
      double x2 = (double) location2.X;
      double x3 = x1 + x2;
      double y1 = (double) location.Y;
      location2 = this.Location;
      double y2 = (double) location2.Y;
      double y3 = y1 + y2;
      local = new PointF((float) x3, (float) y3);
      graphics.DrawPdfTemplate(template, location1, new SizeF(template.Width * scalingX, template.Height * scalingY));
    }
  }
}
