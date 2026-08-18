// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Pixa
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class Pixa
{
  private Boxa m_boxa;
  private int m_n;
  private int m_nalloc;
  private List<Syncfusion.Pdf.Compression.Pix> m_pix;
  private int m_refCount;

  internal Pixa(int n)
  {
    this.Nalloc = n;
    this.N = 0;
    this.RefCount = 1;
    this.Pix = new List<Syncfusion.Pdf.Compression.Pix>();
  }

  internal Boxa Boxa
  {
    get => this.m_boxa;
    set => this.m_boxa = value;
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

  internal List<Syncfusion.Pdf.Compression.Pix> Pix
  {
    get => this.m_pix;
    set => this.m_pix = value;
  }

  internal int RefCount
  {
    get => this.m_refCount;
    set => this.m_refCount = value;
  }
}
