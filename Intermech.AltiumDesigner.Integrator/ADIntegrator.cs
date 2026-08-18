// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADIntegrator
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Integrator.Properties;
using Intermech.Interfaces;
using Intermech.Tools;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.StandaloneView;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADIntegrator : ConfigurableIntegrator
{
  protected override void DoCreateServices()
  {
    base.DoCreateServices();
    this.Services.Add((IIntegratorService) new FileTypeService((IIntegrator) this));
    this.Services.Add((IIntegratorService) new SettingsService((IIntegrator) this));
    this.Services.Add((IIntegratorService) this.CreateApiService());
    this.Services.Add((IIntegratorService) this.CreateFileImportService());
    this.Services.Add((IIntegratorService) new ADCaptureChangesService((IIntegrator) this));
    this.Services.Add((IIntegratorService) this.CreateExtendedSaveService());
    this.Services.Add((IIntegratorService) new ADEmbedAttributesService((IIntegrator) this));
    this.Services.Add((IIntegratorService) this.CreateStandaloneViewService());
    this.Services.Add((IIntegratorService) this.CreateApplicationLauncherService());
  }

  protected override IIntegratorLicense CreateLicenseService()
  {
    return (IIntegratorLicense) new ADLicenseService((IIntegrator) this);
  }

  private ImportService CreateFileImportService()
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    ImportService fileImportService = new ImportService((IIntegrator) this);
    fileImportService.FileTypeService = service;
    return fileImportService;
  }

  private ADExtendedSaveService CreateExtendedSaveService()
  {
    SettingsService service = ServiceUtils.GetService<SettingsService>((object) this, true);
    ADExtendedSaveService extendedSaveService = new ADExtendedSaveService((IIntegrator) this);
    extendedSaveService.SettingsService = service;
    return extendedSaveService;
  }

  private ADInterfaceService CreateApiService()
  {
    SettingsService service1 = ServiceUtils.GetService<SettingsService>((object) this, true);
    IApplicationFileTypes service2 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    return new ADInterfaceService((IIntegrator) this)
    {
      SettingsService = service1,
      FileTypeService = service2
    };
  }

  private ADApplicationLauncherService CreateApplicationLauncherService()
  {
    IApplicationApiService service1 = ServiceUtils.GetService<IApplicationApiService>((object) this, true);
    SettingsService service2 = ServiceUtils.GetService<SettingsService>((object) this, true);
    ADApplicationLauncherService applicationLauncherService = new ADApplicationLauncherService((IIntegrator) this);
    applicationLauncherService.ApiService = service1;
    applicationLauncherService.SettingsService = (IIntegratorSettingsService) service2;
    return applicationLauncherService;
  }

  private StandaloneViewService CreateStandaloneViewService()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    IDocumentApiService service2 = ServiceUtils.GetService<IDocumentApiService>((object) this, true);
    StandaloneViewService standaloneViewService = new StandaloneViewService((IIntegrator) this);
    standaloneViewService.FileTypeService = service1;
    standaloneViewService.DocumentApiService = service2;
    return standaloneViewService;
  }

  public override Guid Id => ADConsts.IntegratorId;

  public override string DisplayName => ADConsts.IntegratorName;

  public override string GetServerObjectTemplate()
  {
    return this.GetServerObjectTemplateFromResource("Intermech.AltiumDesigner.Integrator.Resources.Integrator template.xml");
  }

  public override Image GetApplicationImage(AppImageSize imageSize)
  {
    if (imageSize == AppImageSize.Image16x16)
      return (Image) Resources.ad16x16;
    return imageSize == AppImageSize.Image32x32 ? (Image) Resources.ad32x32 : base.GetApplicationImage(imageSize);
  }

  protected override IPersistentIntegratorSettingsService GetSettingsService()
  {
    return ServiceUtils.GetService<IPersistentIntegratorSettingsService>((object) this, true);
  }

  protected override IIntegratorSettingsViewModelService TryGetSettingsViewModelService()
  {
    return ServiceUtils.GetService<IIntegratorSettingsViewModelService>((object) this, true);
  }
}
