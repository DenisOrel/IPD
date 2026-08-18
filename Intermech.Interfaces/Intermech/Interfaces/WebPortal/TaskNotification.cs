
// Type: Intermech.Interfaces.WebPortal.TaskNotification
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Уведомление об ошибке в задаче</summary>
    public class TaskNotification
    {
      /// <summary>Получатель</summary>
      public string User;
      /// <summary>Адрес электронной почты</summary>
      public string Email;
      /// <summary>Разрешено</summary>
      public bool Enable;

      /// <summary>Конструктор</summary>
      /// <param name="user">Получатель</param>
      /// <param name="email">Адрес электронной почты</param>
      /// <param name="enable">Разрешено</param>
      public TaskNotification(string user, string email, bool enable)
      {
        this.User = user;
        this.Email = email;
        this.Enable = enable;
      }
    }
}
