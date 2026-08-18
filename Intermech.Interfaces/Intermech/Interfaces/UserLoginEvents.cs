
// Type: Intermech.Interfaces.UserLoginEvents
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс содержит информацию о датах и времени двух последних логинов пользователя
    /// </summary>
    [Serializable]
    public class UserLoginEvents
    {
      /// <summary>Создает объект.</summary>
      public UserLoginEvents()
      {
        this.CurrentLoginDateTime = DateTime.MinValue;
        this.PrevLoginDateTime = DateTime.MinValue;
      }

      /// <summary>Дата и время текущего логина</summary>
      public DateTime CurrentLoginDateTime { get; set; }

      /// <summary>Дата и время предыдущего логина</summary>
      public DateTime PrevLoginDateTime { get; set; }
    }
}
