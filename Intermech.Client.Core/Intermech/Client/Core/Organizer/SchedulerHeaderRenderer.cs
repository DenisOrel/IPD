
// Type: Intermech.Client.Core.Organizer.SchedulerHeaderRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
internal class SchedulerHeaderRenderer
{
  private SchedulerColorTable _colorTable = new SchedulerColorTable();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="active"></param>
  /// <param name="inputState"></param>
  private void DrawButtonBackground(
    Graphics g,
    Rectangle bounds,
    bool active,
    InputState inputState)
  {
    ColorBlend colorBlend = new ColorBlend();
    if (!active && inputState == InputState.Normal)
      colorBlend.Colors = new Color[4]
      {
        this._colorTable.ButtonLight,
        this._colorTable.ButtonDark,
        this._colorTable.ButtonHighlightDark,
        this._colorTable.ButtonHighlightLight
      };
    else if (!active && inputState == InputState.Hovered)
      colorBlend.Colors = new Color[4]
      {
        this._colorTable.ButtonHoveredLight,
        this._colorTable.ButtonHoveredDark,
        this._colorTable.ButtonHoveredHighlightDark,
        this._colorTable.ButtonHoveredHighlightLight
      };
    else if (active && inputState == InputState.Normal)
      colorBlend.Colors = new Color[4]
      {
        this._colorTable.ButtonActiveLight,
        this._colorTable.ButtonActiveDark,
        this._colorTable.ButtonActiveHighlightDark,
        this._colorTable.ButtonActiveHighlightLight
      };
    else if (inputState == InputState.Clicked || active && inputState == InputState.Hovered)
      colorBlend.Colors = new Color[4]
      {
        this._colorTable.ButtonClickedLight,
        this._colorTable.ButtonClickedDark,
        this._colorTable.ButtonClickedHighlightDark,
        this._colorTable.ButtonClickedHighlightLight
      };
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
    using (Pen pen = new Pen(this._colorTable.TimeScaleLine))
      g.DrawLines(pen, new Point[3]
      {
        new Point(bounds.Left, bounds.Bottom),
        new Point(bounds.Right - 1, bounds.Bottom),
        new Point(bounds.Right - 1, bounds.Top)
      });
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="font"></param>
  /// <param name="text"></param>
  private void DrawButtonText(Graphics g, Rectangle bounds, Font font, string text)
  {
    TextRenderer.DrawText((IDeviceContext) g, text, font, bounds, this._colorTable.DayHeaderText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  internal virtual void OnDrawBackground(SchedulerHeaderRendererEventArgs e)
  {
    if (e == null || e.Graphics == null || e.Header == null || e.Header.Bounds == Rectangle.Empty)
      return;
    using (Brush brush1 = (Brush) new SolidBrush(this._colorTable.HeaderBackground))
    {
      Graphics graphics = e.Graphics;
      Brush brush2 = brush1;
      Rectangle bounds = e.Header.Bounds;
      int x = bounds.X;
      bounds = e.Header.Bounds;
      int y = bounds.Y;
      bounds = e.Header.Bounds;
      int width = bounds.Width;
      bounds = e.Header.Bounds;
      int height = bounds.Height;
      graphics.FillRectangle(brush2, x, y, width, height);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  internal virtual void OnDrawBorder(SchedulerHeaderRendererEventArgs e)
  {
    if (e == null || e.Graphics == null || e.Header == null || e.Header.Bounds == Rectangle.Empty)
      return;
    using (Pen pen1 = new Pen(this._colorTable.TimeScaleLine))
    {
      Graphics graphics = e.Graphics;
      Pen pen2 = pen1;
      Rectangle bounds = e.Header.Bounds;
      int left = bounds.Left;
      bounds = e.Header.Bounds;
      int bottom1 = bounds.Bottom;
      bounds = e.Header.Bounds;
      int right = bounds.Right;
      bounds = e.Header.Bounds;
      int bottom2 = bounds.Bottom;
      graphics.DrawLine(pen2, left, bottom1, right, bottom2);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  internal virtual void OnDrawButtons(SchedulerHeaderRendererEventArgs e)
  {
    if (e == null || e.Graphics == null || e.Header == null || e.Header.Bounds == Rectangle.Empty)
      return;
    foreach (SchedulerHeaderButton button in e.Header.Buttons)
    {
      this.DrawButtonBackground(e.Graphics, button.Bounds, button.Active, button.State);
      this.DrawButtonText(e.Graphics, button.Bounds, e.Header.Font, button.Text);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  internal virtual void OnDrawRadioButtons(SchedulerHeaderRendererEventArgs e)
  {
    if (e == null || e.Graphics == null || e.Header == null || e.Header.Bounds == Rectangle.Empty)
      return;
    foreach (SchedulerHeaderRadioButton radioButton in e.Header.RadioButtons)
    {
      if (!radioButton.Checked && radioButton.State == InputState.Normal)
        e.Graphics.DrawImage(e.Header.Images.Images["Empty"], radioButton.ImageLocation);
      else if (!radioButton.Checked && radioButton.State == InputState.Hovered)
        e.Graphics.DrawImage(e.Header.Images.Images["EmptyHovered"], radioButton.ImageLocation);
      else if (radioButton.Checked && radioButton.State == InputState.Normal)
        e.Graphics.DrawImage(e.Header.Images.Images["Checked"], radioButton.ImageLocation);
      else if (radioButton.Checked && radioButton.State == InputState.Hovered)
        e.Graphics.DrawImage(e.Header.Images.Images["CheckedHovered"], radioButton.ImageLocation);
      TextRenderer.DrawText((IDeviceContext) e.Graphics, radioButton.Text, e.Header.Font, radioButton.TextBounds, this._colorTable.DayHeaderText, TextFormatFlags.VerticalCenter);
    }
  }
}
