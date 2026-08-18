// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Boxa
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;


namespace Syncfusion.Pdf.Compression
{
    internal class Boxa
    {
      private List<Syncfusion.Pdf.Compression.Box> m_box;
      private int m_n;
      private int m_nalloc;
      private uint m_refCount;

      internal Boxa(int n)
      {
        this.Nalloc = n;
        this.N = 0;
        this.RefCount = 1U;
        this.Box = new List<Syncfusion.Pdf.Compression.Box>();
      }

      internal List<Syncfusion.Pdf.Compression.Box> Box
      {
        get => this.m_box;
        set => this.m_box = value;
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

      internal uint RefCount
      {
        get => this.m_refCount;
        set => this.m_refCount = value;
      }
    }
}
