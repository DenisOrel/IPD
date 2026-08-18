
// Type: Intermech.Interfaces.IFormDesignerServer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для подписывание на обновление списка форм</summary>
    public interface IFormDesignerServer
    {
      /// <summary>Регистрация обработчика на обновление списка форм.</summary>
      /// <param name="typeID">ID типа объекта/связи</param>
      /// <param name="kind">Вид для ID типа</param>
      /// <param name="handler">Обработчик</param>
      void Register(int typeID, AttributableElements kind, UpdateHandlerInfo handler);

      /// <summary>Регистрация обработчика на обновление списка форм.</summary>
      /// <param name="typeID">ID типа объекта/связи</param>
      /// <param name="kind">Вид для ID типа</param>
      /// <param name="handler">Обработчик</param>
      /// <param name="session">Сессия сервера, если есть</param>
      void Register(
        int typeID,
        AttributableElements kind,
        UpdateHandlerInfo handler,
        IUserSession session);
    }
}
