// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Sel
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;


namespace Syncfusion.Pdf.Compression
{
    internal class Sel
    {
      private int m_cx;
      private int m_cy;
      private List<int[]> m_data;
      private string m_name;
      private int m_sx;
      private int m_sy;

      internal int CX
      {
        get => this.m_cx;
        set => this.m_cx = value;
      }

      internal int CY
      {
        get => this.m_cy;
        set => this.m_cy = value;
      }

      internal List<int[]> Data
      {
        get => this.m_data;
        set => this.m_data = value;
      }

      internal string Name
      {
        get => this.m_name;
        set => this.m_name = value;
      }

      internal int SX
      {
        get => this.m_sx;
        set => this.m_sx = value;
      }

      internal int SY
      {
        get => this.m_sy;
        set => this.m_sy = value;
      }
    }
}
