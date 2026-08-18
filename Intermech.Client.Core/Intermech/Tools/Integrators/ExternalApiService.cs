
// Type: Intermech.Tools.Integrators.ExternalApiService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Threading;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса интегратора, предоставляющего доступ к API интегрируемого приложения.
/// </summary>
public abstract class ExternalApiService : IntegratorService, IExternalApiService, IIntegratorService
{
  private int apiSessionLevel;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  public ExternalApiService(IIntegrator owner)
    : base(owner)
  {
  }

  /// <summary>
  /// Открывает сессию доступа к API интегрируемого приложения и конфигурирует приложение для работы в паре с IPS,.
  /// Доступ к API будет предоставлен только для того потока, который вызвал этот метод.
  /// Допускается использование вложенных сессий из одного и того же потока.
  /// </summary>
  /// <exception cref="T:Intermech.Tools.Integrators.IntegratorNotInstalledException">Интеграция с приложением не настроена</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.BadIntegratorSettingsException">Настройки интегратора содержат ошибки, препятствующие его использованию</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.AppNotInstalledException">Не удалось найти приложение на компьютере</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.BadAppSettingsException">Не удалось настроить приложение на работу в паре с IPS</exception>
  public void OpenApiSession()
  {
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      this.DoOpenApiSession(this.apiSessionLevel == 0);
      ++this.apiSessionLevel;
      Monitor.Enter(this.Integrator.SyncRoot);
    }
  }

  /// <summary>
  /// Закрывает сессию доступа к API интегрируемого приложения. Если сессия не была открыта, то метод не имеет эффекта, но и не сбрасывает ошибок.
  /// </summary>
  public void CloseApiSession()
  {
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      if (this.apiSessionLevel <= 0)
        return;
      Monitor.Exit(this.Integrator.SyncRoot);
      --this.apiSessionLevel;
      this.DoCloseApiSession(this.apiSessionLevel == 0);
    }
  }

  /// <summary>
  /// Проверяет, есть ли для текущего потока открытая сессия доступа к API интегрируемого приложения. Если это не так, то метод сбрасывает исключение.
  /// </summary>
  /// <exception cref="T:System.InvalidOperationException">Требуется предварительное открытие сессии к API интегрируемого приложения</exception>
  public void CheckApiSessionOpen()
  {
    this.RequireReadyState();
    if (!this.IsApiSessionOpen)
      throw new InvalidOperationException("Call method OpenSession() first.");
  }

  /// <summary>
  /// Возвращает true, если для текущего потока была открыта сессия доступа к API интегрируемого приложения.
  /// </summary>
  public bool IsApiSessionOpen
  {
    get
    {
      this.RequireReadyState();
      if (!Monitor.TryEnter(this.Integrator.SyncRoot, 1))
        return false;
      int num = this.apiSessionLevel > 0 ? 1 : 0;
      Monitor.Exit(this.Integrator.SyncRoot);
      return num != 0;
    }
  }

  /// <summary>
  /// Открывает сессию доступа к API интегрируемого приложения и конфигурирует приложение для работы в паре с IPS.
  /// </summary>
  /// <param name="topLevelSession">true - если это сессия верхнего уровня, false - если это вложенная сессия</param>
  /// <exception cref="T:Intermech.Tools.Integrators.IntegratorNotInstalledException">Интеграция с приложением не настроена</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.BadIntegratorSettingsException">Настройки интегратора содержат ошибки, препятствующие его использованию</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.AppNotInstalledException">Не удалось найти приложение на компьютере</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.BadAppSettingsException">Не удалось настроить приложение на работу в паре с IPS</exception>
  protected virtual void DoOpenApiSession(bool topLevelSession)
  {
  }

  /// <summary>
  /// Закрывает сессию доступа к API интегрируемого приложения.
  /// </summary>
  /// <param name="topLevelSession">true - если это сессия верхнего уровня, false - если это вложенная сессия</param>
  protected virtual void DoCloseApiSession(bool topLevelSession)
  {
  }
}
