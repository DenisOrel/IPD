// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Server.Library
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Extensions.Server;

public abstract class Library
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  public static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Library._initOnce.Invoke((Action) (() =>
    {
      Intermech.Extensions.Interfaces.Library.Init(serviceProvider, session);
      Intermech.Server.Services.Init(serviceProvider, session);
    }));
  }
}
