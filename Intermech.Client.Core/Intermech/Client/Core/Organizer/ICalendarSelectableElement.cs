
// Type: Intermech.Client.Core.Organizer.ICalendarSelectableElement
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Interface implemented by every selectable element of the calendar
/// </summary>
public interface ICalendarSelectableElement : 
  ISelectableElement,
  IComparable<ICalendarSelectableElement>
{
  /// <summary>Gets the calendar this element belongs to</summary>
  Scheduler Scheduler { get; }

  /// <summary>Gets the calendar</summary>
  DateTime Date { get; }
}
