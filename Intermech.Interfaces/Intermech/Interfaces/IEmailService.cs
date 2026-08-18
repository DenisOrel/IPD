
// Type: Intermech.Interfaces.IEmailService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Сервис по работе с электронной почтой.</summary>
    public interface IEmailService
    {
      /// <summary>Послать сообщение на e-mail (без вложений). Отправитель берется по-умолчанию из настроек.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
      /// <param name="accauntGuid">Глобальный идентификатор аккаунта.</param>
      /// <param name="toEmail">Email получателя, если несколько - разделять ;</param>
      /// <param name="subject">Тема письма.</param>
      /// <param name="message">Сообщение.</param>
      /// <returns>Возвращает MessageID для созданного письма.</returns>
      [NotNull]
      string SendMessage(
        Guid sessionGuid,
        Guid accauntGuid,
        [NotNull] string toEmail,
        [NotNull] string subject,
        [NotNull] string message);

      /// <summary>Послать сообщение на e-mail (с вложениями). Отправитель берется по-умолчанию из настроек.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
      /// <param name="accauntGuid">Глобальный идентификатор аккаунта.</param>
      /// <param name="toEmail">Email получателя, если несколько - разделять ;</param>
      /// <param name="subject">Тема письма.</param>
      /// <param name="message">Сообщение.</param>
      /// <param name="objectID">Объект, у которого беруться все файлы для вложения.</param>
      [NotNull]
      string SendMessage(
        Guid sessionGuid,
        Guid accauntGuid,
        [NotNull] string toEmail,
        [NotNull] string subject,
        [NotNull] string message,
        long objectID);

      /// <summary>Послать сообщение на e-mail (с вложениями). Отправитель берется по-умолчанию из настроек.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
      /// <param name="accauntID">Идентификатор аккаунта. Почтовый адрес аккаунта или глобальный идентификатор аккаунта.</param>
      /// <param name="toEmail">Email получателя, если несколько - разделять ;</param>
      /// <param name="subject">Тема письма.</param>
      /// <param name="message">Сообщение.</param>
      /// <param name="objectID">Объект, у которого беруться файлы для вложения, если Consts.UnknownObject, то вложений нет.</param>
      /// <param name="attachmentIdxs">Список индексов вложенных файлов из атрибута Файл.</param>
      /// <returns>Возвращает MessageID для созданного письма.</returns>
      [NotNull]
      string SendMessage(
        Guid sessionGuid,
        [NotNull] object accauntID,
        [NotNull] string toEmail,
        [NotNull] string subject,
        [NotNull] string message,
        long objectID,
        [CanBeNull] int[] attachmentIdxs);

      /// <summary>Получить список сообщений в папке Входящие.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
      /// <param name="accauntGuid">Глобальный идентификатор аккаунта.</param>
      /// <param name="presentMessageIDs">Идентификаторы уже полученных сообщений.</param>
      [NotNull]
      List<EmailMessage> GetInboxMessages(
        Guid sessionGuid,
        Guid accauntGuid,
        [NotNull] List<string> presentMessageIDs);

      /// <summary>Получить идентификатор письма по его заголовку.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
      /// <param name="accauntGuid">Глобальный идентификатор аккаунта.</param>
      /// <param name="subject">Заголовок.</param>
      [NotNull]
      string GetMessageID(Guid sessionGuid, Guid accauntGuid, [NotNull] string subject);

      /// <summary>Получить размер вложения.</summary>
      /// <param name="fileName">Имя файла вложения.</param>
      long GetAttachmentLength([NotNull] string fileName);

      /// <summary>Получить пачку байт вложения.</summary>
      /// <param name="fileName">Имя файла вложения.</param>
      /// <param name="offset">Смещение в файле.</param>
      /// <param name="count">Количество байт.</param>
      [NotNull]
      byte[] GetAttachmentData([NotNull] string fileName, int offset, int count);

      /// <summary>Почтовые сервера, зарегистрированные в системе.</summary>
      [CanBeNull]
      EmailServer[] Servers { get; }

      /// <summary>Добавить почтовый сервер.</summary>
      /// <param name="newServer">Новый почтовый сервер.</param>
      void AddServer([NotNull] EmailServer newServer);

      /// <summary>Добавить аккаунт.</summary>
      /// <param name="serverGuid">Глобальный идентификатор почтового сервера.</param>
      /// <param name="newAccaunt">Новый аккаунт.</param>
      void AddAccaunt(Guid serverGuid, [NotNull] EmailAccaunt newAccaunt);

      /// <summary>Проверить аккаунт.</summary>
      /// <param name="serverGuid">Глобальный идентификатор почтового сервера.</param>
      /// <param name="newAccaunt">Новый аккаунт.</param>
      void CheckAccaunt(Guid serverGuid, [NotNull] EmailAccaunt newAccaunt);

      /// <summary>Получить список аккаунтов для почтового сервера.</summary>
      /// <param name="serverGuid">Глобальный идентификатор почтового сервера.</param>
      [CanBeNull]
      EmailAccaunt[] GetAccaunts(Guid serverGuid);

      /// <summary>Получить список аккаунтов для пользователя.</summary>
      /// <param name="userID">Идентификатор пользователя.</param>
      /// <param name="ownered">Вернуть только те аккаунты, на которые у пользователя существует право на редактирование.</param>
      [CanBeNull]
      EmailAccaunt[] GetAccaunts(long userID, bool ownered);

      /// <summary>Получить параметры сервера.</summary>
      /// <param name="serverGuid">Глобальный идентификатор почтового сервера.</param>
      [CanBeNull]
      EmailServer GetServer(Guid serverGuid);

      /// <summary>Удалить почтовый сервер.</summary>
      /// <param name="serverGuid">Глобальный идентификатор почтового сервера.</param>
      void DeleteServer(Guid serverGuid);

      /// <summary>Изменить значения почтового сервера.</summary>
      /// <param name="server">Почтовый сервер</param>
      /// <param name="accaunts">Аккаунты</param>
      void SetServer(
        [NotNull] EmailServer server,
        [NotNull] Dictionary<EmailAccaunt, List<AccauntUserInfo>> accaunts);

      /// <summary>Получить список пользователей аккаунта.</summary>
      /// <param name="serverGuid">Глобальный идентификатор почтового сервера.</param>
      /// <param name="accauntGuid">Глобальный идентификатор аккаунта.</param>
      [CanBeNull]
      List<AccauntUserInfo> GetAccauntUsers(Guid serverGuid, Guid accauntGuid);

      /// <summary>Изменить данные авторизации аккаунта.</summary>
      /// <param name="accauntGuid">Глобальный идентификатор аккаунта.</param>
      /// <param name="newLogin">Новый логин.</param>
      /// <param name="newPassword">Новый пароль.</param>
      bool UpdateAccaunt(Guid accauntGuid, [NotNull] string newLogin, [NotNull] string newPassword);

      /// <summary>Получить аккаунт по e-mail.</summary>
      /// <param name="email">.</param>
      /// <returns>The accaunt. This may be null.</returns>
      [CanBeNull]
      EmailAccaunt GetAccaunt([NotNull] string email);

      /// <summary>Количество зачитываемых писем в пакете.</summary>
      /// <value>The total number of emails in package.</value>
      [Obsolete]
      int CountEmailsInPackage { get; }

      /// <summary>Удалить все письма из папки Входящие.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
      /// <param name="accauntGuid">Глобальный идентификатор аккаунта.</param>
      /// <param name="deleteList">Список уникальных идентификаторов писем на сервере.</param>
      void ClearInbox(Guid sessionGuid, Guid accauntGuid, [NotNull] List<string> deleteList);

      /// <summary>Очистка темпового файлового хранилища на диске после приема почты.</summary>
      void ClearTempFiles([NotNull] List<string> files);

      /// <summary>Прокси сервер.</summary>
      ProxyServer Proxy { get; set; }

      /// <summary>Проверка соединения с почтовым сервером.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
      /// <param name="accauntGuid">Глобальный идентификатор аккаунта.</param>
      void CheckAccauntConnection(Guid sessionGuid, Guid accauntGuid);
    }
}
