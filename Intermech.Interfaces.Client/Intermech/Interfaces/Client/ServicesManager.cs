// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ServicesManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel.Design;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Статический класс для работы с глобальными сервисами приложения. Реализация является thread safe.
/// Данный класс морально устарел и оставлен для поддержания работоспособности существующего кода.
/// Новый код должен использовать класс <see cref="T:Intermech.Interfaces.ApplicationServices" />.
/// </summary>
public static class ServicesManager
{
  private static readonly IServiceContainer _services = (IServiceContainer) ApplicationServices.Container;

  /// <summary>Удаляет указанный сервис</summary>
  /// <param name="serviceType">Тип удаляемого сервиса</param>
  public static void RemoveService(Type serviceType)
  {
    ServicesManager._services.RemoveService(serviceType);
  }

  /// <summary>
  /// Добавление в список сервисов функции для получения сервиса.
  /// </summary>
  /// <param name="serviceType"></param>
  /// <param name="callback"></param>
  /// <code>
  /// public object DbSessionableCreator(IServiceContainer container,Type needType)
  /// {
  /// 	if(needType == typeof(MyObject1) return new MyObject1();
  /// 	...
  /// }
  /// ...
  /// ServerService.AddService(typeof(DBSessionable),new ServiceCreatorCallback(DbSessionableCreator);
  /// </code>
  public static void AddService(Type serviceType, ServiceCreatorCallback callback)
  {
    ServicesManager._services.AddService(serviceType, callback);
  }

  /// <summary>Добавляет в список сервисов указанный обЪект</summary>
  /// <param name="serviceType">тип добавляемого сервиса</param>
  /// <param name="serviceInstance">добавляемый сервис</param>
  /// <code>
  /// ServerServices.AddService(typeof(IInterface),new InterfaceImpl());
  /// ...
  /// IInterface it = ServerService.GetService(typeof(IInterface));
  /// </code>
  public static void AddService(Type serviceType, object serviceInstance)
  {
    ServicesManager._services.AddService(serviceType, serviceInstance);
  }

  public static object GetService(Type serviceType)
  {
    return ServicesManager._services.GetService(serviceType);
  }

  public static IServiceContainer ServiceContainer
  {
    [DebuggerStepThrough] get => ServicesManager._services;
  }

  /// <summary>Попытка извлечения сервиса из провайдера сервисов</summary>
  /// <exception cref="T:System.InvalidOperationException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
  /// сервиса (при аргументе throwExceptionIfNotFound == true)</exception>
  /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
  /// <param name="throwExceptionIfNotFound">Выбрасывать ли исключительную ситуацию в случае отсустствия в провайдере запрашиваемого
  /// сервиса</param>
  /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
  /// запрашиваемого сервиса в провайдере. Если null, то испольхзуется стандартное сообщение</param>
  /// <returns>Извекаемый сервис</returns>
  public static T GetService<T>(bool throwExceptionIfNotFound = true, string exceptionMessageIfFail = null)
  {
    return ServicesManager._services.GetService<T>(throwExceptionIfNotFound, exceptionMessageIfFail);
  }

  /// <summary>Извлечение сервиса из провайдера сервисов</summary>
  /// <exception cref="T:System.InvalidOperationException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
  /// сервиса</exception>
  /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
  /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
  /// запрашиваемого сервиса в провайдере. Если null, то испольхзуется стандартное сообщение</param>
  /// <returns>Извекаемый сервис</returns>
  public static T GetService<T>(string exceptionMessageIfFail)
  {
    return ServicesManager._services.GetService<T>(exceptionMessageIfFail);
  }
}
