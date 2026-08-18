// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfSingleValueField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public abstract class PdfSingleValueField : PdfDynamicField
{
  private Dictionary<PdfDocumentBase, PdfTemplateValuePair> m_list;
  private List<PdfGraphics> m_painterGraphics;

  public PdfSingleValueField()
  {
    this.m_list = new Dictionary<PdfDocumentBase, PdfTemplateValuePair>();
    this.m_painterGraphics = new List<PdfGraphics>();
  }

  public PdfSingleValueField(PdfFont font)
    : base(font)
  {
    this.m_list = new Dictionary<PdfDocumentBase, PdfTemplateValuePair>();
    this.m_painterGraphics = new List<PdfGraphics>();
  }

  public PdfSingleValueField(PdfFont font, PdfBrush brush)
    : base(font, brush)
  {
    this.m_list = new Dictionary<PdfDocumentBase, PdfTemplateValuePair>();
    this.m_painterGraphics = new List<PdfGraphics>();
  }

  public PdfSingleValueField(PdfFont font, RectangleF bounds)
    : base(font, bounds)
  {
    this.m_list = new Dictionary<PdfDocumentBase, PdfTemplateValuePair>();
    this.m_painterGraphics = new List<PdfGraphics>();
  }

  protected internal override void PerformDraw(
    PdfGraphics graphics,
    PointF location,
    float scalingX,
    float scalingY)
  {
    if (graphics.Page is PdfPage)
    {
      base.PerformDraw(graphics, location, scalingX, scalingY);
      PdfPage pageFromGraphics = PdfDynamicField.GetPageFromGraphics(graphics);
      if (pageFromGraphics.Section.m_document is PdfLoadedDocument)
      {
        PdfLoadedDocument document1 = pageFromGraphics.Section.m_document as PdfLoadedDocument;
        base.PerformDraw(graphics, location, scalingX, scalingY);
        PdfDynamicField.GetPageFromGraphics(graphics);
        PdfLoadedDocument document2 = pageFromGraphics.Section.m_document as PdfLoadedDocument;
        string s = this.GetValue(graphics);
        if (!this.m_list.ContainsKey((PdfDocumentBase) document2))
        {
          PdfTemplate template = new PdfTemplate(this.GetSize());
          this.m_list[(PdfDocumentBase) document1] = new PdfTemplateValuePair(template, s);
          template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, this.GetSize()), this.StringFormat);
          PointF location1 = new PointF(location.X + this.Location.X, location.Y + this.Location.Y);
          graphics.DrawPdfTemplate(template, location1, new SizeF(template.Width * scalingX, template.Height * scalingY));
          this.m_painterGraphics.Add(graphics);
        }
        else
        {
          PdfTemplateValuePair templateValuePair = this.m_list[(PdfDocumentBase) document1];
          if (templateValuePair.Value != s)
          {
            SizeF size = this.GetSize();
            templateValuePair.Template.Reset(size);
            templateValuePair.Template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, size), this.StringFormat);
          }
          if (this.m_painterGraphics.Contains(graphics))
            return;
          PointF location2 = new PointF(location.X + this.Location.X, location.Y + this.Location.Y);
          graphics.DrawPdfTemplate(templateValuePair.Template, location2, new SizeF(templateValuePair.Template.Width * scalingX, templateValuePair.Template.Height * scalingY));
          this.m_painterGraphics.Add(graphics);
        }
      }
      else
      {
        PdfDocument document = pageFromGraphics.Document;
        string s = this.GetValue(graphics);
        if (!this.m_list.ContainsKey((PdfDocumentBase) document))
        {
          PdfTemplate template = new PdfTemplate(this.GetSize());
          this.m_list[(PdfDocumentBase) document] = new PdfTemplateValuePair(template, s);
          template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, this.GetSize()), this.StringFormat);
          PointF location3 = new PointF(location.X + this.Location.X, location.Y + this.Location.Y);
          graphics.DrawPdfTemplate(template, location3, new SizeF(template.Width * scalingX, template.Height * scalingY));
          this.m_painterGraphics.Add(graphics);
        }
        else
        {
          PdfTemplateValuePair templateValuePair = this.m_list[(PdfDocumentBase) document];
          if (templateValuePair.Value != s)
          {
            SizeF size = this.GetSize();
            templateValuePair.Template.Reset(size);
            templateValuePair.Template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, size), this.StringFormat);
          }
          if (this.m_painterGraphics.Contains(graphics))
            return;
          PointF location4 = new PointF(location.X + this.Location.X, location.Y + this.Location.Y);
          graphics.DrawPdfTemplate(templateValuePair.Template, location4, new SizeF(templateValuePair.Template.Width * scalingX, templateValuePair.Template.Height * scalingY));
          this.m_painterGraphics.Add(graphics);
        }
      }
    }
    else
    {
      if (!(graphics.Page is PdfLoadedPage))
        return;
      base.PerformDraw(graphics, location, scalingX, scalingY);
      PdfLoadedDocument document = PdfDynamicField.GetLoadedPageFromGraphics(graphics).Document as PdfLoadedDocument;
      string s = this.GetValue(graphics);
      if (this.m_list.ContainsKey((PdfDocumentBase) document))
      {
        PdfTemplateValuePair templateValuePair = this.m_list[(PdfDocumentBase) document];
        if (templateValuePair.Value != s)
        {
          SizeF size = this.GetSize();
          templateValuePair.Template.Reset(size);
          templateValuePair.Template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, size), this.StringFormat);
        }
        if (this.m_painterGraphics.Contains(graphics))
          return;
        PointF location5 = new PointF(location.X + this.Location.X, location.Y + this.Location.Y);
        graphics.DrawPdfTemplate(templateValuePair.Template, location5, new SizeF(templateValuePair.Template.Width * scalingX, templateValuePair.Template.Height * scalingY));
        this.m_painterGraphics.Add(graphics);
      }
      else
      {
        PdfTemplate template = new PdfTemplate(this.GetSize());
        this.m_list[(PdfDocumentBase) document] = new PdfTemplateValuePair(template, s);
        template.Graphics.DrawString(s, this.GetFont(), this.Pen, this.GetBrush(), new RectangleF(PointF.Empty, this.GetSize()), this.StringFormat);
        PointF location6 = new PointF(location.X + this.Location.X, location.Y + this.Location.Y);
        graphics.DrawPdfTemplate(template, location6, new SizeF(template.Width * scalingX, template.Height * scalingY));
        this.m_painterGraphics.Add(graphics);
      }
    }
  }
}
