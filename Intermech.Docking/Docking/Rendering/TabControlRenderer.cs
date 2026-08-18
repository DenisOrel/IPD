
// Type: Intermech.Docking.Rendering.TabControlRenderer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Docking.Rendering;

[TypeConverter(typeof (TabControlRendererConverter))]
public class TabControlRenderer : ITabControlRenderer
{
  protected Size _tabPadding = new Size(2, 2);
  protected StringFormat _sff;

  public TabControlRenderer()
  {
    this._sff = new StringFormat(StringFormat.GenericDefault);
    this._sff.Alignment = StringAlignment.Near;
    this._sff.LineAlignment = StringAlignment.Center;
    this._sff.Trimming = StringTrimming.EllipsisCharacter;
    this._sff.FormatFlags |= StringFormatFlags.NoWrap;
  }

  public virtual void DrawFakeTabControlBackgroundExtension(
    Graphics graphics,
    Rectangle bounds,
    Color backColor)
  {
  }

  public virtual void DrawTabControlBackground(
    Graphics graphics,
    Rectangle bounds,
    Color backColor,
    bool client)
  {
    using (SolidBrush solidBrush = new SolidBrush(backColor))
      graphics.FillRectangle((Brush) solidBrush, bounds);
  }

  public virtual void DrawTabControlButton(
    Graphics graphics,
    Rectangle bounds,
    ButtonType buttonType,
    DrawItemState state)
  {
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
      bounds.Offset(1, 1);
    if (buttonType != ButtonType.ScrollLeft)
    {
      if (buttonType != ButtonType.ScrollRight)
        return;
      TitleButtonRenderer.DrawRightScroll(graphics, bounds, SystemColors.ControlText, (state & DrawItemState.Disabled) != DrawItemState.Disabled);
    }
    else
      TitleButtonRenderer.DrawLeftScroll(graphics, bounds, SystemColors.ControlText, (state & DrawItemState.Disabled) != DrawItemState.Disabled);
  }

  protected virtual void DrawTab(
    Graphics g,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    Brush textBrush,
    DrawItemState state,
    bool top,
    bool flat)
  {
    int num = 2;
    if (top)
      num = 0;
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
    {
      if (!flat)
      {
        Rectangle rect = bounds;
        rect.Inflate(-1, 0);
        if (foreColor != backColor)
        {
          Color color1;
          Color color2;
          if (top)
          {
            color1 = foreColor;
            color2 = backColor;
          }
          else
          {
            color2 = foreColor;
            color1 = backColor;
          }
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(rect.X, rect.Y - 1), new Point(rect.X, rect.Bottom), color1, color2))
            g.FillRectangle((Brush) linearGradientBrush, rect);
        }
        else
        {
          using (SolidBrush solidBrush = new SolidBrush(backColor))
            g.FillRectangle((Brush) solidBrush, rect);
        }
      }
      else
      {
        using (Pen pen = new Pen(backColor))
        {
          if (top)
            g.DrawLine(pen, bounds.Left + 1, bounds.Bottom - 2, bounds.Right - 2, bounds.Bottom - 2);
          else
            g.DrawLine(pen, bounds.Left + 1, bounds.Top, bounds.Right - 2, bounds.Top);
        }
      }
      Point[] points = new Point[6];
      if (!top)
      {
        points[0] = new Point(bounds.Left, bounds.Top);
        points[1] = new Point(bounds.Left, bounds.Bottom - 2);
        points[2] = new Point(bounds.Left + 2, bounds.Bottom);
        points[3] = new Point(bounds.Right - 3, bounds.Bottom);
        points[4] = new Point(bounds.Right - 1, bounds.Bottom - 2);
        points[5] = new Point(bounds.Right - 1, bounds.Top);
      }
      else
      {
        points[0] = new Point(bounds.Left, bounds.Bottom - 3);
        points[1] = new Point(bounds.Left, bounds.Top + 1);
        points[2] = new Point(bounds.Left + 2, bounds.Top - 1);
        points[3] = new Point(bounds.Right - 3, bounds.Top - 1);
        points[4] = new Point(bounds.Right - 1, bounds.Top + 1);
        points[5] = new Point(bounds.Right - 1, bounds.Bottom - 3);
      }
      g.DrawLines(SystemPens.ControlDark, points);
    }
    if (bounds.Width >= 22 && image != null)
    {
      g.DrawImage(image, new Rectangle(bounds.X + 4, bounds.Y + 2 + num, image.Width, image.Height));
      bounds.X += 18;
      bounds.Width -= 18;
    }
    bounds.Inflate(-2, 0);
    bounds.Y += num;
    bounds.X += 4;
    if (bounds.Width <= 8)
      return;
    g.DrawString(text, font, textBrush, (RectangleF) bounds, EverettRenderer.StandardStringFormat);
  }

  public virtual void DrawTabControlTab(
    Graphics graphics,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    DrawItemState state,
    bool drawSeparator,
    Intermech.Docking.TabAlignment alignment,
    bool flat)
  {
    bool top = alignment == Intermech.Docking.TabAlignment.Top;
    if (!top)
    {
      ++bounds.Y;
      --bounds.Height;
    }
    if ((state & DrawItemState.Selected) == DrawItemState.Selected)
    {
      this.DrawTab(graphics, bounds, image, text, font, backColor, SystemColors.ControlLightLight, SystemBrushes.ControlText, state, top, flat);
    }
    else
    {
      this.DrawTab(graphics, bounds, image, text, font, backColor, SystemColors.ControlLightLight, SystemBrushes.ControlText, state, top, flat);
      if (!drawSeparator)
        return;
      if (top)
        graphics.DrawLine(SystemPens.ControlDark, bounds.Right - 1, bounds.Top + 2, bounds.Right - 1, bounds.Bottom - 6);
      else
        graphics.DrawLine(SystemPens.ControlDark, bounds.Right - 1, bounds.Top + 3, bounds.Right - 1, bounds.Bottom - 2);
    }
  }

  public virtual void DrawTabControlTabStripBackground(
    Graphics graphics,
    Rectangle bounds,
    Color backColor,
    Intermech.Docking.TabAlignment tabAlignment,
    bool flat)
  {
    if (bounds.IsEmpty || bounds.Width == 0 || bounds.Height == 0)
      return;
    if (!flat)
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, backColor, backColor, LinearGradientMode.Horizontal))
        graphics.FillRectangle((Brush) linearGradientBrush, bounds);
    }
    using (new Pen(SystemColors.ControlLightLight))
    {
      if (tabAlignment == Intermech.Docking.TabAlignment.Bottom)
      {
        graphics.DrawLine(SystemPens.ControlDark, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
      }
      else
      {
        int bottom = bounds.Bottom;
        graphics.DrawLine(SystemPens.ControlDark, bounds.Left, bottom - 2, bounds.Right - 1, bottom - 2);
        int num = flat ? 1 : 0;
      }
    }
  }

  public virtual void FinishRenderSession()
  {
  }

  public virtual Size MeasureTabControlTab(
    Graphics g,
    Image image,
    string text,
    Font font,
    DrawItemState state)
  {
    int num = (int) g.MeasureString(text, font, (SizeF) new Size(int.MaxValue, int.MaxValue), this._sff).Width + 6;
    if (image != null)
      num += image.Width + 2;
    return new Size(num + 4, 0);
  }

  public virtual void StartRenderSession()
  {
  }

  public override string ToString() => "Tab";

  public virtual bool ShouldDrawControlBorder => false;

  public virtual bool ShouldDrawTabControlBackground
  {
    get => true;
    set
    {
    }
  }

  public virtual Size TabControlPadding
  {
    get => this._tabPadding;
    set => this._tabPadding = value;
  }

  public virtual int TabControlTabExtra => 0;

  public virtual int TabControlTabHeight => Control.DefaultFont.Height + 10;

  public virtual int TabControlTabStripHeight => this.TabControlTabHeight + 3;
}
