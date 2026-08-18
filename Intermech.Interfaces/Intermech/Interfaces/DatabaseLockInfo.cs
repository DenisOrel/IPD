
// Type: Intermech.Interfaces.DatabaseLockInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Класс с информацией о блокировке метода в базе данных</summary>
    [Serializable]
    public class DatabaseLockInfo
    {
      /// <summary>True если блокировка была успешно установлена</summary>
      public bool Success { get; private set; }

      /// <summary>Пользователь, создавший блокировку</summary>
      public string LockerUserName { get; private set; }

      /// <summary>Компьютер, с которого вызвана блокировка</summary>
      public string LockerCompName { get; private set; }

      /// <summary>Дата и время начала блокировки</summary>
      public DateTime LockTime { get; private set; }

      public DatabaseLockInfo() => this.Success = true;

      public DatabaseLockInfo(string userName, string compName, DateTime lockTime)
      {
        this.Success = false;
        this.LockerUserName = userName;
        this.LockerCompName = compName;
        this.LockTime = lockTime;
      }

      public string GetErrorMessage(string methodCaption)
      {
        if (this.Success)
          return string.Empty;
        return $"Метод '{methodCaption}' не может быть выполнен, т.к. его выполнение уже инициировано пользователем {this.LockerUserName} с устройства {this.LockerCompName}. Выполнение было начато {this.LockTime}";
      }
    }
}
