// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.CalendarFactory
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Класс-конструктор календарей с его помощью можно получить доступ к календарю по идентификатору.
/// Автоматически пользует кэш</summary>
/// <exception cref="T:Intermech.Interfaces.Calendars.CalendarNotFoundException">Если календарь с указанным идентификатором не найден и <see cref="!:throwIfNotFound" />==true</exception>
[Obsolete("Use Intermech.Calendars.CalendarLoader!")]
public class CalendarFactory
{
  [NotNull]
  [Obsolete("Use Intermech.Calendars.CalendarLoader.GetCalendarByID instead!")]
  public static Calendar CreateCalendarByID([NotEmpty] long calendarID, [NotNull] IUserSession iUserSession)
  {
    return CalendarLoader.GetCalendarByID(iUserSession, calendarID);
  }
}
