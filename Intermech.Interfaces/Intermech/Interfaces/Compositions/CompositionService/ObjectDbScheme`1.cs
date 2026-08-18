
// Type: Intermech.Interfaces.Compositions.CompositionService.ObjectDbScheme`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Interfaces.Compositions.CompositionService
{
    /// <summary>
    /// Базовый класс схемы / модели для загрузки данных объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObjectDbScheme<T>
    {
      /// <summary>Создание / загрузка содержимого объекта</summary>
      /// <param name="dataRow"></param>
      /// <returns></returns>
      public abstract T ParseItem([NotNull] DataRow dataRow);

      /// <summary>Загрузка данных из таблицы в список объектов</summary>
      /// <param name="dataRows"></param>
      /// <returns></returns>
      public virtual IEnumerable<T> ParseItems([NotNull] IEnumerable<DataRow> dataRows)
      {
        return dataRows.Select<DataRow, T>(new System.Func<DataRow, T>(this.ParseItem));
      }

      /// <summary>Загрузка данных из таблицы в список объектов</summary>
      /// <param name="dataRows">Исходная таблица с данными</param>
      /// <param name="objInfoItems">Результирующий список</param>
      /// <returns></returns>
      public virtual bool ParseItems(IEnumerable<DataRow> dataRows, ICollection<T> objects)
      {
        if (dataRows == null || objects == null)
          return false;
        int count = objects.Count;
        foreach (T obj in this.ParseItems(dataRows))
          objects.Add(obj);
        return objects.Count != count;
      }
    }
}
