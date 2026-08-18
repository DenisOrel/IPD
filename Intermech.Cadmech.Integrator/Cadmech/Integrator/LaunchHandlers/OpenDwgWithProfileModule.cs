// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.LaunchHandlers.OpenDwgWithProfileModule
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Tools.LaunchActions;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator.LaunchHandlers;

internal sealed class OpenDwgWithProfileModule : InitializerModule
{
  private ILaunchActionService launchActionService;
  private OpenDwgWithProfileLaunchHandler launchHandler;

  public OpenDwgWithProfileModule(
    ILaunchActionService launchActionService,
    OpenDwgWithProfileLaunchHandler launchHandler)
  {
    if (launchActionService == null)
      throw new ArgumentNullException(nameof (launchActionService));
    if (launchHandler == null)
      throw new ArgumentNullException(nameof (launchHandler));
    this.launchActionService = launchActionService;
    this.launchHandler = launchHandler;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.launchActionService.RegisterHandler((ILaunchHandler) this.launchHandler);
  }

  protected override void DoShutdown()
  {
    this.launchActionService.UnregisterHandler((ILaunchHandler) this.launchHandler);
    base.DoShutdown();
  }
}
