
// Type: Intermech.Tools.Integrators.IntegratorLaunchActionHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Tools.LaunchActions;
using System;
using System.Xml;


namespace Intermech.Tools.Integrators;

public sealed class IntegratorLaunchActionHandler : 
  ParameterlessLaunchHandler,
  ILaunchHandlerFileEvents
{
  private readonly IIntegrator integrator;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="integrator">Интегратор с CAD-системой</param>
  /// <param name="applicationName">Имя приложения</param>
  public IntegratorLaunchActionHandler(IIntegrator integrator, string applicationName)
    : base(integrator.Id, applicationName)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  /// <summary>Стартовать приложение службы инструментов</summary>
  /// <param name="launchParams">Описатель параметров запуска приложения</param>
  /// <param name="handlerData">Конфигурация для запускаемого приложения</param>
  public override void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    if (handlerData == null)
      throw new ArgumentNullException(nameof (handlerData));
    if (!IntegratorServices.Exists(this.integrator.Id))
      throw new IntegratorNotInstalledException(this.integrator.DisplayName);
    ServiceUtils.GetService<ILaunchActionSupport>((object) this.integrator, true).OpenDocument(launchParams, this.AfterPublishFile);
  }

  /// <summary>
  /// Событие вызывается после публикации на диске файла объекта, который будет открыт в приложении.
  /// Используется другими сервисами системы для внедрения в файл информации о состоянии и статусе объекта (подписях и пр.).
  /// </summary>
  public event EventHandler<LaunchHandlerEventArgs> AfterPublishFile;
}
