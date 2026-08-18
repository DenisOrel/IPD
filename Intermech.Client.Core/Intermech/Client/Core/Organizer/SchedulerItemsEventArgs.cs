
// Type: Intermech.Client.Core.Organizer.SchedulerItemsEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class SchedulerItemsEventArgs : EventArgs
{
  private List<CalendarItem> _items;

  /// <summary>Список элементов планировщика.</summary>
  public List<CalendarItem> Items => this._items;

  /// <summary>Конструктор.</summary>
  /// <param name="items"></param>
  public SchedulerItemsEventArgs(List<CalendarItem> items) => this._items = items;
}
