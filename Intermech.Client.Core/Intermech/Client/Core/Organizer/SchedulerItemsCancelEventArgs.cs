
// Type: Intermech.Client.Core.Organizer.SchedulerItemsCancelEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class SchedulerItemsCancelEventArgs : CancelEventArgs
{
  private List<CalendarItem> _items;

  /// <summary>Список элементов планировщика.</summary>
  public List<CalendarItem> Items => this._items;

  /// <summary>Конструктор.</summary>
  /// <param name="items">Элемент планировщика</param>
  public SchedulerItemsCancelEventArgs(List<CalendarItem> items) => this._items = items;
}
