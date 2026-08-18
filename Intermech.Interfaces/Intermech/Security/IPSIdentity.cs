
// Type: Intermech.Security.IPSIdentity
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;
using System.Security.Principal;


namespace Intermech.Security
{
    /// <summary>Описывает идентификационные данные пользователя IPS.</summary>
    public sealed class IPSIdentity : IIdentity, ICloneable, IEquatable<IPSIdentity>
    {
      private static readonly IPSIdentity unloggedUser = new IPSIdentity(0L, "Unlogged IPS user");
      private long userId;
      private string userName;

      /// <summary>Создает объект.</summary>
      /// <param name="userId">Идентификатор пользователя IPS</param>
      /// <param name="userName">Имя пользователя IPS</param>
      public IPSIdentity(long userId, string userName)
      {
        if (userName == null)
          throw new ArgumentNullException(nameof (userName));
        this.userId = userId;
        this.userName = userName;
      }

      /// <summary>
      /// Возвращает идентификационные данные пользователя IPS, который не выполнил вход в IPS.
      /// </summary>
      public static IPSIdentity UnloggedUser
      {
        [DebuggerStepThrough] get => IPSIdentity.unloggedUser;
      }

      /// <summary>Возвращает идентификатор пользователя IPS.</summary>
      public long UserId
      {
        [DebuggerStepThrough] get => this.userId;
      }

      /// <summary>Возвращает имя пользователя IPS.</summary>
      public string UserName
      {
        [DebuggerStepThrough] get => this.userName;
      }

      /// <summary>Возвращает имя пользователя.</summary>
      public string Name
      {
        [DebuggerStepThrough] get => this.userName;
      }

      /// <summary>
      /// Возвращает способ проверки аутентичности пользователя.
      /// </summary>
      public string AuthenticationType
      {
        [DebuggerStepThrough] get => "IPS Security Token";
      }

      /// <summary>
      /// Возвращает true, если пользователь IPS прошел аутентификацию.
      /// </summary>
      public bool IsAuthenticated
      {
        [DebuggerStepThrough] get => this.userId != 0L && this.userId != -1L;
      }

      /// <summary>
      /// Возвращает true, если текущий объект равен указанному объекту.
      /// </summary>
      /// <param name="other">Объект для сравнения на равенство</param>
      /// <returns>true, если текущий объект равен указанному объекту</returns>
      public bool Equals(IPSIdentity other) => other != null && other.userId == this.userId;

      /// <summary>
      /// Возвращает true, если текущий объект равен указанному объекту.
      /// </summary>
      /// <param name="other">Объект для сравнения на равенство</param>
      /// <returns>true, если текущий объект равен указанному объекту</returns>
      public override bool Equals(object obj)
      {
        return !(obj is IPSIdentity other) ? base.Equals(obj) : this.Equals(other);
      }

      /// <summary>Возвращает хэш-код текущего объекта.</summary>
      /// <returns>Хэш-код текущего объекта</returns>
      public override int GetHashCode() => this.userId.GetHashCode();

      public IPSIdentity Clone() => new IPSIdentity(this.userId, this.userName);

      object ICloneable.Clone() => (object) this.Clone();
    }
}
