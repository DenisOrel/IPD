
// Type: Intermech.Interfaces.IDBCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для работы со списом метаданных.</summary>
    public interface IDBCollection
    {
      /// <summary>
      /// Получить DataTable со списком объектов базы данных произвольной категории,
      /// с сортировкой по полям orderBy. addInfo содержат дополнительные управляющие
      /// параметры в зависимости от типа объекта, который реализует этот интерфейс
      /// </summary>
      DataTable Select(string orderBy, params object[] addInfo);

      /// <summary>Количество объектов в списке (только для чтения).</summary>
      long Count { get; }

      /// <summary>
      /// Идентификатор родительского объекта, в который входит данная подвыборка объектов
      /// </summary>
      object ParentID { get; set; }

      /// <summary>
      /// Возвращает список идентификаторов типов, которые видны текущему пользователю в фильтрованных
      /// списках (используется кэшем клиентской части). Поддерживается не всеми!
      /// </summary>
      int[] GetVisibleList();
    }
}
