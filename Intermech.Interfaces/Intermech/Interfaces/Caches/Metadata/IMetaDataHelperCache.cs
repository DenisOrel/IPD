
// Type: Intermech.Interfaces.Caches.Metadata.IMetaDataHelperCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces.Caches.Metadata
{
    /// <summary>
    /// Загрузка информации о коллекциях MetaDataHelper из фиксированного DataSet
    /// ( ONLY FOR PRIVATE (PUMP MODE) USES - для просмотра данных ранее сохраненного клиентского кеша )
    /// НЕ ИСПОЛЬЗОВАТЬ БЕЗ КРАЙНЕЙ НАДОБНОСТИ !!!!
    /// </summary>
    public interface IMetaDataHelperCache
    {
      /// <summary>
      /// Выполнить полную загрузку всех внутренних коллекций кэша метаданных.
      /// Метод выполняет ряд проверок, чтобы избежать лишних операций по работе с
      /// кэшем метаданных
      /// </summary>
      /// <param name="dataSet">Датасет с таблицами кэша метаданных</param>
      /// <param name="forced">true - принудительно загрузить</param>
      void LoadMetadata(DataSet dataSet, bool forced = true);
    }
}
