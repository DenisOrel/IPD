
// Type: Intermech.Interfaces.IMServerLiveStatus
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для получения состояния сервера приложений.
    /// </summary>
    [ClientSideDisconnectionProtection(false)]
    public interface IMServerLiveStatus
    {
      /// <summary>
      /// Метод для проверки работоспособности подключения к серверу приложений.
      /// </summary>
      /// <exception cref="T:System.Exception">Подключение к серверу приложений нарушено</exception>
      void KnockKnock();

      /// <summary>
      /// Метод для проверки работоспособности подключения к серверу приложений.
      /// Подключение считается работоспособным только в том случае, если через remoting
      /// доступен не только сам сервер приложений, но и указанный серверный объект.
      /// </summary>
      /// <param name="serverObject">Дополнительный серверный объект для проверки подключения</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="serverObject" /> содержит null</exception>
      /// <exception cref="T:System.Exception">Подключение к серверу приложений нарушено</exception>
      void KnockKnock(object serverObject);

      /// <summary>
      /// Метод для проверки работоспособности подключения к серверу приложений.
      /// Подключение считается работоспособным только в том случае, если через remoting
      /// доступен не только сам сервер приложений, но и указанные серверные объекты.
      /// </summary>
      /// <param name="serverObjects">Дополнительные серверные объекты для проверки подключения</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="serverObjects" /> содержит null</exception>
      /// <exception cref="T:System.Exception">Подключение к серверу приложений нарушено</exception>
      void KnockKnock(params object[] serverObjects);

      /// <summary>
      /// Возвращает значение загруженности данного сервера приложений за последние 2 часа
      /// </summary>
      int ActivityCounter { get; }

      /// <summary>Возвращает строку подключения к базе данных</summary>
      string ConnectionString { get; }
    }
}
