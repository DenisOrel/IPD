// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Interfaces.Library
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;

#nullable disable
namespace Intermech.Extensions.Interfaces;

public static class Library
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  public static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Library._initOnce.Invoke(ref session, (Action) (() =>
    {
      Intermech.Diagnostics.Check.NotNull<IUserSession>(session, nameof (session));
      Intermech.Extensions.Library.Init(serviceProvider);
      MetadataLoader.Init(session);
      Services.Init(serviceProvider, session);
      Calendars.Init(session);
      Portal.Init(session);
    }));
  }
}
