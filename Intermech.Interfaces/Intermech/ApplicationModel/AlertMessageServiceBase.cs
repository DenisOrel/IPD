using Intermech.Diagnostics;
using System;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Базовый класс сервиса для показа пользователю сообщений об исключительно важных событиях в приложении,
    /// на которые пользователь должен немедленно обратить внимание. Например: недоступность или окончание срока действия ключа защиты,
    /// ошибки инициализации remoting, ошибки инициализации COM, необработанные исключения. Если приложение является интерактивным,
    /// то сообщение будет выведено в виде диалогового окна или строки в консоли приложения с выделением цветом.
    /// Также сообщение будет выведено в журналы событий приложения.
    /// </summary>
    public class AlertMessageServiceBase : IAlertMessageService
    {
      /// <summary>Показывает информационное сообщение пользователю.</summary>
      /// <param name="caption">Заголовок сообщения</param>
      /// <param name="message">Сообщение</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="caption" /> и <paramref name="message" /> не должны быть равны null</exception>
      public void ShowMessage(string caption, string message)
      {
        this.ShowMessage(caption, message, AlertMessageType.Information);
      }

      /// <summary>Показывает сообщение пользователю.</summary>
      /// <param name="caption">Заголовок сообщения</param>
      /// <param name="message">Сообщение</param>
      /// <param name="messageType">Тип сообщения</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="caption" /> и <paramref name="message" /> не должны быть равны null</exception>
      public void ShowMessage(string caption, string message, AlertMessageType messageType)
      {
        if (caption == null)
          throw new ArgumentNullException(nameof (caption));
        if (message == null)
          throw new ArgumentNullException(nameof (message));
        this.DoShowMessage(caption, message, messageType);
      }

      /// <summary>Показывает сообщение пользователю.</summary>
      /// <param name="caption">Заголовок сообщения</param>
      /// <param name="message">Сообщение</param>
      /// <param name="messageType">Тип сообщения</param>
      protected virtual void DoShowMessage(
        string caption,
        string message,
        AlertMessageType messageType)
      {
      }

      /// <summary>
      /// Преобразует тип тревожного сообщения в тип записи для журнала событий.
      /// </summary>
      /// <param name="messageType">Тип сообщения</param>
      /// <returns>Тип записи для журнала событий</returns>
      protected EventLogItemType MessageTypeToEventLogItemType(AlertMessageType messageType)
      {
        if (messageType == AlertMessageType.Warning)
          return EventLogItemType.Warning;
        return messageType == AlertMessageType.Error ? EventLogItemType.Error : EventLogItemType.Information;
      }

      /// <summary>
      /// Возвращает текст сообщения с включенным в него заголовком.
      /// Метод используется при выводе сообщений с помощью неинтерактивных средств (например, в журнал событий приложения).
      /// </summary>
      /// <param name="caption">Заголовок сообщения</param>
      /// <param name="message">Сообщение</param>
      /// <returns>Текст сообщения с включенным в него заголовком</returns>
      protected string CombineCaptionWithMessage(string caption, string message)
      {
        if (caption == null)
          throw new ArgumentNullException(nameof (caption));
        if (message == null)
          throw new ArgumentNullException(nameof (message));
        return !string.IsNullOrEmpty(caption) ? $"{caption} : {message}" : message;
      }
    }
}
