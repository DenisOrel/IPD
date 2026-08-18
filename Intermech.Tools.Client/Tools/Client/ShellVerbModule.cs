// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.ShellVerbModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Tools.LaunchActions;
using Intermech.Tools.ShellVerbs;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class ShellVerbModule : InitializerModule
{
  private ShellVerbLaunchHandler launchHandler;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.launchHandler = new ShellVerbLaunchHandler();
    ClientContext.LaunchActions.RegisterHandler((ILaunchHandler) this.launchHandler);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.launchHandler == null)
      return;
    ClientContext.LaunchActions.UnregisterHandler((ILaunchHandler) this.launchHandler);
    this.launchHandler = (ShellVerbLaunchHandler) null;
  }
}
