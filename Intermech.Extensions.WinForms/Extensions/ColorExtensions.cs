// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ColorExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ColorExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsLight(this Color color)
  {
    return 1.0 - (0.299 * (double) color.R + 0.587 * (double) color.G + 0.114 * (double) color.B) / (double) byte.MaxValue < 0.47;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Color InvertAsBlackWhite(this Color color)
  {
    return !color.IsLight() ? Color.White : Color.Black;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Brush GetInvertedBlackWhiteBrush(this Color color)
  {
    return !color.InvertAsBlackWhite().Equals((object) Color.Black) ? (Brush) Brushes.White.Clone() : (Brush) Brushes.Black.Clone();
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Pen GetInvertedBlackWhitePen(this Color color)
  {
    return !color.InvertAsBlackWhite().Equals((object) Color.Black) ? (Pen) Pens.White.Clone() : (Pen) Pens.Black.Clone();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Color AsBlackWhite(this Color color)
  {
    return !color.IsLight() ? Color.Black : Color.White;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Color LighterBy(this Color color, int percents)
  {
    if (percents < -100 || percents > 100)
      throw new Exception("percents must be between -100 and 100");
    switch (percents)
    {
      case -100:
        return Color.Black;
      case 0:
        return color;
      case 100:
        return Color.White;
      default:
        if (percents > 0)
        {
          float num = (float) percents / 100f;
          return Color.FromArgb((int) color.A, (int) color.R + (int) ((double) ((int) byte.MaxValue - (int) color.R) * (double) num), (int) color.G + (int) ((double) ((int) byte.MaxValue - (int) color.G) * (double) num), (int) color.B + (int) ((double) ((int) byte.MaxValue - (int) color.B) * (double) num));
        }
        float num1 = (float) -percents / 100f;
        return Color.FromArgb((int) color.A, (int) color.R - (int) ((double) color.R * (double) num1), (int) color.G - (int) ((double) color.G * (double) num1), (int) color.B - (int) ((double) color.B * (double) num1));
    }
  }
}
