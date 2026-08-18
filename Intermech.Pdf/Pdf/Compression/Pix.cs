// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Pix
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Compression
{
    internal class Pix
    {
      private PixColormap m_colormap;
      private int m_d;
      private uint[] m_data;
      private int m_h;
      private int m_informat;
      private char m_text;
      private int m_w;
      private int m_wpl;
      private int m_xRes;
      private int m_yRes;

      internal PixColormap Colormap
      {
        get => this.m_colormap;
        set => this.m_colormap = value;
      }

      internal int D
      {
        get => this.m_d;
        set => this.m_d = value;
      }

      internal uint[] Data
      {
        get => this.m_data;
        set => this.m_data = value;
      }

      internal int H
      {
        get => this.m_h;
        set => this.m_h = value;
      }

      internal int Informat
      {
        get => this.m_informat;
        set => this.m_informat = value;
      }

      internal char Text
      {
        get => this.m_text;
        set => this.m_text = value;
      }

      internal int W
      {
        get => this.m_w;
        set => this.m_w = value;
      }

      internal int Wpl
      {
        get => this.m_wpl;
        set => this.m_wpl = value;
      }

      internal int XRes
      {
        get => this.m_xRes;
        set => this.m_xRes = value;
      }

      internal int YRes
      {
        get => this.m_yRes;
        set => this.m_yRes = value;
      }
    }
}
