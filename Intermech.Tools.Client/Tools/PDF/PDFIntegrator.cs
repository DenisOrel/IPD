// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFIntegrator
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
namespace Intermech.Tools.PDF;

internal sealed class PDFIntegrator : ConfigurableIntegrator
{
  public override Guid Id => PDFConsts.IntegratorId;

  public override string DisplayName => PDFConsts.IntegratorName;

  public override Image GetApplicationImage(AppImageSize imageSize)
  {
    if (imageSize == AppImageSize.Image16x16)
      return (Image) PDFResources.IR_PDF_16x16;
    return imageSize == AppImageSize.Image32x32 ? (Image) PDFResources.IR_PDF_32x32 : base.GetApplicationImage(imageSize);
  }

  protected override void DoCreateServices()
  {
    base.DoCreateServices();
    this.Services.Add((IIntegratorService) new PDFFileTypesService((IIntegrator) this));
    this.Services.Add((IIntegratorService) new PDFIntegratorSettingsService((IIntegrator) this));
    this.Services.Add((IIntegratorService) this.CreateApiService());
    this.Services.Add((IIntegratorService) this.CreateStandaloneViewService());
    PDFDataExchangeFactory dataExchangeFactory = new PDFDataExchangeFactory((IIntegrator) this);
    this.Services.Add((IIntegratorService) this.CreateFileImportService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreateCaptureChangesService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreateEmbedAttributesService(dataExchangeFactory));
    this.Services.Add((IIntegratorService) this.CreatePrepareNewObjectsService());
  }

  private SingleFileImportService CreateFileImportService(PDFDataExchangeFactory dataExchangeFactory)
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    SingleFileImportService fileImportService = new SingleFileImportService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
    fileImportService.FileTypeService = service;
    return fileImportService;
  }

  private SingleFileCaptureChangesService CreateCaptureChangesService(
    PDFDataExchangeFactory dataExchangeFactory)
  {
    return new SingleFileCaptureChangesService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
  }

  private SingleFileEmbedAttributesService CreateEmbedAttributesService(
    PDFDataExchangeFactory dataExchangeFactory)
  {
    return new SingleFileEmbedAttributesService((IIntegrator) this, (SingleFileDataExchangeFactory) dataExchangeFactory);
  }

  private PDFApiService CreateApiService()
  {
    IApplicationFileTypes service = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    return new PDFApiService((IIntegrator) this)
    {
      FileTypeService = service
    };
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
    return ServiceUtils.GetService<IPersistentIntegratorSettingsService>((object) this, true);
  }

  protected override IIntegratorSettingsViewModelService TryGetSettingsViewModelService()
  {
    return (IIntegratorSettingsViewModelService) null;
  }
}
