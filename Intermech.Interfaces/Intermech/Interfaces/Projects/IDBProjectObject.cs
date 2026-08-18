
// Type: Intermech.Interfaces.Projects.IDBProjectObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Projects
{
    /// <summary>Интерфейс обработчика проекта</summary>
    public interface IDBProjectObject : IObjectTemplater
    {
      /// <summary>Возвращает количество объектов в данном проекте</summary>
      long LinkedObjectsCount { get; }

      /// <summary>
      /// Возвращает список объектов, принадлежащих данному проекту
      /// </summary>
      long[] LinkedObjects { get; }

      /// <summary>Включает пользователей в состав участников проекта</summary>
      /// <param name="participants">Список добавляемых участников проекта</param>
      void IncludeParticipants(ProjectParticipantInfo[] participants);

      /// <summary>Исключает пользователей из состава участников проекта</summary>
      /// <param name="users">Список идентификаторов исключаемых из проекта пользователей</param>
      void ExcludeParticipants(long[] users);

      /// <summary>Возвращает список участников проекта</summary>
      /// <returns>Список участников проекта</returns>
      ProjectParticipantInfo[] GetParticipants();

      /// <summary>
      /// Возвращает список участников проекта с расширенной информацией по ним
      /// </summary>
      /// <returns>Список участников проекта</returns>
      ProjectParticipantInfoEx[] GetParticipantsInfo();

      /// <summary>
      /// Функция возвращает true, если пользователь userID является менеджером данного проекта
      /// </summary>
      /// <param name="userID">Идентификатор пользователя</param>
      /// <returns>true, если пользователь является менеджером данного проекта</returns>
      bool IsProjectManager(long userID);

      /// <summary>
      /// Функция возвращает true, если пользователь текущей сессии является менеджером данного проекта
      /// </summary>
      /// <returns>true, если пользователь текущей сессии является менеджером данного проекта</returns>
      bool IsProjectManager();

      /// <summary>
      /// Функция возвращает true, если пользователь userID является участником данного проекта
      /// </summary>
      /// <param name="userID">Идентификатор пользователя</param>
      /// <returns>true, если пользователь является участником данного проекта</returns>
      bool IsProjectParticipant(long userID);

      /// <summary>
      /// Функция возвращает true, если пользователь текущей сессии является участником данного проекта
      /// </summary>
      /// <returns>true, если пользователь текущей сессии является участником данного проекта</returns>
      bool IsProjectParticipant();
    }
}
