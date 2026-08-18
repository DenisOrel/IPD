// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Context
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Compression
{
    internal class Context
    {
      private char m_lps;
      private char m_mps;
      private short m_qe;

      internal Context(short qe, char mps, char lps)
      {
        this.Qe = qe;
        this.Mps = mps;
        this.Lps = lps;
      }

      internal char Lps
      {
        get => this.m_lps;
        set => this.m_lps = value;
      }

      internal char Mps
      {
        get => this.m_mps;
        set => this.m_mps = value;
      }

      internal short Qe
      {
        get => this.m_qe;
        set => this.m_qe = value;
      }
    }
}
