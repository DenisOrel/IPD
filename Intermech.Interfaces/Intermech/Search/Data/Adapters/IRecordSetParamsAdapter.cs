
// Type: Intermech.Search.Data.Adapters.IRecordSetParamsAdapter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;


namespace Intermech.Search.Data.Adapters
{
    public interface IRecordSetParamsAdapter
    {
      /// <summary>Получить индекс колонки запроса</summary>
      /// <param name="obligatoryObjectAttribute">Системный атрибут</param>
      /// <returns>Индекс колонки запроса</returns>
      int GetColumnIndex(
        ObligatoryObjectAttributes obligatoryObjectAttribute);

      /// <summary>Получить индекс колонки запроса</summary>
      /// <param name="attributeTypeID">Идентификатор типа аттрибута</param>
      /// <returns>Индекс колонки запроса</returns>
      int GetColumnIndex(int attributeTypeID);
    }
}
