
// Type: Intermech.Security.IPSPrincipal
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;


namespace Intermech.Security
{
    /// <summary>
    /// Описывает пользователя IPS и соответствующий ему контекст безопасности. Эти сведения используются
    /// для аутентификации и авторизации пользователя IPS при работе с системой.
    /// </summary>
    public class IPSPrincipal : IPrincipal, ICloneable, IEquatable<IPSPrincipal>, IEquatable<IPrincipal>
    {
      private static volatile IPSPrincipal defaultPrincipal = new IPSPrincipal(IPSIdentity.UnloggedUser, Guid.Empty, IPSBuiltInRole.User);
      private IPSIdentity identity;
      private Guid securityToken;
      private IPSBuiltInRole role;
      private string roleName;

      /// <summary>Создает объект.</summary>
      /// <param name="identity">Идентификационные данные пользователя IPS</param>
      /// <param name="securityToken">Маркер безопасности</param>
      /// <param name="role">Роль контекста безопасности пользователя IPS</param>
      public IPSPrincipal(IPSIdentity identity, Guid securityToken, IPSBuiltInRole role)
      {
        this.identity = identity != null ? identity : throw new ArgumentNullException(nameof (identity));
        this.securityToken = securityToken;
        this.role = role;
        this.roleName = role.ToString();
      }

      /// <summary>Возвращает идентификационные данные пользователя IPS.</summary>
      public IPSIdentity Identity
      {
        [DebuggerStepThrough] get => this.identity;
      }

      /// <summary>
      /// Возвращает маркер безопасности. Его значение соответствует глобальному идентификатору основной сессии пользователя IPS.
      /// </summary>
      public Guid SecurityToken
      {
        [DebuggerStepThrough] get => this.securityToken;
      }

      /// <summary>
      /// Возвращает роль контекста безопасности для пользователя IPS.
      /// </summary>
      public IPSBuiltInRole Role
      {
        [DebuggerStepThrough] get => this.role;
      }

      /// <summary>
      /// Возвращает true, если пользователь исполняет указанную роль.
      /// </summary>
      /// <param name="role">Роль пользователя IPS</param>
      /// <returns>true, если роль пользователя IPS соответствует указанной роли</returns>
      public bool IsInRole(IPSBuiltInRole role) => this.role == role;

      /// <summary>Возвращает идентификационные данные пользователя IPS.</summary>
      IIdentity IPrincipal.Identity
      {
        [DebuggerStepThrough] get => (IIdentity) this.identity;
      }

      /// <summary>
      /// Возвращает true, если пользователь исполняет указанную роль.
      /// </summary>
      /// <param name="roleName">Имя роли пользователя IPS</param>
      /// <returns>true, если роль пользователя IPS соответствует указанной роли</returns>
      public bool IsInRole(string roleName) => string.Compare(this.roleName, roleName, true) == 0;

      public virtual IPSPrincipal Clone()
      {
        return new IPSPrincipal(this.identity, this.securityToken, this.role);
      }

      object ICloneable.Clone() => (object) this.Clone();

      public bool Equals(IPSPrincipal other)
      {
        return other != null && other.identity.Equals(this.identity) && other.securityToken == this.securityToken;
      }

      public bool Equals(IPrincipal obj) => obj is IPSPrincipal other && this.Equals(other);

      public override bool Equals(object obj)
      {
        return !(obj is IPSPrincipal other) ? base.Equals(obj) : this.Equals(other);
      }

      public override int GetHashCode() => this.securityToken.GetHashCode();

      public static IPSPrincipal CurrentPrincipal
      {
        [DebuggerStepThrough] get
        {
          if (!(Thread.CurrentPrincipal is IPSPrincipal currentPrincipal))
            currentPrincipal = IPSPrincipal.DefaultPrincipal;
          return currentPrincipal;
        }
      }

      public static IPSPrincipal DefaultPrincipal
      {
        [DebuggerStepThrough] get => IPSPrincipal.defaultPrincipal;
        [DebuggerStepThrough] set
        {
          IPSPrincipal.defaultPrincipal = value != null ? value : throw new ArgumentNullException(nameof (value));
        }
      }
    }
}
