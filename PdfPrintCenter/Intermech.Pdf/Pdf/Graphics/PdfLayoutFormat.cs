// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfLayoutFormat
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class PdfLayoutFormat
{
  private bool m_boundsSet;
  private PdfLayoutBreakType m_break;
  private PdfLayoutType m_layout;
  private RectangleF m_paginateBounds;

  public PdfLayoutFormat()
  {
  }

  public PdfLayoutFormat(PdfLayoutFormat baseFormat)
    : this()
  {
    this.Break = baseFormat != null ? baseFormat.Break : throw new ArgumentNullException(nameof (baseFormat));
    this.Layout = baseFormat.Layout;
    this.PaginateBounds = baseFormat.PaginateBounds;
    this.m_boundsSet = baseFormat.UsePaginateBounds;
  }

  public PdfLayoutBreakType Break
  {
    get => this.m_break;
    set => this.m_break = value;
  }

  public PdfLayoutType Layout
  {
    get => this.m_layout;
    set => this.m_layout = value;
  }

  public RectangleF PaginateBounds
  {
    get => this.m_paginateBounds;
    set
    {
      this.m_paginateBounds = value;
      this.m_boundsSet = true;
    }
  }

  internal bool UsePaginateBounds => this.m_boundsSet;
}
