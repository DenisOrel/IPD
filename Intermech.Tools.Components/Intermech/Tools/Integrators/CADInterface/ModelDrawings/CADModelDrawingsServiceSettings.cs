// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelDrawings.CADModelDrawingsServiceSettings
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface.ModelDrawings;

/// <summary>
/// Стандартная реализация провайдера, который предоставляет доступ к настройкам интегратора, необходимым для работы сервиса IModelDrawingsService.
/// </summary>
/// <remarks>Реализация не является является thread safe.</remarks>
public sealed class CADModelDrawingsServiceSettings : IModelDrawingsServiceSettings
{
  private readonly IIntegrator integrator;
  private readonly ICADSettingsService settingsSvc;
  private IntegratorSettingsCache<ICollection<string>> drawingSuffixesCache;

  /// <summary>Создает объект.</summary>
  /// <param name="integrator">Объект интегратора</param>
  /// <param name="settingsService">Сервис настроек интегратора</param>
  public CADModelDrawingsServiceSettings(
    IIntegrator integrator,
    ICADSettingsService settingsService)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (settingsService == null)
      throw new ArgumentNullException(nameof (settingsService));
    this.integrator = integrator;
    this.settingsSvc = settingsService;
    this.drawingSuffixesCache = new IntegratorSettingsCache<ICollection<string>>((IIntegratorSettingsService) this.settingsSvc, new Func<ICollection<string>>(this.GetDrawingSuffixesSlow));
  }

  /// <summary>
  /// Возвращает коллекцию суффиксов, по которым можно опознать файлы чертежей.
  /// </summary>
  /// <returns>Коллекция суффиксов, по которым можно опознать файлы чертежей</returns>
  public ICollection<string> GetDrawingSuffixes() => this.drawingSuffixesCache.Value;

  private ICollection<string> GetDrawingSuffixesSlow()
  {
    return (ICollection<string>) this.settingsSvc.GetCADSettings().DrawingSuffixes;
  }
}
