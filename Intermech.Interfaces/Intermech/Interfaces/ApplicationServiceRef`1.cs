
// Type: Intermech.Interfaces.ApplicationServiceRef`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.ApplicationModel;
using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует ссылку на общедоступный сервис приложения с защитой от null значений. Данный класс является thread-safe.
    /// </summary>
    /// <typeparam name="T">Тип сервиса</typeparam>
    public sealed class ApplicationServiceRef<T> : IServiceRef where T : class
    {
      private readonly bool autoCreateMode;
      private volatile T value;

      /// <summary>Создает объект.</summary>
      public ApplicationServiceRef() => this.autoCreateMode = false;

      /// <summary>Создает объект.</summary>
      /// <param name="autoCreateMode">Включает режим автоматического создания экземпляра сервиса, если он отсутствует в контейнере сервисов</param>
      public ApplicationServiceRef(bool autoCreateMode) => this.autoCreateMode = autoCreateMode;

      /// <summary>Возвращает true, если у ссылки есть целевой объект.</summary>
      public bool HasValue
      {
        [DebuggerStepThrough] get => (object) this.value != null;
      }

      /// <summary>
      /// Возвращает или задает значение ссылки. Если значение читаемой ссылки равно null, то будет сброшено исключение.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Не задано значение ссылки</exception>
      public T Value
      {
        [DebuggerStepThrough] get => this.value ?? this.GetValueFromApplicationServices();
        [DebuggerStepThrough] set => this.value = value;
      }

      private T GetValueFromApplicationServices()
      {
        ApplicationServiceContainer container = ApplicationServices.Container;
        T service = (T) container.GetService(typeof (T));
        if ((object) service != null)
        {
          if (container.AllowCacheServiceReferences)
            this.value = service;
          return service;
        }
        if (!this.autoCreateMode)
          throw new InvalidOperationException($"A reference to service {typeof (T)} is not initialized. You must invoke an appropriate initialization method before use the reference.");
        T instance = Activator.CreateInstance<T>();
        this.value = instance;
        return instance;
      }
    }
}
