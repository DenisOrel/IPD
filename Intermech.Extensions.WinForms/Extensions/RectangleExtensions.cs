// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.RectangleExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using System;
using System.Drawing;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class RectangleExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Point TopLeft(this Rectangle rect) => new Point(rect.Left, rect.Top);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Point TopRight(this Rectangle rect) => new Point(rect.Right, rect.Top);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Point BottomLeft(this Rectangle rect) => new Point(rect.Left, rect.Bottom);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Point BottomRight(this Rectangle rect) => new Point(rect.Right, rect.Bottom);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Rectangle GetGrowBy(this Rectangle rect, int deltaX, int deltaY)
  {
    if (deltaX != 0)
      rect.Width += deltaX;
    if (deltaY != 0)
      rect.Height += deltaY;
    return rect;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Rectangle MoveTopLeft(this Rectangle rect, int deltaX, int deltaY)
  {
    return new Rectangle(rect.Left + deltaX, rect.Top + deltaY, Math.Max(0, rect.Width - deltaX), Math.Max(0, rect.Height - deltaY));
  }
}
