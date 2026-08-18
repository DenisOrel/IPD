// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.IMViewerExtensionModule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Класс для подключения поддержки IMViewer к интегратору.
/// </summary>
public class IMViewerExtensionModule
{
  private readonly IIMViewerClientService imviewerService;

  /// <summary>Создает объект.</summary>
  /// <param name="imviewerService">Клиентский сервис интеграции с IMViewer</param>
  public IMViewerExtensionModule(IIMViewerClientService imviewerService)
  {
    this.imviewerService = imviewerService != null ? imviewerService : throw new ArgumentNullException(nameof (imviewerService));
  }

  /// <summary>Подключает поддержку IMViewer к интегратору.</summary>
  /// <param name="integrator">Объект интегратора</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="integrator" /> содержит null</exception>
  public void AttachTo(IIntegrator integrator)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (!this.imviewerService.Settings.EnableIntegration)
      return;
    ServiceUtils.GetService<CADCaptureChangesFactory>((object) integrator, true).EnableIMViewerExtension = true;
    ServiceUtils.GetService<CADEmbedAttributesService>((object) integrator, true).EnableIMViewerExtension = true;
  }
}
