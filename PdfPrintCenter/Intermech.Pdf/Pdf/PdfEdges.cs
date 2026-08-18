// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfEdges
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

public class PdfEdges
{
  private int m_bottom;
  private int m_left;
  private int m_right;
  private int m_top;

  public PdfEdges()
  {
  }

  public PdfEdges(int left, int right, int top, int bottom)
  {
    this.m_left = left;
    this.m_right = right;
    this.m_top = top;
    this.m_bottom = bottom;
  }

  public int All
  {
    set => this.m_left = this.m_right = this.m_top = this.m_bottom = value;
  }

  public int Bottom
  {
    get => this.m_bottom;
    set => this.m_bottom = value;
  }

  internal bool IsAll
  {
    get => this.m_left == this.m_right && this.m_left == this.m_top && this.m_left == this.m_bottom;
  }

  public int Left
  {
    get => this.m_left;
    set => this.m_left = value;
  }

  public int Right
  {
    get => this.m_right;
    set => this.m_right = value;
  }

  public int Top
  {
    get => this.m_top;
    set => this.m_top = value;
  }
}
