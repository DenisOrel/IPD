
// Type: Intermech.Controls.Thumbnail.BaseRenderer
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Controls.Thumbnail;

/// <summary>Summary description for BaseRenderer.</summary>
public class BaseRenderer : IComponent, IDisposable, IThumbnailRenderer
{
  protected Color _color;
  protected Color _selectedColor;
  protected Color _selectedInactiveColor;
  private ISite _site;
  private Size _minSize;
  private Size _maxSize;
  private StringFormat _textFormat;

  public BaseRenderer(IContainer container) => container.Add((IComponent) this);

  public BaseRenderer()
  {
    this._color = SystemColors.ControlDark;
    this._selectedColor = SystemColors.MenuHighlight;
    this._selectedInactiveColor = SystemColors.ControlDarkDark;
    this._minSize = new Size(124, 124);
    this._maxSize = new Size(800, 800);
    this._textFormat = new StringFormat();
    this._textFormat.Trimming = StringTrimming.EllipsisCharacter;
    this._textFormat.Alignment = StringAlignment.Near;
    this._textFormat.LineAlignment = StringAlignment.Center;
    this._textFormat.FormatFlags = StringFormatFlags.NoWrap;
  }

  public event EventHandler Disposed;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public virtual ISite Site
  {
    get => this._site;
    set => this._site = value;
  }

  public void Dispose()
  {
    if (this._textFormat != null)
    {
      this._textFormat.Dispose();
      this._textFormat = (StringFormat) null;
    }
    if (this.Disposed != null)
      this.Disposed((object) this, EventArgs.Empty);
    GC.SuppressFinalize((object) this);
  }

  protected void OnRedrawRequired(Rectangle bounds)
  {
    if (this.RedrawRequired == null)
      return;
    if (bounds.IsEmpty)
      this.RedrawRequired((object) this, BoundsEventArgs.EmptyBounds);
    else
      this.RedrawRequired((object) this, new BoundsEventArgs(bounds));
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  public Color Color
  {
    get => this._color;
    set
    {
      if (!(this._color != value))
        return;
      this._color = value;
      this.OnRedrawRequired(Rectangle.Empty);
    }
  }

  [Browsable(false)]
  public StringFormat TextFormat => this._textFormat;

  protected bool ShouldSerializeColor() => this._color != SystemColors.ControlDark;

  [RefreshProperties(RefreshProperties.Repaint)]
  public Color SelectedColor
  {
    get => this._selectedColor;
    set
    {
      if (!(this._selectedColor != value))
        return;
      this._selectedColor = value;
      this.OnRedrawRequired(Rectangle.Empty);
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  public Color SelectedInactiveColor
  {
    get => this._selectedInactiveColor;
    set
    {
      if (!(this._selectedInactiveColor != value))
        return;
      this._selectedInactiveColor = value;
      this.OnRedrawRequired(Rectangle.Empty);
    }
  }

  protected bool ShouldSerializeSelectedColor()
  {
    return this._selectedColor != SystemColors.MenuHighlight;
  }

  public static Rectangle SmartStretchBounds(Rectangle bounds, int width, int height)
  {
    Rectangle rectangle = bounds;
    if (width > 0)
    {
      int num1 = bounds.Bottom - bounds.Top;
      int num2 = bounds.Right - bounds.Left;
      double num3 = (double) num1 / (double) height;
      double num4 = (double) num2 / (double) width;
      double num5 = num3 >= num4 ? num4 : num3;
      int height1 = (int) (num5 * (double) height);
      int width1 = (int) (num5 * (double) width);
      int y = rectangle.Top + (num1 - height1) / 2;
      rectangle = new Rectangle(rectangle.Left + (num2 - width1) / 2, y, width1, height1);
    }
    return rectangle;
  }

  public static Rectangle SmartStretchBoundsAdv(Rectangle bounds, int width, int height)
  {
    Rectangle rectangle = bounds;
    if (width > 0)
    {
      int num1 = bounds.Bottom - bounds.Top;
      int num2 = bounds.Right - bounds.Left;
      double num3 = (double) num1 / (double) height;
      double num4 = (double) num2 / (double) width;
      double num5 = num3 >= num4 ? num4 : num3;
      int height1 = (int) (num5 * (double) height);
      int width1 = (int) (num5 * (double) width);
      int top = rectangle.Top;
      rectangle = new Rectangle(rectangle.Left + (num2 - width1) / 2, top, width1, height1);
    }
    return rectangle;
  }

  public event RedrawEventHandler RedrawRequired;

  public virtual void DrawPanel(
    int panelIndex,
    Graphics g,
    Rectangle bounds,
    bool selected,
    bool active)
  {
    using (SolidBrush solidBrush = new SolidBrush(selected ? this._selectedColor : this._color))
    {
      g.FillRectangle((Brush) solidBrush, bounds);
      bounds.Inflate(-2, -2);
      bounds.Height -= 18;
      g.FillRectangle(SystemBrushes.Control, bounds);
    }
  }

  public Size MinimumSize => this._minSize;

  public Size MaximumSize => this._maxSize;
}
