// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DIntegrator
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Compass3D.Integrator.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Tools;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Integrators.CADInterface.ModelDrawings;
using Intermech.Tools.Integrators.StandaloneView;
using System;
using System.Diagnostics;
using System.Drawing;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DIntegrator : CADIntegrator
{
  protected override void DoCreateServices()
  {
    base.DoCreateServices();
    this.Services.Add((IIntegratorService) this.CreateDrawing2DDetectorService());
    this.Services.Add((IIntegratorService) this.CreateAncillaryDrawingsService());
  }

  protected override void DoConfigureServices()
  {
    base.DoConfigureServices();
    Drawing2DDetectorService service1 = ServiceUtils.GetService<Drawing2DDetectorService>((object) this, true);
    K3DCADInterfaceService service2 = ServiceUtils.GetService<K3DCADInterfaceService>((object) this, true);
    service2.Drawing2DDetectorService = service1;
    K3DCaptureChangesFactory service3 = ServiceUtils.GetService<K3DCaptureChangesFactory>((object) this, true);
    service3.ApiService = service2;
    service3.Drawing2DDetectorService = service1;
    new IMViewerExtensionModule(ServiceUtils.GetService<IIMViewerClientService>((object) ApplicationServices.Container, true)).AttachTo((IIntegrator) this);
  }

  private Drawing2DDetectorService CreateDrawing2DDetectorService()
  {
    K3DSettingsService service = ServiceUtils.GetService<K3DSettingsService>((object) this, true);
    return new Drawing2DDetectorService((IIntegrator) this)
    {
      SettingsService = service
    };
  }

  private K3DAncillaryDrawingsService CreateAncillaryDrawingsService()
  {
    IPluginManager service = ServiceUtils.GetService<IPluginManager>((object) ServicesManager.ServiceContainer, true);
    return new K3DAncillaryDrawingsService((IIntegrator) this)
    {
      PluginManager = service
    };
  }

  protected override ICADSettingsFactory CreateSettingsFactory()
  {
    return (ICADSettingsFactory) new K3DIntegratorSettingsFactory((CADIntegrator) this);
  }

  protected override IIntegratorLicense CreateLicenseService()
  {
    return (IIntegratorLicense) new K3DLicenseService((IIntegrator) this);
  }

  protected override IApplicationFileTypes CreateFileTypeService()
  {
    return (IApplicationFileTypes) new K3DFileTypeService((IIntegrator) this);
  }

  protected override CADInterfaceService CreateCADInterfaceService()
  {
    return (CADInterfaceService) new K3DCADInterfaceService((IIntegrator) this);
  }

  protected override IStandardPartLibraryService CreateStandardPartLibraryService()
  {
    return (IStandardPartLibraryService) new CADStandardPartLibraryService((IIntegrator) this, StandardLibraryMode.SeparateStandardSizes, "K3D Library");
  }

  protected override IModelDrawingsService CreateModelDrawingsService()
  {
    ICADSettingsService service = ServiceUtils.GetService<ICADSettingsService>((object) this, true);
    NormalModelDrawingsService modelDrawingsService = new NormalModelDrawingsService((IIntegrator) this, CompassConsts.DrawingFileExtension, new string[2]
    {
      CompassConsts.AssemblyFileExtension,
      CompassConsts.PartFileExtension
    });
    modelDrawingsService.SettingsProvider = (IModelDrawingsServiceSettings) new CADModelDrawingsServiceSettings((IIntegrator) this, service);
    return (IModelDrawingsService) modelDrawingsService;
  }

  protected override CADCaptureChangesFactory CreateCaptureChangesFactory()
  {
    return (CADCaptureChangesFactory) new K3DCaptureChangesFactory((IIntegrator) this);
  }

  protected override CADExtendedSaveService CreateExtendedSaveService(
    CADCaptureChangesFactory factory)
  {
    return factory != null ? (CADExtendedSaveService) new K3DExtendedSaveService((IIntegrator) this, factory) : throw new ArgumentNullException(nameof (factory));
  }

  protected override CADEmbedAttributesService CreateEmbedAttributesService()
  {
    return (CADEmbedAttributesService) new K3DEmbedAttributesService((IIntegrator) this);
  }

  protected override IStandaloneViewService CreateStandaloneViewService()
  {
    StandaloneViewServiceBase standaloneViewService = (StandaloneViewServiceBase) base.CreateStandaloneViewService();
    standaloneViewService.TempFileStrategy = (TempFileStrategy) new SameDirectoryTempFileStrategy();
    return (IStandaloneViewService) standaloneViewService;
  }

  protected override Guid GetPDMBrowserGuid() => new Guid("9a7b5050-1272-46f9-911e-50936a7d949a");

  protected override PDMBrowserService CreatePDMBrowserService(Guid cadSystemId)
  {
    return (PDMBrowserService) new K3DPDMBrowserService((IIntegrator) this, cadSystemId);
  }

  protected override CADPrepareNewObjectsService CreatePrepareNewObjectsService()
  {
    return (CADPrepareNewObjectsService) new K3DPrepareNewObjectsService((IIntegrator) this);
  }

  public override string DisplayName
  {
    [DebuggerStepThrough] get => Plugin.IntegratorName;
  }

  public override Guid Id
  {
    [DebuggerStepThrough] get => Plugin.IntegratorId;
  }

  public override string GetServerObjectTemplate()
  {
    return this.GetServerObjectTemplateFromResource("Intermech.Compass3D.Integrator.Resources.Integrator template.xml");
  }

  public override Image GetApplicationImage(AppImageSize imageSize)
  {
    if (imageSize == AppImageSize.Image16x16)
      return (Image) Resources._16x16;
    return imageSize == AppImageSize.Image32x32 ? (Image) Resources._32x32 : base.GetApplicationImage(imageSize);
  }
}
