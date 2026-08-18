
// Type: Intermech.Search.RelationObjectCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Search
{
    /// <summary>Коллекция связей/объектов</summary>
    public sealed class RelationObjectCollection : List<RelationObject>
    {
      /// <summary>Конструктор</summary>
      public RelationObjectCollection()
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="collection">Коллекция</param>
      public RelationObjectCollection(IEnumerable<RelationObject> collection)
        : base(collection)
      {
      }
    }
}
