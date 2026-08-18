
// Type: Intermech.Interfaces.IDBTimedEventCallback
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, который должен поддерживать сервис для того, чтобы его смогла
    /// вызвать служба временнЫх событий.
    /// </summary>
    public interface IDBTimedEventCallback
    {
      /// <summary>
      /// Этот метод вызывает служба временнЫх событий в момент наступления даты и
      /// времени соответствующего события. Служба событий находит по GUIDу службу-
      /// обработчик события, запрашивает данный интерфейс и при его наличии вызывает
      /// данный метод интерфейса. Если результат 0, то событие успешно выполнилось и оно
      /// удаляется из списка событий. Иначе событие откладывается на столько секунд и
      /// уменьшается счетчик попыток вызова события.
      /// </summary>
      /// <param name="EventDate">Заданная дата срабатывания события.</param>
      /// <param name="StringInfo">Строковая информация по событию</param>
      /// <param name="IntInfo">Дополнительная числовая информация по событию</param>
      /// <param name="UserID">Пользователь, к которому относится данное событие (если
      /// это имеет значение)</param>
      /// <param name="ObjectID">Идентификатор версии объекта, к которому относится
      /// событие</param>
      long DoEvent(DateTime EventDate, string StringInfo, long IntInfo, long UserID, long ObjectID);
    }
}
