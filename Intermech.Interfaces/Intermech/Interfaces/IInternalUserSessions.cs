
// Type: Intermech.Interfaces.IInternalUserSessions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с сессиями специальных пользователей IPS, предназначенных для функционирования внутренних служб IPS.
    /// </summary>
    public interface IInternalUserSessions
    {
      /// <summary>
      /// Функция возвращает отчёт о правах, которыми обладают пользователи usersID над объектами objectsID
      /// </summary>
      /// <param name="userSession">Сессия юзера, которая хочет получить отчет</param>
      /// <param name="usersID">Массив идентификаторов пользователей, для которых нужно получить отчет</param>
      /// <param name="objectsID">Массив ObjectID объектов, для которых нужно получить отчет</param>
      /// <returns>Отчет в виде массива строк</returns>
      string[] GetAccessReport(Guid userSession, long[] usersID, long[] objectsID);
    }
}
