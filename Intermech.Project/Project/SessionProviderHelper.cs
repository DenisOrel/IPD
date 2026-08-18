// Decompiled with JetBrains decompiler
// Type: Intermech.Project.SessionProviderHelper
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Хелпер-класс для быстрого вызова кода, связанного с обращением к сессии с помощью внешней функции</summary>
public static class SessionProviderHelper
{
  /// <summary>Быстрый вызов кода, связанного с обращением к сессии с помощью внешней функции</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeSession(
    [NotNull] this ISessionProvider sessionProvider,
    [NotNull, InstantHandle] Session.SessionHandler sessionHandler)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionHandler(sessionKeeper.Session);
  }

  /// <summary>Быстрый вызов кода, связанного с обращением к сессии с помощью внешней функции</summary>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T InvokeSession<T>(
    [NotNull] this ISessionProvider sessionProvider,
    [NotNull, InstantHandle] Session.SessionHandler<T> sessionHandler)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionHandler(sessionKeeper.Session);
  }
}
