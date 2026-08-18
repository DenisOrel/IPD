
// Type: Intermech.Interfaces.NotifySamples.INotifySamplesProcessor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.NotifySamples
{
    /// <summary>
    /// Интерфейс обработчика уведомляющих выборок пользователя
    /// </summary>
    public interface INotifySamplesProcessor
    {
      /// <summary>
      /// Вызывает опрос уведомляющих выборок юзера для поиска изменившихся данных
      /// </summary>
      /// <returns>Результат опроса</returns>
      NSResult ProcessSamples();

      /// <summary>
      /// Метод сохраняет в базу данных изменения в списках объектов, которые были рассчитаны в процессе опросов выборок
      /// </summary>
      void SaveSamplesState();

      /// <summary>Метод перечитывает данные уведомляющих выборок из БД</summary>
      void ReloadSamples();
    }
}
