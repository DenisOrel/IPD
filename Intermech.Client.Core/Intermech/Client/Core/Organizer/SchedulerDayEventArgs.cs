
// Type: Intermech.Client.Core.Organizer.SchedulerDayEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class SchedulerDayEventArgs : EventArgs
{
  private CalendarDay _day;

  /// <summary>День планировщика.</summary>
  public CalendarDay SchedulerDay => this._day;

  /// <summary>Конструктор.</summary>
  /// <param name="day">День планировщика</param>
  public SchedulerDayEventArgs(CalendarDay day) => this._day = day;
}
