// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCStepPaintData
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCStepPaintData
{
  private Color _color1;
  private Color _color2;
  private Color _captionColor;
  private LinearGradientMode _gradientMode;
  private int _radius;
  internal static LCStepPaintData BrownData;
  internal static LCStepPaintData GrayData;
  internal static LCStepPaintData OrangeData;
  internal static LCStepPaintData GreenData;
  internal static LCStepPaintData BlueData;
  internal static LCStepPaintData PinkData;

  public LCStepPaintData(Color c1, Color c2, LinearGradientMode mode)
  {
    this._color1 = c1;
    this._color2 = c2;
    this._gradientMode = mode;
    this._radius = 3;
    this._captionColor = SystemColors.ControlText;
  }

  public LinearGradientBrush CreateBrush(Rectangle bounds)
  {
    return new LinearGradientBrush(bounds, this._color1, this._color2, this._gradientMode);
  }

  public LinearGradientBrush CreateCommentBrush(Rectangle bounds)
  {
    return new LinearGradientBrush(bounds, Color.White, Color.LightGray, LinearGradientMode.ForwardDiagonal);
  }

  public int Radius => this._radius * 2;

  public int RadiusSize => Math.Abs(this._radius * 2);

  public Color CaptionColor => this._captionColor;

  public static LCStepPaintData Brown
  {
    get
    {
      if (LCStepPaintData.BrownData == null)
        LCStepPaintData.BrownData = new LCStepPaintData(Color.Goldenrod, Color.Cornsilk, LinearGradientMode.Horizontal);
      return LCStepPaintData.BrownData;
    }
  }

  public static LCStepPaintData Gray
  {
    get
    {
      if (LCStepPaintData.GrayData == null)
        LCStepPaintData.GrayData = new LCStepPaintData(Color.Gainsboro, Color.DarkGray, LinearGradientMode.Horizontal);
      return LCStepPaintData.GrayData;
    }
  }

  public static LCStepPaintData Orange
  {
    get
    {
      if (LCStepPaintData.OrangeData == null)
        LCStepPaintData.OrangeData = new LCStepPaintData(Color.Snow, Color.DarkOrange, LinearGradientMode.Horizontal);
      return LCStepPaintData.OrangeData;
    }
  }

  public static LCStepPaintData Green
  {
    get
    {
      if (LCStepPaintData.GreenData == null)
        LCStepPaintData.GreenData = new LCStepPaintData(Color.PaleGreen, Color.ForestGreen, LinearGradientMode.Horizontal);
      return LCStepPaintData.GreenData;
    }
  }

  public static LCStepPaintData Blue
  {
    get
    {
      if (LCStepPaintData.BlueData == null)
        LCStepPaintData.BlueData = new LCStepPaintData(Color.LightCyan, Color.LightSkyBlue, LinearGradientMode.Horizontal);
      return LCStepPaintData.BlueData;
    }
  }

  public static LCStepPaintData Pink
  {
    get
    {
      if (LCStepPaintData.PinkData == null)
        LCStepPaintData.PinkData = new LCStepPaintData(Color.LavenderBlush, Color.HotPink, LinearGradientMode.Horizontal);
      return LCStepPaintData.PinkData;
    }
  }
}
