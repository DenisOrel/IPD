
// Type: Intermech.Interfaces.IDatabaseLocker
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс службы для блокировки повторных вызовов различных методов через базу данных
    /// </summary>
    public interface IDatabaseLocker
    {
      /// <summary>
      /// Проверяет наличие в базе данных блокировки вызова метода methodName
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="methodName">Имя метода</param>
      /// <param name="maxDuration">Максимальная продолжительность метода</param>
      /// <returns>Класс с информацией о блокировке</returns>
      DatabaseLockInfo Lock(IUserSession session, string methodName, TimeSpan maxDuration);

      /// <summary>Удаляет блокировку метода methodName</summary>
      /// <param name="session">Сессия</param>
      /// <param name="methodName">Имя метода</param>
      void UnLock(IUserSession session, string methodName);

      /// <summary>
      /// Удаляет все блокировки всех методов (Требует админских прав!)
      /// </summary>
      /// <param name="session">Сессия</param>
      void UnLockAll(IUserSession session);
    }
}
