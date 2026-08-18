// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IExternalApiService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать сервис интегратора, предоставляющий доступ к API интегрируемого приложения.
/// </summary>
public interface IExternalApiService : IIntegratorService
{
  /// <summary>
  /// Открывает сессию доступа к API интегрируемого приложения и конфигурирует приложение для работы в паре с IPS,.
  /// Доступ к API будет предоставлен только для того потока, который вызвал этот метод.
  /// Допускается использование вложенных сессий из одного и того же потока.
  /// </summary>
  /// <exception cref="T:Intermech.Tools.Integrators.IntegratorNotInstalledException">Интеграция с приложением не настроена</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.BadIntegratorSettingsException">Настройки интегратора содержат ошибки, препятствующие его использованию</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.AppNotInstalledException">Не удалось найти приложение на компьютере</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.BadAppSettingsException">Не удалось настроить приложение на работу в паре с IPS</exception>
  void OpenApiSession();

  /// <summary>
  /// Закрывает сессию доступа к API интегрируемого приложения. Если сессия не была открыта, то метод не имеет эффекта, но и не сбрасывает ошибок.
  /// </summary>
  void CloseApiSession();

  /// <summary>
  /// Возвращает true, если для текущего потока была открыта сессия доступа к API интегрируемого приложения.
  /// </summary>
  bool IsApiSessionOpen { get; }

  /// <summary>
  /// Проверяет, есть ли для текущего потока открытая сессия доступа к API интегрируемого приложения. Если это не так, то метод сбрасывает исключение.
  /// </summary>
  /// <exception cref="T:System.InvalidOperationException">Требуется предварительное открытие сессии к API интегрируемого приложения</exception>
  void CheckApiSessionOpen();
}
