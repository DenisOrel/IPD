
// Type: Intermech.Interfaces.LoginInformation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Информация о свойствах пользователя, необходимая для отображения в диалогах авторизации
    /// </summary>
    [Serializable]
    public class LoginInformation
    {
      /// <summary>Массив допустимых ролей</summary>
      public RoleProperties[] Roles { get; private set; }

      /// <summary>
      /// Словарь допустимых уровней доступа (так исторически сложилось, что это словарь)
      /// </summary>
      public Dictionary<int, string> AccessLevels { get; private set; }

      public LoginInformation(RoleProperties[] roles, Dictionary<int, string> levels)
      {
        this.Roles = roles;
        this.AccessLevels = levels;
      }
    }
}
