
// Type: Intermech.Interfaces.RelationAttributeValues
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, описывающий связь и ее атрибуты, которые нужно данной связи присвоить
    /// </summary>
    [Serializable]
    public class RelationAttributeValues
    {
      /// <summary>Ид. связи</summary>
      public long RelationID;
      /// <summary>Ид. версии дочернего объекта</summary>
      public long PartObjectID;
      /// <summary>Значения атрибутов связи</summary>
      public AttributeValues[] Values;

      public RelationAttributeValues(long relationID, long partObjectID, AttributeValues[] values)
      {
        this.RelationID = relationID;
        this.PartObjectID = partObjectID;
        this.Values = values;
      }
    }
}
