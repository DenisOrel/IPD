
// Type: Intermech.Interfaces.IDBMetadataInfoCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для получения информации о метаданных</summary>
    public interface IDBMetadataInfoCollection
    {
      /// <summary>
      /// Возвращает таблицу с объектами входящими в состав parentID и отсортированными по orderBy
      /// </summary>
      DataTable Select(string orderBy, params object[] addInfo);
    }
}
