// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Sarray
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;


namespace Syncfusion.Pdf.Compression
{
    internal class Sarray
    {
      private List<string> m_array;
      private int m_n;
      private int m_nalloc;
      private int refcount;

      internal Sarray(int n)
      {
        this.Nalloc = n;
        this.N = 0;
        this.refcount = 1;
      }

      internal List<string> Array
      {
        get => this.m_array;
        set => this.m_array = value;
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
}
