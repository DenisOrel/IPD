
// Type: Intermech.Interfaces.IMServerLoginMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Режим авторизации пользователей (Normal - с проверкой пароля в базе данных IPS, WindowsLogin - по имени юзера, зашедшего в винду, DomainLogin - по доменному SID-у юзера)
    /// </summary>
    public enum IMServerLoginMode
    {
      Normal,
      WindowsLogin,
      DomainLogin,
      DomainOnlyLogin,
    }
}
