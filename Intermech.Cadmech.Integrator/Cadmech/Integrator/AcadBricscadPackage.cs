// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadBricscadPackage
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Cadmech.Integrator.COM;
using Intermech.Cadmech.Integrator.DwgCreator;
using Intermech.Cadmech.Integrator.LaunchHandlers;
using Intermech.Interfaces.Plugins;
using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

public sealed class AcadBricscadPackage : IOCBasedPackage, IPackageExtension
{
  private AcadControlPanelModule controlPanelModule;

  public AcadBricscadPackage(IOCBasedPackageParameters createParameters)
    : base(createParameters, "Интегратор с AutoCAD (BricsCAD)")
  {
    this.controlPanelModule = new AcadControlPanelModule();
    this.controlPanelModule.ExceptionPolicy = InitializerExceptionPolicy.Suppress;
  }

  protected override void DoInitializeIOCContainer()
  {
    base.DoInitializeIOCContainer();
    this.IOCContainer.Bind<ActiveCADSystemService>().ToSelf().InSingletonScope();
    this.IOCContainer.Bind<AcadIntegrator>().ToSelf();
    this.IOCContainer.Bind<IntegratorModule<AcadIntegrator>>().ToSelf().OnActivation(new Action<IntegratorModule<AcadIntegrator>>(this.SetupAcadIntegratorModule));
    this.IOCContainer.Bind<AcadArticleAttributesGuardModule>().ToSelf();
    this.IOCContainer.Bind<BricscadIntegrator>().ToSelf();
    this.IOCContainer.Bind<IntegratorModule<BricscadIntegrator>>().ToSelf().OnActivation(new Action<IntegratorModule<BricscadIntegrator>>(this.SetupBricscadIntegratorModule));
    this.IOCContainer.Bind<BricscadArticleAttributesGuardModule>().ToSelf();
    this.IOCContainer.Bind<NanocadIntegrator>().ToSelf();
    this.IOCContainer.Bind<IntegratorModule<NanocadIntegrator>>().ToSelf().OnActivation(new Action<IntegratorModule<NanocadIntegrator>>(this.SetupNanocadIntegratorModule));
    this.IOCContainer.Bind<NanocadArticleAttributesGuardModule>().ToSelf();
    this.IOCContainer.Bind<SearchAPIServiceLink>().ToSelf().InSingletonScope();
    this.IOCContainer.Bind<SearchAPI>().ToSelf();
    this.IOCContainer.Bind<SpdsAPI>().ToSelf();
    this.IOCContainer.Bind<SearchAPIModule>().ToSelf();
    this.IOCContainer.Bind<OpenDwgWithProfileLaunchHandler>().ToSelf();
    this.IOCContainer.Bind<OpenDwgWithProfileModule>().ToSelf();
    this.IOCContainer.Bind<DwgCreatorProvider>().ToSelf();
    this.IOCContainer.Bind<DwgCreatorModule>().ToSelf();
  }

  private void SetupAcadIntegratorModule(IntegratorModule<AcadIntegrator> integratorModule)
  {
    integratorModule.EnableLaunchHandler(AcadConsts.ApplicationName);
  }

  private void SetupBricscadIntegratorModule(
    IntegratorModule<BricscadIntegrator> integratorModule)
  {
    integratorModule.EnableLaunchHandler(BricscadConsts.ApplicationName);
  }

  private void SetupNanocadIntegratorModule(
    IntegratorModule<NanocadIntegrator> integratorModule)
  {
    integratorModule.EnableLaunchHandler(NanocadConsts.ApplicationName);
  }

  protected override void CreateSubModules(LazyInitializerModuleGroup subModules)
  {
    base.CreateSubModules(subModules);
    subModules.Add<IntegratorModule<AcadIntegrator>>();
    subModules.Add<IntegratorModule<BricscadIntegrator>>();
    subModules.Add<IntegratorModule<NanocadIntegrator>>();
    subModules.Add<AcadArticleAttributesGuardModule>();
    subModules.Add<BricscadArticleAttributesGuardModule>();
    subModules.Add<NanocadArticleAttributesGuardModule>();
    subModules.Add<SearchAPIModule>();
    subModules.Add<OpenDwgWithProfileModule>();
    subModules.Add<DwgCreatorModule>();
  }

  protected override void DoUnload()
  {
    if (this.controlPanelModule != null)
    {
      this.controlPanelModule.Shutdown();
      this.controlPanelModule = (AcadControlPanelModule) null;
    }
    base.DoUnload();
  }

  public bool PostInit()
  {
    if (!this.controlPanelModule.CanInitialize())
      return false;
    this.controlPanelModule.Initialize();
    return true;
  }
}
