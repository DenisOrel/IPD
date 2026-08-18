
// Type: Intermech.Interfaces.WebPortal.PublishCompositionRelation
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Информация по публикуемой связи</summary>
    [Serializable]
    public class PublishCompositionRelation : IIncludeTyped
    {
      /// <summary>Идентификатор связи</summary>
      public long PrjLinkID;
      /// <summary>Идентификатор версии дочернего объекта</summary>
      public Guid PartGuid;
      /// <summary>Тип связей</summary>
      public int RelationType;

      /// <summary>Тип включения в итоговый список этого объекта</summary>
      public IncludeTypes Include { get; set; }

      public PublishCompositionRelation(long prjLinkID, Guid partGuid, int relationType)
      {
        this.PrjLinkID = prjLinkID;
        this.PartGuid = partGuid;
        this.RelationType = relationType;
        this.Include = IncludeTypes.Include;
      }
    }
}
