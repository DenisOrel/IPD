// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MainPackage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Localization;
using Intermech.Tools.Client.AuthenticFiles;
using Intermech.Tools.Client.Commands;
using Intermech.Tools.Client.CompositionCopying;
using Intermech.Tools.Client.IMViewer;
using Intermech.Tools.Client.MultiCAD;
using Intermech.Tools.Client.Requirement;
using Intermech.Tools.Components;
using Intermech.Tools.PDMTree;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class MainPackage : IOCBasedPackage
{
  private ISharedLibraryInitializerService sharedLibraryInitializer;
  private AssemblyInitializerModule toolsComponentsInitializer;

  public MainPackage(
    IOCBasedPackageParameters createParameters,
    ISharedLibraryInitializerService sharedLibraryInitializer)
    : base(createParameters, LocalizationHolder.rm.GetString("Tools.Client_86"))
  {
    this.sharedLibraryInitializer = sharedLibraryInitializer != null ? sharedLibraryInitializer : throw new ArgumentNullException(nameof (sharedLibraryInitializer));
  }

  protected override void DoInitializeIOCContainer()
  {
    base.DoInitializeIOCContainer();
    this.TryUpdateApplicationIOCContainer();
    this.IOCContainer.Load((INinjectModule) new CommandsNinjectModule());
    this.IOCContainer.Load((INinjectModule) new PDMSystemNinjectModule());
    this.IOCContainer.Load((INinjectModule) new JTCommandsNinjectModule());
    this.IOCContainer.Load((INinjectModule) new IMViewerNinjectModule());
    this.IOCContainer.Load((INinjectModule) new CADDocumentCopyingNinjectModule());
  }

  private void TryUpdateApplicationIOCContainer()
  {
    NinjectModule[] m = new NinjectModule[1]
    {
      (NinjectModule) new AssemblyNinjectModule()
    };
    this.GlobalIOCContainer.Load((IEnumerable<INinjectModule>) m);
    try
    {
      this.toolsComponentsInitializer = this.sharedLibraryInitializer.InitializerModuleFactory.Create<AssemblyInitializerModule>();
      this.toolsComponentsInitializer.Initialize();
    }
    catch
    {
      foreach (NinjectModule ninjectModule in m)
        this.GlobalIOCContainer.Unload(ninjectModule.Name);
      this.toolsComponentsInitializer = (AssemblyInitializerModule) null;
      throw;
    }
  }

  protected override void CreateSubModules(LazyInitializerModuleGroup subModules)
  {
    base.CreateSubModules(subModules);
    subModules.Add<ExceptionHandlersModule>();
    subModules.Add<CommandsModule>();
    subModules.Add<ArticlePreviewModule>();
    subModules.Add<FileImportModule>();
    subModules.Add<CadmechServicesModule>();
    subModules.Add<ExtAppModule>();
    subModules.Add<ShellVerbModule>();
    subModules.Add<LaunchHandlersModule>();
    subModules.Add<PDMSystemModule>();
    subModules.Add<JTCommandsModule>();
    subModules.Add<AuthenticFilesInitializerModule>();
    subModules.Add<Intermech.Tools.Client.DraftDocuments.Module>();
    subModules.Add<FileCommandsModule>();
    subModules.Add<SetupWindowModule>();
    subModules.Add<PairedLaunchCommandsModule>();
    subModules.Add<Intermech.Tools.StandardParts.Module>();
    subModules.Add<ToolsControlPanelModule>();
    subModules.Add<Intermech.Tools.EnterpriseArchive.Module>();
    subModules.Add<Intermech.Tools.Client.Subsystems.Import_from_Excel.Module>();
    subModules.Add<Intermech.Tools.Client.IntegratorsContextMenu.MenuModule>();
    subModules.Add<Intermech.Tools.Client.IntegrationErrors.MenuModule>();
    subModules.Add<Intermech.Tools.Client.WeldingJoints.MenuModule>();
    subModules.Add<RequirementModule>();
    subModules.Add<Intermech.Tools.Client.IMViewer.MenuModule>();
    subModules.Add<SystemSettingsInitializerModule>();
    subModules.Add<ModelFileReplaceModule>();
    subModules.Add<CADDocumentCopyingInitializerModule>();
  }
}
