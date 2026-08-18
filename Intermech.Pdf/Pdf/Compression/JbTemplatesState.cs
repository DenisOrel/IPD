// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.JbTemplatesState
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Compression
{
    internal class JbTemplatesState
    {
      private JBIG2Classifier m_classer;
      private int m_h;
      private int m_i;
      private int m_n;
      private Numa m_numa;
      private int m_w;

      internal JBIG2Classifier Classer
      {
        get => this.m_classer;
        set => this.m_classer = value;
      }

      internal int H
      {
        get => this.m_h;
        set => this.m_h = value;
      }

      internal int I
      {
        get => this.m_i;
        set => this.m_i = value;
      }

      internal int N
      {
        get => this.m_n;
        set => this.m_n = value;
      }

      internal Numa Numa
      {
        get => this.m_numa;
        set => this.m_numa = value;
      }

      internal int W
      {
        get => this.m_w;
        set => this.m_w = value;
      }
    }
}
