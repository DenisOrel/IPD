
// Type: Intermech.Client.Core.Organizer.CollapseButtonRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Client.Core.Organizer;

/// <summary>Отрисовщик кнопки минимизации.</summary>
public class CollapseButtonRenderer
{
  private ColorTable _colorTbl = new ColorTable();

  /// <summary>Отрисовка контрола.</summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="inputState"></param>
  /// <param name="collapsed"></param>
  public void Draw(Graphics g, Rectangle bounds, InputState inputState, bool collapsed)
  {
    ColorBlend colorBlend = new ColorBlend();
    colorBlend.Colors = new Color[2]
    {
      this._colorTbl.HeaderBgLight,
      this._colorTbl.HeaderBgDark
    };
    colorBlend.Positions = new float[2]{ 0.0f, 1f };
    bounds.Height = bounds.Height == 0 ? 1 : bounds.Height;
    bounds.Width = bounds.Width == 0 ? 1 : bounds.Width;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.Left, bounds.Top), new Point(bounds.Left, bounds.Bottom), Color.White, Color.Black))
    {
      linearGradientBrush.InterpolationColors = colorBlend;
      g.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    if (inputState == InputState.Clicked || inputState == InputState.Hovered)
    {
      Rectangle rect = bounds;
      rect.Location = new Point(rect.Left + 4, rect.Top + 3);
      rect.Size = new Size(rect.Width - 8, rect.Height - 6);
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(rect.Left, rect.Top), new Point(rect.Left, rect.Bottom), Color.White, Color.Black))
      {
        switch (inputState)
        {
          case InputState.Clicked:
            colorBlend.Colors = new Color[2]
            {
              this._colorTbl.CollapseButtonDownDark,
              this._colorTbl.CollapseButtonDownLight
            };
            break;
          case InputState.Hovered:
            colorBlend.Colors = new Color[2]
            {
              this._colorTbl.CollapseButtonHoveredLight,
              this._colorTbl.CollapseButtonHoveredDark
            };
            break;
        }
        linearGradientBrush.InterpolationColors = colorBlend;
        g.FillRectangle((Brush) linearGradientBrush, rect);
      }
    }
    using (Pen pen = new Pen(this._colorTbl.DarkBorder))
    {
      pen.Color = this._colorTbl.ShapesFront;
      pen.Width = 1.5f;
      float x = bounds.Width != 0 ? (float) (bounds.Width / 2 - 1) : 0.0f;
      float y = bounds.Height != 0 ? (float) (bounds.Height / 2 - 3) : 0.0f;
      if (collapsed)
      {
        PointF[] points1 = new PointF[3]
        {
          new PointF(x - 3f, y),
          new PointF(x, y + 3f),
          new PointF(x - 3f, (float) ((double) y + 3.0 + 3.0))
        };
        g.DrawLines(pen, points1);
        PointF[] points2 = new PointF[3]
        {
          new PointF(x + 1f, y),
          new PointF(x + 4f, y + 3f),
          new PointF(x + 1f, (float) ((double) y + 3.0 + 3.0))
        };
        g.DrawLines(pen, points2);
      }
      else
      {
        PointF[] points3 = new PointF[3]
        {
          new PointF(x, y),
          new PointF(x - 3f, y + 3f),
          new PointF(x, (float) ((double) y + 3.0 + 3.0))
        };
        g.DrawLines(pen, points3);
        PointF[] points4 = new PointF[3]
        {
          new PointF(x + 4f, y),
          new PointF(x + 1f, y + 3f),
          new PointF(x + 4f, (float) ((double) y + 3.0 + 3.0))
        };
        g.DrawLines(pen, points4);
      }
    }
  }

  /// <summary>Отрисовка фона.</summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="inputState"></param>
  public void DrawBackground(Graphics g, Rectangle bounds, InputState inputState)
  {
    ColorBlend colorBlend = new ColorBlend();
    switch (inputState)
    {
      case InputState.Normal:
        colorBlend.Colors = new Color[4]
        {
          this._colorTbl.ButtonLight,
          this._colorTbl.ButtonDark,
          this._colorTbl.ButtonHighlightDark,
          this._colorTbl.ButtonHighlightLight
        };
        break;
      case InputState.Hovered:
        colorBlend.Colors = new Color[4]
        {
          this._colorTbl.ButtonHoveredLight,
          this._colorTbl.ButtonHoveredDark,
          this._colorTbl.ButtonHoveredHighlightDark,
          this._colorTbl.ButtonHoveredHighlightLight
        };
        break;
      default:
        colorBlend.Colors = new Color[4]
        {
          this._colorTbl.ButtonClickedLight,
          this._colorTbl.ButtonClickedDark,
          this._colorTbl.ButtonClickedHighlightDark,
          this._colorTbl.ButtonClickedHighlightLight
        };
        break;
    }
    colorBlend.Positions = new float[4]
    {
      0.0f,
      0.62f,
      0.62f,
      1f
    };
    bounds.Height = bounds.Height == 0 ? 1 : bounds.Height;
    bounds.Width = bounds.Width == 0 ? 1 : bounds.Width;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(bounds.Left, bounds.Bottom), new Point(bounds.Left, bounds.Top), Color.White, Color.Black))
    {
      linearGradientBrush.InterpolationColors = colorBlend;
      g.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    using (Pen pen = new Pen(this._colorTbl.DarkBorder))
      g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right, bounds.Top);
  }
}
