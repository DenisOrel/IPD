
// Type: Intermech.Interfaces.UserSessionReleaseMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Режимы освобождения сессии распределителем сессий</summary>
    public enum UserSessionReleaseMode
    {
      /// <summary>Нормальный режим освобождения сессии</summary>
      Normal,
      /// <summary>
      /// Сессия не может быть переиспользована и должна быть отброшена.
      /// Кроме того, запрещено обращаться к этой сессии, так как
      /// это может привести к повреждению сервера приложений
      /// </summary>
      Drop,
    }
}
