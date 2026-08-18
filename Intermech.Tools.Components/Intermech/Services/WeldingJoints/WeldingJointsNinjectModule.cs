// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingJointsNinjectModule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Ninject.Modules;
using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

internal sealed class WeldingJointsNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Kernel.Bind<WeldingSeamsIDCache>().ToSelf().InSingletonScope();
    this.Kernel.Bind<WeldingSeamsModelConfiguration>().ToSelf().InSingletonScope().OnActivation((Action<WeldingSeamsModelConfiguration>) (instance => instance.Initialize()));
    this.Kernel.Bind<IWeldingSeamsModelRoot>().To<WeldingSeamsModelRoot>();
    this.Kernel.Bind<IWeldingJointsService>().To<WeldingJointsService>().InSingletonScope();
  }
}
