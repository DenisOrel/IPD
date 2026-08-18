// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.Services
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Calendars;

public abstract class Services : Intermech.Extensions.Services
{
  /// <summary><see cref="P:Intermech.Calendars.Services.CalendarsService" /></summary>
  [NotNull]
  public static CalendarsService CalendarsService
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Intermech.Extensions.Services.Calendars is CalendarsService calendars ? calendars : throw new NullReferenceException("Intermech.Calendars.CalendarsService is null!");
    }
  }
}
