// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.PixColormap
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class PixColormap
{
  private RGBA_Quad[] m_array;
  private int m_depth;
  private int m_n;
  private int m_nalloc;

  internal RGBA_Quad[] Array
  {
    get => this.m_array;
    set => this.m_array = value;
  }

  internal int Depth
  {
    get => this.m_depth;
    set => this.m_depth = value;
  }

  internal int N
  {
    get => this.m_n;
    set => this.m_n = value;
  }

  internal int Nalloc
  {
    get => this.m_nalloc;
    set => this.m_nalloc = value;
  }
}
