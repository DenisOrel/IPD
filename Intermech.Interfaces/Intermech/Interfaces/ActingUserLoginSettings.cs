
// Type: Intermech.Interfaces.ActingUserLoginSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс содержит информацию о настройка пользователя по исполнению обязанностей за других пользователей
    /// </summary>
    [Serializable]
    public class ActingUserLoginSettings
    {
      /// <summary>
      /// Идентификаторы и имена пользователей, чьи обязанности разрешено выполнять
      /// </summary>
      public Dictionary<long, string> Users;
      /// <summary>
      /// Идентификатор роли, в которых можно исполнять обязанности этих пользователей (если == null, то в любой роли, назначенной юзеру, чьи обязанности он будет исполнять)
      /// </summary>
      public long RoleID;
      /// <summary>Имя роли</summary>
      public string RoleName;
      /// <summary>
      /// Ид. юзера, для которого запросили исполнение обязанностей (т.е. для которого заполнена эта структура)
      /// </summary>
      public long ActingUserID;
    }
}
