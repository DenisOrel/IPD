
// Type: Intermech.Interfaces.QuickObjectInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура с краткой информацией о версии объекта. Используется для кэширования данных
    /// об объектах на серверной и клиентской сторонах при вызове функции IUserSession.GetObjectInfo
    /// </summary>
    [Serializable]
    /// <summary>Создать экземпляр класса</summary>
    /// <param name="objectID">Идентификатор версии объекта</param>
    /// <param name="caption">Заголовок объекта</param>
    /// <param name="objectTypeID">Тип объекта</param>
    /// <param name="obj_guid">Guid версии объекта</param>
    /// <param name="id">Идентификатор объекта</param>
    public struct QuickObjectInfo(
      long objectID,
      string caption,
      int objectTypeID,
      Guid obj_guid,
      long id,
      DateTime loadTime)
    {
      /// <summary>Заголовок объекта</summary>
      public string Caption = caption;
      /// <summary>Тип объекта</summary>
      public int ObjectTypeID = objectTypeID;
      /// <summary>Идентификатор версии объекта</summary>
      public long ObjectID = objectID;
      /// <summary>Глобальный идентификатор версии объекта</summary>
      public Guid VersionGuid = obj_guid;
      /// <summary>Идентификатор объекта</summary>
      public long ID = id;
      /// <summary>
      /// Дата и время загрузки указанной записи из базы данных (UTC)
      /// (поле не сериализуется, используется в клиентском кэше)
      /// </summary>
      [NonSerialized]
      public DateTime LoadTime = loadTime;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="caption">Заголовок объекта</param>
      /// <param name="objectTypeID">Тип объекта</param>
      /// <param name="obj_guid">Guid версии объекта</param>
      /// <param name="id">Идентификатор объекта</param>
      public QuickObjectInfo(long objectID, string caption, int objectTypeID, Guid obj_guid, long id)
        : this(objectID, caption, objectTypeID, obj_guid, id, DateTime.UtcNow)
      {
      }

      /// <summary>
      /// Возвращает true, если структура пустая (например, в базе не найден объект ObjectID)
      /// </summary>
      public bool Empty
      {
        [DebuggerStepThrough] get => this.ObjectTypeID == -1;
      }
    }
}
