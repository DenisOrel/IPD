// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.HtmlToPdf.HtmlToPdfLayoutParams
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.HtmlToPdf;

public class HtmlToPdfLayoutParams : PdfLayoutParams
{
  private RectangleF m_bounds;
  private PdfLayoutFormat m_format;
  private PdfPage m_page;
  private float[] m_verticalOffsets;

  public new RectangleF Bounds
  {
    get => this.m_bounds;
    set => this.m_bounds = value;
  }

  public new PdfLayoutFormat Format
  {
    get => this.m_format;
    set => this.m_format = value;
  }

  public new PdfPage Page
  {
    get => this.m_page;
    set => this.m_page = value;
  }

  public float[] VerticalOffsets
  {
    get => this.m_verticalOffsets;
    set => this.m_verticalOffsets = value;
  }
}
