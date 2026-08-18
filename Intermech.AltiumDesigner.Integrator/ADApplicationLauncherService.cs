// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADApplicationLauncherService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

public sealed class ADApplicationLauncherService(IIntegrator owner) : 
  ApplicationLauncherService(owner),
  IApplicationLauncherService
{
  protected override void DoLaunchApplication()
  {
    using (ADApiSession adApiSession = new ADApiSession(this.Integrator))
      adApiSession.Application.SwitchToApp();
  }
}
