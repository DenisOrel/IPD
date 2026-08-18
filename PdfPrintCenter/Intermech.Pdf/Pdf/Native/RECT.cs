// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.RECT
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Native;

internal struct RECT(int x1, int y1, int x2, int y2)
{
  public int left = x1;
  public int top = y1;
  public int right = x2;
  public int bottom = y2;

  public int Width => this.right - this.left;

  public int Height => this.bottom - this.top;

  public Point TopLeft => new Point(this.left, this.top);

  public Size Size => new Size(this.Width, this.Height);

  public override string ToString() => $"{this.TopLeft}x{this.Size}";

  public static implicit operator Rectangle(RECT rect)
  {
    return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
  }

  public static implicit operator RectangleF(RECT rect)
  {
    return RectangleF.FromLTRB((float) rect.left, (float) rect.top, (float) rect.right, (float) rect.bottom);
  }

  public static implicit operator Size(RECT rect)
  {
    return new Size(rect.right - rect.left, rect.bottom - rect.top);
  }

  public static explicit operator RECT(Rectangle rect)
  {
    return new RECT()
    {
      left = rect.Left,
      right = rect.Right,
      top = rect.Top,
      bottom = rect.Bottom
    };
  }
}
