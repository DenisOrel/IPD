// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IIntegratorSettingsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Memoization;
using Intermech.Tools.Settings;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис интегратора, который позволяет получать настройки интегратора, а также наблюдать за их изменением.
/// </summary>
public interface IIntegratorSettingsService : IIntegratorService
{
  /// <summary>
  /// Возвращает объект настроек интегратора.
  /// При первом вызове этого метода выполняется кэширование настроек интегратора. Кэш настроек автоматически сбрасывается при их изменении в базе IPS.
  /// </summary>
  /// <returns>Объект настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Объект с настройками еще не создан в базе IPS, либо содержит ошибки</exception>
  ISettingsObject GetSettingsObject();

  /// <summary>
  /// Возвращает монитор состояния для настроек интегратора. С его помощью можно определить момент переполучения сервисом настроек с сервера приложений IPS.
  /// </summary>
  /// <returns>Монитор состояния для настроек интегратора</returns>
  IStateMonitor GetSettingsStateMonitor();
}
