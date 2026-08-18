
// Type: Intermech.Interfaces.UserAndRoleInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для передачи и хранения некоторых свойств юзерской сессии
    /// </summary>
    [Serializable]
    public class UserAndRoleInfo
    {
      /// <summary>Guid текущего пользователя</summary>
      public Guid UserGuid = Guid.Empty;
      /// <summary>Guid текущей роли</summary>
      public Guid RoleGuid = Guid.Empty;
      /// <summary>Текущее правило по сортировке и отображению составов</summary>
      public CompositionsAutosortRule Rule;
      /// <summary>Идентификатор настроек роли по умолчанию</summary>
      public long RoleDefaultObjectID = -1;
      /// <summary>Количество записей в пакете</summary>
      public int MaxRows = -1;
      /// <summary>Ид. объекта-юзера</summary>
      public long ID;
    }
}
