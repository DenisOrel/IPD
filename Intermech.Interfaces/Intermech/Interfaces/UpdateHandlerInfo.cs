
// Type: Intermech.Interfaces.UpdateHandlerInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Класс для описания обновлений форм.</summary>
    public class UpdateHandlerInfo
    {
      /// <summary>Конструктор.</summary>
      /// <param name="order">порядок вызова</param>
      /// <param name="handler">вызываемое действие</param>
      public UpdateHandlerInfo(int order, UpdateHandler handler)
      {
        this.Order = order;
        this.Handler = handler;
      }

      /// <summary>Вызываемое действие.</summary>
      public UpdateHandler Handler { get; }

      /// <summary>Метод обновления.</summary>
      public int Order { get; }
    }
}
