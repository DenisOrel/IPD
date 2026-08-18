
// Type: Intermech.Tools.Integrators.IntegratorModule`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Tools.LaunchActions;
using System;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует модуль, обеспечивающий создание, инициализацию и регистрацию интегратора в системе.
/// </summary>
/// <typeparam name="TIntegrator">Тип интегратора</typeparam>
public class IntegratorModule<TIntegrator> : InitializerModule where TIntegrator : class, IIntegrator
{
  private Func<TIntegrator> integratorFactory;
  private IIntegratorRegistry integratorRegistry;
  private ILaunchActionService launchActionService;
  private IOpenFilesService openFilesService;
  private bool enableLaunchHandler;
  private string applicationName;
  private TIntegrator integrator;
  private IOpenFilesServiceExtension openFilesHandler;
  private IntegratorLaunchActionHandler launchActionHandler;

  /// <summary>Создает объект.</summary>
  public IntegratorModule()
    : this(new Func<TIntegrator>(Activator.CreateInstance<TIntegrator>))
  {
  }

  /// <summary>Создает объект.</summary>
  /// <param name="integratorFactory">Фабрика интеграторов</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="integratorFactory" /> не должен быть равен null</exception>
  public IntegratorModule(Func<TIntegrator> integratorFactory)
  {
    this.integratorFactory = integratorFactory != null ? integratorFactory : throw new ArgumentNullException(nameof (integratorFactory));
    this.integratorRegistry = ServiceUtils.GetService<IIntegratorRegistry>((object) ApplicationServices.Container, true);
    this.launchActionService = ServiceUtils.GetService<ILaunchActionService>((object) ApplicationServices.Container, true);
    this.openFilesService = ServiceUtils.GetService<IOpenFilesService>((object) ApplicationServices.Container, true);
  }

  /// <summary>
  /// Разрешает создание обработчика для команд открытия документа, использующих интегратор для взаимодействия с приложением.
  /// </summary>
  /// <param name="applicationName">Имя приложения</param>
  public void EnableLaunchHandler(string applicationName)
  {
    if (applicationName == null)
      throw new ArgumentNullException(nameof (applicationName));
    this.enableLaunchHandler = true;
    this.applicationName = applicationName;
  }

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.integrator = this.integratorFactory();
    this.integrator.Initialize();
    this.integratorRegistry.RegisterIntegrator((IIntegrator) this.integrator);
    this.openFilesHandler = ServiceUtils.GetService<IOpenFilesServiceExtension>((object) this.integrator, false);
    if (this.openFilesHandler != null)
      this.openFilesService.RegisterExtension(this.openFilesHandler);
    if (!this.enableLaunchHandler)
      return;
    this.launchActionHandler = new IntegratorLaunchActionHandler((IIntegrator) this.integrator, this.applicationName);
    this.launchActionService.RegisterHandler((ILaunchHandler) this.launchActionHandler);
  }

  /// <summary>
  /// Завершает работу объектов и сервисов, предоставленных модулем.
  /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
  /// </summary>
  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.launchActionHandler != null)
    {
      this.launchActionService.UnregisterHandler((ILaunchHandler) this.launchActionHandler);
      this.launchActionHandler = (IntegratorLaunchActionHandler) null;
    }
    if (this.openFilesHandler != null)
    {
      this.openFilesService.UnregisterExtension(this.openFilesHandler);
      this.openFilesHandler = (IOpenFilesServiceExtension) null;
    }
    if ((object) this.integrator == null)
      return;
    this.integratorRegistry.UnregisterIntgerator((IIntegrator) this.integrator);
    this.integrator = default (TIntegrator);
  }

  /// <summary>
  /// Возвращает экземпляр интегратора. Значение свойства доступно после инициализации модуля.
  /// </summary>
  public TIntegrator Integrator => this.integrator;
}
