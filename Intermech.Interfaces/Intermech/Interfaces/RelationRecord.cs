
// Type: Intermech.Interfaces.RelationRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Связь (используется при перекачке данных)</summary>
    [Serializable]
    public class RelationRecord
    {
      /// <summary>Идентификатор связи</summary>
      public long PrjLinkId;
      /// <summary>GUID связи</summary>
      public object PrjLinkGuid;
      /// <summary>GUID или Id версии в которую ...</summary>
      public object ProjId;
      /// <summary>GUID или Id объекта, который ...</summary>
      public object PartId;
      /// <summary>Тип связи</summary>
      public int RelationType;
      /// <summary>Дата начала</summary>
      public object CreateDate;
      /// <summary>Идентификатор создателя</summary>
      public long CreatorID;

      public RelationRecord()
      {
      }

      public RelationRecord(
        long prjLinkId,
        object prjLinkGuid,
        object projId,
        object partId,
        int relationType,
        object createDate,
        long creatorId)
      {
        this.PrjLinkId = prjLinkId;
        this.PrjLinkGuid = prjLinkGuid;
        this.ProjId = projId;
        this.PartId = partId;
        this.RelationType = relationType;
        this.CreateDate = createDate;
        this.CreatorID = creatorId;
      }
    }
}
