// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelIntegrator
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
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class ExcelIntegrator : ConfigurableIntegrator
{
  public override Guid Id => ExcelConsts.IntegratorId;

  public override string DisplayName => "Интегратор с Microsoft Excel";

  public override Image GetApplicationImage(AppImageSize imageSize)
  {
    if (imageSize == AppImageSize.Image16x16)
      return (Image) MsoResources.IR_XLS_16x16;
    return imageSize == AppImageSize.Image32x32 ? (Image) MsoResources.IR_XLS_32x32 : base.GetApplicationImage(imageSize);
  }

  protected override void DoCreateServices()
  {
    base.DoCreateServices();
    this.Services.Add((IIntegratorService) new ExcelFileTypesService((IIntegrator) this));
    this.Services.Add((IIntegratorService) new ExcelIntegratorSettingsService((IIntegrator) this));
    this.Services.Add((IIntegratorService) this.CreateApiService());
    this.Services.Add((IIntegratorService) this.CreateStandaloneViewService());
    this.Services.Add((IIntegratorService) this.CreateApplicationLauncherService());
    ExcelDataExchangeFactory dataExchangeFactory = new ExcelDataExchangeFactory((IIntegrator) this);
    this.Services.Add((IIntegratorService) this.CreateFileImportService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreateCaptureChangesService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreateEmbedAttributesService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreatePrepareNewObjectsService());
    this.Services.Add((IIntegratorService) this.CreateExcelAuthenticFilesService());
  }

  private ExcelAuthenticFilesService CreateExcelAuthenticFilesService()
  {
    IApplicationApiService service = ServiceUtils.GetService<IApplicationApiService>((object) this, true);
    return new ExcelAuthenticFilesService((IIntegrator) this)
    {
      ApiService = service
    };
  }

  private SingleFileImportService CreateFileImportService(
    ExcelDataExchangeFactory dataExchangeFactory)
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    SingleFileImportService fileImportService = new SingleFileImportService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
    fileImportService.FileTypeService = service;
    return fileImportService;
  }

  private SingleFileCaptureChangesService CreateCaptureChangesService(
    ExcelDataExchangeFactory dataExchangeFactory)
  {
    return new SingleFileCaptureChangesService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
  }

  private SingleFileEmbedAttributesService CreateEmbedAttributesService(
    ExcelDataExchangeFactory dataExchangeFactory)
  {
    return new SingleFileEmbedAttributesService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
  }

  private ExcelApiService CreateApiService()
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    ExcelApiService apiService = new ExcelApiService((IIntegrator) this);
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

  private ExcelLauncherService CreateApplicationLauncherService()
  {
    IApplicationApiService service1 = ServiceUtils.GetService<IApplicationApiService>((object) this, true);
    ExcelIntegratorSettingsService service2 = ServiceUtils.GetService<ExcelIntegratorSettingsService>((object) this, true);
    ExcelLauncherService applicationLauncherService = new ExcelLauncherService((IIntegrator) this);
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
    return (IPersistentIntegratorSettingsService) ServiceUtils.GetService<ExcelIntegratorSettingsService>((object) this, true);
  }

  protected override IIntegratorSettingsViewModelService TryGetSettingsViewModelService()
  {
    return (IIntegratorSettingsViewModelService) null;
  }
}
