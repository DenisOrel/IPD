
// Type: Intermech.Client.Core.Organizer.CalendarRendererBoxEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Contains information about something's bounds and text to draw on the calendar
/// </summary>
public class CalendarRendererBoxEventArgs : CalendarRendererEventArgs
{
  private Color _backgroundColor;
  private Rectangle _bounds;
  private Font _font;
  private TextFormatFlags _format;
  private string _text;
  private Color _textColor;
  private Size _textSize;

  /// <summary>Initializes some fields</summary>
  private CalendarRendererBoxEventArgs()
  {
  }

  public CalendarRendererBoxEventArgs(CalendarRendererEventArgs original)
    : base(original)
  {
    this.Font = original.Calendar.Font;
    this.Format |= TextFormatFlags.PreserveGraphicsClipping;
    this.TextColor = SystemColors.ControlText;
  }

  public CalendarRendererBoxEventArgs(CalendarRendererEventArgs original, Rectangle bounds)
    : this(original)
  {
    this.Bounds = bounds;
  }

  public CalendarRendererBoxEventArgs(
    CalendarRendererEventArgs original,
    Rectangle bounds,
    string text)
    : this(original)
  {
    this.Bounds = bounds;
    this.Text = text;
  }

  public CalendarRendererBoxEventArgs(
    CalendarRendererEventArgs original,
    Rectangle bounds,
    string text,
    TextFormatFlags flags)
    : this(original)
  {
    this.Bounds = bounds;
    this.Text = text;
    this.Format |= flags;
  }

  public CalendarRendererBoxEventArgs(
    CalendarRendererEventArgs original,
    Rectangle bounds,
    string text,
    Color textColor)
    : this(original)
  {
    this.Bounds = bounds;
    this.Text = text;
    this.TextColor = textColor;
  }

  public CalendarRendererBoxEventArgs(
    CalendarRendererEventArgs original,
    Rectangle bounds,
    string text,
    Color textColor,
    TextFormatFlags flags)
    : this(original)
  {
    this.Bounds = bounds;
    this.Text = text;
    this.TextColor = textColor;
    this.Format |= flags;
  }

  public CalendarRendererBoxEventArgs(
    CalendarRendererEventArgs original,
    Rectangle bounds,
    string text,
    Color textColor,
    Color backgroundColor)
    : this(original)
  {
    this.Bounds = bounds;
    this.Text = text;
    this.TextColor = textColor;
    this.BackgroundColor = backgroundColor;
  }

  /// <summary>Gets or sets the background color of the text</summary>
  public Color BackgroundColor
  {
    get => this._backgroundColor;
    set => this._backgroundColor = value;
  }

  /// <summary>Gets or sets the bounds to draw the text</summary>
  public Rectangle Bounds
  {
    get => this._bounds;
    set => this._bounds = value;
  }

  /// <summary>Gets or sets the font of the text to be rendered</summary>
  public Font Font
  {
    get => this._font;
    set
    {
      this._font = value;
      this._textSize = Size.Empty;
    }
  }

  /// <summary>Gets or sets the format to draw the text</summary>
  public TextFormatFlags Format
  {
    get => this._format;
    set
    {
      this._format = value;
      this._textSize = Size.Empty;
    }
  }

  /// <summary>Gets or sets the text to draw</summary>
  public string Text
  {
    get => this._text;
    set
    {
      this._text = value;
      this._textSize = Size.Empty;
    }
  }

  /// <summary>Gets the result of measuring the text</summary>
  public Size TextSize
  {
    get
    {
      if (this._textSize.IsEmpty)
        this._textSize = TextRenderer.MeasureText(this.Text, this.Font);
      return this._textSize;
    }
  }

  /// <summary>Gets or sets the color to draw the text</summary>
  public Color TextColor
  {
    get => this._textColor;
    set => this._textColor = value;
  }
}
