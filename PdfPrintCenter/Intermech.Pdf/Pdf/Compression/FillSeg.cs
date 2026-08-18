// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.FillSeg
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class FillSeg
{
  private int m_dy;
  private int m_xLeft;
  private int m_xRight;
  private int m_y;

  internal int Dy
  {
    get => this.m_dy;
    set => this.m_dy = value;
  }

  internal int XLeft
  {
    get => this.m_xLeft;
    set => this.m_xLeft = value;
  }

  internal int XRight
  {
    get => this.m_xRight;
    set => this.m_xRight = value;
  }

  internal int Y
  {
    get => this.m_y;
    set => this.m_y = value;
  }
}
