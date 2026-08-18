
// Type: Intermech.Interfaces.ActingUserInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сведения о пользователе, исполняющем обязанности другого пользователя.
    /// </summary>
    [Serializable]
    public sealed class ActingUserInfo
    {
      private long userID;
      private string computerName;
      private TimeSpan timeZoneOffset;
      private int securityLevel;

      /// <summary>Создает объект.</summary>
      /// <param name="userID">Идентификатор пользователя, который исполняет обязанности другого пользователя</param>
      /// <param name="computerName">Сетевое имя компьютера пользователя, который исполняет обязанности другого пользователя</param>
      /// <param name="timeZoneOffset">Смещение по Гвинвичу времени текущей зоны времени для рабочей станции пользователя, который исполняет обязанности другого пользователя</param>
      /// <param name="securityLevel">Уровень доступа пользовательской сессии, которая исполняет обязанности другого юзера</param>
      /// <exception cref="T:ArgumentException">Параметр <paramref name="userID" /> содержит недопустимое значение; параметр <paramref name="computerName" /> не должен быть пуст или равен null</exception>
      public ActingUserInfo(
        long userID,
        string computerName,
        TimeSpan timeZoneOffset,
        int securityLevel)
      {
        if (userID == 0L)
          throw new ArgumentException("Не задан идентификатор пользователя, который будет исполнять обязанности другого пользователя.", nameof (userID));
        if (string.IsNullOrEmpty(computerName))
          throw new ArgumentException("Не задано имя компьютера.", nameof (computerName));
        this.userID = userID;
        this.computerName = computerName;
        this.timeZoneOffset = timeZoneOffset;
        this.securityLevel = securityLevel;
      }

      /// <summary>
      /// Уровень доступа сессии пользователя, который исполняет обязанности другого пользователя
      /// </summary>
      public int SecurityLevel
      {
        [DebuggerStepThrough] get => this.securityLevel;
      }

      /// <summary>
      /// Возвращает идентификатор пользователя, который исполняет обязанности другого пользователя.
      /// </summary>
      public long UserID
      {
        [DebuggerStepThrough] get => this.userID;
      }

      /// <summary>
      /// Возвращает сетевое имя компьютера пользователя, который исполняет обязанности другого пользователя.
      /// </summary>
      public string ComputerName
      {
        [DebuggerStepThrough] get => this.computerName;
      }

      /// <summary>
      /// Возвращает смещение по Гвинвичу времени текущей зоны времени для рабочей станции пользователя, который исполняет обязанности другого пользователя.
      /// </summary>
      public TimeSpan TimeZoneOffset
      {
        [DebuggerStepThrough] get => this.timeZoneOffset;
      }
    }
}
