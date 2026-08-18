
// Type: Intermech.Client.Core.Organizer.CalendarBoxEventArgs
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
public class CalendarBoxEventArgs
{
  /// <summary>Цвет фона.</summary>
  public Color BackgroundColor { get; set; }

  /// <summary>Цвет границы.</summary>
  public Color BorderColor { get; set; }

  /// <summary>Площадь.</summary>
  public Rectangle Bounds { get; private set; }

  /// <summary>Шрифт.</summary>
  public Font Font { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Graphics Graphics { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public bool IsMarked { get; set; }

  /// <summary>Текст.</summary>
  public string Text { get; set; }

  /// <summary>Цвет текста.</summary>
  public Color TextColor { get; set; }

  /// <summary>Флаги текста.</summary>
  public TextFormatFlags TextFlags { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="g"></param>
  /// <param name="bounds">Площадь</param>
  /// <param name="text">Текст</param>
  /// <param name="textAlign">Выравнивание текста</param>
  /// <param name="textColor">Цвет текста</param>
  /// <param name="backColor">Цвет фона</param>
  /// <param name="borderColor">Цвет границы</param>
  internal CalendarBoxEventArgs(
    Graphics g,
    Rectangle bounds,
    string text,
    StringAlignment textAlign,
    Color textColor,
    Color backColor,
    Color borderColor)
  {
    this.Graphics = g;
    this.Bounds = bounds;
    this.Text = text;
    this.TextColor = textColor;
    this.BackgroundColor = backColor;
    this.BorderColor = borderColor;
    switch (textAlign)
    {
      case StringAlignment.Near:
        this.TextFlags |= TextFormatFlags.Default;
        break;
      case StringAlignment.Center:
        this.TextFlags |= TextFormatFlags.HorizontalCenter;
        break;
      case StringAlignment.Far:
        this.TextFlags |= TextFormatFlags.Right;
        break;
    }
    this.TextFlags |= TextFormatFlags.VerticalCenter;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="g"></param>
  /// <param name="bounds">Площадь</param>
  /// <param name="text">Текст</param>
  /// <param name="textColor">Цвет текста</param>
  /// <param name="backColor">Цвет фона</param>
  internal CalendarBoxEventArgs(
    Graphics g,
    Rectangle bounds,
    string text,
    Color textColor,
    Color backColor)
    : this(g, bounds, text, StringAlignment.Center, textColor, backColor, Color.Empty)
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="g"></param>
  /// <param name="bounds">Площадь</param>
  /// <param name="text">Текст</param>
  /// <param name="textAlign">Выравнивание текста</param>
  /// <param name="textColor">Цвет текста</param>
  /// <param name="backColor">Цвет фона</param>
  internal CalendarBoxEventArgs(
    Graphics g,
    Rectangle bounds,
    string text,
    StringAlignment textAlign,
    Color textColor,
    Color backColor)
    : this(g, bounds, text, textAlign, textColor, backColor, Color.Empty)
  {
  }
}
