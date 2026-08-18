
// Type: Intermech.Search.ServiceLocator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Intermech.Search
{
    /// <summary>Локатор сервисов</summary>
    public static class ServiceLocator
    {
      /// <summary>
      /// 
      /// </summary>
      private static readonly ServiceLocator.ServiceInfo _lastServiceInfo = new ServiceLocator.ServiceInfo();
      /// <summary>
      /// Класс для раздельной блокировки потоков на чтение и запись
      /// </summary>
      private static readonly ReaderWriterLockSlim _lastServiceLock = new ReaderWriterLockSlim();
      private static ServiceContainer _serviceContainer = new ServiceContainer();

      /// <summary>Инициализировать</summary>
      /// <param name="serviceProvider">Провайдер сервисов</param>
      public static void Initialize(IServiceProvider serviceProvider)
      {
        if (serviceProvider == null)
          throw new ArgumentNullException(nameof (serviceProvider));
        ServiceLocator._serviceContainer = serviceProvider is ServiceContainer ? (ServiceContainer) serviceProvider : new ServiceContainer(serviceProvider);
      }

      /// <summary>Получить сервис</summary>
      /// <typeparam name="T">Тип сервиса</typeparam>
      /// <returns></returns>
      /// <exception cref="T:System.Exception"></exception>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Get<T>() => (T) ServiceLocator.Get(typeof (T));

      public static bool IsRegistered<T>()
      {
        return ServiceLocator._serviceContainer.GetService(typeof (T)) != null;
      }

      /// <summary>Зарегистрировать сервис</summary>
      /// <typeparam name="T">Тип сервиса</typeparam>
      /// <param name="service">Сервис</param>
      /// <exception cref="T:System.ArgumentNullException">service</exception>
      public static void Register<T>(T service)
      {
        if ((object) service == null)
          throw new ArgumentNullException(nameof (service));
        ServiceLocator.Register(typeof (T), (object) service);
      }

      /// <summary>Зарегистрирвать сервис</summary>
      /// <param name="serviceType">Тип сервиса</param>
      /// <param name="service">Сервис</param>
      /// <exception cref="T:System.ArgumentNullException">
      /// serviceType
      /// or
      /// service
      /// </exception>
      public static void Register(Type serviceType, object service)
      {
        if (serviceType == (Type) null)
          throw new ArgumentNullException(nameof (serviceType));
        if (service == null)
          throw new ArgumentNullException(nameof (service));
        ServiceLocator._serviceContainer.AddService(serviceType, service);
      }

      /// <summary>Разрегистрировать сервис</summary>
      /// <typeparam name="T"></typeparam>
      public static void Unregister<T>() => ServiceLocator.Unregister(typeof (T));

      /// <summary>Разрегистрировать сервис</summary>
      /// <param name="serviceType">Тип сервиса</param>
      /// <exception cref="T:System.ArgumentNullException">serviceType</exception>
      public static void Unregister(Type serviceType)
      {
        if (serviceType == (Type) null)
          throw new ArgumentNullException(nameof (serviceType));
        ServiceLocator._serviceContainer.RemoveService(serviceType);
        ServiceLocator._lastServiceLock.EnterWriteLock();
        try
        {
          if (ServiceLocator._lastServiceInfo.Type != serviceType)
            return;
          ServiceLocator._lastServiceInfo.Type = (Type) null;
          ServiceLocator._lastServiceInfo.Service = (object) null;
        }
        finally
        {
          ServiceLocator._lastServiceLock.ExitWriteLock();
        }
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static object Get(Type type)
      {
        if (type == (Type) null)
          throw new ArgumentNullException(nameof (type));
        ServiceLocator._lastServiceLock.EnterReadLock();
        try
        {
          if (ServiceLocator._lastServiceInfo.Type == type)
            return ServiceLocator._lastServiceInfo.Service;
        }
        finally
        {
          ServiceLocator._lastServiceLock.ExitReadLock();
        }
        ServiceLocator._lastServiceLock.EnterWriteLock();
        object service;
        try
        {
          ServiceLocator._lastServiceInfo.Type = type;
          ServiceLocator._lastServiceInfo.Service = service = ServiceLocator._serviceContainer.GetService(type);
        }
        finally
        {
          ServiceLocator._lastServiceLock.ExitWriteLock();
        }
        return service != null ? service : throw new Exception($"Сервис {type.Name} не найден");
      }

      private class ServiceInfo
      {
        public Type Type;
        public object Service;
      }
    }
}
