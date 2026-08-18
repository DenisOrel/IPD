
// Type: Intermech.Interfaces.WebPortal.ISiteServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Protection;
using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Интерфейс на сервис с настройками соединения с порталом
    /// </summary>
    public interface ISiteServerService
    {
      /// <summary>Настройки соединения</summary>
      ConnectionSettings Settings { get; set; }

      /// <summary>
      /// Флаг того, что серверные сервисы для работы с порталом загружены
      /// </summary>
      bool Initialized { get; }

      /// <summary>Добавить пользователя</summary>
      /// <param name="session">Сессия</param>
      /// <param name="userName">Выводимое имя</param>
      /// <param name="login">Логин</param>
      /// <param name="password">Хэш пароля</param>
      /// <param name="userGuid">Глобальный идентификатор версии объекта-пользователя</param>
      /// <param name="siteCode">Код узла, который публикует пользователя</param>
      /// <returns></returns>
      long AddUser(
        object session,
        string userName,
        string login,
        string password,
        Guid userGuid,
        char siteCode);

      /// <summary>Добавить пользователя</summary>
      /// <param name="session">Сессия</param>
      /// <param name="userName">Выводимое имя</param>
      /// <param name="login">Логин</param>
      /// <param name="password">Хэш пароля</param>
      /// <param name="userGuid">Глобальный идентификатор версии объекта-пользователя</param>
      /// <param name="siteCode">Код узла, который публикует пользователя</param>
      /// <returns></returns>
      long AddUser(
        object session,
        string userName,
        string login,
        PswPackage password,
        Guid userGuid,
        char siteCode);

      /// <summary>Изменить пароль пользователю</summary>
      /// <param name="session">Сессия</param>
      /// <param name="login">Логин</param>
      /// <param name="password">Хэш нового пароля</param>
      void ChangeUserPassword(object session, string login, string password);

      /// <summary>Изменить пароль пользователю</summary>
      /// <param name="session">Сессия</param>
      /// <param name="login">Логин</param>
      /// <param name="password">Хэш нового пароля</param>
      void ChangeUserPassword(object session, string login, PswPackage password);
    }
}
