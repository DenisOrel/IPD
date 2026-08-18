// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Client.Library
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Extensions.Client;

public static class Library
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  public static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Library._initOnce.Invoke(ref session, (Action) (() =>
    {
      Intermech.Diagnostics.Check.NotNull<IUserSession>(session, nameof (session));
      Intermech.Extensions.Interfaces.Library.Init(serviceProvider, session);
      Intermech.Client.Services.Init(serviceProvider, session);
      CurrentUser.Init(session);
    }));
  }
}
