
// Type: Intermech.Client.Core.Organizer.CalendarRendererItemEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Organizer;

/// <summary>Contains information to render an item</summary>
public class CalendarRendererItemEventArgs : CalendarRendererEventArgs
{
  private CalendarItem _item;

  public CalendarRendererItemEventArgs(CalendarRendererEventArgs original, CalendarItem item)
    : base(original)
  {
    this._item = item;
  }

  /// <summary>Gets the Item being rendered</summary>
  public CalendarItem Item => this._item;
}
