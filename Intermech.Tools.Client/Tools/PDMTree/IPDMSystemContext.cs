// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.IPDMSystemContext
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Plugins;
using Intermech.Services.IMViewer;
using Intermech.Tools.Client.Commands;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;
using Ninject;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal interface IPDMSystemContext
{
  BarManager BarManager { get; }

  ICurrentUserAndRole CurrentUserAndRoleService { get; }

  INotificationService NotificationService { get; }

  IMainFormUpdate MainFormService { get; }

  IObjectCreatorService ObjectCreatorService { get; }

  ILaunchActionService LaunchActionService { get; }

  IIntegratorRegistry IntegratorRegistry { get; }

  IFileAttributeEditorService FileAttributeEditorService { get; }

  IFileImportService FileImportService { get; }

  IFileVault FileVaultService { get; }

  ExtendedSaveHelper ExtendedSaveHelper { get; }

  IArticleService ArticleService { get; }

  IPluginManager PluginManager { get; }

  IExceptionHandlerService ExceptionService { get; }

  IUINotificationService UINotificationService { get; }

  [Named("StandardLibraryLog")]
  IEventLogWriter StandardLibraryLog { get; }

  IIMViewerObjectCreatorService IMViewerClientService { get; }
}
