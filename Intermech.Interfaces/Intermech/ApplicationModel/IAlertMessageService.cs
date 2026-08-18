
// Type: Intermech.ApplicationModel.IAlertMessageService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Интерфейс сервиса для показа пользователю сообщений об исключительно важных событиях в приложении,
    /// на которые пользователь должен немедленно обратить внимание. Например: недоступность или окончание срока действия ключа защиты,
    /// ошибки инициализации remoting, ошибки инициализации COM, необработанные исключения. Если приложение является интерактивным,
    /// то сообщение будет выведено в виде диалогового окна или строки в консоли приложения с выделением цветом.
    /// Также сообщение будет выведено в журналы событий приложения.
    /// </summary>
    public interface IAlertMessageService
    {
      /// <summary>Показывает информационное сообщение пользователю.</summary>
      /// <param name="caption">Заголовок сообщения</param>
      /// <param name="message">Сообщение</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="caption" /> и <paramref name="message" /> не должны быть равны null</exception>
      void ShowMessage(string caption, string message);

      /// <summary>Показывает сообщение пользователю.</summary>
      /// <param name="caption">Заголовок сообщения</param>
      /// <param name="message">Сообщение</param>
      /// <param name="messageType">Тип сообщения</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="caption" /> и <paramref name="message" /> не должны быть равны null</exception>
      void ShowMessage(string caption, string message, AlertMessageType messageType);
    }
}
