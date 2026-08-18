// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.HtmlToPdf.HtmlHyperLink
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.HtmlToPdf;

internal class HtmlHyperLink
{
  private RectangleF m_bounds;
  private string m_hash;
  private string m_href;
  private string m_name;

  public HtmlHyperLink(RectangleF Bounds, string Href)
  {
    this.m_bounds = Bounds;
    this.m_href = Href;
    this.ConvertBoundsToPoint();
  }

  internal void ConvertBoundsToPoint()
  {
    this.m_bounds = new PdfUnitConvertor().ConvertFromPixels(this.m_bounds, PdfGraphicsUnit.Point);
  }

  public RectangleF Bounds
  {
    get => this.m_bounds;
    set => this.m_bounds = value;
  }

  internal string Hash
  {
    get => this.m_hash;
    set => this.m_hash = value;
  }

  public string Href => this.m_href;

  internal string Name
  {
    get => this.m_name;
    set => this.m_name = value;
  }
}
