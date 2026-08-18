// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDestinationPageNumberField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfDestinationPageNumberField : PdfPageNumberField
{
  private PdfLoadedPage m_loadedPage;
  private PdfPage m_page;

  public PdfDestinationPageNumberField()
  {
  }

  public PdfDestinationPageNumberField(PdfFont font)
    : base(font)
  {
  }

  public PdfDestinationPageNumberField(PdfFont font, PdfBrush brush)
    : base(font, brush)
  {
  }

  public PdfDestinationPageNumberField(PdfFont font, RectangleF bounds)
    : base(font, bounds)
  {
  }

  protected internal override string GetValue(PdfGraphics graphics)
  {
    return this.m_loadedPage != null ? this.InternalLoadedGetValue(this.m_loadedPage) : this.InternalGetValue(this.m_page);
  }

  public PdfLoadedPage LoadedPage
  {
    get => this.m_loadedPage;
    set => this.m_loadedPage = value != null ? value : throw new ArgumentNullException("Page");
  }

  public PdfPage Page
  {
    get => this.m_page;
    set => this.m_page = value != null ? value : throw new ArgumentNullException(nameof (Page));
  }
}
