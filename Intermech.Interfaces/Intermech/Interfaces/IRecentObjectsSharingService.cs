
// Type: Intermech.Interfaces.IRecentObjectsSharingService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Служба для шаринга списков недавних объектов между пользователями
    /// </summary>
    public interface IRecentObjectsSharingService
    {
      /// <summary>
      /// Возвращает список объектов (юзеров, ролей и групп), которым предоставлен доступа к списку недавних объектов для юзера сессии sessionGuid
      /// </summary>
      /// <param name="sessionGuid">Гуид юзерской сессии</param>
      /// <returns>Текущая настройка</returns>
      long[] GetAccessObjectIDs(Guid sessionGuid);

      /// <summary>
      /// Устанавливает новый список идентификатов пользователей, групп и ролей, которые имеют доступ к списку недавних объектов юзера сессии sessionGuid
      /// </summary>
      /// <param name="sessionGuid">Гуид юзерской сессии</param>
      /// <param name="userGrpIDs">Массив ObjectID объектов, которым предоставлен доступ</param>
      void SetAccessObjectIDs(Guid sessionGuid, long[] userGrpIDs);

      /// <summary>
      /// Ф-ция проверяет можно ли юзеру с сессией user_session получить список недавних объектов юзера userID
      /// </summary>
      /// <param name="sessionGuid">Гуид сессии, которая запросила список объектов другого пользователя</param>
      /// <param name="userID">Ид. юзера, чей список нужно получить</param>
      void ValidateAccessMode(Guid sessionGuid, long userID);
    }
}
