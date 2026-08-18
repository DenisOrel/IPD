
// Type: Intermech.Interfaces.IUserFavouritesService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс серверной службы для работы с нодом Избранное
    /// </summary>
    public interface IUserFavouritesService
    {
      /// <summary>
      /// Добавляет версии объектов в папку Избранное пользователя сессии sessionGUID, если этих версий там еще нет.
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии юзера</param>
      /// <param name="objectIDs">Идентификаторы версий объектов</param>
      void IncludeObjects(Guid sessionGUID, long[] objectIDs);

      /// <summary>
      /// Удаляет версии объектов в папку Избранное пользователя сессии sessionGUID, если этих версий там еще нет.
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии юзера</param>
      /// <param name="objectIDs">Идентификаторы версий объектов</param>
      void ExcludeObjects(Guid sessionGUID, long[] objectIDs);

      /// <summary>Очищает папку Избранное пользователя sessionGUID</summary>
      /// <param name="sessionGUID">Гуид сессии пользователя</param>
      void ClearFavourites(Guid sessionGUID);

      /// <summary>
      /// Возвращает массив идентификаторов типов объектов, добавленных в папку Избранное данного юзера
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии пользователя</param>
      /// <returns>Массив идентификаторов типов объектов</returns>
      int[] GetObjectTypes(Guid sessionGUID);

      /// <summary>
      /// Добавляет тип объектов в список отображаемых в папке Избранное типов
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии пользователя</param>
      /// <param name="objectTypeID">Ид. типа объектов</param>
      void AddObjectType(Guid sessionGUID, int objectTypeID);

      /// <summary>
      /// Удаляет тип объектов из списка отображаемых в папке Избранное типов
      /// </summary>
      /// <param name="sessionGUID">Гуид сессии пользователя</param>
      /// <param name="objectTypeID">Ид. типа объектов</param>
      void DeleteObjectType(Guid sessionGUID, int objectTypeID);
    }
}
