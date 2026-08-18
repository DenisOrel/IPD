// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.ExtAppModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Tools.ExtApps;
using Intermech.Tools.LaunchActions;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class ExtAppModule : InitializerModule
{
  private ExtAppLaunchHandler launchHandler;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.launchHandler = new ExtAppLaunchHandler();
    ClientContext.LaunchActions.RegisterHandler((ILaunchHandler) this.launchHandler);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.launchHandler == null)
      return;
    ClientContext.LaunchActions.UnregisterHandler((ILaunchHandler) this.launchHandler);
    this.launchHandler = (ExtAppLaunchHandler) null;
  }
}
