
// Type: Intermech.Interfaces.IMServer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting;
using Intermech.Remoting.Compression;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс сервера приложения</summary>
    [RemotingCompression(false)]
    [ClientSideDisconnectionProtection(false)]
    public interface IMServer
    {
      /// <summary>Возвращает версию сервера приложений.</summary>
      Version Version { get; }

      /// <summary>
      /// Возвращает режим авторизации пользователей сервером приложений.
      /// </summary>
      IMServerLoginMode LoginMode { get; }

      /// <summary>
      /// Создает и возвращает новую незалогиненную пользовательскую сессию.
      /// </summary>
      /// <returns>Новая незалогиненная пользовательская сессия</returns>
      IUserSession CreateSession();

      /// <summary>
      /// Возвращает сервис для получения конфигурации приложения из файла app.config.
      /// </summary>
      IMServerAppConfiguration AppConfiguration { get; }

      /// <summary>Возвращает сервис состояния сервера приложений.</summary>
      IMServerLiveStatus LiveStatus { get; }

      /// <summary>
      /// Возвращает сервис для явного управления временем жизни объектов сервера приложений с клиента.
      /// </summary>
      ILeaseRenewalService LeaseRenewalService { get; }

      /// <summary>
      /// Возвращает юзерскую картинку, которая должна быть отрисована на загрузочном окне клиента IPS. Если картинки нет - возвращает null.
      /// </summary>
      byte[] UsersBanner { get; }

      /// <summary>Записывает сообщение в лог-файл сервера приложений.</summary>
      /// <param name="text">Текст сообщения</param>
      /// <param name="traceLevel">Уровень трассировки, при котором сообщение будет записано в файл</param>
      /// <param name="traceFileName">Имя файла трассировки</param>
      /// <param name="computerName">Имя компьютера, который записывает сообщение</param>
      /// <param name="userName">Имя пользователя, который записывает сообщение</param>
      void AddToTrace(
        string text,
        int traceLevel,
        string traceFileName = null,
        string computerName = null,
        string userName = null);

      /// <summary>
      /// Записывает несколько сообщений в лог-файл сервера приложений.
      /// </summary>
      /// <param name="eventRecords">Коллекция записываемых сообщений</param>
      void AddToTrace(ICollection<AddToTraceRecord> eventRecords);

      /// <summary>
      /// Возвращает настройку шифрования паролей, которая задана в сервере приложений
      /// </summary>
      char CryptMethod { get; }

      /// <summary>
      /// Получает интерфейс, зарегистрированный на сервере службой ICustomServices и доступный на стороне клиента
      /// </summary>
      /// <param name="serviceType">Тип зарегистрированного интерфейса</param>
      /// <returns>Требуемый интерфейс или null</returns>
      object GetCustomService(Type serviceType);
    }
}
