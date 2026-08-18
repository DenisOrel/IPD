
// Type: Intermech.Tools.Integrators.ReadOnlyIntegratorSettingsService`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Memoization;
using Intermech.Tools.Settings;


namespace Intermech.Tools.Integrators;

public abstract class ReadOnlyIntegratorSettingsService<TSettings> : 
  IntegratorService,
  IIntegratorSettingsService,
  IIntegratorService
  where TSettings : class, ISettingsObject
{
  private TSettings settingsObject;
  private readonly IStateMonitor stateMonitor;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Объект интегратора с приложением</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  public ReadOnlyIntegratorSettingsService(IIntegrator owner)
    : base(owner)
  {
    this.stateMonitor = (IStateMonitor) new ConstantStateMonitor();
  }

  /// <summary>
  /// Возвращает объект настроек интегратора.
  /// При первом вызове этого метода выполняется кэширование настроек интегратора. Кэш настроек автоматически сбрасывается при их изменении в базе IPS.
  /// </summary>
  /// <returns>Объект настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Объект с настройками еще не создан в базе IPS, либо содержит ошибки</exception>
  public TSettings GetSettings()
  {
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      if ((object) this.settingsObject == null)
        this.settingsObject = this.CreateSettings();
      return this.settingsObject;
    }
  }

  /// <summary>
  /// Возвращает объект настроек интегратора.
  /// При первом вызове этого метода выполняется кэширование настроек интегратора. Кэш настроек автоматически сбрасывается при их изменении в базе IPS.
  /// </summary>
  /// <returns>Объект настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Объект с настройками еще не создан в базе IPS, либо содержит ошибки</exception>
  ISettingsObject IIntegratorSettingsService.GetSettingsObject()
  {
    return (ISettingsObject) this.GetSettings();
  }

  /// <summary>
  /// Возвращает монитор состояния для настроек интегратора. С его помощью можно определить момент переполучения сервисом настроек с сервера приложений IPS.
  /// </summary>
  /// <returns>Монитор состояния для настроек интегратора</returns>
  public IStateMonitor GetSettingsStateMonitor()
  {
    this.RequireReadyState();
    return this.stateMonitor;
  }

  protected abstract TSettings CreateSettings();
}
