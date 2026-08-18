// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.CADExtensionsPackage
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Tools.CADExtensions.Commands;
using Intermech.Tools.CADExtensions.ModelDrawings;
using Ninject;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using System;

#nullable disable
namespace Intermech.Tools.CADExtensions;

internal class CADExtensionsPackage : IOCBasedPackage
{
  private IStartupService startupService;
  private InitializerModuleGroup startupCompleteInitializers;

  public CADExtensionsPackage(
    IOCBasedPackageParameters createParameters,
    IStartupService startupService)
    : base(createParameters, "Расширенные команды для работы с чертежами на 3D-модели")
  {
    this.startupService = startupService != null ? startupService : throw new ArgumentNullException(nameof (startupService));
    this.startupCompleteInitializers = new InitializerModuleGroup();
  }

  protected override void DoLoad()
  {
    base.DoLoad();
    this.ScheduleInitializationAtStartupComplete();
  }

  protected override void DoUnload()
  {
    this.startupCompleteInitializers.Shutdown();
    base.DoUnload();
  }

  protected override void DoInitializeIOCContainer()
  {
    base.DoInitializeIOCContainer();
    this.IOCContainer.Load((INinjectModule) new AncillaryDrawingsNinjectModule());
    this.IOCContainer.Load((INinjectModule) new CADAssembliesNinjectModule());
  }

  private void ScheduleInitializationAtStartupComplete()
  {
    if (this.startupService.IsStartupCompleted)
      this.StartupCompleteHandler();
    else
      this.startupService.StartupComplete += (EventHandler) ((sender, e) => this.StartupCompleteHandler());
  }

  private void StartupCompleteHandler()
  {
    this.startupCompleteInitializers.AddRange(this.IOCContainer.GetAll<InitializerModule>((Func<IBindingMetadata, bool>) (binding => binding.Get<bool>(InitializerModuleTags.StartupComplete, false))));
    this.startupCompleteInitializers.Initialize();
  }
}
