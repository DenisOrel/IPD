
// Type: Intermech.Client.Core.Organizer.SchedulerHeaderRadioButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
internal class SchedulerHeaderRadioButton
{
  private string _text = string.Empty;
  private InputState _state;
  private bool _checked;
  private Rectangle _bounds = Rectangle.Empty;
  private Point _imgLocation = Point.Empty;
  private Rectangle _textBounds = Rectangle.Empty;

  /// <summary>
  /// 
  /// </summary>
  internal Rectangle Bounds => this._bounds;

  /// <summary>
  /// 
  /// </summary>
  internal bool Checked
  {
    get => this._checked;
    set => this._checked = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal Point ImageLocation => this._imgLocation;

  /// <summary>
  /// 
  /// </summary>
  internal InputState State
  {
    get => this._state;
    set => this._state = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal string Text => this._text;

  /// <summary>
  /// 
  /// </summary>
  internal Rectangle TextBounds => this._textBounds;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="location"></param>
  /// <param name="imgSize"></param>
  /// <param name="text"></param>
  /// <param name="font"></param>
  internal SchedulerHeaderRadioButton(Point location, Size imgSize, string text, Font font)
  {
    this._text = text;
    Size size = TextRenderer.MeasureText(text, font);
    int height = imgSize.Height > size.Height ? imgSize.Height : size.Height;
    this._bounds = new Rectangle(location.X, location.Y - height / 2, imgSize.Width + 2 + size.Width, height);
    this._imgLocation = new Point(location.X, location.Y - imgSize.Height / 2);
    this._textBounds = new Rectangle(location.X + imgSize.Width + 2, location.Y - size.Height / 2, size.Width, size.Height);
  }
}
