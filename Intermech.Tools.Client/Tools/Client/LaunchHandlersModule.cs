// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.LaunchHandlersModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Tools.LaunchActions;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class LaunchHandlersModule : InitializerModule
{
  private readonly ILaunchActionService launchActionService;
  private readonly ArticleDocumentationLaunchHandler articleDocumentationLaunchHandler;
  private readonly AuthenticFilesLaunchHandler authenticFilesLaunchHandler;

  public LaunchHandlersModule(
    ILaunchActionService launchActionService,
    ArticleDocumentationLaunchHandler articleDocumentationLaunchHandler,
    AuthenticFilesLaunchHandler authenticFilesLaunchHandler)
  {
    this.launchActionService = launchActionService;
    this.articleDocumentationLaunchHandler = articleDocumentationLaunchHandler;
    this.authenticFilesLaunchHandler = authenticFilesLaunchHandler;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.launchActionService.RegisterHandler((ILaunchHandler) this.articleDocumentationLaunchHandler);
    this.launchActionService.RegisterHandler((ILaunchHandler) this.authenticFilesLaunchHandler);
  }

  protected override void DoShutdown()
  {
    this.launchActionService.UnregisterHandler((ILaunchHandler) this.articleDocumentationLaunchHandler);
    this.launchActionService.UnregisterHandler((ILaunchHandler) this.authenticFilesLaunchHandler);
    base.DoShutdown();
  }
}
