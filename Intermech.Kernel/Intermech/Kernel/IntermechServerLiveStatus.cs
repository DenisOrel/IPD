// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.IntermechServerLiveStatus
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Diagnostics;


namespace Intermech.Kernel;

internal sealed class IntermechServerLiveStatus(IntermechServer server) : 
  IntermechServerService(server),
  IMServerLiveStatus
{
  public void KnockKnock()
  {
  }

  public void KnockKnock(object serverObject)
  {
    if (serverObject == null)
      throw new ArgumentNullException(nameof (serverObject));
    if (!(serverObject is IReliableServerObject reliableServerObject))
      return;
    reliableServerObject.KnockKnock();
  }

  public void KnockKnock(params object[] serverObjects)
  {
    if (serverObjects == null)
      throw new ArgumentNullException(nameof (serverObjects));
    foreach (object serverObject in serverObjects)
    {
      if (serverObject is IReliableServerObject reliableServerObject)
        reliableServerObject.KnockKnock();
    }
  }

  public int ActivityCounter
  {
    [DebuggerStepThrough] get => UserSession.Sessions.ActivityCounter;
  }

  public string ConnectionString
  {
    [DebuggerStepThrough] get => ServerConsts.ShortenedConnectionString;
  }
}
