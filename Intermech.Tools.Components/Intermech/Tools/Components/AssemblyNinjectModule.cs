// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Components.AssemblyNinjectModule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Client;
using Intermech.Services.CaptureChanges;
using Intermech.Services.IMViewer;
using Intermech.Services.Requirement;
using Intermech.Services.WeldingJoints;
using Intermech.Tools.Data;
using Ninject;
using Ninject.Modules;

#nullable disable
namespace Intermech.Tools.Components;

public sealed class AssemblyNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<IDCache>().ToSelf().InSingletonScope();
    this.Bind<ICaptureFileChangesService>().To<CaptureFileChangesService>().InSingletonScope();
    this.Kernel.Load((INinjectModule) new WeldingJointsNinjectModule());
    this.Kernel.Load((INinjectModule) new RequirementsNinjectModule());
    this.Kernel.Load((INinjectModule) new IMViewerNinjectModule());
  }
}
