
// Type: Intermech.Client.Core.Organizer.CalendarRendererTimeUnitEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Contains information about a <see cref="T:Intermech.Client.Core.Organizer.SchedulerTimeScaleUnit" /> that is about to be painted
/// </summary>
public class CalendarRendererTimeUnitEventArgs : CalendarRendererEventArgs
{
  private SchedulerTimeScaleUnit _unit;

  public CalendarRendererTimeUnitEventArgs(
    CalendarRendererEventArgs original,
    SchedulerTimeScaleUnit unit)
    : base(original)
  {
    this._unit = unit;
  }

  /// <summary>Gets the unit that is about to be painted</summary>
  public SchedulerTimeScaleUnit Unit => this._unit;
}
