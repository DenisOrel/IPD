// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Pixacc
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Compression
{
    internal class Pixacc
    {
      private int m_h;
      private uint m_offset;
      private Pix m_pix;
      private int m_w;

      internal int H
      {
        get => this.m_h;
        set => this.m_h = value;
      }

      internal uint Offset
      {
        get => this.m_offset;
        set => this.m_offset = value;
      }

      internal Pix Pix
      {
        get => this.m_pix;
        set => this.m_pix = value;
      }

      internal int W
      {
        get => this.m_w;
        set => this.m_w = value;
      }
    }
}
