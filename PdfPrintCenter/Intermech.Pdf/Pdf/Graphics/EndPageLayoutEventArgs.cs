// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.EndPageLayoutEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class EndPageLayoutEventArgs : PdfCancelEventArgs
{
  private PdfPage m_nextPage;
  private PdfLayoutResult m_result;

  public EndPageLayoutEventArgs(PdfLayoutResult result)
  {
    this.m_result = result != null ? result : throw new ArgumentNullException(nameof (result));
  }

  public PdfPage NextPage
  {
    get => this.m_nextPage;
    set => this.m_nextPage = value;
  }

  public PdfLayoutResult Result => this.m_result;
}
