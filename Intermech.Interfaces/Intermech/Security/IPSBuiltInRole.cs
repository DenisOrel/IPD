
// Type: Intermech.Security.IPSBuiltInRole
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Security
{
    /// <summary>
    /// Описывает роли безопасности в IPS, которые могут быть использованы в методе IsInRole.
    /// </summary>
    public enum IPSBuiltInRole
    {
      /// <summary>Обычный пользователь</summary>
      User,
      /// <summary>Администратор</summary>
      Administrator,
      /// <summary>Сервер приложений</summary>
      Server,
    }
}
