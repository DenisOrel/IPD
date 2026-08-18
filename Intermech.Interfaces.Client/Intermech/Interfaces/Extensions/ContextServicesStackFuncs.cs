// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Extensions.ContextServicesStackFuncs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.Interfaces.Extensions;

/// <summary>Класс для расширения над контейнером серсисов, упрощающего регистрацию провайдера локальных команд не удаляя приэтом команды вышестоящего
/// контекста</summary>
public static class ContextServicesStackFuncs
{
  /// <summary>Класс для расширение над контейнером серсисов, упрощающего регистрацию локальных сервисов в формате стека однотипных сервисов в
  /// контексте. Например "фильтрация команд контекстного меню должна осуществляться контролом, а так же всеми
  /// контролами, в которые он вложен (поддерживающих сервис фильтрации команд)"</summary>
  public static void AddLocalService<ServiceType>(
    IServiceContainer localContext,
    ServiceType localService)
    where ServiceType : class
  {
    localContext.AddService(typeof (IContextServicesStack<ServiceType>), (object) new ContextServicesStack<ServiceType>(localContext, localService));
  }

  /// <summary>Класс для расширение над контейнером серсисов, упрощающего регистрацию локальных сервисов в формате стека однотипных сервисов в
  /// контексте. Например "фильтрация команд контекстного меню должна осуществляться контролом, а так же всеми
  /// контролами, в которые он вложен (поддерживающих сервис фильтрации команд)"</summary>
  public static void RemoveLocalService<ServiceType>(
    IServiceContainer localContext,
    ServiceType localService)
    where ServiceType : class
  {
    localContext.RemoveService(typeof (IContextServicesStack<ServiceType>));
  }

  /// <summary>Засуспендить в контексте сервиса Может потребоваться например для того, чтобы во вложенном в диалог (который является
  /// провайдером комманд) пользовательском контроле, запретить отображаться дополнительным командам, создаваемым диалогом</summary>
  /// <param name="localContext">Контекст</param>
  /// <param name="constructor">Конструктор стека сервисов</param>
  /// <param name="localService">Сервис, который должен быть засуспенжен в данном контексте (в контексте будет стек сервисов без онного)</param>
  public static void SuspendLocalService<ServiceType>(
    IServiceContainer localContext,
    Func<ContextServicesStack<ServiceType>> constructor,
    ServiceType localService)
    where ServiceType : class
  {
    ContextServicesStack<ServiceType> serviceInstance = constructor();
    serviceInstance._commandsProviders.Remove(localService);
    localContext.AddService(typeof (IContextServicesStack<ServiceType>), (object) serviceInstance);
  }
}
