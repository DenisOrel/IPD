// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.InitOnceExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class InitOnceExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Invoke(
    [NotNull] this InitOnceGuardian initOnce,
    [CanBeNull] ref IUserSession maybeSession,
    [NotNull, InstantHandle] Action action)
  {
    if (initOnce.Completed)
      return;
    if (maybeSession != null)
    {
      initOnce.Invoke(action);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        maybeSession = sessionKeeper.Session;
        try
        {
          initOnce.Invoke(action);
        }
        finally
        {
          maybeSession = (IUserSession) null;
        }
      }
    }
  }
}
