
// Type: Intermech.ServiceRef`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech
{
    /// <summary>
    /// Реализует ссылку на общедоступный сервис приложения с защитой от null значений. Данный класс является thread-safe.
    /// </summary>
    /// <typeparam name="T">Тип сервиса</typeparam>
    public sealed class ServiceRef<T> : IServiceRef where T : class
    {
      private volatile T value;

      /// <summary>Создает объект.</summary>
      public ServiceRef()
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="value">Начальное значение ссылки</param>
      public ServiceRef(T value) => this.Value = value;

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
        [DebuggerStepThrough] get
        {
          return this.value ?? throw ServiceRef<T>.UninitializedReferenceException();
        }
        [DebuggerStepThrough] set => this.value = value;
      }

      private static InvalidOperationException UninitializedReferenceException()
      {
        return new InvalidOperationException($"A reference to service {typeof (T)} is not initialized. You must invoke an appropriate initialization method before use the reference.");
      }
    }
}
