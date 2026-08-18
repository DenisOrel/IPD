// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.EndItemLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Lists;

public class EndItemLayoutEventArgs
{
  private PdfListItem m_item;
  private PdfPage m_page;

  internal EndItemLayoutEventArgs(PdfListItem item, PdfPage page)
  {
    this.m_item = item;
    this.m_page = page;
  }

  public PdfListItem Item => this.m_item;

  public PdfPage Page => this.m_page;
}
