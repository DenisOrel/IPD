
// Type: Intermech.Interfaces.Snapshots.IDBObjectSnapshot
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.Snapshots
{
    /// <summary>Обработчик итерации объекта</summary>
    public interface IDBObjectSnapshot
    {
      /// <summary>
      /// Возвращает таблицу с дополнительными атрибутами итерации для версии объекта objectID
      /// </summary>
      DataTable GetAttributes(long objectID);

      /// <summary>
      /// Возвращает список версий объектов в данной итерации, отсортированный по полям OrderBy
      /// </summary>
      DataTable ConsistFrom(string orderBy);

      /// <summary>
      /// Возвращает состав объекта projID в данной итерации, причем в составе присутствуют только версии объектов, которые также сохранены в этой итерации
      /// </summary>
      DataTable ConsistFromSnapshotObjects(long projID);

      /// <summary>
      /// Метод возвращает список версий объектов, которые система не сможет изменить при восстановлении итерации данным пользователем.
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта, в которую будут восстанавливать итерацию.</param>
      /// <returns>Список идентификаторов версий объектов, которые не может изменить данный пользователь.</returns>
      List<long> GetReadOnlyObjects(long objectID);

      /// <summary>
      /// Восстанавливает состояние объекта версии objectID из данной итерации
      /// </summary>
      void SaveToObject(long objectID);

      /// <summary>
      /// Восстанавливает состояние объекта версии objectID из данной итерации
      /// </summary>
      /// <param name="objectID">Ид. версий объекта, в которую будет восстановлена данная итерация.</param>
      /// <param name="abortOnError">Прерывать ли восстановление всей итерации, если в итерации существуют версии объектов, которые не могут быть восстановлены.</param>
      void SaveToObject(long objectID, bool abortOnError);

      /// <summary>
      /// Метод сохраняет указанные версии объектов в данную итерацию, заменяя её содержимое.
      /// </summary>
      /// <param name="objectIDs">Список идентификаторов версий объектов (не должен содержать повторяющихся значений!).</param>
      /// <param name="FiltrationOwnerID">Правило подбора версий для случая, когда нужно включать в итерацию другие объекты из состава.</param>
      void SaveToSnapshot(List<long> objectIDs, string FiltrationOwnerID);

      /// <summary>
      /// Возвращает список версий объектов, сохранённых в данной итерации
      /// </summary>
      /// <returns>Список идентификаторов версий объектов, сохранённых в данной итерации.</returns>
      List<long> GetObjectsList();

      /// <summary>// Удаляет итерацию</summary>
      int Delete(long DeleteMode);

      /// <summary>
      /// Удаляет из таблиц итераций запись о версии объекта obj для данной итерации
      /// </summary>
      int DeleteObject(IDBObject obj);

      /// <summary>Идентификатор итерации</summary>
      long SnapshotID { get; }

      /// <summary>Наименование итерации</summary>
      string SnapshotName { get; set; }

      /// <summary>
      /// Идентификатор версии объекта, к которой относится итерация
      /// </summary>
      long ObjectID { get; }

      /// <summary>Идентификатор объекта, к которому относится итерация</summary>
      long ID { get; }

      /// <summary>Дата и время последней модификации итерации</summary>
      DateTime SnapshotModifyDate { get; }

      /// <summary>Владелец итерации</summary>
      long SnapshotOwnerID { get; }
    }
}
