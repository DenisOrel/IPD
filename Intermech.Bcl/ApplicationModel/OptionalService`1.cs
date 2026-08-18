
// Type: Intermech.ApplicationModel.OptionalService`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Провайдер сервиса, получение которого не является гарантированным.
    /// </summary>
    /// <typeparam name="T">Тип сервиса, предоставляемого провайдером</typeparam>
    public class OptionalService<T> : IOptionalService<T>
    {
      private IServiceProvider container;

      /// <summary>Создает объект.</summary>
      /// <param name="container">Контейнер сервисов</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="container" /> не должен быть равен null</exception>
      public OptionalService(IServiceProvider container)
      {
        this.container = container != null ? container : throw new ArgumentNullException(nameof (container));
      }

      /// <summary>
      /// Возвращает объект сервиса или нулевое значение для данного типа объекта, если объект не может быть получен.
      /// </summary>
      /// <returns>Объект или нулевое значение для данного типа объектов</returns>
      public T TryGet() => (T) this.container.GetService(typeof (T));
    }
}
