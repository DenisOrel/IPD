
// Type: Intermech.Interfaces.ServiceProviderValueHolder`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Контейнер для значений, передаваемых через провайдер сервисов <see cref="T:IServiceProvider" />.
    /// </summary>
    /// <typeparam name="T">Тип значения для передачи через провайдер сервисов</typeparam>
    public class ServiceProviderValueHolder<T>
    {
      private T value;

      /// <summary>Создает объект.</summary>
      /// <param name="value">Начальное значение</param>
      public ServiceProviderValueHolder(T value) => this.value = value;

      /// <summary>Возвращает или задает хранимое значение.</summary>
      public T Value
      {
        get => this.value;
        set => this.value = value;
      }
    }
}
