
// Type: Intermech.Client.Core.Organizer.SchedulerItemCancelEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class SchedulerItemCancelEventArgs : CancelEventArgs
{
  private CalendarItem _item;

  /// <summary>Элемент планировщика.</summary>
  public CalendarItem Item => this._item;

  /// <summary>Конструктор.</summary>
  /// <param name="item">Элемент планировщика</param>
  public SchedulerItemCancelEventArgs(CalendarItem item) => this._item = item;
}
