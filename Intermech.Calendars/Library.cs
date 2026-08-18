// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.Library
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.Calendars;

public static class Library
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  [NotNull]
  internal static Assembly Assembly => typeof (Library).Assembly;

  /// <summary>Инициализация сервисов, кэшей и т.п. библиотеки Intermech.Project.Controls</summary>
  public static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Library._initOnce.Invoke(ref session, (Action) (() =>
    {
      MetadataLoader.Init(session);
      Intermech.Extensions.Interfaces.Library.Init(serviceProvider, session);
    }));
  }
}
