
// Type: Intermech.Interfaces.IDBRelationTypeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    public interface IDBRelationTypeCollection : IDBCollection
    {
      /// <summary>
      /// Создает новый тип связей и возвращает его идентификатор
      /// </summary>
      int Create(RelationTypeProperties relationProperties);

      /// <summary>
      /// Возвращает список типов связей, для которых задано использование атрибута номер attributeID
      /// </summary>
      DataTable GetUsedByAttribute(int attributeID);
    }
}
