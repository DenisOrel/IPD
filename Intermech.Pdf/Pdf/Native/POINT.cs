// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.POINT
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Native
{
    internal struct POINT
    {
      public int x;
      public int y;

      public POINT(int X, int Y)
      {
        this.x = X;
        this.y = Y;
      }

      public POINT(int lParam)
      {
        this.x = lParam & (int) ushort.MaxValue;
        this.y = lParam >> 16 /*0x10*/;
      }

      public static implicit operator Point(POINT p) => new Point(p.x, p.y);

      public static implicit operator PointF(POINT p) => new PointF((float) p.x, (float) p.y);

      public static implicit operator POINT(Point p) => new POINT(p.X, p.Y);
    }
}
