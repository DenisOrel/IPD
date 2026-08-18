
// Type: Intermech.Interfaces.Objects.IBlobStorageObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces.Objects
{
    /// <summary>Клиентский интерфейс файлового шкафа</summary>
    public interface IBlobStorageObject : IDBRecords, IDBSessionable
    {
      /// <summary>
      /// Возвращает с сервера статистическую информацию о файловом шкафу
      /// </summary>
      FileStorageInfo GetFileStorageInfo();

      /// <summary>
      /// Перемещает файлы с идентификаторами fileIDs из данного файлового шкафа в файловый шкаф номер toStorageID
      /// </summary>
      bool RemoveFiles(long[] fileIDs, long toStorageID);

      /// <summary>
      /// Возвращает с сервера информацию
      /// об истории изменения файлов объекта
      /// </summary>
      DataTable GetObjectHistory(long id);

      /// <summary>
      /// Возвращает с сервера информацию
      /// об истории изменения файлов версии объекта
      /// </summary>
      DataTable GetVersionHistory(long objectID);

      /// <summary>
      /// получить историю изменения файла
      /// (по имени файла)
      /// </summary>
      /// <returns></returns>
      DataTable GetFileHistory(string fileName, long objectID);

      /// <summary>
      /// получить историю изменения файла
      /// (по blobID)
      /// </summary>
      /// <param name="blobID">id файла</param>
      /// <param name="objectID">id версии объекта</param>
      /// <returns></returns>
      DataTable GetFileHistory(long blobID, long objectID);
    }
}
