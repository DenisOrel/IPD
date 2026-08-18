// Decompiled with JetBrains decompiler
// Type: Intermech.Services.IMViewer.IMViewerInitializerModule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ApplicationModel;
using Intermech.Tools.LaunchActions;
using System;

#nullable disable
namespace Intermech.Services.IMViewer;

/// <summary>
/// Модуль инициализации для клиентской части интеграции с IMViewer.
/// </summary>
internal sealed class IMViewerInitializerModule : InitializerModule
{
  private ILaunchActionService launchActionService;
  private IMViewerLaunchHandler imviewerLaunchHandler;

  public IMViewerInitializerModule(
    ILaunchActionService launchActionService,
    IMViewerLaunchHandler imviewerLaunchHandler)
  {
    if (launchActionService == null)
      throw new ArgumentNullException(nameof (launchActionService));
    if (imviewerLaunchHandler == null)
      throw new ArgumentNullException(nameof (imviewerLaunchHandler));
    this.launchActionService = launchActionService;
    this.imviewerLaunchHandler = imviewerLaunchHandler;
  }

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.launchActionService.RegisterHandler((ILaunchHandler) this.imviewerLaunchHandler);
  }

  /// <summary>
  /// Завершает работу объектов и сервисов, предоставленных модулем.
  /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
  /// </summary>
  protected override void DoShutdown()
  {
    this.launchActionService.UnregisterHandler((ILaunchHandler) this.imviewerLaunchHandler);
    base.DoShutdown();
  }
}
