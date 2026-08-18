// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.Box
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Compression
{
    internal class Box
    {
      private int m_h;
      private uint m_refCount;
      private int m_w;
      private int m_x;
      private int m_y;

      internal Box()
      {
      }

      internal Box(int x, int y, int w, int h)
      {
        this.X = x;
        this.Y = y;
        this.W = w;
        this.H = h;
      }

      internal int H
      {
        get => this.m_h;
        set => this.m_h = value;
      }

      internal uint RefCount
      {
        get => this.m_refCount;
        set => this.m_refCount = value;
      }

      internal int W
      {
        get => this.m_w;
        set => this.m_w = value;
      }

      internal int X
      {
        get => this.m_x;
        set => this.m_x = value;
      }

      internal int Y
      {
        get => this.m_y;
        set => this.m_y = value;
      }
    }
}
