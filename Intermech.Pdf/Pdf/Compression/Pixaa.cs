// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Pixaa
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;


namespace Syncfusion.Pdf.Compression
{
    internal class Pixaa
    {
      private List<Syncfusion.Pdf.Compression.Boxa> m_boxa;
      private int m_n;
      private int m_nalloc;
      private List<Syncfusion.Pdf.Compression.Pixa> m_pixa;

      internal Pixaa(int n)
      {
        this.Nalloc = n;
        this.N = 0;
        this.Pixa = new List<Syncfusion.Pdf.Compression.Pixa>();
        this.Boxa = new List<Syncfusion.Pdf.Compression.Boxa>();
      }

      internal List<Syncfusion.Pdf.Compression.Boxa> Boxa
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

      internal List<Syncfusion.Pdf.Compression.Pixa> Pixa
      {
        get => this.m_pixa;
        set => this.m_pixa = value;
      }
    }
}
