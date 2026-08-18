
// Type: Intermech.Client.Core.Organizer.CalendarRendererEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Contains basic information about a drawing event for <see cref="T:Intermech.Client.Core.Organizer.CalendarRenderer" />
/// </summary>
public class CalendarRendererEventArgs : EventArgs
{
  private Scheduler _calendar;
  private Rectangle _clip;
  private Graphics _graphics;
  private object _tag;

  /// <summary>Use it wisely just to initialize some stuff</summary>
  protected CalendarRendererEventArgs()
  {
  }

  /// <summary>
  /// Creates a new <see cref="T:Intermech.Client.Core.Organizer.CalendarRendererEventArgs" />
  /// </summary>
  /// <param name="calendar">Calendar where painting</param>
  /// <param name="g">Device where to paint</param>
  /// <param name="clipRectangle">Paint event clip area</param>
  public CalendarRendererEventArgs(Scheduler calendar, Graphics g, Rectangle clipRectangle)
  {
    this._calendar = calendar;
    this._graphics = g;
    this._clip = clipRectangle;
  }

  /// <summary>
  /// Creates a new <see cref="T:Intermech.Client.Core.Organizer.CalendarRendererEventArgs" />
  /// </summary>
  /// <param name="calendar">Calendar where painting</param>
  /// <param name="g">Device where to paint</param>
  /// <param name="clipRectangle"></param>
  /// <param name="tag"></param>
  public CalendarRendererEventArgs(
    Scheduler calendar,
    Graphics g,
    Rectangle clipRectangle,
    object tag)
  {
    this._calendar = calendar;
    this._graphics = g;
    this._clip = clipRectangle;
    this._tag = tag;
  }

  /// <summary>
  /// Copies the parameters from the specified <see cref="T:Intermech.Client.Core.Organizer.CalendarRendererEventArgs" />
  /// </summary>
  /// <param name="original"></param>
  public CalendarRendererEventArgs(CalendarRendererEventArgs original)
  {
    this._calendar = original.Calendar;
    this._graphics = original.Graphics;
    this._clip = original.ClipRectangle;
    this._tag = original.Tag;
  }

  /// <summary>Gets the calendar where painting</summary>
  public Scheduler Calendar => this._calendar;

  /// <summary>Gets the clip of the paint event</summary>
  public Rectangle ClipRectangle => this._clip;

  /// <summary>Gets the device where to paint</summary>
  public Graphics Graphics => this._graphics;

  /// <summary>Gets or sets a tag for the event</summary>
  public object Tag
  {
    get => this._tag;
    set => this._tag = value;
  }
}
