
// Type: Intermech.Search.RelationObjectBase
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search
{
    /// <summary>Пара связь/объект</summary>
    [Serializable]
    public abstract class RelationObjectBase
    {
      /// <summary>Конструктор</summary>
      /// <param name="relation">Связь</param>
      /// <param name="object">Объект</param>
      public RelationObjectBase(Relation relation, _Object @object)
      {
        this.Relation = relation;
        this.Object = @object;
      }

      /// <summary>Связь</summary>
      public Relation Relation { get; private set; }

      /// <summary>Объект</summary>
      public _Object Object { get; private set; }
    }
}
