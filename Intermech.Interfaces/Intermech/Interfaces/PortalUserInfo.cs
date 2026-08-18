
// Type: Intermech.Interfaces.PortalUserInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Информация по пользователю портала</summary>
    public class PortalUserInfo
    {
      /// <summary>Отображаемое имя пользователя</summary>
      public string Name;
      /// <summary>Логин</summary>
      public string Login;
      /// <summary>Пароль</summary>
      public string Password;

      public PortalUserInfo()
      {
        this.Name = string.Empty;
        this.Login = string.Empty;
        this.Password = string.Empty;
      }

      public PortalUserInfo(string name, string login, string password)
      {
        this.Name = name;
        this.Login = login;
        this.Password = password;
      }
    }
}
