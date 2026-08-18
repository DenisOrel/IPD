// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.BeginPageLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class BeginPageLayoutEventArgs : PdfCancelEventArgs
{
  private RectangleF m_bounds;
  private PdfPage m_page;

  public BeginPageLayoutEventArgs(RectangleF bounds, PdfPage page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    this.m_bounds = bounds;
    this.m_page = page;
  }

  public RectangleF Bounds
  {
    get => this.m_bounds;
    set => this.m_bounds = value;
  }

  public PdfPage Page => this.m_page;
}
