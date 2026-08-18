// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.GraphicFuncs
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Diagnostics;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#nullable disable
namespace Intermech.Interfaces;

public class GraphicFuncs
{
  public static bool AreColorsSimilar(Color c1, Color c2, int tolerance = 10)
  {
    return Math.Abs((int) c1.R - (int) c2.R) < tolerance && Math.Abs((int) c1.G - (int) c2.G) < tolerance && Math.Abs((int) c1.B - (int) c2.B) < tolerance;
  }

  [NotNull]
  public static string BrushToString([CanBeNull] Brush b)
  {
    switch (b)
    {
      case null:
        return string.Empty;
      case SolidBrush solidBrush:
        return $"S.{solidBrush.Color.ToArgb()}";
      case HatchBrush hatchBrush:
        return $"H.{hatchBrush.ForegroundColor.ToArgb()}.{hatchBrush.BackgroundColor.ToArgb()}.{(int) hatchBrush.HatchStyle}";
      default:
        throw new Exception("Unsupported brush type");
    }
  }

  [CanBeNull]
  public static Brush StringToBrush([CanBeNull] string s)
  {
    Brush brush = (Brush) null;
    if (!string.IsNullOrEmpty(s))
    {
      string[] strArray = s.Split('.');
      if (strArray.Length != 0)
      {
        if (strArray[0] == "S" && strArray.Length >= 2)
          brush = (Brush) new SolidBrush(Color.FromArgb(Convert.ToInt32(strArray[1])));
        else if (strArray[0] == "H" && strArray.Length >= 4)
          brush = (Brush) new HatchBrush((HatchStyle) Convert.ToInt32(strArray[3]), Color.FromArgb(Convert.ToInt32(strArray[1])), Color.FromArgb(Convert.ToInt32(strArray[2])));
      }
    }
    return brush;
  }

  [NotNull]
  public static string PenToString([CanBeNull] Pen p)
  {
    return p == null ? string.Empty : $"P.{p.Color.ToArgb()}";
  }

  [CanBeNull]
  public static Pen StringToPen([CanBeNull] string s)
  {
    Pen pen = (Pen) null;
    if (!string.IsNullOrEmpty(s))
    {
      string[] strArray = s.Split('.');
      if (strArray.Length > 1 && strArray[0] == "P")
        pen = new Pen(Color.FromArgb(Convert.ToInt32(strArray[1])));
    }
    return pen;
  }

  public static int AccurateMeasureString(Graphics graphics, [NotNull] string text, Font font, int width)
  {
    if (text == "")
      return 0;
    StringFormat stringFormat = new StringFormat();
    RectangleF layoutRect = new RectangleF(0.0f, 0.0f, (float) width, 1000f);
    CharacterRange[] ranges = new CharacterRange[1]
    {
      new CharacterRange(0, text.Length)
    };
    stringFormat.SetMeasurableCharacterRanges(ranges);
    Region[] regionArray = graphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
    if (regionArray.Length != 0)
      layoutRect = regionArray[0].GetBounds(graphics);
    return (int) ((double) layoutRect.Height + 1.0);
  }
}
