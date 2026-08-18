
// Type: Intermech.ApplicationModel.ApplicationServiceContainer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Реализует глобальный контейнер сервисов приложения.
    /// Реализация является thread safe.
    /// </summary>
    public class ApplicationServiceContainer : ServiceContainer
    {
      private object syncRoot;
      private HashSet<Type> serviceTypes;
      private bool allowCacheServiceReferences;
      private IApplicationServiceResolver serviceResolver;

      /// <summary>Создает объект.</summary>
      public ApplicationServiceContainer()
      {
        this.syncRoot = new object();
        this.serviceTypes = new HashSet<Type>();
        this.allowCacheServiceReferences = true;
      }

      public override void AddService(Type serviceType, object serviceInstance, bool promote)
      {
        lock (this.syncRoot)
        {
          base.AddService(serviceType, serviceInstance, promote);
          this.serviceTypes.Add(serviceType);
        }
      }

      public override void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
      {
        lock (this.syncRoot)
        {
          base.AddService(serviceType, callback, promote);
          this.serviceTypes.Add(serviceType);
        }
      }

      public override void RemoveService(Type serviceType, bool promote)
      {
        lock (this.syncRoot)
        {
          base.RemoveService(serviceType, promote);
          this.serviceTypes.Remove(serviceType);
        }
      }

      public override object GetService(Type serviceType)
      {
        return this.GetOrResolveService(serviceType, true);
      }

      internal object GetOrResolveService(Type serviceType, bool enableResolver)
      {
        lock (this.syncRoot)
        {
          object service = base.GetService(serviceType);
          if (service != null)
            return service;
          if (enableResolver)
          {
            object orResolveService = this.TryResolveService(serviceType);
            if (orResolveService != null)
              return orResolveService;
          }
          return (object) null;
        }
      }

      private object TryResolveService(Type serviceType)
      {
        if (this.serviceResolver != null)
        {
          object serviceInstance = this.serviceResolver.TryResolve(serviceType);
          if (serviceInstance != null)
          {
            this.AddService(serviceType, serviceInstance);
            return serviceInstance;
          }
        }
        return (object) null;
      }

      /// <summary>
      /// Возвращает или задает режим работы контейнера, разрешающий клиентам контейнера кэшировать ссылки на полченные сервисы.
      /// Значение по умолчанию равно true.
      /// </summary>
      public bool AllowCacheServiceReferences
      {
        get
        {
          lock (this.syncRoot)
            return this.allowCacheServiceReferences;
        }
        set
        {
          lock (this.syncRoot)
            this.allowCacheServiceReferences = value;
        }
      }

      /// <summary>
      /// Возвращает или задает стратегию поиска сервиса приложения, используемую, если не удалось найти сервис в контейнере сервисов приложения.
      /// </summary>
      public IApplicationServiceResolver ServiceResolver
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
            return this.serviceResolver;
        }
        [DebuggerStepThrough] set
        {
          lock (this.syncRoot)
            this.serviceResolver = value;
        }
      }

      /// <summary>
      /// Возвращает список всех типов, зарегистрированных в контейнере.
      /// </summary>
      /// <returns>Список типов</returns>
      public List<Type> GetServiceTypes()
      {
        lock (this.syncRoot)
          return new List<Type>((IEnumerable<Type>) this.serviceTypes);
      }
    }
}
