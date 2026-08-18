// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.POINTS
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Native
{
    internal struct POINTS(short X, short Y)
    {
      public short x = X;
      public short y = Y;

      public static implicit operator Point(POINTS p) => new Point((int) p.x, (int) p.y);

      public static implicit operator PointF(POINTS p) => new PointF((float) p.x, (float) p.y);

      public static implicit operator POINTS(Point p) => new POINTS((short) p.X, (short) p.Y);

      public static implicit operator POINTS(PointF p) => new POINTS((short) p.X, (short) p.Y);
    }
}
