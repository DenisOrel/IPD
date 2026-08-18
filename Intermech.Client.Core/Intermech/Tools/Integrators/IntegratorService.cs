
// Type: Intermech.Tools.Integrators.IntegratorService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Runtime;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Предоставляет базу для реализации компонентов и сервисов интеграторов с приложениями. Сервисы отличаются
/// от компонентов поддержкой поточной безопасностьи (thread-safe), а также существованием только
/// одного экземпляра каждого сервиса в составе интегратора.
/// </summary>
public class IntegratorService : IIntegratorService
{
  private readonly IIntegrator integrator;
  private bool isInitialized;
  private bool isReady;
  private IIntegratorOutput outputService;
  private IIntegratorLicense licenseService;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  public IntegratorService(IIntegrator owner)
  {
    this.integrator = owner != null ? owner : throw new ArgumentNullException(nameof (owner));
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  public void Initialize()
  {
    lock (this.Integrator.SyncRoot)
    {
      if (this.isInitialized)
        return;
      this.DoInitialize();
      this.isInitialized = true;
      this.DoAfterInitialize();
    }
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected virtual void DoInitialize()
  {
    if (this.OutputService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "OutputService");
    if (this.LicenseService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "LicenseService");
  }

  /// <summary>
  /// Обработчик события, который вызывается сразу после успешной инициализации сервиса.
  /// Может использоваться для выполнения действий, требующих предварительной полной инициализации сервиса.
  /// </summary>
  protected virtual void DoAfterInitialize()
  {
  }

  /// <summary>
  /// Позволяет убедиться, что сервис интегратора еще не был инициализирован.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Метод инициализации сервиса уже был вызван</exception>
  protected void RequireNotInitialized()
  {
    if (this.isInitialized)
      throw new InvalidOperationException($"Сервис интегратора типа '{this.GetType()}' уже был инициализирован с помощью метода Initialize().");
  }

  /// <summary>
  /// Позволяет убедиться, что сервис интегратора был инициализирован.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Метод инициализации сервиса не был вызван</exception>
  protected void RequireInitialized()
  {
    if (!this.isInitialized)
      throw new InvalidOperationException($"Сервис интегратора типа '{this.GetType()}' не был инициализирован. Воспользуйтесь методом Initialize().");
  }

  /// <summary>
  /// Возвращает объект, являющийся владельцем компонента или сервиса.
  /// </summary>
  public IIntegrator Integrator
  {
    [DebuggerStepThrough] get => this.integrator;
  }

  /// <summary>
  /// Возвращает состояние инициализации сервиса интегратора.
  /// </summary>
  public bool IsInitialized
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.isInitialized;
    }
  }

  /// <summary>
  /// Позволяет убедиться, что сервис интегратора может использоваться.
  /// Реализация метода является thread-safe.
  /// </summary>
  protected void RequireReadyState()
  {
    if (this.isReady)
      return;
    lock (this.Integrator.SyncRoot)
    {
      if (this.isReady)
        return;
      this.RequireInitialized();
      this.isReady = true;
    }
  }

  /// <summary>
  /// Возвращает или задает сервис вывода сообщений в окно "Вывод". Свойство заполняется самим интегратором перед инициализацией сервиса.
  /// </summary>
  public IIntegratorOutput OutputService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.outputService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.outputService = value;
      }
    }
  }

  /// <summary>
  /// Возвращает или задает сервис выделения и проверки лицензии для интегратора с приложением. Свойство заполняется самим интегратором перед инициализацией сервиса.
  /// </summary>
  public IIntegratorLicense LicenseService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.licenseService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.licenseService = value;
      }
    }
  }
}
