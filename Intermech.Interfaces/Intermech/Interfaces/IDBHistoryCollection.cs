
// Type: Intermech.Interfaces.IDBHistoryCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс истории значений атрибутов</summary>
    public interface IDBHistoryCollection : IDBRecords, IDBSessionable
    {
      /// <summary>
      /// Метод возвращает предварительно подготовленную таблицу для отображения истории изменений
      /// </summary>
      /// <param name="conditions">Условия фильтрации данных</param>
      /// <param name="lastKey">Последнее значение для пакетного чтения данных</param>
      /// <param name="recCount">Количество записей в пакете</param>
      /// <returns>Таблица</returns>
      DataTable Select(ConditionStructure[] conditions, long lastKey, int recCount);
    }
}
