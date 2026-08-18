// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.SessionKeeperExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Interfaces;

public static class SessionKeeperExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T SessionGuarantee<T>(
    [CanBeNull] this SessionKeeper sessionKeeper,
    [NotNull, InstantHandle] SessionKeeperExtensions.NotNullSessionFunc<T> predicate)
  {
    if (sessionKeeper != null)
      return predicate(sessionKeeper.Session);
    using (SessionKeeper sessionKeeper1 = new SessionKeeper())
      return predicate(sessionKeeper1.Session);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void SessionGuarantee(
    [CanBeNull] this SessionKeeper sessionKeeper,
    [NotNull, InstantHandle] SessionKeeperExtensions.NotNullSessionAction action)
  {
    if (sessionKeeper != null)
    {
      action(sessionKeeper.Session);
    }
    else
    {
      using (SessionKeeper sessionKeeper1 = new SessionKeeper())
        action(sessionKeeper1.Session);
    }
  }

  [CanBeNull]
  public delegate T NotNullSessionFunc<T>([NotNull] IUserSession session);

  [CanBeNull]
  public delegate void NotNullSessionAction([NotNull] IUserSession session);
}
