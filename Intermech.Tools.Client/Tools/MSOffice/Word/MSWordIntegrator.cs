// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordIntegrator
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Simple;
using Intermech.Tools.Integrators.StandaloneView;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

internal sealed class MSWordIntegrator : ConfigurableIntegrator
{
  public override Guid Id => MSWordConsts.IntegratorId;

  public override string DisplayName => "Интегратор с Microsoft Word";

  public override Image GetApplicationImage(AppImageSize imageSize)
  {
    if (imageSize == AppImageSize.Image16x16)
      return (Image) MsoResources.IR_DOC_16x16;
    return imageSize == AppImageSize.Image32x32 ? (Image) MsoResources.IR_DOC_32x32 : base.GetApplicationImage(imageSize);
  }

  protected override void DoCreateServices()
  {
    base.DoCreateServices();
    this.Services.Add((IIntegratorService) new MSWordFileTypesService((IIntegrator) this));
    this.Services.Add((IIntegratorService) new MSWordIntegratorSettingsService((IIntegrator) this));
    this.Services.Add((IIntegratorService) this.CreateApiService());
    this.Services.Add((IIntegratorService) this.CreateStandaloneViewService());
    this.Services.Add((IIntegratorService) this.CreateApplicationLauncherService());
    MSWordDataExchangeFactory dataExchangeFactory = new MSWordDataExchangeFactory((IIntegrator) this);
    this.Services.Add((IIntegratorService) this.CreateFileImportService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreateCaptureChangesService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreateEmbedAttributesService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreatePrepareNewObjectsService());
    this.Services.Add((IIntegratorService) this.CreateWordAuthenticFilesService());
  }

  private MSWordAuthenticFilesService CreateWordAuthenticFilesService()
  {
    IApplicationApiService service = ServiceUtils.GetService<IApplicationApiService>((object) this, true);
    return new MSWordAuthenticFilesService((IIntegrator) this)
    {
      ApiService = service
    };
  }

  private SingleFileImportService CreateFileImportService(
    MSWordDataExchangeFactory dataExchangeFactory)
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    SingleFileImportService fileImportService = new SingleFileImportService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
    fileImportService.FileTypeService = service;
    return fileImportService;
  }

  private SingleFileCaptureChangesService CreateCaptureChangesService(
    MSWordDataExchangeFactory dataExchangeFactory)
  {
    return new SingleFileCaptureChangesService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
  }

  private SingleFileEmbedAttributesService CreateEmbedAttributesService(
    MSWordDataExchangeFactory dataExchangeFactory)
  {
    return new SingleFileEmbedAttributesService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
  }

  private MSWordApiService CreateApiService()
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    MSWordApiService apiService = new MSWordApiService((IIntegrator) this);
    apiService.FileTypeService = service;
    return apiService;
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

  private MSWordLauncherService CreateApplicationLauncherService()
  {
    IApplicationApiService service1 = ServiceUtils.GetService<IApplicationApiService>((object) this, true);
    MSWordIntegratorSettingsService service2 = ServiceUtils.GetService<MSWordIntegratorSettingsService>((object) this, true);
    MSWordLauncherService applicationLauncherService = new MSWordLauncherService((IIntegrator) this);
    applicationLauncherService.ApiService = service1;
    applicationLauncherService.SettingsService = (IIntegratorSettingsService) service2;
    return applicationLauncherService;
  }

  private SingleFilePrepareNewObjectsService CreatePrepareNewObjectsService()
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    return new SingleFilePrepareNewObjectsService((IIntegrator) this)
    {
      FileTypeService = service
    };
  }

  protected override IPersistentIntegratorSettingsService GetSettingsService()
  {
    return (IPersistentIntegratorSettingsService) ServiceUtils.GetService<MSWordIntegratorSettingsService>((object) this, true);
  }

  protected override IIntegratorSettingsViewModelService TryGetSettingsViewModelService()
  {
    return (IIntegratorSettingsViewModelService) null;
  }
}
