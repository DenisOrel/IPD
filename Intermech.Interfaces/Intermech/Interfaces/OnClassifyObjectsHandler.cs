
// Type: Intermech.Interfaces.OnClassifyObjectsHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Обработчик события классификации объектов</summary>
    /// <param name="session">Сессия</param>
    /// <param name="classifier">Классификатор</param>
    /// <param name="folder">Папка, куда классифицируют объекты</param>
    /// <param name="objectsID">Идентификаторы классифицируемых объектов</param>
    public delegate void OnClassifyObjectsHandler(
      IUserSession session,
      IDBObject classifier,
      IDBObject folder,
      long[] objectsID);
}
