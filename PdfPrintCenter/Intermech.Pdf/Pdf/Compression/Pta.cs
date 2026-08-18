// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Pta
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class Pta
{
  private int m_n;
  private int m_nalloc;
  private int m_refCount;
  private List<float> m_x;
  private List<float> m_y;

  internal Pta(int n)
  {
    this.Nalloc = n;
    this.N = 0;
    this.X = new List<float>();
    this.Y = new List<float>();
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

  internal int RefCount
  {
    get => this.m_refCount;
    set => this.m_refCount = value;
  }

  internal List<float> X
  {
    get => this.m_x;
    set => this.m_x = value;
  }

  internal List<float> Y
  {
    get => this.m_y;
    set => this.m_y = value;
  }
}
