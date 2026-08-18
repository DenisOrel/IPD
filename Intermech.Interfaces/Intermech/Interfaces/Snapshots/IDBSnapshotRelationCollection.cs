
// Type: Intermech.Interfaces.Snapshots.IDBSnapshotRelationCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces.Snapshots
{
    /// <summary>Интерфейс коллекции связей итерации</summary>
    public interface IDBSnapshotRelationCollection
    {
      /// <summary>Метод возвращает все связи текущей итерации</summary>
      /// <param name="relationTypeID">Тип связей (если меньше 0 возвращает связи всех типов)</param>
      /// <returns>Таблица связей</returns>
      DataTable Select(int relationTypeID);
    }
}
