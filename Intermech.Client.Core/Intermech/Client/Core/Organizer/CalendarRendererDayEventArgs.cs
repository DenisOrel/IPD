
// Type: Intermech.Client.Core.Organizer.CalendarRendererDayEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Contains information about a day to draw on the calendar
/// </summary>
public class CalendarRendererDayEventArgs : CalendarRendererEventArgs
{
  private CalendarDay _day;
  private string _format = "dddd";

  /// <summary>
  /// Creates a new <see cref="T:Intermech.Client.Core.Organizer.CalendarRendererDayEventArgs" /> object
  /// </summary>
  /// <param name="original">Orignal object to copy basic paramters</param>
  /// <param name="day">Day to render</param>
  public CalendarRendererDayEventArgs(CalendarRendererEventArgs original, CalendarDay day)
    : base(original)
  {
    this._day = day;
  }

  /// <summary>
  /// Creates a new <see cref="T:Intermech.Client.Core.Organizer.CalendarRendererDayEventArgs" /> object
  /// </summary>
  /// <param name="original">Orignal object to copy basic paramters</param>
  /// <param name="day">Day to render</param>
  /// <param name="format"></param>
  public CalendarRendererDayEventArgs(
    CalendarRendererEventArgs original,
    CalendarDay day,
    string format)
    : this(original, day)
  {
    this._format = format;
  }

  /// <summary>Gets the day to paint</summary>
  public CalendarDay Day => this._day;

  /// <summary>
  /// 
  /// </summary>
  public string Format => this._format;
}
