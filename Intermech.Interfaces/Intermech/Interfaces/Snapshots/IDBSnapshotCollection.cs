
// Type: Intermech.Interfaces.Snapshots.IDBSnapshotCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.Snapshots
{
    /// <summary>Коллекция итераций</summary>
    public interface IDBSnapshotCollection
    {
      /// <summary>
      /// Создает итерацию с именем snapshotName на основе версии объекта objectID и возвращает ее идентификатор.
      /// </summary>
      long Create(long objectID, string snapshotName, string FiltrationOwnerID);

      /// <summary>
      /// Создает итерацию с именем snapshotName на основе версии объекта objectID и добавляет в неё версии объектов addObjectsID
      /// </summary>
      /// <param name="objectID">Ид. версии основного объекта, для которого создают итерацию.</param>
      /// <param name="snapshotName">Наименование итерации.</param>
      /// <param name="FiltrationOwnerID">Правило подбора версий для случая, когда нужно включать в итерацию другие объекты из состава.</param>
      /// <param name="addObjectsID">Список дополнительных объектов, которые нужно включить в итерацию.</param>
      /// <returns></returns>
      long Create(long objectID, string snapshotName, string FiltrationOwnerID, long[] addObjectsID);

      /// <summary>
      /// Добавляет версию объекта objectID в итерацию номер snapshotID.
      /// FiltrationOwnerID - правило подбора версий для получения состава.
      /// createdObjects - список версий объектов, которые уже создавались при создании итерации - чтобы не делать лишних версий в итерации
      /// </summary>
      void AddObjectToSnapshot(
        long objectID,
        long snapshotID,
        string snapshotName,
        string FiltrationOwnerID,
        List<long> createdObjects);

      /// <summary>
      /// Возвращает таблицу со списком итераций для объекта с идентификатором id
      /// </summary>
      DataTable GetObjectSnapshots(long id, string orderBy);

      /// <summary>
      /// Возвращает таблицу со списком итераций для версии объекта с идентификатором версии objectID
      /// </summary>
      DataTable GetObjectVersionSnapshots(long objectID, string orderBy);

      /// <summary>
      /// Возвращает таблицу со списком итераций для объекта с идентификатором id для визуального отображения пользователям.
      /// </summary>
      DataTable GetObjectSnapshotsEx(long id, string orderBy);

      /// <summary>
      /// Возвращает таблицу со списком итераций для версии объекта с идентификатором версии objectID для визуального отображения пользователям.
      /// </summary>
      DataTable GetObjectVersionSnapshotsEx(long objectID, string orderBy);
    }
}
