
// Type: Intermech.Client.Core.AssemblyNinjectModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.CompositionCopying;
using Intermech.Client.Core.ThumbnailDocs;
using Intermech.Client.DBPatches;
using Intermech.Commands;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.ParamsStorage;
using Intermech.Interfaces.StandaloneView;
using Intermech.Navigator.Interfaces;
using Intermech.PdfPrintCenter.Connector;
using Intermech.Redline;
using Intermech.Services;
using Intermech.Tools.CommonTasks;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using System;


namespace Intermech.Client.Core;

/// <summary>
/// Модуль привязок для IOC-контейнера, обеспечивающий создание сервисов приложения.
/// </summary>
public sealed class AssemblyNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.LoadCoreServices();
    this.LoadCompositionCopying();
    this.LoadRecentObjectsFromNotify();
  }

  private void LoadCoreServices()
  {
    this.Bind<IPackedStream>().To<PackedStreamService>().InSingletonScope();
    this.Bind<IIconReader>().To<IconReader>().InSingletonScope();
    this.Bind<IParamsStorageService>().To<Intermech.Client.Core.ParamsStorageService.ParamsStorageService>().InSingletonScope();
    this.Bind<ISortService>().To<SortService>().InSingletonScope();
    this.Bind<ISaveToDiskService>().To<SaveToDiskService>().InSingletonScope();
    this.Bind<IAttributesLockService>().To<AttributesLockService>().InSingletonScope();
    this.Bind<IAuthFilesService>().To<AuthFilesService>().InSingletonScope();
    this.Bind<IProcessFileService>().To<ProcessFileService>().InSingletonScope();
    this.Bind<IStandaloneViewSettingsService>().To<StandaloneViewSettingsService>().InSingletonScope();
    this.Bind<IObjectCreatorService>().To<Intermech.Client.Core.ObjectCreator.ObjectCreator>().InSingletonScope();
    this.Bind<ISelectionsService>().To<ClientSelectionService>().InSingletonScope();
    this.Bind<IPreviewExtractService>().To<PreviewExtractService>().InSingletonScope();
    this.Bind<IWorkCopyCommandOptions>().To<WorkCopyCommandOptionsService>().InSingletonScope();
    this.Bind<IFileAttributeEditorService>().To<FileAttributeEditorService>().InSingletonScope();
    this.Bind<IFileVaultSettingsService>().To<FileVaultSettingsService>().InSingletonScope();
    this.Bind<IOpenFilesService>().To<OpenFilesService>().InSingletonScope();
    this.Bind<FileVaultServiceFactory>().ToSelf();
    this.Bind<IFileVault>().ToMethod<FileVaultService>((Func<IContext, FileVaultService>) (context => context.Kernel.Get<FileVaultServiceFactory>().Create())).InSingletonScope();
    this.Bind<IFileImportService>().To<FileImportService>().InSingletonScope();
    this.Bind<ILaunchActionService>().To<LaunchActionService>().InSingletonScope();
    this.Bind<IIntegratorRegistry>().To<IntegratorRegistry>().InSingletonScope();
    this.Bind<IExternalRedliningEditorService>().To<ExternalRedliningEditorService>().InSingletonScope();
    this.Bind<IntegratorSettingsCacheManager>().To<IntegratorSettingsCacheManager>().InSingletonScope();
    this.Bind<IPrepareForViewDocumentFilesService>().To<PrepareForViewDocumentFilesService>().InSingletonScope();
    this.Bind<DocumentFilesTaskFactory>().ToSelf().InSingletonScope();
    this.Bind<PatchRunner>().ToSelf();
    this.Bind<IDraftDocumentsService>().To<DraftDocumentsService>().InSingletonScope();
    this.Bind<IDraftDocumentsIdCache>().To<DraftDocumentsIdCache>().WhenInjectedInto<DraftDocumentsService>().InSingletonScope();
    this.Bind<SelectedItemsCommand>().To<PrintPDFCommand>().Named("PrintPDFCommand");
    this.Bind<IPrintPDFCommandFactory>().ToFactory<IPrintPDFCommandFactory>().InSingletonScope();
    this.Bind<IPdfPrintCenterService>().To<PdfPrintCenterService>().InSingletonScope();
    this.Bind<IPDMSystem>().To<PdfPrintCenterPDMSystem>().InSingletonScope();
    this.Bind<PdfPrintCenterLayoutIdCache>().ToSelf().InSingletonScope();
    this.Bind<IWorkAreaFileDeleteService>().To<WorkAreaFileDeleteServiceModule>().InSingletonScope();
  }

  private void LoadCompositionCopying()
  {
    this.Bind<ICompositionCopyingDispatcherService>().To<CompositionCopyingDispatcherService>().InSingletonScope();
    this.Bind<CompositionCopyingCommandsProvider>().ToSelf().WhenInjectedInto<CompositionCopyingInitializerModule>();
    this.Bind<CompositionCopyingInitializerModule>().ToSelf().InSingletonScope();
  }

  private void LoadRecentObjectsFromNotify()
  {
    this.Bind<RecentObjectsFromNotifyServiceModule>().ToSelf().InSingletonScope();
  }
}
