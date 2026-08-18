
// Type: Intermech.Client.Core.Organizer.SchedulerDatesEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class SchedulerDatesEventArgs : EventArgs
{
  private Scheduler _scheduler;
  private DateTime _firstDate = DateTime.MinValue;
  private DateTime _secondDate = DateTime.MinValue;

  /// <summary>Планировщик.</summary>
  public Scheduler Scheduler => this._scheduler;

  /// <summary>Дата начала.</summary>
  public DateTime FirstDate
  {
    get => this._firstDate;
    set => this._firstDate = value;
  }

  /// <summary>Дата окончания.</summary>
  public DateTime SecondDate => this._secondDate;

  /// <summary>Конструктор.</summary>
  /// <param name="scheduler">Планировщик</param>
  /// <param name="firstDate">Дата начала</param>
  /// <param name="secondDate">Дата окончания</param>
  public SchedulerDatesEventArgs(Scheduler scheduler, DateTime firstDate, DateTime secondDate)
  {
    this._scheduler = scheduler;
    this._firstDate = firstDate;
    this._secondDate = secondDate;
  }
}
