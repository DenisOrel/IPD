
// Type: Intermech.Interfaces.IDBRecords
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для получения из базы списка записей, отвечающих заданным условиям
    /// </summary>
    public interface IDBRecords : IDBSessionable
    {
      /// <summary>
      /// Получить таблицу со списком объектов, соответствующую paramSet
      /// </summary>
      DataTable Select(DBRecordSetParams paramSet);

      /// <summary>
      /// Удаляет элементы с идентификаторами idList и возвращает количество удаленных элементов.
      /// Если throwException == false, то давит исключения и продолжает удалять элементы.
      /// Если throwException == true, то удаление идет в одной транзакции.
      /// Параметр deleteMode передается в ф-цию Delete при удалении каждого элемента.
      /// </summary>
      int Delete(long[] idList, bool throwException, long deleteMode);

      /// <summary>
      /// Ф-ция позволяет определить есть ли в базе данных записи, соответствующие условиям conditions.
      /// </summary>
      /// <param name="conditions">Условия поиска записей.</param>
      /// <param name="tags">Доп. данные для передачи в DBRecordSetParams.Tags</param>
      /// <returns>Возвращает true, если в базе есть хотя бы одна запись, удовлетворяющая всем условиям.</returns>
      bool RecordsExists(ConditionStructure[] conditions, HybridDictionary tags);

      /// <summary>
      /// Ф-ция позволяет определить есть ли в базе данных записи, соответствующие условиям conditions.
      /// </summary>
      /// <param name="conditions">Условия поиска записей.</param>
      /// <returns>Возвращает true, если в базе есть хотя бы одна запись, удовлетворяющая всем условиям.</returns>
      bool RecordsExists(ConditionStructure[] conditions);

      /// <summary>
      /// Возвращает таблицу с результатами запроса Select, в которой значения полей, требующих расшифровки Description, заменены объектами ValueWithDescription, которые содержат и значение, и результат
      /// </summary>
      /// <param name="paramSet">Параметры запроса</param>
      /// <returns>Таблица с расшифрованными значениями</returns>
      DataTable SelectWithDescriptions(DBRecordSetParams paramSet);
    }
}
