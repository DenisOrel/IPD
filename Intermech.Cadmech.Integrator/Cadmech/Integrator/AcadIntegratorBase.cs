// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadIntegratorBase
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Cadmech.Integrator.Integrator;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using Ninject;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal abstract class AcadIntegratorBase : ConfigurableIntegrator
{
  [Inject]
  public ActiveCADSystemService ActiveCADSystemService { get; set; }

  protected override void DoCreateServices()
  {
    base.DoCreateServices();
    this.Services.Add((IIntegratorService) new AcadFileTypeService((IIntegrator) this));
    this.Services.Add((IIntegratorService) new AcadIntegratorSettingsService((IIntegrator) this));
    this.Services.Add((IIntegratorService) this.CreateLaunchActionService());
    this.Services.Add((IIntegratorService) this.CreateFileImportService());
    this.Services.Add((IIntegratorService) this.CreateCaptureChangesService());
    this.Services.Add((IIntegratorService) this.CreateApiServiceInternal());
    this.Services.Add((IIntegratorService) this.CreateOpenFilesService());
    this.Services.Add((IIntegratorService) this.CreateFileTreeService());
    this.Services.Add((IIntegratorService) this.CreateApplicationLauncherService());
    this.Services.Add((IIntegratorService) this.CreateCadmech2DService());
    this.Services.Add((IIntegratorService) this.CreateStandaloneViewService());
    this.Services.Add((IIntegratorService) this.CreateAuthenticFilesService());
    this.Services.Add((IIntegratorService) this.CreateTechRequirementsService());
  }

  protected override IIntegratorLicense CreateLicenseService()
  {
    return (IIntegratorLicense) new AcadLicenseService((IIntegrator) this);
  }

  private AcadLaunchActionService CreateLaunchActionService()
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    AcadLaunchActionService launchActionService = new AcadLaunchActionService((IIntegrator) this);
    launchActionService.FileTypeService = service;
    launchActionService.FileVault = ClientContext.FileVault;
    return launchActionService;
  }

  private AcadFileImportService CreateFileImportService()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    AcadIntegratorSettingsService service2 = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this, true);
    AcadFileImportService fileImportService = new AcadFileImportService((IIntegrator) this);
    fileImportService.FileTypeService = service1;
    fileImportService.SettingsService = service2;
    fileImportService.ActiveCADSystemService = this.ActiveCADSystemService;
    return fileImportService;
  }

  private AcadCaptureChangesService CreateCaptureChangesService()
  {
    return new AcadCaptureChangesService((IIntegrator) this);
  }

  private CadApiService CreateApiServiceInternal()
  {
    AcadIntegratorSettingsService service = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this, true);
    CadApiService apiService = this.DoCreateApiService();
    apiService.SettingsService = service;
    return apiService;
  }

  private AcadAuthenticFilesService CreateAuthenticFilesService()
  {
    CadApiService service = ServiceUtils.GetService<CadApiService>((object) this, true);
    return new AcadAuthenticFilesService((IIntegrator) this, ApplicationServices.Container.GetService(typeof (IFileVault)) as IFileVault)
    {
      ApiService = service
    };
  }

  protected abstract CadApiService DoCreateApiService();

  private AcadOpenFiles CreateOpenFilesService()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    CadApiService service2 = ServiceUtils.GetService<CadApiService>((object) this, true);
    return new AcadOpenFiles((IIntegrator) this)
    {
      FileTypeService = service1,
      ApiService = service2
    };
  }

  private AcadFileTreeService CreateFileTreeService()
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    return new AcadFileTreeService((IIntegrator) this)
    {
      FileTypeService = service,
      FileVault = ClientContext.FileVault
    };
  }

  private AcadApplicationLauncherService CreateApplicationLauncherService()
  {
    IApplicationApiService service1 = ServiceUtils.GetService<IApplicationApiService>((object) this, true);
    AcadIntegratorSettingsService service2 = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this, true);
    AcadApplicationLauncherService applicationLauncherService = new AcadApplicationLauncherService((IIntegrator) this);
    applicationLauncherService.ApiService = service1;
    applicationLauncherService.SettingsService = (IIntegratorSettingsService) service2;
    return applicationLauncherService;
  }

  private Cadmech2DService CreateCadmech2DService()
  {
    AcadIntegratorSettingsService service = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this, true);
    return new Cadmech2DService((IIntegrator) this)
    {
      SettingsService = service
    };
  }

  private IStandaloneViewService CreateStandaloneViewService()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    AcadIntegratorSettingsService service2 = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this, true);
    CadApiService service3 = ServiceUtils.GetService<CadApiService>((object) this, true);
    AcadStandaloneViewService standaloneViewService = new AcadStandaloneViewService((IIntegrator) this);
    standaloneViewService.FileTypeService = service1;
    standaloneViewService.IntegratorSettingsService = service2;
    standaloneViewService.ApiService = service3;
    return (IStandaloneViewService) standaloneViewService;
  }

  private ITechRequirementsService CreateTechRequirementsService()
  {
    return (ITechRequirementsService) new CadTechRequirementsService((IIntegrator) this);
  }

  protected override IPersistentIntegratorSettingsService GetSettingsService()
  {
    return ServiceUtils.GetService<IPersistentIntegratorSettingsService>((object) this, true);
  }

  protected override IIntegratorSettingsViewModelService TryGetSettingsViewModelService()
  {
    return ServiceUtils.GetService<IIntegratorSettingsViewModelService>((object) this, false);
  }
}
