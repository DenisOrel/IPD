// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADIntegrator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators.StandaloneView;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public abstract class CADIntegrator : ConfigurableIntegrator
{
  protected ICADSettingsFactory settingsFactory;

  /// <summary>
  /// Создает сервисы интегратора, определяющие его возможности.
  /// </summary>
  protected override void DoCreateServices()
  {
    base.DoCreateServices();
    this.settingsFactory = this.CreateSettingsFactory();
    this.Services.Add((IIntegratorService) this.settingsFactory.CreateSettingsService(this.IsAttributesSharedStorage()));
    this.Services.Add((IIntegratorService) this.CreatePDMBrowserService());
    this.Services.Add((IIntegratorService) this.CreateFileTypeService());
    this.Services.Add((IIntegratorService) this.CreateCADInterfaceServiceInternal());
    this.Services.Add((IIntegratorService) this.CreateCaptureChangesFactory());
    this.Services.Add((IIntegratorService) this.CreateFileImportService());
    this.Services.Add((IIntegratorService) this.CreateCaptureChangesService());
    this.Services.Add((IIntegratorService) this.CreateExtendedSaveService());
    this.Services.Add((IIntegratorService) this.CreateEmbedAttributesService());
    this.Services.Add((IIntegratorService) this.CreateLaunchActionServiceInternal());
    this.Services.Add((IIntegratorService) this.CreateStandardPartLibraryService());
    this.Services.Add((IIntegratorService) this.CreateModelDrawingsService());
    this.Services.Add((IIntegratorService) this.CreateStandaloneViewService());
    this.Services.Add((IIntegratorService) this.CreateAuthenticFilesServiceInternal());
    this.Services.Add((IIntegratorService) this.CreateApplicationLauncherService());
    this.Services.Add((IIntegratorService) this.CreateFileTreeService());
    this.Services.Add((IIntegratorService) this.CreateOpenFilesService());
    this.Services.Add((IIntegratorService) this.CreatePrepareNewObjectsServiceInternal());
  }

  /// <summary>
  /// Создает фабрику объектов, реализующих подсистему настройки интегратора.
  /// </summary>
  /// <returns>Фабрика объектов для подсистемы настройки интегратора</returns>
  protected virtual ICADSettingsFactory CreateSettingsFactory()
  {
    return (ICADSettingsFactory) new DefaultCADSettingsFactory(this);
  }

  protected abstract IApplicationFileTypes CreateFileTypeService();

  private CADLaunchActionService CreateLaunchActionServiceInternal()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    ICADInterfaceService service2 = ServiceUtils.GetService<ICADInterfaceService>((object) this, true);
    CADLaunchActionService launchActionService = this.CreateLaunchActionService();
    if (launchActionService.FileTypeService == null)
      launchActionService.FileTypeService = service1;
    if (launchActionService.FileVault == null)
      launchActionService.FileVault = ClientContext.FileVault;
    if (launchActionService.ApiService == null)
      launchActionService.ApiService = service2;
    return launchActionService;
  }

  protected virtual CADLaunchActionService CreateLaunchActionService()
  {
    return new CADLaunchActionService((IIntegrator) this);
  }

  /// <summary>
  /// Создает фабрику объектов, общих для команд импорта файлов, сохранения изменений, расширенного сохранения.
  /// </summary>
  /// <returns>Фабрика объектов</returns>
  protected virtual CADCaptureChangesFactory CreateCaptureChangesFactory()
  {
    return new CADCaptureChangesFactory((IIntegrator) this);
  }

  private CADFileImportSupportService CreateFileImportService()
  {
    CADCaptureChangesFactory service1 = ServiceUtils.GetService<CADCaptureChangesFactory>((object) this, true);
    IApplicationFileTypes service2 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    CADFileImportSupportService fileImportService = this.CreateFileImportService(service1);
    if (fileImportService.FileTypeService == null)
      fileImportService.FileTypeService = service2;
    return fileImportService;
  }

  protected virtual CADFileImportSupportService CreateFileImportService(
    CADCaptureChangesFactory factory)
  {
    return factory != null ? new CADFileImportSupportService((IIntegrator) this, factory) : throw new ArgumentNullException(nameof (factory));
  }

  private CADCaptureChangesService CreateCaptureChangesService()
  {
    return this.CreateCaptureChangesService(ServiceUtils.GetService<CADCaptureChangesFactory>((object) this, true));
  }

  protected virtual CADCaptureChangesService CreateCaptureChangesService(
    CADCaptureChangesFactory factory)
  {
    return factory != null ? new CADCaptureChangesService((IIntegrator) this, factory) : throw new ArgumentNullException(nameof (factory));
  }

  private CADExtendedSaveService CreateExtendedSaveService()
  {
    CADCaptureChangesFactory service1 = ServiceUtils.GetService<CADCaptureChangesFactory>((object) this, true);
    ICADSettingsService service2 = ServiceUtils.GetService<ICADSettingsService>((object) this, true);
    CADExtendedSaveService extendedSaveService = this.CreateExtendedSaveService(service1);
    extendedSaveService.SettingsService = service2;
    return extendedSaveService;
  }

  protected virtual CADExtendedSaveService CreateExtendedSaveService(
    CADCaptureChangesFactory factory)
  {
    return factory != null ? new CADExtendedSaveService((IIntegrator) this, factory) : throw new ArgumentNullException(nameof (factory));
  }

  protected virtual CADEmbedAttributesService CreateEmbedAttributesService()
  {
    return new CADEmbedAttributesService((IIntegrator) this);
  }

  private ICADInterfaceService CreateCADInterfaceServiceInternal()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    ICADSettingsService service2 = ServiceUtils.GetService<ICADSettingsService>((object) this, true);
    CADInterfaceService interfaceService = this.CreateCADInterfaceService();
    if (interfaceService.FileTypeService == null)
      interfaceService.FileTypeService = service1;
    if (interfaceService.SettingsService == null)
      interfaceService.SettingsService = service2;
    return (ICADInterfaceService) interfaceService;
  }

  protected abstract CADInterfaceService CreateCADInterfaceService();

  protected abstract IStandardPartLibraryService CreateStandardPartLibraryService();

  protected abstract IModelDrawingsService CreateModelDrawingsService();

  protected virtual IStandaloneViewService CreateStandaloneViewService()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    IDocumentApiService service2 = ServiceUtils.GetService<IDocumentApiService>((object) this, true);
    StandaloneViewService standaloneViewService = new StandaloneViewService((IIntegrator) this);
    standaloneViewService.FileTypeService = service1;
    standaloneViewService.DocumentApiService = service2;
    return (IStandaloneViewService) standaloneViewService;
  }

  private CADAuthenticFilesService CreateAuthenticFilesServiceInternal()
  {
    ICADSettingsService service1 = ServiceUtils.GetService<ICADSettingsService>((object) this, true);
    ICADInterfaceService service2 = ServiceUtils.GetService<ICADInterfaceService>((object) this, true);
    CADAuthenticFilesService authenticFilesService = this.CreateAuthenticFilesService();
    if (authenticFilesService.SettingsService == null)
      authenticFilesService.SettingsService = service1;
    if (authenticFilesService.ApiService == null)
      authenticFilesService.ApiService = service2;
    return authenticFilesService;
  }

  protected virtual CADAuthenticFilesService CreateAuthenticFilesService()
  {
    return new CADAuthenticFilesService((IIntegrator) this);
  }

  private CADApplicationLauncherService CreateApplicationLauncherService()
  {
    IApplicationApiService service1 = ServiceUtils.GetService<IApplicationApiService>((object) this, true);
    ICADSettingsService service2 = ServiceUtils.GetService<ICADSettingsService>((object) this, true);
    CADApplicationLauncherService applicationLauncherService = new CADApplicationLauncherService((IIntegrator) this);
    applicationLauncherService.ApiService = service1;
    applicationLauncherService.SettingsService = (IIntegratorSettingsService) service2;
    return applicationLauncherService;
  }

  private CADFileTreeService CreateFileTreeService()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    ICADInterfaceService service2 = ServiceUtils.GetService<ICADInterfaceService>((object) this, true);
    return new CADFileTreeService((IIntegrator) this)
    {
      FileTypeService = service1,
      FileVault = ClientContext.FileVault,
      ApiService = service2
    };
  }

  private CADOpenFiles CreateOpenFilesService()
  {
    IApplicationFileTypes service1 = ServiceUtils.GetService<IApplicationFileTypes>((object) this, true);
    ICADInterfaceService service2 = ServiceUtils.GetService<ICADInterfaceService>((object) this, true);
    return new CADOpenFiles((IIntegrator) this)
    {
      FileTypeService = service1,
      ApiService = service2
    };
  }

  protected virtual bool IsAttributesSharedStorage() => true;

  /// <summary>Возвращает сервис настроек интегратора.</summary>
  /// <returns>Сервис настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Сервис не реализован в интеграторе</exception>
  protected override IPersistentIntegratorSettingsService GetSettingsService()
  {
    return ServiceUtils.GetService<IPersistentIntegratorSettingsService>((object) this, true);
  }

  /// <summary>
  /// Возвращает необязательный сервис моделей представления для настроек интегратора.
  /// </summary>
  /// <returns>Сервис моделей представления или null</returns>
  protected override IIntegratorSettingsViewModelService TryGetSettingsViewModelService()
  {
    return ServiceUtils.GetService<IIntegratorSettingsViewModelService>((object) this, true);
  }

  /// <summary>Возвращает сервис для интеграции с PDM-браузером.</summary>
  /// <returns>Сервис интеграции с PDM-браузером</returns>
  private IPDMBrowserService CreatePDMBrowserService()
  {
    ICADSettingsService service = ServiceUtils.GetService<ICADSettingsService>((object) this, true);
    PDMBrowserService pdmBrowserService = this.CreatePDMBrowserService(this.GetPDMBrowserGuid());
    pdmBrowserService.SettingsService = service;
    return (IPDMBrowserService) pdmBrowserService;
  }

  /// <summary>
  /// Возвращает идентификатор CAD-системы для интеграции с PDM-браузером.
  /// </summary>
  /// <returns>Идентификатор CAD-системы</returns>
  protected abstract Guid GetPDMBrowserGuid();

  /// <summary>Возвращает сервис для интеграции с PDM-браузером.</summary>
  /// <param name="cadSystemId">Идентификатор CAD-системы</param>
  /// <returns>Сервис интеграции с PDM-браузером</returns>
  protected virtual PDMBrowserService CreatePDMBrowserService(Guid cadSystemId)
  {
    return new PDMBrowserService((IIntegrator) this, cadSystemId);
  }

  private CADPrepareNewObjectsService CreatePrepareNewObjectsServiceInternal()
  {
    CADPrepareNewObjectsService newObjectsService = this.CreatePrepareNewObjectsService();
    newObjectsService.FileVault = ClientContext.FileVault;
    return newObjectsService;
  }

  /// <summary>
  /// Возвращает сервис для подготовки новых объектов, создаваемых внутри IPS.
  /// </summary>
  /// <returns>Сервис подготовки новых объектов</returns>
  protected virtual CADPrepareNewObjectsService CreatePrepareNewObjectsService()
  {
    return new CADPrepareNewObjectsService((IIntegrator) this);
  }
}
