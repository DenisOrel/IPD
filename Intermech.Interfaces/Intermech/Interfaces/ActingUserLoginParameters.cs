
// Type: Intermech.Interfaces.ActingUserLoginParameters
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Параметры входа пользователя в систему для режима исполнения обязанностей.
    /// </summary>
    [Serializable]
    public sealed class ActingUserLoginParameters
    {
      private int securityLevel;
      private long userID;
      private long roleID;
      private ActingUserInfo actingUser;

      public ActingUserLoginParameters(
        long userID,
        long roleID,
        int securityLevel,
        ActingUserInfo actingUser)
      {
        if (userID == 0L)
          throw new ArgumentException("Не задан идентификатор пользователя, обязанности которого будут исполняться.", nameof (userID));
        if (roleID == 0L)
          throw new ArgumentException("Не задан идентификатор роли пользователя, обязанности которого будут исполняться.", nameof (roleID));
        if (actingUser == null)
          throw new ArgumentNullException(nameof (actingUser));
        this.userID = userID;
        this.roleID = roleID;
        this.securityLevel = securityLevel;
        this.actingUser = actingUser;
      }

      /// <summary>
      /// Возвращает идентификатор пользователя, обязанности которого будут исполняться.
      /// </summary>
      public long UserID
      {
        [DebuggerStepThrough] get => this.userID;
      }

      /// <summary>
      /// Возвращает идентификатор роли пользователя, обязанности которого будут исполняться.
      /// </summary>
      public long RoleID
      {
        [DebuggerStepThrough] get => this.roleID;
      }

      /// <summary>
      /// Возвращает сведения о пользователе, который будет исполнять обязанности указанного пользователя.
      /// </summary>
      public ActingUserInfo ActingUser
      {
        [DebuggerStepThrough] get => this.actingUser;
      }

      /// <summary>Уровень доступа текущего пользователя</summary>
      public int SecurityLevel
      {
        [DebuggerStepThrough] get => this.securityLevel;
      }
    }
}
