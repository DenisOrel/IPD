
// Type: Intermech.Tools.Integrators.IntegratorBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для интеграторов с приложениями.
/// </summary>
public abstract class IntegratorBase : IIntegrator, IServiceProvider
{
  private readonly IntegratorServiceCollection services;
  private readonly object syncRoot;
  private bool isInitialized;

  /// <summary>Создает объект.</summary>
  public IntegratorBase()
  {
    this.services = new IntegratorServiceCollection();
    this.syncRoot = new object();
  }

  /// <summary>Инициализирует объект интегратора.</summary>
  public void Initialize()
  {
    if (this.isInitialized)
      return;
    lock (this.SyncRoot)
    {
      if (this.isInitialized)
        return;
      this.InitializeCore();
      this.services.LockChanges();
      this.isInitialized = true;
    }
  }

  private void InitializeCore()
  {
    this.DoCreateServices();
    CollectionUtils.RemoveAll<IIntegratorService>((IList<IIntegratorService>) this.services, (Predicate<IIntegratorService>) (item => item == null));
    this.DoConfigureServices();
    this.DoInitializeServices();
  }

  /// <summary>
  /// Создает сервисы интегратора, определяющие его возможности.
  /// </summary>
  protected virtual void DoCreateServices()
  {
    this.Services.Add((IIntegratorService) this.CreateOutputService());
    this.Services.Add((IIntegratorService) this.CreateLicenseService());
  }

  /// <summary>Создает сервис вывода сообщений в окно "Вывод".</summary>
  /// <returns>Экземпляр сервиса</returns>
  protected virtual IIntegratorOutput CreateOutputService()
  {
    IOutputView service = ServiceUtils.GetService<IOutputView>((object) ServicesManager.ServiceContainer, false);
    return (IIntegratorOutput) new IntegratorOutputService((IIntegrator) this)
    {
      OutputView = service
    };
  }

  /// <summary>
  /// Создает сервис для работы с лицензией на интегратор с приложением.
  /// </summary>
  /// <returns>Экземпляр сервиса</returns>
  protected virtual IIntegratorLicense CreateLicenseService()
  {
    return (IIntegratorLicense) new EmptyLicenseService((IIntegrator) this);
  }

  /// <summary>
  /// Выполняет конфигурирование сервисов интегратора перед их инициализацией. Как правило,
  /// этот метод используется для установления тех связей между сервисами,
  /// которые невозможно заполнить в методе <see cref="M:DoCreateServices" />.
  /// </summary>
  protected virtual void DoConfigureServices()
  {
    IIntegratorOutput service1 = ServiceUtils.GetService<IIntegratorOutput>((object) this, true);
    IIntegratorLicense service2 = ServiceUtils.GetService<IIntegratorLicense>((object) this, true);
    foreach (IIntegratorService service3 in (Collection<IIntegratorService>) this.services)
    {
      if (service3 is IntegratorService integratorService)
      {
        if (integratorService.OutputService == null)
          integratorService.OutputService = service1;
        if (integratorService.LicenseService == null)
          integratorService.LicenseService = service2;
      }
    }
  }

  /// <summary>Выполняет инициализацию сервисов интегратора.</summary>
  protected virtual void DoInitializeServices()
  {
    foreach (IIntegratorService service in (Collection<IIntegratorService>) this.services)
      service.Initialize();
  }

  /// <summary>
  /// Возвращает глобальный идентификатор объекта интегратора в базе IPS.
  /// </summary>
  public abstract Guid Id { get; }

  /// <summary>Возвращает название интегратора.</summary>
  public abstract string DisplayName { get; }

  /// <summary>
  /// Возвращает объект для обеспечения сервисами интегратора поточной безопасности (thread-safe).
  /// </summary>
  public object SyncRoot
  {
    [DebuggerStepThrough] get => this.syncRoot;
  }

  /// <summary>
  /// Возвращает шаблон для серверного объекта интегратора в форме xml-документа.
  /// Он используется при создании нового объекта интегратора в базе IPS.
  /// </summary>
  /// <returns>Шаблон для серверного объекта интегратора в форме xml-документа</returns>
  public abstract string GetServerObjectTemplate();

  /// <summary>
  /// Возвращает пустой шаблон для серверного объекта интегратора в форме xml-документа.
  /// </summary>
  /// <returns>Шаблон для серверного объекта интегратора в форме xml-документа</returns>
  protected string GetEmptyServerObjectTemplate()
  {
    return new EmptySettingsCodec(this.DisplayName).Encode((ISettingsObject) new EmptySettingsCodec.EmptySettings()).OuterXml;
  }

  /// <summary>
  /// Читает из указанного ресурса шаблон для серверного объекта интегратора в форме xml-документа.
  /// Он используется при создании нового объекта интегратора в базе IPS.
  /// </summary>
  /// <param name="streamName">Имя ресурса</param>
  /// <returns>Шаблон для серверного объекта интегратора в форме xml-документа</returns>
  /// <exception cref="T:System.ArgumentNullException">streamName</exception>
  protected string GetServerObjectTemplateFromResource(string streamName)
  {
    if (streamName == null)
      throw new ArgumentNullException(nameof (streamName));
    lock (this)
    {
      Assembly assembly = this.GetType().Assembly;
      using (Stream manifestResourceStream = assembly.GetManifestResourceStream(streamName))
      {
        if (manifestResourceStream == null)
          throw new Exception($"Не удалось найти ресурс '{streamName}' в сборке {assembly}.");
        using (StreamReader streamReader = new StreamReader(manifestResourceStream, true))
          return streamReader.ReadToEnd();
      }
    }
  }

  /// <summary>
  /// Создает и возвращает визуальный редактор настроек интегратора.
  /// </summary>
  /// <returns>Элемент управления</returns>
  public abstract DataEditorControl CreateSettingsEditor();

  /// <summary>
  /// Возвращает изображение для иконки приложения, с которым осуществляется интеграция.
  /// Метод может вернуть null, если изображения запрошенного размера нет.
  /// </summary>
  /// <param name="imageSize">Размер изображения</param>
  /// <returns>Изображение иконки приложения или null</returns>
  public virtual Image GetApplicationImage(AppImageSize imageSize) => (Image) null;

  /// <summary>Возвращает указанный сервис интегратора.</summary>
  /// <param name="serviceType">Тип сервиса</param>
  /// <returns>Найденный сервис интегратора или null, если сервис не поддерживается интегратором</returns>
  /// <exception cref="T:System.ArgumentNullException">serviceType</exception>
  public object GetService(Type serviceType)
  {
    if (serviceType == (Type) null)
      throw new ArgumentNullException(nameof (serviceType));
    lock (this.SyncRoot)
      return this.services.TryGetService(serviceType);
  }

  /// <summary>
  /// Возвращает коллекцию сервисов интегратора. Изменять содержимое коллекции можно до момента инициализации интегратора.
  /// После инициализации коллекция становиться неизменяемой.
  /// </summary>
  protected ICollection<IIntegratorService> Services
  {
    [DebuggerStepThrough] get => (ICollection<IIntegratorService>) this.services;
  }
}
