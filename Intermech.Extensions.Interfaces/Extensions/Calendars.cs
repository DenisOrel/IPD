// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Calendars
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Metadata;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class Calendars
{
  [CanBeNull]
  private static ICalendarsService _service;
  [CanBeNull]
  private static ICalendar _standard;
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  public static long StandardCalendarID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return SystemObject.StandardCalendar.ObjectID;
    }
  }

  [NotNull]
  public static ICalendarsService Service
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Intermech.Extensions.Calendars._service.CheckInitializedIn<ICalendarsService>(typeof (Library));
    }
  }

  [ContractAnnotation("throwIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ICalendar Get([NotNull] IUserSession userSession, [NotEmpty] long calendarID, bool throwIfNotFound = true)
  {
    return Intermech.Extensions.Calendars.Service.GetCalendar(userSession, calendarID, throwIfNotFound);
  }

  [NotNull]
  public static ICalendar Standard
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Intermech.Extensions.Calendars._standard.CheckInitializedIn<ICalendar>(typeof (Library));
    }
  }

  internal static void Init([NotNull] IUserSession session, [CanBeNull] ICalendarsService calendarsService = null)
  {
    Intermech.Extensions.Calendars._initOnce.Invoke((Action) (() =>
    {
      Intermech.Extensions.Calendars._service = calendarsService ?? Services.Calendars;
      Intermech.Extensions.Calendars._standard = Intermech.Extensions.Calendars._service.GetCalendar(session, Intermech.Extensions.Calendars.StandardCalendarID);
    }));
  }
}
